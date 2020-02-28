using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using log4net;
using SpectatorFootball.League_Info;
using SpectatorFootball.Models;
using SpectatorFootball.League;

namespace SpectatorFootball
{
    public class LeagueDAO
    {
        private static ILog logger = LogManager.GetLogger("RollingFile");


        public void Create_New_League(Mem_League Mem_League, string newleague_filepath)
        {

            //Note that filepath will be converted to just the filename because these files have 
            //already been copied to the appropriate location under the league folder.
             string strStage = null;

             strStage = "Getting connection";
             logger.Debug(strStage);

             string con = Common.LeageConnection.Connect(newleague_filepath);
             using (var context = new leagueContext(con))
             {
                    SpectatorFootball.Models.League Leg = League_Helper.Clone_League(Mem_League.Leagues);
                    Leg.League_Logo_Filepath = Path.GetFileName(Leg.League_Logo_Filepath);
                    context.Leagues.Add(Leg);

                    foreach (Conference c in Mem_League.Conferences)
                        context.Conferences.Add(c);

                    foreach (Division d in Mem_League.Divisions)
                        context.Divisions.Add(d);

                    foreach (Game g in Mem_League.Games)
                        context.Games.Add(g);

                    foreach (Team t in Mem_League.Teams)
                    {
                        Team temp_team = Team_Helper.Clone_Team(t);
                        temp_team.Helmet_img_path = Path.GetFileName(temp_team.Helmet_img_path);
                        temp_team.Stadium_Img_Path = Path.GetFileName(temp_team.Stadium_Img_Path);
                        context.Teams.Add(temp_team);
                    }

                    foreach (Player p in Mem_League.Players)
                        context.Players.Add(p);

                    context.DBVersions.Add(Mem_League.DBVersion[0]);

                    context.SaveChanges();

              }

              logger.Info("League successfuly saved to database.");
            }

        }
    }


