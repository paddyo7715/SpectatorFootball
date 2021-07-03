using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.Models
{
    public class Punting_Accum_Stats
    {
        public Player p { get; set; }
        public long Punts { get; set; }
        public long Yards { get; set; }
        public string Punt_AVG { get; set; }
        public long Coffin_Corners { get; set; }
    }
}
