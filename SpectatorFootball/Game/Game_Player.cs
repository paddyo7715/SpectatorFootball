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
        public double end_YardLine;
        public double end_Vertical_Percent_Pos;
        string pre_movement = null;
        bool main_object;
        List<string> movements = new List<string>();
        int current_movement = 0;
        bool bFinished = false;

        public void setValues(Player_States pState, bool bCarringBall, double YardLine, int Vertical_Percent_Pos)
        {
            this.pState = pState;
            this.bCarringBall = bCarringBall;
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
