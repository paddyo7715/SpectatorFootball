using SpectatorFootball.Enum;
using SpectatorFootball.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using log4net;
using System.Text;
using System.Threading.Tasks;
using SpectatorFootball.PenaltiesNS;

namespace SpectatorFootball.GameNS
{

    [Serializable]
    public class GameEngine
    {
        private static ILog logger = LogManager.GetLogger("RollingFile");
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
        private double g_Yards_to_go;
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

        private List<Penalty> Penalty_List = null;

        //Game Settings
        private bool bSureGame = false;
        private bool bAllowKickoffs = false;
        private bool bTwoPointConv = false;
        private bool bThreePointConv = false;
        private bool bAllowInjuries = false;
        private bool bAllowPenalties = false;
        private bool bSimGame = false;
        private List<Penalty> PenaltiesData = null;

        //other settings
        private double YardsInField = 100.0;
        private double KickoffYardline = 35.0;
        private double FreeKickYardline = 20.0;
        private double TouchbackYardline = 25.0;
        private double nonKickoff_StartingYardline = 20.0;
        private long non_forfeit_win_score = 2;
        private long forfeit_lose_score = 0;

 
        public GameEngine(Game g, Teams_by_Season at, List<Player_and_Ratings> Away_Players,
            Teams_by_Season ht, List<Player_and_Ratings> Home_Players, bool bSimGame, 
            List<Penalty> PenaltiesData, long two_point_con, long three_point_conv,
            long Kickoff_Type, long Injuries, long Penalties)
        {
            this.at = at;
            this.Away_Players = Away_Players;
            this.ht = ht;
            this.Home_Players = Home_Players;
            this.g = g;
            this.bSimGame = bSimGame;
            this.Penalty_List = PenaltiesData;

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

            List<Game_Player_Penalty_Stats> Game_Player_Penalty_Stats = new List<Game_Player_Penalty_Stats>();
            List<Game_Scoring_Summary> Game_Scoring_Summary = new List<Game_Scoring_Summary>();
            List<Game_Player_Stats> Game_Player_Stats = new List<Game_Player_Stats>();

            //Add starter records for both teams
            foreach (Player_Pos pp in System.Enum.GetValues(typeof(Player_Pos)))
            {
                int num_starters = Team_Helper.getNumStartingPlayersByPosition(pp);
                List<Player_and_Ratings> away_starter_list = Away_Players.Where(x => x.p.Pos == (int)pp).OrderBy(x => x.p.Pos).ThenByDescending(x => x.Overall_Grade).Take(num_starters).ToList();
                foreach (Player_and_Ratings a in away_starter_list)
                    Game_Player_Stats.Add(new Game_Player_Stats() { Franchise_ID = at.Franchise_ID, Game_ID = g.ID, Player_ID = a.p.ID, Started = 1 });

                List<Player_and_Ratings> home_starter_list = Home_Players.Where(x => x.p.Pos == (int)pp).OrderBy(x => x.p.Pos).ThenByDescending(x => x.Overall_Grade).Take(num_starters).ToList();
                foreach (Player_and_Ratings a in away_starter_list)
                    Game_Player_Stats.Add(new Game_Player_Stats() { Franchise_ID = ht.Franchise_ID, Game_ID = g.ID, Player_ID = a.p.ID, Started = 1 });
            }

            //initialize the injuries list
            lInj = new List<Injury>();

            //set max possible point for 1 touchdown
            if (two_point_con == 1)
            {
                bTwoPointConv = true;
                Max_TD_Points = 8;
            }

            if (three_point_conv == 1)
            {
                bThreePointConv = true;
                Max_TD_Points = 9;
            }

            if (Kickoff_Type == 1)
                bAllowKickoffs = true;
            else
                bAllowKickoffs = false;

            if (Injuries == 1)
                bAllowInjuries = true;
            else
                bAllowInjuries = false;

            if (Penalties == 1)
                bAllowPenalties = true;
            else
                bAllowPenalties = false;

            //create the two coaching objects
            Away_Coach = new Coach(at.Franchise_ID, g, Max_TD_Points, Home_Players, Away_Players, lInj);
            Home_Coach = new Coach(ht.Franchise_ID, g, Max_TD_Points, Home_Players, Away_Players, lInj);

            g_fid_posession = CoinToss(CommonUtils.getRandomNum(1, 100), ht.Franchise_ID, at.Franchise_ID);
            Fid_first_posession = g_fid_posession;

            bKickoff = true;
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

            bool isBallCarryingTeam = false;

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
            else if (bKickoff)             //if this play is a kickoff then set where to kickoff from
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
            double PossessionAdjuster = Game_Engine_Helper.HorizontalAdj(bLefttoRight);

            //Call the play
            Offensive_Package = Offensive_Coach.Call_Off_PlayFormation(bKickoff, bExtraPoint, bFreeKick, PossessionAdjuster);
            DEF_Formation = Defensive_Coach.Call_Def_Formation(Offensive_Package, PossessionAdjuster);
            logger.Debug("ExecutePlay Offensive and Defensive plays called.");

            //You could get the allow substitutions from either coach
            bool bAllowSubs = Home_Coach.AllowSubstitutions(Offensive_Package.Formation.bSpecialTeams);

            if (Offensive_Package.Formation.bSpecialTeams)
                bAllowSubs = true;

            logger.Debug("ExecutePlay Allowsubstituions finished.");

            // Get the Game Values Before the Play is Executed
            r.Line_of_Scimmage = g_Line_of_Scrimmage;
            r.bLefttoRight = bLefttoRight;

            r.Before_Away_Score = g.Away_Score.ToString();
            r.Before_Home_Score = g.Home_Score.ToString();

            Down_and_Yards = Game_Helper.getDownAndYardString(g_Down, g_Yards_to_go, g_Line_of_Scrimmage, bLefttoRight);
            r.Before_Down_and_Yards = Down_and_Yards;
            r.Before_Away_Timeouts = g_Away_timeouts.ToString();
            r.Before_Home_Timeouts = g_Home_timeouts.ToString();

            r.Before_Display_QTR = Game_Helper.getQTRString((long)g.Quarter) + " QTR";
            r.Before_Display_Time = Game_Helper.getTimestringFromSeconds((long)g.Time);
            //=================================================================

            bool bPlayWithReturner = isPlayWithReturner(Offensive_Package.Play);

            logger.Debug("Before  Offensive_Coach.Populate_Formation");
            Offensive_Package.Formation.Player_list = Offensive_Coach.Populate_Formation(
                Offensive_Package.Formation.Player_list,
                bAllowSubs, Offensive_Package.Formation.bSpecialTeams, false);
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

                r.Long_Message = getForfeit_Message(at.Nickname, ht.Nickname, (long)g.Away_Score, (long)g.Home_Score);
            }
            logger.Debug("Before  Defensive_Coach.Populate_Formation");
            DEF_Formation.Player_list = Defensive_Coach.Populate_Formation(
                DEF_Formation.Player_list,
                 bAllowSubs, DEF_Formation.bSpecialTeams, bPlayWithReturner);
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

