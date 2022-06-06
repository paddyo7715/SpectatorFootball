using SpectatorFootball.Enum;
using SpectatorFootball.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.GameNS
{
    public class Game_Helper
    {
        public static string getTimestringFromSeconds(long? sec)
        {
            string r = null;

            long minutes = (long)sec / 60;
            long seconds = (long)sec % 60;

            string sSeconds = seconds <= 9 ? "0" + seconds.ToString() : seconds.ToString();

            r = minutes.ToString() + ":" + sSeconds;

            return r;
        }
        public static string getQTRString(long? qtr)
        {
            string r = null;

            switch (qtr)
            {
                case 1:
                    r = "1st";
                    break;
                case 2:
                    r = "2nd";
                    break;
                case 3:
                    r = "3rd";
                    break;
                case 4:
                    r = "4th";
                    break;
                default:
                    r = "OT   ";
                    break;
            }

            return r;
        }

        public static string getDownAndYardString(int Down, int YardstoGo, double BallYardline, bool bLefttoRight)
        {
            string r = "";

            switch(Down)
            {
                case 1:
                    r = "1st";
                    break;
                case 2:
                    r = "2nd";
                    break;
                case 3:
                    r = "3rd";
                    break;
                case 4:
                    r = "4th";
                    break;
                default:
                    r = "";
                    break;
            }

            if (r != "")
            {
                r += " and ";

                string toGo = "";

                if (bLefttoRight && (100 - (int) BallYardline) <= YardstoGo)
                    toGo = "Goal";
                else if (!bLefttoRight && (int) BallYardline <= YardstoGo)
                    toGo = "Goal";
                else
                    toGo = YardstoGo.ToString();

                r += toGo;
            }

            return r;
        }

        private static Player_and_Ratings getUninjuredPlayer(List<Player_Pos> posList,
            List<Player_and_Ratings> Players,
            List<Injury> lInj,
            List<Formation_Rec> fLIst)
        {
            Player_and_Ratings tmp = null;
            Player_and_Ratings r = null;

            foreach (Player_Pos pp in posList)
            {
                int iPos = (int)pp;
                r = Players.Where(x => x.p.Pos == iPos &&
                !lInj.Any(i => i.Player.ID == x.p.ID) &&
                !fLIst.Any(f => f.p_and_r.p.ID == x.p.ID)
                ).FirstOrDefault();
            }

            return r;
        }
        public static Formation getFormation(Formations_Enum fe)
        {
            List<Formation_Rec> r = new List<Formation_Rec>();
            Formation f = new Formation();
            f.Player_list = new List<Formation_Rec>();

            switch (fe)
            {
                case Formations_Enum.KICKOFF_REGULAR_KICK:
                    f.Name = "Kickoff";
                    f.f_enum = fe;
                    f.bSpecialTeams = true;
                    f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.DB, YardLine = -1, Vertical_Percent_Pos = 8, bSpecialTeams = true });
                    f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.WR, YardLine = -1, Vertical_Percent_Pos = 16, bSpecialTeams = true });
                    f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.DB, YardLine = -1, Vertical_Percent_Pos = 24, bSpecialTeams = true });
                    f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.TE, YardLine = -1, Vertical_Percent_Pos = 32, bSpecialTeams = true });
                    f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.LB, YardLine = -1, Vertical_Percent_Pos = 40, bSpecialTeams = true });
                    f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.K, YardLine = -7, Vertical_Percent_Pos = 48, bSpecialTeams = true });
                    f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.LB, YardLine = -1, Vertical_Percent_Pos = 56, bSpecialTeams = true });
                    f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.DB, YardLine = -1, Vertical_Percent_Pos = 64, bSpecialTeams = true });
                    f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.LB, YardLine = -1, Vertical_Percent_Pos = 72, bSpecialTeams = true });
                    f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.WR, YardLine = -1, Vertical_Percent_Pos = 80, bSpecialTeams = true });
                    f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.DB, YardLine = -1, Vertical_Percent_Pos = 88, bSpecialTeams = true });
                    break;
                case Formations_Enum.KICKOFF_REGULAR_RECEIVE:
                    f.Name = "Onside Kick";
                    f.f_enum = fe;
                    f.bSpecialTeams = true;
                    f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.DB, YardLine = 15, Vertical_Percent_Pos = 8, bSpecialTeams = true });
                    f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.RB, YardLine = 25, Vertical_Percent_Pos = 16, bSpecialTeams = true });
                    f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.LB, YardLine = 15, Vertical_Percent_Pos = 24, bSpecialTeams = true });
                    f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.RB, YardLine = 25, Vertical_Percent_Pos = 32, bSpecialTeams = true });
                    f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.LB, YardLine = 15, Vertical_Percent_Pos = 40, bSpecialTeams = true });
                    f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.WR, YardLine = 60, Vertical_Percent_Pos = 48, bSpecialTeams = true });
                    f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.LB, YardLine = 15, Vertical_Percent_Pos = 56, bSpecialTeams = true });
                    f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.TE, YardLine = 25, Vertical_Percent_Pos = 64, bSpecialTeams = true });
                    f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.RB, YardLine = 15, Vertical_Percent_Pos = 72, bSpecialTeams = true });
                    f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.TE, YardLine = 25, Vertical_Percent_Pos = 80, bSpecialTeams = true });
                    f.Player_list.Add(new Formation_Rec() { Pos = Enum.Player_Pos.DB, YardLine = 15, Vertical_Percent_Pos = 88, bSpecialTeams = true });
                    break;
            }
            return f;
        }
    }
}
