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
        private const int BALL_SIZE = 12;

        public Ball_States bState;
        public Graphics_Ball_Stats graph_bState;
        public double YardLine;
        public double Vertical_Percent_Pos;
        public List<Play_Stage> Stages = null;
        public int current_Stage = 0;
        public int current_action = 0;
        public int current_point = 0;
        public int Height;
        public int width;
        public Game_Sounds? Before_Sound;
        public Game_Sounds? After_Sound;
        public bool bStageFinished = false;

        public Graphics_Game_Ball(Ball_States bState, double YardLine, double Vertical_Percent_Pos,
            List<Play_Stage> Stages)
        {
            this.bState = bState;
            this.YardLine = YardLine;
            this.Vertical_Percent_Pos = Vertical_Percent_Pos;
            this.Stages = Stages;

            graph_bState = setGraphicsState(this.bState);

        }
        private Graphics_Ball_Stats setGraphicsState(Ball_States bState)
        {
            Graphics_Ball_Stats r = Graphics_Ball_Stats.TEED_UP;

            switch (bState)
            {
                case Ball_States.TEED_UP:
                    Height = BALL_SIZE;
                    width = BALL_SIZE;
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
                        Height = BALL_SIZE;
                        width = BALL_SIZE * 2;
                        r = Graphics_Ball_Stats.END_OVER_END_1;
                    }
                    else
                    {
                        Height = BALL_SIZE;
                        width = BALL_SIZE;
                        r = Graphics_Ball_Stats.END_OVER_END_2;
                    }
                    break;
                case Ball_States.ON_THE_GROUND:
                    Height = BALL_SIZE;
                    width = BALL_SIZE * 2;
                    r = Graphics_Ball_Stats.ON_GROUND;
                    break;
                case Ball_States.SPIRAL:
                    Height = BALL_SIZE;
                    width = BALL_SIZE * 2;
                    if (graph_bState != Graphics_Ball_Stats.SPIRAL_1)
                        r = Graphics_Ball_Stats.SPIRAL_1;
                    else
                        r = Graphics_Ball_Stats.SPIRAL_2;
                    break;
            }

            return r;
        }

        public void Update()
        {
            Before_Sound = null;
            After_Sound = null;
            Play_Stage pStage = Stages[current_Stage];

            //This must never be true
            if (pStage.Main_Object && pStage.Actions.Count() == 0)
                throw new Exception("Cant be main object with no actions");

            //Only if there are actions/movements left.
            if (pStage.Actions[current_action].PointXY.Count() > 0 &&
                current_action < pStage.Actions.Count() &&
               (current_point < pStage.Actions[current_action].PointXY.Count()))
            {

                Action act = pStage.Actions[current_action];

                bState = (Ball_States)act.b_state;
                graph_bState = setGraphicsState(bState);
                YardLine = act.PointXY[current_point].x;
                Vertical_Percent_Pos = act.PointXY[current_point].y;


                Before_Sound = act.Before_Sound;
                After_Sound = act.After_Sound;

                current_point++;
                if (current_point >= act.PointXY.Count())
                {
                    current_action++;
                    current_point = 0;
                }

                //If this is the main object in the stage and all of its actions are over then the stage is done
                if (pStage.Main_Object &&
                    (current_action >= pStage.Actions.Count()) &&
                    (current_point >= pStage.Actions[current_action].PointXY.Count()))
                {
                    bStageFinished = true;
                }

            } //if actions
            else
                graph_bState = setGraphicsState(bState);


        }

    }
}
