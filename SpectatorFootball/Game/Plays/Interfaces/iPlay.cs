using SpectatorFootball.Enum;
using SpectatorFootball.GameNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.GameNS
{
    public interface iPlay
    {
        Play_Enum Play { get; set; }
        Play_Result getPlayResult();
        Play_Result Execute(bool bPreSnapPenalty);
        bool isPreSnapPenalty_Eligible();
        bool isAccumeStats();

    }
}
