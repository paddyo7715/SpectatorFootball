using SpectatorFootball.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.GameNS
{
    public class Punt_Return_Formation : iFormation_Play
    {
        public Formation getFormation(Formations_Enum fe, double PossessionAdjuster)
        {
            List<Formation_Rec> r = new List<Formation_Rec>();
            Formation f = new Formation();
            f.Player_list = new List<Formation_Rec>();

            f.Name = "Punt Return";
            f.f_enum = fe;
            f.bSpecialTeams = true;
            f.ReturnerIndex = 5;
            f.Line_Players = new List<int>() { 1, 2, 3, 4, 6, 7, 8, 9 };
            f.Gunners = new List<int>() { 0, 10 };
            f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.DB, YardLine = (2.0 * PossessionAdjuster), Vertical_Percent_Pos = 23, State = Player_States.STANDING });
            f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.LB, YardLine = (2.0 * PossessionAdjuster), Vertical_Percent_Pos = 38, State = Player_States.STANDING });
            f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.LB, YardLine = (2.0 * PossessionAdjuster), Vertical_Percent_Pos = 41, State = Player_States.STANDING });
            f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.DL, YardLine = (2.0 * PossessionAdjuster), Vertical_Percent_Pos = 44, State = Player_States.STANDING });
            f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.DL, YardLine = (2.0 * PossessionAdjuster), Vertical_Percent_Pos = 48, State = Player_States.STANDING });
            f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.DB, YardLine = (40.0 * PossessionAdjuster), Vertical_Percent_Pos = 50, State = Player_States.STANDING });
            f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.DL, YardLine = (2.0 * PossessionAdjuster), Vertical_Percent_Pos = 52, State = Player_States.STANDING });
            f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.DL, YardLine = (2.0 * PossessionAdjuster), Vertical_Percent_Pos = 55, State = Player_States.STANDING });
            f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.LB, YardLine = (2.0 * PossessionAdjuster), Vertical_Percent_Pos = 58, State = Player_States.STANDING });
            f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.LB, YardLine = (2.0 * PossessionAdjuster), Vertical_Percent_Pos = 61, State = Player_States.STANDING });
            f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.DB, YardLine = (2.0 * PossessionAdjuster), Vertical_Percent_Pos = 77, State = Player_States.STANDING });

            return f;
        }
    }
}


