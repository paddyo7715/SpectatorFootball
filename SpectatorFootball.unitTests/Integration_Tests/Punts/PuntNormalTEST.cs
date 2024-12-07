using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpectatorFootball.Enum;
using SpectatorFootball.GameNS;
using SpectatorFootball.PlayNS;
using SpectatorFootball.unitTests.Helper_ClassesNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.unitTests.Integration_Tests.Punts
{
    [TestClass]
    public class PuntNormalTEST
    {
        [TestCategory("Integration")]
        [TestMethod]
        public void Normal_Punt()
        {
            List<Game_Player> Punt_Players = null;
            List<Game_Player> Receiving_Players = null;

            int at = 1;
            int ht = 22;
            int possess_team = 0;

            int L_test_counter = 0;

            int L_num_returned = 0;
            int L_num_TDs = 0;
            int L_num_out_of_EZ = 0;
            int L_num_kneel_down = 0;
            int L_num_fumble = 0;
            int L_num_fumble_lost = 0;
            int L_Defender_close_toKicker = 0;
            int L_Blocked_Punts = 0;
            double L_avg_return = 0.0;
            int L_cc_attempt = 0;
            int L_cc_made = 0;

            int R_num_returned = 0;
            int R_num_TDs = 0;
            int R_num_out_of_EZ = 0;
            int R_num_kneel_down = 0;
            int R_num_fumble = 0;
            int R_num_fumble_lost = 0; 
            int R_Defender_close_toKicker = 0;
            int R_Blocked_Punts = 0;
            double R_avg_return = 0.0;
            int R_cc_attempt = 0;
            int R_cc_made = 0;

            double g_yardline = 0.0;

            int num_plays = 20000;
            int icount = num_plays / 2;
            for (int i = 0; i < num_plays; i++)
            {
                bool bLefttoRight;
                if (i % 2 == 0)
                {
                    bLefttoRight = true;
                    g_yardline = 20.0;
                    possess_team = at;
                }
                else
                {
                    bLefttoRight = false;
                    g_yardline = 80.0;
                    possess_team = ht;
                }

                double PossessionAdjuster = Game_Engine_Helper.HorizontalAdj(bLefttoRight);
                Formation Punt_Forn = formation_helper.getFormation(Formations_Enum.PUNT, PossessionAdjuster);
                Formation Punt_Ret_Form = formation_helper.getFormation(Formations_Enum.PUNT_RETURN, PossessionAdjuster);

                Punt_Players = Help_Class.setGamePlayerLIsts(1, g_yardline, Punt_Forn);
                Receiving_Players = Help_Class.setGamePlayerLIsts(22, g_yardline, Punt_Ret_Form);

                Game_Ball gb = new Game_Ball()
                {
                    State = Ball_States.TEED_UP,
                    Initial_State = Ball_States.TEED_UP,
                    Current_Vertical_Percent_Pos = 50.0,
                    Current_YardLine = g_yardline,
                    Starting_Vertical_Percent_Pos = 50.0,
                    Starting_YardLine = g_yardline
                };

                Play_Punt Punt = new Play_Punt(Punt_Forn, Punt_Ret_Form, possess_team, at, ht, gb, Punt_Players, Receiving_Players, bLefttoRight, false, false);
                Play_Result pResult = Punt.Execute(false);

                string Play_Result_Validation = Play_Validator.Validate_Play_Result(Play_Enum.PUNT, Punt_Players, Receiving_Players, pResult, gb, bLefttoRight);
                if (Play_Result_Validation != null)
                    throw new Exception(Play_Result_Validation);

                string Play_Stats_Validation = Play_Validator.Validate_Punt_play_stats(Play_Enum.PUNT, Punt_Players, Receiving_Players, pResult);
                if (Play_Stats_Validation != null)
                    throw new Exception(Play_Stats_Validation);

                if (bLefttoRight)
                {
                    L_test_counter += pResult.test_counter;

                    if (pResult.bPunt_Out_of_Endzone) L_num_out_of_EZ++;
                    if (pResult.bPunt_KneelDown) L_num_kneel_down++;
                    if (pResult.bPunt_Returned) L_num_returned++;
                    if (pResult.bPunt_Returned) L_avg_return += pResult.Yards_Returned;
                    if (pResult.bTouchDown) L_num_TDs++;
                    if (pResult.bFumble) L_num_fumble++;
                    if (pResult.bFumble_Lost) L_num_fumble_lost++;
                    if (pResult.Defender_Close_to_Kicker != null) L_Defender_close_toKicker++;
                    if (pResult.bPunt_blocked) L_Blocked_Punts++;
                    if (pResult.bCoffinCornerAttemt) L_cc_attempt++;
                    if (pResult.bCoffinCornerMade) L_cc_made++;
                }
                else
                {
                    if (pResult.bPunt_Out_of_Endzone) R_num_out_of_EZ++;
                    if (pResult.bPunt_KneelDown) R_num_kneel_down++;
                    if (pResult.bPunt_Returned) R_num_returned++;
                    if (pResult.bPunt_Returned) R_avg_return += pResult.Yards_Returned;
                    if (pResult.bTouchDown) R_num_TDs++;
                    if (pResult.bFumble) R_num_fumble++;
                    if (pResult.bFumble_Lost) R_num_fumble_lost++;
                    if (pResult.Defender_Close_to_Kicker != null) R_Defender_close_toKicker++;
                    if (pResult.bPunt_blocked) R_Blocked_Punts++;
                    if (pResult.bCoffinCornerAttemt) R_cc_attempt++;
                    if (pResult.bCoffinCornerMade) R_cc_made++;

                }

            }



            double L_avg = (double)L_avg_return / (double) L_num_returned;

            if (L_num_returned != icount - L_Blocked_Punts)
                throw new Exception("L_num_returns out of range " + L_num_returned);
            if (L_num_kneel_down != 0)
                throw new Exception("L_num_kneel_down out of range " + L_num_kneel_down);
            if (L_num_out_of_EZ != 0)
                throw new Exception("L_num_out_of_EZ out of range " + L_num_out_of_EZ);
            if (L_num_TDs < L_num_returned * .0015 || L_num_TDs > L_num_returned * .01)
                throw new Exception("L_num_TDs out of range " + L_num_TDs);
            if (L_num_fumble < L_num_returned * .006 || L_num_fumble > L_num_returned * .05)
                throw new Exception("L_num_fumble out of range " + L_num_fumble); if (L_num_fumble_lost > L_num_fumble) if (L_num_fumble_lost > L_num_fumble)
                throw new Exception("L_num_fumble_lost out of range " + L_num_fumble_lost);
            if (L_avg <= 7.0 || L_avg  > 15.0)
                throw new Exception("L_AVG Return:: out of range " + (L_avg_return / L_num_returned));
            if (L_Defender_close_toKicker < icount * .03 || L_Defender_close_toKicker > icount * .08)
                throw new Exception("L_Defender_close_toKicker:: out of range " + L_Defender_close_toKicker);
            if (L_Blocked_Punts < icount * .003 || L_Blocked_Punts > icount * .015)
                throw new Exception("L_Blocked_Punts:: out of range " + L_Blocked_Punts);
            if (L_cc_attempt != 0)
                throw new Exception("L_cc_attempt:: out of range " + L_cc_attempt);
            if (L_cc_made != 0)
                throw new Exception("L_cc_made:: out of range " + L_cc_made);

            double R_avg = (double)R_avg_return / (double)R_num_returned;

            if (R_num_returned != icount - R_Blocked_Punts)
                throw new Exception("R_num_returns out of range " + R_num_returned);
            if (R_num_kneel_down != 0)
                throw new Exception("R_num_kneel_down out of range " + R_num_kneel_down);
            if (R_num_out_of_EZ != 0)
                throw new Exception("R_num_out_of_EZ out of range " + R_num_out_of_EZ);
            if (R_num_TDs < R_num_returned * .0015 || R_num_TDs > R_num_returned * .01)
                throw new Exception("R_num_TDs out of range " + R_num_TDs);
            if (R_num_fumble < R_num_returned * .006 || R_num_fumble > R_num_returned * .05)
                throw new Exception("R_num_fumble out of range " + R_num_fumble);
            if (R_num_fumble_lost > R_num_fumble)
                throw new Exception("R_num_fumble_lost out of range " + R_num_fumble_lost);
            if (R_avg <= 7.0 || R_avg > 15.0)
                throw new Exception("R_AVG Return:: out of range " + (R_avg_return / R_num_returned));
            if (R_Defender_close_toKicker < icount * .03 || R_Defender_close_toKicker > icount * .08)
                throw new Exception("R_Defender_close_toKicker:: out of range " + R_Defender_close_toKicker);
            if (R_Blocked_Punts < icount * .003 || R_Blocked_Punts > icount * .015)
                throw new Exception("R_Blocked_Punts:: out of range " + R_Blocked_Punts);
            if (R_cc_attempt != 0)
                throw new Exception("R_cc_attempt:: out of range " + R_cc_attempt);
            if (R_cc_made != 0)
                throw new Exception("R_cc_made:: out of range " + R_cc_made);

            Assert.IsTrue(true);

        }
    }
}
