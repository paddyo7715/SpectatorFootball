using SpectatorFootball.Models;
using System;
using System.Data;
using System.Data.SQLite;

namespace SpectatorFootball
{
    public class Player_DAO
    {
        public long AddSinglePlayer(Player p, string league_filepath)
        {
            long r = 0;
            string con = Common.LeageConnection.Connect(league_filepath);

            using (var context = new leagueContext(con))
            {
                //Save the player record
                context.Players.Add(p);
                context.SaveChanges();
            }

            return r;
        }
    }
}
