using System.Collections.Generic;
using SpectatorFootball.Models;
using SpectatorFootball.Enum;
using SpectatorFootball.DAO;
using System;
using SpectatorFootball.DraftNS;
using SpectatorFootball.League;
using System.Linq;
using System.IO;
using SpectatorFootball.Team;

namespace SpectatorFootball
{
    public class Team_Services
    {

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

        public void UpdateTeam(Loaded_League_Structure lls, Teams_by_Season t)
        {
            string League_Shortname = lls.season.League_Structure_by_Season.First().Short_Name;
            string DIRPath_League = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + League_Shortname.ToUpper();
            string League_con_string = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + League_Shortname + Path.DirectorySeparatorChar + League_Shortname + "." + app_Constants.DB_FILE_EXT;

            TeamDAO tDAO = new TeamDAO();
            tDAO.UpdateTeam(t, League_con_string);
        }

        public Team_Player_Accum_Stats getTeamSeasonStats(string League_Shortname, long season_id, long Franchise_id)
        {
            Team_Player_Accum_Stats r = null;

            string DIRPath_League = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + League_Shortname.ToUpper();
            string League_con_string = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + League_Shortname + Path.DirectorySeparatorChar + League_Shortname + "." + app_Constants.DB_FILE_EXT;
            TeamDAO td = new TeamDAO();

            r = td.getTeamSeasonStats(season_id, Franchise_id, League_con_string);

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
    }
}
