﻿using System.Collections.Generic;
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
                i = 10;
                process_state = "Creating League Folder Strucuture 1 of 4";
                state_struct = "Processing..." + "|" + process_state + "|" + "";
                bw.ReportProgress(i, state_struct);

                // Create the League Folder
                logger.Info("Creating league folder");
                DIRPath_League = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + nls.Season.League_Structure_by_Season[0].Short_Name.ToUpper();
                Directory.CreateDirectory(DIRPath_League);

                // Create Backup Folder
                logger.Info("Creating league backup folder");
                Directory.CreateDirectory(DIRPath_League + Path.DirectorySeparatorChar + app_Constants.BACKUP_FOLDER);

                // Create the helmet image League Folder
                logger.Info("Creating league helmet folder");
                Directory.CreateDirectory(DIRPath_League + Path.DirectorySeparatorChar + app_Constants.LEAGUE_HELMETS_SUBFOLDER);

                // Create the stadium image League folder
                logger.Info("Creating league image folder");
                Directory.CreateDirectory(DIRPath_League + Path.DirectorySeparatorChar + app_Constants.LEAGUE_STADIUM_SUBFOLDER);

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
                i = 25;
                process_state = "Create Players for Draft 2 of 4";
                state_struct = "Processing..." + "|" + process_state + "|" + "";
                bw.ReportProgress(i, state_struct);

                //Create players for draft
                int num_players = (int)(((app_Constants.QB_PER_TEAM + app_Constants.RB_PER_TEAM + app_Constants.WR_PER_TEAM + app_Constants.TE_PER_TEAM + app_Constants.OL_PER_TEAM + app_Constants.DL_PER_TEAM + app_Constants.LB_PER_TEAM + app_Constants.DB_PER_TEAM + app_Constants.K_PER_TEAM + app_Constants.P_PER_TEAM) * nls.Season.League_Structure_by_Season[0].Num_Teams ) * app_Constants.DRAFT_MULTIPLIER);

                List<Player> new_player_list = League_Helper.Create_New_Players(num_players);

                foreach (Player p in new_player_list)
                    nls.Season.Player_Ratings.Add(p.Player_Ratings.First()); ;

                //Create Draft
                int draft_rounds = (int)(((app_Constants.QB_PER_TEAM + app_Constants.RB_PER_TEAM + app_Constants.WR_PER_TEAM + app_Constants.TE_PER_TEAM + app_Constants.OL_PER_TEAM + app_Constants.DL_PER_TEAM + app_Constants.LB_PER_TEAM + app_Constants.DB_PER_TEAM + app_Constants.K_PER_TEAM + app_Constants.P_PER_TEAM) * nls.Season.League_Structure_by_Season[0].Num_Teams));
                Draft_Helper DH = new Draft_Helper();
                List<Draft> Draft_List = DH.Create_Draft(nls.Season.Year-1,nls.Season.League_Structure_by_Season[0].Draft_Type_Code, draft_rounds, nls.Franchises ,ddf);

                // Update the progress bar
                i = 50;
                process_state = "Creating Schedule 3 of 4";
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
                i = 75;
                process_state = "Saving League to Database 4 of 4";
                state_struct = "Processing..." + "|" + process_state + "|" + "";
                bw.ReportProgress(i, state_struct);

                // Write the league records to the database
                logger.Info("Saving new league to database");
                LeagueDAO.Create_New_League(nls,ddf);

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

        public Loaded_League_Structure LoadExistingLeague(string League_Shortname)
        {
            Loaded_League_Structure r = new Loaded_League_Structure();

            string League_con_string = Environment.SpecialFolder.MyDocuments + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + League_Shortname + Path.DirectorySeparatorChar + League_Shortname + Path.DirectorySeparatorChar + app_Constants.DB_FILE_EXT;

            DBVersionDAO dbverdbo = new DBVersionDAO();
            r.DBVersion = dbverdbo.getLatestDBVersion(League_con_string);

            return r;
        }
    }
}
