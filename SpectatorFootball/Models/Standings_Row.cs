using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace SpectatorFootball.League
{
    public class Standings_Row
    {
        public long Div_Num { get; set; }
        public long Team_ID { get; set; }
        public long Franchise_ID { get; set; }
        public string Helmet_img { get; set; }
        public BitmapImage HelmetImage { get; set; }
        public string Team_Name { get; set; }
        public string clinch_char { get; set; }
        public long wins { get; set; }
        public long loses { get; set; }
        public long ties { get; set; }
        public long winpct { get; set; }
        public long pointsfor { get; set; }
        public long pointagainst { get; set; }
        public string Streakchar { get; set; }





    }

}
