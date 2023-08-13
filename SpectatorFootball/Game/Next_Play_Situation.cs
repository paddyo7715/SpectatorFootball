using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.GameNS
{
    //Notes
    //For plays such as kickoff, free kick, onside kick, extrap point kick or 1, 2 or 3 yard after touchdown play,
    //the Down and Yards to go will both be 0, because they are meaningless for those plays.  Yardline will
    //be populated.
    //for plays such as pass, rush or FG where down and yards to go have meaning, if a team makes a first down
    //then it will always be 10 yards to go, even if they are on the 5 yardline.  This was I will know to
    //display 1st and Goal, because the yards_to_go will be > than the distance from the goal line.
    class Next_Play_Situation
    {
        public int Down;
        public double Yards_to_Go;
        public double Yardline;
        public bool bTurnover_On_Downs;

        public double Yards_Gained;

        //Only relevant for Penalties
        public bool bHalf_the_distance;
        public double Penalty_Yards;
    }
}
