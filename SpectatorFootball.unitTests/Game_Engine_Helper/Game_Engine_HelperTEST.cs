using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpectatorFootball.GameNS;
using SpectatorFootball.Models;
using SpectatorFootball.unitTests.Helper_ClassesNS;
using SpectatorFootball.Enum;

namespace SpectatorFootball.unitTests.GameEngineTEST
{
    [TestClass]
    public class Game_Engine_HelperTEST
    {
        private List<Player_and_Ratings> Home_Team_PandR = null;
        private List<Player_and_Ratings> Away_Team_PandR = null;
        Teams_by_Season at = null;
        Teams_by_Season ht = null;

        [TestCategory("Game_Engine_Helper")]
        [TestMethod]
        public void coinToss_HTWins()
        {

            long r = Game_Engine_Helper.CoinToss(49, 11, 22);

            Assert.IsTrue(r == 11);
        }
        [TestCategory("Game_Engine_Helper")]
        [TestMethod]
        public void coinToss_ATWins()
        {
            long r = Game_Engine_Helper.CoinToss(51, 11, 22);

            Assert.IsTrue(r == 22);
        }

        [TestCategory("Game_Engine_Helper")]
        [TestMethod]
        public void HorizontalAdj_True_result1()
        {
            Assert.IsTrue(Game_Engine_Helper.HorizontalAdj(true) == 1);
        }
        [TestCategory("Game_Engine_Helper")]
        [TestMethod]
        public void HorizontalAdj_False_resultMinus1()
        {
            Assert.IsTrue(Game_Engine_Helper.HorizontalAdj(false) == -1);
        }
        [TestCategory("Game_Engine_Helper")]
        [TestMethod]
        public void isPlayWithReturner_Kickoff_true()
        {
            Assert.IsTrue(Game_Engine_Helper.isPlayWithReturner(Play_Enum.KICKOFF_NORMAL) == true);
        }
        [TestCategory("Game_Engine_Helper")]
        [TestMethod]
        public void isPlayWithReturner_FreeKick_true()
        {
            Assert.IsTrue(Game_Engine_Helper.isPlayWithReturner(Play_Enum.FREE_KICK) == true);
        }
        [TestCategory("Game_Engine_Helper")]
        [TestMethod]
        public void isPlayWithReturner_Punt_true()
        {
            Assert.IsTrue(Game_Engine_Helper.isPlayWithReturner(Play_Enum.PUNT) == true);
        }
        [TestCategory("Game_Engine_Helper")]
        [TestMethod]
        public void isPlayWithReturner_Pass_false()
        {
            Assert.IsTrue(Game_Engine_Helper.isPlayWithReturner(Play_Enum.PASS) == false);
        }
        [TestCategory("Game_Engine_Helper")]
        [TestMethod]
        public void Switch_Posession_at_possession()
        {
            Assert.IsTrue(Game_Engine_Helper.Switch_Posession(11,11,22) == 22);
        }
        [TestCategory("Game_Engine_Helper")]
        [TestMethod]
        public void Switch_Posession_ht_possession()
        {
            Assert.IsTrue(Game_Engine_Helper.Switch_Posession(22, 11, 22) == 11);
        }
        [TestCategory("Game_Engine_Helper")]
        [TestMethod]
        public void Switch_LefttoRight_true_return_false()
        {
            Assert.IsTrue(Game_Engine_Helper.Switch_LefttoRight(true) == false);
        }
        [TestCategory("Game_Engine_Helper")]
        [TestMethod]
        public void Switch_LefttoRight_false_return_true()
        {
            Assert.IsTrue(Game_Engine_Helper.Switch_LefttoRight(false) == true);
        }
        [TestCategory("Game_Engine_Helper")]
        [TestMethod]
        public void getYardsGained_left_10_yardgame()
        {
            Assert.IsTrue(Game_Engine_Helper.getYardsGained(true, 20,30) == 10);
        }
        [TestCategory("Game_Engine_Helper")]
        [TestMethod]
        public void getYardsGained_left_loss_5_yards()
        {
            Assert.IsTrue(Game_Engine_Helper.getYardsGained(true, 20, 15) == -5);
        }
        [TestCategory("Game_Engine_Helper")]
        [TestMethod]
        public void getYardsGained_left_2_yard_game_into_EZ()
        {
            Assert.IsTrue(Game_Engine_Helper.getYardsGained(true, 98, 102) == 2);
        }
        [TestCategory("Game_Engine_Helper")]
        [TestMethod]
        public void getYardsGained_left_2_yard_loss_into_EZ()
        {
            Assert.IsTrue(Game_Engine_Helper.getYardsGained(true,2, -2) == -2);
        }
        [TestCategory("Game_Engine_Helper")]
        [TestMethod]
        public void getYardsGained_right_10_yardgame()
        {
            Assert.IsTrue(Game_Engine_Helper.getYardsGained(false, 30, 20) == 10);
        }
        [TestCategory("Game_Engine_Helper")]
        [TestMethod]
        public void getYardsGained_right_loss_5_yards()
        {
            Assert.IsTrue(Game_Engine_Helper.getYardsGained(false, 15, 20) == -5);
        }
        [TestCategory("Game_Engine_Helper")]
        [TestMethod]
        public void getYardsGained_right_2_yard_game_into_EZ()
        {
            Assert.IsTrue(Game_Engine_Helper.getYardsGained(false, 2, -2) == 2);
        }
        [TestCategory("Game_Engine_Helper")]
        [TestMethod]
        public void getYardsGained_right_2_yard_loss_into_EZ()
        {
            Assert.IsTrue(Game_Engine_Helper.getYardsGained(false, 98, 102) == -2);
        }
        [TestCategory("Game_Engine_Helper")]
        [TestMethod]
        public void isTouchdown_left_noTD()
        {
            Assert.IsTrue(Game_Engine_Helper.isTouchdown(true, 98.0) == false);
        }
        [TestCategory("Game_Engine_Helper")]
        [TestMethod]
        public void isTouchdown_left_TD()
        {
            Assert.IsTrue(Game_Engine_Helper.isTouchdown(true, 102.0) == true);
        }


