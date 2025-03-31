using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpectatorFootball.Enum;
using SpectatorFootball.GameNS;

namespace SpectatorFootball.GameNS
{
    public class FG_Def_Formation : iFormation_Play
    {
        public Formation getFormation(Formations_Enum fe, double PossessionAdjuster)
        {
            List<Formation_Rec> r = new List<Formation_Rec>();
            Formation f = new Formation();
            f.Player_list = new List<Formation_Rec>();

            f.Name = "FG Defense";
            f.f_enum = fe;
            f.bSpecialTeams = true;
            f.Line_Players = new List<int>() {0, 1, 2, 3, 5, 7, 8, 9, 10 };
            f.Backfield_Players = new List<int>() { 4, 6 };
            f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.WR, YardLine = (2.0 * PossessionAdjuster), Vertical_Percent_Pos = 38, State = Player_States.STANDING });
            f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.OL, YardLine = (2.0 * PossessionAdjuster), Vertical_Percent_Pos = 41, State = Player_States.STANDING });
            f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.DL, YardLine = (2.0 * PossessionAdjuster), Vertical_Percent_Pos = 44, State = Player_States.STANDING });
            f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.OL, YardLine = (2.0 * PossessionAdjuster), Vertical_Percent_Pos = 47, State = Player_States.STANDING });
            f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.DB, YardLine = (4.0 * PossessionAdjuster), Vertical_Percent_Pos = 46, State = Player_States.STANDING });
            f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.OL, YardLine = (2.0 * PossessionAdjuster), Vertical_Percent_Pos = 50, State = Player_States.STANDING });
            f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.WR, YardLine = (4.0 * PossessionAdjuster), Vertical_Percent_Pos = 52, State = Player_States.STANDING });
            f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.OL, YardLine = (2.0 * PossessionAdjuster), Vertical_Percent_Pos = 53, State = Player_States.STANDING });
            f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.DL, YardLine = (2.0 * PossessionAdjuster), Vertical_Percent_Pos = 56, State = Player_States.STANDING });
            f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.OL, YardLine = (2.0 * PossessionAdjuster), Vertical_Percent_Pos = 59, State = Player_States.STANDING });
            f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.DB, YardLine = (2.0 * PossessionAdjuster), Vertical_Percent_Pos = 62, State = Player_States.STANDING });

            return f;
        }
    }
}
