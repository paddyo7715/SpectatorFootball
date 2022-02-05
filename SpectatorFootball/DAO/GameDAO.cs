using SpectatorFootball.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace SpectatorFootball.DAO
{
    class GameDAO
    {
        public Game getGamefromID(long game_id, string league_filepath)
        {
            Game r = null;

            string con = Common.LeageConnection.Connect(league_filepath);

            using (var context = new leagueContext(con))
            {
                r = context.Games.Where(x => x.ID == game_id).First();
            }

            return r;
        }

        public BoxScore getBoxScorefromID(long game_id,long season_id, string league_filepath)
        {
            BoxScore r = null;

            string con = Common.LeageConnection.Connect(league_filepath);

            using (var context = new leagueContext(con))
            {
                r = (from g in context.Games
                                   .Include(x => x.Game_Player_FG_Defense_Stats.Select(s => s.Player ))
                                   .Include(x => x.Game_Player_Kick_Returner_Stats.Select(s => s.Player))
                                   .Include(x => x.Game_Player_Kicker_Stats.Select(s => s.Player))
                                   .Include(x => x.Game_Player_Kickoff_Defenders.Select(s => s.Player))
                                   .Include(x => x.Game_Player_Kickoff_Receiver_Stats.Select(s => s.Player))
                                   .Include(x => x.Game_Player_Offensive_Linemen_Stats.Select(s => s.Player))
                                   .Include(x => x.Game_Player_Pass_Defense_Stats.Select(s => s.Player))
                                   .Include(x => x.Game_Player_Defense_Stats.Select(s => s.Player))
                                   .Include(x => x.Game_Player_Passing_Stats.Select(s => s.Player))
                                   .Include(x => x.Game_Player_Penalty_Stats.Select(s => s.Player))
                                   .Include(x => x.Game_Player_Penalty_Stats.Select(s => s.Player))
                                   .Include(x => x.Game_Player_Punt_Defenders.Select(s => s.Player))
                                   .Include(x => x.Game_Player_Punt_Receiver_Stats.Select(s => s.Player))
                                   .Include(x => x.Game_Player_Punt_Returner_Stats.Select(s => s.Player))
                                   .Include(x => x.Game_Player_Punter_Stats.Select(s => s.Player))
                                   .Include(x => x.Game_Player_Receiving_Stats.Select(s => s.Player))
                                   .Include(x => x.Game_Player_Rushing_Stats.Select(s => s.Player))
                                   .Include(x => x.Game_Scoring_Summary)
                     join at in context.Teams_by_Season
                     on g.Away_Team_Franchise_ID equals at.Franchise_ID
                     join ht in context.Teams_by_Season
                     on g.Home_Team_Franchise_ID equals ht.Franchise_ID
                     where at.Season_ID == season_id && ht.Season_ID == season_id
                     select new BoxScore
                     {
                         Game = g,
                         aTeam = at,
                         hTeam = ht
                     }).First();
            }

            return r;
        }
        public void SaveGame(Game g,List<Injury> lInj, List<Injury_Log> inj_log, List<Playoff_Teams_by_Season> Playoff_Teams, List<Game> Playoff_Schedule, string league_filepath)
        {
            string con = Common.LeageConnection.Connect(league_filepath);

            using (var context = new leagueContext(con))
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    //Save the player record
                    context.Games.Add(g);
                    context.Entry(g).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();

                    if (lInj != null && lInj.Count() > 0)
                    {
                        context.Injuries.AddRange(lInj);
                        context.SaveChanges();
                        context.Injury_Log.AddRange(inj_log);
                        context.SaveChanges();
                    }

                    if (Playoff_Teams != null && Playoff_Teams.Count() > 0)
                    {
                        context.Playoff_Teams_by_Season.AddRange(Playoff_Teams);
                        //if there is only 1 playoff team record, it means that this team lost a game
                        //and the record should be updated in the database.
                        if (Playoff_Teams.Count() == 1)
                            context.Entry(g).State = System.Data.Entity.EntityState.Modified;

                        context.SaveChanges();
                    }

                    if (Playoff_Schedule != null && Playoff_Schedule.Count() > 0)
                    {
                        context.Games.AddRange(Playoff_Schedule);
                        context.SaveChanges();
                    }

                    



                    dbContextTransaction.Commit();
                }
            }

            return;
        }

        public int NumUnplayedRegGames(long season_id, string league_filepath)
        {
            int r = 0;

            string con = Common.LeageConnection.Connect(league_filepath);

            using (var context = new leagueContext(con))
            {
                r = context.Games.Where(x => x.Season_ID == season_id &&
                x.Game_Done == null && x.Week < app_Constants.PLAYOFF_WIDLCARD_WEEK_1).Count();
            }

            return r;
        }

        public bool isGamePlayed(long g_id, string league_filepath)
        {
            bool r = false;

            string con = Common.LeageConnection.Connect(league_filepath);

            using (var context = new leagueContext(con))
            {
                r = context.Games.Any(x => x.ID == g_id && x.Game_Done == 1);
            }

            return r;
        }
        public int NumUnplayedPlayoffGames(long season_id, string league_filepath)
        {
            int r = 0;

            string con = Common.LeageConnection.Connect(league_filepath);

            using (var context = new leagueContext(con))
            {
                r = context.Games.Where(x => x.Season_ID == season_id &&
                x.Game_Done == null && x.Week >= app_Constants.PLAYOFF_WIDLCARD_WEEK_1).Count();
            }

            return r;
        }
    }
}
