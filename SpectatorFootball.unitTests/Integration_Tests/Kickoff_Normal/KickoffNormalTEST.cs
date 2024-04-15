using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpectatorFootball.Enum;
using SpectatorFootball.GameNS;
using SpectatorFootball.unitTests.Helper_ClassesNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.IntegrationTests.Kickoff_Normal
{
    [TestClass]
    public class KickoffNormalTEST
    {
        [TestCategory("Normal_Kickoff")]
        [TestMethod]
        public void Normal_Kickoff_Avgs()
        {
            List<Game_Player> Kickoff_Players = null;
            List<Game_Player> Receiving_Players = null;

            int at = 11;
            int ht = 22;
            int possess_team = 0;

            int L_num_returned = 0;
            int L_num_TDs = 0;
            int L_num_out_of_EZ = 0;
            int L_num_kneel_down = 0;
            int L_num_fumble = 0;
            int L_num_fumble_lost = 0;
            int L_num_40plus_non_TD = 0;
            int L_num_run_out_of_bound = 0;
            double L_total_starting_yardline = 0.0;

            int R_num_returned = 0;
            int R_num_TDs = 0;
            int R_num_out_of_EZ = 0;
            int R_num_kneel_down = 0;
            int R_num_fumble = 0;
            int R_num_fumble_lost = 0;
            int R_num_40plus_non_TD = 0;
            int R_num_run_out_of_bound = 0;
            double R_total_starting_yardline = 0.0;

            double g_yardline = 0.0;
            Play_Enum pe = Play_Enum.KICKOFF_NORMAL;


            int num_plays = 2000;
            for (int i = 0; i < num_plays; i++)
            {
                bool bLefttoRight;
                if (i % 2 == 0)
                {
                    bLefttoRight = true;
                    g_yardline = 35.0;
                    possess_team = at;
                }
                else
                {
                    bLefttoRight = false;
                    g_yardline = 65.0;
                    possess_team = ht;
                }

                double PossessionAdjuster = Game_Engine_Helper.HorizontalAdj(bLefttoRight);
                Formation KickForm = Game_Helper.getFormation(Formations_Enum.KICKOFF_REGULAR_KICK, PossessionAdjuster);
                Formation RecForm = Game_Helper.getFormation(Formations_Enum.KICKOFF_REGULAR_RECEIVE, PossessionAdjuster);

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

                Play_Kickoff_Normal kickoff = new Play_Kickoff_Normal(possess_team, at, ht, gb, Kickoff_Players, Receiving_Players, bLefttoRight, false, true, false);
                kickoff.init();
                Play_Result pResult = kickoff.Execute();

                if (bLefttoRight)
                {
                    if (pResult.bKick_Out_of_Endzone) L_num_out_of_EZ++;
                    if (pResult.bTouchback && !pResult.bKick_Out_of_Endzone) L_num_kneel_down++;
                    if (!pResult.bTouchback) L_num_returned++;
                    if (pResult.bTouchDown) L_num_TDs++;
                    if (pResult.bFumble) L_num_fumble++;
                    if (pResult.bFumble_Lost) L_num_fumble_lost++;
                    if (pResult.bRunOutofBounds) L_num_run_out_of_bound++;
                    if (!pResult.bTouchDown && pResult.Yards_Returned >= 40) L_num_40plus_non_TD++;
                    if (!pResult.bTouchback && !pResult.bTouchDown) L_total_starting_yardline += (100.0 - pResult.Returner.Current_YardLine);
                }
                else
                {
                    if (pResult.bKick_Out_of_Endzone) R_num_out_of_EZ++;
                    if (pResult.bTouchback && !pResult.bKick_Out_of_Endzone) R_num_kneel_down++;
                    if (!pResult.bTouchback) R_num_returned++;
                    if (pResult.bTouchDown) R_num_TDs++;
                    if (pResult.bFumble) R_num_fumble++;
                    if (pResult.bFumble_Lost) R_num_fumble_lost++;
                    if (pResult.bRunOutofBounds) R_num_run_out_of_bound++;
                    if (!pResult.bTouchDown && pResult.Yards_Returned >= 40) R_num_40plus_non_TD++;
                    if (!pResult.bTouchback && !pResult.bTouchDown) R_total_starting_yardline += pResult.Returner.Current_YardLine;

                }

            }

string left_string = "Left Returns: " + L_num_returned + " kneel downs: " + L_num_kneel_down + " thru endzone: " + L_num_out_of_EZ +
                " TDs: " + L_num_TDs + " Fumbles: " + L_num_fumble + " Fumbles Lost: " + L_num_fumble_lost + " Run out of Bounds: " + L_num_run_out_of_bound + 
                " 40+ yards returns: " + L_num_40plus_non_TD + " AVG starting YL: " + L_total_starting_yardline / (L_num_returned- L_num_TDs) +
                " Right Returns: " + R_num_returned + " kneel downs: " + R_num_kneel_down + " thru endzone: " + R_num_out_of_EZ +
                " TDs: " + R_num_TDs + " Fumbles: " + R_num_fumble + " Fumbles Lost: " + R_num_fumble_lost + " Run out of Bounds: " + R_num_run_out_of_bound +
                " 40+ yards returns: " + R_num_40plus_non_TD + " AVG starting YL: " + R_total_starting_yardline / (R_num_returned - R_num_TDs);

            Assert.IsTrue(true);

        }

    }
}
