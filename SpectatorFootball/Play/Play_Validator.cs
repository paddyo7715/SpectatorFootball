using SpectatorFootball.Enum;
using SpectatorFootball.GameNS;
using SpectatorFootball.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.PlayNS
{
    public class Play_Validator
    {
        public static string Validate_Play_Result(Play_Enum pe, List<Game_Player> Punters, List<Game_Player> Returners, Play_Result pr, Game_Ball gb, bool bLefttoRight)
        {
            string r = null;

            switch (pe)
            {
                case Play_Enum.EXTRA_POINT:
                    if ((!pr.bXPMade && !pr.bXPMissed) || (pr.bXPMade && pr.bXPMissed))
                        r += " XP either missed and made or neither missed or made";
                    if (pr.FGXP_Blocked && pr.bXPMade)
                        r += " XP blocked but marked as good";
                    if (pr.FGXP_Blocked && pr.bFGXPHitGP)
                        r += " XP blocked but makred as hit goalpost";
                    if (pr.FGXP_Blocked && pr.Defender_Close_to_Kicker != null)
                        r += " XP blocked but player marked as close to kicker";
                    break;
                case Play_Enum.FIELD_GOAL:
                    if ((!pr.bFGMade && !pr.bFGMissed) || (pr.bFGMade && pr.bFGMissed))
                        r += " FG either missed and made or neither missed or made";
                    if (pr.FGXP_Blocked && pr.bFGMade)
                        r += " FG blocked but marked as good";
                    if (pr.FGXP_Blocked && pr.bFGXPHitGP)
                        r += " FG blocked but makred as hit goalpost";
                    if (pr.FGXP_Blocked && pr.Defender_Close_to_Kicker != null)
                        r += " FG blocked but player marked as close to kicker";
                    break;
                case Play_Enum.PUNT:
                    //no apparent result
                    if (!pr.bPunt_Returned && !pr.bPunt_Out_of_Bounds && !pr.bPunt_Out_of_Endzone && !pr.bPunt_blocked && !pr.bPunt_KneelDown && !pr.bPunt_Not_Fielded)
                        r += " No play result for punt";

                    //is it a touchback
                    if (!pr.bFumble_Lost && !pr.bPunt_Out_of_Bounds && Game_Engine_Helper.isTouchBack(bLefttoRight, gb.Current_YardLine) && !pr.bTouchback)
                        r += " Punt result should be btouchback set but doesnt";

                    if (pr.bFumble_Lost && !pr.bPunt_Out_of_Bounds && Game_Engine_Helper.isTouchBack(bLefttoRight, gb.Current_YardLine) && !pr.bTouchDown)
                        r += " Punt result should be touchdown on fumble in EZ but isn't";

                    //Are punt yards correct
                    double punt_yards = Game_Engine_Helper.getPuntYards(pr.Play_Start_Yardline, pr.Kick_landing_YL, bLefttoRight);
                    if (punt_yards != pr.Punt_Yards)
                        r += " Punt yards not set correctly";

                    if (pr.bPunt_Returned)
                    {
                        if (Game_Engine_Helper.isTouchdown(!bLefttoRight, gb.Current_YardLine, false) && !pr.bTouchDown)
                            r += " Punt result should be returned TD set but doesnt";

                        if (pr.bTouchDown && !pr.bFumble_Lost && !Game_Engine_Helper.isTouchdown(!bLefttoRight, gb.Current_YardLine, false)) r += " Punt marked TD but ball is not in endzone";
                        if (pr.bTouchDown && pr.bFumble_Lost && !Game_Engine_Helper.isTouchBack(bLefttoRight, gb.Current_YardLine)) r += " Punt should be a TD for the punt team on afumble in the ez";
                        if (!pr.bFumble && pr.bFumble_Lost) r += " punt marked not fumbled but fubmle lost";

                        if (pr.bPunt_blocked || pr.bPunt_KneelDown || pr.bPunt_Not_Fielded || pr.bPunt_Out_of_Bounds || pr.bPunt_Out_of_Endzone)
                            r += " Punt returned but market as something else, such as blocked";

                        double ret_yards = Game_Engine_Helper.getPuntReturnYards(!bLefttoRight, pr.Kick_caught_yl, pr.Punt_Returner.Current_YardLine);
                        if (ret_yards != pr.Yards_Returned)
                            r += " Punt return yards not set correctly";
                    }

                    //blocked punt in endzone
                    if (Game_Engine_Helper.isTouchBack(bLefttoRight, gb.Current_YardLine) && pr.bPunt_blocked)
                    {
                        if (Punters.Contains(pr.Blocked_Punt_Recoverer) && !pr.bSafety)
                            r += " Blocked punt should have resulted in safety";
                        else if (Returners.Contains(pr.Blocked_Punt_Recoverer) && !pr.bTouchDown)
                            r += " Blocked punt should have resulted in TD";
                    }
                    break;
                case Play_Enum.KICKOFF_NORMAL:
                    //no apparent result
                    if (!pr.bKick_Returned && !pr.bKick_Out_of_Endzone && !pr.bKick_KneelDown)
                        r += " No play result for kickoff";

                    //is it a touchback
                    if (!pr.bFumble_Lost && !pr.bKick_Out_of_Bounds && Game_Engine_Helper.isTouchBack(bLefttoRight, gb.Current_YardLine) && !pr.bTouchback)
                        r += " Kickoff result should be btouchback set but doesnt";

                    if (pr.bFumble_Lost && !pr.bKick_Out_of_Bounds && Game_Engine_Helper.isTouchBack(bLefttoRight, gb.Current_YardLine) && !pr.bTouchDown)
                        r += " Punt result should be touchdown on fumble in EZ but isn't";

                    if (pr.bKick_Returned)
                    {
                        if (Game_Engine_Helper.isTouchdown(!bLefttoRight, gb.Current_YardLine, false) && !pr.bTouchDown)
                            r += " kickoff result should be returned TD set but doesnt";

                        if (pr.bTouchDown && !pr.bFumble_Lost && !Game_Engine_Helper.isTouchdown(!bLefttoRight, gb.Current_YardLine, false)) r += " kickoff marked TD but ball is not in endzone";
                        if (pr.bTouchDown && pr.bFumble_Lost && !Game_Engine_Helper.isTouchBack(bLefttoRight, gb.Current_YardLine)) r += " kickoff should be a TD for the punt team on afumble in the ez";
                        if (!pr.bFumble && pr.bFumble_Lost) r += " kickoff marked not fumbled but fubmle lost";

                        if (pr.bKick_KneelDown || pr.bKick_Out_of_Endzone)
                            r += " kickoff returned but market as something else, such as blocked";

                        double ret_yards = Game_Engine_Helper.getKickoffReturnYards(!bLefttoRight, pr.Kick_caught_yl, pr.Returner.Current_YardLine);
                        if (ret_yards != pr.Yards_Returned)
                            r += " kickoff return yards not set correctly";
                    }

                    break;
                case Play_Enum.KICKOFF_ONSIDES:
                    //no apparent result
                    if (!pr.bOnsideAtt)
                        r += " Onside Kick without an onside attempt";

                    //is it a touchback
                    if (pr.bOnsideMade && pr.Onside_Kick_Recoverer == null)
                        r += " Onside kick made but no one recovered it";

                    break;
                default:
//                    r += " Play_Result_Validator unknown play";
                    break;
                }

            return r;
        }
        public static string Validate_Punt_play_stats(Play_Enum pe, List<Game_Player> team1, List<Game_Player> team2, Play_Result pr)
        {
            string r = null;

            switch (pe)
            {
                case Play_Enum.EXTRA_POINT:
                    foreach (Game_Player p in team1)
                    {
                        long pPlayer_id = p.p_and_r.pr.First().Player_ID;
                        Game_Player_Stats pStat = pr.Play_Player_Stats.Where(x => x.Player_ID == pPlayer_id).FirstOrDefault();

                        long xp_made = pr.bXPMade ? 1 : 0;

                        if (p == pr.Kicker)
                        {
                            if (pStat.XP_Plays != 1) r += " XP kicker fg play stat not set correctly";
                            if (pStat.XP_Att != 1) r += " XP kicker att set correctly";
                            if (pStat.XP_Made != xp_made) r += " XP kicker made not set correctly";
                        }
                        else
                        {
                            if (pStat.XP_Plays != 1) r += " XP Player fg play stat not set correctly";

                        }
                    }

                    long xp_blocks = 0;
                    foreach (Game_Player p in team2)
                    {
                        long pPlayer_id = p.p_and_r.pr.First().Player_ID;
                        Game_Player_Stats pStat = pr.Play_Player_Stats.Where(x => x.Player_ID == pPlayer_id).FirstOrDefault();

                        if (pStat.XP_Def_Plays != 1) r += " XP Def Player play stat not set correctly";
                        xp_blocks += pStat.XP_Block;
                    }

                    if (pr.FGXP_Blocked && xp_blocks != 1)
                        r += " XP blocked but no def player has a block stat";
                    break;
                case Play_Enum.FIELD_GOAL:
                    foreach (Game_Player p in team1)
                    {
                        long pPlayer_id = p.p_and_r.pr.First().Player_ID;
                        Game_Player_Stats pStat = pr.Play_Player_Stats.Where(x => x.Player_ID == pPlayer_id).FirstOrDefault();

                        long fg_made = pr.bFGMade ? 1 : 0;
                        long fg_long = pr.bFGMade ? (int) (pr.Field_Goal_Attempt_Length + 0.5) : 0;

                        if (p == pr.Kicker)
                        {
                            if (pStat.FG_Plays != 1) r += " FG kicker fg play stat not set correctly";
                            if (pStat.FG_Att != 1) r += " FG kicker att set correctly";
                            if (pStat.FG_Made != fg_made) r += " FG kicker made not set correctly";
                            if (pr.bFGMade && pStat.FG_Long != fg_long) r += " FG kicker long stat not set correctly";
                        }
                        else
                        {
                            if (pStat.FG_Plays != 1) r += " FG Player fg play stat not set correctly";

                        }
                    }

                    long fg_blocks = 0;
                    foreach (Game_Player p in team2)
                    {
                        long pPlayer_id = p.p_and_r.pr.First().Player_ID;
                        Game_Player_Stats pStat = pr.Play_Player_Stats.Where(x => x.Player_ID == pPlayer_id).FirstOrDefault();

                        if (pStat.fg_def_plays != 1) r += " FG Def Player play stat not set correctly";
                        fg_blocks += pStat.fg_def_block;
                    }

                    if (pr.FGXP_Blocked && fg_blocks != 1)
                        r += " FG blocked but no def player has a block stat";
                    break;

                case Play_Enum.PUNT:
                        foreach (Game_Player p in team1)
                {
                    long pPlayer_id = p.p_and_r.pr.First().Player_ID;
                    Game_Player_Stats pStat = pr.Play_Player_Stats.Where(x => x.Player_ID == pPlayer_id).FirstOrDefault();

                    long punt_def_forced_fumbles = pr.Forced_Fumble_Tackler == p ? 1 : 0;
                    long punt_forced_fumbles_recovered = pr.Fumble_Recoverer == p ? 1 : 0;
                    long punt_def_tackles = pr.Tackler == p ? 1 : 0;
                    long punt_def_tackles_missed = pr.Missed_Tackles.Contains(p) ? 1 : 0;
                    if (pStat.punt_def_forced_fumbles != punt_def_forced_fumbles) r += " punt defender forced fumbles not set correctly";
                    if (pStat.punt_forced_fumbles_recovered != punt_forced_fumbles_recovered) r += " punt def fumble recoveries not set correctly";
                    if (pStat.punt_def_tackles != punt_def_tackles) r += " punt def tackles not set correctly";
                    if (pStat.punt_def_tackles_missed != punt_def_tackles_missed) r += " punt def tackles missing not set correctly";

                    if (p == pr.Punter)
                    {
                        long punts = pr.bPunt_blocked ? 0 : 1;
                        long punt_yards = !pr.bPunt_blocked  ? (int) (pr.Punt_Yards + .5) : 0;
                        long punter_kill_att = pr.bCoffinCornerAttemt ? 1 : 0;
                        long punter_kill_made = pr.bCoffinCornerMade ? 1 : 0;
                        long punter_blicks = pr.bPunt_blocked ? 1 : 0;


                        if (pStat.punter_punts != punts) r += " Punter punts not set correctly";
                        if (pStat.punter_plays != 1) r += " Punter plays not set correctly";
                        if (pStat.punter_punt_yards != punt_yards) r += " Punter punt yards not set correctly";
                        if (pStat.punter_kill_att != punter_kill_att) r += " Punter kill atts not set correctly";
                        if (pStat.punter_kill_Succ != punter_kill_made) r += " Punter kills made not set correctly";
                        if (pStat.punter_blocks != punter_blicks) r += " Punter blocks not set correctly";
                    }
                    else
                    {
                        if (pStat.punt_def_plays != 1) r += " Punter def plays not set correctly";

                    }
                }

                foreach (Game_Player p in team2)
                {
                    long pPlayer_id = p.p_and_r.pr.First().Player_ID;
                    Game_Player_Stats pStat = pr.Play_Player_Stats.Where(x => x.Player_ID == pPlayer_id).FirstOrDefault();

                    long blocks = pr.bPunt_blocked && pr.Defender_Close_to_Kicker == p ? 1 : 0;
                    long block_recovery = pr.Blocked_Punt_Recoverer == p ? 1 : 0;
                    long block_recovery_TDs = pr.bTouchDown && pr.bPunt_blocked && pr.Blocked_Punt_Recoverer == p ? 1 : 0;
                    if (pStat.punt_rec_blocks != blocks) r += " Punt rec blocks not set correctly";
                    if (pStat.punt_rec_block_recovery != block_recovery) r += " Punt rec block recoveries not set correctly";
                    if (pStat.punt_rec_block_recovery_TDs != block_recovery_TDs) r += " block recovered TDs not set correctly";

                    if (p == pr.Returner)
                    {
                        long ret = pr.bPunt_Returned ? 1 : 0;
                        double yards_ret = pr.bPunt_Returned ? pr.Yards_Returned : 0;
                        long fumbles = pr.bFumble ? 1 : 0;
                        long fumbles_Lost = pr.bFumble_Lost ? 1 : 0;
                        long TDs = pr.bPunt_Returned && pr.bTouchDown ? 1 : 0;

                        if (pStat.punt_ret_plays != 1) r += " Returner plays not set correctly";
                        if (ret != pStat.punt_ret) r += " Punt returned but punt_ret not 1";
                        if (yards_ret != pStat.punt_ret_yards) r = " Returner Yards incorrect";
                        if (yards_ret != pStat.punt_ret_yards_long) r = " Returner long Yards incorrect";
                        if (fumbles != pStat.punt_ret_fumbles) r = " Punt fubmles incorrect";
                        if (fumbles_Lost != pStat.punt_ret_fumbles_lost) r = " punt fumbles lost incorrect";
                        if (TDs != pStat.punt_ret_TDs) r = " punt return TDs incorrect";
                    }
                    else
                    {
                        if (pStat.punt_rec_plays != 1) r += " Punter rec plays not set correctly";
                    }
                }
                break;
                case Play_Enum.KICKOFF_NORMAL:
                    foreach (Game_Player p in team1)
                    {
                        long pPlayer_id = p.p_and_r.pr.First().Player_ID;
                        Game_Player_Stats pStat = pr.Play_Player_Stats.Where(x => x.Player_ID == pPlayer_id).FirstOrDefault();

                        long kickoff_def_forced_fumbles = pr.Forced_Fumble_Tackler == p ? 1 : 0;
                        long kickoff_forced_fumbles_recovered = pr.Fumble_Recoverer == p ? 1 : 0;
                        long kickoff_def_tackles = pr.Tackler == p ? 1 : 0;
                        long kickoff_def_tackles_missed = pr.Missed_Tackles.Contains(p) ? 1 : 0;
                        if (pStat.ko_def_Forced_Fumbles != kickoff_def_forced_fumbles) r += " kickoff defender forced fumbles not set correctly";
                        if (pStat.ko_fumbles_recovered != kickoff_forced_fumbles_recovered) r += " kickoff def fumble recoveries not set correctly";
                        if (pStat.ko_def_tackles != kickoff_def_tackles) r += " kickoff def tackles not set correctly";
                        if (pStat.ko_def_tackles_missed != kickoff_def_tackles_missed) r += " kickoff def tackles missing not set correctly";

                        if (p == pr.Kicker)
                        {
                            long kickoffs = 1;
                            if (pStat.Kickoffs != kickoffs) r += " kicker kickoffs not set correctly";
                            if (pStat.kicker_plays != 1) r += " kicker plays not set correctly";
                        }
                        else
                        {
                            if (pStat.ko_def_plays != 1) r += " punt team def plays not set correctly";

                        }
                    }

                    foreach (Game_Player p in team2)
                    {
                        long pPlayer_id = p.p_and_r.pr.First().Player_ID;
                        Game_Player_Stats pStat = pr.Play_Player_Stats.Where(x => x.Player_ID == pPlayer_id).FirstOrDefault();

                        if (p == pr.Returner)
                        {
                            long ko_ret = pr.bKick_Returned ? 1 : 0;
                            long yards_ret = pr.bKick_Returned ? (int)(pr.Yards_Returned +.5) : 0;
                            long fumbles = pr.bFumble ? 1 : 0;
                            long fumbles_Lost = pr.bFumble_Lost ? 1 : 0;
                            long TDs = pr.bKick_Returned && pr.bTouchDown ? 1 : 0;

                            if (pStat.ko_ret_plays != 1) r += " kickoff Returner plays not set correctly";
                            if (ko_ret != pStat.ko_ret) r += " kickoff returned but punt_ret not 1";
                            if (yards_ret != pStat.ko_ret_yards) r = " kickoff Returner Yards incorrect";
                            if (yards_ret != pStat.ko_ret_yards_long) r = " kickoff Returner long Yards incorrect";
                            if (fumbles != pStat.ko_ret_fumbles) r = " kickoff fubmles incorrect";
                            if (fumbles_Lost != pStat.ko_ret_fumbles_lost) r = " kickoff fumbles lost incorrect";
                            if (TDs != pStat.ko_ret_TDs) r = " kickoff return TDs incorrect";
                        }
                        else
                        {
                            if (pStat.ko_rec_plays != 1) r += " kickoff rec plays not set correctly";
                        }
                    }
                    break;
                case Play_Enum.KICKOFF_ONSIDES:
                    foreach (Game_Player p in team1)
                    {
                        long pPlayer_id = p.p_and_r.pr.First().Player_ID;
                        Game_Player_Stats pStat = pr.Play_Player_Stats.Where(x => x.Player_ID == pPlayer_id).FirstOrDefault();

                        if (p == pr.Onside_Kick_Recoverer && pStat.ko_onside_recovered != 1) r += " player recovered onside kick but onside kicks recovered not set correctly";
                        if (pStat.ko_onside_play != 1) r += " player onside kickoff plays not correct";

                        if (p == pr.Kicker)
                        {
                            long onside_kicks_made = pr.bAway_OnsideMade ? 1 : 0;
                            if (pStat.ko_onside_kick_att != 1) r += " kicker onside kickoff attempts not set correctly";
                            if (pStat.ko_onside_kick_made != onside_kicks_made) r += " kicker onside kicks made not set correctly";
                        }

                    }

                    foreach (Game_Player p in team2)
                    {
                        long pPlayer_id = p.p_and_r.pr.First().Player_ID;
                        Game_Player_Stats pStat = pr.Play_Player_Stats.Where(x => x.Player_ID == pPlayer_id).FirstOrDefault();

                        if (p == pr.Onside_Kick_Recoverer && pStat.ko_onside_recovered != 1) r += " player recovered onside kick but onside kicks recovered not set correctly";
                        if (pStat.ko_onside_def_plays != 1) r += " player onside kickoff plays not correct";
                    }
                    break;
                default:
//                    r += " Play_Result_Validator unknown play";
                    break;
            }

            return r;
        }
    }
}
