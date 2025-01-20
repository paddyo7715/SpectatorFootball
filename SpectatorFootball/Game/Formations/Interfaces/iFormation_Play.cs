using SpectatorFootball.Enum;
using SpectatorFootball.GameNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.GameNS
{
    public interface iFormation_Play
    {
        Formation getFormation(Formations_Enum fe, double PossessionAdjuster);
    }
}
