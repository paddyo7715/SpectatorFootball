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

        List<string> Player_Pre_Movements = null;

        public Ball_States bState;
        public bool bSpecialTeams { get; set; }
        public List<Formation_Rec> Player_list { get; set; }
    }
}
