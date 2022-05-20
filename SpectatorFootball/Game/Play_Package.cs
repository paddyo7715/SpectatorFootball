using SpectatorFootball.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.GameNS
{
    public class Play_Package
    {
        public List<Formation_Rec> Formation { get; set; }
        public Formations_Enum Formation_Name { get; set; }
        public Play_Enum  Play { get; set; }
    }
}
