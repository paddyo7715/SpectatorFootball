using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.Models
{
    public class BoxScore
    {
        public Game Game { get; set; }
        public string AwayNickname { get; set; }
        public Teams_by_Season aTeam { get; set; }
        public Teams_by_Season hTeam { get; set; }
    }
}
