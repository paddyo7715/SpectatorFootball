using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using log4net;
using SpectatorFootball.Models;
using SpectatorFootball.League;
using System.Linq;
using System.Data.SqlClient;
using System.Data.Entity;
using SpectatorFootball.Free_AgencyNS;
using SpectatorFootball.Team;
using SpectatorFootball.Enum;

namespace SpectatorFootball.DAO
{
    public class TeamDAO
    {
        private static ILog logger = LogManager.GetLogger("RollingFile");

        public List<Player> GetTeamPlayers(long season_id, long? franchise_id, string league_filepath)
        {
            List<Player> r = null;

            string con = Common.LeageConnection.Connect(league_filepath);

            using (var context = new leagueContext(con))
            {
                r = context.Players.Where(x => x.Players_By_Team.Any(p => p.Franchise_ID == franchise_id &&
                                     x.Retired == 0 && !x.Injuries.Any(z => z.Player_ID == x.ID) && p.Season_ID == season_id)).Include(x => x.Player_Ratings).Where(x => x.Player_Ratings.Any(i => i.Season_ID == season_id)).Include(x => x.Drafts).ToList();
            }

            return r;
        }

        public List<Pos_and_Count> GetTeamPlayerPosCounts(long season_id, long? franchise_id, string league_filepath)
        {
            List<Pos_and_Count> r = null;

            string con = Common.LeageConnection.Connect(league_filepath);

            using (var context = new leagueContext(con))
            {
                r = context.Players.Where(x => x.Retired == 0 && !x.Injuries.Any(z => z.Player_ID == x.ID) &&
                x.Players_By_Team.Any(w => w.Season_ID == season_id && w.Franchise_ID == franchise_id))
                .GroupBy(x => x.Pos)
                .Select(x => new Pos_and_Count { pos = (int)x.Key, pos_count = x.Count() }).ToList();
            }

            return r;
        }

        public int GetTeamPosCount(long season_id, long? franchise_id,long Pos, string league_filepath)
        {
            int r = 0;

            string con = Common.LeageConnection.Connect(league_filepath);

            using (var context = new leagueContext(con))
            {
                r = context.Players.Where(x => x.Retired == 0 && x.Pos == (int)Pos &&
                x.Players_By_Team.Any(w => w.Season_ID == season_id && w.Franchise_ID == franchise_id) &&
                !x.Injuries.Any(z => z.Player_ID == x.ID))
                .Count();
            }

            return r;
        }

        public Teams_by_Season getTeamfromFranchiseID(long season_id, long franchise_id, string league_filepath)
        {
            Teams_by_Season r = null;

            string con = Common.LeageConnection.Connect(league_filepath);

            using (var context = new leagueContext(con))
            {
                r = context.Teams_by_Season.Where(x => x.Franchise_ID == franchise_id && x.Season_ID == season_id).First();
            }

            return r;
        }
        public string getTeamNamefromFranchiseID(long season_id, long franchise_id, string league_filepath)
        {
            string r = null;

            string con = Common.LeageConnection.Connect(league_filepath);

            using (var context = new leagueContext(con))
            {
                r = context.Teams_by_Season.Where(x => x.Franchise_ID == franchise_id && x.Season_ID == season_id).Select(x => x.Nickname).First();
            }

            return r;
        }

        public List<long> getAllFranchiseIDThisSeason(long season_id, string league_filepath)
        {
            List<long> r = null;

            string con = Common.LeageConnection.Connect(league_filepath);

            using (var context = new leagueContext(con))
            {
                r = context.Teams_by_Season.Where(x => x.Season_ID == season_id).Select(x => x.Franchise_ID).ToList();
            }

            return r;
        }

