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

        private int back_width = 2880;
        private int back_height = 1480;
        private int Field_Border = 240;
        private int EndZonePixels = 200;
        private int Pixels_per_yard = 20;

        private int Width_dir = -1;
        private int Height_dir = -1;

        private int Screen_Res_Width;
        private int Screen_Res_Height;

        //this is the model 
        private Game g = null;

        //This is the game engine where the game is played!!
        public GameEngine ge = null;

        public Ellipse Ball = new Ellipse();
        private string ball_Color;
        private string ball_shade_color;
        private LinearGradientBrush myLinearGradientBrush1 = null;
        private LinearGradientBrush myLinearGradientBrush2 = null;

        private List<Rectangle> Away_Players_rect = new List<Rectangle>();
        private List<Rectangle> Home_Players_rect = new List<Rectangle>();

        private List<Rectangle> Goalpost_Rects = new List<Rectangle>();
        private Rectangle Mid_Field_Art_Rect = null;

        private Graphics_Game_Ball gGame_Ball = null;
        private bool ThreeDee_ball;
        private List<Graphics_Game_Player> Offensive_Players;
        private List<Graphics_Game_Player> Defensive_Players;

        private MediaPlayer Sound_player = new MediaPlayer();

        private const int PLAYER_SIZE = 50;
        private const int PLAYER_BALL_SIZE_DIFF = 30;
        private const double MID_FIELD_ART_OPACITY = .94;

        private const int GOALPOST_WIDTH = 50;
        private const int GOALPOST_HEIGHT = 160;
        private const int GOALPOSTS_VERT = 50;
        private const double GOALPOST_AWAY_YL = -11.5;
        private const double GOALPOST_HOME_YL = 111.5;

        public const double VIEW_EDGE_OFFSET_YARDLINE = 12.0;

        public const double FIFTY_YARDLINE_ART_LY = 50.0;
        public const double FIFTY_YARDLINE_ART_ERT = 50.0;
        private const int FIFTY_YARDLINE_ART_WIDTH = 160;
        private const int FIFTY_YARDLINE_ART_HEIGHT = 160;

        //I'm not sure why I have to do this, but it seems that
        //the 1210 might not be the full view it might be just 1204
        //        private const int RIGHT_PIXEL_FUDGE = 6;
        private const int RIGHT_PIXEL_FUDGE = 0;
        private const int TOP_PIXEL_FUDGE = 10;
        private double CANVAS_WIDTH;
        private double CANVAS_HEIGHT;

        private Uniform_Image Uniform_Img;

        private int VIEW_EDGE_PIXELS;

        private const int PLAYER_IN_SPRITE_ROW = 29;

        private BitmapImage[] A_Player_Sprites = null;
        private BitmapImage[] H_Player_Sprites = null;
        private BitmapImage[] Goalpost_Images = null;

        //game graphics objects
        private double[] a_edge;
        private Play_Struct Play;

        private int sleepfor = 100;

        private const int GOALPOST_INDEX = 200;
        private const int PLAYER_CATCHING_BALL_ZINDEX = 100;
        private const int BALL_ZINDEX = 90;
        private const int PERSON_ON_FIELD_ZINDEX = 50;
        private const int FIELD_ART_ZINDEX = 10;

        private System.Drawing.Bitmap goalpost_sheet = null;

        public Game_Window(MainWindow pw, Game g)
        {
            InitializeComponent();

            logger.Debug("Game Window Constructor started");

            this.pw = pw;
            this.g = g;

            Gamepnl.Visibility = Visibility.Collapsed;

            Game_Services gs = new Game_Services();

            try
            {

                //Get the screen resolution of the primary monitor
                Screen_Res_Width = (int)System.Windows.SystemParameters.PrimaryScreenWidth;
                Screen_Res_Height = (int)System.Windows.SystemParameters.PrimaryScreenHeight;

                this.Width = Screen_Res_Width -10;
                this.Height = Screen_Res_Height - 100;
                this.Top = 0;
                this.Left = 0;

                MyCanvas.Height = this.Height - 70;
                MyCanvas.Width = this.Width - 10;


                Gamepnl.Width = this.Width - 10;
                Gamepnl.Height = this.Height - 10;


                Game_intro_pnl.Width = this.Width - 10;
                Game_intro_pnl.Height = this.Height - 10;


                Canvas.SetLeft(background, 0);
                Canvas.SetTop(background, 0);
                background.Height = 1480;
                background.Width = 2880;

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

                ge = new GameEngine(g, (Teams_by_Season)at, (List<Player_and_Ratings>)Away_Players,
                    (Teams_by_Season)ht, (List<Player_and_Ratings>)Home_Players, false,
                    pw.Loaded_League.PenaltiesData, pw.Loaded_League.season.League_Structure_by_Season[0].Two_Point_Conversion,
                    pw.Loaded_League.season.League_Structure_by_Season[0].Three_Point_Conversion,
                    pw.Loaded_League.season.League_Structure_by_Season[0].Kickoff_Type,
                    pw.Loaded_League.season.League_Structure_by_Season[0].Injuries,
                    pw.Loaded_League.season.League_Structure_by_Season[0].Penalties);

                VIEW_EDGE_PIXELS = Yardline_to_Pixel(VIEW_EDGE_OFFSET_YARDLINE, false);
                CANVAS_WIDTH = MyCanvas.Width - RIGHT_PIXEL_FUDGE;
                CANVAS_HEIGHT = MyCanvas.Height;

                //Add ball to canvas
                MyCanvas.Children.Add(Ball);

                //Create the player rectangles for both away and home
                for (int xxx = 0; xxx < app_Constants.PLAYERS_ON_FIELD_PER_TEAM; xxx++)
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

                //Create the goalpost rectangles for both away and home
                for (int xxx = 0; xxx < 2; xxx++)
                {
                    Rectangle ap = new Rectangle()
                    {
                        Height = GOALPOST_HEIGHT,
                        Width = GOALPOST_WIDTH,
                    };

                    Goalpost_Rects.Add(ap);
                    MyCanvas.Children.Add(ap);
                }

                Mid_Field_Art_Rect = new Rectangle()
                {
                    Height = FIFTY_YARDLINE_ART_HEIGHT,
                    Width = FIFTY_YARDLINE_ART_WIDTH
                };
                MyCanvas.Children.Add(Mid_Field_Art_Rect);

                //Load and colorize the sprite sheets
                Uniform_Img = new Uniform_Image(CommonUtils.getAppPath() + System.IO.Path.DirectorySeparatorChar + "Images" + System.IO.Path.DirectorySeparatorChar + "Players" + System.IO.Path.DirectorySeparatorChar + "Player_Sprite_Sheet.png");

                Uniform_Img.Flip_All_Colors(true , CommonUtils.SystemDrawColorfromHex(ht.Helmet_Color) , CommonUtils.SystemDrawColorfromHex(ht.Helmet_Facemask_Color) , CommonUtils.SystemDrawColorfromHex(ht.Helmet_Logo_Color) , CommonUtils.SystemDrawColorfromHex(ht.Home_jersey_Color), CommonUtils.SystemDrawColorfromHex(ht.Home_Jersey_Number_Color) , CommonUtils.SystemDrawColorfromHex(ht.Home_Jersey_Number_Outline_Color) , CommonUtils.SystemDrawColorfromHex(ht.Home_Sleeve_Color) , CommonUtils.SystemDrawColorfromHex(ht.Home_Jersey_Shoulder_Stripe) , CommonUtils.SystemDrawColorfromHex(ht.Home_Jersey_Sleeve_Stripe_Color_1) , CommonUtils.SystemDrawColorfromHex(ht.Home_Jersey_Sleeve_Stripe_Color_2) , CommonUtils.SystemDrawColorfromHex(ht.Home_Jersey_Sleeve_Stripe_Color_3) , CommonUtils.SystemDrawColorfromHex(ht.Home_Jersey_Sleeve_Stripe_Color_4) , CommonUtils.SystemDrawColorfromHex(ht.Home_Jersey_Sleeve_Stripe_Color_5) , CommonUtils.SystemDrawColorfromHex(ht.Home_Jersey_Sleeve_Stripe_Color_6) , CommonUtils.SystemDrawColorfromHex(ht.Home_Pants_Color) , CommonUtils.SystemDrawColorfromHex(ht.Home_Pants_Stripe_Color_1) , CommonUtils.SystemDrawColorfromHex(ht.Home_Pants_Stripe_Color_2) , CommonUtils.SystemDrawColorfromHex(ht.Home_Pants_Stripe_Color_3) , CommonUtils.SystemDrawColorfromHex(ht.Socks_Color) , CommonUtils.SystemDrawColorfromHex(ht.Cleats_Color));
                Uniform_Img.Flip_All_Colors(false, CommonUtils.SystemDrawColorfromHex(at.Helmet_Color), CommonUtils.SystemDrawColorfromHex(at.Helmet_Facemask_Color), CommonUtils.SystemDrawColorfromHex(at.Helmet_Logo_Color), CommonUtils.SystemDrawColorfromHex(at.Away_jersey_Color), CommonUtils.SystemDrawColorfromHex(at.Away_Jersey_Number_Color), CommonUtils.SystemDrawColorfromHex(at.Away_Jersey_Number_Outline_Color), CommonUtils.SystemDrawColorfromHex(at.Away_Sleeve_Color), CommonUtils.SystemDrawColorfromHex(at.Away_Jersey_Shoulder_Stripe), CommonUtils.SystemDrawColorfromHex(at.Away_Jersey_Sleeve_Stripe_Color_1), CommonUtils.SystemDrawColorfromHex(at.Away_Jersey_Sleeve_Stripe_Color_2), CommonUtils.SystemDrawColorfromHex(at.Away_Jersey_Sleeve_Stripe_Color_3), CommonUtils.SystemDrawColorfromHex(at.Away_Jersey_Sleeve_Stripe_Color_4), CommonUtils.SystemDrawColorfromHex(at.Away_Jersey_Sleeve_Stripe_Color_5), CommonUtils.SystemDrawColorfromHex(at.Away_Jersey_Sleeve_Stripe_Color_6), CommonUtils.SystemDrawColorfromHex(at.Away_Pants_Color), CommonUtils.SystemDrawColorfromHex(at.Away_Pants_Stripe_Color_1), CommonUtils.SystemDrawColorfromHex(at.Away_Pants_Stripe_Color_2), CommonUtils.SystemDrawColorfromHex(at.Away_Pants_Stripe_Color_3), CommonUtils.SystemDrawColorfromHex(at.Socks_Color), CommonUtils.SystemDrawColorfromHex(at.Cleats_Color));

                League_Services ls = new League_Services();
                string[] m = ls.getGameOptions();
                string[] m2 = m[0].Split('|');
                ball_Color = m2[0];
                ball_shade_color = m2[1];
                ThreeDee_ball = Convert.ToBoolean(m[1]);

                Uniform_Img.Flip_One_Color(true, app_Constants.STOCK_BALL_COLOR, CommonUtils.SystemDrawColorfromHex(ball_Color));
                Uniform_Img.Flip_One_Color(false, app_Constants.STOCK_BALL_COLOR, CommonUtils.SystemDrawColorfromHex(ball_Color));

                A_Player_Sprites = CommonUtils.SplitImageSheet(PLAYER_IN_SPRITE_ROW, 2, PLAYER_SIZE, PLAYER_SIZE, Uniform_Img.Away_Uniform_Image);
                H_Player_Sprites = CommonUtils.SplitImageSheet(PLAYER_IN_SPRITE_ROW, 2, PLAYER_SIZE, PLAYER_SIZE, Uniform_Img.Home_Uniform_image);

                string goalposts_file = CommonUtils.getAppPath() + System.IO.Path.DirectorySeparatorChar + "Images" + System.IO.Path.DirectorySeparatorChar + "Goalposts.png";
                goalpost_sheet = new System.Drawing.Bitmap(goalposts_file);
                Goalpost_Images = CommonUtils.SplitImageSheet(2, 2, GOALPOST_WIDTH, GOALPOST_HEIGHT, goalpost_sheet);

                for (int iiii = 0; iiii < 2; iiii++)
                {
                    ImageBrush Goalpost_Sheet = new ImageBrush();
                    Goalpost_Sheet.ImageSource = Goalpost_Images[iiii];
                    Goalpost_Rects[iiii].Fill = Goalpost_Sheet;
                }

                ImageBrush ib = new ImageBrush(pw.Loaded_League.getHelmetImg(ht.Helmet_Image_File));
                ib.Opacity = MID_FIELD_ART_OPACITY;

                Mid_Field_Art_Rect.Fill = ib;

                //setup the gradiants for the game ball
                //Gradient 1
                myLinearGradientBrush1 = new LinearGradientBrush();
                myLinearGradientBrush1.StartPoint = new Point(0.9,0);
                myLinearGradientBrush1.EndPoint = new Point(0.9, 1);
                myLinearGradientBrush1.GradientStops.Add(
                    new GradientStop(CommonUtils.getColorfromHex(ball_Color), 0.0));
                myLinearGradientBrush1.GradientStops.Add(
                    new GradientStop(CommonUtils.getColorfromHex(ball_shade_color), 0.9));
                //gradient 2
                myLinearGradientBrush2 = new LinearGradientBrush();
                myLinearGradientBrush2.StartPoint = new Point(0.9, 0);
                myLinearGradientBrush2.EndPoint = new Point(0.9, 1);
                myLinearGradientBrush2.GradientStops.Add(
                    new GradientStop(CommonUtils.getColorfromHex(ball_shade_color), 0.0));
                myLinearGradientBrush2.GradientStops.Add(
                    new GradientStop(CommonUtils.getColorfromHex(ball_Color), 0.1));

                dispatcherTimer.Tick += CloseGameInfo;
                dispatcherTimer.Interval = new TimeSpan(0, 0, 0);
                dispatcherTimer.Start();

                logger.Debug("Game Window Constructor ended");
            }
            catch (Exception e)
            {
                Mouse.OverrideCursor = null;
                string err = "Error Loading Data to Start Game !";
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
            logger.Debug("CloseGameInfo started");

            dispatcherTimer.Stop();

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

            GameTimer.Tick += Play_Game;
            GameTimer.Interval = TimeSpan.FromMilliseconds(5000);
            GameTimer.Start();

            logger.Debug("CloseGameInfo ended");

        }

        private void Play_Game(object sender, EventArgs e)
        {
            logger.Debug("Play_Game started");

            GameTimer.Stop();

            Game_intro_pnl.Visibility = Visibility.Collapsed;
            Gamepnl.Visibility = Visibility.Visible;

            ImageBrush backgroundField = new ImageBrush();
            backgroundField.ImageSource = new BitmapImage(new Uri(CommonUtils.getAppPath() + "/images/Stadiums/Grass_BrightGreen.png"));

            background.Fill = backgroundField;

            bool bGameEneded = false;
            Play = null;

            while (!bGameEneded)
            {
                gGame_Ball = null;
                Offensive_Players = null;
                Defensive_Players = null;
                logger.Debug("Play_Game before executePlay");
                Play = ge.ExecutePlay();
                logger.Debug("Play_Game End executePlay");

                //play.game_ball is null error
                gGame_Ball = new Graphics_Game_Ball(Play.Game_Ball.Initial_State, Play.Game_Ball.Starting_YardLine, Play.Game_Ball.Starting_Vertical_Percent_Pos, Play.Game_Ball.Stages, ThreeDee_ball);

                Offensive_Players = CreateGamePlayersLIst(Play.Offensive_Players);
                Defensive_Players = CreateGamePlayersLIst(Play.Defensive_Players);

                //set the left edge of the view
                a_edge = setViewEdge(gGame_Ball.YardLine, Play.bLefttoRight, gGame_Ball.Vertical_Percent_Pos);
                ShowGraphicObjects(a_edge, gGame_Ball, Offensive_Players, Defensive_Players, Play.bLefttoRight);


                //Set all graphics objects including setting the view edges
//                ShowGraphicObjects(a_edge, gGame_Ball, Offensive_Players, Defensive_Players, Play.bLefttoRight);

                //Set the scoreboard before the play
                setScoreboard(Play.Before_Away_Score, Play.Before_Home_Score, Play.Before_Display_Time, Play.Before_Display_QTR, Play.Before_Away_Timeouts, Play.Before_Home_Timeouts, Play.Before_Down_and_Yards);

                //go thru the play stages.  The ball and all players have the same number of stages.
                for (int stg = 0; stg < gGame_Ball.Stages.Count; stg++)
                {
                    bool bStageFinished = false;
                    gGame_Ball.ChangeStage(stg);
                    do
                    {
                        //set the ball position and state
                        gGame_Ball.Update();

//                        if (gGame_Ball.bStageFinished)
//                            bStageFinished = true;

                        //Go thru all offensive and def players and place them
                        for (int pSlot = 0; pSlot < Offensive_Players.Count(); pSlot++)
                        {
                            Offensive_Players[pSlot].ChangeStage(stg);
                            Defensive_Players[pSlot].ChangeStage(stg);

                            Offensive_Players[pSlot].Update();
                            Defensive_Players[pSlot].Update();

                            if (Offensive_Players[pSlot].bStageFinished || Defensive_Players[pSlot].bStageFinished)
                                bStageFinished = true;
                        }
                        Thread.Sleep(sleepfor);
                        //Show graphic objects
                        a_edge = setViewEdge(gGame_Ball.YardLine, Play.bLefttoRight, gGame_Ball.Vertical_Percent_Pos);

                        //bpo test
                        logger.Debug("Ball x: " + gGame_Ball.YardLine + "y: " + gGame_Ball.Vertical_Percent_Pos);
                        //                        logger.Debug("L to R: " + Play.bLefttoRight + " Yardline: " + gGame_Ball.YardLine + " Vertical: " + gGame_Ball.Vertical_Percent_Pos + " left: " + a_edge[0] + " top " + a_edge[1] + " visiblity: " + Gamepnl.Visibility.ToString());
                        //

                        ShowGraphicObjects(a_edge, gGame_Ball, Offensive_Players, Defensive_Players, Play.bLefttoRight);
                        if (gGame_Ball.bStageFinished)
                           bStageFinished = true;
                    } while (!bStageFinished);

                }  // for loop stage

                //                bGameEneded = Play.bGameOver;
                //just to test one play take this out.
                bGameEneded = true;

            }  //Game ended

            //Set this in case a team scores on the last play of the game

            //End of game not sure where this should go
            //gs.SaveGame(g, g.injuries, pw.Loaded_League);
            //Game done see if the state of the league has changed
            //Set_TopMenu?.Invoke(this, new EventArgs());
            //this.Close();

            logger.Debug("Play_Game ended");
        }

        private double[] setViewEdge(double YardLIne, bool bLefttoRight, double vert_percent)
        {
            double view_edge_left;
            double view_edge_top;

            //Set the left edge
            int H_Pixel = Yardline_to_Pixel(YardLIne, true);
            view_edge_left = H_Pixel * -1;

//            logger.Debug("SetView: " + YardLIne);
//            logger.Debug("H_Pixel: " + H_Pixel);
//            logger.Debug("VIEW_EDGE_PIXELS: " + VIEW_EDGE_PIXELS);

            //Correct if necessary
            if (bLefttoRight)
            {
                view_edge_left += VIEW_EDGE_PIXELS;
//                logger.Debug("before: " + view_edge_left);
            }
            else
            {
                view_edge_left +=  CANVAS_WIDTH - VIEW_EDGE_PIXELS;
//                logger.Debug("before: " + view_edge_left);
            }

            //correct the view if the field will go off left the edge
            if (view_edge_left < CANVAS_WIDTH - back_width)
                view_edge_left = CANVAS_WIDTH - back_width;
//            view_edge_left = -back_width;


            if (view_edge_left > 0)
                view_edge_left = 0;

//            logger.Debug("after: " + view_edge_left);

            //set the top edge
            double vertTemp1 = VertPercent_to_Pixel(vert_percent, 0);
            double halfCanHeight = CANVAS_HEIGHT / 2;

            view_edge_top = vertTemp1 - halfCanHeight - TOP_PIXEL_FUDGE;
            view_edge_top *= -1;

//            logger.Debug("Can_Height: " + CANVAS_HEIGHT);
//            logger.Debug("vertTemp1: " + vertTemp1);
//            logger.Debug("view_edge_top: " + view_edge_top);

            //correct the view if the field will go off the top edge
            if (view_edge_top < CANVAS_HEIGHT - back_height)
                view_edge_top = CANVAS_HEIGHT - back_height;

            if (view_edge_top > 0)
                view_edge_top = 0;

//            logger.Debug("after: " + view_edge_top);

            return new double[2] { view_edge_left, view_edge_top };
        }

        private void setBAll(Graphics_Game_Ball gBall, double[] a_edge, bool bLefttoRight)
        {

            Ball.Width = gBall.width;
            Ball.Height = gBall.Height;

            Ball.Stroke = System.Windows.Media.Brushes.Black;


            switch (gBall.bState)
            {
                case Ball_States.TEED_UP:
                    Ball.Fill = (Brush)CommonUtils.getBrushfromHex(ball_Color);
                    break;
                 case Ball_States.END_OVER_END:
                    int rnum = CommonUtils.getRandomNum(1, 2);
                    if (rnum == 1)
                        Ball.Fill = myLinearGradientBrush1;
                    else
                        Ball.Fill = myLinearGradientBrush2;
                    break;
                case Ball_States.ON_THE_GROUND:
                    Ball.Fill = (Brush)CommonUtils.getBrushfromHex(ball_Color);
                    break;
                case Ball_States.SPIRAL:
                    if (gBall.graph_bState == Graphics_Ball_Stats.SPIRAL_1)
                        Ball.Fill = myLinearGradientBrush1;
                    else
                        Ball.Fill = myLinearGradientBrush2;
                    break;
                case Ball_States.ROLLING:
                    logger.Debug("Ball Height: " + gBall.Height + " Width: " + gBall.width);
                    if (gBall.graph_bState == Graphics_Ball_Stats.ROLLING_1)
                        Ball.Fill = myLinearGradientBrush1;
                    else
                        Ball.Fill = myLinearGradientBrush2;
                    break;

            }


            int H_Pixel = Yardline_to_Pixel(gBall.YardLine, true);
            double v_Pixel = VertPercent_to_Pixel(gBall.Vertical_Percent_Pos, gBall.Height);

            H_Pixel -= (int)gBall.width / 2;

            //Adjust the position on the canvas for the view edge
            H_Pixel += (int)a_edge[0];
            v_Pixel += (int)a_edge[1];

            if (gBall.bState != Ball_States.CARRIED)
            { 
                Canvas.SetTop(Ball, v_Pixel);
                Canvas.SetLeft(Ball, H_Pixel);
                Canvas.SetZIndex(Ball, BALL_ZINDEX);
            }
        }

        private void setPlayer(Graphics_Game_Ball gBall, Graphics_Game_Player ggp, double[] a_edge, BitmapImage[] Player_Sprites, bool bLefttoRight, bool bOffense, int xxx, List<Rectangle> players_rect)
        {
            double yardline = ggp.YardLine;

            int Left_Right_Image_Offset = 0;

            if ((bLefttoRight && !bOffense) || !bLefttoRight && bOffense)
                Left_Right_Image_Offset = PLAYER_IN_SPRITE_ROW;

            int ind = (int)ggp.graph_pState;

            ImageBrush Player_Sheet = new ImageBrush();
             Player_Sheet.ImageSource = Player_Sprites[ind + Left_Right_Image_Offset];

            players_rect[xxx].Fill = Player_Sheet;

            //bpo test
            int H_Pixel = Yardline_to_Pixel(yardline, true);

            //bpo test to see if this is ok
            if (bLefttoRight)
                H_Pixel -= PLAYER_SIZE - PLAYER_BALL_SIZE_DIFF;
            else
                H_Pixel -= PLAYER_BALL_SIZE_DIFF;

//            H_Pixel -= (PLAYER_SIZE/2) - PLAYER_BALL_SIZE_DIFF;


            double v_Pixel = VertPercent_to_Pixel(ggp.Vertical_Percent_Pos, PLAYER_SIZE);

            //Adjust the position on the canvas for the view edge
            H_Pixel += (int)a_edge[0];
            v_Pixel += (int)a_edge[1];

            //Make it so the kickers feet line up with the ball so pull them up a little
//            v_Pixel -= PLAYER_SIZE/4;


//            logger.Debug("vertical pixel: " + ggp.Vertical_Percent_Pos + " " + v_Pixel);

            Canvas.SetTop(players_rect[xxx], v_Pixel);
            Canvas.SetLeft(players_rect[xxx], H_Pixel);

            if (ggp.bPlayerCatchesBall)
                Canvas.SetZIndex(players_rect[xxx], PLAYER_CATCHING_BALL_ZINDEX);
            else
                Canvas.SetZIndex(players_rect[xxx], PERSON_ON_FIELD_ZINDEX);

        }

        private void setGoalposts(double[] a_edge, int ind)
        {
            double yardline;

            if (ind == 0)
                yardline = GOALPOST_AWAY_YL;
            else
                yardline = GOALPOST_HOME_YL;

            //bpo test
            int H_Pixel = Yardline_to_Pixel(yardline, true);

            //bpo test to see if this is ok
            if (ind == 0)
                H_Pixel -= PLAYER_SIZE - PLAYER_BALL_SIZE_DIFF;
            else
                H_Pixel -= PLAYER_BALL_SIZE_DIFF;


            double v_Pixel = VertPercent_to_Pixel(GOALPOSTS_VERT, GOALPOST_HEIGHT);

            //Adjust the position on the canvas for the view edge
            H_Pixel += (int)a_edge[0];
            v_Pixel += (int)a_edge[1];

            Canvas.SetTop(Goalpost_Rects[ind], v_Pixel);
            Canvas.SetLeft(Goalpost_Rects[ind], H_Pixel);

            Canvas.SetZIndex(Goalpost_Rects[ind], GOALPOST_INDEX);


        }

        private void Show_Play(object sender, EventArgs e)
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

        private int Yardline_to_Pixel(double y, bool bAddEndzone)
        {
            int r = 0;

            if (bAddEndzone)
                r = Field_Border + EndZonePixels;

             r+= (int)(y * Pixels_per_yard);

            return r;
        }

        private double VertPercent_to_Pixel(double v, double objectHeight)
        {
            double r = 0.0;
//            double ballHeight = bIncludeBall ? Game_Ball.Height : 0.0;
           

//            double verical_field_pixels = back_height - objectHeight - (Field_Border * 2);
            double verical_field_pixels = back_height - (Field_Border * 2);

            r = (verical_field_pixels * (v / 100.0)) + Field_Border;
            r -= (int)objectHeight / 2;
            return r;
        }

        private void ShowGraphicObjects(double[] a_edge, Graphics_Game_Ball Game_Ball, List<Graphics_Game_Player> Off_Players, List<Graphics_Game_Player> Def_Players, bool bLefttoRight)
        {
            Canvas.SetLeft(background, a_edge[0]);
            Canvas.SetTop(background, a_edge[1]);

            if (Game_Ball.Sound != null)
            Play_Sound((Game_Sounds)Game_Ball.Sound);

            List<Rectangle> off_Players_rect = null;
            List<Rectangle> def_Players_rect = null;


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
            foreach (Graphics_Game_Player f in Off_Players)
            {
                    if (f.Sound != null)
                        Play_Sound((Game_Sounds)f.Sound);

                    setPlayer(Game_Ball, f, a_edge, off_Player_Sprites, bLefttoRight, true, xxx, off_Players_rect);

                    xxx++;
            }

            xxx = 0;
            foreach (Graphics_Game_Player f in Def_Players)
            {
                    if (f.Sound != null)
                        Play_Sound((Game_Sounds)f.Sound);

                    if (xxx == 5)
                    logger.Debug("Returner x: " + f.YardLine + "y: " +f.Vertical_Percent_Pos);


                setPlayer(Game_Ball, f, a_edge, def_Player_Sprites, bLefttoRight, false, xxx, def_Players_rect);

                    xxx++;
            }

            setBAll(Game_Ball, a_edge, bLefttoRight);

            for (int igp = 0; igp < 2; igp++)
                setGoalposts(a_edge, igp);

            setMidFieldArt(a_edge);

            DoEvents();

        }
    private void Play_Sound(Game_Sounds gs)
        {
            string s = CommonUtils.getAppPath() + "\\Sounds\\";
            try
            {

                switch (gs)
                {
                    case Game_Sounds.BALL_HITS_GOALPOST:
                        s += "Doink.mp3";
                        break;
                    case Game_Sounds.HUT_HUT:
                        s += "huthut.mp3";
                        break;
                    case Game_Sounds.KICK:
                        s += "kickball.mp3";
                        break;
                    case Game_Sounds.LOUD_BOO:
                        s += "Booing_Long.mp3";
                        break;
                    case Game_Sounds.LOUD_CHEER:
                        s += "Cheers_long.mp3";
                        break;
                    case Game_Sounds.LOW_BOO:
                        s += "Booing_Short.mp3";
                        break;
                    case Game_Sounds.LOW_CHEER:
                        s += "Cheer_Short.mp3";
                        break;
                    case Game_Sounds.PLAYERS_COLLIDING:
                        s += "Players_Colliding.mp3";
                        break;
                    case Game_Sounds.PLAYER_TACKLED:
                        s += "Tackle.mp3";
                        break;
                    case Game_Sounds.WHISTLE:
                        s += "Whistle.mp3";
                        break;
                }

                var u = new Uri(s);
                Sound_player.Open(u);
                Sound_player.Play();

            }
            catch { }

        }
        private void setScoreboard(string Away_Score, string Home_Score, string Time, string QTR, string Away_Timeouts, string Home_Timeouts, string Down_and_Yards)
        {
            lblAwayScore.Content = Away_Score;
            lblHomeScore.Content = Home_Score;

            lblClock.Content = Time;
            lblQTR.Content = QTR;

            lblAwayTimeouts.Content = Away_Timeouts;
            lblHomeTimeouts.Content = Home_Timeouts;

            lblDown.Content = Down_and_Yards;
        }
        private List<Graphics_Game_Player> CreateGamePlayersLIst(List<Game_Player> gpList)
        {
            List<Graphics_Game_Player> r = new List<Graphics_Game_Player>();
            foreach (Game_Player p in gpList)
            {
//                Graphics_Game_Player ggp = new Graphics_Game_Player(p.State, p.bCarryingBall, p.Current_YardLine, p.Current_Vertical_Percent_Pos, p.Stages);
                Graphics_Game_Player ggp = new Graphics_Game_Player(p.Initial_State, false, p.Starting_YardLine, p.Starting_Vertical_Percent_Pos, p.Stages);
                r.Add(ggp);
            }
            return r;
        }
        public static void DoEvents()
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Render,
                                                  new System.Action(delegate { }));
        }
        private void setMidFieldArt(double[] a_edge)
        {
            double yardline;

            yardline = FIFTY_YARDLINE_ART_LY;

            //bpo test
            int H_Pixel = Yardline_to_Pixel(yardline, true);

            H_Pixel -= FIFTY_YARDLINE_ART_WIDTH / 2;

            double v_Pixel = VertPercent_to_Pixel(FIFTY_YARDLINE_ART_ERT, FIFTY_YARDLINE_ART_HEIGHT);

            //Adjust the position on the canvas for the view edge
            H_Pixel += (int)a_edge[0];
            v_Pixel += (int)a_edge[1];

            Canvas.SetTop(Mid_Field_Art_Rect, v_Pixel);
            Canvas.SetLeft(Mid_Field_Art_Rect, H_Pixel);

            Canvas.SetZIndex(Mid_Field_Art_Rect, FIELD_ART_ZINDEX);


        }
    }
}
