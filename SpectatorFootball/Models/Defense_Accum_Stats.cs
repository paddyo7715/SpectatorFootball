using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.Models
{
    public class Defense_Accum_Stats
    {
        public Player p { get; set; }
        public long Plays { get; set; }
        public long Tackles { get; set; }
        public long Sacks { get; set; }
        public long Pressures { get; set; }
        public long Run_for_Loss { get; set; }
        public long Forced_Fumble { get; set; }
    }
}
