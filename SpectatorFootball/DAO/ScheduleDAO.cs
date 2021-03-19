using SpectatorFootball.Enum;
using SpectatorFootball.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.DAO
{
    class ScheduleDAO
    {
        public List<long> getYearinSched(long season_id, string league_filepath)
        {

            List<long> r = null;

            string con = Common.LeageConnection.Connect(league_filepath);

            using (var context = new leagueContext(con))
            {
                r = context.Games.Where(x => x.Season_ID == season_id)
                    .Select(x => x.Week).Distinct().ToList();
                return r;
            }
        }
        public long getCurrentWeek(long season_id, string league_filepath)
        {

            long r = 0;

            string con = Common.LeageConnection.Connect(league_filepath);

            using (var context = new leagueContext(con))
            {
                r = context.Games.Where(x => x.Season_ID == season_id && (x.Game_Done == 0 || x.Game_Done == null))
                    .OrderBy(x => x.ID)
                    .Select(x => x.Week).FirstOrDefault();
                return r;
            }
        }

        public List<WeeklyScheduleRec> getWeeklySched(long season_id, long Week, string league_filepath)
        {

            List<WeeklyScheduleRec> r;

            string con = Common.LeageConnection.Connect(league_filepath);

            using (var context = new leagueContext(con))
            {
                r = (from g in context.Games
                     join at in context.Teams_by_Season
                     on g.Away_Team_Franchise_ID equals at.Franchise.ID
                     join ht in context.Teams_by_Season
                     on g.Home_Team_Franchise_ID equals ht.Franchise_ID
                     where g.Season_ID == season_id && at.Season_ID == season_id
                     && ht.Season_ID == season_id
                     orderby g.ID
                     select new WeeklyScheduleRec
                     {
                         Game_ID = g.ID,
                         Game_Complete = g.Game_Done == 1 ? true : false,
                         Away_Team_Name = at.City + " " + at.Nickname,
                         Away_helmet_filename = at.Helmet_Image_File,
                         Away_Score = g.Away_Score != null ? g.Away_Score.ToString() : "",
                         Home_Score = g.Home_Score != null ? g.Home_Score.ToString() : "",
                         Home_helmet_filename = ht.Helmet_Image_File,
                         Home_Team_Name = ht.City + " " + ht.Nickname,
                         QTR = g.Quarter,
                         QTR_Time = g.Time
                     }).ToList();
            }

            return r;
        }

    }
}
