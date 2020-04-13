using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpectatorFootball.Models;

namespace SpectatorFootball.League
{
    public class Loaded_League_Structure
    {
        public DBVersion DBVersion { get; set; }
        public SpectatorFootball.Models.League League { get; set; }

    }
}
