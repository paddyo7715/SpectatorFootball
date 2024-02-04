using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpectatorFootball.Models;
using SpectatorFootball.GameNS;
using SpectatorFootball.PenaltiesNS;
using SpectatorFootball.Enum;

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
            Penalty p = new Penalty() { bDeclinable = true };
            Play_Result pResult = new Play_Result() {end_of_play_yardline = 50, Yards_Gained = 0, bInterception = true, Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptDef_Penalty(Enum.Play_Enum.RUN, pResult, Yards_to_go, true, false, false, 25);


            Assert.IsTrue(bAccept);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptDef_Penalty_TD()
        {
            int Yards_to_go = 5;
            Penalty p = new Penalty() { bDeclinable = true };
            Play_Result pResult = new Play_Result() { end_of_play_yardline = 50, Yards_Gained = 50, bTouchDown = true, Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptDef_Penalty(Enum.Play_Enum.RUN, pResult, Yards_to_go, true, false, false, 25);

            Assert.IsTrue(!bAccept);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptDef_Spot_Foul_Penalty_TD()
        {
            int Yards_to_go = 5;
            Penalty p = new Penalty() { bDeclinable = true, bSpot_Foul = true };
            Play_Result pResult = new Play_Result() { end_of_play_yardline = 50, Yards_Gained = 50, bTouchDown = true, Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptDef_Penalty(Enum.Play_Enum.RUN, pResult, Yards_to_go, true, false, false, 25);

            Assert.IsTrue(!bAccept);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptDef_Penalty_Safety()
        {
            int Yards_to_go = 5;
            Penalty p = new Penalty() { bDeclinable = true };
            Play_Result pResult = new Play_Result() { end_of_play_yardline = 50, Yards_Gained = 50, bSafety = true, Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptDef_Penalty(Enum.Play_Enum.PASS, pResult, Yards_to_go, true, false, false, 25);

            Assert.IsTrue(bAccept);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptDef_Spot_Foul_Penalty_Safety()
        {
            int Yards_to_go = 5;
            Penalty p = new Penalty() { bDeclinable = true, bSpot_Foul = true };
            Play_Result pResult = new Play_Result() { end_of_play_yardline = 50, Yards_Gained = 50, bSafety = true, Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptDef_Penalty(Enum.Play_Enum.PASS, pResult, Yards_to_go, true, false, false, 25);

            Assert.IsTrue(bAccept);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptDef_Penalty_xpmissed()
        {
            int Yards_to_go = 5;
            Penalty p = new Penalty() { bDeclinable = true };
            Play_Result pResult = new Play_Result() { end_of_play_yardline = 50, Yards_Gained = 50, bXPMissed = true, Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptDef_Penalty(Enum.Play_Enum.EXTRA_POINT, pResult, Yards_to_go, true, false, false, 25);

            Assert.IsTrue(bAccept);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptDef_Spot_Foul_Penalty_xpmissed()
        {
            int Yards_to_go = 5;
            Penalty p = new Penalty() { bDeclinable = true, bSpot_Foul = true };
            Play_Result pResult = new Play_Result() { end_of_play_yardline = 50, Yards_Gained = 50, bXPMissed = true, Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptDef_Penalty(Enum.Play_Enum.EXTRA_POINT, pResult, Yards_to_go, true, false, false, 25);

            Assert.IsTrue(bAccept);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptDef_Penalty_xpmade()
        {
            int Yards_to_go = 5;
            Penalty p = new Penalty() { bDeclinable = true };
            Play_Result pResult = new Play_Result() { end_of_play_yardline = 50, Yards_Gained = 50, bXPMade = true, Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptDef_Penalty(Enum.Play_Enum.EXTRA_POINT, pResult, Yards_to_go, true, false, false, 25);

            Assert.IsTrue(!bAccept);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptDefSpot_Foul_Penalty_xpmade()
        {
            int Yards_to_go = 5;
            Penalty p = new Penalty() { bDeclinable = true, bSpot_Foul = true };
            Play_Result pResult = new Play_Result() { end_of_play_yardline = 50, Yards_Gained = 50, bXPMade = true, Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptDef_Penalty(Enum.Play_Enum.EXTRA_POINT, pResult, Yards_to_go, true, false, false, 25);

            Assert.IsTrue(!bAccept);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptDef_Penalty_FGMissed()
        {
            int Yards_to_go = 5;
            Penalty p = new Penalty() { bDeclinable = true };
            Play_Result pResult = new Play_Result() { end_of_play_yardline = 70, Yards_Gained = 50, bFGMissed = true, Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptDef_Penalty(Enum.Play_Enum.FIELD_GOAL, pResult, Yards_to_go, true, false, false, 25);

            Assert.IsTrue(bAccept);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptDef_Penalty_FGMade_no_FD()
        {
            int Yards_to_go = 10;
            Penalty p = new Penalty() { bDeclinable = true, Yards = 5 };
            Play_Result pResult = new Play_Result() { end_of_play_yardline = 75, bFGMade = true, Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptDef_Penalty(Enum.Play_Enum.FIELD_GOAL, pResult, Yards_to_go, true, false, false, 25);

            Assert.IsTrue(!bAccept);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptDef_Penalty_FGMade_FD()
        {
            int Yards_to_go = 10;
            Penalty p = new Penalty() { bDeclinable = true, Yards = 10 };
            Play_Result pResult = new Play_Result() { end_of_play_yardline = 75, bFGMade = true, Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0, Quarter = 3, Time = 500 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptDef_Penalty(Enum.Play_Enum.FIELD_GOAL, pResult, Yards_to_go, true, false, false, 25);

            Assert.IsTrue(bAccept);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptDef_Spot_Foul_Penalty_FGMade_FD()
        {
            int Yards_to_go = 10;
            Penalty p = new Penalty() { bDeclinable = true, Yards = 10, bSpot_Foul = true };
            Play_Result pResult = new Play_Result() { end_of_play_yardline = 65, bFGMade = true, Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0, Quarter = 3, Time = 500 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptDef_Penalty(Enum.Play_Enum.FIELD_GOAL, pResult, Yards_to_go, true, false, false, 25);

            Assert.IsTrue(bAccept);
        }
        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptDef_Penalty_FGMade_FD_near_end_of_half_or_game_dont_take1()
        {
            int Yards_to_go = 10;
            Penalty p = new Penalty() { bDeclinable = true, Yards = 10 };
            Play_Result pResult = new Play_Result() { end_of_play_yardline = 75, bFGMade = true, Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0, Quarter = 4, Time = 6 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptDef_Penalty(Enum.Play_Enum.FIELD_GOAL, pResult, Yards_to_go, true, false, false, 25);

            Assert.IsTrue(!bAccept);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptDef_Penalty_Spot_Foul_FGMade_FD_near_end_of_half_or_game_dont_take1()
        {
            int Yards_to_go = 10;
            Penalty p = new Penalty() { bDeclinable = true, Yards = 10, bSpot_Foul = true };
            Play_Result pResult = new Play_Result() { end_of_play_yardline = 76, bFGMade = true, Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0, Quarter = 4, Time = 6 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptDef_Penalty(Enum.Play_Enum.FIELD_GOAL, pResult, Yards_to_go, true, false, false, 25);

            Assert.IsTrue(!bAccept);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptDef_Penalty_FGMade_FD_near_end_of_half_or_game_dont_take2()
        {
            int Yards_to_go = 10;
            Penalty p = new Penalty() { bDeclinable = true, Yards = 10 };
            Play_Result pResult = new Play_Result() { end_of_play_yardline = 80, bFGMade = true, Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0, Quarter = 4, Time = 18 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptDef_Penalty(Enum.Play_Enum.FIELD_GOAL, pResult, Yards_to_go, true, false, false, 25);

            Assert.IsTrue(!bAccept);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptDef_Penalty_FGMade_FD_near_end_of_half_or_game_dont_take3()
        {
            int Yards_to_go = 10;
            Penalty p = new Penalty() { bDeclinable = true, Yards = 10 };
            Play_Result pResult = new Play_Result() { end_of_play_yardline = 70, bFGMade = true, Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0, Quarter = 4, Time = 28 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptDef_Penalty(Enum.Play_Enum.FIELD_GOAL, pResult, Yards_to_go, true, false, false, 25);

            Assert.IsTrue(!bAccept);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptDef_Penalty_FGMade_FD_near_end_of_half_or_game_dont_take4()
        {
            int Yards_to_go = 10;
            Penalty p = new Penalty() { bDeclinable = true, Yards = 10 };
            Play_Result pResult = new Play_Result() { end_of_play_yardline = 65, bFGMade = true, Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0, Quarter = 4, Time = 38 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptDef_Penalty(Enum.Play_Enum.FIELD_GOAL, pResult, Yards_to_go, true, false, false, 25);

            Assert.IsTrue(!bAccept);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptDef_Penalty_FGMade_FD_near_end_of_half_or_game_less_urgent_take_penalty()
        {
            int Yards_to_go = 2;
            Penalty p = new Penalty() { bDeclinable = true, Yards = 10 };
            Play_Result pResult = new Play_Result() { end_of_play_yardline = 88, bFGMade = true, Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0, Quarter = 4, Time = 100 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptDef_Penalty(Enum.Play_Enum.FIELD_GOAL, pResult, Yards_to_go, true, false, false, 25);

            Assert.IsTrue(bAccept);
        }
        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptDef_Penalty_FGMade_FD_near_end_of_OT_Decline()
        {
            int Yards_to_go = 2;
            Penalty p = new Penalty() { bDeclinable = true, Yards = 10 };
            Play_Result pResult = new Play_Result() { end_of_play_yardline = 88, bFGMade = true, Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0, Quarter = 6, Time = 100 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptDef_Penalty(Enum.Play_Enum.FIELD_GOAL, pResult, Yards_to_go, true, false, false, 25);

            Assert.IsTrue(bAccept);
        }
        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptDef_Penalty_bOnePntAfterTDMissed()
        {
            int Yards_to_go = 2;
            Penalty p = new Penalty() { bDeclinable = true, Yards = 10 };
            Play_Result pResult = new Play_Result() { end_of_play_yardline = 88, bOnePntAfterTDMissed = true, Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0, Quarter = 4, Time = 100 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptDef_Penalty(Enum.Play_Enum.SCRIM_PLAY_1XP_RUN, pResult, Yards_to_go, true, false, false, 25);

            Assert.IsTrue(bAccept);
        }
        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptDef_Penalty_bOnePntAfterTDMade()
        {
            int Yards_to_go = 2;
            Penalty p = new Penalty() { bDeclinable = true, Yards = 10 };
            Play_Result pResult = new Play_Result() { end_of_play_yardline = 88, bOnePntAfterTDMade = true, Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0, Quarter = 4, Time = 100 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptDef_Penalty(Enum.Play_Enum.SCRIM_PLAY_1XP_PASS, pResult, Yards_to_go, true, false, false, 25);

            Assert.IsTrue(!bAccept);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptDef_Penalty_bTwoPntAfterTDMissed()
        {
            int Yards_to_go = 2;
            Penalty p = new Penalty() { bDeclinable = true, Yards = 10 };
            Play_Result pResult = new Play_Result() { end_of_play_yardline = 88, bTwoPntAfterTDMissed = true, Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0, Quarter = 4, Time = 100 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptDef_Penalty(Enum.Play_Enum.SCRIM_PLAY_2XP_RUN, pResult, Yards_to_go, true, false, false, 25);

            Assert.IsTrue(bAccept);
        }
        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptDef_Penalty_bTwoPntAfterTDMade()
        {
            int Yards_to_go = 2;
            Penalty p = new Penalty() { bDeclinable = true, Yards = 10 };
            Play_Result pResult = new Play_Result() { end_of_play_yardline = 88, bTwoPntAfterTDMade = true, Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0, Quarter = 4, Time = 100 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptDef_Penalty(Enum.Play_Enum.SCRIM_PLAY_2XP_PASS, pResult, Yards_to_go, true, false, false, 25);

            Assert.IsTrue(!bAccept);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptDef_Penalty_bThreePntAfterTDMissed()
        {
            int Yards_to_go = 2;
            Penalty p = new Penalty() { bDeclinable = true, Yards = 10 };
            Play_Result pResult = new Play_Result() { end_of_play_yardline = 88, bThreePntAfterTDMissed = true, Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0, Quarter = 4, Time = 100 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptDef_Penalty(Enum.Play_Enum.SCRIM_PLAY_2XP_RUN, pResult, Yards_to_go, true, false, false, 25);

            Assert.IsTrue(bAccept);
        }
        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptDef_Penalty_bThreePntAfterTDMade()
        {
            int Yards_to_go = 2;
            Penalty p = new Penalty() { bDeclinable = true, Yards = 10 };
            Play_Result pResult = new Play_Result() { end_of_play_yardline = 88, bThreePntAfterTDMade = true, Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0, Quarter = 4, Time = 100 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptDef_Penalty(Enum.Play_Enum.SCRIM_PLAY_3XP_RUN, pResult, Yards_to_go, true, false, false, 25);

            Assert.IsTrue(!bAccept);
        }
        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptDef_Penalty_LastPlay_not_winning_Accept()
        {
            int Yards_to_go = 2;
            Penalty p = new Penalty() { bDeclinable = true, Yards = 10 };
            Play_Result pResult = new Play_Result() { end_of_play_yardline = 88, bThreePntAfterTDMade = false, Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0, Quarter = 4, Time = 0 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptDef_Penalty(Enum.Play_Enum.PASS, pResult, Yards_to_go, true, true, false, 25);

            Assert.IsTrue(bAccept);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptDef_Def_Offices_Penalty_not_First_Down_Good_Return_Punt_accept()
        {
            int Yards_to_go = 10;
            Penalty p = new Penalty() { code = Penalty_Codes.DO, bDeclinable = true, Yards = 5 };
            Play_Result pResult = new Play_Result() {Play_Start_Yardline = 20, end_of_play_yardline = 59, Penalty = p};
            Game g = new Game() { Home_Score = 0, Away_Score = 0, Quarter = 4, Time = 200 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptDef_Penalty(Enum.Play_Enum.PUNT, pResult, Yards_to_go, true, false, false, 25);

            Assert.IsTrue(bAccept);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptDef_Def_Offices_Penalty_not_First_Down_Not_Good_Return_Punt_dont_accept()
        {
            int Yards_to_go = 10;
            Penalty p = new Penalty() { code = Penalty_Codes.DO, bDeclinable = true, Yards = 5 };
            Play_Result pResult = new Play_Result() { Play_Start_Yardline = 20, end_of_play_yardline = 61, Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0, Quarter = 4, Time = 200 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptDef_Penalty(Enum.Play_Enum.PUNT, pResult, Yards_to_go, true, false, false, 25);

            Assert.IsTrue(!bAccept);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptDef_Def_Offices_Penalty_not_First_Down_Not_Good_Return_Punt_touchback_accept()
        {
            int Yards_to_go = 10;
            Penalty p = new Penalty() { code = Penalty_Codes.DO, bDeclinable = true, Yards = 5 };
            Play_Result pResult = new Play_Result() { Play_Start_Yardline = 50, bTouchback = true, Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0, Quarter = 4, Time = 200 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptDef_Penalty(Enum.Play_Enum.PUNT, pResult, Yards_to_go, true, false, false, 25);

            Assert.IsTrue(bAccept);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptDef_Def_Penalty_Auto_First_Down_Bad_Return_Punt_accept()
        {
            int Yards_to_go = 10;
            Penalty p = new Penalty() { code = Penalty_Codes.RK, bDeclinable = true, Yards = 5, bAuto_FirstDown = true };
            Play_Result pResult = new Play_Result() { Play_Start_Yardline = 20, end_of_play_yardline = 90, Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0, Quarter = 4, Time = 200 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptDef_Penalty(Enum.Play_Enum.PUNT, pResult, Yards_to_go, true, false, false, 25);

            Assert.IsTrue(bAccept);
        }
        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptDef_Def_Penalty_not_First_Down_but_first_Down_Punt_accept()
        {
            int Yards_to_go = 1;
            Penalty p = new Penalty() { code = Penalty_Codes.DO, bDeclinable = true, Yards = 5, bAuto_FirstDown = false };
            Play_Result pResult = new Play_Result() { Play_Start_Yardline = 20, end_of_play_yardline = 90, Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0, Quarter = 4, Time = 200 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptDef_Penalty(Enum.Play_Enum.PUNT, pResult, Yards_to_go, true, false, false, 25);

            Assert.IsTrue(bAccept);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptDef_Def_Penalty_Big_Kick_Turnover_Decline_Penalty()
        {
            int Yards_to_go = 1;
            Penalty p = new Penalty() { code = Penalty_Codes.DO, bDeclinable = true, Yards = 5, bAuto_FirstDown = false };
            Play_Result pResult = new Play_Result() { Play_Start_Yardline = 20, end_of_play_yardline = 90, Penalty = p, bFumble_Lost = true };
            Game g = new Game() { Home_Score = 0, Away_Score = 0, Quarter = 4, Time = 200 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptDef_Penalty(Enum.Play_Enum.PUNT, pResult, Yards_to_go, true, false, false, 25);

            Assert.IsTrue(!bAccept);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptDef_Def_Penalty_Big_Kick_Coffin_Corner_not_FD_Decline_Penalty()
        {
            int Yards_to_go = 10;
            Penalty p = new Penalty() { code = Penalty_Codes.DO, bDeclinable = true, Yards = 5, bAuto_FirstDown = false };
            Play_Result pResult = new Play_Result() { Play_Start_Yardline = 20, end_of_play_yardline = 90, Penalty = p, bCoffinCornerMade = true };
            Game g = new Game() { Home_Score = 0, Away_Score = 0, Quarter = 4, Time = 200 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptDef_Penalty(Enum.Play_Enum.PUNT, pResult, Yards_to_go, true, false, false, 25);

            Assert.IsTrue(!bAccept);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptDef_Def_Punt_Penalty_Big_Kick_Returned_TD_not_FD_Accept_Penalty()
        {
            int Yards_to_go = 10;
            Penalty p = new Penalty() { code = Penalty_Codes.DO, bDeclinable = true, Yards = 5, bAuto_FirstDown = false };
            Play_Result pResult = new Play_Result() { Play_Start_Yardline = 20, end_of_play_yardline = 90, Penalty = p, bTouchDown = true };
            Game g = new Game() { Home_Score = 0, Away_Score = 0, Quarter = 4, Time = 200 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptDef_Penalty(Enum.Play_Enum.PUNT, pResult, Yards_to_go, true, false, false, 25);

            Assert.IsTrue(bAccept);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptDef_Penalty_kickoff_accept()
        {
            int Yards_to_go = 2;
            Penalty p = new Penalty() { bDeclinable = true, Yards = 10 };
            Play_Result pResult = new Play_Result() { end_of_play_yardline = 88, Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0, Quarter = 4, Time = 0 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptDef_Penalty(Enum.Play_Enum.KICKOFF_NORMAL, pResult, Yards_to_go, true, false, false, 25);

            Assert.IsTrue(bAccept);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptDef_not_FD_Penalty_on_RunFD()
        {
            int Yards_to_go = 10;
            Penalty p = new Penalty() { bDeclinable = true, Yards = 5 };
            Play_Result pResult = new Play_Result() { end_of_play_yardline = 50, Penalty = p, Yards_Gained = 10 };
            Game g = new Game() { Home_Score = 0, Away_Score = 0, Quarter = 4, Time = 0 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptDef_Penalty(Enum.Play_Enum.RUN, pResult, Yards_to_go, true, false, false, 25);

            Assert.IsTrue(!bAccept);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptDef_not_FD_Penalty_on_RunFD_half_the_dist()
        {
            int Yards_to_go = 10;
            Penalty p = new Penalty() { bDeclinable = true, Yards = 10 };
            Play_Result pResult = new Play_Result() { end_of_play_yardline = 85, Penalty = p, Yards_Gained = 10 };
            Game g = new Game() { Home_Score = 0, Away_Score = 0, Quarter = 4, Time = 0 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptDef_Penalty(Enum.Play_Enum.RUN, pResult, Yards_to_go, true, false, false, 25);

            Assert.IsTrue(!bAccept);
        }
        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptDef_FD_Penalty_on_long_pass_complete_dont_accept()
        {
            int Yards_to_go = 10;
            Penalty p = new Penalty() { bDeclinable = true, bAuto_FirstDown = true, Yards = 15 };
            Play_Result pResult = new Play_Result() { end_of_play_yardline = 15, Penalty = p, Yards_Gained = 40 };
            Game g = new Game() { Home_Score = 0, Away_Score = 0, Quarter = 4, Time = 0 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptDef_Penalty(Enum.Play_Enum.PASS, pResult, Yards_to_go, true, false, false, 25);

            Assert.IsTrue(!bAccept);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptDef_auto_FD_Penalty_on_short_run_accept()
        {
            int Yards_to_go = 10;
            Penalty p = new Penalty() { bDeclinable = true, bAuto_FirstDown = true, Yards = 10 };
            Play_Result pResult = new Play_Result() { end_of_play_yardline = 15, Penalty = p, Yards_Gained = 2 };
            Game g = new Game() { Home_Score = 0, Away_Score = 0, Quarter = 4, Time = 0 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptDef_Penalty(Enum.Play_Enum.RUN, pResult, Yards_to_go, true, false, false, 25);

            Assert.IsTrue(bAccept);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptDef_FD_Penalty_on_short_run_accept()
        {
            int Yards_to_go = 10;
            Penalty p = new Penalty() { bDeclinable = true, Yards = 10 };
            Play_Result pResult = new Play_Result() { end_of_play_yardline = 15, Penalty = p, Yards_Gained = 2 };
            Game g = new Game() { Home_Score = 0, Away_Score = 0, Quarter = 4, Time = 0 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptDef_Penalty(Enum.Play_Enum.RUN, pResult, Yards_to_go, true, false, false, 25);

            Assert.IsTrue(bAccept);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptDef_FD_Penalty_on_FD_Pass_accept()
        {
            int Yards_to_go = 10;
            Penalty p = new Penalty() { bDeclinable = true, Yards = 10 };
            Play_Result pResult = new Play_Result() { end_of_play_yardline = 15, Penalty = p, Yards_Gained = 10 };
            Game g = new Game() { Home_Score = 0, Away_Score = 0, Quarter = 4, Time = 0 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptDef_Penalty(Enum.Play_Enum.PASS, pResult, Yards_to_go, true, false, false, 25);

            Assert.IsTrue(!bAccept);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptDef_FD_Penalty_on_FD_Pass_dont_accept()
        {
            int Yards_to_go = 8;
            Penalty p = new Penalty() { bDeclinable = true, Yards = 10 };
            Play_Result pResult = new Play_Result() { end_of_play_yardline = 15, Penalty = p, Yards_Gained = 9 };
            Game g = new Game() { Home_Score = 0, Away_Score = 0, Quarter = 4, Time = 0 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptDef_Penalty(Enum.Play_Enum.PASS, pResult, Yards_to_go, true, false, false, 25);

            Assert.IsTrue(bAccept);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptDef_auto_FD_Penalty_on_FD_run_dont_accept()
        {
            int Yards_to_go = 6;
            Penalty p = new Penalty() { bDeclinable = true, Yards = 10 };
            Play_Result pResult = new Play_Result() { end_of_play_yardline = 90, Penalty = p, Yards_Gained = 7 };
            Game g = new Game() { Home_Score = 0, Away_Score = 0, Quarter = 4, Time = 0 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptDef_Penalty(Enum.Play_Enum.RUN, pResult, Yards_to_go, true, false, false, 25);

            Assert.IsTrue(!bAccept);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptOff_Penalty_Turnover()
        {
            int Yards_to_go = 5;
            Penalty p = new Penalty() { bDeclinable = true };
            Play_Result pResult = new Play_Result() { end_of_play_yardline = 50, Yards_Gained = 0, bInterception = true, Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptOff_Penalty(Enum.Play_Enum.RUN, pResult, Yards_to_go, true, false, false, 25);

            Assert.IsTrue(!bAccept);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptOff_Spot_Foul_Penalty_Turnover()
        {
            int Yards_to_go = 5;
            Penalty p = new Penalty() { bDeclinable = true, bSpot_Foul = true };
            Play_Result pResult = new Play_Result() { end_of_play_yardline = 50, Yards_Gained = 0, bInterception = true, Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptOff_Penalty(Enum.Play_Enum.RUN, pResult, Yards_to_go, true, false, false, 25);

            Assert.IsTrue(!bAccept);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptOff_Penalty_TD()
        {
            int Yards_to_go = 5;
            Penalty p = new Penalty() { bDeclinable = true };
            Play_Result pResult = new Play_Result() { end_of_play_yardline = 50, Yards_Gained = 50, bTouchDown = true, Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptOff_Penalty(Enum.Play_Enum.RUN, pResult, Yards_to_go, true, false, false, 25);

            Assert.IsTrue(bAccept);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptOff_Spot_Foul_Penalty_TD()
        {
            int Yards_to_go = 5;
            Penalty p = new Penalty() { bDeclinable = true, bSpot_Foul = true };
            Play_Result pResult = new Play_Result() { end_of_play_yardline = 50, Yards_Gained = 50, bTouchDown = true, Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptOff_Penalty(Enum.Play_Enum.RUN, pResult, Yards_to_go, true, false, false, 25);

            Assert.IsTrue(bAccept);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptOff_Penalty_Safety()
        {
            int Yards_to_go = 5;
            Penalty p = new Penalty() { bDeclinable = true };
            Play_Result pResult = new Play_Result() { end_of_play_yardline = 50, Yards_Gained = 50, bSafety = true, Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptOff_Penalty(Enum.Play_Enum.PASS, pResult, Yards_to_go, true, false, false, 25);

            Assert.IsTrue(!bAccept);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptOff_Spot_Foul_Penalty_Safety()
        {
            int Yards_to_go = 5;
            Penalty p = new Penalty() { bDeclinable = true, bSpot_Foul = true };
            Play_Result pResult = new Play_Result() { end_of_play_yardline = 50, Yards_Gained = 50, bSafety = true, Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptOff_Penalty(Enum.Play_Enum.PASS, pResult, Yards_to_go, true, false, false, 25);

            Assert.IsTrue(!bAccept);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptOff_Penalty_xpmissed()
        {
            int Yards_to_go = 5;
            Penalty p = new Penalty() { bDeclinable = true };
            Play_Result pResult = new Play_Result() { end_of_play_yardline = 50, Yards_Gained = 50, bXPMissed = true, Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptOff_Penalty(Enum.Play_Enum.EXTRA_POINT, pResult, Yards_to_go, true, false, false, 25);

            Assert.IsTrue(!bAccept);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptOff_Spot_Foul_Penalty_xpmissed()
        {
            int Yards_to_go = 5;
            Penalty p = new Penalty() { bDeclinable = true, bSpot_Foul = true };
            Play_Result pResult = new Play_Result() { end_of_play_yardline = 50, Yards_Gained = 50, bXPMissed = true, Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptOff_Penalty(Enum.Play_Enum.EXTRA_POINT, pResult, Yards_to_go, true, false, false, 25);

            Assert.IsTrue(!bAccept);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptOff_Penalty_xpmade()
        {
            int Yards_to_go = 5;
            Penalty p = new Penalty() { bDeclinable = true };
            Play_Result pResult = new Play_Result() { end_of_play_yardline = 50, Yards_Gained = 50, bXPMade = true, Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptOff_Penalty(Enum.Play_Enum.EXTRA_POINT, pResult, Yards_to_go, true, false, false, 25);

            Assert.IsTrue(bAccept);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptOff_Spot_Foul_Penalty_xpmade()
        {
            int Yards_to_go = 5;
            Penalty p = new Penalty() { bDeclinable = true, bSpot_Foul = true };
            Play_Result pResult = new Play_Result() { end_of_play_yardline = 50, Yards_Gained = 50, bXPMade = true, Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptOff_Penalty(Enum.Play_Enum.EXTRA_POINT, pResult, Yards_to_go, true, false, false, 25);

            Assert.IsTrue(bAccept);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptOff_Penalty_FGMissed()
        {
            int Yards_to_go = 5;
            Penalty p = new Penalty() { bDeclinable = true };
            Play_Result pResult = new Play_Result() { end_of_play_yardline = 50, Yards_Gained = 50, bFGMissed = true, Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptOff_Penalty(Enum.Play_Enum.FIELD_GOAL, pResult, Yards_to_go, true, false, false, 25);

            Assert.IsTrue(!bAccept);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptOff_Penalty_FGmADE()
        {
            int Yards_to_go = 5;
            Penalty p = new Penalty() { bDeclinable = true };
            Play_Result pResult = new Play_Result() { end_of_play_yardline = 50, Yards_Gained = 0, bFGMade = true, Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptOff_Penalty(Enum.Play_Enum.FIELD_GOAL, pResult, Yards_to_go, true, false, false, 25);

            Assert.IsTrue(bAccept);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptOff_Spot_Foul_Penalty_FGmADE()
        {
            int Yards_to_go = 5;
            Penalty p = new Penalty() { bDeclinable = true, bSpot_Foul = true };
            Play_Result pResult = new Play_Result() { end_of_play_yardline = 50, Yards_Gained = 0, bFGMade = true, Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptOff_Penalty(Enum.Play_Enum.FIELD_GOAL, pResult, Yards_to_go, true, false, false, 25);

            Assert.IsTrue(bAccept);
        }


        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptoFF_Penalty_bOnePntAfterTDMissed()
        {
            int Yards_to_go = 2;
            Penalty p = new Penalty() { bDeclinable = true, Yards = 10 };
            Play_Result pResult = new Play_Result() { end_of_play_yardline = 88, bOnePntAfterTDMissed = true, Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0, Quarter = 4, Time = 100 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptOff_Penalty(Enum.Play_Enum.FIELD_GOAL, pResult, Yards_to_go, true, false, false, 25);

            Assert.IsTrue(!bAccept);
        }
        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptoFF_Penalty_bOnePntAfterTDMade()
        {
            int Yards_to_go = 2;
            Penalty p = new Penalty() { bDeclinable = true, Yards = 10 };
            Play_Result pResult = new Play_Result() { end_of_play_yardline = 88, bOnePntAfterTDMade = true, Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0, Quarter = 4, Time = 100 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptOff_Penalty(Enum.Play_Enum.FIELD_GOAL, pResult, Yards_to_go, true, false, false, 25);

            Assert.IsTrue(bAccept);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptoFF_Penalty_bTwoPntAfterTDMissed()
        {
            int Yards_to_go = 2;
            Penalty p = new Penalty() { bDeclinable = true, Yards = 10 };
            Play_Result pResult = new Play_Result() { end_of_play_yardline = 88, bTwoPntAfterTDMissed = true, Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0, Quarter = 4, Time = 100 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptOff_Penalty(Enum.Play_Enum.FIELD_GOAL, pResult, Yards_to_go, true, false, false, 25);

            Assert.IsTrue(!bAccept);
        }
        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptoFF_Penalty_bTwoPntAfterTDMade()
        {
            int Yards_to_go = 2;
            Penalty p = new Penalty() { bDeclinable = true, Yards = 10 };
            Play_Result pResult = new Play_Result() { end_of_play_yardline = 88, bTwoPntAfterTDMade = true, Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0, Quarter = 4, Time = 100 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptOff_Penalty(Enum.Play_Enum.FIELD_GOAL, pResult, Yards_to_go, true, false, false, 25);

            Assert.IsTrue(bAccept);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptoFF_Penalty_bThreePntAfterTDMissed()
        {
            int Yards_to_go = 2;
            Penalty p = new Penalty() { bDeclinable = true, Yards = 10 };
            Play_Result pResult = new Play_Result() { end_of_play_yardline = 88, bThreePntAfterTDMissed = true, Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0, Quarter = 4, Time = 100 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptOff_Penalty(Enum.Play_Enum.FIELD_GOAL, pResult, Yards_to_go, true, false, false, 25);

            Assert.IsTrue(!bAccept);
        }
        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptOff_FG_Penalty_bThreePntAfterTDMade()
        {
            int Yards_to_go = 2;
            Penalty p = new Penalty() { bDeclinable = true, Yards = 10 };
            Play_Result pResult = new Play_Result() { end_of_play_yardline = 88, bThreePntAfterTDMade = true, Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0, Quarter = 4, Time = 100 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptOff_Penalty(Enum.Play_Enum.FIELD_GOAL, pResult, Yards_to_go, true, false, false, 25);

            Assert.IsTrue(bAccept);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptOff_Penalty_LastPlay_not_winning_Accept()
        {
            int Yards_to_go = 2;
            Penalty p = new Penalty() { bDeclinable = true, Yards = 10 };
            Play_Result pResult = new Play_Result() { end_of_play_yardline = 88, bThreePntAfterTDMade = false, Penalty = p };
            Game g = new Game() { Home_Score = 7, Away_Score = 0, Quarter = 4, Time = 0 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptOff_Penalty(Enum.Play_Enum.PASS, pResult, Yards_to_go, true, true, false, 25);

            Assert.IsTrue(bAccept);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptOff_Punt_Off_Penalty_Bad_Net_Yards_Decline()
        {
            int Yards_to_go = 2;
            Penalty p = new Penalty() { bDeclinable = true, Yards = 10 };
            Play_Result pResult = new Play_Result() { Play_Start_Yardline = 50, end_of_play_yardline = 88, bCoffinCornerMade = false, Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0, Quarter = 4, Time = 0 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptOff_Penalty(Enum.Play_Enum.PUNT, pResult, Yards_to_go, true, false, false, 25);

            Assert.IsTrue(!bAccept);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptOff_Right_Punt_Off_Penalty_Good_Net_Yards_Accept()
        {
            int Yards_to_go = 2;
            Penalty p = new Penalty() { bDeclinable = true, Yards = 10 };
            Play_Result pResult = new Play_Result() { Play_Start_Yardline = 88, end_of_play_yardline = 20, bCoffinCornerMade = false, Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0, Quarter = 4, Time = 0 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptOff_Penalty(Enum.Play_Enum.PUNT, pResult, Yards_to_go, false, false, false, 25);

            Assert.IsTrue(bAccept);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptOff_Right_Punt_Off_Penalty_Bad_Net_Yards_Decline()
        {
            int Yards_to_go = 2;
            Penalty p = new Penalty() { bDeclinable = true, Yards = 10 };
            Play_Result pResult = new Play_Result() {Play_Start_Yardline = 80, end_of_play_yardline = 50, bCoffinCornerMade = false, Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0, Quarter = 4, Time = 0 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptOff_Penalty(Enum.Play_Enum.PUNT, pResult, Yards_to_go, false, false, false, 25);

            Assert.IsTrue(!bAccept);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptOff_Punt_Off_Penalty_Good_Net_Yards_Accept()
        {
            int Yards_to_go = 2;
            Penalty p = new Penalty() { bDeclinable = true, Yards = 10 };
            Play_Result pResult = new Play_Result() { Play_Start_Yardline = 20, end_of_play_yardline = 88, bCoffinCornerMade = false, Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0, Quarter = 4, Time = 0 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptOff_Penalty(Enum.Play_Enum.PUNT, pResult, Yards_to_go, true, false, false, 25);

            Assert.IsTrue(bAccept);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptOff_Punt_Bad_Net_Touchback_Decline()
        {
            int Yards_to_go = 2;
            Penalty p = new Penalty() { bDeclinable = true, Yards = 10 };
            Play_Result pResult = new Play_Result() { Play_Start_Yardline = 50, end_of_play_yardline = 105, bTouchback = true, Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0, Quarter = 4, Time = 0 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptOff_Penalty(Enum.Play_Enum.PUNT, pResult, Yards_to_go, true, false, false, 25);

            Assert.IsTrue(!bAccept);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptOff_Punt_Good_Net_Turnover_Accept()
        {
            int Yards_to_go = 2;
            Penalty p = new Penalty() { bDeclinable = true, Yards = 10 };
            Play_Result pResult = new Play_Result() { Play_Start_Yardline = 20, end_of_play_yardline = 105, bFumble_Lost = true, Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0, Quarter = 4, Time = 0 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptOff_Penalty(Enum.Play_Enum.PUNT, pResult, Yards_to_go, true, false, false, 25);

            Assert.IsTrue(bAccept);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptOff_Punt_Coffin_Corner_Decline()
        {
            int Yards_to_go = 2;
            Penalty p = new Penalty() { bDeclinable = true, Yards = 10 };
            Play_Result pResult = new Play_Result() { Play_Start_Yardline = 50, end_of_play_yardline = 105, bCoffinCornerMade = true, Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0, Quarter = 4, Time = 0 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptOff_Penalty(Enum.Play_Enum.PUNT, pResult, Yards_to_go, true, false, false, 25);

            Assert.IsTrue(bAccept);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptOff_Punt_TD_Decline()
        {
            int Yards_to_go = 2;
            Penalty p = new Penalty() { bDeclinable = true, Yards = 10 };
            Play_Result pResult = new Play_Result() { Play_Start_Yardline = 50, end_of_play_yardline = 0, bTouchDown = true, Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0, Quarter = 4, Time = 0 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptOff_Penalty(Enum.Play_Enum.PUNT, pResult, Yards_to_go, true, false, false, 25);

            Assert.IsTrue(!bAccept);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptOff_Penalty_kickoff_accept()
        {
            int Yards_to_go = 0;
            Penalty p = new Penalty() { bDeclinable = true, Yards = 10 };
            Play_Result pResult = new Play_Result() { Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0, Quarter = 4, Time = 0 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptOff_Penalty(Enum.Play_Enum.KICKOFF_NORMAL, pResult, Yards_to_go, true, false, false, 25);

            Assert.IsTrue(bAccept);
        }
        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptOff_Penalty_kickoff_TD_accept()
        {
            int Yards_to_go = 0;
            Penalty p = new Penalty() { bDeclinable = true, Yards = 10 };
            Play_Result pResult = new Play_Result() { Penalty = p, bTouchDown = true };
            Game g = new Game() { Home_Score = 0, Away_Score = 0, Quarter = 4, Time = 0 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptOff_Penalty(Enum.Play_Enum.KICKOFF_NORMAL, pResult, Yards_to_go, true, false, false, 25);

            Assert.IsTrue(bAccept);
        }
        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptOff_Penalty_kickoff_Turnover_Reject()
        {
            int Yards_to_go = 0;
            Penalty p = new Penalty() { bDeclinable = true, Yards = 10 };
            Play_Result pResult = new Play_Result() { Penalty = p, bFumble = true, bFumble_Lost = true };
            Game g = new Game() { Home_Score = 0, Away_Score = 0, Quarter = 4, Time = 0 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptOff_Penalty(Enum.Play_Enum.KICKOFF_NORMAL, pResult, Yards_to_go, true, false, false, 25);

            Assert.IsTrue(!bAccept);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptOff_Penalty_Run_For_Loss_Dont_Accept()
        {
            int Yards_to_go = 2;
            Penalty p = new Penalty() { bDeclinable = true, Yards = 10 };
            Play_Result pResult = new Play_Result() { end_of_play_yardline = 88, Yards_Gained = -5, Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0, Quarter = 4, Time = 300 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptOff_Penalty(Enum.Play_Enum.RUN, pResult, Yards_to_go, true, false, false, 25);

            Assert.IsTrue(!bAccept);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptOff_Penalty_Pas_For_Good_Game_Accept()
        {
            int Yards_to_go = 2;
            Penalty p = new Penalty() { bDeclinable = true, Yards = 10 };
            Play_Result pResult = new Play_Result() { end_of_play_yardline = 88, Yards_Gained = 10, Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0, Quarter = 4, Time = 300 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptOff_Penalty(Enum.Play_Enum.PASS, pResult, Yards_to_go, true, false, false, 25);

            Assert.IsTrue(bAccept);
        }
        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptOff_Penalty_Pas_For_Good_Game_Accept_End_of_Game()
        {
            int Yards_to_go = 2;
            Penalty p = new Penalty() { bDeclinable = true, Yards = 10 };
            Play_Result pResult = new Play_Result() { end_of_play_yardline = 20, Yards_Gained = 40, Penalty = p };
            Game g = new Game() { Home_Score = 7, Away_Score = 0, Quarter = 4, Time = 0 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptOff_Penalty(Enum.Play_Enum.PASS, pResult, Yards_to_go, true, true, false, 25);

            Assert.IsTrue(bAccept);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptOff_Penalty_bOnePntAfterTDMissed()
        {
            int Yards_to_go = 2;
            Penalty p = new Penalty() { bDeclinable = true, Yards = 10 };
            Play_Result pResult = new Play_Result() { end_of_play_yardline = 88, bOnePntAfterTDMissed = true, Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0, Quarter = 4, Time = 100 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptOff_Penalty(Enum.Play_Enum.SCRIM_PLAY_1XP_RUN, pResult, Yards_to_go, true, false, false, 25);

            Assert.IsTrue(!bAccept);
        }
        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptOff_Penalty_bOnePntAfterTDMade()
        {
            int Yards_to_go = 2;
            Penalty p = new Penalty() { bDeclinable = true, Yards = 10 };
            Play_Result pResult = new Play_Result() { end_of_play_yardline = 88, bOnePntAfterTDMade = true, Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0, Quarter = 4, Time = 100 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptOff_Penalty(Enum.Play_Enum.SCRIM_PLAY_1XP_PASS, pResult, Yards_to_go, true, false, false, 25);

            Assert.IsTrue(bAccept);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptOff_Penalty_bTwoPntAfterTDMissed()
        {
            int Yards_to_go = 2;
            Penalty p = new Penalty() { bDeclinable = true, Yards = 10 };
            Play_Result pResult = new Play_Result() { end_of_play_yardline = 88, bTwoPntAfterTDMissed = true, Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0, Quarter = 4, Time = 100 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptOff_Penalty(Enum.Play_Enum.SCRIM_PLAY_2XP_RUN, pResult, Yards_to_go, true, false, false, 25);

            Assert.IsTrue(!bAccept);
        }
        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptOff_Penalty_bTwoPntAfterTDMade()
        {
            int Yards_to_go = 2;
            Penalty p = new Penalty() { bDeclinable = true, Yards = 10 };
            Play_Result pResult = new Play_Result() { end_of_play_yardline = 88, bTwoPntAfterTDMade = true, Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0, Quarter = 4, Time = 100 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptOff_Penalty(Enum.Play_Enum.SCRIM_PLAY_2XP_PASS, pResult, Yards_to_go, true, false, false, 25);

            Assert.IsTrue(bAccept);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptOff_Penalty_bThreePntAfterTDMissed()
        {
            int Yards_to_go = 2;
            Penalty p = new Penalty() { bDeclinable = true, Yards = 10 };
            Play_Result pResult = new Play_Result() { end_of_play_yardline = 88, bThreePntAfterTDMissed = true, Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0, Quarter = 4, Time = 100 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptOff_Penalty(Enum.Play_Enum.SCRIM_PLAY_2XP_RUN, pResult, Yards_to_go, true, false, false, 25);

            Assert.IsTrue(!bAccept);
        }
        [TestCategory("Penalties")]
        [TestMethod]
        public void AcceptOff_Penalty_bThreePntAfterTDMade()
        {
            int Yards_to_go = 2;
            Penalty p = new Penalty() { bDeclinable = true, Yards = 10 };
            Play_Result pResult = new Play_Result() { end_of_play_yardline = 88, bThreePntAfterTDMade = true, Penalty = p };
            Game g = new Game() { Home_Score = 0, Away_Score = 0, Quarter = 4, Time = 100 };
            Coach c = new Coach(11, g, 7, new List<Player_and_Ratings>(), new List<Player_and_Ratings>(), new List<Injury>());
            bool bAccept = c.AcceptOff_Penalty(Enum.Play_Enum.SCRIM_PLAY_3XP_RUN, pResult, Yards_to_go, true, false, false, 25);

            Assert.IsTrue(bAccept);
        }


    }
}
