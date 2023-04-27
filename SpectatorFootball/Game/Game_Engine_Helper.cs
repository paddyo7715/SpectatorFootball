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
        //This method will pick the running slot
        public static int getKickReturnRunSlot(int slot_index, bool bLookforhole, List<int?> group, bool bAnyFive)
        {
            int r;
            List<int> empty_indexes = CommonUtils.GetIndexes(group, true);
            List<int> possible_indexes = new List<int>();
            if (bAnyFive)
            {
                if (bLookforhole && empty_indexes.Count() > 0)
                    possible_indexes = empty_indexes;
                else
                    possible_indexes = CommonUtils.GetIndexes(group, false);
            }
            else
            {
                if (bLookforhole) //you can go a max of 2 up or down
                {
                    foreach (int i in empty_indexes)
                    {
                        if (i >= slot_index - app_Constants.KICKOFF_AFTER_FIRST_GROUP_SLOT_VARIANCE && i <= slot_index + app_Constants.KICKOFF_AFTER_FIRST_GROUP_SLOT_VARIANCE)
                            possible_indexes.Add(i);
                    }
                    if (possible_indexes.Count() == 0)
                    {
                        for (int i = slot_index - app_Constants.KICKOFF_AFTER_FIRST_GROUP_SLOT_VARIANCE; i <= slot_index + app_Constants.KICKOFF_AFTER_FIRST_GROUP_SLOT_VARIANCE; i++)
                        {
                            if (i >= 0 && i < group.Count())
                                possible_indexes.Add(i);
                        }
                    }
                }
                else
                {
                    for (int i = slot_index - app_Constants.KICKOFF_AFTER_FIRST_GROUP_SLOT_VARIANCE; i <= slot_index + app_Constants.KICKOFF_AFTER_FIRST_GROUP_SLOT_VARIANCE; i++)
                    {
                        if (i >= 0 && i < group.Count())
                            possible_indexes.Add(i);
                    }
                }

            }

            int r_ind = CommonUtils.getRandomIndex(possible_indexes.Count());
            r = possible_indexes[r_ind];

            return r;
        }

        public static int getClosestKickGroupPlayerInd(int slot_index, List<int?> Group)
        {
            int r,g;
            List<int> Possible_Indexes = new List<int>();

            if (Group[slot_index] != null)
                Possible_Indexes.Add(slot_index);

            if (slot_index > 0 && Group[slot_index - 1] != null)
                Possible_Indexes.Add(slot_index - 1);

            if (slot_index < app_Constants.KICKOFF_PLAYERS_IN_GROUP - 1 && Group[slot_index + 1] != null)
                Possible_Indexes.Add(slot_index + 1);

            if (Possible_Indexes.Count == 0)
                throw new Exception("Could not find closest tacker in method getClosestKickGroupPlayerInd");

            int r_ind = CommonUtils.getRandomIndex(Possible_Indexes.Count());
            g = Possible_Indexes[r_ind];

            r = (int)Group[g];

            return  r;
        }

        public static bool ReturnerLookforHole(double agility)
        {
            bool r = false;

            int agility_var = (int)agility - app_Constants.KICKOFF_AGILITY_CUTOFF;
            int r_agile = CommonUtils.getRandomNum(1, app_Constants.KICKOFF_AVOID_TRACKER_CALC_VARIABLE);
            if (r_agile <= agility_var)
                r = true;

            return r;
        }
        public static int? getPossibleUporDownTackler(bool bSwerveUp, int slot_index, List<int?> group)
        {
            int? r = null;

            if (bSwerveUp)
            {
                if (slot_index > 0) r = group[slot_index - 1];
            }
            else
            {
                if (slot_index < app_Constants.KICKOFF_PLAYERS_IN_GROUP - 1) r = group[slot_index + 1];
            }

            return r;
        }
        public static List<int> getPossibleAdjacentTacklers(int slot_index, List<int?> group)
        {
            List<int> r = new List<int>();

            //Check one spot above
            if (slot_index > 0)
            {
                int above_slot = slot_index - 1;
                if (group[above_slot] != null)
                    r.Add((int)group[above_slot]);
            }

            //Check one apot below
            if (slot_index < app_Constants.KICKOFF_PLAYERS_IN_GROUP - 1)
            {
                int below_slot = slot_index + 1;
                if (group[below_slot] != null)
                    r.Add((int)group[below_slot]);
            }

            return r;
        }
        public static double getKickoffGroupOffset(int ind)
        {
            double r = 0;

            ind -= 2;

            r = app_Constants.KICKOFF_GROUP_VERT_DIST * ind;

            return r;
        }
        public static List<int?> ExpandGroup(List<int?> Group)
        {
            List<int?> r = new List<int?>();
            int empty_spots = app_Constants.KICKOFF_PLAYERS_IN_GROUP - Group.Count();

            foreach (int? s in Group)
            {
                bool bStopEmpties = false;
                while (!bStopEmpties && empty_spots > 0)
                {
                    int rnd = CommonUtils.getRandomNum(1, 10);
                    if (rnd <= 6)
                    {
                        r.Add(null);
                        empty_spots--;
                    }
                    else
                        bStopEmpties = true;
                }

                r.Add(s);
            }

            for (int i = 0; i < empty_spots; i++)
                r.Add(null);

            return r;
        }
        public static List<int?> removeRandomIndexes(List<int?> lst)
        {
            List<int?> r = new List<int?>();

            while (lst.Count > app_Constants.KICKOFF_PLAYERS_IN_GROUP)
            {
                int ind = CommonUtils.getRandomNum(1, lst.Count()) - 1;
                r.Add(lst[ind]);
                lst.RemoveAt(ind);
            }

            return r;
        }
    }
}
