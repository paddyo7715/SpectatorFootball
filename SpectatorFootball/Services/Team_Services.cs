using System.Collections.Generic;
using SpectatorFootball.Models;
using SpectatorFootball.Enum;
using SpectatorFootball.DAO;
using System;
using log4net;
using SpectatorFootball.DraftNS;
using SpectatorFootball.League;
using System.Linq;
using System.IO;
using SpectatorFootball.Team;
using SpectatorFootball.PlayerNS;

namespace SpectatorFootball
{
    public class Team_Services
    {
        private static ILog logger = LogManager.GetLogger("RollingFile");
        public string FirstDuplicateTeam(List<Teams_by_Season> Team_List)
        {
            string r = null;
            var hs = new HashSet<string>();

            foreach (var t in Team_List)
            {
                string s = t.City.ToUpper().Trim() + " " + t.Nickname.ToUpper().Trim();
                if (hs.Contains(s))
                {
                    r = s;
                    break;
                }
                else
                    hs.Add(s);
            }

            return r;
        }

        public void UpdateTeam(Loaded_League_Structure lls, Teams_by_Season t, 
            string sNewHelmetPath, string sNewFieldPath)
        {
            string League_Shortname = lls.season.League_Structure_by_Season.First().Short_Name;
            string DIRPath_League = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + League_Shortname.ToUpper();
            string League_con_string = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + League_Shortname + Path.DirectorySeparatorChar + League_Shortname + "." + app_Constants.DB_FILE_EXT;
            string helment_img_path = DIRPath_League + Path.DirectorySeparatorChar + app_Constants.LEAGUE_HELMETS_SUBFOLDER;

            string old_helment_filename = t.Helmet_Image_File;
            string new_helment_filename = Path.GetFileName(sNewHelmetPath);
            bool bHemetImageChanged = false;



            //Copy and update if it is there the helmet and stadium images
            if (sNewHelmetPath != DIRPath_League + Path.DirectorySeparatorChar + app_Constants.LEAGUE_HELMETS_SUBFOLDER + Path.DirectorySeparatorChar + Path.GetFileName(t.Helmet_img_path))
            {
                logger.Debug("Copying " + sNewFieldPath);
                t.Helmet_Image_File = new_helment_filename;
                File.Copy(t.Helmet_img_path, DIRPath_League + Path.DirectorySeparatorChar + app_Constants.LEAGUE_HELMETS_SUBFOLDER + Path.DirectorySeparatorChar + Path.GetFileName(sNewHelmetPath), true);
                bHemetImageChanged = true;
            }
            if (sNewFieldPath != DIRPath_League + Path.DirectorySeparatorChar + app_Constants.LEAGUE_STADIUM_SUBFOLDER + Path.DirectorySeparatorChar + Path.GetFileName(t.Stadium_Img_Path))
            {
                logger.Debug("Copying " + sNewFieldPath);
                t.Stadium_Image_File = Path.GetFileName(sNewFieldPath); ;
                File.Copy(t.Stadium_Img_Path, DIRPath_League + Path.DirectorySeparatorChar + app_Constants.LEAGUE_STADIUM_SUBFOLDER + Path.DirectorySeparatorChar + Path.GetFileName(sNewFieldPath), true);
            }
            //Update database with new team details
            TeamDAO tDAO = new TeamDAO();
            tDAO.UpdateTeam(t, League_con_string);

            //Update the internal list that holds the league helmets only if the filename has changed
            if (bHemetImageChanged)
            {
                League_Helmet lHelmet = lls.Team_Helmets.Where(x => x.Helmet_File == old_helment_filename).FirstOrDefault();
                lHelmet.Helmet_File = new_helment_filename;
                lHelmet.Image = CommonUtils.getImageorBlank(helment_img_path + Path.DirectorySeparatorChar + new_helment_filename);
            }

        }

