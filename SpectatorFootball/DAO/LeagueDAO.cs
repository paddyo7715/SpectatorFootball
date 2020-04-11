using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using log4net;
using SpectatorFootball.Models;
using SpectatorFootball.League;

namespace SpectatorFootball
{
    public class LeagueDAO
    {
        private static ILog logger = LogManager.GetLogger("RollingFile");


        public void Create_New_League(New_League_Structure Mem_League, string newleague_filepath)
        {

            //Note that filepath will be converted to just the filename because these files have 
            //already been copied to the appropriate location under the league folder.
             string strStage = null;

             strStage = "Getting connection";
             logger.Debug(strStage);

             string con = Common.LeageConnection.Connect(newleague_filepath);
             using (var context = new leagueContext(con))
             {
                    context.Leagues.Add(Mem_League.League);
                    context.DBVersions.Add(Mem_League.DBVersion);

                    context.SaveChanges();
            }

              logger.Info("League successfuly saved to database.");
        }
        public Loaded_League_Structure Load_League(string year)
        {
            Loaded_League_Structure r = null;



            return r;
        }

    }
}


