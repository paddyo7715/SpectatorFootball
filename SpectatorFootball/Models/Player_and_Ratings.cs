using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.Models
{
    public class Player_and_Ratings
    {
        public Player p { get; set; }
        public List<Player_Ratings> pr { get; set; }
        public double Overall_Grade { get; set; }
    }
}
