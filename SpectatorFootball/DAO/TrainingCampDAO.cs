using SpectatorFootball.Enum;
using SpectatorFootball.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
                        HelmetImage = null,
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

                r = context.Players.Where(x => x.Retired == 0 && x.Franchise_ID == franchise_id)
                    .Where(x => x.Player_Ratings.Any(i => i.Season_ID == season_id))
                    .Select(x => new Player_and_Ratings_and_Draft
                    {
                        p = x,
                        pr = x.Player_Ratings.Where(w => w.Season_ID == season_id).ToList(),
                        Overall_Grade = 0,
                        d = x.Drafts.Where(w => w.Season_ID == season_id).ToList()
                    }).ToList();



            }

            return r;
        }
    }
}
