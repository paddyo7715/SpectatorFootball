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

        }
    }
}
