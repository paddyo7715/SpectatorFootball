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
        public static block_result Attempt_Block(bool runBlock, int rndNum, 
            long blkPass_Block_Rating, long blkRun_Block_Rating, long bklAgility,
            long atkPass_Attack, long atkRun_Attack, long atkAgility, long atkSpeed)
        {
            block_result r = block_result.EVEN;
            int Blocker_adv_val = 0;
            int Attacker_adv_val = 0;
            int Blocker_Flt_val = 0;
            int Attacker_Flt_val = 0;

            const double PASS_BLK_MULT = 25;
            const double PASS_BLK_AGIL_MULT = 6.5;
            const double PASS_ATK_MULT = 20;
            const double PASS_ATK_AGIL_MULT = 6.5;
            const double PASS_ATK_SPEED_MULT = 5;

            const double RUN_BLK_MULT = 25;
            const double RUN_BLK_AGIL_MULT = 6.5;
            const double RUN_ATK_MULT = 20;
            const double RUN_ATK_AGIL_MULT = 6.5;

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

            Blocker_Flt_val = (int)(Blocker_adv_val * FALLEN_PERCENT_VALUE);
            Attacker_Flt_val = (int)(Attacker_adv_val * FALLEN_PERCENT_VALUE);

            //noew give the attacker an advantage such as +=2
            Attacker_adv_val = (int) (Attacker_adv_val * ATTACKER_ADVANTAGE);
            Attacker_Flt_val = (int)(Attacker_Flt_val * ATTACKER_ADVANTAGE);

            Attacker_adv_val = app_Constants.BLOCKING_MAX_RAND - Attacker_adv_val;

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

            double crrier_val = (carrier_speed + carrier_agility + carrier_power_running) * 3;
            double tackler_val = tackler_tackle_rating * app_Constants.TACKLER_ADVANTAGE_MULTIPLIER;

            int max_rnd_num = (int)(tackler_val + crrier_val);
            int rnd_value = CommonUtils.getRandomNum(1, max_rnd_num + app_Constants.KICKOFF_TACKLE_TEST_ADJUSTER);
            if (rnd_value <= tackler_val)
                r = true;

            return r;
        }
    }
}
