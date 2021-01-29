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
            List<Player_and_Ratings_and_Draft> r = new List<Player_and_Ratings_and_Draft>();

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
            }

            return r;
        }

        public static List<Player_and_Ratings_and_Draft> TrainingCampRB(List<Player_and_Ratings_and_Draft> pList)
        {
            List<Player_and_Ratings_and_Draft> r = new List<Player_and_Ratings_and_Draft>();

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
                                for (ii = iYardLine; ii < 100; ii+= yeardsGainged)
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
            }

            return r;
        }

        public static List<Player_and_Ratings_and_Draft> TrainingCampWR(List<Player_and_Ratings_and_Draft> pList)
        {
            List<Player_and_Ratings_and_Draft> r = new List<Player_and_Ratings_and_Draft>();

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

                                //See if the player can get a TD for more points
                                int yeardsGainged = app_Constants.TRAINING_CAMP_RB_PASS_LEN;
                                int ii;
                                for (ii = iYardLine; ii < 100; ii += yeardsGainged)
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

            } //for for each player

            return r;
        }

        public static List<Player_and_Ratings_and_Draft> TrainingCampTE(List<Player_and_Ratings_and_Draft> pList)
        {
            List<Player_and_Ratings_and_Draft> r = new List<Player_and_Ratings_and_Draft>();

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
                        if (itempblocktype <= app_Constants.TRAINING_CAMP_TE_BLOCK_RECEIVER_PERCENT)
                        {
                            //Run block
                            int inttemprblock = CommonUtils.getRandomNum(1, 100);
                            if (inttemprblock < iRunBlocking)
                                tc_grade += app_Constants.TRAINING_CAMP_TE_GOOD_RUN_BLOCK;
                        }
                        else
                        {
                            //Pass block
                            int inttemppblock = CommonUtils.getRandomNum(1, 100);
                            if (inttemppblock < iPassBlocking)
                                tc_grade += app_Constants.TRAINING_CAMP_TE_GOOD_PASS_BLOCK;
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

                                    //See if the player can get a TD for more points
                                    int yeardsGainged = app_Constants.TRAINING_CAMP_RB_PASS_LEN;
                                    int ii;
                                    for (ii = iYardLine; ii < 100; ii += yeardsGainged)
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

            } //for for each player

            return r;
        }


    }
}
