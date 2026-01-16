using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using log4net;
using SpectatorFootball.Enum;
using SpectatorFootball.GameNS;
using SpectatorFootball.Models;
using SpectatorFootball.NarrationAndText;

namespace SpectatorFootball.GameNS
{
    public class Play_FG_XP : iPlay
    {
        private static ILog logger = LogManager.GetLogger("RollingFile");

        private long Possessing_Team_Id;
        private long at;
        private long ht;
        private Game_Ball gBall;
        private List<Game_Player> FG_Players;
        private List<Game_Player> FG_Def_Players;
        private bool bLefttoRight;
        private bool FreeKic;
        private bool bLast_Play;
        private Formation FG_Formation = null;
        private Formation FG_Def_Formation = null;
        List<Game_Player> Blockers = null;
        List<Game_Player> Attackers = null;
        private bool bFG = true;
        public Play_Result r = new Play_Result();
        private Announcer _announcer = new Announcer();

        public double touchback_yl { get; set; } = 20;

        public Play_Enum Play { get; set; } = Play_Enum.FIELD_GOAL;

        public Play_FG_XP(Formation FG_Formation, Formation FG_Def_Formation, long Possessing_Team_Id, long at, long ht, Game_Ball gBall, List<Game_Player> FG_Players, List<Game_Player> FG_Def_Players, bool bLefttoRight, bool bLast_Play, bool bFG)
        {
            this.Possessing_Team_Id = Possessing_Team_Id;
            this.at = at;
            this.ht = ht;
            this.gBall = gBall;
            this.FG_Players = FG_Players;
            this.FG_Def_Players = FG_Def_Players;
            this.bLefttoRight = bLefttoRight;
            this.bLast_Play = bLast_Play;
            this.FG_Formation = FG_Formation;
            this.FG_Def_Formation = FG_Def_Formation;
            this.bFG = bFG;

            if (!bFG) this.Play = Play_Enum.EXTRA_POINT;

            r.BallPossessing_Team_Id = Possessing_Team_Id == at ? ht : at;
            r.NonbBallPossessing_Team_Id = Possessing_Team_Id == at ? at : ht;
            r.at = at;
            r.ht = ht;
            r = setPlayerActions(FG_Formation, FG_Def_Formation, FG_Players, FG_Def_Players, r);
        }

        public Play_Result Execute(bool bPreSnapPenalty)
        {
            List<string> Play_Stages = new List<string>();
            double starting_yl = gBall.Current_YardLine;
            double starting_yardline = gBall.Current_YardLine;
            r.Play_Start_Yardline = starting_yardline;

            //set field goal attempt length
            r.Field_Goal_Attempt_Length = Game_Engine_Helper.yards_from_end_of_endzone(FG_Players[(int)FG_Formation.FGHolderIndex].Current_YardLine, bLefttoRight);

            double max_kick_len = 0;

            Set_Ball_and_Players_Before_Snap(gBall, FG_Players, FG_Def_Players, FG_Formation, FG_Def_Formation, bLefttoRight);

            if (bPreSnapPenalty)
            {
                Playstub_Uncrouch.Execute(bLefttoRight, gBall, FG_Players, FG_Def_Players);
            }
            else
            {
                //First decide if an attacker breaks thru the line to attempt a block
                Blockers = Game_Engine_Helper.getPlayerSublist(FG_Players, FG_Formation.Line_Players);
                Attackers = Game_Engine_Helper.getPlayerSublist(FG_Def_Players, FG_Def_Formation.Line_Players);

                Game_Player Kick_Blocker = Game_Engine_Helper.getAttacker_BreakThru(Blockers, Attackers, 2);
                r.Defender_Close_to_Kicker = Kick_Blocker;

                Snap_Ball_Lines_Clash(gBall, FG_Players, FG_Def_Players, FG_Formation, FG_Def_Formation, bLefttoRight);
                Run_Up_And_Kick_Ball(r,gBall, FG_Players, FG_Def_Players, FG_Formation, FG_Def_Formation, Kick_Blocker, bLefttoRight);

                if (Kick_Blocker != null)
                    r.FGXP_Blocked = isKickBlocked();

                //bpo test
                //r.FGXP_Blocked = true;

                //if there is a block then there can't be a roughing/runnig into the kicker penalty
                if (r.FGXP_Blocked)
                {
                    r.FGXP_Blocked = true;
                    if (bFG)
                        r.bFGMissed = true;
                    else
                        r.bXPMissed = true;
                    r.Defender_Close_to_Kicker = null;
                    Kick_blocked(gBall, FG_Players, FG_Def_Players, Kick_Blocker, bLefttoRight);
                }
                else
                {
                    long leg_stn = r.Kicker.p_and_r.pr.First().Kicker_Leg_Power_Rating;
                    double kick_len = r.Kicker.getMaxFGLen(leg_stn);
                    double end_v = r.Kicker.getFGVert(r.Field_Goal_Attempt_Length);

                    //bpo
                    end_v = 42.0;

                    Ball_Kicked(r, gBall, FG_Players, FG_Def_Players, kick_len, end_v, bLefttoRight);
                }

                r.Play_Player_Stats = SetPlayerStats(r, FG_Players, FG_Def_Players, Kick_Blocker);
            }
            return r;
        }

