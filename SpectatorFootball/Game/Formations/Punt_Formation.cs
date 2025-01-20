using SpectatorFootball.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.GameNS
{
    public class Punt_Formation : iFormation_Play
    {
        public Formation getFormation(Formations_Enum fe, double PossessionAdjuster)
        {
            List<Formation_Rec> r = new List<Formation_Rec>();
            Formation f = new Formation();
            f.Player_list = new List<Formation_Rec>();

            f.Name = "Punt";
            f.f_enum = fe;
            f.bSpecialTeams = true;
            f.KickerIndex = 5;
            f.Punter_Behind_Line_ayrds = 12.0;
            f.Line_Players = new List<int>() { 1, 2, 3, 8, 9 };
            f.Backfield_Players = new List<int>() { 4, 6, 7 };
            f.Gunners = new List<int>() { 0, 10 };
            f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.WR, YardLine = (-1.0 * PossessionAdjuster), Vertical_Percent_Pos = 23, State = Player_States.STANDING });
            f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.OL, YardLine = (-1.0 * PossessionAdjuster), Vertical_Percent_Pos = 39, State = Player_States.STANDING });
            f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.OL, YardLine = (-1.0 * PossessionAdjuster), Vertical_Percent_Pos = 44, State = Player_States.STANDING });
            f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.OL, YardLine = (-1.0 * PossessionAdjuster), Vertical_Percent_Pos = 49, State = Player_States.STANDING });
            f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.DB, YardLine = (-2.5 * PossessionAdjuster), Vertical_Percent_Pos = 45, State = Player_States.STANDING });
            f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.P, YardLine = (-12.0 * PossessionAdjuster), Vertical_Percent_Pos = 49, State = Player_States.STANDING });
            f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.TE, YardLine = (-4.0 * PossessionAdjuster), Vertical_Percent_Pos = 51, State = Player_States.STANDING });
            f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.RB, YardLine = (-2.0 * PossessionAdjuster), Vertical_Percent_Pos = 57, State = Player_States.STANDING });
            f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.OL, YardLine = (-1.0 * PossessionAdjuster), Vertical_Percent_Pos = 54, State = Player_States.STANDING });
            f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.OL, YardLine = (-1.0 * PossessionAdjuster), Vertical_Percent_Pos = 59, State = Player_States.STANDING });
            f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.WR, YardLine = (-1.0 * PossessionAdjuster), Vertical_Percent_Pos = 77, State = Player_States.STANDING });
 
            return f;
        }
    }
}
