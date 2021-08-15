using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.Models
{
    public class Rushing_Accum_Stats_by_year
    {
        public long Year { get; set; }
        public long Rushes { get; set; }
        public long Yards { get; set; }
        public string Yards_Per_Carry { get; set; }
        public long TDs { get; set; }
        public long Fumbles { get; set; }
        public long Fumbles_Lost { get; set; }
    }
}
