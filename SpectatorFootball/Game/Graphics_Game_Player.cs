﻿using SpectatorFootball.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

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
        public int current_Stage = 0;
        public int current_action = 0;
        public int current_point = 0;
        public Game_Sounds? Sound;
        public bool bStageFinished = false;

        public Graphics_Game_Player(Player_States pState, bool bCarringBall, double YardLine,
            double Vertical_Percent_Pos, List<Play_Stage> Stages)
        {
            this.pState = pState;
            this.bCarringBall = bCarringBall;
            this.YardLine = YardLine;
            this.Vertical_Percent_Pos = Vertical_Percent_Pos;
            this.Stages = Stages;

            graph_pState = setGraphicsState(this.pState);

        }

        private Graphics_Player_States setGraphicsState(Player_States pState)
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
        public void Update()
        {
            Sound = null;
            bStageFinished = false;
            Play_Stage pStage = Stages[current_Stage];

            //This must never be true
            if (pStage.Main_Object && pStage.Actions.Count() == 0)
                throw new Exception("Cant be main object with no actions");

            //Only if there are actions/movements left.
                if (current_action < pStage.Actions.Count() &&
                    pStage.Actions[current_action].PointXY.Count() > 0 &&
                   (current_point < pStage.Actions[current_action].PointXY.Count()))
            {

                Action act = pStage.Actions[current_action];

                pState = (Player_States)act.p_state;
                graph_pState = setGraphicsState(pState);
                YardLine = act.PointXY[current_point].x;
                Vertical_Percent_Pos = act.PointXY[current_point].y;

                if (act.PointXY != null && act.PointXY.Count() > 0)
                {
                    if (current_point == 0)
                        Sound = act.Sound;
                    else
                        Sound = null;
                }

                current_point++;
                if (current_point >= act.PointXY.Count())
                {
                    current_action++;
                    current_point = 0;
                }

                graph_pState = setGraphicsState(pState);

            } //if actions
            else
            {
                if (pStage.Main_Object)
                {
                    bStageFinished = true;
                }
            }
        }
    }

}
