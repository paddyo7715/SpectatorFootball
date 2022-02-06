using log4net;
using SpectatorFootball.CustomControls;
using SpectatorFootball.League;
using SpectatorFootball.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using SpectatorFootball.Help_Forms;

namespace SpectatorFootball.WindowsLeague
{
    /// <summary>
    /// Interaction logic for LeagueStandings.xaml
    /// </summary>
    public partial class LeagueStandings : UserControl
    {
        private static ILog logger = LogManager.GetLogger("RollingFile");
        // pw is the parent window mainwindow
        private MainWindow pw;

        private Style DivGridHeader_Style = (Style)System.Windows.Application.Current.FindResource("StandingGridHDRStyle");

        public List<Season> Seasons { get; set; }
        public long CurrYear { get; set; }

        public event EventHandler Show_MainMenu;
        public event EventHandler<teamEventArgs> Show_TeamDetail;
        public LeagueStandings(MainWindow pw)
        {
            InitializeComponent();
            this.pw = pw;
            Seasons = pw.Loaded_League.AllSeasons;
            CurrYear = pw.Loaded_League.Current_Year;
            DataContext = this;
        }

    public void SetupLeagueStructure()
    {
        int num_weeks;
        int num_games;
        int num_divs;
        int num_teams;
        int num_confs;
        int num_playoff_teams;
        int teams_per_division;

        Style Largelblstyle = (Style)System.Windows.Application.Current.FindResource("Largelbltyle");
        Style GroupBoxstyle = (Style)System.Windows.Application.Current.FindResource("GroupBoxstyle");
        Style Largetxttyle = (Style)System.Windows.Application.Current.FindResource("Largetxttyle");
        Style MediumLargetxttyle = (Style)System.Windows.Application.Current.FindResource("MediumLargetxttyle");
        Style Conflbltyle = (Style)System.Windows.Application.Current.FindResource("Conflbltyle");
        Style UnselNewTeamSP = (Style)System.Windows.Application.Current.FindResource("UnselNewTeamSP");
        Style DragEnt_NewTeamSP = (Style)System.Windows.Application.Current.FindResource("DragEnt_NewTeamSP");

        League_Structure_by_Season ls = pw.Loaded_League.season.League_Structure_by_Season[0];

        //Set either the Logo Image or Label depending on if the user has selected a logo image
        if (ls.League_Logo_File == null || ls.League_Logo_File.Trim() == "")
            {
                League_Logo_lbl.Content = ls.Short_Name;
                League_Logo_lbl.Visibility = Visibility.Visible;
                League_image.Visibility = Visibility.Hidden;
            }
        else
            {
                string Logo_image_path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + System.IO.Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + System.IO.Path.DirectorySeparatorChar + ls.Short_Name + System.IO.Path.DirectorySeparatorChar + ls.League_Logo_File;
                League_image.Source = new BitmapImage(new Uri((string)Logo_image_path));
                League_Logo_lbl.Visibility = Visibility.Hidden;
                League_image.Visibility = Visibility.Visible;
            }

         num_weeks = (int) ls.Number_of_weeks;
        num_games = (int) ls.Number_of_Games;
        num_divs = (int) ls.Number_of_Divisions;
        num_teams = (int) ls.Num_Teams;
        num_confs = (int) ls.Number_of_Conferences;
        num_playoff_teams = (int) ls.Num_Playoff_Teams;
        teams_per_division = num_teams / num_divs;

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

            var lblConf1 = new Label();
            lblConf1.Name = "standConf1";
            lblConf1.Width = 150;
            lblConf1.Style = Conflbltyle;

            conf1_sp.Children.Add(lblConf1);
            v_sp1.Children.Add(conf1_sp);

            this.RegisterName(lblConf1.Name, lblConf1);

            for (int i = 1; i <= num_divs_per_conf; i++)
            {

                ListView lbDiv = new ListView();
                lbDiv.Name = "DivGrid" + i.ToString();
                lbDiv.Style = (Style)FindResource("StandingsGridStyle");

                GridView gr = new GridView();
                
                GridViewColumn grc1 = new GridViewColumn(); grc1.Header = ""; grc1.Width = 20; grc1.DisplayMemberBinding = new Binding("clinch_char");
                grc1.HeaderContainerStyle = DivGridHeader_Style;

                FrameworkElementFactory h_image = new FrameworkElementFactory(typeof(Image));
                Binding b = new Binding("HelmetImage");
                h_image.SetBinding(Image.SourceProperty, b);

                h_image.SetValue(Image.WidthProperty, 20.0);
                h_image.SetValue(Image.HeightProperty, 20.0);

                FrameworkElementFactory lb_team = new FrameworkElementFactory(typeof(Label));
                Binding b2 = new Binding("Team_Name");
                lb_team.SetBinding(Label.ContentProperty, b2);

                FrameworkElementFactory sp_team = new FrameworkElementFactory(typeof(StackPanel));
                sp_team.SetValue(StackPanel.OrientationProperty, Orientation.Horizontal);
                sp_team.AppendChild(h_image);
                sp_team.AppendChild(lb_team);
                GridViewColumn grc2 = new GridViewColumn(); grc2.Header = ""; grc2.Width = 200;
                DataTemplate datatemp = new DataTemplate();
                datatemp.VisualTree = sp_team;
                grc2.CellTemplate = datatemp;
                grc2.Header = pw.Loaded_League.season.Divisions.Where(x => x.ID == i).Select(x => x.Name).First();
                grc2.HeaderContainerStyle = (Style)FindResource("HeaderStyleLeft");

                GridViewColumn grc3 = new GridViewColumn(); grc3.Header = "W"; grc3.Width = 20; grc3.DisplayMemberBinding = new Binding("wins");
                grc3.HeaderContainerStyle = DivGridHeader_Style;
                GridViewColumn grc4 = new GridViewColumn(); grc4.Header = "L"; grc4.Width = 20; grc4.DisplayMemberBinding = new Binding("loses");
                grc4.HeaderContainerStyle = DivGridHeader_Style;
                GridViewColumn grc5 = new GridViewColumn(); grc5.Header = "T"; grc5.Width = 20; grc5.DisplayMemberBinding = new Binding("ties");
                grc5.HeaderContainerStyle = DivGridHeader_Style;
                GridViewColumn grc6 = new GridViewColumn(); grc6.Header = "PCT"; grc6.Width = 30; grc6.DisplayMemberBinding = new Binding("winpct");
                grc6.HeaderContainerStyle = DivGridHeader_Style;
                GridViewColumn grc7 = new GridViewColumn(); grc7.Header = "PF"; grc7.Width = 30; grc7.DisplayMemberBinding = new Binding("pointsfor");
                grc7.HeaderContainerStyle = DivGridHeader_Style;
                GridViewColumn grc8 = new GridViewColumn(); grc8.Header = "PA"; grc8.Width = 30; grc8.DisplayMemberBinding = new Binding("pointagainst");
                grc8.HeaderContainerStyle = DivGridHeader_Style;
                GridViewColumn grc9 = new GridViewColumn(); grc9.Header = "Strk"; grc9.Width = 30; grc9.DisplayMemberBinding = new Binding("Streakchar");
                grc9.HeaderContainerStyle = DivGridHeader_Style;
                gr.Columns.Add(grc1); gr.Columns.Add(grc2); gr.Columns.Add(grc3);
                gr.Columns.Add(grc4); gr.Columns.Add(grc5); gr.Columns.Add(grc6);
                gr.Columns.Add(grc7); gr.Columns.Add(grc8); gr.Columns.Add(grc9);
                lbDiv.View = gr;
                lbDiv.Margin = new Thickness(15, 0, 15, 11);
                lbDiv.AddHandler(GridViewRowPresenter.MouseLeftButtonUpEvent, new RoutedEventHandler(ListViewDiv_MouseUpEvent));
                v_sp1.Children.Add(lbDiv);

                this.RegisterName(lbDiv.Name, lbDiv);

            }

            var v_sp2 = new StackPanel();
            v_sp2.Orientation = Orientation.Vertical;
            v_sp2.VerticalAlignment = VerticalAlignment.Top;
            v_sp2.HorizontalAlignment = HorizontalAlignment.Center;

            var conf2_sp = new StackPanel();
            conf2_sp.Orientation = Orientation.Horizontal;
            conf2_sp.HorizontalAlignment = HorizontalAlignment.Center;

            var lblConf2 = new Label();
            lblConf2.Name = "standConf2";
            lblConf2.Width = 150;
            lblConf2.Style = Conflbltyle;

            conf2_sp.Children.Add(lblConf2);
            v_sp2.Children.Add(conf2_sp);

            this.RegisterName(lblConf2.Name, lblConf2);

            for (int i = num_divs_per_conf + 1; i <= num_divs; i++)
            {
                ListView lbDiv = new ListView();
                lbDiv.Name = "DivGrid" + i.ToString();
                lbDiv.Style = (Style)FindResource("StandingsGridStyle");

                GridView gr = new GridView();
                GridViewColumn grc1 = new GridViewColumn(); grc1.Header = ""; grc1.Width = 20; grc1.DisplayMemberBinding = new Binding("clinch_char");
                grc1.HeaderContainerStyle = DivGridHeader_Style;

                FrameworkElementFactory h_image = new FrameworkElementFactory(typeof(Image));
                    //                Binding b = new Binding("Helmet_img");
                    //                h_image.SetBinding(Image.SourceProperty, b);
                Binding b = new Binding("HelmetImage");
                h_image.SetBinding(Image.SourceProperty, b);
                h_image.SetValue(Image.WidthProperty, 20.0);
                h_image.SetValue(Image.HeightProperty, 20.0);

                FrameworkElementFactory lb_team = new FrameworkElementFactory(typeof(Label));
                Binding b2 = new Binding("Team_Name");
                lb_team.SetBinding(Label.ContentProperty, b2);

                FrameworkElementFactory sp_team = new FrameworkElementFactory(typeof(StackPanel));
                sp_team.SetValue(StackPanel.OrientationProperty, Orientation.Horizontal);
                sp_team.AppendChild(h_image);
                sp_team.AppendChild(lb_team);
                GridViewColumn grc2 = new GridViewColumn(); grc2.Header = ""; grc2.Width = 200;
                DataTemplate datatemp = new DataTemplate();
                datatemp.VisualTree = sp_team;
                grc2.CellTemplate = datatemp;
                grc2.Header = pw.Loaded_League.season.Divisions.Where(x => x.ID == i).Select(x => x.Name).First();
                grc2.HeaderContainerStyle = (Style)FindResource("HeaderStyleLeft");

                GridViewColumn grc3 = new GridViewColumn(); grc3.Header = "W"; grc3.Width = 20; grc3.DisplayMemberBinding = new Binding("wins");
                grc3.HeaderContainerStyle = DivGridHeader_Style;
                GridViewColumn grc4 = new GridViewColumn(); grc4.Header = "L"; grc4.Width = 20; grc4.DisplayMemberBinding = new Binding("loses");
                grc4.HeaderContainerStyle = DivGridHeader_Style;
                GridViewColumn grc5 = new GridViewColumn(); grc5.Header = "T"; grc5.Width = 20; grc5.DisplayMemberBinding = new Binding("ties");
                grc5.HeaderContainerStyle = DivGridHeader_Style;
                GridViewColumn grc6 = new GridViewColumn(); grc6.Header = "PCT"; grc6.Width = 30; grc6.DisplayMemberBinding = new Binding("winpct");
                grc6.HeaderContainerStyle = DivGridHeader_Style;
                GridViewColumn grc7 = new GridViewColumn(); grc7.Header = "PF"; grc7.Width = 30; grc7.DisplayMemberBinding = new Binding("pointsfor");
                grc7.HeaderContainerStyle = DivGridHeader_Style;
                GridViewColumn grc8 = new GridViewColumn(); grc8.Header = "PA"; grc8.Width = 30; grc8.DisplayMemberBinding = new Binding("pointagainst");
                grc8.HeaderContainerStyle = DivGridHeader_Style;
                GridViewColumn grc9 = new GridViewColumn(); grc9.Header = "Strk"; grc9.Width = 30; grc9.DisplayMemberBinding = new Binding("Streakchar");
                grc9.HeaderContainerStyle = DivGridHeader_Style;
                gr.Columns.Add(grc1); gr.Columns.Add(grc2); gr.Columns.Add(grc3);
                gr.Columns.Add(grc4); gr.Columns.Add(grc5); gr.Columns.Add(grc6);
                gr.Columns.Add(grc7); gr.Columns.Add(grc8); gr.Columns.Add(grc9);
                lbDiv.View = gr;
                lbDiv.Margin = new Thickness(15, 0, 15, 11);
                lbDiv.AddHandler(GridViewRowPresenter.MouseLeftButtonUpEvent, new RoutedEventHandler(ListViewDiv_MouseUpEvent));
                v_sp2.Children.Add(lbDiv);

                this.RegisterName(lbDiv.Name, lbDiv);


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
                    ListView lbDiv = new ListView();
                    lbDiv.Name = "DivGrid" + i.ToString();
                    lbDiv.Style = (Style)FindResource("StandingsGridStyle");

                    GridView gr = new GridView();
                    GridViewColumn grc1 = new GridViewColumn(); grc1.Header = ""; grc1.Width = 20; grc1.DisplayMemberBinding = new Binding("clinch_char");
                    grc1.HeaderContainerStyle = DivGridHeader_Style;

                    FrameworkElementFactory h_image = new FrameworkElementFactory(typeof(Image));
                    Binding b = new Binding("HelmetImage");
                    h_image.SetBinding(Image.SourceProperty, b);
                    h_image.SetValue(Image.WidthProperty, 20.0);
                    h_image.SetValue(Image.HeightProperty, 20.0);

                    FrameworkElementFactory lb_team = new FrameworkElementFactory(typeof(Label));
                    Binding b2 = new Binding("Team_Name");
                    lb_team.SetBinding(Label.ContentProperty, b2);

                    FrameworkElementFactory sp_team = new FrameworkElementFactory(typeof(StackPanel));
                    sp_team.SetValue(StackPanel.OrientationProperty, Orientation.Horizontal);
                    sp_team.AppendChild(h_image);
                    sp_team.AppendChild(lb_team);
                    GridViewColumn grc2 = new GridViewColumn(); grc2.Header = ""; grc2.Width = 200;
                    DataTemplate datatemp = new DataTemplate();
                    datatemp.VisualTree = sp_team;
                    grc2.CellTemplate = datatemp;
                    grc2.Header = pw.Loaded_League.season.Divisions.Where(x => x.ID == i).Select(x => x.Name).First();
                    grc2.HeaderContainerStyle = (Style)FindResource("HeaderStyleLeft");

                    GridViewColumn grc3 = new GridViewColumn(); grc3.Header = "W"; grc3.Width = 20; grc3.DisplayMemberBinding = new Binding("wins");
                    grc3.HeaderContainerStyle = DivGridHeader_Style;
                    GridViewColumn grc4 = new GridViewColumn(); grc4.Header = "L"; grc4.Width = 20; grc4.DisplayMemberBinding = new Binding("loses");
                    grc4.HeaderContainerStyle = DivGridHeader_Style;
                    GridViewColumn grc5 = new GridViewColumn(); grc5.Header = "T"; grc5.Width = 20; grc5.DisplayMemberBinding = new Binding("ties");
                    grc5.HeaderContainerStyle = DivGridHeader_Style;
                    GridViewColumn grc6 = new GridViewColumn(); grc6.Header = "PCT"; grc6.Width = 30; grc6.DisplayMemberBinding = new Binding("winpct");
                    grc6.HeaderContainerStyle = DivGridHeader_Style;
                    GridViewColumn grc7 = new GridViewColumn(); grc7.Header = "PF"; grc7.Width = 30; grc7.DisplayMemberBinding = new Binding("pointsfor");
                    grc7.HeaderContainerStyle = DivGridHeader_Style;
                    GridViewColumn grc8 = new GridViewColumn(); grc8.Header = "PA"; grc8.Width = 30; grc8.DisplayMemberBinding = new Binding("pointagainst");
                    grc8.HeaderContainerStyle = DivGridHeader_Style;
                    GridViewColumn grc9 = new GridViewColumn(); grc9.Header = "Strk"; grc9.Width = 30;  grc9.DisplayMemberBinding = new Binding("Streakchar");
                    grc9.HeaderContainerStyle = DivGridHeader_Style;
                    gr.Columns.Add(grc1); gr.Columns.Add(grc2); gr.Columns.Add(grc3);
                    gr.Columns.Add(grc4); gr.Columns.Add(grc5); gr.Columns.Add(grc6);
                    gr.Columns.Add(grc7); gr.Columns.Add(grc8); gr.Columns.Add(grc9);
                    lbDiv.View = gr;
                    lbDiv.Margin = new Thickness(15, 0, 15, 11);

                    lbDiv.AddHandler(GridViewRowPresenter.MouseLeftButtonUpEvent, new RoutedEventHandler(ListViewDiv_MouseUpEvent));

                    v2_sp.Children.Add(lbDiv);

                    this.RegisterName(lbDiv.Name, lbDiv);
                }

                sp1.Children.Add(v2_sp);
        }

