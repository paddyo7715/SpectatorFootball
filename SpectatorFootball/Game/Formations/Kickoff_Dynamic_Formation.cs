using SpectatorFootball.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.GameNS
{
    public class Kickoff_Dynamic_Formation : iFormation_Play
    {
        public Formation getFormation(Formations_Enum fe, double PossessionAdjuster)
        {
            List<Formation_Rec> r = new List<Formation_Rec>();
            Formation f = new Formation();
            f.Player_list = new List<Formation_Rec>();

            f.Name = "Kickoff_Dynamic";
            f.f_enum = fe;
            f.bSpecialTeams = true;
            f.KickerIndex = 5;
            f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.DB, YardLine = (+25.0 * PossessionAdjuster), Vertical_Percent_Pos = 30, State = Player_States.STANDING });
            f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.LB, YardLine = (+25.0 * PossessionAdjuster), Vertical_Percent_Pos = 34, State = Player_States.STANDING });
            f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.TE, YardLine = (+25.0 * PossessionAdjuster), Vertical_Percent_Pos = 38, State = Player_States.STANDING });
            f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.LB, YardLine = (+25.0 * PossessionAdjuster), Vertical_Percent_Pos = 42, State = Player_States.STANDING });
            f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.DB, YardLine = (+25.0 * PossessionAdjuster), Vertical_Percent_Pos = 46, State = Player_States.STANDING });
            f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.K, YardLine = (-7.0 * PossessionAdjuster), Vertical_Percent_Pos = 49, State = Player_States.STANDING });
            f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.WR, YardLine = (+25.0 * PossessionAdjuster), Vertical_Percent_Pos = 54, State = Player_States.STANDING });
            f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.LB, YardLine = (+25.0 * PossessionAdjuster), Vertical_Percent_Pos = 58, State = Player_States.STANDING });
            f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.WR, YardLine = (+25.0 * PossessionAdjuster), Vertical_Percent_Pos = 62, State = Player_States.STANDING });
            f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.DB, YardLine = (+25.0 * PossessionAdjuster), Vertical_Percent_Pos = 66, State = Player_States.STANDING });
            f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.DB, YardLine = (+25.0 * PossessionAdjuster), Vertical_Percent_Pos = 70, State = Player_States.STANDING });
            return f;

        }
    }
}
