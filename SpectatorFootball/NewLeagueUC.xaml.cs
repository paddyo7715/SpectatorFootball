﻿using System.Windows.Media.Imaging;
using System.Windows;
using System.Windows.Input;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Controls;
using System;
using System.ComponentModel;
using System.IO;
using Microsoft.Win32;
using log4net;
using SpectatorFootball.Models;
using SpectatorFootball.Versioning;
using SpectatorFootball.League;
using SpectatorFootball.CustomControls;

namespace SpectatorFootball
{
    public partial class NewLeagueUC : UserControl
    {
        private static ILog logger = LogManager.GetLogger("RollingFile");
        // pw is the parent window mainwindow
        private MainWindow pw;
        private List<Stock_Teams> st_list;
        private ListBox dragSource = null;
        private StackPanel drag_data = null;
        private string drag_from = null;
        private Style UnselNewTeamSP = (Style)System.Windows.Application.Current.FindResource("UnselNewTeamSP");
        private Style DragEnt_NewTeamSP = (Style)System.Windows.Application.Current.FindResource("DragEnt_NewTeamSP");
        public Point startPoint { get; set; }

        public event EventHandler Show_MainMenu;
        public event EventHandler<teamEventArgs> Show_NewTeam;

        public BackgroundWorker bw = null;
        public Progress_Dialog pop = null; 

        public NewLeagueUC(MainWindow pw, List<Stock_Teams> st_list)
        {

            // This call is required by the designer.
            InitializeComponent();

            this.pw = pw;
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

                var BitmapImage = new BitmapImage(new Uri(CommonUtils.getAppPath() + app_Constants.APP_HELMET_FOLDER + st.Helmet_img_path));
                var helmet_img = new Image();
                helmet_img.Width = 25;
                helmet_img.Height = 25;
                helmet_img.Source = BitmapImage;

                List<Uniform_Color_percents> Color_Percents_List = null;
                Color_Percents_List = Uniform.getColorList(st.Home_Jersey_Number_Color, st.Home_jersey_Color, st.Helmet_Color,
                    st.Home_Pants_Color, st.Home_Sleeve_Color, st.Home_Jersey_Shoulder_Stripe, st.Home_Jersey_Sleeve_Stripe_Color_1,
                    st.Home_Jersey_Sleeve_Stripe_Color_2, st.Home_Jersey_Sleeve_Stripe_Color_3, st.Home_Jersey_Sleeve_Stripe_Color_4,
                    st.Home_Jersey_Sleeve_Stripe_Color_5, st.Home_Jersey_Sleeve_Stripe_Color_6, st.Home_Pants_Stripe_Color_1,
                    st.Home_Pants_Stripe_Color_2, st.Home_Pants_Stripe_Color_3);

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
            DIRPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + newl1shortname.Text);

            if (CommonUtils.isBlank(newl1shortname.Text))
                throw new Exception("On the Settings Tab, League Short Name must be supplied!");
            
            if (newl1shortname.Text == app_Constants.LOG_FOLDER)
                throw new Exception("On the Settings Tab, League Short Name Can Not be Logs.  Please chose another name for you league!");

            if (!CommonUtils.isAlphaNumeric(newl1shortname.Text, false))
                throw new Exception("On the Settings Tab, Invalid character in League Short Name!");

            if (CommonUtils.isBlank(newl1longname.Text))
                throw new Exception("On the Settings Tab, League Long Name must be supplied!");

            if (!CommonUtils.isAlphaNumeric(newl1longname.Text, true))
                throw new Exception("On the Settings Tab, Invalid character in League Long Name!");

            if (CommonUtils.isBlank(newLogoPath.Text))
                throw new Exception("On the Settings Tab, League Logo Must be Selected!");

            if (CommonUtils.isBlank(newl1championshipgame.Text))
                throw new Exception("On the Settings Tab, Championship Game must be supplied!");
            if (!CommonUtils.isAlphaNumeric(newl1championshipgame.Text, true))
                throw new Exception("On the Settings Tab, Invalid character in Championship Game!");

