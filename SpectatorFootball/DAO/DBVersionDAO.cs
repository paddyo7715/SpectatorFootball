using SpectatorFootball.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.DAO
{
    public class DBVersionDAO
    {
        public DBVersion getLatestDBVersion(string league_filepath)
        {
            DBVersion r = new DBVersion();

            string con = Common.LeageConnection.Connect(league_filepath);
            using (var context = new leagueContext(con))
            {
                r = context.DBVersions.OrderByDescending(x => x.ID).First();       
            }

            return r;
        }
    }
}
