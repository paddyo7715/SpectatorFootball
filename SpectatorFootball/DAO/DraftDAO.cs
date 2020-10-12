using log4net;
using SpectatorFootball.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.DAO
{
    class DraftDAO
    {
        private static ILog logger = LogManager.GetLogger("RollingFile");

        public void SelectPlayer(Player p, Draft d_selection, string league_filepath)
        {
            List<Player> r = null;

            string con = Common.LeageConnection.Connect(league_filepath);

            using (var context = new leagueContext(con))
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    //Save the player record
                    context.Players.Add(p);
                    context.Entry(p).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();

                    //Save the draft record
                    context.Drafts.Add(d_selection);
                    context.Entry(d_selection).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                }
            }

            return;
        }

    }
}
