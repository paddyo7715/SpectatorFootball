using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using log4net;
using SpectatorFootball.Models;
using SpectatorFootball.League;
using System.Linq;
using System.Data.SqlClient;
using System.Data.Entity;

namespace SpectatorFootball
{
    public class LeagueDAO
    {
        private static ILog logger = LogManager.GetLogger("RollingFile");


        public void Create_New_League(New_League_Structure Mem_League, string newleague_filepath)
        {

            //Note that filepath will be converted to just the filename because these files have 
            //already been copied to the appropriate location under the league folder.
             string con = Common.LeageConnection.Connect(newleague_filepath);
             using (var context = new leagueContext(con))
             {
                context.Seasons.Add(Mem_League.Season);
                context.Franchises.AddRange(Mem_League.Franchises);
                context.DBVersions.Add(Mem_League.DBVersion);
                context.Players.AddRange(Mem_League.Players);

                context.SaveChanges();
            }

              logger.Info("League successfuly saved to database.");
        }


        public List<int> GetTeamRankings_byID(long year, string league_filepath)
        {
            List<int> r = null;

            string con = Common.LeageConnection.Connect(league_filepath);
            string sSQL = @"SELECT T.Franchise_ID as Franchise_ID, SUM(T.points) as Points,SUM(T.Points_Diff) as Points_Diff from
                            (select g.id,Home_Team_Franchise_ID as Franchise_ID, case when Week< 1000 and
                                 Home_Score > Away_Score THEN 10 * 2
                                 when Week< 1000 and
                                 Home_Score == Away_Score THEN 10 * 1.5
                                 when Week< 1000 and
                                 Home_Score < Away_Score THEN 10 * 1
                                 when Week >= 1000 and
                                 Home_Score > Away_Score THEN Week *2
                                 when Week >= 1000 and
                                 Home_Score == Away_Score THEN Week *1.5
                                 when Week >= 1000 and
                                 Home_Score < Away_Score THEN Week *1
                                 ELSE 0 END as points, Home_Score - Away_Score AS Points_Diff FROM Game g, Season s 
                            WHERE g.SEASON_ID = s.ID AND S.YEAR = @year
							UNION
                            select g.id,Away_Team_Franchise_ID as Franchise_ID, case when Week< 1000 and
                                Home_Score > Away_Score THEN 10 * 1
                                 when Week< 1000 and
                                Home_Score == Away_Score THEN 10 * 1.5
                                 when Week< 1000 and
                                Home_Score < Away_Score  THEN 10 * 2
                            when Week >= 1000 and
                                Home_Score > Away_Score  THEN Week *1
                                 when Week >= 1000 and
                                Home_Score == Away_Score  THEN Week *1.5
                                 when Week >= 1000 and
                                Home_Score < Away_Score   THEN Week *2
                                 ELSE 0 END AS points, Away_Score - Home_Score AS Points_Diff from Game g, Season s 
                            WHERE g.SEASON_ID = s.ID AND S.YEAR = @year) as T
                        Group by T.Franchise_ID
                        Order by Points, Points_Diff, random(); ";
            

            using (var context = new leagueContext(con))
            {
                var teamRankings = context.Database.SqlQuery<int>(sSQL, new SqlParameter("@year", year)).ToList();
                r = teamRankings;
            }
            return r;
        }

        public List<int> GetPlayoffTeamRankings_byID(long year, string league_filepath)
        {
            List<int> r = null;

            string con = Common.LeageConnection.Connect(league_filepath);
            string sSQL = @"SELECT T.Franchise_ID as Franchise_ID, SUM(T.points) as Points,SUM(T.Points_Diff) as Points_Diff from
                            (select g.id, Home_Team_Franchise_ID as Franchise_ID, case 
                                 when Week >= 1000 and
                                 Home_Score > Away_Score THEN Week *2
                                 when Week >= 1000 and
                                 Home_Score == Away_Score THEN Week *1.5
                                 when Week >= 1000 and
                                 Home_Score < Away_Score THEN Week *1
                                 ELSE 0 END as points, Home_Score - Away_Score AS Points_Diff FROM Game g, Season s 
								WHERE g.SEASON_ID = s.ID AND S.YEAR = @year and week >= 1000
							union	
                            select g.id, Away_Team_Franchise_ID as Franchise_ID, case 
                                when Week >= 1000 and
                                Home_Score > Away_Score  THEN Week *1
                                 when Week >= 1000 and
                                Home_Score == Away_Score  THEN Week *1.5
                                 when Week >= 1000 and
                                Home_Score < Away_Score   THEN Week *2
                                 ELSE 0 END AS points, Away_Score - Home_Score AS Points_Diff from Game g, Season s 
                            WHERE g.SEASON_ID = s.ID AND S.YEAR = @year and week >= 1000) as T
                            Group by T.Franchise_ID
                            Order by Points, Points_Diff, random(); 
";


            using (var context = new leagueContext(con))
            {
                var teamRankings = context.Database.SqlQuery<int>(sSQL, new SqlParameter("@year", year)).ToList();
                r = teamRankings;
            }
            return r;
        }

