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
using System.Timers;
using System.Windows.Threading;
using SpectatorFootball.GameNS;
using System.Windows.Shapes;
using SpectatorFootball.Enum;

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

        private int back_width = 2680;
        private int back_height = 1280;
        private int Field_Border = 140;
        private int EndZonePixels = 200;
        private int Pixels_per_yard = 20;

        private int Width_dir = -1;
        private int Height_dir = -1;

        //this is the model 
        private Game g = null;

        //This is the game engine where the game is played!!
        public GameEngine ge = null;

        //ball objects
        public Game_Ball Game_Ball = new Game_Ball();
        public Ellipse Ball = new Ellipse();

        //Home and Away Player lists
        private List<Rectangle> Away_Players_rect = new List<Rectangle>();
        private List<Rectangle> Home_Players_rect = new List<Rectangle>();

        private const int PLAYER_SIZE = 50;
        private const int BALL_SIZE = 12;

        public const double VIEW_EDGE_OFFSET_YARDLINE = 12.0;

        //I'm not sure why I have to do this, but it seems that
        //the 1210 might not be the full view it might be just 1204
//        private const int RIGHT_PIXEL_FUDGE = 6;
        private const int RIGHT_PIXEL_FUDGE = 0;
        private const int TOP_PIXEL_FUDGE = 10;
        private double CANVAS_WIDTH;
        private double CANVAS_HEIGHT;


        private int VIEW_EDGE_PIXELS;

        private ImageBrush A_Standing_Player = new ImageBrush();
        private ImageBrush H_Standing_Player = new ImageBrush();

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

                Can_Width = (int)CANVAS_WIDTH;
                Can_Height = (int) MyCanvas.Height;

                dispatcherTimer.Tick += CloseGameInfo;
                dispatcherTimer.Interval = new TimeSpan(0, 0, 5);
                dispatcherTimer.Start();

                ge = new GameEngine(pw, g, (Teams_by_Season)at, (List<Player_and_Ratings>)Away_Players,
                    (Teams_by_Season)ht, (List<Player_and_Ratings>)Home_Players);

                VIEW_EDGE_PIXELS = Yardline_to_Pixel(VIEW_EDGE_OFFSET_YARDLINE, false);
                CANVAS_WIDTH = MyCanvas.Width - RIGHT_PIXEL_FUDGE;
                CANVAS_HEIGHT = MyCanvas.Height;

                //Add ball to canvas
                MyCanvas.Children.Add(Ball);

                //Create the player rectangles for both away and home
                for (int xxx=0; xxx < app_Constants.PLAYERS_ON_FIELD_PER_TEAM; xxx++)
                {
                    Rectangle ap = new Rectangle()
                    {
                        Height = PLAYER_SIZE,
                        Width = PLAYER_SIZE,
                    };

                    Away_Players_rect.Add(ap);
                    MyCanvas.Children.Add(ap);

                    Rectangle hp = new Rectangle
                    {
                        Height = PLAYER_SIZE,
                        Width = PLAYER_SIZE,
                    };

                    Home_Players_rect.Add(hp);
                    MyCanvas.Children.Add(hp);
                }

                //Load and colorize the sprite sheets
                Uniform_Image Uniform_Img = new Uniform_Image(CommonUtils.getAppPath() + System.IO.Path.DirectorySeparatorChar + "Images" + System.IO.Path.DirectorySeparatorChar + "Players" + System.IO.Path.DirectorySeparatorChar + "Player_Sprite_Sheet.png");

                Uniform_Img.Flip_All_Colors(true , CommonUtils.SystemDrawColorfromHex(ht.Helmet_Color) , CommonUtils.SystemDrawColorfromHex(ht.Helmet_Facemask_Color) , CommonUtils.SystemDrawColorfromHex(ht.Helmet_Logo_Color) , CommonUtils.SystemDrawColorfromHex(ht.Home_jersey_Color), CommonUtils.SystemDrawColorfromHex(ht.Home_Jersey_Number_Color) , CommonUtils.SystemDrawColorfromHex(ht.Home_Jersey_Number_Outline_Color) , CommonUtils.SystemDrawColorfromHex(ht.Home_Sleeve_Color) , CommonUtils.SystemDrawColorfromHex(ht.Home_Jersey_Shoulder_Stripe) , CommonUtils.SystemDrawColorfromHex(ht.Home_Jersey_Sleeve_Stripe_Color_1) , CommonUtils.SystemDrawColorfromHex(ht.Home_Jersey_Sleeve_Stripe_Color_2) , CommonUtils.SystemDrawColorfromHex(ht.Home_Jersey_Sleeve_Stripe_Color_3) , CommonUtils.SystemDrawColorfromHex(ht.Home_Jersey_Sleeve_Stripe_Color_4) , CommonUtils.SystemDrawColorfromHex(ht.Home_Jersey_Sleeve_Stripe_Color_5) , CommonUtils.SystemDrawColorfromHex(ht.Home_Jersey_Sleeve_Stripe_Color_6) , CommonUtils.SystemDrawColorfromHex(ht.Home_Pants_Color) , CommonUtils.SystemDrawColorfromHex(ht.Home_Pants_Stripe_Color_1) , CommonUtils.SystemDrawColorfromHex(ht.Home_Pants_Stripe_Color_2) , CommonUtils.SystemDrawColorfromHex(ht.Home_Pants_Stripe_Color_3) , CommonUtils.SystemDrawColorfromHex(ht.Socks_Color) , CommonUtils.SystemDrawColorfromHex(ht.Cleats_Color));
                Uniform_Img.Flip_All_Colors(false, CommonUtils.SystemDrawColorfromHex(at.Helmet_Color), CommonUtils.SystemDrawColorfromHex(at.Helmet_Facemask_Color), CommonUtils.SystemDrawColorfromHex(at.Helmet_Logo_Color), CommonUtils.SystemDrawColorfromHex(at.Away_jersey_Color), CommonUtils.SystemDrawColorfromHex(at.Away_Jersey_Number_Color), CommonUtils.SystemDrawColorfromHex(at.Away_Jersey_Number_Outline_Color), CommonUtils.SystemDrawColorfromHex(at.Away_Sleeve_Color), CommonUtils.SystemDrawColorfromHex(at.Away_Jersey_Shoulder_Stripe), CommonUtils.SystemDrawColorfromHex(at.Away_Jersey_Sleeve_Stripe_Color_1), CommonUtils.SystemDrawColorfromHex(at.Away_Jersey_Sleeve_Stripe_Color_2), CommonUtils.SystemDrawColorfromHex(at.Away_Jersey_Sleeve_Stripe_Color_3), CommonUtils.SystemDrawColorfromHex(at.Away_Jersey_Sleeve_Stripe_Color_4), CommonUtils.SystemDrawColorfromHex(at.Away_Jersey_Sleeve_Stripe_Color_5), CommonUtils.SystemDrawColorfromHex(at.Away_Jersey_Sleeve_Stripe_Color_6), CommonUtils.SystemDrawColorfromHex(at.Away_Pants_Color), CommonUtils.SystemDrawColorfromHex(at.Away_Pants_Stripe_Color_1), CommonUtils.SystemDrawColorfromHex(at.Away_Pants_Stripe_Color_2), CommonUtils.SystemDrawColorfromHex(at.Away_Pants_Stripe_Color_3), CommonUtils.SystemDrawColorfromHex(at.Socks_Color), CommonUtils.SystemDrawColorfromHex(at.Cleats_Color));
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
            backgroundField.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/Stadiums/Grass_BrightGreen.png"));
         
            background.Fill = backgroundField;

            A_Standing_Player.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/Players/L_Stand_Player.png"));
            H_Standing_Player.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/Players/R_Stand_Player.png"));




            //This causes the field to move
            //                                   GameTimer.Tick += ShowFrame;
            //                                   GameTimer.Interval = TimeSpan.FromMilliseconds(20);
            //                                   GameTimer.Start();

            bool bGameEneded = false;
            Play_Struct Play = null;

            while (!bGameEneded)
            {

                Play = ge.ExecutePlay();

                bool bKickoff = false;
                if (Play.Offensive_Package.Play == Enum.Play_Enum.KICKOFF_NORMAL ||
                    Play.Offensive_Package.Play == Enum.Play_Enum.KICKOFF_ONSIDES)
                    bKickoff = true;

                Game_Ball.setValues(Play.Initial_Ball_State, Play.Line_of_Scimmage, Play.Vertical_Ball_Placement);

                //set the left edge of the view
                double[] a_edge = setViewEdge(Game_Ball.YardLine, Play.bLefttoRight, Game_Ball.Vertical_Percent_Pos);

                //Set all graphics objects including setting the view edges
                ShowGraphicObjects(a_edge, Game_Ball, Play.Offensive_Package.Formation.Player_list, Play.Defensive_Formation.Player_list, bKickoff, Play.bLefttoRight);

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
            //Set this in case a team scores on the last play of the game
            lblAwayScore.Content = Play.After_Away_Score;
            lblHomeScore.Content = Play.After_Home_Score;
            lblClock.Content = Play.After_Display_Time;

            //End of game not sure where this should go
            //gs.SaveGame(g, g.injuries, pw.Loaded_League);
            //Game done see if the state of the league has changed
            //Set_TopMenu?.Invoke(this, new EventArgs());
            //this.Close();
        }

        private double[] setViewEdge(double YardLIne, bool bLefttoRight, double vert_percent)
        {
            double view_edge_left;
            double view_edge_top;

            //Set the left edge
            int H_Pixel = Yardline_to_Pixel(YardLIne, true);
            view_edge_left = H_Pixel * -1;

            logger.Debug("SetView: " + YardLIne);
            logger.Debug("H_Pixel: " + H_Pixel);
            logger.Debug("VIEW_EDGE_PIXELS: " + VIEW_EDGE_PIXELS);

            //Correct if necessary
            if (bLefttoRight)
            {
                view_edge_left += VIEW_EDGE_PIXELS;
                logger.Debug("before: " + view_edge_left);
            }
            else
            {
                view_edge_left +=  CANVAS_WIDTH - VIEW_EDGE_PIXELS;
                logger.Debug("before: " + view_edge_left);
            }

            //correct the view if the field will go off left the edge
            if (view_edge_left < CANVAS_WIDTH - back_width)
                view_edge_left =  - back_width;

            if (view_edge_left > 0)
                view_edge_left = 0;

            logger.Debug("after: " + view_edge_left);

            //set the top edge
            double vertTemp1 = VertPercent_to_Pixel(vert_percent, 0);
            double halfCanHeight = Can_Height / 2;
            view_edge_top = vertTemp1 - halfCanHeight - TOP_PIXEL_FUDGE;
            view_edge_top *= -1;

            logger.Debug("Can_Height: " + CANVAS_HEIGHT);
            logger.Debug("vertTemp1: " + vertTemp1);
            logger.Debug("view_edge_top: " + view_edge_top);

            //correct the view if the field will go off the top edge
            if (view_edge_top < CANVAS_HEIGHT - back_height)
                view_edge_top = -back_width;

            if (view_edge_top > 0)
                view_edge_top = 0;

            logger.Debug("after: " + view_edge_top);

            return new double[2] { view_edge_left, view_edge_top };
        }

        private void setBAll(double yardLine, double Vertical_Ball_Placement, Ball_States bstate, double[] a_edge, bool bKickoff, bool bLefttoRight)
        {
            switch (bstate)
            {
                case Ball_States.TEED_UP:
                    Game_Ball.width = BALL_SIZE;
                    Game_Ball.Height = BALL_SIZE;
                    Ball.Width = Game_Ball.width;
                    Ball.Height = Game_Ball.Height;
                    Ball.Fill = System.Windows.Media.Brushes.Brown;
                    Ball.Stroke = System.Windows.Media.Brushes.Black;
                    break;
            }

            int H_Pixel = Yardline_to_Pixel(yardLine, true);
            double v_Pixel = VertPercent_to_Pixel(Vertical_Ball_Placement, BALL_SIZE);

            if (bKickoff)
                H_Pixel -= (int)BALL_SIZE / 2;
            else
                if (bLefttoRight)
                    H_Pixel -= BALL_SIZE;

            //Adjust the position on the canvas for the view edge
            H_Pixel += (int)a_edge[0];
            v_Pixel += (int)a_edge[1];

            Canvas.SetTop(Ball, v_Pixel);
            Canvas.SetLeft(Ball, H_Pixel);
        }

        private void setPlayer(double yardLine, double Vertical_Placement, Player_States pstate, double[] a_edge, Rectangle Player_Rect, bool bLefttoRight, bool awayTeam)
        {
            /*           switch (Pstate)
                       {
                           case Ball_States.TEED_UP:
                               Game_Ball.width = 8.0;
                               Game_Ball.Height = 8.0;
                               Ball.Width = Game_Ball.width;
                               Ball.Height = Game_Ball.Height;
                               Ball.Fill = System.Windows.Media.Brushes.Brown;
                               Ball.Stroke = System.Windows.Media.Brushes.Black;
                               break;
                       }
           */

            if (awayTeam)
                Player_Rect.Fill = A_Standing_Player;
            else
                Player_Rect.Fill = H_Standing_Player;

            int H_Pixel = Yardline_to_Pixel(yardLine, true);
            if (bLefttoRight)
                H_Pixel -= PLAYER_SIZE;
            
            double v_Pixel = VertPercent_to_Pixel(Vertical_Placement, PLAYER_SIZE);

            //Adjust the position on the canvas for the view edge
            H_Pixel += (int)a_edge[0];
            v_Pixel += (int)a_edge[1];


            logger.Debug("vertical pixel: " + Vertical_Placement + " " + v_Pixel);

            Canvas.SetTop(Player_Rect, v_Pixel);
            Canvas.SetLeft(Player_Rect, H_Pixel);
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

            int current_Top = (int)Canvas.GetTop(background);

            if (current_left < CANVAS_WIDTH - back_width + 6)
                Width_dir = 1;

            if (current_left >= 0)
                Width_dir = -1;
 
            if (current_Top < (Can_Height - back_height + 6))
                Height_dir = 1;

            if (current_Top >= 0)
                Height_dir = -1;
 

            Canvas.SetLeft(background, current_left + (3 * Width_dir));
            Canvas.SetTop(background, current_Top + (3 * Height_dir));


        }

        private int Yardline_to_Pixel(double y, bool bAddEndzone)
        {
            int r = 0;

            if (bAddEndzone)
                r = Field_Border + EndZonePixels;

             r+= (int)(y * Pixels_per_yard);

            return r;
        }

        private double VertPercent_to_Pixel(double v, int objectHeight)
        {
            double r = 0.0;
//            double ballHeight = bIncludeBall ? Game_Ball.Height : 0.0;
           

//            double verical_field_pixels = back_height - objectHeight - (Field_Border * 2);
            double verical_field_pixels = back_height - (Field_Border * 2);

            r = (verical_field_pixels * (v / 100.0)) + Field_Border;
            r -= (int)objectHeight / 2;
            return r;
        }

    private void ShowGraphicObjects(double[] a_edge, Game_Ball Game_Ball, List<Formation_Rec> Off_Players, List<Formation_Rec> Def_Players,bool bKickoff, bool bLefttoRight)
    {
        Canvas.SetLeft(background, a_edge[0]);
        Canvas.SetTop(background, a_edge[1]);

        //Place the ball on the field if not carried
        if (Game_Ball.bState != Ball_States.CARRIED)
            setBAll(Game_Ball.YardLine, Game_Ball.Vertical_Percent_Pos, Game_Ball.bState, a_edge, bKickoff, bLefttoRight);

       int xxx = 0;
        foreach (Formation_Rec f in Off_Players)
        {
              double away_yardline = Game_Ball.YardLine + f.YardLine;
              setPlayer(away_yardline, f.Vertical_Percent_Pos, f.State, a_edge, Away_Players_rect[xxx], bLefttoRight, true);

            xxx++;
        }

        xxx = 0;
        foreach (Formation_Rec f in Def_Players)
        {
              double home_yardline = Game_Ball.YardLine + f.YardLine;
              setPlayer(home_yardline, f.Vertical_Percent_Pos, f.State, a_edge, Home_Players_rect[xxx], bLefttoRight, false);

            xxx++;
        }

    }
    }
}
