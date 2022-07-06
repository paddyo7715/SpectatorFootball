using SpectatorFootball.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.GameNS
{
    public class Play_Struct
    {
        //Before the play is executed
        public long Away_Score;
        public long Home_Score;
        public string Display_QTR;
        public string Display_Time;
        public string Down_and_Yards;
        public int Away_Timeouts;
        public int Home_Timeouts;
        public bool bStartQTR;
        public Ball_States Initial_Ball_State;

        //These are from after the play is executed
        public bool bGameOver;
        public long After_Away_Score;
        public long After_Home_Score;
        public string After_Display_Time;
        public bool bLefttoRight;
        //Second to delay before the next play.  Used to show things such as
        //stats inbetween halves.
        public int Delay_seconds;

        public string Short_Message;
        public string Long_Message;

        public Double Line_of_Scimmage;
        public Double Vertical_Ball_Placement;

        public Play_Package Offensive_Package = null;
        public Formation Defensive_Formation;

        List<string> Player_Movements = null;
    }
}
