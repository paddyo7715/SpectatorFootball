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
                var teamRankings = context.Database.SqlQuery<int>("sSQL", new SqlParameter("@year", year)).ToList();
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
                var teamRankings = context.Database.SqlQuery<int>("sSQL", new SqlParameter("@year", year)).ToList();
                r = teamRankings;
            }
            return r;
        }

        public List<Standings_Row> getStandings(int season_id, string league_filepath)
        {
            List<Standings_Row> r = null;

            string con = Common.LeageConnection.Connect(league_filepath);
//This SQL statement returns the complete standings.  This could not have been doen in linq
            string sSQL = @"select (T.Team_Slot / ((LS.num_teams / LS.Number_of_Divisions)+1)) + 1 as Div_Num, T.id as Team_ID,
                            T.City || ' ' || T.Nickname as Team_Name,
                            case when clinch = 2 then 'x' when clinch = 1 then 'y' else ' ' end as clinch_char,
                            wins, loses, ties, 
                            case when wins + loses = 0 then 0 else ((wins * 1000) + (ties * 500)) / (wins+loses) end as winpct,
                            pointsfor, pointagainst, 
                            case when streak / 1000 = 1 then 'W' || (streak % 1000)
	                             when streak / 1000 = 2 then 'L' || (streak % 1000)
	                             when streak / 1000 = 3 then 'T' || (streak % 1000) end as streak_char
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
                            select franchise_id, 0, 0, 0, 0, 0, 0, case when result = 'W' then 1000 + Games when result = 'L' then 2000 + games else 3000 + games end from
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
                            ORDER BY franchise_id, week) GG
                            where week = (select max(week) from Game where week < 1000 and (GG.franchise_id = Home_Team_Franchise_ID or GG.franchise_id = Away_Team_Franchise_ID) ))
                            group by franchise_id) A, Teams_by_Season T, League_Structure_by_Season LS
                            where A.franchise_id = T.franchise_id and
                                  T.season_id = LS.season_id and
	                              T.season_id = @Season_ID 
                            order by Div_Num,winpct desc, wins-loses, random();";


            using (var context = new leagueContext(con))
            {
                List<Standings_Row> standings = context.Database.SqlQuery<Standings_Row>("sSQL").ToList();
                r = standings;
            }
            return r;
        }


        public Loaded_League_Structure Load_League(string year)
        {
            Loaded_League_Structure r = null;



            return r;
        }

    }
}


