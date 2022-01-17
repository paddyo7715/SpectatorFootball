using SpectatorFootball.DAO;
using SpectatorFootball.Enum;
using SpectatorFootball.League;
using SpectatorFootball.Models;
using SpectatorFootball.PlayerNS;
using SpectatorFootball.Team;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.Services
{
    class Game_Services
    {
        public Game geGamefromID(long game_id, Loaded_League_Structure lls)
        {
            Game r = null;
            string DIRPath_League = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + lls.season.League_Structure_by_Season[0].Short_Name.ToUpper();
            string League_con_string = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + lls.season.League_Structure_by_Season[0].Short_Name.ToUpper() + Path.DirectorySeparatorChar + lls.season.League_Structure_by_Season[0].Short_Name.ToUpper() + "." + app_Constants.DB_FILE_EXT;

            GameDAO gd = new GameDAO();

            r = gd.getGamefromID(game_id, League_con_string);

            return r;
        }
        public void SaveGame(Game g, Loaded_League_Structure lls)
        {
            string DIRPath_League = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + lls.season.League_Structure_by_Season[0].Short_Name.ToUpper();
            string League_con_string = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + lls.season.League_Structure_by_Season[0].Short_Name.ToUpper() + Path.DirectorySeparatorChar + lls.season.League_Structure_by_Season[0].Short_Name.ToUpper() + "." + app_Constants.DB_FILE_EXT;

            GameDAO gd = new GameDAO();
            gd.SaveGame(g, League_con_string);
            return;
        }
        public BoxScore getGameandStatsfromID(long game_id, Loaded_League_Structure lls)
        {
            BoxScore r = null;
            string DIRPath_League = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + lls.season.League_Structure_by_Season[0].Short_Name.ToUpper();
            string League_con_string = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + lls.season.League_Structure_by_Season[0].Short_Name.ToUpper() + Path.DirectorySeparatorChar + lls.season.League_Structure_by_Season[0].Short_Name.ToUpper() + "." + app_Constants.DB_FILE_EXT;

            GameDAO gd = new GameDAO();

            r = gd.getBoxScorefromID(game_id,lls.season.ID, League_con_string);

            return r;
        }

        public List<Player> GetTeamPlayersForGame(long f_id, Loaded_League_Structure lls)
        {
            //This method will return the players ready for a game sorted in the order of the depth
            //chart.  Due to injuries, if the team does not have enought or too many plaers at a 
            //position then the roster will be adjusted to have the correct number of players.
            List<Player> r = null;
            List<Player> add_players = new List<Player>();
            List<long> cut_players = new List<long>();
            TeamDAO tdao = new TeamDAO();

            string DIRPath_League = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + lls.season.League_Structure_by_Season[0].Short_Name.ToUpper();
            string League_con_string = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + lls.season.League_Structure_by_Season[0].Short_Name.ToUpper() + Path.DirectorySeparatorChar + lls.season.League_Structure_by_Season[0].Short_Name.ToUpper() + "." + app_Constants.DB_FILE_EXT;


            //Get the current players for this team
            List<Roster_rec> curr_players = tdao.getTeamRoster(lls.season.ID, f_id, League_con_string);
            //get the count per position, so that we can determine if players need to be dropped or singed.
            List<Pos_Player_Tot> ppt = curr_players.GroupBy(x => x.p.Pos).Select(x =>
               new Pos_Player_Tot
               {
                   Pos = (Player_Pos)x.Key,
                   num_players = x.Count()
               }).ToList();
            //next cycle thru each of the positions and decide if we need to add or cut players
            //at each position
            foreach (Player_Pos pp in System.Enum.GetValues(typeof(Player_Pos)))
            {
                int required_pos_num_players = Team_Helper.getNumPlayersByPosition(pp);
                int actual_pos_num_players = ppt.Where(x => x.Pos == pp).Select(x => x.num_players).First();
                //if the number of players at the position is less then what it need to be,
                //because of an injury then the team needs to sign free agent(s) for this position.
                if (actual_pos_num_players < required_pos_num_players)
                {
                    Player_and_Ratings new_player = null;
                    FreeAgencyDAO fdao = new FreeAgencyDAO();
                    List<Player_and_Ratings> pos_players = fdao.getBestFreeAgentbyPos(lls.season.ID, pp, League_con_string);

                    if (pos_players != null && pos_players.Count() > 0)
                    {
                        //Set the overall rating for each player
                        foreach (Player_and_Ratings pr in pos_players)
                        {
                            pr.Overall_Grade = Player_Helper.Create_Overall_Rating((Player_Pos)pr.p.Pos, pr.pr[0]);
                        }

                        //Sort the free agents by overall rating
                        pos_players = pos_players.OrderByDescending(x => x.Overall_Grade).ToList();
                        new_player = pos_players.First();
                    }
                    else
                    {
                        //stopped here. since no available free agents at the position requested could be found, we need to create a crappy player.
                        //and create a player_and_rating object for it.
                    }
                    //then I need to assign a new number for the player
                    //then create all the objects for the database
                }
                //don't forget that I also need to check if we have too many players at a position.
            }

                return r;
        }
    }
}
