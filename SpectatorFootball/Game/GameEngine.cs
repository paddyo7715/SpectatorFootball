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
        private bool bSureGame;
        private Teams_by_Season at = null;
        private List<Player_and_Ratings> Away_Players = null;
        private Teams_by_Season ht = null;
        private List<Player_and_Ratings> Home_Players = null;
        private Game g = null;
        private List<Injury> lInj = null;

        private Play_Struct play; 

        private long Fid_first_posession;
        private long fid_posession;

        private Coach Away_Coach = null;
        private Coach Home_Coach = null;

        private bool bKickoff = false;



        public GameEngine(Game g, Teams_by_Season at, List<Player_and_Ratings> Away_Players,
            Teams_by_Season ht, List<Player_and_Ratings> Home_Players)
        {
            this.at = at;
            this.Away_Players = Away_Players;
            this.ht = ht;
            this.Home_Players = Home_Players;
            this.g = g;

            //Initialize the game object
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

            //create the two coaching objects
            Away_Coach = new Coach(at.Franchise_ID, g);
            Home_Coach = new Coach(ht.Franchise_ID, g);

            //The first play needs to be a kickoff
            bKickoff = true;
        }
        private bool ExecutePlay()
        {
            bool bEndofGame = false;
            Play_Package Offensive_Package = null;
            List<Formation_Rec> DEF_Formation = null;

            //call the offensive and defensive plays
            if (fid_posession == at.Franchise_ID)
            {
                Offensive_Package = Away_Coach.Call_Off_PlayFormation(bKickoff);
                DEF_Formation = Home_Coach.Call_Def_Formation(Offensive_Package);
            }
            else
            {
                Offensive_Package = Home_Coach.Call_Off_PlayFormation(bKickoff);
                DEF_Formation = Away_Coach.Call_Def_Formation(Offensive_Package);
            }

           


            //put players in both formations
            //execute the play
            //accume stats
            //return structure of play for game window 
            //sleep based on a global variable tied to the game speed




            return bEndofGame;
        }
    }
}
