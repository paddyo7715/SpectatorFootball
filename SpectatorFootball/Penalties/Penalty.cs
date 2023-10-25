using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpectatorFootball.Enum;

namespace SpectatorFootball.PenaltiesNS
{
    public class Penalty
    {
        public Penalty_Codes code { get; set; }

        public List<Play_Enum> Penalty_Play_Types = new List<Play_Enum>();
        public int Yards { get; set; }
        public bool bDeclinable { get; set; }
        public Play_Snap_Timing Play_Timing { get; set; }
        public bool bAuto_FirstDown { get; set; }
        public bool bSpot_Foul { get; set; }
        public string Description { get; set; }
        public int Frequency_Rating { get; set; }

        public List<Player_Action_State> Player_Action_States = new List<Player_Action_State>();
    }
}
