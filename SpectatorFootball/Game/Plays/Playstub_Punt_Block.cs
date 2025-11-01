using log4net;
using SpectatorFootball.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.GameNS
{
    public class Playstub_Punt_Block
    {
        private static ILog logger = LogManager.GetLogger("RollingFile");
        public static Tuple<Game_Player, bool> Execute(bool bLefttoRight,
            Game_Ball gBall,
            List<Game_Player> Punt_Players,
            List<Game_Player> Return_Players,
            List<Game_Player> close_Punt_Players,
            List<Game_Player> close_Return_Players,
            Game_Player Punter,
            bool bOrignal_BallPossessorTeam)
        {
            Game_Player r = null;
            bool bLost = false;
            int rnd;
            bool bRecover;

            gBall.Popup(50,3);

            int io_Players = 0;
            foreach (Game_Player p in Punt_Players)
            {
                if (p == Punter)
                {
                    p.Punter_Put_Leg_Down(-0.4);
                }
                else
                {
                    p.Stand();
                }
                io_Players++;
            }

            io_Players = 0;

            foreach (Game_Player p in Return_Players)
            {
                p.Stand();
                io_Players++;
            }

            if (close_Punt_Players.Count() == 0 && close_Return_Players.Count() == 0)
                throw new Exception("No close players on either team for fumble.  Should never happen");

            while (r == null)
            {
                Game_Player p = null;
                bool bCheckTacklers = CommonUtils.getRandomTrueFalse();
                if (bCheckTacklers)
                {
                    rnd = CommonUtils.getRandomIndex(close_Return_Players.Count);
                    p = close_Return_Players[rnd];
                    bLost = false;
                }
                else
                {
                    rnd = CommonUtils.getRandomIndex(close_Punt_Players.Count);
                    p = close_Punt_Players[rnd];
                    bLost = true;
                }

                bRecover = RecoverBall(p.p_and_r.pr.First().Hands_Rating);
                if (bRecover)
                    r = p;
            }

            //for the ball
            gBall.Carried_Fake_Movement(5);

            int ind = 0;
            foreach (Game_Player p in Punt_Players)
            {
                double prev_yl = p.Current_YardLine;
                double prev_v = p.Current_Vertical_Percent_Pos;

                p.Current_YardLine = gBall.Current_YardLine;
                p.Current_Vertical_Percent_Pos = gBall.Current_Vertical_Percent_Pos;

                p.Current_YardLine += CommonUtils.VaryDoulblernd(p.Current_YardLine, 1.5);
                p.Current_Vertical_Percent_Pos += CommonUtils.VaryDoulblernd(p.Current_Vertical_Percent_Pos, 1.5);

                Player_States moving_ps = Game_Engine_Helper.setRunningState(bLefttoRight, true, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos, 0.0);
                if (p == Punter)
                    p.Cover_Ball(moving_ps, prev_yl, prev_v, bOrignal_BallPossessorTeam);
                else if (close_Punt_Players.Contains(p))
                    p.Attempt_Tackle(moving_ps, prev_yl, prev_v);
                else
                {
                    p.Same_As_Last_Action();
/*                    p.Current_YardLine += CommonUtils.VaryDoulblernd(p.Current_YardLine, 5);
                    p.Current_Vertical_Percent_Pos += CommonUtils.VaryDoulblernd(p.Current_Vertical_Percent_Pos, 5);
                    moving_ps = Game_Engine_Helper.setRunningState(bLefttoRight, true, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos, 0.0);
                    p.Run(moving_ps, prev_yl, prev_v);
*/
            }
            ind++;
            }

            ind = 0;
            foreach (Game_Player p in Return_Players)
            {
                double prev_yl = p.Current_YardLine;
                double prev_v = p.Current_Vertical_Percent_Pos;

                p.Current_YardLine = gBall.Current_YardLine;
                p.Current_Vertical_Percent_Pos = gBall.Current_Vertical_Percent_Pos;

                p.Current_YardLine += CommonUtils.VaryDoulblernd(p.Current_YardLine, 1.5);
                p.Current_Vertical_Percent_Pos += CommonUtils.VaryDoulblernd(p.Current_Vertical_Percent_Pos, 1.5);

                Player_States moving_ps = Game_Engine_Helper.setRunningState(bLefttoRight, false, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos, 0.0);

                if (close_Return_Players.Contains(p))
                    p.Attempt_Tackle(moving_ps, prev_yl, prev_v);
                else
                {
                    p.Same_As_Last_Action();
/*                    p.Current_YardLine += CommonUtils.VaryDoulblernd(p.Current_YardLine, 5);
                    p.Current_Vertical_Percent_Pos += CommonUtils.VaryDoulblernd(p.Current_Vertical_Percent_Pos, 5);
                    moving_ps = Game_Engine_Helper.setRunningState(bLefttoRight, false, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos, 0.0);
                    p.Run(moving_ps, prev_yl, prev_v);
*/
                }
                ind++;
            }

            return new Tuple<Game_Player, bool>(r, bLost);

        }

        public static bool RecoverBall(long handsRating)
        {
            long fudge = 40;

            bool r = false;
            const int UPPER_LIMIT = 200;

            int rnd = CommonUtils.getRandomNum(1, UPPER_LIMIT);
            if (rnd <= handsRating + fudge)
                r = true;

            return r;
        }
    }
}
