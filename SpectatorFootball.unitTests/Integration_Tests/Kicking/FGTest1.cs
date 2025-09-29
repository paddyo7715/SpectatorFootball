using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpectatorFootball.Enum;
using SpectatorFootball.GameNS;
using SpectatorFootball.PlayNS;
using SpectatorFootball.unitTests.Helper_ClassesNS;

namespace SpectatorFootball.unitTests.Integration_Tests.Kicking
{
    [TestClass]
    public class FGTest1
    {
        [TestCategory("Integration")]
        [TestMethod]
        public void FG_25yards_or_less()
        {
            List<Game_Player> Kicking_Players = null;
            List<Game_Player> Defending_Players = null;

            int at = 1;
            int ht = 22;
            int possess_team = 0;

            int L_Kick_good = 0;
            int L_Kick_Missed = 0;
            int L_Kick_Blocked = 0;
            int L_Kick_hitGP_Missed = 0;
            int L_Kick_hitGP_Good = 0;
            int L_Kick_short = 0;
            int bad_plays = 0;

            int R_Kick_good = 0;
            int R_Kick_Missed = 0;
            int R_Kick_Blocked = 0;
            int R_Kick_hitGP_Missed = 0;
            int R_Kick_hitGP_Good = 0;
            int R_Kick_short = 0;


            int min_yardline_rnd = 1;
            int max_yardline_rnd = 10;

            double g_yardline = 0.0;
            int bad_players = 0;

            int num_plays = 20000;
            int icount = num_plays / 2;
            for (int i = 0; i < num_plays; i++)
            {
                bool bLefttoRight;
                if (i % 2 == 0)
                {
                    bLefttoRight = true;
                    g_yardline = 1.0;
                    possess_team = at;
                }
                else
                {
                    bLefttoRight = false;
                    g_yardline = 99.0;
                    possess_team = ht;
                }

                g_yardline = Game_Engine_Helper.getScrimmageLine(CommonUtils.getRandomNum(min_yardline_rnd, max_yardline_rnd), !bLefttoRight);

                double PossessionAdjuster = Game_Engine_Helper.HorizontalAdj(bLefttoRight);
                Formation FG_Forn = formation_helper.getFormation(Formations_Enum.FIELD_GOAL, PossessionAdjuster);
                Formation FG_Def_Form = formation_helper.getFormation(Formations_Enum.FIELD_GOAL_DEFENSE, PossessionAdjuster);

                Kicking_Players = Help_Class.setGamePlayerLIsts(1, g_yardline, FG_Forn);
                Defending_Players = Help_Class.setGamePlayerLIsts(22, g_yardline, FG_Def_Form);

                Game_Ball gb = new Game_Ball()
                {
                    State = Ball_States.TEED_UP,
                    Initial_State = Ball_States.TEED_UP,
                    Current_Vertical_Percent_Pos = 50.0,
                    Current_YardLine = g_yardline,
                    Starting_Vertical_Percent_Pos = 50.0,
                    Starting_YardLine = g_yardline
                };

//                        public Play_FG_XP(bool bLast_Play, bool bFG)
                Play_FG_XP FG = new Play_FG_XP(FG_Forn, FG_Def_Form, possess_team, at, ht, gb, Kicking_Players, Defending_Players, bLefttoRight, false, true);
                Play_Result pResult = FG.Execute(false);

                bad_players = Play_Validator.testPlayerStage_Irregularities(gb, Kicking_Players, Defending_Players);
                if (bad_players > 0) bad_players = 1;

                if (bad_players == 1)
                    bad_plays++;

                string Play_Result_Validation = Play_Validator.Validate_Play_Result(Play_Enum.FIELD_GOAL, Kicking_Players, Defending_Players, pResult, gb, bLefttoRight);
                if (Play_Result_Validation != null)
                    throw new Exception(Play_Result_Validation);

                string Play_Stats_Validation = Play_Validator.Validate_Punt_play_stats(Play_Enum.FIELD_GOAL, Kicking_Players, Defending_Players, pResult);
                if (Play_Stats_Validation != null)
                    throw new Exception(Play_Stats_Validation);

                if (bLefttoRight)
                {
                    if (pResult.bFGMade) L_Kick_good++;
                    if (pResult.bFGMissed) L_Kick_Missed++;
                    if (pResult.FGXP_Blocked) L_Kick_Blocked++;
                    if (!pResult.bFGMade && pResult.bFGXPHitGP) L_Kick_hitGP_Missed++;
                    if (pResult.bFGMade && pResult.bFGXPHitGP) L_Kick_hitGP_Good++;
                    if (pResult.bFGXPShort) L_Kick_short++;
                }
                else
                {
                    if (pResult.bFGMade) R_Kick_good++;
                    if (pResult.bFGMissed) R_Kick_Missed++;
                    if (pResult.FGXP_Blocked) R_Kick_Blocked++;
                    if (!pResult.bFGMade && pResult.bFGXPHitGP) R_Kick_hitGP_Missed++;
                    if (pResult.bFGMade && pResult.bFGXPHitGP) R_Kick_hitGP_Good++;
                    if (pResult.bFGXPShort) R_Kick_short++;
                }
            }

            if (bad_plays > 0)
                throw new Exception("Too many bad plays " + bad_plays);

            if (L_Kick_good > 9800 || L_Kick_good < 9400)
                throw new Exception("L_Kick_good out of range " + L_Kick_good);
            if (L_Kick_Missed > 600 || L_Kick_Missed < 200)
                throw new Exception("L_Kick_Missed out of range " + L_Kick_Missed);
            if (L_Kick_Blocked > 100 || L_Kick_Blocked < 25)
                throw new Exception("L_Kick_Blocked out of range " + L_Kick_Blocked);
            if (L_Kick_hitGP_Missed < 10 || L_Kick_hitGP_Missed > 50)
                throw new Exception("L_Kick_hitGP_Missed out of range " + L_Kick_hitGP_Missed);
            if (L_Kick_hitGP_Good < 0 || L_Kick_hitGP_Good > 10)
                throw new Exception("L_Kick_hitGP_Good out of range " + L_Kick_hitGP_Good);
            if (L_Kick_short > 0)
                throw new Exception("L_Kick_short out of range " + L_Kick_hitGP_Good);

            if (R_Kick_good > 9800 || R_Kick_good < 9400)
                throw new Exception("R_Kick_good out of range " + R_Kick_good);
            if (R_Kick_Missed > 600 || R_Kick_Missed < 200)
                throw new Exception("R_Kick_Missed out of range " + R_Kick_Missed);
            if (R_Kick_Blocked > 100 || R_Kick_Blocked < 25)
                throw new Exception("R_Kick_Blocked out of range " + R_Kick_Blocked);
            if (R_Kick_hitGP_Missed < 10 || R_Kick_hitGP_Missed > 50)
                throw new Exception("R_Kick_hitGP_Missed out of range " + R_Kick_hitGP_Missed);
            if (R_Kick_hitGP_Good < 0 || R_Kick_hitGP_Good > 10)
                throw new Exception("R_Kick_hitGP_Good out of range " + R_Kick_hitGP_Good);
            if (R_Kick_short > 0)
                throw new Exception("R_Kick_short out of range " + L_Kick_hitGP_Good);

            Assert.IsTrue(true);

        }

