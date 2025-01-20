using SpectatorFootball.Enum;
using SpectatorFootball.Models;
using SpectatorFootball.PenaltiesNS;
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

        public static string getDownAndYardString(int Down, double YardstoGo, double BallYardline, bool bLefttoRight)
        {
            string r = "";

            switch (Down)
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

                double dist_from_GL = Game_Engine_Helper.calcDistanceFromOpponentGL(BallYardline, bLefttoRight);
                if (dist_from_GL <= YardstoGo)
                    toGo = "Goal";
                else
                {
                    int itogo = (int)YardstoGo;
                    if (YardstoGo == 0) YardstoGo = 1;
                    toGo = YardstoGo.ToString();
                }

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
    }
}
