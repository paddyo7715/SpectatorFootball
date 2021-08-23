﻿using System;
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
                r = context.Players.Where(x => x.Franchise_ID == franchise_id && x.Retired == 0).Include(x => x.Player_Ratings).Where(x => x.Player_Ratings.Any(i => i.Season_ID == season_id)).Include(x => x.Drafts).ToList();
            }

            return r;
        }

        public List<Pos_and_Count> GetTeamPlayerPosCounts(long season_id, long? franchise_id, string league_filepath)
        {
            List<Pos_and_Count> r = null;

            string con = Common.LeageConnection.Connect(league_filepath);

            using (var context = new leagueContext(con))
            {
                r = context.Players.Where(x => x.Franchise_ID == franchise_id && x.Retired == 0).GroupBy(x => x.Pos).Select(x => new Pos_and_Count { pos = (int)x.Key, pos_count = x.Count() }).ToList();
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
                context.Teams_by_Season.Add(t);
                context.Entry(t).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }

        }
        public Team_Player_Accum_Stats getTeamSeasonPlayerStats(long season_id, long franchise_id, string league_filepath)
        {
            Team_Player_Accum_Stats r = new Team_Player_Accum_Stats();

            List<Passing_Accum_Stats> PassStats = null;
            List<Rushing_Accum_Stats> RushingStats = null;
            List<Receiving_Accum_Stats> ReceivingStats = null;
            List<Blocking_Accum_Stats> BlockingStats = null;
            List<Defense_Accum_Stats> DefenseStats = null;
            List<Pass_Defense_Accum_Stats> PassDefenseStats = null;
            List<Kicking_Accum_Stats> KickerStats = null;
            List<Punting_Accum_Stats> PunterStats = null;
            List<KickReturn_Accum_Stats> KickoffReturnStats = null;
            List<PuntReturns_Accum_Stats> PuntReturnStats = null;

            string con = Common.LeageConnection.Connect(league_filepath);

            using (var context = new leagueContext(con))
            {
                PassStats = context.Game_Player_Passing_Stats.Where(x => x.Game.Season_ID == season_id && x.Game.Week < app_Constants.PLAYOFF_WIDLCARD_WEEK_1 &&
                    (x.Game.Home_Team_Franchise_ID == franchise_id || x.Game.Away_Team_Franchise_ID == franchise_id)).GroupBy(x => x.Player)
                    .Select(x => new Passing_Accum_Stats
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
                    (x.Game.Home_Team_Franchise_ID == franchise_id || x.Game.Away_Team_Franchise_ID == franchise_id)).GroupBy(x => x.Player)
                    .Select(x => new Rushing_Accum_Stats
                    {
                        p = x.Key,
                        Rushes = x.Sum(s => s.Rush_Att),
                        Yards = x.Sum(s => s.Rush_Yards),
                        TDs = x.Sum(s => s.Rush_TDs),
                        Fumbles = x.Sum(s => s.Fumbles),
                        Fumbles_Lost = x.Sum(s => s.Fumbles_Lost)
                    }).OrderByDescending(x => x.Yards).ThenByDescending(x => x.TDs).ToList();

                ReceivingStats = context.Game_Player_Receiving_Stats.Where(x => x.Game.Season_ID == season_id && x.Game.Week < app_Constants.PLAYOFF_WIDLCARD_WEEK_1 &&
                    (x.Game.Home_Team_Franchise_ID == franchise_id || x.Game.Away_Team_Franchise_ID == franchise_id)).GroupBy(x => x.Player)
                    .Select(x => new Receiving_Accum_Stats
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
                    (x.Game.Home_Team_Franchise_ID == franchise_id || x.Game.Away_Team_Franchise_ID == franchise_id)).GroupBy(x => x.Player)
                    .Select(x => new Blocking_Accum_Stats
                    {
                        p = x.Key,
                        Plays = x.Sum(s => s.Oline_Plays),
                        Pancakes = x.Sum(s => s.OLine_Pancakes),
                        Sacks_Allowed = x.Sum(s => s.OLine_Sacks_Allowed),
                        Pressures_Allowed = x.Sum(s => s.QB_Pressures_Allowed)
                    }).OrderByDescending(x => x.Pancakes).ThenByDescending(x => x.Plays).ToList();

                DefenseStats = context.Game_Player_Defense_Stats.Where(x => x.Game.Season_ID == season_id && x.Game.Week < app_Constants.PLAYOFF_WIDLCARD_WEEK_1 &&
                    (x.Game.Home_Team_Franchise_ID == franchise_id || x.Game.Away_Team_Franchise_ID == franchise_id)).GroupBy(x => x.Player)
                    .Select(x => new Defense_Accum_Stats
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
                    (x.Game.Home_Team_Franchise_ID == franchise_id || x.Game.Away_Team_Franchise_ID == franchise_id)).GroupBy(x => x.Player)
                    .Select(x => new Pass_Defense_Accum_Stats
                    {
                        p = x.Key,
                        Pass_Defenses = x.Sum(s => s.Def_Pass_Defenses),
                        Ints = x.Sum(s => s.Ints),
                        TDs_Surrendered = x.Sum(s => s.Touchdowns_Surrendered),
                        Forced_Fumble = x.Sum(s => s.Forced_Fumbles)
                    }).OrderByDescending(x => x.Ints).ThenByDescending(x => x.Pass_Defenses).ToList();

                KickerStats = context.Game_Player_Kicker_Stats.Where(x => x.Game.Season_ID == season_id && x.Game.Week < app_Constants.PLAYOFF_WIDLCARD_WEEK_1 &&
                    (x.Game.Home_Team_Franchise_ID == franchise_id || x.Game.Away_Team_Franchise_ID == franchise_id)).GroupBy(x => x.Player)
                    .Select(x => new Kicking_Accum_Stats
                    {
                        p = x.Key,
                        FG_ATT = x.Sum(s => s.FG_Att),
                        FG_Made = x.Sum(s => s.FG_Made),
                        FG_Long = x.Max(s => s.FG_Long),
                        XP_ATT = x.Sum(s => s.XP_Att),
                        XP_Made = x.Sum(s => s.XP_Made)
                    }).OrderByDescending(x => x.FG_Made).ToList();

                PunterStats = context.Game_Player_Punter_Stats.Where(x => x.Game.Season_ID == season_id && x.Game.Week < app_Constants.PLAYOFF_WIDLCARD_WEEK_1 &&
                    (x.Game.Home_Team_Franchise_ID == franchise_id || x.Game.Away_Team_Franchise_ID == franchise_id)).GroupBy(x => x.Player)
                    .Select(x => new Punting_Accum_Stats
                    {
                        p = x.Key,
                        Punts = x.Sum(s => s.num_punts),
                        Yards = x.Sum(s => s.Punt_yards),
                        Coffin_Corners = x.Sum(s => s.Punt_killed_num)
                    }).OrderByDescending(x => x.Punts).ToList();

                KickoffReturnStats = context.Game_Player_Kick_Returner_Stats.Where(x => x.Game.Season_ID == season_id && x.Game.Week < app_Constants.PLAYOFF_WIDLCARD_WEEK_1 &&
                    (x.Game.Home_Team_Franchise_ID == franchise_id || x.Game.Away_Team_Franchise_ID == franchise_id)).GroupBy(x => x.Player)
                    .Select(x => new KickReturn_Accum_Stats
                    {
                        p = x.Key,
                        Returns = x.Sum(s => s.Kickoffs_Returned),
                        Yards = x.Sum(s => s.Kickoffs_Returned_Yards),
                        Yards_Long = x.Max(s => s.Kickoff_Return_Yards_Long),
                        TDs = x.Sum(s => s.Kickoffs_Returned_TDs),
                        Fumbles = x.Sum(s => s.Kickoffs_Returned_Fumbles)
                    }).OrderByDescending(x => x.Returns).ToList();

                PuntReturnStats = context.Game_Player_Punt_Returner_Stats.Where(x => x.Game.Season_ID == season_id && x.Game.Week < app_Constants.PLAYOFF_WIDLCARD_WEEK_1 &&
                    (x.Game.Home_Team_Franchise_ID == franchise_id || x.Game.Away_Team_Franchise_ID == franchise_id)).GroupBy(x => x.Player)
                    .Select(x => new PuntReturns_Accum_Stats
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
        public List<Player_Ratings> getTeamRoster(long season_id, long Franchise_id, string league_filepath)
        {
            List<Player_Ratings> r = null;


            string con = Common.LeageConnection.Connect(league_filepath);

            using (var context = new leagueContext(con))
            {
                r = context.Player_Ratings.Where(x => x.Season_ID == season_id && x.Player.Franchise_ID == Franchise_id).Include(x => x.Player).OrderBy(x => x.Player.Pos).ThenBy(x => x.Player.Last_Name).ThenBy(x => x.Player.First_Name).ToList();
            }
            return r;

        }
        public Teams_by_Season getTeamFromPlayerID(Player p, string league_filepath)
        {
            Teams_by_Season r = null;

            string con = Common.LeageConnection.Connect(league_filepath);

            using (var context = new leagueContext(con))
            {
                r = context.Teams_by_Season.Where(x => x.Franchise.Players.Any(c => c.ID == p.ID)).FirstOrDefault();
            }
            return r;
        }
    }
}