        [TestCategory("Integration")]
        [TestMethod]
        public void FG_35yards_or_less()
        {
            List<Game_Player> Kicking_Players = null;
            List<Game_Player> Defending_Players = null;

            int at = 1;
            int ht = 22;
            int possess_team = 0;

            int L_Kick_good = 0;
            int L_Kick_Missed = 0;
            int L_Kick_Blocked = 0;
            int L_Kick_hitGP_Missed = 0;
            int L_Kick_hitGP_Good = 0;
            int L_Kick_short = 0;

            int R_Kick_good = 0;
            int R_Kick_Missed = 0;
            int R_Kick_Blocked = 0;
            int R_Kick_hitGP_Missed = 0;
            int R_Kick_hitGP_Good = 0;
            int R_Kick_short = 0;


            int min_yardline_rnd = 11;
            int max_yardline_rnd = 20;


            double g_yardline = 0.0;

            int num_plays = 20000;
            int icount = num_plays / 2;
            for (int i = 0; i < num_plays; i++)
            {
                bool bLefttoRight;
                if (i % 2 == 0)
                {
                    bLefttoRight = true;
                    g_yardline = 1.0;
                    possess_team = at;
                }
                else
                {
                    bLefttoRight = false;
                    g_yardline = 99.0;
                    possess_team = ht;
                }

                g_yardline = Game_Engine_Helper.getScrimmageLine(CommonUtils.getRandomNum(min_yardline_rnd, max_yardline_rnd), !bLefttoRight);

                double PossessionAdjuster = Game_Engine_Helper.HorizontalAdj(bLefttoRight);
                Formation FG_Forn = formation_helper.getFormation(Formations_Enum.FIELD_GOAL, PossessionAdjuster);
                Formation FG_Def_Form = formation_helper.getFormation(Formations_Enum.FIELD_GOAL_DEFENSE, PossessionAdjuster);

                Kicking_Players = Help_Class.setGamePlayerLIsts(1, g_yardline, FG_Forn);
                Defending_Players = Help_Class.setGamePlayerLIsts(22, g_yardline, FG_Def_Form);

                Game_Ball gb = new Game_Ball()
                {
                    State = Ball_States.TEED_UP,
                    Initial_State = Ball_States.TEED_UP,
                    Current_Vertical_Percent_Pos = 50.0,
                    Current_YardLine = g_yardline,
                    Starting_Vertical_Percent_Pos = 50.0,
                    Starting_YardLine = g_yardline
                };

                //                        public Play_FG_XP(bool bLast_Play, bool bFG)
                Play_FG_XP FG = new Play_FG_XP(FG_Forn, FG_Def_Form, possess_team, at, ht, gb, Kicking_Players, Defending_Players, bLefttoRight, false, true);
                Play_Result pResult = FG.Execute(false);

                string Play_Result_Validation = Play_Validator.Validate_Play_Result(Play_Enum.FIELD_GOAL, Kicking_Players, Defending_Players, pResult, gb, bLefttoRight);
                if (Play_Result_Validation != null)
                    throw new Exception(Play_Result_Validation);

                string Play_Stats_Validation = Play_Validator.Validate_Punt_play_stats(Play_Enum.FIELD_GOAL, Kicking_Players, Defending_Players, pResult);
                if (Play_Stats_Validation != null)
                    throw new Exception(Play_Stats_Validation);

                if (bLefttoRight)
                {
                    if (pResult.bFGMade) L_Kick_good++;
                    if (pResult.bFGMissed) L_Kick_Missed++;
                    if (pResult.FGXP_Blocked) L_Kick_Blocked++;
                    if (!pResult.bFGMade && pResult.bFGXPHitGP) L_Kick_hitGP_Missed++;
                    if (pResult.bFGMade && pResult.bFGXPHitGP) L_Kick_hitGP_Good++;
                    if (pResult.bFGXPShort) L_Kick_short++;
                }
                else
                {
                    if (pResult.bFGMade) R_Kick_good++;
                    if (pResult.bFGMissed) R_Kick_Missed++;
                    if (pResult.FGXP_Blocked) R_Kick_Blocked++;
                    if (!pResult.bFGMade && pResult.bFGXPHitGP) R_Kick_hitGP_Missed++;
                    if (pResult.bFGMade && pResult.bFGXPHitGP) R_Kick_hitGP_Good++;
                    if (pResult.bFGXPShort) R_Kick_short++;
                }
            }

            if (L_Kick_good > 9300 || L_Kick_good < 8700)
                throw new Exception("L_Kick_good out of range " + L_Kick_good);
            if (L_Kick_Missed > 1300 || L_Kick_Missed < 700)
                throw new Exception("L_Kick_Missed out of range " + L_Kick_Missed);
            if (L_Kick_Blocked > 100 || L_Kick_Blocked < 25)
                throw new Exception("L_Kick_Blocked out of range " + L_Kick_Blocked);
            if (L_Kick_hitGP_Missed < 5 || L_Kick_hitGP_Missed > 70)
                throw new Exception("L_Kick_hitGP_Missed out of range " + L_Kick_hitGP_Missed);
            if (L_Kick_hitGP_Good < 0 || L_Kick_hitGP_Good > 10)
                throw new Exception("L_Kick_hitGP_Good out of range " + L_Kick_hitGP_Good);
            if (L_Kick_short > 90)
                throw new Exception("L_Kick_short out of range " + L_Kick_hitGP_Good);

            if (R_Kick_good > 9300 || R_Kick_good < 8700)
                throw new Exception("R_Kick_good out of range " + R_Kick_good);
            if (R_Kick_Missed > 1300 || R_Kick_Missed < 700)
                throw new Exception("R_Kick_Missed out of range " + R_Kick_Missed);
            if (R_Kick_Blocked > 100 || R_Kick_Blocked < 25)
                throw new Exception("R_Kick_Blocked out of range " + R_Kick_Blocked);
            if (R_Kick_hitGP_Missed < 5 || R_Kick_hitGP_Missed > 70)
                throw new Exception("R_Kick_hitGP_Missed out of range " + R_Kick_hitGP_Missed);
            if (R_Kick_hitGP_Good < 0 || R_Kick_hitGP_Good > 10)
                throw new Exception("R_Kick_hitGP_Good out of range " + R_Kick_hitGP_Good);
            if (R_Kick_short > 90)
                throw new Exception("R_Kick_short out of range " + L_Kick_hitGP_Good);

            Assert.IsTrue(true);

        }

