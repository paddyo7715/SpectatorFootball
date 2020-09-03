using System.Collections.Generic;
using System;
using System.ComponentModel;
using System.IO;
using log4net;
using System.Linq;
using SpectatorFootball.Models;
using System.Data.Entity.Validation;
using SpectatorFootball.League;
using SpectatorFootball.DAO;
using SpectatorFootball.Drafts;
using SpectatorFootball.Versioning;
using SpectatorFootball.Enum;
using System.Security.AccessControl;
using System.Security.Principal;

namespace SpectatorFootball
{
    public class League_Services
    {
        private static ILog logger = LogManager.GetLogger("RollingFile");

        public void CreateNewLeague(New_League_Structure nls, BackgroundWorker bw)
        {
            logger.Info("CreateNewLeague Started");

            var ts = new Team_Services();

            // Create the league folder
            string DIRPath_League = null;
            string New_League_File = nls.Season.League_Structure_by_Season[0].Short_Name.ToUpper() + "." + app_Constants.DB_FILE_EXT;
            string League_con_string = Environment.SpecialFolder.MyDocuments + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + nls.Season.League_Structure_by_Season[0].Short_Name + Path.DirectorySeparatorChar + nls.Season.League_Structure_by_Season[0].Short_Name + Path.DirectorySeparatorChar + app_Constants.DB_FILE_EXT;
            var LeagueDAO = new LeagueDAO();
            string process_state = "Processing...";
            string state_struct = null;
            int i = default(int);
            try
            {
                // Update the progress bar
                i = 5;
                process_state = "Creating League Folder Strucuture 1 of 5";
                state_struct = "Processing..." + "|" + process_state + "|" + "";
                bw.ReportProgress(i, state_struct);

                // Create the League Folder
                logger.Info("Creating league folder");
                DIRPath_League = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + nls.Season.League_Structure_by_Season[0].Short_Name.ToUpper();
                Directory.CreateDirectory(DIRPath_League);
                CommonUtils.SetFullAccess(DIRPath_League);

                // Create Backup Folder
                logger.Info("Creating league backup folder");
                Directory.CreateDirectory(DIRPath_League + Path.DirectorySeparatorChar + app_Constants.BACKUP_FOLDER);
                CommonUtils.SetFullAccess(DIRPath_League + Path.DirectorySeparatorChar + app_Constants.BACKUP_FOLDER);

                // Create the helmet image League Folder
                logger.Info("Creating league helmet folder");
                Directory.CreateDirectory(DIRPath_League + Path.DirectorySeparatorChar + app_Constants.LEAGUE_HELMETS_SUBFOLDER);
                CommonUtils.SetFullAccess(DIRPath_League + Path.DirectorySeparatorChar + app_Constants.LEAGUE_HELMETS_SUBFOLDER);

                // Create the stadium image League folder
                logger.Info("Creating league image folder");
                Directory.CreateDirectory(DIRPath_League + Path.DirectorySeparatorChar + app_Constants.LEAGUE_STADIUM_SUBFOLDER);
                CommonUtils.SetFullAccess(DIRPath_League + Path.DirectorySeparatorChar + app_Constants.LEAGUE_STADIUM_SUBFOLDER);

                // Copy the League Logo file
                logger.Info("Starting league Logo Copy and Rename");
                string sf = nls.Season.League_Structure_by_Season[0].League_Logo_Filepath;
                string df = DIRPath_League + Path.DirectorySeparatorChar + Path.GetFileName(sf);
                File.Copy(sf, df);

                //Create League Profile file
                using (StreamWriter sw = new StreamWriter(DIRPath_League + Path.DirectorySeparatorChar + app_Constants.LEAGUE_PROFILE_FILE))
                {
                    sw.WriteLine("LongName: " + nls.Season.League_Structure_by_Season[0].Long_Name);
                    sw.WriteLine("LogoFileName: " + Path.GetFileName(nls.Season.League_Structure_by_Season[0].League_Logo_Filepath));
                }

                // Copy and Create the league database file
                logger.Info("Starting league database creation and copy");
                string ssf = CommonUtils.getAppPath() + Path.DirectorySeparatorChar + app_Constants.BLANK_DB_FOLDER + Path.DirectorySeparatorChar + app_Constants.BLANK_DB;
                string ddf = DIRPath_League + Path.DirectorySeparatorChar + New_League_File;
                File.Copy(ssf, ddf);

                // Copy team image files to league folder
                logger.Info("Helmet and stadium files copy starting");
                foreach (var t in nls.Season.Teams_by_Season)
                {
                    logger.Debug("Copying " + t.Helmet_img_path);
                    File.Copy(t.Helmet_img_path, DIRPath_League + Path.DirectorySeparatorChar + app_Constants.LEAGUE_HELMETS_SUBFOLDER + Path.DirectorySeparatorChar + Path.GetFileName(t.Helmet_img_path));
                    logger.Debug("Copying " + t.Stadium_Img_Path);
                    File.Copy(t.Stadium_Img_Path, DIRPath_League + Path.DirectorySeparatorChar + app_Constants.LEAGUE_STADIUM_SUBFOLDER + Path.DirectorySeparatorChar + Path.GetFileName(t.Stadium_Img_Path));
                }

                // Update the progress bar
                i = 20;
                process_state = "Create Players for Draft 2 of 5";
                state_struct = "Processing..." + "|" + process_state + "|" + "";
                bw.ReportProgress(i, state_struct);

                //Create players for draft
                int num_players = (int)(((app_Constants.QB_PER_TEAM + app_Constants.RB_PER_TEAM + app_Constants.WR_PER_TEAM + app_Constants.TE_PER_TEAM + app_Constants.OL_PER_TEAM + app_Constants.DL_PER_TEAM + app_Constants.LB_PER_TEAM + app_Constants.DB_PER_TEAM + app_Constants.K_PER_TEAM + app_Constants.P_PER_TEAM) * nls.Season.League_Structure_by_Season[0].Num_Teams ) * app_Constants.DRAFT_MULTIPLIER);

                List<Player> new_player_list = League_Helper.Create_New_Players(num_players);

                nls.Players = new List<Player>();
                foreach (Player p in new_player_list)
                {
                    nls.Players.Add(p);
//                    nls.Season.Player_Ratings.Add(p.Player_Ratings.First());
                }

                i = 50;
                process_state = "Create Players for Draft 3 of 5";
                state_struct = "Processing..." + "|" + process_state + "|" + "";
                bw.ReportProgress(i, state_struct);

                //Create Draft
                int draft_rounds = (app_Constants.QB_PER_TEAM + app_Constants.RB_PER_TEAM + app_Constants.WR_PER_TEAM + app_Constants.TE_PER_TEAM + app_Constants.OL_PER_TEAM + app_Constants.DL_PER_TEAM + app_Constants.LB_PER_TEAM + app_Constants.DB_PER_TEAM + app_Constants.K_PER_TEAM + app_Constants.P_PER_TEAM);
                Draft_Helper DH = new Draft_Helper();
                List<Draft> Draft_List = DH.Create_Draft(nls.Season.Year-1,nls.Season.League_Structure_by_Season[0].Draft_Type_Code, draft_rounds, nls.Franchises ,null);
                foreach (Draft d in Draft_List)
                {
                    nls.Season.Drafts.Add(d);
                }

                // Update the progress bar
                i = 70;
                process_state = "Creating Schedule 4 of 5";
                state_struct = "Processing..." + "|" + process_state + "|" + "";
                bw.ReportProgress(i, state_struct);

                // Creating schedule
                logger.Info("Creating schedule");
                List<string> sched = create_schedule(nls.Season.League_Structure_by_Season[0].Short_Name, (int) nls.Season.League_Structure_by_Season[0].Num_Teams, nls.Season.Divisions.Count, nls.Season.Conferences.Count, (int) nls.Season.League_Structure_by_Season[0].Number_of_Games, (int) nls.Season.League_Structure_by_Season[0].Number_of_weeks);

                foreach (string line in sched)
                {
                    string[] m = line.Split(',');
                    string sWeek = m[0];
                    string ht = m[1];
                    string at = m[2];

                    if (sWeek.StartsWith("Week"))
                        continue;

                    Game g = new Game()
                    {
                        Week = long.Parse(sWeek),
                        Home_Team_Franchise_ID = nls.Season.Teams_by_Season.Where(x => x.Team_Slot == long.Parse(ht)).First().Franchise_ID,
                        Away_Team_Franchise_ID = nls.Season.Teams_by_Season.Where(x => x.Team_Slot == long.Parse(at)).First().Franchise_ID
                    };

                    nls.Season.Games.Add(g);

                }

                // Update the progress bar
                i = 85;
                process_state = "Saving League to Database 5 of 5";
                state_struct = "Processing..." + "|" + process_state + "|" + "";
                bw.ReportProgress(i, state_struct);

                // Write the league records to the database
                logger.Info("Saving new league to database");
                LeagueDAO.Create_New_League(nls,ddf);

                //Backup the database file as season started
                string backed_up_file = DIRPath_League + Path.DirectorySeparatorChar + app_Constants.BACKUP_FOLDER + Path.DirectorySeparatorChar + nls.Season.Year + "_SeasonStarted_" + New_League_File;
                File.Copy(df, backed_up_file);

                // Update the progress bar
                i = 100;
                process_state = "";
                state_struct = "Complete" + "|" + process_state + "|" + "League Completed Successfully!";
                bw.ReportProgress(i, state_struct);
            }
            catch (DbEntityValidationException e)
            {
                state_struct = "Error" + "|" + process_state + "|" + "Failed to Create New League";
                bw.ReportProgress(i, state_struct);

                Directory.Delete(DIRPath_League, true);

                foreach (var eve in e.EntityValidationErrors)
                {
                    logger.Error("Entity of type \"{0}\" in state \"{1}\" has the following validation errors: entry: " +
                        eve.Entry.Entity.GetType().Name + " stat: " + eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        logger.Error("- Property: \"{0}\", Error: \"{1}\" property name: " + 
                            ve.PropertyName + "Error message: " + ve.ErrorMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                state_struct = "Error" + "|" + process_state + "|" + "Failed to Create New League";
                bw.ReportProgress(i, state_struct);
                
                Directory.Delete(DIRPath_League,true);

                logger.Error("Create league service failed");
                if (ex.InnerException != null)
                    logger.Error("Inner Exception: " + ex.InnerException.ToString());
                logger.Error(ex);
            }
        }
        public List<string> create_schedule(string Short_Name, int Num_Teams, int Num_Divisions, int Num_Conferences, int Number_of_Games, int Number_of_weeks)
        {
            int Number_of_Tries = 100;
            var ls = new Schedule(Short_Name, Num_Teams, Num_Teams / Num_Divisions, Num_Conferences, Number_of_Games, Number_of_weeks - Number_of_Games);
            string r = "Before attempting schedule";

            List<string> s = null;

            // allow up to 10 tries if the schedule validation fails.
            for (int i = 0; i <= Number_of_Tries; i++)
            {
                s = ls.Generate_Regular_Schedule();
                logger.Debug("Possible League Schedule Created");
                var val_sched = new Validate_Sched(Short_Name, Num_Teams, Num_Teams / Num_Divisions, Num_Conferences, Number_of_Games, Number_of_weeks - Number_of_Games);
                r = val_sched.Validate(s);
                if (r == null)
                {
                    logger.Debug("Schedule validated");
                    break;
                }
            }

            if (r != null)
                throw new Exception("Error creating schedule. Error: " + r);

            return s;
        }

        public string[] CheckDBVersion(string League_Shortname)
        {
            int r;
            string League_con_string = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + League_Shortname + Path.DirectorySeparatorChar + League_Shortname + "." + app_Constants.DB_FILE_EXT;

            logger.Info("Loading: " + League_con_string);

            DBVersionDAO dbverdbo = new DBVersionDAO();
            DBVersion dbversion = dbverdbo.getLatestDBVersion(League_con_string);

            logger.Info("db version of database file is " + dbversion.Version);
            logger.Info("Version of program          is " + App_Version.APP_VERSION);

            App_Version aver = new App_Version();

            r = aver.isCompatibleVersion(dbversion.Version);

            if (r == 0)
                logger.Info("Program and Database versions are compatible");
            else if (r == 2)
                logger.Info("Program and Database versions are not compatible");
            else
            {
                logger.Info("Program and Database versions are not compatible");
                //backup database before upgrade
                //put code here to upgrade the database
            }

            return new string[] {r.ToString(), dbversion.Version, App_Version.APP_VERSION };
        }

        public void UpgradeDB(string League_Shortname, string dbVersion, string ProgramVersion)
        {
            //First backup the database file with a special name that indicates the current
            //version number
            //then upgrade that database

        }

        public Season LoadSeason(string year,string League_Shortname)
        {
            Season r;
            string League_con_string = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + League_Shortname + Path.DirectorySeparatorChar + League_Shortname + "." + app_Constants.DB_FILE_EXT;
            LeagueDAO ld = new LeagueDAO();

            r = ld.LoadSeason(year, League_con_string);

            return r;
        }

        public League_State getSeasonState(string year, long Season_ID, string League_Shortname)
        {
            League_State r;
            string League_con_string = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + League_Shortname + Path.DirectorySeparatorChar + League_Shortname + "." + app_Constants.DB_FILE_EXT;
            LeagueDAO ld = new LeagueDAO();

            r = League_State.Season_Started;

            if (year == null)
                r = League_State.Previous_Year;
            else
            {
                int[] m = ld.getSeasonTableTotals(Season_ID, League_con_string);

                int draft_count = m[0];
                int draft_completed_count = m[1];
                int training_camp_count = m[2];
                int Unplayed_Regular_Season_Games_Count = m[3];
                int playoff_teams_count = m[4];
                int Unplayed_Playoff_Games_Count = m[5];
                int player_awards_count = m[6];

                if (draft_count == 0)
                    r = League_State.Draft_Completed;
                else if (draft_count > 0 && draft_completed_count > 0)
                    r = League_State.Draft_Started;

                if (training_camp_count > 0)
                    r = League_State.Training_Camp_Ended;

                if (Unplayed_Regular_Season_Games_Count > 0)
                    r = League_State.Regular_Season_in_Progress;
                else
                    r = League_State.Regular_Season_Ended;

                if (playoff_teams_count > 0)
                {
                    if (Unplayed_Playoff_Games_Count > 0)
                        r = League_State.Playoffs_In_Progress;
                    else
                        r = League_State.Playoffs_Ended;
                }

                if (player_awards_count > 0)
                    r = League_State.Season_Ended;
            }

                return r;

            }

        public List<Standings_Row> getLeageStandings(long Season_ID, string League_Shortname)
        {
            List<Standings_Row> r;
            string League_con_string = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + League_Shortname + Path.DirectorySeparatorChar + League_Shortname + "." + app_Constants.DB_FILE_EXT;
            LeagueDAO ld = new LeagueDAO();

            r = ld.getStandings(Season_ID, League_con_string);

            List<Standing_Streak> sstreak = ld.getStandingsStreak(Season_ID, League_con_string);

            //Now we need to set the streak in the standings list for each team.  
            int team_id = -1;
            foreach (Standing_Streak ss in sstreak)
            {
                if (team_id != ss.team_id)
                {
                    string streak_char = ss.Result + ss.Games;
                    foreach (Standings_Row sr in r)
                    {
                        if (sr.Team_ID == ss.team_id)
                        {
                            sr.Streakchar = streak_char;
                            break;
                        }
                        else
                            continue;
                    }
                }
            }

            return r;
        }



    }
}
