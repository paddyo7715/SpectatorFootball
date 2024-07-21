using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpectatorFootball.Enum;

namespace SpectatorFootball.GameNS
{
    public class Formation
    {
        public string Name { get; set; }

        public Formations_Enum f_enum;

        public Ball_States bState;
        public bool bSpecialTeams { get; set; }
        public List<Formation_Rec> Player_list { get; set; }

        //Special player slot variables
        public int? KickerIndex { get; set; }
        public int? ReturnerIndex { get; set; }
        public List<int> Line_Players { get; set; }
        public List<int> Backfield_Players { get; set; }
        public List<int> Gunners { get; set; }
        public double? Punter_Behind_Line_ayrds { get; set; }
    }
}
