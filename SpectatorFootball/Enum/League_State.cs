using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.Enum
{
//This enum is used for a loaded league and holds the state of the league, so that it can be determined
//which menu items to enable and disable.
    public enum League_State
    {
        Season_Started, //New season, but draft has not started.
        Draft_Started,  //The draft has started.
        Draft_Completed,  //The draft has completed.
        FreeAgency_Started, //Free Agency has started but not yet completed
        FreeAgency_Completed,  //Free Agency has completed
        Training_Camp_Ended,  //Training camp has been completed.
        Regular_Season_in_Progress, //Training camp has ended and the regular season has not yet ended.
        Regular_Season_Ended,  //Every game in the regular season has ended.  Assing playoff teams.
        Playoffs_In_Progress,  //zero or not all playoff games are played includding championship game.
        Playoffs_Ended,  //Chanpionship game has been completed.
        Season_Ended,  //Year end awards have been completed.
        Previous_Year  //year end awards have been given out and the only thing to do is to start the new season.
    }

}