        setStandings();

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
        public void setStandings()
        {
            Style Teamlbltyle = (Style)System.Windows.Application.Current.FindResource("Teamlbltyle");

            logger.Info("Setting team labels");

            if (pw.Loaded_League.season.League_Structure_by_Season[0].Number_of_Conferences > 0)
            {
                Label ConfLbl = (Label)this.FindName("standConf1");
                Label ConfLb2 = (Label)this.FindName("standConf2");
                ConfLbl.Content = pw.Loaded_League.season.Conferences.Where(x => x.Ordinal == 1).Select(b => b.Conf_Name).First();
                ConfLb2.Content = pw.Loaded_League.season.Conferences.Where(x => x.Ordinal == 2).Select(b => b.Conf_Name).First();

            }

            League_Structure_by_Season ls = pw.Loaded_League.season.League_Structure_by_Season[0];
            int num_divs = (int)ls.Number_of_Divisions;
            int teams_per_div = (int)ls.Num_Teams / num_divs;
            for (int i = 1; i <= num_divs; i++)
            {
                string divgrid = "DivGrid" + i.ToString();
                ListView lsDivGrid = (ListView)this.FindName(divgrid);
                lsDivGrid.Items.Clear();

                for (int t = 0; t < teams_per_div; t++)
                {
                    int ind = ((i - 1) * teams_per_div) + t;
                    lsDivGrid.Items.Add(pw.Loaded_League.Standings[ind]);
                }


            }

        }
        private void ListViewDiv_MouseUpEvent(object sender, RoutedEventArgs e)
        {
            ListView ls = (ListView)sender;

            if (ls.SelectedItems.Count > 0)
            {
                var rw = ls.SelectedItems[0];
                string divName = ls.Name;

                long teams_per_div = pw.Loaded_League.season.League_Structure_by_Season[0].Num_Teams /
                    pw.Loaded_League.season.League_Structure_by_Season[0].Number_of_Divisions;
                string div_string = Regex.Replace(divName, "[^0-9.]", "");
                int div_num = int.Parse(div_string);
                int div_team_num = ls.SelectedIndex;
                int n = (int)(((div_num - 1) * teams_per_div) + div_team_num) + 1;
                Show_TeamDetail?.Invoke(this, new teamEventArgs(n));
            }
        }

