using log4net;
using SpectatorFootball.Models;
using SpectatorFootball.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Timers;
using System.Windows.Threading;
using SpectatorFootball.GameNS;

namespace SpectatorFootball.WindowsLeague
{
    /// <summary>
    /// Interaction logic for Game_Window.xaml
    /// </summary>
    public partial class Game_Window : Window
    {
        private static ILog logger = LogManager.GetLogger("RollingFile");

        public event EventHandler Set_TopMenu;

        private MainWindow pw;
        private Teams_by_Season at = null;
        private Teams_by_Season ht = null;

        private List<Player_and_Ratings> Away_Players = null;
        private List<Player_and_Ratings> Home_Players = null;

        private DispatcherTimer dispatcherTimer = new DispatcherTimer();
        private DispatcherTimer GameTimer = new DispatcherTimer();

        private string Field_File = "";

        private int Can_Width;
        private int Can_Height;

        private int back_width = 2480;
        private int back_height = 680;
        private int Field_Border = 40;
        private int EndZonePixels = 200;
        private int Pixels_per_yard = 20;

        private int Width_dir = -1;
        private int Height_dir = -1;

        //this is the model 
        private Game g = null;

        //This is the game engine where the game is played.
        GameEngine ge = null;

        Rectangle Ball = new Rectangle();

