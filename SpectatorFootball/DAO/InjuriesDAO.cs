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
using SpectatorFootball.Enum;

namespace SpectatorFootball.DAO
{
    public class InjuriesDAO
    {
        private static ILog logger = LogManager.GetLogger("RollingFile");

        public List<Injury> GetTeamInjuredPlayers(long season_id, long franchise_id, string league_filepath)
        {
            List<Injury> r = null;

            string con = Common.LeageConnection.Connect(league_filepath);

            using (var context = new leagueContext(con))
            {
                //                r = context.Injuries.Where(x => x.Season_ID == season_id && x.Player.Franchise_ID == franchise_id).Include(x => x.Player).ToList();
               r = context.Injuries.Where(x => x.Season_ID == season_id && x.Player.Players_By_Team.Any(p => p.Franchise_ID == franchise_id)).Include(x => x.Player).ToList();
            }

            return r;
        }


    }
}