        private void standBack_Click(object sender, RoutedEventArgs e)
        {
            pw.Loaded_League = null;
            Show_MainMenu?.Invoke(this, new EventArgs());
        }

        private void standingsSeasonsdb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (standingsSeasonsdb.SelectedValue != null)
            {

                string sYear = null;
                bool Latest_year = true;

                if (standingsSeasonsdb.SelectedIndex == 0)
                    GoCurrentSeason.IsEnabled = false;
                else
                {
                    GoCurrentSeason.IsEnabled = true;
                    sYear = standingsSeasonsdb.SelectedValue.ToString();
                    Latest_year = false;
                }

                //Load the league season.  Null for year parameter mean load the latest year
                string short_name = pw.Loaded_League.season.League_Structure_by_Season[0].Short_Name;
                League_Services ls = new League_Services();
                pw.Loaded_League.season = ls.LoadSeason(sYear, short_name);
                pw.Loaded_League.Current_Year = (long)standingsSeasonsdb.SelectedValue;

                //Set league state
                pw.Loaded_League.LState = ls.getSeasonState(Latest_year, pw.Loaded_League.season.ID, short_name);

                //Set top menu based on league state
                pw.setMenuonState(pw.Loaded_League.LState, pw.Loaded_League);

                //Load the league standings
                pw.Loaded_League.Standings = ls.getLeageStandings(pw.Loaded_League);

                //Update Standings
                setStandings();
            }

        }

        private void GoCurrentSeason_Click(object sender, RoutedEventArgs e)
        {
            standingsSeasonsdb.SelectedIndex = 0;
        }

        private void standings_NextStepbtn_Click(object sender, RoutedEventArgs e)
        {
            var Next_form = new Next_Standings();
            Next_form.Top = (SystemParameters.PrimaryScreenHeight - Next_form.Height) / 2;
            Next_form.Left = (SystemParameters.PrimaryScreenWidth - Next_form.Width) / 2;
            Next_form.SetContent(pw.Loaded_League.LState);
            Next_form.ShowDialog();
        }

        private void help_btn_Click(object sender, RoutedEventArgs e)
        {

            var help_form = new Help_LeagueStandings();
            help_form.Top = (SystemParameters.PrimaryScreenHeight - help_form.Height) / 2;
            help_form.Left = (SystemParameters.PrimaryScreenWidth - help_form.Width) / 2;
            help_form.ShowDialog();

        }
    }
}
