using SpectatorFootball.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.DraftNS
{
    //This class represents a single positional draft need.  When a team is on the clock to
    //select a draft pick, they will have one of these records which will list positions that
    //they are interested in (in order) and positions that they do not want to pick.
    public class Draft_Need
    {
        public List<Player_Pos> Wanted_Positions { get; set; }

        public List<Player_Pos> Unwanted_Positions  { get; set; }

    }
}
