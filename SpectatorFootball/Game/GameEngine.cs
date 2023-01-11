﻿using SpectatorFootball.Enum;
using SpectatorFootball.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using log4net;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.GameNS
{

    [Serializable]
    public class GameEngine
    {
        private static ILog logger = LogManager.GetLogger("RollingFile");
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
        private bool bAllowInjuries = false;
        private bool bAllowPenalties = false;
        private bool bSimGame = false;

        //other settings
        private double YardsInField = 100.0;
        private double KickoffYardline = 35.0;
        private double FreeKickYardline = 20.0;
        private double nonKickoff_StartingYardline = 20.0;
        private long non_forfeit_win_score = 2;
        private long forfeit_lose_score = 0;

        public GameEngine(MainWindow pw, Game g, Teams_by_Season at, List<Player_and_Ratings> Away_Players,
            Teams_by_Season ht, List<Player_and_Ratings> Home_Players, bool bSimGame)
        {
            this.pw = pw;
            this.at = at;
            this.Away_Players = Away_Players;
            this.ht = ht;
            this.Home_Players = Home_Players;
            this.g = g;
            this.bSimGame = bSimGame;

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

            //Add starter records for both teams
            foreach (Player_Pos pp in System.Enum.GetValues(typeof(Player_Pos)))
            {
                int num_starters = Team_Helper.getNumStartingPlayersByPosition(pp);
                List<Player_and_Ratings> away_starter_list = Away_Players.Where(x => x.p.Pos == (int)pp).OrderBy(x => x.p.Pos).ThenByDescending(x => x.Overall_Grade).Take(num_starters).ToList();
                foreach (Player_and_Ratings a in away_starter_list)
                {
                    switch ((Player_Pos)a.p.Pos)
                    {
                        case Player_Pos.QB:
                            {
                                Game_Player_Passing_Stats.Add(new Game_Player_Passing_Stats() { Franchise_ID = at.Franchise_ID, Game_ID = g.ID, Player_ID = a.p.ID, Started = 1 });
                                break;
                            }

                        case Player_Pos.RB:
                            {
                                Game_Player_Rushing_Stats.Add(new Game_Player_Rushing_Stats() { Franchise_ID = at.Franchise_ID, Game_ID = g.ID, Player_ID = a.p.ID, Started = 1 });
                                break;
                            }

                        case Player_Pos.WR:
                        case Player_Pos.TE:
                            {
                                Game_Player_Receiving_Stats.Add(new Game_Player_Receiving_Stats() { Franchise_ID = at.Franchise_ID, Game_ID = g.ID, Player_ID = a.p.ID, Started = 1 });
                                break;
                            }
                        case Player_Pos.OL:
                            {
                                Game_Player_Offensive_Linemen_Stats.Add(new Game_Player_Offensive_Linemen_Stats() { Franchise_ID = at.Franchise_ID, Game_ID = g.ID, Player_ID = a.p.ID, Started = 1 });
                                break;
                            }

                        case Player_Pos.DL:
                        case Player_Pos.LB:
                            {
                                Game_Player_Defense_Stats.Add(new Game_Player_Defense_Stats() { Franchise_ID = at.Franchise_ID, Game_ID = g.ID, Player_ID = a.p.ID, Started = 1 });
                                break;
                            }
                        case Player_Pos.DB:
                            {
                                Game_Player_Pass_Defense_Stats.Add(new Game_Player_Pass_Defense_Stats() { Franchise_ID = at.Franchise_ID, Game_ID = g.ID, Player_ID = a.p.ID, Started = 1 });
                                break;
                            }

                        case Player_Pos.K:
                            {
                                Game_Player_Kicker_Stats.Add(new Game_Player_Kicker_Stats() { Franchise_ID = at.Franchise_ID, Game_ID = g.ID, Player_ID = a.p.ID, Started = 1 });
                                break;
                            }

                        case Player_Pos.P:
                            {
                                Game_Player_Punter_Stats.Add(new Game_Player_Punter_Stats() { Franchise_ID = at.Franchise_ID, Game_ID = g.ID, Player_ID = a.p.ID, Started = 1 });
                                break;
                            }
                    }
                }
                List<Player_and_Ratings> home_starter_list = Home_Players.Where(x => x.p.Pos == (int)pp).OrderBy(x => x.p.Pos).ThenByDescending(x => x.Overall_Grade).Take(num_starters).ToList();
                foreach (Player_and_Ratings a in away_starter_list)
                {
                    switch ((Player_Pos)a.p.Pos)
                    {
                        case Player_Pos.QB:
                            {
                                Game_Player_Passing_Stats.Add(new Game_Player_Passing_Stats() { Franchise_ID = ht.Franchise_ID, Game_ID = g.ID, Player_ID = a.p.ID, Started = 1 });
                                break;
                            }

                        case Player_Pos.RB:
                            {
                                Game_Player_Rushing_Stats.Add(new Game_Player_Rushing_Stats() { Franchise_ID = ht.Franchise_ID, Game_ID = g.ID, Player_ID = a.p.ID, Started = 1 });
                                break;
                            }

                        case Player_Pos.WR:
                        case Player_Pos.TE:
                            {
                                Game_Player_Receiving_Stats.Add(new Game_Player_Receiving_Stats() { Franchise_ID = ht.Franchise_ID, Game_ID = g.ID, Player_ID = a.p.ID, Started = 1 });
                                break;
                            }
                        case Player_Pos.OL:
                            {
                                Game_Player_Offensive_Linemen_Stats.Add(new Game_Player_Offensive_Linemen_Stats() { Franchise_ID = ht.Franchise_ID, Game_ID = g.ID, Player_ID = a.p.ID, Started = 1 });
                                break;
                            }

                        case Player_Pos.DL:
                        case Player_Pos.LB:
                            {
                                Game_Player_Defense_Stats.Add(new Game_Player_Defense_Stats() { Franchise_ID = ht.Franchise_ID, Game_ID = g.ID, Player_ID = a.p.ID, Started = 1 });
                                break;
                            }
                        case Player_Pos.DB:
                            {
                                Game_Player_Pass_Defense_Stats.Add(new Game_Player_Pass_Defense_Stats() { Franchise_ID = ht.Franchise_ID, Game_ID = g.ID, Player_ID = a.p.ID, Started = 1 });
                                break;
                            }

                        case Player_Pos.K:
                            {
                                Game_Player_Kicker_Stats.Add(new Game_Player_Kicker_Stats() { Franchise_ID = ht.Franchise_ID, Game_ID = g.ID, Player_ID = a.p.ID, Started = 1 });
                                break;
                            }

                        case Player_Pos.P:
                            {
                                Game_Player_Punter_Stats.Add(new Game_Player_Punter_Stats() { Franchise_ID = ht.Franchise_ID, Game_ID = g.ID, Player_ID = a.p.ID, Started = 1 });
                                break;
                            }
                    }
                }
            }

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

            if (pw.Loaded_League.season.League_Structure_by_Season[0].Kickoff_Type == 1)
                bAllowKickoffs = true;
            else
                bAllowKickoffs = false;

            if (pw.Loaded_League.season.League_Structure_by_Season[0].Injuries == 1)
                bAllowInjuries = true;
            else
                bAllowInjuries = false;

            if (pw.Loaded_League.season.League_Structure_by_Season[0].Penalties == 1)
                bAllowPenalties = true;
            else
                bAllowPenalties = false;

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

            List<Game_Player> Offensive_Players = null;
            List<Game_Player> Defensive_Players = null;
            Game_Ball Game_Ball = null;

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
            logger.Debug("ExecutePlay Offensive and Defensive plays called.");

            //You could get the allow substitutions from either coach
            bool bAllowSubs = Home_Coach.AllowSubstitutions();
            logger.Debug("ExecutePlay Allowsubstituions finished.");

            // Get the Game Values Before the Play is Executed
            r.Line_of_Scimmage = g_Line_of_Scrimmage;
            r.bLefttoRight = bLefttoRight;

            r.Before_Away_Score = g.Away_Score.ToString();
            r.Before_Home_Score = g.Home_Score.ToString();

            Down_and_Yards = Game_Helper.getDownAndYardString(g_Down, g_Yards_to_go, g_Vertical_Ball_Placement, bLefttoRight);
            r.Before_Down_and_Yards = Down_and_Yards;
            r.Before_Away_Timeouts = g_Away_timeouts.ToString();
            r.Before_Home_Timeouts = g_Home_timeouts.ToString();

            r.Before_Display_QTR = Game_Helper.getQTRString((long)g.Quarter) + " QTR";
            r.Before_Display_Time = Game_Helper.getTimestringFromSeconds((long)g.Time);
            //=================================================================
            logger.Debug("Before  Offensive_Coach.Populate_Formation");
            Offensive_Package.Formation.Player_list = Offensive_Coach.Populate_Formation(
                Offensive_Package.Formation.Player_list,
                bAllowSubs, Offensive_Package.Formation.bSpecialTeams);
            logger.Debug("After  Offensive_Coach.Populate_Formation");
            if (Offensive_Package.Formation.Player_list == null)
            {
                r.bForfeitedGame = true;
                r.bGameOver = true;

                if (bLefttoRight && g.Away_Score >= g.Home_Score)
                {
                    g.Away_Score = forfeit_lose_score;
                    g.Home_Score = non_forfeit_win_score;
                }

                r.Long_Message = getForfeit_Message(at, ht, (long)g.Away_Score, (long)g.Home_Score);
            }
            logger.Debug("Before  Defensive_Coach.Populate_Formation");
            DEF_Formation.Player_list = Defensive_Coach.Populate_Formation(
                DEF_Formation.Player_list,
                 bAllowSubs, DEF_Formation.bSpecialTeams);
            logger.Debug("After  Defensive_Coach.Populate_Formation");
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

            Offensive_Players = setGamePlayerLIsts(g_Line_of_Scrimmage, Offensive_Package.Formation);
            logger.Debug("Offensive Players set");
            Defensive_Players = setGamePlayerLIsts(g_Line_of_Scrimmage, DEF_Formation);
            logger.Debug("Defensive Players set");

            Game_Ball = new Game_Ball()
            {
                State = Offensive_Package.Formation.bState,
                Initial_State = Offensive_Package.Formation.bState,
                Current_Vertical_Percent_Pos = g_Vertical_Ball_Placement,
                Current_YardLine = g_Line_of_Scrimmage,
                Starting_Vertical_Percent_Pos = g_Vertical_Ball_Placement,
                Starting_YardLine = g_Line_of_Scrimmage
            };

            //Only execute the play and accume the play stats if the game has not been forfeited
            if (!r.bForfeitedGame)
            {
                Play_Result p_result = null;
                double yards_gained = 0.0;

                logger.Debug("Before  kickoff play");
                if (bKickoff && Offensive_Package.Play == Play_Enum.KICKOFF_NORMAL)
                    p_result = Kickoff_Normal_Play(Game_Ball, Offensive_Players, Defensive_Players, bLefttoRight, false, bSimGame);
                logger.Debug("AFter  kickoff play");

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
                        if (ps.Kickoff_Return_Yards_Long < s.Kickoff_Return_Yards_Long) ps.Kickoff_Return_Yards_Long = s.Kickoff_Return_Yards_Long;
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

                foreach (Game_Player_Passing_Stats s in p_result.Game_Player_Passing_Stats)
                {
                    Game_Player_Passing_Stats ps = g.Game_Player_Passing_Stats.Where(x => x.Player_ID == s.Player_ID).FirstOrDefault();
                    if (ps == null)
                        g.Game_Player_Passing_Stats.Add(s);
                    else
                    {
                        ps.Fumbles += s.Fumbles;
                        ps.Fumbles_Lost += s.Fumbles_Lost;
                        ps.Sacked += s.Sacked;
                        ps.Pass_Comp += s.Pass_Comp;
                        ps.Pass_Att += s.Pass_Att;
                        ps.Pass_Yards += s.Pass_Yards;
                        ps.Pass_TDs += s.Pass_TDs;
                        ps.Pass_Ints += s.Pass_Ints;
                        if (ps.Long < s.Long) ps.Long = s.Long;
                        ps.Passes_Blocks += s.Passes_Blocks;
                    }
                }

                foreach (Game_Player_Penalty_Stats s in p_result.Game_Player_Penalty_Stats)
                    g.Game_Player_Penalty_Stats.Add(s);

                foreach (Game_Player_Punt_Defenders s in p_result.Game_Player_Punt_Defenders)
                {
                    Game_Player_Punt_Defenders ps = g.Game_Player_Punt_Defenders.Where(x => x.Player_ID == s.Player_ID).FirstOrDefault();
                    if (ps == null)
                        g.Game_Player_Punt_Defenders.Add(s);
                    else
                    {
                        ps.Forced_Fumbles_Punt += s.Forced_Fumbles_Punt;
                        ps.Fumbles_Punt_Recovered += s.Fumbles_Punt_Recovered;
                        ps.Fumbles_Punt_Recovered_TDs += s.Fumbles_Punt_Recovered_TDs;
                        ps.Fumbles_Punt_Recovered_Yards += s.Fumbles_Punt_Recovered_Yards;
                        ps.Punt_Tackles += s.Punt_Tackles;
                    }
                }

                foreach (Game_Player_Punt_Receiver_Stats s in p_result.Game_Player_Punt_Receiver_Stats)
                {
                    Game_Player_Punt_Receiver_Stats ps = g.Game_Player_Punt_Receiver_Stats.Where(x => x.Player_ID == s.Player_ID).FirstOrDefault();
                    if (ps == null)
                        g.Game_Player_Punt_Receiver_Stats.Add(s);
                    else
                    {
                        ps.Punt_Block += s.Punt_Block;
                        ps.Punt_Block_Recovery += s.Punt_Block_Recovery;
                        ps.Punt_Block_Recovery_Yards += s.Punt_Block_Recovery_Yards;
                        ps.Punt_Block_Recovery_TDs += s.Punt_Block_Recovery_TDs;
                    }
                }


                foreach (Game_Player_Punt_Returner_Stats s in p_result.Game_Player_Punt_Returner_Stats)
                {
                    Game_Player_Punt_Returner_Stats ps = g.Game_Player_Punt_Returner_Stats.Where(x => x.Player_ID == s.Player_ID).FirstOrDefault();
                    if (ps == null)
                        g.Game_Player_Punt_Returner_Stats.Add(s);
                    else
                    {
                        ps.Punts_Returned += s.Punts_Returned;
                        ps.Punts_Returned_Yards += s.Punts_Returned_Yards;
                        ps.Punts_Returned_TDs += s.Punts_Returned_TDs;
                        if (ps.Punt_Returned_Yards_Long < s.Punt_Returned_Yards_Long) ps.Punt_Returned_Yards_Long = s.Punt_Returned_Yards_Long;
                        ps.Punts_Returned_Fumbles += s.Punts_Returned_Fumbles;
                        ps.Punts_Returned_Fumbles_Lost += s.Punts_Returned_Fumbles_Lost;
                    }
                }

                foreach (Game_Player_Punter_Stats s in p_result.Game_Player_Punter_Stats)
                {
                    Game_Player_Punter_Stats ps = g.Game_Player_Punter_Stats.Where(x => x.Player_ID == s.Player_ID).FirstOrDefault();
                    if (ps == null)
                        g.Game_Player_Punter_Stats.Add(s);
                    else
                    {
                        ps.Fumbles += s.Fumbles;
                        ps.Fumbles_Lost += s.Fumbles_Lost;
                        ps.num_punts += s.num_punts;
                        ps.Punt_yards += s.Punt_yards;
                        ps.Punt_Killed_att += s.Punt_Killed_att;
                        ps.Punt_killed_num += s.Punt_killed_num;
                        ps.Blocked_Punts += s.Blocked_Punts;
                    }
                }

                foreach (Game_Player_Receiving_Stats s in p_result.Game_Player_Receiving_Stats)
                {
                    Game_Player_Receiving_Stats ps = g.Game_Player_Receiving_Stats.Where(x => x.Player_ID == s.Player_ID).FirstOrDefault();
                    if (ps == null)
                        g.Game_Player_Receiving_Stats.Add(s);
                    else
                    {
                        ps.Fumbles += s.Fumbles;
                        ps.Fumbles_Lost += s.Fumbles_Lost;
                        ps.Rec_Catches += s.Rec_Catches;
                        ps.Rec_Drops += s.Rec_Drops;
                        ps.Rec_Yards += s.Rec_Yards;
                        ps.Rec_TDs += s.Rec_TDs;
                        if (ps.Long < s.Long) ps.Long = s.Long;
                    }
                }

                foreach (Game_Player_Rushing_Stats s in p_result.Game_Player_Rushing_Stats)
                {
                    Game_Player_Rushing_Stats ps = g.Game_Player_Rushing_Stats.Where(x => x.Player_ID == s.Player_ID).FirstOrDefault();
                    if (ps == null)
                        g.Game_Player_Rushing_Stats.Add(s);
                    else
                    {
                        ps.Fumbles += s.Fumbles;
                        ps.Fumbles_Lost += s.Fumbles_Lost;
                        ps.Rush_Att += s.Rush_Att;
                        ps.Rush_Yards += s.Rush_Yards;
                        ps.Rush_TDs += s.Rush_TDs;
                        if (ps.Long < s.Long) ps.Long = s.Long;
                    }
                }

                foreach (Game_Scoring_Summary s in p_result.Game_Scoring_Summary)
                    g.Game_Scoring_Summary.Add(s);

                logger.Debug("After accum stats");

                //work the injuries
                if (bAllowInjuries)
                {
                    Reduce_Play_Injuries();

                    string injur_message = null;
                    logger.Debug("Before checkforinjuries");
                    Injury new_injury = CheckforInjuries(Offensive_Package, DEF_Formation, injur_message);
                    logger.Debug("After checkforinjuries");

                    if (new_injury != null)
                    {
                        lInj.Add(new_injury);
                        r.Long_Message += injur_message;
                    }
                }

                //if the play resulted in a change of possession then switch possession
                if (p_result.bSwitchPossession)
                    g_fid_posession = Switch_Posession(g_fid_posession, at.Franchise_ID, ht.Franchise_ID);


            }

            GameEnd();

            r.Offensive_Players = Offensive_Players;
            r.Defensive_Players = Defensive_Players;
            r.Game_Ball = Game_Ball;

            // Get the Game Values Before the Play is Executed
            r.After_Away_Score = g.Away_Score.ToString();
            r.After_Home_Score = g.Home_Score.ToString();

            Down_and_Yards = Game_Helper.getDownAndYardString(g_Down, g_Yards_to_go, g_Vertical_Ball_Placement, bLefttoRight);
            r.After_Down_and_Yards = Down_and_Yards;
            r.After_Away_Timeouts = g_Away_timeouts.ToString();
            r.After_Home_Timeouts = g_Home_timeouts.ToString();

            r.After_QTR = Game_Helper.getQTRString((long)g.Quarter) + " QTR";
            r.After_Time = Game_Helper.getTimestringFromSeconds((long)g.Time);
            //=================================================================

            r.Delay_seconds = Delay_Seconds;

            Execut_Play_Num += 1;

            if (Execut_Play_Num >= 1)
                g_bGameOver = true;

            r.bGameOver = g_bGameOver;



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
        private Play_Result Kickoff_Normal_Play(Game_Ball gBall, List<Game_Player> Off_Players, List<Game_Player> Def_Players, bool bLefttoRight, bool FreeKic, bool bSim)
        {
            Play_Result r = new Play_Result(bLefttoRight);
            List<string> Play_Stages = new List<string>();
            string pre_snap = null;

            //Get the kicker
            Game_Player Kicker = Off_Players.Where(x => x.Pos == Player_Pos.K).First();

            //First Stage the kicker runs up and kicks the ball
            if (!bSim)
            {
                gBall.State = Ball_States.TEED_UP;
                Action bas = new Action(Game_Object_Types.B, gBall.Starting_YardLine, gBall.Starting_Vertical_Percent_Pos, 0.0, 0.0, false, false, null, gBall.State, null, Movement.NONE, null);
                Play_Stage bStage = new Play_Stage();
                bStage.Main_Object = false;
                bStage.Actions.Add(bas);
                gBall.Stages.Add(bStage);
            }

            int io_Players = 1;
            //cycle thru the offensive/kickoff team then he defense
            //if kicker then do their special thing; otherwise, the player just remains standing 
            foreach (Game_Player p in Off_Players)
            {
                if (p == Kicker)
                {
                    Action pas = null;
                    Action pas2 = null;
                    double prev_yl = p.Current_YardLine;
                    double prev_v = p.Current_Vertical_Percent_Pos;
                    p.Current_YardLine += 5.4 * HorizontalAdj(bLefttoRight);

                    if (!bSim)
                    {
                        Player_States moving_ps = setRunningState(bLefttoRight, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos);
                        pas = new Action(Game_Object_Types.P, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos, false, false, moving_ps, null, null, Movement.LINE, null);
                    }
                    prev_yl = p.Current_YardLine;
                    prev_v = p.Current_Vertical_Percent_Pos;
                    p.Current_YardLine += 0.4 * HorizontalAdj(bLefttoRight);
                    p.Current_Vertical_Percent_Pos += 0.0;
                    if (!bSim)  pas2 = new Action(Game_Object_Types.P, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos, false, true, Player_States.FG_KICK, null, Game_Sounds.KICK,Movement.LINE, null);

                    if (!bSim)
                    {
                        Play_Stage pStage = new Play_Stage();
                        pStage.Main_Object = true;
                        pStage.Actions.Add(pas);
                        pStage.Actions.Add(pas2);
                        p.Stages.Add(pStage);
                    }
//                    p.Starting_YardLine += p.Current_YardLine;
//                    p.Starting_Vertical_Percent_Pos += p.Current_Vertical_Percent_Pos;
                }
                else
                {

                    //Other players just stand there waiting for the kick
                    if (!bSim)
                    {
                        Action pas = new Action(Game_Object_Types.P, p.Starting_YardLine, p.Starting_Vertical_Percent_Pos, 0.0, 0.0, false, false, Player_States.STANDING, null, null, Movement.NONE, null);
                        Play_Stage pStage = new Play_Stage();
                        pStage.Main_Object = false;
                        pStage.Actions.Add(pas);
                        p.Stages.Add(pStage);
                    }
                }
                io_Players++;
            }

            int id_Players = 1;
            //The team receiving the kick will just stand there before the kick
            foreach (Game_Player p in Def_Players)
            {
                //Receiving players just stand there waiting for the kick
                if (!bSim)
                {
                    Action pas = new Action(Game_Object_Types.P, p.Starting_YardLine, p.Starting_Vertical_Percent_Pos, 0.0, 0.0, false, true, Player_States.STANDING, null, null, Movement.NONE, null);
                    Play_Stage pStage = new Play_Stage();
                    pStage.Main_Object = false;
                    pStage.Actions.Add(pas);
                    p.Stages.Add(pStage);
                }
            }

            id_Players++;

//bpo commenting out
///*
            //Stage two the ball flies thru the air
            //Now determine how far and straight the kick is
//bpo comment this code out for now to test
//*bpo test
            long leg_strength = Kicker.p_and_r.pr.First().Kicker_Leg_Power_Rating;
            KickOff_Length kick_length_enum = Kicking_Helper.getKickOff_Len_enum(leg_strength);
            double Kickoff_Len = Kicking_Helper.getKickoff_len(kick_length_enum);

            long leg_accuracy = Kicker.p_and_r.pr.First().Kicker_Leg_Accuracy_Rating;
            KickOff_Verticl Kick_Vert_enum = Kicking_Helper.getKickoff_Vert_enum(leg_accuracy);
            double Kickoff_Vert = Kicking_Helper.getKickoff_Vert(Kick_Vert_enum);

            //Adjust the length of the kick based on the vertical
            Kickoff_Len = Kicking_Helper.AdjustKickLength(Kickoff_Len, Kickoff_Vert);

            //bpo test
//            Kickoff_Len = 65.0;
//            Kickoff_Vert = 21.0;
            //

            //possision where ball should be caught
            gBall.Current_YardLine = gBall.Starting_YardLine + (Kickoff_Len * HorizontalAdj(bLefttoRight));
            gBall.Current_Vertical_Percent_Pos = Kickoff_Vert;
            if (!bSim)
            {
                gBall.State = Ball_States.END_OVER_END;
                Action bas = new Action(Game_Object_Types.B, gBall.Starting_YardLine, gBall.Starting_Vertical_Percent_Pos, gBall.Current_YardLine, gBall.Current_Vertical_Percent_Pos, false, true, null, gBall.State, null, Movement.LINE, Ball_Speed.SLOW);
                Play_Stage bStage = new Play_Stage();
                bStage.Main_Object = true;
                bStage.Actions.Add(bas);
                gBall.Stages.Add(bStage);
            }
            //            gBall.Starting_YardLine = gBall.Current_YardLine;
            //            gBall.Starting_Vertical_Percent_Pos = gBall.Current_Vertical_Percent_Pos;

            //for now just have all the other players just stand there.
            id_Players = 1;
            foreach (Game_Player p in Off_Players)
            {
                if (!bSim)
                {
                    Action pas = new Action(Game_Object_Types.P, p.Starting_YardLine, p.Starting_Vertical_Percent_Pos, 0.0, 0.0, false, true, Player_States.STANDING, null, null, Movement.NONE, null);
                    Play_Stage pStage = new Play_Stage();
                    pStage.Main_Object = false;
                    pStage.Actions.Add(pas);
                    p.Stages.Add(pStage);
                }
                id_Players++;
            }

            id_Players = 1;
            foreach (Game_Player p in Def_Players)
            {
                Action pas = null;
                Action pas2 = null;
                //The player who will return the ball
                switch (id_Players)
                {
                    case 11:
                        double prev_yl = p.Current_YardLine;
                        double prev_v = p.Current_Vertical_Percent_Pos;
                        p.Current_YardLine = gBall.Current_YardLine;
                        p.Current_Vertical_Percent_Pos = gBall.Current_Vertical_Percent_Pos;
                        Player_States moving_ps = setRunningState(bLefttoRight, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos);
                        if (!bSim) pas = new Action(Game_Object_Types.P, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos, false, false, moving_ps, null, null, Movement.LINE, null);

                        if (!bSim) pas2 = new Action(Game_Object_Types.P, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos, false, true, Player_States.ABOUT_TO_CATCH_KICK, null, null, null, null);

                        if (!bSim)
                        {
                            Play_Stage pStage = new Play_Stage();
                            pStage.Main_Object = false;
                            pStage.Actions.Add(pas);
                            pStage.Actions.Add(pas2);
                            p.Stages.Add(pStage);
                        }
                        break;

                    default:
                        //Receiving players just stand there waiting for the kick
                        if (!bSim)
                        {
                            pas = new Action(Game_Object_Types.P, p.Starting_YardLine, p.Starting_Vertical_Percent_Pos, 0.0, 0.0, false, true, Player_States.STANDING, null, null, Movement.NONE, null);
                            Play_Stage pStage = new Play_Stage();
                            pStage.Main_Object = false;
                            pStage.Actions.Add(pas);
                            p.Stages.Add(pStage);
                        }
                        break;
                }

                id_Players++;
            }
//*/ 
//commented out code


            if (!bSim)  r.Play_Stages = Play_Stages;
            return r;
        }
        private Injury CheckforInjuries(Play_Package Offensive_Package, Formation DEF_Formation, string inj_message)
        {
            Injury r = null;
            List<Formation_Rec> injury_team = null;
            string injury_team_nickname = null;
            string injury_player_name = null;
            long off_id = 0;
            long deff_id = 0;
            long f_id = 0;
            string off_nickname = null;
            string def_nickname = null;

            if (g_fid_posession == at.Franchise_ID)
            {
                off_id = at.Franchise_ID;
                deff_id = ht.Franchise_ID;
                off_nickname = at.Nickname;
                def_nickname = ht.Nickname;
            }
            else
            {
                off_id = ht.Franchise_ID;
                deff_id = at.Franchise_ID;
                off_nickname = ht.Nickname;
                def_nickname = at.Nickname;
            }

            int iInjury = CommonUtils.getRandomNum(1, 100);
            if (iInjury <= app_Constants.PERCENT_INJURY_ON_A_PLAY)
            {
                int iOffDef = CommonUtils.getRandomNum(1, 100);
                if (iOffDef <= app_Constants.PERCENT_OFF_DEF) //if true then defense injury injury   
                {
                    injury_team = DEF_Formation.Player_list;
                    injury_team_nickname = def_nickname;
                    f_id = deff_id;
                }
                else
                {
                    injury_team = Offensive_Package.Formation.Player_list;
                    injury_team_nickname = off_nickname;
                    f_id = off_id;
                }

                int kickerCount = 0;
                while (true)
                {
                    int iFormNum = CommonUtils.getRandomNum(1, injury_team.Count()) - 1;
                    Formation_Rec injured_player = injury_team[iFormNum];
                    //it should be rare that a kicker is injured
                    if (injured_player.Pos == Player_Pos.K || injured_player.Pos == Player_Pos.P)
                        kickerCount++;

                    if (kickerCount < 2)
                        continue;

                    if (injured_player == null)
                        logger.Debug("injured_player is null");
                    else if (injured_player.p_and_r == null)
                        logger.Debug("injured_player.p_and_r is null");

                    long toughness = injured_player.p_and_r.pr.First().Toughness_Ratings;
                    long injury_chance = app_Constants.INJURY_ADJUSTER - toughness;

                    injury_player_name = injured_player.p_and_r.p.First_Name + " " + injured_player.p_and_r.p.Last_Name;

                    r = new Injury() { Player_ID = injured_player.p_and_r.p.ID, Franchise_ID = f_id, Season_ID = g.Season_ID, Week = g.Week, Career_Ending = 0, Num_of_Plays = 0, Num_of_Weeks = 0, Season_Ending = 0 };

                    int itype_num = CommonUtils.getRandomNum(1, 100);
                    if (itype_num <= app_Constants.CHANCE_CAREER_ENDING)
                        r.Career_Ending = 1;
                    else if (itype_num <= app_Constants.CHANCE_SEASON_ENDING)
                        r.Season_Ending = 1;
                    else if (itype_num <= app_Constants.CHANCE_WEEKS)
                    {
                        int iWeeks = CommonUtils.getRandomNum(1, app_Constants.MAX_WEEKS_OUT);
                        r.Num_of_Weeks = iWeeks;
                    }
                    else
                    {
                        int iPlays = CommonUtils.getRandomNum(1, app_Constants.MAX_PLAYS_OUT);
                        r.Num_of_Weeks = iPlays;
                    }
                    break;
                }

                inj_message = Environment.NewLine + Environment.NewLine;
                string injury_text = null;
                if (r.Num_of_Plays > 0)
                    injury_text = " has been shaken up and will be out for " + r.Num_of_Plays + " plays";
                else if (r.Num_of_Weeks > 0)
                    injury_text = " has suffered an injury and will be out for " + r.Num_of_Weeks + " weeks";
                else if (r.Season_Ending == 1)
                    injury_text = " has suffered a serious injury and will be out for the rest of the season";
                else
                    injury_text = " has suffered a horrific career ending injury.  He will never return from this";

                inj_message += injury_player_name + " of the " + injury_team_nickname + " has suffered";

            }  //if injury

            return r;
        }

        private void Reduce_Play_Injuries()
        {
            List<Injury> injuries_ended = lInj.Where(x => x.Num_of_Plays == 1).ToList();
            List<Injury> injuries_reduced = lInj.Where(x => x.Num_of_Plays > 1).ToList();

            //These players can return to play
            foreach (Injury j in injuries_ended)
                lInj.Remove(j);

            //These players are one play closer to returning to the field
            foreach (Injury j in injuries_reduced)
                j.Num_of_Plays -= 1;

        }
        private List<Game_Player> setGamePlayerLIsts(double Line_of_Scrimmage, Formation f)
        {
            List<Game_Player> r = new List<Game_Player>();
            foreach (Formation_Rec fr in f.Player_list)
            {
                r.Add(new Game_Player()
                {
                    bCarryingBall = fr.bCarryingBall,
                    Current_Vertical_Percent_Pos = fr.Vertical_Percent_Pos,
                    Current_YardLine = Line_of_Scrimmage + fr.YardLine,
                    Starting_Vertical_Percent_Pos = fr.Vertical_Percent_Pos,
                    Starting_YardLine = Line_of_Scrimmage + fr.YardLine,
                    Pos = fr.Pos,
                    p_and_r = fr.p_and_r,
                    State = fr.State
                });
            }

            return r;
        }

        private int HorizontalAdj(bool b)
        {
            int r = 1;

            if (!b)
                r *= -1;

            return r;
        }

        private Player_States setRunningState(bool bLefttoRight, double x1, double y1, double x2, double y2)
        {
            Player_States r = Player_States.RUNNING_FORWARD;
            double xdiff = x1 - x2;
            double ydiff = y1 - y2;

            if (Math.Abs(xdiff) >= Math.Abs(ydiff) )
            {
                if (bLefttoRight)
                {
                    if (xdiff < 0)
                        r = Player_States.RUNNING_FORWARD;
                    else
                        r = Player_States.RUNNING_BACKWORDS;
                }
                else
                {
                    if (xdiff < 0)
                        r = Player_States.RUNNING_BACKWORDS;
                    else
                        r = Player_States.RUNNING_FORWARD;
                }
            }
            else
            {
                if (ydiff < 0)
                    r = Player_States.RUNNING_DOWN;
                else
                    r = Player_States.RUNNING_UP;
            }


            return r;
        }


    }
}
