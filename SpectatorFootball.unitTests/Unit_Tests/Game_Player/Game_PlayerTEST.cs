using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpectatorFootball.GameNS;
using SpectatorFootball.Models;
using SpectatorFootball.unitTests.Helper_ClassesNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpectatorFootball.PlayerNS;

namespace SpectatorFootball.unitTests.Unit_Tests.Game_PlayerTest
{
    [TestClass]
    public class Game_PlayerTEST
    {
        [TestCategory("Unit")]
        [TestMethod]
        public void getReturnerAction_OutofBounds_Top_Left()
        {
            Game_Player gp = Help_Class.getPunter(11);
            Player_Ratings pr = gp.p_and_r.pr.First();
            pr.Decision_Making_Rating = 40;
            var t = gp.getReturnerAction(50.0, -1.0, false, true);

            Assert.IsTrue(t.Item1 && !t.Item2 && !t.Item3 && !t.Item4 && !t.Item5);
        }
        [TestCategory("Unit")]
        [TestMethod]
        public void getReturnerAction_OutofBounds_Bottum_Left()
        {
            Game_Player gp = Help_Class.getPunter(11);
            Player_Ratings pr = gp.p_and_r.pr.First();
            pr.Decision_Making_Rating = 40;
            var t = gp.getReturnerAction(50.0, 101.0, false, true);

            Assert.IsTrue(t.Item1 && !t.Item2 && !t.Item3 && !t.Item4 && !t.Item5);
        }
        [TestCategory("Unit")]
        [TestMethod]
        public void getReturnerAction_OutofEndZone_Bottum_Left()
        {
            Game_Player gp = Help_Class.getPunter(11);
            Player_Ratings pr = gp.p_and_r.pr.First();
            pr.Decision_Making_Rating = 40;
            var t = gp.getReturnerAction(111.0, 51.0, false, true);

            Assert.IsTrue(!t.Item1 && t.Item2 && !t.Item3 && !t.Item4 && !t.Item5);
        }
        [TestCategory("Unit")]
        [TestMethod]
        public void getReturnerAction_DontField_Left()
        {
            Game_Player gp = Help_Class.getPunter(11);
            Player_Ratings pr = gp.p_and_r.pr.First();
            pr.Decision_Making_Rating = 40;
            var t = gp.getReturnerAction(96.0, 51.0, false, true);

            Assert.IsTrue(!t.Item1 && !t.Item2 && !t.Item3 && !t.Item4 && t.Item5);
        }

        [TestCategory("Unit")]
        [TestMethod]
        public void getReturnerAction_lastPlay_inEndZone_Return_Left()
        {
            Game_Player gp = Help_Class.getPunter(11);
            Player_Ratings pr = gp.p_and_r.pr.First();
            pr.Decision_Making_Rating = 40;
            var t = gp.getReturnerAction(105.0, 51.0, true, true);

            Assert.IsTrue(!t.Item1 && !t.Item2 && !t.Item3 && t.Item4 && !t.Item5);
        }

        [TestCategory("Unit")]
        [TestMethod]
        public void getReturnerAction_inEndZone_Kneel_Left()
        {
            Game_Player gp = Help_Class.getPunter(11);
            Player_Ratings pr = gp.p_and_r.pr.First();
            pr.Decision_Making_Rating = 40;
            var t = gp.getReturnerAction(105.0, 51.0, false, true);

            Assert.IsTrue(!t.Item1 && !t.Item2 && t.Item3 && !t.Item4 && !t.Item5);
        }

        [TestCategory("Unit")]
        [TestMethod]
        public void getReturnerAction_Regular_Return_Left()
        {
            Game_Player gp = Help_Class.getPunter(11);
            Player_Ratings pr = gp.p_and_r.pr.First();
            pr.Decision_Making_Rating = 40;
            var t = gp.getReturnerAction(80.0, 51.0, false, true);

            Assert.IsTrue(!t.Item1 && !t.Item2 && !t.Item3 && t.Item4 && !t.Item5);
        }

