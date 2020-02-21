using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using log4net;
using SpectatorFootball.League_Info;
using SpectatorFootball.Models;

namespace SpectatorFootball
{
    public class LeagueDAO
    {
        private static ILog logger = LogManager.GetLogger("RollingFile");


        public void Create_New_League(Mem_League Mem_League, string newleague_filepath)
        {

             string strStage = null;

             strStage = "Getting connection";
             logger.Debug(strStage);

             string con = Common.LeageConnection.Connect(newleague_filepath);
             using (var context = new leagueContext(con))
             {
                    context.Leagues.Add(Mem_League.Leagues);
                    foreach (Conference c in Mem_League.Conferences)
                        context.Conferences.Add(c);

                    foreach (Division d in Mem_League.Divisions)
                        context.Divisions.Add(d);

                    foreach (Game g in Mem_League.Games)
                        context.Games.Add(g);

                    foreach (Team t in Mem_League.Teams)
                        context.Teams.Add(t);

                    foreach (Player p in Mem_League.Players)
                        context.Players.Add(p);

                context.DBVersions.Add(Mem_League.DBVersion[0]);


                    context.SaveChanges();

              }

              logger.Info("League successfuly saved to database.");
            }

        }
    }


