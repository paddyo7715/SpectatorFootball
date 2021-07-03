using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.Models
{
    public class Receiving_Accum_Stats
    {
        public Player p { get; set; }
        public long Catches { get; set; }
        public long Yards { get; set; }
        public long Drops { get; set; }
        public string Yards_Per_Catch { get; set; }
        public long TDs { get; set; }
        public long Fumbles { get; set; }
        public long Fumbles_Lost { get; set; }
    }
}
