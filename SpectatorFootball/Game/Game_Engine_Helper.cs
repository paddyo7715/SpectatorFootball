using SpectatorFootball.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.GameNS
{
    public class Game_Engine_Helper
    {
        public static long CoinToss(int rnum, long ht_id, long at_id)
        {
            long r = 0;

            if (rnum <= 50)
                r = ht_id;
            else
                r = at_id;

            return r;
        }

        public static int HorizontalAdj(bool b)
        {
            int r = 1;

            if (!b)
                r *= -1;

            return r;
        }
        public static Player_States setRunningState(bool bLefttoRight, bool bOffense, double x1, double y1, double x2, double y2)
        {
            Player_States r = Player_States.RUNNING_FORWARD;
            double xdiff = x2 - x1;
            double ydiff = (y2 - y1) / 2.5;

            if (Math.Abs(xdiff) >= Math.Abs(ydiff))
            {
                if (bLefttoRight)
                {
                    if (bOffense)
                    {
                        if (xdiff < 0 && xdiff < -app_Constants.MOVEMENT_DIST_BEFORE_TURNING_BACK)
                            r = Player_States.RUNNING_BACKWORDS;
                        else if (xdiff < 0)
                            r = Player_States.RUNNING_FORWARD;
                        else
                            r = Player_States.RUNNING_FORWARD;
                    }
                    else
                    {
                        if (xdiff > 0 && xdiff > app_Constants.MOVEMENT_DIST_BEFORE_TURNING_BACK)
                            r = Player_States.RUNNING_BACKWORDS;
                        else if (xdiff > 0)
                            r = Player_States.RUNNING_FORWARD;
                        else
                            r = Player_States.RUNNING_FORWARD;
                    }
                }
                else
                {
                    if (bOffense)
                    {
                        if (xdiff > 0 && xdiff > app_Constants.MOVEMENT_DIST_BEFORE_TURNING_BACK)
                            r = Player_States.RUNNING_BACKWORDS;
                        else if (xdiff > 0)
                            r = Player_States.RUNNING_FORWARD;
                        else
                            r = Player_States.RUNNING_FORWARD;
                    }
                    else
                    {
                        if (xdiff < 0 && xdiff < -app_Constants.MOVEMENT_DIST_BEFORE_TURNING_BACK)
                            r = Player_States.RUNNING_BACKWORDS;
                        else if (xdiff < 0)
                            r = Player_States.RUNNING_FORWARD;
                        else
                            r = Player_States.RUNNING_FORWARD;
                    }
                }
            }
            else
            {
                if (ydiff > 0)
                    r = Player_States.RUNNING_DOWN;
                else
                    r = Player_States.RUNNING_UP;
            }

            return r;
        }
        public static block_result Attempt_Block(bool runBlock, int rndNum, 
            long blkPass_Block_Rating, long blkRun_Block_Rating, long bklAgility,
            long atkPass_Attack, long atkRun_Attack, long atkAgility, long atkSpeed)
        {
            block_result r = block_result.EVEN;
            int Blocker_adv_val = 0;
            int Attacker_adv_val = 0;
            int Blocker_Flt_val = 0;
            int Attacker_Flt_val = 0;

            const double PASS_BLK_MULT = 2.5;
            const double PASS_BLK_AGIL_MULT = 1.0;
            const double PASS_ATK_MULT = 2.0;
            const double PASS_ATK_AGIL_MULT = .65;
            const double PASS_ATK_SPEED_MULT = .5;

            const double RUN_BLK_MULT = 2.5;
            const double RUN_BLK_AGIL_MULT = .65;
            const double RUN_ATK_MULT = 2.0;
            const double RUN_ATK_AGIL_MULT = .65;

            const double ATTACKER_ADVANTAGE = 1.5;

            const double FALLEN_PERCENT_VALUE = .1;


            //On a run block then the attacker speed is not considered
            if (!runBlock)
            {
                Blocker_adv_val = (int) (Math.Round(blkPass_Block_Rating * PASS_BLK_MULT) + (bklAgility * PASS_BLK_AGIL_MULT));
                Attacker_adv_val = (int)(Math.Round(atkPass_Attack * PASS_ATK_MULT) + (atkAgility * PASS_ATK_AGIL_MULT) + (atkSpeed * PASS_ATK_SPEED_MULT));
            }
            else
            {
                Blocker_adv_val = (int)(Math.Round(blkRun_Block_Rating * RUN_BLK_MULT) + (bklAgility * RUN_BLK_AGIL_MULT));
                Attacker_adv_val = (int)(Math.Round(atkRun_Attack * RUN_ATK_MULT) + (atkAgility * RUN_ATK_AGIL_MULT));
            }

            //noew give the attacker an advantage such as +=2
            Attacker_adv_val = (int)(Attacker_adv_val * ATTACKER_ADVANTAGE);

            Blocker_Flt_val = (int)(Blocker_adv_val * FALLEN_PERCENT_VALUE + .5);
            Attacker_Flt_val = (int)(Attacker_adv_val * FALLEN_PERCENT_VALUE + .5);

            Attacker_adv_val = app_Constants.BLOCKING_MAX_RAND - Attacker_adv_val;
            Attacker_Flt_val = app_Constants.BLOCKING_MAX_RAND - Attacker_Flt_val;

            if (rndNum <= Blocker_Flt_val)
                r = block_result.BLOCKER_DOMINATED;
            else if (rndNum <= Blocker_adv_val)
                r = block_result.BLOCKER_ADVANTAGE;
            else if (rndNum >= Attacker_Flt_val)
                r = block_result.TACKLER_DOMINATED;
            else if (rndNum >= Attacker_adv_val)
                r = block_result.TACKLER_ADVANTAGE;

            return r;
        }
        //This method is called just before Make_Tackle and is used to adjust the tackler's
        //tackle rating based on if and how the blocker blocked him just before the tackle attempt
        public static long AdjustTackleRating_forBlock(block_result b_result, long tackler_tackle_rating)
        {
            long r;

            double tackler_val = tackler_tackle_rating;

            switch (b_result)
            {
                case block_result.TACKLER_DOMINATED:
                    tackler_val *= app_Constants.TACKLER_DOMINATED;
                    break;
                case block_result.TACKLER_ADVANTAGE:
                    tackler_val *= app_Constants.TACKLER_ADVANTAGE;
                    break;
                case block_result.BLOCKER_DOMINATED:
                    tackler_val *= app_Constants.BLOCKER_DOMINATED;
                    break;
                case block_result.BLOCKER_ADVANTAGE:
                    tackler_val /= app_Constants.BLOCKER_ADVANTAGE;
                    break;
                case block_result.EVEN:
                    tackler_val /= app_Constants.EVEN;
                    break;
            }

            r = (long) Math.Round(tackler_val);
            return r;

        }

        public static bool Make_Tackle(long carrier_speed, long carrier_agility,
                                long carrier_power_running, long tackler_tackle_rating)
        {
            bool r = false;

            double crrier_val = (carrier_speed + carrier_agility + carrier_power_running) / 3;
            double tackler_val = tackler_tackle_rating * app_Constants.TACKLER_ADVANTAGE_MULTIPLIER;

            int max_rnd_num = (int)(tackler_val + crrier_val);
            int rnd_value = CommonUtils.getRandomNum(1, max_rnd_num + app_Constants.KICKOFF_TACKLE_TEST_ADJUSTER);
            if (rnd_value <= tackler_val)
                r = true;

            return r;
        }
        public static bool isPlayWithReturner(Play_Enum p_enum)
        {
            bool r = false;
            if (p_enum == Play_Enum.KICKOFF_NORMAL ||
                p_enum == Play_Enum.PUNT ||
                p_enum == Play_Enum.FREE_KICK)
                r = true;

            return r;
        }
        public static long Switch_Posession(long Current_possession, long at, long ht)
        {
            long r = Current_possession;

            if (Current_possession == at)
                r = ht;
            else
                r = at;

            return r;
        }

        public static bool Switch_LefttoRight(bool bLefttoRight)
        {
            bool r;

            if (bLefttoRight) r = false; else r = true;

            return r;
        }
        public static bool DoesBallCarrierFumble(Ball_Carry_Actions bca, long BallCarrier_BallSafety_rating, long Tackle_rating,
            long Run_Attack_Rating)
        {
            bool r = false;
            double rating_multiplyer = 1.0;
            const int fumble_calc_top = 7000;
            long fumble_calc_threshold = 0;

            long fumble_avg_value = Tackle_rating + Run_Attack_Rating - BallCarrier_BallSafety_rating;
            if (fumble_avg_value < 0)
                fumble_avg_value = 0;

            switch (bca)
            {
                case Ball_Carry_Actions.PASSER_SACKED:
                    rating_multiplyer = 15;
                    break;
                case Ball_Carry_Actions.KICK_RETURN:
                    rating_multiplyer = 3.1;
                    break;
                case Ball_Carry_Actions.RUNNING_THE_BALL:
                    rating_multiplyer = 1.16;
                    break;
                case Ball_Carry_Actions.RUNNING_AFTER_CATCH:
                    rating_multiplyer = 0.96;
                    break;
                default:
                    throw new Exception("Unknown Ball_Carry_Actions in DoesBallCarrierFumble");
            }

            fumble_calc_threshold = (int)Math.Round(fumble_avg_value * rating_multiplyer);

            int rnd = CommonUtils.getRandomNum(1, fumble_calc_top);

            if (rnd <= fumble_calc_threshold)
                r = true;

            return r;
        }
        public static double getYardsGained(bool bLefttoRight, double starting_yrdline, double end_yrdline)
        {
            double r = 0.0;

            //Don't count the extra yards into the endzone as yards gained
            if (end_yrdline < 0)
                end_yrdline = 0;
            else if (end_yrdline > 100)
                end_yrdline = 100;

            if (bLefttoRight)
                r = end_yrdline - starting_yrdline;
            else
                r = starting_yrdline - end_yrdline;

            return r;
        }

        public static bool isTouchdown(bool bLefttoRight, double end_yrdline)
        {
            bool r = false;

            if (bLefttoRight)
            {
                if (end_yrdline >= 100.0)
                    r = true;
            }
            else
            {
                if (end_yrdline <= 0.0)
                    r = true;
            }

            return r;
        }

        public static double calcDistanceFromOpponentGL(double line_of_scrimage, bool blefttoright)
        {
            double r = 0;

            if (blefttoright)
                r = 100 - line_of_scrimage;
            else
                r = line_of_scrimage;

            return r;
        }

        public static double calcDistanceFromMyGL(double line_of_scrimage, bool blefttoright)
        {
            double r = 0;

            blefttoright = !blefttoright;

            if (blefttoright)
                r = 100 - line_of_scrimage;
            else
                r = line_of_scrimage;

            return r;
        }
        public static bool isBallTeamPenalty(Play_Result pResult)
        {
            bool r = false;

            Game_Player p = pResult.Penalized_Player;

            if (pResult.Passer == p || pResult.Pass_Catchers.Contains(p) || pResult.Ball_Runners.Contains(p) ||
                pResult.Pass_Blockers.Contains(p))
                r = true;
            else if (pResult.Returner == p || pResult.Punt_Returner == p || pResult.Kick_Returners.Contains(p) ||
                pResult.Punt_Returners.Contains(p) || pResult.FieldGaol_Kicking_Team.Contains(p))
                r = true;
            else if (pResult.Pass_Rushers.Contains(p) || pResult.Pass_Defenders.Contains(p) ||
                pResult.Run_Defenders.Contains(p))
                r = false;
            else if (pResult.Kicker == p || pResult.Punter == p || pResult.Kick_Defenders.Contains(p) ||
                pResult.Punt_Defenders.Contains(p) || pResult.Field_Goal_Defenders.Contains(p))
                r = false;
            else
                throw new Exception("isBallTeamPenalty error can't determine penalty team!");
            return r;
        }

    }
}
