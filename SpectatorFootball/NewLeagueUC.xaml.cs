using System.Windows.Media.Imaging;
using System.Windows;
using System.Windows.Input;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Controls;
using System;
using System.ComponentModel;
using System.IO;
using log4net;

namespace SpectatorFootball
{
    public partial class NewLeagueUC : UserControl
    {
        private static ILog logger = LogManager.GetLogger("RollingFile");
        // pw is the parent window mainwindow
        private MainWindow pw;
        private List<TeamMdl> st_list;
        private ListBox dragSource = null;
        private StackPanel drag_data = null;
        private string drag_from = null;
        private Style UnselNewTeamSP = (Style)System.Windows.Application.Current.FindResource("UnselNewTeamSP");
        private Style DragEnt_NewTeamSP = (Style)System.Windows.Application.Current.FindResource("DragEnt_NewTeamSP");
        public Point startPoint { get; set; }

        public event EventHandler Show_MainMenu;
        public event EventHandler<teamEventArgs> Show_NewTeam;

        public BackgroundWorker bw = null;
        public Progress_Dialog pop = new Progress_Dialog();

        public NewLeagueUC(MainWindow pw, List<TeamMdl> st_list)
        {

            // This call is required by the designer.
            InitializeComponent();

            this.pw = pw;
            this.pw.League = new Leaguemdl();
            this.st_list = st_list;

            int icurrentyear = DateTime.Today.Year;

            for (int i = icurrentyear - 100; i <= icurrentyear + 100; i++)
                newl1StartingYear.Items.Add(i.ToString());

            newl1StartingYear.Text = icurrentyear.ToString();
            newl1Structure.SelectedIndex = 0;

            setStockTeams();
        }
        private void setStockTeams()
        {
            StockTeamsGrid.Items.Clear();

            foreach (var st in st_list)
            {
                var h_sp = new StackPanel();
                h_sp.Orientation = Orientation.Horizontal;

                var BitmapImage = new BitmapImage(new Uri(CommonUtils.getAppPath() + App_Constants.APP_HELMET_FOLDER + st.Helmet_img_path));
                var helmet_img = new Image();
                helmet_img.Width = 25;
                helmet_img.Height = 25;
                helmet_img.Source = BitmapImage;

                List<Uniform_Color_percents> Color_Percents_List = null;
                Color_Percents_List = Uniform.getColorList(st.Uniform);

                var team_label = new Label();
                team_label.Foreground = new SolidColorBrush(CommonUtils.getColorfromHex(Color_Percents_List[0].color_string));
                team_label.Content = st.City + " " + st.Nickname;
                team_label.Height = 25;
                team_label.Width = 180;
                team_label.VerticalContentAlignment = VerticalAlignment.Center;

                if (Color_Percents_List.Count > 2)
                {
                    var BackBrush = new LinearGradientBrush();
                    BackBrush.StartPoint = new Point(0, 0);
                    BackBrush.EndPoint = new Point(1, 1);

                    float running_value = 0;
                    for (int i = 1; i <= Color_Percents_List.Count - 1; i++)
                    {
                        BackBrush.GradientStops.Add(new GradientStop(CommonUtils.getColorfromHex(Color_Percents_List[i].color_string), running_value));

                        running_value = Color_Percents_List[i].value;

                        BackBrush.GradientStops.Add(new GradientStop(CommonUtils.getColorfromHex(Color_Percents_List[i].color_string), running_value));
                    }
                    team_label.Background = BackBrush;
                }
                else
                    team_label.Background = new SolidColorBrush(CommonUtils.getColorfromHex(Color_Percents_List[1].color_string));
                team_label.FontFamily = new FontFamily("Times New Roman");
                team_label.FontSize = 12;

                h_sp.Children.Add(helmet_img);
                h_sp.Children.Add(team_label);
                // h_sp.Children.Add(std_img)
                h_sp.Margin = new Thickness(5);

                StockTeamsGrid.Items.Add(h_sp);
            }
        }


        private void validate()
        {
            int dummy_int;
            string DIRPath;
            DIRPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), App_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + newl1shortname.Text);

            if (CommonUtils.isBlank(newl1shortname.Text))
                throw new Exception("League Short Name must be supplied!");
            if (!CommonUtils.isAlpha(newl1shortname.Text, false))
                throw new Exception("Invalid character in League Short Name!");

            if (CommonUtils.isBlank(newl1longname.Text))
                throw new Exception("League Long Name must be supplied!");
            if (!CommonUtils.isAlpha(newl1longname.Text, true))
                throw new Exception("Invalid character in League Long Name!");

            if (CommonUtils.isBlank(newl1championshipgame.Text))
                throw new Exception("Championship Game must be supplied!");
            if (!CommonUtils.isAlpha(newl1championshipgame.Text, true))
                throw new Exception("Invalid character in Championship Game!");