        [TestCategory("Integration")]
        [TestMethod]
        public void FG_45yards_or_less()
        {
            List<Game_Player> Kicking_Players = null;
            List<Game_Player> Defending_Players = null;

            int at = 1;
            int ht = 22;
            int possess_team = 0;

            int L_Kick_good = 0;
            int L_Kick_Missed = 0;
            int L_Kick_Blocked = 0;
            int L_Kick_hitGP_Missed = 0;
            int L_Kick_hitGP_Good = 0;
            int L_Kick_short = 0;

            int R_Kick_good = 0;
            int R_Kick_Missed = 0;
            int R_Kick_Blocked = 0;
            int R_Kick_hitGP_Missed = 0;
            int R_Kick_hitGP_Good = 0;
            int R_Kick_short = 0;


            int min_yardline_rnd = 21;
            int max_yardline_rnd = 30;


            double g_yardline = 0.0;

            int num_plays = 20000;
            int icount = num_plays / 2;
            for (int i = 0; i < num_plays; i++)
            {
                bool bLefttoRight;
                if (i % 2 == 0)
                {
                    bLefttoRight = true;
                    g_yardline = 1.0;
                    possess_team = at;
                }
                else
                {
                    bLefttoRight = false;
                    g_yardline = 99.0;
                    possess_team = ht;
                }

                g_yardline = Game_Engine_Helper.getScrimmageLine(CommonUtils.getRandomNum(min_yardline_rnd, max_yardline_rnd), !bLefttoRight);

                double PossessionAdjuster = Game_Engine_Helper.HorizontalAdj(bLefttoRight);
                Formation FG_Forn = formation_helper.getFormation(Formations_Enum.FIELD_GOAL, PossessionAdjuster);
                Formation FG_Def_Form = formation_helper.getFormation(Formations_Enum.FIELD_GOAL_DEFENSE, PossessionAdjuster);

                Kicking_Players = Help_Class.setGamePlayerLIsts(1, g_yardline, FG_Forn);
                Defending_Players = Help_Class.setGamePlayerLIsts(22, g_yardline, FG_Def_Form);

                Game_Ball gb = new Game_Ball()
                {
                    State = Ball_States.TEED_UP,
                    Initial_State = Ball_States.TEED_UP,
                    Current_Vertical_Percent_Pos = 50.0,
                    Current_YardLine = g_yardline,
                    Starting_Vertical_Percent_Pos = 50.0,
                    Starting_YardLine = g_yardline
                };

                //                        public Play_FG_XP(bool bLast_Play, bool bFG)
                Play_FG_XP FG = new Play_FG_XP(FG_Forn, FG_Def_Form, possess_team, at, ht, gb, Kicking_Players, Defending_Players, bLefttoRight, false, true);
                Play_Result pResult = FG.Execute(false);

                string Play_Result_Validation = Play_Validator.Validate_Play_Result(Play_Enum.FIELD_GOAL, Kicking_Players, Defending_Players, pResult, gb, bLefttoRight);
                if (Play_Result_Validation != null)
                    throw new Exception(Play_Result_Validation);

                string Play_Stats_Validation = Play_Validator.Validate_Punt_play_stats(Play_Enum.FIELD_GOAL, Kicking_Players, Defending_Players, pResult);
                if (Play_Stats_Validation != null)
                    throw new Exception(Play_Stats_Validation);

                if (bLefttoRight)
                {
                    if (pResult.bFGMade) L_Kick_good++;
                    if (pResult.bFGMissed) L_Kick_Missed++;
                    if (pResult.FGXP_Blocked) L_Kick_Blocked++;
                    if (!pResult.bFGMade && pResult.bFGXPHitGP) L_Kick_hitGP_Missed++;
                    if (pResult.bFGMade && pResult.bFGXPHitGP) L_Kick_hitGP_Good++;
                    if (pResult.bFGXPShort) L_Kick_short++;
                }
                else
                {
                    if (pResult.bFGMade) R_Kick_good++;
                    if (pResult.bFGMissed) R_Kick_Missed++;
                    if (pResult.FGXP_Blocked) R_Kick_Blocked++;
                    if (!pResult.bFGMade && pResult.bFGXPHitGP) R_Kick_hitGP_Missed++;
                    if (pResult.bFGMade && pResult.bFGXPHitGP) R_Kick_hitGP_Good++;
                    if (pResult.bFGXPShort) R_Kick_short++;
                }
            }

            if (L_Kick_good > 8600 || L_Kick_good < 8000)
                throw new Exception("L_Kick_good out of range " + L_Kick_good);
            if (L_Kick_Missed > 2000 || L_Kick_Missed < 1400)
                throw new Exception("L_Kick_Missed out of range " + L_Kick_Missed);
            if (L_Kick_Blocked > 100 || L_Kick_Blocked < 25)
                throw new Exception("L_Kick_Blocked out of range " + L_Kick_Blocked);
            if (L_Kick_hitGP_Missed < 10 || L_Kick_hitGP_Missed > 70)
                throw new Exception("L_Kick_hitGP_Missed out of range " + L_Kick_hitGP_Missed);
            if (L_Kick_hitGP_Good < 0 || L_Kick_hitGP_Good > 10)
                throw new Exception("L_Kick_hitGP_Good out of range " + L_Kick_hitGP_Good);
            if (L_Kick_short < 250 || L_Kick_short > 1000)
                throw new Exception("L_Kick_short out of range " + L_Kick_hitGP_Good);

            if (R_Kick_good > 8600 || R_Kick_good < 8000)
                throw new Exception("R_Kick_good out of range " + R_Kick_good);
            if (R_Kick_Missed > 2000 || R_Kick_Missed < 1400)
                throw new Exception("R_Kick_Missed out of range " + R_Kick_Missed);
            if (R_Kick_Blocked > 100 || R_Kick_Blocked < 25)
                throw new Exception("R_Kick_Blocked out of range " + R_Kick_Blocked);
            if (R_Kick_hitGP_Missed < 10 || R_Kick_hitGP_Missed > 70)
                throw new Exception("R_Kick_hitGP_Missed out of range " + R_Kick_hitGP_Missed);
            if (R_Kick_hitGP_Good < 0 || R_Kick_hitGP_Good > 10)
                throw new Exception("R_Kick_hitGP_Good out of range " + R_Kick_hitGP_Good);
            if (R_Kick_short < 250 || R_Kick_short > 1000)
                throw new Exception("R_Kick_short out of range " + L_Kick_hitGP_Good);

            Assert.IsTrue(true);

        }

