using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SpectatorFootball.Models
{
    public class WeeklyScheduleRec
    {

        public long Game_ID { get; set; }
        public string Status { get; set; }
        public bool Game_Complete { get; set; }
        public string Away_Team_Name { get; set; }
        public string Away_helmet_filename { get; set; }
        public BitmapImage Away_HelmetImage { get; set; }
        public string Away_Score { get; set; }
        public string Home_Score { get; set; }
        public string Home_helmet_filename { get; set; }
        public BitmapImage Home_HelmetImage { get; set; }
        public string Home_Team_Name { get; set; }
        public long? QTR { get; set; }
        public long? QTR_Time { get; set; }
        public string Action { get; set; }
        public long iWeek { get; set; }


    }
}
