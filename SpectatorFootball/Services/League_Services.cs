using System.Collections.Generic;
using System;
using System.ComponentModel;
using System.IO;
using log4net;
using System.Linq;
using SpectatorFootball.League_Info;
using SpectatorFootball.Models;
using System.Data.Entity.Validation;

namespace SpectatorFootball
{
    public class League_Services
    {
        private static ILog logger = LogManager.GetLogger("RollingFile");

        public void CreateNewLeague(Mem_League nl, BackgroundWorker bw)
        {
            logger.Info("CreateNewLeague Started");

            var ts = new Team_Services();

            // Create the league folder
            string DIRPath_League = null;
            string New_League_File = nl.Leagues.Short_Name.ToUpper() + "." + App_Constants.DB_FILE_EXT;
            string League_con_string = Environment.SpecialFolder.MyDocuments + Path.DirectorySeparatorChar + App_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + nl.Leagues.Short_Name + Path.DirectorySeparatorChar + nl.Leagues.Short_Name + Path.DirectorySeparatorChar + App_Constants.DB_FILE_EXT;
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
                DIRPath_League = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + App_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + nl.Leagues.Short_Name.ToUpper();
                Directory.CreateDirectory(DIRPath_League);

                // Create Backup Folder
                logger.Info("Creating league backup folder");
                Directory.CreateDirectory(DIRPath_League + Path.DirectorySeparatorChar + App_Constants.BACKUP_FOLDER);

                // Create the helmet image League Folder
                logger.Info("Creating league helmet folder");
                Directory.CreateDirectory(DIRPath_League + Path.DirectorySeparatorChar + App_Constants.LEAGUE_HELMETS_SUBFOLDER);

                // Create the stadium image League folder
                logger.Info("Creating league image folder");
                Directory.CreateDirectory(DIRPath_League + Path.DirectorySeparatorChar + App_Constants.LEAGUE_STADIUM_SUBFOLDER);

                // Copy and Create the league database file
                logger.Info("Starting league database creation and copy");
                string sf = CommonUtils.getAppPath() + Path.DirectorySeparatorChar + App_Constants.BLANK_DB_FOLDER + Path.DirectorySeparatorChar + App_Constants.BLANK_DB;
                string df = DIRPath_League + Path.DirectorySeparatorChar + New_League_File;
                File.Copy(sf, df);

                // Copy team image files to league folder
                logger.Info("Helmet and stadium files copy starting");
                foreach (var t in nl.Teams)
                {
                    logger.Debug("Copying " + t.Helmet_img_path);
                    File.Copy(t.Helmet_img_path, DIRPath_League + Path.DirectorySeparatorChar + App_Constants.LEAGUE_HELMETS_SUBFOLDER + Path.DirectorySeparatorChar + Path.GetFileName(t.Helmet_img_path));
                    logger.Debug("Copying " + t.Stadium_Img_Path);
                    File.Copy(t.Stadium_Img_Path, DIRPath_League + Path.DirectorySeparatorChar + App_Constants.LEAGUE_STADIUM_SUBFOLDER + Path.DirectorySeparatorChar + Path.GetFileName(t.Stadium_Img_Path));
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
                    List<Player> Roster = ts.Roll_Players((int)t.ID);
                    nl.Players.AddRange(Roster);
                }

                // Update the progress bar
                i = 50;
                process_state = "Creating Schedule 3 of 4";
                state_struct = "Processing..." + "|" + process_state + "|" + "";
                bw.ReportProgress(i, state_struct);

                // Creating schedule
                logger.Info("Creating schedule");
                List<string> sched = create_schedule(nl.Leagues.Short_Name, (int) nl.Leagues.Num_Teams, nl.Divisions.Count, nl.Conferences.Count, (int) nl.Leagues.Number_of_Games, (int) nl.Leagues.Number_of_weeks);

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
                        Year = nl.Leagues.Starting_Year,
                        Week = long.Parse(sWeek),
                        Home_Team_ID = long.Parse(ht),
                        Away_Team_ID = long.Parse(at),
                        League_ID = 1
                    };

                    nl.Games.Add(g);

                }

                // Update the progress bar
                i = 75;
                process_state = "Saving League to Database 4 of 4";
                state_struct = "Processing..." + "|" + process_state + "|" + "";
                bw.ReportProgress(i, state_struct);

                // Write the league records to the database
                logger.Info("Saving new league to database");
                LeagueDAO.Create_New_League(nl,df);

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
    }
}
