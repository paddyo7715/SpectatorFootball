using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.Enum
{
    //These player states are NOT for the graphics engine.  The are the states
    //that a player can be in from the game.  Example: if a player is in the
    //running_forward state then they alertenate between running 1 and 2.
    public enum Player_States
    {
        STN,  //STANDING
        RUNF, //RUNNING_FORWARD
        FGK, //FG_KICK
        CTCHK,  //ABOUT_TO_CATCH_KICK
        BLK,  //BLOCKING
        RUNU, //RUNNING_UP 
        RUND,  //RUNNING_DOWN
        TACKL,  //TACKLING
        TACKLD,  //TACKLED
        ONBK,  //ON_BACK
        RUNB  //RUNNING_BACKWORDS
    }
}
