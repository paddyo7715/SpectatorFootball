using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.League
{
    public class Standing_Streak
    {
        public long team_id { get; set; }
        public string Result { get; set; }
        public long week { get; set; }
        public long Games { get; set; }

    }
}
