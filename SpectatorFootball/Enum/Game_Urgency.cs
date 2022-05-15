using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.Enum
{
    //This enum is used by each coach during a game to determine how urgent the game
    //is for them in terms of allowing starter substitutions and being more aggressivve
    //or conservative with their play calling
    public enum Game_Urgency
    {
        EXTREMELY_AGGRESSIVE, //End of game or half and the team need a score
        VERRY_AGGRESSIVE, //team will go for it on a 4th down
        SORT_OF_AGGRESSIVE, //Should be favoring the pass
        NORMAL,  
        SORT_OF_SAFE,  //favor the run
        VERRY_SAFE, //run even on third down
        EXTREMELY_SAFE  //only run to keep the clock going
    }

}
