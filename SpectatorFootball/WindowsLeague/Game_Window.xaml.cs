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
        private string ball_Color;

        //Home and Away Player lists
        private List<Player_Graphics1_Rec> Away_Players_rect = new List<Player_Graphics1_Rec>();
        private List<Player_Graphics1_Rec> Home_Players_rect = new List<Player_Graphics1_Rec>();



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

        private Uniform_Image Uniform_Img;

        private int VIEW_EDGE_PIXELS;

        private const int PLAYER_IN_SPRITE_ROW = 19;

//        private ImageBrush A_Player_Sheet = new ImageBrush();
//        private ImageBrush H_Player_Sheet = new ImageBrush();

        private BitmapImage[] A_Player_Sprites = null;
        private BitmapImage[] H_Player_Sprites = null;

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

                    Away_Players_rect.Add(new Player_Graphics1_Rec() { Player_Rect = ap } );
                    MyCanvas.Children.Add(ap);

                    Rectangle hp = new Rectangle
                    {
                        Height = PLAYER_SIZE,
                        Width = PLAYER_SIZE,
                    };

                    Home_Players_rect.Add(new Player_Graphics1_Rec() { Player_Rect = hp });
                    MyCanvas.Children.Add(hp);
                }

                //Load and colorize the sprite sheets
                Uniform_Img = new Uniform_Image(CommonUtils.getAppPath() + System.IO.Path.DirectorySeparatorChar + "Images" + System.IO.Path.DirectorySeparatorChar + "Players" + System.IO.Path.DirectorySeparatorChar + "Player_Sprite_Sheet.png");

                Uniform_Img.Flip_All_Colors(true , CommonUtils.SystemDrawColorfromHex(ht.Helmet_Color) , CommonUtils.SystemDrawColorfromHex(ht.Helmet_Facemask_Color) , CommonUtils.SystemDrawColorfromHex(ht.Helmet_Logo_Color) , CommonUtils.SystemDrawColorfromHex(ht.Home_jersey_Color), CommonUtils.SystemDrawColorfromHex(ht.Home_Jersey_Number_Color) , CommonUtils.SystemDrawColorfromHex(ht.Home_Jersey_Number_Outline_Color) , CommonUtils.SystemDrawColorfromHex(ht.Home_Sleeve_Color) , CommonUtils.SystemDrawColorfromHex(ht.Home_Jersey_Shoulder_Stripe) , CommonUtils.SystemDrawColorfromHex(ht.Home_Jersey_Sleeve_Stripe_Color_1) , CommonUtils.SystemDrawColorfromHex(ht.Home_Jersey_Sleeve_Stripe_Color_2) , CommonUtils.SystemDrawColorfromHex(ht.Home_Jersey_Sleeve_Stripe_Color_3) , CommonUtils.SystemDrawColorfromHex(ht.Home_Jersey_Sleeve_Stripe_Color_4) , CommonUtils.SystemDrawColorfromHex(ht.Home_Jersey_Sleeve_Stripe_Color_5) , CommonUtils.SystemDrawColorfromHex(ht.Home_Jersey_Sleeve_Stripe_Color_6) , CommonUtils.SystemDrawColorfromHex(ht.Home_Pants_Color) , CommonUtils.SystemDrawColorfromHex(ht.Home_Pants_Stripe_Color_1) , CommonUtils.SystemDrawColorfromHex(ht.Home_Pants_Stripe_Color_2) , CommonUtils.SystemDrawColorfromHex(ht.Home_Pants_Stripe_Color_3) , CommonUtils.SystemDrawColorfromHex(ht.Socks_Color) , CommonUtils.SystemDrawColorfromHex(ht.Cleats_Color));
                Uniform_Img.Flip_All_Colors(false, CommonUtils.SystemDrawColorfromHex(at.Helmet_Color), CommonUtils.SystemDrawColorfromHex(at.Helmet_Facemask_Color), CommonUtils.SystemDrawColorfromHex(at.Helmet_Logo_Color), CommonUtils.SystemDrawColorfromHex(at.Away_jersey_Color), CommonUtils.SystemDrawColorfromHex(at.Away_Jersey_Number_Color), CommonUtils.SystemDrawColorfromHex(at.Away_Jersey_Number_Outline_Color), CommonUtils.SystemDrawColorfromHex(at.Away_Sleeve_Color), CommonUtils.SystemDrawColorfromHex(at.Away_Jersey_Shoulder_Stripe), CommonUtils.SystemDrawColorfromHex(at.Away_Jersey_Sleeve_Stripe_Color_1), CommonUtils.SystemDrawColorfromHex(at.Away_Jersey_Sleeve_Stripe_Color_2), CommonUtils.SystemDrawColorfromHex(at.Away_Jersey_Sleeve_Stripe_Color_3), CommonUtils.SystemDrawColorfromHex(at.Away_Jersey_Sleeve_Stripe_Color_4), CommonUtils.SystemDrawColorfromHex(at.Away_Jersey_Sleeve_Stripe_Color_5), CommonUtils.SystemDrawColorfromHex(at.Away_Jersey_Sleeve_Stripe_Color_6), CommonUtils.SystemDrawColorfromHex(at.Away_Pants_Color), CommonUtils.SystemDrawColorfromHex(at.Away_Pants_Stripe_Color_1), CommonUtils.SystemDrawColorfromHex(at.Away_Pants_Stripe_Color_2), CommonUtils.SystemDrawColorfromHex(at.Away_Pants_Stripe_Color_3), CommonUtils.SystemDrawColorfromHex(at.Socks_Color), CommonUtils.SystemDrawColorfromHex(at.Cleats_Color));

                League_Services ls = new League_Services();
                string[] m = ls.getGameOptions();
                string[] m2 = m[0].Split('|');
                ball_Color = m2[0];

                Uniform_Img.Flip_One_Color(true, app_Constants.STOCK_BALL_COLOR, CommonUtils.SystemDrawColorfromHex(ball_Color));
                Uniform_Img.Flip_One_Color(false, app_Constants.STOCK_BALL_COLOR, CommonUtils.SystemDrawColorfromHex(ball_Color));

                A_Player_Sprites = Uniform_Img.SplitSpriteSheet(false, PLAYER_IN_SPRITE_ROW, PLAYER_SIZE);
                H_Player_Sprites = Uniform_Img.SplitSpriteSheet(true, PLAYER_IN_SPRITE_ROW, PLAYER_SIZE);


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

            //            A_Standing_Player.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/Players/L_Stand_Player.png"));
            //            H_Standing_Player.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/Players/R_Stand_Player.png"));

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

        private Player_Graphic_Sprite setNewSprite(Player_States pState, Player_Graphic_Sprite sState, bool bPossessBall)
        {
            Player_Graphic_Sprite r = Player_Graphic_Sprite.STANDING;

            switch (pState)
            {
                case Player_States.STANDING:
                    r = Player_Graphic_Sprite.STANDING;
                    break;
                case Player_States.RUNNING_FORWARD:
                    if (bPossessBall)
                    {
                        if (sState == Player_Graphic_Sprite.RUNNING_FORWARD_NO_BALL_1)
                            r = Player_Graphic_Sprite.RUNNING_FORWARD_WITH_BALL_2;
                        else
                            r = Player_Graphic_Sprite.RUNNING_FORWARD_WITH_BALL_1;
                    }
                    else
                    {
                        if (sState == Player_Graphic_Sprite.RUNNING_FORWARD_NO_BALL_1)
                            r = Player_Graphic_Sprite.RUNNING_FORWARD_NO_BALL_1;
                        else
                            r = Player_Graphic_Sprite.RUNNING_FORWARD_NO_BALL_2;
                    }
                    break;
                case Player_States.FG_KICK:
                    r = Player_Graphic_Sprite.FG_KICK;
                    break;
                case Player_States.ABOUT_TO_CATCH_KICK:
                    r = Player_Graphic_Sprite.ABOUT_TO_CATCH_KICK;
                    break;
                case Player_States.BLOCKING:
                    if (sState == Player_Graphic_Sprite.BLOCKING_1)
                        r = Player_Graphic_Sprite.BLOCKING_2;
                    else
                        r = Player_Graphic_Sprite.BLOCKING_1;
                    break;
                case Player_States.RUNNING_UP:
                    if (bPossessBall)
                    {
                        if (sState == Player_Graphic_Sprite.RUNNING_UP_WITH_BALL_1)
                            r = Player_Graphic_Sprite.RUNNING_UP_WITH_BALL_2;
                        else
                            r = Player_Graphic_Sprite.RUNNING_UP_WITH_BALL_1;
                    }
                    else
                    {
                        if (sState == Player_Graphic_Sprite.RUNNING_UP_NO_BALL_1)
                            r = Player_Graphic_Sprite.RUNNING_UP_NO_BALL_1;
                        else
                            r = Player_Graphic_Sprite.RUNNING_UP_NO_BALL_2;
                    }
                    break;
                case Player_States.RUNNING_DOWN:
                    if (bPossessBall)
                    {
                        if (sState == Player_Graphic_Sprite.RUNNING_DOWN_WITH_BALL_1)
                            r = Player_Graphic_Sprite.RUNNING_DOWN_WITH_BALL_2;
                        else
                            r = Player_Graphic_Sprite.RUNNING_DOWN_WITH_BALL_1;
                    }
                    else
                    {
                        if (sState == Player_Graphic_Sprite.RUNNING_DOWN_NO_BALL_1)
                            r = Player_Graphic_Sprite.RUNNING_DOWN_NO_BALL_1;
                        else
                            r = Player_Graphic_Sprite.RUNNING_DOWN_NO_BALL_2;
                    }
                    break;
                case Player_States.TACKLING:
                    r = Player_Graphic_Sprite.TACKLING;
                    break;
                case Player_States.TACKLED:
                    r = Player_Graphic_Sprite.TACKLED;
                    break;
                case Player_States.ON_BACK:
                    r = Player_Graphic_Sprite.ON_BACK;
                    break;
                default:
                    throw new Exception("Unknown Player_States " + pState.ToString());
            }
            return r;
        }

        private void setPlayer(double yardLine, double Vertical_Placement, double[] a_edge, Player_States State, Player_Graphics1_Rec Player_Graphic, BitmapImage[] Player_Sprites, bool bLefttoRight, bool bOffense)
        {
            int Left_Right_Image_Offset = 0;

            if ((bLefttoRight && !bOffense) || !bLefttoRight && bOffense)
                Left_Right_Image_Offset = PLAYER_IN_SPRITE_ROW;

            //set the new 
            Player_Graphic.Graphic_Sprinte = setNewSprite(State, Player_Graphic.Graphic_Sprinte, Player_Graphic.bHasBall);
            int ind = (int)Player_Graphic.Graphic_Sprinte;

            ImageBrush Player_Sheet = new ImageBrush();
             Player_Sheet.ImageSource = Player_Sprites[ind + Left_Right_Image_Offset];

            Player_Graphic.Player_Rect.Fill = Player_Sheet; 

            int H_Pixel = Yardline_to_Pixel(yardLine, true);
            if (bLefttoRight)
                H_Pixel -= PLAYER_SIZE;
            
            double v_Pixel = VertPercent_to_Pixel(Vertical_Placement, PLAYER_SIZE);

            //Adjust the position on the canvas for the view edge
            H_Pixel += (int)a_edge[0];
            v_Pixel += (int)a_edge[1];

            logger.Debug("vertical pixel: " + Vertical_Placement + " " + v_Pixel);

            Canvas.SetTop(Player_Graphic.Player_Rect, v_Pixel);
            Canvas.SetLeft(Player_Graphic.Player_Rect, H_Pixel);
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

        List<Player_Graphics1_Rec> off_Players_rect = null;
        List<Player_Graphics1_Rec> def_Players_rect = null;

        BitmapImage[] off_Player_Sprites = null;
        BitmapImage[] def_Player_Sprites = null;

        if (bLefttoRight)
        {
            off_Players_rect = Away_Players_rect;
            off_Player_Sprites = A_Player_Sprites;
            def_Players_rect = Home_Players_rect;
            def_Player_Sprites = H_Player_Sprites;
        }
        else
        {
            off_Players_rect = Home_Players_rect;
            off_Player_Sprites = H_Player_Sprites;
            def_Players_rect = Away_Players_rect;
            def_Player_Sprites = A_Player_Sprites;
        }

        int xxx = 0;
        foreach (Formation_Rec f in Off_Players)
        {
              double yardline = Game_Ball.YardLine + f.YardLine;
              setPlayer(yardline, f.Vertical_Percent_Pos, a_edge,f.State, off_Players_rect[xxx], off_Player_Sprites, bLefttoRight, true);
              xxx++;
        }

        xxx = 0;
        foreach (Formation_Rec f in Def_Players)
        {
              double yardline = Game_Ball.YardLine + f.YardLine;
              setPlayer(yardline, f.Vertical_Percent_Pos, a_edge, f.State, def_Players_rect[xxx], def_Player_Sprites, bLefttoRight, false);
              xxx++;
        }

    }
    }
}
