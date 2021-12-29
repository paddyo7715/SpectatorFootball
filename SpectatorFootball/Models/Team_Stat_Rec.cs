using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SpectatorFootball.Models
{
    public class Team_Stat_Rec
    {
        public long f_id { get; set; }
        public string Team_Name { get; set; }
        public BitmapImage HelmetImage { get; set; }
        public long Wins { get; set; }
        public long Loses { get; set; }
        public long Ties { get; set; }
        public long PF { get; set; }
        public long PA { get; set; }    
        public string PPG_For { get; set; }
        public string PPG_Against { get; set; }
        public long Passing_Yards_For { get; set; }
        public long Passing_Yards_Against { get; set; }
        public long Rushing_Yards_For { get; set; }
        public long Rushing_Yards_Against { get; set; }
        public long Total_Yards_For { get; set; }
        public long Total_Yards_Against { get; set; }
        public long Turnovers_Comm { get; set; }
        public long Turnovers_Recv { get; set; }
        public long Third_Down_String { get; set; }
        public long Third_Down_Conversions { get; set; }
        public long Third_Down_Conversions_Att { get; set; }
        public long Fourth_Down_String { get; set; }
        public long Fourth_Down_Conversions { get; set; }
        public long Fourth_Down_Conversions_Att { get; set; }

    }
}
