using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.Models
{
    public class Pass_Defense_Accum_Stats_by_year
    {
        public long Year { get; set; }
        public long Pos { get; set; }
        public string Team { get; set; }
        public long Pass_Defenses { get; set; }
        public long Ints { get; set; }
        public long TDs_Surrendered { get; set; }
        public long Forced_Fumble { get; set; }
    }
}