        public Team_Player_Accum_Stats getTeamSeasonStats(string League_Shortname, long season_id, long Franchise_id)
        {
            Team_Player_Accum_Stats r = null;

            string DIRPath_League = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + League_Shortname.ToUpper();
            string League_con_string = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + League_Shortname + Path.DirectorySeparatorChar + League_Shortname + "." + app_Constants.DB_FILE_EXT;
            TeamDAO td = new TeamDAO();

            r = td.getTeamSeasonPlayerStats(season_id, Franchise_id, League_con_string);

            //Add passing complete % and QB rating to passing stats
            foreach (var p in r.Passing_Stats)
            {
                p.Comp_Percent = Player_Helper.FormatCompPercent(p.Completes, p.Ateempts);
                p.QBR = Player_Helper.CalculateQBR(p.Completes, p.Ateempts, p.Yards, p.TDs, p.Ints);
            }

            //Add the yards per carry stat to the rushing stats
            foreach (var s in r.Rushing_Stats)
                s.Yards_Per_Carry = Player_Helper.CalcYardsPerCarry_or_Catch(s.Rushes , s.Yards);

            //Add the yards per catch stat to the receiving stats
            foreach (var s in r.Receiving_Stats)
                s.Yards_Per_Catch = Player_Helper.CalcYardsPerCarry_or_Catch(s.Catches, s.Yards);

            //Add the FG percent to kicking stats
            foreach (var s in r.Kicking_Stats)
                s.FG_Percent = Player_Helper.CalcYardsPerCarry_or_Catch(s.FG_Made, s.FG_ATT);

            //Add punt avg to the punting stats
            foreach (var s in r.Punting_Stats)
                s.Punt_AVG = Player_Helper.CalcYardsPerCarry_or_Catch(s.Punts, s.Yards);

            //Add kick return avg to kick return stats
            foreach (var s in r.KickRet_Stats)
                s.Yards_avg = Player_Helper.CalcYardsPerCarry_or_Catch(s.Returns, s.Yards);

            //Add punt return avg to punt return stats
            foreach (var s in r.PuntRet_Stats)
                s.Yards_avg = Player_Helper.CalcYardsPerCarry_or_Catch(s.Returns, s.Yards);

            return r;
        }

        public List<Three_Coll_List> getTeamStats(string League_Shortname, long season_id, long Franchise_id)
        {
            List<Three_Coll_List> r = new List<Three_Coll_List>();
            TeamStatsRaw tr = null;

            string DIRPath_League = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + League_Shortname.ToUpper();
            string League_con_string = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + League_Shortname + Path.DirectorySeparatorChar + League_Shortname + "." + app_Constants.DB_FILE_EXT;
            TeamDAO td = new TeamDAO();

            tr = td.getTeamStats(season_id, Franchise_id, League_con_string);

            r.Add(new Three_Coll_List() { col1 = tr.Team_Points.ToString(), col2 = "Points", col3 = tr.Opp_Points.ToString() });
            r.Add(new Three_Coll_List() { col1 = tr.Team_First_Downs.ToString(), col2 = "First Downs", col3 = tr.Opp_First_Downs.ToString() }); r.Add(new Three_Coll_List() { col1 = tr.Team_First_Downs.ToString(), col2 = "First Downs", col3 = tr.Opp_First_Downs.ToString() });
            r.Add(new Three_Coll_List() { col1 = tr.Team_Third_Downs_Att.ToString() + "/" + tr.Team_Third_Downs_Made + " (" + Player_Helper.FormatCompPercent(tr.Team_Third_Downs_Att, tr.Team_Third_Downs_Made) + "%)", col2 = "Third down Conversions", col3 = tr.Opp_Third_Downs_Att.ToString() + "/" + tr.Opp_Third_Downs_Made + " (" + Player_Helper.FormatCompPercent(tr.Opp_Third_Downs_Att, tr.Opp_Third_Downs_Made) + "%)" });
            r.Add(new Three_Coll_List() { col1 = tr.Team_Fourth_Downs_Att.ToString() + "/" + tr.Team_Fourth_Downs_Made + " (" + Player_Helper.FormatCompPercent(tr.Team_Fourth_Downs_Att, tr.Team_Fourth_Downs_Made) + "%)", col2 = "Fourth down Conversions", col3 = tr.Opp_Fourth_Downs_Att.ToString() + "/" + tr.Opp_Fourth_Downs_Made + " (" + Player_Helper.FormatCompPercent(tr.Opp_Fourth_Downs_Att, tr.Opp_Fourth_Downs_Made) + "%)" });
            r.Add(new Three_Coll_List() { col1 = tr.Team_Rushing_Yards.ToString(), col2 = "Rushing Yards", col3 = tr.Opp_Rushing_Yards.ToString() });
            r.Add(new Three_Coll_List() { col1 = tr.Team_Passing_Yards.ToString(), col2 = "Passing Yards", col3 = tr.Opp_Passing_Yards.ToString() });
            r.Add(new Three_Coll_List() { col1 = (tr.Team_Rushing_Yards + tr.Team_Passing_Yards).ToString(), col2 = "Total yards", col3 = (tr.Opp_Rushing_Yards + tr.Opp_Passing_Yards).ToString() });
            r.Add(new Three_Coll_List() { col1 = tr.Team_Sacks.ToString(), col2 = "Sacks", col3 = tr.Opp_Sacks.ToString() });
            r.Add(new Three_Coll_List() { col1 = tr.Team_Turnovers.ToString(), col2 = "Turnovers", col3 = tr.Opp_Turnovers.ToString() });

            return r;
        }

