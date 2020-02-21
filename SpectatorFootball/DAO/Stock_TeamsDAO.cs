using System.Collections.Generic;
using System;
using System.Data;
using System.IO;
using SpectatorFootball.Models;
using System.Data.Entity;
using System.Linq;

namespace SpectatorFootball
{
    public class Stock_TeamsDAO
    {

        public List<Stock_Teams> getAllStockTeams()
        {
            List<Stock_Teams> r = null;
            string con = Common.SettingsConnection.Connect();
            using (var context = new settingsContext(con))
            {
                context.Database.Log = Console.Write;
                r = context.Stock_Teams.Where(x => true).OrderBy( x => x.City).ThenBy(x => x.Nickname).ToList();

            }

            return r;
        }
        public void AddStockTeam(Stock_Teams st)
        {

            string con = Common.SettingsConnection.Connect();
            using (var context = new settingsContext(con))
            {
                context.Stock_Teams.Add(st);
                context.SaveChanges();
            }

        }
        public void DeleteStockTeam(int t_id)
        {

            var st = new Stock_Teams { ID = t_id };
            string con = Common.SettingsConnection.Connect();
            using (var context = new settingsContext(con))
            {
                context.Stock_Teams.Attach(st);
                context.Stock_Teams.Remove(st);
                context.SaveChanges();
            }

        }
        public void UpdateStockTeam(Stock_Teams st)
        {

            string con = Common.SettingsConnection.Connect();
            using (var context = new settingsContext(con))
            {
                context.Stock_Teams.Add(st);
                context.Entry(st).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }

        }
        public bool DoesTeamAlreadyExist(string City, string Nickname)
        {
            bool r = false;

            string con = Common.SettingsConnection.Connect();
            using (var context = new settingsContext(con))
            {
                var query = from m in context.Stock_Teams
                            where m.City.ToLower() == City.ToLower() && m.Nickname.ToLower() == Nickname.ToLower()
                            select m;

                var count = query.Count();
                if (count > 0) r = true;
            }

            return r;

        }
    }
}
