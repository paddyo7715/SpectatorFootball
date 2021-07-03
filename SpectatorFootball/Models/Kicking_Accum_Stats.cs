using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.Models
{
    public class Kicking_Accum_Stats
    {
        public Player p { get; set; }
        public long FG_ATT { get; set; }
        public long FG_Made { get; set; }
        public string FG_Percent { get; set; }
        public long FG_Long { get; set; }
        public long XP_ATT { get; set; }
        public long XP_Made { get; set; }
    }
}
