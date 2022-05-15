using SpectatorFootball.Enum;
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
        public long player_id { get; set; }

    }
}
