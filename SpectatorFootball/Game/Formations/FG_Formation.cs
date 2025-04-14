using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpectatorFootball.Enum;
using SpectatorFootball.GameNS;

namespace SpectatorFootball.GameNS
{
    public class FG_Formation : iFormation_Play
    {
        public Formation getFormation(Formations_Enum fe, double PossessionAdjuster)
        {
            List<Formation_Rec> r = new List<Formation_Rec>();
            Formation f = new Formation();
            f.Player_list = new List<Formation_Rec>();

            f.Name = "FG";
            f.f_enum = fe;
            f.bSpecialTeams = true;
            f.KickerIndex = 5;
            f.FGHolderIndex = 4;

            f.Line_Players = new List<int>() {0, 1, 2, 3, 6, 7, 8, 9, 10 };
            f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.WR, YardLine = (-1.5 * PossessionAdjuster), Vertical_Percent_Pos = 38, State = Player_States.STANDING });
            f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.OL, YardLine = (-1.0 * PossessionAdjuster), Vertical_Percent_Pos = 41, State = Player_States.STANDING });
            f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.OL, YardLine = (-1.0 * PossessionAdjuster), Vertical_Percent_Pos = 44, State = Player_States.STANDING });
            f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.DL, YardLine = (-1.0 * PossessionAdjuster), Vertical_Percent_Pos = 47, State = Player_States.STANDING });
            f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.DB, YardLine = (-5.0 * PossessionAdjuster), Vertical_Percent_Pos = 48, State = Player_States.STANDING });
            f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.K, YardLine = (-13.0 * PossessionAdjuster), Vertical_Percent_Pos = 49, State = Player_States.STANDING });
            f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.OL, YardLine = (-1.0 * PossessionAdjuster), Vertical_Percent_Pos = 50, State = Player_States.STANDING });
            f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.DL, YardLine = (-1.0 * PossessionAdjuster), Vertical_Percent_Pos = 53, State = Player_States.STANDING });
            f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.OL, YardLine = (-1.0 * PossessionAdjuster), Vertical_Percent_Pos = 56, State = Player_States.STANDING });
            f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.OL, YardLine = (-1.0 * PossessionAdjuster), Vertical_Percent_Pos = 59, State = Player_States.STANDING });
            f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.DB, YardLine = (-1.5 * PossessionAdjuster), Vertical_Percent_Pos = 62, State = Player_States.STANDING });

            return f;
        }
    }
}
