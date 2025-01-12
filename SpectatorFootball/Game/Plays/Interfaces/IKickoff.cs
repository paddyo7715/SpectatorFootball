using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.GameNS
{
    public interface IKickoff
    {
        double kickoff_yl { get; set; }
        double touchback_yl { get; set; }
    }
}