        [TestCategory("Game_Engine_Helper")]
        [TestMethod]
        public void isTouchdown_right_noTD()
        {
            Assert.IsTrue(Game_Engine_Helper.isTouchdown(false, 2.0) == false);
        }
        [TestCategory("Game_Engine_Helper")]
        [TestMethod]
        public void isTouchdown_right_TD()
        {
            Assert.IsTrue(Game_Engine_Helper.isTouchdown(false, -2.0) == true);
        }

        [TestCategory("Game_Engine_Helper")]
        [TestMethod]
        public void calcDistanceFromOpponentGL_left()
        {
            Assert.IsTrue(Game_Engine_Helper.calcDistanceFromOpponentGL(85, true) == 15);
        }
        [TestCategory("Game_Engine_Helper")]
        [TestMethod]
        public void calcDistanceFromOpponentGL_right()
        {
            Assert.IsTrue(Game_Engine_Helper.calcDistanceFromOpponentGL(85, false) == 85);
        }
        [TestCategory("Game_Engine_Helper")]
        [TestMethod]
        public void calcDistanceFromMyGL_left()
        {
            Assert.IsTrue(Game_Engine_Helper.calcDistanceFromMyGL(85, true) == 85);
        }
        [TestCategory("Game_Engine_Helper")]
        [TestMethod]
        public void calcDistanceFromMyGL_right()
        {
            Assert.IsTrue(Game_Engine_Helper.calcDistanceFromMyGL(85, false) == 15);
        }
        [TestCategory("Game_Engine_Helper")]
        [TestMethod]
        public void getPlayerAction_Passer()
        {
            Game_Player p = new Game_Player();
            Play_Result pResult = new Play_Result();
            pResult.Passer = p;
            pResult.Penalized_Player = p;
            bool b = Game_Engine_Helper.isBallTeamPenalty(pResult);
            Assert.IsTrue(b);
        }
        [TestCategory("Game_Engine_Helper")]
        [TestMethod]
        public void getPlayerAction_Kicker()
        {
            Game_Player p = new Game_Player();
            Play_Result pResult = new Play_Result();
            pResult.Kicker = p;
            pResult.Penalized_Player = p;
            bool b = Game_Engine_Helper.isBallTeamPenalty(pResult);
            Assert.IsTrue(!b);
        }
        [TestCategory("Game_Engine_Helper")]
        [TestMethod]
        public void getPlayerAction_Punter()
        {
            Game_Player p = new Game_Player();
            Play_Result pResult = new Play_Result();
            pResult.Punter = p;
            pResult.Penalized_Player = p;
            bool b = Game_Engine_Helper.isBallTeamPenalty(pResult);
            Assert.IsTrue(!b);
        }
        [TestCategory("Game_Engine_Helper")]
        [TestMethod]
        public void getPlayerAction_Returner()
        {
            Game_Player p = new Game_Player();
            Play_Result pResult = new Play_Result();
            pResult.Returner = p;
            pResult.Penalized_Player = p;
            bool b = Game_Engine_Helper.isBallTeamPenalty(pResult);
            Assert.IsTrue(b);
        }
        [TestCategory("Game_Engine_Helper")]
        [TestMethod]
        public void getPlayerAction_PuntReturner()
        {
            Game_Player p = new Game_Player();
            Play_Result pResult = new Play_Result();
            pResult.Punt_Returner = p;
            pResult.Penalized_Player = p;
            bool b = Game_Engine_Helper.isBallTeamPenalty(pResult);
            Assert.IsTrue(b);
        }
        [TestCategory("Game_Engine_Helper")]
        [TestMethod]
        public void getPlayerAction_PassCatchers()
        {
            Game_Player p = new Game_Player();
            Play_Result pResult = new Play_Result();
            pResult.Pass_Catchers.Add(p);
            pResult.Penalized_Player = p;
            bool b = Game_Engine_Helper.isBallTeamPenalty(pResult);
            Assert.IsTrue(b);
        }
        [TestCategory("Game_Engine_Helper")]
        [TestMethod]
        public void getPlayerAction_BallRunners()
        {
            Game_Player p = new Game_Player();
            Play_Result pResult = new Play_Result();
            pResult.Ball_Runners.Add(p);
            pResult.Penalized_Player = p;
            bool b = Game_Engine_Helper.isBallTeamPenalty(pResult);
            Assert.IsTrue(b);
        }
        [TestCategory("Game_Engine_Helper")]
        [TestMethod]
        public void getPlayerAction_PassBlockers()
        {
            Game_Player p = new Game_Player();
            Play_Result pResult = new Play_Result();
            pResult.Pass_Blockers.Add(p);
            pResult.Penalized_Player = p;
            bool b = Game_Engine_Helper.isBallTeamPenalty(pResult);
            Assert.IsTrue(b);
        }
        [TestCategory("Game_Engine_Helper")]
        [TestMethod]
        public void getPlayerAction_PassRushers()
        {
            Game_Player p = new Game_Player();
            Play_Result pResult = new Play_Result();
            pResult.Pass_Rushers.Add(p);
            pResult.Penalized_Player = p;
            bool b = Game_Engine_Helper.isBallTeamPenalty(pResult);
            Assert.IsTrue(!b);
        }
        [TestCategory("Game_Engine_Helper")]
        [TestMethod]
        public void getPlayerAction_PassDefenders()
        {
            Game_Player p = new Game_Player();
            Play_Result pResult = new Play_Result();
            pResult.Pass_Defenders.Add(p);
            pResult.Penalized_Player = p;
            bool b = Game_Engine_Helper.isBallTeamPenalty(pResult);
            Assert.IsTrue(!b);
        }
        [TestCategory("Game_Engine_Helper")]
        [TestMethod]
        public void getPlayerAction_RunDefenders()
        {
            Game_Player p = new Game_Player();
            Play_Result pResult = new Play_Result();
            pResult.Run_Defenders.Add(p);
            pResult.Penalized_Player = p;
            bool b = Game_Engine_Helper.isBallTeamPenalty(pResult);
            Assert.IsTrue(!b);
        }
        [TestCategory("Game_Engine_Helper")]
        [TestMethod]
        public void getPlayerAction_KickReturners()
        {
            Game_Player p = new Game_Player();
            Play_Result pResult = new Play_Result();
            pResult.Kick_Returners.Add(p);
            pResult.Penalized_Player = p;
            bool b = Game_Engine_Helper.isBallTeamPenalty(pResult);
            Assert.IsTrue(b);
        }
        [TestCategory("Game_Engine_Helper")]
        [TestMethod]
        public void getPlayerAction_KickDefenders()
        {
            Game_Player p = new Game_Player();
            Play_Result pResult = new Play_Result();
            pResult.Kick_Defenders.Add(p);
            pResult.Penalized_Player = p;
            bool b = Game_Engine_Helper.isBallTeamPenalty(pResult);
            Assert.IsTrue(!b);
        }
        [TestCategory("Game_Engine_Helper")]
        [TestMethod]
        public void getPlayerAction_PuntReturners()
        {
            Game_Player p = new Game_Player();
            Play_Result pResult = new Play_Result();
            pResult.Punt_Returners.Add(p);
            pResult.Penalized_Player = p;
            bool b = Game_Engine_Helper.isBallTeamPenalty(pResult);
            Assert.IsTrue(b);
        }
        [TestCategory("Game_Engine_Helper")]
        [TestMethod]
        public void getPlayerAction_Field_Goal_Kicking_Team()
        {
            Game_Player p = new Game_Player();
            Play_Result pResult = new Play_Result();
            pResult.FieldGaol_Kicking_Team.Add(p);
            pResult.Penalized_Player = p;
            bool b = Game_Engine_Helper.isBallTeamPenalty(pResult);
            Assert.IsTrue(b);
        }
        [TestCategory("Game_Engine_Helper")]
        [TestMethod]
        public void getPlayerAction_Field_Goal_Defenders()
        {
            Game_Player p = new Game_Player();
            Play_Result pResult = new Play_Result();
            pResult.Field_Goal_Defenders.Add(p);
            pResult.Penalized_Player = p;
            bool b = Game_Engine_Helper.isBallTeamPenalty(pResult);
            Assert.IsTrue(!b);
        }
        [TestCategory("Game_Engine_Helper")]
        [TestMethod]
        public void isGameEnd__Regualr_3rdQuarterTied_false()
        {
            Assert.IsTrue(Game_Engine_Helper.isGameEnd(0, 3, 100, 10, 10) == false);
        }
        [TestCategory("Game_Engine_Helper")]
        [TestMethod]
        public void isGameEnd_Regualr_EndofGameTied_False()
        {
            Assert.IsTrue(Game_Engine_Helper.isGameEnd(0 ,4, 0, 10, 10) == false);
        }
        [TestCategory("Game_Engine_Helper")]
        [TestMethod]
        public void isGameEnd_Playeroffs_EndofGame_true()
        {
            Assert.IsTrue(Game_Engine_Helper.isGameEnd(1, 4, 0, 10, 20) == true);
        }
        [TestCategory("Game_Engine_Helper")]
        [TestMethod]
        public void isGameEnd_playoffs_OertimeTied_true()
        {
            Assert.IsTrue(Game_Engine_Helper.isGameEnd(1, 6, 100, 20, 20) == false);
        }
        [TestCategory("Game_Engine_Helper")]
        [TestMethod]
        public void isGameEnd_playoffs_FirstOertimeTied_true()
        {
            Assert.IsTrue(Game_Engine_Helper.isGameEnd(1, 5, 0, 20, 20) == false);
        }
        [TestCategory("Game_Engine_Helper")]
        [TestMethod]
        public void isGameEnd_Regualr_FirstOertimeTied_true()
        {
            Assert.IsTrue(Game_Engine_Helper.isGameEnd(0, 5, 0, 20, 20) == true);
        }
        [TestCategory("Game_Engine_Helper")]
        [TestMethod]
        public void getScrimmageLine_left_20()
        {
            Assert.IsTrue(Game_Engine_Helper.getScrimmageLine(20.0, true) == 20.0);
        }
        [TestCategory("Game_Engine_Helper")]
        [TestMethod]
        public void getScrimmageLine_right_20()
        {
            Assert.IsTrue(Game_Engine_Helper.getScrimmageLine(20.0, false) == 80.0);
        }
        [TestCategory("Game_Engine_Helper")]
        [TestMethod]
        public void setScoringBool_home_team_TD()
        {
            Play_Result pResult = new Play_Result()
            { BallPossessing_Team_Id = 11, ht = 11, at = 22, bTouchDown = true };

            pResult = Game_Engine_Helper.setScoringBool(pResult, false);

            Assert.IsTrue(pResult.bHomeTD == true && pResult.bHomeFG == false && pResult.bHomeXP == false &&
                pResult.bHomeXP1 == false && pResult.bHomeXP2 == false && pResult.bHomeXP3 == false &&
                pResult.bHomeSafetyFor == false &&
                pResult.bAwayTD == false && pResult.bAwayFG == false && pResult.bAwayXP == false &&
                pResult.bAwayXP1 == false && pResult.bAwayXP2 == false && pResult.bAwayXP3 == false &&
                pResult.bAwaySafetyFor == false);
        }

