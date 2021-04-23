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
        public string AwayCity { get; set; }
        public string AwayCityAbbr { get; set; }
        public string HomeNickname { get; set; }
        public string HomeCity { get; set; }
        public string HomeCityAbbr { get; set; }
    }
}
