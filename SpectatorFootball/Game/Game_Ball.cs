using SpectatorFootball.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.GameNS
{
    public class Game_Ball
    {
        public Ball_States bState;
        public double YardLine;
        public double Vertical_Percent_Pos;

        public void setGraphicsProps(Ball_States bState, double YardLine, double Vertical_Percent_Pos)
        {
            this.bState = bState;
            this.YardLine = YardLine;
            this.Vertical_Percent_Pos = Vertical_Percent_Pos;
        }

        public Graphics_Object_Properties getGraphicsProps()
        {
            return new Graphics_Object_Properties()
            {
                bState = this.bState,
                Vertical_Percent_Pos = this.Vertical_Percent_Pos,
                YardLine = this.YardLine
            };
        }

    }
}
