using SpectatorFootball.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.GameNS
{
    public class Kickoff_Classic_Return_Formation : iFormation_Play
    {
        public Formation getFormation(Formations_Enum fe, double PossessionAdjuster)
        {
            List<Formation_Rec> r = new List<Formation_Rec>();
            Formation f = new Formation();
            f.Player_list = new List<Formation_Rec>();

            f.Name = "Kickoff Receive";
            f.f_enum = fe;
            f.bSpecialTeams = true;
            f.ReturnerIndex = 5;
            f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.RB, YardLine = (15.0 * PossessionAdjuster), Vertical_Percent_Pos = 20, State = Player_States.STANDING });
            f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.TE, YardLine = (25.0 * PossessionAdjuster), Vertical_Percent_Pos = 27, State = Player_States.STANDING });
            f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.LB, YardLine = (15.0 * PossessionAdjuster), Vertical_Percent_Pos = 32, State = Player_States.STANDING });
            f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.DB, YardLine = (50.0 * PossessionAdjuster), Vertical_Percent_Pos = 35, State = Player_States.STANDING });
            f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.RB, YardLine = (15.0 * PossessionAdjuster), Vertical_Percent_Pos = 44, State = Player_States.STANDING });
            f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.WR, YardLine = (60.0 * PossessionAdjuster), Vertical_Percent_Pos = 50, State = Player_States.STANDING });
            f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.LB, YardLine = (25.0 * PossessionAdjuster), Vertical_Percent_Pos = 55, State = Player_States.STANDING });
            f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.DB, YardLine = (50.0 * PossessionAdjuster), Vertical_Percent_Pos = 65, State = Player_States.STANDING });
            f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.LB, YardLine = (15.0 * PossessionAdjuster), Vertical_Percent_Pos = 68, State = Player_States.STANDING });
            f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.TE, YardLine = (25.0 * PossessionAdjuster), Vertical_Percent_Pos = 73, State = Player_States.STANDING });
            f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.RB, YardLine = (15.0 * PossessionAdjuster), Vertical_Percent_Pos = 80, State = Player_States.STANDING });
            return f;

        }
    }
}


