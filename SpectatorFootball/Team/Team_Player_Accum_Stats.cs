using SpectatorFootball.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.Team
{
    public class Team_Player_Accum_Stats
    {
        public List<Passing_Accum_Stats_by_year> Passing_Stats { get; set; }
        public List<Rushing_Accum_Stats_by_year> Rushing_Stats { get; set; }
        public List<Receiving_Accum_Stats_by_year> Receiving_Stats { get; set; }
        public List<Blocking_Accum_Stats_by_year> Blocking_Stats { get; set; }
        public List<Defense_Accum_Stats_by_year> Defense_Stats { get; set; }
        public List<Pass_Defense_Accum_Stats_by_year> Pass_Defense_Stats { get; set; }
        public List<Kicking_Accum_Stats_by_year> Kicking_Stats { get; set; }
        public List<Punting_Accum_Stats_by_year> Punting_Stats { get; set; }
        public List<KickReturn_Accum_Stats_by_year> KickRet_Stats { get; set; }
        public List<PuntReturns_Accum_Stats_by_year> PuntRet_Stats { get; set; }
    }
}
