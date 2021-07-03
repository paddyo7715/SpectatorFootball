using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.Models
{
    public class KickReturn_Accum_Stats
    {
        public Player p { get; set; }
        public long Returns { get; set; }
        public long Yards { get; set; }
        public string Yards_avg { get; set; }
        public long Yards_Long { get; set; }
        public long TDs { get; set; }
        public long Fumbles { get; set; }
    }
}
