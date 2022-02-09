using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SpectatorFootball.Models
{
    public class Receiving_Accum_Stats_by_year
    {
        public Player p { get; set; }
        public bool isRookie { get; set; }
        public long f_id { get; set; }
        public string City_Abbr { get; set; }
        public BitmapImage HelmetImage { get; set; }
        public long Year { get; set; }
        public long Pos { get; set; }
        public string Team { get; set; }
        public long Catches { get; set; }
        public long Yards { get; set; }
        public long Drops { get; set; }
        public string Yards_Per_Catch { get; set; }
        public long TDs { get; set; }
        public long Fumbles { get; set; }
        public long Fumbles_Lost { get; set; }
    }
}
