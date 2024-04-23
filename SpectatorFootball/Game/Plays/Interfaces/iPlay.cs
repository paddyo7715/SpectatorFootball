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

        void init();

        Play_Result Execute();
        bool isPreSnapPenalty_Eligible();
        bool isAccumeStats();

    }
}
