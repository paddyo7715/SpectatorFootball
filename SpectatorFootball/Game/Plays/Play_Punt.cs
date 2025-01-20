using log4net;
using OxyPlot.Wpf;
using SpectatorFootball.Common;
using SpectatorFootball.Enum;
using SpectatorFootball.GameNS;
using SpectatorFootball.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

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
        private bool bLast_Play;
        private Formation Punt_Formation = null;
        private Formation Return_Formation = null;
        List<Game_Player> Blockers = null;
        List<Game_Player> Attackers = null;
        public Play_Result r = new Play_Result();

        public Play_Enum Play { get; set; } = Play_Enum.PUNT;

        public Play_Punt(Formation Punt_Formation, Formation Return_Formation, long Possessing_Team_Id, long at, long ht, Game_Ball gBall, List<Game_Player> Punt_Players, List<Game_Player> Return_Players, bool bLefttoRight, bool bLast_Play)
        {
            this.Possessing_Team_Id = Possessing_Team_Id;
            this.at = at;
            this.ht = ht;
            this.gBall = gBall;
            this.Punt_Players = Punt_Players;
            this.Return_Players = Return_Players;
            this.bLefttoRight = bLefttoRight;
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
            double first_block_dropback_yards = 3.0;
            double starting_yl = gBall.Current_YardLine;
            bool bPuntLogEnoughfor_CC = false;
            double starting_yardline = gBall.Current_YardLine;
            r.Play_Start_Yardline = starting_yardline;

            Set_Ball_and_Players_Before_Snap(gBall, Punt_Players, Return_Players, Punt_Formation, Return_Formation);

            if (bPreSnapPenalty)
            {
                Playstub_Uncrouch.Execute(bLefttoRight, gBall, Punt_Players, Return_Players);
            }
            else
            {
                Snap_Ball_Lines_Clash(gBall, Punt_Players, Return_Players, Punt_Formation, Return_Formation, bLefttoRight, first_block_dropback_yards);
                Punter_Prepares_to_Kick(gBall, Punt_Players, Return_Players, Punt_Formation, Return_Formation, bLefttoRight, first_block_dropback_yards);
                if (r.Defender_Close_to_Kicker != null && puntBlocked((double)Punt_Formation.Punter_Behind_Line_ayrds))
                {
                    r.bPunt_blocked = true;
                    Tuple<Game_Player, bool> t = Playstub_Punt_Block.Execute(bLefttoRight, gBall, Punt_Players, Return_Players, Blockers, Attackers, r.Punter);
                    r.Blocked_Punt_Recoverer = t.Item1;

                    bool bPunt_Team_Recovers = Punt_Players.Any(x => x == r.Blocked_Punt_Recoverer);
                    Tuple<bool, bool> t2 = Game_Engine_Helper.BlockedPuntTD_or_Safety(bPunt_Team_Recovers, gBall.Current_YardLine, bLefttoRight);
                    r.bTouchDown = t2.Item1;
                    r.bSafety = t2.Item2;
                }
                else
                {
                    var t = getMaxPuntLengthandVert(r.Punter);
                    double MaxPuntLen = t.Item1;
                    double MaxPuntVert = t.Item2;

                    var t2 = Game_Engine_Helper.isCCEligible_and_Punt_long_Enough(MaxPuntLen, starting_yardline, bLefttoRight);

                    r.bCoffinCornerAttemt = t2.Item1;
                    bPuntLogEnoughfor_CC = t2.Item2;

                    if (r.bCoffinCornerAttemt && bPuntLogEnoughfor_CC)
                        r.bCoffinCornerMade = Game_Engine_Helper.CoffinCornerMade(r.Punter.p_and_r.pr.First().Kicker_Leg_Power_Rating);

                    var tackle_groups = Game_Engine_Helper.setTackleGroups(Punt_Players, r.Punter);
                    var tBallAct = Game_Engine_Helper.getPuntLandingSpot_and_isCatchable(r.bCoffinCornerAttemt, bPuntLogEnoughfor_CC, r.bCoffinCornerMade, MaxPuntLen, MaxPuntVert, starting_yl, bLefttoRight);

                    r.Kick_caught_yl = tBallAct.Item1;

                    BallPuntedPlayersRun(gBall, Punt_Players, Return_Players, tBallAct, r.Punter, r.Punt_Returner, tackle_groups, r, bLast_Play, bLefttoRight);
                    if (r.bPunt_Returned)
                    {
                        r = return_punt(Punt_Players, Return_Players, gBall, tackle_groups, r, bLefttoRight);
                        r.Yards_Returned = Game_Engine_Helper.getPuntReturnYards(!bLefttoRight, r.Kick_caught_yl, r.Punt_Returner.Current_YardLine);

                        //if there is a returned punt that was fumbled, did the fumble take place in
                        //the returnerns endzone.  If so the play must result in a TD or safety.
                        if (r.bFumble)
                        {
                            //The logic is the same as a blocked punt, so I just reused that method.
                            Tuple<bool, bool> t3 = Game_Engine_Helper.ReturnTD_or_Safety(!r.bFumble_Lost, gBall.Current_YardLine, bLefttoRight);
                            r.bTouchDown = t3.Item1;
                            r.bTouchback = t3.Item2;
                        }
                        else if (r.bPunt_Returned && Game_Engine_Helper.isTouchBack(bLefttoRight, gBall.Current_YardLine))
                            r.bTouchback = true;
                    }
                }

                r.Punt_Yards = Game_Engine_Helper.getPuntYards(starting_yl, r.Kick_landing_YL, bLefttoRight);
                r.end_of_play_yardline = gBall.Current_YardLine;

                r.Play_Player_Stats = SetPlayerStats(r, Punt_Players, Return_Players, r.Missed_Tackles);
            }
            return r;
        }

        private void BallPuntedPlayersRun(Game_Ball gBall, List<Game_Player> Punt_Players, List<Game_Player> Return_Players, Tuple<double,double> tballAct,
            Game_Player Punter, Game_Player Punt_Returner, List<List<int?>> tGroups, Play_Result pr, bool bLast_Play, bool bLefttoRight)
        {
            double newBallX = tballAct.Item1;
            double newBallY = tballAct.Item2;

            double prevBallX = gBall.Current_YardLine;
            double prevBallY = gBall.Current_Vertical_Percent_Pos;
            pr.Kick_landing_YL = newBallX;

            int delay_factor = 3;

            int tgroup_ind = 0;

            int id_Players = 0;
            foreach (Game_Player p in Punt_Players)
            {
                double yardline_Offset = 0.0;
                double vert_offset = 0.0;

                double prev_yl = p.Current_YardLine;
                double prev_v = p.Current_Vertical_Percent_Pos;

                if (p == r.Punter)
                {
                    yardline_Offset = Math.Abs(newBallX - p.Current_YardLine) - app_Constants.PUNT_KICKER_FROM_RETURNER;
                    vert_offset = newBallY;

                    p.Current_YardLine += yardline_Offset * Game_Engine_Helper.HorizontalAdj(bLefttoRight);
                    p.Current_Vertical_Percent_Pos = vert_offset;

                    Player_States moving_ps = Game_Engine_Helper.setRunningState(bLefttoRight, true, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos, app_Constants.MOVEMENT_DIST_BEFORE_TURNING_BACK);
                    p.Punter_Put_Leg_Down_and_Run(moving_ps, prev_yl, prev_v);
                }
                else
                {
                    tgroup_ind = Game_Engine_Helper.getTackleGroup(id_Players, tGroups);
                    int slot = tGroups[tgroup_ind].IndexOf(id_Players);

                    p.Current_YardLine += getAttBlkyl(newBallX, p.Current_YardLine, tgroup_ind) * Game_Engine_Helper.HorizontalAdj(bLefttoRight);
                    p.Current_Vertical_Percent_Pos = getAttBlkVert(newBallY, slot);
                    int delay = tgroup_ind * delay_factor + 1;
                    Player_States moving_ps = Game_Engine_Helper.setRunningState(bLefttoRight, true, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos, app_Constants.MOVEMENT_DIST_BEFORE_TURNING_BACK);
                    p.Delay_Then_Run_and_Stand(moving_ps, prev_yl, prev_v, delay);
                }
                id_Players++;
            }

            id_Players = 0;
            foreach (Game_Player p in Return_Players)
            {
                double prev_yl = p.Current_YardLine;
                double prev_v = p.Current_Vertical_Percent_Pos;

                if (p == r.Punt_Returner)
                {
                    var tRetAct = r.Punt_Returner.PuntReturnerActions(newBallX, newBallY, bLast_Play, bLefttoRight);

                    gBall.Current_YardLine = newBallX;
                    gBall.Current_Vertical_Percent_Pos = newBallY;

                    pr.bPunt_Out_of_Bounds = tRetAct.Item1;
                    pr.bPunt_Out_of_Endzone = tRetAct.Item2;
                    pr.bPunt_KneelDown = tRetAct.Item3;
                    pr.bPunt_Returned = tRetAct.Item4;
                    pr.bPunt_Not_Fielded = tRetAct.Item5;

                    if (pr.bPunt_Out_of_Endzone || pr.bPunt_KneelDown || pr.bPunt_Not_Fielded)
                        pr.bTouchback = true;

                    if (pr.bPunt_Out_of_Bounds)
                        gBall.Punt_Out_of_Bounds(bLefttoRight);
                    else if (pr.bPunt_Out_of_Endzone)
                        gBall.Punt_End_Over_End_Thru_Air_Out_of_Endzone(bLefttoRight);
                    else if (pr.bPunt_Not_Fielded)
                        gBall.Punt_End_Over_End_Thru_Air_Not_Caught(bLefttoRight);
                    else
                        gBall.Punt_End_Over_End_Thru_Air(prevBallX, prevBallY, bLefttoRight);

                    logger.Debug("Punt Result: " + pr.bPunt_blocked + " " + pr.bPunt_Out_of_Bounds + " " + pr.bPunt_Out_of_Endzone + " " + pr.bKick_KneelDown + " " + pr.bPunt_Not_Fielded + " " + pr.bPunt_Returned + " " + newBallX + " " + newBallY + " " + p.Current_YardLine + " " + p.Current_Vertical_Percent_Pos);

                    Player_States moving_ps = Game_Engine_Helper.setRunningState(bLefttoRight, false, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos, app_Constants.MOVEMENT_DIST_BEFORE_TURNING_BACK);
                    if (pr.bPunt_Out_of_Endzone || pr.bPunt_Out_of_Bounds)
                        p.Run_Then_Stand(moving_ps, prev_yl, prev_v);
                    else if (pr.bPunt_KneelDown)
                        p.Run_Then_CatchKick(moving_ps, prev_yl, prev_v);
                    else if (pr.bPunt_Returned)
                        p.Run_Then_CatchKick(moving_ps, prev_yl, prev_v);
                    else if (pr.bPunt_Not_Fielded)
                        p.Run_Then_Stand(moving_ps, prev_yl, prev_v);
                }
                else
                {
                    p.Current_YardLine = Punt_Players[id_Players].Current_YardLine + (app_Constants.PUNT_DIST_BETWEEN_BLOCK_ATTACHERS * Game_Engine_Helper.HorizontalAdj(bLefttoRight));
                    p.Current_Vertical_Percent_Pos = Punt_Players[id_Players].Current_Vertical_Percent_Pos;
                    tgroup_ind = Game_Engine_Helper.getTackleGroup(id_Players, tGroups);
                    int delay = tgroup_ind * delay_factor + 1;
                    Player_States moving_ps = Game_Engine_Helper.setRunningState(bLefttoRight, false, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos, app_Constants.MOVEMENT_DIST_BEFORE_TURNING_BACK);
                    p.Delay_Then_Run_and_Stand(moving_ps, prev_yl, prev_v, delay);
                }
                id_Players++;
            }

            //If the returner kneels then we need to do antoher stage
            if (pr.bPunt_KneelDown)
            {
                foreach (Game_Player p in Punt_Players)
                    p.Same_As_Last_Action();

                foreach (Game_Player p in Return_Players)
                {
                    double prev_yl = p.Current_YardLine;
                    double prev_v = p.Current_Vertical_Percent_Pos;

                    if (p == r.Punt_Returner)
                    {
                        p.Kneel_With_Ball(p.Current_YardLine, p.Current_Vertical_Percent_Pos);

                        //for the ball
                        gBall.Carried_Fake_Movement(1);
                    }
                    else
                       p.Same_As_Last_Action();
                }
            }
        }



        public static Play_Result setPlayerActions(Formation Punt_Formation, Formation Punt_Return_Formation,
            List<Game_Player> Punt_Players, List<Game_Player> Return_Players, Play_Result pResult)
        {
            Play_Result r = pResult;

            r.Punter = Punt_Players[(int)Punt_Formation.KickerIndex];
            //Get the kicker - kicker and returner must be slot 5 in the formation
            r.Punt_Returner = Return_Players[(int)Punt_Return_Formation.ReturnerIndex];

            //for testing print out all the players and their relevant ratings
            int d_index = 0;
            foreach (Game_Player p in Punt_Players)
            {
                if (p != r.Punter)
                    r.Punt_Defenders.Add(p);

                string sPos = p.Pos.ToString();
                long leg_stn = p.p_and_r.pr.First().Kicker_Leg_Power_Rating;
                long leg_acc = p.p_and_r.pr.First().Kicker_Leg_Accuracy_Rating;
                long rn_att = p.p_and_r.pr.First().Run_Attack_Rating;
                d_index++;
            }

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
                d_index++;
            }
            //=============================================

            return r;
        }

        public Game_Player getAttacker_BreakThru(List<Game_Player> Blockers, List<Game_Player> Attackers)
        {
            Game_Player r = null;
            int max_attack = 0;
            int attacker_wins = 0;
            Game_Player Best_Attacker = null;

            for (int i = 0; i < Blockers.Count; i++)
            {
                int attacker_score = 0;
                int blocker_score = 0;
                int attacker_ability = (int)Attackers[i].p_and_r.pr.First().Pass_Attack_Rating * 10;
                int blocker_ability = (int)Blockers[i].p_and_r.pr.First().Pass_Block_Rating * 11;

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
            if (attacker_wins >= Attackers.Count - 1)
                r = Best_Attacker;

            return r;
        }
        private bool puntBlocked(double punter_yards_behind)
        {
            bool r = false;
            int upper_limit = 10;

            if (punter_yards_behind < 10.0)
                upper_limit = 9;

            int i = CommonUtils.getRandomNum(1, upper_limit);

            if (i == 1)
                r = true;

            return r;
        }

        private void Set_Ball_and_Players_Before_Snap(Game_Ball gBall, List<Game_Player> Punt_Players, List<Game_Player> Return_Players,
           Formation Punt_Formation, Formation Return_Formation)
        {

            gBall.TeeUp();

            int io_Players = 0;
            foreach (Game_Player p in Punt_Players)
            {
                bool bMain = true;
                double prev_yl = p.Current_YardLine;
                double prev_v = p.Current_Vertical_Percent_Pos;

                if (Punt_Formation.Line_Players.Contains(io_Players))
                {
                    p.Crouch(prev_yl, prev_v, bMain);
                    bMain = false;
                }
                else
                    p.Stand();

                io_Players++;
            }

            io_Players = 0;
            //The team receiving the kick will just stand there before the kick
            foreach (Game_Player p in Return_Players)
            {

                double prev_yl = p.Current_YardLine;
                double prev_v = p.Current_Vertical_Percent_Pos;
                if (Return_Formation.Line_Players.Contains(io_Players))
                    p.Crouch(prev_yl, prev_v, false);
                else
                    p.Stand();

                io_Players++;
            }
        }

        private void Snap_Ball_Lines_Clash(Game_Ball gBall, List<Game_Player> Punt_Players, List<Game_Player> Return_Players,
             Formation Punt_Formation, Formation Return_Formation, bool bLefttoRight, double first_block_dropback_yards)
        {
            double ball_yl = gBall.Current_YardLine;
            double line_yl = 0.0;
            //possision where ball should be caught
            double prev_yl = gBall.Current_YardLine;
            double prev_v = gBall.Current_Vertical_Percent_Pos;
            gBall.Current_YardLine = gBall.Starting_YardLine + ((double)(Punt_Formation.Punter_Behind_Line_ayrds - 1.75) * Game_Engine_Helper.HorizontalAdj(!bLefttoRight));
            gBall.Current_Vertical_Percent_Pos = gBall.Starting_Vertical_Percent_Pos;

            gBall.Spiral(prev_yl, prev_v);

            int io_Players = 0;
            foreach (Game_Player p in Punt_Players)
            {
                prev_yl = p.Current_YardLine;
                prev_v = p.Current_Vertical_Percent_Pos;
                if (p == r.Punter)
                {
                    p.Punter_Ready_for_Ball(prev_yl, prev_v);
                }
                else if (Punt_Formation.Line_Players.Contains(io_Players))
                {
                    p.Current_YardLine -= first_block_dropback_yards * Game_Engine_Helper.HorizontalAdj(bLefttoRight);
                    line_yl = p.Current_YardLine;

                    Player_States moving_ps = Game_Engine_Helper.setRunningState(bLefttoRight, true, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos, app_Constants.MOVEMENT_DIST_BEFORE_TURNING_BACK);
                    p.Run_Then_Block(moving_ps, prev_yl, prev_v);

                }
                else if (Punt_Formation.Backfield_Players.Contains(io_Players))
                {
                    p.Current_YardLine = line_yl;

                    Player_States moving_ps = Game_Engine_Helper.setRunningState(bLefttoRight, true, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos, app_Constants.MOVEMENT_DIST_BEFORE_TURNING_BACK);
                    p.Run_Then_Block(moving_ps, prev_yl, prev_v);

                }
                else if (Punt_Formation.Gunners.Contains(io_Players))
                {
                    p.Block(false);
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
                    p.Current_YardLine -= (first_block_dropback_yards + .75) * Game_Engine_Helper.HorizontalAdj(bLefttoRight);
                    line_yl = p.Current_YardLine;

                    Player_States moving_ps = Game_Engine_Helper.setRunningState(bLefttoRight, true, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos, app_Constants.MOVEMENT_DIST_BEFORE_TURNING_BACK);
                    p.Run_Then_Block(moving_ps, prev_yl, prev_v);
                }
                else if (Return_Formation.Gunners.Contains(io_Players))
                {
                    p.Current_YardLine -= .75 * Game_Engine_Helper.HorizontalAdj(bLefttoRight);
                    line_yl = p.Current_YardLine;

                    Player_States moving_ps = Game_Engine_Helper.setRunningState(bLefttoRight, true, prev_yl, prev_v, line_yl, p.Current_Vertical_Percent_Pos, app_Constants.MOVEMENT_DIST_BEFORE_TURNING_BACK);
                    p.Run_Then_Block(moving_ps, prev_yl, prev_v);

                }
                else
                    p.Same_As_Last_Action_not_main();
                io_Players++;
            }
        }

        private void Punter_Prepares_to_Kick(Game_Ball gBall, List<Game_Player> Punt_Players, List<Game_Player> Return_Players,
             Formation Punt_Formation, Formation Return_Formation, bool bLefttoRight, double first_block_dropback_yards)
        {
            double half_yards = (double)(Punt_Formation.Punter_Behind_Line_ayrds - first_block_dropback_yards) / 2.0;

            //First decide if an attacker breaks thru the line to attempt a block
            List<int> blocker_index_list = new List<int>();
            blocker_index_list.AddRange(Punt_Formation.Line_Players);
            blocker_index_list.AddRange(Punt_Formation.Backfield_Players);

            Blockers = Game_Engine_Helper.getPlayerSublist(Punt_Players, blocker_index_list);
            Attackers = Game_Engine_Helper.getPlayerSublist(Return_Players, Return_Formation.Line_Players);

            r.Defender_Close_to_Kicker = getAttacker_BreakThru(Blockers, Attackers);

            double prev_ylb = gBall.Current_YardLine;
            double prev_vb = gBall.Current_Vertical_Percent_Pos;
            gBall.Current_YardLine += half_yards * Game_Engine_Helper.HorizontalAdj(bLefttoRight);
            gBall.Carried_notMain(prev_ylb, prev_vb);

            int io_Players = 0;
            foreach (Game_Player p in Punt_Players)
            {

                double prev_yl = p.Current_YardLine;
                double prev_v = p.Current_Vertical_Percent_Pos;
                if (p == r.Punter)
                {
                    p.Current_YardLine += half_yards * Game_Engine_Helper.HorizontalAdj(bLefttoRight);
                    p.Run_and_Punt(prev_yl, prev_v);
                }
                else if (Punt_Formation.Line_Players.Contains(io_Players) || Punt_Formation.Backfield_Players.Contains(io_Players))
                {
                    p.Same_As_Last_Action();
                }
                else if (Punt_Formation.Gunners.Contains(io_Players))
                {
                    p.Current_YardLine += half_yards * Game_Engine_Helper.HorizontalAdj(bLefttoRight);

                    Player_States moving_ps = Game_Engine_Helper.setRunningState(bLefttoRight, true, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos, app_Constants.MOVEMENT_DIST_BEFORE_TURNING_BACK);
                    p.Run(moving_ps, prev_yl, prev_v);

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
                    p.Current_Vertical_Percent_Pos = gBall.Current_Vertical_Percent_Pos;
                    Player_States moving_ps = Game_Engine_Helper.setRunningState(bLefttoRight, true, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos, app_Constants.MOVEMENT_DIST_BEFORE_TURNING_BACK);

                    p.Run_and_TrytoBlockKick(moving_ps, prev_yl, prev_v);

                }
                else if (Return_Formation.Line_Players.Contains(io_Players))
                {
                    p.Same_As_Last_Action();
                }
                else if (Return_Formation.Gunners.Contains(io_Players))
                {
                    p.Current_YardLine += half_yards * Game_Engine_Helper.HorizontalAdj(bLefttoRight);

                    Player_States moving_ps = Game_Engine_Helper.setRunningState(bLefttoRight, true, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos, app_Constants.MOVEMENT_DIST_BEFORE_TURNING_BACK);
                    p.Run(moving_ps, prev_yl, prev_v);
                }
                else
                    p.Same_As_Last_Action_not_main();
                io_Players++;
            }
        }

        public Tuple<double,double> getMaxPuntLengthandVert(Game_Player Punter)
        { 
            long leg_strength = Punter.p_and_r.pr.First().Kicker_Leg_Power_Rating;
            Punt_Len Punt_length_enum = Kicking_Helper.getPunt_Len_enum(leg_strength);
            double Punt_Len = Kicking_Helper.getPunt_len(Punt_length_enum);

            long leg_accuracy = r.Punter.p_and_r.pr.First().Kicker_Leg_Accuracy_Rating;
            Punt_Vertical Punt_Vert_enum = Kicking_Helper.getPunt_Vert_enum(leg_accuracy);
            double Punt_Vert = Kicking_Helper.getPunt_Vert(Punt_Vert_enum);

            double realPuntLen = Kicking_Helper.AdjustKickLength(Punt_Len, Punt_Vert);

            return Tuple.Create(realPuntLen, Punt_Vert);
        }

        public double getAttBlkVert(double bally, int slot)
        {
            double r = 0;
            double too_low = 90.0;
            double too_high = 10.0;

            slot -= 2;

            if (bally < too_high)
                bally = 21.0;
            else if (bally > too_low)
                bally = 79.0;

            r = bally + app_Constants.PUNT_GROUP_VERT_DIST * slot;

            return r;
        }

        public double getAttBlkyl(double ballx, double playerx, int group)
        {
            double r = 0;
            int min_yl, max_yl = 0;

            switch (group)
            {
                case 0:
                    min_yl = app_Constants.PUNT_GROUP_1_MIN;
                    max_yl = app_Constants.PUNT_GROUP_1_MAX;
                    break;
                case 1:
                    min_yl = app_Constants.PUNT_GROUP_2_MIN;
                    max_yl = app_Constants.PUNT_GROUP_2_MAX;
                    break;
                case 2:
                    min_yl = app_Constants.PUNT_GROUP_3_MIN;
                    max_yl = app_Constants.PUNT_GROUP_3_MAX;
                    break;
                default:
                    throw new Exception("Error in method getAttBlkyl, unknow group index " + group );
            }

            int yard_off_returner = CommonUtils.getRandomNum(min_yl, max_yl) + app_Constants.PUNT_GROUP_1_MIN;
            r = Math.Abs(ballx - playerx) - yard_off_returner;

            return r;
        }

        private Play_Result return_punt(List<Game_Player> Punt_Players, List<Game_Player> Return_Players,
            Game_Ball gBall, List<List<int?>> tGroups, Play_Result r, bool bLefttoRight)
        {
            int slot_index, prev_slot_index = 0;
            List<int> Past_Blocker_Tackler_List = new List<int>();
            int id_Players = 0;
            int ind_close_Tklr = 0;


            var ts = GetInitialSlot(gBall.Current_Vertical_Percent_Pos);
            slot_index = ts.Item1;
            double slot2_vert = ts.Item2;
            bLefttoRight = Game_Engine_Helper.Switch_LefttoRight(bLefttoRight);

            List<int?> group = new List<int?>();
            for (int i = 1; i <= app_Constants.PUNT_TACKLING_GROUPS + 1; i++)
            {

                bool bFindOpenSlot = false;
                double agility = r.Punt_Returner.p_and_r.pr.First().Agilty_Rating;
                bFindOpenSlot = ReturnerLookforHole(agility);

                int Tackler_Index = 0;
                double dbetweenVert = 0.0;

                bool bAnySlot = false;

                //For kicker tackler
                if (i > app_Constants.PUNT_TACKLING_GROUPS)
                {
                    group = new List<int?>() { null, null, null, null, null };
                    group[slot_index] = Punt_Formation.KickerIndex;
                }
                else
                    group = tGroups[i - 1];

                prev_slot_index = slot_index;
                slot_index = getPuntReturnRunSlot(slot_index, bFindOpenSlot, group, bAnySlot);

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
                    Tackler_Index = (int)group[slot_index];
                    TB_List.Add(Tackler_Index);

                    dbetweenVert = app_Constants.PUNT_GROUP_VERT_DIST / 2.0;

                    bSwereUp = CommonUtils.getRandomTrueFalse();
                    if (bSwereUp)
                        dbetweenVert *= -1;

                    int? adjacent_tackler = getPossibleUporDownTackler(bSwereUp, slot_index, group);
                    if (adjacent_tackler != null)
                        TB_List.Add((int)adjacent_tackler);
                }
                else
                {
                    //Since the returner is running to an open slot, let's see if the slot just above and below has potential tacklers
                    TB_List.AddRange(getPossibleAdjacentTacklers(slot_index, group));
                }

                if (TB_List.Count > 0)
                    ind_close_Tklr = getClosestKickGroupPlayerInd(slot_index, group);

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
                        Punt_Players[tackler_ind].p_and_r.pr.First().Pass_Attack_Rating,
                        Punt_Players[tackler_ind].p_and_r.pr.First().Run_Attack_Rating,
                        Punt_Players[tackler_ind].p_and_r.pr.First().Agilty_Rating,
                        Punt_Players[tackler_ind].p_and_r.pr.First().Speed_Rating);

                    long tackler_tackle_rating = Punt_Players[tackler_ind].p_and_r.pr.First().Tackle_Rating;
                    //adjust potential tackler's tackle rating based on the block
                    tackler_tackle_rating = Game_Engine_Helper.AdjustTackleRating_forBlock(br, tackler_tackle_rating);

                    bool bTack = Game_Engine_Helper.Make_Tackle(
                        r.Punt_Returner.p_and_r.pr.First().Speed_Rating,
                        r.Punt_Returner.p_and_r.pr.First().Agilty_Rating,
                        r.Punt_Returner.p_and_r.pr.First().Running_Power_Rating,
                        tackler_tackle_rating);

                    //bpo test
/*                    if (i <= 3)
                        bTack = false;
                    else
                        bTack = true; */
                    //********************

                    if (bTack)
                        r.Tackler = Punt_Players[tackler_ind];
                    else
                        r.Missed_Tackles.Add(Punt_Players[tackler_ind]);

                    if (r.Tackler != null)
                        break;

                    if (b_list_ind == TB_List.Count - 1)
                        b_list_ind = 0;
                    else
                        b_list_ind++;
                }  

                if (TB_List.Count > 0)
                {
                    ind_close_Tklr = getClosestKickGroupPlayerInd(slot_index, group);

                    returner_hole_yl = Punt_Players[ind_close_Tklr].Current_YardLine;
                    if (group[slot_index] != null)
                        returner_before_tackler_yardline = returner_hole_yl - (app_Constants.PUNT_YARDS_BEFORE_TACKLER * Game_Engine_Helper.HorizontalAdj(bLefttoRight));
                    else
                        returner_before_tackler_yardline = returner_hole_yl - (app_Constants.PUNT_YARDS_BEFORE_TACKLER2 * Game_Engine_Helper.HorizontalAdj(bLefttoRight));

                    returner_before_tackler_vert = slot2_vert + getPuntGroupOffset(slot_index);

                    returner_swerve_vert = returner_before_tackler_vert + dbetweenVert;
                }

                if (i == 4)
                    Breakthrough_len = (Game_Engine_Helper.calcDistanceFromOpponentGL(returner_hole_yl, bLefttoRight) + 5.0) * Game_Engine_Helper.HorizontalAdj(bLefttoRight);
                else
                    Breakthrough_len = (app_Constants.PUNT_GROUP_1_MAX - app_Constants.PUNT_GROUP_1_MIN) + (app_Constants.PUNT_GROUP_2_MIN - app_Constants.PUNT_GROUP_1_MAX) * Game_Engine_Helper.HorizontalAdj(bLefttoRight);

                Breakthrough_vert = slot2_vert + getPuntGroupOffset(slot_index);

                id_Players = 0;
                foreach (Game_Player p in Punt_Players)
                {
                    if (p == r.Punter && i != 4)  //In fourth group, kicker is tackler
                    {
                        double prev_yl = p.Current_YardLine;
                        double prev_v = p.Current_Vertical_Percent_Pos;

                        if (TB_List.Count > 0)
                            p.Current_Vertical_Percent_Pos = returner_before_tackler_vert;
                        else
                            p.Current_Vertical_Percent_Pos = Breakthrough_vert;

                        Player_States moving_ps = Game_Engine_Helper.setRunningState(bLefttoRight, false, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos, app_Constants.MOVEMENT_DIST_BEFORE_TURNING_BACK);
                        p.Run(moving_ps, prev_yl, prev_v);

                        if (TB_List.Count > 0 && !r.bRunOutofBounds)
                            p.Stand();
                    }
                    else if (TB_List.Contains(id_Players))
                    {
                        //keep blocking till the returner runs up to you
                        p.Block(false);

                        double prev_yl = p.Current_YardLine;
                        double prev_v = p.Current_Vertical_Percent_Pos;

                        p.Current_YardLine = returner_hole_yl;
                        p.Current_Vertical_Percent_Pos = returner_swerve_vert;

                        //Move vertically to make the tackle
                        if (!r.bRunOutofBounds)
                        {
                            Player_States moving_ps = Game_Engine_Helper.setRunningState(bLefttoRight, false, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos, app_Constants.MOVEMENT_DIST_BEFORE_TURNING_BACK);
                            p.Attempt_Tackle(moving_ps, prev_yl, prev_v);
                        }
                    }
                    // Players from previous groups should still do what they last did not go back to blocking
                    else if (Past_Blocker_Tackler_List.Contains(id_Players))
                    {
                        p.Same_As_Last_Action();
                        if (TB_List.Count > 0 && !r.bRunOutofBounds)
                            p.Same_As_Last_Action();
                    }
                    else
                    {
                        p.Block(false);
                        //If there is a tackler then  continue to block while he attempts the tackle
                        if (TB_List.Count > 0 && !r.bRunOutofBounds)
                            p.Block(false);
                    }
                    id_Players++;
                }

                id_Players = 0;
                foreach (Game_Player p in Return_Players)
                {
                    if (p == r.Punt_Returner) 
                    {
                        if (TB_List.Count > 0)
                        {
                            double prev_yl = p.Current_YardLine;
                            double prev_v = p.Current_Vertical_Percent_Pos;

                            p.Current_YardLine = returner_before_tackler_yardline;
                            p.Current_Vertical_Percent_Pos = returner_before_tackler_vert;

                            //must move the ball too, even thogh it will not be visible.
                            gBall.Current_YardLine = p.Current_YardLine;
                            gBall.Current_Vertical_Percent_Pos = p.Current_Vertical_Percent_Pos;

                            Player_States moving_ps = Game_Engine_Helper.setRunningState(bLefttoRight, true, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos, app_Constants.MOVEMENT_DIST_BEFORE_TURNING_BACK);
                            p.Run_With_Ball(moving_ps, prev_yl, prev_v);

                            //for the ball
                            gBall.Carried(prev_yl, prev_v);

                            //he either gets tackled or not
                            prev_yl = p.Current_YardLine;
                            prev_v = p.Current_Vertical_Percent_Pos;

                            p.Current_YardLine = returner_hole_yl;
                            p.Current_Vertical_Percent_Pos = returner_swerve_vert;

                            //must move the ball too, even thogh it will not be visible.
                            gBall.Current_YardLine = p.Current_YardLine;
                            gBall.Current_Vertical_Percent_Pos = p.Current_Vertical_Percent_Pos;

                            Player_States moving_ps2 = Game_Engine_Helper.setRunningState(bLefttoRight, true, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos, app_Constants.MOVEMENT_DIST_BEFORE_TURNING_BACK);
                            if (r.Tackler != null)
                            {
                                p.Run_and_Tackled(moving_ps, prev_yl, prev_v);
                                gBall.Carried_Tackled(prev_yl, prev_v);
                            }
                            else
                            {
                                if (i==4)  //if kicker tacker doesn't tackle then TD
                                {
                                    p.Current_YardLine += Breakthrough_len;
                                    gBall.Current_YardLine += Breakthrough_len;
                                }

                                p.Run_With_Ball(moving_ps2, prev_yl, prev_v);
                                //for the ball
                                gBall.Carried(prev_yl, prev_v);
                            }

                        }
                        else
                        {
                            double prev_yl = p.Current_YardLine;
                            double prev_v = p.Current_Vertical_Percent_Pos;

                            p.Current_YardLine += Breakthrough_len;
                            p.Current_Vertical_Percent_Pos = Breakthrough_vert;

                            gBall.Current_YardLine = p.Current_YardLine;
                            gBall.Current_Vertical_Percent_Pos = p.Current_Vertical_Percent_Pos;

                            Player_States moving_ps = Game_Engine_Helper.setRunningState(bLefttoRight, true, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos, app_Constants.MOVEMENT_DIST_BEFORE_TURNING_BACK);
                            p.Run_With_Ball(moving_ps, prev_yl, prev_v);

                            //for the ball
                            gBall.Carried(prev_yl, prev_v);
                        }
                    }
                    // Players from previous groups should still do what they last did not go back to blocking
                    else if (Past_Blocker_Tackler_List.Contains(id_Players))
                    {
                            p.Same_As_Last_Action();
                            if (TB_List.Count > 0 && !r.bRunOutofBounds)
                                p.Same_As_Last_Action();
                    }
                    else if (TB_List.Contains(id_Players))
                    {
                        p.Block(true);

                        if (!r.bRunOutofBounds)
                            p.Stand();
                    }
                    else
                    {
                        p.Block(true);

                        if (TB_List.Count > 0 && !r.bRunOutofBounds)
                            p.Block(true);
                    }
                    id_Players++;
                }

                //if there is a tackle (not the punter) then check if the ball is fumbled.
                if (r.Tackler != null && i != 4) 
                {
                    long ball_safety_rating = r.Punt_Returner.p_and_r.pr.First().Ball_Safety_Rating;
                    long tackle_rating = r.Tackler.p_and_r.pr.First().Tackle_Rating;
                    long run_attack_rating = r.Tackler.p_and_r.pr.First().Run_Attack_Rating;

                    r.bFumble = Game_Engine_Helper.DoesBallCarrierFumble(
                               Ball_Carry_Actions.PUNT_RETURN,
                               ball_safety_rating, tackle_rating, run_attack_rating);

                    r.test_counter++;

                    //if there is a fumble then there can not be a tackle, but give the tackler
                    //creit for forcing the fumble
                    if (r.bFumble)
                    {
                        r.Forced_Fumble_Tackler = r.Tackler;
                        List<Game_Player> pFumble_Rec_Punt_Players = new List<Game_Player>();
                        List<Game_Player> pFumble_Rec_Return_Players = new List<Game_Player>();
                        List<int> closest_players = getPuntGroupClosestPlayers(slot_index, group);
                        getBothGroupSlotPlayers(Punt_Players, Return_Players,
                            pFumble_Rec_Punt_Players, pFumble_Rec_Return_Players, closest_players);
                        pFumble_Rec_Return_Players.Add(r.Punt_Returner);
                        Tuple<Game_Player, bool> t = Playstub_Fumble.Execute(bLefttoRight, gBall,
                            Punt_Players, Return_Players,
                            pFumble_Rec_Punt_Players, pFumble_Rec_Return_Players,
                            r.Punt_Returner, r.Tackler);

                        r.Fumble_Recoverer = t.Item1;
                        r.bFumble_Lost = t.Item2;

                        //If there is a fumble then no tackle is awarded
                        r.Tackler = null;
                    }
                }

                //set td or not
                if (Game_Engine_Helper.isTouchdown(bLefttoRight, r.Punt_Returner.Current_YardLine, r.bTouchback))
                {
                    r.bTouchDown = true;
                    //handle case where the returner is tacked in the EZ
                    r.Tackler = null;
                    r.bFumble = false;
                    r.bFumble_Lost = false;
                    r.Fumble_Recoverer = null;
                    r.Forced_Fumble_Tackler = null;
                    break;
                }

                if (r.Tackler != null || r.bTouchback || r.bRunOutofBounds || r.bFumble)
                    break;

                Past_Blocker_Tackler_List.AddRange(group.Where(x => x != null).Select(x => (int)x).ToList());
            }  //on group 1,2,3 and 4

            return r;
        }

        public static bool ReturnerLookforHole(double agility)
        {
            bool r = false;

            int agility_var = (int)agility - app_Constants.PUNT_AGILITY_CUTOFF;
            int r_agile = CommonUtils.getRandomNum(1, app_Constants.PUNT_AVOID_TRACKER_CALC_VARIABLE);
            if (r_agile <= agility_var)
                r = true;

            return r;
        }

        public static Tuple<int, double> GetInitialSlot(double bally)
        {
            double vert = 0.0;

            int slot;
            if (bally <= app_Constants.PUNT_TOP_VERT_CUTOFF)
            {
                slot = 0;
                vert = bally + app_Constants.PUNT_GROUP_VERT_DIST * 2;
            }
            else if (bally >= app_Constants.PUNT_BOTTOM_VERT_CUTOFF)
            {
                slot = 4;
                vert = bally - app_Constants.PUNT_GROUP_VERT_DIST * 2;
            }
            else
            {
                slot = 2;
                vert = bally;
            }

            return Tuple.Create(slot, vert);
        }


        public static int getPuntReturnRunSlot(int slot_index, bool bLookforhole, List<int?> group, bool bAnyFive)
        {
            int r;
            List<int> empty_indexes = CommonUtils.GetIndexes(group, true);
            List<int> possible_indexes = new List<int>();
            if (bAnyFive)
            {
                if (bLookforhole && empty_indexes.Count() > 0)
                    possible_indexes = empty_indexes;
                else
                    possible_indexes = CommonUtils.GetIndexes(group, false);
            }
            else
            {
                if (bLookforhole) //you can go a max of 2 up or down
                {
                    foreach (int i in empty_indexes)
                    {
                        if (i >= slot_index - app_Constants.PUNT_AFTER_FIRST_GROUP_SLOT_VARIANCE && i <= slot_index + app_Constants.PUNT_AFTER_FIRST_GROUP_SLOT_VARIANCE)
                            possible_indexes.Add(i);
                    }
                    if (possible_indexes.Count() == 0)
                    {
                        for (int i = slot_index - app_Constants.PUNT_AFTER_FIRST_GROUP_SLOT_VARIANCE; i <= slot_index + app_Constants.PUNT_AFTER_FIRST_GROUP_SLOT_VARIANCE; i++)
                        {
                            if (i >= 0 && i < group.Count())
                                possible_indexes.Add(i);
                        }
                    }
                }
                else
                {
                    for (int i = slot_index - app_Constants.PUNT_AFTER_FIRST_GROUP_SLOT_VARIANCE; i <= slot_index + app_Constants.PUNT_AFTER_FIRST_GROUP_SLOT_VARIANCE; i++)
                    {
                        if (i >= 0 && i < group.Count())
                            possible_indexes.Add(i);
                    }
                }

            }

            int r_ind = CommonUtils.getRandomIndex(possible_indexes.Count());
            r = possible_indexes[r_ind];

            return r;
        }
        public static int? getPossibleUporDownTackler(bool bSwerveUp, int slot_index, List<int?> group)
        {
            int? r = null;

            if (bSwerveUp)
            {
                if (slot_index > 0) r = group[slot_index - 1];
            }
            else
            {
                if (slot_index < app_Constants.PUNT_PLAYERS_IN_GROUP - 1) r = group[slot_index + 1];
            }

            return r;
        }
        public static List<int> getPossibleAdjacentTacklers(int slot_index, List<int?> group)
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
            if (slot_index < app_Constants.PUNT_PLAYERS_IN_GROUP - 1)
            {
                int below_slot = slot_index + 1;
                if (group[below_slot] != null)
                    r.Add((int)group[below_slot]);
            }

            return r;
        }
        public static int getClosestKickGroupPlayerInd(int slot_index, List<int?> Group)
        {
            int r, g;
            List<int> Possible_Indexes = new List<int>();

            if (Group[slot_index] != null)
                Possible_Indexes.Add(slot_index);

            if (slot_index > 0 && Group[slot_index - 1] != null)
                Possible_Indexes.Add(slot_index - 1);

            if (slot_index < app_Constants.PUNT_PLAYERS_IN_GROUP - 1 && Group[slot_index + 1] != null)
                Possible_Indexes.Add(slot_index + 1);

            if (Possible_Indexes.Count == 0)
                throw new Exception("Could not find closest tacker in method getClosestKickGroupPlayerInd");

            int r_ind = CommonUtils.getRandomIndex(Possible_Indexes.Count());
            g = Possible_Indexes[r_ind];

            r = (int)Group[g];

            return r;
        }

        public static double getPuntGroupOffset(int ind)
        {
            double r = 0;

            ind -= 2;

            r = app_Constants.PUNT_GROUP_VERT_DIST * ind;

            return r;
        }

        public static List<int> getPuntGroupClosestPlayers(int slot_index, List<int?> group)
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
            if (slot_index < app_Constants.PUNT_PLAYERS_IN_GROUP - 1)
            {
                int below_slot = slot_index + 1;
                if (group[below_slot] != null)
                    r.Add((int)group[below_slot]);
            }

            if (group[slot_index] != null)
                r.Add((int)group[slot_index]);

            return r;
        }

        public static void getBothGroupSlotPlayers(
            List<Game_Player> Punt_Players,
            List<Game_Player> Return_Players,
            List<Game_Player> pFumble_Rec_Punt_Players,
            List<Game_Player> pFumble_Rec_Return_Players,
            List<int> grpIndexes)
        {

            foreach (int i in grpIndexes)
            {
                pFumble_Rec_Punt_Players.Add(Punt_Players[i]);
                pFumble_Rec_Return_Players.Add(Return_Players[i]);
            }
        }

        public static List<Game_Player_Stats> SetPlayerStats(Play_Result pr, List<Game_Player> Punt_Players, List<Game_Player> Return_Players,
            List<Game_Player> Missed_Tackles)
        {
            long lTDs = pr.bTouchDown ? 1 : 0;
            long lFubmle = pr.bFumble ? 1 : 0;
            long lFubmle_Lost = pr.bFumble_Lost ? 1 : 0;
            long lPunt_out_of_Endzone = pr.bPunt_Out_of_Endzone ? 1 : 0;           
            long cc_attempts = pr.bCoffinCornerAttemt ? 1 : 0;
            long cc_made = pr.bCoffinCornerMade ? 1 : 0;
            long punter_blocks = pr.bPunt_blocked ? 1 : 0;

            List<Game_Player_Stats> r = new List<Game_Player_Stats>();

            //Set a play record for each player in the play
            foreach (Game_Player p in Punt_Players)
            {
                long punt_def_tackles = pr.Tackler == p ? 1 : 0;
                long punt_def_tackles_missed = Missed_Tackles.Contains(p) ? 1 : 0;
                if (p == pr.Punter)
                    r.Add(new Game_Player_Stats()
                    {
                        Player_ID = pr.Punter.p_and_r.pr.First().Player_ID,
                        punter_plays = 1,
                        punter_punts = !pr.bPunt_blocked ?  1 : 0,
                        punter_punt_yards = !pr.bPunt_blocked ? (int)(pr.Punt_Yards + 0.5) : 0,
                        punter_kill_att = cc_attempts,
                        punter_kill_Succ = cc_made,
                        punter_blocks = punter_blocks,
                        punt_def_tackles = punt_def_tackles,
                        punt_def_tackles_missed = punt_def_tackles_missed
                    });
                else
                {
                    int ind = 0;
                    long punt_def_forced_fumbles = pr.Forced_Fumble_Tackler == p ? 1 : 0;
                    long punt_forced_fumbles_recovered = pr.Fumble_Recoverer == p ? 1 : 0;
                    r.Add(new Game_Player_Stats()
                    {
                        Player_ID = p.p_and_r.pr.First().Player_ID,
                        punt_def_plays = 1,
                        punt_def_forced_fumbles = punt_def_forced_fumbles,
                        punt_forced_fumbles_recovered = punt_forced_fumbles_recovered,
                        punt_def_tackles = punt_def_tackles,
                        punt_def_tackles_missed = punt_def_tackles_missed
                    });
                    ind++;
                }
            }

            foreach (Game_Player p in Return_Players)
            {
                if (p == pr.Returner)
                {
                    long punt_returns = pr.bPunt_Returned ? 1 : 0;
                    long punt_ret_TDs = pr.bTouchDown && !pr.bPunt_blocked && !pr.bFumble_Lost ? 1 : 0;
                    long punt_ret_fumbles = pr.bFumble ? 1 : 0;
                    long punt_ret_fumbles_Lost = pr.bFumble_Lost ? 1 : 0;

                    r.Add(new Game_Player_Stats()
                    {
                        Player_ID = pr.Punt_Returner.p_and_r.pr.First().Player_ID,
                        punt_ret_plays = 1,
                        punt_ret = punt_returns,
                        punt_ret_yards = (int)(pr.Yards_Returned + 0.5),
                        punt_ret_TDs = punt_ret_TDs,
                        punt_ret_yards_long = (int)(pr.Yards_Returned + 0.5),
                        punt_ret_fumbles = punt_ret_fumbles,
                        punt_ret_fumbles_lost = punt_ret_fumbles_Lost,
                    });
                }
                else
                {
                    long punt_rec_blocks = pr.bPunt_blocked && pr.Defender_Close_to_Kicker == p ? 1 : 0;
                    long punt_rec_block_recovery = pr.Blocked_Punt_Recoverer == p ? 1 : 0;
                    long punt_rec_block_recovery_TDs = pr.Blocked_Punt_Recoverer == p && pr.bTouchDown ? 1 : 0;

                    r.Add(new Game_Player_Stats() { 
                        Player_ID = p.p_and_r.pr.First().Player_ID,
                        punt_rec_plays = 1,
                        punt_rec_blocks = punt_rec_blocks,
                        punt_rec_block_recovery = punt_rec_block_recovery,
                        punt_rec_block_recovery_TDs = punt_rec_block_recovery_TDs

                    });
                }
            }

             return r;
        }
    }
}

