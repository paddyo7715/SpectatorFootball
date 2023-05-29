using SpectatorFootball.Common;
using SpectatorFootball.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SpectatorFootball.GameNS
{
    public class Graphics_Game_Ball
    {
        private double ball_size = 12;
        private const double BASE_BALL_SIZE = 12;



        public Ball_States bState;
        public Graphics_Ball_Stats graph_bState;
        public double YardLine;
        public double Vertical_Percent_Pos;
        public List<Play_Stage> Stages = null;
        public int current_Stage = 0;
        public int current_action = 0;
        public int current_point = 0;
        public double Height;
        public double width;
        public Game_Sounds? Sound;
        public bool bStageFinished = false;
        public bool ThreeDee_ball;

        public Graphics_Game_Ball(Ball_States bState, double YardLine, double Vertical_Percent_Pos,
            List<Play_Stage> Stages, bool ThreeDee_ball)
        {
            this.bState = bState;
            this.YardLine = YardLine;
            this.Vertical_Percent_Pos = Vertical_Percent_Pos;
            this.Stages = Stages;
            this.ThreeDee_ball = ThreeDee_ball;

           graph_bState = setGraphicsState(this.bState);

        }
        private Graphics_Ball_Stats setGraphicsState(Ball_States bState)
        {
            Graphics_Ball_Stats r = Graphics_Ball_Stats.TEED_UP;

            switch (bState)
            {
                case Ball_States.TEED_UP:
                    Height = ball_size;
                    width = ball_size;
                    r = Graphics_Ball_Stats.TEED_UP;
                    break;
                case Ball_States.CARRIED:
                    Height = 0;
                    width = 0;
                    r = Graphics_Ball_Stats.CARRIED;
                    break;
                case Ball_States.END_OVER_END:
                    if (graph_bState != Graphics_Ball_Stats.END_OVER_END_1)
                    {
                        Height = ball_size;
                        width = (int)(ball_size * 1.75);
                        r = Graphics_Ball_Stats.END_OVER_END_1;
                    }
                    else
                    {
                        Height = ball_size;
                        width = ball_size;
                        r = Graphics_Ball_Stats.END_OVER_END_2;
                    }
                    break;
                case Ball_States.ON_THE_GROUND:
                    Height = ball_size;
                    width = ball_size * 2;
                    r = Graphics_Ball_Stats.ON_GROUND;
                    break;
                case Ball_States.SPIRAL:
                    Height = ball_size;
                    width = ball_size * 2;
                    if (graph_bState != Graphics_Ball_Stats.SPIRAL_2)
                        r = Graphics_Ball_Stats.SPIRAL_1;
                    else
                        r = Graphics_Ball_Stats.SPIRAL_2;
                    break;
                case Ball_States.BOUNCING:
                    if (graph_bState != Graphics_Ball_Stats.BOUNCING_1)
                    {
                        r = Graphics_Ball_Stats.BOUNCING_1;
                        Height = BASE_BALL_SIZE;
                        width = BASE_BALL_SIZE * 1.25;
                    }
                    else
                    {
                        r = Graphics_Ball_Stats.BOUNCING_2;
                        Height = BASE_BALL_SIZE * 1.25;
                        width = BASE_BALL_SIZE * 1.65;
                    }
                    break;
                case Ball_States.ROLLING:
                    Height = BASE_BALL_SIZE * 1.2;
                    width = BASE_BALL_SIZE * 0.90;
                    if (graph_bState != Graphics_Ball_Stats.ROLLING_1)
                        r = Graphics_Ball_Stats.ROLLING_1;
                    else
                        r = Graphics_Ball_Stats.ROLLING_2;
                    break;
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

                bState = (Ball_States)act.b_state;
                graph_bState = setGraphicsState(bState);

                //Only if there are actions/movements left.
                if (act.PointXY.Count() > 0 &&
                   (current_point < act.PointXY.Count()))
                {
                    YardLine = act.PointXY[current_point].x;
                    Vertical_Percent_Pos = act.PointXY[current_point].y;

                    //set the size of the bll
                    if (ThreeDee_ball)
                        setBallSize(current_point, act.PointXY.Count());

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

                    //                   graph_pState = setGraphicsState(pState);

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

        private int setBallSize(int current_point, int totPoints)
        {
            int r = 1;

            if (current_point <= (totPoints / 2))
                ball_size += 1;
            else
                ball_size -= 1;

            if (current_point + 1 == totPoints)
                ball_size = BASE_BALL_SIZE;

            return r;
        }
       
    }
}