                r.Long_Message = getForfeit_Message(at.Nickname, ht.Nickname, (long)g.Away_Score, (long)g.Home_Score);
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

                switch (Offensive_Package.Play)
                {
                    case Play_Enum.KICKOFF_NORMAL:
                        Play_Kickoff_Normal Kickoff_k = new Play_Kickoff_Normal(g_fid_posession, at.Franchise_ID, ht.Franchise_ID, Game_Ball, Offensive_Players, Defensive_Players, bLefttoRight, false, bSimGame, false);
                        Kickoff_k.init();
                        p_result = Kickoff_k.Execute();
                        break;
                    case Play_Enum.FREE_KICK:
                        Play_Kickoff_Normal Kickoff_fk = new Play_Kickoff_Normal(g_fid_posession, at.Franchise_ID, ht.Franchise_ID, Game_Ball, Offensive_Players, Defensive_Players, bLefttoRight, true, bSimGame, false);
                        Kickoff_fk.init();
                        p_result = Kickoff_fk.Execute();
                        break;
                }

                int ball_stages = Game_Ball.Stages.Count();
                for(int pind = 0; pind < Offensive_Players.Count(); pind++)
                {
                    if (Offensive_Players[pind].Stages.Count() != ball_stages ||
                        Defensive_Players[pind].Stages.Count() != ball_stages)
                        throw new Exception("Number of stages do not match between ball, offensive and defensive players");
                }

                if (bAllowPenalties && Penalty_Helper.isNoPenaltyPlay(p_result, Offensive_Package.Play))
                {
                    Tuple<Game_Player, Penalty> t = Penalty_Helper.PostSnap_Penalty(Offensive_Package.Play, Penalty_List,
                        Offensive_Players, Defensive_Players, p_result);
                    p_result.Penalized_Player = t.Item1;
                    p_result.Penalty = t.Item2;

                    if (p_result.Penalized_Player != null)
                    {
                        bool bAway_Pen_Player = Away_Players.Any(x => x.p == p_result.Penalized_Player.p_and_r.p);
                        bool bHome_Pen_Player = Home_Players.Any(x => x.p == p_result.Penalized_Player.p_and_r.p);
                        Coach Penalty_Coach = null;
                        if (bAway_Pen_Player)
                        {
                            p_result.bPenatly_on_Away_Team = true;
                            Penalty_Coach = Home_Coach;
                        }
                        else
                        {
                            p_result.bPenatly_on_Away_Team = false;
                            Penalty_Coach = Away_Coach;
                        }

                        isBallCarryingTeam = isBallTeamPenalty(p_result);
                        if (p_result.Penalty.bDeclinable)
                        {
                            if (isBallCarryingTeam)
                                p_result.bPenalty_Rejected = !Penalty_Coach.AcceptOff_Penalty(Offensive_Package.Play, p_result, g_Yards_to_go, g_Line_of_Scrimmage, bLefttoRight, false, false);
                            else
                                p_result.bPenalty_Rejected = !Penalty_Coach.AcceptDef_Penalty(Offensive_Package.Play, p_result, g_Yards_to_go ,g_Line_of_Scrimmage, bLefttoRight, false, false);
                        }
                    }
                }

                //set results and accume team stats
                bool bswitchPossession = false;
                double penalty_yards = 0;
                p_result = setPlayOutCome(isBallCarryingTeam, Offensive_Package.Play, g_Down,
                    g_Yards_to_go, p_result, g_Line_of_Scrimmage, bLefttoRight, TouchbackYardline);

                //Add penalty if applicable
                if (p_result.Penalty != null && !p_result.bPenalty_Rejected)
                {
                    Game_Player_Penalty_Stats pen_stat = new Game_Player_Penalty_Stats();
                    long f_id = Away_Players.Contains(p_result.Penalized_Player.p_and_r) ? at.Franchise_ID : ht.Franchise_ID;

                    Game_Player_Penalty_Stats pen_rec = new Game_Player_Penalty_Stats()
                    {
                        Franchise_ID = f_id,
                        Game_ID = g.ID,
                        Penalty_Code = p_result.Penalty.code.ToString(),
                        Penalty_Yards = (long)p_result.Final_Added_Penalty_Yards,
                        Player_ID = p_result.Penalized_Player.p_and_r.p.ID
                    };
                    p_result.Play_Player_Penalty_Stats.Add(pen_rec);
                }


                if (p_result.bPlay_Stands)
                {
                    bKickoff = p_result.bFinal_NextPlayKickoff;
                    bFreeKick = p_result.bFinal_NextPlayFreeKick;
                    bExtraPoint = p_result.bFinal_NextPlayXP;

                    if (p_result.bFinal_SwitchPossession)
                        bswitchPossession = true;

                    //acume individual stats
                    Accume_Play_Stats(p_result.Play_Player_Stats,
                         p_result.Play_Player_Penalty_Stats);
                    logger.Debug("After accum stats");
                }

                UpdateScore(p_result, g);

                g_Down = p_result.Final_Down;
                g_Yards_to_go = p_result.Final_yard_to_go;
                //Final_end_of_play NA when next play is kickoff, freekick of XP.  This will be set 
                //on the next play
                g_Line_of_Scrimmage = p_result.Final_end_of_Play_Yardline;
                penalty_yards = p_result.Final_Added_Penalty_Yards;

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

                if (bswitchPossession)
                    g_fid_posession = Switch_Posession(g_fid_posession, at.Franchise_ID, ht.Franchise_ID);

            }