            if (CommonUtils.isBlank(newlnumweeks.Text) || !int.TryParse(newlnumweeks.Text, out dummy_int))
                throw new Exception("On the Settings Tab, Invalid Value for Number of Weeks!");
            if (CommonUtils.isBlank(newlnumgames.Text) || !int.TryParse(newlnumgames.Text, out dummy_int))
                throw new Exception("On the Settings Tab, Invalid Value for Number of Games!");
            if (CommonUtils.isBlank(newlnumdivisions.Text) || !int.TryParse(newlnumdivisions.Text, out dummy_int))
                throw new Exception("On the Settings Tab, Invalid Value for Number of Divisions!");
            if (CommonUtils.isBlank(newlnumteams.Text) || !int.TryParse(newlnumteams.Text, out dummy_int))
                throw new Exception("On the Settings Tab, Invalid Value for Number of Teams!");
            if (CommonUtils.isBlank(newlnumplayoffteams.Text) || !int.TryParse(newlnumplayoffteams.Text, out dummy_int))
                throw new Exception("On the Settings Tab, Invalid Value for Number of Playoff Teams");

            for (int i = 1; i <= Convert.ToInt32(newlnumconferences.Text); i++)
            {
                string conftxtname = "newlConf" + i.ToString();
                TextBox conftxtbox = (TextBox) this.FindName(conftxtname);

                if (conftxtname == null || conftxtbox.Text == "")
                    throw new Exception("On the Team Tab, Conference name " + i.ToString() + " must be supplied!");

                if (conftxtbox.Text.StartsWith("Conference "))
                    throw new Exception("On the Team Tab, Conference name " + i.ToString() + " is Invalid!");

                if (!CommonUtils.isAlpha(conftxtbox.Text, true))
                    throw new Exception("On the Team Tab, Invalid character in Conference Name " + conftxtbox.Text + "!");
            }

            for (int i = 1; i <= Convert.ToInt32(newlnumdivisions.Text); i++)
            {
                string divtxtname = "newldiv" + i.ToString();
                TextBox divtxtbox = (TextBox)this.FindName(divtxtname);

                if (divtxtbox == null || divtxtbox.Text.Trim().Length == 0)
                    throw new Exception("On the Team Tab, A name for division " + i.ToString() + " must be supplied!");

                if (divtxtbox.Text.StartsWith("Division "))
                    throw new Exception("On the Team Tab, A name for division " + i.ToString() + " is Invalid!");

                if (!CommonUtils.isAlpha(divtxtbox.Text, true))
                    throw new Exception("On the Team Tab, Invalid character in Division Name " + divtxtbox.Text + "!");

            }
            for (int i = 1; i <= Convert.ToInt32(newlnumteams.Text); i++)
            {
                string teamlabel = "newllblTeam" + i.ToString();
                Label tlabel = (Label) this.FindName(teamlabel);
                if (tlabel.Content.ToString() == app_Constants.EMPTY_TEAM_SLOT)
                    throw new Exception("On the Teams Tab, All Empty Team Slots must be filled with a team!");
            }

            var ts = new Team_Services();
            string first_dup_team = ts.FirstDuplicateTeam(pw.New_Mem_Season.Season.Teams_by_Season);
            if (first_dup_team != null)
                throw new Exception("On the Settings Tab, Duplicate team " + first_dup_team + " found in league!  Leagues can not have duplicate teams!");

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
            int teams_per_division;
            int j;
            Style Largelblstyle = (Style)System.Windows.Application.Current.FindResource("Largelbltyle");
            Style GroupBoxstyle = (Style)System.Windows.Application.Current.FindResource("GroupBoxstyle");
            Style Largetxttyle = (Style)System.Windows.Application.Current.FindResource("Largetxttyle");
            Style MediumLargetxttyle = (Style)System.Windows.Application.Current.FindResource("MediumLargetxttyle");
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

            logger.Debug("Set all teams blank");

