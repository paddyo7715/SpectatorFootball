using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SpectatorFootball.Playoffs
{
    public class Playoff_Bracket_rec
    {
        public long game_id { get; set; }
        public string Panel_name { get; set; }
        public BitmapImage Away_HelmetImage { get; set; }
        public string Away_city { get; set; }
        public string Away_Score { get; set; }
        public string Home_helmet_filename { get; set; }
        public BitmapImage Home_HelmetImage { get; set; }
        public string Home_city { get; set; }
        public string Home_Score { get; set; }
    }
}
