using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SpectatorFootball.Models
{
    class DraftPick
    {
        public long Pick_no { get; set; }
        public long Round { get; set; }
        public string helmet_filename { get; set; }
        public BitmapImage HelmetImage { get; set; }
        public string Team_Name { get; set; }
        public string Pick_Pos_Name { get; set; }
        public long Season_ID { get; set; }
        public long ID { get; set; }
        public long? Franchise_ID { get; set; }
        public string Home_Jersey_Color { get; set; }
        public string Home_Jersey_Number_Color { get; set; }
        public string Home_Jersey_Outline_Color { get; set; }
        public string Helmet_Color { get; set; }
        public string Helmet_Logo_Color { get; set; }


    }
}