        [TestCategory("Integration")]
        [TestMethod]
        public void FG_55yards_or_less()
        {
            List<Game_Player> Kicking_Players = null;
            List<Game_Player> Defending_Players = null;

            int at = 1;
            int ht = 22;
            int possess_team = 0;

            int L_Kick_good = 0;
            int L_Kick_Missed = 0;
            int L_Kick_Blocked = 0;
            int L_Kick_hitGP_Missed = 0;
            int L_Kick_hitGP_Good = 0;
            int L_Kick_short = 0;

            int R_Kick_good = 0;
            int R_Kick_Missed = 0;
            int R_Kick_Blocked = 0;
            int R_Kick_hitGP_Missed = 0;
            int R_Kick_hitGP_Good = 0;
            int R_Kick_short = 0;


            int min_yardline_rnd = 31;
            int max_yardline_rnd = 40;


            double g_yardline = 0.0;

            int num_plays = 20000;
            int icount = num_plays / 2;
            for (int i = 0; i < num_plays; i++)
            {
                bool bLefttoRight;
                if (i % 2 == 0)
                {
                    bLefttoRight = true;
                    g_yardline = 1.0;
                    possess_team = at;
                }
                else
                {
                    bLefttoRight = false;
                    g_yardline = 99.0;
                    possess_team = ht;
                }

                g_yardline = Game_Engine_Helper.getScrimmageLine(CommonUtils.getRandomNum(min_yardline_rnd, max_yardline_rnd), !bLefttoRight);

                double PossessionAdjuster = Game_Engine_Helper.HorizontalAdj(bLefttoRight);
                Formation FG_Forn = formation_helper.getFormation(Formations_Enum.FIELD_GOAL, PossessionAdjuster);
                Formation FG_Def_Form = formation_helper.getFormation(Formations_Enum.FIELD_GOAL_DEFENSE, PossessionAdjuster);

                Kicking_Players = Help_Class.setGamePlayerLIsts(1, g_yardline, FG_Forn);
                Defending_Players = Help_Class.setGamePlayerLIsts(22, g_yardline, FG_Def_Form);

                Game_Ball gb = new Game_Ball()
                {
                    State = Ball_States.TEED_UP,
                    Initial_State = Ball_States.TEED_UP,
                    Current_Vertical_Percent_Pos = 50.0,
                    Current_YardLine = g_yardline,
                    Starting_Vertical_Percent_Pos = 50.0,
                    Starting_YardLine = g_yardline
                };

                //                        public Play_FG_XP(bool bLast_Play, bool bFG)
                Play_FG_XP FG = new Play_FG_XP(FG_Forn, FG_Def_Form, possess_team, at, ht, gb, Kicking_Players, Defending_Players, bLefttoRight, false, true);
                Play_Result pResult = FG.Execute(false);

                string Play_Result_Validation = Play_Validator.Validate_Play_Result(Play_Enum.FIELD_GOAL, Kicking_Players, Defending_Players, pResult, gb, bLefttoRight);
                if (Play_Result_Validation != null)
                    throw new Exception(Play_Result_Validation);

                string Play_Stats_Validation = Play_Validator.Validate_Punt_play_stats(Play_Enum.FIELD_GOAL, Kicking_Players, Defending_Players, pResult);
                if (Play_Stats_Validation != null)
                    throw new Exception(Play_Stats_Validation);

                if (bLefttoRight)
                {
                    if (pResult.bFGMade) L_Kick_good++;
                    if (pResult.bFGMissed) L_Kick_Missed++;
                    if (pResult.FGXP_Blocked) L_Kick_Blocked++;
                    if (!pResult.bFGMade && pResult.bFGXPHitGP) L_Kick_hitGP_Missed++;
                    if (pResult.bFGMade && pResult.bFGXPHitGP) L_Kick_hitGP_Good++;
                    if (pResult.bFGXPShort) L_Kick_short++;
                }
                else
                {
                    if (pResult.bFGMade) R_Kick_good++;
                    if (pResult.bFGMissed) R_Kick_Missed++;
                    if (pResult.FGXP_Blocked) R_Kick_Blocked++;
                    if (!pResult.bFGMade && pResult.bFGXPHitGP) R_Kick_hitGP_Missed++;
                    if (pResult.bFGMade && pResult.bFGXPHitGP) R_Kick_hitGP_Good++;
                    if (pResult.bFGXPShort) R_Kick_short++;
                }
            }

            if (L_Kick_good > 5600 || L_Kick_good < 5000)
                throw new Exception("L_Kick_good out of range " + L_Kick_good);
            if (L_Kick_Missed > 5000 || L_Kick_Missed < 4400)
                throw new Exception("L_Kick_Missed out of range " + L_Kick_Missed);
            if (L_Kick_Blocked > 100 || L_Kick_Blocked < 25)
                throw new Exception("L_Kick_Blocked out of range " + L_Kick_Blocked);
            if (L_Kick_hitGP_Missed < 5 || L_Kick_hitGP_Missed > 70)
                throw new Exception("L_Kick_hitGP_Missed out of range " + L_Kick_hitGP_Missed);
            if (L_Kick_hitGP_Good < 0 || L_Kick_hitGP_Good > 10)
                throw new Exception("L_Kick_hitGP_Good out of range " + L_Kick_hitGP_Good);
            if (L_Kick_short < 3800 || L_Kick_short > 4500)
                throw new Exception("L_Kick_short out of range " + L_Kick_hitGP_Good);

            if (R_Kick_good > 5600 || R_Kick_good < 5000)
                throw new Exception("R_Kick_good out of range " + R_Kick_good);
            if (R_Kick_Missed > 5000 || R_Kick_Missed < 4400)
                throw new Exception("R_Kick_Missed out of range " + R_Kick_Missed);
            if (R_Kick_Blocked > 100 || R_Kick_Blocked < 25)
                throw new Exception("R_Kick_Blocked out of range " + R_Kick_Blocked);
            if (R_Kick_hitGP_Missed < 5 || R_Kick_hitGP_Missed > 70)
                throw new Exception("R_Kick_hitGP_Missed out of range " + R_Kick_hitGP_Missed);
            if (R_Kick_hitGP_Good < 0 || R_Kick_hitGP_Good > 10)
                throw new Exception("R_Kick_hitGP_Good out of range " + R_Kick_hitGP_Good);
            if (R_Kick_short < 3800 || R_Kick_short > 4500)
                throw new Exception("R_Kick_short out of range " + L_Kick_hitGP_Good);

            Assert.IsTrue(true);

        }

