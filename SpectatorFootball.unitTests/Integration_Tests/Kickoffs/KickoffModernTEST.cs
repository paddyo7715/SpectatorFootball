using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpectatorFootball.Enum;
using SpectatorFootball.GameNS;
using SpectatorFootball.PlayNS;
using SpectatorFootball.unitTests.Helper_ClassesNS;

namespace SpectatorFootball.Integration_Tests.Kickoff
{
    [TestClass]
    public class KickoffModernTEST
    {
        [TestCategory("Integration")]
        [TestMethod]
        public void Normal_Kickoff_Avgs()
        {
            List<Game_Player> Kickoff_Players = null;
            List<Game_Player> Receiving_Players = null;

            int at = 11;
            int ht = 22;
            int possess_team = 0;

            int L_test_counter = 0;

            int L_num_returned = 0;
            int L_num_TDs = 0;
            int L_num_out_of_EZ = 0;
            int L_num_kneel_down = 0;
            int L_num_fumble = 0;
            int L_num_fumble_lost = 0;
            int L_num_40plus_non_TD = 0;
            int L_num_run_out_of_bound = 0;
            double L_total_starting_yardline = 0.0;
            double L_avg_return = 0.0;

            int R_num_returned = 0;
            int R_num_TDs = 0;
            int R_num_out_of_EZ = 0;
            int R_num_kneel_down = 0;
            int R_num_fumble = 0;
            int R_num_fumble_lost = 0;
            int R_num_40plus_non_TD = 0;
            int R_num_run_out_of_bound = 0;
            double R_total_starting_yardline = 0.0;
            double R_avg_return = 0.0;

            double g_yardline = 0.0;
            Play_Enum pe = Play_Enum.KICKOFF_NORMAL;

            int bad_players = 0;
            int bad_plays = 0;

            int num_plays = 20000;
            for (int i = 0; i < num_plays; i++)
            {
                bool bLefttoRight;
                if (i % 2 == 0)
                {
                    bLefttoRight = true;
                    g_yardline = 25.0;
                    possess_team = at;
                }
                else
                {
                    bLefttoRight = false;
                    g_yardline = 75.0;
                    possess_team = ht;
                }

                double PossessionAdjuster = Game_Engine_Helper.HorizontalAdj(bLefttoRight);
                Formation KickForm = formation_helper.getFormation(Formations_Enum.KICKOFF_MODERN_KICK, PossessionAdjuster);
                Formation RecForm = formation_helper.getFormation(Formations_Enum.KICKOFF_MODERN_RECEIVE, PossessionAdjuster);

                Kickoff_Players = Help_Class.setGamePlayerLIsts(11, g_yardline, KickForm);
                Receiving_Players = Help_Class.setGamePlayerLIsts(22, g_yardline, RecForm);

                Game_Ball gb = new Game_Ball()
                {
                    State = Ball_States.TEED_UP,
                    Initial_State = Ball_States.TEED_UP,
                    Current_Vertical_Percent_Pos = 50.0,
                    Current_YardLine = g_yardline,
                    Starting_Vertical_Percent_Pos = 50.0,
                    Starting_YardLine = g_yardline
                };

                Play_Kickoff_Classic kickoff = new Play_Kickoff_Classic(KickForm, RecForm, possess_team, at, ht, gb, Kickoff_Players, Receiving_Players, bLefttoRight, false);
                Play_Result pResult = kickoff.Execute(false);

                bad_players = Play_Validator.testPlayerStage_Irregularities(gb, Kickoff_Players, Receiving_Players);
                if (bad_players > 0) bad_players = 1;

                if (bad_players == 1)
                    bad_plays++;

                string Play_Result_Validation = Play_Validator.Validate_Play_Result(Play_Enum.KICKOFF_NORMAL, Kickoff_Players, Receiving_Players, pResult, gb, bLefttoRight);
                if (Play_Result_Validation != null)
                    throw new Exception(Play_Result_Validation);

                string Play_Stats_Validation = Play_Validator.Validate_Punt_play_stats(Play_Enum.KICKOFF_NORMAL, Kickoff_Players, Receiving_Players, pResult);
                if (Play_Stats_Validation != null)
                    throw new Exception(Play_Stats_Validation);

                if (bLefttoRight)
                {
                    L_test_counter += pResult.test_counter;

                    if (pResult.bKick_Out_of_Endzone) L_num_out_of_EZ++;
                    if (pResult.bTouchback && !pResult.bKick_Out_of_Endzone) L_num_kneel_down++;
                    if (pResult.bKick_Returned) L_num_returned++;
                    if (!pResult.bTouchback) L_avg_return += pResult.Yards_Returned;
                    if (pResult.bTouchDown) L_num_TDs++;
                    if (pResult.bFumble) L_num_fumble++;
                    if (pResult.bFumble_Lost) L_num_fumble_lost++;
                    if (!pResult.bTouchback && !pResult.bTouchDown) L_total_starting_yardline += (100.0 - pResult.Returner.Current_YardLine);
                }
                else
                {
                    if (pResult.bKick_Out_of_Endzone) R_num_out_of_EZ++;
                    if (pResult.bTouchback && !pResult.bKick_Out_of_Endzone) R_num_kneel_down++;
                    if (pResult.bKick_Returned) R_num_returned++;
                    if (!pResult.bTouchback) R_avg_return += pResult.Yards_Returned;
                    if (pResult.bTouchDown) R_num_TDs++;
                    if (pResult.bFumble) R_num_fumble++;
                    if (pResult.bFumble_Lost) R_num_fumble_lost++;
                    if (!pResult.bTouchback && !pResult.bTouchDown) R_total_starting_yardline += pResult.Returner.Current_YardLine;

                }

            }

            double dd = L_avg_return / L_num_returned;

            //Note because the players used for this test case are average players and not the same as players that make the team and start
            //in a regualr game, these ranges will be pretty wide.
            //Actual ranges for when actaul players play:
            // 88% of kickoffs should be returned
            // 27 yards average return.
            // 24 TDs for every 1000 returns
            // 140 fumbles for every 1000 returns

            if (bad_plays > 0)
                throw new Exception("Too many bad plays " + bad_plays);

            if (L_num_returned < 8400 || L_num_returned > 9200)
                throw new Exception("L_num_returns out of range " + L_num_returned);
            if (L_num_kneel_down < 800 || L_num_kneel_down > 1600)
                throw new Exception("L_num_kneel_down out of range " + L_num_kneel_down);
            if (L_num_out_of_EZ != 0)
                throw new Exception("L_num_out_of_EZ out of range " + L_num_out_of_EZ);
            if (L_num_TDs < 14 || L_num_TDs > 40)
                throw new Exception("L_num_TDs out of range " + L_num_TDs);
            if (L_num_fumble < 80 || L_num_fumble > 200)
                throw new Exception("L_num_fumble out of range " + L_num_fumble);
            if (L_num_fumble_lost > 200)
                throw new Exception("L_num_fumble_lost out of range " + L_num_fumble_lost);
            if (L_total_starting_yardline / (L_num_returned - L_num_TDs) < 20.0 || L_total_starting_yardline / (L_num_returned - L_num_TDs) > 35.0)
                throw new Exception("L_AVG starting YL: out of range " + L_total_starting_yardline / (L_num_returned - L_num_TDs));
            if ((L_avg_return / L_num_returned) < 20.0 || (L_avg_return / L_num_returned) > 35.0)
                throw new Exception("L_AVG Return:: out of range " + (L_avg_return / L_num_returned));


            if (R_num_returned < 8400 || R_num_returned > 9200)
                throw new Exception("R_num_returns out of range " + R_num_returned);
            if (R_num_kneel_down < 800 || R_num_kneel_down > 1600)
                throw new Exception("R_num_kneel_down out of range " + R_num_kneel_down);
            if (R_num_out_of_EZ != 0)
                throw new Exception("R_num_out_of_EZ out of range " + R_num_out_of_EZ);
            if (R_num_TDs < 14 || R_num_TDs > 40)
                throw new Exception("R_num_TDs out of range " + R_num_TDs);
            if (R_num_fumble < 80 || R_num_fumble > 200)
                throw new Exception("R_num_fumble out of range " + R_num_fumble);
            if (R_num_fumble_lost > 200)
                throw new Exception("R_num_fumble_lost out of range " + R_num_fumble_lost);
            if (R_total_starting_yardline / (R_num_returned - R_num_TDs) < 20.0 || R_total_starting_yardline / (R_num_returned - R_num_TDs) > 35.0)
                throw new Exception("R_AVG starting YL: out of range " + R_total_starting_yardline / (R_num_returned - R_num_TDs));
            if ((R_avg_return / R_num_returned) < 20.0 || (R_avg_return / R_num_returned) > 35.0)
                throw new Exception("R_AVG Return:: out of range " + (R_avg_return / R_num_returned));

            Assert.IsTrue(true);

        }
    }
}
