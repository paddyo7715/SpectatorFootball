using SpectatorFootball.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.GameNS
{
    public class Game_Player
    {
        public Player_States pState;
        public bool bCarringBall;
        public double YardLine;
        public double Vertical_Percent_Pos;

        public void setValues(Player_States pState, bool bCarringBall, double YardLine, double Vertical_Percent_Pos)
        {
            this.pState = pState;
            this.bCarringBall = bCarringBall;
            this.YardLine = YardLine;
            this.Vertical_Percent_Pos = Vertical_Percent_Pos;
        }
    }

}
