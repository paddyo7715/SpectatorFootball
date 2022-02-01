using SpectatorFootball.League;
using SpectatorFootball.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.Playoffs
{
    class Playoff_Helper
    {
        public static int NumPlayoffWeeks(long playoffTeams)
        {
            int r = 0;

            if (playoffTeams < 2 || playoffTeams > app_Constants.MAX_PLAYOFF_TEAMS)
                throw new Exception("Invalid Number of Playoff Teams");

            if (playoffTeams == 2)
                r = 1;
            else if (playoffTeams <= 4)
                r = 2;
            else if (playoffTeams <= 8)
                r = 3;
            else if (playoffTeams <= 16)
                r = 4;
            else if (playoffTeams <= 32)
                r = 5;
            else if (playoffTeams <= 64)
                r = 6;

            return r;
        }

        public static string GetPlayoffGameName(long Week, long conferences, int Num_Full_Playoff_Weeks, string champGame)
        {
            string r = null;

            if (Week < app_Constants.PLAYOFF_WIDLCARD_WEEK_2)
            {
                if (Num_Full_Playoff_Weeks < 5)
                    r = "Wildcard";
                else
                    r = "Wildcard 1";
            }
            else if (Week < app_Constants.PLAYOFF_WIDLCARD_WEEK_2)
                r = "Wildcard 2";
            else if (Week < app_Constants.PLAYOFF_WIDLCARD_WEEK_3)
                r = "Wildcard 3";
            else if (Week < app_Constants.PLAYOFF_DIVISIONAL_WEEK)
                r = "Divisional Playoff";
            else if (Week < app_Constants.PLAYOFF_CONFERENCE_WEEK)
            {
                if (conferences == 2)
                    r = "Conference Championship";
                else
                    r = "Quarter Finals";
            }
            else
                r = champGame;

            return r;
        }

        public static List<Playoff_Teams_by_Season> getConferencePlayoffTeams(
            long season_id, long conf_num, List<Team_Wins_Loses_rec> team_wl_recs,
            long playoff_temas_per_conference)
        {
            List<Playoff_Teams_by_Season> r = new List<Playoff_Teams_by_Season>();
            List<Team_Wins_Loses_rec> Div_Winners = new List<Team_Wins_Loses_rec>();
            List<Team_Wins_Loses_rec> All_Other_teams = new List<Team_Wins_Loses_rec>();

            var testvar = team_wl_recs.Where(x => x.Conf_Num == conf_num)
             .OrderByDescending(x => x.winlossRating)
             .ThenByDescending(x => x.pointsfor - x.pointagainst)
             .ThenByDescending(x => x.Random_Number)
             .GroupBy(x => x.Div_Num)
             .OrderBy(x => x.Key);

            foreach (var k in testvar)
            {
                int i = 0;
                foreach (var rec in k)
                {
                    if (i == 0)
                        Div_Winners.Add(rec);
                    else
                        All_Other_teams.Add(rec);
                    i++;
                }
            }

            //sort the div winners list
            Div_Winners = Div_Winners
              .OrderByDescending(x => x.winlossRating)
             .ThenByDescending(x => x.pointsfor - x.pointagainst)
             .ThenByDescending(x => x.Random_Number).ToList();

            //sort all the other conference teams
            All_Other_teams = All_Other_teams
              .OrderByDescending(x => x.winlossRating)
             .ThenByDescending(x => x.pointsfor - x.pointagainst)
             .ThenByDescending(x => x.Random_Number).ToList();

            //Add the division winners as playoff teams.  They will always make the playoffs
            int added_playoff_teams = 0;
            foreach (Team_Wins_Loses_rec t in Div_Winners)
            {
                r.Add(new Playoff_Teams_by_Season()
                {
                    Conf_ID = t.Conf_Num,
                    Franchise_ID = t.Franchise_ID,
                    Rank = added_playoff_teams + 1,
                    Eliminated = 0,
                     Season_ID = season_id
                }
                    );
                added_playoff_teams++;
            }

            //now add the wildcard teams, if any
            foreach (Team_Wins_Loses_rec t in All_Other_teams)
            {
                if (added_playoff_teams < playoff_temas_per_conference)
                {
                    r.Add(new Playoff_Teams_by_Season()
                    {
                        Conf_ID = t.Conf_Num,
                        Franchise_ID = t.Franchise_ID,
                        Rank = added_playoff_teams + 1,
                        Eliminated = 0,
                        Season_ID = season_id
                    });
                }
                else
                    break;

                added_playoff_teams++;
            }

            return r;
        }

        public static List<string> CreateWeeklyPlayoffSchedule(
            List<Playoff_Teams_by_Season> Playoff_Teams,
            long lastWeekVal, long num_divs)
        {
            List<string> r = new List<string>();
            long divs_per_conf = 0;
            long num_confs = 0;
            long teams_still_active = 0;
            List<Playoff_Teams_by_Season> Acive_Teams = null;

            //get number of conferences
            num_confs = Playoff_Teams.Select(x => x.Conf_ID).Distinct().Count();

            //Get number of div per conference
            divs_per_conf = num_divs / num_confs;

            //Get teams still active in the playoffs
            teams_still_active = Playoff_Teams.Where(x => x.Eliminated == 0).Count();

            //Is this the championship game?
            if (teams_still_active == 2)
            {
                //Get Teams that have not been eliminated in the playoffs any conference
                Acive_Teams = Playoff_Teams.Where(x => x.Eliminated == 0).ToList();
                int rNum = CommonUtils.getRandomNum(1, 2);
                string ht, at = null;
                if (rNum == 1)
                {
                    at = Acive_Teams[0].Franchise_ID.ToString();
                    ht = Acive_Teams[1].Franchise_ID.ToString();
                }
                else
                {
                    at = Acive_Teams[1].Franchise_ID.ToString();
                    ht = Acive_Teams[0].Franchise_ID.ToString();
                }

                string s = app_Constants.PLAYOFF_CHAMPIONSHIP_WEEK + "," + at + "," + ht;
                r.Add(s);
            }
            else
            {
                //do the scheduing for each conference
                for (int cn = 1; cn <= num_confs; cn++)
                {
                    List<Playoff_Teams_by_Season> Active_Teams_conf = Playoff_Teams.Where(x => x.Eliminated == 0 && x.Conf_ID == cn).OrderByDescending(x => x.Rank).ToList();
                    bool even_num_teams = teams_still_active % 2 == 0 ? true : false;
                    string sWeek = null;
                    string ht, at;

                    if (teams_still_active == 2)
                        sWeek = app_Constants.PLAYOFF_CONFERENCE_WEEK.ToString();
                    else if (teams_still_active == num_divs)
                        sWeek = app_Constants.PLAYOFF_DIVISIONAL_WEEK.ToString();
                    else
                        sWeek = (lastWeekVal + 1).ToString();

                    int start_team = even_num_teams ? 0 : 1;
                    for (int fac = start_team; fac < Active_Teams_conf.Count(); fac++)
                    {
                        ht = Active_Teams_conf[fac].Franchise_ID.ToString();
                        at = Active_Teams_conf[Active_Teams_conf.Count() - 1 - fac].Franchise_ID.ToString();
                        string s = sWeek + "," + at + "," + ht;
                        r.Add(s);
                    }
                    
                }
            }
 
            return r;
        }

    }



}