            pw.New_Mem_Season.Season.League_Structure_by_Season[0].Number_of_weeks = num_weeks;
            pw.New_Mem_Season.Season.League_Structure_by_Season[0].Number_of_Games = num_games;
            pw.New_Mem_Season.Season.League_Structure_by_Season[0].Num_Teams = num_teams;
            pw.New_Mem_Season.Season.League_Structure_by_Season[0].Num_Playoff_Teams = num_playoff_teams;
            pw.New_Mem_Season.Season.League_Structure_by_Season[0].Number_of_Divisions = num_divs;
            pw.New_Mem_Season.Season.League_Structure_by_Season[0].Number_of_Conferences = num_confs;

            pw.New_Mem_Season.Season.Teams_by_Season = new List<Teams_by_Season>();
            for (int i = 1; i <= num_teams; i++)
                pw.New_Mem_Season.Season.Teams_by_Season.Add(new Teams_by_Season() { ID = i, Team_Slot = i , City = app_Constants.EMPTY_TEAM_SLOT });


            // Clear previous division selections
            logger.Debug("Clear previous division selections");
            sp1.Children.Clear();

            if (this.FindName("newllblConf1") != null)
                unregisterControl("newlConf1");

            if (this.FindName("newllblConf2") != null)
                unregisterControl("newlConf2");

            logger.Debug("Unregister all div controls");
            for (int I = 1; I <= Convert.ToInt32(app_Constants.MAX_DIVISIONS); I++)
            {
                if (this.FindName("newldiv" + I.ToString()) != null)
                    unregisterControl("newldiv" + I.ToString());
                else
                    break;
            }

            logger.Debug("Unregister all team controls");
            for (int I = 1; I <= Convert.ToInt32(app_Constants.MAX_TEAMS); I++)
            {
                if (this.FindName("newllblTeam" + I.ToString()) != null)
                {
                    unregisterControl("newlimgTeam" + I.ToString());
                    unregisterControl("newllblTeam" + I.ToString());
                }
                else
                    break;
            }

            // setting division from new_teams on teams tab
            Style Teamlbltyle = (Style)System.Windows.Application.Current.FindResource("Teamlbltyle");

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

                var txtConf1 = new CustomTextBox();
                txtConf1.Name = "newlConf1";
                txtConf1.Width = 150;
                txtConf1.Style = MediumLargetxttyle;
                txtConf1.MaxLength = 60;
                txtConf1.PlaceholderText = "Conference 1";

                conf1_sp.Children.Add(txtConf1);
                v_sp1.Children.Add(conf1_sp);

                this.RegisterName(txtConf1.Name, txtConf1);