        public Game_Window(MainWindow pw, Game g)
        {
            InitializeComponent();
            this.pw = pw;
            this.g = g;

            Gamepnl.Visibility = Visibility.Collapsed;

            Game_Services gs = new Game_Services();

            try
            {
                lblLeague.Content = pw.Loaded_League.season.League_Structure_by_Season[0].Long_Name;
                at = pw.Loaded_League.season.Teams_by_Season.Where(x => x.Franchise_ID == g.Away_Team_Franchise_ID).First();
                ht = pw.Loaded_League.season.Teams_by_Season.Where(x => x.Franchise_ID == g.Home_Team_Franchise_ID).First();
                string away_record = pw.Loaded_League.getTeamStandings(at.City + " " + at.Nickname);
                string home_record = pw.Loaded_League.getTeamStandings(ht.City + " " + ht.Nickname);

                Away_Helmet.Source = pw.Loaded_League.getHelmetImg(at.Helmet_Image_File);
                AwayCity.Content = at.City;
                AwayName.Content = at.Nickname;
                AwayRecord.Content = "(" + away_record + ")";

                Home_Helmet.Source = pw.Loaded_League.getHelmetImg(ht.Helmet_Image_File);
                HomeCity.Content = ht.City;
                HomeName.Content = ht.Nickname;
                HomeRecord.Content = "(" + home_record + ")";

                lblCity.Content = ht.Stadium_Location;

                long season_id = pw.Loaded_League.season.ID;

                Away_Players = gs.GetTeamPlayersForGame(at.Franchise_ID, g.Week, pw.Loaded_League);
                Home_Players = gs.GetTeamPlayersForGame(ht.Franchise_ID, g.Week, pw.Loaded_League);

                Can_Width = (int) MyCanvas.Width;
                Can_Height = (int) MyCanvas.Height;

                dispatcherTimer.Tick += CloseGameInfo;
                dispatcherTimer.Interval = new TimeSpan(0, 0, 5);
                dispatcherTimer.Start();

                ge = new GameEngine(pw, g, (Teams_by_Season)at, (List<Player_and_Ratings>)Away_Players,
                    (Teams_by_Season)ht, (List<Player_and_Ratings>)Home_Players);

                Ball.Width = 8;
                Ball.Height = 8;

                //End of game not sure where this should go
                //gs.SaveGame(g, g.injuries, pw.Loaded_League);
                //Game done see if the state of the league has changed
                //Set_TopMenu?.Invoke(this, new EventArgs());
                //this.Close();



            }
            catch (Exception e)
            {
                Mouse.OverrideCursor = null;
                string err = "Error Loading Data to Start Game!";
                logger.Error(err);
                logger.Error(e);
                MessageBox.Show(CommonUtils.substr(err, 0, 100), "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }

        }

        private void Game_close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CloseGameInfo(object sender, EventArgs e)
        {
            dispatcherTimer.Stop();
            Game_intro_pnl.Visibility = Visibility.Collapsed;
            Gamepnl.Visibility = Visibility.Visible;

            //set away team
            lblAwayTeam.Content = at.Nickname;
            string[] m1 = Uniform.getTeamDispColors(at.Home_jersey_Color,
                at.Home_Jersey_Number_Color,
                at.Home_Jersey_Number_Outline_Color,
                at.Helmet_Color,
                at.Helmet_Logo_Color,
                at.Home_Pants_Color);

            lblAwayTeam.Foreground = new SolidColorBrush(CommonUtils.getColorfromHex(m1[1]));
            lblAwayTeam.Background = new SolidColorBrush(CommonUtils.getColorfromHex(m1[0]));

            lblHomeTeam.Content = ht.Nickname;
            string[] m2 = Uniform.getTeamDispColors(ht.Home_jersey_Color,
                ht.Home_Jersey_Number_Color,
                ht.Home_Jersey_Number_Outline_Color,
                ht.Helmet_Color,
                ht.Helmet_Logo_Color,
                ht.Home_Pants_Color);

            lblHomeTeam.Foreground = new SolidColorBrush(CommonUtils.getColorfromHex(m2[1]));
            lblHomeTeam.Background = new SolidColorBrush(CommonUtils.getColorfromHex(m2[0]));
            
            ImageBrush backgroundField = new ImageBrush();
            backgroundField.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/Stadiums/GenericGrass.png"));
         
            background.Fill = backgroundField;

            //This causes the field to move
            //                       GameTimer.Tick += ShowFrame;
            //                       GameTimer.Interval = TimeSpan.FromMilliseconds(20);
            //                       GameTimer.Start();

            bool bGameEneded = false;
            while (!bGameEneded)
            {

                Play_Struct Play = ge.ExecutePlay();

                //Place the ball on the field
                setBAll(Play);


                //Set the scoreboard after the play
                lblAwayScore.Content = Play.Away_Score;
                lblHomeScore.Content = Play.Home_Score;

                lblClock.Content = Play.Display_Time;
                lblQTR.Content = Play.Display_QTR;

                lblAwayTimeouts.Content = Play.Away_Timeouts;
                lblHomeTimeouts.Content = Play.Home_Timeouts;


                bGameEneded = Play.bGameOver;
                //just to test one play take this out.
            }





        }
        private void setBAll(Play_Struct Play)
        {
            int H_Pixel = Yardline_to_Pixel(Play.Line_of_Scimmage);
            double v_Pixel = VertPercent_to_Pixel(Play.Vertical_Ball_Placement);

            Ball.Fill = System.Windows.Media.Brushes.Brown;
            Ball.Stroke = System.Windows.Media.Brushes.Black;
            Canvas.SetTop(Ball, v_Pixel);
            Canvas.SetLeft(Ball, H_Pixel);
            if (!MyCanvas.Children.Contains(Ball))
                MyCanvas.Children.Add(Ball);
        }

        private void Show_Play()
        {
            Rectangle OPlayer1 = new Rectangle();
            OPlayer1.Width = 25;
            OPlayer1.Height = 25;
            OPlayer1.Fill = System.Windows.Media.Brushes.Blue;
            OPlayer1.Stroke = System.Windows.Media.Brushes.Black;
            //            OPlayer1.StrokeThickness = 2;
            Canvas.SetTop(OPlayer1, 400);
            Canvas.SetLeft(OPlayer1, 250);

            MyCanvas.Children.Add(OPlayer1);
        }


        private void btnSpeedSlower_click(object sender, EventArgs e)
        {

        }
        private void btnSpeedFaster_click(object sender, EventArgs e)
        {

        }
        private void btnPauseResume_click(object sender, EventArgs e)
        {

        }

        private void ShowFrame(object sender, EventArgs e)
        {
            int current_left = (int) Canvas.GetLeft(background);

//            int current_Top = (int)Canvas.GetTop(background);

            if ((current_left + (Can_Width + 60)) <= 0)
                Width_dir = 1;

            if (current_left >= 0)
                Width_dir = -1;
 /*
            if ((current_Top + (Can_Height + 60)) <= 0)
                Height_dir = 1;

            if (current_Top >= 0)
                Height_dir = -1;
 */

            Canvas.SetLeft(background, current_left + (3 * Width_dir));
//            Canvas.SetTop(background, current_Top + (3 * Height_dir));


        }

        private int Yardline_to_Pixel(double y)
        {
            int r = 0;

            r = Field_Border + EndZonePixels + (int)(y * Pixels_per_yard) - (int) (Ball.Width /2);

            return r;
        }

        private double VertPercent_to_Pixel(int v)
        {
            double r = 0.0;

            int verical_field_pixels = back_height - (int) Ball.Height - (Field_Border*2);
            r = (verical_field_pixels * (v / 100.0)) + Field_Border;

            return r;
        }
    }
}
