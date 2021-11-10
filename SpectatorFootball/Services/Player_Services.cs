using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpectatorFootball;
using SpectatorFootball.Common;
using SpectatorFootball.DAO;
using SpectatorFootball.Enum;
using SpectatorFootball.League;
using SpectatorFootball.Models;
using SpectatorFootball.PlayerNS;
using SpectatorFootball.Team;

namespace SpectatorFootball.Services

{
    class Player_Services
    {
        public Player_Card_Data getPlayerCardData(Player p, Loaded_League_Structure lls)
        {
            string DIRPath_League = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + lls.season.League_Structure_by_Season[0].Short_Name.ToUpper();
            string League_con_string = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + lls.season.League_Structure_by_Season[0].Short_Name.ToUpper() + Path.DirectorySeparatorChar + lls.season.League_Structure_by_Season[0].Short_Name.ToUpper() + "." + app_Constants.DB_FILE_EXT;

            TeamDAO td = new TeamDAO();
            AwardsDAO ad = new AwardsDAO();
            Teams_by_Season t = td.getTeamFromPlayerID(p, lls.season.ID, League_con_string);

            Player_Card_Data r = new Player_Card_Data()
            {
                team = t,
                Player = p
            };
            
            Player_DAO pd = new Player_DAO();
            List<Team_Player_Accum_Stats_by_year> reg_playoff_stats = pd.getPlayerStatsByYear_Reg_Playoff(p.ID, t.Season_ID, League_con_string);
            r.Regular_Season_Stats = reg_playoff_stats[0];
            r.Playoff_Stats = reg_playoff_stats[1];
            List<Long_and_String> TeamsYears = pd.getPlayerTeamsbyYear(p.ID, League_con_string);

            //Fill in Missing Regualr Season Stats Info
            //Add passing complete % and QB rating to passing stats
            foreach (var pp in r.Regular_Season_Stats.Passing_Stats)
            {
                pp.Team = TeamsYears.Where(x => x.l1 == pp.Year).Select(x => x.s1).FirstOrDefault();
                pp.Comp_Percent = Player_Helper.FormatCompPercent(pp.Completes, pp.Ateempts);
                pp.QBR = Player_Helper.CalculateQBR(pp.Completes, pp.Ateempts, pp.Yards, pp.TDs, pp.Ints);
            }

            //Add the yards per carry stat to the rushing stats
            foreach (var s in r.Regular_Season_Stats.Rushing_Stats)
            {
                s.Team = TeamsYears.Where(x => x.l1 == s.Year).Select(x => x.s1).FirstOrDefault();
                s.Yards_Per_Carry = Player_Helper.CalcYardsPerCarry_or_Catch(s.Rushes, s.Yards);
            }
            //Add the yards per catch stat to the receiving stats
            foreach (var s in r.Regular_Season_Stats.Receiving_Stats)
            {
                s.Team = TeamsYears.Where(x => x.l1 == s.Year).Select(x => x.s1).FirstOrDefault();
                s.Yards_Per_Catch = Player_Helper.CalcYardsPerCarry_or_Catch(s.Catches, s.Yards);
            }
            //
            foreach (var s in r.Regular_Season_Stats.Blocking_Stats)
            {
                s.Team = TeamsYears.Where(x => x.l1 == s.Year).Select(x => x.s1).FirstOrDefault();
            }
            //
            foreach (var s in r.Regular_Season_Stats.Defense_Stats)
            {
                s.Team = TeamsYears.Where(x => x.l1 == s.Year).Select(x => x.s1).FirstOrDefault();
            }
            //
            foreach (var s in r.Regular_Season_Stats.Pass_Defense_Stats)
            {
                s.Team = TeamsYears.Where(x => x.l1 == s.Year).Select(x => x.s1).FirstOrDefault();
            }
            //Add the FG percent to kicking stats
            foreach (var s in r.Regular_Season_Stats.Kicking_Stats)
            {
                s.Team = TeamsYears.Where(x => x.l1 == s.Year).Select(x => x.s1).FirstOrDefault();
                s.FG_Percent = Player_Helper.CalcYardsPerCarry_or_Catch(s.FG_Made, s.FG_ATT);
            }
            //Add punt avg to the punting stats
            foreach (var s in r.Regular_Season_Stats.Punting_Stats)
            {
                s.Team = TeamsYears.Where(x => x.l1 == s.Year).Select(x => x.s1).FirstOrDefault();
                s.Punt_AVG = Player_Helper.CalcYardsPerCarry_or_Catch(s.Punts, s.Yards);
            }
            //Add kick return avg to kick return stats
            foreach (var s in r.Regular_Season_Stats.KickRet_Stats)
            {
                s.Team = TeamsYears.Where(x => x.l1 == s.Year).Select(x => x.s1).FirstOrDefault();
                s.Yards_avg = Player_Helper.CalcYardsPerCarry_or_Catch(s.Returns, s.Yards);
            }
            //Add punt return avg to punt return stats
            foreach (var s in r.Regular_Season_Stats.PuntRet_Stats)
            {
                s.Team = TeamsYears.Where(x => x.l1 == s.Year).Select(x => x.s1).FirstOrDefault();
                s.Yards_avg = Player_Helper.CalcYardsPerCarry_or_Catch(s.Returns, s.Yards);
            }
            //Fill in Missing Playoff stats info
            //Add passing complete % and QB rating to passing stats
            foreach (var pp in r.Playoff_Stats.Passing_Stats)
            {
                pp.Team = TeamsYears.Where(x => x.l1 == pp.Year).Select(x => x.s1).FirstOrDefault();
                pp.Comp_Percent = Player_Helper.FormatCompPercent(pp.Completes, pp.Ateempts);
                pp.QBR = Player_Helper.CalculateQBR(pp.Completes, pp.Ateempts, pp.Yards, pp.TDs, pp.Ints);
            }
            //Add the yards per carry stat to the rushing stats
            foreach (var s in r.Playoff_Stats.Rushing_Stats)
            {
                s.Team = TeamsYears.Where(x => x.l1 == s.Year).Select(x => x.s1).FirstOrDefault();
                s.Yards_Per_Carry = Player_Helper.CalcYardsPerCarry_or_Catch(s.Rushes, s.Yards);
            }
            //Add the yards per catch stat to the receiving stats
            foreach (var s in r.Playoff_Stats.Receiving_Stats)
            {
                s.Team = TeamsYears.Where(x => x.l1 == s.Year).Select(x => x.s1).FirstOrDefault();
                s.Yards_Per_Catch = Player_Helper.CalcYardsPerCarry_or_Catch(s.Catches, s.Yards);
            }
            //
            foreach (var s in r.Playoff_Stats.Blocking_Stats)
            {
                s.Team = TeamsYears.Where(x => x.l1 == s.Year).Select(x => x.s1).FirstOrDefault();
            }
            //
            foreach (var s in r.Playoff_Stats.Defense_Stats)
            {
                s.Team = TeamsYears.Where(x => x.l1 == s.Year).Select(x => x.s1).FirstOrDefault();
            }
            //
            foreach (var s in r.Playoff_Stats.Pass_Defense_Stats)
            {
                s.Team = TeamsYears.Where(x => x.l1 == s.Year).Select(x => x.s1).FirstOrDefault();
            }
            //Add the FG percent to kicking stats
            foreach (var s in r.Playoff_Stats.Kicking_Stats)
            {
                s.Team = TeamsYears.Where(x => x.l1 == s.Year).Select(x => x.s1).FirstOrDefault();
                s.FG_Percent = Player_Helper.CalcYardsPerCarry_or_Catch(s.FG_Made, s.FG_ATT);
            }
            //Add punt avg to the punting stats
            foreach (var s in r.Playoff_Stats.Punting_Stats)
            {
                s.Team = TeamsYears.Where(x => x.l1 == s.Year).Select(x => x.s1).FirstOrDefault();
                s.Punt_AVG = Player_Helper.CalcYardsPerCarry_or_Catch(s.Punts, s.Yards);
            }
            //Add kick return avg to kick return stats
            foreach (var s in r.Playoff_Stats.KickRet_Stats)
            {
                s.Team = TeamsYears.Where(x => x.l1 == s.Year).Select(x => x.s1).FirstOrDefault();
                s.Yards_avg = Player_Helper.CalcYardsPerCarry_or_Catch(s.Returns, s.Yards);
            }
            //Add punt return avg to punt return stats
            foreach (var s in r.Playoff_Stats.PuntRet_Stats)
            {
                s.Team = TeamsYears.Where(x => x.l1 == s.Year).Select(x => x.s1).FirstOrDefault();
                s.Yards_avg = Player_Helper.CalcYardsPerCarry_or_Catch(s.Returns, s.Yards);
            }
            List<Two_Coll_List> Award_List = pd.getPlayerAwards(p.ID, t.Season_ID, League_con_string);
            r.Player_Awards = Award_List;
            r.Player_Ratings = pd.getPlayerRatingsAllYears(p.ID, t.Season_ID, League_con_string);

           Draft d = pd.getPlayerDraftRecord(p.ID, t.Season_ID, League_con_string);

            if (d != null)
            {
                r.Draft_Info = d.Season.Year.ToString() + " round " + d.Round.ToString() + " pick # " + d.Pick_Number + " overall";
                r.Draft_Info += " by the " + td.getTeamNamefromFranchiseID(d.Season_ID, d.Franchise_ID, League_con_string);
            }
            else
                r.Draft_Info = "Undrafted";

            r.Awards = ad.getPlayerAwards(p.ID, League_con_string);

            return r;
        }
    }
}
