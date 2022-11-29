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
        private int current_Stage = 0;
        private int current_size = 0;

        private int current_Action = 0;
        public bool bFinished = false;
        public int Height;
        public int width;
        public string sSound = null;
        public Ellipse Ball = null;

        public Graphics_Game_Ball(Ball_States bState, double YardLine, double Vertical_Percent_Pos,
            List<Play_Stage> Stages, Ellipse Ball, string ball_Color)
        {
            this.bState = bState;
            this.YardLine = YardLine;
            this.Vertical_Percent_Pos = Vertical_Percent_Pos;
            this.Stages = Stages;
            this.Ball = Ball;

            current_size = BALL_SIZE;
            graph_bState = setGraphicsState();

            Ball.Width = width;
            Ball.Height = Height;
            Ball.Fill = (Brush)CommonUtils.getBrushfromHex(ball_Color);
            Ball.Stroke = System.Windows.Media.Brushes.Black;

        }
        private Graphics_Ball_Stats setGraphicsState()
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

        public void isEnd_Movement()
        {

/*            if (YardLine >= end_YardLine && Vertical_Percent_Pos >= end_Vertical_Percent_Pos)
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
