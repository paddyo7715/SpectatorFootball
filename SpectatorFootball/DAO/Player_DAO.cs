using SpectatorFootball.Models;
using SpectatorFootball.PlayerNS;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Data;
using System.Data.SQLite;
using SpectatorFootball.Team;

namespace SpectatorFootball
{
    public class Player_DAO
    {
        public long AddSinglePlayer(Player p, string league_filepath)
        {
            long r = 0;
            string con = Common.LeageConnection.Connect(league_filepath);

            using (var context = new leagueContext(con))
            {
                //Save the player record
                context.Players.Add(p);
                context.SaveChanges();
            }

            return r;
        }
        public List<Team_Player_Accum_Stats_by_year> getPlayerStatsByYear_Reg_Playoff(long player_id, long season_id, string league_filepath)
        {
            List<Team_Player_Accum_Stats_by_year> r = new List<Team_Player_Accum_Stats_by_year>();

            List<Passing_Accum_Stats_by_year> PassStats_regualr = null;
            List<Rushing_Accum_Stats_by_year> RushingStats_regualr = null;
            List<Receiving_Accum_Stats_by_year> ReceivingStats_regualr = null;
            List<Blocking_Accum_Stats_by_year> BlockingStats_regualr = null;
            List<Defense_Accum_Stats_by_year> DefenseStats_regualr = null;
            List<Pass_Defense_Accum_Stats_by_year> PassDefenseStats_regualr = null;
            List<Kicking_Accum_Stats_by_year> KickerStats_regualr = null;
            List<Punting_Accum_Stats_by_year> PunterStats_regualr = null;
            List<KickReturn_Accum_Stats_by_year> KickoffReturnStats_regualr = null;
            List<PuntReturns_Accum_Stats_by_year> PuntReturnStats_regualr = null;

            List<Passing_Accum_Stats_by_year> PassStats_playoff = null;
            List<Rushing_Accum_Stats_by_year> RushingStats_playoff = null;
            List<Receiving_Accum_Stats_by_year> ReceivingStats_playoff = null;
            List<Blocking_Accum_Stats_by_year> BlockingStats_playoff = null;
            List<Defense_Accum_Stats_by_year> DefenseStats_playoff = null;
            List<Pass_Defense_Accum_Stats_by_year> PassDefenseStats_playoff = null;
            List<Kicking_Accum_Stats_by_year> KickerStats_playoff = null;
            List<Punting_Accum_Stats_by_year> PunterStats_playoff = null;
            List<KickReturn_Accum_Stats_by_year> KickoffReturnStats_playoff = null;
            List<PuntReturns_Accum_Stats_by_year> PuntReturnStats_playoff = null;

            string con = Common.LeageConnection.Connect(league_filepath);

            using (var context = new leagueContext(con))
            {
//Get Player's Regular Season Stats for his career
                PassStats_regualr = context.Game_Player_Passing_Stats.Where(x => x.Game.Season_ID == season_id && x.Game.Week < app_Constants.PLAYOFF_WIDLCARD_WEEK_1 &&
                    x.Player_ID == player_id).GroupBy(x => x.Game.Season.Year)
                    .Select(x => new Passing_Accum_Stats_by_year
                    {
                        Year = x.Key,
                        Completes = x.Sum(s => s.Pass_Comp),
                        Ateempts = x.Sum(s => s.Pass_Att),
                        Yards = x.Sum(s => s.Pass_Yards),
                        TDs = x.Sum(s => s.Pass_TDs),
                        Ints = x.Sum(s => s.Pass_Ints),
                        Fumbles = x.Sum(s => s.Fumbles),
                        Fumbles_Lost = x.Sum(s => s.Fumbles_Lost),
                    }).OrderByDescending(x => x.Yards).ThenByDescending(x => x.TDs).ToList();

                RushingStats_regualr = context.Game_Player_Rushing_Stats.Where(x => x.Game.Season_ID == season_id && x.Game.Week < app_Constants.PLAYOFF_WIDLCARD_WEEK_1 &&
                    x.Player_ID == player_id).GroupBy(x => x.Game.Season.Year)
                    .Select(x => new Rushing_Accum_Stats_by_year
                    {
                        Year = x.Key,
                        Rushes = x.Sum(s => s.Rush_Att),
                        Yards = x.Sum(s => s.Rush_Yards),
                        TDs = x.Sum(s => s.Rush_TDs),
                        Fumbles = x.Sum(s => s.Fumbles),
                        Fumbles_Lost = x.Sum(s => s.Fumbles_Lost)
                    }).OrderByDescending(x => x.Yards).ThenByDescending(x => x.TDs).ToList();

                ReceivingStats_regualr = context.Game_Player_Receiving_Stats.Where(x => x.Game.Season_ID == season_id && x.Game.Week < app_Constants.PLAYOFF_WIDLCARD_WEEK_1 &&
                    x.Player_ID == player_id).GroupBy(x => x.Game.Season.Year)
                    .Select(x => new Receiving_Accum_Stats_by_year
                    {
                        Year = x.Key,
                        Catches = x.Sum(s => s.Rec_Catches),
                        Yards = x.Sum(s => s.Rec_Yards),
                        TDs = x.Sum(s => s.Rec_TDs),
                        Drops = x.Sum(s => s.Rec_Drops),
                        Fumbles = x.Sum(s => s.Fumbles),
                        Fumbles_Lost = x.Sum(s => s.Fumbles_Lost)
                    }).OrderByDescending(x => x.Catches).ThenByDescending(x => x.Yards).ToList();

                BlockingStats_regualr = context.Game_Player_Offensive_Linemen_Stats.Where(x => x.Game.Season_ID == season_id && x.Game.Week < app_Constants.PLAYOFF_WIDLCARD_WEEK_1 &&
                    x.Player_ID == player_id).GroupBy(x => x.Game.Season.Year)
                    .Select(x => new Blocking_Accum_Stats_by_year
                    {
                        Year = x.Key,
                        Plays = x.Sum(s => s.Oline_Plays),
                        Pancakes = x.Sum(s => s.OLine_Pancakes),
                        Sacks_Allowed = x.Sum(s => s.OLine_Sacks_Allowed),
                        Pressures_Allowed = x.Sum(s => s.QB_Pressures_Allowed)
                    }).OrderByDescending(x => x.Pancakes).ThenByDescending(x => x.Plays).ToList();

                DefenseStats_regualr = context.Game_Player_Defense_Stats.Where(x => x.Game.Season_ID == season_id && x.Game.Week < app_Constants.PLAYOFF_WIDLCARD_WEEK_1 &&
                    x.Player_ID == player_id).GroupBy(x => x.Game.Season.Year)
                    .Select(x => new Defense_Accum_Stats_by_year
                    {
                        Year = x.Key,
                        Plays = x.Sum(s => s.Pass_Rushes + s.Pass_Blocks),
                        Tackles = x.Sum(s => s.Def_Tackles),
                        Sacks = x.Sum(s => s.Def_Sacks),
                        Pressures = x.Sum(s => s.QB_Pressures),
                        Run_for_Loss = x.Sum(s => s.Def_Rushing_Loss),
                        Forced_Fumble = x.Sum(s => s.Forced_Fumbles)
                    }).OrderByDescending(x => x.Tackles).ThenByDescending(x => x.Sacks).ThenByDescending(x => x.Run_for_Loss).ToList();

                PassDefenseStats_regualr = context.Game_Player_Pass_Defense_Stats.Where(x => x.Game.Season_ID == season_id && x.Game.Week < app_Constants.PLAYOFF_WIDLCARD_WEEK_1 &&
                    x.Player_ID == player_id).GroupBy(x => x.Game.Season.Year)
                    .Select(x => new Pass_Defense_Accum_Stats_by_year
                    {
                        Year = x.Key,
                        Pass_Defenses = x.Sum(s => s.Def_Pass_Defenses),
                        Ints = x.Sum(s => s.Ints),
                        TDs_Surrendered = x.Sum(s => s.Touchdowns_Surrendered),
                        Forced_Fumble = x.Sum(s => s.Forced_Fumbles)
                    }).OrderByDescending(x => x.Ints).ThenByDescending(x => x.Pass_Defenses).ToList();

                KickerStats_regualr = context.Game_Player_Kicker_Stats.Where(x => x.Game.Season_ID == season_id && x.Game.Week < app_Constants.PLAYOFF_WIDLCARD_WEEK_1 &&
                    x.Player_ID == player_id).GroupBy(x => x.Game.Season.Year)
                    .Select(x => new Kicking_Accum_Stats_by_year
                    {
                        Year = x.Key,
                        FG_ATT = x.Sum(s => s.FG_Att),
                        FG_Made = x.Sum(s => s.FG_Made),
                        FG_Long = x.Max(s => s.FG_Long),
                        XP_ATT = x.Sum(s => s.XP_Att),
                        XP_Made = x.Sum(s => s.XP_Made)
                    }).OrderByDescending(x => x.FG_Made).ToList();

                PunterStats_regualr = context.Game_Player_Punter_Stats.Where(x => x.Game.Season_ID == season_id && x.Game.Week < app_Constants.PLAYOFF_WIDLCARD_WEEK_1 &&
                    x.Player_ID == player_id).GroupBy(x => x.Game.Season.Year)
                    .Select(x => new Punting_Accum_Stats_by_year
                    {
                        Year = x.Key,
                        Punts = x.Sum(s => s.num_punts),
                        Yards = x.Sum(s => s.Punt_yards),
                        Coffin_Corners = x.Sum(s => s.Punt_killed_num)
                    }).OrderByDescending(x => x.Punts).ToList();

                KickoffReturnStats_regualr = context.Game_Player_Kick_Returner_Stats.Where(x => x.Game.Season_ID == season_id && x.Game.Week < app_Constants.PLAYOFF_WIDLCARD_WEEK_1 &&
                    x.Player_ID == player_id).GroupBy(x => x.Game.Season.Year)
                    .Select(x => new KickReturn_Accum_Stats_by_year
                    {
                        Year = x.Key,
                        Returns = x.Sum(s => s.Kickoffs_Returned),
                        Yards = x.Sum(s => s.Kickoffs_Returned_Yards),
                        Yards_Long = x.Max(s => s.Kickoff_Return_Yards_Long),
                        TDs = x.Sum(s => s.Kickoffs_Returned_TDs),
                        Fumbles = x.Sum(s => s.Kickoffs_Returned_Fumbles)
                    }).OrderByDescending(x => x.Returns).ToList();

                PuntReturnStats_regualr = context.Game_Player_Punt_Returner_Stats.Where(x => x.Game.Season_ID == season_id && x.Game.Week < app_Constants.PLAYOFF_WIDLCARD_WEEK_1 &&
                    x.Player_ID == player_id).GroupBy(x => x.Game.Season.Year)
                    .Select(x => new PuntReturns_Accum_Stats_by_year
                    {
                        Year = x.Key,
                        Returns = x.Sum(s => s.Punts_Returned),
                        Yards = x.Sum(s => s.Punts_Returned_Yards),
                        Yards_Long = x.Max(s => s.Punt_Returned_Yards_Long),
                        TDs = x.Sum(s => s.Punts_Returned_TDs),
                        Fumbles = x.Sum(s => s.Punts_Returned_Fumbles)
                    }).OrderByDescending(x => x.Returns).ToList();
//Get Player's Playoff stats for his career
                PassStats_playoff = context.Game_Player_Passing_Stats.Where(x => x.Game.Season_ID == season_id && x.Game.Week >= app_Constants.PLAYOFF_WIDLCARD_WEEK_1 &&
                    x.Player_ID == player_id).GroupBy(x => x.Game.Season.Year)
                    .Select(x => new Passing_Accum_Stats_by_year
                    {
                        Year = x.Key,
                        Completes = x.Sum(s => s.Pass_Comp),
                        Ateempts = x.Sum(s => s.Pass_Att),
                        Yards = x.Sum(s => s.Pass_Yards),
                        TDs = x.Sum(s => s.Pass_TDs),
                        Ints = x.Sum(s => s.Pass_Ints),
                        Fumbles = x.Sum(s => s.Fumbles),
                        Fumbles_Lost = x.Sum(s => s.Fumbles_Lost),
                    }).OrderByDescending(x => x.Yards).ThenByDescending(x => x.TDs).ToList();

                RushingStats_playoff = context.Game_Player_Rushing_Stats.Where(x => x.Game.Season_ID == season_id && x.Game.Week >= app_Constants.PLAYOFF_WIDLCARD_WEEK_1 &&
                    x.Player_ID == player_id).GroupBy(x => x.Game.Season.Year)
                    .Select(x => new Rushing_Accum_Stats_by_year
                    {
                        Year = x.Key,
                        Rushes = x.Sum(s => s.Rush_Att),
                        Yards = x.Sum(s => s.Rush_Yards),
                        TDs = x.Sum(s => s.Rush_TDs),
                        Fumbles = x.Sum(s => s.Fumbles),
                        Fumbles_Lost = x.Sum(s => s.Fumbles_Lost)
                    }).OrderByDescending(x => x.Yards).ThenByDescending(x => x.TDs).ToList();

                ReceivingStats_playoff = context.Game_Player_Receiving_Stats.Where(x => x.Game.Season_ID == season_id && x.Game.Week >= app_Constants.PLAYOFF_WIDLCARD_WEEK_1 &&
                    x.Player_ID == player_id).GroupBy(x => x.Game.Season.Year)
                    .Select(x => new Receiving_Accum_Stats_by_year
                    {
                        Year = x.Key,
                        Catches = x.Sum(s => s.Rec_Catches),
                        Yards = x.Sum(s => s.Rec_Yards),
                        TDs = x.Sum(s => s.Rec_TDs),
                        Drops = x.Sum(s => s.Rec_Drops),
                        Fumbles = x.Sum(s => s.Fumbles),
                        Fumbles_Lost = x.Sum(s => s.Fumbles_Lost)
                    }).OrderByDescending(x => x.Catches).ThenByDescending(x => x.Yards).ToList();

                BlockingStats_playoff = context.Game_Player_Offensive_Linemen_Stats.Where(x => x.Game.Season_ID == season_id && x.Game.Week >= app_Constants.PLAYOFF_WIDLCARD_WEEK_1 &&
                    x.Player_ID == player_id).GroupBy(x => x.Game.Season.Year)
                    .Select(x => new Blocking_Accum_Stats_by_year
                    {
                        Year = x.Key,
                        Plays = x.Sum(s => s.Oline_Plays),
                        Pancakes = x.Sum(s => s.OLine_Pancakes),
                        Sacks_Allowed = x.Sum(s => s.OLine_Sacks_Allowed),
                        Pressures_Allowed = x.Sum(s => s.QB_Pressures_Allowed)
                    }).OrderByDescending(x => x.Pancakes).ThenByDescending(x => x.Plays).ToList();

                DefenseStats_playoff = context.Game_Player_Defense_Stats.Where(x => x.Game.Season_ID == season_id && x.Game.Week >= app_Constants.PLAYOFF_WIDLCARD_WEEK_1 &&
                    x.Player_ID == player_id).GroupBy(x => x.Game.Season.Year)
                    .Select(x => new Defense_Accum_Stats_by_year
                    {
                        Year = x.Key,
                        Plays = x.Sum(s => s.Pass_Rushes + s.Pass_Blocks),
                        Tackles = x.Sum(s => s.Def_Tackles),
                        Sacks = x.Sum(s => s.Def_Sacks),
                        Pressures = x.Sum(s => s.QB_Pressures),
                        Run_for_Loss = x.Sum(s => s.Def_Rushing_Loss),
                        Forced_Fumble = x.Sum(s => s.Forced_Fumbles)
                    }).OrderByDescending(x => x.Tackles).ThenByDescending(x => x.Sacks).ThenByDescending(x => x.Run_for_Loss).ToList();

                PassDefenseStats_playoff = context.Game_Player_Pass_Defense_Stats.Where(x => x.Game.Season_ID == season_id && x.Game.Week >= app_Constants.PLAYOFF_WIDLCARD_WEEK_1 &&
                    x.Player_ID == player_id).GroupBy(x => x.Game.Season.Year)
                    .Select(x => new Pass_Defense_Accum_Stats_by_year
                    {
                        Year = x.Key,
                        Pass_Defenses = x.Sum(s => s.Def_Pass_Defenses),
                        Ints = x.Sum(s => s.Ints),
                        TDs_Surrendered = x.Sum(s => s.Touchdowns_Surrendered),
                        Forced_Fumble = x.Sum(s => s.Forced_Fumbles)
                    }).OrderByDescending(x => x.Ints).ThenByDescending(x => x.Pass_Defenses).ToList();

                KickerStats_playoff = context.Game_Player_Kicker_Stats.Where(x => x.Game.Season_ID == season_id && x.Game.Week >= app_Constants.PLAYOFF_WIDLCARD_WEEK_1 &&
                    x.Player_ID == player_id).GroupBy(x => x.Game.Season.Year)
                    .Select(x => new Kicking_Accum_Stats_by_year
                    {
                        Year = x.Key,
                        FG_ATT = x.Sum(s => s.FG_Att),
                        FG_Made = x.Sum(s => s.FG_Made),
                        FG_Long = x.Max(s => s.FG_Long),
                        XP_ATT = x.Sum(s => s.XP_Att),
                        XP_Made = x.Sum(s => s.XP_Made)
                    }).OrderByDescending(x => x.FG_Made).ToList();

                PunterStats_playoff = context.Game_Player_Punter_Stats.Where(x => x.Game.Season_ID == season_id && x.Game.Week >= app_Constants.PLAYOFF_WIDLCARD_WEEK_1 &&
                    x.Player_ID == player_id).GroupBy(x => x.Game.Season.Year)
                    .Select(x => new Punting_Accum_Stats_by_year
                    {
                        Year = x.Key,
                        Punts = x.Sum(s => s.num_punts),
                        Yards = x.Sum(s => s.Punt_yards),
                        Coffin_Corners = x.Sum(s => s.Punt_killed_num)
                    }).OrderByDescending(x => x.Punts).ToList();

                KickoffReturnStats_playoff = context.Game_Player_Kick_Returner_Stats.Where(x => x.Game.Season_ID == season_id && x.Game.Week >= app_Constants.PLAYOFF_WIDLCARD_WEEK_1 &&
                    x.Player_ID == player_id).GroupBy(x => x.Game.Season.Year)
                    .Select(x => new KickReturn_Accum_Stats_by_year
                    {
                        Year = x.Key,
                        Returns = x.Sum(s => s.Kickoffs_Returned),
                        Yards = x.Sum(s => s.Kickoffs_Returned_Yards),
                        Yards_Long = x.Max(s => s.Kickoff_Return_Yards_Long),
                        TDs = x.Sum(s => s.Kickoffs_Returned_TDs),
                        Fumbles = x.Sum(s => s.Kickoffs_Returned_Fumbles)
                    }).OrderByDescending(x => x.Returns).ToList();

                PuntReturnStats_playoff = context.Game_Player_Punt_Returner_Stats.Where(x => x.Game.Season_ID == season_id && x.Game.Week >= app_Constants.PLAYOFF_WIDLCARD_WEEK_1 &&
                    x.Player_ID == player_id).GroupBy(x => x.Game.Season.Year)
                    .Select(x => new PuntReturns_Accum_Stats_by_year
                    {
                        Year = x.Key,
                        Returns = x.Sum(s => s.Punts_Returned),
                        Yards = x.Sum(s => s.Punts_Returned_Yards),
                        Yards_Long = x.Max(s => s.Punt_Returned_Yards_Long),
                        TDs = x.Sum(s => s.Punts_Returned_TDs),
                        Fumbles = x.Sum(s => s.Punts_Returned_Fumbles)
                    }).OrderByDescending(x => x.Returns).ToList();
            }

            r.Add(new Team_Player_Accum_Stats_by_year()
            {
                Passing_Stats = PassStats_regualr,
                Receiving_Stats = ReceivingStats_regualr,
                Rushing_Stats = RushingStats_regualr,
                Blocking_Stats = BlockingStats_regualr,
                Defense_Stats = DefenseStats_regualr,
                Pass_Defense_Stats = PassDefenseStats_regualr,
                Kicking_Stats = KickerStats_regualr,
                Punting_Stats = PunterStats_regualr,
                KickRet_Stats = KickoffReturnStats_regualr,
                PuntRet_Stats = PuntReturnStats_regualr
            });

            r.Add(new Team_Player_Accum_Stats_by_year()
            {
                Passing_Stats = PassStats_playoff,
                Receiving_Stats = ReceivingStats_playoff,
                Rushing_Stats = RushingStats_playoff,
                Blocking_Stats = BlockingStats_playoff,
                Defense_Stats = DefenseStats_playoff,
                Pass_Defense_Stats = PassDefenseStats_playoff,
                Kicking_Stats = KickerStats_playoff,
                Punting_Stats = PunterStats_playoff,
                KickRet_Stats = KickoffReturnStats_playoff,
                PuntRet_Stats = PuntReturnStats_playoff
            });

            return r;
        }

        public List<Two_Coll_List> getPlayerAwards(long player_id, long season_id, string league_filepath)
        {
            List<Two_Coll_List> r = new List<Two_Coll_List>();

            string con = Common.LeageConnection.Connect(league_filepath);

            using (var context = new leagueContext(con))
            {
                context.Database.Log = Console.Write;
                r = context.Player_Awards.Where(x => x.Player_ID == player_id).GroupBy(x => x.Award.Description)
                .Select(x => new Two_Coll_List
                {
                    col1 = x.Key,
                    col2 = x.Count().ToString()
                 }).OrderBy(x => x.col2).ToList();
            }
            return r;
        }
        public List<Player_Ratings> getPlayerRatingsAllYears(long player_id, long season_id, string league_filepath)
        {
            List<Player_Ratings> r = null;

            string con = Common.LeageConnection.Connect(league_filepath);

            using (var context = new leagueContext(con))
            {
                r = context.Player_Ratings.Where(x => x.Player_ID == player_id).OrderBy(x => x.Season_ID).ToList();
            }
            return r;
        }
    }
}