        [TestCategory("Integration")]
        [TestMethod]
        public void FG_65yards_or_less()
        {
            List<Game_Player> Kicking_Players = null;
            List<Game_Player> Defending_Players = null;

            int at = 1;
            int ht = 22;
            int possess_team = 0;

            int L_Kick_good = 0;
            int L_Kick_Missed = 0;
            int L_Kick_Blocked = 0;
            int L_Kick_hitGP_Missed = 0;
            int L_Kick_hitGP_Good = 0;
            int L_Kick_short = 0;

            int R_Kick_good = 0;
            int R_Kick_Missed = 0;
            int R_Kick_Blocked = 0;
            int R_Kick_hitGP_Missed = 0;
            int R_Kick_hitGP_Good = 0;
            int R_Kick_short = 0;


            int min_yardline_rnd = 41;
            int max_yardline_rnd = 50;


            double g_yardline = 0.0;

            int num_plays = 20000;
            int icount = num_plays / 2;
            for (int i = 0; i < num_plays; i++)
            {
                bool bLefttoRight;
                if (i % 2 == 0)
                {
                    bLefttoRight = true;
                    g_yardline = 1.0;
                    possess_team = at;
                }
                else
                {
                    bLefttoRight = false;
                    g_yardline = 99.0;
                    possess_team = ht;
                }

                g_yardline = Game_Engine_Helper.getScrimmageLine(CommonUtils.getRandomNum(min_yardline_rnd, max_yardline_rnd), !bLefttoRight);

                double PossessionAdjuster = Game_Engine_Helper.HorizontalAdj(bLefttoRight);
                Formation FG_Forn = formation_helper.getFormation(Formations_Enum.FIELD_GOAL, PossessionAdjuster);
                Formation FG_Def_Form = formation_helper.getFormation(Formations_Enum.FIELD_GOAL_DEFENSE, PossessionAdjuster);

                Kicking_Players = Help_Class.setGamePlayerLIsts(1, g_yardline, FG_Forn);
                Defending_Players = Help_Class.setGamePlayerLIsts(22, g_yardline, FG_Def_Form);

                Game_Ball gb = new Game_Ball()
                {
                    State = Ball_States.TEED_UP,
                    Initial_State = Ball_States.TEED_UP,
                    Current_Vertical_Percent_Pos = 50.0,
                    Current_YardLine = g_yardline,
                    Starting_Vertical_Percent_Pos = 50.0,
                    Starting_YardLine = g_yardline
                };

                //                        public Play_FG_XP(bool bLast_Play, bool bFG)
                Play_FG_XP FG = new Play_FG_XP(FG_Forn, FG_Def_Form, possess_team, at, ht, gb, Kicking_Players, Defending_Players, bLefttoRight, false, true);
                Play_Result pResult = FG.Execute(false);

                string Play_Result_Validation = Play_Validator.Validate_Play_Result(Play_Enum.FIELD_GOAL, Kicking_Players, Defending_Players, pResult, gb, bLefttoRight);
                if (Play_Result_Validation != null)
                    throw new Exception(Play_Result_Validation);

                string Play_Stats_Validation = Play_Validator.Validate_Punt_play_stats(Play_Enum.FIELD_GOAL, Kicking_Players, Defending_Players, pResult);
                if (Play_Stats_Validation != null)
                    throw new Exception(Play_Stats_Validation);

                if (bLefttoRight)
                {
                    if (pResult.bFGMade) L_Kick_good++;
                    if (pResult.bFGMissed) L_Kick_Missed++;
                    if (pResult.FGXP_Blocked) L_Kick_Blocked++;
                    if (!pResult.bFGMade && pResult.bFGXPHitGP) L_Kick_hitGP_Missed++;
                    if (pResult.bFGMade && pResult.bFGXPHitGP) L_Kick_hitGP_Good++;
                    if (pResult.bFGXPShort) L_Kick_short++;
                }
                else
                {
                    if (pResult.bFGMade) R_Kick_good++;
                    if (pResult.bFGMissed) R_Kick_Missed++;
                    if (pResult.FGXP_Blocked) R_Kick_Blocked++;
                    if (!pResult.bFGMade && pResult.bFGXPHitGP) R_Kick_hitGP_Missed++;
                    if (pResult.bFGMade && pResult.bFGXPHitGP) R_Kick_hitGP_Good++;
                    if (pResult.bFGXPShort) R_Kick_short++;
                }
            }

            if (L_Kick_good > 1900 || L_Kick_good < 1300)
                throw new Exception("L_Kick_good out of range " + L_Kick_good);
            if (L_Kick_Missed > 8700 || L_Kick_Missed < 8100)
                throw new Exception("L_Kick_Missed out of range " + L_Kick_Missed);
            if (L_Kick_Blocked > 100 || L_Kick_Blocked < 25)
                throw new Exception("L_Kick_Blocked out of range " + L_Kick_Blocked);
            if (L_Kick_hitGP_Missed < 5 || L_Kick_hitGP_Missed > 60)
                throw new Exception("L_Kick_hitGP_Missed out of range " + L_Kick_hitGP_Missed);
            if (L_Kick_hitGP_Good < 0 || L_Kick_hitGP_Good > 10)
                throw new Exception("L_Kick_hitGP_Good out of range " + L_Kick_hitGP_Good);
            if (L_Kick_short < 7500 || L_Kick_short > 8500)
                throw new Exception("L_Kick_short out of range " + L_Kick_hitGP_Good);

            if (R_Kick_good > 1900 || R_Kick_good < 1300)
                throw new Exception("R_Kick_good out of range " + R_Kick_good);
            if (R_Kick_Missed > 8700 || R_Kick_Missed < 8100)
                throw new Exception("R_Kick_Missed out of range " + R_Kick_Missed);
            if (R_Kick_Blocked > 100 || R_Kick_Blocked < 25)
                throw new Exception("R_Kick_Blocked out of range " + R_Kick_Blocked);
            if (R_Kick_hitGP_Missed < 5 || R_Kick_hitGP_Missed > 60)
                throw new Exception("R_Kick_hitGP_Missed out of range " + R_Kick_hitGP_Missed);
            if (R_Kick_hitGP_Good < 0 || R_Kick_hitGP_Good > 10)
                throw new Exception("R_Kick_hitGP_Good out of range " + R_Kick_hitGP_Good);
            if (R_Kick_short < 7500 || R_Kick_short > 8500)
                throw new Exception("R_Kick_short out of range " + L_Kick_hitGP_Good);

            Assert.IsTrue(true);

        }

