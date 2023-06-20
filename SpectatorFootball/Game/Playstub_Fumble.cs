using SpectatorFootball.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.GameNS
{
    class Playstub_Fumble
    {
        public static Game_Player Execute(bool bLefttoRight,
            Game_Ball gBall, 
            List<Game_Player> Tackling_Players, 
            List<Game_Player> BallCarrying_Players,
            List<Game_Player> close_Tackling_Players,
            List<Game_Player> close_BallCarrying_Players,
            Game_Player Returner,
            Game_Player Tackler,
            bool bSim)
        {
            Game_Player r = null;
            bool bRecover = false;
            int rnd = 0;

            if (close_Tackling_Players.Count() == 0 && close_BallCarrying_Players.Count() == 0)
                throw new Exception("No close players on either team for fumble.  Should never happen");

            while (r == null)
            {
                Game_Player p = null;
                bool bCheckTacklers = CommonUtils.getRandomTrueFalse();
                if (bCheckTacklers)
                {
                    rnd = CommonUtils.getRandomIndex(close_Tackling_Players.Count);
                    p = close_Tackling_Players[rnd];
                }
                else
                {
                    rnd = CommonUtils.getRandomIndex(close_BallCarrying_Players.Count);
                    p = close_BallCarrying_Players[rnd];
                }

                bRecover = RecoverFumble(p.p_and_r.pr.First().Hands_Rating);
                if (bRecover)
                    r = p;
            }

            if (!bSim)
            {
                //for the ball
                gBall.Carried_Fake_Movement(10);
            }

            foreach (Game_Player p in BallCarrying_Players)
            {
                if (close_BallCarrying_Players.Contains(p) && p != Returner)
                {
                    //keep blocking till the returner runs up to you
                    if (!bSim)
                        p.Block();

                    double prev_yl = p.Current_YardLine;
                    double prev_v = p.Current_Vertical_Percent_Pos;

                    p.Current_YardLine = gBall.Current_YardLine;
                    p.Current_Vertical_Percent_Pos = gBall.Current_Vertical_Percent_Pos;

                    if (bSim)
                    {
                        Player_States moving_ps = Game_Engine_Helper.setRunningState(bLefttoRight, true, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos);
                        p.Run_and_Tackled(moving_ps, prev_yl, prev_v);
                    }
                }
                // Players from previous groups should still do what they last did not go back to blocking
                else
                {
                    if (!bSim)
                        p.Same_As_Last_Action();
                }
            }

            foreach (Game_Player p in Tackling_Players)
            {
                if (close_Tackling_Players.Contains(p) && p != Tackler)
                {
                    //keep blocking till the returner runs up to you
                    if (!bSim)
                        p.Block();

                    double prev_yl = p.Current_YardLine;
                    double prev_v = p.Current_Vertical_Percent_Pos;

                    p.Current_YardLine = gBall.Current_YardLine;
                    p.Current_Vertical_Percent_Pos = gBall.Current_Vertical_Percent_Pos;

                    if (bSim)
                    {
                        Player_States moving_ps = Game_Engine_Helper.setRunningState(bLefttoRight, true, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos);
                        p.Run_and_Tackled(moving_ps, prev_yl, prev_v);
                    }
                }
                // Players from previous groups should still do what they last did not go back to blocking
                else 
                {
                    if (!bSim)
                        p.Same_As_Last_Action();
                }
            }

            return r;
        }
        public static bool RecoverFumble(long handsRating)
        {
            bool r = false;
            const int UPPER_LIMIT = 200;

            int rnd = CommonUtils.getRandomNum(1, UPPER_LIMIT);
            if (rnd <= handsRating)
                r = true;

            return r;
        }
     }
}
