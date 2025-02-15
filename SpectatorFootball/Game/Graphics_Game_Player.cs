using SpectatorFootball.Enum;
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
        public bool bPlayerCatchesBall = false;
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

            graph_pState = setGraphicsState(this.pState) ;

        }

        public bool arePointsDone()
        {
            bool r = true;

            if (!Stages[current_Stage].Main_Object && current_Stage < Stages.Count && current_action < Stages[current_Stage].Actions.Count)
            {
                int pCount = Stages[current_Stage].Actions[current_action].PointXY.Count;
                if ((current_point + 1) < pCount)
                    r = false;
            }

            return r;
        }

        public void ChangeStage(int current_Stage)
        {
            if (this.current_Stage != current_Stage)
            {
                this.current_action = 0;
                this.current_point = 0;
            }
            this.current_Stage = current_Stage;
        }

        private Graphics_Player_States setGraphicsState(Player_States pState)
        {
            Graphics_Player_States r = Graphics_Player_States.STANDING;

            switch (pState)
            {
                case Player_States.STANDING:
                    r = bCarringBall ? Graphics_Player_States.STANDING_WITH_BALL : Graphics_Player_States.STANDING;
                    break;
                case Player_States.RUNNING_FORWARD:
                    if (bCarringBall)
                        if (graph_pState == Graphics_Player_States.RUNNING_WITH_BALL_1)
                            r = Graphics_Player_States.RUNNING_WITH_BALL_2;
                        else if (graph_pState == Graphics_Player_States.RUNNING_WITH_BALL_2)
                            r = Graphics_Player_States.RUNNING_WITH_BALL_3;
                        else if (graph_pState == Graphics_Player_States.RUNNING_WITH_BALL_3)
                            r = Graphics_Player_States.RUNNING_WITH_BALL_4;
                        else
                            r = Graphics_Player_States.RUNNING_WITH_BALL_1;
                    else
                        if (graph_pState == Graphics_Player_States.RUNNING_1)
                            r = Graphics_Player_States.RUNNING_2;
                        else if (graph_pState == Graphics_Player_States.RUNNING_2)
                            r = Graphics_Player_States.RUNNING_3;
                        else if (graph_pState == Graphics_Player_States.RUNNING_3)
                            r = Graphics_Player_States.RUNNING_4;
                        else
                            r = Graphics_Player_States.RUNNING_1;
                    break;
                case Player_States.FG_KICK:
                    Sound = Game_Sounds.KICK;
                    r = Graphics_Player_States.FG_KICK;
                    break;
                case Player_States.ABOUT_TO_CATCH_KICK:
                    r = Graphics_Player_States.ABOUT_TO_CATCH_KICK;
                    break;
                case Player_States.BLOCKING:
                    if (graph_pState == Graphics_Player_States.BLOCKING_1)
                    {
                        int yyy = CommonUtils.getRandomNum(1, 100);
                        if (yyy <= 4) Sound = Game_Sounds.PLAYERS_COLLIDING;

                        bool bYesNo = CommonUtils.getRandomTrueFalse();
                        if (bYesNo)
                            r = Graphics_Player_States.BLOCKING_3;
                        else
                            r = Graphics_Player_States.BLOCKING_2;
                    }
                    else
                        r = Graphics_Player_States.BLOCKING_1;
                    break;
                case Player_States.RUNNING_UP:
                    if (bCarringBall)
                        if (graph_pState == Graphics_Player_States.RUNNING_UP_WITH_BALL_1)
                            r = Graphics_Player_States.RUNNING_UP_WITH_BALL_2;
                        else if (graph_pState == Graphics_Player_States.RUNNING_UP_WITH_BALL_2)
                            r = Graphics_Player_States.RUNNING_UP_WITH_BALL_3;
                        else if (graph_pState == Graphics_Player_States.RUNNING_UP_WITH_BALL_3)
                            r = Graphics_Player_States.RUNNING_UP_WITH_BALL_4;
                        else
                            r = Graphics_Player_States.RUNNING_UP_WITH_BALL_1;
                    else
                      if (graph_pState == Graphics_Player_States.RUNNING_UP_1)
                        r = Graphics_Player_States.RUNNING_UP_2;
                      else if (graph_pState == Graphics_Player_States.RUNNING_UP_2)
                        r = Graphics_Player_States.RUNNING_UP_3;
                      else if (graph_pState == Graphics_Player_States.RUNNING_UP_3)
                        r = Graphics_Player_States.RUNNING_UP_4;
                      else
                        r = Graphics_Player_States.RUNNING_UP_1;
                    break;
                case Player_States.RUNNING_DOWN:
                    if (bCarringBall)
                        if (graph_pState == Graphics_Player_States.RUNNING_DOWN_WITH_BALL_1)
                            r = Graphics_Player_States.RUNNING_DOWN_WITH_BALL_2;
                        else if (graph_pState == Graphics_Player_States.RUNNING_DOWN_WITH_BALL_2)
                            r = Graphics_Player_States.RUNNING_DOWN_WITH_BALL_3;
                        else if (graph_pState == Graphics_Player_States.RUNNING_DOWN_WITH_BALL_3)
                            r = Graphics_Player_States.RUNNING_DOWN_WITH_BALL_4;
                        else
                            r = Graphics_Player_States.RUNNING_DOWN_WITH_BALL_1;
                    else
                        if (graph_pState == Graphics_Player_States.RUNNING_DOWN_1)
                            r = Graphics_Player_States.RUNNING_DOWN_2;
                        else if (graph_pState == Graphics_Player_States.RUNNING_DOWN_2)
                            r = Graphics_Player_States.RUNNING_DOWN_3;
                        else if (graph_pState == Graphics_Player_States.RUNNING_DOWN_3)
                            r = Graphics_Player_States.RUNNING_DOWN_4;
                        else
                            r = Graphics_Player_States.RUNNING_DOWN_1;
                    break;
                case Player_States.TACKLING:
                    if (!graph_pState.ToString().ToUpper().StartsWith("TACKLING_"))
                    {
                        Sound = Game_Sounds.PLAYER_TACKLED;
                        r = Graphics_Player_States.TACKLING_1;
                    }
                    else if (graph_pState == Graphics_Player_States.TACKLING_1)
                        r = Graphics_Player_States.TACKLING_2;
                    else
                        r = Graphics_Player_States.TACKLING_3;
                    break;
                case Player_States.TACKLED:
                    if (!graph_pState.ToString().ToUpper().StartsWith("TACKLED_"))
                        r = Graphics_Player_States.TACKLED_1;
                    else if (graph_pState == Graphics_Player_States.TACKLED_1)
                        r = Graphics_Player_States.TACKLED_2;
                    else if (graph_pState == Graphics_Player_States.TACKLED_2 ||
                        graph_pState == Graphics_Player_States.TACKLED_3)
                        r = Graphics_Player_States.TACKLED_3;
                    break;
                case Player_States.ON_BACK:
                    r = Graphics_Player_States.ON_BACK;
                    break;
                case Player_States.RUNNING_BACKWORDS:
                     if (graph_pState == Graphics_Player_States.RUNNING_BACKWORDS_1)
                            r = Graphics_Player_States.RUNNING_BACKWORDS_2;
                        else if (graph_pState == Graphics_Player_States.RUNNING_BACKWORDS_2)
                            r = Graphics_Player_States.RUNNING_BACKWORDS_3;
                        else if (graph_pState == Graphics_Player_States.RUNNING_BACKWORDS_3)
                            r = Graphics_Player_States.RUNNING_BACKWORDS_4;
                        else
                            r = Graphics_Player_States.RUNNING_BACKWORDS_1;
                    break;
                case Player_States.KNEELING:
                    if (graph_pState == Graphics_Player_States.ABOUT_TO_CATCH_KICK)
                        r = Graphics_Player_States.KNEELING_1;
                    else if (graph_pState == Graphics_Player_States.KNEELING_1)
                         r = Graphics_Player_States.KNEELING_2;
                    else if (graph_pState == Graphics_Player_States.KNEELING_2)
                          r = Graphics_Player_States.KNEELING_3;
                    break;
                case Player_States.FALL_ON_BALL:
                    if (!graph_pState.ToString().ToUpper().StartsWith("FALL_ON_BALL_"))
                    {
                        r = Graphics_Player_States.FALL_ON_BALL_1;
                    }
                    else if (graph_pState == Graphics_Player_States.FALL_ON_BALL_1)
                        r = Graphics_Player_States.FALL_ON_BALL_2;
                    else
                        r = Graphics_Player_States.FALL_ON_BALL_3;
                    break;
                case Player_States.PUNTER_READY:
                    r = Graphics_Player_States.PUNTER_READY;
                    break;
                case Player_States.PUNTER_RUN:
                    if (graph_pState == Graphics_Player_States.PUNTER_RUN_1)
                        r = Graphics_Player_States.PUNTER_RUN_2;
                    else if (graph_pState == Graphics_Player_States.PUNTER_RUN_2)
                        r = Graphics_Player_States.PUNTER_RUN_3;
                    else if (graph_pState == Graphics_Player_States.PUNTER_RUN_3)
                        r = Graphics_Player_States.PUNTER_RUN_4;
                    else
                        r = Graphics_Player_States.PUNTER_RUN_1;
                    break;
                case Player_States.PUNTER_KICK:
                    if (!graph_pState.ToString().ToUpper().StartsWith("PUNTER_KICK_"))
                        r = Graphics_Player_States.PUNTER_KICK_1;
                    else if (graph_pState == Graphics_Player_States.PUNTER_KICK_1)
                        r = Graphics_Player_States.PUNTER_KICK_2;
                    else if (graph_pState == Graphics_Player_States.PUNTER_KICK_2)
                        r = Graphics_Player_States.PUNTER_KICK_3;
                    break;
                case Player_States.PUNTER_AFTER_KICK:
                    if (!graph_pState.ToString().ToUpper().StartsWith("PUNTER_AFTER_KICK_"))
                        r = Graphics_Player_States.PUNTER_AFTER_KICK_1;
                    else if (graph_pState == Graphics_Player_States.PUNTER_AFTER_KICK_1)
                        r = Graphics_Player_States.PUNTER_AFTER_KICK_2;
                    break;
                case Player_States.BLOCK_KICK:
                    r = Graphics_Player_States.BLOCK_KICK;
                    break;
                case Player_States.CROUCH_BLOCK_DOWN:
                    if (!graph_pState.ToString().ToUpper().StartsWith("CROUCH_BLOCK_"))
                        r = Graphics_Player_States.CROUCH_BLOCK_1;
                    else if (graph_pState.ToString().ToUpper().StartsWith("CROUCH_BLOCK_"))
                        r = Graphics_Player_States.CROUCH_BLOCK_2;
                    break;
                case Player_States.CROUCH_BLOCK_UP:
                    if (graph_pState.ToString().ToUpper().StartsWith("CROUCH_BLOCK_2"))
                        r = Graphics_Player_States.CROUCH_BLOCK_1;
                    else 
                        r = Graphics_Player_States.STANDING;
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

            if (current_action < pStage.Actions.Count())
            {
                Action act = pStage.Actions[current_action];

                pState = (Player_States)act.p_state;
                bPlayerCatchesBall = pStage.Player_Catches_Ball;

                bCarringBall = act.bPossesses_Ball;

                graph_pState = setGraphicsState(pState);

                //Only if there are actions/movements left.
                if (act.PointXY.Count() > 0 &&
                   (current_point < act.PointXY.Count()))
                {
                    YardLine = act.PointXY[current_point].x;
                    Vertical_Percent_Pos = act.PointXY[current_point].y;

                    current_point++;
                    if (current_point >= act.PointXY.Count())
                    {
                        current_action++;
                        current_point = 0;
                    }


                } //if pointxy left
                else
                {
                    if (pStage.Main_Object)
                    {
                        bStageFinished = true;
                    }
                }
            }  //if actions
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
