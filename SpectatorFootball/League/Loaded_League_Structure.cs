using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpectatorFootball.Enum;
using SpectatorFootball.Models;

namespace SpectatorFootball.League
{
    public class Loaded_League_Structure
    {
        public League_State LState { get; set; }
        public int Current_Year{ get; set; }
        public List<Standings_Row> Standings = null;
        public Season season = null;

    }
}
