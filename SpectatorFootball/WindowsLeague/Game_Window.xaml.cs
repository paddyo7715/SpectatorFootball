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
using SpectatorFootball.Common;
using System.IO;
using System.Media;
using System.Runtime.InteropServices.WindowsRuntime;
using OxyPlot.Wpf;
using System.Windows.Media.Animation;
using System.Collections.ObjectModel;
using SpectatorFootball.League;
using System.Diagnostics.Eventing.Reader;

namespace SpectatorFootball.WindowsLeague
{
    /// <summary>
    /// Interaction logic for Game_Window.xaml
    /// </summary>
    public partial class Game_Window : Window
    {
        private static ILog logger = LogManager.GetLogger("RollingFile");
        public ObservableCollection<Football_Color_rec> ball_color_list { get; set; }

        private bool bCloseWindow = false;

        public event EventHandler Set_TopMenu;

        private MainWindow pw;
        private Teams_by_Season at = null;
        private Teams_by_Season ht = null;

        private List<Player_and_Ratings> Away_Players = null;
        private List<Player_and_Ratings> Home_Players = null;

        private DispatcherTimer dispatcherTimer = new DispatcherTimer();
        private DispatcherTimer GameTimer = new DispatcherTimer();
        private DispatcherTimer GameSpeed_Time = new DispatcherTimer();

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
        private const int POPUP_WIDTH = 500;
        private const int POPUP_HEIGHT = 500;

        //I'm not sure why I have to do this, but it seems that
        //the 1210 might not be the full view it might be just 1204
        //        private const int RIGHT_PIXEL_FUDGE = 6;
        private const int RIGHT_PIXEL_FUDGE = 0;
        private const int TOP_PIXEL_FUDGE = 10;
        private double CANVAS_WIDTH;
        private double CANVAS_HEIGHT;

        private Uniform_Image Uniform_Img;

        private int VIEW_EDGE_PIXELS;

        private const int PLAYER_IN_SPRITE_ROW = 65;

        private BitmapImage[] A_Player_Sprites = null;
        private BitmapImage[] H_Player_Sprites = null;
        private BitmapImage[] Goalpost_Images = null;

        //game graphics objects
        private double[] a_edge;

        private int sleepfor = 100;

        private const int POPUP_INDEX = 999;
        private const int GOALPOST_INDEX = 200;
        private const int GOALPOST_INDEX_BALL_OVER = 20;
        private const int PLAYER_CATCHING_BALL_ZINDEX = 100;
        private const int BALL_ZINDEX = 90;
        private const int PERSON_ON_FIELD_ZINDEX = 50;
        private const int FIELD_ART_ZINDEX = 10;

        private System.Drawing.Bitmap goalpost_sheet = null;

        private MediaPlayer crowd_player = null;

        private double crowd_volumn = .4;

        private bool bExit_Pressed = false;

        private bool bChampionshipGame = false;

        private string Global_Game_Spped = null;

        //flash message
        private int iflashmessages_count = 0;
        private const int FLASH_MESSAGES = 10;

        private List<string> scodes = new List<string>()
            { "SS", "S", "N", "F", "FF"};
        private List<int> newSpped = new List<int>()
            {
                180 ,140, 100, 60, 20
            };

