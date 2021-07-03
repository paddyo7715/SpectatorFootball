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
        public List<Passing_Accum_Stats> Passing_Stats { get; set; }
        public List<Rushing_Accum_Stats> Rushing_Stats { get; set; }
        public List<Receiving_Accum_Stats> Receiving_Stats { get; set; }
        public List<Blocking_Accum_Stats> Blocking_Stats { get; set; }
        public List<Defense_Accum_Stats> Defense_Stats { get; set; }
        public List<Pass_Defense_Accum_Stats> Pass_Defense_Stats { get; set; }
        public List<Kicking_Accum_Stats> Kicking_Stats { get; set; }
        public List<Punting_Accum_Stats> Punting_Stats { get; set; }
        public List<KickReturn_Accum_Stats> KickRet_Stats { get; set; }
        public List<PuntReturns_Accum_Stats> PuntRet_Stats { get; set; }
    }
}
