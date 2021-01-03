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
using SpectatorFootball.Free_Agency;

namespace SpectatorFootball.DAO
{
    class TeamDAO
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

    }
}