        public Game_Window(MainWindow pw, Game g, bool bCrowdNoise, bool bFootballSounds, string GameSpeed)
        {
            InitializeComponent();

            chbCrowdNoise.IsChecked = bCrowdNoise;
            chbFootballSounds.IsChecked = bFootballSounds;
            setBackground_onoff();

            GameSpeed = GameSpeed.Trim();
            setSpeedButtons(GameSpeed);

            lblGameSpeed.Content = getGameSpeedDisplay(GameSpeed);
            Global_Game_Spped = GameSpeed;

            sleepfor = getSpeedTime(Global_Game_Spped);

            ball_color_list = new ObservableCollection<Football_Color_rec>(League_Helper.getBallColors());

            this.DataContext = this;

            //Needed to prime the media player
            Play_Sound(Game_Sounds.SILENCE);

            this.pw = pw;
            this.g = g;

            Gamepnl.Visibility = Visibility.Collapsed;

            Game_Services gs = new Game_Services();

            try
            {
                //Get the screen resolution of the primary monitor
                Tuple<int, int> Screen_Res = sysInfor.getScreenResolution();
                Screen_Res_Width = Screen_Res.Item1;
                Screen_Res_Height = Screen_Res.Item2;

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
                    (Teams_by_Season)ht, (List<Player_and_Ratings>)Home_Players, 
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
                Tuple<string, bool, bool, string> t = ls.getGameOptions();
                string[] m2 = t.Item1.Split('|');
                ball_Color = m2[0];
                ball_shade_color = m2[1];

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

                //bpo test ball


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
                dispatcherTimer.Interval = new TimeSpan(0, 0, 2);
                dispatcherTimer.Start();

                bChampionshipGame = g.Championship_Game == 1 ? true : false;
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
            bExit_Pressed = true;
            this.Close();
        }

        private void CloseGameInfo(object sender, EventArgs e)
        {

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

            GameTimer.Tick += Start_Game;
            GameTimer.Interval = TimeSpan.FromMilliseconds(1000);
            GameTimer.Start();

            string s = CommonUtils.getAppPath() + "\\Sounds\\";
            var u = new Uri(s + "cheer.mp3");
            Background_Crowd.Source = u;
        }


        private void Start_Game(object sender, EventArgs e)
        {
            GameTimer.Stop();

            Game_intro_pnl.Visibility = Visibility.Collapsed;
            Gamepnl.Visibility = Visibility.Visible;

            ImageBrush backgroundField = new ImageBrush();
            backgroundField.ImageSource = new BitmapImage(new Uri(CommonUtils.getAppPath() + "/images/Stadiums/Grass_BrightGreen.png"));

            background.Fill = backgroundField;

            //Needed to prime the media player
            Play_Sound(Game_Sounds.SILENCE);

            Task task = Play_Game();

                //Set this in case a team scores on the last play of the game

                //End of game not sure where this should go
                //gs.SaveGame(g, g.injuries, pw.Loaded_League);
                //Game done see if the state of the league has changed
                //Set_TopMenu?.Invoke(this, new EventArgs());
                //this.Close();

        }

        private async Task Play_Game()
        {
            Play_Struct Play;
            bool bBall_Over_Goalposts = false;
            bool bGameEneded = false;
            Play = null;
            Graphics_Game_Ball gGame_Ball = null;
            List<Graphics_Game_Player> Offensive_Players;
            List<Graphics_Game_Player> Defensive_Players;

            Background_Crowd.Play();

            while (!bGameEneded)
            {
                if (bCloseWindow) break;
                if (btnPauseResume.Content.ToString().StartsWith("R"))
                {
                    await Task.Delay(50);
                    continue;
                }

                gGame_Ball = null;
                Offensive_Players = null;
                Defensive_Players = null;
                Play = ge.ExecutePlay();

                //play.game_ball is null error
                gGame_Ball = new Graphics_Game_Ball(Play.Game_Ball.Initial_State, Play.Game_Ball.Starting_YardLine, Play.Game_Ball.Starting_Vertical_Percent_Pos, Play.Game_Ball.Stages);

                Offensive_Players = CreateGamePlayersLIst(Play.Offensive_Players);
                Defensive_Players = CreateGamePlayersLIst(Play.Defensive_Players);

                //set the left edge of the view
                a_edge = setViewEdge(gGame_Ball.YardLine, Play.bLefttoRight, gGame_Ball.Vertical_Percent_Pos);
                ShowGraphicObjects(a_edge, gGame_Ball, Offensive_Players, Defensive_Players, false, Play.bLefttoRight);


                //Set all graphics objects including setting the view edges
                //                ShowGraphicObjects(a_edge, gGame_Ball, Offensive_Players, Defensive_Players, Play.bLefttoRight);

                //Set the scoreboard before the play
                setScoreboard(Play.Before_Away_Score, Play.Before_Home_Score, Play.Before_Display_Time, Play.Before_Display_QTR, Play.Before_Away_Timeouts, Play.Before_Home_Timeouts, Play.Before_Down_and_Yards);

                //go thru the play stages.  The ball and all players have the same number of stages.
                for (int stg = 0; stg < gGame_Ball.Stages.Count; stg++)
                {
                    if (bCloseWindow) break;
                    if (btnPauseResume.Content.ToString().StartsWith("R"))
                    {
                        await Task.Delay(50);
                        continue;
                    }

                    bool bStageFinished = false;
                    gGame_Ball.ChangeStage(stg);

                    if (gGame_Ball.Stages[stg].bBall_Over_Goalposts)
                        bBall_Over_Goalposts = true;
                    else
                        bBall_Over_Goalposts = false;

                    do
                    {
                        if (bCloseWindow) break;
                        if (btnPauseResume.Content.ToString().StartsWith("R"))
                        {
                            await Task.Delay(50);
                            continue;
                        }

                        //set the ball position and state
                        gGame_Ball.Update();
                        //                        if (gGame_Ball.bStageFinished)
                        //                            bStageFinished = true;

                            //Go thru all offensive and def players and place them
                        for (int pSlot = 0; pSlot < Offensive_Players.Count(); pSlot++)
                        {
                            if (bCloseWindow) break;
                            if (btnPauseResume.Content.ToString().StartsWith("R"))
                            {
                                await Task.Delay(50);
                                continue;
                            }

                            Offensive_Players[pSlot].ChangeStage(stg);
                            Defensive_Players[pSlot].ChangeStage(stg);

                            Offensive_Players[pSlot].Update();
                            Defensive_Players[pSlot].Update();

                            if (Offensive_Players[pSlot].bStageFinished || Defensive_Players[pSlot].bStageFinished)
                                bStageFinished = true;
                        }
                        await Task.Delay(sleepfor);
                        //Show graphic objects
                        a_edge = setViewEdge(gGame_Ball.YardLine, Play.bLefttoRight, gGame_Ball.Vertical_Percent_Pos);

                        ShowGraphicObjects(a_edge, gGame_Ball, Offensive_Players, Defensive_Players, bBall_Over_Goalposts, Play.bLefttoRight);
                        if (gGame_Ball.bStageFinished)
                            bStageFinished = true;

                        logger.Debug("Crowd Volume: " + crowd_volumn);
                    } while (!bStageFinished);
                    /*
                                            if (!gGame_Ball.arePointsDone())
                                                logger.Debug("Points Not Done Ball");

                                            for (int pSlot = 0; pSlot < Offensive_Players.Count(); pSlot++)
                                            {
                                                if (!Offensive_Players[pSlot].arePointsDone())
                                                    logger.Debug("Points Not Offensive Players");
                                                if (!Defensive_Players[pSlot].arePointsDone())
                                                    logger.Debug("Points Not Defensive Players");
                                            }
                    */
                }

                if (!bExit_Pressed)
                    Play_Sound(Game_Sounds.WHISTLE);
                else
                    break;

                await Task.Delay(sleepfor);

                Announcer.Visibility = Visibility.Collapsed; 
                ScoreBoard.Visibility = Visibility.Visible;

                //                Thread.Sleep(500);


                //Show the play result
                TextBlock txbPlayResult = new TextBlock();
                txbPlayResult.Background = Brushes.Black;
                txbPlayResult.Opacity = 0.6;
                txbPlayResult.Width = CANVAS_WIDTH;
                txbPlayResult.Height = CANVAS_HEIGHT;
                Canvas.SetZIndex(txbPlayResult, POPUP_INDEX);

                // Set position on the Canvas
                Canvas.SetLeft(txbPlayResult, 0);
                Canvas.SetTop(txbPlayResult, 0);

                // Add the Label to the Canvas
                MyCanvas.Children.Add(txbPlayResult);


                //                bGameEneded = Play.bGameOver;
                //just to test one play take this out.
                bGameEneded = true;
                //                backgroundMusicPlayer.Stop();

             }  //Game ended

            logger.Debug("Crowd Volume: " + crowd_volumn);
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
                 case Ball_States.PUNT_THRU_THE_AIR:
                    int rnum = CommonUtils.getRandomNum(1, 2);
                    if (rnum == 1)
                        Ball.Fill = myLinearGradientBrush1;
                    else
                        Ball.Fill = myLinearGradientBrush2;
                    break;
                case Ball_States.POPUP:
                    int rnum2 = CommonUtils.getRandomNum(1, 2);
                    if (rnum2 == 1)
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
                    if (gBall.graph_bState == Graphics_Ball_Stats.ROLLING_1)
                        Ball.Fill = myLinearGradientBrush1;
                    else
                        Ball.Fill = myLinearGradientBrush2;
                    break;

            }

            int H_Pixel = Yardline_to_Pixel(gBall.YardLine, true);
            double v_Pixel = VertPercent_to_Pixel(gBall.Vertical_Percent_Pos, gBall.Height);

            H_Pixel -= (int)gBall.Height / 2;

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

        private void setGoalposts(double[] a_edge, int ind, bool bBall_Over_Goalposts)
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

            if (bBall_Over_Goalposts)
                Canvas.SetZIndex(Goalpost_Rects[ind], GOALPOST_INDEX_BALL_OVER);
            else
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

        private void GameSpeed_BackgroundWhite(object sender, EventArgs e)
        {
            GameSpeed_Time.Stop();
            lblGameSpeed.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
        }
        private void btnSpeedSlower_click(object sender, EventArgs e)
        {
            lblGameSpeed.Background = new SolidColorBrush(Color.FromRgb(255, 255, 0));

            Global_Game_Spped = getNextSpeedCode(Global_Game_Spped, false);
            setSpeedButtons(Global_Game_Spped);
            lblGameSpeed.Content = getGameSpeedDisplay(Global_Game_Spped);

            GameSpeed_Time.Tick += GameSpeed_BackgroundWhite;
            GameSpeed_Time.Interval = new TimeSpan(0, 0, 0, 0, 100);
            GameSpeed_Time.Start();
        }
        private void btnSpeedFaster_click(object sender, EventArgs e)
        {
            lblGameSpeed.Background = new SolidColorBrush(Color.FromRgb(0, 255, 0));

            Global_Game_Spped = getNextSpeedCode(Global_Game_Spped, true);
            setSpeedButtons(Global_Game_Spped);
            lblGameSpeed.Content = getGameSpeedDisplay(Global_Game_Spped);

            GameSpeed_Time.Tick += GameSpeed_BackgroundWhite;
            GameSpeed_Time.Interval = new TimeSpan(0, 0, 0, 0, 100);
            GameSpeed_Time.Start();
        }
        private void btnPauseResume_click(object sender, EventArgs e)
        {
            if (btnPauseResume.Content.ToString().StartsWith("P"))
            {
                btnPauseResume.Content = "Resume";
                Background_Crowd.Volume = 0;
                btnPauseResume.Background = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                btnPauseResume.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            }
            else
            {
                btnPauseResume.Content = "Pause";
                btnPauseResume.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                btnPauseResume.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));

            }

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

