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
using System.Collections.Generic;

namespace SpectatorFootball.DAO
{
    class PlayoffsDAO
    {
        public List<Playoff_Teams_by_Season> getNonEliminatedPlayoffsTeams(long season_id, string league_filepath)
        {
            List<Playoff_Teams_by_Season> r = null;

            string con = Common.LeageConnection.Connect(league_filepath);

            using (var context = new leagueContext(con))
            {
                r = context.Playoff_Teams_by_Season.Where(x => x.Season_ID == season_id && x.Eliminated == 0).ToList();
            }

            return r;
        }
        public List<Playoff_Teams_by_Season> getPlayoffsTeams(long season_id, string league_filepath)
        {
            List<Playoff_Teams_by_Season> r = null;

            string con = Common.LeageConnection.Connect(league_filepath);

            using (var context = new leagueContext(con))
            {
                r = context.Playoff_Teams_by_Season.Where(x => x.Season_ID == season_id).ToList();
            }

            return r;
        }
        public List<Game> getPlayoffGames(long season_id, string league_filepath)
        {
            List<Game> r = null;

            string con = Common.LeageConnection.Connect(league_filepath);

            using (var context = new leagueContext(con))
            {
                r = context.Games.Where(x => x.Season_ID == season_id && x.Week >= app_Constants.PLAYOFF_WIDLCARD_WEEK_1)
                    .OrderBy(x => x.ID).ToList();
            }

            return r;
        }
    }
}
