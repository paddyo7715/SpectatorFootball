using SpectatorFootball.Enum;
using SpectatorFootball.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.Training_CampNS
{
    class TrainingCamp_Helper
    {
        public static List<Player_and_Ratings_and_Draft> TrainingCampQB(List<Player_and_Ratings_and_Draft> pList)
        {
            List<Player_and_Ratings_and_Draft> r = pList.Where(x => x.p.Pos == (int)Player_Pos.QB).ToList();

            foreach (Player_and_Ratings_and_Draft p in r)
            {
                double tc_grade = 0.0;
                for (int i=0; i < app_Constants.TRAINING_CAMP_NUM_PLAYS; i++)
                {
                    int iAccuracy = (int)p.pr[0].Accuracy_Rating;
                    int iArmStrength = (int)p.pr[0].Arm_Strength_Rating;
                    int iDecisionMaking = (int)p.pr[0].Decision_Making_Rating;
                    int iAgility = (int)p.pr[0].Agilty_Rating;
                    int iSpeed = (int)p.pr[0].Speed_Rating;

                    int accuracy_delta = 0;

                    //Execute qb play
                    int iYardLine = CommonUtils.getRandomNum(1, 100);
                    int itemp = CommonUtils.getRandomNum(1, 100);
                    int ipassattyards = 0;
                    if (itemp <= app_Constants.TRAINING_CAMP_QB_10YRD_PASS_PERC)
                    {
                        ipassattyards = 10;
                        accuracy_delta = 4;
                    }
                    else if (itemp <= app_Constants.TRAINING_CAMP_QB_20YRD_PASS_PERC)
                    {
                        ipassattyards = 20;
                        accuracy_delta = 8;
                    }
                    else if (itemp <= app_Constants.TRAINING_CAMP_QB_30YRD_PASS_PERC)
                    {
                        ipassattyards = 30;
                        accuracy_delta = 15;
                    }
                    else if (itemp <= app_Constants.TRAINING_CAMP_QB_40YRD_PASS_PERC)
                    {
                        ipassattyards = 40;
                        accuracy_delta = 25;
                    }
                    else
                    {
                        ipassattyards = 60;
                        accuracy_delta = 35;
                    }

                    int itempRand = CommonUtils.getRandomNum(1, 100);
                    bool bPressure = false;
                    if (itempRand <= app_Constants.TRAINING_CAMP_QB_PRESSURE_RATE)
                        bPressure = true;

                    //If QB under pressure than reduce his accuracy and descision making
                    if (bPressure)
                    {
                        //If QB is mobile they have a chance to escape being under pressure
                        itempRand = CommonUtils.getRandomNum(1, 1000);
                        if (itempRand <= (iAgility + iSpeed))
                            bPressure = false;
                        else
                        {
                            iAccuracy -= app_Constants.TRAINING_CAMP_QB_PRESSURE_REDUCER;
                            iDecisionMaking -= app_Constants.TRAINING_CAMP_QB_PRESSURE_REDUCER;
                        }
                    }

                    bool bReceiverOpen = false;
                    for (int j=0; i<app_Constants.TRAINING_CAMP_QB_LOOKS;j++)
                    {
                        int itemp2 = CommonUtils.getRandomNum(1, 100);
                        if (itemp2 <= app_Constants.TRAINING_CAMP_QB_RECEIVER_OPEN)
                        {
                            bReceiverOpen = true;
                            break;
                        }
                        itemp2 = CommonUtils.getRandomNum(1, 100);
                        if (iDecisionMaking <= itemp2)
                            continue;
                        else
                            break;

                    }

                    iAccuracy -= accuracy_delta;

                    //QB makes decision to throw the football or run
                    if (bReceiverOpen)
                    {

                        int itemp3 = CommonUtils.getRandomNum(1, 100);
                        if (iAccuracy <= itemp3)
                        {
                            tc_grade += app_Constants.TRAINING_CAMP_QB_COMPLETION_AWARD;
                            if ((ipassattyards * app_Constants.TRAINING_CAMP_QB_RECEIVE_AFTER_CATCH_MULTIPLYER) + iYardLine >= 100)
                                tc_grade += app_Constants.TRAINING_CAMP_QB_TD_AWARD;
                        }
                    }
                    else
                    {
                        iAccuracy *= (int)app_Constants.TRAINING_CAMP_QB_COVERED_DIVIDER;
                        if (iAccuracy <= 0)
                            iAccuracy = 1;
                        int itemp3 = CommonUtils.getRandomNum(1, 100);
                        if (iAccuracy <= itemp3)
                        {
                            tc_grade += app_Constants.TRAINING_CAMP_QB_COMPLETION_AWARD;
                            if ((ipassattyards * app_Constants.TRAINING_CAMP_QB_RECEIVE_AFTER_CATCH_MULTIPLYER) + iYardLine >= 100)
                                tc_grade += app_Constants.TRAINING_CAMP_QB_TD_AWARD;
                        }
                        else if (itemp3 >= 100 - app_Constants.TRAINING_CAMP_QB_DEFENDER_INT_PERC)
                            tc_grade  += app_Constants.TRAINING_CAMP_QB_INT_AWARD;

                    }


                }
                p.Grade += tc_grade;
            }

            return r;
        }

        public static List<Player_and_Ratings_and_Draft> TrainingCampRB(List<Player_and_Ratings_and_Draft> pList)
        {
            List<Player_and_Ratings_and_Draft> r = pList.Where(x => x.p.Pos == (int)Player_Pos.RB).ToList();

            foreach (Player_and_Ratings_and_Draft p in r)
            {
                double tc_grade = 0.0;
                for (int i = 0; i < app_Constants.TRAINING_CAMP_NUM_PLAYS; i++)
                {
                    int iRunningPower = (int)p.pr[0].Running_Power_Rating;
                    int iHands = (int)p.pr[0].Hands_Rating;
                    int iAgility = (int)p.pr[0].Agilty_Rating;
                    int iSpeed = (int)p.pr[0].Speed_Rating;

                    int iYardLine = CommonUtils.getRandomNum(1, 100);
                    int itemp9 = CommonUtils.getRandomNum(1, 100);
                    if (itemp9 <= app_Constants.TRAINING_CAMP_RB_PERCENT_RECEIVER)
                    {
                        //Execute pass play
                        int iacc = CommonUtils.getRandomNum(1, 100);
                        if (iacc <= app_Constants.TRAINING_CAMP_RB_ACCURACY_PERC)
                        {
                            int itemp7 = CommonUtils.getRandomNum(1, 100 + app_Constants.TRAINING_CAMP_RECEIVER_FUDGE);
                            if (itemp7 <= iHands)
                            {
                                //pass caught
                                tc_grade += app_Constants.TRAINING_CAMP_RECEIVER_PASS_CAUGHT;

                                //See if the player can get a TD for more points
                                int yeardsGainged = app_Constants.TRAINING_CAMP_RB_PASS_LEN;
                                int ii;
                                for (ii = iYardLine + yeardsGainged; ii < 100; ii+= app_Constants.TRAINING_CAMP_RECEIVER_YAC)
                                {
                                    int itempaa = CommonUtils.getRandomNum(1, app_Constants.TRAINING_CAMP_RECEIVER_AFTER_CATCH_FACTOR);
                                    if (itempaa > (iAgility + iSpeed))
                                        break;
                                }
                                if (ii >= 100)
                                    tc_grade += app_Constants.TRAINING_CAMP_RB_TD;
                            }
                        }
                    }
                    else
                    {
                        //Execute running play
                        int iii;
                        for (iii = iYardLine - 5; iii < 100;iii+=app_Constants.TRAINING_CAMP_RB_STRIDE)
                        {
                            //Before breaking out, a runners power running ability is important
                            if (iii < (iYardLine + app_Constants.TRAINING_CAMP_RB_POWER_BREAK_AWAY))
                            {
                                int itemp12 = CommonUtils.getRandomNum(1, 100 + app_Constants.TRAINING_CAMP_RB_POWER_BUMP) ;
                                if (itemp12 > (app_Constants.TRAINING_CAMP_RB_POWER_BUMP + iRunningPower - app_Constants.TRAINING_CAMP_RB_DELTA))
                                    break;
                            }
                            else if (iii < (iYardLine + app_Constants.TRAINING_CAMP_RB_AGILITY_BREAK_AWAY))
                            {
                                int itemp12 = CommonUtils.getRandomNum(1, 100 + app_Constants.TRAINING_CAMP_RB_POWER_BUMP) ;
                                if (itemp12 > (app_Constants.TRAINING_CAMP_RB_POWER_BUMP + iAgility - app_Constants.TRAINING_CAMP_RB_DELTA))
                                    break;
                            }
                            else
                            {
                                int itemp12 = CommonUtils.getRandomNum(1, 100 + app_Constants.TRAINING_CAMP_RB_POWER_BUMP);
                                if (itemp12 > (app_Constants.TRAINING_CAMP_RB_POWER_BUMP + iSpeed - app_Constants.TRAINING_CAMP_RB_DELTA))
                                    break;
                            }
                        }
                        if (iii < iYardLine)
                            tc_grade += app_Constants.TRAINING_CAMP_RB_RUNNING_LOSS;
                        else if ((iii - iYardLine) > app_Constants.TRAINING_CAMP_RB_GAIN_FOR_POINT)
                            tc_grade += app_Constants.TRAINING_CAMP_RB_RUNNING_PLUS5;
                        else if (iii >= 100)
                            tc_grade += app_Constants.TRAINING_CAMP_RB_TD;


                    }

                }
                p.Grade += tc_grade;
            }

            return r;
        }

        public static List<Player_and_Ratings_and_Draft> TrainingCampWR(List<Player_and_Ratings_and_Draft> pList)
        {
            List<Player_and_Ratings_and_Draft> r = pList.Where(x => x.p.Pos == (int)Player_Pos.WR).ToList();

            foreach (Player_and_Ratings_and_Draft p in r)
            {
                double tc_grade = 0.0;
                for (int i = 0; i < app_Constants.TRAINING_CAMP_NUM_PLAYS; i++)
                {
                    int iHands = (int)p.pr[0].Hands_Rating;
                    int iAgility = (int)p.pr[0].Agilty_Rating;
                    int iSpeed = (int)p.pr[0].Speed_Rating;

                    int iYardLine = CommonUtils.getRandomNum(1, 100);

                    //Did receiver get open
                    int itemprndOpen = CommonUtils.getRandomNum(1, app_Constants.TRAINING_CAMP_RECEIVER_OPEN_FUDGE);
                    if ((iAgility + iSpeed) < itemprndOpen)
                    {
                        int iacc = CommonUtils.getRandomNum(1, 100);
                        if (iacc <= app_Constants.TRAINING_CAMP_RB_ACCURACY_PERC)
                        {
                            int itemp7 = CommonUtils.getRandomNum(1, 100 + app_Constants.TRAINING_CAMP_RECEIVER_FUDGE);
                            if (itemp7 <= iHands)
                            {
                                //pass caught
                                tc_grade += app_Constants.TRAINING_CAMP_RECEIVER_PASS_CAUGHT;

                                int itemp = CommonUtils.getRandomNum(1, 100);
                                int ipassattyards = 0;
                                if (itemp <= app_Constants.TRAINING_CAMP_QB_10YRD_PASS_PERC)
                                {
                                    ipassattyards = 10;
                                }
                                else if (itemp <= app_Constants.TRAINING_CAMP_QB_20YRD_PASS_PERC)
                                {
                                    ipassattyards = 20;
                                }
                                else if (itemp <= app_Constants.TRAINING_CAMP_QB_30YRD_PASS_PERC)
                                {
                                    ipassattyards = 30;
                                }
                                else if (itemp <= app_Constants.TRAINING_CAMP_QB_40YRD_PASS_PERC)
                                {
                                    ipassattyards = 40;
                                }
                                else
                                {
                                    ipassattyards = 60;
                                }



                                //See if the player can get a TD for more points
                                int ii;
                                for (ii = iYardLine + ipassattyards; ii < 100; ii += app_Constants.TRAINING_CAMP_RECEIVER_YAC)
                                {
                                    int itempaa = CommonUtils.getRandomNum(1, app_Constants.TRAINING_CAMP_RECEIVER_AFTER_CATCH_FACTOR);
                                    if (itempaa > (iAgility + iSpeed))
                                        break;
                                }
                                if (ii >= 100)
                                    tc_grade += app_Constants.TRAINING_CAMP_RB_TD;
                            }
                        }
                    } //did receiver get open
                }  //numer of plays
                p.Grade += tc_grade;
            } //for for each player

            return r;
        }

        public static List<Player_and_Ratings_and_Draft> TrainingCampTE(List<Player_and_Ratings_and_Draft> pList)
        {
            List<Player_and_Ratings_and_Draft> r = pList.Where(x => x.p.Pos == (int)Player_Pos.TE).ToList();

            foreach (Player_and_Ratings_and_Draft p in r)
            {
                double tc_grade = 0.0;
                for (int i = 0; i < app_Constants.TRAINING_CAMP_NUM_PLAYS; i++)
                {
                    int iHands = (int)p.pr[0].Hands_Rating;
                    int iAgility = (int)p.pr[0].Agilty_Rating;
                    int iSpeed = (int)p.pr[0].Speed_Rating;
                    int iRunBlocking = (int)p.pr[0].Running_Power_Rating;
                    int iPassBlocking = (int)p.pr[0].Pass_Block_Rating;

                    int itemprunpass = CommonUtils.getRandomNum(1, 100);
                    if (itemprunpass <= app_Constants.TRAINING_CAMP_TE_BLOCK_PERCENT)
                    {
                        int itempblocktype = CommonUtils.getRandomNum(1, 100);
                        if (itempblocktype <= app_Constants.TRAINING_BLOCKER_RUN_PERCENT)
                        {
                            //Run block
                            int inttemprblock = CommonUtils.getRandomNum(1, 100 + app_Constants.TRAINING_BLOCK_FUDGE);
                            if (inttemprblock < iRunBlocking)
                                tc_grade += app_Constants.TRAINING_CAMP_BLOCKER_GOOD_RUN_BLOCK;
                        }
                        else
                        {
                            //Pass block
                            int inttemppblock = CommonUtils.getRandomNum(1, 200);
                            if (inttemppblock < (iPassBlocking + iAgility))
                                tc_grade += app_Constants.TRAINING_CAMP_BLOCKER_GOOD_PASS_BLOCK;
                        }
                    }
                    else
                    {
                        int iYardLine = CommonUtils.getRandomNum(1, 100);

                        //Did receiver get open
                        int itemprndOpen = CommonUtils.getRandomNum(1, app_Constants.TRAINING_CAMP_RECEIVER_OPEN_FUDGE);
                        if ((iAgility + iSpeed) < itemprndOpen)
                        {
                            int iacc = CommonUtils.getRandomNum(1, 100);
                            if (iacc <= app_Constants.TRAINING_CAMP_RB_ACCURACY_PERC)
                            {
                                int itemp7 = CommonUtils.getRandomNum(1, 100 + app_Constants.TRAINING_CAMP_RECEIVER_FUDGE);
                                if (itemp7 <= iHands)
                                {
                                    //pass caught
                                    tc_grade += app_Constants.TRAINING_CAMP_RECEIVER_PASS_CAUGHT;

                                    int yeardsGainged = app_Constants.TRAINING_CAMP_RB_PASS_LEN;

                                    //See if the player can get a TD for more points
                                    int ii;
                                    for (ii = iYardLine + yeardsGainged; ii < 100; ii += app_Constants.TRAINING_CAMP_RECEIVER_YAC)
                                    {
                                        int itempaa = CommonUtils.getRandomNum(1, app_Constants.TRAINING_CAMP_RECEIVER_AFTER_CATCH_FACTOR);
                                        if (itempaa > (iAgility + iSpeed))
                                            break;
                                    }
                                    if (ii >= 100)
                                        tc_grade += app_Constants.TRAINING_CAMP_RB_TD;
                                }
                            }
                        } //did receiver get open
                    } //block or receiver
                }  //numer of plays
                p.Grade += tc_grade;
            } //for for each player

            return r;
        }

        public static List<Player_and_Ratings_and_Draft> TrainingCampOL(List<Player_and_Ratings_and_Draft> pList)
        {
            List<Player_and_Ratings_and_Draft> r = pList.Where(x => x.p.Pos == (int)Player_Pos.OL).ToList();

            foreach (Player_and_Ratings_and_Draft p in r)
            {
                double tc_grade = 0.0;
                for (int i = 0; i < app_Constants.TRAINING_CAMP_NUM_PLAYS; i++)
                {
                    int iAgility = (int)p.pr[0].Agilty_Rating;
                    int iRunBlocking = (int)p.pr[0].Running_Power_Rating;
                    int iPassBlocking = (int)p.pr[0].Pass_Block_Rating;

                    int itempblocktype = CommonUtils.getRandomNum(1, 100);
                    if (itempblocktype <= app_Constants.TRAINING_BLOCKER_RUN_PERCENT)
                    {
                        //Run block
                        int inttemprblock = CommonUtils.getRandomNum(1, 100 + app_Constants.TRAINING_BLOCK_FUDGE);
                        if (inttemprblock < iRunBlocking)
                            tc_grade += app_Constants.TRAINING_CAMP_BLOCKER_GOOD_RUN_BLOCK;
                    }
                    else
                    {
                        //Pass block
                        int inttemppblock = CommonUtils.getRandomNum(1, 200);
                        if (inttemppblock < (iPassBlocking + iAgility))
                            tc_grade += app_Constants.TRAINING_CAMP_BLOCKER_GOOD_PASS_BLOCK;
                    }


                }  //numer of plays
                p.Grade += tc_grade;
            } //for for each player

            return r;
        }

        public static List<Player_and_Ratings_and_Draft> TrainingCampDL(List<Player_and_Ratings_and_Draft> pList)
        {
            List<Player_and_Ratings_and_Draft> r = pList.Where(x => x.p.Pos == (int)Player_Pos.DL).ToList();

            foreach (Player_and_Ratings_and_Draft p in r)
            {
                double tc_grade = 0.0;
                for (int i = 0; i < app_Constants.TRAINING_CAMP_NUM_PLAYS; i++)
                {

                    int iAgility = (int)p.pr[0].Agilty_Rating;
                    int iRunAttacking = (int)p.pr[0].Run_Attack_Rating;
                    int iPassAttacking = (int)p.pr[0].Pass_Attack_Rating;

                    int itempblocktype = CommonUtils.getRandomNum(1, 100);
                    if (itempblocktype <= app_Constants.TRAINING_BLOCKER_RUN_PERCENT)
                    {
                        //Run block
                        int inttemprblock = CommonUtils.getRandomNum(1, 100);
                        if (inttemprblock < iRunAttacking)
                            tc_grade += app_Constants.TRAINING_CAMP_RUSHER_GOOD_RUN_BLOCK;
                    }
                    else
                    {
                        //Pass block
                        int inttemppblock = CommonUtils.getRandomNum(1, 100 + app_Constants.TRAINING_BLOCK_FUDGE);
                        if (inttemppblock < iPassAttacking)
                            tc_grade += app_Constants.TRAINING_CAMP_RUSHER_GOOD_PASS_BLOCK;
                    }


                }  //numer of plays
                p.Grade += tc_grade;
            } //for for each player

            return r;
        }

        public static List<Player_and_Ratings_and_Draft> TrainingCampLB(List<Player_and_Ratings_and_Draft> pList)
        {
            List<Player_and_Ratings_and_Draft> r = pList.Where(x => x.p.Pos == (int)Player_Pos.LB).ToList();

            foreach (Player_and_Ratings_and_Draft p in r)
            {
                double tc_grade = 0.0;
                for (int i = 0; i < app_Constants.TRAINING_CAMP_NUM_PLAYS; i++)
                {
                    int iHands = (int)p.pr[0].Hands_Rating;
                    int iAgility = (int)p.pr[0].Agilty_Rating;
                    int iSpeed = (int)p.pr[0].Speed_Rating;
                    int iRunAttacking = (int)p.pr[0].Run_Attack_Rating;
                    int iPassAttacking = (int)p.pr[0].Pass_Attack_Rating;

                    int itemprunpass = CommonUtils.getRandomNum(1, 100);
                    if (itemprunpass <= app_Constants.TRAINING_CAMP_LB_BLOCK_PERCENT)
                    {
                        int itempblocktype = CommonUtils.getRandomNum(1, 100);
                        if (itempblocktype <= app_Constants.TRAINING_BLOCKER_RUN_PERCENT)
                        {
                            //Run block
                            int inttemprblock = CommonUtils.getRandomNum(1, 100 + app_Constants.TRAINING_BLOCK_FUDGE);
                            if (inttemprblock < iRunAttacking)
                                tc_grade += app_Constants.TRAINING_CAMP_RUSHER_GOOD_RUN_BLOCK;
                        }
                        else
                        {
                            //Pass block
                            int inttemppblock = CommonUtils.getRandomNum(1, 200);
                            if (inttemppblock < (iPassAttacking + iAgility))
                                tc_grade += app_Constants.TRAINING_CAMP_RUSHER_GOOD_PASS_BLOCK;
                        }
                    }
                    else
                    {
                        int iYardLine = CommonUtils.getRandomNum(1, 100);

                        //Did LB cover receiver
                        int itemprndOpen = CommonUtils.getRandomNum(1, app_Constants.TRAINING_CAMP_RECEIVER_OPEN_FUDGE);
                        if (itemprndOpen <= (iAgility + iSpeed)  )
                        {
                            tc_grade += app_Constants.TRAINING_CAMP_COVER_RECEIVER;
                            int iTempInt = CommonUtils.getRandomNum(1, 100 + app_Constants.TRAINING_CAMP_LB_FUDGE);
                            if (iTempInt <= iHands)
                                tc_grade += app_Constants.TRAINING_CAMP_COVER_int;

                        } 
                        else
                        {
                            int yeardsGainged = app_Constants.TRAINING_CAMP_RB_PASS_LEN;

                            //See if the player can get a TD then the lb will lose points
                            int ii;
                            for (ii = iYardLine + yeardsGainged; ii < 100; ii += app_Constants.TRAINING_CAMP_RECEIVER_YAC)
                            {
                                int itempaa = CommonUtils.getRandomNum(1, app_Constants.TRAINING_CAMP_RECEIVER_AFTER_CATCH_FACTOR);
                                if (itempaa > (iAgility + iSpeed))
                                    break;
                            }
                            if (ii >= 100)
                                tc_grade += app_Constants.TRAINING_CAMP_COVER_SURRENDER_TD;

                        } //did receiver get open
                    } //block or receiver
                }  //numer of plays
                p.Grade += tc_grade;
            } //for for each player

            return r;
        }

        public static List<Player_and_Ratings_and_Draft> TrainingCampDB(List<Player_and_Ratings_and_Draft> pList)
        {
            List<Player_and_Ratings_and_Draft> r = pList.Where(x => x.p.Pos == (int)Player_Pos.DB).ToList();

            foreach (Player_and_Ratings_and_Draft p in r)
            {
                double tc_grade = 0.0;
                for (int i = 0; i < app_Constants.TRAINING_CAMP_NUM_PLAYS; i++)
                {
                    int iHands = (int)p.pr[0].Hands_Rating;
                    int iAgility = (int)p.pr[0].Agilty_Rating;
                    int iSpeed = (int)p.pr[0].Speed_Rating;


                        int iYardLine = CommonUtils.getRandomNum(1, 100);

                        //Did CB cover receiver
                        int itemprndOpen = CommonUtils.getRandomNum(1, app_Constants.TRAINING_CAMP_RECEIVER_OPEN_FUDGE);
                        if (itemprndOpen <= (iAgility + iSpeed))
                        {
                            tc_grade += app_Constants.TRAINING_CAMP_COVER_RECEIVER;
                            int iTempInt = CommonUtils.getRandomNum(1, 100 + app_Constants.TRAINING_CAMP_LB_FUDGE);
                            if (iTempInt <= iHands)
                                tc_grade += app_Constants.TRAINING_CAMP_COVER_int;

                        }
                        else
                        {

                            //See if the player can get a TD then the CB will lose points
                        int itemp = CommonUtils.getRandomNum(1, 100);
                        int ipassattyards = 0;
                        if (itemp <= app_Constants.TRAINING_CAMP_QB_10YRD_PASS_PERC)
                        {
                            ipassattyards = 10;
                        }
                        else if (itemp <= app_Constants.TRAINING_CAMP_QB_20YRD_PASS_PERC)
                        {
                            ipassattyards = 20;
                        }
                        else if (itemp <= app_Constants.TRAINING_CAMP_QB_30YRD_PASS_PERC)
                        {
                            ipassattyards = 30;
                        }
                        else if (itemp <= app_Constants.TRAINING_CAMP_QB_40YRD_PASS_PERC)
                        {
                            ipassattyards = 40;
                        }
                        else
                        {
                            ipassattyards = 60;
                        }

                        int ii;
                            for (ii = iYardLine + ipassattyards; ii < 100; ii += app_Constants.TRAINING_CAMP_RECEIVER_YAC)
                            {
                                int itempaa = CommonUtils.getRandomNum(1, app_Constants.TRAINING_CAMP_RECEIVER_AFTER_CATCH_FACTOR);
                                if ((iAgility + iSpeed) <= itempaa)
                                    break;
                            }
                            if (ii >= 100)
                                tc_grade += app_Constants.TRAINING_CAMP_COVER_SURRENDER_TD;

                        } //did receiver get open
                }  //numer of plays
                p.Grade += tc_grade;
            } //for for each player

            return r;
        }

        public static List<Player_and_Ratings_and_Draft> TrainingCampFG(List<Player_and_Ratings_and_Draft> pList)
        {
            List<Player_and_Ratings_and_Draft> r = pList.Where(x => x.p.Pos == (int)Player_Pos.K).ToList();

            foreach (Player_and_Ratings_and_Draft p in r)
            {
                double tc_grade = 0.0;
                for (int i = 0; i < app_Constants.TRAINING_CAMP_NUM_PLAYS; i++)
                {
                    int iLegStrength = (int)p.pr[0].Kicker_Leg_Power_Rating;
                    int iLegAccuracy = (int)p.pr[0].Kicker_Leg_Accuracy_Rating;

                    int iYardLine = CommonUtils.getRandomNum(1, app_Constants.TRAINING_CAMP_FG_FURTHEST_YRD);
                    iYardLine += app_Constants.TRAINING_CAMP_FG_EXTRA_YEARDS;

                    Double accuracy_factor = 1.0;
                    if (iYardLine >= 30)
                        accuracy_factor = 1.2;
                    else if (iYardLine >= 40)
                        accuracy_factor = 1.5;
                    else if (iYardLine >= 50)
                        accuracy_factor = 2.0;
                    else
                        accuracy_factor = 3.0;

                    iLegAccuracy = (int)(iLegAccuracy / accuracy_factor);

                    int itempFG = CommonUtils.getRandomNum(1, 100 + app_Constants.TRAINING_CAMP_FG_FUDGE);

                    if (itempFG <= iLegAccuracy)
                        tc_grade += app_Constants.TRAINING_CAMP_FG_AWARD;

                }  //numer of plays
                p.Grade += tc_grade;
            } //for for each player

            return r;
        }

        public static List<Player_and_Ratings_and_Draft> TrainingCampP(List<Player_and_Ratings_and_Draft> pList)
        {
            List<Player_and_Ratings_and_Draft> r = pList.Where(x => x.p.Pos == (int)Player_Pos.P).ToList();

            foreach (Player_and_Ratings_and_Draft p in r)
            {
                double tc_grade = 0.0;
                for (int i = 0; i < app_Constants.TRAINING_CAMP_NUM_PLAYS; i++)
                {
                    int iLegStrength = (int)p.pr[0].Kicker_Leg_Power_Rating;
                    int iLegAccuracy = (int)p.pr[0].Kicker_Leg_Accuracy_Rating;

                    int iYardLine = CommonUtils.getRandomNum(1, app_Constants.TRAINING_CAMP_P_COFFIN_YRD);
                    int Punt_length = app_Constants.TRAINING_CAMP_P_MIN_PUNT;

                    for (int z = 0; z < 6; z++)
                    {
                        int itempP = CommonUtils.getRandomNum(1, 100 + app_Constants.TRAINING_CAMP_P_FUDGE);
                        if (itempP > iLegStrength)
                            break;

                        Punt_length += app_Constants.TRAINING_CAMP_P_YARD_INC;

                        if (iYardLine - Punt_length < 7)
                        {
                            int iCofficN = CommonUtils.getRandomNum(1, 100 + app_Constants.TRAINING_CAMP_P_FUDGE);
                            if (iCofficN <= iLegAccuracy)
                                tc_grade += app_Constants.TRAINING_CAMP_P_COFFIN_CORNER_AWARD;
                            break;
                        }
                    }

                    double dd = (Punt_length = app_Constants.TRAINING_CAMP_P_MIN_PUNT) * 0.1;
                    tc_grade += dd;

                }  //numer of plays
                p.Grade += tc_grade;
            } //for for each player

            return r;
        }

        public static void AdjustGradeforAge(List<Player_and_Ratings_and_Draft> tcPlayers)
        {
            foreach (Player_and_Ratings_and_Draft p in tcPlayers)
            {
                Double new_grade = p.Grade;
                new_grade -= p.p.Age - app_Constants.STARTING_ROOKIE_AGE;
                p.Grade = new_grade;
            }
        }
            
        
    }
}
