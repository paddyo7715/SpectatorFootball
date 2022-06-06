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

        private long Max_TD_Points = 7;

        //Game State that are not in the Game model
        private long Fid_first_posession;
        private long fid_posession;

        private int Down;
        private int Yards_to_go;
        private Double Line_of_Scrimmage;
        public int Vertical_Ball_Placement;

        private int Away_timeouts = 3;
        private int Home_timeouts = 3;

        private bool bStartQTR = true;
        private bool bKickoff = false;
        private bool bGameOver = false;

        //Just for testing
        private int Execut_Play_Num = 0;

        private bool bSureGame;

        public GameEngine(MainWindow pw, Game g, Teams_by_Season at, List<Player_and_Ratings> Away_Players,
            Teams_by_Season ht, List<Player_and_Ratings> Home_Players)
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
            g.Quarter = 0;
            g.Time = 0;
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
                Max_TD_Points = 8;

            if (pw.Loaded_League.season.League_Structure_by_Season[0].Three_Point_Conversion == 1)
                Max_TD_Points = 9;

            //create the two coaching objects
            Away_Coach = new Coach(at.Franchise_ID, g, Max_TD_Points, Home_Players, Away_Players, lInj);
            Home_Coach = new Coach(ht.Franchise_ID, g, Max_TD_Points, Home_Players, Away_Players, lInj);

            //The first play needs to be a kickoff
            bKickoff = true;

            fid_posession = CoinToss();
            Fid_first_posession = fid_posession;
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

            Offensive_Package = Offensive_Coach.Call_Off_PlayFormation(bKickoff);
            DEF_Formation = Defensive_Coach.Call_Def_Formation(Offensive_Package);


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

            //if this is a new quarter
            if (bStartQTR)
            {
                g.Quarter += 1;
                Delay_Seconds = 0;
                g.Time = app_Constants.GAME_QUARTER_SECONDS;

                switch (g.Quarter)
                {
                    case 1:
                    case 3:
                        Down = 1;
                        Yards_to_go = 10;
                        bKickoff = true;
                        break;
                    case 2:
                    case 4:
                        break;
                    default:  //overtime
                        Down = 1;
                        Yards_to_go = 10;
                        bKickoff = true;
                        break;
                }

                if (bLefttoRight)
                    Line_of_Scrimmage = 35.0;
                else
                    Line_of_Scrimmage = 65.0;

                Vertical_Ball_Placement = 50;
            }
            else
            {
                //execute the play
                //accume stats
                Down_and_Yards = Game_Helper.getDownAndYardString(Down, Yards_to_go, Line_of_Scrimmage, bLefttoRight);
                GameQTREnd();

            }

            //After the play is complete and everything is updated.
            r.Away_Score = (long) g.Away_Score;
            r.Home_Score = (long)g.Home_Score;

             r.Down_and_Yards = Down_and_Yards;

            r.Away_Score = (long) g.Away_Score;
            r.Home_Score = (long) g.Home_Score;
            r.Away_Timeouts = Away_timeouts;
            r.Home_Timeouts = Home_timeouts;

            r.Offensive_Package = Offensive_Package;
            r.Defensive_Formation = DEF_Formation;
            r.Delay_seconds = Delay_Seconds;
            r.Display_QTR = Game_Helper.getQTRString(g.Quarter) + " QTR";
            r.Display_Time = Game_Helper.getTimestringFromSeconds(g.Time);
            r.Line_of_Scimmage = Line_of_Scrimmage;
            r.Vertical_Ball_Placement = Vertical_Ball_Placement;

            Execut_Play_Num += 1;

            if (Execut_Play_Num >= 1)
                bGameOver = true;

            r.bGameOver = bGameOver;
            return r;
        }
        private void GameQTREnd()
        {

            //If we are in overtime and one team has a higher score then the game is over
            if (g.Quarter > 4 && g.Away_Score != g.Home_Score)
                bGameOver = true;
            else if (g.Quarter == 4 && g.Time <= 0 && g.Away_Score != g.Home_Score)
                bGameOver = true;
            else if (g.Time <= 0)
                bStartQTR = true;

        }
    }
}
