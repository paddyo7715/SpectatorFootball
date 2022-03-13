using SpectatorFootball.Playoffs;
using SpectatorFootball.Services;
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
    /// Interaction logic for PlayoffsUX.xaml
    /// </summary>
    public partial class PlayoffsUX : UserControl
    {
        private MainWindow pw;
        private List<int> WeeklyGameSlots = null;
        private long num_playoffteams;
        private const double GAMESLOT_LENGTH = 150;
        private const double GAMESLOT_HEIGHT = 80;
        private double HORIZONTAL_ADJ = 50.0;
        private double VERTICAL_ADJ = 30;
        private const double CHAMP_GAME_TOP = 75;
        private const double HELMET_WIDTH_MULT = 0.266;
        private const double CITY_WIDTH_MULT = 0.533;
        private const double SCORE_WIDTH_NULT = 0.20;
        private double Canvas_width;
        private double Canvas_height;
        private double width_adjustment = 0.0;
        List<Playoff_Bracket_rec> BracketsData = null;

        public event EventHandler Show_Standings;
        public PlayoffsUX(MainWindow pw)
        {
            InitializeComponent();
            lblPlayoffHeader.Content = pw.Loaded_League.season.Year + " " +
                pw.Loaded_League.season.League_Structure_by_Season[0].Short_Name +
                " Playoffs";
            this.pw = pw;

            Canvas_width = cnvPlayoffBrackets.Width;

            num_playoffteams = pw.Loaded_League.season.League_Structure_by_Season[0].Num_Playoff_Teams;

            WeeklyGameSlots = Playoff_Helper.PlayoffGamesPerWeek(num_playoffteams);

            if (WeeklyGameSlots.Count > 3)
            {
                HORIZONTAL_ADJ = 30.0;
                width_adjustment = 100.0;
            }
            else
                HORIZONTAL_ADJ = 80.0;

            setGameSlots();
        }

        private void setGameSlots()
        {
            int weeks = WeeklyGameSlots.Count;
            int weeksLeft = weeks;
            double h_value = 0.0;
            long gslot_id = 0;
            Playoff_Services ploff_services = new Playoff_Services();
            BracketsData = ploff_services.getPlayoffGames(pw.Loaded_League);

            Canvas_height = cnvPlayoffBrackets.Height;
            Canvas_width = cnvPlayoffBrackets.Width;

            double gs_Left;
            //set the horizontal space inbetween weekly games on left and right
            double temp = (GAMESLOT_LENGTH * weeksLeft) + (HORIZONTAL_ADJ * (weeksLeft)) + width_adjustment;
            gs_Left = (Canvas_width - temp) / 2;

            foreach (int i in WeeklyGameSlots)
            {

                int num_games = i;

                double week_game_height = (GAMESLOT_HEIGHT * (num_games/2)) + ((VERTICAL_ADJ) * (num_games-1)) ;
                double gs_running_top = (Canvas_height - week_game_height) / 2;

                for (int j = 1; j <= num_games / 2; j++)
                {

                    string gs_name = gs_name = "gw" + i.ToString() + "_" + j.ToString();

                    gslot_id++;
                    CreateGameSlot(gslot_id, gs_running_top, gs_Left - GAMESLOT_LENGTH);

                    gslot_id++;
                    CreateGameSlot(gslot_id, gs_running_top, Canvas_width - gs_Left);

                    gs_running_top += GAMESLOT_HEIGHT + VERTICAL_ADJ;
                }

                weeksLeft--;
                gs_Left += GAMESLOT_LENGTH + HORIZONTAL_ADJ;
            }

            //The championship game and label
            gslot_id++;

            Label lblChamp = new Label();
            lblChamp.Width = GAMESLOT_LENGTH;
            lblChamp.HorizontalContentAlignment = HorizontalAlignment.Center;
            lblChamp.Content = pw.Loaded_League.season.League_Structure_by_Season[0].Championship_Game_Name + " " + pw.Loaded_League.Current_Year;
            cnvPlayoffBrackets.Children.Add(lblChamp);
            Canvas.SetTop(lblChamp, CHAMP_GAME_TOP - 30);
            Canvas.SetLeft(lblChamp, (Canvas_width - GAMESLOT_LENGTH) / 2);

            CreateGameSlot(gslot_id, CHAMP_GAME_TOP, (Canvas_width - GAMESLOT_LENGTH) / 2);
        }
        private void help_btn_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btnStandings_Click(object sender, RoutedEventArgs e)
        {
            if (Mouse.OverrideCursor == Cursors.Wait) return;
            Show_Standings?.Invoke(this, new EventArgs());
        }

        private void CreateGameSlot(long gslot_id, double pTop, double pLeft)
        {
            Boolean bGameAvailable = gslot_id <= BracketsData.Count ? true : false;

            StackPanel AwaySP = new StackPanel();
            AwaySP.Height = GAMESLOT_HEIGHT / 2;
            AwaySP.Width = GAMESLOT_LENGTH;
            AwaySP.Orientation = Orientation.Horizontal;

            var Away_helmet_img = new Image();
            Away_helmet_img.Width = GAMESLOT_LENGTH * HELMET_WIDTH_MULT;
            Away_helmet_img.Height = GAMESLOT_HEIGHT / 2;
            if (bGameAvailable)
                Away_helmet_img.Source = BracketsData[(int)gslot_id - 1].Away_HelmetImage;

            Label AwayCity = new Label();
            AwayCity.HorizontalContentAlignment = HorizontalAlignment.Left;
            AwayCity.Height = GAMESLOT_HEIGHT / 2;
            AwayCity.Width = GAMESLOT_LENGTH * CITY_WIDTH_MULT;
            if (bGameAvailable)
                AwayCity.Content = BracketsData[(int)gslot_id - 1].Away_city;

                Label AwayScore = new Label();
            AwayScore.Content = "";
            AwayScore.HorizontalContentAlignment = HorizontalAlignment.Right;
            AwayScore.Height = GAMESLOT_HEIGHT / 2;
            AwayScore.Width = GAMESLOT_LENGTH * SCORE_WIDTH_NULT;
            if (bGameAvailable)
                AwayScore.Content = BracketsData[(int)gslot_id - 1].Away_Score;

            AwaySP.Children.Add(Away_helmet_img);
            AwaySP.Children.Add(AwayCity);
            AwaySP.Children.Add(AwayScore);

            StackPanel HomeSP = new StackPanel();
            HomeSP.Height = GAMESLOT_HEIGHT / 2;
            HomeSP.Width = GAMESLOT_LENGTH;
            HomeSP.Orientation = Orientation.Horizontal;

            var Home_helmet_img = new Image();
            Home_helmet_img.Width = GAMESLOT_LENGTH * HELMET_WIDTH_MULT;
            Home_helmet_img.Height = GAMESLOT_HEIGHT / 2;
            if (bGameAvailable)
                Home_helmet_img.Source = BracketsData[(int)gslot_id - 1].Home_HelmetImage;

            Label HomeCity = new Label();
            HomeCity.HorizontalContentAlignment = HorizontalAlignment.Left;
            HomeCity.Height = GAMESLOT_HEIGHT / 2;
            HomeCity.Width = GAMESLOT_LENGTH * CITY_WIDTH_MULT;
            if (bGameAvailable)
                HomeCity.Content = BracketsData[(int)gslot_id - 1].Home_city;

            Label HomeScore = new Label();
            HomeScore.Content = "";
            HomeScore.HorizontalContentAlignment = HorizontalAlignment.Right;
            HomeScore.Height = GAMESLOT_HEIGHT / 2;
            HomeScore.Width = GAMESLOT_LENGTH * SCORE_WIDTH_NULT;
            if (bGameAvailable)
                HomeScore.Content = BracketsData[(int)gslot_id - 1].Home_Score;

            HomeSP.Children.Add(Home_helmet_img);
            HomeSP.Children.Add(HomeCity);
            HomeSP.Children.Add(HomeScore);

            StackPanel sp = new StackPanel();
            sp.Orientation = Orientation.Vertical;
            sp.Name = "gsSlot_" + gslot_id.ToString();
            sp.Background = Brushes.White;
            sp.Height = GAMESLOT_HEIGHT;
            sp.Width = GAMESLOT_LENGTH;

            sp.Children.Add(AwaySP);
            sp.Children.Add(HomeSP);

            if (bGameAvailable)
            {
                sp.MouseDown += bracket_mouseDown;
                BracketsData[(int)gslot_id - 1].Panel_name = sp.Name;
            }

            cnvPlayoffBrackets.Children.Add(sp);
            Canvas.SetTop(sp, pTop);
            Canvas.SetLeft(sp, pLeft);
        }

        private void bracket_mouseDown(object sender, RoutedEventArgs e)
        {
            StackPanel sp = (StackPanel)sender;
            long game_id = BracketsData.Where(x => x.Panel_name == sp.Name).Select(x => x.game_id).First();
 
        }
        }


}