            g_bGameOver = isGameEnd((long) g.Playoff_Game, (long) g.Quarter, (long) g.Time, (long) g.Away_Score, (long) g.Home_Score);

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
        public static string getForfeit_Message(string away_team_name, string home_team_name, long away_score, long home_score)
        {
            string r = "";

            string win_team = "";
            string lose_team = "";
            long win_score = 0;
            long lose_score = 0;

            if (away_score > home_score)
            {
                win_team = away_team_name;
                lose_team = home_team_name;
                win_score = away_score;
                lose_score = home_score;
            }
            else
            {
                win_team = home_team_name;
                lose_team = away_team_name;
                win_score = home_score;
                lose_score = away_score;
            }

            r = "The " + win_team + " have won the game through forfeit, becuase the " + lose_team + " could not field enough players." + Environment.NewLine + Environment.NewLine; ;
            r += "The final score is " + win_score + " to " + lose_score;

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
        public void Accume_Play_Stats(List<Game_Player_Stats> Game_Player_Stats,
         List<Game_Player_Penalty_Stats> Game_Player_Penalty_Stats)
        {

            foreach (Game_Player_Stats s in Game_Player_Stats)
            {
                Game_Player_Stats ps = Game_Player_Stats.Where(x => x.Player_ID == s.Player_ID).FirstOrDefault();
                if (ps == null)
                    g.Game_Player_Stats.Add(s);
                else
                {
                    //Passing
                    ps.off_pass_plays += s.off_pass_plays;
                    ps.off_pass_fumbles += s.off_pass_fumbles;
                    ps.off_pass_Fumbles_Lost += s.off_pass_Fumbles_Lost;
                    ps.off_pass_Sacked += s.off_pass_Sacked;
                    ps.off_pass_Pressures += s.off_pass_Pressures;
                    ps.off_pass_Comp += s.off_pass_Comp;
                    ps.off_pass_Att += s.off_pass_Att;
                    ps.off_pass_Yards += s.off_pass_Yards;
                    ps.off_pass_TDs += s.off_pass_TDs;
                    ps.off_pass_Ints += s.off_pass_Ints;
                    if (ps.off_pass_Long < s.off_pass_Long) ps.off_pass_Long = s.off_pass_Long;
                    ps.off_pass_Passes_Blocked += s.off_pass_Passes_Blocked;

                    //Rushing
                    ps.off_rush_plays += s.off_rush_plays;
                    ps.off_rush_fumbles += s.off_rush_fumbles;
                    ps.off_rush_fumbles_lost += s.off_rush_fumbles_lost;
                    ps.off_rush_att += s.off_rush_att;
                    ps.off_rush_Yards += s.off_rush_Yards;
                    ps.off_rush_TDs += s.off_rush_TDs;
                    if (ps.off_rush_long < s.off_rush_long) ps.off_rush_long = s.off_rush_long;

                    //Receiving
                    ps.off_rec_plays += s.off_rec_plays;
                    ps.off_rec_fumbles += s.off_rec_fumbles;
                    ps.off_rec_fumbles_lost += s.off_rec_fumbles_lost;
                    ps.off_rec_catches += s.off_rec_catches;
                    ps.off_rec_drops += s.off_rec_drops;
                    ps.off_rec_Yards += s.off_rec_Yards;
                    ps.off_rec_TDs += s.off_rec_TDs;
                    if (ps.off_rec_long < s.off_rec_long) ps.off_rec_long = s.off_rec_long;

                    //Offensive Line
                    ps.off_line_plays += s.off_line_plays;
                    ps.off_line_sacks_allowed += s.off_line_sacks_allowed;
                    ps.off_line_Rushing_Loss_Allowed += s.off_line_Rushing_Loss_Allowed;
                    ps.off_line_missed_block += s.off_line_missed_block;
                    ps.off_line_pancakes += s.off_line_pancakes;
                    ps.off_line_was_pancaked += s.off_line_was_pancaked;
                    ps.off_line_QB_Pressures_Allowed += s.off_line_QB_Pressures_Allowed;

                    //Rush Defense
                    ps.def_rush_plays += s.def_rush_plays;
                    ps.def_rush_sacks += s.def_rush_sacks;
                    ps.def_rush_Rushing_Loss += s.def_rush_Rushing_Loss;
                    ps.def_rush_tackles += s.def_rush_tackles;
                    ps.def_rush_Missed_Tackles += s.def_rush_Missed_Tackles;
                    ps.def_rush_Safety += s.def_rush_Safety;
                    ps.def_rush_TDs += s.def_rush_TDs;
                    ps.def_rush_Forced_Fumbles += s.def_rush_Forced_Fumbles;
                    ps.def_rush_Recovered_Fumbles += s.def_rush_Recovered_Fumbles;
                    ps.def_rush_QB_Pressures += s.def_rush_QB_Pressures;
                    ps.def_rush_Pass_Blocks += s.def_rush_Pass_Blocks;
                    ps.def_rush_pancakes += s.def_rush_pancakes;
                    ps.def_rush_was_pancaked += s.def_rush_was_pancaked;

                    //Pass Defense
                    ps.def_pass_plays += s.def_pass_plays;
                    ps.def_pass_Ints += s.def_pass_Ints;
                    ps.def_pass_Int_Yards += s.def_pass_Int_Yards;
                    ps.def_pass_Int_TDs += s.def_pass_Int_TDs;
                    ps.def_pass_Pass_KnockedAway += s.def_pass_Pass_KnockedAway;
                    ps.def_pass_Tackles += s.def_pass_Tackles;
                    ps.def_pass_Missed_Tackles += s.def_pass_Missed_Tackles;
                    ps.def_pass_Forced_Fumbles += s.def_pass_Forced_Fumbles;
                    ps.def_pass_Recovered_Fumbles += s.def_pass_Recovered_Fumbles;
                    ps.def_pass_Recovered_Fumble_Yards += s.def_pass_Recovered_Fumble_Yards;
                    ps.def_pass_Recovered_Fumble_TDs += s.def_pass_Recovered_Fumble_TDs;

                    //Kicking
                    ps.kicker_plays += s.kicker_plays;
                    ps.XP_Att += s.XP_Att;
                    ps.XP_Made += s.XP_Made;
                    ps.FG_Att += s.FG_Att;
                    ps.FG_Made += s.FG_Made;
                    if (ps.FG_Long < s.FG_Long) ps.FG_Long = s.FG_Long;
                    ps.FG_Blocked += s.FG_Blocked;
                    ps.Kickoffs += s.Kickoffs;
                    ps.Kickoffs_Out_of_Bounds += s.Kickoffs_Out_of_Bounds;
                    ps.Kickoff_Touchbacks += s.Kickoff_Touchbacks;
                    ps.Kickoff_Thru_Endzones += s.Kickoff_Thru_Endzones;
                    ps.Kickoff_Onside_Att += s.Kickoff_Onside_Att;
                    ps.Kickoff_Onside_Succ += s.Kickoff_Onside_Succ;

                    //Kickoff Returns
                    ps.ko_ret_plays += s.ko_ret_plays;
                    ps.ko_ret += s.ko_ret;
                    ps.ko_ret_yards += s.ko_ret_yards;
                    ps.ko_ret_TDs += s.ko_ret_TDs;
                    if (ps.ko_ret_yards_long < s.ko_ret_yards_long) ps.ko_ret_yards_long = s.ko_ret_yards_long;
                    ps.ko_ret_touchbacks += s.ko_ret_touchbacks;
                    ps.ko_ret_Thru_Endzones += s.ko_ret_Thru_Endzones;
                    ps.ko_ret_fumbles += s.ko_ret_fumbles;
                    ps.ko_ret_fumbles_lost += s.ko_ret_fumbles_lost;

                    //Kickoff Defense
                    ps.ko_def_plays += s.ko_def_plays;
                    ps.ko_def_Forced_Fumbles += s.ko_def_Forced_Fumbles;
                    ps.ko_def_fumbles_recovered += s.ko_def_fumbles_recovered;
                    ps.ko_def_fumbles_recovered_TDs += s.ko_def_fumbles_recovered_TDs;
                    ps.ko_def_fumbles_recovered_Yards += s.ko_def_fumbles_recovered_Yards;
                    ps.ko_def_tackles += s.ko_def_tackles;
                    ps.ko_def_tackles_missed += s.ko_def_tackles_missed;
                    ps.ko_def_Onside_Kick_Recoveries += s.ko_def_Onside_Kick_Recoveries;

                    //Kickoff Receivers
                    ps.ko_rec_plays += s.ko_rec_plays;
                    ps.ko_rec_Onside_Recovery += s.ko_rec_Onside_Recovery;
                    ps.ko_rec_fumbles_recovered += s.ko_rec_fumbles_recovered;

                    //FG Defense
                    ps.fg_def_plays += s.fg_def_plays;
                    ps.fg_def_block += s.fg_def_block;
                    ps.fg_def_block_recovered += s.fg_def_block_recovered;
                    ps.fg_def_block_recovered_yards += s.fg_def_block_recovered_yards;
                    ps.fg_def_block_recovered_TDs += s.fg_def_block_recovered_TDs;
                    ps.XP_Block += s.XP_Block;

                    //Punter
                    ps.punter_plays += s.punter_plays;
                    ps.punter_Fumbles += s.punter_Fumbles;
                    ps.punter_Fumbles_lost += s.punter_Fumbles_lost;
                    ps.punter_Fumbles_recovered += s.punter_Fumbles_recovered;
                    ps.punter_punts += s.punter_punts;
                    ps.punter_punt_yards += s.punter_punt_yards;
                    ps.punter_kill_att += s.punter_kill_att;
                    ps.punter_kill_Succ += s.punter_kill_Succ;
                    ps.punter_blocks += s.punter_blocks;

                    //Punt Returns
                    ps.punt_ret_plays += s.punt_ret_plays;
                    ps.punt_ret += s.punt_ret;
                    ps.punt_ret_yards += s.punt_ret_yards;
                    ps.punt_ret_TDs += s.punt_ret_TDs;
                    if (ps.punt_ret_yards_long < s.punt_ret_yards_long) ps.punt_ret_yards_long = s.punt_ret_yards_long;
                    ps.punt_ret_fumbles += s.punt_ret_fumbles;
                    ps.punt_ret_fumbles_lost += s.punt_ret_fumbles_lost;

                    //Punt Reveivers
                    ps.punt_rec_plays += s.punt_rec_plays;
                    ps.punt_rec_blocks += s.punt_rec_blocks;
                    ps.punt_rec_block_recovery += s.punt_rec_block_recovery;
                    ps.punt_rec_block_recovery_yards += s.punt_rec_block_recovery_yards;
                    ps.punt_rec_block_recovery_TDs += s.punt_rec_block_recovery_TDs;

                    //Punt Defense
                    ps.punt_def_plays += s.punt_def_plays;
                    ps.punt_def_forced_fumbles += s.punt_def_forced_fumbles;
                    ps.punt_def_forced_fumbles_recovered += s.punt_def_forced_fumbles_recovered;
                    ps.punt_def_forced_fumbles_recovered_TDs += s.punt_def_forced_fumbles_recovered_TDs;
                    ps.punt_def_forced_fumbles_recovered_Yards += s.punt_def_forced_fumbles_recovered_Yards;
                    ps.punt_def_tackles += s.punt_def_tackles;
                    ps.punt_def_tackles_missed += s.punt_def_tackles_missed;
                }
            }

            foreach (Game_Player_Penalty_Stats s in Game_Player_Penalty_Stats)
                g.Game_Player_Penalty_Stats.Add(s);

        }

        //This method will update the play_result object and return other values to
        //indicate the final result of the play taking into account the penalty
        //The next yardline will be set to 0 in the case the next play is a kickoff, freekick or extra point,
        //That is because the executeplay method sets the yardline for that.  
        //special note: final ending yardline is for this play and not always the starting yardline
        //of the next play
        public static Play_Result setPlayOutCome(bool penOnBallCarryingTeam,
            Play_Enum PE, int Down, double Yards_to_Go, Play_Result pResult,
            double original_Yardline, bool bLefttoRgiht, double TouchBack_Yardline)
        {
            Play_Result r = pResult;
            bool bTurnover = r.bInterception || r.bFumble_Lost ? true : false;
            double dist_from_GL;
            double Penalty_Yards;
            bool bHalft_the_dist = false;

            //Determine outcome by play type
            switch (PE)
            {
                case Play_Enum.KICKOFF_NORMAL:
                case Play_Enum.FREE_KICK:
                case Play_Enum.KICKOFF_ONSIDES:

                    r.bPlay_Stands = true;
                    r.bFinal_SwitchPossession = true;

                    if (bTurnover)
                    {
                        r.bFinal_SwitchPossession = !r.bFinal_SwitchPossession;
                        setTurnoverGameStat(r);
                    }

                    //No penalties or turnoers on touchbacks
                    if (r.bTouchback && r.Penalty == null) 
                    {
                        r.Final_Down = 1;
                        r.Final_yard_to_go = 10;
                        r.Final_end_of_Play_Yardline = getScrimmageLine(TouchBack_Yardline, bLefttoRgiht);
                    }
                    else if (r.Penalty == null || r.bPenalty_Rejected || (r.Penalty.bSpot_Foul && !penOnBallCarryingTeam))
                    {
                        r = setScoringBool(r, bTurnover);

                        if (r.bTouchDown)
                            r.bFinal_SwitchPossession = false;

                        if (r.Penalty != null && !r.bPenalty_Rejected)
                        {
                            bool bIgnorePeanlty = IgnorePenalty(r);

                            if (bIgnorePeanlty)
                            {
                                r.bIgnorePenalty = true;
                                r.Final_Added_Penalty_Yards = 0.0;
                            }
                            else
                            {
                                double greater_yl = Game_Engine_Helper.GreaterYardline(r.end_of_play_yardline, r.Penalized_Player.Current_YardLine, bLefttoRgiht);
                                dist_from_GL = Game_Engine_Helper.calcDistanceFromOpponentGL(greater_yl, bLefttoRgiht);
                                Tuple<bool, double> t = Penalty_Helper.isHalfTheDistance(r.Penalty.Yards, dist_from_GL);
                                if (t.Item1)
                                    r.Final_Added_Penalty_Yards = t.Item2;
                                else
                                    r.Final_Added_Penalty_Yards = r.Penalty.Yards;

                                r.Final_end_of_Play_Yardline = greater_yl + (r.Final_Added_Penalty_Yards * Game_Engine_Helper.HorizontalAdj(bLefttoRgiht));
                            }
                        }
                        else
                        {
                            dist_from_GL = Game_Engine_Helper.calcDistanceFromOpponentGL(r.end_of_play_yardline, bLefttoRgiht);
                            r.Final_end_of_Play_Yardline = r.end_of_play_yardline;
                        }

                        //set possible next mandatory play
                        bool bnextSpecial = setNextPLay(r);

                        if (bnextSpecial)
                        {
                            r.Final_Down = 0;
                            r.Final_yard_to_go = 0;
                        }
                        else
                        {
                            r.Final_Down = 1;
                            r.Final_yard_to_go = 10;
                        }
                    }
                    else if ((r.Penalty.bSpot_Foul && penOnBallCarryingTeam))
                    {
                        double lower_yl = Game_Engine_Helper.LessYardline(r.end_of_play_yardline, r.Penalized_Player.Current_YardLine, bLefttoRgiht);
                        dist_from_GL = Game_Engine_Helper.calcDistanceFromMyGL(lower_yl, bLefttoRgiht);
                        Tuple<bool, double> t = Penalty_Helper.isHalfTheDistance(r.Penalty.Yards, dist_from_GL);
                        if (t.Item1)
                            r.Final_Added_Penalty_Yards = t.Item2;
                        else
                            r.Final_Added_Penalty_Yards = r.Penalty.Yards;

                        r.Final_end_of_Play_Yardline = lower_yl - (r.Final_Added_Penalty_Yards * Game_Engine_Helper.HorizontalAdj(bLefttoRgiht));
                        r.Final_Down = 1;
                        r.Final_yard_to_go = 10;

                        //Not for onside kick, If need be reset returner stats because penalty may have altered them
                        if (PE != Play_Enum.KICKOFF_ONSIDES)
                        {
                            double reduce_yards = Game_Engine_Helper.Yards_to_Reduce(r.end_of_play_yardline, r.Penalized_Player.Current_YardLine, bLefttoRgiht);
                            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == r.Returner.p_and_r.p.ID).First();
                            a_stat.ko_ret_TDs = 0;
                            a_stat.ko_ret_yards -= (long)(reduce_yards + 0.5);
                            a_stat.ko_ret_yards_long = a_stat.ko_ret_yards;
                        }
                    }
                    else
                        throw new Exception("Error in setPlayOutCome, non spot penalty on kickoffs!");

                    break;
                case Play_Enum.FIELD_GOAL:
                    if (r.Penalty == null || r.bPenalty_Rejected)
                    {
                        r.bPlay_Stands = true;
                        r = setScoringBool(r, bTurnover);
                        if (r.bFGMade)
                        {
                            r.bFinal_NextPlayKickoff = true; 
                            r.Final_Down = 0;
                            r.Final_yard_to_go = 0;
                        }
                        else
                        {
                            r.bFinal_SwitchPossession = true;
                            r.Final_Down = 1;
                            r.Final_yard_to_go = 10;
                            r.Final_end_of_Play_Yardline = r.end_of_play_yardline;
                        }
                    }
                    else if (!penOnBallCarryingTeam)
                    {
                        setDefenseNonSpotPenalty(r, bLefttoRgiht, Down, Yards_to_Go);
                    }
                    else if (penOnBallCarryingTeam)
                    {
                        setOffensePenaltyNonSpot(r, bLefttoRgiht, Down, Yards_to_Go);
                    }
                    else
                        throw new Exception("Error in setPlayOutCome, FG unknown situation!");

                    break;
                case Play_Enum.EXTRA_POINT:
                case Play_Enum.SCRIM_PLAY_1XP_RUN:
                case Play_Enum.SCRIM_PLAY_1XP_PASS:
                case Play_Enum.SCRIM_PLAY_2XP_RUN:
                case Play_Enum.SCRIM_PLAY_2XP_PASS:
                case Play_Enum.SCRIM_PLAY_3XP_RUN:
                case Play_Enum.SCRIM_PLAY_3XP_PASS:
                    if (r.Penalty == null || r.bPenalty_Rejected)
                    {
                        r.bPlay_Stands = true;
                        r = setScoringBool(r, bTurnover);

                        r.bFinal_NextPlayKickoff = true; 
                        r.Final_Down = 0;
                        r.Final_yard_to_go = 0;

                    }
                    else if (!penOnBallCarryingTeam)
                    {
                        bool bIgnorePeanlty = IgnorePenalty(r);

                        if (bIgnorePeanlty)
                        {
                            r.bIgnorePenalty = true;
                            r.Final_Added_Penalty_Yards = 0.0;
                        }
                        else
                        {
                            dist_from_GL = Game_Engine_Helper.calcDistanceFromOpponentGL(r.end_of_play_yardline, bLefttoRgiht);
                            Tuple<bool, double> t = Penalty_Helper.isHalfTheDistance(r.Penalty.Yards, dist_from_GL);
                            if (t.Item1)
                                Penalty_Yards = t.Item2;
                            else
                                Penalty_Yards = pResult.Penalty.Yards;

                            r.Final_Down = 0;
                            r.Final_yard_to_go = 0;

                            r.Final_end_of_Play_Yardline += (r.Yards_Gained + Penalty_Yards) * Game_Engine_Helper.HorizontalAdj(bLefttoRgiht);
                            r.Final_Added_Penalty_Yards = Penalty_Yards;
                        }
                    }
                    else if (penOnBallCarryingTeam)
                    {
                        dist_from_GL = Game_Engine_Helper.calcDistanceFromMyGL(r.end_of_play_yardline, bLefttoRgiht);
                        Tuple<bool, double> t = Penalty_Helper.isHalfTheDistance(r.Penalty.Yards, dist_from_GL);
                        if (t.Item1)
                            Penalty_Yards = t.Item2;
                        else
                            Penalty_Yards = pResult.Penalty.Yards;
                        r.Final_Down = 0;
                        r.Final_yard_to_go = 0;
                        r.Final_end_of_Play_Yardline += (r.Yards_Gained + Penalty_Yards) * Game_Engine_Helper.HorizontalAdj(bLefttoRgiht);
                        r.Final_Added_Penalty_Yards = Penalty_Yards;
                    }
                    else
                        throw new Exception("Error in setPlayOutCome, FG unknown situation!");

                    break;
//Spot fouls assessed after the ball is kicked, all other penalties before the kick
                case Play_Enum.PUNT:
                    //No penalties or turnoers on touchbacks or out of bounds
                    if ((r.bTouchback || r.bRunOutofBounds))
                    {
                        r.bPlay_Stands = true;
                        r.Final_Down = 1;
                        r.Final_yard_to_go = 10;
                    }
                    else if (r.Penalty == null || r.bPenalty_Rejected)
                    {
                        r.bPlay_Stands = true;

                        //set possible next mandatory play
                        bool bnextSpecial = setNextPLay(r);

                        if (bnextSpecial)
                        {
                            r.Final_Down = 0;
                            r.Final_yard_to_go = 0;
                        }
                        else
                        {
                            r.Final_Down = 1;
                            r.Final_yard_to_go = 10;
                            r.Final_end_of_Play_Yardline = r.end_of_play_yardline;
                        }

                        if (bTurnover)
                        {
                            r.bFinal_SwitchPossession = true;
                            setTurnoverGameStat(r);
                        }

                    }
                    else if (r.Penalty.bSpot_Foul && !penOnBallCarryingTeam)
                    {
                        bool bIgnorePeanlty = IgnorePenalty(r);

                        if (bIgnorePeanlty)
                        {
                            r.bIgnorePenalty = true;
                            r.Final_Added_Penalty_Yards = 0.0;
                        }
                        else
                        {
                            dist_from_GL = Game_Engine_Helper.calcDistanceFromOpponentGL(original_Yardline, bLefttoRgiht);
                            Tuple<bool, double> t = Penalty_Helper.isHalfTheDistance(pResult.Penalty.Yards, dist_from_GL);
                            if (t.Item1)
                                Penalty_Yards = t.Item2;
                            else
                                Penalty_Yards = pResult.Penalty.Yards;

                            r.Final_Down = 1;
                            r.Final_yard_to_go = 10;

                            r.Final_Added_Penalty_Yards = Penalty_Yards;
                            r.end_of_play_yardline += Penalty_Yards * Game_Engine_Helper.HorizontalAdj(bLefttoRgiht);
                            r.Final_end_of_Play_Yardline = r.end_of_play_yardline;
                        }
                    }
                    else if (r.Penalty.bSpot_Foul && penOnBallCarryingTeam)
                    {
                        dist_from_GL = Game_Engine_Helper.calcDistanceFromMyGL(original_Yardline, bLefttoRgiht);
                        Tuple<bool, double> t = Penalty_Helper.isHalfTheDistance(pResult.Penalty.Yards, dist_from_GL);
                        if (t.Item1)
                            Penalty_Yards = t.Item2;
                        else
                            Penalty_Yards = pResult.Penalty.Yards;

                        r.Final_Down = 1;
                        r.Final_yard_to_go = 10;

                        r.Final_Added_Penalty_Yards = Penalty_Yards;
                        r.end_of_play_yardline -= Penalty_Yards * Game_Engine_Helper.HorizontalAdj(bLefttoRgiht);
                        r.Final_end_of_Play_Yardline = r.end_of_play_yardline;
                    }
                    else if (!penOnBallCarryingTeam)
                    {
                        setDefenseNonSpotPenalty(r, bLefttoRgiht, Down, Yards_to_Go);
                    }
                    else if (penOnBallCarryingTeam)
                    {
                        setOffensePenaltyNonSpot(r, bLefttoRgiht, Down, Yards_to_Go);
                    }
                    break;
                case Play_Enum.RUN:
                case Play_Enum.PASS:
                    bool bAutoFirstDown = false;
                    bool bFirstDown = false;
                    if (pResult.Penalty == null || pResult.bPenalty_Rejected || (pResult.Penalty.bSpot_Foul && !penOnBallCarryingTeam))
                    {
                        r.bPlay_Stands = true;
                        r.bFinal_SwitchPossession = true;
                        r = setScoringBool(r, bTurnover);
                        dist_from_GL = Game_Engine_Helper.calcDistanceFromOpponentGL(r.end_of_play_yardline, bLefttoRgiht);

                        if (r.Penalty != null && !r.bPenalty_Rejected)
                        {
                            bool bIgnorePeanlty = IgnorePenalty(r);

                            if (bIgnorePeanlty)
                            {
                                r.bIgnorePenalty = true;
                                r.Final_Added_Penalty_Yards = 0.0;
                            }
                            else
                            {
                                Tuple<bool, double> t = Penalty_Helper.isHalfTheDistance(r.Penalty.Yards, dist_from_GL);
                                if (t.Item1)
                                    r.Final_Added_Penalty_Yards = t.Item2;
                                else
                                    r.Final_Added_Penalty_Yards = r.Penalty.Yards;

                                bAutoFirstDown = Penalty_Helper.isFirstDowwithPenalty(r.Penalty, Yards_to_Go, bHalft_the_dist, r.Final_Added_Penalty_Yards);
                            }
                        }

                        //set possible next mandatory play
                        bool bnextSpecial = setNextPLay(r);

                        if (bnextSpecial)
                        {
                            r.Final_Down = 0;
                            r.Final_yard_to_go = 0;
                        }
                        else
                        {
                            if (bAutoFirstDown || (pResult.Yards_Gained + r.Final_Added_Penalty_Yards) >= Yards_to_Go)
                            {
                                bFirstDown = true;
                                r.Final_Down = 1;
                                r.Final_yard_to_go = 10;
                                setFirstDownStat(r);
                            }
                            else if (Down == 4)
                            {
                                r.Final_Down = 1;
                                r.Final_yard_to_go = 10;
                                r.bFinal_SwitchPossession = true;
                            }
                            else
                            {
                                r.Final_Down = Down + 1;
                                r.Final_yard_to_go = Yards_to_Go - pResult.Yards_Gained + r.Final_Added_Penalty_Yards;
                            }

                        }

                        if (Down == 3)
                            setThirdDownStat(r, bFirstDown);
                         else if (Down == 4)
                            setFourthDownStat(r, bFirstDown);

                        if (bTurnover)
                        {
                            r.bFinal_SwitchPossession = true;
                            setTurnoverGameStat(r);
                            if (r.bTouchback)
                                r.Final_end_of_Play_Yardline = getScrimmageLine(TouchBack_Yardline, !bLefttoRgiht);
                        }

                    }
                    else if (!penOnBallCarryingTeam)
                    {
                        setDefenseNonSpotPenalty(r, bLefttoRgiht, Down, Yards_to_Go);
                    }
                    else if (penOnBallCarryingTeam)
                    {
                        setOffensePenaltyNonSpot(r, bLefttoRgiht, Down, Yards_to_Go);
                    }
                    break;

            }

            return r;
            }
            public static void UpdateScore(Play_Result pResult, Game g)
            {
                //Scoring
                const long TOUCHDOWN_POINTS = 6;
                const long FIELDGOAL_POINTS = 3;
                const long SAFETY_POINTS = 2;
                const long EXTRA_POINT_POINTS = 1;
                const long EXTRA_POINT_1_POINTS = 1;
                const long EXTRA_POINT_2_POINTS = 2;
                const long EXTRA_POINT_3_POINTS = 3;

            long Away_Score_Add = 0;
            long Home_Score_Add = 0;

            //TDs
            if (pResult.bAwayTD)
                Away_Score_Add = TOUCHDOWN_POINTS;

            if (pResult.bHomeTD)
                Home_Score_Add = TOUCHDOWN_POINTS;

            //FGs
            if (pResult.bAwayFG)
                Away_Score_Add = FIELDGOAL_POINTS;

            if (pResult.bHomeFG)
                Home_Score_Add = FIELDGOAL_POINTS;

            //Safety
            if (pResult.bAwaySafetyFor)
                Away_Score_Add = SAFETY_POINTS;

            if (pResult.bHomeSafetyFor)
                Home_Score_Add = SAFETY_POINTS;

            //XP
            if (pResult.bAwayXP)
                Away_Score_Add = EXTRA_POINT_POINTS;

            if (pResult.bHomeXP)
                Home_Score_Add = EXTRA_POINT_POINTS;

            //XP1
            if (pResult.bAwayXP1)
                Away_Score_Add = EXTRA_POINT_1_POINTS;

            if (pResult.bHomeXP1)
                Home_Score_Add = EXTRA_POINT_1_POINTS;

            //XP2
            if (pResult.bAwayXP2)
                Away_Score_Add = EXTRA_POINT_2_POINTS;

            if (pResult.bHomeXP2)
                Home_Score_Add = EXTRA_POINT_2_POINTS;

            //XP3
            if (pResult.bAwayXP3)
                Away_Score_Add = EXTRA_POINT_3_POINTS;

            if (pResult.bHomeXP3)
                Home_Score_Add = EXTRA_POINT_3_POINTS;

            g.Away_Score += Away_Score_Add;
            g.Home_Score += Home_Score_Add;

            switch (g.Quarter)
                {
                    case 1:
                        g.Away_Score_Q1 += Away_Score_Add;
                        g.Home_Score_Q1 += Home_Score_Add;
                        break;
                    case 2:
                        g.Away_Score_Q2 += Away_Score_Add;
                        g.Home_Score_Q2 += Home_Score_Add;
                        break;
                    case 3:
                        g.Away_Score_Q3 += Away_Score_Add;
                        g.Home_Score_Q3 += Home_Score_Add;
                        break;
                    case 4:
                        g.Away_Score_Q4 += Away_Score_Add;
                        g.Home_Score_Q4 += Home_Score_Add;
                        break;
                    default:
                        g.Away_Score_OT += Away_Score_Add;
                        g.Home_Score_OT += Home_Score_Add;
                        break;
                }
        }

