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

    }
}
