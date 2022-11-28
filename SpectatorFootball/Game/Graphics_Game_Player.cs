using SpectatorFootball.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.GameNS
{
    public class Graphics_Game_Player
    {
        public Player_States pState;
        public Graphics_Player_States graph_pState;
        public bool bCarringBall;
        public double YardLine;
        public double Vertical_Percent_Pos;
        public List<Play_Stage> Stages = null;
        private int current_Stage = 0;
        private int current_Action = 0;
        bool bFinished = false;
        public string sSound = null;
        

        public Graphics_Game_Player(Player_States pState, bool bCarringBall, double YardLine,
            double Vertical_Percent_Pos, List<Play_Stage> Stages)
        {
            this.pState = pState;
            this.bCarringBall = bCarringBall;
            this.YardLine = YardLine;
            this.Vertical_Percent_Pos = Vertical_Percent_Pos;
            this.Stages = Stages;

            graph_pState = setGraphicsState();



        }

        private Graphics_Player_States setGraphicsState()
        {
            Graphics_Player_States r = Graphics_Player_States.STANDING;

            switch (pState)
            {
                case Player_States.STANDING:
                    r = Graphics_Player_States.STANDING;
                    break;
                case Player_States.RUNNING_FORWARD:
                    if (bCarringBall)
                        if (graph_pState != Graphics_Player_States.RUNNING_1)
                            r = Graphics_Player_States.RUNNING_WITH_BALL_1;
                        else
                            r = Graphics_Player_States.RUNNING_WITH_BALL_2;
                    else
                        if (graph_pState != Graphics_Player_States.RUNNING_1)
                            r = Graphics_Player_States.RUNNING_1;
                        else
                            r = Graphics_Player_States.RUNNING_2;
                    break;
                case Player_States.FG_KICK:
                    r = Graphics_Player_States.FG_KICK;
                    break;
                case Player_States.ABOUT_TO_CATCH_KICK:
                    r = Graphics_Player_States.ABOUT_TO_CATCH_KICK;
                    break;
                case Player_States.BLOCKING:
                    if (graph_pState != Graphics_Player_States.BLOCKING_1)
                        r = Graphics_Player_States.BLOCKING_1;
                    else
                        r = Graphics_Player_States.BLOCKING_2;
                    break;
                case Player_States.RUNNING_UP:
                    if (bCarringBall)
                        if (graph_pState != Graphics_Player_States.RUNNING_UP_WITH_BALL_1)
                            r = Graphics_Player_States.RUNNING_UP_WITH_BALL_1;
                        else
                            r = Graphics_Player_States.RUNNING_UP_WITH_BALL_2;
                    else
                        if (graph_pState != Graphics_Player_States.RUNNING_UP_1)
                        r = Graphics_Player_States.RUNNING_UP_1;
                    else
                        r = Graphics_Player_States.RUNNING_UP_2;
                    break;
                case Player_States.RUNNING_DOWN:
                    if (bCarringBall)
                        if (graph_pState != Graphics_Player_States.RUNNING_DOWN_WITH_BALL_1)
                            r = Graphics_Player_States.RUNNING_DOWN_WITH_BALL_1;
                        else
                            r = Graphics_Player_States.RUNNING_DOWN_WITH_BALL_2;
                    else
                        if (graph_pState != Graphics_Player_States.RUNNING_DOWN_1)
                        r = Graphics_Player_States.RUNNING_DOWN_1;
                    else
                        r = Graphics_Player_States.RUNNING_DOWN_2;
                    break;
                case Player_States.TACKLING:
                    r = Graphics_Player_States.TACKLING;
                    break;
                case Player_States.TACKLED:
                    r = Graphics_Player_States.TACKLED;
                    break;
                case Player_States.ON_BACK:
                    r = Graphics_Player_States.ON_BACK;
                    break;
                case Player_States.RUNNING_BACKWORDS:
                    if (graph_pState != Graphics_Player_States.RUNNING_BACKWORDS_1)
                        r = Graphics_Player_States.RUNNING_BACKWORDS_1;
                    else
                        r = Graphics_Player_States.RUNNING_BACKWORDS_2;
                    break;
            }
            return r;
        }
            public void isEnd_Movement()
        {

            /*
            if (YardLine >= end_YardLine && Vertical_Percent_Pos >= end_Vertical_Percent_Pos)
            {
                if (current_movement >= movements.Count - 1)
                    bFinished = true;
                {
                    bFinished = false;
                    current_movement++;
                }
            }
*/

        }
    }

}
