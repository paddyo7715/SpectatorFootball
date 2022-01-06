using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SpectatorFootball.Models
{
    public class League_Injuries
    {
        public string helmet_filename { get; set; }
        public BitmapImage HelmetImage { get; set; }
        public string Team_Name { get; set; }
        public string Pick_Pos_Name { get; set; }
        public long Player_ID { get; set; }
        public Injury Injury { get; set; }
    }
}
