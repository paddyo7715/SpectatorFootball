using SpectatorFootball.Awards;
using SpectatorFootball.DAO;
using SpectatorFootball.Enum;
using SpectatorFootball.Free_AgencyNS;
using SpectatorFootball.League;
using SpectatorFootball.Models;
using SpectatorFootball.PlayerNS;
using SpectatorFootball.Playoffs;
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
        public void SaveGame(Game g, List<Injury> lInj , Loaded_League_Structure lls)
        {
            string DIRPath_League = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + lls.season.League_Structure_by_Season[0].Short_Name.ToUpper();
            string League_con_string = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + lls.season.League_Structure_by_Season[0].Short_Name.ToUpper() + Path.DirectorySeparatorChar + lls.season.League_Structure_by_Season[0].Short_Name.ToUpper() + "." + app_Constants.DB_FILE_EXT;
            GameDAO gdao = new GameDAO();
            List<Playoff_Teams_by_Season> Playoff_Teams = new List<Playoff_Teams_by_Season>();
            List<Game> Playoff_Schedule = new List<Game>();
            Player_Awards Championship_MVP = null;
            long num_divs = lls.season.League_Structure_by_Season[0].Number_of_Divisions;

            //We must determine one of the following to see what else needs to be done when this game
            //is saved:
            //*  If this is the last game in the regualr season then the playoff teams will need to be
            //   seeded/written and the first playoff week schedule will need to be written.
            //*  if this is the last playoff game in the week then next week's playoff games will need
            //   to be created.
            //*  if this is a regular season game but not the last one or the championship game then
            //   nothing need to be done.
            switch (lls.LState)
            {
                case League_State.Regular_Season_in_Progress:
                    int Reg_Games_Left = gdao.NumUnplayedRegGames(lls.season.ID, League_con_string);
                    if (Reg_Games_Left == 1)
                    {
                        bool bGame_Played = gdao.isGamePlayed(g.ID, League_con_string);
                        if (!bGame_Played)
                        {
                            //If we get here then this is the last game of the season to  be saved,
                            //so it is necessary to pick the playoff teams and then write the first
                            //week of playoff games.
                            long num_confs = lls.season.League_Structure_by_Season[0].Number_of_Conferences;
                            long playoff_teams = lls.season.League_Structure_by_Season[0].Num_Playoff_Teams;
                            long Playoff_teams_per_Conf = num_confs == 2 ? playoff_teams / num_confs : playoff_teams;

                            List <Team_Wins_Loses_rec> w_recs = League_Helper.getFinalRegSeasonRecords(
                                lls.Standings, g,
                                num_confs,
                                num_divs);

                            if (num_confs == 2)
                            {
                                List<Playoff_Teams_by_Season> Playoff_Teams1 =
                                    Playoff_Helper.getConferencePlayoffTeams(
                                    lls.season.ID, 1, w_recs, Playoff_teams_per_Conf);
                                List<Playoff_Teams_by_Season> Playoff_Teams2 =
                                    Playoff_Helper.getConferencePlayoffTeams(
                                    lls.season.ID, 2, w_recs, Playoff_teams_per_Conf);
                                Playoff_Teams.AddRange(Playoff_Teams1);
                                Playoff_Teams.AddRange(Playoff_Teams2);

                            }
                            else
                            {
                                Playoff_Teams = Playoff_Helper.getConferencePlayoffTeams(
                                    lls.season.ID, 1, w_recs, Playoff_teams_per_Conf);
                            }

                            List<string> sched = Playoff_Helper.CreateWeeklyPlayoffSchedule(
                                Playoff_Teams, g.Week, num_divs);

                            foreach (string line in sched)
                            {
                                string[] m = line.Split(',');
                                string sWeek = m[0];
                                string ht = m[1];
                                string at = m[2];

                                Game pg = new Game()
                                {
                                    Week = long.Parse(sWeek),
                                    Away_Team_Franchise_ID = int.Parse(at),
                                    Home_Team_Franchise_ID = int.Parse(ht)
                                };

                                Playoff_Schedule.Add(pg);

                            }
                        }
                    }
                    break;
                case League_State.Playoffs_In_Progress:
                    PlayoffsDAO poDAO = new PlayoffsDAO();
                    List<Playoff_Teams_by_Season> ptList =
                        poDAO.getNonEliminatedPlayoffsTeams(lls.season.ID, League_con_string);
                    int Playoff_Games_Left = gdao.NumUnplayedPlayoffGames(lls.season.ID, League_con_string);

                    long losing_playoff_team = g.Home_Team_Franchise_ID > g.Away_Team_Franchise_ID ? g.Away_Team_Franchise_ID : g.Home_Team_Franchise_ID;
                    Playoff_Teams_by_Season elim_playoff_team = ptList.Where(x => x.Franchise_ID == losing_playoff_team).First();
                    elim_playoff_team.Eliminated = 1;
                    //if there is just 1 playoff team then in the dao method, I will know to edit it and not add.
                    Playoff_Teams.Add(elim_playoff_team);

                    //If we need to create a new playoff week schedule  
                    if (Playoff_Games_Left == 1 && g.Week != app_Constants.PLAYOFF_CHAMPIONSHIP_WEEK)
                    {
                        List<string> sched = Playoff_Helper.CreateWeeklyPlayoffSchedule(
                        Playoff_Teams, g.Week, num_divs);

                        foreach (string line in sched)
                        {
                            string[] m = line.Split(',');
                            string sWeek = m[0];
                            string ht = m[1];
                            string at = m[2];

                            Game pg = new Game()
                            {
                                Week = long.Parse(sWeek),
                                Away_Team_Franchise_ID = int.Parse(at),
                                Home_Team_Franchise_ID = int.Parse(ht)
                            };

                            Playoff_Schedule.Add(pg);

                        }
                    }
                    //if this is the championship game then we have to assign a MVP for the game
                    //and assign this award to the player
                    else if (g.Week == app_Constants.PLAYOFF_CHAMPIONSHIP_WEEK)
                    {
                        League_Stats lstats = new League_Stats();
                        lstats = Award_Helper.setLState_from_Game_stats(g);
                        List<Award_Rating_Rec> award_recs_list = Award_Helper.setPlayersMasterList(lstats);
                        Award_Rating_Rec ar = award_recs_list.OrderByDescending(x => x.Grade).First();
                        Championship_MVP = new Player_Awards();
                        Championship_MVP.Award_Code = app_Constants.CHAMPIONSHIP_MVP;
                        Championship_MVP.Season_ID = lls.season.ID;
                        Championship_MVP.Player_ID = ar.p.ID;
                    }
                    

                    //decide which team lost and their playoff teams by season record must be updated.
                    //note that we will know that the playoff team by season needs to be updated is if there
                    //is only 1 record and if there is more than 1 then they have to be added.
                    long losing_f_id = g.Home_Score > g.Away_Score ? g.Away_Team_Franchise_ID : g.Home_Team_Franchise_ID;
                    Playoff_Teams_by_Season Losing_Playoff_Team = ptList.Where(x => x.Franchise_ID == losing_f_id).First();
                    Losing_Playoff_Team.Eliminated = 1;
                    Playoff_Teams.Add(Losing_Playoff_Team);
                    break;
            }

            //if there were injuries in the game then create the injury log records
            List<Injury_Log> inj_log = new List<Injury_Log>();
            foreach (Injury ij in lInj)
                inj_log.Add(new Injury_Log() { Injured = 1, Season_ID = lls.season.ID, Player_ID = ij.Player_ID, Week = g.Week });

            gdao.SaveGame(g, lInj, inj_log, Playoff_Teams, Playoff_Schedule, Championship_MVP, League_con_string);
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
            List<Injury> del_Injuries = new List<Injury>();
            List<Injury_Log> injLog = new List<Injury_Log>();

            TeamDAO tdao = new TeamDAO();
            FreeAgencyDAO fdao = new FreeAgencyDAO();
            InjuriesDAO injDAO = new InjuriesDAO();
            ScheduleDAO schDAO = new ScheduleDAO();

            string DIRPath_League = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + lls.season.League_Structure_by_Season[0].Short_Name.ToUpper();
            string League_con_string = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + lls.season.League_Structure_by_Season[0].Short_Name.ToUpper() + Path.DirectorySeparatorChar + lls.season.League_Structure_by_Season[0].Short_Name.ToUpper() + "." + app_Constants.DB_FILE_EXT;

            //Get the injuries for this team to see if any of the players return this week
            //Only if this team has injuries do we need to calculate weeks in schedule to see
            //if they might return this week
            List<Injury> TInj = injDAO.GetTeamInjuredPlayers(lls.season.ID, f_id, League_con_string);
            if (TInj != null && TInj.Count > 0)
            {
                List<long> schedWeeks = schDAO.getWeeksinSched(lls.season.ID, League_con_string);
                int CurrentWeekIndex = schedWeeks.IndexOf(Week);
                foreach (Injury inj in TInj)
                {
                    if (inj.Season_Ending == 1 || inj.Career_Ending == 1)
                        continue;

                    int injuryIndex = schedWeeks.IndexOf(inj.Week);
                    int Length_of_injury = (int) inj.Num_of_Weeks;

                    //Check to see if the injured player's injury is over and if so
                    //then remove his injury, so that he is active again.
                    if ((CurrentWeekIndex- injuryIndex) >= Length_of_injury)
                    {
                        del_Injuries.Add(inj);
                        injLog.Add(new Injury_Log()
                        { Injured = 0, Player_ID = inj.Player_ID, Season_ID = lls.season.ID, Week = Week });
                    }
                }
                injDAO.ReturnPlayersFromInjury(del_Injuries, injLog, League_con_string);
            }

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
                        Player created_player = Player_Helper.CreatePlayer(pp, true, true, false, lls.season.ID);
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
