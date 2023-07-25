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
        public Penalty_Play_Types Penalty_Play_Type { get; set; }
        public double Yards { get; set; }
        public bool bDeclinable { get; set; }
        public bool bAuto_FirstDown { get; set; }
        public bool bSpot_Found { get; set; }
        public string Description { get; set; }

        public List<Player_Action_Stats> Player_Action_States = new List<Player_Action_Stats>();
    }
}
