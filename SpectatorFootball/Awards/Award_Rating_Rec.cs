using SpectatorFootball.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.Awards
{
    class Award_Rating_Rec
    {
        public Player p { get; set; }
        public long Grade { get; set; }
        public bool isRookie { get; set; }
    }
}