                for (int i = 1; i <= num_divs_per_conf; i++)
                {
                    var txtDivision1 = new CustomTextBox();
                    txtDivision1.Name = "newldiv" + i.ToString();
                    txtDivision1.Width = 150;
                    txtDivision1.Style = MediumLargetxttyle;
                    txtDivision1.PlaceholderText = "Division " + i.ToString();

                    var gb_div = new GroupBox();
                    gb_div.Margin = new Thickness(1, 1, 1, 1);
                    gb_div.FontSize = 14;
                    gb_div.Header = txtDivision1;

                    var v_sp_in_groupbox = new StackPanel();
                    v_sp_in_groupbox.Orientation = Orientation.Vertical;
                    v_sp_in_groupbox.Width = 350;

                    gb_div.Content = v_sp_in_groupbox;

                    this.RegisterName(txtDivision1.Name, txtDivision1);

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

                var txtConf2 = new CustomTextBox();
                txtConf2.Name = "newlConf2";
                txtConf2.Width = 150;
                txtConf2.Style = MediumLargetxttyle;
                txtConf2.MaxLength = 60;
                txtConf2.PlaceholderText = "Conference 2";

                conf2_sp.Children.Add(txtConf2);
                v_sp2.Children.Add(conf2_sp);

                this.RegisterName(txtConf2.Name, txtConf2);

                for (int i = num_divs_per_conf + 1; i <= num_divs; i++)
                {
                    var txtDivision2 = new CustomTextBox();
                    txtDivision2.Name = "newldiv" + i.ToString();
                    txtDivision2.Width = 150;
                    txtDivision2.Style = MediumLargetxttyle;
                    txtDivision2.PlaceholderText = "Division " + i.ToString();

                    var gb_div = new GroupBox();
                    gb_div.Margin = new Thickness(1, 1, 1, 1);
                    gb_div.FontSize = 14;
                    gb_div.Header = txtDivision2;

                    var v_sp_in_groupbox = new StackPanel();
                    v_sp_in_groupbox.Orientation = Orientation.Vertical;
                    v_sp_in_groupbox.Width = 350;

                    gb_div.Content = v_sp_in_groupbox;

                    this.RegisterName(txtDivision2.Name, txtDivision2);

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
                    var txtDivision1 = new CustomTextBox();
                    txtDivision1.Name = "newldiv" + i.ToString();
                    txtDivision1.Width = 150;
                    txtDivision1.Style = MediumLargetxttyle;
                    txtDivision1.PlaceholderText = "Division " + i.ToString();

                    var gb_div = new GroupBox();
                    gb_div.Margin = new Thickness(1, 1, 1, 1);
                    gb_div.FontSize = 14;
                    gb_div.Header = txtDivision1;

                    var v_sp_in_groupbox = new StackPanel();
                    v_sp_in_groupbox.Orientation = Orientation.Vertical;
                    v_sp_in_groupbox.Width = 350;

                    gb_div.Content = v_sp_in_groupbox;
                    this.RegisterName(txtDivision1.Name, txtDivision1);

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
                        team_label.Style = Largelblstyle;

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
           
            int ipenalties;
            int iinjuries;
            int Home_Field_Advantage;
            int Kickoff;
            int OnsideKick;
            int One_Point_Conversion;
            int Two_Point_Conversion;
            int Three_Point_Conversion;

            String DraftType = null;

            try
            {
                logger.Info("Create new league clicked");
                validate();
                logger.Info("league validated!");

                Mouse.OverrideCursor = Cursors.Wait;

                //Do this since it is easier to just work with s than that long string.
                Season s = pw.New_Mem_Season.Season;
                List<Franchise> Franchises = new List<Franchise>();
                pw.New_Mem_Season.Franchises = Franchises;
                s.Year = Convert.ToInt32(newl1StartingYear.Text);

                App_Version app_ver = new App_Version();

                League_Structure_by_Season ls = s.League_Structure_by_Season[0];

                if (newlPenYes.IsChecked == true)
                    ipenalties = 1;
                else
                    ipenalties = 0;

                if (newlInjYes.IsChecked == true)
                    iinjuries = 1;
                else
                    iinjuries = 0;

                if (newlHomeAdvYes.IsChecked == true)
                    Home_Field_Advantage = 1;
                else
                    Home_Field_Advantage = 0;

                if (newlKickoffYesYes.IsChecked == true)
                    Kickoff = 1;
                else
                    Kickoff = 0;

                if (newl1OnsideKick.IsChecked == true)
                    OnsideKick = 1;
                else
                    OnsideKick = 2;

                if (newl1PointConvKick.IsChecked == true)
                    One_Point_Conversion = 1;
                else
                    One_Point_Conversion = 2;

                if (newl2PointConvYes.IsChecked == true)
                    Two_Point_Conversion = 1;
                else
                    Two_Point_Conversion = 0;

                if (newl3PointConvYes.IsChecked == true)
                    Three_Point_Conversion = 1;
                else
                    Three_Point_Conversion = 0;

                if (newlDraft_FD.IsChecked == true) DraftType = "FD";
                else if (newlDraft_SD.IsChecked == true) DraftType = "SD";
                else if (newlDraft_DL.IsChecked == true) DraftType = "DL";
                else if (newlDraft_FL.IsChecked == true) DraftType = "FL";
                else if (newlDraft_FR.IsChecked == true) DraftType = "FR";
                else if (newlDraft_CR.IsChecked == true) DraftType = "CR";

                ls.Short_Name = newl1shortname.Text;
                ls.Long_Name = newl1longname.Text;
                ls.League_Logo_Filepath = newLogoPath.Text;
                ls.League_Logo_File = Path.GetFileName(newLogoPath.Text);
                ls.Championship_Game_Name = newl1championshipgame.Text;
                ls.Number_of_Conferences = Convert.ToInt32(newlnumconferences.Text);
                ls.Number_of_Divisions = Convert.ToInt32(newlnumdivisions.Text);
                ls.Number_of_Games = Convert.ToInt32(newlnumgames.Text);
                ls.Number_of_weeks = Convert.ToInt32(newlnumweeks.Text);
                ls.Num_Playoff_Teams = Convert.ToInt32(newlnumplayoffteams.Text);
                ls.Num_Teams = Convert.ToInt32(newlnumteams.Text);
                ls.Penalties = ipenalties;
                ls.Injuries = iinjuries;
                ls.Draft_Type_Code = DraftType;
                ls.Home_Advantage = Home_Field_Advantage;
                ls.Kickoff_Type = Kickoff;
                ls.Onside_Kick = OnsideKick;
                ls.Extra_Point = One_Point_Conversion;
                ls.Two_Point_Conversion = Two_Point_Conversion;
                ls.Three_Point_Conversion = Three_Point_Conversion;

                if (Convert.ToInt32(newlnumconferences.Text) == 2)
                {
                    string conf1_name = ((TextBox)this.FindName("newlConf1")).Text.Trim();
                    string conf2_name = ((TextBox)this.FindName("newlConf2")).Text.Trim();

                    Conference c1 = new Conference() { Ordinal = 1, Conf_Name = conf1_name };
                    Conference c2 = new Conference() { Ordinal = 2, Conf_Name = conf2_name };

                    s.Conferences.Add(c1);
                    s.Conferences.Add(c2);
                }

                for (int i = 1; i <= Convert.ToInt32(newlnumdivisions.Text); i++)
                {
                    string d_name = ((TextBox)this.FindName("newldiv" + i.ToString())).Text.Trim();
                    Division d = new Division() { Ordinal = i, Name = d_name };
                    s.Divisions.Add(d);
                }

                //set the owner of all teams to C for computer for now.
                for (int t_int = 0; t_int < (int) ls.Num_Teams; t_int++)
                {
                    s.Teams_by_Season[t_int].Team_Slot = t_int + 1;
                    s.Teams_by_Season[t_int].Owner = "C";
                    s.Teams_by_Season[t_int].Helmet_Image_File = Path.GetFileName(s.Teams_by_Season[t_int].Helmet_img_path);
                    s.Teams_by_Season[t_int].Stadium_Image_File = Path.GetFileName(s.Teams_by_Season[t_int].Stadium_Img_Path);
                    int f_id = t_int + 1;
                    s.Teams_by_Season[t_int].Franchise_ID = f_id;

                    Franchise f = new Franchise() {ID = f_id, Name = s.Teams_by_Season[t_int].City + " " + s.Teams_by_Season[t_int].Nickname + " Founded " + s.Year.ToString() };
                    f.Teams_by_Season.Add(s.Teams_by_Season[t_int]);
                    Franchises.Add(f);
                
                }

                // Background Worker code for popup
                pop = new Progress_Dialog();
                pop.leagueCreated += leagueCreated;
                bw = new BackgroundWorker();

                // Add any initialization after the InitializeComponent() call.
                bw.DoWork += bw_DoWork;
                bw.ProgressChanged += bw_ProgressChanged;
                bw.RunWorkerCompleted += bw_RunWorkerCompleted;

                pw.New_Mem_Season.DBVersion = new DBVersion() { Date_Created = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"), Version = App_Version.APP_VERSION, Action = "Created New" };

                bw.WorkerReportsProgress = true;
                bw.RunWorkerAsync(pw.New_Mem_Season);

                // Progress Bar Window
                pop.ShowDialog();
            }
            catch (Exception ex)
            {
                logger.Error("Error creating new league");
                logger.Error(ex);
                Mouse.OverrideCursor = null;
                MessageBox.Show(CommonUtils.substr(ex.Message, 0, 100), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
           New_League_Structure nlg = (New_League_Structure)e.Argument;

            var s = new League_Services();
            s.CreateNewLeague(nlg, bw);
        }
        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Style lableError = (Style)System.Windows.Application.Current.FindResource("LabelError");
            pop.btnclose.IsEnabled = true;

            // Progress Bar Window close
            if (pop.prgTest.Foreground == Brushes.Red)
            {
                pop.statuslbl.Style = lableError;
                pop.statuslbl.Content = "Error!  League Not Create!";
            }
            else
                pop.statuslbl.Content = "League Created Successfully!";

            Mouse.OverrideCursor = null;
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

                Teams_by_Season st = pw.New_Mem_Season.Season.Teams_by_Season[i - 1];
                if (st.City != app_Constants.EMPTY_TEAM_SLOT)
                {
                    logger.Debug("Setting label for " + st.City);

                    teamLbl.Content = st.City + " " + st.Nickname;
                    teamLbl.VerticalContentAlignment = VerticalAlignment.Center;
                }
                else
                    teamLbl.Content = app_Constants.EMPTY_TEAM_SLOT;

                string img_path = pw.New_Mem_Season.Season.Teams_by_Season[i - 1].Helmet_img_path;

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

                // get the imdex of the team label that the new team is to be dropped on
                int new_index = CommonUtils.ExtractTeamNumber(new_label.Name) - 1;
                // Get the full stock team that was dragged
                Stock_Teams new_team = Team_Helper.get_team_from_name(drag_data_label.Content.ToString(), st_list);


                new_sp.Style = UnselNewTeamSP;

                new_image.Source = ((Image)drag_data.Children[0]).Source;
                new_label.Content = ((Label)drag_data.Children[1]).Content;

                dragSource.Items.Remove(drag_data);
                Teams_by_Season cloned_team = Team_Helper.Clonse_Team_from_Stock(new_team);
                cloned_team.ID = new_index + 1;
                // Set prefix the helmet image path and stadium image path with the app folders for this
                // computer, because these stock teams were created on the developer's computer and
                // would not be correct.
                cloned_team.Helmet_img_path = CommonUtils.getAppPath() + app_Constants.APP_HELMET_FOLDER + new_team.Helmet_img_path;
                cloned_team.Stadium_Img_Path = CommonUtils.getAppPath() + app_Constants.APP_STADIUM_FOLDER + new_team.Stadium_Img_Path;


                pw.New_Mem_Season.Season.Teams_by_Season[new_index] = cloned_team;
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
                drag_data_label.Content = app_Constants.EMPTY_TEAM_SLOT;

                // Set new team in league to the old team and change the id to the new slot
                pw.New_Mem_Season.Season.Teams_by_Season[new_index] = pw.New_Mem_Season.Season.Teams_by_Season[old_index];
//                pw.Mem_League.Teams[new_index].ID = new_index + 1;

                // Set old slot to a new blank team
                var blank_team = new Teams_by_Season() {ID = old_index ,City = app_Constants.EMPTY_TEAM_SLOT };
                pw.New_Mem_Season.Season.Teams_by_Season[old_index] = blank_team;
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

        private void newl1longnameLeagueLogoPath_Click(object sender, RoutedEventArgs e)
        {
            var OpenFileDialog = new OpenFileDialog();
            string init_folder = CommonUtils.getAppPath();
            init_folder += Path.DirectorySeparatorChar + "Images" + Path.DirectorySeparatorChar + "Logos";

            OpenFileDialog.InitialDirectory = init_folder;
            OpenFileDialog.Multiselect = false;
            OpenFileDialog.Filter = "Image Files|*.jpg;*.gif;*.png;*.bmp";

            if (OpenFileDialog.ShowDialog() == true)
            {
                string filepath = OpenFileDialog.FileName;
                newLogoPath.Text = filepath;
             }
        }

        private void help_btn_Click(object sender, RoutedEventArgs e)
        {
            var hlp_form = new Help_NewLeague();
            hlp_form.Top = (SystemParameters.PrimaryScreenHeight - hlp_form.Height) / 2;
            hlp_form.Left = (SystemParameters.PrimaryScreenWidth - hlp_form.Width) / 2;
            hlp_form.ShowDialog();
        }
        //This event handler is fired from the popup that shows the dialog box that shows the 
        //progress in creating the league if the league creation is successful and the user
        //then clicks on the OK button on the dialog.
        private void leagueCreated(object sender, EventArgs e)
        {
            Show_MainMenu?.Invoke(this, new EventArgs());
        }
    }
}
