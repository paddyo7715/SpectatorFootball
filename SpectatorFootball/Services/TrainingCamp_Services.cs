using SpectatorFootball.DAO;
using SpectatorFootball.League;
using SpectatorFootball.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SpectatorFootball.Enum;
using SpectatorFootball.Training_CampNS;
using SpectatorFootball.PlayerNS;

namespace SpectatorFootball.Services
{
    class TrainingCamp_Services
    {
        public List<TrainingCampStatus> getTrainingCampStatuses(Loaded_League_Structure lls)
        {
            List<TrainingCampStatus> r = null;

            string League_Shortname = lls.season.League_Structure_by_Season[0].Short_Name;
            long season_id = lls.season.ID;

            string DIRPath_League = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + League_Shortname.ToUpper();
            string League_con_string = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + League_Shortname + Path.DirectorySeparatorChar + League_Shortname + "." + app_Constants.DB_FILE_EXT;
            TrainingCampDAO tcd = new TrainingCampDAO();

            r = tcd.getTrainingCampStatuses(season_id, League_con_string);

            foreach (TrainingCampStatus d in r)
            {
                string helmet_filename = d.helmet_filename;
                d.HelmetImage = lls.getHelmetImg(helmet_filename);

            }

            return r;
        }

        public void Execute_Team_TrainingCamp(long franchise_id, long season_id, string League_Shortname)
        {
            //For testing player draft versus overall and training camp results
//            StreamWriter sw = new StreamWriter("C:\\Database\\Players.txt", true);

            string DIRPath_League = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + League_Shortname.ToUpper();
            string League_con_string = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + League_Shortname + Path.DirectorySeparatorChar + League_Shortname + "." + app_Constants.DB_FILE_EXT;

            List<Players_By_Team> Updated_Players = new List<Players_By_Team>();
            List<Players_By_Team> Made_Players = new List<Players_By_Team>();
            List<Players_By_Team> Cut_Players = new List<Players_By_Team>();

            List<Free_Agency> F_Trans = new List<Free_Agency>();
            List<Training_Camp_by_Season> tc_list = new List<Training_Camp_by_Season>();

            TrainingCampDAO tDAO = new TrainingCampDAO();
            List<Player_and_Ratings_and_Draft> prd_list = tDAO.getTrainingCampPlayers(franchise_id, season_id, League_con_string);
            List<Player> PlayersMadeTeam = new List<Player>();
            List<Player> PlayersCut = new List<Player>();
            List<Free_Agency> CutTransactions = new List<Free_Agency>();

            //Set the starting of their draft grade which is either the overall grad or
            //draft profile grade if this is the player's first year.
            foreach (Player_and_Ratings_and_Draft p in prd_list)
            {
                bool bJust_draft = p.bJust_Drafted;
                Player player = p.player;
                Player_Pos ppos = (Player_Pos)player.Pos;
                if (!bJust_draft)
                    p.Grade = Player_Helper.Create_Overall_Rating(ppos, p.pr[0]);
                else
                    p.Grade = player.Draft_Grade;
            }

            foreach (Player_Pos pp in System.Enum.GetValues(typeof(Player_Pos)))
            {
                List<Player_and_Ratings_and_Draft> posResultList = null;
                switch (pp)
                {
                    case Player_Pos.QB:
                        posResultList = TrainingCamp_Helper.TrainingCampQB(prd_list);
                        break;
                    case Player_Pos.RB:
                        posResultList = TrainingCamp_Helper.TrainingCampRB(prd_list);
                        break;
                    case Player_Pos.WR:
                        posResultList = TrainingCamp_Helper.TrainingCampWR(prd_list);
                        break;
                    case Player_Pos.TE:
                        posResultList = TrainingCamp_Helper.TrainingCampTE(prd_list);
                        break;
                    case Player_Pos.OL:
                        posResultList = TrainingCamp_Helper.TrainingCampOL(prd_list);
                        break;
                    case Player_Pos.DL:
                        posResultList = TrainingCamp_Helper.TrainingCampDL(prd_list);
                        break;
                    case Player_Pos.LB:
                        posResultList = TrainingCamp_Helper.TrainingCampLB(prd_list);
                        break;
                    case Player_Pos.DB:
                        posResultList = TrainingCamp_Helper.TrainingCampDB(prd_list);
                        break;
                    case Player_Pos.K:
                        posResultList = TrainingCamp_Helper.TrainingCampFG(prd_list);
                        break;
                    case Player_Pos.P:
                        posResultList = TrainingCamp_Helper.TrainingCampP(prd_list);
                        break;
                }
                //Take into account a younger player's potential when deciding to which players to
                //keep on the team by reducing the training camp score of older players
                TrainingCamp_Helper.AdjustGradeforAge(posResultList);

                //next add to the free agency transaction and player lists from the 
                //posResultList based on that bool of wether they made the team
                int Num_Team_Slots = Team_Helper.getNumPlayersByPosition(pp);

                //The reason the list is sorted in ascending order is that I would want to
                //clear the franchise id and jersey number from players that are cut from
                //the team first.  Note that these players may or may not have a jersey
                //number depending on if they were on the team last year.
                //This is helpful so that for a player that does make the team and does
                //not yet have a jersey number can get one of the freed up.
                posResultList = posResultList.OrderBy(x => x.Grade).ToList();

//For testing player ratings and draft profile and training camp grade
//                sw.WriteLine("Franchise id: " + franchise_id);
                int icount = posResultList.Count();
                foreach (Player_and_Ratings_and_Draft p in posResultList)
                {

                    //For testing player ratings and draft profile and training camp grade
//                    sw.WriteLine(p.p.ID + " " + pp.ToString() +  " O: " + Player_Helper.Create_Overall_Rating(pp,p.pr.First())
//                        + " D: " + p.p.Draft_Grade + " T: " + p.Grade + " Age: " + p.p.Age);

                    if (icount > Num_Team_Slots)
                    {
                        //These players are cut
                        Free_Agency fa = new Free_Agency()
                        {
                            Franchise_ID = franchise_id,
                            Player_ID = p.p.Player_ID,
                            Season_ID = season_id,
                            Signed = 0,
                            Week = app_Constants.TRAINING_CAMP_WEEK
                        };
                        Training_Camp_by_Season tcamp = new Training_Camp_by_Season()
                        {
                            Franchise_ID = franchise_id,
                            Player_ID = p.p.Player_ID,
                            Season_ID = season_id,
                            Grade = (long)p.Grade,
                            Made_Team = 0
                        };
                        F_Trans.Add(fa);
                        tc_list.Add(tcamp);
                        //in this case -1 means no team, but it doesn't matter since this will not be written to the databse this way.  This record will be deleted.
                        p.p.Franchise_ID = -1;  
                        p.p.Jersey_Number = null;
                    }
                    else
                    {
                        //The player has made the team
                        if (p.p.Jersey_Number == null)
                        {
                            p.p.Jersey_Number = Team_Helper.getFreePlayerNumber(Updated_Players, pp);

/*
                            for (int jj = 0; jj < 50; jj++)
                            {
                                j_number = Player_Helper.getPlayerNumber(pp);
                                if (!Updated_Players.Any(x => x.Jersey_Number == j_number))
                                {
                                    p.p.Jersey_Number = j_number;
                                    break;
                                }
                            }

                            //if we still couldn't find a number for this player then search other positions
                            //since I intend to cap the number of injuries to about 40 per team per year
                            //There will always be a number free.
                            if (p.p.Jersey_Number == null)
                            {
                                for (int jj = 1; jj < 1000; jj++)
                                {
                                    if (!Updated_Players.Any(x => x.Jersey_Number == jj))
                                    {
                                        p.p.Jersey_Number = jj;
                                        break;
                                    }

                                }
                            }
*/

                        }
                        Training_Camp_by_Season tcamp = new Training_Camp_by_Season()
                        {
                            Franchise_ID = franchise_id,
                            Player_ID = p.p.Player_ID,
                            Season_ID = season_id,
                            Grade = (long)p.Grade,
                            Made_Team = 1
                        };
                        tc_list.Add(tcamp);
                    }
                    Updated_Players.Add(p.p);
                    icount--;
                }

                Made_Players = Updated_Players.Where(x => x.Jersey_Number != null).ToList();
                Cut_Players =  Updated_Players.Where(x => x.Jersey_Number == null).ToList();

            }

            //For testing player ratings and draft profile and training camp grade
            //            sw.Close();

            TrainingCampDAO tcDAO = new TrainingCampDAO();
            tcDAO.updatePlayersandFreeAgency(Made_Players, Cut_Players, F_Trans, tc_list, League_con_string);

        }
        public TrainingCampResults getPlayersTrainingCampResult(long franchise_id, long season_id, string League_Shortname)
        {
            TrainingCampResults r = null;

            string DIRPath_League = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + League_Shortname.ToUpper();
            string League_con_string = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + League_Shortname + Path.DirectorySeparatorChar + League_Shortname + "." + app_Constants.DB_FILE_EXT;
            TrainingCampDAO tcd = new TrainingCampDAO();

            List<Player> MadePlayers = tcd.getPlayersTrainingCampMade(franchise_id, season_id, League_con_string);
            List<Player> CutPlayers = tcd.getPlayersTrainingCampCut(franchise_id, season_id, League_con_string);

            r = new TrainingCampResults();

            r.OffMade = MadePlayers.Where(x => Player_Helper.isOffense(x.Pos)).OrderBy(x => x.Pos).ToList();
            r.DefMade = MadePlayers.Where(x => !Player_Helper.isOffense(x.Pos)).OrderBy(x => x.Pos).ToList();
            r.OffCut = CutPlayers.Where(x => Player_Helper.isOffense(x.Pos)).OrderBy(x => x.Pos).ToList();
            r.DefCut = CutPlayers.Where(x => !Player_Helper.isOffense(x.Pos)).OrderBy(x => x.Pos).ToList();

            return r;
        }
    }
}

