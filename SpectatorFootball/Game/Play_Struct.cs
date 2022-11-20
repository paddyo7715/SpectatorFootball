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
        public string Before_Away_Score;
        public string Before_Home_Score;
        public string Before_Display_QTR;
        public string Before_Display_Time;
        public string Before_Down_and_Yards;
        public string Before_Away_Timeouts;
        public string Before_Home_Timeouts;
        public bool bStartQTR;
        public Ball_States Initial_Ball_State;

        //These are from after the play is executed
        public bool bGameOver;
        public bool bForfeitedGame;
        public string After_Away_Score;
        public string After_Home_Score;
        public string After_Time;
        public string After_QTR;
        public string After_Down_and_Yards;
        public string After_Away_Timeouts;
        public string After_Home_Timeouts;
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

        public string Before_Snap = null;
        public List<string> Play_Stages = null;
    }
}
