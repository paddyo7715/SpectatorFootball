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

namespace SpectatorFootball.Integration_Tests.Kickoff
{
    [TestClass]
    public class KickoffOnsideTEST
    {
        [TestCategory("Integration")]
        [TestMethod]
        public void Onside_Kickoff_Avgs()
        {
            List<Game_Player> Kickoff_Players = null;
            List<Game_Player> Receiving_Players = null;
            int bad_plays = 0;

            int at = 11;
            int ht = 22;
            int possess_team = 0;

            int L_num_att = 0;
            int L_num_made = 0;
            int L_bOnside_Muffed = 0;

            int R_num_att = 0;
            int R_num_made = 0;
            int R_bOnside_Muffed = 0;

            double g_yardline = 0.0;
            Play_Enum pe = Play_Enum.KICKOFF_ONSIDES;

            int bad_players = 0;

            int num_plays = 20000;
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
                Formation KickForm = formation_helper.getFormation(Formations_Enum.KICKOFF_ONSIDE_KICK, PossessionAdjuster);
                Formation RecForm = formation_helper.getFormation(Formations_Enum.KICKOFF_ONSIDE_RECEIVE, PossessionAdjuster);

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

                Play_Kickoff_Onsides kickoff = new Play_Kickoff_Onsides(KickForm, RecForm, possess_team, at, ht, gb, Kickoff_Players, Receiving_Players, bLefttoRight, false, false);
                Play_Result pResult = kickoff.Execute(false);

                bad_players = Play_Validator.testPlayerStage_Irregularities(gb, Kickoff_Players, Receiving_Players);
                if (bad_players > 0) bad_players = 1;

                if (bad_players == 1)
                    bad_plays++;

                string Play_Result_Validation = Play_Validator.Validate_Play_Result(Play_Enum.KICKOFF_ONSIDES, Kickoff_Players, Receiving_Players, pResult, gb, bLefttoRight);
                if (Play_Result_Validation != null)
                    throw new Exception(Play_Result_Validation);

                string Play_Stats_Validation = Play_Validator.Validate_Punt_play_stats(Play_Enum.KICKOFF_ONSIDES, Kickoff_Players, Receiving_Players, pResult);
                if (Play_Stats_Validation != null)
                    throw new Exception(Play_Stats_Validation);

                if (bLefttoRight)
                {
                    L_num_att++;
                    if (pResult.bOnside_Muffed) L_bOnside_Muffed++;
                    if (pResult.bOnsideMade) L_num_made++;
                }
                else
                {
                    R_num_att++;
                    if (pResult.bOnside_Muffed) R_bOnside_Muffed++;
                    if (pResult.bOnsideMade) R_num_made++;
                }

            }

            //Actual onside stats should be 4 out of 10 muffed and 2 out of 10 sucessful.

            if (bad_plays > 0)
                throw new Exception("Too many bad plays " + bad_plays);

            if (L_bOnside_Muffed < 3000 || L_bOnside_Muffed > 5000)
                throw new Exception("L_bOnside_Muffed out of range " + L_bOnside_Muffed);
            if (L_num_made < 1500 || L_num_made > 2500)
                throw new Exception("L_num_made out of range " + L_num_made);

            if (R_bOnside_Muffed < 3000 || R_bOnside_Muffed > 5000)
                throw new Exception("R_bOnside_Muffed out of range " + R_bOnside_Muffed);
            if (R_num_made < 1500 || R_num_made > 2500)
                throw new Exception("R_num_made out of range " + R_num_made);

            Assert.IsTrue(true);

        }
    }
}