        public List<Standings_Row> getStandings(long season_id, string league_filepath)

        {
            List<Standings_Row> r = null;

            string con = Common.LeageConnection.Connect(league_filepath);
            //This SQL statement returns the complete standings.  This could not have been doen in linq
            //Note:  for some reason the streak char does not return.  I'm leaving the sql part for the
            //string char in this sql statement, in the hope that some day it will work.
            //The issue is not EF6, as I originally thought.  The issue seems to be the sqlite provider
            //for .net.
            //I need to add the streak char to the list of standing_row objects in another method.

            string sSQL = @"select ((T.Team_Slot-1) / (LS.num_teams / LS.Number_of_Divisions)) + 1 as Div_Num, T.id as Team_ID, T.Franchise_ID as Franchise_ID,
                            T.Helmet_Image_File as Helmet_img, T.City || ' ' || T.Nickname as Team_Name,
                            case when clinch = 2 then 'x' when clinch = 1 then 'y' else ' ' end as clinch_char,
                            wins, loses, ties, 
                            case when wins + loses = 0 then 0 else ((wins * 1000) + (ties * 500)) / (wins+loses) end as winpct,
                            pointsfor, pointagainst, 
                            case when streak / 1000 = 1 then 'W' || (streak % 1000)
	                             when streak / 1000 = 2 then 'L' || (streak % 1000)
	                             when streak / 1000 = 3 then 'T' || (streak % 1000) 
                                 else '' end as Streakchar
                            from
                            (select franchise_id, sum(clinch) as clinch, sum(wins) as wins, sum(loses) as loses,
                            sum(ties) as ties, sum(home_score) as pointsfor, sum(away_sore) as pointagainst, sum(streak) as streak from
                            (select franchise_id,0 as clinch, 0 as wins, 0 as loses, 0 as ties, 0 as home_score, 0 as away_sore, 0 as streak from Teams_by_Season where season_id = @Season_ID
                            union
                            select home_team_franchise_id, 0 as clinch,
                            case when home_score > away_score then 1 else 0 end as wins,
                            case when home_score < away_score then 1 else 0 end as loses,
                            case when home_score = away_score then 1 else 0 end as ties,
                            home_score,
                            away_score, 0
                            from game where season_id = @Season_ID and game_done = 1 and week < 1000
                            union
                            select away_team_franchise_id, 0 as clinch,
                            case when away_score > home_score then 1 else 0 end as wins,
                            case when away_score < home_score then 1 else 0 end as loses,
                            case when away_score = home_score then 1 else 0 end as ties,
                            away_score,
                            home_score, 0
                            from game where season_id = @Season_ID and game_done = 1  and week < 1000
                            union
                            select franchise_id, case when rank <= (select number_of_divisions from League_Structure_by_Season where season_id = @Season_ID) THEN 2 else 1 end, 0, 0, 0, 0, 0,0 from Playoff_Teams_by_Season
                            where season_id = @Season_ID
                            union
                            select GG.franchise_id, 0, 0, 0, 0, 0, 0, case when result = 'W' then 1000 + Games when result = 'L' then 2000 + games else 3000 + games end as streak from
                                        (SELECT franchise_id,Result, 
                                            week, 
                                            COUNT(*) as Games
                                        FROM
                                        (SELECT franchise_id, week, Result, 
                                            (SELECT COUNT(*) 
                                            FROM 
                                            (
                                        select home_team_franchise_id as franchise_id, week, case WHEN Home_Score > Away_Score THEN 'W' WHEN Home_Score < Away_Score THEN 'L' ELSE 'T' END as Result from Game
                                        where game_done = 1 and week < 1000
                                        union
                                        select away_team_franchise_id, week, case WHEN Away_Score > Home_Score THEN 'W' WHEN Away_Score < Home_Score THEN 'L' ELSE 'T' END as Result from Game 
                                        where game_done = 1 and week < 1000
                                        order by franchise_id, week
                                            ) G 
                                            WHERE G.franchise_id = GR.franchise_id and G.Result <> GR.Result 
                                            AND G.week <= GR.week) as RunGroup 
                                        FROM 
                                        (
                                        select home_team_franchise_id as franchise_id, week, case WHEN Home_Score > Away_Score THEN 'W' WHEN Home_Score < Away_Score THEN 'L' ELSE 'T' END as Result from Game
                                        where game_done = 1 and week < 1000
                                        union
                                        select away_team_franchise_id, week, case WHEN Away_Score > Home_Score THEN 'W' WHEN Away_Score < Home_Score THEN 'L' ELSE 'T' END as Result from Game 
                                        where game_done = 1 and week < 1000
                                        order by franchise_id, week
                                        ) GR)
                                        GROUP BY franchise_id, Result, RunGroup
                                        ORDER BY franchise_id, week) GG,
                            (SELECT FRANCHISE_ID as franchise_id, MAX(week) as week FROM
                            (
                            (select home_team_franchise_id as franchise_id, max(week) as week from game where week < 1000 and season_id = @Season_ID group by 
                            home_team_franchise_id 
                            union
                            select away_team_franchise_id as franchise_id, max(week) as week from game where week < 1000 and season_id = @Season_ID group by 
                            away_team_franchise_id) 
                            )
                            GROUP BY FRANCHISE_ID) as g2
                            where GG.franchise_id = g2.franchise_id and
	                                GG.week = g2.week)
                            group by franchise_id) A, Teams_by_Season T, League_Structure_by_Season LS
                            where A.franchise_id = T.franchise_id and
                                  T.season_id = LS.season_id and
	                              T.season_id = @Season_ID 
                            order by Div_Num,(wins*2) + ties - (loses*2) desc,pointsfor-pointagainst  desc,random() desc;";

            sSQL = sSQL.Replace("@Season_ID", season_id + "");


            using (var context = new leagueContext(con))
            {
                context.Configuration.AutoDetectChangesEnabled = false;
                r = context.Database.SqlQuery<Standings_Row>(sSQL).ToList();
            }

            return r;
        }

