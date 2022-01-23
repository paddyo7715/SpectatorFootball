using SpectatorFootball.DAO;
using SpectatorFootball.Enum;
using SpectatorFootball.Free_AgencyNS;
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

        public List<Player_and_Ratings> GetTeamPlayersForGame(long f_id, long Week, Loaded_League_Structure lls)
        {
            //This method will return the players ready for a game sorted in the order of the depth
            //chart.  Due to injuries, if the team does not have enought or too many plaers at a 
            //position then the roster will be adjusted to have the correct number of players.
            List<Player_and_Ratings> r = null;
//            List<Player_and_Ratings> add_players = new List<Player_and_Ratings>();
//            List<Player_and_Ratings> cut_players = new List<Player_and_Ratings>();
            TeamDAO tdao = new TeamDAO();
            FreeAgencyDAO fdao = new FreeAgencyDAO();

            string DIRPath_League = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + lls.season.League_Structure_by_Season[0].Short_Name.ToUpper();
            string League_con_string = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + lls.season.League_Structure_by_Season[0].Short_Name.ToUpper() + Path.DirectorySeparatorChar + lls.season.League_Structure_by_Season[0].Short_Name.ToUpper() + "." + app_Constants.DB_FILE_EXT;

//            List<Pos_and_Count> ppt = tdao.GetTeamPlayerPosCounts(lls.season.ID, f_id, League_con_string);

            //next cycle thru each of the positions and decide if we need to add or cut players
            //at each position
            foreach (Player_Pos pp in System.Enum.GetValues(typeof(Player_Pos)))
            {
                int required_pos_num_players = Team_Helper.getNumPlayersByPosition(pp);
                int actual_pos_num_players = tdao.GetTeamPosCount(lls.season.ID, f_id, (long)pp, League_con_string);
                //                int actual_pos_num_players = ppt.Where(x => x.pos == (int)pp).Select(x => x.pos_count).First();
                //if the number of players at the position is less then what it need to be,
                //because of an injury then the team needs to sign free agent(s) for this position.
                while (actual_pos_num_players < required_pos_num_players)
                {
                    Player_and_Ratings new_player = null;
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
                        Player created_player = Player_Helper.CreatePlayer(pp, true, true, false);
                        //Set the season id on the player_rating, since it is not
                        //set from the method
                        List<Player_Ratings> plr = created_player.Player_Ratings.ToList();
                        plr[0].Season_ID = lls.season.ID;

                        Player_DAO pda = new Player_DAO();
                        pda.AddSinglePlayer(created_player, League_con_string);

                        new_player = new Player_and_Ratings()
                        { p = created_player, pr = plr };
                    }
                    //Get a unique number on the team for a player
                    List<Players_By_Team> pbt = tdao.getTeamPlayersbyTeam(lls.season.ID, f_id, League_con_string);
                    int jersey_number = Team_Helper.getFreePlayerNumber(pbt, pp);

                    //Add the player_by_team record to the new player object
                    Players_By_Team pt = new Players_By_Team()
                    { Franchise_ID = f_id, Season_ID = lls.season.ID, Jersey_Number = jersey_number };
                    new_player.pbt = pt;

                    Free_Agency fa_entity = new Free_Agency()
                    {
                        Week = Week,
                        Signed = 1,
                        Season_ID = lls.season.ID,
                        Player_ID = new_player.p.ID,
                        Franchise_ID = f_id
                    };

                    fdao.SelectPlayer(pt, fa_entity, League_con_string);

                    actual_pos_num_players = tdao.GetTeamPosCount(lls.season.ID, f_id, (long)pp, League_con_string);
                }

                while (actual_pos_num_players > required_pos_num_players) {
                    List<Player_and_Ratings> pos_players = tdao.getPosPlayers(lls.season.ID, f_id, (int)pp, League_con_string);

                    //Set the overall rating for each player
                    foreach (Player_and_Ratings pr in pos_players)
                    {
                        pr.Overall_Grade = Player_Helper.Create_Overall_Rating((Player_Pos)pr.p.Pos, pr.pr[0]);
                    }
                    //sort the pos_players by overall grade descending
                    pos_players = pos_players.OrderByDescending(x => x.Overall_Grade).ToList();

                    //Now cycle all the players on the team for this position and remove
                    //the bottom players that don't make the cut.
                    int i = 0;
                    foreach(Player_and_Ratings pr in pos_players)
                    {
                        if (i < actual_pos_num_players)
                            continue;
                        else
                        {
                            //remove this player from the team
                            Free_Agency fa_entity = new Free_Agency()
                            {
                                Week = Week,
                                Signed = 0,
                                Season_ID = lls.season.ID,
                                Player_ID = pr.p.ID,
                                Franchise_ID = f_id
                            };

                            fdao.ReleasePlayersandFreeAgency(pr.pbt, fa_entity, League_con_string);
                        }

                        i++;
                    }
                }
            }

            r = tdao.getTeamPlayers(lls.season.ID, f_id, League_con_string);
            //Set the overall ratings for players and now sort the players in the order of position and rating
            //for roster depth
            foreach (Player_and_Ratings pr in r)
            {
                pr.Overall_Grade = Player_Helper.Create_Overall_Rating((Player_Pos)pr.p.Pos, pr.pr[0]);
            }
            r = r.OrderByDescending(x => x.p.Pos).ThenByDescending(x => x.Overall_Grade).ToList();

            return r;
        }
    }
}