        [TestCategory("Game_Engine_Helper")]
        [TestMethod]
        public void setScoringBool_away_team_TD()
        {
            Play_Result pResult = new Play_Result()
            { BallPossessing_Team_Id = 22, ht = 11, at = 22, bTouchDown = true };

            pResult = Game_Engine_Helper.setScoringBool(pResult, false);

            Assert.IsTrue(pResult.bHomeTD == false && pResult.bHomeFG == false && pResult.bHomeXP == false &&
                pResult.bHomeXP1 == false && pResult.bHomeXP2 == false && pResult.bHomeXP3 == false &&
                pResult.bHomeSafetyFor == false &&
                pResult.bAwayTD == true && pResult.bAwayFG == false && pResult.bAwayXP == false &&
                pResult.bAwayXP1 == false && pResult.bAwayXP2 == false && pResult.bAwayXP3 == false &&
                pResult.bAwaySafetyFor == false);
        }


        [TestCategory("Game_Engine_Helper")]
        [TestMethod]
        public void setScoringBool_home_team_TD_Turnover()
        {
            Play_Result pResult = new Play_Result()
            { BallPossessing_Team_Id = 22, ht = 11, at = 22, bTouchDown = true};

            pResult = Game_Engine_Helper.setScoringBool(pResult, true);

            Assert.IsTrue(pResult.bHomeTD == true && pResult.bHomeFG == false && pResult.bHomeXP == false &&
                pResult.bHomeXP1 == false && pResult.bHomeXP2 == false && pResult.bHomeXP3 == false &&
                pResult.bHomeSafetyFor == false &&
                pResult.bAwayTD == false && pResult.bAwayFG == false && pResult.bAwayXP == false &&
                pResult.bAwayXP1 == false && pResult.bAwayXP2 == false && pResult.bAwayXP3 == false &&
                pResult.bAwaySafetyFor == false);
        }

