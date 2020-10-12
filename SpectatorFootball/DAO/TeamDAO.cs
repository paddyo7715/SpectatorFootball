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
namespace SpectatorFootball.DAO
{
    class TeamDAO
    {
        private static ILog logger = LogManager.GetLogger("RollingFile");

        public List<Player> GetTeamPlayers(long season_id, long franchise_id, string league_filepath)
        {
            List<Player> r = null;

            string con = Common.LeageConnection.Connect(league_filepath);

            using (var context = new leagueContext(con))
            {
                r = context.Players.Where(x => x.Franchise_ID == franchise_id && x.Retired == 0).Include(x => x.Player_Ratings).Where(x => x.Player_Ratings.Any(i => i.Season_ID == season_id)).Include(x => x.Drafts).ToList();
            }

            return r;
        }

    }
}
