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

        private List<Penalty> Penalty_List = null;

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

            //Get all Penalties
            Penalty_List = pw.Loaded_League.PenaltiesData;

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
                g_fid_posession = Game_Engine_Helper.Switch_Posession(g_fid_posession, at.Franchise_ID, ht.Franchise_ID);
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

            Down_and_Yards = Game_Helper.getDownAndYardString(g_Down, g_Yards_to_go, g_Vertical_Ball_Placement, bLefttoRight);
            r.Before_Down_and_Yards = Down_and_Yards;
            r.Before_Away_Timeouts = g_Away_timeouts.ToString();
            r.Before_Home_Timeouts = g_Home_timeouts.ToString();

            r.Before_Display_QTR = Game_Helper.getQTRString((long)g.Quarter) + " QTR";
            r.Before_Display_Time = Game_Helper.getTimestringFromSeconds((long)g.Time);
            //=================================================================

            bool bPlayWithReturner = Game_Engine_Helper.isPlayWithReturner(Offensive_Package.Play);

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

                r.Long_Message = getForfeit_Message(at, ht, (long)g.Away_Score, (long)g.Home_Score);
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
                    p_result = Play_Kickoff_Normal.Execute(Game_Ball, Offensive_Players, Defensive_Players, bLefttoRight, false, bSimGame, false);
                logger.Debug("AFter  kickoff play");

                int ball_stages = Game_Ball.Stages.Count();
                for(int pind = 0; pind < Offensive_Players.Count(); pind++)
                {
                    if (Offensive_Players[pind].Stages.Count() != ball_stages ||
                        Defensive_Players[pind].Stages.Count() != ball_stages)
                        throw new Exception("Number of stages do not match between ball, offensive and defensive players");
                }

                //set results and accume team stats
                yards_gained = p_result.Yards_Gained;

                //                g.Away_Score += p_result.away_points;
                //                g.Home_Score += p_result.home_points;

                /*                switch (g.Quarter)
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
                */

                if (bAllowPenalties && Penalty_Helper.isNoPenaltyPlay(p_result, Offensive_Package.Play))
                {
                    p_result.Penalized_Player = Penalty_Helper.getPenaltyPlayer(Offensive_Package.Play, Offensive_Players, Defensive_Players, p_result.Passer, p_result.Kicker, p_result.Punter);
                    if (p_result.Penalized_Player != null)
                    {
                        Player_Action_Stats pa_state = Penalty_Helper.getPlayerAction(p_result.Penalized_Player, p_result);
                        p_result.Penalty = Penalty_Helper.getPenalty(Penalty_List, Offensive_Package.Play, pa_state);

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

                        if (p_result.Penalty.bDeclinable)
                        {
                            if ((bAway_Pen_Player && bLefttoRight) || bHome_Pen_Player && !bLefttoRight)
                                p_result.bPenalty_Accepted = Penalty_Coach.AcceptOff_Penalty(p_result, Offensive_Package.Play, p_result.Penalty, g_Down, g_Yards_to_go);
                            else
                                p_result.bPenalty_Accepted = Penalty_Coach.AcceptDef_Penalty(p_result, g_Down, g_Yards_to_go);
                        }
                    }
                    //for some reason, I put adding the penalties in the accum method.  Take that out of there and add here.
                    //only accum the penalty if it is accepted but still set it in the play result.

                }


                //acume individual stats
                Accume_Play_Stats(p_result.Play_Player_Stats,
                     p_result.Play_Player_Penalty_Stats);
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
                if (p_result.bFumble_Lost || p_result.bInterception)
                    g_fid_posession = Game_Engine_Helper.Switch_Posession(g_fid_posession, at.Franchise_ID, ht.Franchise_ID);

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

    }
}