        [TestCategory("Unit")]
        [TestMethod]
        public void getReturnerAction_OutofBounds_Top_Right()
        {
            Game_Player gp = Help_Class.getPunter(11);
            Player_Ratings pr = gp.p_and_r.pr.First();
            pr.Decision_Making_Rating = 40;
            var t = gp.getReturnerAction(50.0, -1.0, false, false);

            Assert.IsTrue(t.Item1 && !t.Item2 && !t.Item3 && !t.Item4 && !t.Item5);
        }
        [TestCategory("Unit")]
        [TestMethod]
        public void getReturnerAction_OutofBounds_Bottum_Right()
        {
            Game_Player gp = Help_Class.getPunter(11);
            Player_Ratings pr = gp.p_and_r.pr.First();
            pr.Decision_Making_Rating = 40;
            var t = gp.getReturnerAction(50.0, 101.0, false, false);

            Assert.IsTrue(t.Item1 && !t.Item2 && !t.Item3 && !t.Item4 && !t.Item5);
        }
        [TestCategory("Unit")]
        [TestMethod]
        public void getReturnerAction_OutofEndZone_Bottum_Right()
        {
            Game_Player gp = Help_Class.getPunter(11);
            Player_Ratings pr = gp.p_and_r.pr.First();
            pr.Decision_Making_Rating = 40;
            var t = gp.getReturnerAction(-11.0, 51.0, false, false);

            Assert.IsTrue(!t.Item1 && t.Item2 && !t.Item3 && !t.Item4 && !t.Item5);
        }
        [TestCategory("Unit")]
        [TestMethod]
        public void getReturnerAction_DontField_Right()
        {
            Game_Player gp = Help_Class.getPunter(11);
            Player_Ratings pr = gp.p_and_r.pr.First();
            pr.Decision_Making_Rating = 40;
            var t = gp.getReturnerAction(4.0, 51.0, false, false);

            Assert.IsTrue(!t.Item1 && !t.Item2 && !t.Item3 && !t.Item4 && t.Item5);
        }

        [TestCategory("Unit")]
        [TestMethod]
        public void getReturnerAction_lastPlay_inEndZone_Return_Right()
        {
            Game_Player gp = Help_Class.getPunter(11);
            Player_Ratings pr = gp.p_and_r.pr.First();
            pr.Decision_Making_Rating = 40;
            var t = gp.getReturnerAction(-5.0, 51.0, true, false);

            Assert.IsTrue(!t.Item1 && !t.Item2 && !t.Item3 && t.Item4 && !t.Item5);
        }

        [TestCategory("Unit")]
        [TestMethod]
        public void getReturnerAction_inEndZone_Kneel_Right()
        {
            Game_Player gp = Help_Class.getPunter(11);
            Player_Ratings pr = gp.p_and_r.pr.First();
            pr.Decision_Making_Rating = 40;
            var t = gp.getReturnerAction(-5.0, 51.0, false, false);

            Assert.IsTrue(!t.Item1 && !t.Item2 && t.Item3 && !t.Item4 && !t.Item5);
        }

        [TestCategory("Unit")]
        [TestMethod]
        public void getReturnerAction_Regular_Return_Right()
        {
            Game_Player gp = Help_Class.getPunter(11);
            Player_Ratings pr = gp.p_and_r.pr.First();
            pr.Decision_Making_Rating = 40;
            var t = gp.getReturnerAction(20.0, 51.0, false,  false);

            Assert.IsTrue(!t.Item1 && !t.Item2 && !t.Item3 && t.Item4 && !t.Item5);
        }
        [TestCategory("Unit")]
        [TestMethod]
        public void getClassicKickoff_ReturnerAction_OutofEndZone_Bottum_Left()
        {
            Game_Player gp = Help_Class.getPunter(11);
            Player_Ratings pr = gp.p_and_r.pr.First();
            pr.Decision_Making_Rating = 40;
            var t = gp.getKickoff_ReturnerAction(111.0, false, 50, true);

            Assert.IsTrue(t.Item1 && !t.Item2 && !t.Item3);
        }
        [TestCategory("Unit")]
        [TestMethod]
        public void getClassicKickoff_ReturnerAction_Return_close_to_goalLine_Left()
        {
            Game_Player gp = Help_Class.getPunter(11);
            Player_Ratings pr = gp.p_and_r.pr.First();
            pr.Decision_Making_Rating = 40;
            var t = gp.getKickoff_ReturnerAction(96.0, false, 99, true);

            Assert.IsTrue(!t.Item1 && !t.Item2 && t.Item3);
        }

        [TestCategory("Unit")]
        [TestMethod]
        public void getClassicKickoff_ReturnerAction_inEndZone_Return_Left()
        {
            Game_Player gp = Help_Class.getPunter(11);
            Player_Ratings pr = gp.p_and_r.pr.First();
            pr.Decision_Making_Rating = 40;
            var t = gp.getKickoff_ReturnerAction(105.0, false, 100, true);

            Assert.IsTrue(!t.Item1 && !t.Item2 && t.Item3);
        }

