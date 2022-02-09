using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SpectatorFootball.Models
{
    public class Blocking_Accum_Stats_by_year
    {
        public Player p { get; set; }
        public bool isRookie { get; set; }
        public long f_id { get; set; }
        public string City_Abbr { get; set; }
        public BitmapImage HelmetImage { get; set; }
        public long Year { get; set; }
        public long Pos { get; set; }
        public string Team { get; set; }
        public long Plays { get; set; }
        public long Pancakes { get; set; }
        public long Sacks_Allowed { get; set; }
        public long Pressures_Allowed { get; set; }
        public long Rushing_Loss_Allowed { get; set; }
    }
}