        public List<Standing_Streak> getStandingsStreak(long season_id, string league_filepath)

        {
            List<Standing_Streak> r = null;

            string con = Common.LeageConnection.Connect(league_filepath);

            string sSQL = @"
                        SELECT ID as team_id, Result,
                          week,
                          COUNT(*) as Games
                        FROM
                        (SELECT franchise_id, week, Result,
                          (SELECT COUNT(*)
                           FROM
                           (
                        select home_team_franchise_id as franchise_id, week, case WHEN Home_Score > Away_Score THEN 'W' 
                        WHEN Home_Score < Away_Score THEN 'L' ELSE 'T' END as Result from Game
                        where game_done = 1 and week < 1000
                        union
                        select away_team_franchise_id, week, case WHEN Away_Score > Home_Score THEN 'W' WHEN Away_Score < 
                        Home_Score THEN 'L' ELSE 'T' END as Result from Game
                        where game_done = 1 and week < 1000
                        order by franchise_id, week
                           ) G
                           WHERE G.franchise_id = GR.franchise_id and G.Result <> GR.Result
                           AND G.week <= GR.week) as RunGroup
                        FROM
                        (
                        select home_team_franchise_id as franchise_id, week, case WHEN Home_Score > Away_Score THEN 'W' 
                        WHEN Home_Score < Away_Score THEN 'L' ELSE 'T' END as Result from Game
                        where game_done = 1 and week < 1000
                        union
                        select away_team_franchise_id, week, case WHEN Away_Score > Home_Score THEN 'W' WHEN Away_Score < 
                        Home_Score THEN 'L' ELSE 'T' END as Result from Game
                        where game_done = 1 and week < 1000
                        order by franchise_id, week
                        ) GR) xx, teams_by_season tbs
                        where season_id = @Season_ID and xx.franchise_id = tbs.franchise_id
                        GROUP BY team_id, Result, RunGroup
                        ORDER BY team_id, week desc
                        ";

            sSQL = sSQL.Replace("@Season_ID", season_id + "");


            SQLiteConnection ObjConnection = new SQLiteConnection("Data Source=C:\\Users\\Brenden\\Documents\\Spect_Football_Data\\APFL\\APFL.db;");

            using (var context = new leagueContext(con))
            {
                context.Configuration.AutoDetectChangesEnabled = false;
                r = context.Database.SqlQuery<Standing_Streak>(sSQL).ToList();
            }

            return r;
        }