        public Play_Result getPlayResult()
        {
            return r;
        }

        public bool isPreSnapPenalty_Eligible()
        {
            return true;
        }

        public bool isAccumeStats()
        {
            return true;
        }

        public static Play_Result setPlayerActions(Formation FG_Formation, Formation FG_Def_Formation,
            List<Game_Player> FG_Players, List<Game_Player> FG_Def_Players, Play_Result pResult)
        {
            Play_Result r = pResult;

            r.Kicker = FG_Players[(int)FG_Formation.KickerIndex];
            //Get the kicker - kicker and returner must be slot 5 in the formation

            return r;
        }

        private void Set_Ball_and_Players_Before_Snap(Game_Ball gBall, List<Game_Player> FG_Players, List<Game_Player> FG_Def_Players,
            Formation FG_Formation, Formation FG_Def_Formation, bool blefttoright)
        {

            gBall.TeeUp();

            bool bfirst = true;
            int io_Players = 0;
            foreach (Game_Player p in FG_Players)
            {
                bool bMain = true;
                if (bfirst)
                {
                    bMain = true;
                    bfirst = false;
                }
                else
                    bMain = false;

                double prev_yl = p.Current_YardLine;
                double prev_v = p.Current_Vertical_Percent_Pos;

                if (FG_Formation.Line_Players.Contains(io_Players))
                {
                    p.Crouch(prev_yl, prev_v, bMain);
                    bMain = false;
                }
                else if (FG_Formation.FGHolderIndex == io_Players)
                {
                    p.Ready_Hold_FG();
                }

                else
                    p.Stand();

                io_Players++;
            }

            io_Players = 0;
            //The team receiving the kick will just stand there before the kick
            foreach (Game_Player p in FG_Def_Players)
            {
                double prev_yl = p.Current_YardLine;
                double prev_v = p.Current_Vertical_Percent_Pos;
                if (FG_Def_Formation.Line_Players.Contains(io_Players))
                    p.Crouch(prev_yl, prev_v, false);
                else
                    p.Stand();

                io_Players++;
            }
        }

        public static List<Game_Player_Stats> SetPlayerStats(Play_Result pr, List<Game_Player> FG_Players, List<Game_Player> FG_Def_Players,
            Game_Player kick_blocker)
        {
            long FGAtt = pr.bFGMade || pr.bFGMissed ? 1 : 0;
            long FGMade = pr.bFGMade ? 1 : 0;
            long XPAtt = pr.bXPMade || pr.bXPMissed ? 1 : 0;
            long XPMade = pr.bXPMade ? 1 : 0;

            long FGPlays = pr.bFGMade || pr.bFGMissed ? 1 : 0;
            long XPPlays = pr.bXPMade || pr.bXPMissed ? 1 : 0;
            long FG_Long = pr.bFGMade ? (int) (pr.Field_Goal_Attempt_Length + .5) : 0;

            List<Game_Player_Stats> r = new List<Game_Player_Stats>();

            //Set a play record for each player in the play
            foreach (Game_Player p in FG_Players)
            {
                if (p == pr.Kicker)
                    r.Add(new Game_Player_Stats()
                    {
                        Player_ID = pr.Kicker.p_and_r.pr.First().Player_ID,
                        FG_Plays = FGPlays,
                        XP_Plays = XPPlays,
                        FG_Att = FGAtt,
                        FG_Made = FGMade,
                        FG_Long = FG_Long,
                        XP_Att = XPAtt,
                        XP_Made = XPMade
                    });

                else
                {
                    r.Add(new Game_Player_Stats()
                    {
                        Player_ID = p.p_and_r.pr.First().Player_ID,
                        FG_Plays = FGPlays,
                        XP_Plays = XPPlays
                    });
                }
            }

            foreach (Game_Player p in FG_Def_Players)
            {
                long FG_Blocks = 0;
                long XP_Blocks = 0;
                if (kick_blocker == p)
                {
                   FG_Blocks = FGPlays == 1 ? 1 : 0;
                   XP_Blocks = XPPlays == 1 ? 1 : 0;
                }

                r.Add(new Game_Player_Stats()
                {
                    Player_ID = p.p_and_r.pr.First().Player_ID,
                    fg_def_plays = FGPlays,
                    XP_Def_Plays = XPPlays,
                    fg_def_block = FG_Blocks,
                    XP_Block = XP_Blocks
                });
            }

            return r;
        }