        [TestCategory("Game_Engine_Helper")]
        [TestMethod]
        public void setScoringBool_away_team_TD_Turnover()
        {
            Play_Result pResult = new Play_Result()
            { BallPossessing_Team_Id = 11, ht = 11, at = 22, bTouchDown = true };

            pResult = Game_Engine_Helper.setScoringBool(pResult, true);

            Assert.IsTrue(pResult.bHomeTD == false && pResult.bHomeFG == false && pResult.bHomeXP == false &&
                pResult.bHomeXP1 == false && pResult.bHomeXP2 == false && pResult.bHomeXP3 == false &&
                pResult.bHomeSafetyFor == false &&
                pResult.bAwayTD == true && pResult.bAwayFG == false && pResult.bAwayXP == false &&
                pResult.bAwayXP1 == false && pResult.bAwayXP2 == false && pResult.bAwayXP3 == false &&
                pResult.bAwaySafetyFor == false);
        }

        [TestCategory("Game_Engine_Helper")]
        [TestMethod]
        public void setScoringBool_Home_team_FG()
        {
            Play_Result pResult = new Play_Result()
            { BallPossessing_Team_Id = 11, ht = 11, at = 22, bFGMade = true };

            pResult = Game_Engine_Helper.setScoringBool(pResult, false);

            Assert.IsTrue(pResult.bHomeTD == false && pResult.bHomeFG == true && pResult.bHomeXP == false &&
                pResult.bHomeXP1 == false && pResult.bHomeXP2 == false && pResult.bHomeXP3 == false &&
                pResult.bHomeSafetyFor == false &&
                pResult.bAwayTD == false && pResult.bAwayFG == false && pResult.bAwayXP == false &&
                pResult.bAwayXP1 == false && pResult.bAwayXP2 == false && pResult.bAwayXP3 == false &&
                pResult.bAwaySafetyFor == false);
        }