        public Season LoadSeason(string year, string newleague_filepath)
        {
            Season s = new Season();

            //if year is null then that means to load the current year.
            string con = Common.LeageConnection.Connect(newleague_filepath);
            using (var context = new leagueContext(con))
            {
                if (year == null)
                    s = context.Seasons.OrderByDescending(x => x.Year).Include(b => b.League_Structure_by_Season).Include(b => b.Teams_by_Season).Include(c => c.Conferences).Include(d => d.Divisions).FirstOrDefault();
                else
                {
                    long lYear = long.Parse(year);
                    s = context.Seasons.Where(x => x.Year == lYear).Include(b => b.League_Structure_by_Season).Include(b => b.Teams_by_Season).Include(c => c.Conferences).Include(d => d.Divisions).FirstOrDefault();
                }
            }

            logger.Info("Season Successfully Loaded.");

            return s;
        }

        public int[] getSeasonTableTotals(long season_id, string newleague_filepath)
        {
            int draft_not_done = 0;
            int draft_started = 0;
            int free_agency_started = 0;
            int teams_lt_tcamp_players = 0;
            int teamsnotFull_Count = 0;
            int training_camp_Started = 0;
            int Regualar_Season_Started = 0;
            int Regualar_Season_done = 0;
            int Playoffs_Started = 0;
            int Champ_game_played = 0;
            int player_awards_done = 0;

            string con = Common.LeageConnection.Connect(newleague_filepath);
            using (var context = new leagueContext(con))
            {
                bool bdnotdone = context.Drafts.Any(x => x.Season_ID == season_id && x.Player_ID == null);
                if (bdnotdone)
                    draft_not_done = 1;
                else
                    draft_not_done = 0;

                bool bdStarted = context.Drafts.Any(x => x.Season_ID == season_id && x.Player_ID != null);
                if (bdStarted)
                    draft_started = 1;
                else
                    draft_started = 0;

                bool btfa = context.Free_Agency.Any(x => x.Season_ID == season_id);
                if (btfa)
                    free_agency_started = 1;
                else
                    free_agency_started = 0;

                bool batlttccount = context.Teams_by_Season.Any(x => x.Season_ID == season_id && x.Franchise.Players_By_Team.Count() < app_Constants.TRAINING_CAMP_TEAM_PLAYER_COUNT);
                if (batlttccount)
                    teams_lt_tcamp_players = 1;
                else
                    teams_lt_tcamp_players = 0;

                teamsnotFull_Count = context.Teams_by_Season.Where(x => x.Season_ID == season_id && x.Franchise.Players_By_Team.Count() != app_Constants.REGULAR_SEASON_TEAM_PLAYER_COUNT).Count();
                bool btcs = context.Training_Camp_by_Season.Any(x => x.Season_ID == season_id);
                if (btcs)
                    training_camp_Started = 1;
                else
                    training_camp_Started = 0;

                bool bRSGP = context.Games.Any(x => x.Season_ID == season_id && x.Week < app_Constants.PLAYOFF_WIDLCARD_WEEK_1 && x.Game_Done == 1);
                if (bRSGP)
                    Regualar_Season_Started = 1;
                else
                    Regualar_Season_Started = 0;

                bool bRSGP2 = context.Games.Any(x => x.Season_ID == season_id && x.Week < app_Constants.PLAYOFF_WIDLCARD_WEEK_1 && x.Game_Done != 1);
                if (bRSGP2)
                    Regualar_Season_done = 0;
                else
                    Regualar_Season_done = 1;

                bool bPlayoffs = context.Games.Any(x => x.Season_ID == season_id && x.Week >= app_Constants.PLAYOFF_WIDLCARD_WEEK_1);
                if (bPlayoffs)
                    Playoffs_Started = 1;
                else
                    Playoffs_Started = 0;

                Champ_game_played = context.Games.Where(x => x.Season_ID == season_id && x.Championship_Game == 1 && x.Game_Done == 1).Count();
                bool bAwards = context.Player_Awards.Any(x => x.Season_ID == season_id);
                if (bAwards)
                    player_awards_done = 1;
                else
                    player_awards_done = 0;
            }

            return new int[] { draft_not_done, draft_started, free_agency_started, teams_lt_tcamp_players, teamsnotFull_Count, training_camp_Started, Regualar_Season_Started, Regualar_Season_done, Playoffs_Started, Champ_game_played, player_awards_done };

        }
        public List<Season> getAllSeasons(string league_filepath)
        {
            List<Season> r = new List<Season>();

            string con = Common.LeageConnection.Connect(league_filepath);
            using (var context = new leagueContext(con))
            {
                r = context.Seasons.OrderByDescending(x => x.ID).ToList();
            }

            return r;
        }

