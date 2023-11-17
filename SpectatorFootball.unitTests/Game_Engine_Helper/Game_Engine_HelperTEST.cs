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

namespace SpectatorFootball.unitTests.GameEngine_HelperTEST
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
        [TestCategory("GameEngine")]
        [TestMethod]
        public void calcDistanceFromMyGL_left()
        {
            Assert.IsTrue(Game_Engine_Helper.calcDistanceFromMyGL(85, true) == 85);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void calcDistanceFromMyGL_right()
        {
            Assert.IsTrue(Game_Engine_Helper.calcDistanceFromMyGL(85, false) == 15);
        }
        [TestCategory("Game_Engine_Helper")]
        [TestMethod]
        public void calcDistanceFromOpponentGL_right()
        {
            Assert.IsTrue(Game_Engine_Helper.calcDistanceFromOpponentGL(85, false) == 85);
        }

    }
}
