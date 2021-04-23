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
                         AwayCity = at.City,
                         AwayNickname = at.Nickname,
                         AwayCityAbbr = at.City_Abr,
                         HomeCity = ht.City,
                         HomeNickname = ht.Nickname,
                         HomeCityAbbr = ht.City_Abr
                     }).First();
            }

            return r;
        }
        public void SaveGame(Game g, string league_filepath)
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

                    dbContextTransaction.Commit();
                }
            }

            return;
        }
    }
}