        public List<Roster_rec> getTeamRoster(long season_id, long Franchise_id, string League_Shortname)
        {
            List<Roster_rec> r = null;
            string DIRPath_League = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + League_Shortname.ToUpper();
            string League_con_string = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + League_Shortname + Path.DirectorySeparatorChar + League_Shortname + "." + app_Constants.DB_FILE_EXT;

            TeamDAO td = new TeamDAO();

            r = td.getTeamRoster(season_id, Franchise_id, League_con_string);

            return r;
        }

        public List<Team_History_Row> getTeamHistory(long Franchise_id, string League_Shortname)
        {
            List<Team_History_Row> r = null;
            string DIRPath_League = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + League_Shortname.ToUpper();
            string League_con_string = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + League_Shortname + Path.DirectorySeparatorChar + League_Shortname + "." + app_Constants.DB_FILE_EXT;

            TeamDAO td = new TeamDAO();

            r = td.getTeamHistory(Franchise_id, League_con_string);

//Cycle thru the team history recrds and set playoff record to blanks if the team did not 
//make the playoffs and set champoinship result string
            foreach (Team_History_Row h in r)
            {
                h.Blank = "";
                if (h.play_wins == "0" && h.play_loses == "0")
                {
                    h.play_loses = "";
                    h.play_PA = "";
                    h.play_PF = "";
                    h.play_wins = "";
                }
                if (h.champ_PF > 0 || h.champ_PA > 0)
                {
                    string win_or_lose = h.champ_PF > h.champ_PA ? "Won" : "Lost";
                    h.champ_result = win_or_lose + " the game by a score of " + h.champ_PF.ToString() + "-" + h.champ_PA.ToString();
                }
                else
                    h.champ_result = "";
            }

            return r;
        }
        public List<OneInt2Strings> getTeamTransactions(long season_id, long Franchise_id, string League_Shortname)
        {
            List<OneInt2Strings> r = null;
            string DIRPath_League = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + League_Shortname.ToUpper();
            string League_con_string = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + League_Shortname + Path.DirectorySeparatorChar + League_Shortname + "." + app_Constants.DB_FILE_EXT;

            TeamDAO td = new TeamDAO();

            r = td.getTeamTransactions(season_id, Franchise_id, League_con_string);

            return r;
        }
    }
    }
