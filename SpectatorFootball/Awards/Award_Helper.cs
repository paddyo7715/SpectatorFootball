﻿using SpectatorFootball.Enum;
using SpectatorFootball.League;
using SpectatorFootball.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.Awards
{
    class Award_Helper
    {
        public static List<Award_Rating_Rec> setPlayersMasterList(League_Stats lStats)
        {
            List<Award_Rating_Rec> r = new List<Award_Rating_Rec>();

            //Add QBs
            foreach (Passing_Accum_Stats_by_year x in lStats.Passing_Stats)
            {
                long lGrade = (int) ((x.Yards * app_Constants.PASSING_YARDS_AWARD_MULTIPLYER) +
                    (x.TDs * app_Constants.PASSING_TD_AWARD_MULTIPLYER) -
                    (x.Ints * app_Constants.PASSING_INT_AWARD_MULTIPLYER) -
                    (x.Fumbles_Lost * app_Constants.PASSING_FUMBLE_AWARD_MULTIPLYER));
                r.Add(new Award_Rating_Rec()
                { Grade = lGrade, p = x.p, isRookie = x.isRookie });
            }

            //Add RBs and QBs
            foreach (Rushing_Accum_Stats_by_year x in lStats.Rushing_Stats)
            {
                long lGrade = (int)((x.Yards * app_Constants.RUSHING_YARDS_AWARD_MULTIPLYER) +
                    (x.TDs * app_Constants.RUSHING_TD_AWARD_MULTIPLYER) -
                    (x.Fumbles_Lost * app_Constants.RUSHING_FUMBLE_AWARD_MULTIPLYER));

                Award_Rating_Rec a = r.Where(t => t.p.ID == x.p.ID).FirstOrDefault();

                //Since players can have more than 1 type of stats, check if they are
                //already in the grading list then add their grade; otherwise just add 
                //them to the list.
                if (a == null)
                {
                    r.Add(new Award_Rating_Rec()
                    { Grade = lGrade, p = x.p, isRookie = x.isRookie });
                }
                else
                {
                    a.Grade += lGrade;
                }
            }

            //Add WRs and TEs
            foreach (Receiving_Accum_Stats_by_year x in lStats.Receiving_Stats)
            {
                long lGrade = (int)((x.Yards * app_Constants.RECEIVING_YARDS_AWARD_MULTIPLYER) +
                    (x.Catches * app_Constants.RECEIVING_CATCHES_AWARD_MULTIPLYER) +
                    (x.TDs * app_Constants.RECEIVING_TD_AWARD_MULTIPLYER) -
                    (x.Drops * app_Constants.RECEIVING_DROPS_AWARD_MULTIPLYER) -
                    (x.Fumbles_Lost * app_Constants.RECEIVING_FUMBLE_AWARD_MULTIPLYER));

                Award_Rating_Rec a = r.Where(t => t.p.ID == x.p.ID).FirstOrDefault();

                //Since players can have more than 1 type of stats, check if they are
                //already in the grading list then add their grade; otherwise just add 
                //them to the list.
                if (a == null)
                {
                    r.Add(new Award_Rating_Rec()
                    { Grade = lGrade, p = x.p, isRookie = x.isRookie });
                }
                else
                {
                    a.Grade += lGrade;
                }
            }

            //Add OLs
            foreach (Blocking_Accum_Stats_by_year x in lStats.Blocking_Stats)
            {
                long lGrade = (x.Pancakes * app_Constants.BLOCKING_PANCAKES_MULTIPLYER) -
                    (x.Sacks_Allowed * app_Constants.BLOCKING_SACKS_ALLOWED_MULTIPLYER) -
                    (x.Rushing_Loss_Allowed * app_Constants.BLOCKING_RUSHING_LOSS_MULTIPLYER) -
                    (x.Pressures_Allowed * app_Constants.BLOCKING_QB_PRESSURES_MULTIPLYER);

                Award_Rating_Rec a = r.Where(t => t.p.ID == x.p.ID).FirstOrDefault();

                //Since players can have more than 1 type of stats, check if they are
                //already in the grading list then add their grade; otherwise just add 
                //them to the list.
                if (a == null)
                {
                    r.Add(new Award_Rating_Rec()
                    { Grade = lGrade, p = x.p, isRookie = x.isRookie });
                }
                else
                {
                    a.Grade += lGrade;
                }
            }

            //Add Ks
            foreach (Kicking_Accum_Stats_by_year x in lStats.Kicking_Stats)
            {
                long lGrade = (x.FG_Made * app_Constants.FG_MADE_MULTIPLYER) -
                    ((x.FG_ATT - x.FG_Made) * app_Constants.FG_MISSED_MULTIPLYER) +
                    (x.XP_Made * app_Constants.XP_MADE_MULTIPLYER) -
                    ((x.XP_ATT - x.XP_Made) * app_Constants.XP_MISSED_MULTIPLYER);

                Award_Rating_Rec a = r.Where(t => t.p.ID == x.p.ID).FirstOrDefault();

                //Since players can have more than 1 type of stats, check if they are
                //already in the grading list then add their grade; otherwise just add 
                //them to the list.
                if (a == null)
                {
                    r.Add(new Award_Rating_Rec()
                    { Grade = lGrade, p = x.p, isRookie = x.isRookie });
                }
                else
                {
                    a.Grade += lGrade;
                }
            }

            //Add ps
            foreach (Punting_Accum_Stats_by_year x in lStats.Punting_Stats)
            {
                long lGrade = (int) (((x.Punts == 0 ? 0.0 : (double)x.Yards / (double)x.Punts)  * app_Constants.PUNTING_AVG_MULTIPLYER) -
                    (x.Fumbles_Lost * app_Constants.PUNTING_FUMBLES_MULTIPLYER) -
                    (x.Blocked_Punts * app_Constants.PUNTING_BLOCKED_PUNTS_MULTIPLYER) +
                    (x.Coffin_Corners * app_Constants.PUNTING_COFFIN_CORNERS_MULTIPLYER));

                Award_Rating_Rec a = r.Where(t => t.p.ID == x.p.ID).FirstOrDefault();

                //Since players can have more than 1 type of stats, check if they are
                //already in the grading list then add their grade; otherwise just add 
                //them to the list.
                if (a == null)
                {
                    r.Add(new Award_Rating_Rec()
                    { Grade = lGrade, p = x.p, isRookie = x.isRookie });
                }
                else
                {
                    a.Grade += lGrade;
                }
            }

            //Add DLs
            foreach (Defense_Accum_Stats_by_year x in lStats.Defense_Stats)
            {
                long lGrade = (x.Tackles * app_Constants.DEFENSE_TACKLES_MULTIPLYER) +
                    (x.Sacks * app_Constants.DEFENSE_SACKS_MULTIPLYER) +
                    (x.Run_for_Loss * app_Constants.DEFENSE_RUSH_LOSS_MULTIPLYER) -
                    (x.Missed_Tackles * app_Constants.DEFENSE_MISSED_TACKLES_MULTIPLYER) +
                    (x.Def_Safety * app_Constants.DEFENSE_SAFETY_MULTIPLYER) +
                    (x.Def_TDs * app_Constants.DEFENSE_TD_MULTIPLYER) +
                    (x.Forced_Fumble * app_Constants.DEFENSE_FUMBLE_MULTIPLYER) +
                    (x.Pressures * app_Constants.DEFENSE_QB_PRESSURES_MULTIPLYER);

                Award_Rating_Rec a = r.Where(t => t.p.ID == x.p.ID).FirstOrDefault();

                //Since players can have more than 1 type of stats, check if they are
                //already in the grading list then add their grade; otherwise just add 
                //them to the list.
                if (a == null)
                {
                    r.Add(new Award_Rating_Rec()
                    { Grade = lGrade, p = x.p, isRookie = x.isRookie });
                }
                else
                {
                    a.Grade += lGrade;
                }
            }

            //Add DBs
            foreach (Pass_Defense_Accum_Stats_by_year x in lStats.Pass_Defense_Stats)
            {
                long lGrade = (x.Ints * app_Constants.DEFENSE_PASS_INTS_MULTIPLYER) +
                    (x.Def_int_TDs * app_Constants.DEFENSE_PASS_TDS_MULTIPLYER) +
                    (x.Pass_Defenses * app_Constants.DEFENSE_PASS_DEFENSES_MULTIPLYER) +
                    (x.Tackles * app_Constants.DEFENSE_PASS_TACKLES_MULTIPLYER) -
                    (x.Missed_Tackles * app_Constants.DEFENSE_PASS_MISSED_TACKLES_MULTIPLYER) -
                    (x.TDs_Surrendered * app_Constants.DEFENSE_PASS_TDS_SURRENDERED_MULTIPLYER) +
                    (x.Forced_Fumble * app_Constants.DEFENSE_PASS_FORCED_FUMBLE_MULTIPLYER);

                Award_Rating_Rec a = r.Where(t => t.p.ID == x.p.ID).FirstOrDefault();

                //Since players can have more than 1 type of stats, check if they are
                //already in the grading list then add their grade; otherwise just add 
                //them to the list.
                if (a == null)
                {
                    r.Add(new Award_Rating_Rec()
                    { Grade = lGrade, p = x.p, isRookie = x.isRookie });
                }
                else
                {
                    a.Grade += lGrade;
                }
            }



            return r;
        }
        //This method converts the game stats to lstats inorder to rank them for giving
        //out an mvp for a game.
        public static League_Stats setLState_from_Game_stats(Game g)
        {
            League_Stats r = new League_Stats();

            //passing stats
            r.Passing_Stats = new List<Passing_Accum_Stats_by_year>();
            foreach (Game_Player_Stats x in g.Game_Player_Stats)
            {
                r.Passing_Stats.Add(new Passing_Accum_Stats_by_year()
                {
                    p = x.Player,
                    Ateempts = x.off_pass_Att,
                    Completes = x.off_pass_Comp,
                    Fumbles_Lost = x.off_pass_Fumbles_Lost,
                    Ints = x.off_pass_Ints,
                    TDs = x.off_pass_TDs,
                    Pos = x.Player.Pos,
                    Yards = x.off_pass_Yards
                });
            }

            //Rushing stats
            r.Rushing_Stats = new List<Rushing_Accum_Stats_by_year>();
            foreach (Game_Player_Stats x in g.Game_Player_Stats)
            {
                r.Rushing_Stats.Add(new Rushing_Accum_Stats_by_year()
                {
                    p = x.Player,
                    Pos = x.Player.Pos,
                    Fumbles_Lost = x.off_rush_fumbles_lost,
                    Rushes = x.off_rush_att,
                    TDs = x.off_rush_TDs,
                    Yards = x.off_rush_Yards
                });
            }

            //Receiving Stats
            r.Receiving_Stats = new List<Receiving_Accum_Stats_by_year>();
            foreach (Game_Player_Stats x in g.Game_Player_Stats)
            {
                r.Receiving_Stats.Add(new Receiving_Accum_Stats_by_year()
                {
                    Catches = x.off_rec_catches,
                    Drops = x.off_rec_drops,
                    Fumbles_Lost = x.off_rec_fumbles_lost,
                    p = x.Player,
                    Pos = x.Player.Pos,
                    TDs = x.off_rec_TDs,
                    Yards = x.off_rec_Yards
                });
            }

            //blocking stats
            r.Blocking_Stats = new List<Blocking_Accum_Stats_by_year>();
            foreach (Game_Player_Stats x in g.Game_Player_Stats)
            {
                r.Blocking_Stats.Add(new Blocking_Accum_Stats_by_year()
                {
                    p = x.Player,
                    Pancakes = x.off_line_pancakes,
                    Plays = x.off_line_plays,
                    Pos = x.Player.Pos,
                    Pressures_Allowed = x.off_line_QB_Pressures_Allowed,
                    Rushing_Loss_Allowed = x.off_line_Rushing_Loss_Allowed,
                    Sacks_Allowed = x.off_line_sacks_allowed
                });
            }

            //FG
            r.Kicking_Stats = new List<Kicking_Accum_Stats_by_year>();
            foreach (Game_Player_Stats x in g.Game_Player_Stats)
            {
                r.Kicking_Stats.Add(new Kicking_Accum_Stats_by_year()
                {
                    FG_ATT = x.FG_Att,
                    FG_Made = x.FG_Made,
                    p = x.Player,
                    XP_ATT = x.XP_Att,
                    XP_Made = x.XP_Made,
                    Pos = x.Player.Pos
                });
            }

            //Punting
            r.Punting_Stats = new List<Punting_Accum_Stats_by_year>();
            foreach (Game_Player_Stats x in g.Game_Player_Stats)
            {
                r.Punting_Stats.Add(new Punting_Accum_Stats_by_year()
                {
                    p = x.Player,
                    Pos = x.Player.Pos,
                    Coffin_Corners = x.punter_kill_Succ,
                    Punts = x.punter_punts,
                    Yards = x.punter_punt_yards,
                    Fumbles_Lost = x.punter_Fumbles_lost
                });
            }

            //Defense
            r.Defense_Stats = new List<Defense_Accum_Stats_by_year>();
            foreach (Game_Player_Stats x in g.Game_Player_Stats)
            {
                r.Defense_Stats.Add(new Defense_Accum_Stats_by_year()
                {
                    p = x.Player,
                    Pos = x.Player.Pos,
                    Plays = x.def_rush_plays,
                    Def_Safety = x.def_rush_Safety,
                    Missed_Tackles = x.def_rush_Missed_Tackles,
                    Forced_Fumble = x.def_rush_Forced_Fumbles,
                    Pressures = x.def_rush_QB_Pressures,
                    Run_for_Loss = x.def_rush_Rushing_Loss,
                    Sacks = x.def_rush_sacks,
                    Tackles = x.def_rush_tackles,
                    Def_TDs = x.def_rush_TDs
                });
            }

            //Pass Defense
            r.Pass_Defense_Stats = new List<Pass_Defense_Accum_Stats_by_year>();
            foreach (Game_Player_Stats x in g.Game_Player_Stats)
            {
                r.Pass_Defense_Stats.Add(new Pass_Defense_Accum_Stats_by_year()
                {
                    p = x.Player,
                    Pos = x.Player.Pos,
                    Def_int_TDs = x.def_pass_Int_TDs,
                    Forced_Fumble = x.def_pass_Forced_Fumbles,
                    Ints = x.def_pass_Ints,
                    Missed_Tackles = x.def_pass_Missed_Tackles,
                    Pass_Defenses = x.def_pass_Pass_KnockedAway,
                    Tackles = x.def_pass_Tackles,
                    TDs_Surrendered = x.def_pass_TDs_Surrendered
                });
            }

                return r;
        }
        public static int getAllProNum(Player_Pos pos, long num_teams)
        {
            int r = 0;

            switch (pos)
            {
                case Player_Pos.QB:
                    {
                        double t = (double)num_teams * (double)app_Constants.ALL_PRO_PERCENT
                            * app_Constants.STARTER_QB_PER_TEAM;
                        r =  (int)(t + 0.5);
                        break;
                    }

                case Player_Pos.RB:
                    {
                        double t = (double)num_teams * (double)app_Constants.ALL_PRO_PERCENT
                            * app_Constants.STARTER_RB_PER_TEAM;
                        r = (int)(t + 0.5); 
                        break;
                    }

                case Player_Pos.WR:
                    {
                        double t = (double)num_teams * (double)app_Constants.ALL_PRO_PERCENT
                            * app_Constants.STARTER_WR_PER_TEAM; 
                        r = (int)(t + 0.5); 
                        break;
                    }

                case Player_Pos.TE:
                    {
                        double t = (double)num_teams * (double)app_Constants.ALL_PRO_PERCENT
                            * app_Constants.STARTER_TE_PER_TEAM;
                        r = (int)(t + 0.5); 
                        break;
                    }

                case Player_Pos.OL:
                    {
                        double t = (double)num_teams * (double)app_Constants.ALL_PRO_PERCENT
                            * app_Constants.STARTER_OL_PER_TEAM;
                        r = (int)(t + 0.5);
                        break;
                    }

                case Player_Pos.DL:
                    {
                        double t = (double)num_teams * (double)app_Constants.ALL_PRO_PERCENT
                            * app_Constants.STARTER_DL_PER_TEAM;
                        r = (int)(t + 0.5);
                        break;
                    }

                case Player_Pos.DB:
                    {
                        double t = (double)num_teams * (double)app_Constants.ALL_PRO_PERCENT
                            * app_Constants.STARTER_DB_PER_TEAM;
                        r = (int)(t + 0.5);
                        break;
                    }

                case Player_Pos.LB:
                    {
                        double t = (double)num_teams * (double)app_Constants.ALL_PRO_PERCENT
                            * app_Constants.STARTER_LB_PER_TEAM;
                        r = (int)(t + 0.5);
                        break;
                    }

                case Player_Pos.K:
                    {
                        double t = (double)num_teams * (double)app_Constants.ALL_PRO_PERCENT
                            * app_Constants.STARTER_K_PER_TEAM;
                        r = (int)(t + 0.5);
                        break;
                    }
                case Player_Pos.P:
                    {
                        double t = (double)num_teams * (double)app_Constants.ALL_PRO_PERCENT
                            * app_Constants.STARTER_P_PER_TEAM;
                        r = (int)(t + 0.5);
                        break;
                    }
            }

            if (r == 0) r = 1;
            return r;
        }
    }
}