using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.Models
{
    public class Blocking_Accum_Stats
    {
        public Player p { get; set; }
        public long Plays { get; set; }
        public long Pancakes { get; set; }
        public long Sacks_Allowed { get; set; }
        public long Pressures_Allowed { get; set; }
    }
}
