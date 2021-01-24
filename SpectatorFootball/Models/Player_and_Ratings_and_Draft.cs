using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.Models
{
    class Player_and_Ratings_and_Draft
    {
        public Player p { get; set; }
        public List<Player_Ratings> pr { get; set; }
        public double Overall_Grade { get; set; }
        public List<Draft> d { get; set; }
        public int Grade { get; set; }
        public bool bMadeTeam { get; set; }
    }
}