        private void Snap_Ball_Lines_Clash(Game_Ball gBall, List<Game_Player> FG_Players, List<Game_Player> FG_Def_Players,
            Formation FG_Formation, Formation FG_Def_Formation, bool bLefttoRight)
        {
            double yards_before_holder = -1.0;
            double def_line_advance_yards = 1.5;
            double def_backfield_advance_yards = 2.5;
            double holder_yl = FG_Players[(int) FG_Formation.FGHolderIndex].Current_YardLine;
            double holder_v = FG_Players[(int)FG_Formation.FGHolderIndex].Current_Vertical_Percent_Pos; 

            double ball_yl = gBall.Current_YardLine;
            double line_yl = 0.0;
            //possision where ball should be caught
            double prev_yl = gBall.Current_YardLine;
            double prev_v = gBall.Current_Vertical_Percent_Pos;
            gBall.Current_YardLine = holder_yl + (yards_before_holder * Game_Engine_Helper.HorizontalAdj(!bLefttoRight));
            gBall.Current_Vertical_Percent_Pos = holder_v;

            gBall.Spiral(prev_yl, prev_v);

            //Set ball back to vert 50 where the golder places it.
            gBall.Current_Vertical_Percent_Pos = prev_v;

            int io_Players = 0;
            foreach (Game_Player p in FG_Players)
            {
                prev_yl = p.Current_YardLine;
                prev_v = p.Current_Vertical_Percent_Pos;
                if (FG_Formation.Line_Players.Contains(io_Players))
                {
                    Player_States moving_ps = Game_Engine_Helper.setRunningState(bLefttoRight, true, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos, app_Constants.MOVEMENT_DIST_BEFORE_TURNING_BACK);
                    p.Block(false);

                }
                else
                    p.Same_As_Last_Action_not_main();

                io_Players++;
            }

            io_Players = 0;
            foreach (Game_Player p in FG_Def_Players)
            {
                prev_yl = p.Current_YardLine;
                prev_v = p.Current_Vertical_Percent_Pos;

                if (FG_Def_Formation.Line_Players.Contains(io_Players))
                {
                    p.Current_YardLine -= def_line_advance_yards * Game_Engine_Helper.HorizontalAdj(bLefttoRight);
                    line_yl = p.Current_YardLine;

                    Player_States moving_ps = Game_Engine_Helper.setRunningState(bLefttoRight, true, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos, app_Constants.MOVEMENT_DIST_BEFORE_TURNING_BACK);
                    p.Run_Then_Block(moving_ps, prev_yl, prev_v);
                }
                else if (FG_Def_Formation.Backfield_Players.Contains(io_Players))
                {
                    p.Current_YardLine -= def_backfield_advance_yards * Game_Engine_Helper.HorizontalAdj(bLefttoRight);
                    line_yl = p.Current_YardLine;

                    Player_States moving_ps = Game_Engine_Helper.setRunningState(bLefttoRight, true, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos, app_Constants.MOVEMENT_DIST_BEFORE_TURNING_BACK);
                    p.Run_Then_Block(moving_ps, prev_yl, prev_v);
                }
                else
                    p.Same_As_Last_Action_not_main();

                io_Players++;
            }
        }
        private void Run_Up_And_Kick_Ball(Play_Result pr, Game_Ball gBall, List<Game_Player> FG_Players, List<Game_Player> FG_Def_Players,
            Formation FG_Formation, Formation FG_Def_Formation, Game_Player Close_Player, bool bLefttoRight)
        {
            double holder_yl = FG_Players[(int)FG_Formation.FGHolderIndex].Current_YardLine;

            double ball_yl = holder_yl;
            double line_yl = gBall.Current_Vertical_Percent_Pos;
            //possision where ball should be caught
            double prev_yl = gBall.Current_YardLine;
            double prev_v = gBall.Current_Vertical_Percent_Pos;
            gBall.Current_YardLine = holder_yl;
            double half_yards = 4.0;


            gBall.Carried_Fake_Movement(1);

            int io_Players = 0;
            foreach (Game_Player p in FG_Players)
            {
                prev_yl = p.Current_YardLine;
                prev_v = p.Current_Vertical_Percent_Pos;
                if (pr.Kicker == p)
                {
                    p.Current_YardLine = holder_yl;

                    Player_States moving_ps = Game_Engine_Helper.setRunningState(bLefttoRight, true, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos, app_Constants.MOVEMENT_DIST_BEFORE_TURNING_BACK);
                    p.KickBall(moving_ps, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos,null, null);

                }
                else if (FG_Formation.FGHolderIndex == io_Players)
                {
                    p.Holder_Place_Ball(prev_yl, prev_v);
                }
                else
                    p.Same_As_Last_Action_not_main();

                io_Players++;
            }

            io_Players = 0;
            foreach (Game_Player p in FG_Def_Players)
            {
                if (p == Close_Player)
                {
                    prev_yl = p.Current_YardLine;
                    prev_v = p.Current_Vertical_Percent_Pos;
                    p.Current_YardLine -= half_yards * Game_Engine_Helper.HorizontalAdj(bLefttoRight);
                    p.Current_Vertical_Percent_Pos = gBall.Current_Vertical_Percent_Pos;
                    Player_States moving_ps = Game_Engine_Helper.setRunningState(bLefttoRight, true, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos, app_Constants.MOVEMENT_DIST_BEFORE_TURNING_BACK);

                    p.Run_and_TrytoBlockKick(moving_ps, prev_yl, prev_v);
                }
                else
                {
                    p.Same_As_Last_Action_not_main();
                }
                io_Players++;
            }
        }

