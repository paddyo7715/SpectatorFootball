using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SpectatorFootball.Models
{
    public class PuntReturns_Accum_Stats_by_year
    {
        public Player p { get; set; }
        public bool isRookie { get; set; }
        public long f_id { get; set; }
        public string City_Abbr { get; set; }
        public BitmapImage HelmetImage { get; set; }
        public long Year { get; set; }
        public long Pos { get; set; }
        public string Team { get; set; }
        public long Returns { get; set; }
        public long Yards { get; set; }
        public string Yards_avg { get; set; }
        public long Yards_Long { get; set; }
        public long TDs { get; set; }
        public long Fumbles { get; set; }
    }
}
