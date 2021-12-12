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
using SpectatorFootball.DraftsNS;
using SpectatorFootball.Versioning;
using SpectatorFootball.Enum;
using System.Security.AccessControl;
using System.Security.Principal;
using SpectatorFootball.PlayerNS;

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
                File.Copy(ddf, backed_up_file);

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

        public League_Stats getSeasonStats(Loaded_League_Structure lls,  League_Stats lStats, Stat_Type st, string sort_field, bool sort_desc, long season_id,  string League_Shortname)
        {
            string DIRPath_League = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + lls.season.League_Structure_by_Season[0].Short_Name.ToUpper();
            string helment_img_path = DIRPath_League + Path.DirectorySeparatorChar + app_Constants.LEAGUE_HELMETS_SUBFOLDER;
            League_Stats r = lStats;
            string League_con_string = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + lls.season.League_Structure_by_Season[0].Short_Name.ToUpper() + Path.DirectorySeparatorChar + lls.season.League_Structure_by_Season[0].Short_Name.ToUpper() + "." + app_Constants.DB_FILE_EXT;

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
                        case "Name":
                        case "City_Abbr":
                        case "Comp_Percent":
                        case "QBR":
                            if (sort_desc == false)
                                lStats.Passing_Stats = lStats.Passing_Stats.OrderBy(x =>
                                sort_field == "Name" ? x.p.Last_Name + x.p.First_Name :
                                sort_field == "City_Abbr" ? x.City_Abbr :
                                sort_field == "Comp_Percent" ? x.Comp_Percent :
                                sort_field == "QBR" ? x.QBR : x.QBR).ToList();
                            else
                                lStats.Passing_Stats = lStats.Passing_Stats.OrderByDescending(x =>
                                sort_field == "Name" ? x.p.Last_Name + x.p.First_Name :
                                sort_field == "City_Abbr" ? x.City_Abbr :
                                sort_field == "Comp_Percent" ? x.Comp_Percent :
                                sort_field == "QBR" ? x.QBR : x.QBR).ToList();
                            break;
                        case "Completes":
                        case "Ateempts":
                        case "Yards":
                        case "TDs":
                        case "Ints":
                        case "Fumbles_Lost":
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
                        case "Name":
                        case "City_Abbr":
                        case "Yards_Per_Carry":
                            if (sort_desc == false)
                                lStats.Rushing_Stats = lStats.Rushing_Stats.OrderBy(x =>
                                sort_field == "Name" ? x.p.Last_Name + x.p.First_Name :
                                sort_field == "City_Abbr" ? x.City_Abbr :
                                sort_field == "Yards_Per_Carry" ? x.Yards_Per_Carry : x.Yards_Per_Carry).ToList();
                            else
                                lStats.Rushing_Stats = lStats.Rushing_Stats.OrderByDescending(x =>
                                sort_field == "Name" ? x.p.Last_Name + x.p.First_Name :
                                sort_field == "City_Abbr" ? x.City_Abbr :
                                sort_field == "Yards_Per_Carry" ? x.Yards_Per_Carry : x.Yards_Per_Carry).ToList();
                            break;
                        case "Rushes":
                        case "Yards":
                        case "TDs":
                        case "Fumbles_Lost":
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
                        case "Name":
                        case "City_Abbr":
                        case "Yards_Per_Catch":
                            if (sort_desc == false)
                                lStats.Receiving_Stats = lStats.Receiving_Stats.OrderBy(x =>
                                sort_field == "Name" ? x.p.Last_Name + x.p.First_Name :
                                sort_field == "City_Abbr" ? x.City_Abbr :
                                sort_field == "Yards_Per_Catch" ? x.Yards_Per_Catch : x.Yards_Per_Catch).ToList();
                            else
                                lStats.Receiving_Stats = lStats.Receiving_Stats.OrderByDescending(x =>
                                sort_field == "Name" ? x.p.Last_Name + x.p.First_Name :
                                sort_field == "City_Abbr" ? x.City_Abbr :
                                sort_field == "Yards_Per_Catch" ? x.Yards_Per_Catch : x.Yards_Per_Catch).ToList();
                            break;
                        case "Catches":
                        case "Yards":
                        case "TDs":       
                        case "Drops":
                        case "Fumbles_Lost":
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
                        case "Name":
                        case "City_Abbr":
                            if (sort_desc == false)
                                lStats.Blocking_Stats = lStats.Blocking_Stats.OrderBy(x =>
                                sort_field == "Name" ? x.p.Last_Name + x.p.First_Name :
                                sort_field == "City_Abbr" ? x.City_Abbr : x.City_Abbr).ToList();
                            else
                                lStats.Blocking_Stats = lStats.Blocking_Stats.OrderByDescending(x =>
                                sort_field == "Name" ? x.p.Last_Name + x.p.First_Name :
                                sort_field == "City_Abbr" ? x.City_Abbr : x.City_Abbr).ToList();
                            break;
                        case "Plays":
                        case "Pancakes":
                        case "Sacks_Allowed":
                        case "Pressures_Allowed":
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
                        case "Name":
                        case "City_Abbr":
                            if (sort_desc == false)
                                lStats.Defense_Stats = lStats.Defense_Stats.OrderBy(x =>
                                sort_field == "Name" ? x.p.Last_Name + x.p.First_Name :
                                sort_field == "City_Abbr" ? x.City_Abbr : x.City_Abbr).ToList();
                            else
                                lStats.Defense_Stats = lStats.Defense_Stats.OrderByDescending(x =>
                                sort_field == "Name" ? x.p.Last_Name + x.p.First_Name :
                                sort_field == "City_Abbr" ? x.City_Abbr : x.City_Abbr).ToList();
                            break;
                        case "Plays":
                        case "Tackles":
                        case "Sacks":
                        case "Pressures":
                        case "Run_for_Loss":
                        case "Forced_Fumble":
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
                    if (lStats.Defense_Stats == null)
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
                        case "Name":
                        case "City_Abbr":
                            if (sort_desc == false)
                                lStats.Pass_Defense_Stats = lStats.Pass_Defense_Stats.OrderBy(x =>
                                sort_field == "Name" ? x.p.Last_Name + x.p.First_Name :
                                sort_field == "City_Abbr" ? x.City_Abbr : x.City_Abbr).ToList();
                            else
                                lStats.Pass_Defense_Stats = lStats.Pass_Defense_Stats.OrderByDescending(x =>
                                sort_field == "Name" ? x.p.Last_Name + x.p.First_Name :
                                sort_field == "City_Abbr" ? x.City_Abbr : x.City_Abbr).ToList();
                            break;
                        case "Pass_Defenses":
                        case "Ints":
                        case "TDs_Surrendered":
                        case "Forced_Fumble":
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



            }

            //            r = ld.getAllSeasons(League_con_string);

            return r;
        }

    }
}
