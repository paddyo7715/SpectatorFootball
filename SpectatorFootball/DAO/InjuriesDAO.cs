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
               r = context.Injuries.Where(x => x.Season_ID == season_id && x.Player.Players_By_Team.Any(p => p.Franchise_ID == franchise_id)).Include(x => x.Player).ToList();
            }

            return r;
        }

        public List<League_Injuries> GetLeagueInjuredPlayers(long season_id, string league_filepath)
        {
            List<League_Injuries> r = null;

            string con = Common.LeageConnection.Connect(league_filepath);

            using (var context = new leagueContext(con))
            {
                r = (from i in context.Injuries
                     join t in context.Teams_by_Season
                     on i.Franchise equals t.Franchise
                     join p in context.Players
                     on i.Player_ID equals p.ID into Inners
                     from pn in Inners.DefaultIfEmpty()
                     where i.Season_ID == season_id && t.Season_ID == season_id
                     orderby i.Franchise_ID, i.Week
                     select new League_Injuries
                     {
                         helmet_filename = t.Helmet_Image_File,
                         Team_Name = t.City + " " + t.Nickname,
                         Pick_Pos_Name = (pn.Pos + " " + pn.First_Name + " " + pn.Last_Name).Trim(),
                         Player_ID = pn.ID,
                         Injury = i
                     }).ToList();
            }

            return r;
        }


    }
}
