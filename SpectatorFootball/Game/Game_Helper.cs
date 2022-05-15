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
        //This method is called during a game and a player for a specific position can not be found,
        //so a player at another position must be chosen to take his place.  Example:  All 3 QBs
        //go down to injury
        private static Player_and_Ratings getPlayerSubstitutes(Player_Pos pos, 
            List<Player_and_Ratings> Players,
            List<Injury> lInj,
            List<Formation_Rec> fLIst)
        {
            Player_and_Ratings r = null;
            List<Player_Pos> pp = new List<Player_Pos>();

            switch (pos)
            {
                case Player_Pos.QB:
                    {
                        int iPos = (int)pos;
                        r = Players.OrderByDescending(x => x.pr.First().Accuracy_Rating).FirstOrDefault();
                        break;
                    }

                case Player_Pos.RB:
                    {
                        pp.Add(Player_Pos.WR);
                        pp.Add(Player_Pos.TE);
                        pp.Add(Player_Pos.DB);
                        r = getUninjuredPlayer(pp, Players, lInj, fLIst);
                        break;
                    }

                case Player_Pos.WR:
                    {
                        pp.Add(Player_Pos.TE);
                        pp.Add(Player_Pos.DB);
                        pp.Add(Player_Pos.RB);
                        r = getUninjuredPlayer(pp, Players, lInj, fLIst);
                        break;
                    }

                case Player_Pos.TE:
                    {
                        pp.Add(Player_Pos.TE);
                        pp.Add(Player_Pos.DB);
                        pp.Add(Player_Pos.RB);
                        r = getUninjuredPlayer(pp, Players, lInj, fLIst);
                        break;
                    }

                case Player_Pos.OL:
                    {
                        pp.Add(Player_Pos.TE);
                        pp.Add(Player_Pos.DL);
                        pp.Add(Player_Pos.LB);
                        r = getUninjuredPlayer(pp, Players, lInj, fLIst);
                        break;
                    }

                case Player_Pos.DL:
                    {
                        pp.Add(Player_Pos.LB);
                        pp.Add(Player_Pos.OL);
                        r = getUninjuredPlayer(pp, Players, lInj, fLIst);
                        break;
                    }

                case Player_Pos.LB:
                    {
                        pp.Add(Player_Pos.DB);
                        pp.Add(Player_Pos.TE);
                        r = getUninjuredPlayer(pp, Players, lInj, fLIst);
                        break;
                    }

                case Player_Pos.DB:
                    {
                        pp.Add(Player_Pos.WR);
                        pp.Add(Player_Pos.RB);
                        r = getUninjuredPlayer(pp, Players, lInj, fLIst);
                        break;
                    }

                case Player_Pos.K:
                    {
                        r = Players.OrderByDescending(x => x.pr.First().Kicker_Leg_Accuracy_Rating).FirstOrDefault();
                        break;
                    }

                case Player_Pos.P:
                    {
                        r = Players.OrderByDescending(x => x.pr.First().Kicker_Leg_Power_Rating).FirstOrDefault();
                        break;
                    }
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
                !fLIst.Any(f => f.player_id == x.p.ID)
                ).FirstOrDefault();
            }

            return r;
        }
    }
}
