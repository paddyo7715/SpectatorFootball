using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.Models
{
    class Player_and_Ratings_and_Draft
    {
        public Players_By_Team p { get; set; }
        public List<Player_Ratings> pr { get; set; }
        public Player player { get; set; }
        public bool bJust_Drafted { get; set; }
        public double Overall_Grade { get; set; }
        public double Grade { get; set; }
        public bool bMadeTeam { get; set; }
    }
}
