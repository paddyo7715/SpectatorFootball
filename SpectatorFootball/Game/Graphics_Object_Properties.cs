using SpectatorFootball.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.GameNS
{
    public class Graphics_Object_Properties
    {
        public Ball_States bState { get; set; }
        public Player_States pState { get; set; }
        public double YardLine { get; set; }
        public double Vertical_Percent_Pos { get; set; }
    }
}
