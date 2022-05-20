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
        public int Away_Score;
        public int Home_Score;
        public string Display_QTR;
        public string Display_Time;

        //Second to delay before the next play.  Used to show things such as
        //stats inbetween halves.
        public int Delay_seconds;

        public string Short_Message;
        public string Long_Message;

        public Play_Package Offensive_Package = null;
        public Formations_Enum Defensive_Formation;

        List<string> Player_Movements = null;
/*      Player Movements Structure
        if it is null then there are mo player movements, example beginning of game or half


    */

    }
}
