using System;
using System.Data.SQLite;
using System.Data;
using System.Collections.Generic;
using SpectatorFootball.Models;
using System.Linq;
using System.Data.Entity;

namespace SpectatorFootball
{
    public class Player_NamesDAO
    {

        public int AddFirstName(List<Potential_First_Names> FirstNames)
        {

            int r = 0;

            string con = Common.SettingsConnection.Connect();
            using (var context = new settingsContext(con))
            {
               context.Potential_First_Names.AddRange(FirstNames);
               var added = context.ChangeTracker.Entries().Where(x => x.State == EntityState.Added).Count();
                //                context.Database.Log = Console.Write;
                try
                {
                    context.SaveChanges();
                    r = FirstNames.Count;
                }
                catch { }
             }

            return r;
        }

        public int AddLastName(List<Potential_Last_Names> LastNames)
        {

            int r = 0;


            string con = Common.SettingsConnection.Connect();
            using (var context = new settingsContext(con))
            {
                context.Potential_Last_Names.AddRange(LastNames);
                try
                {
                    context.SaveChanges();
                    r = LastNames.Count;
                }
                catch { }
             }


            return r;
        }
        public long getTotalFirstNames()
        {
            long r = 0;

            string con = Common.SettingsConnection.Connect();
            using (var context = new settingsContext(con))
            {
 //               context.Database.Log = Console.Write;
                r = context.Potential_First_Names.Count();
            }
 
            return r;
        }

        public long getTotalLastNames()
        {
            long r = 0;

            string con = Common.SettingsConnection.Connect();
            using (var context = new settingsContext(con))
            {
                r = context.Potential_Last_Names.Count();
            }

            return r;
        }
        public string[] CreatePlayerName()
        {
            string sFirstName = null;
            string sLastName = null;

            string con = Common.SettingsConnection.Connect();
            using (var context = new settingsContext(con))
            {
                 sFirstName = context.Database.SqlQuery<string>("SELECT FirstName FROM POTENTIAL_FIRST_NAMES ORDER BY RANDOM() LIMIT 1;").FirstOrDefault();
                 sLastName = context.Database.SqlQuery<string>("SELECT LastName FROM POTENTIAL_LAST_NAMES ORDER BY RANDOM() LIMIT 1;").FirstOrDefault();
            }

            return new string[] { sFirstName, sLastName };
        }
    }
}
