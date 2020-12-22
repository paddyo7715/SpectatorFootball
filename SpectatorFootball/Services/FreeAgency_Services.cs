using SpectatorFootball.DAO;
using SpectatorFootball.Enum;
using SpectatorFootball.Free_Agency;
using SpectatorFootball.League;
using SpectatorFootball.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SpectatorFootball.Services
{
    class FreeAgency_Services
    {
        public List<FreeAgencyTrans> GetFreeAgentTransList(Loaded_League_Structure lls)
        {
            List<FreeAgencyTrans> r = null;
            string DIRPath_League = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + lls.season.League_Structure_by_Season[0].Short_Name.ToUpper();

            string League_con_string = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + lls.season.League_Structure_by_Season[0].Short_Name.ToUpper() + Path.DirectorySeparatorChar + lls.season.League_Structure_by_Season[0].Short_Name.ToUpper() + "." + app_Constants.DB_FILE_EXT;
            FreeAgencyDAO fad = new FreeAgencyDAO();

            r = fad.GetYearlyFATrans(lls.season.ID, League_con_string);

            foreach (FreeAgencyTrans f in r)
            {
                string helmet_filename = f.helmet_filename;
                f.HelmetImage = lls.getHelmetImg(helmet_filename);

                if (f.Pick_Pos_Name != null && f.Pick_Pos_Name.Trim().Length > 0)
                {
                    string[] m = f.Pick_Pos_Name.Split(' ');
                    int ipos = int.Parse(m[0]);
                    Player_Pos ppos = (Player_Pos)ipos;
                    string pick_name = f.Pick_Pos_Name.Substring(1);
                    f.Pick_Pos_Name = ppos.ToString() + " " + pick_name;
                }
            }

            return r;
        }

        public List<Player_and_Ratings> GetFreeAgentsList(Loaded_League_Structure lls)
        {
            List<Player_and_Ratings> r = null;
            string DIRPath_League = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + lls.season.League_Structure_by_Season[0].Short_Name.ToUpper();
            int iFirstKicker = System.Enum.GetNames(typeof(Player_Pos)).Length - 2;

            string League_con_string = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + lls.season.League_Structure_by_Season[0].Short_Name.ToUpper() + Path.DirectorySeparatorChar + lls.season.League_Structure_by_Season[0].Short_Name.ToUpper() + "." + app_Constants.DB_FILE_EXT;
            FreeAgencyDAO fad = new FreeAgencyDAO();

            r = fad.getFreeAgents(lls.season.ID, League_con_string);

            //Set the overall rating for each player
            foreach (Player_and_Ratings pr in r)
            {
                pr.Overall_Grade = Player_Helper.Create_Overall_Rating((Player_Pos)pr.p.Pos, pr.pr[0]);
            }

            //Sort the free agents by overall rating
            r = r.OrderByDescending(x => x.Overall_Grade).ToList();


            return r;
        }
        //A returned null means no player selected, so this team has enough players
        public Player Select_Free_Agent(Loaded_League_Structure lls,long Franchise_ID, List<Player_and_Ratings> Free_Agents, List<FreeAgencyTrans> faTransList)
        {
            Player r = null;
            List<Player_Pos> Needed_pos = new List<Player_Pos>();
            long Season_ID = lls.season.League_Structure_by_Season[0].Season_ID;
            string League_Shortname = lls.season.League_Structure_by_Season[0].Short_Name;

            string DIRPath_League = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + League_Shortname.ToUpper();
            string League_con_string = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + League_Shortname + Path.DirectorySeparatorChar + League_Shortname + "." + app_Constants.DB_FILE_EXT;
            TeamDAO td = new TeamDAO();

            //First get each position on the team and the number of players at each position
            List<Pos_and_Count> p = td.GetTeamPlayerPosCounts(Season_ID, Franchise_ID, League_con_string);

            //Only if the team needs to select more players
            if (p.Count() < app_Constants.TRAINING_CAMP_TEAM_PLAYER_COUNT)
            {
                foreach (Player_Pos pp in System.Enum.GetValues(typeof(Player_Pos)))
                {
                    int number_at_position = 0;
                    int iPos = (int)pp;
                    Pos_and_Count pc = p.Where(x => x.pos == iPos).FirstOrDefault();
                    if (pc != null)
                        number_at_position = pc.pos_count;

                    if (!Team_Helper.isTooManyPosPlayersCamp(pp, number_at_position))
                        Needed_pos.Add(pp);

                }

                //Now start looking at each free agent ordered by rating and select a free agent if the
                //are in your needed position list.  If a player can not be found, create one.
                List<Player_and_Ratings> available_free_agents = Free_Agents.Where(x => x.p.Franchise_ID == null).OrderByDescending(x => x.Overall_Grade).ToList();
                foreach (Player_and_Ratings par in available_free_agents)
                {
                    if (Needed_pos.Contains((Player_Pos)par.p.Pos))
                    {
                        par.p.Franchise_ID = Franchise_ID;
                        r = par.p;
                        break;    
                    }
                }
                //If a player could not be found then assign a new, not very good, player
                if (r == null)
                {
                    Player new_player = Player_Helper.CreatePlayer(Needed_pos[0], true);
                    Player_Helper.CrappifyPlayerRatings(new_player.Player_Ratings.First());
                    double new_overall_rating = Player_Helper.Create_Overall_Rating((Player_Pos)new_player.Pos, new_player.Player_Ratings.First());

                    Player_DAO pda = new Player_DAO();
                    pda.AddSinglePlayer(new_player, League_con_string);

                    r = new_player;
                    Free_Agents.Add(new Player_and_Ratings() { p = new_player, pr = new_player.Player_Ratings.ToList(), Overall_Grade = new_overall_rating });
                }

                r.Franchise_ID = Franchise_ID;

                Teams_by_Season tbs = td.getTeamfromFranchiseID(Season_ID, Franchise_ID, League_con_string);
                string helmet_filename = tbs.Helmet_Image_File;
                BitmapImage HelmetImage = lls.getHelmetImg(tbs.Helmet_Image_File);
                string Team_Name = tbs.City + " " + tbs.Nickname;
                string Pick_Pos_Name = (r.Pos + " " + r.First_Name + " " + r.Last_Name).Trim();

                FreeAgencyTrans ft = new FreeAgencyTrans()
                {
                    Franchise_ID = Franchise_ID,
                    HelmetImage = HelmetImage,
                    helmet_filename = helmet_filename,
                    Season_ID = Season_ID,
                    Team_Name = Team_Name,
                    Pick_Pos_Name = Pick_Pos_Name
                };
                faTransList.Add(ft);

                FreeAgencyDAO fad = new FreeAgencyDAO();
                fad.SelectPlayer(r, ft, League_con_string);
            }

            return r;
        }
    }
}
