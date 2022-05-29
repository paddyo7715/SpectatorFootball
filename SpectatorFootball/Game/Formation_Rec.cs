using SpectatorFootball.Enum;
using SpectatorFootball.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.GameNS
{
    public class Formation_Rec
    {
        public Player_Pos Pos { get; set; }
        public int YardLine { get; set; }
        public int Vertical_Percent_Pos { get; set; }
        public bool bSpecialTeams { get; set; }
        public Player_and_Ratings p_and_r { get; set; }

    }
}
