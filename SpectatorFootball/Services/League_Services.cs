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
using SpectatorFootball.DraftsNS;
using SpectatorFootball.Versioning;
using SpectatorFootball.Enum;
using System.Security.AccessControl;
using System.Security.Principal;
using SpectatorFootball.PlayerNS;
using SpectatorFootball.Awards;

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
            long starting_season_id = 1;
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

                //Create game options file if it does not exist
                string options_filename = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + app_Constants.GAME_OPTIONS_FILE;
                if (!File.Exists(options_filename))
                    Create_Options_File(app_Constants.DEFAULT_GAME_BALL_COLOR + "|" + app_Constants.DEFAULT_GAME_BALL_2_COLOR, true);

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

                // Copy the League Logo file if selected
                if (nls.Season.League_Structure_by_Season[0].League_Logo_Filepath != null &&
                    nls.Season.League_Structure_by_Season[0].League_Logo_Filepath.Trim() != "")
                {
                    logger.Info("Starting league Logo Copy and Rename");
                    string sf = nls.Season.League_Structure_by_Season[0].League_Logo_Filepath;
                    string df = DIRPath_League + Path.DirectorySeparatorChar + Path.GetFileName(sf);
                    File.Copy(sf, df);
                }

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
                logger.Info("Creating Players for Draft");
                process_state = "Create Players for Draft 2 of 5";
                state_struct = "Processing..." + "|" + process_state + "|" + "";
                bw.ReportProgress(i, state_struct);

                //Create players for draft
                int num_players = (int)(((app_Constants.QB_PER_TEAM + app_Constants.RB_PER_TEAM + app_Constants.WR_PER_TEAM + app_Constants.TE_PER_TEAM + app_Constants.OL_PER_TEAM + app_Constants.DL_PER_TEAM + app_Constants.LB_PER_TEAM + app_Constants.DB_PER_TEAM + app_Constants.K_PER_TEAM + app_Constants.P_PER_TEAM) * nls.Season.League_Structure_by_Season[0].Num_Teams ) * app_Constants.DRAFT_MULTIPLIER);

                List<Player> new_player_list = League_Helper.Create_New_Players(num_players, true, starting_season_id);

                nls.Players = new List<Player>();
                foreach (Player p in new_player_list)
                {
                    nls.Players.Add(p);
                }

                i = 50;
                process_state = "Create Draft 3 of 5";
                state_struct = "Processing..." + "|" + process_state + "|" + "";
                bw.ReportProgress(i, state_struct);

                //Create Draft
                int draft_rounds = (app_Constants.QB_PER_TEAM + app_Constants.RB_PER_TEAM + app_Constants.WR_PER_TEAM + app_Constants.TE_PER_TEAM + app_Constants.OL_PER_TEAM + app_Constants.DL_PER_TEAM + app_Constants.LB_PER_TEAM + app_Constants.DB_PER_TEAM + app_Constants.K_PER_TEAM + app_Constants.P_PER_TEAM);
                Draft_Helper DH = new Draft_Helper();
                List<Draft> Draft_List = DH.Create_Draft(nls.Season.Year-1,nls.Season.League_Structure_by_Season[0].Draft_Type_Code, draft_rounds, nls.Franchises, starting_season_id, null);
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
                        Season_ID = starting_season_id,
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
                File.Copy(ddf, backed_up_file);

                logger.Info("CreateNewLeague Ended Successfully");
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

        public void Create_Options_File(string ball_color, bool ThreeDee)
        {
            string options_filename = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + app_Constants.GAME_OPTIONS_FILE;
            FileStream stream = new FileStream(options_filename, FileMode.Create);

            using (StreamWriter sw = new StreamWriter(stream))
            {
                sw.WriteLine("Game_Ball_Color: " + ball_color);
                sw.WriteLine("Ball_Style_3d: " + ThreeDee.ToString());
            }
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

        public League_State getSeasonState(bool Latest_year, long Season_ID, string League_Shortname)
        {
            string League_con_string = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + League_Shortname + Path.DirectorySeparatorChar + League_Shortname + "." + app_Constants.DB_FILE_EXT;
            LeagueDAO ld = new LeagueDAO();
            int[] m = ld.getSeasonTableTotals(Season_ID, League_con_string);
            return League_Helper.DetermineLeagueState(Latest_year, m);
        }

        public List<Standings_Row> getLeageStandings(Loaded_League_Structure lld)
        {
            string DIRPath_League = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + lld.season.League_Structure_by_Season[0].Short_Name.ToUpper();
            string helment_img_path = DIRPath_League + Path.DirectorySeparatorChar + app_Constants.LEAGUE_HELMETS_SUBFOLDER;
            List<Standings_Row> r;
            string League_con_string = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + lld.season.League_Structure_by_Season[0].Short_Name.ToUpper() + Path.DirectorySeparatorChar + lld.season.League_Structure_by_Season[0].Short_Name.ToUpper() + "." + app_Constants.DB_FILE_EXT;
            LeagueDAO ld = new LeagueDAO();

            r = ld.getStandings(lld.season.ID, League_con_string);

            List<Standing_Streak> sstreak = ld.getStandingsStreak(lld.season.ID, League_con_string);

            //Now we need to set the streak in the standings list for each team, also set the
            //path to the helment image.  
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

            foreach (Standings_Row sr in r)
                sr.HelmetImage = lld.getHelmetImg(sr.Helmet_img);  

            return r;
        }

        public List<Season> getAllSeasons(string League_Shortname)
        {
            string DIRPath_League = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + League_Shortname.ToUpper();
            string helment_img_path = DIRPath_League + Path.DirectorySeparatorChar + app_Constants.LEAGUE_HELMETS_SUBFOLDER;
            List<Season> r;
            string League_con_string = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + League_Shortname + Path.DirectorySeparatorChar + League_Shortname + "." + app_Constants.DB_FILE_EXT;
            LeagueDAO ld = new LeagueDAO();

            r = ld.getAllSeasons(League_con_string);

            return r;
        }

        public League_Stats getSeasonStats(Loaded_League_Structure lls,  League_Stats lStats, Stat_Type st, string sort_field, bool sort_desc)
        {
            string DIRPath_League = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + lls.season.League_Structure_by_Season[0].Short_Name.ToUpper();
            string helment_img_path = DIRPath_League + Path.DirectorySeparatorChar + app_Constants.LEAGUE_HELMETS_SUBFOLDER;
            League_Stats r = lStats;
            string League_con_string = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + lls.season.League_Structure_by_Season[0].Short_Name.ToUpper() + Path.DirectorySeparatorChar + lls.season.League_Structure_by_Season[0].Short_Name.ToUpper() + "." + app_Constants.DB_FILE_EXT;
            long season_id = lls.season.ID;

            switch (st)
            {
                case Stat_Type.PASSING:
                    if (lStats.Passing_Stats == null)
                    {
                        TeamDAO td = new TeamDAO();
                        List<Teams_by_Season> team_list = td.getAllTeamsSeason(season_id, League_con_string);
                        LeagueDAO ld = new LeagueDAO();
                        lStats.Passing_Stats = ld.getLeagueSeasonPassingStats(season_id, League_con_string);
                        foreach (Passing_Accum_Stats_by_year s in lStats.Passing_Stats)
                        {
                            Teams_by_Season t = team_list.Where(x => x.Franchise_ID == s.f_id).First();
                            s.HelmetImage = lls.getHelmetImg(t.Helmet_Image_File);
                            s.City_Abbr = t.City_Abr;
                            s.Comp_Percent = Player_Helper.FormatCompPercent(s.Completes, s.Ateempts);
                            s.QBR = Player_Helper.CalculateQBR(s.Completes, s.Ateempts, s.Yards, s.TDs, s.Ints);
                        }
                    }
                    switch (sort_field)
                    {
                        case "Player":
                            if (sort_desc == false)
                                lStats.Passing_Stats = lStats.Passing_Stats.OrderBy(x =>
                                x.p.Last_Name).ThenBy(x => x.p.First_Name).ToList();
                            else
                                lStats.Passing_Stats = lStats.Passing_Stats.OrderByDescending(x =>
                                x.p.Last_Name).ThenByDescending(x => x.p.First_Name).ToList();
                            break;
                        case "Comp %":
                        case "QBR":
                            if (sort_desc == false)
                                lStats.Passing_Stats = lStats.Passing_Stats.OrderBy(x =>
                                sort_field == "Name" ? x.p.Last_Name + x.p.First_Name :
                                sort_field == "Comp_Percent" ? x.Comp_Percent :
                                sort_field == "QBR" ? x.QBR : x.QBR).ToList();
                            else
                                lStats.Passing_Stats = lStats.Passing_Stats.OrderByDescending(x =>
                                sort_field == "Name" ? x.p.Last_Name + x.p.First_Name :
                                sort_field == "Comp_Percent" ? x.Comp_Percent :
                                sort_field == "QBR" ? x.QBR : x.QBR).ToList();
                            break;
                        case "Completes":
                        case "Ateempts":
                        case "Yards":
                        case "TDs":
                        case "Ints":
                            if (sort_desc == false)
                                lStats.Passing_Stats = lStats.Passing_Stats.OrderBy(x =>
                                    sort_field == "Completes" ? x.Completes :
                                    sort_field == "Ateempts" ? x.Ateempts :
                                    sort_field == "Yards" ? x.Yards :
                                    sort_field == "TDs" ? x.TDs :
                                    sort_field == "Ints" ? x.Ints :
                                    sort_field == "Fumbles_Lost" ? x.Fumbles_Lost : x.Yards).ToList();
                            else
                                lStats.Passing_Stats = lStats.Passing_Stats.OrderByDescending(x =>
                                    sort_field == "Completes" ? x.Completes :
                                    sort_field == "Ateempts" ? x.Ateempts :
                                    sort_field == "Yards" ? x.Yards :
                                    sort_field == "TDs" ? x.TDs :
                                    sort_field == "Ints" ? x.Ints :
                                    sort_field == "Fumbles_Lost" ? x.Fumbles_Lost : x.Yards).ToList();
                            break;
                    }
                  break;
                case Stat_Type.RUSHING:
                    if (lStats.Rushing_Stats == null)
                    {
                        TeamDAO td = new TeamDAO();
                        List<Teams_by_Season> team_list = td.getAllTeamsSeason(season_id, League_con_string);
                        LeagueDAO ld = new LeagueDAO();
                        lStats.Rushing_Stats = ld.getLeagueSeasonRushingStats(season_id, League_con_string);
                        foreach (Rushing_Accum_Stats_by_year s in lStats.Rushing_Stats)
                        {
                            Teams_by_Season t = team_list.Where(x => x.Franchise_ID == s.f_id).First();
                            s.HelmetImage = lls.getHelmetImg(t.Helmet_Image_File);
                            s.City_Abbr = t.City_Abr;
                            s.Yards_Per_Carry = Player_Helper.CalcYardsPerCarry_or_Catch(s.Rushes, s.Yards);
                        }
                    }
                    switch (sort_field)
                    {
                        case "Player":
                            if (sort_desc == false)
                                lStats.Rushing_Stats = lStats.Rushing_Stats.OrderBy(x =>
                                x.p.Last_Name).ThenBy(x => x.p.First_Name).ToList();
                            else
                                lStats.Rushing_Stats = lStats.Rushing_Stats.OrderByDescending(x =>
                                x.p.Last_Name).ThenByDescending(x => x.p.First_Name).ToList();
                            break;
                        case "Yards/Carry":
                            if (sort_desc == false)
                                lStats.Rushing_Stats = lStats.Rushing_Stats.OrderBy(x =>
                                sort_field == "Name" ? x.p.Last_Name + x.p.First_Name :
                                sort_field == "Yards_Per_Carry" ? x.Yards_Per_Carry : x.Yards_Per_Carry).ToList();
                            else
                                lStats.Rushing_Stats = lStats.Rushing_Stats.OrderByDescending(x =>
                                sort_field == "Name" ? x.p.Last_Name + x.p.First_Name :
                                sort_field == "Yards_Per_Carry" ? x.Yards_Per_Carry : x.Yards_Per_Carry).ToList();
                            break;
                        case "Rushes":
                        case "Yards":
                        case "TDs":
                        case "Fumbles Lost":
                            if (sort_desc == false)
                                lStats.Rushing_Stats = lStats.Rushing_Stats.OrderBy(x =>
                                    sort_field == "Rushes" ? x.Rushes :
                                    sort_field == "Yards" ? x.Yards :
                                    sort_field == "TDs" ? x.TDs :
                                    sort_field == "Fumbles_Lost" ? x.Fumbles_Lost : x.Yards).ToList();
                            else
                                lStats.Rushing_Stats = lStats.Rushing_Stats.OrderByDescending(x =>
                                    sort_field == "Rushes" ? x.Rushes :
                                    sort_field == "Yards" ? x.Yards :
                                    sort_field == "TDs" ? x.TDs :
                                    sort_field == "Fumbles_Lost" ? x.Fumbles_Lost : x.Yards).ToList();
                            break;
                    }
                    break;
                case Stat_Type.RECEIVING:
                    if (lStats.Receiving_Stats == null)
                    {
                        TeamDAO td = new TeamDAO();
                        List<Teams_by_Season> team_list = td.getAllTeamsSeason(season_id, League_con_string);
                        LeagueDAO ld = new LeagueDAO();
                        lStats.Receiving_Stats = ld.getLeagueSeasonReceivingStats(season_id, League_con_string);
                        foreach (Receiving_Accum_Stats_by_year s in lStats.Receiving_Stats)
                        {
                            Teams_by_Season t = team_list.Where(x => x.Franchise_ID == s.f_id).First();
                            s.HelmetImage = lls.getHelmetImg(t.Helmet_Image_File);
                            s.City_Abbr = t.City_Abr;
                            s.Yards_Per_Catch = Player_Helper.CalcYardsPerCarry_or_Catch(s.Catches, s.Yards);
                        }
                    }
                    switch (sort_field)
                    {
                        case "Player":
                            if (sort_desc == false)
                                lStats.Receiving_Stats = lStats.Receiving_Stats.OrderBy(x =>
                                x.p.Last_Name).ThenBy(x => x.p.First_Name).ToList();
                            else
                                lStats.Receiving_Stats = lStats.Receiving_Stats.OrderByDescending(x =>
                                x.p.Last_Name).ThenByDescending(x => x.p.First_Name).ToList();
                            break;
                          case "Yards/Catch":
                            if (sort_desc == false)
                                lStats.Receiving_Stats = lStats.Receiving_Stats.OrderBy(x =>
                                sort_field == "Name" ? x.p.Last_Name + x.p.First_Name :
                                sort_field == "Yards_Per_Catch" ? x.Yards_Per_Catch : x.Yards_Per_Catch).ToList();
                            else
                                lStats.Receiving_Stats = lStats.Receiving_Stats.OrderByDescending(x =>
                                sort_field == "Name" ? x.p.Last_Name + x.p.First_Name :
                                sort_field == "Yards_Per_Catch" ? x.Yards_Per_Catch : x.Yards_Per_Catch).ToList();
                            break;
                        case "Catches":
                        case "Yards":
                        case "TDs":       
                        case "Drops":
                        case "Fumbles Lost":
                            if (sort_desc == false)
                                lStats.Receiving_Stats = lStats.Receiving_Stats.OrderBy(x =>
                                    sort_field == "Catches" ? x.Catches :
                                    sort_field == "Yards" ? x.Yards :
                                    sort_field == "TDs" ? x.TDs :
                                    sort_field == "Drops" ? x.Drops :
                                    sort_field == "Fumbles_Lost" ? x.Fumbles_Lost : x.Yards).ToList();
                            else
                                lStats.Receiving_Stats = lStats.Receiving_Stats.OrderByDescending(x =>
                                    sort_field == "Catches" ? x.Catches :
                                    sort_field == "Yards" ? x.Yards :
                                    sort_field == "TDs" ? x.TDs :
                                    sort_field == "Drops" ? x.Drops :
                                    sort_field == "Fumbles_Lost" ? x.Fumbles_Lost : x.Yards).ToList();
                            break;
                    }
                    break;

                case Stat_Type.BLOCKING:
                    if (lStats.Blocking_Stats == null)
                    {
                        TeamDAO td = new TeamDAO();
                        List<Teams_by_Season> team_list = td.getAllTeamsSeason(season_id, League_con_string);
                        LeagueDAO ld = new LeagueDAO();
                        lStats.Blocking_Stats = ld.getLeagueSeasonBlockingStats(season_id, League_con_string);
                        foreach (Blocking_Accum_Stats_by_year s in lStats.Blocking_Stats)
                        {
                            Teams_by_Season t = team_list.Where(x => x.Franchise_ID == s.f_id).First();
                            s.HelmetImage = lls.getHelmetImg(t.Helmet_Image_File);
                            s.City_Abbr = t.City_Abr;
                        }
                    }
                    switch (sort_field)
                    {
                        case "Player":
                            if (sort_desc == false)
                                lStats.Blocking_Stats = lStats.Blocking_Stats.OrderBy(x =>
                                x.p.Last_Name).ThenBy(x => x.p.First_Name).ToList();
                            else
                                lStats.Blocking_Stats = lStats.Blocking_Stats.OrderByDescending(x =>
                                x.p.Last_Name).ThenByDescending(x => x.p.First_Name).ToList();
                            break;
                        case "Plays":
                        case "Pancakes":
                        case "Sacks Allowed":
                        case "Pressures Allowed":
                            if (sort_desc == false)
                                lStats.Blocking_Stats = lStats.Blocking_Stats.OrderBy(x =>
                                    sort_field == "Plays" ? x.Plays :
                                    sort_field == "Pancakes" ? x.Pancakes :
                                    sort_field == "Sacks_Allowed" ? x.Sacks_Allowed :
                                    sort_field == "Pressures_Allowed" ? x.Pressures_Allowed : x.Pressures_Allowed).ToList();
                            else
                                lStats.Blocking_Stats = lStats.Blocking_Stats.OrderByDescending(x =>
                                    sort_field == "Plays" ? x.Plays :
                                    sort_field == "Pancakes" ? x.Pancakes :
                                    sort_field == "Sacks_Allowed" ? x.Sacks_Allowed :
                                    sort_field == "Pressures_Allowed" ? x.Pressures_Allowed : x.Pressures_Allowed).ToList();
                            break;
                    }
                    break;

                case Stat_Type.DEFENSE:
                    if (lStats.Defense_Stats == null)
                    {
                        TeamDAO td = new TeamDAO();
                        List<Teams_by_Season> team_list = td.getAllTeamsSeason(season_id, League_con_string);
                        LeagueDAO ld = new LeagueDAO();
                        lStats.Defense_Stats = ld.getLeagueSeasonDefenseStats(season_id, League_con_string);
                        foreach (Defense_Accum_Stats_by_year s in lStats.Defense_Stats)
                        {
                            Teams_by_Season t = team_list.Where(x => x.Franchise_ID == s.f_id).First();
                            s.HelmetImage = lls.getHelmetImg(t.Helmet_Image_File);
                            s.City_Abbr = t.City_Abr;
                        }
                    }
                    switch (sort_field)
                    {
                        case "Player":
                            if (sort_desc == false)
                                lStats.Defense_Stats = lStats.Defense_Stats.OrderBy(x =>
                                x.p.Last_Name).ThenBy(x => x.p.First_Name).ToList();
                            else
                                lStats.Defense_Stats = lStats.Defense_Stats.OrderByDescending(x =>
                                x.p.Last_Name).ThenByDescending(x => x.p.First_Name).ToList();
                            break;
                        case "Plays":
                        case "Tackles":
                        case "Sacks":
                        case "Pressures":
                        case "Run for Loss":
                        case "Forced Fumble":
                            if (sort_desc == false)
                                lStats.Defense_Stats = lStats.Defense_Stats.OrderBy(x =>
                                    sort_field == "Plays" ? x.Plays :
                                    sort_field == "Tackles" ? x.Tackles :
                                    sort_field == "Sacks" ? x.Sacks :
                                    sort_field == "Pressures" ? x.Pressures :
                                    sort_field == "Run_for_Loss" ? x.Run_for_Loss : 
                                    sort_field == "Forced_Fumble" ? x.Forced_Fumble : x.Forced_Fumble).ToList();
                            else
                                lStats.Defense_Stats = lStats.Defense_Stats.OrderByDescending(x =>
                                    sort_field == "Plays" ? x.Plays :
                                    sort_field == "Tackles" ? x.Tackles :
                                    sort_field == "Sacks" ? x.Sacks :
                                    sort_field == "Pressures" ? x.Pressures :
                                    sort_field == "Run_for_Loss" ? x.Run_for_Loss : 
                                    sort_field == "Forced_Fumble" ? x.Forced_Fumble : x.Forced_Fumble).ToList();
                            break;
                    }
                    break;
                case Stat_Type.PASS_DEFENSE:
                    if (lStats.Pass_Defense_Stats == null)
                    {
                        TeamDAO td = new TeamDAO();
                        List<Teams_by_Season> team_list = td.getAllTeamsSeason(season_id, League_con_string);
                        LeagueDAO ld = new LeagueDAO();
                        lStats.Pass_Defense_Stats = ld.getLeagueSeasonPassDefenseStats(season_id, League_con_string);
                        foreach (Pass_Defense_Accum_Stats_by_year s in lStats.Pass_Defense_Stats)
                        {
                            Teams_by_Season t = team_list.Where(x => x.Franchise_ID == s.f_id).First();
                            s.HelmetImage = lls.getHelmetImg(t.Helmet_Image_File);
                            s.City_Abbr = t.City_Abr;
                        }
                    }
                    switch (sort_field)
                    {
                        case "Player":
                            if (sort_desc == false)
                                lStats.Pass_Defense_Stats = lStats.Pass_Defense_Stats.OrderBy(x =>
                                x.p.Last_Name).ThenBy(x => x.p.First_Name).ToList();
                            else
                                lStats.Pass_Defense_Stats = lStats.Pass_Defense_Stats.OrderByDescending(x =>
                                x.p.Last_Name).ThenByDescending(x => x.p.First_Name).ToList();
                            break;
                        case "Pass Defenses":
                        case "Ints":
                        case "TDs Surrendered":
                        case "Forced Fumble":
                            if (sort_desc == false)
                                lStats.Pass_Defense_Stats = lStats.Pass_Defense_Stats.OrderBy(x =>
                                    sort_field == "Pass_Defenses" ? x.Pass_Defenses :
                                    sort_field == "Ints" ? x.Ints :
                                    sort_field == "TDs_Surrendered" ? x.TDs_Surrendered :
                                    sort_field == "Forced_Fumble" ? x.Forced_Fumble : x.Forced_Fumble).ToList();
                            else
                                lStats.Pass_Defense_Stats = lStats.Pass_Defense_Stats.OrderByDescending(x =>
                                    sort_field == "Pass_Defenses" ? x.Pass_Defenses :
                                    sort_field == "Ints" ? x.Ints :
                                    sort_field == "TDs_Surrendered" ? x.TDs_Surrendered :
                                    sort_field == "Forced_Fumble" ? x.Forced_Fumble : x.Forced_Fumble).ToList();
                            break;
                    }
                    break;

                case Stat_Type.KICKING:
                    if (lStats.Kicking_Stats == null)
                    {
                        TeamDAO td = new TeamDAO();
                        List<Teams_by_Season> team_list = td.getAllTeamsSeason(season_id, League_con_string);
                        LeagueDAO ld = new LeagueDAO();
                        lStats.Kicking_Stats = ld.getLeagueSeasonKickerStats (season_id, League_con_string);
                        foreach (Kicking_Accum_Stats_by_year s in lStats.Kicking_Stats)
                        {
                            Teams_by_Season t = team_list.Where(x => x.Franchise_ID == s.f_id).First();
                            s.HelmetImage = lls.getHelmetImg(t.Helmet_Image_File);
                            s.City_Abbr = t.City_Abr;
                            s.FG_Percent = Player_Helper.CalcYardsPerCarry_or_Catch(s.FG_Made, s.FG_ATT);
                        }
                    }
                    switch (sort_field)
                    {
                        case "Player":
                            if (sort_desc == false)
                                lStats.Kicking_Stats = lStats.Kicking_Stats.OrderBy(x =>
                                x.p.Last_Name).ThenBy(x => x.p.First_Name).ToList();
                            else
                                lStats.Kicking_Stats = lStats.Kicking_Stats.OrderByDescending(x =>
                                x.p.Last_Name).ThenByDescending(x => x.p.First_Name).ToList();
                            break;
                        case "FG %":
                            if (sort_desc == false)
                                lStats.Kicking_Stats = lStats.Kicking_Stats.OrderBy(x =>
                                sort_field == "Name" ? x.p.Last_Name + x.p.First_Name :
                                sort_field == "FG_Percent" ? x.FG_Percent :
                                sort_field == "City_Abbr" ? x.City_Abbr : x.City_Abbr).ToList();
                            else
                                lStats.Kicking_Stats = lStats.Kicking_Stats.OrderByDescending(x =>
                                sort_field == "Name" ? x.p.Last_Name + x.p.First_Name :
                                sort_field == "FG_Percent" ? x.FG_Percent :
                                sort_field == "City_Abbr" ? x.City_Abbr : x.City_Abbr).ToList();
                            break;
                        case "FG ATT":
                        case "FG Made":
                        case "FG Long":
                        case "XP ATT":
                        case "XP Made":
                            if (sort_desc == false)
                                lStats.Kicking_Stats = lStats.Kicking_Stats.OrderBy(x =>
                                    sort_field == "FG_ATT" ? x.FG_ATT :
                                    sort_field == "FG_Made" ? x.FG_Made :
                                    sort_field == "FG_Long" ? x.FG_Long :
                                    sort_field == "XP_ATT" ? x.XP_ATT :
                                    sort_field == "XP_Made" ? x.XP_Made : x.FG_Made).ToList();
                            else
                                lStats.Kicking_Stats = lStats.Kicking_Stats.OrderByDescending(x =>
                                    sort_field == "FG_ATT" ? x.FG_ATT :
                                    sort_field == "FG_Made" ? x.FG_Made :
                                    sort_field == "FG_Long" ? x.FG_Long :
                                    sort_field == "XP_ATT" ? x.XP_ATT :
                                    sort_field == "XP_Made" ? x.XP_Made : x.FG_Made).ToList();
                            break;
                    }
                    break;


                case Stat_Type.PUNTING:
                    if (lStats.Punting_Stats == null)
                    {
                        TeamDAO td = new TeamDAO();
                        List<Teams_by_Season> team_list = td.getAllTeamsSeason(season_id, League_con_string);
                        LeagueDAO ld = new LeagueDAO();
                        lStats.Punting_Stats = ld.getLeagueSeasonPunterStats(season_id, League_con_string);
                        foreach (Punting_Accum_Stats_by_year s in lStats.Punting_Stats)
                        {
                            Teams_by_Season t = team_list.Where(x => x.Franchise_ID == s.f_id).First();
                            s.HelmetImage = lls.getHelmetImg(t.Helmet_Image_File);
                            s.City_Abbr = t.City_Abr;
                            s.Punt_AVG = Player_Helper.CalcYardsPerCarry_or_Catch(s.Punts, s.Yards);
                        }
                    }
                    switch (sort_field)
                    {
                        case "Player":
                            if (sort_desc == false)
                                lStats.Punting_Stats = lStats.Punting_Stats.OrderBy(x =>
                                x.p.Last_Name).ThenBy(x => x.p.First_Name).ToList();
                            else
                                lStats.Punting_Stats = lStats.Punting_Stats.OrderByDescending(x =>
                                x.p.Last_Name).ThenByDescending(x => x.p.First_Name).ToList();
                            break;
                        case "Punt AVG":
                            if (sort_desc == false)
                                lStats.Punting_Stats = lStats.Punting_Stats.OrderBy(x =>
                                sort_field == "Name" ? x.p.Last_Name + x.p.First_Name :
                                sort_field == "Punt_AVG" ? x.Punt_AVG :
                                sort_field == "City_Abbr" ? x.City_Abbr : x.City_Abbr).ToList();
                            else
                                lStats.Punting_Stats = lStats.Punting_Stats.OrderByDescending(x =>
                                sort_field == "Name" ? x.p.Last_Name + x.p.First_Name :
                                sort_field == "Punt_AVG" ? x.Punt_AVG :
                                sort_field == "City_Abbr" ? x.City_Abbr : x.City_Abbr).ToList();
                            break;
                        case "Punts":
                        case "Yards":
                        case "Coffin Corners":
                            if (sort_desc == false)
                                lStats.Punting_Stats = lStats.Punting_Stats.OrderBy(x =>
                                    sort_field == "Punts" ? x.Punts :
                                    sort_field == "Yards" ? x.Yards :
                                    sort_field == "Coffin_Corners" ? x.Coffin_Corners : x.Yards).ToList();
                            else
                                lStats.Punting_Stats = lStats.Punting_Stats.OrderByDescending(x =>
                                    sort_field == "Punts" ? x.Punts :
                                    sort_field == "Yards" ? x.Yards :
                                    sort_field == "Coffin_Corners" ? x.Coffin_Corners : x.Yards).ToList();
                            break;
                    }
                    break;

                case Stat_Type.KICK_RETURNS:
                    if (lStats.KickRet_Stats == null)
                    {
                        TeamDAO td = new TeamDAO();
                        List<Teams_by_Season> team_list = td.getAllTeamsSeason(season_id, League_con_string);
                        LeagueDAO ld = new LeagueDAO();
                        lStats.KickRet_Stats = ld.getLeagueSeasonKickReturnStats(season_id, League_con_string);
                        foreach (KickReturn_Accum_Stats_by_year s in lStats.KickRet_Stats)
                        {
                            Teams_by_Season t = team_list.Where(x => x.Franchise_ID == s.f_id).First();
                            s.HelmetImage = lls.getHelmetImg(t.Helmet_Image_File);
                            s.City_Abbr = t.City_Abr;
                            s.Yards_avg = Player_Helper.CalcYardsPerCarry_or_Catch(s.Returns, s.Yards);
                        }
                    }
                    switch (sort_field)
                    {
                        case "Player":
                            if (sort_desc == false)
                                lStats.KickRet_Stats = lStats.KickRet_Stats.OrderBy(x =>
                                x.p.Last_Name).ThenBy(x => x.p.First_Name).ToList();
                            else
                                lStats.KickRet_Stats = lStats.KickRet_Stats.OrderByDescending(x =>
                                x.p.Last_Name).ThenByDescending(x => x.p.First_Name).ToList();
                            break;
                       case "Yards/Return":
                            if (sort_desc == false)
                                lStats.KickRet_Stats = lStats.KickRet_Stats.OrderBy(x =>
                                sort_field == "Name" ? x.p.Last_Name + x.p.First_Name :
                                sort_field == "Yards_avg" ? x.Yards_avg :
                                sort_field == "City_Abbr" ? x.City_Abbr : x.City_Abbr).ToList();
                            else
                                lStats.KickRet_Stats = lStats.KickRet_Stats.OrderByDescending(x =>
                                sort_field == "Name" ? x.p.Last_Name + x.p.First_Name :
                                sort_field == "Yards_avg" ? x.Yards_avg :
                                sort_field == "City_Abbr" ? x.City_Abbr : x.City_Abbr).ToList();
                            break;
                        case "Returns":
                        case "Yards":
                        case "Yards Long":
                        case "TDs":
                        case "Fumbles":
                            if (sort_desc == false)
                                lStats.KickRet_Stats = lStats.KickRet_Stats.OrderBy(x =>
                                    sort_field == "Returns" ? x.Returns :
                                    sort_field == "Yards" ? x.Yards :
                                    sort_field == "Yards_Long" ? x.Yards_Long :
                                    sort_field == "TDs" ? x.TDs :
                                    sort_field == "Fumbles" ? x.Fumbles : x.Yards).ToList();
                            else
                                lStats.KickRet_Stats = lStats.KickRet_Stats.OrderByDescending(x =>
                                    sort_field == "Returns" ? x.Returns :
                                    sort_field == "Yards" ? x.Yards :
                                    sort_field == "Yards_Long" ? x.Yards_Long :
                                    sort_field == "TDs" ? x.TDs :
                                    sort_field == "Fumbles" ? x.Fumbles : x.Yards).ToList();
                            break;
                    }
                    break;

                case Stat_Type.PUNT_RETURNS:
                    if (lStats.PuntRet_Stats == null)
                    {
                        TeamDAO td = new TeamDAO();
                        List<Teams_by_Season> team_list = td.getAllTeamsSeason(season_id, League_con_string);
                        LeagueDAO ld = new LeagueDAO();
                        lStats.PuntRet_Stats = ld.getLeagueSeasonPUntReturnStats(season_id, League_con_string);
                        foreach (PuntReturns_Accum_Stats_by_year s in lStats.PuntRet_Stats)
                        {
                            Teams_by_Season t = team_list.Where(x => x.Franchise_ID == s.f_id).First();
                            s.HelmetImage = lls.getHelmetImg(t.Helmet_Image_File);
                            s.City_Abbr = t.City_Abr;
                            s.Yards_avg = Player_Helper.CalcYardsPerCarry_or_Catch(s.Returns, s.Yards);
                        }
                    }
                    switch (sort_field)
                    {
                        case "Player":
                            if (sort_desc == false)
                                lStats.PuntRet_Stats = lStats.PuntRet_Stats.OrderBy(x =>
                                x.p.Last_Name).ThenBy(x => x.p.First_Name).ToList();
                            else
                                lStats.PuntRet_Stats = lStats.PuntRet_Stats.OrderByDescending(x =>
                                x.p.Last_Name).ThenByDescending(x => x.p.First_Name).ToList();
                            break;
                        case "Yards/Return":
                            if (sort_desc == false)
                                lStats.PuntRet_Stats = lStats.PuntRet_Stats.OrderBy(x =>
                                sort_field == "Name" ? x.p.Last_Name + x.p.First_Name :
                                sort_field == "Yards_avg" ? x.Yards_avg :
                                sort_field == "City_Abbr" ? x.City_Abbr : x.City_Abbr).ToList();
                            else
                                lStats.PuntRet_Stats = lStats.PuntRet_Stats.OrderByDescending(x =>
                                sort_field == "Name" ? x.p.Last_Name + x.p.First_Name :
                                sort_field == "Yards_avg" ? x.Yards_avg :
                                sort_field == "City_Abbr" ? x.City_Abbr : x.City_Abbr).ToList();
                            break;
                        case "Returns":
                        case "Yards":
                        case "Yards Long":
                        case "TDs":
                        case "Fumbles":
                            if (sort_desc == false)
                                lStats.PuntRet_Stats = lStats.PuntRet_Stats.OrderBy(x =>
                                    sort_field == "Returns" ? x.Returns :
                                    sort_field == "Yards" ? x.Yards :
                                    sort_field == "Yards_Long" ? x.Yards_Long :
                                    sort_field == "TDs" ? x.TDs :
                                    sort_field == "Fumbles" ? x.Fumbles : x.Yards).ToList();
                            else
                                lStats.PuntRet_Stats = lStats.PuntRet_Stats.OrderByDescending(x =>
                                    sort_field == "Returns" ? x.Returns :
                                    sort_field == "Yards" ? x.Yards :
                                    sort_field == "Yards_Long" ? x.Yards_Long :
                                    sort_field == "TDs" ? x.TDs :
                                    sort_field == "Fumbles" ? x.Fumbles : x.Yards).ToList();
                            break;
                    }
                    break;
            }

            return r;
        }

        public List<Team_Stat_Rec> getLeagueStats(Loaded_League_Structure lld, List<Team_Stat_Rec> Final_stats, string sortfield, bool isdecending)
        {
            string DIRPath_League = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + lld.season.League_Structure_by_Season[0].Short_Name.ToUpper();
            string helment_img_path = DIRPath_League + Path.DirectorySeparatorChar + app_Constants.LEAGUE_HELMETS_SUBFOLDER;
            string League_con_string = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + lld.season.League_Structure_by_Season[0].Short_Name.ToUpper() + Path.DirectorySeparatorChar + lld.season.League_Structure_by_Season[0].Short_Name.ToUpper() + "." + app_Constants.DB_FILE_EXT;
            LeagueDAO ld = new LeagueDAO();

            if (Final_stats == null)
            {
                List<Team_Stat_Rec> TeamBlankStats = new List<Team_Stat_Rec>();

                //First set a blank team stat record for each team, so that each team has a record
                //even if they haven't played a game.
                foreach (Teams_by_Season t in lld.season.Teams_by_Season)
                    TeamBlankStats.Add(new Team_Stat_Rec() { f_id = t.Franchise_ID });

                //Next, get the home and away team stats
                List<Team_Stat_Rec>[] haStats = ld.getLeagueStatsHomeAway(lld.season.ID, League_con_string);

                //now merge that data from the blank stats, home and away stat recs
                var combined_Stats =
                    TeamBlankStats.Concat(haStats[0]).Concat(haStats[1]);

                //Now group the stats by franchise id to ge tthe final list
                Final_stats = combined_Stats.GroupBy(x => x.f_id)
                    .Select(x => new Team_Stat_Rec
                    {
                        f_id = x.Key,
                        Passing_Yards_For = (long)x.Sum(s => s.Passing_Yards_For),
                        Passing_Yards_Against = (long)x.Sum(s => s.Passing_Yards_Against),
                        Rushing_Yards_For = (long)x.Sum(s => s.Rushing_Yards_For),
                        Rushing_Yards_Against = (long)x.Sum(s => s.Rushing_Yards_Against),
                        Turnovers_Comm = (long)x.Sum(s => s.Turnovers_Comm),
                        Turnovers_Recv = (long)x.Sum(s => s.Turnovers_Recv),
                        Third_Down_Conversions = (long)x.Sum(s => s.Third_Down_Conversions),
                        Third_Down_Conversions_Att = (long)x.Sum(s => s.Third_Down_Conversions_Att),
                        Fourth_Down_Conversions = (long)x.Sum(s => s.Fourth_Down_Conversions),
                        Fourth_Down_Conversions_Att = (long)x.Sum(s => s.Fourth_Down_Conversions_Att)
                    }).ToList();

                //Now do all the calculations and set the helmet for each team record
                List<Standings_Row> Standings = lld.Standings;
                foreach (Team_Stat_Rec s in Final_stats)
                {
                    Teams_by_Season t = lld.season.Teams_by_Season.Where(x => x.Franchise_ID == s.f_id).First();
                    s.Team_Name = t.City + " " + t.Nickname;
                    s.HelmetImage = lld.getHelmetImg(t.Helmet_Image_File);
                    s.Wins = Standings.Where(x => x.Team_ID == t.ID).Select(x => x.wins).First();
                    s.Loses = Standings.Where(x => x.Team_ID == t.ID).Select(x => x.loses).First();
                    s.Ties = Standings.Where(x => x.Team_ID == t.ID).Select(x => x.ties).First();
                    s.PF = Standings.Where(x => x.Team_ID == t.ID).Select(x => x.pointsfor).First();
                    s.PA = Standings.Where(x => x.Team_ID == t.ID).Select(x => x.pointagainst).First();
                    s.PPG_For = Player_Helper.CalcYardsPerCarry_or_Catch(s.Wins + s.Loses + s.Ties, s.PF);
                    s.PPG_Against = Player_Helper.CalcYardsPerCarry_or_Catch(s.Wins + s.Loses + s.Ties, s.PA);
                    s.Total_Yards_For = s.Passing_Yards_For + s.Rushing_Yards_For;
                    s.Total_Yards_Against = s.Passing_Yards_Against + s.Rushing_Yards_Against;
                    s.Third_Down_String = Player_Helper.FormatCompPercent(s.Third_Down_Conversions, s.Third_Down_Conversions_Att);
                    s.Third_Down_Display_String = s.Third_Down_Conversions + "-" + s.Third_Down_Conversions_Att + " (" + String.Format("{0:0.0}", double.Parse(s.Third_Down_String)) + "%)";
                    s.Fourth_Down_String = Player_Helper.FormatCompPercent(s.Fourth_Down_Conversions, s.Fourth_Down_Conversions_Att);
                    s.Fourth_Down_Display_String = s.Fourth_Down_Conversions + "-" + s.Fourth_Down_Conversions_Att + " (" + String.Format("{0:0.0}", double.Parse(s.Fourth_Down_String)) + "%)";
                    s.Power_Ranking = Team_Helper.calcTeam_PowerRating(s.Wins, s.Loses, s.PF, s.PA);
                }
            }

            //Finally sort the team standings
            switch(sortfield)
            {
                case "Team":
                    if (isdecending)
                        Final_stats = Final_stats.OrderByDescending(x => x.Team_Name).ToList();
                    else
                        Final_stats = Final_stats.OrderBy(x => x.Team_Name).ToList();
                    break;
                case "Wins":
                    if (isdecending)
                        Final_stats = Final_stats.OrderByDescending(x => x.Wins).ToList();
                    else
                        Final_stats = Final_stats.OrderBy(x => x.Wins).ToList();
                    break;
                case "Loses":
                    if (isdecending)
                        Final_stats = Final_stats.OrderByDescending(x => x.Loses).ToList();
                    else
                        Final_stats = Final_stats.OrderBy(x => x.Loses).ToList();
                    break;
                case "Ties":
                    if (isdecending)
                        Final_stats = Final_stats.OrderByDescending(x => x.Ties).ToList();
                    else
                        Final_stats = Final_stats.OrderBy(x => x.Ties).ToList();
                    break;
                case "PF":
                    if (isdecending)
                        Final_stats = Final_stats.OrderByDescending(x => x.PF).ToList();
                    else
                        Final_stats = Final_stats.OrderBy(x => x.PF).ToList();
                    break;
                case "PA":
                    if (isdecending)
                        Final_stats = Final_stats.OrderByDescending(x => x.PA).ToList();
                    else
                        Final_stats = Final_stats.OrderBy(x => x.PA).ToList();
                    break;
                case "PPG F":
                    if (isdecending)
                        Final_stats = Final_stats.OrderByDescending(x => x.PPG_For).ToList();
                    else
                        Final_stats = Final_stats.OrderBy(x => x.PPG_For).ToList();
                    break;
                case "PPG A":
                    if (isdecending)
                        Final_stats = Final_stats.OrderByDescending(x => x.PPG_Against).ToList();
                    else
                        Final_stats = Final_stats.OrderBy(x => x.PPG_Against).ToList();
                    break;
                case "PYF":
                    if (isdecending)
                        Final_stats = Final_stats.OrderByDescending(x => x.Passing_Yards_For).ToList();
                    else
                        Final_stats = Final_stats.OrderBy(x => x.Passing_Yards_For).ToList();
                    break;
                case "PYA":
                    if (isdecending)
                        Final_stats = Final_stats.OrderByDescending(x => x.Passing_Yards_Against).ToList();
                    else
                        Final_stats = Final_stats.OrderBy(x => x.Passing_Yards_Against).ToList();
                    break;
                case "RYF":
                    if (isdecending)
                        Final_stats = Final_stats.OrderByDescending(x => x.Rushing_Yards_For).ToList();
                    else
                        Final_stats = Final_stats.OrderBy(x => x.Rushing_Yards_For).ToList();
                    break;
                case "RYA":
                    if (isdecending)
                        Final_stats = Final_stats.OrderByDescending(x => x.Rushing_Yards_Against).ToList();
                    else
                        Final_stats = Final_stats.OrderBy(x => x.Rushing_Yards_Against).ToList();
                    break;
                case "Total Yards F":
                    if (isdecending)
                        Final_stats = Final_stats.OrderByDescending(x => x.Total_Yards_For).ToList();
                    else
                        Final_stats = Final_stats.OrderBy(x => x.Total_Yards_For).ToList();
                    break;
                case "Total Yards A":
                    if (isdecending)
                        Final_stats = Final_stats.OrderByDescending(x => x.Total_Yards_Against).ToList();
                    else
                        Final_stats = Final_stats.OrderBy(x => x.Total_Yards_Against).ToList();
                    break;
                case "TOF":
                    if (isdecending)
                        Final_stats = Final_stats.OrderByDescending(x => x.Turnovers_Recv).ToList();
                    else
                        Final_stats = Final_stats.OrderBy(x => x.Turnovers_Recv).ToList();
                    break;
                case "TOA":
                    if (isdecending)
                        Final_stats = Final_stats.OrderByDescending(x => x.Turnovers_Comm).ToList();
                    else
                        Final_stats = Final_stats.OrderBy(x => x.Turnovers_Comm).ToList();
                    break;
                case "3rd Downs":
                    if (isdecending)
                        Final_stats = Final_stats.OrderByDescending(x => x.Third_Down_String).ToList();
                    else
                        Final_stats = Final_stats.OrderBy(x => x.Third_Down_String).ToList();
                    break;
                case "4th Downs":
                    if (isdecending)
                        Final_stats = Final_stats.OrderByDescending(x => x.Fourth_Down_String).ToList();
                    else
                        Final_stats = Final_stats.OrderBy(x => x.Fourth_Down_String).ToList();
                    break;
                case "Sck F":
                    if (isdecending)
                        Final_stats = Final_stats.OrderByDescending(x => x.Sacks_For).ToList();
                    else
                        Final_stats = Final_stats.OrderBy(x => x.Sacks_For).ToList();
                    break;
                case "Sck A":
                    if (isdecending)
                        Final_stats = Final_stats.OrderByDescending(x => x.Sacks_Against).ToList();
                    else
                        Final_stats = Final_stats.OrderBy(x => x.Sacks_Against).ToList();
                    break;
                case "Rating":
                    if (isdecending)
                        Final_stats = Final_stats.OrderByDescending(x => x.Power_Ranking).ToList();
                    else
                        Final_stats = Final_stats.OrderBy(x => x.Power_Ranking).ToList();
                    break;
            }

            return Final_stats;
        }

        public void EndSeason(Loaded_League_Structure lls, BackgroundWorker bw)
        {
            string League_con_string = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + lls.season.League_Structure_by_Season[0].Short_Name.ToUpper() + Path.DirectorySeparatorChar + lls.season.League_Structure_by_Season[0].Short_Name.ToUpper() + "." + app_Constants.DB_FILE_EXT;
            string process_state = "Processing...";
            string state_struct = null;
            logger.Info("EndSeason Started");
            long Next_Season_ID;
            InjuriesDAO injDAO = new InjuriesDAO();
            LeagueDAO ld = new LeagueDAO();
            TeamDAO tDAO = new TeamDAO();

            //Current Season Lists
            List<Injury> del_InjuriesList = new List<Injury>();
            List<Player> new_player_list = new List<Player>();
            List<Player> Update_Players = new List<Player>();
            List<Player_Awards> Player_Awards = new List<Player_Awards>();

            //New Season Lists
            List<Player_Ratings> new_Player_Ratings = new List<Player_Ratings>();
            List<Injury_Log> Inj_Log = new List<Injury_Log>();
            List<Players_By_Team> new_Players_by_team = new List<Players_By_Team>();
            List<Player_Retiring_Log> new_Player_Retiring_Log = new List<Player_Retiring_Log>();
            List<League_Structure_by_Season> new_ls_list = new List<League_Structure_by_Season>();
            List<Conference> conf_list = new List<Conference>();
            List<Division> div_list = new List<Division>();
            List<Teams_by_Season> new_teams = new List<Teams_by_Season>();
            List<Draft> Draft_List = new List<Draft>();
            List<Game> new_games = new List<Game>();

            int i = default(int);
            try
            {
                Next_Season_ID = lls.season.ID + 1;

                // Update the progress bar
                i = 5;
                process_state = "Processing Injured Players 1 of 7";
                logger.Info(process_state);
                state_struct = "Processing..." + "|" + process_state + "|" + "";
                bw.ReportProgress(i, state_struct);
                //get all injured players for this season.  All of these records be deleted
                del_InjuriesList = injDAO.getLeagueInjuredPlayers(lls.season.ID, League_con_string);

                // Update the progress bar
                i = 10;
                process_state = "Assigning Player Awards 2 of 7";
                logger.Info(process_state);
                state_struct = "Processing..." + "|" + process_state + "|" + "";
                bw.ReportProgress(i, state_struct);

                League_Stats lStats = new League_Stats();
                lStats.Passing_Stats = ld.getLeagueSeasonPassingStats(lls.season.ID, League_con_string);
                lStats.Rushing_Stats = ld.getLeagueSeasonRushingStats(lls.season.ID, League_con_string);
                lStats.Receiving_Stats = ld.getLeagueSeasonReceivingStats(lls.season.ID, League_con_string);
                lStats.Blocking_Stats = ld.getLeagueSeasonBlockingStats(lls.season.ID, League_con_string);
                lStats.Defense_Stats = ld.getLeagueSeasonDefenseStats(lls.season.ID, League_con_string);
                lStats.Pass_Defense_Stats = ld.getLeagueSeasonPassDefenseStats(lls.season.ID, League_con_string);
                lStats.Kicking_Stats = ld.getLeagueSeasonKickerStats(lls.season.ID, League_con_string);
                lStats.Punting_Stats = ld.getLeagueSeasonPunterStats(lls.season.ID, League_con_string);
                List<Award_Rating_Rec> award_recs_list = Award_Helper.setPlayersMasterList(lStats);

                //Determine Off and Deff Rocking of the year awards
                Award_Rating_Rec off_roy_rec = award_recs_list.Where(x => x.isRookie && Player_Helper.isOffense(x.p.Pos)).OrderByDescending(x => x.Grade).First();
                Player_Awards.Add(new Player_Awards()
                {
                    Award_Code = app_Constants.ROCKIE_OF_THE_YEAR_OFF,
                    Player_ID = off_roy_rec.p.ID,
                    Season_ID = lls.season.ID
                });

                Award_Rating_Rec def_roy_rec = award_recs_list.Where(x => x.isRookie && !Player_Helper.isOffense(x.p.Pos)).OrderByDescending(x => x.Grade).First();
                Player_Awards.Add(new Player_Awards()
                {
                    Award_Code = app_Constants.ROCKIE_OF_THE_YEAR_DEF,
                    Player_ID = off_roy_rec.p.ID,
                    Season_ID = lls.season.ID
                });

                //Determine Off and Deff player of the year awards
                Award_Rating_Rec off_poy_rec = award_recs_list.Where(x => Player_Helper.isOffense(x.p.Pos)).OrderByDescending(x => x.Grade).First();
                Player_Awards.Add(new Player_Awards()
                {
                    Award_Code = app_Constants.PLAYER_OF_THE_YEAR_OFF,
                    Player_ID = off_roy_rec.p.ID,
                    Season_ID = lls.season.ID
                });

                Award_Rating_Rec def_poy_rec = award_recs_list.Where(x => !Player_Helper.isOffense(x.p.Pos)).OrderByDescending(x => x.Grade).First();
                Player_Awards.Add(new Player_Awards()
                {
                    Award_Code = app_Constants.PLAYER_OF_THE_YEAR_DEF,
                    Player_ID = off_roy_rec.p.ID,
                    Season_ID = lls.season.ID
                });

                //Set all pros for all positions
                foreach (Player_Pos pp in System.Enum.GetValues(typeof(Player_Pos)))
                {
                    int num_all_pros = Award_Helper.getAllProNum(pp, lls.season.League_Structure_by_Season[0].Num_Teams);
                    List<Award_Rating_Rec> all_pro_list = award_recs_list
                        .Where(x => x.p.Pos == (int)pp).OrderByDescending(x => x.Grade).Take(num_all_pros).ToList();
                    foreach (Award_Rating_Rec a in all_pro_list)
                    {
                        Player_Awards.Add(new Player_Awards()
                        {
                            Award_Code = app_Constants.ALL_PRO,
                            Player_ID = a.p.ID,
                            Season_ID = lls.season.ID
                        });
                    }
                }

                i = 25;
                process_state = "Aging Players 3 of 7";
                logger.Info(process_state);
                state_struct = "Processing..." + "|" + process_state + "|" + "";
                bw.ReportProgress(i, state_struct);
                List<Player_and_Ratings> all_players_list = ld.getAllActivePlayers(lls.season.ID, League_con_string);
                foreach (Player_and_Ratings pr in all_players_list)
                {
                    bool bPlayertoRetire = false;
                    //First check if the player has a career ending injury and if so then retire him
                    bool bCareerEndingInj = false;
                    Injury player_injury = del_InjuriesList.Where(x => x.Player_ID == pr.p.ID).FirstOrDefault();
                    if (player_injury != null && player_injury.Career_Ending == 1)
                        bCareerEndingInj = true;

                    if (!bCareerEndingInj)
                    {
                        bPlayertoRetire = Player_Helper.WillPlayerRetire(pr.p.Age);
                        if (!bPlayertoRetire)
                        {
                            //If the player had an injury indicate that the player is returning from their injury.
                            if (player_injury != null)
                            {
                                Inj_Log.Add(new Injury_Log()
                                {
                                    Season_ID = Next_Season_ID,
                                    Injured = 0,
                                    Player_ID = pr.p.ID,
                                    Week = app_Constants.PLAYER_RETIRING_RETURNING_WEEK
                                });
                            }

                            //adjust player ratings
                            Player_Ratings new_pr = new Player_Ratings()
                            {
                                Season_ID = Next_Season_ID,
                                Player_ID = pr.pr[0].Player_ID,
                                Accuracy_Rating = pr.pr[0].Accuracy_Rating,
                                Agilty_Rating = pr.pr[0].Agilty_Rating,
                                Arm_Strength_Rating = pr.pr[0].Arm_Strength_Rating,
                                Ball_Safety_Rating = pr.pr[0].Ball_Safety_Rating,
                                Decision_Making_Rating = pr.pr[0].Decision_Making_Rating,
                                Hands_Rating = pr.pr[0].Hands_Rating,
                                Kicker_Leg_Accuracy_Rating = pr.pr[0].Kicker_Leg_Accuracy_Rating,
                                Kicker_Leg_Power_Rating = pr.pr[0].Kicker_Leg_Power_Rating,
                                Pass_Attack_Rating = pr.pr[0].Pass_Attack_Rating,
                                Pass_Block_Rating = pr.pr[0].Pass_Block_Rating,
                                Running_Power_Rating = pr.pr[0].Running_Power_Rating,
                                Run_Attack_Rating = pr.pr[0].Run_Attack_Rating,
                                Run_Block_Rating = pr.pr[0].Run_Block_Rating,
                                Speed_Rating = pr.pr[0].Speed_Rating,
                                Sportsmanship_Ratings = pr.pr[0].Sportsmanship_Ratings,
                                Tackle_Rating = pr.pr[0].Tackle_Rating,
                                Toughness_Ratings = pr.pr[0].Toughness_Ratings,
                                Work_Ethic_Ratings = pr.pr[0].Work_Ethic_Ratings
                            };
                            new_pr = Player_Helper.adjust_ratings(pr.p.Age, new_pr, (Player_Pos)pr.p.Pos);
                            new_Player_Ratings.Add(new_pr);

                            //Update Player record by incrementing age
                            pr.p.Age += 1;
                            Update_Players.Add(pr.p);

                            //If the player was on a team last year, then Write next year's player_by_team record
                            if (pr.pbt != null)
                            {
                                Players_By_Team new_pbt = new Players_By_Team()
                                {
                                    Season_ID = Next_Season_ID,
                                    Player_ID = pr.pbt.Player_ID,
                                    Franchise_ID = pr.pbt.Franchise_ID,
                                    Jersey_Number = pr.pbt.Jersey_Number
                                };
                                new_Players_by_team.Add(new_pbt);
                            }

                        }
                    }

                    if (bPlayertoRetire || bCareerEndingInj)
                    {
                        //the player is to retire
                        Player_Retiring_Log new_prl = new Player_Retiring_Log()
                        {
                            Player_ID = pr.p.ID,
                            Season_ID = Next_Season_ID,
                            Week = app_Constants.PLAYER_RETIRING_RETURNING_WEEK
                        };
                        new_Player_Retiring_Log.Add(new_prl);

                        pr.p.Retired = 1;
                        Update_Players.Add(pr.p);
                    }
                }

                i = 50;
                process_state = "Processing New Season Objects 4 of 7";
                logger.Info(process_state);
                state_struct = "Processing..." + "|" + process_state + "|" + "";

                League_Structure_by_Season old_ls = lls.season.League_Structure_by_Season[0];
                League_Structure_by_Season new_league_structure = new League_Structure_by_Season()
                {
                    Season_ID = Next_Season_ID,
                    Championship_Game_Name = old_ls.Championship_Game_Name,
                    Draft_Type_Code = old_ls.Draft_Type_Code,
                    Extra_Point = old_ls.Extra_Point,
                    Home_Advantage = old_ls.Home_Advantage,
                    Injuries = old_ls.Injuries,
                    Kickoff_Type = old_ls.Kickoff_Type,
                    League_Logo_File = old_ls.League_Logo_File,
                    League_Logo_Filepath = old_ls.League_Logo_Filepath,
                    Long_Name = old_ls.Long_Name,
                    Number_of_Conferences = old_ls.Number_of_Conferences,
                    Number_of_Divisions = old_ls.Number_of_Divisions,
                    Number_of_Games = old_ls.Number_of_Games,
                    Number_of_weeks = old_ls.Number_of_weeks,
                    Num_Playoff_Teams = old_ls.Num_Playoff_Teams,
                    Num_Teams = old_ls.Num_Teams,
                    Onside_Kick = old_ls.Onside_Kick,
                    Penalties = old_ls.Penalties,
                    Short_Name = old_ls.Short_Name,
                    Three_Point_Conversion = old_ls.Three_Point_Conversion,
                    Two_Point_Conversion = old_ls.Two_Point_Conversion
                };

                new_ls_list.Add(new_league_structure);
                foreach (Conference c in lls.season.Conferences)
                {
                    conf_list.Add(new Conference()
                    {
                        Season_ID = Next_Season_ID,
                        Conf_Name = c.Conf_Name,
                        Ordinal = c.Ordinal
                    });
                }

                foreach (Division d in lls.season.Divisions)
                {
                    div_list.Add(new Division()
                    {
                        Season_ID = Next_Season_ID,
                        Name = d.Name,
                        Ordinal = d.Ordinal
                    });
                }

                //Create new teams for next season
                foreach (Teams_by_Season t in lls.season.Teams_by_Season)
                {
                    new_teams.Add(new Teams_by_Season()
                    {
                        Season_ID = Next_Season_ID,
                        Nickname = t.Nickname,
                        Home_jersey_Color = t.Home_jersey_Color,
                        Home_Jersey_Number_Outline_Color = t.Home_Jersey_Number_Outline_Color,
                        Home_Jersey_Number_Color = t.Home_Jersey_Number_Color,
                        Home_Jersey_Shoulder_Stripe = t.Home_Jersey_Shoulder_Stripe,
                        Home_Jersey_Sleeve_Stripe_Color_1 = t.Home_Jersey_Sleeve_Stripe_Color_1,
                        Home_Jersey_Sleeve_Stripe_Color_2 = t.Home_Jersey_Sleeve_Stripe_Color_2,
                        Home_Jersey_Sleeve_Stripe_Color_3 = t.Home_Jersey_Sleeve_Stripe_Color_3,
                        Home_Jersey_Sleeve_Stripe_Color_4 = t.Home_Jersey_Sleeve_Stripe_Color_4,
                        Home_Jersey_Sleeve_Stripe_Color_5 = t.Home_Jersey_Sleeve_Stripe_Color_5,
                        Home_Jersey_Sleeve_Stripe_Color_6 = t.Home_Jersey_Sleeve_Stripe_Color_6,
                        Home_Sleeve_Color = t.Home_Sleeve_Color,
                        Home_Pants_Color = t.Home_Pants_Color,
                        Home_Pants_Stripe_Color_1 = t.Home_Pants_Stripe_Color_1,
                        Home_Pants_Stripe_Color_2 = t.Home_Pants_Stripe_Color_2,
                        Home_Pants_Stripe_Color_3 = t.Home_Pants_Stripe_Color_3,
                        Away_jersey_Color = t.Away_jersey_Color,
                        Away_Jersey_Number_Outline_Color = t.Away_Jersey_Number_Outline_Color,
                        Away_Jersey_Number_Color = t.Away_Jersey_Number_Color,
                        Away_Jersey_Shoulder_Stripe = t.Away_Jersey_Shoulder_Stripe,
                        Away_Jersey_Sleeve_Stripe_Color_1 = t.Away_Jersey_Sleeve_Stripe_Color_1,
                        Away_Jersey_Sleeve_Stripe_Color_2 = t.Away_Jersey_Sleeve_Stripe_Color_2,
                        Away_Jersey_Sleeve_Stripe_Color_3 = t.Away_Jersey_Sleeve_Stripe_Color_3,
                        Away_Jersey_Sleeve_Stripe_Color_4 = t.Away_Jersey_Sleeve_Stripe_Color_4,
                        Away_Jersey_Sleeve_Stripe_Color_5 = t.Away_Jersey_Sleeve_Stripe_Color_5,
                        Away_Jersey_Sleeve_Stripe_Color_6 = t.Away_Jersey_Sleeve_Stripe_Color_6,
                        Away_Sleeve_Color = t.Away_Sleeve_Color,
                        Away_Pants_Color = t.Away_Pants_Color,
                        Away_Pants_Stripe_Color_1 = t.Away_Pants_Stripe_Color_1,
                        Away_Pants_Stripe_Color_2 = t.Away_Pants_Stripe_Color_2,
                        Away_Pants_Stripe_Color_3 = t.Away_Pants_Stripe_Color_3,
                        City = t.City,
                        City_Abr = t.City_Abr,
                        Cleats_Color = t.Cleats_Color,
                        Franchise_ID = t.Franchise_ID,
                        Helmet_Color = t.Helmet_Color,
                        Helmet_Facemask_Color = t.Helmet_Facemask_Color,
                        Helmet_Image_File = t.Helmet_Image_File,
                        Helmet_img_path = t.Helmet_img_path,
                        Helmet_Logo_Color = t.Helmet_Logo_Color,
                        Owner = t.Owner,
                        Socks_Color = t.Socks_Color,
                        Stadium_Capacity = t.Stadium_Capacity,
                        Stadium_Field_Color = t.Stadium_Field_Color,
                        Stadium_Field_Type = t.Stadium_Field_Type,
                        Stadium_Image_File = t.Stadium_Image_File,
                        Stadium_Img_Path = t.Stadium_Img_Path,
                        Stadium_Location = t.Stadium_Location,
                        Stadium_Name = t.Stadium_Name,
                        Team_Slot = t.Team_Slot
                    });

                    
                }

                // Update the progress bar
                i = 60;
                process_state = "Create Players for Draft 5 of 7";
                logger.Info(process_state);
                state_struct = "Processing..." + "|" + process_state + "|" + "";
                bw.ReportProgress(i, state_struct);

                //Create players for draft
                int num_players = (int)((app_Constants.NORMAL_DRAFT_ROUNDS * lls.season.League_Structure_by_Season[0].Num_Teams) * app_Constants.DRAFT_MULTIPLIER);
                new_player_list = League_Helper.Create_New_Players(num_players, false, Next_Season_ID);

                i = 70;
                process_state = "Create Draft 6 of 7";
                logger.Info(process_state);
                state_struct = "Processing..." + "|" + process_state + "|" + "";
                bw.ReportProgress(i, state_struct);

                //Get Franchises to be used for draft.
                List<Franchise> fran_list = tDAO.getAllFranchises(League_con_string);

                //Create Draft
                int draft_rounds = app_Constants.NORMAL_DRAFT_ROUNDS;
                Draft_Helper DH = new Draft_Helper();
                Draft_List = DH.Create_Draft(lls.season.Year, new_league_structure.Draft_Type_Code, draft_rounds, fran_list, Next_Season_ID, League_con_string);

                i = 85;
                process_state = "Processing Creating Schedule for Next Season  7 of 7";
                logger.Info(process_state);
                state_struct = "Processing..." + "|" + process_state + "|" + "";
                bw.ReportProgress(i, state_struct);
                // Creating schedule
                logger.Info("Creating schedule");
                List<string> sched = create_schedule(new_league_structure.Short_Name, (int)new_league_structure.Num_Teams, div_list.Count, conf_list.Count, (int)new_league_structure.Number_of_Games, (int)new_league_structure.Number_of_weeks);
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
                        Season_ID = Next_Season_ID, 
                        Week = long.Parse(sWeek),
                        Home_Team_Franchise_ID = new_teams.Where(x => x.Team_Slot == long.Parse(ht)).First().Franchise_ID,
                        Away_Team_Franchise_ID = new_teams.Where(x => x.Team_Slot == long.Parse(at)).First().Franchise_ID
                    };

                    new_games.Add(g);

                }

                Season New_Season = new Season()
                {
                    ID = Next_Season_ID,
                    Year = lls.season.Year++,
                    League_Structure_by_Season = new_ls_list,
                    Conferences = conf_list,
                    Divisions = div_list,
                    Drafts = Draft_List,
                    Teams_by_Season = new_teams,
                    Games = new_games,
                    Player_Ratings = new_Player_Ratings,
                    Players_By_Team = new_Players_by_team,
                    Injury_Log = Inj_Log,
                    Player_Retiring_Log = new_Player_Retiring_Log
                };

                ld.EndSeason(del_InjuriesList, new_player_list, Update_Players, Player_Awards, League_con_string);

                //call dao end of season method
                logger.Info("EndSeason Ended Successfully");
                // Update the progress bar
                i = 100;
                process_state = "";
                state_struct = "Complete" + "|" + process_state + "|" + "League Completed Successfully!";
                bw.ReportProgress(i, state_struct);
            }
            catch (Exception ex)
            {
                state_struct = "Error" + "|" + process_state + "|" + "Failed to end season";
                bw.ReportProgress(i, state_struct);

                logger.Error("Create league service failed");
                if (ex.InnerException != null)
                    logger.Error("Inner Exception: " + ex.InnerException.ToString());
                logger.Error(ex);
            }

            logger.Info("EndSeason Ended Successfully");
        }

        public string[] getGameOptions()
        {
            string sBallColor = "";
            string s3d = "";
            bool tbool;

            string options_filename = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + app_Constants.GAME_OPTIONS_FILE;
            if (File.Exists(options_filename))
            {
                foreach (string line in File.ReadAllLines(options_filename))
                {
                    if (line.StartsWith("Game_Ball_Color:"))
                        sBallColor = line.Split(':')[1];
                    if (line.StartsWith("Ball_Style_3d:"))
                        s3d = line.Split(':')[1];
                }

                try
                {
                    tbool = Convert.ToBoolean(s3d);
                }
                catch (Exception e)
                {
                    tbool = true;
                }

            }
            else
            {
                sBallColor = app_Constants.DEFAULT_GAME_BALL_COLOR + "|" + app_Constants.DEFAULT_GAME_BALL_2_COLOR;
                s3d = "true";
            }

            return new string[] { sBallColor.Trim(), s3d };

        }

    }
}