        public void UpdateTeam(Teams_by_Season t, string league_filepath)
        {

            string con = Common.LeageConnection.Connect(league_filepath);
            using (var context = new leagueContext(con))
            {
//                context.Database.Log = Console.Write;
                context.Teams_by_Season.Add(t);
                context.Entry(t).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }

        }
        public Team_Player_Accum_Stats getTeamSeasonPlayerStats(long season_id, long franchise_id, string league_filepath)
        {
            Team_Player_Accum_Stats r = new Team_Player_Accum_Stats();

            List<Passing_Accum_Stats_by_year> PassStats = null;
            List<Rushing_Accum_Stats_by_year> RushingStats = null;
            List<Receiving_Accum_Stats_by_year> ReceivingStats = null;
            List<Blocking_Accum_Stats_by_year> BlockingStats = null;
            List<Defense_Accum_Stats_by_year> DefenseStats = null;
            List<Pass_Defense_Accum_Stats_by_year> PassDefenseStats = null;
            List<Kicking_Accum_Stats_by_year> KickerStats = null;
            List<Punting_Accum_Stats_by_year> PunterStats = null;
            List<KickReturn_Accum_Stats_by_year> KickoffReturnStats = null;
            List<PuntReturns_Accum_Stats_by_year> PuntReturnStats = null;

            string con = Common.LeageConnection.Connect(league_filepath);

            using (var context = new leagueContext(con))
            {
                PassStats = context.Game_Player_Passing_Stats.Where(x => x.Game.Season_ID == season_id && x.Game.Week < app_Constants.PLAYOFF_WIDLCARD_WEEK_1 &&
                    (x.Game.Home_Team_Franchise_ID == franchise_id || x.Game.Away_Team_Franchise_ID == franchise_id)
                    && x.Player.Players_By_Team.Any(a => a.Franchise_ID == franchise_id && a.Season_ID == season_id)).GroupBy(x => x.Player)
                    .Select(x => new Passing_Accum_Stats_by_year
                    {
                        p = x.Key,
                        Completes = x.Sum(s => s.Pass_Comp),
                        Ateempts = x.Sum(s => s.Pass_Att),
                        Yards = x.Sum(s => s.Pass_Yards),
                        TDs = x.Sum(s => s.Pass_TDs),
                        Ints = x.Sum(s => s.Pass_Ints),
                        Fumbles = x.Sum(s => s.Fumbles),
                        Fumbles_Lost = x.Sum(s => s.Fumbles_Lost),
                    }).OrderByDescending(x => x.Yards).ThenByDescending(x => x.TDs).ToList();

                RushingStats = context.Game_Player_Rushing_Stats.Where(x => x.Game.Season_ID == season_id && x.Game.Week < app_Constants.PLAYOFF_WIDLCARD_WEEK_1 &&
                    (x.Game.Home_Team_Franchise_ID == franchise_id || x.Game.Away_Team_Franchise_ID == franchise_id)
                    && x.Player.Players_By_Team.Any(a => a.Franchise_ID == franchise_id && a.Season_ID == season_id)).GroupBy(x => x.Player)
                    .Select(x => new Rushing_Accum_Stats_by_year
                    {
                        p = x.Key,
                        Rushes = x.Sum(s => s.Rush_Att),
                        Yards = x.Sum(s => s.Rush_Yards),
                        TDs = x.Sum(s => s.Rush_TDs),
                        Fumbles = x.Sum(s => s.Fumbles),
                        Fumbles_Lost = x.Sum(s => s.Fumbles_Lost)
                    }).OrderByDescending(x => x.Yards).ThenByDescending(x => x.TDs).ToList();

                ReceivingStats = context.Game_Player_Receiving_Stats.Where(x => x.Game.Season_ID == season_id && x.Game.Week < app_Constants.PLAYOFF_WIDLCARD_WEEK_1 &&
                    (x.Game.Home_Team_Franchise_ID == franchise_id || x.Game.Away_Team_Franchise_ID == franchise_id)
                    && x.Player.Players_By_Team.Any(a => a.Franchise_ID == franchise_id && a.Season_ID == season_id)).GroupBy(x => x.Player)
                    .Select(x => new Receiving_Accum_Stats_by_year
                    {
                        p = x.Key,
                        Catches = x.Sum(s => s.Rec_Catches),
                        Yards = x.Sum(s => s.Rec_Yards),
                        TDs = x.Sum(s => s.Rec_TDs),
                        Drops = x.Sum(s => s.Rec_Drops),
                        Fumbles = x.Sum(s => s.Fumbles),
                        Fumbles_Lost = x.Sum(s => s.Fumbles_Lost)
                    }).OrderByDescending(x => x.Catches).ThenByDescending(x => x.Yards).ToList();

                BlockingStats = context.Game_Player_Offensive_Linemen_Stats.Where(x => x.Game.Season_ID == season_id && x.Game.Week < app_Constants.PLAYOFF_WIDLCARD_WEEK_1 &&
                    (x.Game.Home_Team_Franchise_ID == franchise_id || x.Game.Away_Team_Franchise_ID == franchise_id)
                    && x.Player.Players_By_Team.Any(a => a.Franchise_ID == franchise_id && a.Season_ID == season_id)).GroupBy(x => x.Player)
                    .Select(x => new Blocking_Accum_Stats_by_year
                    {
                        p = x.Key,
                        Plays = x.Sum(s => s.Oline_Plays),
                        Pancakes = x.Sum(s => s.OLine_Pancakes),
                        Sacks_Allowed = x.Sum(s => s.OLine_Sacks_Allowed),
                        Pressures_Allowed = x.Sum(s => s.QB_Pressures_Allowed)
                    }).OrderByDescending(x => x.Pancakes).ThenByDescending(x => x.Plays).ToList();

                DefenseStats = context.Game_Player_Defense_Stats.Where(x => x.Game.Season_ID == season_id && x.Game.Week < app_Constants.PLAYOFF_WIDLCARD_WEEK_1 &&
                    (x.Game.Home_Team_Franchise_ID == franchise_id || x.Game.Away_Team_Franchise_ID == franchise_id)
                    && x.Player.Players_By_Team.Any(a => a.Franchise_ID == franchise_id && a.Season_ID == season_id)).GroupBy(x => x.Player)
                    .Select(x => new Defense_Accum_Stats_by_year
                    {
                        p = x.Key,
                        Plays = x.Sum(s => s.Pass_Rushes + s.Pass_Blocks),
                        Tackles = x.Sum(s => s.Def_Tackles),
                        Sacks = x.Sum(s => s.Def_Sacks),
                        Pressures = x.Sum(s => s.QB_Pressures),
                        Run_for_Loss = x.Sum(s => s.Def_Rushing_Loss),
                        Forced_Fumble = x.Sum(s => s.Forced_Fumbles)
                    }).OrderByDescending(x => x.Tackles).ThenByDescending(x => x.Sacks).ThenByDescending(x => x.Run_for_Loss).ToList();

                PassDefenseStats = context.Game_Player_Pass_Defense_Stats.Where(x => x.Game.Season_ID == season_id && x.Game.Week < app_Constants.PLAYOFF_WIDLCARD_WEEK_1 &&
                    (x.Game.Home_Team_Franchise_ID == franchise_id || x.Game.Away_Team_Franchise_ID == franchise_id)
                    && x.Player.Players_By_Team.Any(a => a.Franchise_ID == franchise_id && a.Season_ID == season_id)).GroupBy(x => x.Player)
                    .Select(x => new Pass_Defense_Accum_Stats_by_year
                    {
                        p = x.Key,
                        Pass_Defenses = x.Sum(s => s.Def_Pass_Defenses),
                        Ints = x.Sum(s => s.Ints),
                        TDs_Surrendered = x.Sum(s => s.Touchdowns_Surrendered),
                        Forced_Fumble = x.Sum(s => s.Forced_Fumbles)
                    }).OrderByDescending(x => x.Ints).ThenByDescending(x => x.Pass_Defenses).ToList();

                KickerStats = context.Game_Player_Kicker_Stats.Where(x => x.Game.Season_ID == season_id && x.Game.Week < app_Constants.PLAYOFF_WIDLCARD_WEEK_1 &&
                    (x.Game.Home_Team_Franchise_ID == franchise_id || x.Game.Away_Team_Franchise_ID == franchise_id)
                    && x.Player.Players_By_Team.Any(a => a.Franchise_ID == franchise_id && a.Season_ID == season_id)).GroupBy(x => x.Player)
                    .Select(x => new Kicking_Accum_Stats_by_year
                    {
                        p = x.Key,
                        FG_ATT = x.Sum(s => s.FG_Att),
                        FG_Made = x.Sum(s => s.FG_Made),
                        FG_Long = x.Max(s => s.FG_Long),
                        XP_ATT = x.Sum(s => s.XP_Att),
                        XP_Made = x.Sum(s => s.XP_Made)
                    }).OrderByDescending(x => x.FG_Made).ToList();

                PunterStats = context.Game_Player_Punter_Stats.Where(x => x.Game.Season_ID == season_id && x.Game.Week < app_Constants.PLAYOFF_WIDLCARD_WEEK_1 &&
                    (x.Game.Home_Team_Franchise_ID == franchise_id || x.Game.Away_Team_Franchise_ID == franchise_id)
                    && x.Player.Players_By_Team.Any(a => a.Franchise_ID == franchise_id && a.Season_ID == season_id)).GroupBy(x => x.Player)
                    .Select(x => new Punting_Accum_Stats_by_year
                    {
                        p = x.Key,
                        Punts = x.Sum(s => s.num_punts),
                        Yards = x.Sum(s => s.Punt_yards),
                        Coffin_Corners = x.Sum(s => s.Punt_killed_num)
                    }).OrderByDescending(x => x.Punts).ToList();

                KickoffReturnStats = context.Game_Player_Kick_Returner_Stats.Where(x => x.Game.Season_ID == season_id && x.Game.Week < app_Constants.PLAYOFF_WIDLCARD_WEEK_1 &&
                    (x.Game.Home_Team_Franchise_ID == franchise_id || x.Game.Away_Team_Franchise_ID == franchise_id)
                    && x.Player.Players_By_Team.Any(a => a.Franchise_ID == franchise_id && a.Season_ID == season_id)).GroupBy(x => x.Player)
                    .Select(x => new KickReturn_Accum_Stats_by_year
                    {
                        p = x.Key,
                        Returns = x.Sum(s => s.Kickoffs_Returned),
                        Yards = x.Sum(s => s.Kickoffs_Returned_Yards),
                        Yards_Long = x.Max(s => s.Kickoff_Return_Yards_Long),
                        TDs = x.Sum(s => s.Kickoffs_Returned_TDs),
                        Fumbles = x.Sum(s => s.Kickoffs_Returned_Fumbles)
                    }).OrderByDescending(x => x.Returns).ToList();

                PuntReturnStats = context.Game_Player_Punt_Returner_Stats.Where(x => x.Game.Season_ID == season_id && x.Game.Week < app_Constants.PLAYOFF_WIDLCARD_WEEK_1 &&
                    (x.Game.Home_Team_Franchise_ID == franchise_id || x.Game.Away_Team_Franchise_ID == franchise_id)
                    && x.Player.Players_By_Team.Any(a => a.Franchise_ID == franchise_id && a.Season_ID == season_id)).GroupBy(x => x.Player)
                    .Select(x => new PuntReturns_Accum_Stats_by_year
                    {
                        p = x.Key,
                        Returns = x.Sum(s => s.Punts_Returned),
                        Yards = x.Sum(s => s.Punts_Returned_Yards),
                        Yards_Long = x.Max(s => s.Punt_Returned_Yards_Long),
                        TDs = x.Sum(s => s.Punts_Returned_TDs),
                        Fumbles = x.Sum(s => s.Punts_Returned_Fumbles)
                    }).OrderByDescending(x => x.Returns).ToList();

            }

            r.Passing_Stats = PassStats;
            r.Rushing_Stats = RushingStats;
            r.Receiving_Stats = ReceivingStats;
            r.Blocking_Stats = BlockingStats;
            r.Defense_Stats = DefenseStats;
            r.Pass_Defense_Stats = PassDefenseStats;
            r.Kicking_Stats = KickerStats;
            r.Punting_Stats = PunterStats;
            r.KickRet_Stats = KickoffReturnStats;
            r.PuntRet_Stats = PuntReturnStats;

            return r;
        }
        public TeamStatsRaw getTeamStats(long season_id, long franchise_id, string league_filepath)
        {
            string con = Common.LeageConnection.Connect(league_filepath);

            TeamStatsRaw r = null;

            string sSQL = @"select ifnull(SUM(CASE WHEN Home_Team_Franchise_ID = @Franchise_ID then Home_Score else Away_Score end),0) as Team_Points,
                       ifnull(SUM(CASE WHEN Home_Team_Franchise_ID <> @Franchise_ID then Away_Score else Home_Score end),0) as Opp_Points,
                       ifnull(SUM(CASE WHEN Home_Team_Franchise_ID = @Franchise_ID then Home_firstdowns else Away_firstdowns end),0) as Team_First_Downs,
                       ifnull(SUM(CASE WHEN Home_Team_Franchise_ID <> @Franchise_ID then Away_firstdowns else Home_firstdowns end),0) as Opp_First_Downs,
                       ifnull(SUM(CASE WHEN Home_Team_Franchise_ID = @Franchise_ID then Home_thirddown_conversions else Away_thirddown_conversions end),0) as Team_Third_Downs_Made,
                       ifnull(SUM(CASE WHEN Home_Team_Franchise_ID <> @Franchise_ID then Away_thirddown_conversions else Away_thirddown_conversions end),0) as Opp_Third_Downs_Made,
                       ifnull(SUM(CASE WHEN Home_Team_Franchise_ID = @Franchise_ID then Home_thirddowns else Away_thirddowns end),0) as Team_Third_Downs_Att,
                       ifnull(SUM(CASE WHEN Home_Team_Franchise_ID <> @Franchise_ID then Away_thirddowns else Away_thirddowns end),0) as Opp_Third_Downs_Att,
                       ifnull(SUM(CASE WHEN Home_Team_Franchise_ID = @Franchise_ID then Home_fourthdown_conversions else Away_fourthdown_conversions end),0) as Team_fourth_Downs_Made,
                       ifnull(SUM(CASE WHEN Home_Team_Franchise_ID <> @Franchise_ID then Away_fourthdown_conversions else Away_fourthdown_conversions end),0) as Opp_fourth_Downs_Made,
                       ifnull(SUM(CASE WHEN Home_Team_Franchise_ID = @Franchise_ID then Home_fourthdowns else Away_fourthdowns end),0) as Team_fourth_Downs_Att,
                       ifnull(SUM(CASE WHEN Home_Team_Franchise_ID <> @Franchise_ID then Away_fourthdowns else Away_fourthdowns end),0) as Opp_fourth_Downs_Att,
                       ((select ifnull(sum(p.pass_yards),0) from Game g, Game_Player_Passing_Stats p where g.Season_ID = @Season_ID  and  g.Home_Team_Franchise_ID = @Franchise_ID and g.ID = p.Game_ID) +
	                   (select ifnull(sum(p.pass_yards),0) from Game g, Game_Player_Passing_Stats p where g.Season_ID = @Season_ID  and g.Away_Team_Franchise_ID = @Franchise_ID and g.ID = p.Game_ID)) as Team_Passing_Yards,
                       ((select ifnull(sum(p.pass_yards),0) from Game g, Game_Player_Passing_Stats p where g.Season_ID = @Season_ID  and g.Home_Team_Franchise_ID <> @Franchise_ID and g.ID = p.Game_ID) +
	                   (select ifnull(sum(p.pass_yards),0) from Game g, Game_Player_Passing_Stats p where g.Season_ID = @Season_ID  and g.Away_Team_Franchise_ID <> @Franchise_ID and g.ID = p.Game_ID)) as Opp_Passing_Yards,
                        ((select ifnull(sum(p.rush_yards),0) from Game g, Game_Player_Rushing_Stats p where g.Season_ID = @Season_ID  and g.Home_Team_Franchise_ID = @Franchise_ID and g.ID = p.Game_ID) +
	                   (select ifnull(sum(p.rush_yards),0) from Game g, Game_Player_Rushing_Stats p where g.Season_ID = @Season_ID  and g.Away_Team_Franchise_ID = @Franchise_ID and g.ID = p.Game_ID)) as Team_Rushing_Yards,
                       ((select ifnull(sum(p.rush_yards),0) from Game g, Game_Player_Rushing_Stats p where g.Season_ID = @Season_ID  and g.Home_Team_Franchise_ID <> @Franchise_ID and g.ID = p.Game_ID) +
	                   (select ifnull(sum(p.rush_yards),0) from Game g, Game_Player_Rushing_Stats p where g.Season_ID = @Season_ID  and g.Away_Team_Franchise_ID <> @Franchise_ID and g.ID = p.Game_ID)) as Opp_Rushing_Yards,
                         ((select ifnull(sum(p.Def_Sacks),0) from Game g, Game_Player_Defense_Stats p where g.Season_ID = @Season_ID  and g.Home_Team_Franchise_ID = @Franchise_ID and g.ID = p.Game_ID) +
	                   (select ifnull(sum(p.Def_Sacks),0) from Game g, Game_Player_Defense_Stats p where g.Season_ID = @Season_ID  and g.Away_Team_Franchise_ID = @Franchise_ID and g.ID = p.Game_ID)) as Team_Sacks,
                       ((select ifnull(sum(p.Def_Sacks),0) from Game g, Game_Player_Defense_Stats p where g.Season_ID = @Season_ID  and g.Home_Team_Franchise_ID <> @Franchise_ID and g.ID = p.Game_ID) +
	                   (select ifnull(sum(p.Def_Sacks),0) from Game g, Game_Player_Defense_Stats p where g.Season_ID = @Season_ID  and g.Away_Team_Franchise_ID <> @Franchise_ID and g.ID = p.Game_ID)) as Opp_Sacks,
                          ((select ifnull(sum(p.Kickoffs_Returned_Fumbles_Lost),0) from Game g, Game_Player_Kick_Returner_Stats p where g.Season_ID = @Season_ID  and g.Home_Team_Franchise_ID = @Franchise_ID and g.ID = p.Game_ID) +
	                   (select ifnull(sum(p.Kickoffs_Returned_Fumbles_Lost),0) from Game g, Game_Player_Kick_Returner_Stats p where g.Season_ID = @Season_ID  and g.Away_Team_Franchise_ID = @Franchise_ID and g.ID = p.Game_ID) +
	                (select ifnull(sum(p.Fumbles_Lost),0) from Game g, Game_Player_Passing_Stats p where g.Season_ID = @Season_ID  and g.Home_Team_Franchise_ID = @Franchise_ID and g.ID = p.Game_ID) +
	                   (select ifnull(sum(p.Fumbles_Lost),0) from Game g, Game_Player_Passing_Stats p where g.Season_ID = @Season_ID  and g.Away_Team_Franchise_ID = @Franchise_ID and g.ID = p.Game_ID) +
	                (select ifnull(sum(p.Punts_Returned_Fumbles_Lost),0) from Game g, Game_Player_Punt_Returner_Stats p where g.Season_ID = @Season_ID  and g.Home_Team_Franchise_ID = @Franchise_ID and g.ID = p.Game_ID) +
	                   (select ifnull(sum(p.Punts_Returned_Fumbles_Lost),0) from Game g, Game_Player_Punt_Returner_Stats p where g.Season_ID = @Season_ID  and g.Away_Team_Franchise_ID = @Franchise_ID and g.ID = p.Game_ID) +
	                (select ifnull(sum(p.Fumbles_Lost),0) from Game g, Game_Player_Punter_Stats p where g.Season_ID = @Season_ID  and g.Home_Team_Franchise_ID = @Franchise_ID and g.ID = p.Game_ID) +
	                   (select ifnull(sum(p.Fumbles_Lost),0) from Game g, Game_Player_Punter_Stats p where g.Season_ID = @Season_ID  and g.Away_Team_Franchise_ID = @Franchise_ID and g.ID = p.Game_ID) +
	                (select ifnull(sum(p.Fumbles_Lost),0) from Game g, Game_Player_Receiving_Stats p where g.Season_ID = @Season_ID  and g.Home_Team_Franchise_ID = @Franchise_ID and g.ID = p.Game_ID) +
	                   (select ifnull(sum(p.Fumbles_Lost),0) from Game g, Game_Player_Receiving_Stats p where g.Season_ID = @Season_ID  and g.Away_Team_Franchise_ID = @Franchise_ID and g.ID = p.Game_ID) +
	                (select ifnull(sum(p.Fumbles_Lost),0) from Game g, Game_Player_Rushing_Stats p where g.Season_ID = @Season_ID  and g.Home_Team_Franchise_ID = @Franchise_ID and g.ID = p.Game_ID) +
	                   (select ifnull(sum(p.Fumbles_Lost),0) from Game g, Game_Player_Rushing_Stats p where g.Season_ID = @Season_ID  and g.Away_Team_Franchise_ID = @Franchise_ID and g.ID = p.Game_ID)
	                ) as Team_Turnovers,
                          ((select ifnull(sum(p.Kickoffs_Returned_Fumbles_Lost),0) from Game g, Game_Player_Kick_Returner_Stats p where g.Season_ID = @Season_ID  and g.Home_Team_Franchise_ID <> @Franchise_ID and g.ID = p.Game_ID) +
	                   (select ifnull(sum(p.Kickoffs_Returned_Fumbles_Lost),0) from Game g, Game_Player_Kick_Returner_Stats p where g.Season_ID = @Season_ID  and g.Away_Team_Franchise_ID <> @Franchise_ID and g.ID = p.Game_ID) +
	                (select ifnull(sum(p.Fumbles_Lost),0) from Game g, Game_Player_Passing_Stats p where g.Season_ID = @Season_ID  and g.Home_Team_Franchise_ID <> @Franchise_ID and g.ID = p.Game_ID) +
	                   (select ifnull(sum(p.Fumbles_Lost),0) from Game g, Game_Player_Passing_Stats p where g.Season_ID = @Season_ID  and g.Away_Team_Franchise_ID <> @Franchise_ID and g.ID = p.Game_ID) +
	                (select ifnull(sum(p.Punts_Returned_Fumbles_Lost),0) from Game g, Game_Player_Punt_Returner_Stats p where g.Season_ID = @Season_ID  and g.Home_Team_Franchise_ID <> @Franchise_ID and g.ID = p.Game_ID) +
	                   (select ifnull(sum(p.Punts_Returned_Fumbles_Lost),0) from Game g, Game_Player_Punt_Returner_Stats p where g.Season_ID = @Season_ID  and g.Away_Team_Franchise_ID <> @Franchise_ID and g.ID = p.Game_ID) +
	                (select ifnull(sum(p.Fumbles_Lost),0) from Game g, Game_Player_Punter_Stats p where g.Season_ID = @Season_ID  and g.Home_Team_Franchise_ID <> @Franchise_ID and g.ID = p.Game_ID) +
	                   (select ifnull(sum(p.Fumbles_Lost),0) from Game g, Game_Player_Punter_Stats p where g.Season_ID = @Season_ID  and g.Away_Team_Franchise_ID <> @Franchise_ID and g.ID = p.Game_ID) +
	                (select ifnull(sum(p.Fumbles_Lost),0) from Game g, Game_Player_Receiving_Stats p where g.Season_ID = @Season_ID  and g.Home_Team_Franchise_ID <> @Franchise_ID and g.ID = p.Game_ID) +
	                   (select ifnull(sum(p.Fumbles_Lost),0) from Game g, Game_Player_Receiving_Stats p where g.Season_ID = @Season_ID  and g.Away_Team_Franchise_ID <> @Franchise_ID and g.ID = p.Game_ID) +
	                (select ifnull(sum(p.Fumbles_Lost),0) from Game g, Game_Player_Rushing_Stats p where g.Season_ID = @Season_ID  and g.Home_Team_Franchise_ID <> @Franchise_ID and g.ID = p.Game_ID) +
	                   (select ifnull(sum(p.Fumbles_Lost),0) from Game g, Game_Player_Rushing_Stats p where g.Season_ID = @Season_ID  and g.Away_Team_Franchise_ID <> @Franchise_ID and g.ID = p.Game_ID)
	                ) as Opp_Turnovers
                    from Game where Season_ID = @Season_ID  and (Home_Team_Franchise_ID = @Franchise_ID or Away_Team_Franchise_ID = @Franchise_ID) and Week < 1000 and Game_Done = 1; ;";


            sSQL = sSQL.Replace("@Season_ID", season_id + "");
            sSQL = sSQL.Replace("@Franchise_ID", franchise_id + "");


            using (var context = new leagueContext(con))
            {
                context.Configuration.AutoDetectChangesEnabled = false;
                r = context.Database.SqlQuery<TeamStatsRaw>(sSQL).First();
            }

            return r;
        }
        public List<Roster_rec> getTeamRoster(long season_id, long Franchise_id, string league_filepath)
        {
            List<Roster_rec> r = new List<Roster_rec>();
            List<Players_By_Team> pbt_list = null;
            List<Player_Ratings> pr_list = null;

            string con = Common.LeageConnection.Connect(league_filepath);

            using (var context = new leagueContext(con))
            {
                //Special Note
                //Because of a strange issue in sqlite (APPLY joins are not supported)
                //So I had to break it into two queries and then put them together.
                //If I ever update the version of sqlite, I might want to try if I
                //can do this in one query.  To do this, I would get the player, player_ratings 
                //and player_by_team all in the select of one query.

                //                context.Database.Log = Console.Write;
                pbt_list = context.Players_By_Team.Where(x => x.Franchise_ID == Franchise_id && x.Season_ID == season_id).Include(x => x.Player).OrderBy(x => x.Player.Pos).ThenBy(x => x.Player.Last_Name).ThenBy(x => x.Player.First_Name).ToList();
                pr_list = context.Player_Ratings.Where(x => x.Season_ID == season_id && x.Player.Players_By_Team.Any(a => a.Franchise_ID == Franchise_id && a.Season_ID == season_id)).Include(x => x.Player).ToList();

                foreach (Players_By_Team pbt in pbt_list)
                {
                    Player p = pbt.Player;
                    Players_By_Team pt = pbt;
                    Player_Ratings pr = pr_list.Where(x => x.Player_ID == pbt.Player.ID).First();

                    pbt.Player = null;

                    r.Add(new Roster_rec()
                    {
                        p = p,
                        pbt = pt,
                        pr = pr
                    }
                    );
                }
            }
            return r;

        }
        public Teams_by_Season getTeamFromPlayerID(Player p,long season_id, string league_filepath)
        {
            Teams_by_Season r = null;

            string con = Common.LeageConnection.Connect(league_filepath);

            using (var context = new leagueContext(con))
            {
//                r = context.Teams_by_Season.Where(x => x.Franchise.Players_By_Team.Any(c => c.Franchise_ID == x.Franchise_ID && c.Season_ID == season_id)).FirstOrDefault();
                r = context.Teams_by_Season.Where(x => x.Season_ID == season_id && x.Franchise.Players_By_Team.Any(a => a.Player_ID == p.ID && a.Season_ID == season_id) ).FirstOrDefault();
            }
            return r;
        }
        public List<Team_History_Row> getTeamHistory(long f_id, string league_filepath)
        {
            List<Team_History_Row> r = null;

            string con = Common.LeageConnection.Connect(league_filepath);

            using (var context = new leagueContext(con))
            {
                //                context.Database.Log = Console.Write;
                string sSQL = @"
                    select r.Year as Year, cast(sum(reg_wins) as text) as reg_wins, cast(sum(reg_loses) as text) as reg_loses, cast(sum(reg_ties) as text) as reg_ties, cast(sum(reg_PF) as text) as reg_PF, cast(sum(reg_PA) as text) as reg_PA,
					                       cast(sum(play_wins) as text) as play_wins, cast(sum(play_loses) as text) as play_loses, cast(sum(play_PF) as text) as play_PF, cast(sum(play_PA) as text) as play_PA,
					                       sum(champ_PF) as champ_PF, sum(champ_PA) as champ_PA, '' as champ_result
                    from (
                    select Year, 
                      case when Week < @Playoff_Week and ((Home_Team_Franchise_ID = @Franchise_ID and home_score > away_score) or (Away_Team_Franchise_ID = @Franchise_ID and Away_score > home_score)) then 1 else 0 end as reg_wins,
                      case when Week < @Playoff_Week and ((Home_Team_Franchise_ID = @Franchise_ID and home_score < away_score) or (Away_Team_Franchise_ID = @Franchise_ID and Away_score < home_score)) then 1 else 0 end as reg_loses,
                      case when Week < @Playoff_Week and ((Home_Team_Franchise_ID = @Franchise_ID and home_score = away_score) or (Away_Team_Franchise_ID = @Franchise_ID and Away_score = home_score)) then 1 else 0 end as reg_ties,
                      ifnull(case when Week < @Playoff_Week and Home_Team_Franchise_ID = @Franchise_ID then Home_Score 
                           when Week < @Playoff_Week and Away_Team_Franchise_ID = @Franchise_ID then Away_Score else 0 end,0) as reg_PF,
                      ifnull(case when Week < @Playoff_Week and Home_Team_Franchise_ID <> @Franchise_ID then Home_Score 
                           when Week < @Playoff_Week and Away_Team_Franchise_ID <> @Franchise_ID then Away_Score else 0 end,0) as reg_PA,
                      case when Week > @Playoff_Week and ((Home_Team_Franchise_ID = @Franchise_ID and home_score > away_score) or (Away_Team_Franchise_ID = @Franchise_ID and Away_score > home_score)) then 1 else 0 end as play_wins,
                      case when Week > @Playoff_Week and ((Home_Team_Franchise_ID = @Franchise_ID and home_score < away_score) or (Away_Team_Franchise_ID = @Franchise_ID and Away_score < home_score)) then 1 else 0 end as play_loses,
                      ifnull(case when Week > @Playoff_Week and Home_Team_Franchise_ID = @Franchise_ID then Home_Score 
                           when Week > @Playoff_Week and Away_Team_Franchise_ID = @Franchise_ID then Away_Score else 0 end,0) as play_PF,
                      ifnull(case when Week > @Playoff_Week and Home_Team_Franchise_ID <> @Franchise_ID then Home_Score 
                           when Week > @Playoff_Week and Away_Team_Franchise_ID <> @Franchise_ID then Away_Score else 0 end,0) as play_PA,
                      ifnull(case when Week = @Champ_Week and Home_Team_Franchise_ID = @Franchise_ID then Home_Score 
                           when Week = @Champ_Week and Away_Team_Franchise_ID = @Franchise_ID then Away_Score else 0 end,0) as champ_PF,
                      ifnull(case when Week = @Champ_Week and Home_Team_Franchise_ID <> @Franchise_ID then Home_Score 
                           when Week = @Champ_Week and Away_Team_Franchise_ID <> @Franchise_ID then Away_Score else 0 end,0) as champ_PA
                      from game g, season s where g.season_id = s.id and (Home_Team_Franchise_ID = @Franchise_ID or Away_Team_Franchise_ID = @Franchise_ID)
                    ) as r
                    group by r.Year;
                ";
                sSQL = sSQL.Replace("@Franchise_ID", f_id.ToString());
                sSQL = sSQL.Replace("@Playoff_Week", app_Constants.PLAYOFF_WIDLCARD_WEEK_1.ToString());
                sSQL = sSQL.Replace("@Champ_Week", app_Constants.PLAYOFF_CHAMPIONSHIP_WEEK.ToString());

                context.Configuration.AutoDetectChangesEnabled = false;
                r = context.Database.SqlQuery<Team_History_Row>(sSQL).ToList();
            }
            return r;
        }
        public List<OneInt2Strings> getTeamTransactions(long season_id, long f_id, string league_filepath)
        {
            List<OneInt2Strings> r = null;

            string con = Common.LeageConnection.Connect(league_filepath);

            using (var context = new leagueContext(con))
            {
                r = context.Player_Retiring_Log.Where(x => x.Season_ID == season_id && x.Player.Players_By_Team.Any(a => a.Franchise_ID == f_id && a.Season_ID == season_id)).Select(x => new OneInt2Strings { key = app_Constants.PLAYER_RETIRING_RETURNING_WEEK, p = x.Player, Week = "Before Season", Event = ((Player_Pos)x.Player.Pos).ToString() + " " + x.Player.First_Name + " " + x.Player.Last_Name + " has retired", round = 0 })
                    .Union(context.Drafts.Where(x => x.Franchise_ID == f_id && x.Season_ID == season_id).Select(x => new OneInt2Strings { key = app_Constants.DRAFT_WEEK, p = x.Player, Week = "Draft", Event = ((Player_Pos)x.Player.Pos).ToString() + " " + x.Player.First_Name + " " + x.Player.Last_Name + " has been drafted in round " + x.Round.ToString(), round = x.Round }))
                    .Union(context.Free_Agency.Where(x => x.Franchise_ID == f_id && x.Season_ID == season_id).Select(x => new OneInt2Strings { key = x.Week, p = x.Player, Week = x.Week == app_Constants.FREE_AGENCY_WEEK ? "Free Agency Week" : x.Week == app_Constants.TRAINING_CAMP_WEEK ? "Training Camp Complete" : x.Week < 0 ? "Before Regular Season" : x.Week < app_Constants.PLAYOFF_WIDLCARD_WEEK_1 ? "Week " + x.Week : "Playoffs", Event = ((Player_Pos)x.Player.Pos).ToString() + " " + x.Player.First_Name + " " + x.Player.Last_Name + " has been " + (x.Signed == 1 ? "signed" : "released"), round = 0 }))
                    .Union(context.Injury_Log.Where(x => x.Season_ID == season_id && x.Player.Players_By_Team.Any(b => b.Franchise_ID == f_id && b.Season_ID == season_id)).Select(x => new OneInt2Strings { key = (int)x.Week, p = x.Player, Week = x.Week < app_Constants.PLAYOFF_WIDLCARD_WEEK_1 ? "Week " + x.Week : "Playoffs", Event = ((Player_Pos)x.Player.Pos).ToString() + " " + x.Player.First_Name + " " + x.Player.Last_Name + " has " + (x.Injured == 1 ? "been injured for " : "returned from injury"), round = 0 })).OrderBy(x => x.key).ThenBy(x => x.round).ToList();
            }
            return r;
        }
        public List<Teams_by_Season> getAllTeamsSeason(long season_id, string league_filepath)
        {
            List<Teams_by_Season> r = null;

            string con = Common.LeageConnection.Connect(league_filepath);

            using (var context = new leagueContext(con))
            {
                r = context.Teams_by_Season.Where(x => x.Season_ID == season_id).ToList();
            }

            return r;
        }