        public List<Passing_Accum_Stats_by_year> getLeagueSeasonPassingStats(long season_id, string league_filepath)
        {
            List<Passing_Accum_Stats_by_year> r = new List<Passing_Accum_Stats_by_year>();

            string con = Common.LeageConnection.Connect(league_filepath);
            using (var context = new leagueContext(con))
            {
                r = context.Game_Player_Passing_Stats.Where(x => x.Game.Season_ID == season_id && x.Game.Week < app_Constants.PLAYOFF_WIDLCARD_WEEK_1).GroupBy(x => x.Player)
                .Select(x => new Passing_Accum_Stats_by_year
                {
                    p = x.Key,
                    f_id = x.Key.Players_By_Team.Where(t => t.Season_ID == season_id).Select(t => t.Franchise_ID).Max(),
                    Completes = x.Sum(s => s.Pass_Comp),
                    Ateempts = x.Sum(s => s.Pass_Att),
                    Yards = x.Sum(s => s.Pass_Yards),
                    TDs = x.Sum(s => s.Pass_TDs),
                    Ints = x.Sum(s => s.Pass_Ints),
                    Fumbles = x.Sum(s => s.Fumbles),
                    Fumbles_Lost = x.Sum(s => s.Fumbles_Lost),
                }).ToList();
            }

            return r;
        }

        public List<Rushing_Accum_Stats_by_year> getLeagueSeasonRushingStats(long season_id, string league_filepath)
        {
            List<Rushing_Accum_Stats_by_year> r = new List<Rushing_Accum_Stats_by_year>();

            string con = Common.LeageConnection.Connect(league_filepath);
            using (var context = new leagueContext(con))
            {
                r = context.Game_Player_Rushing_Stats.Where(x => x.Game.Season_ID == season_id && x.Game.Week < app_Constants.PLAYOFF_WIDLCARD_WEEK_1).GroupBy(x => x.Player)
                    .Select(x => new Rushing_Accum_Stats_by_year
                    {
                        p = x.Key,
                        f_id = x.Key.Players_By_Team.Where(t => t.Season_ID == season_id).Select(t => t.Franchise_ID).Max(),
                        Rushes = x.Sum(s => s.Rush_Att),
                        Yards = x.Sum(s => s.Rush_Yards),
                        TDs = x.Sum(s => s.Rush_TDs),
                        Fumbles = x.Sum(s => s.Fumbles),
                        Fumbles_Lost = x.Sum(s => s.Fumbles_Lost)
                    }).ToList();
            }

            return r;
        }