        //This method returns a true if the next play dows not have a down or ytg
        private static bool setNextPLay(Play_Result pResult)
        {
            bool r = true;

            if (pResult.bTouchDown)
                pResult.bFinal_NextPlayXP = true;
            else if (pResult.bSafety)
                pResult.bFinal_NextPlayFreeKick = true;
            else if (pResult.bFGMade || pResult.bFGMissed || pResult.bXPMade || pResult.bXPMissed ||
                pResult.bOnePntAfterTDMade || pResult.bOnePntAfterTDMissed || pResult.bTwoPntAfterTDMade ||
                pResult.bTwoPntAfterTDMissed || pResult.bThreePntAfterTDMade || pResult.bThreePntAfterTDMissed)
                pResult.bFinal_NextPlayKickoff = true;
            else
                r = false;

            return r;
        }
        private static void setFirstDownStat(Play_Result pR)
        {
            if (pR.BallPossessing_Team_Id == pR.at)
                pR.AwayFirstDowns++;
            else
                pR.HomeFirstDowns++;
        }

        private static void setThirdDownStat(Play_Result pR, bool b3rdMade)
        {
            if (pR.BallPossessing_Team_Id == pR.at)
            {
                pR.Away3rdDownAtt++;
                if (b3rdMade) pR.Away3rdDownMade++;
            }
            else
            {
                pR.Home3rdDownAtt++;
                if (b3rdMade) pR.Home3rdDownMade++;
            }
        }

