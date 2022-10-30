using SpectatorFootball.Enum;
using SpectatorFootball.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.GameNS
{

    [Serializable]
    public class GameEngine
    {
        private MainWindow pw = null;
        private Teams_by_Season at = null;
        private List<Player_and_Ratings> Away_Players = null;
        private Teams_by_Season ht = null;
        private List<Player_and_Ratings> Home_Players = null;
        private Game g = null;
        private List<Injury> lInj = null;

        private Play_Struct play; 

        private Coach Away_Coach = null;
        private Coach Home_Coach = null;

        //Game State that are not in the Game model
        private long Fid_first_posession;

        //Global state variables
        private long g_fid_posession;
        private int g_Down;
        private int g_Yards_to_go;
        private Double g_Line_of_Scrimmage;
        public int g_Vertical_Ball_Placement;
        private int g_Away_timeouts = 3;
        private int g_Home_timeouts = 3;
        private bool g_bKickoff = true;
        private bool g_bGameOver = false;

        //Just for testing
        private int Execut_Play_Num = 0;

        private long Max_TD_Points = 7;

        //Game Settings
        private bool bSureGame = false;
        private bool bAllowKickoffs = false;
        private bool bTwoPointConv = false;
        private bool bThreePointConv = false;

        //other settings
        private double YardsInField = 100.0;
        private double KickoffYardline = 35.0;
        private double nonKickoff_StartingYardline = 25.0;


        public GameEngine(MainWindow pw, Game g, Teams_by_Season at, List<Player_and_Ratings> Away_Players,
            Teams_by_Season ht, List<Player_and_Ratings> Home_Players, bool bWatchGame)
        {
            this.pw = pw;
            this.at = at;
            this.Away_Players = Away_Players;
            this.ht = ht;
            this.Home_Players = Home_Players;
            this.g = g;

            //Initialize the game object
            g.Home_Score = 0;
            g.Away_Score = 0;
            g.Home_FirstDowns = 0;
            g.Home_ThirdDown_Conversions = 0;
            g.Home_ThirdDowns = 0;
            g.Home_FourthDown_Conversions = 0;
            g.Home_FourthDowns = 0;
            g.Home_1Point_Conv_Att = 0;
            g.Home_1Point_Conv_Made = 0;
            g.Home_2Point_Conv_Att = 0;
            g.Home_2Point_Conv_Made = 0;
            g.Home_3Point_Conv_Att = 0;
            g.Home_3Point_Conv_Made = 0;
            g.Home_TOP = 0;
            g.Away_FirstDowns = 0;
            g.Away_ThirdDown_Conversions = 0;
            g.Away_ThirdDowns = 0;
            g.Away_FourthDown_Conversions = 0;
            g.Away_FourthDowns = 0;
            g.Away_1Point_Conv_Att = 0;
            g.Away_1Point_Conv_Made = 0;
            g.Away_2Point_Conv_Att = 0;
            g.Away_2Point_Conv_Made = 0;
            g.Away_3Point_Conv_Att = 0;
            g.Away_3Point_Conv_Made = 0;
            g.Away_TOP = 0;
            g.Away_Score_Q1 = 0;
            g.Home_Score_Q1 = 0;
            g.Home_Score_Q2 = 0;
            g.Away_Score_Q2 = 0;
            g.Home_Score_Q3 = 0;
            g.Away_Score_Q3 = 0;
            g.Home_Score_Q4 = 0;
            g.Away_Score_Q4 = 0;
            g.Home_Score_OT = 0;
            g.Away_Score_OT = 0;
            g.Quarter = 1;
            g.Time = app_Constants.GAME_QUARTER_SECONDS;
            g.Playoff_Game = 0;
            g.Championship_Game = 0;
            g.Game_Done = 0;
            g.Home_Passing_Yards = 0;
            g.Away_Passing_Yards = 0;
            g.Home_Rushing_Yards = 0;
            g.Away_Rushing_Yards = 0;
            g.Home_Turnovers = 0;
            g.Away_Turnovers = 0;
            g.Home_Sacks = 0;
            g.Away_Sacks = 0;
            g.Forfeited_Game = 0;

            List<Game_Player_Defense_Stats> Game_Player_Defense_Stats = new List<Game_Player_Defense_Stats>();
            List<Game_Player_FG_Defense_Stats> Game_Player_FG_Defense_Stats = new List<Game_Player_FG_Defense_Stats>();
            List<Game_Player_Kick_Returner_Stats> Game_Player_Kick_Returner_Stats = new List<Game_Player_Kick_Returner_Stats>();
            List<Game_Player_Kicker_Stats> Game_Player_Kicker_Stats = new List<Game_Player_Kicker_Stats>();
            List<Game_Player_Kickoff_Defenders> Game_Player_Kickoff_Defenders = new List<Game_Player_Kickoff_Defenders>();
            List<Game_Player_Kickoff_Receiver_Stats> Game_Player_Kickoff_Receiver_Stats = new List<Game_Player_Kickoff_Receiver_Stats>();
            List<Game_Player_Offensive_Linemen_Stats> Game_Player_Offensive_Linemen_Stats = new List<Game_Player_Offensive_Linemen_Stats>();
            List<Game_Player_Pass_Defense_Stats> Game_Player_Pass_Defense_Stats = new List<Game_Player_Pass_Defense_Stats>();
            List<Game_Player_Passing_Stats> Game_Player_Passing_Stats = new List<Game_Player_Passing_Stats>();
            List<Game_Player_Penalty_Stats> Game_Player_Penalty_Stats = new List<Game_Player_Penalty_Stats>();
            List<Game_Player_Punt_Defenders> Game_Player_Punt_Defenders = new List<Game_Player_Punt_Defenders>();
            List<Game_Player_Punt_Receiver_Stats> Game_Player_Punt_Receiver_Stats = new List<Game_Player_Punt_Receiver_Stats>();
            List<Game_Player_Punt_Returner_Stats> Game_Player_Punt_Returner_Stats = new List<Game_Player_Punt_Returner_Stats>();
            List<Game_Player_Punter_Stats> Game_Player_Punter_Stats = new List<Game_Player_Punter_Stats>();
            List<Game_Player_Receiving_Stats> Game_Player_Receiving_Stats = new List<Game_Player_Receiving_Stats>();
            List<Game_Player_Rushing_Stats> Game_Player_Rushing_Stats = new List<Game_Player_Rushing_Stats>();
            List<Game_Scoring_Summary> Game_Scoring_Summary = new List<Game_Scoring_Summary>();

            //initialize the injuries list
            lInj = new List<Injury>();

            //set max possible point for 1 touchdown
            if (pw.Loaded_League.season.League_Structure_by_Season[0].Two_Point_Conversion == 1)
            {
                bTwoPointConv = true;
                Max_TD_Points = 8;
            }

            if (pw.Loaded_League.season.League_Structure_by_Season[0].Three_Point_Conversion == 1)
            {
                bThreePointConv = true;
                Max_TD_Points = 9;
            }

            //create the two coaching objects
            Away_Coach = new Coach(at.Franchise_ID, g, Max_TD_Points, Home_Players, Away_Players, lInj);
            Home_Coach = new Coach(ht.Franchise_ID, g, Max_TD_Points, Home_Players, Away_Players, lInj);

            g_fid_posession = CoinToss();
            Fid_first_posession = g_fid_posession;
        }

        private long CoinToss()
        {
            long r = 0;
            int i = CommonUtils.getRandomNum(1, 100);
            if (i <= 50)
                r = ht.Franchise_ID;
            else
                r = at.Franchise_ID;

            return r;
        }

        public Play_Struct ExecutePlay()
        {
            Play_Struct r = new Play_Struct();
            Play_Package Offensive_Package = null;
            Formation DEF_Formation = null;
            Coach Offensive_Coach = null;
            Coach Defensive_Coach = null;
            int Delay_Seconds = 0;
            string Down_and_Yards = "";

            //if the play should be a kickoff but kickoffs not used in this league then set the team
            //on the 25 with a first and ten and switch possession
            if (!bAllowKickoffs)
            {
                g_Down = 1;
                g_Yards_to_go = 10;
                g_Line_of_Scrimmage = nonKickoff_StartingYardline;
            }

            //Local Game vairalbes just for this play
            long Away_Score = (long) g.Away_Score;
            long Home_Score = (long)g.Home_Score;
            long Quarter = (long) g.Quarter;
            long Time = (long) g.Time;
            long fid_posession = this.g_fid_posession;
            int Down = this.g_Down;
            int Yards_to_go = this.g_Yards_to_go;
            Double Line_of_Scrimmage = this.g_Line_of_Scrimmage;
            double Vertical_Ball_Placement = this.g_Vertical_Ball_Placement;
            int Away_timeouts = this.g_Away_timeouts;
            int Home_timeouts = this.g_Home_timeouts;
            bool bKickoff = this.g_bKickoff;
            bool bGameOver = this.g_bGameOver;

            //call the offensive and defensive plays
            bool bLefttoRight;

            if (fid_posession == at.Franchise_ID)
            {
                Offensive_Coach = Away_Coach;
                Defensive_Coach = Home_Coach;
                bLefttoRight = true;
            }
            else
            {
                Offensive_Coach = Home_Coach;
                Defensive_Coach = Away_Coach;
                bLefttoRight = false;
            }

            //if this play is a kickoff then set where to kickoff from
            if (bKickoff)
            {

//bpo here
//                if (bLefttoRight)
//                    Line_of_Scrimmage = 35.0;
//                else
//                    Line_of_Scrimmage = 65.0;

                Line_of_Scrimmage = getScrimmageLine(KickoffYardline, bLefttoRight);


                Vertical_Ball_Placement = 50.0;
            }

            //adjust the formation positions depending on which team has the ball
            double PossessionAdjuster = bLefttoRight ? 1.0 : -1.0;

            Offensive_Package = Offensive_Coach.Call_Off_PlayFormation(bKickoff, PossessionAdjuster);
            DEF_Formation = Defensive_Coach.Call_Def_Formation(Offensive_Package, PossessionAdjuster);

            //You could get the allow substitutions from either coach
            bool bAllowSubs = Home_Coach.AllowSubstitutions();

            Offensive_Package.Formation.Player_list = Offensive_Coach.Populate_Formation(
                Offensive_Package.Formation.Player_list,
                bAllowSubs, Offensive_Package.Formation.bSpecialTeams);
            if (Offensive_Package.Formation.Player_list == null)
            {
                //the offense has to forfeit the game
            }
            DEF_Formation.Player_list = Defensive_Coach.Populate_Formation(
                DEF_Formation.Player_list,
                 bAllowSubs, DEF_Formation.bSpecialTeams);
            if (DEF_Formation.Player_list == null)
            {
                //the defense has to forfeit the game
            }


            //execute the play
            //accume stats
            Down_and_Yards = Game_Helper.getDownAndYardString(Down, Yards_to_go, Line_of_Scrimmage, bLefttoRight);
            GameEnd();


            //After the play is complete 
            //I need to update the global variables here
            r.Away_Score = Away_Score;
            r.Home_Score = Home_Score;

             r.Down_and_Yards = Down_and_Yards;
            r.Away_Timeouts = Away_timeouts;
            r.Home_Timeouts = Home_timeouts;

            r.Offensive_Package = Offensive_Package;
            r.Defensive_Formation = DEF_Formation;
            r.Delay_seconds = Delay_Seconds;
            r.Display_QTR = Game_Helper.getQTRString(Quarter) + " QTR";
            r.Display_Time = Game_Helper.getTimestringFromSeconds(Time);
            r.Line_of_Scimmage = Line_of_Scrimmage;
            r.Vertical_Ball_Placement = Vertical_Ball_Placement;
            r.bLefttoRight = bLefttoRight;

            Execut_Play_Num += 1;

            if (Execut_Play_Num >= 1)
                g_bGameOver = true;

            r.bGameOver = g_bGameOver;

            //just incase a team scores on the final play of the half or game
            r.After_Away_Score = (long) g.Away_Score;
            r.After_Home_Score = (long) g.Home_Score;
            r.After_Display_Time = Game_Helper.getTimestringFromSeconds(g.Time); 

 
            return r;
        }
        private void GameEnd()
        {

            //If we are in overtime and one team has a higher score then the game is over
            if (g.Quarter > 4 && g.Away_Score != g.Home_Score)
                g_bGameOver = true;
            else if (g.Quarter == 4 && g.Time <= 0 && g.Away_Score != g.Home_Score)
                g_bGameOver = true;
            else
                g_bGameOver = false;
        }
        private double getScrimmageLine(double y, bool bLefttoRight)
        {
            double yardLine = y;

            if (!bLefttoRight)
                yardLine = YardsInField - y;

            return yardLine;
        }
    }
}