        public List<Receiving_Accum_Stats_by_year> getLeagueSeasonReceivingStats(long season_id, string league_filepath)
        {
            List<Receiving_Accum_Stats_by_year> r = new List<Receiving_Accum_Stats_by_year>();

            string con = Common.LeageConnection.Connect(league_filepath);
            using (var context = new leagueContext(con))
            {
                r = context.Game_Player_Receiving_Stats.Where(x => x.Game.Season_ID == season_id && x.Game.Week < app_Constants.PLAYOFF_WIDLCARD_WEEK_1).GroupBy(x => x.Player)
                    .Select(x => new Receiving_Accum_Stats_by_year
                    {
                        p = x.Key,
                        f_id = x.Key.Players_By_Team.Where(t => t.Season_ID == season_id).Select(t => t.Franchise_ID).Max(),
                        Catches = x.Sum(s => s.Rec_Catches),
                        Yards = x.Sum(s => s.Rec_Yards),
                        TDs = x.Sum(s => s.Rec_TDs),
                        Drops = x.Sum(s => s.Rec_Drops),
                        Fumbles = x.Sum(s => s.Fumbles),
                        Fumbles_Lost = x.Sum(s => s.Fumbles_Lost)
                    }).ToList();
            }

            return r;
        }

        public List<Blocking_Accum_Stats_by_year> getLeagueSeasonBlockingStats(long season_id, string league_filepath)
        {
            List<Blocking_Accum_Stats_by_year> r = new List<Blocking_Accum_Stats_by_year>();

            string con = Common.LeageConnection.Connect(league_filepath);
            using (var context = new leagueContext(con))
            {
                r = context.Game_Player_Offensive_Linemen_Stats.Where(x => x.Game.Season_ID == season_id && x.Game.Week < app_Constants.PLAYOFF_WIDLCARD_WEEK_1).GroupBy(x => x.Player)
                    .Select(x => new Blocking_Accum_Stats_by_year
                    {
                        p = x.Key,
                        f_id = x.Key.Players_By_Team.Where(t => t.Season_ID == season_id).Select(t => t.Franchise_ID).Max(),
                        Plays = x.Sum(s => s.Oline_Plays),
                        Pancakes = x.Sum(s => s.OLine_Pancakes),
                        Sacks_Allowed = x.Sum(s => s.OLine_Sacks_Allowed),
                        Pressures_Allowed = x.Sum(s => s.QB_Pressures_Allowed)
                    }).ToList();
            }

            return r;
        }

        public List<Defense_Accum_Stats_by_year> getLeagueSeasonDefenseStats(long season_id, string league_filepath)
        {
            List<Defense_Accum_Stats_by_year> r = new List<Defense_Accum_Stats_by_year>();

            string con = Common.LeageConnection.Connect(league_filepath);
            using (var context = new leagueContext(con))
            {
                r = context.Game_Player_Defense_Stats.Where(x => x.Game.Season_ID == season_id && x.Game.Week < app_Constants.PLAYOFF_WIDLCARD_WEEK_1).GroupBy(x => x.Player)
                    .Select(x => new Defense_Accum_Stats_by_year
                    {
                        p = x.Key,
                        f_id = x.Key.Players_By_Team.Where(t => t.Season_ID == season_id).Select(t => t.Franchise_ID).Max(),
                        Plays = x.Sum(s => s.Pass_Rushes + s.Pass_Blocks),
                        Tackles = x.Sum(s => s.Def_Tackles),
                        Sacks = x.Sum(s => s.Def_Sacks),
                        Pressures = x.Sum(s => s.QB_Pressures),
                        Run_for_Loss = x.Sum(s => s.Def_Rushing_Loss),
                        Forced_Fumble = x.Sum(s => s.Forced_Fumbles)
                    }).ToList();
            }

            return r;
        }

        public List<Pass_Defense_Accum_Stats_by_year> getLeagueSeasonPassDefenseStats(long season_id, string league_filepath)
        {
            List<Pass_Defense_Accum_Stats_by_year> r = new List<Pass_Defense_Accum_Stats_by_year>();

            string con = Common.LeageConnection.Connect(league_filepath);
            using (var context = new leagueContext(con))
            {
                r = context.Game_Player_Pass_Defense_Stats.Where(x => x.Game.Season_ID == season_id && x.Game.Week < app_Constants.PLAYOFF_WIDLCARD_WEEK_1).GroupBy(x => x.Player)
                    .Select(x => new Pass_Defense_Accum_Stats_by_year
                    {
                        p = x.Key,
                        f_id = x.Key.Players_By_Team.Where(t => t.Season_ID == season_id).Select(t => t.Franchise_ID).Max(),
                        Pass_Defenses = x.Sum(s => s.Def_Pass_Defenses),
                        Ints = x.Sum(s => s.Ints),
                        TDs_Surrendered = x.Sum(s => s.Touchdowns_Surrendered),
                        Forced_Fumble = x.Sum(s => s.Forced_Fumbles)
                    }).ToList();
            }

            return r;
        }

