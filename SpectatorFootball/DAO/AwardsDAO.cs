using SpectatorFootball.Models;
using SpectatorFootball.PlayerNS;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Data;
using System.Data.SQLite;
using SpectatorFootball.Team;
using System.Data.Entity;

namespace SpectatorFootball.DAO
{
    public class AwardsDAO
    {
        public List<Two_Coll_List> getPlayerAwards(long player_id, string league_filepath)
        {
            List<Two_Coll_List> r = null;

            string con = Common.LeageConnection.Connect(league_filepath);

            using (var context = new leagueContext(con))
            {

                r = context.Player_Awards.Where(x => x.Player_ID == player_id).OrderBy(x => x.Season.Year)
                    .Select(x => new Two_Coll_List
                    {
                        col1 = x.Season.Year.ToString(),
                        col2 = x.Award.Name
                    }).ToList() ;  

            }
            return r;
        }
    }
}
