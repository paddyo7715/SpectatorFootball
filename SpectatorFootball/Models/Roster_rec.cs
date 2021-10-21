using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.Models
{
    public class Roster_rec
    {
        public Player p { get; set; }
        public Player_Ratings pr { get; set; }
        public Players_By_Team pbt { get; set; }
    }
}