        public List<Kicking_Accum_Stats_by_year> getLeagueSeasonKickerStats(long season_id, string league_filepath)
        {
            List<Kicking_Accum_Stats_by_year> r = new List<Kicking_Accum_Stats_by_year>();

            string con = Common.LeageConnection.Connect(league_filepath);
            using (var context = new leagueContext(con))
            {
                r = context.Game_Player_Kicker_Stats.Where(x => x.Game.Season_ID == season_id && x.Game.Week < app_Constants.PLAYOFF_WIDLCARD_WEEK_1).GroupBy(x => x.Player)
                    .Select(x => new Kicking_Accum_Stats_by_year
                    {
                        p = x.Key,
                        f_id = x.Key.Players_By_Team.Where(t => t.Season_ID == season_id).Select(t => t.Franchise_ID).Max(),
                        FG_ATT = x.Sum(s => s.FG_Att),
                        FG_Made = x.Sum(s => s.FG_Made),
                        FG_Long = x.Max(s => s.FG_Long),
                        XP_ATT = x.Sum(s => s.XP_Att),
                        XP_Made = x.Sum(s => s.XP_Made)
                    }).ToList();
            }

            return r;
        }

        public List<Punting_Accum_Stats_by_year> getLeagueSeasonPunterStats(long season_id, string league_filepath)
        {
            List<Punting_Accum_Stats_by_year> r = new List<Punting_Accum_Stats_by_year>();

            string con = Common.LeageConnection.Connect(league_filepath);
            using (var context = new leagueContext(con))
            {
                r = context.Game_Player_Punter_Stats.Where(x => x.Game.Season_ID == season_id && x.Game.Week < app_Constants.PLAYOFF_WIDLCARD_WEEK_1).GroupBy(x => x.Player)
                    .Select(x => new Punting_Accum_Stats_by_year
                    {
                        p = x.Key,
                        f_id = x.Key.Players_By_Team.Where(t => t.Season_ID == season_id).Select(t => t.Franchise_ID).Max(),
                        Punts = x.Sum(s => s.num_punts),
                        Yards = x.Sum(s => s.Punt_yards),
                        Coffin_Corners = x.Sum(s => s.Punt_killed_num)
                    }).ToList();
            }

            return r;
        }

        public List<KickReturn_Accum_Stats_by_year> getLeagueSeasonKickReturnStats(long season_id, string league_filepath)
        {
            List<KickReturn_Accum_Stats_by_year> r = new List<KickReturn_Accum_Stats_by_year>();

            string con = Common.LeageConnection.Connect(league_filepath);
            using (var context = new leagueContext(con))
            {
                r = context.Game_Player_Kick_Returner_Stats.Where(x => x.Game.Season_ID == season_id && x.Game.Week < app_Constants.PLAYOFF_WIDLCARD_WEEK_1).GroupBy(x => x.Player)
                    .Select(x => new KickReturn_Accum_Stats_by_year
                    {
                        p = x.Key,
                        f_id = x.Key.Players_By_Team.Where(t => t.Season_ID == season_id).Select(t => t.Franchise_ID).Max(),
                        Returns = x.Sum(s => s.Kickoffs_Returned),
                        Yards = x.Sum(s => s.Kickoffs_Returned_Yards),
                        Yards_Long = x.Max(s => s.Kickoff_Return_Yards_Long),
                        TDs = x.Sum(s => s.Kickoffs_Returned_TDs),
                        Fumbles = x.Sum(s => s.Kickoffs_Returned_Fumbles)
                    }).ToList();
            }

            return r;
        }

