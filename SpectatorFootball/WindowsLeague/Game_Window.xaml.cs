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
        public Graphics_Game_Ball gGame_Ball = null;
        public List<Graphics_Game_Player> Offensive_Players;
        public List<Graphics_Game_Player> Defensive_Players;

        public Ellipse Ball = new Ellipse();
        private string ball_Color;

        private List<Rectangle> Away_Players_rect = new List<Rectangle>();
        private List<Rectangle> Home_Players_rect = new List<Rectangle>();


        private MediaPlayer Sound_player = new MediaPlayer();

        private const int PLAYER_SIZE = 50;

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

        private const int PLAYER_IN_SPRITE_ROW = 21;

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
                    (Teams_by_Season)ht, (List<Player_and_Ratings>)Home_Players, true);

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
            backgroundField.ImageSource = new BitmapImage(new Uri(CommonUtils.getAppPath() + "/images/Stadiums/Grass_BrightGreen.png"));

            background.Fill = backgroundField;

            //This causes the field to move
            //                                   GameTimer.Tick += ShowFrame;
            //                                   GameTimer.Interval = TimeSpan.FromMilliseconds(20);
            //                                   GameTimer.Start();

            bool bGameEneded = false;
            Play_Struct Play;

            //bpo test
//            Play_Sound(Game_Sounds.BALL_HITS_GOALPOST);
            //

            while (!bGameEneded)
            {
                Play = ge.ExecutePlay();

                //play.game_ball is null error
                gGame_Ball = new Graphics_Game_Ball(Play.Game_Ball.Initial_State, Play.Game_Ball.Starting_YardLine, Play.Game_Ball.Starting_Vertical_Percent_Pos, Play.Game_Ball.Stages);

                Offensive_Players = CreateGamePlayersLIst(Play.Offensive_Players);
                Defensive_Players = CreateGamePlayersLIst(Play.Defensive_Players);

                //set the left edge of the view
                double[] a_edge = setViewEdge(gGame_Ball.YardLine, Play.bLefttoRight, gGame_Ball.Vertical_Percent_Pos);

                //Set all graphics objects including setting the view edges
                ShowGraphicObjects(a_edge, gGame_Ball, Offensive_Players, Defensive_Players, Play.bLefttoRight);

                //Set the scoreboard before the play
                setScoreboard(Play.Before_Away_Score, Play.Before_Home_Score, Play.Before_Display_Time, Play.Before_Display_QTR, Play.Before_Away_Timeouts, Play.Before_Home_Timeouts, Play.Before_Down_and_Yards);

                bGameEneded = Play.bGameOver;
                //just to test one play take this out.

                //go thru the play stages.  The ball and all players have the same number of stages.
                for (int stg=0; stg < gGame_Ball.Stages.Count; stg++)
                {
                    //reset the ball and all players to the new stage and reset their movement index to 0
                    gGame_Ball.current_Stage = stg;
                    gGame_Ball.current_movement = 0;

                    foreach(Graphics_Game_Player p in Offensive_Players)
                    {
                        p.current_Stage = stg;
                        p.current_movement = 0;
                    }

                    foreach (Graphics_Game_Player p in Defensive_Players)
                    {
                        p.current_Stage = stg;
                        p.current_movement = 0;
                    }

                    bool bMovementsDone = false;
                    do
                    {


                        
                        if (gGame_Ball.bFinished || Offensive_Players.Where(x => x.bFinished).Count() > 0)
                            bMovementsDone = true;
                    } while (bMovementsDone);



                    //up the stage number of the ball and player objects offense and defense


                }  // for loop stage
            }  //Game ended

            //Set this in case a team scores on the last play of the game

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

        private void setBAll(Graphics_Game_Ball gBall, double[] a_edge, bool bLefttoRight)
        {

            Ball.Width = gGame_Ball.width;
            Ball.Height = gGame_Ball.Height;
            Ball.Fill = (Brush) CommonUtils.getBrushfromHex(ball_Color);
            Ball.Stroke = System.Windows.Media.Brushes.Black;

            int H_Pixel = Yardline_to_Pixel(gGame_Ball.YardLine, true);
            double v_Pixel = VertPercent_to_Pixel(gGame_Ball.Vertical_Percent_Pos, gGame_Ball.Height);

             H_Pixel -= (int)gGame_Ball.width / 2;

            //Adjust the position on the canvas for the view edge
            H_Pixel += (int)a_edge[0];
            v_Pixel += (int)a_edge[1];

            Canvas.SetTop(Ball, v_Pixel);
            Canvas.SetLeft(Ball, H_Pixel);
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

            int H_Pixel = Yardline_to_Pixel(yardline, true);
            if (bLefttoRight)
                H_Pixel -= PLAYER_SIZE;
            
            double v_Pixel = VertPercent_to_Pixel(ggp.Vertical_Percent_Pos, PLAYER_SIZE);

            //Adjust the position on the canvas for the view edge
            H_Pixel += (int)a_edge[0];
            v_Pixel += (int)a_edge[1];

            //Make it so the kickers feet line up with the ball so pull them up a little
            v_Pixel -= PLAYER_SIZE/3;


            logger.Debug("vertical pixel: " + ggp.Vertical_Percent_Pos + " " + v_Pixel);

            Canvas.SetTop(players_rect[xxx], v_Pixel);
            Canvas.SetLeft(players_rect[xxx], H_Pixel);
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

    private void ShowGraphicObjects(double[] a_edge, Graphics_Game_Ball Game_Ball, List<Graphics_Game_Player> Off_Players, List<Graphics_Game_Player> Def_Players, bool bLefttoRight)
    {
        Canvas.SetLeft(background, a_edge[0]);
        Canvas.SetTop(background, a_edge[1]);

        //Place the ball on the field if not carried
        if (Game_Ball.bState != Ball_States.CARRIED)
            setBAll(Game_Ball, a_edge, bLefttoRight);

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
                double yardline = f.YardLine;
                setPlayer(Game_Ball, f, a_edge, off_Player_Sprites, bLefttoRight, true, xxx, off_Players_rect);
              xxx++;
        }

        xxx = 0;
        foreach (Graphics_Game_Player f in Def_Players)
        {
              double yardline = Game_Ball.YardLine + f.YardLine;
              setPlayer(Game_Ball, f, a_edge, def_Player_Sprites, bLefttoRight, false, xxx, def_Players_rect);
              xxx++;
        }

    }
    private void Play_Sound(Game_Sounds gs)
        {
            string s = CommonUtils.getAppPath() + "/Sounds/";
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

//                var u = new Uri(s);
//                Sound_player.Open(u);
//                Sound_player.Play();

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
                Graphics_Game_Player ggp = new Graphics_Game_Player(p.State, p.bCarryingBall, p.Current_YardLine, p.Current_Vertical_Percent_Pos, p.Stages);
                r.Add(ggp);
            }
            return r;
        }
    }
}