        private bool isKickBlocked()
        {
            bool r = false;
            int upper_limit = 10;

            int i = CommonUtils.getRandomNum(1, upper_limit);

            if (i <= 6)
                r = true;

            return r;
        }

        

        private void Kick_blocked(Game_Ball gBall, List<Game_Player> FG_Players, List<Game_Player> FG_Def_Players, Game_Player blocker, bool bLefttoRight)
        {
            double yards_blocked = 12.0;
            bool top = CommonUtils.getRandomTrueFalse();

            double prev_yl = gBall.Current_YardLine;
            double prev_v = gBall.Current_Vertical_Percent_Pos;

            double ending_vert = top ? 101.0 : -1.0;

            gBall.Current_YardLine = blocker.Current_YardLine;

            double end_yl = gBall.Current_YardLine + yards_blocked * Game_Engine_Helper.HorizontalAdj(bLefttoRight);
            double end_v = ending_vert;

            gBall.FG_Blocked(prev_yl, prev_v, end_yl, end_v, bLefttoRight, -0.4, _announcer.Announce_InPlay(announce_event.FG_BLOCKED, r.Kicker.p_and_r.p.Last_Name, Game_Engine_Helper.getYardlineDisplay(gBall.Current_YardLine)), null);

            int io_Players = 0;
            foreach (Game_Player p in FG_Players)
            {
                prev_yl = p.Current_YardLine;
                prev_v = p.Current_Vertical_Percent_Pos;

                Tuple<double, double> t = getBlock_RunTo(end_v);
                p.Current_YardLine += t.Item1;
                p.Current_Vertical_Percent_Pos += t.Item2;
                Player_States moving_ps = Game_Engine_Helper.setRunningState(bLefttoRight, true, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos, app_Constants.MOVEMENT_DIST_BEFORE_TURNING_BACK);
                if (p == r.Kicker)
                    p.Delay_Then_Run_and_Stand(moving_ps, prev_yl, prev_v, 3, _announcer.Announce_InPlay(announce_event.FG_BLOCKED, r.Kicker.p_and_r.p.Last_Name, Game_Engine_Helper.getYardlineDisplay(p.Current_YardLine)), null);
                else
                    p.Delay_Then_Run_and_Stand(moving_ps, prev_yl, prev_v, 3, null, null);

                io_Players++;
            }

            io_Players = 0;
            foreach (Game_Player p in FG_Def_Players)
            {
                prev_yl = p.Current_YardLine;
                prev_v = p.Current_Vertical_Percent_Pos;
                Tuple<double, double> t = getBlock_RunTo(end_v);
                p.Current_YardLine += t.Item1;
                p.Current_Vertical_Percent_Pos += t.Item2;
                Player_States moving_ps = Game_Engine_Helper.setRunningState(bLefttoRight, true, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos, app_Constants.MOVEMENT_DIST_BEFORE_TURNING_BACK);
                p.Delay_Then_Run_and_Stand(moving_ps, prev_yl, prev_v, 3, null, null);
                io_Players++;
            }

        }

