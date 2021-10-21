using SpectatorFootball.Enum;
using SpectatorFootball.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.DAO
{
    class TrainingCampDAO
    {
        public List<TrainingCampStatus> getTrainingCampStatuses(long season_id, string league_filepath)
        {

            List<TrainingCampStatus> r = null;

            string con = Common.LeageConnection.Connect(league_filepath);

            using (var context = new leagueContext(con))
            {

                r = context.Teams_by_Season.Where(x => x.Season_ID == season_id)
                    .Select(x => new TrainingCampStatus
                    {
                        helmet_filename = x.Helmet_Image_File,
                        Franchise_ID = x.Franchise_ID,
                        Season_ID = x.Season_ID,
                        Team_Name = x.City + " " + x.Nickname,
                        Status = x.Franchise.Training_Camp_by_Season.Any(y => y.Season_ID == season_id) ? 3 : 1
                    }).ToList();

                return r;
            }
        }

        public List<Player_and_Ratings_and_Draft> getTrainingCampPlayers(long franchise_id, long season_id, string league_filepath)
        {
            List<Player_and_Ratings_and_Draft> r = null;

            string con = Common.LeageConnection.Connect(league_filepath);
   
            using (var context = new leagueContext(con))
            {
//                context.Database.Log = Console.Write;

                r = context.Players_By_Team.Where(x => x.Franchise_ID == franchise_id &&
                    x.Season_ID == season_id)
                    .Include(x => x.Player)
                    .Select(x => new Player_and_Ratings_and_Draft
                    {
                        p = x,
                        pr = x.Player.Player_Ratings.Where(w => w.Season_ID == season_id).ToList(),
                        bJust_Drafted = x.Player.Drafts.Any(u => u.Season_ID == season_id),
                        Overall_Grade = 0
                    }).ToList();


            }

            return r;
        }
        public void updatePlayersandFreeAgency(List<Players_By_Team> make_List, List<Players_By_Team> cut_List, List<Free_Agency> faList,
            List<Training_Camp_by_Season> tc_list, string league_filepath)
        {
            string con = Common.LeageConnection.Connect(league_filepath);

            using (var context = new leagueContext(con))
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    //Edit the player by team record for the players that have made the team
                    foreach (Players_By_Team p in make_List)
                    {
                        p.Player.Player_Ratings = null;
                        context.Players_By_Team.Add(p);
                        context.Entry(p).State = System.Data.Entity.EntityState.Modified;
                    }
                    context.SaveChanges();

                    //Delete the player by team record for the players that were cut
                    foreach (Players_By_Team p in cut_List)
                    {
                        p.Player.Player_Ratings = null;
                        context.Players_By_Team.Add(p);
                        context.Entry(p).State = System.Data.Entity.EntityState.Deleted;
                    }
                    context.SaveChanges();

                    context.Training_Camp_by_Season.AddRange(tc_list);
                    context.SaveChanges();

                    //Save the free agency records
                    context.Free_Agency.AddRange(faList);
                    context.SaveChanges();

                    dbContextTransaction.Commit();
                }
            }
        }
        public List<Player> getPlayersTrainingCampMade(long franchise_id, long season_id, string league_filepath)
        {
            List<Player> r = null;

            string con = Common.LeageConnection.Connect(league_filepath);

            using (var context = new leagueContext(con))
            {

                r = context.Training_Camp_by_Season.Where(x => x.Franchise_ID == franchise_id &&
                x.Season_ID == season_id && x.Made_Team == 1).Select(x => x.Player).ToList();

            }
           
            return r;
        }

        public List<Player> getPlayersTrainingCampCut(long franchise_id, long season_id, string league_filepath)
        {
            List<Player> r = null;

            string con = Common.LeageConnection.Connect(league_filepath);

            using (var context = new leagueContext(con))
            {

                r = context.Training_Camp_by_Season.Where(x => x.Franchise_ID == franchise_id &&
                x.Season_ID == season_id && x.Made_Team == 0).Select(x => x.Player).ToList();

            }

            return r;
        }

    }
}
