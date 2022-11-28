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
        STANDING,  
        RUNNING_FORWARD, 
        FG_KICK, 
        ABOUT_TO_CATCH_KICK,  
        BLOCKING,  
        RUNNING_UP,  
        RUNNING_DOWN,  
        TACKLING,  
        TACKLED,  
        ON_BACK,  
        RUNNING_BACKWORDS  
    }
}