        private void Ball_Kicked(Play_Result pr, Game_Ball gBall, List<Game_Player> FG_Players, List<Game_Player> FG_Def_Players, double kick_len, double ball_end_v, bool bLefttoRight)
        {

            double prev_yl = gBall.Current_YardLine;
            double prev_v = gBall.Current_Vertical_Percent_Pos;

            gBall.Current_YardLine += kick_len * Game_Engine_Helper.HorizontalAdj(bLefttoRight); ;
            gBall.Current_Vertical_Percent_Pos = ball_end_v;

            Tuple<bool,bool, FG_Path, double, double, double, double> t = Game_Engine_Helper.FGResult(prev_yl, prev_v, gBall.Current_YardLine, gBall.Current_Vertical_Percent_Pos, bLefttoRight);

            if (bFG)
            {
                pr.bFGMade = t.Item1;
                pr.bFGMissed = !t.Item1;
            }
            else
            {
                pr.bXPMade = t.Item1;
                pr.bXPMissed = !t.Item1;
            }


            //bpo test
            /*            for (int ccc = 1; ccc <= 1000; ccc++)
                        {
                            double vert = (double)ccc / 10.0;
                            Tuple<bool, bool, FG_Path, double, double, double, double> yyy = Game_Engine_Helper.FGResult(prev_yl, prev_v, gBall.Current_YardLine, vert, bLefttoRight);
                            logger.Debug(vert + " " + yyy.Item1 + " " + yyy.Item2);
                        } */



            /*            if (t.Item1)
                            logger.Debug("Field goal is Good");
                        else
                            logger.Debug("Field goal is not Good");
            */

            if (t.Item2)
            {
                gBall.Current_YardLine = t.Item4;
                gBall.Current_Vertical_Percent_Pos = t.Item5;
            }

            logger.Debug("Kick len " + kick_len);
            logger.Debug("ball end yardline " + gBall.Current_YardLine);
            logger.Debug("ball end vert " + gBall.Current_Vertical_Percent_Pos);


            switch (t.Item3)
            {
                case FG_Path.INTO_CROWD:
                    gBall.FG_Into_Stands(prev_yl, prev_v, bLefttoRight);
                    break;
                case FG_Path.SHORT_OF_GOALPOSTS:
                    pr.bFGXPShort = true;
                    gBall.FG_Short(prev_yl, prev_v, bLefttoRight);
                    break;
                case FG_Path.BEYOND_GOALPOSTS:
                    gBall.FG_Long_Enough(prev_yl, prev_v, bLefttoRight);
                    break;
                case FG_Path.HIT_GOALPOST_INTO_CROWD:
                    pr.bFGXPHitGP = true;
                    gBall.FG_Hits_GP_Into_Stands(prev_yl, prev_v, t.Item6, t.Item7, bLefttoRight, 0.25,null,_announcer.Announce_InPlay(announce_event.FG_HITS_GP, r.Kicker.p_and_r.p.Last_Name, Game_Engine_Helper.getYardlineDisplay(gBall.Current_YardLine)));
                    break;
                case FG_Path.HIT_GAOLPOST:
                    pr.bFGXPHitGP = true;
                    gBall.FG_Hits_GP(prev_yl, prev_v, t.Item6, t.Item7, bLefttoRight, 0.25, _announcer.Announce_InPlay(announce_event.FG_HITS_GP, r.Kicker.p_and_r.p.Last_Name, Game_Engine_Helper.getYardlineDisplay(gBall.Current_YardLine)), null);
                    break;
            }

            int io_Players = 0;
            foreach (Game_Player p in FG_Players)
            {
                if (FG_Formation.FGHolderIndex == io_Players)
                    p.Ready_Hold_FG();
                else if (p == r.Kicker)
                    p.Kicker_Put_Leg_Down_and_Stand(_announcer.Announce_InPlay(announce_event.FG_AWAY, r.Kicker.p_and_r.p.Last_Name, Game_Engine_Helper.getYardlineDisplay(p.Current_YardLine)),null);
                else
                    p.Stand();
                io_Players++;
            }

            io_Players = 0;
            foreach (Game_Player p in FG_Def_Players)
            {
                p.Stand();
                io_Players++;
            }
        }

        private Tuple<double, double> getBlock_RunTo(double end_v)
        {
            double y = CommonUtils.getRandomNum(1, 4);
            double v = CommonUtils.getRandomNum(1, 10);

            if (end_v < 0)
            {
                y *= -1;
                v *= -1;
            }

            return Tuple.Create(y,v);
        }
    }
}