        [TestCategory("Game_Engine_Helper")]
        [TestMethod]
        public void setScoringBool_Away_team_FG()
        {
            Play_Result pResult = new Play_Result()
            { BallPossessing_Team_Id = 22, ht = 11, at = 22, bFGMade = true };

            pResult = Game_Engine_Helper.setScoringBool(pResult, false);

            Assert.IsTrue(pResult.bHomeTD == false && pResult.bHomeFG == false && pResult.bHomeXP == false &&
                pResult.bHomeXP1 == false && pResult.bHomeXP2 == false && pResult.bHomeXP3 == false &&
                pResult.bHomeSafetyFor == false &&
                pResult.bAwayTD == false && pResult.bAwayFG == true && pResult.bAwayXP == false &&
                pResult.bAwayXP1 == false && pResult.bAwayXP2 == false && pResult.bAwayXP3 == false &&
                pResult.bAwaySafetyFor == false);
        }

        [TestCategory("Game_Engine_Helper")]
        [TestMethod]
        public void setScoringBool_Home_team_XP()
        {
            Play_Result pResult = new Play_Result()
            { BallPossessing_Team_Id = 11, ht = 11, at = 22, bXPMade = true };

            pResult = Game_Engine_Helper.setScoringBool(pResult, false);

            Assert.IsTrue(pResult.bHomeTD == false && pResult.bHomeFG == false && pResult.bHomeXP == true &&
                pResult.bHomeXP1 == false && pResult.bHomeXP2 == false && pResult.bHomeXP3 == false &&
                pResult.bHomeSafetyFor == false &&
                pResult.bAwayTD == false && pResult.bAwayFG == false && pResult.bAwayXP == false &&
                pResult.bAwayXP1 == false && pResult.bAwayXP2 == false && pResult.bAwayXP3 == false &&
                pResult.bAwaySafetyFor == false);
        }