            if (CommonUtils.isBlank(newlnumweeks.Text) || !int.TryParse(newlnumweeks.Text, out dummy_int))
                throw new Exception("Invalid Value for Number of Weeks!");
            if (CommonUtils.isBlank(newlnumgames.Text) || !int.TryParse(newlnumgames.Text, out dummy_int))
                throw new Exception("Invalid Value for Number of Games!");
            if (CommonUtils.isBlank(newlnumdivisions.Text) || !int.TryParse(newlnumdivisions.Text, out dummy_int))
                throw new Exception("Invalid Value for Number of Divisions!");
            if (CommonUtils.isBlank(newlnumteams.Text) || !int.TryParse(newlnumteams.Text, out dummy_int))
                throw new Exception("Invalid Value for Number of Teams!");
            if (CommonUtils.isBlank(newlnumplayoffteams.Text) || !int.TryParse(newlnumplayoffteams.Text, out dummy_int))
                throw new Exception("Invalid Value for Number of Playoff Teams");

            for (int i = 1; i <= Convert.ToInt32(newlnumconferences.Text); i++)
            {
                string conftxtname = "newlConf" + i.ToString();
                TextBox conftxtbox = (TextBox) this.FindName(conftxtname);

                if (conftxtname == null || conftxtbox.Text == "")
                    throw new Exception("Conference name " + i.ToString() + " must be supplied!");

                if (!CommonUtils.isAlpha(conftxtbox.Text, true))
                    throw new Exception("Invalid character in Conference Name " + conftxtbox.Text + "!");
            }

            for (int i = 1; i <= Convert.ToInt32(newlnumdivisions.Text); i++)
            {
                string divtxtname = "newldiv" + i.ToString();
                TextBox divtxtbox = (TextBox) this.FindName(divtxtname);

                if (divtxtbox == null || divtxtbox.Text.Trim().Length == 0)
                    throw new Exception("A name for division " + i.ToString() + " must be supplied!");

                if (!CommonUtils.isAlpha(divtxtbox.Text, true))
                    throw new Exception("Invalid character in Division Name " + divtxtbox.Text + "!");
            }

            for (int i = 1; i <= Convert.ToInt32(newlnumdivisions.Text); i++)
            {
                string teamlabel = "newllblTeam" + i.ToString();
                Label tlabel = (Label) this.FindName(teamlabel);
                if (tlabel.Content.ToString() == App_Constants.EMPTY_TEAM_SLOT)
                    throw new Exception("All Empty Team Slots must be filled with a team!");
            }

            var ts = new Team_Services();
            string first_dup_team = ts.FirstDuplicateTeam(pw.League.Teams);
            if (first_dup_team != null)
                throw new Exception("Duplicate team " + first_dup_team + " found in league!  Leagues can not have duplicate teams!");