        private void ShowGraphicObjects(double[] a_edge, Graphics_Game_Ball Game_Ball, List<Graphics_Game_Player> Off_Players, List<Graphics_Game_Player> Def_Players, bool bBall_Over_Goalposts, bool bLefttoRight)
        {
            Canvas.SetLeft(background, a_edge[0]);
            Canvas.SetTop(background, a_edge[1]);

            if (Game_Ball.Sound != Game_Sounds.NONE)
                Play_Sound((Game_Sounds)Game_Ball.Sound);

            //adjust crowd noise 
            adjust_crowd_noise(Game_Ball.crowd_adj, bChampionshipGame, !bLefttoRight);

            setAnnouncement(Game_Ball.Announcement);

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
                if (f.Sound != Game_Sounds.NONE)
                    Play_Sound((Game_Sounds)f.Sound);

                adjust_crowd_noise(f.crowd_adj, bChampionshipGame, !bLefttoRight);

                setPlayer(Game_Ball, f, a_edge, off_Player_Sprites, bLefttoRight, true, xxx, off_Players_rect);

                setAnnouncement(f.Announcement);

                xxx++;
            }

            xxx = 0;
            foreach (Graphics_Game_Player f in Def_Players)
            {
                if (f.Sound != Game_Sounds.NONE)
                    Play_Sound((Game_Sounds)f.Sound);

                adjust_crowd_noise(f.crowd_adj, bChampionshipGame, !bLefttoRight);

                setPlayer(Game_Ball, f, a_edge, def_Player_Sprites, bLefttoRight, false, xxx, def_Players_rect);

                setAnnouncement(f.Announcement);

                xxx++;
            }

