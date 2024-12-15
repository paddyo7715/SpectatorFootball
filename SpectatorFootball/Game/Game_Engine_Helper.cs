using log4net;
using log4net.Core;
using log4net.Repository.Hierarchy;
using SpectatorFootball.Common;
using SpectatorFootball.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace SpectatorFootball.GameNS
{
    public class Game_Engine_Helper
    {
        private static ILog logger = LogManager.GetLogger("RollingFile");
        public static int HorizontalAdj(bool b)
        {
            int r = 1;

            if (!b)
                r *= -1;

            return r;
        }
        public static Player_States setRunningState_backup(bool bLefttoRight, bool bOffense, double x1, double y1, double x2, double y2)
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

        public static Player_States setRunningState(bool bLefttoRight, bool bOffense, double x1, double y1, double x2, double y2, double yard_backbettle)
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
                        if (xdiff < 0 && xdiff < -yard_backbettle)
                            r = Player_States.RUNNING_BACKWORDS;
                        else if (xdiff < 0)
                            r = Player_States.RUNNING_FORWARD;
                        else
                            r = Player_States.RUNNING_FORWARD;
                    }
                    else
                    {
                        if (xdiff > 0 && xdiff > yard_backbettle)
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
                        if (xdiff > 0 && xdiff > yard_backbettle)
                            r = Player_States.RUNNING_BACKWORDS;
                        else if (xdiff > 0)
                            r = Player_States.RUNNING_FORWARD;
                        else
                            r = Player_States.RUNNING_FORWARD;
                    }
                    else
                    {
                        if (xdiff < 0 && xdiff < -yard_backbettle)
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
                Blocker_adv_val = (int)(Math.Round(blkPass_Block_Rating * PASS_BLK_MULT) + (bklAgility * PASS_BLK_AGIL_MULT));
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

            r = (long)Math.Round(tackler_val);
            return r;

        }

        public static bool Make_Tackle(long carrier_speed, long carrier_agility,
                                long carrier_power_running, long tackler_tackle_rating)
        {
            bool r = false;

            double crrier_val = (carrier_speed + carrier_agility + carrier_power_running) / 3;
            double tackler_val = (int)(tackler_tackle_rating * app_Constants.TACKLER_ADVANTAGE_MULTIPLIER + 0.5);

            int max_rnd_num = (int)(tackler_val + crrier_val);
            int rnd_value = CommonUtils.getRandomNum(1, max_rnd_num + app_Constants.KICKOFF_TACKLE_TEST_ADJUSTER);
            if (rnd_value <= tackler_val)
                r = true;

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
                case Ball_Carry_Actions.PUNT_RETURN:
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
        public static double getScrimmageLine(double y, bool bLefttoRight)
        {
            double yardLine = y;

            if (!bLefttoRight)
                yardLine = 100 - y;

            return yardLine;
        }
        public static double yards_from_end_of_endzone(double x, bool bLefttoRight)
        {
            double yards;

            if (bLefttoRight)
                yards = app_Constants.FIELD_YARDS + app_Constants.ENDZONE_YARDS - x;
            else
                yards = app_Constants.ENDZONE_YARDS + x;

            return yards;
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
        public static double getKickoffReturnYards(bool bLefttoRight, double starting_yrdline, double end_yrdline)
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

        public static double getPuntReturnYards(bool bLefttoRight, double starting_yrdline, double end_yrdline)
        {
            double r = 0.0;

            //Don't count the extra yards into the endzone as yards gained
            if (end_yrdline < 0)
                end_yrdline = 0;
            else if (end_yrdline > 100)
                end_yrdline = 100;

            if (starting_yrdline < 0)
                starting_yrdline = 0;
            else if (starting_yrdline > 100)
                starting_yrdline = 100;

            if (bLefttoRight)
                r = end_yrdline - starting_yrdline;
            else
                r = starting_yrdline - end_yrdline;

            return r;
        }

        public static bool isTouchdown(bool bLefttoRight, double end_yrdline, bool bTouchback)
        {
            bool r = false;

            if (!bTouchback)
            {
                if (!bLefttoRight)
                {
                    if (end_yrdline <= 0.0)
                        r = true;
                }
                else
                {
                    if (end_yrdline >= 100.0)
                        r = true;
                }
            }

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
        public static double calcDistanceFromOpponentGL(double line_of_scrimage, bool blefttoright)
        {
            double r = 0;

            if (blefttoright)
                r = 100 - line_of_scrimage;
            else
                r = line_of_scrimage;

            return r;
        }
        public static double GreaterYardline(double Yardline1, double Yardline2, bool blefttoright)
        {
            double r = 0;

            if (blefttoright)
                r = Yardline1 > Yardline2 ? Yardline1 : Yardline2;
            else
                r = Yardline1 > Yardline2 ? Yardline2 : Yardline1;

            return r;
        }
        public static double LessYardline(double Yardline1, double Yardline2, bool blefttoright)
        {
            double r = 0;

            if (blefttoright)
                r = Yardline1 < Yardline2 ? Yardline1 : Yardline2;
            else
                r = Yardline1 < Yardline2 ? Yardline2 : Yardline1;

            return r;
        }
        public static double Yards_to_Reduce(double Yardline1, double Yardline2, bool blefttoright)
        {
            double r = 0;

            double d = Yardline1;
            if (Yardline1 < 0)
                d = 0;
            else if (Yardline1 > 100)
                d = 100;


            if (blefttoright && d > Yardline2)
                r = d - Yardline2;
            else if (!blefttoright && d < Yardline2)
                r = Math.Abs(d - Yardline2);

            return r;
        }
        public static bool isHomeTeamPosessing(Play_Enum pe, long Homeid, long awayid, long PossessesID)
        {
            bool r = false;
            long checkfid = Homeid;

            if (pe == Play_Enum.KICKOFF_NORMAL || pe == Play_Enum.KICKOFF_ONSIDES ||
                pe == Play_Enum.PUNT)
                checkfid = awayid;

            if (PossessesID == checkfid)
                r = true;

            logger.Debug("isHomeTeamPosessing " + pe.ToString() + " " + Homeid + " " + awayid + " " + PossessesID);

            return r;
        }
        public static bool DoesPlayerCoverOnsideKick(long Hands_rating)
        {
            bool r = false;
            const int top = 222;

            int rnd = CommonUtils.getRandomNum(1, top);

            if (rnd < (Hands_rating * 2))
                r = true;

            return r;
        }
        public static List<Game_Player> getPlayerSublist(List<Game_Player> pList, List<int> Index_list)
        {
            List<Game_Player> r = new List<Game_Player> ();

            int ind = 0;
            foreach (Game_Player p in pList)
            {
                if (Index_list.Contains(ind))
                    r.Add(p);
                    ind++;
            }

            return r;
        }
        public static Tuple<bool,bool> isCCEligible_and_Punt_long_Enough(double PuntLen, double y, bool bLefttoRight)
        {
            bool bEligible = false;
            bool bLongEnough = false;

            double cc_yardline = 45.0;
            double yardLine = y;

            if (!bLefttoRight)
                cc_yardline = 100.0 - cc_yardline;

            if (bLefttoRight && yardLine >= cc_yardline)
                bEligible = true;
            else if (!bLefttoRight && yardLine <= cc_yardline)
                bEligible = true;

            if (bEligible)
            {
                double dist_from_gl = calcDistanceFromOpponentGL(y, bLefttoRight);

                if (PuntLen >= dist_from_gl - app_Constants.DIST_FROM_GL_FOR_CC)
                    bLongEnough = true; 
            }

            return Tuple.Create(bEligible, bLongEnough);
        }

        public static bool CoffinCornerMade(long punt_accuracy)
        {
            bool r = false;
            int upper_limit = 300;

            int rnd = CommonUtils.getRandomNum(1, upper_limit);

            if (rnd <= punt_accuracy)
                r = true;

            return r;
        }
        public static int getAVGSpeedScore(long SpeedRating)
        {
            int iterations = 10;

            int tot = 0;
            for (int i = 0; i < iterations; i++)
            {
                int rnd = CommonUtils.getRandomNum(1, 100);
                if (rnd <= SpeedRating)
                    tot++;
            }

            return tot;

        }
        public static List<List<int?>> setTackleGroups(List<Game_Player> Punt_Players, Game_Player Punter)
        {
            List<Int_and_Double> Slot_List_sorted = setReturnSppedRanks(Punt_Players, Punter);
            return setReturnTackleGroups(Slot_List_sorted);
        }

        public static List<Int_and_Double> setReturnSppedRanks(List<Game_Player> Punt_Players, Game_Player Punter)
        {
            List<Int_and_Double> Slot_List_unsorted = new List<Int_and_Double>();

            int ind = 0;
            foreach (Game_Player p in Punt_Players)
            {
                if (p != Punter)
                {
                    long speed_Rating = p.p_and_r.pr.First().Speed_Rating;
                    double speed_score = Game_Engine_Helper.getAVGSpeedScore(speed_Rating);
                    Slot_List_unsorted.Add(new Int_and_Double() { i1 = ind, d2 = speed_score });
                }
                ind++;
            }

            List<Int_and_Double> Slot_List_sorted = Slot_List_unsorted.OrderByDescending(x => x.d2).ToList();

            if (Slot_List_sorted.Count != 10)
                throw new Exception("setReturnSppedRanks error Slot_list_sorted has " + Slot_List_sorted.Count + " records!");

            return Slot_List_sorted;
        }

        public static List<List<int?>> setReturnTackleGroups(List<Int_and_Double> Slot_List_sorted)
        {
            List<int?> group_1 = new List<int?>();
            List<int?> group_2 = new List<int?>();
            List<int?> group_3 = new List<int?>();

            //first add any gruop 1 players
            foreach (Int_and_Double d in Slot_List_sorted)
            {
                if (d.d2 >= 8 && group_1.Count() < 5)  
                    group_1.Add(d.i1);
            }

            int g2_count = (10 - group_1.Count()) / 2;
            foreach (Int_and_Double d in Slot_List_sorted)
            {
                if (group_1.Contains(d.i1))
                    continue;
                else
                {
                    if (group_2.Count() < g2_count)
                        group_2.Add(d.i1);
                    else
                        group_3.Add(d.i1);
                }
            }

            if (group_1.Count + group_2.Count + group_3.Count != 10)
                throw new Exception("setReturnTackleGroups error before sort 3 groups do not have 10 players.");

            group_1 = group_1.OrderBy(x => x).ToList();
            group_2 = group_2.OrderBy(x => x).ToList();
            group_3 = group_3.OrderBy(x => x).ToList();

            group_1 = Game_Engine_Helper.ExpandGroup(group_1);
            group_2 = Game_Engine_Helper.ExpandGroup(group_2);
            group_3 = Game_Engine_Helper.ExpandGroup(group_3);

            int player_count = 0;
            for(int yyy=0; yyy<=10;yyy++)
            {
                if (yyy == 5) continue;
                if (!group_1.Contains(yyy) && !group_2.Contains(yyy) && !group_3.Contains(yyy))
                    throw new Exception("player not found in any of the 3 final groups");
            }


            return new List<List<int?>> { group_1, group_2, group_3 };
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

        //This method determines the yardline and vertical that the punt will land an if it is catchable.
        //Note that the returner may still opt of not catch and return the ball.
        public static Tuple<double, double>  getPuntLandingSpot_and_isCatchable(bool isCCeligble, bool isCCLongEnough, bool isCCmade,
            double MaxLen, double MaxVert, double current_yardline, bool bLefttoRight)
        {
            bool bTop = CommonUtils.getRandomTrueFalse();
            int rtemp = CommonUtils.getRandomNum(1, app_Constants.DIST_FROM_GL_FOR_CC);

            var t = getPuntLandingSpot(isCCeligble, isCCLongEnough, isCCmade, MaxLen, MaxVert, current_yardline, bTop, rtemp, bLefttoRight);

            return Tuple.Create(t.Item1, t.Item2);
        }

        public static Tuple<double, double> getPuntLandingSpot(bool isCCeligble, bool isCCLongEnough, bool isCCmade,
            double MaxLen, double MaxVert, double current_yardline, bool bTop, int rtemp, bool bLefttoRight)
        {
            double FURTHEST_LEFT = -21.0;
            double FURTHEST_RIGHT = 121.0;
            double TOP_MADE = -1.0;
            double TOP_NOT_MADE = app_Constants.PUNT_GROUP_VERT_DIST;
            double BOTTOM_MADE = 101.0;
            double BOTTOM_NOT_MADE = 100 - app_Constants.PUNT_GROUP_VERT_DIST;

            double yardline = 0.0;
            double vertline = 0.0;
            bool bCatchable = false;


            if (!isCCeligble || !isCCLongEnough)
            {
                yardline = current_yardline += MaxLen * Game_Engine_Helper.HorizontalAdj(bLefttoRight);
                vertline = MaxVert;
            }
            else
            {
                double rnd = Convert.ToDouble(rtemp);
                if (bLefttoRight)
                    yardline = 100 - rnd;
                else
                    yardline = rnd;

                if (isCCmade)
                    vertline = bTop ? TOP_MADE : BOTTOM_MADE;
                else
                {
                    if (CommonUtils.getRandomTrueFalse())
                    {
                        yardline = current_yardline += MaxLen * Game_Engine_Helper.HorizontalAdj(bLefttoRight);
                        vertline = MaxVert;
                    }
                    else
                        vertline = bTop ? TOP_NOT_MADE : BOTTOM_NOT_MADE;
                }
            }

            //Make sure punt is not too far left or right out of the endzone
            if (yardline < FURTHEST_LEFT)
                yardline = FURTHEST_LEFT;
            else if (yardline > FURTHEST_RIGHT)
                yardline = FURTHEST_RIGHT;

            return Tuple.Create(yardline, vertline);
        }
        public static bool isPuntCatchable(double end_yardline, double end_vert)
        {
            return end_yardline >= 1.0 && end_yardline <= 99.0 && end_vert > 0 && end_vert < 100;
        }
        public static bool isBallvertTop(double y)
        {
            if (y <= 50.0)
                return true;
            else
                return false;
        }
        public static bool isBounceBall(double ball_end_yl)
        {
            return ball_end_yl >= -37.0 && ball_end_yl <= 137;
        }
        public static int getTackleGroup(int slot_id, List<List<int?>> tGroups)
        {
            bool bFound = false;

            int i = 0;
            foreach (var tgroup in tGroups)
            {
                if (tgroup.Contains(slot_id))
                {
                    bFound = true;
                    break;
                }    
                i++;
            }

            if (!bFound)
                throw new Exception("Error in getTackleGroup: index not found in tackle groups!");

            return i;
        }
        public static Tuple<bool, bool> BlockedPuntTD_or_Safety(bool bPuntTeamRecovers, double yl, bool bLefttoRight)
        {
            bool bTD = false;
            bool bSafety = false;

            if ((bLefttoRight && yl <= 0.0) || (!bLefttoRight && yl >= 100.0))
            {
                if (bPuntTeamRecovers)
                    bSafety = true;
                else
                    bTD = true;
            }

            return Tuple.Create(bTD, bSafety);
        }

        public static Tuple<bool, bool> ReturnTD_or_Safety(bool bPuntTeamRecovers, double yl, bool bLefttoRight)
        {
            bool bTD = false;
            bool bSafety = false;

            if ((!bLefttoRight && yl <= 0.0) || (bLefttoRight && yl >= 100.0))
            {
                if (bPuntTeamRecovers)
                    bSafety = true;
                else
                    bTD = true;
            }

            return Tuple.Create(bTD, bSafety);
        }

        public static double getPuntYards(double LOS, double ball_yl, bool bLefttoRight)
        {
            double r = 0.0;

            if (bLefttoRight && ball_yl > 100.0)
                ball_yl = 100.0;
            else if (!bLefttoRight && ball_yl < 0.0)
                ball_yl = 0.0;

            if (bLefttoRight)
                r = ball_yl - LOS;
            else
                r = LOS - ball_yl;

            return r;
        }

        public static bool isBallOutofBounds(double y)
        {
            bool r = false;

            if (y > 100.0 || y < 0.0) r = true;

            return r;
        }

        public static bool isTouchBack(bool bLefttoRight, double end_yrdline)
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
    }
}
