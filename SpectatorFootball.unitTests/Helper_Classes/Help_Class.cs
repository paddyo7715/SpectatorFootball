using SpectatorFootball.Enum;
using SpectatorFootball.Models;
using SpectatorFootball.PlayerNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.unitTests.Helper_ClassesNS
{
    //This class has methods to help in the setup of some test cases
    public class Help_Class
    {
        public static List<Player_and_Ratings> CreateRandomTeamPandR(long f_id)
        {
            List<Player_and_Ratings> r = new List<Player_and_Ratings>();

            for (int i=0; i<app_Constants.QB_PER_TEAM; i++)
            {
                Player p = Player_Helper.CreatePlayer(Player_Pos.QB, true, false, true, 1);
                Players_By_Team pbt = new Players_By_Team()
                { Franchise_ID = f_id, Player_ID = p.ID, Season_ID = 1 };
                Player_and_Ratings p_and_r = new Player_and_Ratings()
                { p = p, pr = p.Player_Ratings.ToList(), pbt = pbt };
                r.Add(p_and_r);
            }

            for (int i = 0; i < app_Constants.RB_PER_TEAM; i++)
            {
                Player p = Player_Helper.CreatePlayer(Player_Pos.RB, true, false, true, 1);
                Players_By_Team pbt = new Players_By_Team()
                { Franchise_ID = f_id, Player_ID = p.ID, Season_ID = 1 };
                Player_and_Ratings p_and_r = new Player_and_Ratings()
                { p = p, pr = p.Player_Ratings.ToList(), pbt = pbt };
                r.Add(p_and_r);
            }

            for (int i = 0; i < app_Constants.WR_PER_TEAM; i++)
            {
                Player p = Player_Helper.CreatePlayer(Player_Pos.WR, true, false, true, 1);
                Players_By_Team pbt = new Players_By_Team()
                { Franchise_ID = f_id, Player_ID = p.ID, Season_ID = 1 };
                Player_and_Ratings p_and_r = new Player_and_Ratings()
                { p = p, pr = p.Player_Ratings.ToList(), pbt = pbt };
                r.Add(p_and_r);
            }

            for (int i = 0; i < app_Constants.TE_PER_TEAM; i++)
            {
                Player p = Player_Helper.CreatePlayer(Player_Pos.TE, true, false, true, 1);
                Players_By_Team pbt = new Players_By_Team()
                { Franchise_ID = f_id, Player_ID = p.ID, Season_ID = 1 };
                Player_and_Ratings p_and_r = new Player_and_Ratings()
                { p = p, pr = p.Player_Ratings.ToList(), pbt = pbt };
                r.Add(p_and_r);
            }


            for (int i = 0; i < app_Constants.OL_PER_TEAM; i++)
            {
                Player p = Player_Helper.CreatePlayer(Player_Pos.OL, true, false, true, 1);
                Players_By_Team pbt = new Players_By_Team()
                { Franchise_ID = f_id, Player_ID = p.ID, Season_ID = 1 };
                Player_and_Ratings p_and_r = new Player_and_Ratings()
                { p = p, pr = p.Player_Ratings.ToList(), pbt = pbt };
                r.Add(p_and_r);
            }

            for (int i = 0; i < app_Constants.DL_PER_TEAM; i++)
            {
                Player p = Player_Helper.CreatePlayer(Player_Pos.DL, true, false, true, 1);
                Players_By_Team pbt = new Players_By_Team()
                { Franchise_ID = f_id, Player_ID = p.ID, Season_ID = 1 };
                Player_and_Ratings p_and_r = new Player_and_Ratings()
                { p = p, pr = p.Player_Ratings.ToList(), pbt = pbt };
                r.Add(p_and_r);
            }

            for (int i = 0; i < app_Constants.LB_PER_TEAM; i++)
            {
                Player p = Player_Helper.CreatePlayer(Player_Pos.LB, true, false, true, 1);
                Players_By_Team pbt = new Players_By_Team()
                { Franchise_ID = f_id, Player_ID = p.ID, Season_ID = 1 };
                Player_and_Ratings p_and_r = new Player_and_Ratings()
                { p = p, pr = p.Player_Ratings.ToList(), pbt = pbt };
                r.Add(p_and_r);
            }

            for (int i = 0; i < app_Constants.DB_PER_TEAM; i++)
            {
                Player p = Player_Helper.CreatePlayer(Player_Pos.DB, true, false, true, 1);
                Players_By_Team pbt = new Players_By_Team()
                { Franchise_ID = f_id, Player_ID = p.ID, Season_ID = 1 };
                Player_and_Ratings p_and_r = new Player_and_Ratings()
                { p = p, pr = p.Player_Ratings.ToList(), pbt = pbt };
                r.Add(p_and_r);
            }

            for (int i = 0; i < app_Constants.K_PER_TEAM; i++)
            {
                Player p = Player_Helper.CreatePlayer(Player_Pos.K, true, false, true, 1);
                Players_By_Team pbt = new Players_By_Team()
                { Franchise_ID = f_id, Player_ID = p.ID, Season_ID = 1 };
                Player_and_Ratings p_and_r = new Player_and_Ratings()
                { p = p, pr = p.Player_Ratings.ToList(), pbt = pbt };
                r.Add(p_and_r);
            }

            for (int i = 0; i < app_Constants.P_PER_TEAM; i++)
            {
                Player p = Player_Helper.CreatePlayer(Player_Pos.P, true, false, true, 1);
                Players_By_Team pbt = new Players_By_Team()
                { Franchise_ID = f_id, Player_ID = p.ID, Season_ID = 1 };
                Player_and_Ratings p_and_r = new Player_and_Ratings()
                { p = p, pr = p.Player_Ratings.ToList(), pbt = pbt };
                r.Add(p_and_r);
            }

            return r;
        }
    }
}