        [TestCategory("Integration")]
        [TestMethod]
        public void XP_Kick()
        {
            List<Game_Player> Kicking_Players = null;
            List<Game_Player> Defending_Players = null;

            int at = 1;
            int ht = 22;
            int possess_team = 0;

            int L_Kick_good = 0;
            int L_Kick_Missed = 0;
            int L_Kick_Blocked = 0;
            int L_Kick_hitGP_Missed = 0;
            int L_Kick_hitGP_Good = 0;
            int L_Kick_short = 0;

            int R_Kick_good = 0;
            int R_Kick_Missed = 0;
            int R_Kick_Blocked = 0;
            int R_Kick_hitGP_Missed = 0;
            int R_Kick_hitGP_Good = 0;
            int R_Kick_short = 0;

            double g_yardline = 15.0;

            int num_plays = 20000;
            int icount = num_plays / 2;
            for (int i = 0; i < num_plays; i++)
            {
                bool bLefttoRight;
                if (i % 2 == 0)
                {
                    bLefttoRight = true;
                    g_yardline = 85.0;
                    possess_team = at;
                }
                else
                {
                    bLefttoRight = false;
                    g_yardline = 15.0;
                    possess_team = ht;
                }

                double PossessionAdjuster = Game_Engine_Helper.HorizontalAdj(bLefttoRight);
                Formation FG_Forn = formation_helper.getFormation(Formations_Enum.FIELD_GOAL, PossessionAdjuster);
                Formation FG_Def_Form = formation_helper.getFormation(Formations_Enum.FIELD_GOAL_DEFENSE, PossessionAdjuster);

                Kicking_Players = Help_Class.setGamePlayerLIsts(1, g_yardline, FG_Forn);
                Defending_Players = Help_Class.setGamePlayerLIsts(22, g_yardline, FG_Def_Form);

                Game_Ball gb = new Game_Ball()
                {
                    State = Ball_States.TEED_UP,
                    Initial_State = Ball_States.TEED_UP,
                    Current_Vertical_Percent_Pos = 50.0,
                    Current_YardLine = g_yardline,
                    Starting_Vertical_Percent_Pos = 50.0,
                    Starting_YardLine = g_yardline
                };

                //                        public Play_FG_XP(bool bLast_Play, bool bFG)
                Play_FG_XP FG = new Play_FG_XP(FG_Forn, FG_Def_Form, possess_team, at, ht, gb, Kicking_Players, Defending_Players, bLefttoRight, false, false);
                Play_Result pResult = FG.Execute(false);

                string Play_Result_Validation = Play_Validator.Validate_Play_Result(Play_Enum.EXTRA_POINT, Kicking_Players, Defending_Players, pResult, gb, bLefttoRight);
                if (Play_Result_Validation != null)
                    throw new Exception(Play_Result_Validation);

                string Play_Stats_Validation = Play_Validator.Validate_Punt_play_stats(Play_Enum.EXTRA_POINT, Kicking_Players, Defending_Players, pResult);
                if (Play_Stats_Validation != null)
                    throw new Exception(Play_Stats_Validation);

                if (bLefttoRight)
                {
                    if (pResult.bXPMade) L_Kick_good++;
                    if (pResult.bXPMissed) L_Kick_Missed++;
                    if (pResult.FGXP_Blocked) L_Kick_Blocked++;
                    if (!pResult.bXPMade && pResult.bFGXPHitGP) L_Kick_hitGP_Missed++;
                    if (pResult.bXPMade && pResult.bFGXPHitGP) L_Kick_hitGP_Good++;
                    if (pResult.bFGXPShort) L_Kick_short++;
                }
                else
                {
                    if (pResult.bXPMade) R_Kick_good++;
                    if (pResult.bXPMissed) R_Kick_Missed++;
                    if (pResult.FGXP_Blocked) R_Kick_Blocked++;
                    if (!pResult.bXPMade && pResult.bFGXPHitGP) R_Kick_hitGP_Missed++;
                    if (pResult.bXPMade && pResult.bFGXPHitGP) R_Kick_hitGP_Good++;
                    if (pResult.bFGXPShort) R_Kick_short++;
                }
            }

            if (L_Kick_good > 9500 || L_Kick_good < 8900)
                throw new Exception("L_Kick_good out of range " + L_Kick_good);
            if (L_Kick_Missed > 1100 || L_Kick_Missed < 500)
                throw new Exception("L_Kick_Missed out of range " + L_Kick_Missed);
            if (L_Kick_Blocked > 100 || L_Kick_Blocked < 25)
                throw new Exception("L_Kick_Blocked out of range " + L_Kick_Blocked);
            if (L_Kick_hitGP_Missed < 1 || L_Kick_hitGP_Missed > 60)
                throw new Exception("L_Kick_hitGP_Missed out of range " + L_Kick_hitGP_Missed);
            if (L_Kick_hitGP_Good < 0 || L_Kick_hitGP_Good > 10)
                throw new Exception("L_Kick_hitGP_Good out of range " + L_Kick_hitGP_Good);
            if (L_Kick_short > 55)
                throw new Exception("L_Kick_short out of range " + L_Kick_hitGP_Good);

            if (R_Kick_good > 9500 || R_Kick_good < 8900)
                throw new Exception("R_Kick_good out of range " + R_Kick_good);
            if (R_Kick_Missed > 1100 || R_Kick_Missed < 500)
                throw new Exception("R_Kick_Missed out of range " + R_Kick_Missed);
            if (R_Kick_Blocked > 100 || R_Kick_Blocked < 25)
                throw new Exception("R_Kick_Blocked out of range " + R_Kick_Blocked);
            if (R_Kick_hitGP_Missed < 1 || R_Kick_hitGP_Missed > 60)
                throw new Exception("R_Kick_hitGP_Missed out of range " + R_Kick_hitGP_Missed);
            if (R_Kick_hitGP_Good < 0 || R_Kick_hitGP_Good > 10)
                throw new Exception("R_Kick_hitGP_Good out of range " + R_Kick_hitGP_Good);
            if (R_Kick_short > 55)
                throw new Exception("R_Kick_short out of range " + L_Kick_hitGP_Good);

            Assert.IsTrue(true);

        }


    }
}
