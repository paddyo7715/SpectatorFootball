﻿using SpectatorFootball.Enum;
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
        public double g_Vertical_Ball_Placement;
        private int g_Away_timeouts = 3;
        private int g_Home_timeouts = 3;
        private bool g_bGameOver = false;

        //Manditory next plays
        private bool bKickoff;
        private bool bExtraPoint;
        private bool bFreeKick;

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
        private double FreeKickYardline = 20.0;
        private double nonKickoff_StartingYardline = 25.0;
        private long non_forfeit_win_score = 2;
        private long forfeit_lose_score = 0;


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

            bKickoff = true;
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

            if (g_fid_posession == at.Franchise_ID)
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

            //if the play should be a kickoff but kickoffs not used in this league then set the team
            //on the 25 with a first and ten and switch possession
            if (!bAllowKickoffs && (bKickoff || bFreeKick))
            {
                g_Down = 1;
                g_Yards_to_go = 10;
                g_Line_of_Scrimmage = nonKickoff_StartingYardline;
                g_Vertical_Ball_Placement = 50.0;
                g_fid_posession = Switch_Posession(g_fid_posession, at.Franchise_ID, ht.Franchise_ID);
            }

            //if this play is a kickoff then set where to kickoff from
            if (bKickoff)
            {
                g_Line_of_Scrimmage = getScrimmageLine(KickoffYardline, bLefttoRight);
                g_Vertical_Ball_Placement = 50.0;
            }
            else if (bFreeKick)
            {
                g_Line_of_Scrimmage = getScrimmageLine(KickoffYardline, bLefttoRight);
                g_Vertical_Ball_Placement = 50.0;
            }

            //adjust the formation positions depending on which team has the ball
            double PossessionAdjuster = bLefttoRight ? 1.0 : -1.0;

            //Call the play
            Offensive_Package = Offensive_Coach.Call_Off_PlayFormation(bKickoff, bExtraPoint, bFreeKick, PossessionAdjuster);
            DEF_Formation = Defensive_Coach.Call_Def_Formation(Offensive_Package, PossessionAdjuster);

            //You could get the allow substitutions from either coach
            bool bAllowSubs = Home_Coach.AllowSubstitutions();

            Offensive_Package.Formation.Player_list = Offensive_Coach.Populate_Formation(
                Offensive_Package.Formation.Player_list,
                bAllowSubs, Offensive_Package.Formation.bSpecialTeams);
            if (Offensive_Package.Formation.Player_list == null)
            {
                r.bForfeitedGame = true;
                r.bGameOver = true;

                if(bLefttoRight && g.Away_Score >= g.Home_Score)
                {
                    g.Away_Score = forfeit_lose_score;
                    g.Home_Score = non_forfeit_win_score;
                }

                r.Long_Message = getForfeit_Message(at, ht,(long) g.Away_Score, (long) g.Home_Score);
            }
            DEF_Formation.Player_list = Defensive_Coach.Populate_Formation(
                DEF_Formation.Player_list,
                 bAllowSubs, DEF_Formation.bSpecialTeams);
            if (DEF_Formation.Player_list == null)
            {
                r.bForfeitedGame = true;
                r.bGameOver = true;

                if (!bLefttoRight && g.Home_Score >= g.Away_Score)
                {
                    g.Home_Score = forfeit_lose_score;
                    g.Away_Score = non_forfeit_win_score;
                }

                r.Long_Message = getForfeit_Message(at, ht, (long)g.Away_Score, (long)g.Home_Score);
            }

            //Only execute the play and accume the play stats if the game has not been forfeited
            if (!r.bForfeitedGame)
            {
                Play_Result p_result = null;
                double yards_gained = 0.0;

                if (bKickoff && Offensive_Package.Play == Play_Enum.KICKOFF_NORMAL)
                    p_result = Kickoff_Normal_Play(bLefttoRight, false);

                //set results and accume team stats
                r.Long_Message = p_result.Message;
                yards_gained = p_result.yards_gained;
                bKickoff = p_result.bKickoff;
                bExtraPoint = p_result.bExtraPoint;
                bFreeKick = p_result.bFreeKick;

                g.Away_Score += p_result.away_points;
                g.Home_Score += p_result.home_points;

                switch (g.Quarter)
                {
                    case 1:
                        g.Away_Score_Q1 += p_result.away_points;
                        g.Home_Score_Q1 += p_result.home_points;
                        break;
                    case 2:
                        g.Away_Score_Q2 += p_result.away_points;
                        g.Home_Score_Q2 += p_result.home_points;
                        break;
                    case 3:
                        g.Away_Score_Q3 += p_result.away_points;
                        g.Home_Score_Q3 += p_result.home_points;
                        break;
                    case 4:
                        g.Away_Score_Q4 += p_result.away_points;
                        g.Home_Score_Q4 += p_result.home_points;
                        break;
                    default:
                        g.Away_Score_OT += p_result.away_points;
                        g.Home_Score_OT += p_result.home_points;
                        break;
                }

                g.Away_FirstDowns += p_result.away_first_downs;
                g.Away_ThirdDowns += p_result.away_third_down_att;
                g.Away_ThirdDown_Conversions += p_result.away_third_down_conv;
                g.Away_FourthDowns += p_result.away_fourth_down_att;
                g.Away_FourthDown_Conversions += p_result.away_fourth_down_conv;

                g.Away_1Point_Conv_Att += p_result.away_1point_att;
                g.Away_1Point_Conv_Made += p_result.away_1point_conv;
                g.Away_2Point_Conv_Att += p_result.away_2point_att;
                g.Away_2Point_Conv_Made += p_result.away_2point_conv;
                g.Away_3Point_Conv_Att += p_result.away_3point_att;
                g.Away_3Point_Conv_Made += p_result.away_3point_conv;

                g.Away_TOP += p_result.away_time_of_possession;
                g.Away_Turnovers += p_result.away_turnovers;
                g.Away_Sacks += p_result.away_sacks;

                g.Home_FirstDowns += p_result.home_first_downs;
                g.Home_ThirdDowns += p_result.home_third_down_att;
                g.Home_ThirdDown_Conversions += p_result.home_third_down_conv;
                g.Home_FourthDowns += p_result.home_fourth_down_att;
                g.Home_FourthDown_Conversions += p_result.home_fourth_down_conv;

                g.Home_1Point_Conv_Att += p_result.home_1point_att;
                g.Home_1Point_Conv_Made += p_result.home_1point_conv;
                g.Home_2Point_Conv_Att += p_result.home_2point_att;
                g.Home_2Point_Conv_Made += p_result.home_2point_conv;
                g.Home_3Point_Conv_Att += p_result.home_3point_att;
                g.Home_3Point_Conv_Made += p_result.home_3point_conv;

                g.Home_TOP += p_result.home_time_of_possession;
                g.Home_Turnovers += p_result.home_turnovers;
                g.Home_Sacks += p_result.home_sacks;

                //acume individual stats
                foreach (Game_Player_Defense_Stats s in p_result.Game_Player_Defense_Stats)
                {
                    Game_Player_Defense_Stats ps = g.Game_Player_Defense_Stats.Where(x => x.Player_ID == s.Player_ID).FirstOrDefault(); 
                    if (ps == null)
                        g.Game_Player_Defense_Stats.Add(s);
                    else
                    {
                        ps.Pass_Blocks += s.Pass_Blocks;
                        ps.Pass_Rushes += s.Pass_Rushes;
                        ps.QB_Pressures += s.QB_Pressures;
                        ps.Recovered_Fumbles += s.Recovered_Fumbles;
                    }
                }

                foreach (Game_Player_FG_Defense_Stats s in p_result.Game_Player_FG_Defense_Stats)
                {
                    Game_Player_FG_Defense_Stats ps = g.Game_Player_FG_Defense_Stats.Where(x => x.Player_ID == s.Player_ID).FirstOrDefault();
                    if (ps == null)
                        g.Game_Player_FG_Defense_Stats.Add(s);
                    else
                    {
                        ps.FG_Block += s.FG_Block;
                        ps.FG_Block_Recovery += s.FG_Block_Recovery;
                        ps.FG_Block_Recovery_Yards += s.FG_Block_Recovery_Yards;
                        ps.FG_Block_Recovery_TDs += s.FG_Block_Recovery_TDs;
                        ps.XP_Block += s.XP_Block;
                    }
                }

                foreach (Game_Player_Kick_Returner_Stats s in p_result.Game_Player_Kick_Returner_Stats)
                {
                    Game_Player_Kick_Returner_Stats ps = g.Game_Player_Kick_Returner_Stats.Where(x => x.Player_ID == s.Player_ID).FirstOrDefault();
                    if (ps == null)
                        g.Game_Player_Kick_Returner_Stats.Add(s);
                    else
                    {
                        ps.Kickoffs_Returned += s.Kickoffs_Returned;
                        ps.Kickoffs_Returned_Yards += s.Kickoffs_Returned_Yards;
                        ps.Kickoffs_Returned_TDs += s.Kickoffs_Returned_TDs;
                        ps.Kickoff_Return_Yards_Long += s.Kickoff_Return_Yards_Long;
                        ps.Kickoffs_Returned_Fumbles += s.Kickoffs_Returned_Fumbles;
                        ps.Kickoffs_Returned_Fumbles_Lost += s.Kickoffs_Returned_Fumbles_Lost;
                    }
                }

                foreach (Game_Player_Kicker_Stats s in p_result.Game_Player_Kicker_Stats)
                {
                    Game_Player_Kicker_Stats ps = g.Game_Player_Kicker_Stats.Where(x => x.Player_ID == s.Player_ID).FirstOrDefault();
                    if (ps == null)
                        g.Game_Player_Kicker_Stats.Add(s);
                    else
                    {
                        ps.XP_Att += s.XP_Att;
                        ps.XP_Made += s.XP_Made;
                        ps.FG_Att += s.FG_Att;
                        ps.FG_Made += s.FG_Made;
                        if (ps.FG_Long < s.FG_Long) ps.FG_Long = s.FG_Long;
                        ps.FG_Blocked += s.FG_Blocked;
                        ps.Kickoffs += s.Kickoffs;
                        ps.Kickoff_Out_of_Bounds += s.Kickoff_Out_of_Bounds;
                        ps.Kickoff_Touchback += s.Kickoff_Touchback;
                        ps.onside_attempt += s.onside_attempt;
                        ps.onside_successful += s.onside_successful;
                    }
                }

                foreach (Game_Player_Kickoff_Defenders s in p_result.Game_Player_Kickoff_Defenders)
                {
                    Game_Player_Kickoff_Defenders ps = g.Game_Player_Kickoff_Defenders.Where(x => x.Player_ID == s.Player_ID).FirstOrDefault();
                    if (ps == null)
                        g.Game_Player_Kickoff_Defenders.Add(s);
                    else
                    {
                        ps.Forced_Fumbles_Kickoffs += s.Forced_Fumbles_Kickoffs;
                        ps.Fumbles_Kickoffs_Recovered += s.Fumbles_Kickoffs_Recovered;
                        ps.Fumbles_Kickoffs_Recovered_TDs += s.Fumbles_Kickoffs_Recovered_TDs;
                        ps.Fumbles_Kickoffs_Recovered_Yards += s.Fumbles_Kickoffs_Recovered_Yards;
                        ps.Kickoff_Tackles += s.Kickoff_Tackles;
                        ps.KickOff_Onside_Kick_Recoveries += s.KickOff_Onside_Kick_Recoveries;
                    }
                }

                foreach (Game_Player_Kickoff_Receiver_Stats s in p_result.Game_Player_Kickoff_Receiver_Stats)
                {
                    Game_Player_Kickoff_Receiver_Stats ps = g.Game_Player_Kickoff_Receiver_Stats.Where(x => x.Player_ID == s.Player_ID).FirstOrDefault();
                    if (ps == null)
                        g.Game_Player_Kickoff_Receiver_Stats.Add(s);
                    else
                    {
                        ps.Onside_Kickoff_Recovery += s.Onside_Kickoff_Recovery;
                    }
                }

                foreach (Game_Player_Offensive_Linemen_Stats s in p_result.Game_Player_Offensive_Linemen_Stats)
                {
                    Game_Player_Offensive_Linemen_Stats ps = g.Game_Player_Offensive_Linemen_Stats.Where(x => x.Player_ID == s.Player_ID).FirstOrDefault();
                    if (ps == null)
                        g.Game_Player_Offensive_Linemen_Stats.Add(s);
                    else
                    {
                        ps.Oline_Plays += s.Oline_Plays;
                        ps.OLine_Sacks_Allowed += s.OLine_Sacks_Allowed;
                        ps.OLine_Rushing_Loss_Allowed += s.OLine_Rushing_Loss_Allowed;
                        ps.OLine_Missed_Block += s.OLine_Missed_Block;
                        ps.OLine_Pancakes += s.OLine_Pancakes;
                        ps.QB_Pressures_Allowed += s.QB_Pressures_Allowed;
                    }
                }

                foreach (Game_Player_Pass_Defense_Stats s in p_result.Game_Player_Pass_Defense_Stats)
                {
                    Game_Player_Pass_Defense_Stats ps = g.Game_Player_Pass_Defense_Stats.Where(x => x.Player_ID == s.Player_ID).FirstOrDefault();
                    if (ps == null)
                        g.Game_Player_Pass_Defense_Stats.Add(s);
                    else
                    {
                        ps.Ints += s.Ints;
                        ps.Def_Int_Yards += s.Def_Int_Yards;
                        ps.Def_int_TDs += s.Def_int_TDs;
                        ps.Def_Pass_Defenses += s.Def_Pass_Defenses;
                        ps.Tackles += s.Tackles;
                        ps.Def_Missed_Tackles += s.Def_Missed_Tackles;
                        ps.Touchdowns_Surrendered += s.Touchdowns_Surrendered;
                        ps.Forced_Fumbles += s.Forced_Fumbles;
                        ps.Fumble_Recoveries += s.Fumble_Recoveries;
                        ps.Fumble_Return_Yards += s.Fumble_Return_Yards;
                        ps.Fumble_Return_TD += s.Fumble_Return_TD;
                    }
                }

                //if the play resulted in a change of possession then switch possession
                if (p_result.bSwitchPossession)
                    g_fid_posession = Switch_Posession(g_fid_posession, at.Franchise_ID, ht.Franchise_ID);


            }

            Down_and_Yards = Game_Helper.getDownAndYardString(g_Down, g_Yards_to_go, g_Vertical_Ball_Placement, bLefttoRight);
            GameEnd();

            //After the play is complete 
            //I need to update the global variables here
            r.Away_Score = (long) g.Away_Score;
            r.Home_Score = (long) g.Home_Score;

             r.Down_and_Yards = Down_and_Yards;
            r.Away_Timeouts = g_Away_timeouts;
            r.Home_Timeouts = g_Home_timeouts;

            r.Offensive_Package = Offensive_Package;
            r.Defensive_Formation = DEF_Formation;
            r.Delay_seconds = Delay_Seconds;
            r.Display_QTR = Game_Helper.getQTRString((long)g.Quarter) + " QTR";
            r.Display_Time = Game_Helper.getTimestringFromSeconds((long)g.Time);
            r.Line_of_Scimmage = g_Line_of_Scrimmage;
            r.Vertical_Ball_Placement = g_Vertical_Ball_Placement;
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
        private long Switch_Posession(long Current_possession, long at, long ht)
        {
            long r = Current_possession;

            if (Current_possession == at)
                r = ht;
            else
                r = at;

            return r;
        }
        private string getForfeit_Message(Teams_by_Season at, Teams_by_Season ht, long away_score, long home_score)
        {
            string r = "";

            string win_team = "";
            string lose_team = "";
            long win_score = 0;
            long lose_score = 0;

            if (away_score > home_score)
            {
                win_team = at.Nickname;
                lose_team = ht.Nickname;
                win_score = away_score;
                lose_score = home_score;
            }
            else
            {
                win_team = ht.Nickname;
                lose_team = at.Nickname;
                win_score = home_score;
                lose_score = away_score;
            }

            r = "The " + win_team + " have won the game through forfeit, becuase the " + lose_team + " could not field enough players." + Environment.NewLine + Environment.NewLine; ;
            r += "The final score is " + win_score + " to " + lose_score;

            return r;
        }
        private Play_Result Kickoff_Normal_Play(bool bLefttoRight, bool FreeKic)
        {
            Play_Result r = new Play_Result(bLefttoRight);


            return r; 
        }
    }
}
