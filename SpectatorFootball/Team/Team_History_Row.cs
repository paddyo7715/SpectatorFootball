using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.Team
{
    public class Team_History_Row
    {
        public long Year { get; set; }
        public string reg_wins { get; set; }
        public string reg_loses { get; set; }
        public string reg_ties { get; set; }
        public string reg_PF { get; set; }
        public string reg_PA { get; set; }
        public string Blank { get; set; }  //Can't believe I have to do this.
        public string play_wins { get; set; }
        public string play_loses { get; set; }
        public string play_PF { get; set; }
        public string play_PA { get; set; }
        public long? champ_PF { get; set; }
        public long? champ_PA { get; set; }
        public string champ_result { get; set; }


    }
}
