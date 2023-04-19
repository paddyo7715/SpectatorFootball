using SpectatorFootball.Enum;
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

                int ball_stages = Game_Ball.Stages.Count();
                for(int pind = 0; pind < Offensive_Players.Count(); pind++)
                {
                    if (Offensive_Players[pind].Stages.Count() != ball_stages ||
                        Defensive_Players[pind].Stages.Count() != ball_stages)
                        throw new Exception("Number of stages do not match between ball, offensive and defensive players");
                }

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

        //Kickoff or Free Kick
        private Play_Result Kickoff_Normal_Play(Game_Ball gBall, List<Game_Player> Kickoff_Players, List<Game_Player> Return_Players, bool bLefttoRight, bool FreeKic, bool bSim)
        {
            Play_Result r = new Play_Result();
            r.bRighttoLeft = bLefttoRight;
            List<string> Play_Stages = new List<string>();

            //for testing print out all the players and their relevant ratings
            logger.Debug("bLefttoRight: " + bLefttoRight.ToString());
            logger.Debug(" ");

            logger.Debug("Kickoff Players");
            int d_index = 0;
            foreach (Game_Player p in Kickoff_Players)
            {
                string sPos = p.Pos.ToString();
                long leg_stn = p.p_and_r.pr.First().Kicker_Leg_Power_Rating;
                long leg_acc = p.p_and_r.pr.First().Kicker_Leg_Accuracy_Rating;
                long rn_att = p.p_and_r.pr.First().Run_Attack_Rating;
                logger.Debug("Ind:" + d_index +
                    " POS:" + sPos +
                    " Leg Strength:" + leg_stn +
                    " Leg Accuracy:" + leg_acc +
                    " Run Attack:" + rn_att
                    );
                d_index++;
            }
            logger.Debug("Receiving Players");
            d_index = 0;
            foreach (Game_Player p in Return_Players)
            {
                string sPos = p.Pos.ToString();
                long spd = p.p_and_r.pr.First().Speed_Rating;
                long agile = p.p_and_r.pr.First().Agilty_Rating;
                long rn_block = p.p_and_r.pr.First().Run_Block_Rating;
                long tkl = p.p_and_r.pr.First().Tackle_Rating;
                logger.Debug("Ind:" + d_index +
                    " POS:" + sPos +
                    " Spped:" + spd +
                    " Agility:" + agile +
                    " Run Blocking:" + rn_block +
                    " Tackling:" + tkl
                    );
                d_index++;
            }
            logger.Debug(" ");
            //=============================================

            //Get the kicker - kicker and returner must be slot 5 in the formation
            Game_Player Kicker = Kickoff_Players[5];
            Game_Player Returner = Return_Players[5];
            //================================  Stage One =======================================
            logger.Debug("Stage 1");
            logger.Debug("=====================================================");
            //================ Kicker Runs up to the ball and kicks it ==========================
            if (!bSim)
                gBall.TeeUp();

            int io_Players = 0;
            //cycle thru the offensive/kickoff team then he defense
            //if kicker then do their special thing; otherwise, the player just remains standing 
            foreach (Game_Player p in Kickoff_Players)
            {
                if (p == Kicker)
                {
                    double prev_yl = p.Current_YardLine;
                    double prev_v = p.Current_Vertical_Percent_Pos;
                    double Runup_end_yardline = p.Current_YardLine += 5.4 * HorizontalAdj(bLefttoRight);
                    double Runup_end_vert_pos = p.Current_Vertical_Percent_Pos += 0.0;

                    p.Current_YardLine = Runup_end_yardline +(0.4 * HorizontalAdj(bLefttoRight));
                    p.Current_Vertical_Percent_Pos += 0.0;

                    if (!bSim)
                    {
                        Player_States moving_ps = setRunningState(bLefttoRight, true, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos);
                        p.KickBall(moving_ps, prev_yl, prev_v, Runup_end_yardline, Runup_end_vert_pos);
                    }
                }
                else
                {
                    //Other players just stand there waiting for the kick
                    if (!bSim)
                        p.Stand();
                }
                io_Players++;
            }

            //The team receiving the kick will just stand there before the kick
            foreach (Game_Player p in Return_Players)
            {
                //Receiving players just stand there waiting for the kick
                if (!bSim)
                    p.Stand();
            }
            //===== End of Stage One - Kicker Runs up to the ball and kicks it ================
            logger.Debug("=======================================================");
            logger.Debug("");
            //================================  Stage Two =======================================
            //================ The ball flies thru the air and the players run ==================
            logger.Debug("Stage 2");
            logger.Debug("=======================================================");

            //Now determine how far and straight the kick is
            long leg_strength = Kicker.p_and_r.pr.First().Kicker_Leg_Power_Rating;
            KickOff_Length kick_length_enum = Kicking_Helper.getKickOff_Len_enum(leg_strength);
            double Kickoff_Len = Kicking_Helper.getKickoff_len(kick_length_enum);

            long leg_accuracy = Kicker.p_and_r.pr.First().Kicker_Leg_Accuracy_Rating;
            KickOff_Verticl Kick_Vert_enum = Kicking_Helper.getKickoff_Vert_enum(leg_accuracy);
            double Kickoff_Vert = Kicking_Helper.getKickoff_Vert(Kick_Vert_enum);

            logger.Debug("Kicker Leg Strength:" + leg_strength + " Leg Accuracy:" + leg_accuracy);
            logger.Debug("Kickoff length enum:" + kick_length_enum.ToString());
            logger.Debug("Kickoff length:" + Kickoff_Len.ToString());
            logger.Debug("Kickoff Vert enum:" + Kick_Vert_enum.ToString());
            logger.Debug("Kickoff Vert:" + Kickoff_Vert.ToString());

            //Adjust the length of the kick based on the vertical
            Kickoff_Len = Kicking_Helper.AdjustKickLength(Kickoff_Len, Kickoff_Vert);
            logger.Debug("Adjusted Kickoff len:" + Kickoff_Len.ToString());

            //possision where ball should be caught
            gBall.Current_YardLine = gBall.Starting_YardLine + (Kickoff_Len * HorizontalAdj(bLefttoRight));
            gBall.Current_Vertical_Percent_Pos = Kickoff_Vert;
            if (!bSim)
                gBall.End_Over_End_Thru_Air();
   
            //decide which players are in group 1 (closest to returner( group 2 and group 3
            List<int?> group_1 = new List<int?>();
            List<int?> group_2 = new List<int?>();
            List<int?> group_3 = new List<int?>();
            int id_Players = 0;
            foreach (Game_Player p in Kickoff_Players)
            {
                long speed = p.p_and_r.pr.First().Speed_Rating;
                string debug_string = null;
                if (p != Kicker)
                {
                    int group_var = (int)speed - app_Constants.KICKOFF_SPEED_CUTOFF;
                    debug_string += "group_var:" + group_var + " ";
                    int temp1 = CommonUtils.getRandomNum(1, app_Constants.KICKOFF_GROUP_CALC_VARIABLE);
                    debug_string += "temp1:" + temp1 + " ";
                    if (temp1 <= group_var)
                    {
                        group_1.Add(id_Players);
                        debug_string += "added to group 1";
                    }
                    else
                    {
                        int temp2 = CommonUtils.getRandomNum(1, app_Constants.KICKOFF_GROUP_CALC_VARIABLE);
                        debug_string += "temp2:" + temp2 + " ";
                        if (temp2 <= group_var)
                        {
                            group_2.Add(id_Players);
                            debug_string += "added to group 2";
                        }
                        else
                        {
                            group_3.Add(id_Players);
                            debug_string += "added to group 3";
                        }
                    }
                    logger.Debug("Player slot:" + id_Players + " Speed:" + speed + " " + debug_string);
                }

                id_Players++;
            }

            logger.Debug("Groups before rearranging");
            logger.Debug("Group 1:" + string.Join(",", group_1.ToArray()));
            logger.Debug("Group 2:" + string.Join(",", group_2.ToArray()));
            logger.Debug("Group 3:" + string.Join(",", group_3.ToArray()));
            //Adjust the group lists if any one of them has > 5 members
            //emoveRandomIndexes(int Desired_size, List<int> lst)
            List<int?> g1_deletes = removeRandomIndexes(group_1);
            group_2.AddRange(g1_deletes);
            List<int?> g3_deletes = removeRandomIndexes(group_3);
            group_2.AddRange(g3_deletes);
            List<int?> g2_deletes = removeRandomIndexes(group_2);
            group_3.AddRange(g2_deletes);

            group_1 = group_1.OrderBy(x => x).ToList();
            group_2 = group_2.OrderBy(x => x).ToList();
            group_3 = group_3.OrderBy(x => x).ToList();

            logger.Debug("Groups after rearranging");
            logger.Debug("Group 1:" + string.Join(",", group_1.ToArray()));
            logger.Debug("Group 2:" + string.Join(",", group_2.ToArray()));
            logger.Debug("Group 3:" + string.Join(",", group_3.ToArray()));

            //Expand out the group lists
            group_1 = ExpandGroup(group_1);
            group_2 = ExpandGroup(group_2);
            group_3 = ExpandGroup(group_3);

            if (group_1.Count() > app_Constants.KICKOFF_PLAYERS_IN_GROUP ||
                group_2.Count() > app_Constants.KICKOFF_PLAYERS_IN_GROUP ||
                group_3.Count() > app_Constants.KICKOFF_PLAYERS_IN_GROUP)
                throw new Exception("Group list with more than " + app_Constants.KICKOFF_PLAYERS_IN_GROUP);

            double yardline_Offset;
            double vert_offset;

            id_Players = 0;
            logger.Debug("Kickoff Players:");
            foreach (Game_Player p in Kickoff_Players)
            {
                if (p == Kicker)
                {
                    yardline_Offset = Math.Abs(gBall.Current_YardLine - p.Current_YardLine) - 50;
                    vert_offset = gBall.Current_Vertical_Percent_Pos;
                }
                else
                {
                    //which group is the play in
                    if (group_1.Contains(id_Players))
                    {
                        int yard_off_returner = CommonUtils.getRandomNum(app_Constants.KICKOFF_GROUP_1_MIN, app_Constants.KICKOFF_GROUP_1_MAX);
                        yardline_Offset = Math.Abs(gBall.Current_YardLine - p.Current_YardLine) - yard_off_returner;
                        int group_index = group_1.IndexOf(id_Players);
                        vert_offset = gBall.Current_Vertical_Percent_Pos + getKickoffGroupOffset(group_index);

                        string sPos = p.Pos.ToString();
                        long spd = p.p_and_r.pr.First().Speed_Rating;
                        logger.Debug("Ind:" + id_Players +
                            " POS:" + sPos +
                            " target_YardLine:" + yardline_Offset * HorizontalAdj(bLefttoRight) +
                            " target vertical:" + vert_offset
                            );


                    }
                    else if (group_2.Contains(id_Players))
                    {
                        int yard_off_returner = CommonUtils.getRandomNum(app_Constants.KICKOFF_GROUP_2_MIN, app_Constants.KICKOFF_GROUP_2_MAX);
                        yardline_Offset = Math.Abs(gBall.Current_YardLine - p.Current_YardLine) - yard_off_returner;
                        int group_index = group_2.IndexOf(id_Players);
                        vert_offset = gBall.Current_Vertical_Percent_Pos + getKickoffGroupOffset(group_index);
                    }
                    else
                    {
                        int yard_off_returner = CommonUtils.getRandomNum(app_Constants.KICKOFF_GROUP_3_MIN, app_Constants.KICKOFF_GROUP_3_MAX);
                        yardline_Offset = Math.Abs(gBall.Current_YardLine - p.Current_YardLine) - yard_off_returner;
                        int group_index = group_3.IndexOf(id_Players);
                        vert_offset = gBall.Current_Vertical_Percent_Pos + getKickoffGroupOffset(group_index);
                    }
                }

                double prev_yl = p.Current_YardLine;
                double prev_v = p.Current_Vertical_Percent_Pos;
                p.Current_YardLine += yardline_Offset * HorizontalAdj(bLefttoRight);
                p.Current_Vertical_Percent_Pos = vert_offset;

/*                string sPos = p.Pos.ToString();
                long spd = p.p_and_r.pr.First().Speed_Rating;
                logger.Debug("Ind:" + ind_index +
                    " POS:" + sPos +
                    " prev_yl:" + prev_yl +
                    " prev_v:" + prev_v +
                    " Current_YardLine:" + p.Current_YardLine +
                    " Current_Vertical_Percent_Pos:" + p.Current_Vertical_Percent_Pos
                    );
*/

                if (!bSim)
                {
                    Player_States moving_ps = setRunningState(bLefttoRight, true, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos);
                    p.Run_Then_Stand(moving_ps, prev_yl, prev_v);
                }
                id_Players++;
            }

            id_Players = 0;
            foreach (Game_Player p in Return_Players)
            {
                //The player who will return the ball
                if (p == Returner)
                {
                    double prev_yl = p.Current_YardLine;
                    double prev_v = p.Current_Vertical_Percent_Pos;
                    p.Current_YardLine = gBall.Current_YardLine;
                    p.Current_Vertical_Percent_Pos = gBall.Current_Vertical_Percent_Pos;
                    if (!bSim)
                    {
                        Player_States moving_ps = setRunningState(bLefttoRight, false, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos);
                        p.Run_Then_CatchKick(moving_ps, prev_yl, prev_v);
                    }
                }
                else
                {
                    //Receiving players 
                    double prev_yl = p.Current_YardLine;
                    double prev_v = p.Current_Vertical_Percent_Pos;

                    p.Current_YardLine = Kickoff_Players[id_Players].Current_YardLine + (app_Constants.KICKOFF_DIST_BETWEEN_BLOCK_ATTACHERS * HorizontalAdj(bLefttoRight));
                    p.Current_Vertical_Percent_Pos = Kickoff_Players[id_Players].Current_Vertical_Percent_Pos;
                    if (!bSim)
                    {
                        Player_States moving_ps = setRunningState(bLefttoRight, false, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos);
                        p.Run_Then_Stand(moving_ps, prev_yl, prev_v);
                    }
                }
                id_Players++;
            }
            //===== End of Stage Two - The ball flies thru the air and the players run ============
            logger.Debug("=======================================================");
            logger.Debug("");

            //============================  Stage Three, four and five ==========================
            //==== Returner runs to group 1,2 and 3 other players block or attempt to tackle ====
            //Note that offensive team are now the tacklers
//            bool bTackled = false;

            double returner_catch_vert = Returner.Current_Vertical_Percent_Pos;
            int slot_index = 2;
            int prev_slot_index;
            List<int> Past_Blocker_Tackler_List = new List<int>();
            logger.Debug("Stage 3, 4 or 5");
            logger.Debug("=======================================================");
            //since the returner will catch the ball switch blefttoright
            if (bLefttoRight) bLefttoRight = false; else bLefttoRight = true;
            //                r.bSwitchPossession = true;

            List<int?> group = new List<int?>();
            for (int i = 1; i <= app_Constants.KICKOFF_TACKLING_GROUPS; i++)
            {
                switch (i)
                {
                    case 1:
                        group = group_1;
                        break;
                    case 2:
                        group = group_2;
                        break;
                    case 3:
                        group = group_3;
                         break;
                }

                logger.Debug("Group:" + i);
                logger.Debug("Group List:" + string.Join(",", group));
                logger.Debug("Group Count:" + group.Count());
                logger.Debug("=======================================================");

                double agility = Returner.p_and_r.pr.First().Agilty_Rating;
                logger.Debug("Returner Agility:" + agility);
                //Returners agility determines if he looks for an open 
                //slot to run thru in the group.
                int agility_var = (int)agility - app_Constants.KICKOFF_AGILITY_CUTOFF;
                logger.Debug("agility_var:" + agility_var);
                int r_agile = CommonUtils.getRandomNum(1, app_Constants.KICKOFF_AVOID_TRACKER_CALC_VARIABLE);
                logger.Debug("r_agile:" + r_agile);

                int Returner_Avoid_Tackle = 0;

                bool bFindOpenSlot = false;
                if (r_agile <= agility_var)
                    bFindOpenSlot = true;

                logger.Debug("bFindOpenSlot:" + bFindOpenSlot.ToString());

                int Tackler_Ability = 0;
                int Tackler_Index = 0;
                double Tacker_vert = 0.0;
                double returner_slot_vert = 0.0;
                double dbetweenVert = 0.0;

                Game_Player Tackler = null;

                bool bAnySlot = i == 1 ? true : false;
                prev_slot_index = slot_index;
                logger.Debug("bAnySlot: " + bAnySlot.ToString());
                slot_index = Game_Engine_Helper.getKickReturnRunSlot(slot_index, bFindOpenSlot, group, bAnySlot);
                logger.Debug("slot_index: " + slot_index.ToString());

                List<int> TB_List = new List<int>();  //tacklers/blocker around the returner

                double returner_swerve_vert = 0.0;
                double Breakthrough_len = 0.0;
                double Breakthrough_vert = 0.0;

                double returner_before_tackler_yardline = 0.0;
                double returner_before_tackler_vert = 0.0;
                double returner_hole_yl = 0.0;

                bool bSwereUp;
                if (group[slot_index] != null)
                {
                    logger.Debug("Tackler in this slot");

                    Tackler_Index = (int)group[slot_index];
                    TB_List.Add(Tackler_Index);

                    dbetweenVert = app_Constants.KICKOFF_GROUP_VERT_DIST / 2.0;

                    bSwereUp = CommonUtils.getRandomTrueFalse();
                    if (bSwereUp) 
                        dbetweenVert *= -1;
                    int? adjacent_tackler = getPossibleUporDownTackler(bSwereUp, slot_index, group);
                    if (adjacent_tackler != null)
                        TB_List.Add((int)adjacent_tackler);
                }
                else
                {
                    logger.Debug("The slot is empty");
                    //Since the returner is running to an open slot, let's see if the slot just above and below has potential tacklers
                    TB_List.AddRange(getPossibleAdjacentTacklers(slot_index, group));
                }

                //go thru the tacler/blocker list to determine if a tackle is made
                int b_list_ind = CommonUtils.getRandomIndex(TB_List.Count);
                for (int tb_xx = 0; tb_xx < TB_List.Count; tb_xx++)
                {
                    int tackler_ind = TB_List[b_list_ind];
                    block_result br = Game_Engine_Helper.Attempt_Block(true,
                        CommonUtils.getRandomNum(1, app_Constants.BLOCKING_MAX_RAND),
                        Return_Players[tackler_ind].p_and_r.pr.First().Pass_Block_Rating,
                        Return_Players[tackler_ind].p_and_r.pr.First().Run_Block_Rating,
                        Return_Players[tackler_ind].p_and_r.pr.First().Agilty_Rating,
                        Kickoff_Players[tackler_ind].p_and_r.pr.First().Pass_Attack_Rating,
                        Kickoff_Players[tackler_ind].p_and_r.pr.First().Run_Attack_Rating,
                        Kickoff_Players[tackler_ind].p_and_r.pr.First().Agilty_Rating,
                        Kickoff_Players[tackler_ind].p_and_r.pr.First().Speed_Rating);

                    long tackler_tackle_rating = Kickoff_Players[tackler_ind].p_and_r.pr.First().Tackle_Rating;
                    //adjust potential tackler's tackle rating based on the block
                    tackler_tackle_rating = Game_Engine_Helper.AdjustTackleRating_forBlock(br, tackler_tackle_rating);

                    bool bTack = Game_Engine_Helper.Make_Tackle(
                        Returner.p_and_r.pr.First().Speed_Rating,
                        Returner.p_and_r.pr.First().Agilty_Rating,
                        Returner.p_and_r.pr.First().Running_Power_Rating,
                        tackler_tackle_rating);

                    if (bTack)
                        Tackler = Kickoff_Players[tackler_ind];

                    if (Tackler != null)
                        break;

                    if (b_list_ind == TB_List.Count - 1)
                        b_list_ind = 0;
                    else
                        b_list_ind++;
                }

                if (TB_List.Count > 0)
                {
                    //the runner will run one yard before the tackler and then swerve up or down
                    int ind_close_Tklr = Game_Engine_Helper.getClosestKickGroupPlayerInd(slot_index, group);
                    returner_hole_yl = Kickoff_Players[ind_close_Tklr].Current_YardLine;

                    if (group[slot_index] != null)
                        returner_before_tackler_yardline = returner_hole_yl - (app_Constants.KICKOFF_YARDS_BEFORE_TACKLER * HorizontalAdj(bLefttoRight));
                    else
                        returner_before_tackler_yardline = returner_hole_yl - (app_Constants.KICKOFF_YARDS_BEFORE_TACKLER2 * HorizontalAdj(bLefttoRight));

                    returner_before_tackler_vert = returner_catch_vert + getKickoffGroupOffset(slot_index);
                    returner_swerve_vert = returner_before_tackler_vert + dbetweenVert;


                    logger.Debug("ind_close_Tklr: " + ind_close_Tklr + "  Kickoff_Players[ind_close_Tklr].Current_YardLine: " + Kickoff_Players[ind_close_Tklr].Current_YardLine);
                    logger.Debug("returner_before_tackler_yardline: " + returner_before_tackler_yardline + "  returner_before_tackler_vert: " + returner_before_tackler_vert);
                    logger.Debug("dbetweenVert: " + dbetweenVert + "  returner_swerve_vert: " + returner_swerve_vert);
                    logger.Debug("returner_swerve_vert: " + returner_swerve_vert);
                }
                else
                {
                    Breakthrough_vert = returner_catch_vert + getKickoffGroupOffset(slot_index);
                    Breakthrough_len = (app_Constants.KICKOFF_GROUP_1_MAX - app_Constants.KICKOFF_GROUP_1_MIN) + (app_Constants.KICKOFF_GROUP_2_MIN - app_Constants.KICKOFF_GROUP_1_MAX);
                }

                logger.Debug("Past_Blocker_Tackler_List: " + string.Join(",", Past_Blocker_Tackler_List.ToArray()));

                id_Players = 0;
                foreach (Game_Player p in Kickoff_Players)
                {
                    if (TB_List.Contains(id_Players))
                    {
                        //keep blocking till the returner runs up to you
                        if (!bSim)
                            p.Block();

                        double prev_yl = p.Current_YardLine;
                        double prev_v = p.Current_Vertical_Percent_Pos;

                        p.Current_YardLine = returner_hole_yl;
                        p.Current_Vertical_Percent_Pos = returner_swerve_vert;

                        //Move vertically to make the tackle
                        if (!bSim)
                        {
                            Player_States moving_ps = setRunningState(bLefttoRight, false, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos);
                            p.Attempt_Tackle(moving_ps, prev_yl, prev_v);
                        }
                    }
                    // Players from previous groups should still do what they last did not go back to blocking
                    else if (Past_Blocker_Tackler_List.Contains(id_Players))
                    {
                        if (!bSim)
                        {
                            p.Same_As_Last_Action();
                            if (TB_List.Count > 0)
                                p.Same_As_Last_Action();
                        }
                    }
                    else
                    {
                        if (!bSim)
                        {
                            p.Block();
                            //If there is a tackler then  continue to block while he attempts the tackle
                            if (TB_List.Count > 0)
                                p.Block();
                        }
                    }
                    id_Players++;
                }

                id_Players = 0;
                foreach (Game_Player p in Return_Players)
                {
                    if (p == Returner)  //Kick Returner
                    {
                        logger.Debug("Returner: ID " + id_Players);
                        if (TB_List.Count > 0)
                        {
                            logger.Debug("Returner runs up to tackler:");

                            double prev_yl = p.Current_YardLine;
                            double prev_v = p.Current_Vertical_Percent_Pos;

                            p.Current_YardLine = returner_before_tackler_yardline;
                            p.Current_Vertical_Percent_Pos = returner_before_tackler_vert;

                            //must move the ball too, even thogh it will not be visible.
                            gBall.Current_YardLine = p.Current_YardLine;
                            gBall.Current_Vertical_Percent_Pos = p.Current_Vertical_Percent_Pos;

                            logger.Debug("prev_yl:" + prev_yl.ToString() + " prev_v:" + prev_v.ToString());
                            logger.Debug("Current_YardLine:" + p.Current_YardLine.ToString() + " Current_Vertical_Percent_Pos:" + p.Current_Vertical_Percent_Pos.ToString());

                            if (!bSim)
                            {
                                Player_States moving_ps = setRunningState(bLefttoRight, true, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos);
                                p.Run(moving_ps, prev_yl, prev_v);

                                //for the ball
                                gBall.Carried(prev_yl, prev_v);
                            }

                            //he either gets tackled or not
                            prev_yl = p.Current_YardLine;
                            prev_v = p.Current_Vertical_Percent_Pos;

                            p.Current_YardLine = returner_hole_yl;
                            p.Current_Vertical_Percent_Pos = returner_swerve_vert;

                            //must move the ball too, even thogh it will not be visible.
                            gBall.Current_YardLine = p.Current_YardLine;
                            gBall.Current_Vertical_Percent_Pos = p.Current_Vertical_Percent_Pos;

                            logger.Debug("prev_yl:" + prev_yl.ToString() + " prev_v:" + prev_v.ToString());
                            logger.Debug("Current_YardLine:" + p.Current_YardLine.ToString() + " Current_Vertical_Percent_Pos:" + p.Current_Vertical_Percent_Pos.ToString());

                            if (!bSim)
                            {
                                Player_States moving_ps = setRunningState(bLefttoRight, true, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos);
                                if (Tackler != null)
                                    p.Run_and_Tackled(moving_ps, prev_yl, prev_v);
                                else
                                    p.Run(moving_ps, prev_yl, prev_v);

                                //for the ball
                                gBall.Carried(prev_yl, prev_v);
                            }
                        }
                        else
                        {
                            logger.Debug("Returner Breaks Thru:");

                            double prev_yl = p.Current_YardLine;
                            double prev_v = p.Current_Vertical_Percent_Pos;

                            p.Current_YardLine += Breakthrough_len * HorizontalAdj(bLefttoRight);
                            p.Current_Vertical_Percent_Pos = Breakthrough_vert;

                            gBall.Current_YardLine = p.Current_YardLine;
                            gBall.Current_Vertical_Percent_Pos = p.Current_Vertical_Percent_Pos;

                            logger.Debug("prev_yl:" + prev_yl.ToString() + " prev_v:" + prev_v.ToString());
                            logger.Debug("Current_YardLine:" + p.Current_YardLine.ToString() + " Current_Vertical_Percent_Pos:" + p.Current_Vertical_Percent_Pos.ToString());

                            if (!bSim)
                            {
                                Player_States moving_ps = setRunningState(bLefttoRight, true, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos);
                                p.Run(moving_ps, prev_yl, prev_v);

                                //for the ball
                                gBall.Carried(prev_yl, prev_v);
                            }
                        }
                    }
                    // Players from previous groups should still do what they last did not go back to blocking
                    else if (Past_Blocker_Tackler_List.Contains(id_Players))
                    {
                        if (!bSim)
                        {
                            p.Same_As_Last_Action();
                            if (TB_List.Count > 0)
                                p.Same_As_Last_Action();
                        }
                    }
                    else if (TB_List.Contains(id_Players))
                    {
                        logger.Debug("Blocker");
 
                        if (!bSim)
                        {
                            p.Block();
                            p.Stand();
                        }
                    }
                    else
                    {
                        if (!bSim)
                        {
                            p.Block();

                            if (TB_List.Count > 0)
                                p.Block();
                        }
                    }
                    id_Players++;
                }

                //bpo test code take it out
                //bTackled = true;
                //=========================
                if (Tackler != null) break;

                Past_Blocker_Tackler_List.AddRange(group.Where(x => x != null).Select(x => (int)x).ToList());
            }  //on group 1,2 or 3


            
            //===== End of Stage Three,four or five - the returner runs thru groups 1,2 and 3 ============
            logger.Debug("=======================================================");
            logger.Debug("");


            //==== Stage Six Kicker tries to tackle the returner if the returner makes it this far =====
            logger.Debug("Stage 6");
            logger.Debug("=======================================================");

            bool bTackled = false;
            bool bTacklerFallDown = false;
            double slot_vert = 0;
            if (!bTackled)
            {
                logger.Debug("Stage Six:");

                //Returners agility determines if he looks for a hole to rnn thru.
                double agility = Returner.p_and_r.pr.First().Agilty_Rating;
                logger.Debug("Returner Agility:" + agility);
                //                int agility_var = (int)agility - app_Constants.KICKOFF_AGILITY_CUTOFF;
                //                logger.Debug("agility_var:" + agility_var);
                int r_agile = CommonUtils.getRandomNum(1, app_Constants.KICKOFF_AVOID_TRACKER_CALC_VARIABLE);
                logger.Debug("r_agile:" + r_agile);
                bool bFindOpenSlot = false;
                if (r_agile <= agility)
                    bFindOpenSlot = true;
                logger.Debug("bFindOpenSlot:" + bFindOpenSlot.ToString());

                //Determine if the kicker will tackle the returner
                long Returner_Avoid_Tackle = (int)(Returner.p_and_r.pr.First().Agilty_Rating +
                  Returner.p_and_r.pr.First().Speed_Rating) / 2;
                logger.Debug("Returner_Avoid_Tackle:" + Returner_Avoid_Tackle.ToString());
                long Tackler_Ability = (int)(Kicker.p_and_r.pr.First().Tackle_Rating);
                logger.Debug("Tackler_Ability:" + Tackler_Ability.ToString());

                bool bPossibleTacker = false;
                string slot_string = null;
                bool bTackler = false;

                //The index of the kicker doesn't matter
                List<int?> g_list = new List<int?>() {null, null,2,null,null };
                slot_index = Game_Engine_Helper.getKickReturnRunSlot(slot_index, bFindOpenSlot, g_list, false);
                logger.Debug("slot_index:" + slot_index);

                //bpo test
                //slot_index = 2;

                if (slot_index == 2)
                    bTackler = true;

                logger.Debug("Tackler_Index:" + slot_index);

                if (bTackler)
                {
                    int rtack = CommonUtils.getRandomNum(1, app_Constants.KICKOFF_KICKER_MAKE_TACKLE_CALC_VARIABLE);
                    if (rtack <= Tackler_Ability)
                    {
                        bTackled = true;
                        bTacklerFallDown = true;
                    }
                    else
                    {
                        bTackled = false;
                        bTacklerFallDown = true;
                    }
                }
                else
                {
                    bTackled = false;
                    bTacklerFallDown = true;
                }

                slot_vert = Kicker.Current_Vertical_Percent_Pos + getKickoffGroupOffset(slot_index);

                double BreakAwayYardline = 0.0;
                double dbetweenVert = 0.0;
                double returner_before_tackler_yardline = 0.0;
                double returner_before_tackler_vert = 0.0;

                if (bTackler)
                {
                    //the runner will run one yard before the tackler and then swerve up or down
                    returner_before_tackler_yardline = Kicker.Current_YardLine - (app_Constants.KICKOFF_YARDS_BEFORE_TACKLER * HorizontalAdj(bLefttoRight));
                    returner_before_tackler_vert = Kicker.Current_Vertical_Percent_Pos;

                    dbetweenVert = app_Constants.KICKOFF_GROUP_VERT_DIST / 2.0;
                    double returner_swerve_vert;
                    int r_swerve = CommonUtils.getRandomNum(1, 2);
                    if (r_swerve == 1)
                        dbetweenVert *= -1;

                    returner_swerve_vert = Kicker.Current_Vertical_Percent_Pos + dbetweenVert;

                    logger.Debug("Slot: " + slot_index + "Curent vert: " + Kicker.Current_Vertical_Percent_Pos + " getKickoffGroupOffset:" + getKickoffGroupOffset(slot_index));
                }
                else
                {
                    if (bLefttoRight)
                        BreakAwayYardline = 105.0;
                    else
                        BreakAwayYardline = -5.0;
                }

                id_Players = 0;
                foreach (Game_Player p in Kickoff_Players)
                {
                    if (p == Kicker)
                    {
                        logger.Debug("Kicker Tacker:");

                        //keep blocking till the returner runs up to you
                        if (!bSim)
                            p.Stand();

                        double prev_yl = p.Current_YardLine;
                        double prev_v = p.Current_Vertical_Percent_Pos;

                        p.Current_Vertical_Percent_Pos += dbetweenVert;

                        logger.Debug("prev_yl:" + prev_yl.ToString() + " prev_v:" + prev_v.ToString());
                        logger.Debug("Current_YardLine:" + p.Current_YardLine.ToString() + " Current_Vertical_Percent_Pos:" + p.Current_Vertical_Percent_Pos.ToString());

                        //Move vertically to make the tackle
                        if (bTackler)
                        {
                            Player_States moving_ps = setRunningState(bLefttoRight, false, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos);
                            p.Attempt_Tackle(moving_ps, prev_yl, prev_v);
                        }
                    }
                    else if (Past_Blocker_Tackler_List.Contains(id_Players))
                    {
                        if (!bSim)
                        {
                            p.Same_As_Last_Action();
                            if (bTackler)
                                p.Same_As_Last_Action();
                        }
                    }
                    else
                    {
                        if (!bSim)
                        {
                            p.Block();

                            //If there is a tackler then  continue to block while he attempts the tackle
                            if (bTackler)
                                p.Block();
                        }
                    }
                    id_Players++;
                }

                id_Players = 0;
                foreach (Game_Player p in Return_Players)
                {
                    if (p == Returner)  //Kick Returner
                    {
                        logger.Debug("Returner: ID " + id_Players);
                        if (bTackler)
                        {
                            logger.Debug("Returner runs up to tackler:");

                            double prev_yl = p.Current_YardLine;
                            double prev_v = p.Current_Vertical_Percent_Pos;

                            p.Current_YardLine = returner_before_tackler_yardline;
                            p.Current_Vertical_Percent_Pos = returner_before_tackler_vert;

                            //must move the ball too, even thogh it will not be visible.
                            gBall.Current_YardLine = p.Current_YardLine;
                            gBall.Current_Vertical_Percent_Pos = p.Current_Vertical_Percent_Pos;

                            logger.Debug("prev_yl:" + prev_yl.ToString() + " prev_v:" + prev_v.ToString());
                            logger.Debug("Current_YardLine:" + p.Current_YardLine.ToString() + " Current_Vertical_Percent_Pos:" + p.Current_Vertical_Percent_Pos.ToString());

                            if (!bSim)
                            {
                                Player_States moving_ps = setRunningState(bLefttoRight, true, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos);
                                p.Run(moving_ps, prev_yl, prev_v);

                                //for the ball
                                gBall.Carried(prev_yl, prev_v);
                            }

                            //he either gets tackled or not
                            prev_yl = p.Current_YardLine;
                            prev_v = p.Current_Vertical_Percent_Pos;

                            p.Current_YardLine = Kicker.Current_YardLine;
                            p.Current_Vertical_Percent_Pos = Kicker.Current_Vertical_Percent_Pos;

                            //must move the ball too, even thogh it will not be visible.
                            gBall.Current_YardLine = p.Current_YardLine;
                            gBall.Current_Vertical_Percent_Pos = p.Current_Vertical_Percent_Pos;

                            logger.Debug("prev_yl:" + prev_yl.ToString() + " prev_v:" + prev_v.ToString());
                            logger.Debug("Current_YardLine:" + p.Current_YardLine.ToString() + " Current_Vertical_Percent_Pos:" + p.Current_Vertical_Percent_Pos.ToString());

                            if (!bSim)
                            {
                                Player_States moving_ps = setRunningState(bLefttoRight, true, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos);
                                if (bTackled)
                                    p.Run_and_Tackled(moving_ps, prev_yl, prev_v);
                                else
                                    p.Run(moving_ps, prev_yl, prev_v);

                                //for the ball
                                gBall.Carried(prev_yl, prev_v);
                            }
                        }
                        else
                        {
                            logger.Debug("Returner Breaks Thru:");

                            double prev_yl = p.Current_YardLine;
                            double prev_v = p.Current_Vertical_Percent_Pos;

                            p.Current_YardLine = BreakAwayYardline;

                            gBall.Current_YardLine = p.Current_YardLine;
                            gBall.Current_Vertical_Percent_Pos = p.Current_Vertical_Percent_Pos;

                            logger.Debug("prev_yl:" + prev_yl.ToString() + " prev_v:" + prev_v.ToString());
                            logger.Debug("Current_YardLine:" + p.Current_YardLine.ToString() + " Current_Vertical_Percent_Pos:" + p.Current_Vertical_Percent_Pos.ToString());

                            if (!bSim)
                            {
                                Player_States moving_ps = setRunningState(bLefttoRight, true, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos);
                                p.Run(moving_ps, prev_yl, prev_v);

                                //for the ball
                                gBall.Carried(prev_yl, prev_v);
                            }
                        }
                    }
                    else if (Past_Blocker_Tackler_List.Contains(id_Players))
                    {
                        if (!bSim)
                        {
                            p.Same_As_Last_Action();
                            if (bTackler)
                                p.Same_As_Last_Action();
                        }
                    }
                    else
                    {
                        if (!bSim)
                        {
                            p.Block();

                            if (bTackler)
                                p.Block();
                        }
                    }
                }
                logger.Debug("=======================================================");
                logger.Debug("");
            } //not tackled

            //===== end of stage six
            if (!bSim) r.Play_Stages = Play_Stages;
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
                    logger.Debug("checkforinjuries while (true) pass");
                    int iFormNum = CommonUtils.getRandomNum(1, injury_team.Count()) - 1;
                    Formation_Rec injured_player = injury_team[iFormNum];
                    //it should be rare that a kicker is injured
                    if (injured_player.Pos == Player_Pos.K || injured_player.Pos == Player_Pos.P)
                    {
                        kickerCount++;

                        if (kickerCount < 2)
                            continue;
                    }

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
                    State = fr.State,
                    Initial_State = fr.State
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

        private Player_States setRunningState(bool bLefttoRight, bool bOffense, double x1, double y1, double x2, double y2)
        {
            Player_States r = Player_States.RUNNING_FORWARD;
            double xdiff = x2 - x1;
            double ydiff = (y2 - y1) / 2.5;

            if (Math.Abs(xdiff) >= Math.Abs(ydiff))
            {
                if (bLefttoRight)
                {
                    if (bOffense)
                    {
                        if (xdiff < 0 && xdiff < -app_Constants.MOVEMENT_DIST_BEFORE_TURNING_BACK)
                            r = Player_States.RUNNING_BACKWORDS;
                        else if (xdiff < 0)
                            r = Player_States.RUNNING_FORWARD;
                        else
                            r = Player_States.RUNNING_FORWARD;
                    }
                    else
                    {
                        if (xdiff > 0 && xdiff > app_Constants.MOVEMENT_DIST_BEFORE_TURNING_BACK)
                            r = Player_States.RUNNING_BACKWORDS;
                        else if (xdiff > 0)
                            r = Player_States.RUNNING_FORWARD;
                        else
                            r = Player_States.RUNNING_FORWARD;
                    }
                }
                else
                {
                    if (bOffense)
                    {
                        if (xdiff > 0 && xdiff > app_Constants.MOVEMENT_DIST_BEFORE_TURNING_BACK)
                            r = Player_States.RUNNING_BACKWORDS;
                        else if (xdiff > 0)
                            r = Player_States.RUNNING_FORWARD;
                        else
                            r = Player_States.RUNNING_FORWARD;
                    }
                    else
                    {
                        if (xdiff < 0 && xdiff < -app_Constants.MOVEMENT_DIST_BEFORE_TURNING_BACK)
                            r = Player_States.RUNNING_BACKWORDS;
                        else if (xdiff < 0)
                            r = Player_States.RUNNING_FORWARD;
                        else
                            r = Player_States.RUNNING_FORWARD;
                    }
                }
            }
            else
            {
                if (ydiff > 0)
                    r = Player_States.RUNNING_DOWN;
                else
                    r = Player_States.RUNNING_UP;
            }

            return r;
        }
        private List<int?> removeRandomIndexes(List<int?> lst)
        {
            List<int?> r = new List<int?>();

            while (lst.Count > app_Constants.KICKOFF_PLAYERS_IN_GROUP)
            {
                int ind = CommonUtils.getRandomNum(1, lst.Count()) - 1;
                r.Add(lst[ind]);
                lst.RemoveAt(ind);
            }

            return r;
        }
        //This method will expand the passed in tackler group list
        private List<int?> ExpandGroup(List<int?> Group)
        {
            List<int?> r = new List<int?>();
            int empty_spots = app_Constants.KICKOFF_PLAYERS_IN_GROUP - Group.Count();

            foreach (int? s in Group)
            {
                bool bStopEmpties = false;
                while(!bStopEmpties && empty_spots > 0)
                {
                    int rnd = CommonUtils.getRandomNum(1, 10);
                    if (rnd <= 6)
                    {
                        r.Add(null);
                        empty_spots--;
                    }
                    else
                        bStopEmpties = true;
                }

                 r.Add(s);
            }

            for (int i = 0; i < empty_spots; i++)
                r.Add(null);

            return r;
        }

         private double getKickoffGroupOffset(int ind)
        {
            double r = 0;

            ind -= 2;

            r = app_Constants.KICKOFF_GROUP_VERT_DIST * ind;

            return r;
        }
        List<int> getPossibleAdjacentTacklers(int slot_index, List<int?> group)
        {
            List<int> r = new List<int>();

            //Check one spot above
            if (slot_index > 0)
            {
                int above_slot = slot_index - 1;
               if (group[above_slot] != null)
                    r.Add((int)group[above_slot]);
            }

            //Check one apot below
            if (slot_index < app_Constants.KICKOFF_PLAYERS_IN_GROUP-1)
            {
                int below_slot = slot_index + 1;
                if (group[below_slot] != null)
                    r.Add((int)group[below_slot]);
            }

            return r;
        }

        public int? getPossibleUporDownTackler(bool bSwerveUp, int slot_index, List<int?> group)
        {
            int? r = null;

            if (bSwerveUp)
            {
                if (slot_index > 0) r = group[slot_index - 1];
            }
            else
            {
                if (slot_index < app_Constants.KICKOFF_PLAYERS_IN_GROUP - 1) r = group[slot_index + 1];
            }

            return r;
        }


    }
}
