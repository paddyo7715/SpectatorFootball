using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SpectatorFootball.Models
{
    public class Pass_Defense_Accum_Stats_by_year
    {
        public Player p { get; set; }
        public bool isRookie { get; set; }
        public long f_id { get; set; }
        public string City_Abbr { get; set; }
        public BitmapImage HelmetImage { get; set; }
        public long Year { get; set; }
        public long Pos { get; set; }
        public string Team { get; set; }
        public long Pass_Defenses { get; set; }
        public long Ints { get; set; }
        public long Forced_Fumble { get; set; }
        public long Def_int_TDs { get; set; }
        public long Tackles { get; set; }
        public long Missed_Tackles { get; set; }
    }
}