        public List<PuntReturns_Accum_Stats_by_year> getLeagueSeasonPUntReturnStats(long season_id, string league_filepath)
        {
            List<PuntReturns_Accum_Stats_by_year> r = new List<PuntReturns_Accum_Stats_by_year>();

            string con = Common.LeageConnection.Connect(league_filepath);
            using (var context = new leagueContext(con))
            {
                r = context.Game_Player_Punt_Returner_Stats.Where(x => x.Game.Season_ID == season_id && x.Game.Week < app_Constants.PLAYOFF_WIDLCARD_WEEK_1).GroupBy(x => x.Player)
                    .Select(x => new PuntReturns_Accum_Stats_by_year
                    {
                        p = x.Key,
                        f_id = x.Key.Players_By_Team.Where(t => t.Season_ID == season_id).Select(t => t.Franchise_ID).Max(),
                        Returns = x.Sum(s => s.Punts_Returned),
                        Yards = x.Sum(s => s.Punts_Returned_Yards),
                        Yards_Long = x.Max(s => s.Punt_Returned_Yards_Long),
                        TDs = x.Sum(s => s.Punts_Returned_TDs),
                        Fumbles = x.Sum(s => s.Punts_Returned_Fumbles)
                    }).ToList();
            }

            return r;
        }

        public List<Team_Stat_Rec>[] getLeagueStatsHomeAway(long season_id, string league_filepath)
        {
            List<Team_Stat_Rec>[] r = new List<Team_Stat_Rec>[2];

            string con = Common.LeageConnection.Connect(league_filepath);
            using (var context = new leagueContext(con))
            {
                //Get Home Team Stats
                r[0] = context.Games.Where(x => x.Season_ID == season_id && x.Week < app_Constants.PLAYOFF_WIDLCARD_WEEK_1 && x.Game_Done == 1).GroupBy(x => x.Home_Team_Franchise_ID)
                    .Select(x => new Team_Stat_Rec
                    {
                        f_id = x.Key,
                        Passing_Yards_For = (long)x.Sum(s => s.Home_Passing_Yards),
                        Passing_Yards_Against = (long)x.Sum(s => s.Away_Passing_Yards),
                        Rushing_Yards_For = (long)x.Sum(s => s.Home_Rushing_Yards),
                        Rushing_Yards_Against = (long)x.Sum(s => s.Away_Rushing_Yards),
                        Turnovers_Comm = (long)x.Sum(s => s.Home_Turnovers),
                        Turnovers_Recv = (long)x.Sum(s => s.Away_Turnovers),
                        Third_Down_Conversions = (long)x.Sum(s => s.Home_ThirdDown_Conversions),
                        Third_Down_Conversions_Att = (long)x.Sum(s => s.Home_ThirdDowns),
                        Fourth_Down_Conversions = (long)x.Sum(s => s.Home_FourthDown_Conversions),
                        Fourth_Down_Conversions_Att = (long)x.Sum(s => s.Home_FourthDowns),
                        Sacks_For = (long)x.Sum(s => s.Home_Sacks),
                        Sacks_Against = (long)x.Sum(s => s.Away_Sacks)
                    }).ToList();

                //Get Away Team Stats
                r[1] = context.Games.Where(x => x.Season_ID == season_id && x.Week < app_Constants.PLAYOFF_WIDLCARD_WEEK_1 && x.Game_Done == 1).GroupBy(x => x.Home_Team_Franchise_ID)
                    .Select(x => new Team_Stat_Rec
                    {
                        f_id = x.Key,
                        Passing_Yards_For = (long)x.Sum(s => s.Away_Passing_Yards),
                        Passing_Yards_Against = (long)x.Sum(s => s.Home_Passing_Yards),
                        Rushing_Yards_For = (long)x.Sum(s => s.Away_Rushing_Yards),
                        Rushing_Yards_Against = (long)x.Sum(s => s.Home_Rushing_Yards),
                        Turnovers_Comm = (long)x.Sum(s => s.Away_Turnovers),
                        Turnovers_Recv = (long)x.Sum(s => s.Home_Turnovers),
                        Third_Down_Conversions = (long)x.Sum(s => s.Away_ThirdDown_Conversions),
                        Third_Down_Conversions_Att = (long)x.Sum(s => s.Away_ThirdDowns),
                        Fourth_Down_Conversions = (long)x.Sum(s => s.Away_FourthDown_Conversions),
                        Fourth_Down_Conversions_Att = (long)x.Sum(s => s.Away_FourthDowns),
                        Sacks_For = (long)x.Sum(s => s.Away_Sacks),
                        Sacks_Against = (long)x.Sum(s => s.Home_Sacks)
                    }).ToList();
            }

            return r;
        }
    }
}