        [TestCategory("Game_Engine_Helper")]
        [TestMethod]
        public void setScoringBool_Away_team_XP()
        {
            Play_Result pResult = new Play_Result()
            { BallPossessing_Team_Id = 22, ht = 11, at = 22, bXPMade = true };

            pResult = Game_Engine_Helper.setScoringBool(pResult, false);

            Assert.IsTrue(pResult.bHomeTD == false && pResult.bHomeFG == false && pResult.bHomeXP == false &&
                pResult.bHomeXP1 == false && pResult.bHomeXP2 == false && pResult.bHomeXP3 == false &&
                pResult.bHomeSafetyFor == false &&
                pResult.bAwayTD == false && pResult.bAwayFG == false && pResult.bAwayXP == true &&
                pResult.bAwayXP1 == false && pResult.bAwayXP2 == false && pResult.bAwayXP3 == false &&
                pResult.bAwaySafetyFor == false);
        }


        [TestCategory("Game_Engine_Helper")]
        [TestMethod]
        public void setScoringBool_Home_team_XP1()
        {
            Play_Result pResult = new Play_Result()
            { BallPossessing_Team_Id = 11, ht = 11, at = 22, bOnePntAfterTDMade = true };

            pResult = Game_Engine_Helper.setScoringBool(pResult, false);

            Assert.IsTrue(pResult.bHomeTD == false && pResult.bHomeFG == false && pResult.bHomeXP == false &&
                pResult.bHomeXP1 == true && pResult.bHomeXP2 == false && pResult.bHomeXP3 == false &&
                pResult.bHomeSafetyFor == false &&
                pResult.bAwayTD == false && pResult.bAwayFG == false && pResult.bAwayXP == false &&
                pResult.bAwayXP1 == false && pResult.bAwayXP2 == false && pResult.bAwayXP3 == false &&
                pResult.bAwaySafetyFor == false);
        }

