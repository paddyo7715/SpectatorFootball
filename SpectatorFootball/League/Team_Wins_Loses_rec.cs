using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.League
{
    class Team_Wins_Loses_rec
    {
        public long Div_Num { get; set; }
        public long Conf_Num { get; set; }
        public long Franchise_ID { get; set; }
        public string Team_Name { get; set; }
        public long wins { get; set; }
        public long loses { get; set; }
        public long ties { get; set; }
        public long winpct { get; set; }
        public long pointsfor { get; set; }
        public long pointagainst { get; set; }
        public long Random_Number { get; set; }
    }
}