        private static void setFourthDownStat(Play_Result pR, bool b3rdMade)
        {
            if (pR.BallPossessing_Team_Id == pR.at)
            {
                pR.Away4thDownAtt++;
                if (b3rdMade) pR.Away4thDownMade++;
            }
            else
            {
                pR.Home4thDownAtt++;
                if (b3rdMade) pR.Home4thDownMade++;
            }
        }

        private static void setTurnoverGameStat(Play_Result pR)
        {
            if (pR.BallPossessing_Team_Id == pR.at)
                pR.AwayTurnoers++;
            else
                pR.HomeTurnoers++;
        }

        private static void setDefenseNonSpotPenalty(Play_Result r, bool bLefttoRgiht,int Down, double Yards_to_Go)
        {
            bool bHalft_the_dist = false;
            double Penalty_Yards;

            bool bIgnorePeanlty = IgnorePenalty(r);

            if (bIgnorePeanlty)
            {
                r.bIgnorePenalty = true;
                r.Final_Added_Penalty_Yards = 0.0;
            }
            else
            {
                double dist_from_GL = Game_Engine_Helper.calcDistanceFromOpponentGL(r.end_of_play_yardline, bLefttoRgiht);
                Tuple<bool, double> t = Penalty_Helper.isHalfTheDistance(r.Penalty.Yards, dist_from_GL);
                bHalft_the_dist = t.Item1;
                if (t.Item1)
                    Penalty_Yards = t.Item2;
                else
                    Penalty_Yards = r.Penalty.Yards;
                bool bFirstDown = Penalty_Helper.isFirstDowwithPenalty(r.Penalty, Yards_to_Go, bHalft_the_dist, Penalty_Yards);

                if (bFirstDown)
                {
                    r.Final_Down = 1;
                    r.Final_yard_to_go = 10;
                    setFirstDownStat(r);
                }
                else
                {
                    r.Final_Down = Down;
                    r.Final_yard_to_go = Yards_to_Go - Penalty_Yards;
                }
                r.Final_end_of_Play_Yardline = r.end_of_play_yardline + (Penalty_Yards * Game_Engine_Helper.HorizontalAdj(bLefttoRgiht));
                r.Final_Added_Penalty_Yards = Penalty_Yards;
            }
        }
        private static void setOffensePenaltyNonSpot(Play_Result r, bool bLefttoRgiht, int Down, double Yards_to_Go)
        {
            double Penalty_Yards;
             double dist_from_GL = Game_Engine_Helper.calcDistanceFromMyGL(r.end_of_play_yardline, bLefttoRgiht);
            Tuple<bool, double> t = Penalty_Helper.isHalfTheDistance(r.Penalty.Yards, dist_from_GL);
            if (t.Item1)
                Penalty_Yards = t.Item2;
            else
                Penalty_Yards = r.Penalty.Yards;

            r.Final_Down = Down;
            r.Final_yard_to_go = Yards_to_Go + Penalty_Yards;
            r.Final_end_of_Play_Yardline = r.end_of_play_yardline - (Penalty_Yards * Game_Engine_Helper.HorizontalAdj(bLefttoRgiht));
            r.Final_Added_Penalty_Yards = Penalty_Yards;
        }
        public static long CoinToss(int rnum, long ht_id, long at_id)
        {
            long r = 0;

            if (rnum <= 50)
                r = ht_id;
            else
                r = at_id;

            return r;
        }
        public static bool isPlayWithReturner(Play_Enum p_enum)
        {
            bool r = false;
            if (p_enum == Play_Enum.KICKOFF_NORMAL ||
                p_enum == Play_Enum.PUNT ||
                p_enum == Play_Enum.FREE_KICK)
                r = true;

            return r;
        }
        public static long Switch_Posession(long Current_possession, long at, long ht)
        {
            long r = Current_possession;

            if (Current_possession == at)
                r = ht;
            else
                r = at;

            return r;
        }
        public static bool isBallTeamPenalty(Play_Result pResult)
        {
            bool r = false;

            Game_Player p = pResult.Penalized_Player;

            if (pResult.Passer == p || pResult.Pass_Catchers.Contains(p) || pResult.Ball_Runners.Contains(p) ||
                pResult.Pass_Blockers.Contains(p))
                r = true;
            else if (pResult.Returner == p || pResult.Punt_Returner == p || pResult.Kick_Returners.Contains(p) ||
                pResult.Punt_Returners.Contains(p) || pResult.FieldGaol_Kicking_Team.Contains(p))
                r = true;
            else if (pResult.Pass_Rushers.Contains(p) || pResult.Pass_Defenders.Contains(p) ||
                pResult.Run_Defenders.Contains(p))
                r = false;
            else if (pResult.Kicker == p || pResult.Punter == p || pResult.Kick_Defenders.Contains(p) ||
                pResult.Punt_Defenders.Contains(p) || pResult.Field_Goal_Defenders.Contains(p))
                r = false;
            else
                throw new Exception("isBallTeamPenalty error can't determine penalty team!");
            return r;
        }
        public static bool isGameEnd(long Playoffs, long Quarter, long Time, long Away_Score, long Home_Score)
        {
            bool r = false;

            if (Quarter == 4 && Time == 0 && Away_Score != Home_Score)
                r = true;
            else if (Quarter > 4 && Away_Score != Home_Score)
                r = true;
            else if (Playoffs == 0 && Quarter == 5 && Time == 0)
                r = true;
            else
                r = false;

            return r;
        }
        public static double getScrimmageLine(double y, bool bLefttoRight)
        {
            double yardLine = y;

            if (!bLefttoRight)
                yardLine = 100 - y;

            return yardLine;
        }
        public static Play_Result setScoringBool(Play_Result r, bool bTurnover)
        {
            Play_Result pResult = r;

            if (pResult.BallPossessing_Team_Id == pResult.at)
            {
                if (bTurnover)
                    pResult.bHomeTD = pResult.bTouchDown;
                else
                    pResult.bAwayTD = pResult.bTouchDown;

                pResult.bAwayFG = pResult.bFGMade;
                pResult.bAwayXP = pResult.bXPMade;
                pResult.bAwayXP1 = pResult.bOnePntAfterTDMade;
                pResult.bAwayXP2 = pResult.bTwoPntAfterTDMade;
                pResult.bAwayXP3 = pResult.bThreePntAfterTDMade;

                pResult.bHomeSafetyFor = pResult.bSafety;
            }
            else
            {
                if (bTurnover)
                    pResult.bAwayTD = pResult.bTouchDown;
                else
                    pResult.bHomeTD = pResult.bTouchDown;

                pResult.bHomeFG = pResult.bFGMade;
                pResult.bHomeXP = pResult.bXPMade;
                pResult.bHomeXP1 = pResult.bOnePntAfterTDMade;
                pResult.bHomeXP2 = pResult.bTwoPntAfterTDMade;
                pResult.bHomeXP3 = pResult.bThreePntAfterTDMade;

                pResult.bAwaySafetyFor = pResult.bSafety;
            }

            return r;
        }

        private static bool IgnorePenalty(Play_Result pResult)
        {
            bool r = false;

            if (pResult.bTouchDown || pResult.bFGMade || pResult.bXPMade ||
                pResult.bOnePntAfterTDMade || pResult.bTwoPntAfterTDMade || pResult.bThreePntAfterTDMade)
                r = true;

            return r;
        }

    }
}
