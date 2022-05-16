using SpectatorFootball.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.GameNS
{
    //Note that all formations are from the top of the screen to the bottom.
    //so the first positition is at the top of the screen and the last is at the bottom

    public class Formations
    {
        public static List<Formation_Rec> getFormation(Formations_Enum f)
        {
            List<Formation_Rec> r = new List<Formation_Rec>();

            switch (f)
            {
                case Formations_Enum.KICKOFF_REGULAR_KICK:
                    r.Add(new Formation_Rec(){ Pos = Enum.Player_Pos.DB, YardLine = -1, Vertical_Percent_Pos = 8, bSpecialTeams = true});
                    r.Add(new Formation_Rec() { Pos = Enum.Player_Pos.WR, YardLine = -1, Vertical_Percent_Pos = 16, bSpecialTeams = true });
                    r.Add(new Formation_Rec() { Pos = Enum.Player_Pos.DB, YardLine = -1, Vertical_Percent_Pos = 24, bSpecialTeams = true });
                    r.Add(new Formation_Rec() { Pos = Enum.Player_Pos.TE, YardLine = -1, Vertical_Percent_Pos = 32, bSpecialTeams = true });
                    r.Add(new Formation_Rec() { Pos = Enum.Player_Pos.LB, YardLine = -1, Vertical_Percent_Pos = 40, bSpecialTeams = true });
                    r.Add(new Formation_Rec() { Pos = Enum.Player_Pos.K, YardLine = -7, Vertical_Percent_Pos = 48, bSpecialTeams = true });
                    r.Add(new Formation_Rec() { Pos = Enum.Player_Pos.LB, YardLine = -1, Vertical_Percent_Pos = 56, bSpecialTeams = true });
                    r.Add(new Formation_Rec() { Pos = Enum.Player_Pos.DB, YardLine = -1, Vertical_Percent_Pos = 64, bSpecialTeams = true });
                    r.Add(new Formation_Rec() { Pos = Enum.Player_Pos.LB, YardLine = -1, Vertical_Percent_Pos = 72, bSpecialTeams = true });
                    r.Add(new Formation_Rec() { Pos = Enum.Player_Pos.WR, YardLine = -1, Vertical_Percent_Pos = 80, bSpecialTeams = true });
                    r.Add(new Formation_Rec() { Pos = Enum.Player_Pos.DB, YardLine = -1, Vertical_Percent_Pos = 88, bSpecialTeams = true });
                    break;
                case Formations_Enum.KICKOFF_REGULAR_RECEIVE:
                    r.Add(new Formation_Rec() { Pos = Enum.Player_Pos.DB, YardLine = 15, Vertical_Percent_Pos = 8, bSpecialTeams = true });
                    r.Add(new Formation_Rec() { Pos = Enum.Player_Pos.RB, YardLine = 25, Vertical_Percent_Pos = 16, bSpecialTeams = true });
                    r.Add(new Formation_Rec() { Pos = Enum.Player_Pos.LB, YardLine = 15, Vertical_Percent_Pos = 24, bSpecialTeams = true });
                    r.Add(new Formation_Rec() { Pos = Enum.Player_Pos.RB, YardLine = 25, Vertical_Percent_Pos = 32, bSpecialTeams = true });
                    r.Add(new Formation_Rec() { Pos = Enum.Player_Pos.LB, YardLine = 15, Vertical_Percent_Pos = 40, bSpecialTeams = true });
                    r.Add(new Formation_Rec() { Pos = Enum.Player_Pos.WR, YardLine = 60, Vertical_Percent_Pos = 48, bSpecialTeams = true });
                    r.Add(new Formation_Rec() { Pos = Enum.Player_Pos.LB, YardLine = 15, Vertical_Percent_Pos = 56, bSpecialTeams = true });
                    r.Add(new Formation_Rec() { Pos = Enum.Player_Pos.TE, YardLine = 25, Vertical_Percent_Pos = 64, bSpecialTeams = true });
                    r.Add(new Formation_Rec() { Pos = Enum.Player_Pos.RB, YardLine = 15, Vertical_Percent_Pos = 72, bSpecialTeams = true });
                    r.Add(new Formation_Rec() { Pos = Enum.Player_Pos.TE, YardLine = 25, Vertical_Percent_Pos = 80, bSpecialTeams = true });
                    r.Add(new Formation_Rec() { Pos = Enum.Player_Pos.DB, YardLine = 15, Vertical_Percent_Pos = 88, bSpecialTeams = true });
                    break;
            }
            return r;
            }


        }
}