            setBAll(Game_Ball, a_edge, bLefttoRight);

            for (int igp = 0; igp < 2; igp++)
                setGoalposts(a_edge, igp, bBall_Over_Goalposts);

            setMidFieldArt(a_edge);

            DoEvents();

        }
        private void Play_Sound(Game_Sounds gs)
        {
            MediaPlayer Sound_player = new MediaPlayer();

            string s = CommonUtils.getAppPath() + "\\Sounds\\";

            try
            {

                switch (gs)
                {
                    case Game_Sounds.SILENCE:
                        s += "silence.mp3";
                        break;
                    case Game_Sounds.BALL_HITS_GOALPOST:
                        s += "Doink.mp3";
                        break;
                    case Game_Sounds.HUT_HUT:
                        s += "huthut.mp3";
                        break;
                    case Game_Sounds.KICK:
                        s += "kickball.mp3";
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

                if (chbFootballSounds.IsChecked == true)
                {
                    Sound_player.Open(u);
                    Sound_player.Play();
                }

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

        private void Background_Crowd_MediaEnded(object sender, RoutedEventArgs e)
        {
            Background_Crowd.Position = TimeSpan.FromMilliseconds(1);
        }

        private void adjust_crowd_noise(double adj, bool bChampGame, bool bLefttoRight)
        {
             if (adj != 0) logger.Debug("adj: " + adj);

            adj *= Game_Engine_Helper.HorizontalAdj(bLefttoRight);

            //In the championship game, the crowd cheers for both teams.
            adj = bChampGame ? Math.Abs(adj) : adj;

            adj = adj < 0 ? adj / 4.0 : adj;

            crowd_volumn += adj;

            if (chbCrowdNoise.IsChecked == true && btnPauseResume.Content.ToString().StartsWith("P")) 
                Background_Crowd.Volume = crowd_volumn;
        }

        private void chbCrowdNoise_Click(object sender, RoutedEventArgs e)
        {
            setBackground_onoff();
        }

        private void setBackground_onoff()
        {
            if (chbCrowdNoise.IsChecked == null || chbCrowdNoise.IsChecked == false ||
                btnPauseResume.Content.ToString().StartsWith("R"))
            {
                Background_Crowd.Volume = 0;
            }
            else
            {
                Background_Crowd.Volume = crowd_volumn;
            }


        }

        private string getGameSpeedDisplay(string GameSpeed)
        {
            string r = null;

            if (GameSpeed == "FF")
                r = "Fastest";
            else if (GameSpeed == "F")
                r = "Fast";
            else if (GameSpeed == "N")
                r = "Normal";
            else if (GameSpeed == "S")
                r = "Slow";
            else if (GameSpeed == "SS")
                r = "Slowest";

            return r;
        }
        private void setSpeedButtons(string Spedcode)
        {
            bool bSlower = true;
            bool bFaster = true;

            if (Spedcode == "SS")
                bSlower = false;
            else if (Spedcode == "FF")
                bFaster = false;

            btnSpeedSlower.IsEnabled = bSlower;
            btnSpeedFaster.IsEnabled = bFaster;
        }



        private string getNextSpeedCode(string oldSppedCode, bool bUp)
        {
            string r = null;

            int current_index = scodes.IndexOf(oldSppedCode);

            if (current_index == -1)
                MessageBox.Show("Error", "Current Speed Code Not found");
            else
            {
                int new_index = current_index;

                if (bUp && current_index < scodes.Count - 1)
                    new_index++;
                else if (!bUp && current_index > 0)
                    new_index--;

                sleepfor = newSpped[new_index];

                r = scodes[new_index];
            }

            return r;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            bCloseWindow = true;
        }

        private void setAnnouncement(string newAnnouncement)
        {

            //bpo test
            int zzz = 0;
            if (newAnnouncement != null && newAnnouncement.Length > 0)
                zzz = 5;

            if (ScoreBoard.Visibility == Visibility.Visible && newAnnouncement != null &&
                newAnnouncement.Length > 0)
            {
                Announcer.Visibility = Visibility.Visible;
                ScoreBoard.Visibility = Visibility.Collapsed;
            }

            if (newAnnouncement != null && newAnnouncement.Length > 0)
                lblAnnouncer.Content = newAnnouncement;
        }

        private int getSpeedTime(string speedCode)
        {
            int r = 0;

            int current_index = scodes.IndexOf(speedCode);
            if (current_index == -1)
                r = 2;
            else
            {
                r = newSpped[current_index];
            }

            return r;
        }
    }
}
