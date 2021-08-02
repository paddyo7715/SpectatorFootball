using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.Models
{
    public class TeamStatsRaw
    {
        public long Team_Points { get; set; }
        public long Team_First_Downs { get; set; }
        public long Team_Third_Downs_Made { get; set; }
        public long Team_Third_Downs_Att { get; set; }
        public long Team_Fourth_Downs_Made { get; set; }
        public long Team_Fourth_Downs_Att { get; set; }
        public long Team_Rushing_Yards { get; set; }
        public long Team_Passing_Yards { get; set; }
        public long Team_Sacks { get; set; }
        public long Team_Turnovers { get; set; }
        public long Opp_Points { get; set; }
        public long Opp_First_Downs { get; set; }
        public long Opp_Third_Downs_Made { get; set; }
        public long Opp_Third_Downs_Att { get; set; }
        public long Opp_Fourth_Downs_Made { get; set; }
        public long Opp_Fourth_Downs_Att { get; set; }
        public long Opp_Rushing_Yards { get; set; }
        public long Opp_Passing_Yards { get; set; }
        public long Opp_Sacks { get; set; }
        public long Opp_Turnovers { get; set; }

    }
}
