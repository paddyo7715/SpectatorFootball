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

                    context.SaveChanges();
            }

              logger.Info("League successfuly saved to database.");
        }

        public List<int> GetTeamRankings_byID(long year, string league_filepath)
        {
            List<int> r = null;

            string con = Common.LeageConnection.Connect(league_filepath);
            string sSQL = @"SELECT T.Franchise_ID as Franchise_ID, SUM(T.points) as Points,SUM(T.Points_Diff) as Points_Diff ,random() as random_num from
                            (select Home_Team_Franchise_ID as Franchise_ID, case when Week< 1000 and
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
                                 ELSE 0 END as points, Home_Score - Away_Score AS Points_Diff FROM Game UNION
                            select Away_Team_Franchise_ID as Franchise_ID, case when Week< 1000 and
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
                            Group by T.Franchise_ID, Points_Diff, random_num
                            Order by Points desc, Points_Diff desc, random_num; ";
            

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
            string sSQL = @"SELECT T.Franchise_ID as Franchise_ID, SUM(T.points) as Points,SUM(T.Points_Diff) as Points_Diff ,random() as random_num from
                            (select Home_Team_Franchise_ID as Franchise_ID, case 
                                 when Week >= 1000 and
                                 Home_Score > Away_Score THEN Week *2
                                 when Week >= 1000 and
                                 Home_Score == Away_Score THEN Week *1.5
                                 when Week >= 1000 and
                                 Home_Score < Away_Score THEN Week *1
                                 ELSE 0 END as points, Home_Score - Away_Score AS Points_Diff FROM Game UNION
                            select Away_Team_Franchise_ID as Franchise_ID, case 
                                when Week >= 1000 and
                                Home_Score > Away_Score  THEN Week *1
                                 when Week >= 1000 and
                                Home_Score == Away_Score  THEN Week *1.5
                                 when Week >= 1000 and
                                Home_Score < Away_Score   THEN Week *2
                                 ELSE 0 END AS points, Away_Score - Home_Score AS Points_Diff from Game g, Season s 
                            WHERE g.SEASON_ID = s.ID AND S.YEAR = @year) as T
                            Group by T.Franchise_ID, Points_Diff, random_num
                            Order by Points desc, Points_Diff desc, random_num; ";


            using (var context = new leagueContext(con))
            {
                var teamRankings = context.Database.SqlQuery<int>("sSQL", new SqlParameter("@year", year)).ToList();
                r = teamRankings;
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


