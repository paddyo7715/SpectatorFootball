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
        public LeagueStandings(MainWindow pw)
        {
            InitializeComponent();
            this.pw = pw;
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
        int j;
        Style Largelblstyle = (Style)System.Windows.Application.Current.FindResource("Largelbltyle");
        Style GroupBoxstyle = (Style)System.Windows.Application.Current.FindResource("GroupBoxstyle");
        Style Largetxttyle = (Style)System.Windows.Application.Current.FindResource("Largetxttyle");
        Style MediumLargetxttyle = (Style)System.Windows.Application.Current.FindResource("MediumLargetxttyle");
        Style Conflbltyle = (Style)System.Windows.Application.Current.FindResource("Conflbltyle");
        Style UnselNewTeamSP = (Style)System.Windows.Application.Current.FindResource("UnselNewTeamSP");
        Style DragEnt_NewTeamSP = (Style)System.Windows.Application.Current.FindResource("DragEnt_NewTeamSP");

        League_Structure_by_Season ls = pw.Loaded_League.season.League_Structure_by_Season[0];

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

                FrameworkElementFactory h_image = new FrameworkElementFactory(typeof(Image));
                Binding b = new Binding("Helmet_img");
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
                GridViewColumn grc4 = new GridViewColumn(); grc4.Header = "L"; grc4.Width = 20; grc4.DisplayMemberBinding = new Binding("loses");
                GridViewColumn grc5 = new GridViewColumn(); grc5.Header = "T"; grc5.Width = 20; grc5.DisplayMemberBinding = new Binding("ties");
                GridViewColumn grc6 = new GridViewColumn(); grc6.Header = "PCT"; grc6.Width = 30; grc6.DisplayMemberBinding = new Binding("winpct");
                GridViewColumn grc7 = new GridViewColumn(); grc7.Header = "PF"; grc7.Width = 30; grc7.DisplayMemberBinding = new Binding("pointsfor");
                GridViewColumn grc8 = new GridViewColumn(); grc8.Header = "PA"; grc8.Width = 30; grc8.DisplayMemberBinding = new Binding("pointagainst");
                GridViewColumn grc9 = new GridViewColumn(); grc9.Header = "Strk"; grc9.Width = 30; grc9.DisplayMemberBinding = new Binding("Streakchar");
                gr.Columns.Add(grc1); gr.Columns.Add(grc2); gr.Columns.Add(grc3);
                gr.Columns.Add(grc4); gr.Columns.Add(grc5); gr.Columns.Add(grc6);
                gr.Columns.Add(grc7); gr.Columns.Add(grc8); gr.Columns.Add(grc9);
                lbDiv.View = gr;
                lbDiv.Margin = new Thickness(15, 0, 15, 11);

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

                FrameworkElementFactory h_image = new FrameworkElementFactory(typeof(Image));
                Binding b = new Binding("Helmet_img");
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
                GridViewColumn grc4 = new GridViewColumn(); grc4.Header = "L"; grc4.Width = 20; grc4.DisplayMemberBinding = new Binding("loses");
                GridViewColumn grc5 = new GridViewColumn(); grc5.Header = "T"; grc5.Width = 20; grc5.DisplayMemberBinding = new Binding("ties");
                GridViewColumn grc6 = new GridViewColumn(); grc6.Header = "PCT"; grc6.Width = 30; grc6.DisplayMemberBinding = new Binding("winpct");
                GridViewColumn grc7 = new GridViewColumn(); grc7.Header = "PF"; grc7.Width = 30; grc7.DisplayMemberBinding = new Binding("pointsfor");
                GridViewColumn grc8 = new GridViewColumn(); grc8.Header = "PA"; grc8.Width = 30; grc8.DisplayMemberBinding = new Binding("pointagainst");
                GridViewColumn grc9 = new GridViewColumn(); grc9.Header = "Strk"; grc9.Width = 30; grc9.DisplayMemberBinding = new Binding("Streakchar");
                gr.Columns.Add(grc1); gr.Columns.Add(grc2); gr.Columns.Add(grc3);
                gr.Columns.Add(grc4); gr.Columns.Add(grc5); gr.Columns.Add(grc6);
                gr.Columns.Add(grc7); gr.Columns.Add(grc8); gr.Columns.Add(grc9);
                lbDiv.View = gr;
                lbDiv.Margin = new Thickness(15, 0, 15, 11);

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

                    FrameworkElementFactory h_image = new FrameworkElementFactory(typeof(Image));
                    Binding b = new Binding("Helmet_img");
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
                    GridViewColumn grc4 = new GridViewColumn(); grc4.Header = "L"; grc4.Width = 20; grc4.DisplayMemberBinding = new Binding("loses");
                    GridViewColumn grc5 = new GridViewColumn(); grc5.Header = "T"; grc5.Width = 20; grc5.DisplayMemberBinding = new Binding("ties");
                    GridViewColumn grc6 = new GridViewColumn(); grc6.Header = "PCT"; grc6.Width = 30; grc6.DisplayMemberBinding = new Binding("winpct");
                    GridViewColumn grc7 = new GridViewColumn(); grc7.Header = "PF"; grc7.Width = 30; grc7.DisplayMemberBinding = new Binding("pointsfor");
                    GridViewColumn grc8 = new GridViewColumn(); grc8.Header = "PA"; grc8.Width = 30; grc8.DisplayMemberBinding = new Binding("pointagainst");
                    GridViewColumn grc9 = new GridViewColumn(); grc9.Header = "Strk"; grc9.Width = 30;  grc9.DisplayMemberBinding = new Binding("Streakchar");
                    gr.Columns.Add(grc1); gr.Columns.Add(grc2); gr.Columns.Add(grc3);
                    gr.Columns.Add(grc4); gr.Columns.Add(grc5); gr.Columns.Add(grc6);
                    gr.Columns.Add(grc7); gr.Columns.Add(grc8); gr.Columns.Add(grc9);
                    lbDiv.View = gr;
                    lbDiv.Margin = new Thickness(15, 0, 15, 11);

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



                /*                string teamLabel = "newllblTeam" + i.ToString();
                                string teamImage = "newlimgTeam" + i.ToString();
                                logger.Debug("setting teamlabel and teamimage " + " " + teamImage);

                                Label teamLbl = (Label)this.FindName(teamLabel);
                                Image teamImg = (Image)this.FindName(teamImage);
                                logger.Debug("teamlabel and teamimg found");

                                teamLbl.Style = Teamlbltyle;

                                List<Standings_Row> st = pw.Loaded_League.Standings;
                                teamLbl.Content = st[i-1].Team_Name;
                                teamLbl.VerticalContentAlignment = VerticalAlignment.Center;
                */

                //                string img_path = pw.New_Mem_Season.Season.Teams_by_Season[i - 1].Helmet_img_path;

                //                 if (img_path != null && img_path.Length > 0)
                //                {
                //                    var helmetIMG_source = new BitmapImage(new Uri(img_path));
                //                    teamImg.Source = helmetIMG_source;
                //               }
            }

        }
    }
}
