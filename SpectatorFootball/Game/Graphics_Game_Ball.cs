using SpectatorFootball.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.GameNS
{
    public class Graphics_Game_Ball
    {
        public Ball_States bState;
        public double YardLine;
        public double Vertical_Percent_Pos;
        public double end_YardLine;
        public double end_Vertical_Percent_Pos;
        bool main_object;
        List<string> movements = new List<string>();
        string pre_movement = null;
        int current_movement = 0;
        bool bFinished = false;

        public double Height;
        public double width;

        public void setValues(Ball_States bState, double YardLine, double Vertical_Percent_Pos)
        {
            this.bState = bState;
            this.YardLine = YardLine;
            this.Vertical_Percent_Pos = Vertical_Percent_Pos;
        }
        public void isEnd_Movement()
        {

            if (YardLine >= end_YardLine && Vertical_Percent_Pos >= end_Vertical_Percent_Pos)
            {
                if (current_movement >= movements.Count - 1)
                    bFinished = true;
                {
                    bFinished = false;
                    current_movement++;
                }
            }

        }
    }
}
