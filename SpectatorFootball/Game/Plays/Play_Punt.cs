using log4net;
using SpectatorFootball.Enum;
using SpectatorFootball.GameNS;
using SpectatorFootball.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.GameNS
{
    public class Play_Punt : iPlay
    {
        private static ILog logger = LogManager.GetLogger("RollingFile");

        private long Possessing_Team_Id;
        private long at;
        private long ht;
        private Game_Ball gBall;
        private List<Game_Player> Punt_Players;
        private List<Game_Player> Return_Players;
        private bool bLefttoRight;
        private bool FreeKic;
        private bool bSim;
        private bool bLast_Play;
        private Formation Punt_Formation = null;
        private Formation Return_Formation = null;


        public Play_Result r = new Play_Result();

        Play_Enum Play { get; set; } = Play_Enum.PUNT;

        Play_Enum iPlay.Play { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }        private Formation Kickoff_Formation = null;
        public Play_Punt(Formation Punt_Formation, Formation Return_Formation, long Possessing_Team_Id, long at, long ht, Game_Ball gBall, List<Game_Player> Punt_Players, List<Game_Player> Return_Players, bool bLefttoRight, bool bSim, bool bLast_Play)
        {
            this.Possessing_Team_Id = Possessing_Team_Id;
            this.at = at;
            this.ht = ht;
            this.gBall = gBall;
            this.Punt_Players = Punt_Players;
            this.Return_Players = Return_Players;
            this.bLefttoRight = bLefttoRight;
            this.bSim = bSim;
            this.bLast_Play = bLast_Play;
            this.Punt_Formation = Punt_Formation;
            this.Return_Formation = Return_Formation;

            r.BallPossessing_Team_Id = Possessing_Team_Id == at ? ht : at;
            r.NonbBallPossessing_Team_Id = Possessing_Team_Id == at ? at : ht;
            r.at = at;
            r.ht = ht;
            r = setPlayerActions(Punt_Formation, Return_Formation, Punt_Players, Return_Players, r);
        }

        public bool isPreSnapPenalty_Eligible()
        {
            return true;
        }

        public bool isAccumeStats()
        {
            return true;
        }
        public Play_Result getPlayResult()
        {
            return r;
        }

        public Play_Result Execute(bool bPreSnapPenalty)
        {
            List<string> Play_Stages = new List<string>();
            List<Game_Player> Missed_Tackles = new List<Game_Player>();
            double retuner_catches_ball_yl = 0.0;
            double first_block_dropback_yards = 3.0;
            //================================  Stage One =======================================
            //Players get ready for play 
            logger.Debug("Stage 1");
            logger.Debug("=====================================================");
            logger.Debug(this.bLefttoRight);

            if (!bSim)
                gBall.TeeUp();

            int io_Players = 0;
            foreach (Game_Player p in Punt_Players)
            {
                bool bMain = true;
                double prev_yl = p.Current_YardLine;
                double prev_v = p.Current_Vertical_Percent_Pos;

                if (!bSim)
                {
                    if (Punt_Formation.Line_Players.Contains(io_Players))
                    {
                        p.Crouch(prev_yl, prev_v, bMain);
                        bMain = false;
                    }
                    else
                        p.Stand();
                }
                io_Players++;
            }

            io_Players = 0;
            //The team receiving the kick will just stand there before the kick
            foreach (Game_Player p in Return_Players)
            {
                if (!bSim)
                {
                    double prev_yl = p.Current_YardLine;
                    double prev_v = p.Current_Vertical_Percent_Pos;
                    if (Return_Formation.Line_Players.Contains(io_Players))
                        p.Crouch(prev_yl, prev_v, false);
                    else
                        p.Stand();
                }

                io_Players++;
            }
            //===== End of Stage One - Players get ready for play ================
            logger.Debug("=======================================================");
            logger.Debug("");

            //================================  Stage Two =======================================
            //if presnap penatly then players stand up; otherwise snap ball back to punter
            logger.Debug("Stage 2");
            logger.Debug("=====================================================");
            if (bPreSnapPenalty)
            {
                Playstub_Uncrouch.Execute(bLefttoRight, gBall, Punt_Players, Return_Players, bSim);
            }

            //Snap ball back to punter, lines clash
            if (!bPreSnapPenalty)
            {
                double ball_yl = gBall.Current_YardLine;
                double line_yl = 0.0;
                //possision where ball should be caught
                double prev_yl = gBall.Current_YardLine;
                double prev_v = gBall.Current_Vertical_Percent_Pos;
                gBall.Current_YardLine = gBall.Starting_YardLine + ((double )(Punt_Formation.Punter_Behind_Line_ayrds -1.0) * Game_Engine_Helper.HorizontalAdj(!bLefttoRight));
                gBall.Current_Vertical_Percent_Pos = gBall.Starting_Vertical_Percent_Pos;

                if (!bSim)
                    gBall.Spiral(prev_yl, prev_v);

                io_Players = 0;
                foreach (Game_Player p in Punt_Players)
                {

                    prev_yl = p.Current_YardLine;
                    prev_v = p.Current_Vertical_Percent_Pos;
                    if (p == r.Punter)
                    {
                        if (!bSim) p.Punter_Ready_for_Ball(prev_yl, prev_v);
                    }
                    else if (Punt_Formation.Line_Players.Contains(io_Players))
                    {
                        p.Current_YardLine -= first_block_dropback_yards * Game_Engine_Helper.HorizontalAdj(bLefttoRight);
                        line_yl = p.Current_YardLine;
                        if (!bSim)
                        {
                            Player_States moving_ps = Game_Engine_Helper.setRunningState(bLefttoRight, true, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos, app_Constants.MOVEMENT_DIST_BEFORE_TURNING_BACK);
                            p.Run_Then_Block(moving_ps, prev_yl, prev_v);
                        }
                    }
                    else if (Punt_Formation.Backfield_Players.Contains(io_Players))
                    {
                        p.Current_YardLine = line_yl;
                        if (!bSim)
                        {
                            Player_States moving_ps = Game_Engine_Helper.setRunningState(bLefttoRight, true, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos, app_Constants.MOVEMENT_DIST_BEFORE_TURNING_BACK);
                            p.Run_Then_Block(moving_ps, prev_yl, prev_v);
                        }
                    }
                    else if (Punt_Formation.Gunners.Contains(io_Players))
                    {
                        if (!bSim)
                        {
                            p.Block(false);
                        }
                    }
                    else
                        p.Same_As_Last_Action_not_main();

                    io_Players++;
                }

                io_Players = 0;
                foreach (Game_Player p in Return_Players)
                {
                    prev_yl = p.Current_YardLine;
                    prev_v = p.Current_Vertical_Percent_Pos;

                    if (Return_Formation.Line_Players.Contains(io_Players))
                    {
                        p.Current_YardLine -= (first_block_dropback_yards+.75) * Game_Engine_Helper.HorizontalAdj(bLefttoRight);
                        line_yl = p.Current_YardLine;
                        if (!bSim)
                        {
                            Player_States moving_ps = Game_Engine_Helper.setRunningState(bLefttoRight, true, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos, app_Constants.MOVEMENT_DIST_BEFORE_TURNING_BACK);
                            p.Run_Then_Block(moving_ps, prev_yl, prev_v);
                        }
                    }
                    else if (Return_Formation.Gunners.Contains(io_Players))
                    {
                        p.Current_YardLine -= .75 * Game_Engine_Helper.HorizontalAdj(bLefttoRight);
                        line_yl = p.Current_YardLine;
                        if (!bSim)
                        {
                            Player_States moving_ps = Game_Engine_Helper.setRunningState(bLefttoRight, true, prev_yl, prev_v, line_yl, p.Current_Vertical_Percent_Pos, app_Constants.MOVEMENT_DIST_BEFORE_TURNING_BACK);
                            p.Run_Then_Block(moving_ps, prev_yl, prev_v);
                        }
                    }
                    else
                        p.Same_As_Last_Action_not_main();
                    io_Players++;
                }
            }
            //===== End of Stage Two - ============================================
            logger.Debug("=======================================================");
            logger.Debug("");


            //================================  Stage Three =======================================
            //Punter walks forward and attempt to kick the ball, while possibly a rusher tries to block the kick
            logger.Debug("Stage 2");
            logger.Debug("=====================================================");

            //Punter attempts to kick, attacker possibly attempts block
            if (!bPreSnapPenalty)
            {
                double half_yards = (double) (Punt_Formation.Punter_Behind_Line_ayrds - first_block_dropback_yards)  / 2.0;

                //First decide if an attacker breaks thru the line to attempt a block
                List<int> blocker_index_list = new List<int>();
                blocker_index_list.AddRange(Punt_Formation.Line_Players);
                blocker_index_list.AddRange(Punt_Formation.Backfield_Players);

                List<Game_Player> Blockers = Game_Engine_Helper.getPlayerSublist(Punt_Players, blocker_index_list);
                List<Game_Player> Attackers = Game_Engine_Helper.getPlayerSublist(Return_Players, Return_Formation.Line_Players);

                r.Defender_Close_to_Kicker = getAttacker_BreakThru(Blockers, Attackers);

                if (!bSim)
                {
                    double prev_yl = gBall.Current_YardLine;
                    double prev_v = gBall.Current_Vertical_Percent_Pos;
                    gBall.Current_YardLine += half_yards  * Game_Engine_Helper.HorizontalAdj(bLefttoRight);
                    gBall.Carried_notMain(prev_yl, prev_v);
                }

                //bpo stopped here 
                //foreach in the steop I need to work on the foreaches.

                io_Players = 0;
                foreach (Game_Player p in Punt_Players)
                {

                    double prev_yl = p.Current_YardLine;
                    double prev_v = p.Current_Vertical_Percent_Pos;
                    if (p == r.Punter)
                    {
                        p.Current_YardLine += half_yards * Game_Engine_Helper.HorizontalAdj(bLefttoRight);
                        if (!bSim) p.Run_and_Punt(prev_yl, prev_v);
                    }
                    else if (Punt_Formation.Line_Players.Contains(io_Players) || Punt_Formation.Backfield_Players.Contains(io_Players))
                    {
                        if (!bSim)
                        {
                            p.Same_As_Last_Action();
                        }
                    }
                    else if (Punt_Formation.Gunners.Contains(io_Players))
                    {
                        p.Current_YardLine += half_yards * Game_Engine_Helper.HorizontalAdj(bLefttoRight);
                        if (!bSim)
                        {
                            Player_States moving_ps = Game_Engine_Helper.setRunningState(bLefttoRight, true, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos, app_Constants.MOVEMENT_DIST_BEFORE_TURNING_BACK);
                            p.Run(moving_ps, prev_yl, prev_v);
                        }
                    }


                    io_Players++;
                }

                io_Players = 0;
                foreach (Game_Player p in Return_Players)
                {
                    double prev_yl = p.Current_YardLine;
                    double prev_v = p.Current_Vertical_Percent_Pos;

                    if (p == r.Defender_Close_to_Kicker)
                    {
                        p.Current_YardLine -= half_yards * Game_Engine_Helper.HorizontalAdj(bLefttoRight);
                        Player_States moving_ps = Game_Engine_Helper.setRunningState(bLefttoRight, true, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos, app_Constants.MOVEMENT_DIST_BEFORE_TURNING_BACK);
                        if (!bSim)
                        {
                            p.Run_and_TrytoBlockKick(moving_ps, prev_yl, prev_v);
                        }
                    }
                    else if (Return_Formation.Line_Players.Contains(io_Players))
                    {
                        if (!bSim)
                        {
                            p.Same_As_Last_Action();
                        }
                    }
                    else if (Return_Formation.Gunners.Contains(io_Players))
                    {
                        p.Current_YardLine += half_yards * Game_Engine_Helper.HorizontalAdj(bLefttoRight);
                        if (!bSim)
                        {
                            Player_States moving_ps = Game_Engine_Helper.setRunningState(bLefttoRight, true, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos, app_Constants.MOVEMENT_DIST_BEFORE_TURNING_BACK);
                            p.Run(moving_ps, prev_yl, prev_v);
                        }
                    }
                    else
                        p.Same_As_Last_Action_not_main();
                    io_Players++;
                }
            }

                return r;
        }





        public static List<Game_Player_Stats> SetPlayerStats(List<Game_Player> Punt_Players, List<Game_Player> Return_Players, bool bTouchback, bool coffin_corner_att, bool coffin_corner_made, bool bTouchdown,
    bool bFumble, bool bFumble_Lost, double Yards,
    Game_Player Punter, Game_Player Punt_Returner, Game_Player Tackler,
    Game_Player Forced_Fumble_Recoverer, List<Game_Player> Missed_Tackle)
        {
            List<Game_Player_Stats> r = new List<Game_Player_Stats>();

            return r;
        }

        public static Play_Result setPlayerActions(Formation Kickoff_Formation, Formation Return_Formation, 
            List<Game_Player> Kickoff_Players, List<Game_Player> Return_Players, Play_Result pResult)
        {
            Play_Result r = pResult;

            r.Punter = Kickoff_Players[(int)Kickoff_Formation.KickerIndex];
            //Get the kicker - kicker and returner must be slot 5 in the formation
            r.Punt_Returner = Return_Players[(int)Return_Formation.ReturnerIndex];

            //for testing print out all the players and their relevant ratings
            logger.Debug("Kickoff Players");
            int d_index = 0;
            foreach (Game_Player p in Kickoff_Players)
            {
                if (p != r.Punter)
                    r.Punt_Defenders.Add(p);

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
                if (p != r.Punt_Returner)
                    r.Punt_Returners.Add(p);

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

            return r;
        }

        public Game_Player getAttacker_BreakThru(List<Game_Player> Blockers, List<Game_Player> Attackers)
        {
            Game_Player r = null;
            int max_attack = 0;
            int attacker_wins = 0;
            Game_Player Best_Attacker = null;

            for (int i=0; i<Blockers.Count; i++)
            {
                int attacker_score = 0;
                int blocker_score = 0;
                int attacker_ability = (int) Attackers[i].p_and_r.pr.First().Pass_Attack_Rating * 10;
                int blocker_ability = (int) Blockers[i].p_and_r.pr.First().Pass_Block_Rating * 10;

                attacker_score = CommonUtils.getRandomNum(1, attacker_ability);
                blocker_score = CommonUtils.getRandomNum(1, blocker_ability);

                if (attacker_score > blocker_score)
                {
                    attacker_wins++;
                    if ((attacker_score - blocker_score) > max_attack)
                    {
                        max_attack = attacker_score - blocker_score;
                        Best_Attacker = Attackers[i];
                    }
                }
            }

            //if the attackers win 6 of 8 battles the attacker with the highest score breaks thru
            if (attacker_wins >= 6)
                r = Best_Attacker;

            return null;
        }
        private bool puntBlocked(int punter_yards_behind)
        {
            bool r = false;
            int upper_limit = 10;

            if (punter_yards_behind < 10)
                upper_limit = 9;

            int i = CommonUtils.getRandomNum(1, upper_limit);

            if (i == 1)
                r = true;

            return r;
        }
    }
}
