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
        Season_Started,
        Draft_Completed,
        Training_Camp_Completed,
        Regular_Season_in_Progress,
        Playoffs_In_Progress,
        Playoffs_Ended,
        Awards_Completed,
        Previous_Year
    }

}
