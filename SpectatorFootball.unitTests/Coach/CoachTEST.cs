using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpectatorFootball.Models;
using SpectatorFootball.GameNS;
using SpectatorFootball.PenaltiesNS;

namespace SpectatorFootball.unitTests.CoachTest
{
    [TestClass]
    public class CoachTEST
    {
        [TestCategory("Coach")]
        [TestMethod]
        public void setOurThereScore_oursisAway()
        {
            Game g = new Game() { Away_Team_Franchise_ID = 22, Home_Team_Franchise_ID = 11, Away_Score = 22, Home_Score = 11 };

            Coach c = new Coach(22, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            Tuple<long, long> t = c.setOurThereScore();

            Assert.IsTrue(t.Item1 == 22 && t.Item2 == 11);
        }
        [TestCategory("Coach")]
        [TestMethod]
        public void setOurThereScore_oursisHome()
        {
            Game g = new Game() { Away_Team_Franchise_ID = 22, Home_Team_Franchise_ID = 11, Away_Score = 22, Home_Score = 11 };

            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            Tuple<long, long> t = c.setOurThereScore();

            Assert.IsTrue(t.Item1 == 11 && t.Item2 == 22);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptDef_Penalty_Turnover()
        {
            int Yards_to_go = 5;
            double Line_of_Scrimmage = 50.0;
            Penalty p = new Penalty() { bDeclinable = true };
            Play_Result pResult = new Play_Result() { Yards_Gained = 0, bInterception = true, Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptDef_Penalty(Enum.Play_Enum.RUN, pResult, Yards_to_go,Line_of_Scrimmage, true, false, false);


            Assert.IsTrue(bAccept);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptOff_Penalty_TD()
        {
            int Yards_to_go = 5;
            double Line_of_Scrimmage = 50.0;
            Penalty p = new Penalty() { bDeclinable = true };
            Play_Result pResult = new Play_Result() { Yards_Gained = 50, bTouchDown = true, Penalty = p };
            Game g = new Game(){ Home_Score = 0, Away_Score = 0 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptDef_Penalty(Enum.Play_Enum.RUN, pResult, Yards_to_go, Line_of_Scrimmage, true, false, false);

            Assert.IsTrue(!bAccept);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptOff_Penalty_Safety()
        {
            int Yards_to_go = 5;
            double Line_of_Scrimmage = 50.0;
            Penalty p = new Penalty() { bDeclinable = true };
            Play_Result pResult = new Play_Result() { Yards_Gained = 50, bSafety = true, Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptDef_Penalty(Enum.Play_Enum.PASS, pResult, Yards_to_go, Line_of_Scrimmage, true, false, false);

            Assert.IsTrue(bAccept);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptOff_Penalty_xpmissed()
        {
            int Yards_to_go = 5;
            double Line_of_Scrimmage = 50.0;
            Penalty p = new Penalty() { bDeclinable = true };
            Play_Result pResult = new Play_Result() { Yards_Gained = 50, bXPMissed = true, Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptDef_Penalty(Enum.Play_Enum.EXTRA_POINT, pResult, Yards_to_go, Line_of_Scrimmage, true, false, false);

            Assert.IsTrue(bAccept);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptOff_Penalty_xpmade()
        {
            int Yards_to_go = 5;
            double Line_of_Scrimmage = 50.0;
            Penalty p = new Penalty() { bDeclinable = true };
            Play_Result pResult = new Play_Result() { Yards_Gained = 50, bXPMade = true, Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptDef_Penalty(Enum.Play_Enum.EXTRA_POINT, pResult, Yards_to_go, Line_of_Scrimmage, true, false, false);

            Assert.IsTrue(!bAccept);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptOff_Penalty_FGMissed()
        {
            int Yards_to_go = 5;
            double Line_of_Scrimmage = 50.0;
            Penalty p = new Penalty() { bDeclinable = true };
            Play_Result pResult = new Play_Result() { Yards_Gained = 50, bFGMissed = true, Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptDef_Penalty(Enum.Play_Enum.FIELD_GOAL, pResult, Yards_to_go, Line_of_Scrimmage, true, false, false);

            Assert.IsTrue(bAccept);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptOff_Penalty_FGMade_no_FD()
        {
            int Yards_to_go = 10;
            double Line_of_Scrimmage = 35.0;
            Penalty p = new Penalty() { bDeclinable = true, Yards = 5 };
            Play_Result pResult = new Play_Result() { bFGMade = true, Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptDef_Penalty(Enum.Play_Enum.FIELD_GOAL, pResult, Yards_to_go, Line_of_Scrimmage, true, false, false);

            Assert.IsTrue(!bAccept);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptOff_Penalty_FGMade_FD()
        {
            int Yards_to_go = 10;
            double Line_of_Scrimmage = 35.0;
            Penalty p = new Penalty() { bDeclinable = true, Yards = 10 };
            Play_Result pResult = new Play_Result() { bFGMade = true, Penalty = p};
            Game g = new Game() { Home_Score = 0, Away_Score = 0, Quarter = 3, Time = 500 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptDef_Penalty(Enum.Play_Enum.FIELD_GOAL, pResult, Yards_to_go, Line_of_Scrimmage, true, false, false);

            Assert.IsTrue(bAccept);
        }
        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptOff_Penalty_FGMade_FD_near_end_of_half_or_game_dont_take1()
        {
            int Yards_to_go = 10;
            double Line_of_Scrimmage = 24.0;
            Penalty p = new Penalty() { bDeclinable = true, Yards = 10 };
            Play_Result pResult = new Play_Result() { bFGMade = true, Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0, Quarter = 4, Time = 6 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptDef_Penalty(Enum.Play_Enum.FIELD_GOAL, pResult, Yards_to_go, Line_of_Scrimmage, true, false, false);

            Assert.IsTrue(!bAccept);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptOff_Penalty_FGMade_FD_near_end_of_half_or_game_dont_take2()
        {
            int Yards_to_go = 10;
            double Line_of_Scrimmage = 80.0;
            Penalty p = new Penalty() { bDeclinable = true, Yards = 10 };
            Play_Result pResult = new Play_Result() { bFGMade = true, Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0, Quarter = 4, Time = 18 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptDef_Penalty(Enum.Play_Enum.FIELD_GOAL, pResult, Yards_to_go, Line_of_Scrimmage, true, false, false);

            Assert.IsTrue(!bAccept);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptOff_Penalty_FGMade_FD_near_end_of_half_or_game_dont_take3()
        {
            int Yards_to_go = 10;
            double Line_of_Scrimmage = 70.0;
            Penalty p = new Penalty() { bDeclinable = true, Yards = 10 };
            Play_Result pResult = new Play_Result() { bFGMade = true, Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0, Quarter = 4, Time = 28 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptDef_Penalty(Enum.Play_Enum.FIELD_GOAL, pResult, Yards_to_go, Line_of_Scrimmage, true, false, false);

            Assert.IsTrue(!bAccept);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptOff_Penalty_FGMade_FD_near_end_of_half_or_game_dont_take4()
        {
            int Yards_to_go = 10;
            double Line_of_Scrimmage = 60.0;
            Penalty p = new Penalty() { bDeclinable = true, Yards = 10 };
            Play_Result pResult = new Play_Result() { bFGMade = true, Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0,Quarter = 4, Time = 38 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptDef_Penalty(Enum.Play_Enum.FIELD_GOAL, pResult, Yards_to_go, Line_of_Scrimmage, true, false, false);

            Assert.IsTrue(!bAccept);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptOff_Penalty_FGMade_FD_near_end_of_half_or_game_less_urgent_take_penalty()
        {
            int Yards_to_go = 2;
            double Line_of_Scrimmage = 88.0;
            Penalty p = new Penalty() { bDeclinable = true, Yards = 10 };
            Play_Result pResult = new Play_Result() { bFGMade = true, Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0, Quarter = 4, Time = 100 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptDef_Penalty(Enum.Play_Enum.FIELD_GOAL, pResult, Yards_to_go, Line_of_Scrimmage, true, false, false);

            Assert.IsTrue(bAccept);
        }
        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptOff_Penalty_bOnePntAfterTDMissed()
        {
            int Yards_to_go = 2;
            double Line_of_Scrimmage = 88.0;
            Penalty p = new Penalty() { bDeclinable = true, Yards = 10 };
            Play_Result pResult = new Play_Result() { bOnePntAfterTDMissed = true, Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0,Quarter = 4, Time = 100 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptDef_Penalty(Enum.Play_Enum.FIELD_GOAL, pResult, Yards_to_go, Line_of_Scrimmage, true, false, false);

            Assert.IsTrue(bAccept);
        }
        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptOff_Penalty_bOnePntAfterTDMade()
        {
            int Yards_to_go = 2;
            double Line_of_Scrimmage = 88.0;
            Penalty p = new Penalty() { bDeclinable = true, Yards = 10 };
            Play_Result pResult = new Play_Result() { bOnePntAfterTDMade = true, Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0,Quarter = 4, Time = 100 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptDef_Penalty(Enum.Play_Enum.FIELD_GOAL, pResult, Yards_to_go, Line_of_Scrimmage, true, false, false);

            Assert.IsTrue(!bAccept);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptOff_Penalty_bTwoPntAfterTDMissed()
        {
            int Yards_to_go = 2;
            double Line_of_Scrimmage = 88.0;
            Penalty p = new Penalty() { bDeclinable = true, Yards = 10 };
            Play_Result pResult = new Play_Result() { bTwoPntAfterTDMissed = true, Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0, Quarter = 4, Time = 100 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptDef_Penalty(Enum.Play_Enum.FIELD_GOAL, pResult, Yards_to_go, Line_of_Scrimmage, true, false, false);

            Assert.IsTrue(bAccept);
        }
        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptOff_Penalty_bTwoPntAfterTDMade()
        {
            int Yards_to_go = 2;
            double Line_of_Scrimmage = 88.0;
            Penalty p = new Penalty() { bDeclinable = true, Yards = 10 };
            Play_Result pResult = new Play_Result() { bTwoPntAfterTDMade = true, Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0,Quarter = 4, Time = 100 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptDef_Penalty(Enum.Play_Enum.FIELD_GOAL, pResult, Yards_to_go, Line_of_Scrimmage, true, false, false);

            Assert.IsTrue(!bAccept);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptOff_Penalty_bThreePntAfterTDMissed()
        {
            int Yards_to_go = 2;
            double Line_of_Scrimmage = 88.0;
            Penalty p = new Penalty() { bDeclinable = true, Yards = 10 };
            Play_Result pResult = new Play_Result() { bThreePntAfterTDMissed = true, Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0,Quarter = 4, Time = 100 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptDef_Penalty(Enum.Play_Enum.FIELD_GOAL, pResult, Yards_to_go, Line_of_Scrimmage, true, false, false);

            Assert.IsTrue(bAccept);
        }
        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptOff_Penalty_bThreePntAfterTDMade()
        {
            int Yards_to_go = 2;
            double Line_of_Scrimmage = 88.0;
            Penalty p = new Penalty() { bDeclinable = true, Yards = 10 };
            Play_Result pResult = new Play_Result() { bThreePntAfterTDMade = true, Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0,Quarter = 4, Time = 100 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptDef_Penalty(Enum.Play_Enum.FIELD_GOAL, pResult, Yards_to_go, Line_of_Scrimmage, true, false, false);

            Assert.IsTrue(!bAccept);
        }
        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptOff_Penalty_LastPlay_NoScoreorTurnover_not_winning_Accept()
        {
            int Yards_to_go = 2;
            double Line_of_Scrimmage = 88.0;
            Penalty p = new Penalty() { bDeclinable = true, Yards = 10 };
            Play_Result pResult = new Play_Result() { bThreePntAfterTDMade = true, Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0,Quarter = 4, Time = 0 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptDef_Penalty(Enum.Play_Enum.PASS, pResult, Yards_to_go, Line_of_Scrimmage, true, true, false);

            Assert.IsTrue(!bAccept);
        }
        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptOff_Penalty_Punt_Coffin_Corner_dont_accept()
        {
            int Yards_to_go = 2;
            double Line_of_Scrimmage = 88.0;
            Penalty p = new Penalty() { bDeclinable = true, Yards = 10 };
            Play_Result pResult = new Play_Result() { bCoffinCornerMade = true, Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0, Quarter = 4, Time = 0 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptDef_Penalty(Enum.Play_Enum.PUNT, pResult, Yards_to_go, Line_of_Scrimmage, true, false, false);

            Assert.IsTrue(!bAccept);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptOff_Penalty_Punt_not_Coffin_Corner_accept()
        {
            int Yards_to_go = 2;
            double Line_of_Scrimmage = 88.0;
            Penalty p = new Penalty() { bDeclinable = true, Yards = 10 };
            Play_Result pResult = new Play_Result() { Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0, Quarter = 4, Time = 0 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptDef_Penalty(Enum.Play_Enum.PUNT, pResult, Yards_to_go, Line_of_Scrimmage, true, false, false);

            Assert.IsTrue(bAccept);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptOff_Penalty_kickoff_accept()
        {
            int Yards_to_go = 2;
            double Line_of_Scrimmage = 88.0;
            Penalty p = new Penalty() { bDeclinable = true, Yards = 10 };
            Play_Result pResult = new Play_Result() { Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0, Quarter = 4, Time = 0 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptDef_Penalty(Enum.Play_Enum.KICKOFF_NORMAL, pResult, Yards_to_go, Line_of_Scrimmage, true, false, false);

            Assert.IsTrue(bAccept);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptOff_not_FD_Penalty_on_RunFD()
        {
            int Yards_to_go = 10;
            double Line_of_Scrimmage = 50.0;
            Penalty p = new Penalty() { bDeclinable = true, Yards = 5 };
            Play_Result pResult = new Play_Result() { Penalty = p, Yards_Gained = 10 };
            Game g = new Game() { Home_Score = 0, Away_Score = 0, Quarter = 4, Time = 0 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptDef_Penalty(Enum.Play_Enum.RUN, pResult, Yards_to_go, Line_of_Scrimmage, true, false, false);

            Assert.IsTrue(!bAccept);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptOff_not_FD_Penalty_on_RunFD_half_the_dist()
        {
            int Yards_to_go = 10;
            double Line_of_Scrimmage = 85.0;
            Penalty p = new Penalty() { bDeclinable = true, Yards = 10 };
            Play_Result pResult = new Play_Result() { Penalty = p, Yards_Gained = 10 };
            Game g = new Game() { Home_Score = 0, Away_Score = 0, Quarter = 4, Time = 0 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptDef_Penalty(Enum.Play_Enum.RUN, pResult, Yards_to_go, Line_of_Scrimmage, true, false, false);

            Assert.IsTrue(!bAccept);
        }
        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptOff_FD_Penalty_on_long_pass_complete_dont_accept()
        {
            int Yards_to_go = 10;
            double Line_of_Scrimmage = 15.0;
            Penalty p = new Penalty() { bDeclinable = true, bAuto_FirstDown = true,  Yards = 15 };
            Play_Result pResult = new Play_Result() { Penalty = p, Yards_Gained = 40 };
            Game g = new Game() { Home_Score = 0, Away_Score = 0, Quarter = 4, Time = 0 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptDef_Penalty(Enum.Play_Enum.PASS, pResult, Yards_to_go, Line_of_Scrimmage, true, false, false);

            Assert.IsTrue(!bAccept);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptOff_auto_FD_Penalty_on_short_run_accept()
        {
            int Yards_to_go = 10;
            double Line_of_Scrimmage = 15.0;
            Penalty p = new Penalty() { bDeclinable = true, bAuto_FirstDown = true, Yards = 10 };
            Play_Result pResult = new Play_Result() { Penalty = p, Yards_Gained = 2 };
            Game g = new Game() { Home_Score = 0, Away_Score = 0, Quarter = 4, Time = 0 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptDef_Penalty(Enum.Play_Enum.RUN, pResult, Yards_to_go, Line_of_Scrimmage, true, false, false);

            Assert.IsTrue(bAccept);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptOff_FD_Penalty_on_short_run_accept()
        {
            int Yards_to_go = 10;
            double Line_of_Scrimmage = 15.0;
            Penalty p = new Penalty() { bDeclinable = true, Yards = 10 };
            Play_Result pResult = new Play_Result() { Penalty = p, Yards_Gained = 2 };
            Game g = new Game() { Home_Score = 0, Away_Score = 0, Quarter = 4, Time = 0 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptDef_Penalty(Enum.Play_Enum.RUN, pResult, Yards_to_go, Line_of_Scrimmage, true, false, false);

            Assert.IsTrue(bAccept);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptOff_FD_Penalty_on_FD_Pass_accept()
        {
            int Yards_to_go = 10;
            double Line_of_Scrimmage = 15.0;
            Penalty p = new Penalty() { bDeclinable = true, Yards = 10 };
            Play_Result pResult = new Play_Result() { Penalty = p, Yards_Gained = 10 };
            Game g = new Game() { Home_Score = 0, Away_Score = 0, Quarter = 4, Time = 0 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptDef_Penalty(Enum.Play_Enum.PASS, pResult, Yards_to_go, Line_of_Scrimmage, true, false, false);

            Assert.IsTrue(!bAccept);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptOff_FD_Penalty_on_FD_Pass_dont_accept()
        {
            int Yards_to_go = 8;
            double Line_of_Scrimmage = 15.0;
            Penalty p = new Penalty() { bDeclinable = true, Yards = 10 };
            Play_Result pResult = new Play_Result() { Penalty = p, Yards_Gained = 9 };
            Game g = new Game() { Home_Score = 0, Away_Score = 0, Quarter = 4, Time = 0 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptDef_Penalty(Enum.Play_Enum.PASS, pResult, Yards_to_go, Line_of_Scrimmage, true, false, false);

            Assert.IsTrue(bAccept);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptOff_auto_FD_Penalty_on_FD_run_dont_accept()
        {
            int Yards_to_go = 6;
            double Line_of_Scrimmage = 90.0;
            Penalty p = new Penalty() { bDeclinable = true, Yards = 10 };
            Play_Result pResult = new Play_Result() { Penalty = p, Yards_Gained = 7 };
            Game g = new Game() { Home_Score = 0, Away_Score = 0, Quarter = 4, Time = 0 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptDef_Penalty(Enum.Play_Enum.RUN, pResult, Yards_to_go, Line_of_Scrimmage, true, false, false);

            Assert.IsTrue(!bAccept);
        }



    }
}
