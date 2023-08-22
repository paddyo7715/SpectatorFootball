using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.Enum
{
    public enum Player_Action_State
    {
        PAS, // Passer
        PC, //Pass catcher
        BRN, //Ball Runner
        PB, //Pass blocker
        PAR, //Pass Rusher
        PD, //Pass Defender
        RB, //Run Blocker
        RD, //Run Defenders
        K, //kicker
        KR, //kick retuner
        KRT, //Kick return team (not kick returner)
        KDT, //Kick defense teamm (not the kicker)
        P, //Punter
        PR, //Punt retuner
        PRT, //Punt return team (not kick returner)
        PDT, //Punt defense teamm (not the punter)
        FGT, //Field goal kicking team not Kicker
        FGD, //Feild goal defense
    }
}
