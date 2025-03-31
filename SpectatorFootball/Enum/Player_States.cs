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
        RUNNING_UP,
        RUNNING_DOWN,
        RUNNING_BACKWORDS,
        RUNNING_SLOW_FORWARD,
        RUNNING_SLOW_UP,
        RUNNING_SLOW_DOWN,
        RUNNING_SLOW_BACKWORDS,
        FG_KICK, 
        ABOUT_TO_CATCH_KICK,  
        BLOCKING,  
        TACKLING,  
        TACKLED,  
        ON_BACK,  
        KNEELING,
        FALL_ON_BALL,
        PUNTER_READY,
        PUNTER_RUN,
        PUNTER_KICK,
        PUNTER_AFTER_KICK,
        BLOCK_KICK,
        CROUCH_BLOCK_DOWN,
        CROUCH_BLOCK_UP,
        FG_HOLDER_READY,
        FG_HOLDER_PLACE_BALL
    }
}
