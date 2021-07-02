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
using SpectatorFootball.Free_AgencyNS;
using SpectatorFootball.Team;

namespace SpectatorFootball.DAO
{
    public class TeamDAO
    {
        private static ILog logger = LogManager.GetLogger("RollingFile");

        public List<Player> GetTeamPlayers(long season_id, long? franchise_id, string league_filepath)
        {
            List<Player> r = null;

            string con = Common.LeageConnection.Connect(league_filepath);

            using (var context = new leagueContext(con))
            {
                r = context.Players.Where(x => x.Franchise_ID == franchise_id && x.Retired == 0).Include(x => x.Player_Ratings).Where(x => x.Player_Ratings.Any(i => i.Season_ID == season_id)).Include(x => x.Drafts).ToList();
            }

            return r;
        }

        public List<Pos_and_Count> GetTeamPlayerPosCounts(long season_id, long? franchise_id, string league_filepath)
        {
            List<Pos_and_Count> r = null;

            string con = Common.LeageConnection.Connect(league_filepath);

            using (var context = new leagueContext(con))
            {
                r = context.Players.Where(x => x.Franchise_ID == franchise_id && x.Retired == 0).GroupBy(x => x.Pos).Select(x => new Pos_and_Count { pos = (int)x.Key, pos_count = x.Count() }).ToList();
            }

            return r;
        }

        public Teams_by_Season getTeamfromFranchiseID(long season_id, long franchise_id, string league_filepath)
        {
            Teams_by_Season r = null;

            string con = Common.LeageConnection.Connect(league_filepath);

            using (var context = new leagueContext(con))
            {
                r = context.Teams_by_Season.Where(x => x.Franchise_ID == franchise_id && x.Season_ID == season_id).First();
            }

            return r;
        }

        public List<long> getAllFranchiseIDThisSeason(long season_id, string league_filepath)
        {
            List<long> r = null;

            string con = Common.LeageConnection.Connect(league_filepath);

            using (var context = new leagueContext(con))
            {
                r = context.Teams_by_Season.Where(x => x.Season_ID == season_id).Select(x => x.Franchise_ID).ToList();
            }

            return r;
        }

        public void UpdateTeam(Teams_by_Season t, string league_filepath)
        {

            string con = Common.LeageConnection.Connect(league_filepath);
            using (var context = new leagueContext(con))
            {
                context.Teams_by_Season.Add(t);
                context.Entry(t).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }

        }
        public Team_Player_Accum_Stats getTeamSeasonStats(long season_id, long franchise_id, string league_filepath)
        {
            Team_Player_Accum_Stats r = new Team_Player_Accum_Stats();

            List<Passing_Accum_Stats> PassStats = null;

            string con = Common.LeageConnection.Connect(league_filepath);

            using (var context = new leagueContext(con))
            {
                PassStats = context.Game_Player_Passing_Stats.Where(x => x.Game.Season_ID == season_id &&
                    (x.Game.Home_Team_Franchise_ID == franchise_id || x.Game.Away_Team_Franchise_ID == franchise_id)).GroupBy(x => x.Player)
                    .Select(x => new Passing_Accum_Stats
                    {
                        p = x.Key,
                        Completes = x.Sum(s => s.Pass_Comp),
                        Ateempts = x.Sum(s => s.Pass_Att),
                        Yards = x.Sum(s => s.Pass_Yards),
                        TDs = x.Sum(s => s.Pass_TDs),
                        Ints = x.Sum(s => s.Pass_Ints),
                        Fumbles = x.Sum(s => s.Fumbles),
                        Fumbles_Lost = x.Sum(s => s.Fumbles_Lost)
                    }).ToList();
            }

            r.Passing_Stats = PassStats;

            return r;
        }

    }
}
