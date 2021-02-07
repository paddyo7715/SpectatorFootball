using SpectatorFootball.Enum;
using SpectatorFootball.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.DAO
{
    class FreeAgencyDAO
    {
        public List<FreeAgencyTrans> GetYearlyFATrans(long season_id, string league_filepath)
        {
            List<FreeAgencyTrans> r = null;

            string con = Common.LeageConnection.Connect(league_filepath);

            using (var context = new leagueContext(con))
            {

                r = (from f in context.Free_Agency
                     join t in context.Teams_by_Season
                     on f.Franchise equals t.Franchise
                     join p in context.Players
                     on f.Player_ID equals p.ID into Inners
                     from pn in Inners.DefaultIfEmpty()
                     where f.Season_ID == season_id && t.Season_ID == season_id
                     orderby f.ID
                     select new FreeAgencyTrans
                     {
                         helmet_filename = t.Helmet_Image_File,
                         Team_Name = t.City + " " + t.Nickname,
                         Pick_Pos_Name = (pn.Pos + " " + pn.First_Name + " " + pn.Last_Name).Trim(),
                         Season_ID = f.Season_ID,
                         Franchise_ID = f.Franchise_ID,
                         ID = f.ID
                     }).ToList();

            }

            return r;
        }

        public List<Player_and_Ratings> getFreeAgents(long season_id, string league_filepath)
        {
            List<Player_and_Ratings> r = null;


            string con = Common.LeageConnection.Connect(league_filepath);

            using (var context = new leagueContext(con))
            {
                int iFirstKicker = System.Enum.GetNames(typeof(Player_Pos)).Length - 2;
                r = context.Players.Where(x => x.Retired == 0 && x.Franchise_ID == null)
                    .Where(x => x.Player_Ratings.Any(i => i.Season_ID == season_id))
                    .Select(x => new Player_and_Ratings
                    {
                        p = x,
                        pr = x.Player_Ratings.Where(w => w.Season_ID == season_id).ToList(),
                        Overall_Grade = 0
                    }).ToList();
            }

            return r;
        }
        public bool isFreeAgencyDone(long season_id, string newleague_filepath)
        {
            bool r = false;

            int teamsLessThanFull_Count = 0;
            int training_camp_count = 0;

            string con = Common.LeageConnection.Connect(newleague_filepath);
            using (var context = new leagueContext(con))
            {
                teamsLessThanFull_Count = context.Teams_by_Season.Where(x => x.Season_ID == season_id && x.Franchise.Players.Count() < app_Constants.TRAINING_CAMP_TEAM_PLAYER_COUNT).Count();
                training_camp_count = context.Training_Camp_by_Season.Where(x => x.Season_ID == season_id).Count();
            }

            if (training_camp_count > 0)
                r = true;
            else if (teamsLessThanFull_Count > 0)
                r = false;
            else
                r = true;

            return r;

        }
        public void SelectPlayer(Player p, FreeAgencyTrans fa_selection, string league_filepath)
        {

            string con = Common.LeageConnection.Connect(league_filepath);

            SpectatorFootball.Models.Free_Agency fa_entity = new SpectatorFootball.Models.Free_Agency()
            {
                Week = app_Constants.FREE_AGENCY_WEEK,
                Signed = 1,
                Season_ID = fa_selection.Season_ID,
                Player_ID = p.ID,
                Franchise_ID = (long)p.Franchise_ID
            };

            using (var context = new leagueContext(con))
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    //Save the player record
                    context.Players.Add(p);
                    context.Entry(p).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();

                    //Save the Free Agency Record
                    context.Free_Agency.Add(fa_entity);
                    context.SaveChanges();

                    dbContextTransaction.Commit();
                }
            }

            return;
        }
    }
}