            if (Directory.Exists(DIRPath))
                throw new Exception("League " + newl1shortname.Text + " already exists!");
        }

        private void newl1Structure_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem cbitem = (ComboBoxItem)newl1Structure.SelectedItem;
            int[] v = CommonUtils.getLeagueStructure(cbitem.Content.ToString());
            int num_weeks;
            int num_games;
            int num_divs;
            int num_teams;
            int num_confs;
            int num_playoff_teams;
            int last_div_first_group;
            int teams_per_division;
            int j;
            Style Largelblstyle = (Style)System.Windows.Application.Current.FindResource("Largelbltyle");
            Style GroupBoxstyle = (Style)System.Windows.Application.Current.FindResource("GroupBoxstyle");
            Style Largetxttyle = (Style)System.Windows.Application.Current.FindResource("Largetxttyle");
            Style Conflbltyle = (Style)System.Windows.Application.Current.FindResource("Conflbltyle");
            Style UnselNewTeamSP = (Style)System.Windows.Application.Current.FindResource("UnselNewTeamSP");
            Style DragEnt_NewTeamSP = (Style)System.Windows.Application.Current.FindResource("DragEnt_NewTeamSP");

            num_weeks = v[0];
            num_games = v[1];
            num_divs = v[2];
            num_teams = v[3];
            num_confs = v[4];
            num_playoff_teams = v[5];
            teams_per_division = num_teams / num_divs;

            logger.Info("League structure changed to " + "num_weeks " + num_weeks.ToString() + "num_games " + num_games.ToString() + "num_divs " + num_divs.ToString() + "num_teams " + num_teams.ToString() + "num_confs " + num_confs.ToString() + "num_playoff_teams " + num_playoff_teams.ToString() + "teams_per_division " + teams_per_division.ToString());

            newlnumweeks.Text = num_weeks.ToString();
            newlnumgames.Text = num_games.ToString();
            newlnumdivisions.Text = num_divs.ToString();
            newlnumteams.Text = num_teams.ToString();
            newlnumconferences.Text = num_confs.ToString();
            newlnumplayoffteams.Text = num_playoff_teams.ToString();

            logger.Debug("setOrganization");
            pw.League.setOrganization(num_weeks, num_games, num_teams, num_playoff_teams);

            // Clear previous division selections
            logger.Debug("Clear previous division selections");
            spDivisions.Children.Clear();
            sp1.Children.Clear();
            unregisterControl("newlConf1");
            unregisterControl("newlConf2");
            unregisterControl("newllblConf1");
            unregisterControl("newllblConf2");

            logger.Debug("Unregister all team controls");
            for (int I = 1; I <= Convert.ToInt32(num_teams); I++)
            {
                unregisterControl("newldiv" + I.ToString());
                unregisterControl("newldiv_team" + I.ToString());
                unregisterControl("newlimgTeam" + I.ToString());
                unregisterControl("newllblTeam" + I.ToString());
            }

            if (num_confs == 2)
            {
                logger.Debug("Num conferences = 2");
                var v_sp1 = new StackPanel();
                v_sp1.Orientation = Orientation.Vertical;
                v_sp1.VerticalAlignment = VerticalAlignment.Top;
                v_sp1.HorizontalAlignment = HorizontalAlignment.Center;

                var conf_panel_1_sq = new StackPanel();
                conf_panel_1_sq.Name = "conference_panel_1";
                conf_panel_1_sq.Orientation = Orientation.Horizontal;

                var conf_1_label = new Label();
                conf_1_label.Content = "Conference 1:";
                conf_1_label.Style = Largelblstyle;

                var txtConf1 = new TextBox();
                txtConf1.Name = "newlConf1";
                txtConf1.Width = 150;
                txtConf1.Style = Largetxttyle;
                txtConf1.MaxLength = 60;
                txtConf1.AddHandler(UIElement.LostFocusEvent, new RoutedEventHandler(confTextbox_LostFocus));

                conf_panel_1_sq.Children.Add(conf_1_label);
                conf_panel_1_sq.Children.Add(txtConf1);

                v_sp1.Children.Add(conf_panel_1_sq);

                var gb_conf1 = new GroupBox();
                gb_conf1.Name = "gb_conf1";
                gb_conf1.Margin = new Thickness(10, 10, 10, 10);
                gb_conf1.FontSize = 18;
                gb_conf1.Header = "Divisions:";
                gb_conf1.Style = GroupBoxstyle;

                v_sp1.Children.Add(gb_conf1);

                // register the dynamically added control so that it can be looked up later.
                this.RegisterName(txtConf1.Name, txtConf1);

                logger.Debug("Conference 1 controls created.");

                var v_sp2 = new StackPanel();
                v_sp2.Orientation = Orientation.Vertical;
                v_sp2.VerticalAlignment = VerticalAlignment.Top;
                v_sp2.HorizontalAlignment = HorizontalAlignment.Center;

                var conf_panel_2_sq = new StackPanel();
                conf_panel_2_sq.Name = "conference_panel_2";
                conf_panel_2_sq.Orientation = Orientation.Horizontal;

                var conf_2_label = new Label();
                conf_2_label.Content = "Conference 2:";
                conf_2_label.Style = Largelblstyle;

                var txtConf2 = new TextBox();
                txtConf2.Name = "newlConf2";
                txtConf2.Width = 150;
                txtConf2.Style = Largetxttyle;
                txtConf2.MaxLength = 60;
                txtConf2.AddHandler(UIElement.LostFocusEvent, new RoutedEventHandler(confTextbox_LostFocus));

                conf_panel_2_sq.Children.Add(conf_2_label);
                conf_panel_2_sq.Children.Add(txtConf2);

                v_sp2.Children.Add(conf_panel_2_sq);

                var gb_conf2 = new GroupBox();
                gb_conf2.Name = "gb_conf2";
                gb_conf2.Margin = new Thickness(10, 10, 10, 10);
                gb_conf2.FontSize = 18;
                gb_conf2.Header = "Divisions:";
                gb_conf2.Style = GroupBoxstyle;

                v_sp2.Children.Add(gb_conf2);

                // register the dynamically added control so that it can be looked up later.
                this.RegisterName(txtConf2.Name, txtConf2);

                logger.Debug("Conference 2 controls created.");

                var st_v_gb1 = new StackPanel();
                st_v_gb1.Orientation = Orientation.Vertical;
                st_v_gb1.HorizontalAlignment = HorizontalAlignment.Center;
                st_v_gb1.Margin = new Thickness(5, 5, 10, 10);

                var st_v_gb2 = new StackPanel();
                st_v_gb2.Orientation = Orientation.Vertical;
                st_v_gb2.HorizontalAlignment = HorizontalAlignment.Center;
                st_v_gb2.Margin = new Thickness(5, 5, 10, 10);

                last_div_first_group = num_divs / 2;

                logger.Debug("last_div_first_group " + Convert.ToInt32(last_div_first_group));

                // set the labels font text colors ext.
                for (int i = 1; i <= last_div_first_group; i++)
                {
                    j = i + last_div_first_group;
                    var sp1 = new StackPanel();
                    sp1.Orientation = Orientation.Horizontal;
                    sp1.Margin = new Thickness(0, 0, 0, 2);
                    sp1.Name = "div1_staack";

                    var div_1_label = new Label();
                    div_1_label.Content = "Division " + i.ToString();
                    div_1_label.Style = Largelblstyle;

                    var txtDivision1 = new TextBox();
                    txtDivision1.Name = "newldiv" + i.ToString();
                    txtDivision1.Width = 150;
                    txtDivision1.Style = Largetxttyle;
                    txtDivision1.AddHandler(UIElement.LostFocusEvent, new RoutedEventHandler(divTextbox_LostFocus));

                    // register the dynamically added control so that it can be looked up later.
                    this.RegisterName(txtDivision1.Name, txtDivision1);

                    sp1.Children.Add(div_1_label);
                    sp1.Children.Add(txtDivision1);

                    st_v_gb1.Children.Add(sp1);

                    logger.Debug("Division " + i.ToString() + " created");

                    var sp2 = new StackPanel();
                    sp2.Orientation = Orientation.Horizontal;
                    sp2.Margin = new Thickness(0, 0, 0, 2);
                    sp1.Name = "div2_staack";

                    var div_2_label = new Label();
                    div_2_label.Content = "Division " + j.ToString();
                    div_2_label.Style = Largelblstyle;

                    var txtDivision2 = new TextBox();
                    txtDivision2.Name = "newldiv" + j.ToString();
                    txtDivision2.Width = 150;
                    txtDivision2.Style = Largetxttyle;
                    txtDivision2.AddHandler(UIElement.LostFocusEvent, new RoutedEventHandler(divTextbox_LostFocus));

                    sp2.Children.Add(div_2_label);
                    sp2.Children.Add(txtDivision2);

                    st_v_gb2.Children.Add(sp2);

                    // register the dynamically added control so that it can be looked up later.
                    this.RegisterName(txtDivision2.Name, txtDivision2);

                    logger.Debug("Division " + j.ToString() + " created");
                }
                gb_conf1.Content = st_v_gb1;
                gb_conf2.Content = st_v_gb2;

                spDivisions.Children.Add(v_sp1);
                spDivisions.Children.Add(v_sp2);
            }
            else
            {
                logger.Debug("0 Conferences");
                var v_sp = new StackPanel();
                v_sp.Orientation = Orientation.Vertical;
                v_sp.VerticalAlignment = VerticalAlignment.Top;
                v_sp.HorizontalAlignment = HorizontalAlignment.Center;

                var gb_conf1 = new GroupBox();
                gb_conf1.Name = "gb_conf1";
                gb_conf1.Margin = new Thickness(10, 10, 10, 10);
                gb_conf1.FontSize = 18;
                gb_conf1.Header = "Divisions:";
                gb_conf1.Style = GroupBoxstyle;

                v_sp.Children.Add(gb_conf1);
                var st_v_gb = new StackPanel();
                st_v_gb.Orientation = Orientation.Vertical;
                st_v_gb.HorizontalAlignment = HorizontalAlignment.Center;
                st_v_gb.Margin = new Thickness(5, 5, 10, 10);

                // set the labels font text colors ext.
                for (int i = 1; i <= num_divs; i++)
                {
                    var sp1 = new StackPanel();
                    sp1.Orientation = Orientation.Horizontal;
                    sp1.Margin = new Thickness(0, 0, 0, 2);
                    sp1.Name = "div1_staack";

                    var div_1_label = new Label();
                    div_1_label.Content = "Division " + i.ToString();
                    div_1_label.Style = Largelblstyle;

                    var txtDivision1 = new TextBox();
                    txtDivision1.Name = "newldiv" + i.ToString();
                    txtDivision1.Width = 150;
                    txtDivision1.Style = Largetxttyle;
                    txtDivision1.AddHandler(UIElement.LostFocusEvent, new RoutedEventHandler(divTextbox_LostFocus));

                    sp1.Children.Add(div_1_label);
                    sp1.Children.Add(txtDivision1);

                    st_v_gb.Children.Add(sp1);
                    gb_conf1.Content = st_v_gb;

                    // register the dynamically added control so that it can be looked up later.
                    this.RegisterName(txtDivision1.Name, txtDivision1);

                    logger.Debug("Division " + i.ToString() + " created");
                }

                spDivisions.Children.Add(v_sp);
            }

            // setting division from new_teams on teams tab
            Style Teamlbltyle = (Style)System.Windows.Application.Current.FindResource("Teamlbltyle");
            // Dim Conflbltyle As Style = Application.Current.FindResource("Conflbltyle")

            int t_id = 1;
            int num_divs_per_conf;
            if (num_confs == 0)
                num_divs_per_conf = num_divs;
            else
                num_divs_per_conf = num_divs / num_confs;

            if (num_confs == 2)
            {
                var v_sp1 = new StackPanel();
                v_sp1.Orientation = Orientation.Vertical;
                v_sp1.VerticalAlignment = VerticalAlignment.Top;
                v_sp1.HorizontalAlignment = HorizontalAlignment.Center;

                var conf1_sp = new StackPanel();
                conf1_sp.Orientation = Orientation.Horizontal;
                conf1_sp.HorizontalAlignment = HorizontalAlignment.Center;

                var conf1_label = new Label();
                conf1_label.Name = "newllblConf1";
                conf1_label.Width = 150;
                conf1_label.Style = Conflbltyle;

                conf1_sp.Children.Add(conf1_label);
                v_sp1.Children.Add(conf1_sp);

                this.RegisterName(conf1_label.Name, conf1_label);

                for (int i = 1; i <= num_divs_per_conf; i++)
                {
                    var gb_hdr_label = new Label();
                    gb_hdr_label.Name = "newldiv_team" + i.ToString();
                    gb_hdr_label.Foreground = Brushes.White;

                    var gb_div = new GroupBox();
                    gb_div.Margin = new Thickness(1, 1, 1, 1);
                    gb_div.FontSize = 14;
                    gb_div.Header = gb_hdr_label;

                    var v_sp_in_groupbox = new StackPanel();
                    v_sp_in_groupbox.Orientation = Orientation.Vertical;
                    v_sp_in_groupbox.Width = 350;

                    gb_div.Content = v_sp_in_groupbox;

                    this.RegisterName(gb_hdr_label.Name, gb_hdr_label);

                    for (int z = 1; z <= teams_per_division; z++)
                    {
                        var sp_team = new StackPanel();
                        sp_team.Orientation = Orientation.Horizontal;
                        sp_team.Style = UnselNewTeamSP;

                        var helmet_img = new Image();
                        helmet_img.Name = "newlimgTeam" + t_id.ToString();
                        helmet_img.Width = 20;
                        helmet_img.Height = 20;

                        var team_label = new Label();
                        team_label.Name = "newllblTeam" + t_id.ToString();
                        team_label.Padding = new Thickness(10, 0, 0, 0);
                        team_label.Width = 250;
                        team_label.Style = Teamlbltyle;


                        sp_team.Children.Add(helmet_img);
                        sp_team.Children.Add(team_label);
                        sp_team.AllowDrop = true;

                        sp_team.AddHandler(UIElement.MouseDownEvent, new MouseButtonEventHandler(sp_team_MouseDown));
                        sp_team.AddHandler(UIElement.MouseMoveEvent, new MouseEventHandler(sp_team_MouseMove));
                        sp_team.AddHandler(UIElement.DragEnterEvent, new DragEventHandler(sp_team_dragenter));
                        sp_team.AddHandler(UIElement.DragLeaveEvent, new DragEventHandler(sp_team_dragleave));
                        sp_team.AddHandler(UIElement.DropEvent, new DragEventHandler(sp_team_drop));

                        sp_team.Style = UnselNewTeamSP;

                        v_sp_in_groupbox.Children.Add(sp_team);

                        this.RegisterName(helmet_img.Name, helmet_img);
                        this.RegisterName(team_label.Name, team_label);

                        logger.Debug("Team " + t_id.ToString() + " control created");

                        t_id += 1;
                    }
                    v_sp1.Children.Add(gb_div);
                }

                var v_sp2 = new StackPanel();
                v_sp2.Orientation = Orientation.Vertical;
                v_sp2.VerticalAlignment = VerticalAlignment.Top;
                v_sp2.HorizontalAlignment = HorizontalAlignment.Center;

                var conf2_sp = new StackPanel();
                conf2_sp.Orientation = Orientation.Horizontal;
                conf2_sp.HorizontalAlignment = HorizontalAlignment.Center;

                var conf2_label = new Label();
                conf2_label.Name = "newllblConf2";
                conf2_label.Width = 150;
                conf2_label.Style = Conflbltyle;

                conf2_sp.Children.Add(conf2_label);
                v_sp2.Children.Add(conf2_sp);

                this.RegisterName(conf2_label.Name, conf2_label);

                for (int i = num_divs_per_conf + 1; i <= num_divs; i++)
                {
                    var gb_hdr_label = new Label();
                    gb_hdr_label.Name = "newldiv_team" + i.ToString();
                    gb_hdr_label.Foreground = Brushes.White;

                    var gb_div = new GroupBox();
                    gb_div.Margin = new Thickness(1, 1, 1, 1);
                    gb_div.FontSize = 14;
                    gb_div.Header = gb_hdr_label;

                    var v_sp_in_groupbox = new StackPanel();
                    v_sp_in_groupbox.Orientation = Orientation.Vertical;
                    v_sp_in_groupbox.Width = 350;

                    gb_div.Content = v_sp_in_groupbox;

                    this.RegisterName(gb_hdr_label.Name, gb_hdr_label);

                    for (int z = 1; z <= teams_per_division; z++)
                    {
                        var sp_team = new StackPanel();
                        sp_team.Orientation = Orientation.Horizontal;

                        var helmet_img = new Image();
                        helmet_img.Name = "newlimgTeam" + t_id.ToString();
                        helmet_img.Width = 20;
                        helmet_img.Height = 20;

                        var team_label = new Label();
                        team_label.Name = "newllblTeam" + t_id.ToString();
                        team_label.Padding = new Thickness(10, 0, 0, 0);
                        team_label.Width = 250;
                        team_label.Style = Teamlbltyle;

                        sp_team.Children.Add(helmet_img);
                        sp_team.Children.Add(team_label);
                        sp_team.AllowDrop = true;
                        sp_team.AddHandler(UIElement.MouseDownEvent, new MouseButtonEventHandler(sp_team_MouseDown));
                        sp_team.AddHandler(UIElement.MouseMoveEvent, new MouseEventHandler(sp_team_MouseMove));
                        sp_team.AddHandler(UIElement.DragEnterEvent, new DragEventHandler(sp_team_dragenter));
                        sp_team.AddHandler(UIElement.DragLeaveEvent, new DragEventHandler(sp_team_dragleave));
                        sp_team.AddHandler(UIElement.DropEvent, new DragEventHandler(sp_team_drop));
                        sp_team.Style = UnselNewTeamSP;

                        v_sp_in_groupbox.Children.Add(sp_team);

                        this.RegisterName(helmet_img.Name, helmet_img);
                        this.RegisterName(team_label.Name, team_label);

                        logger.Debug("Team " + t_id.ToString() + " control created");

                        t_id += 1;
                    }
                    v_sp2.Children.Add(gb_div);
                }

                sp1.Children.Add(v_sp1);
                sp1.Children.Add(v_sp2);
            }
            else
            {
                var v2_sp = new StackPanel();
                v2_sp.Orientation = Orientation.Vertical;
                v2_sp.HorizontalAlignment = HorizontalAlignment.Center;

                for (int i = 1; i <= num_divs; i++)
                {
                    var gb_hdr_label = new Label();
                    gb_hdr_label.Name = "newldiv_team" + i.ToString();
                    gb_hdr_label.Foreground = Brushes.White;

                    var gb_div = new GroupBox();
                    gb_div.Margin = new Thickness(1, 1, 1, 1);
                    gb_div.FontSize = 14;
                    gb_div.Header = gb_hdr_label;

                    var v_sp_in_groupbox = new StackPanel();
                    v_sp_in_groupbox.Orientation = Orientation.Vertical;
                    v_sp_in_groupbox.Width = 350;

                    gb_div.Content = v_sp_in_groupbox;
                    this.RegisterName(gb_hdr_label.Name, gb_hdr_label);

                    for (int z = 1; z <= teams_per_division; z++)
                    {
                        var sp_team = new StackPanel();
                        sp_team.Orientation = Orientation.Horizontal;

                        var helmet_img = new Image();
                        helmet_img.Name = "newlimgTeam" + t_id.ToString();
                        helmet_img.Width = 20;
                        helmet_img.Height = 20;

                        var team_label = new Label();
                        team_label.Name = "newllblTeam" + t_id.ToString();
                        team_label.Padding = new Thickness(10, 0, 0, 0);
                        team_label.Width = 250;
                        team_label.Style = Teamlbltyle;

                        sp_team.Children.Add(helmet_img);
                        sp_team.Children.Add(team_label);
                        sp_team.AllowDrop = true;
                        sp_team.AddHandler(UIElement.MouseDownEvent, new MouseButtonEventHandler(sp_team_MouseDown));
                        sp_team.AddHandler(UIElement.MouseMoveEvent, new MouseEventHandler(sp_team_MouseMove));
                        sp_team.AddHandler(UIElement.DragEnterEvent, new DragEventHandler(sp_team_dragenter));
                        sp_team.AddHandler(UIElement.DragLeaveEvent, new DragEventHandler(sp_team_dragleave));
                        sp_team.AddHandler(UIElement.DropEvent, new DragEventHandler(sp_team_drop));
                        sp_team.Style = UnselNewTeamSP;

                        v_sp_in_groupbox.Children.Add(sp_team);

                        this.RegisterName(helmet_img.Name, helmet_img);
                        this.RegisterName(team_label.Name, team_label);

                        logger.Debug("Team " + t_id.ToString() + " control created");

                        t_id += 1;
                    }
                    v2_sp.Children.Add(gb_div);
                }

                sp1.Children.Add(v2_sp);
            }


            setTeamsLabels();
        }
        private void newl1Cancel_Click(object sender, RoutedEventArgs e)
        {
            Show_MainMenu?.Invoke(this, new EventArgs());
        }
        private void unregisterControl(string s)
        {
            try
            {
                this.UnregisterName(s);
            }
            catch (Exception IG)
            {
                logger.Error("Error unregisting code " + IG.Message);
                logger.Error(IG);
            }
        }
        private void newl1Next_Click(object sender, RoutedEventArgs e)
        {
            var Conferences_list = new List<string>();
            var Divisions_list = new List<string>();

            try
            {
                logger.Info("Create new league clicked");
                validate();
                logger.Info("league validated!");

                if (Convert.ToInt32(newlnumconferences.Text) == 2)
                {
                    Conferences_list.Add(((TextBox)this.FindName("newlConf1")).Text.Trim());
                    Conferences_list.Add(((TextBox)this.FindName("newlConf2")).Text.Trim());
                }

                for (int i = 1; i <= Convert.ToInt32(newlnumdivisions.Text); i++)
                    Divisions_list.Add(((TextBox)this.FindName("newldiv" + i.ToString())).Text.Trim());

                var lyears = new List<int>(new int[] { Convert.ToInt32(newl1StartingYear.Text) });

                pw.League.setBasicInfo(newl1shortname.Text.ToUpper().Trim(), newl1longname.Text.Trim(), Convert.ToInt32(newl1StartingYear.Text), newl1championshipgame.Text.Trim(), Conferences_list, Divisions_list, lyears, Leaguemdl.League_State.Regular_Season);

                // Background Worker code for popup
                pop.btnclose.Visibility = Visibility.Hidden;

                bw = new BackgroundWorker();

                // Add any initialization after the InitializeComponent() call.
                bw.DoWork += bw_DoWork;
                bw.ProgressChanged += bw_ProgressChanged;
                bw.RunWorkerCompleted += bw_RunWorkerCompleted;

                bw.WorkerReportsProgress = true;
                bw.RunWorkerAsync(pw.League);

                // Progress Bar Window
                pop.ShowDialog();
            }
            catch (Exception ex)
            {
                logger.Error("Error creating new league");
                logger.Error(ex);
                MessageBox.Show(CommonUtils.substr(ex.Message, 0, 100), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            Leaguemdl lg = (Leaguemdl)e.Argument;

            var s = new League_Services();
            s.CreateNewLeague(lg, bw);
        }
        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Style lableError = (Style)System.Windows.Application.Current.FindResource("LabelError");
            pop.btnclose.Visibility = Visibility.Visible;

            // Progress Bar Window close
            if (pop.prgTest.Foreground == Brushes.Red)
            {
                pop.statuslbl.Style = lableError;
                pop.statuslbl.Content = "Error!  League Not Create!";
            }
            else
                pop.statuslbl.Content = "League Created Successfully!";
        }
        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            string user_state_Struct = e.UserState.ToString();
            string[] user_stats = user_state_Struct.Split('|');

            pop.Title = user_stats[0];
            pop.prgTest.Value = e.ProgressPercentage;
            pop.prglbl.Content = user_stats[1];

            if (user_stats[0] == "Error")
                pop.prgTest.Foreground = Brushes.Red;
        }

        // This event handler is called when the division name textbox loses focus, so that
        // the division names can be set on the tab of new teams
        private void divTextbox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox l = (TextBox) e.Source;
            int n = CommonUtils.ExtractDivNumber(l.Name);
            Label divLabel = (Label) this.FindName("newldiv_team" + n.ToString());
            divLabel.Content = l.Text;
        }
        // This method is fired when either conference textbox loses focus so that either conference
        // label can be set on the teams tab
        private void confTextbox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox l = (TextBox) e.Source;
            Label confLabel = null;
            if (l.Name.EndsWith("1"))
                confLabel = (Label) this.FindName("newllblConf1");
            else
                confLabel = (Label) this.FindName("newllblConf2");
            confLabel.Content = l.Text;
        }
        public void setTeamsLabels()
        {
            Style Teamlbltyle = (Style) System.Windows.Application.Current.FindResource("Teamlbltyle");

            logger.Info("Setting team labels");

            for (int i = 1; i <= Convert.ToInt32(newlnumteams.Text); i++)
            {
                string teamLabel = "newllblTeam" + i.ToString();
                string teamImage = "newlimgTeam" + i.ToString();
                logger.Debug("setting teamlabel and teamimage " + " " + teamImage);

                Label teamLbl = (Label) this.FindName(teamLabel);
                Image teamImg = (Image) this.FindName(teamImage);
                logger.Debug("teamlabel and teamimg found");

                teamLbl.Style = Teamlbltyle;

                TeamMdl st = pw.League.Teams[i - 1];
                if (st.City != App_Constants.EMPTY_TEAM_SLOT)
                {
                    logger.Debug("Setting label for " + st.City);

                    teamLbl.Content = st.City + " " + st.Nickname;
                    teamLbl.VerticalContentAlignment = VerticalAlignment.Center;
                }
                else
                    teamLbl.Content = App_Constants.EMPTY_TEAM_SLOT;

                string img_path = pw.League.Teams[i - 1].Helmet_img_path;

                logger.Debug("Helmet img_path: " + img_path);

                if (img_path != null && img_path.Length > 0)
                {
                    var helmetIMG_source = new BitmapImage(new Uri(img_path));
                    teamImg.Source = helmetIMG_source;
                }
            }
        }
        private void sp_team_drop(object sender, DragEventArgs e)
        {
            if (sender == e.Source)
                return;

            var new_sp = (StackPanel)sender;
            Image new_image = (Image) new_sp.Children[0];
            Label new_label = (Label) new_sp.Children[1];

            Image drag_data_image = null;
            Label drag_data_label = null;


            if (drag_from  == "stock")
            {
                drag_data_image = (Image) drag_data.Children[0];
                drag_data_label = (Label) drag_data.Children[1];

                // Get the full stock team that was dragged
                TeamMdl new_team = Team.get_team_from_name(drag_data_label.Content.ToString(), st_list);
                // get the imdex of the team label that the new team is to be dropped on
                int new_index = CommonUtils.ExtractTeamNumber(new_label.Name) - 1;

                new_sp.Style = UnselNewTeamSP;

                new_image.Source = ((Image)drag_data.Children[0]).Source;
                new_label.Content = ((Label)drag_data.Children[1]).Content;

                dragSource.Items.Remove(drag_data);
                TeamMdl cloned_team = new TeamMdl(new_team);
                cloned_team.setID(new_index);
                // Set prefix the helmet image path and stadium image path with the app folders for this
                // computer, because these stock teams were created on the developer's computer and
                // would not be correct.
                cloned_team.setStockImagePaths(CommonUtils.getAppPath() + App_Constants.APP_HELMET_FOLDER + new_team.Helmet_img_path, CommonUtils.getAppPath() + App_Constants.APP_STADIUM_FOLDER + new_team.Stadium.Stadium_Img_Path);
                pw.League.Teams[new_index] = cloned_team;
            }
            else if (drag_from == "league")
            {
                drag_data_image = (Image) drag_data.Children[0];
                drag_data_label = (Label) drag_data.Children[1];

                // get the new team id from the label name
                int new_index = CommonUtils.ExtractTeamNumber(new_label.Name) - 1;
                // get the old team id from the label name
                int old_index = CommonUtils.ExtractTeamNumber(drag_data_label.Name) - 1;

                new_sp.Style = UnselNewTeamSP;

                new_image.Source = ((Image)drag_data.Children[0]).Source;
                new_label.Content = ((Label)drag_data.Children[1]).Content;

                drag_data_image.Source = null;
                drag_data_label.Content = App_Constants.EMPTY_TEAM_SLOT;

                // Set new team in league to the old team and change the id to the new slot
                pw.League.Teams[new_index] = pw.League.Teams[old_index];
                pw.League.Teams[new_index].setID(new_index);

                // Set old slot to a new blank team
                var blank_team = new TeamMdl(old_index, App_Constants.EMPTY_TEAM_SLOT);
                pw.League.Teams[old_index] = blank_team;
            }
        }
        private void sp_team_MouseMove(object sender, MouseEventArgs e)
        {

            // Get the current mouse position
            Point mousePos = e.GetPosition(null);
            Vector diff = startPoint - mousePos;

            if ((int)e.LeftButton == (int)MouseButtonState.Pressed && (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance || Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
                drag_data = null;
                drag_from = "league";
                var parent = (StackPanel)sender;
                drag_data = parent;
                if (drag_data != null)
                    DragDrop.DoDragDrop(parent, drag_data, DragDropEffects.Move);
            }
        }

        private void sp_team_MouseDown(object sender, MouseButtonEventArgs e)
        {
            startPoint = e.GetPosition(null);

            if (e.ClickCount == 2)
            {
                StackPanel s = (StackPanel) sender;
                Label l = (Label) s.Children[1];
                int n = CommonUtils.ExtractTeamNumber(l.Name);
                Show_NewTeam?.Invoke(this, new teamEventArgs(n));
            }
        }

        private void sp_team_dragenter(object sender, DragEventArgs e)
        {
            var tb = sender as StackPanel;
            tb.Style = DragEnt_NewTeamSP;
        }

        private void sp_team_dragleave(object sender, DragEventArgs e)
        {
            var tb = sender as StackPanel;
            tb.Style = UnselNewTeamSP;
        }

        private void StockTeamsGrid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            drag_data = null;
            drag_from = "stock";
            var parent = (ListBox)sender;
            dragSource = parent;
            var data = GetDataFromListBox(dragSource, e.GetPosition(parent));
            if (data != null)
            {
                drag_data = (StackPanel)data;
                DragDrop.DoDragDrop(parent, data, DragDropEffects.Move);
            }
        }

        private static object GetDataFromListBox(ListBox source, Point point)
        {
            var element = source.InputHitTest(point) as UIElement;

            if (element != null)
            {
                var data = DependencyProperty.UnsetValue;

                while (data == DependencyProperty.UnsetValue)
                {
                    data = source.ItemContainerGenerator.ItemFromContainer(element);

                    if (data == DependencyProperty.UnsetValue)
                        element = VisualTreeHelper.GetParent(element) as UIElement;

                    if (element == source)
                        return null;
                }

                if (data != DependencyProperty.UnsetValue)
                    return data;
            }

            return null;
        }

        private void help_btn_Click(object sender, RoutedEventArgs e)
        {
            var hlp_form = new Help_NewLeague();
            hlp_form.Top = (SystemParameters.PrimaryScreenHeight - hlp_form.Height) / 2;
            hlp_form.Left = (SystemParameters.PrimaryScreenWidth - hlp_form.Width) / 2;
            hlp_form.ShowDialog();
        }
    }
}