        public List<Players_By_Team> getTeamPlayersbyTeam(long season_id, long Franchise_id, string league_filepath)
        {
            List<Players_By_Team> r = new List<Players_By_Team>();

            string con = Common.LeageConnection.Connect(league_filepath);

            using (var context = new leagueContext(con))
            {
                r = context.Players_By_Team.Where(x => x.Player.Retired == 0 && !x.Player.Injuries.Any(z => z.Player_ID == x.Player.ID) && x.Franchise_ID == Franchise_id && x.Season_ID == season_id).Include(x => x.Player).OrderBy(x => x.Player.Pos).ThenBy(x => x.Player.Last_Name).ThenBy(x => x.Player.First_Name).ToList();
            }
            return r;

        }

        public List<Player_and_Ratings> getPosPlayers(long season_id,long f_id,int pp, string league_filepath)
        {
            List<Player_and_Ratings> r = null;

            string con = Common.LeageConnection.Connect(league_filepath);

            using (var context = new leagueContext(con))
            {
                int iFirstKicker = System.Enum.GetNames(typeof(Player_Pos)).Length - 2;

                r = context.Players.Where(x => x.Retired == 0 && x.Pos == pp && !x.Injuries.Any(z => z.Player_ID == x.ID) &&
                    x.Players_By_Team.Any(w => w.Season_ID == season_id && w.Franchise_ID == f_id))
                    .Select(x => new Player_and_Ratings
                    {
                        p = x,
                        pr = x.Player_Ratings.Where(w => w.Season_ID == season_id).ToList(),
                        pbt = null,
                        Overall_Grade = 0
                    }).ToList();
            }

            return r;
        }
        public List<Player_and_Ratings> getTeamPlayersforGame(long season_id, long f_id, string league_filepath)
        {
            List<Player_and_Ratings> r = null;

            string con = Common.LeageConnection.Connect(league_filepath);

            using (var context = new leagueContext(con))
            {
                int iFirstKicker = System.Enum.GetNames(typeof(Player_Pos)).Length - 2;

                r = context.Players.Where(x => x.Retired == 0 &&
                    x.Players_By_Team.Any(w => w.Season_ID == season_id && w.Franchise_ID == f_id) &&
                    !x.Injuries.Any(z => z.Player_ID == x.ID))
                    .Select(x => new Player_and_Ratings
                    {
                        p = x,
                        pr = x.Player_Ratings.Where(w => w.Season_ID == season_id).ToList(),
                        pbt = null,
                        Overall_Grade = 0
                    }).ToList();
            }

            return r;
        }

        public List<Franchise> getAllFranchises(string league_filepath)
        {
            List<Franchise> r = null;

            string con = Common.LeageConnection.Connect(league_filepath);

            using (var context = new leagueContext(con))
            {
                r = context.Franchises.OrderBy(x => x.ID).ToList();
            }

            return r;
        }
    }
}