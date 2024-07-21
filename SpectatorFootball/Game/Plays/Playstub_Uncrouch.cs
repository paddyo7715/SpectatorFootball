using SpectatorFootball.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace SpectatorFootball.GameNS
{
    internal class Playstub_Uncrouch
    {
        private static ILog logger = LogManager.GetLogger("RollingFile");
        public static void Execute(bool bLefttoRight,
            Game_Ball gBall,
            List<Game_Player> Team1,
            List<Game_Player> Team2,
            bool bSim)
        {

            if (!bSim)
            {
                //for the ball
                gBall.Fake_Movement(5);

            }
            int ind = 0;
            foreach (Game_Player p in Team1)
            {
                double prev_yl = p.Current_YardLine;
                double prev_v = p.Current_Vertical_Percent_Pos;

                p.Current_YardLine = gBall.Current_YardLine;
                p.Current_Vertical_Percent_Pos = gBall.Current_Vertical_Percent_Pos;

                if (!bSim)
                {
                    Player_States moving_ps = Game_Engine_Helper.setRunningState(bLefttoRight, true, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos, 0.0);
                    if (p.State == Player_States.CROUCH_BLOCK_UP)
                        p.Crouch_Stand_Up(prev_yl, prev_v);
                    else 
                        p.Stand();
                 }
                ind++;
            }

            ind = 0;
            foreach (Game_Player p in Team2)
            {
                double prev_yl = p.Current_YardLine;
                double prev_v = p.Current_Vertical_Percent_Pos;

                p.Current_YardLine = gBall.Current_YardLine;
                p.Current_Vertical_Percent_Pos = gBall.Current_Vertical_Percent_Pos;

                if (!bSim)
                {
                    Player_States moving_ps = Game_Engine_Helper.setRunningState(bLefttoRight, true, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos, 0.0);
                    if (p.State == Player_States.CROUCH_BLOCK_UP)
                        p.Crouch_Stand_Up(prev_yl, prev_v);
                    else
                        p.Stand();
                }
                ind++;
            }

            return;
        }
    }
}
