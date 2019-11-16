﻿using System.Collections.Generic;
using System;
using System.ComponentModel;
using System.IO;
using log4net;
using System.Linq;

namespace SpectatorFootball
{
    public class League_Services
    {
        private static ILog logger = LogManager.GetLogger("RollingFile");

        public void CreateNewLeague(Leaguemdl nl, BackgroundWorker bw)
        {
            logger.Info("CreateNewLeague Started");

            var ts = new Team_Services();

            // Create the league folder
            string DIRPath = System.IO.Path.Combine(Environment.SpecialFolder.MyDocuments.ToString(), App_Constants.GAME_DOC_FOLDER);
            string DIRPath_League = Environment.SpecialFolder.MyDocuments + Path.DirectorySeparatorChar + App_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + nl.Short_Name;
            string New_League_File = nl.Short_Name + "." + App_Constants.DB_FILE_EXT;
            string League_con_string = Environment.SpecialFolder.MyDocuments + Path.DirectorySeparatorChar + App_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + nl.Short_Name + Path.DirectorySeparatorChar + nl.Short_Name + Path.DirectorySeparatorChar + App_Constants.DB_FILE_EXT;
            var LeagueDAO = new LeagueDAO(nl);
            string process_state = "Processing...";
            string state_struct = null;
            int i = default(int);

            try
            {
                // Update the progress bar
                i = 2;
                process_state = "Creating League Folder Strucuture 1 of 4";
                state_struct = "Processing..." + "|" + process_state + "|" + "";
                bw.ReportProgress(i, state_struct);

                // Create the League Folder
                logger.Info("Creating league folder");
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + App_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + nl.Short_Name);

                // Create Backup Folder
                logger.Info("Creating league backup folder");
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + App_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + nl.Short_Name + Path.DirectorySeparatorChar + App_Constants.BACKUP_FOLDER);

                // Create the helmet image League Folder
                logger.Info("Creating league helmet folder");
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + App_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + nl.Short_Name + Path.DirectorySeparatorChar + App_Constants.LEAGUE_HELMETS_SUBFOLDER);

                // Create the stadium image League folder
                logger.Info("Creating league image folder");
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + App_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + nl.Short_Name + Path.DirectorySeparatorChar + App_Constants.LEAGUE_STADIUM_SUBFOLDER);

                // Copy and Create the league database file
                logger.Info("Starting league database creation and copy");
                File.Copy(CommonUtils.getAppPath() + Path.DirectorySeparatorChar + App_Constants.BLANK_DB_FOLDER + Path.DirectorySeparatorChar + App_Constants.BLANK_DB, DIRPath_League + Path.DirectorySeparatorChar + New_League_File);

                // Copy team image files to league folder
                logger.Info("Helmet and stadium files copy starting");
                foreach (var t in nl.Teams)
                {
                    logger.Debug("Copying " + t.Helmet_img_path);
                    File.Copy(t.Helmet_img_path, DIRPath_League + Path.DirectorySeparatorChar + App_Constants.LEAGUE_HELMETS_SUBFOLDER + Path.DirectorySeparatorChar + Path.GetFileName(t.Helmet_img_path));
                    logger.Debug("Copying " + t.Stadium.Stadium_Img_Path);
                    File.Copy(t.Stadium.Stadium_Img_Path, DIRPath_League + Path.DirectorySeparatorChar + App_Constants.LEAGUE_STADIUM_SUBFOLDER + Path.DirectorySeparatorChar + Path.GetFileName(t.Stadium.Stadium_Img_Path));
                }

                // Update the progress bar
                i = 25;
                process_state = "Create Players 2 of 4";
                state_struct = "Processing..." + "|" + process_state + "|" + "";
                bw.ReportProgress(i, state_struct);

                // Create players for each team
                logger.Info("Creating players for new league.");
                foreach (var t in nl.Teams)
                {
                    logger.Debug("Creating players for team " + t.Nickname);
                    List<PlayerMdl> Roster = ts.Roll_Players("");
                    Roster = Roster.OrderBy(x => x.Pos).ThenByDescending(x => x.Ratings.OverAll).ToList();
                    t.setPlayers(Roster);
                }

                // Update the progress bar
                i = 50;
                process_state = "Creating Schedule 3 of 4";
                state_struct = "Processing..." + "|" + process_state + "|" + "";
                bw.ReportProgress(i, state_struct);

                // Creating schedule
                logger.Info("Creating schedule");
                nl.setSchedule(create_schedule(nl.Short_Name, nl.Num_Teams, nl.Divisions.Count, nl.Conferences.Count, nl.Number_of_Games, nl.Number_of_weeks));

                // Update the progress bar
                i = 75;
                process_state = "Saving League to Database 4 of 4";
                state_struct = "Processing..." + "|" + process_state + "|" + "";
                bw.ReportProgress(i, state_struct);

                // Write the league records to the database
                logger.Info("Saving new league to database");
                LeagueDAO.Create_New_League(League_con_string);

                // Update the progress bar
                i = 100;
                process_state = "";
                state_struct = "Complete" + "|" + process_state + "|" + "League Completed Successfully!";
                bw.ReportProgress(i, state_struct);
            }
            catch (Exception ex)
            {
                state_struct = "Error" + "|" + process_state + "|" + "Failed to Create New League";
                bw.ReportProgress(i, state_struct);

                Directory.Delete(DIRPath_League);

                logger.Error("Create league service failed");
                logger.Error(ex);
            }
        }
        public List<string> create_schedule(string Short_Name, int Num_Teams, int Num_Divisions, int Num_Conferences, int Number_of_Games, int Number_of_weeks)
        {
            var ls = new Schedule(Short_Name, Num_Teams, Num_Teams / Num_Divisions, Num_Conferences, Number_of_Games, Number_of_weeks - Number_of_Games);

            List<string> s = null;

            // allow up to 5 tries if the schedule validation fails.
            for (int i = 0; i <= 6; i++)
            {
                s = ls.Generate_Regular_Schedule();
                logger.Debug("Possible League Schedule Created");
                var val_sched = new Validate_Sched(Short_Name, Num_Teams, Num_Teams / Num_Divisions, Num_Conferences, Number_of_Games, Number_of_weeks - Number_of_Games);
                string r = val_sched.Validate(s);
                if (r != null)
                {
                    logger.Debug("Schedule validated");
                    break;
                }
                if (i >= 5)
                    throw new Exception("More than 6 tries to validate schedule failed.");
            }

            return s;
        }
    }
}