        [TestCategory("Game_Engine_Helper")]
        [TestMethod]
        public void setScoringBool_Away_team_XP1()
        {
            Play_Result pResult = new Play_Result()
            { BallPossessing_Team_Id = 22, ht = 11, at = 22, bOnePntAfterTDMade = true };

            pResult = Game_Engine_Helper.setScoringBool(pResult, false);

            Assert.IsTrue(pResult.bHomeTD == false && pResult.bHomeFG == false && pResult.bHomeXP == false &&
                pResult.bHomeXP1 == false && pResult.bHomeXP2 == false && pResult.bHomeXP3 == false &&
                pResult.bHomeSafetyFor == false &&
                pResult.bAwayTD == false && pResult.bAwayFG == false && pResult.bAwayXP == false &&
                pResult.bAwayXP1 == true && pResult.bAwayXP2 == false && pResult.bAwayXP3 == false &&
                pResult.bAwaySafetyFor == false);
        }

        [TestCategory("Game_Engine_Helper")]
        [TestMethod]
        public void setScoringBool_Home_team_XP2()
        {
            Play_Result pResult = new Play_Result()
            { BallPossessing_Team_Id = 11, ht = 11, at = 22, bTwoPntAfterTDMade = true };

            pResult = Game_Engine_Helper.setScoringBool(pResult, false);

            Assert.IsTrue(pResult.bHomeTD == false && pResult.bHomeFG == false && pResult.bHomeXP == false &&
                pResult.bHomeXP1 == false && pResult.bHomeXP2 == true && pResult.bHomeXP3 == false &&
                pResult.bHomeSafetyFor == false &&
                pResult.bAwayTD == false && pResult.bAwayFG == false && pResult.bAwayXP == false &&
                pResult.bAwayXP1 == false && pResult.bAwayXP2 == false && pResult.bAwayXP3 == false &&
                pResult.bAwaySafetyFor == false);
        }

        [TestCategory("Game_Engine_Helper")]
        [TestMethod]
        public void setScoringBool_Away_team_XP2()
        {
            Play_Result pResult = new Play_Result()
            { BallPossessing_Team_Id = 22, ht = 11, at = 22, bTwoPntAfterTDMade = true };

            pResult = Game_Engine_Helper.setScoringBool(pResult, false);

            Assert.IsTrue(pResult.bHomeTD == false && pResult.bHomeFG == false && pResult.bHomeXP == false &&
                pResult.bHomeXP1 == false && pResult.bHomeXP2 == false && pResult.bHomeXP3 == false &&
                pResult.bHomeSafetyFor == false &&
                pResult.bAwayTD == false && pResult.bAwayFG == false && pResult.bAwayXP == false &&
                pResult.bAwayXP1 == false && pResult.bAwayXP2 == true && pResult.bAwayXP3 == false &&
                pResult.bAwaySafetyFor == false);
        }

