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

        [TestCategory("Unit")]
        [TestMethod]
        public void HorizontalAdj_True_result1()
        {
            Assert.IsTrue(Game_Engine_Helper.HorizontalAdj(true) == 1);
        }
        [TestCategory("Unit")]
        [TestMethod]
        public void HorizontalAdj_False_resultMinus1()
        {
            Assert.IsTrue(Game_Engine_Helper.HorizontalAdj(false) == -1);
        }
        [TestCategory("Unit")]
        [TestMethod]
        public void Switch_LefttoRight_true_return_false()
        {
            Assert.IsTrue(Game_Engine_Helper.Switch_LefttoRight(true) == false);
        }
        [TestCategory("Unit")]
        [TestMethod]
        public void Switch_LefttoRight_false_return_true()
        {
            Assert.IsTrue(Game_Engine_Helper.Switch_LefttoRight(false) == true);
        }
        [TestCategory("Unit")]
        [TestMethod]
        public void getYardsGained_left_10_yardgame()
        {
            Assert.IsTrue(Game_Engine_Helper.getYardsGained(true, 20, 30) == 10);
        }
        [TestCategory("Unit")]
        [TestMethod]
        public void getYardsGained_left_loss_5_yards()
        {
            Assert.IsTrue(Game_Engine_Helper.getYardsGained(true, 20, 15) == -5);
        }
        [TestCategory("Unit")]
        [TestMethod]
        public void getYardsGained_left_2_yard_game_into_EZ()
        {
            Assert.IsTrue(Game_Engine_Helper.getYardsGained(true, 98, 102) == 2);
        }
        [TestCategory("Unit")]
        [TestMethod]
        public void getYardsGained_left_2_yard_loss_into_EZ()
        {
            Assert.IsTrue(Game_Engine_Helper.getYardsGained(true, 2, -2) == -2);
        }
        [TestCategory("Unit")]
        [TestMethod]
        public void getYardsGained_right_10_yardgame()
        {
            Assert.IsTrue(Game_Engine_Helper.getYardsGained(false, 30, 20) == 10);
        }
        [TestCategory("Unit")]
        [TestMethod]
        public void getYardsGained_right_loss_5_yards()
        {
            Assert.IsTrue(Game_Engine_Helper.getYardsGained(false, 15, 20) == -5);
        }
        [TestCategory("Unit")]
        [TestMethod]
        public void getYardsGained_right_2_yard_game_into_EZ()
        {
            Assert.IsTrue(Game_Engine_Helper.getYardsGained(false, 2, -2) == 2);
        }
        [TestCategory("Unit")]
        [TestMethod]
        public void getYardsGained_right_2_yard_loss_into_EZ()
        {
            Assert.IsTrue(Game_Engine_Helper.getYardsGained(false, 98, 102) == -2);
        }
        [TestCategory("Unit")]
        [TestMethod]
        public void isTouchdown_left_noTD()
        {
            Assert.IsTrue(Game_Engine_Helper.isTouchdown(false, 2.0, false) == false);
        }
        [TestCategory("Unit")]
        [TestMethod]
        public void isTouchdown_left_TD()
        {
            Assert.IsTrue(Game_Engine_Helper.isTouchdown(false, -2.0, false) == true);
        }
        [TestCategory("Unit")]
        [TestMethod]
        public void isTouchdown_right_noTD()
        {
            Assert.IsTrue(Game_Engine_Helper.isTouchdown(true, 98.0, false) == false);
        }
        [TestCategory("Unit")]
        [TestMethod]
        public void isTouchdown_right_TD()
        {
            Assert.IsTrue(Game_Engine_Helper.isTouchdown(true, 102.0, false) == true);
        }

        [TestCategory("Unit")]
        [TestMethod]
        public void calcDistanceFromOpponentGL_left()
        {
            Assert.IsTrue(Game_Engine_Helper.calcDistanceFromOpponentGL(85, true) == 15);
        }
        [TestCategory("Unit")]
        [TestMethod]
        public void calcDistanceFromMyGL_left()
        {
            Assert.IsTrue(Game_Engine_Helper.calcDistanceFromMyGL(85, true) == 85);
        }
        [TestCategory("Unit")]
        [TestMethod]
        public void calcDistanceFromMyGL_right()
        {
            Assert.IsTrue(Game_Engine_Helper.calcDistanceFromMyGL(85, false) == 15);
        }
        [TestCategory("Unit")]
        [TestMethod]
        public void calcDistanceFromOpponentGL_right()
        {
            Assert.IsTrue(Game_Engine_Helper.calcDistanceFromOpponentGL(85, false) == 85);
        }
        [TestCategory("Unit")]
        [TestMethod]
        public void getScrimmageLine_left_20()
        {
            Assert.IsTrue(Game_Engine_Helper.getScrimmageLine(20.0, true) == 20.0);
        }
        [TestCategory("Unit")]
        [TestMethod]
        public void getScrimmageLine_right_20()
        {
            Assert.IsTrue(Game_Engine_Helper.getScrimmageLine(20.0, false) == 80.0);
        }
        [TestCategory("Unit")]
        [TestMethod]
        public void isCCEligible_and_Punt_long_Enough_not_eligible_left()
        {
            Tuple<bool, bool> t = Game_Engine_Helper.isCCEligible_and_Punt_long_Enough(50.0, 10.0, true);
            Assert.IsTrue(!t.Item1 && !t.Item2);
        }
        [TestCategory("Unit")]
        [TestMethod]
        public void isCCEligible_and_Punt_long_Enough_not_eligible_right()
        {
            Tuple<bool, bool> t = Game_Engine_Helper.isCCEligible_and_Punt_long_Enough(50.0, 90.0, false);
            Assert.IsTrue(!t.Item1 && !t.Item2);
        }
        [TestCategory("Unit")]
        [TestMethod]
        public void isCCEligible_and_Punt_long_Enough_eligible_long_enough_left()
        {
            Tuple<bool, bool> t = Game_Engine_Helper.isCCEligible_and_Punt_long_Enough(43.0, 50.0, true);
            Assert.IsTrue(t.Item1 && t.Item2);
        }
        [TestCategory("Unit")]
        [TestMethod]
        public void isCCEligible_and_Punt_long_Enough_eligible_long_enough_right()
        {
            Tuple<bool, bool> t = Game_Engine_Helper.isCCEligible_and_Punt_long_Enough(43.0, 50.0, false);
            Assert.IsTrue(t.Item1 && t.Item2);
        }
        [TestCategory("Unit")]
        [TestMethod]
        public void isCCEligible_and_Punt_long_Enough_eligible_long_not_enough_left()
        {
            Tuple<bool, bool> t = Game_Engine_Helper.isCCEligible_and_Punt_long_Enough(42.0, 45.0, true);
            Assert.IsTrue(t.Item1 && !t.Item2);
        }
        [TestCategory("Unit")]
        [TestMethod]
        public void isCCEligible_and_Punt_long_Enough_eligible_long_not_enough_right()
        {
            Tuple<bool, bool> t = Game_Engine_Helper.isCCEligible_and_Punt_long_Enough(42.0, 55.0, false);
            Assert.IsTrue(t.Item1 && !t.Item2);
        }

        [TestCategory("Unit")]
        [TestMethod]
        public void getPuntLandingSpot_normal_punt_left()
        {
            bool bTop = false;
            int rtemp = 10;
            Tuple<double, double> t = Game_Engine_Helper.getPuntLandingSpot(false, false, false, 40.0, 35.0, 20.0, bTop, rtemp, true);
            Assert.IsTrue(t.Item1 == 60.0  && t.Item2 == 35.0);
        }

        [TestCategory("Unit")]
        [TestMethod]
        public void getPuntLandingSpot_normal_punt_out_endzone_left()
        {
            bool bTop = false;
            int rtemp = 10;
            Tuple<double, double> t = Game_Engine_Helper.getPuntLandingSpot(false, false, false, 90.0, 49.0, 40.0, bTop, rtemp, true);
            Assert.IsTrue(t.Item1 == 121.0 && t.Item2 == 49.0);
        }

        [TestCategory("Unit")]
        [TestMethod]
        public void getPuntLandingSpot_CCEligible_not_long_enough()
        {
            bool bTop = false;
            int rtemp = 10;
            Tuple<double, double> t = Game_Engine_Helper.getPuntLandingSpot(true, false, false, 35.0, 40.0, 50.0, bTop, rtemp, true);
            Assert.IsTrue(t.Item1 == 85.0 && t.Item2 == 40.0);
        }

        [TestCategory("Unit")]
        [TestMethod]
        public void getPuntLandingSpot_Normal_Punt_left()
        {
            bool bTop = false;
            int rtemp = 10;
            Tuple<double, double> t = Game_Engine_Helper.getPuntLandingSpot(false, false, false, 55.0, 40.0, 20.0, bTop, rtemp, true);
            Assert.IsTrue(t.Item1 == 75.0 && t.Item2 == 40.0);
        }

        [TestCategory("Unit")]
        [TestMethod]
        public void getPuntLandingSpot_CCEligible_Missed_botton_left()
        {
            bool bTop = false;
            int rtemp = 10;
            Tuple<double, double> t = Game_Engine_Helper.getPuntLandingSpot(true, true, false, 55.0, 40.0, 50.0, bTop, rtemp, true);
            Assert.IsTrue(t.Item2 <= 99.0 && t.Item2 >= 1.0);
        }

        [TestCategory("Unit")]
        [TestMethod]
        public void getPuntLandingSpot_CCEligible_Missed_top_left()
        {
            bool bTop = true;
            int rtemp = 10;
            Tuple<double, double> t = Game_Engine_Helper.getPuntLandingSpot(true, true, false, 55.0, 40.0, 50.0, bTop, rtemp, true);
            Assert.IsTrue(t.Item2 <= 99.0 && t.Item2 >= 1.0);
        }

        [TestCategory("Unit")]
        [TestMethod]
        public void getPuntLandingSpot_CCEligible_Made_botton_left()
        {
            bool bTop = false;
            int rtemp = 10;
            Tuple<double, double> t = Game_Engine_Helper.getPuntLandingSpot(true, true, true, 55.0, 40.0, 50.0, bTop, rtemp, true);
            Assert.IsTrue(t.Item1 >= 90.0 && t.Item1 <= 99.0 && t.Item2 == 101.0);
        }

        [TestCategory("Unit")]
        [TestMethod]
        public void getPuntLandingSpot_CCEligible_Made_top_left()
        {
            bool bTop = true;
            int rtemp = 10;
            Tuple<double, double> t = Game_Engine_Helper.getPuntLandingSpot(true, true, true, 55.0, 40.0, 50.0, bTop, rtemp, true);
            Assert.IsTrue(t.Item1 >= 90.0 && t.Item1 <= 99.0 && t.Item2 == -1.0);
        }

        [TestCategory("Unit")]
        [TestMethod]
        public void getPuntLandingSpot_Normal_Punt_right()
        {
            bool bTop = false;
            int rtemp = 10;
            Tuple<double, double> t = Game_Engine_Helper.getPuntLandingSpot(false, false, false, 55.0, 40.0, 80.0, bTop, rtemp, false);
            Assert.IsTrue(t.Item1 == 25.0 && t.Item2 == 40.0);
        }

        [TestCategory("Unit")]
        [TestMethod]
        public void getPuntLandingSpot_CCEligible_Missed_botton_right()
        {
            bool bTop = false;
            int rtemp = 10;
            Tuple<double, double> t = Game_Engine_Helper.getPuntLandingSpot(true, true, false, 55.0, 40.0, 50.0, bTop, rtemp, false);
            Assert.IsTrue(t.Item2 <= 99.0 && t.Item2 >= 1.0);
        }

        [TestCategory("Unit")]
        [TestMethod]
        public void getPuntLandingSpot_CCEligible_Missed_top_right()
        {
            bool bTop = true;
            int rtemp = 10;
            Tuple<double, double> t = Game_Engine_Helper.getPuntLandingSpot(true, true, false, 55.0, 40.0, 50.0, bTop, rtemp, false);
            Assert.IsTrue(t.Item2 <= 99.0 && t.Item2 >= 1.0);
        }

        [TestCategory("Unit")]
        [TestMethod]
        public void getPuntLandingSpot_CCEligible_Made_botton_right()
        {
            bool bTop = false;
            int rtemp = 10;
            Tuple<double, double> t = Game_Engine_Helper.getPuntLandingSpot(true, true, true, 55.0, 40.0, 50.0, bTop, rtemp, false);
            Assert.IsTrue(t.Item1 >= 1.0 && t.Item1 <= 10.0 && t.Item2 == 101.0);
        }

        [TestCategory("Unit")]
        [TestMethod]
        public void getPuntLandingSpot_CCEligible_Made_top_right()
        {
            bool bTop = true;
            int rtemp = 10;
            Tuple<double, double> t = Game_Engine_Helper.getPuntLandingSpot(true, true, true, 55.0, 40.0, 50.0, bTop, rtemp, false);
            Assert.IsTrue(t.Item1 >= 1.0 && t.Item1 <= 10.0 && t.Item2 == -1.0);
        }

        [TestCategory("Unit")]
        [TestMethod]
        public void isPuntCatchable_10_13()
        {
            Assert.IsTrue(Game_Engine_Helper.isPuntCatchable(10,13));
        }

        [TestCategory("Unit")]
        [TestMethod]
        public void isPuntCatchable_m1_44()
        {
            Assert.IsTrue(!Game_Engine_Helper.isPuntCatchable(-1, 44));
        }

        [TestCategory("Unit")]
        [TestMethod]
        public void isPuntCatchable_50_m10()
        {
            Assert.IsTrue(!Game_Engine_Helper.isPuntCatchable(50, -10));
        }

        [TestCategory("Unit")]
        [TestMethod]
        public void isPuntCatchable_101_13()
        {
            Assert.IsTrue(!Game_Engine_Helper.isPuntCatchable(101, 13));
        }

        [TestCategory("Unit")]
        [TestMethod]
        public void isPuntCatchable_100_10()
        {
            Assert.IsTrue(!Game_Engine_Helper.isPuntCatchable(100, 10));
        }

        [TestCategory("Unit")]
        [TestMethod]
        public void isPuntCatchable_0_0()
        {
            Assert.IsTrue(!Game_Engine_Helper.isPuntCatchable(0, 0));
        }

        [TestCategory("Unit")]
        [TestMethod]
        public void BlockedPuntTD_or_Safety_p_recovers_no_safety_or_TD_Left()
        {
            Tuple<bool, bool> t = Game_Engine_Helper.BlockedPuntTD_or_Safety(true, 0.1, true);

            Assert.IsTrue(!t.Item1 && !t.Item2);
        }

        [TestCategory("Unit")]
        [TestMethod]
        public void BlockedPuntTD_or_Safety_r_recovers_no_safety_or_TD_Left()
        {
            Tuple<bool, bool> t = Game_Engine_Helper.BlockedPuntTD_or_Safety(false, 0.1, true);

            Assert.IsTrue(!t.Item1 && !t.Item2);
        }

        [TestCategory("Unit")]
        [TestMethod]
        public void BlockedPuntTD_or_Safety_p_recovers_safety_Left()
        {
            Tuple<bool, bool> t = Game_Engine_Helper.BlockedPuntTD_or_Safety(true, -0.1, true);

            Assert.IsTrue(!t.Item1 && t.Item2);
        }

        [TestCategory("Unit")]
        [TestMethod]
        public void BlockedPuntTD_or_Safety_p_recovers_TD_Left()
        {
            Tuple<bool, bool> t = Game_Engine_Helper.BlockedPuntTD_or_Safety(false, 0.0, true);

            Assert.IsTrue(t.Item1 && !t.Item2);
        }

        [TestCategory("Unit")]
        [TestMethod]
        public void BlockedPuntTD_or_Safety_p_recovers_no_safety_or_TD_Right()
        {
            Tuple<bool, bool> t = Game_Engine_Helper.BlockedPuntTD_or_Safety(true, 99.9, false);

            Assert.IsTrue(!t.Item1 && !t.Item2);
        }

        [TestCategory("Unit")]
        [TestMethod]
        public void BlockedPuntTD_or_Safety_r_recovers_no_safety_or_TD_Right()
        {
            Tuple<bool, bool> t = Game_Engine_Helper.BlockedPuntTD_or_Safety(false, 99.9, false);

            Assert.IsTrue(!t.Item1 && !t.Item2);
        }

        [TestCategory("Unit")]
        [TestMethod]
        public void BlockedPuntTD_or_Safety_p_recovers_safety_Right()
        {
            Tuple<bool, bool> t = Game_Engine_Helper.BlockedPuntTD_or_Safety(true, 101.1, false);

            Assert.IsTrue(!t.Item1 && t.Item2);
        }

        [TestCategory("Unit")]
        [TestMethod]
        public void BlockedPuntTD_or_Safety_p_recovers_TD_Right()
        {
            Tuple<bool, bool> t = Game_Engine_Helper.BlockedPuntTD_or_Safety(false, 101.1, false);

            Assert.IsTrue(t.Item1 && !t.Item2);
        }
        [TestCategory("Unit")]
        [TestMethod]
        public void getPuntYards_Normal_Punt_Left()
        {
            double len = Game_Engine_Helper.getPuntYards(20.0, 59.9, true);

            Assert.IsTrue(len == 39.9);
        }
        [TestCategory("Unit")]
        [TestMethod]
        public void getPuntYards_Out_of_bounds_Left()
        {
            double len = Game_Engine_Helper.getPuntYards(20.0, 60.0, true);

            Assert.IsTrue(len == 40.0);
        }
        [TestCategory("Unit")]
        [TestMethod]
        public void getPuntYards_Downed_in_EZ_Left()
        {
            double len = Game_Engine_Helper.getPuntYards(60.0, 105.0, true);

            Assert.IsTrue(len == 40.0);
        }
        [TestCategory("Unit")]
        [TestMethod]
        public void getPuntYards_Out_of_EZ_Left()
        {
            double len = Game_Engine_Helper.getPuntYards(60.0, 115.0, true);

            Assert.IsTrue(len == 40.0);
        }

        [TestCategory("Unit")]
        [TestMethod]
        public void getPuntYards_Normal_Punt_Right()
        {
            double len = Game_Engine_Helper.getPuntYards(80.0, 40.1, false);

            Assert.IsTrue(len == 39.9);
        }
        [TestCategory("Unit")]
        [TestMethod]
        public void getPuntYards_Out_of_bounds_Right()
        {
            double len = Game_Engine_Helper.getPuntYards(80.0, 40.0, false);

            Assert.IsTrue(len == 40.0);
        }
        [TestCategory("Unit")]
        [TestMethod]
        public void getPuntYards_Downed_in_EZ_Right()
        {
            double len = Game_Engine_Helper.getPuntYards(40.0, -5.0, false);

            Assert.IsTrue(len == 40.0);
        }
        [TestCategory("Unit")]
        [TestMethod]
        public void getPuntYards_Out_of_EZ_Right()
        {
            double len = Game_Engine_Helper.getPuntYards(40.0, -15.0, false);

            Assert.IsTrue(len == 40.0);
        }

        [TestCategory("Unit")]
        [TestMethod]
        public void isBallOutofBounds_not_out_top()
        { 
            Assert.IsTrue(!Game_Engine_Helper.isBallOutofBounds(1.0));
        }

        [TestCategory("Unit")]
        [TestMethod]
        public void isBallOutofBounds_not_out_bottom()
        {
            Assert.IsTrue(!Game_Engine_Helper.isBallOutofBounds(99.0));
        }

        [TestCategory("Unit")]
        [TestMethod]
        public void isBallOutofBounds_out_top()
        {
            Assert.IsTrue(Game_Engine_Helper.isBallOutofBounds(-1.0));
        }

        [TestCategory("Unit")]
        [TestMethod]
        public void isBallOutofBounds_out_bottom()
        {
            Assert.IsTrue(Game_Engine_Helper.isBallOutofBounds(100.1));
        }
    }
}
