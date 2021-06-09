using log4net;
using SpectatorFootball.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using SpectatorFootball.Enum;

namespace SpectatorFootball.DAO
{
    class DraftDAO
    {
        private static ILog logger = LogManager.GetLogger("RollingFile");

        public void SelectPlayer(Player p, DraftPick dp_selection, string league_filepath)
        {

            string con = Common.LeageConnection.Connect(league_filepath);

            Draft d_selection = new Draft()
            {
                ID = dp_selection.ID,
                Pick_Number = dp_selection.Pick_no,
                Round = dp_selection.Round,
                Season_ID = dp_selection.Season_ID,
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

                    //Save the draft record
                    context.Drafts.Add(d_selection);
                    context.Entry(d_selection).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();

                    dbContextTransaction.Commit();
                }
            }

            return;
        }

        public List<DraftPick> GetDraftList(long season_id, string league_filepath)
        {
            List<DraftPick> r = null;

            string con = Common.LeageConnection.Connect(league_filepath);

            using (var context = new leagueContext(con))
            {
//                context.Database.Log = Console.Write;

                r = (from d in context.Drafts
                        join t in context.Teams_by_Season
                        on d.Franchise equals t.Franchise
                        join p in context.Players
                        on d.Player_ID equals p.ID into Inners
                        from pn in Inners.DefaultIfEmpty()
                        where d.Season_ID == season_id && t.Season_ID == season_id
                        orderby d.Pick_Number
                        select new DraftPick
                        {
                            Pick_no = d.Pick_Number,
                            Round = d.Round,
                            helmet_filename = t.Helmet_Image_File,
                            Team_Name = t.City + " " + t.Nickname,
                            Pick_Pos_Name = (pn.Pos + " " + pn.First_Name + " " + pn.Last_Name).Trim(),
                            Season_ID = d.Season_ID,
                            Franchise_ID = d.Franchise_ID,
                            ID = d.ID,
                            Home_Jersey_Color = t.Home_jersey_Color,
                            Home_Jersey_Number_Color = t.Home_Jersey_Number_Color,
                            Home_Jersey_Outline_Color = t.Home_Jersey_Number_Outline_Color,
                            Helmet_Color = t.Helmet_Color,
                            Helmet_Logo_Color = t.Helmet_Logo_Color
                        }).ToList();
            }

            return r;
        }

        public List<Player> getDraftablePlayers(long season_id, string league_filepath)
        {
            List<Player> r = null;

            string con = Common.LeageConnection.Connect(league_filepath);

            using (var context = new leagueContext(con))
            {
                int iFirstKicker = System.Enum.GetNames(typeof(Player_Pos)).Length - 2;
                r = context.Players.Where(x => x.Player_Ratings.Max(y => y.Season_ID) == season_id).OrderByDescending(x => x.Draft_Grade - ((x.Pos / iFirstKicker) * 100)).Include(i => i.Drafts).ToList();
            }

            return r;
        }

    }
}