        [TestCategory("Game_Engine_Helper")]
        [TestMethod]
        public void setScoringBool_Home_team_XP3()
        {
            Play_Result pResult = new Play_Result()
            { BallPossessing_Team_Id = 11, ht = 11, at = 22, bThreePntAfterTDMade = true };

            pResult = Game_Engine_Helper.setScoringBool(pResult, false);

            Assert.IsTrue(pResult.bHomeTD == false && pResult.bHomeFG == false && pResult.bHomeXP == false &&
                pResult.bHomeXP1 == false && pResult.bHomeXP2 == false && pResult.bHomeXP3 == true &&
                pResult.bHomeSafetyFor == false &&
                pResult.bAwayTD == false && pResult.bAwayFG == false && pResult.bAwayXP == false &&
                pResult.bAwayXP1 == false && pResult.bAwayXP2 == false && pResult.bAwayXP3 == false &&
                pResult.bAwaySafetyFor == false);
        }

        [TestCategory("Game_Engine_Helper")]
        [TestMethod]
        public void setScoringBool_Away_team_XP3()
        {
            Play_Result pResult = new Play_Result()
            { BallPossessing_Team_Id = 22, ht = 11, at = 22, bThreePntAfterTDMade = true };

            pResult = Game_Engine_Helper.setScoringBool(pResult, false);

            Assert.IsTrue(pResult.bHomeTD == false && pResult.bHomeFG == false && pResult.bHomeXP == false &&
                pResult.bHomeXP1 == false && pResult.bHomeXP2 == false && pResult.bHomeXP3 == false &&
                pResult.bHomeSafetyFor == false &&
                pResult.bAwayTD == false && pResult.bAwayFG == false && pResult.bAwayXP == false &&
                pResult.bAwayXP1 == false && pResult.bAwayXP2 == false && pResult.bAwayXP3 == true &&
                pResult.bAwaySafetyFor == false);
        }

        [TestCategory("Game_Engine_Helper")]
        [TestMethod]
        public void setScoringBool_Home_team_Safety()
        {
            Play_Result pResult = new Play_Result()
            { BallPossessing_Team_Id = 22, ht = 11, at = 22, bSafety = true };

            pResult = Game_Engine_Helper.setScoringBool(pResult, false);

            Assert.IsTrue(pResult.bHomeTD == false && pResult.bHomeFG == false && pResult.bHomeXP == false &&
                pResult.bHomeXP1 == false && pResult.bHomeXP2 == false && pResult.bHomeXP3 == false &&
                pResult.bHomeSafetyFor == true &&
                pResult.bAwayTD == false && pResult.bAwayFG == false && pResult.bAwayXP == false &&
                pResult.bAwayXP1 == false && pResult.bAwayXP2 == false && pResult.bAwayXP3 == false &&
                pResult.bAwaySafetyFor == false);
        }

        [TestCategory("Game_Engine_Helper")]
        [TestMethod]
        public void setScoringBool_Away_team_Safety()
        {
            Play_Result pResult = new Play_Result()
            { BallPossessing_Team_Id = 11, ht = 11, at = 22, bSafety = true };

            pResult = Game_Engine_Helper.setScoringBool(pResult, false);

            Assert.IsTrue(pResult.bHomeTD == false && pResult.bHomeFG == false && pResult.bHomeXP == false &&
                pResult.bHomeXP1 == false && pResult.bHomeXP2 == false && pResult.bHomeXP3 == false &&
                pResult.bHomeSafetyFor == false &&
                pResult.bAwayTD == false && pResult.bAwayFG == false && pResult.bAwayXP == false &&
                pResult.bAwayXP1 == false && pResult.bAwayXP2 == false && pResult.bAwayXP3 == false &&
                pResult.bAwaySafetyFor == true);
        }
    }
}
