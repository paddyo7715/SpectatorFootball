using OxyPlot;
using SpectatorFootball.Common;
using SpectatorFootball.Models;
using SpectatorFootball.Team;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SpectatorFootball.PlayerNS
{
    public class Player_Card_Data
    {
        public Teams_by_Season team { get; set; }
        public Player Player { get; set; }
        public string Draft_Info { get; set; }
        public Team_Player_Accum_Stats_by_year Regular_Season_Stats { get; set; }
        public Team_Player_Accum_Stats_by_year Playoff_Stats { get; set; }
        public List<Two_Coll_List> Player_Awards { get; set; }
        public List<Player_Ratings> Player_Ratings { get; set; }
        public PlotModel model { get; set; }
        public List<Two_Coll_List> Awards { get; set; }
        public List<Long_and_String> Teams { get; set; }
    }
}
