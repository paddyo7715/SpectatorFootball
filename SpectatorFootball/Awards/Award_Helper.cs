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
                    (x.TDs * app_Constants.RECEIVING_TD_AWARD_MULTIPLYER) -
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
    }
}
