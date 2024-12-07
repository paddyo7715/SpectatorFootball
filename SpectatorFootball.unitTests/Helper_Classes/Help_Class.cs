using SpectatorFootball.Enum;
using SpectatorFootball.GameNS;
using SpectatorFootball.Models;
using SpectatorFootball.PlayerNS;
using SpectatorFootball.unitTests.DAOStubs;
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
                Player_NamesDAOTEST pnDAO = new Player_NamesDAOTEST();
                HomeTownsDAOTEST htDAO = new HomeTownsDAOTEST();
                Player p = Player_Helper.CreatePlayer(Player_Pos.QB, true, false, true, 1, pnDAO, htDAO);
                Players_By_Team pbt = new Players_By_Team()
                { Franchise_ID = f_id, Player_ID = p.ID, Season_ID = 1 };
                Player_and_Ratings p_and_r = new Player_and_Ratings()
                { p = p, pr = p.Player_Ratings.ToList(), pbt = pbt };
                r.Add(p_and_r);
            }

            for (int i = 0; i < app_Constants.RB_PER_TEAM; i++)
            {
                Player_NamesDAOTEST pnDAO = new Player_NamesDAOTEST();
                HomeTownsDAOTEST htDAO = new HomeTownsDAOTEST();
                Player p = Player_Helper.CreatePlayer(Player_Pos.RB, true, false, true, 1, pnDAO, htDAO);
                Players_By_Team pbt = new Players_By_Team()
                { Franchise_ID = f_id, Player_ID = p.ID, Season_ID = 1 };
                Player_and_Ratings p_and_r = new Player_and_Ratings()
                { p = p, pr = p.Player_Ratings.ToList(), pbt = pbt };
                r.Add(p_and_r);
            }

            for (int i = 0; i < app_Constants.WR_PER_TEAM; i++)
            {
                Player_NamesDAOTEST pnDAO = new Player_NamesDAOTEST();
                HomeTownsDAOTEST htDAO = new HomeTownsDAOTEST();
                Player p = Player_Helper.CreatePlayer(Player_Pos.WR, true, false, true, 1, pnDAO, htDAO);
                Players_By_Team pbt = new Players_By_Team()
                { Franchise_ID = f_id, Player_ID = p.ID, Season_ID = 1 };
                Player_and_Ratings p_and_r = new Player_and_Ratings()
                { p = p, pr = p.Player_Ratings.ToList(), pbt = pbt };
                r.Add(p_and_r);
            }

            for (int i = 0; i < app_Constants.TE_PER_TEAM; i++)
            {
                Player_NamesDAOTEST pnDAO = new Player_NamesDAOTEST();
                HomeTownsDAOTEST htDAO = new HomeTownsDAOTEST();
                Player p = Player_Helper.CreatePlayer(Player_Pos.TE, true, false, true, 1, pnDAO, htDAO);
                Players_By_Team pbt = new Players_By_Team()
                { Franchise_ID = f_id, Player_ID = p.ID, Season_ID = 1 };
                Player_and_Ratings p_and_r = new Player_and_Ratings()
                { p = p, pr = p.Player_Ratings.ToList(), pbt = pbt };
                r.Add(p_and_r);
            }


            for (int i = 0; i < app_Constants.OL_PER_TEAM; i++)
            {
                Player_NamesDAOTEST pnDAO = new Player_NamesDAOTEST();
                HomeTownsDAOTEST htDAO = new HomeTownsDAOTEST();
                Player p = Player_Helper.CreatePlayer(Player_Pos.OL, true, false, true, 1, pnDAO, htDAO);
                Players_By_Team pbt = new Players_By_Team()
                { Franchise_ID = f_id, Player_ID = p.ID, Season_ID = 1 };
                Player_and_Ratings p_and_r = new Player_and_Ratings()
                { p = p, pr = p.Player_Ratings.ToList(), pbt = pbt };
                r.Add(p_and_r);
            }

            for (int i = 0; i < app_Constants.DL_PER_TEAM; i++)
            {
                Player_NamesDAOTEST pnDAO = new Player_NamesDAOTEST();
                HomeTownsDAOTEST htDAO = new HomeTownsDAOTEST();
                Player p = Player_Helper.CreatePlayer(Player_Pos.DL, true, false, true, 1, pnDAO, htDAO);
                Players_By_Team pbt = new Players_By_Team()
                { Franchise_ID = f_id, Player_ID = p.ID, Season_ID = 1 };
                Player_and_Ratings p_and_r = new Player_and_Ratings()
                { p = p, pr = p.Player_Ratings.ToList(), pbt = pbt };
                r.Add(p_and_r);
            }

            for (int i = 0; i < app_Constants.LB_PER_TEAM; i++)
            {
                Player_NamesDAOTEST pnDAO = new Player_NamesDAOTEST();
                HomeTownsDAOTEST htDAO = new HomeTownsDAOTEST();
                Player p = Player_Helper.CreatePlayer(Player_Pos.LB, true, false, true, 1, pnDAO, htDAO);
                Players_By_Team pbt = new Players_By_Team()
                { Franchise_ID = f_id, Player_ID = p.ID, Season_ID = 1 };
                Player_and_Ratings p_and_r = new Player_and_Ratings()
                { p = p, pr = p.Player_Ratings.ToList(), pbt = pbt };
                r.Add(p_and_r);
            }

            for (int i = 0; i < app_Constants.DB_PER_TEAM; i++)
            {
                Player_NamesDAOTEST pnDAO = new Player_NamesDAOTEST();
                HomeTownsDAOTEST htDAO = new HomeTownsDAOTEST();
                Player p = Player_Helper.CreatePlayer(Player_Pos.DB, true, false, true, 1, pnDAO, htDAO);
                Players_By_Team pbt = new Players_By_Team()
                { Franchise_ID = f_id, Player_ID = p.ID, Season_ID = 1 };
                Player_and_Ratings p_and_r = new Player_and_Ratings()
                { p = p, pr = p.Player_Ratings.ToList(), pbt = pbt };
                r.Add(p_and_r);
            }

            for (int i = 0; i < app_Constants.K_PER_TEAM; i++)
            {
                Player_NamesDAOTEST pnDAO = new Player_NamesDAOTEST();
                HomeTownsDAOTEST htDAO = new HomeTownsDAOTEST();
                Player p = Player_Helper.CreatePlayer(Player_Pos.K, true, false, true, 1, pnDAO, htDAO);
                Players_By_Team pbt = new Players_By_Team()
                { Franchise_ID = f_id, Player_ID = p.ID, Season_ID = 1 };
                Player_and_Ratings p_and_r = new Player_and_Ratings()
                { p = p, pr = p.Player_Ratings.ToList(), pbt = pbt };
                r.Add(p_and_r);
            }

            for (int i = 0; i < app_Constants.P_PER_TEAM; i++)
            {
                Player_NamesDAOTEST pnDAO = new Player_NamesDAOTEST();
                HomeTownsDAOTEST htDAO = new HomeTownsDAOTEST();
                Player p = Player_Helper.CreatePlayer(Player_Pos.P, true, false, true, 1, pnDAO, htDAO);
                Players_By_Team pbt = new Players_By_Team()
                { Franchise_ID = f_id, Player_ID = p.ID, Season_ID = 1 };
                Player_and_Ratings p_and_r = new Player_and_Ratings()
                { p = p, pr = p.Player_Ratings.ToList(), pbt = pbt };
                r.Add(p_and_r);
            }

            return r;
        }
        public static List<Game_Player> getRandomPlayersforPlay(long f_id)
        {
            //position doesn't matter because this is for penalties
            List<Game_Player> r = new List<Game_Player>();

            for (int i = 0; i < 11; i++)
            {
                Player_NamesDAOTEST pnDAO = new Player_NamesDAOTEST();
                HomeTownsDAOTEST htDAO = new HomeTownsDAOTEST();
                Player p = Player_Helper.CreatePlayer(Player_Pos.TE, true, false, true, 1, pnDAO, htDAO);
                Players_By_Team pbt = new Players_By_Team()
                { Franchise_ID = f_id, Player_ID = p.ID, Season_ID = 1 };
                Player_and_Ratings p_and_r = new Player_and_Ratings()
                { p = p, pr = p.Player_Ratings.ToList(), pbt = pbt };
                Game_Player gp = new Game_Player(){ Pos = (Player_Pos)p_and_r.p.Pos, p_and_r = p_and_r };
                r.Add(gp);
            }

            return r;
        }

        public static List<Game_Player> setGamePlayerLIsts(long f_id, double Line_of_Scrimmage, Formation f)
        {
            long player_id = f_id;
            List<Game_Player> r = new List<Game_Player>();
            foreach (Formation_Rec fr in f.Player_list)
            {
                Player_NamesDAOTEST pnDAO = new Player_NamesDAOTEST();
                HomeTownsDAOTEST htDAO = new HomeTownsDAOTEST();
                Player p = Player_Helper.CreatePlayer(fr.Pos, true, false, true, 1, pnDAO, htDAO);

                p.ID = player_id;
                Player_Ratings pl_ratings = p.Player_Ratings.First();
                pl_ratings.ID = player_id;
                pl_ratings.Player_ID = player_id;

                Players_By_Team pbt = new Players_By_Team()
                { Franchise_ID = f_id, Player_ID = player_id, Season_ID = 1 };
                Player_and_Ratings p_and_r = new Player_and_Ratings()
                { p = p, pr = p.Player_Ratings.ToList(), pbt = pbt };

                r.Add(new Game_Player()
                {
                    bCarryingBall = fr.bCarryingBall,
                    Current_Vertical_Percent_Pos = fr.Vertical_Percent_Pos,
                    Current_YardLine = Line_of_Scrimmage + fr.YardLine,
                    Starting_Vertical_Percent_Pos = fr.Vertical_Percent_Pos,
                    Starting_YardLine = Line_of_Scrimmage + fr.YardLine,
                    Pos = fr.Pos,
                    p_and_r =  p_and_r,
                    State = fr.State,
                    Initial_State = fr.State
                });
                player_id++;
            }

            return r;
        }
        public static Game_Player getPunter(int f_id)
        {
            Game_Player r = new Game_Player();
            Player_NamesDAOTEST pnDAO = new Player_NamesDAOTEST();
            HomeTownsDAOTEST htDAO = new HomeTownsDAOTEST();
            Player p = Player_Helper.CreatePlayer(Player_Pos.P, true, false, true, 1, pnDAO, htDAO);
            Players_By_Team pbt = new Players_By_Team()
            { Franchise_ID = f_id, Player_ID = p.ID, Season_ID = 1 };
            Player_and_Ratings p_and_r = new Player_and_Ratings()
            { p = p, pr = p.Player_Ratings.ToList(), pbt = pbt };
            r.p_and_r = p_and_r;
            return r;
        }

    }
}