        [TestCategory("Unit")]
        [TestMethod]
        public void getClassicKickoff_ReturnerAction_lastPlay_inEndZone_Return_Left()
        {
            Game_Player gp = Help_Class.getPunter(11);
            Player_Ratings pr = gp.p_and_r.pr.First();
            pr.Decision_Making_Rating = 40;
            var t = gp.getKickoff_ReturnerAction(105.0, true, 10, true);

            Assert.IsTrue(!t.Item1 && !t.Item2 && t.Item3);
        }

        [TestCategory("Unit")]
        [TestMethod]
        public void getClassicKickoff_ReturnerAction_inEndZone_Kneel_Left()
        {
            Game_Player gp = Help_Class.getPunter(11);
            Player_Ratings pr = gp.p_and_r.pr.First();
            pr.Decision_Making_Rating = 40;
            var t = gp.getKickoff_ReturnerAction(105.0, false, 10, true);

            Assert.IsTrue(!t.Item1 && t.Item2 && !t.Item3);
        }

        [TestCategory("Unit")]
        [TestMethod]
        public void getClassicKickoff_ReturnerAction_Regular_Return_Left()
        {
            Game_Player gp = Help_Class.getPunter(11);
            Player_Ratings pr = gp.p_and_r.pr.First();
            pr.Decision_Making_Rating = 40;
            var t = gp.getKickoff_ReturnerAction(80.0, false, 10, true);

            Assert.IsTrue(!t.Item1 && !t.Item2 && t.Item3);
        }
        [TestCategory("Unit")]
        [TestMethod]
        public void getClassicKickoff_ReturnerAction_OutofEndZone_Bottum_Right()
        {
            Game_Player gp = Help_Class.getPunter(11);
            Player_Ratings pr = gp.p_and_r.pr.First();
            pr.Decision_Making_Rating = 40;
            var t = gp.getKickoff_ReturnerAction(-11.0, false, 50, false);

            Assert.IsTrue(t.Item1 && !t.Item2 && !t.Item3);
        }
        [TestCategory("Unit")]
        [TestMethod]
        public void getClassicKickoff_ReturnerAction_Return_close_to_goalLine_Right()
        {
            Game_Player gp = Help_Class.getPunter(11);
            Player_Ratings pr = gp.p_and_r.pr.First();
            pr.Decision_Making_Rating = 40;
            var t = gp.getKickoff_ReturnerAction(4.0, false, 99, false);

            Assert.IsTrue(!t.Item1 && !t.Item2 && t.Item3);
        }

        [TestCategory("Unit")]
        [TestMethod]
        public void getClassicKickoff_ReturnerAction_inEndZone_Return_Right()
        {
            Game_Player gp = Help_Class.getPunter(11);
            Player_Ratings pr = gp.p_and_r.pr.First();
            pr.Decision_Making_Rating = 40;
            var t = gp.getKickoff_ReturnerAction(-5.0, false, 99, false);

            Assert.IsTrue(!t.Item1 && !t.Item2 && t.Item3);
        }

        [TestCategory("Unit")]
        [TestMethod]
        public void getClassicKickoff_ReturnerAction_lastPlay_inEndZone_Return_Right()
        {
            Game_Player gp = Help_Class.getPunter(11);
            Player_Ratings pr = gp.p_and_r.pr.First();
            pr.Decision_Making_Rating = 40;
            var t = gp.getKickoff_ReturnerAction(-5.0,  true, 10, false);

            Assert.IsTrue(!t.Item1 && !t.Item2 && t.Item3);
        }

        [TestCategory("Unit")]
        [TestMethod]
        public void getClassicKickoff_ReturnerAction_inEndZone_Kneel_Right()
        {
            Game_Player gp = Help_Class.getPunter(11);
            Player_Ratings pr = gp.p_and_r.pr.First();
            pr.Decision_Making_Rating = 40;
            var t = gp.getKickoff_ReturnerAction(-5.0,  false, 10, false);

            Assert.IsTrue(!t.Item1 && t.Item2 && !t.Item3);
        }

        [TestCategory("Unit")]
        [TestMethod]
        public void getClassicKickoff_ReturnerAction_Regular_Return_Right()
        {
            Game_Player gp = Help_Class.getPunter(11);
            Player_Ratings pr = gp.p_and_r.pr.First();
            pr.Decision_Making_Rating = 40;
            var t = gp.getKickoff_ReturnerAction(20.0, false, 10, false);

            Assert.IsTrue(!t.Item1 && !t.Item2 && t.Item3);
        }
    }
}
