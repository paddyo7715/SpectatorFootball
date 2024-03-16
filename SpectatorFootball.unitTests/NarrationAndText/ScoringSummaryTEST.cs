using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpectatorFootball.GameNS;
using SpectatorFootball.NarrationAndText;
using SpectatorFootball.unitTests.Helper_ClassesNS;
using System;
using System.Collections.Generic;

namespace SpectatorFootball.unitTests.NarrationAndText
{
    [TestClass]
    public class ScoringSummaryTEST
    {
        [TestCategory("NarrationAndText")]
        [TestMethod]
        public void CreateScoringSummaryEntry_KO_TD()
        {
            List<Game_Player> Kickoff_Players = Help_Class.getRandomPlayersforPlay(1);

            Game_Player gp = Kickoff_Players[0];
            gp.p_and_r.p.First_Name = "Jim";
            gp.p_and_r.p.Last_Name = "Miller";
            Play_Result pResult = new Play_Result();
            pResult.Returner = gp;
            pResult.bAwayTD = true;
            pResult.Yards_Returned = 99.0;

            string s = ScoringSummary.CreateScoringSummaryEntry(Enum.Play_Enum.KICKOFF_NORMAL, pResult);
            string expected_result = "J Miller kickoff return of 99 yards for a TD";                 
        
            Assert.IsTrue(s == expected_result);    
        }
        [TestCategory("NarrationAndText")]
        [TestMethod]
        public void CreateScoringSummaryEntry_FK_TD()
        {
            List<Game_Player> Kickoff_Players = Help_Class.getRandomPlayersforPlay(1);

            Game_Player gp = Kickoff_Players[0];
            gp.p_and_r.p.First_Name = "Jim";
            gp.p_and_r.p.Last_Name = "Miller";
            Play_Result pResult = new Play_Result();
            pResult.Returner = gp;
            pResult.bAwayTD = true;
            pResult.Yards_Returned = 99.0;

            string s = ScoringSummary.CreateScoringSummaryEntry(Enum.Play_Enum.FREE_KICK, pResult);
            string expected_result = "J Miller free kick return of 99 yards for a TD";

            Assert.IsTrue(s == expected_result);
        }
        [TestCategory("NarrationAndText")]
        [TestMethod]
        public void CreateScoringSummaryEntry_PR_TD()
        {
            List<Game_Player> Kickoff_Players = Help_Class.getRandomPlayersforPlay(1);

            Game_Player gp = Kickoff_Players[0];
            gp.p_and_r.p.First_Name = "Jim";
            gp.p_and_r.p.Last_Name = "Miller";
            Play_Result pResult = new Play_Result();
            pResult.Punt_Returner = gp;
            pResult.bAwayTD = true;
            pResult.Yards_Returned = 99.0;

            string s = ScoringSummary.CreateScoringSummaryEntry(Enum.Play_Enum.PUNT, pResult);
            string expected_result = "J Miller punt return of 99 yards for a TD";

            Assert.IsTrue(s == expected_result);
        }

        [TestCategory("NarrationAndText")]
        [TestMethod]
        public void CreateScoringSummaryEntry_FG_Good()
        {
            List<Game_Player> Kickoff_Players = Help_Class.getRandomPlayersforPlay(1);

            Game_Player gp = Kickoff_Players[0];
            gp.p_and_r.p.First_Name = "Jim";
            gp.p_and_r.p.Last_Name = "Miller";
            Play_Result pResult = new Play_Result();
            pResult.Kicker = gp;
            pResult.bAwayFG = true;
            pResult.Field_Goal_Attempt_Length = 50.0;

            string s = ScoringSummary.CreateScoringSummaryEntry(Enum.Play_Enum.FIELD_GOAL, pResult);
            string expected_result = "J Miller makes a 50 yard field goal";

            Assert.IsTrue(s == expected_result);
        }
        [TestCategory("NarrationAndText")]
        [TestMethod]
        public void CreateScoringSummaryEntry_EP_Good()
        {
            List<Game_Player> Kickoff_Players = Help_Class.getRandomPlayersforPlay(1);

            Game_Player gp = Kickoff_Players[0];
            gp.p_and_r.p.First_Name = "Jim";
            gp.p_and_r.p.Last_Name = "Miller";
            Play_Result pResult = new Play_Result();
            pResult.Kicker = gp;
            pResult.bAwayXP = true;
 
            string s = ScoringSummary.CreateScoringSummaryEntry(Enum.Play_Enum.EXTRA_POINT, pResult);
            string expected_result = "J Miller makes the extra point";

            Assert.IsTrue(s == expected_result);
        }
        [TestCategory("NarrationAndText")]
        [TestMethod]
        public void CreateScoringSummaryEntry_1XP_RUN()
        {
            List<Game_Player> Kickoff_Players = Help_Class.getRandomPlayersforPlay(1);

            Game_Player gp = Kickoff_Players[0];
            gp.p_and_r.p.First_Name = "Jim";
            gp.p_and_r.p.Last_Name = "Miller";
            Play_Result pResult = new Play_Result();
            pResult.Running_Back = gp;
            pResult.bAwayXP1 = true;

            string s = ScoringSummary.CreateScoringSummaryEntry(Enum.Play_Enum.SCRIM_PLAY_1XP_RUN, pResult);
            string expected_result = "J Miller runs in the 1 point conversion play";

            Assert.IsTrue(s == expected_result);
        }
        [TestCategory("NarrationAndText")]
        [TestMethod]
        public void CreateScoringSummaryEntry_2XP_RUN()
        {
            List<Game_Player> Kickoff_Players = Help_Class.getRandomPlayersforPlay(1);

            Game_Player gp = Kickoff_Players[0];
            gp.p_and_r.p.First_Name = "Jim";
            gp.p_and_r.p.Last_Name = "Miller";
            Play_Result pResult = new Play_Result();
            pResult.Running_Back = gp;
            pResult.bAwayXP2 = true;

            string s = ScoringSummary.CreateScoringSummaryEntry(Enum.Play_Enum.SCRIM_PLAY_2XP_RUN, pResult);
            string expected_result = "J Miller runs in the 2 point conversion play";

            Assert.IsTrue(s == expected_result);
        }
        [TestCategory("NarrationAndText")]
        [TestMethod]
        public void CreateScoringSummaryEntry_3XP_RUN()
        {
            List<Game_Player> Kickoff_Players = Help_Class.getRandomPlayersforPlay(1);

            Game_Player gp = Kickoff_Players[0];
            gp.p_and_r.p.First_Name = "Jim";
            gp.p_and_r.p.Last_Name = "Miller";
            Play_Result pResult = new Play_Result();
            pResult.Running_Back = gp;
            pResult.bAwayXP3 = true;

            string s = ScoringSummary.CreateScoringSummaryEntry(Enum.Play_Enum.SCRIM_PLAY_3XP_RUN, pResult);
            string expected_result = "J Miller runs in the 3 point conversion play";

            Assert.IsTrue(s == expected_result);
        }

        [TestCategory("NarrationAndText")]
        [TestMethod]
        public void CreateScoringSummaryEntry_1XP_PASS_QBRun()
        {
            List<Game_Player> Kickoff_Players = Help_Class.getRandomPlayersforPlay(2);
            Play_Result pResult = new Play_Result();
            Game_Player gp = Kickoff_Players[0];
            gp.p_and_r.p.First_Name = "Jim";
            gp.p_and_r.p.Last_Name = "Miller";

            Game_Player gp2 = Kickoff_Players[1];
            gp2.p_and_r.p.First_Name = "Frank";
            gp2.p_and_r.p.Last_Name = "Johnson";
            pResult.Receiver = gp2;

            pResult.Passer = gp;
            pResult.Running_Back = gp;
            pResult.bAwayXP1 = true;

            string s = ScoringSummary.CreateScoringSummaryEntry(Enum.Play_Enum.SCRIM_PLAY_1XP_PASS, pResult);
            string expected_result = "J Miller runs in the 1 point conversion play";

            Assert.IsTrue(s == expected_result);
        }
        [TestCategory("NarrationAndText")]
        [TestMethod]
        public void CreateScoringSummaryEntry_1XP_PASS()
        {
            List<Game_Player> Kickoff_Players = Help_Class.getRandomPlayersforPlay(2);
            Play_Result pResult = new Play_Result();
            Game_Player gp = Kickoff_Players[0];
            gp.p_and_r.p.First_Name = "Jim";
            gp.p_and_r.p.Last_Name = "Miller";
            pResult.Passer = gp;
            Game_Player gp2 = Kickoff_Players[1];
            gp2.p_and_r.p.First_Name = "Frank";
            gp2.p_and_r.p.Last_Name = "Johnson";
            pResult.Receiver = gp2;
            pResult.bAwayXP1 = true;

            string s = ScoringSummary.CreateScoringSummaryEntry(Enum.Play_Enum.SCRIM_PLAY_1XP_PASS, pResult);
            string expected_result = "J Miller pass to F Johnson for the 1 point conversion play";

            Assert.IsTrue(s == expected_result);
        }
        [TestCategory("NarrationAndText")]
        [TestMethod]
        public void CreateScoringSummaryEntry_2XP_PASS_QBRun()
        {
            List<Game_Player> Kickoff_Players = Help_Class.getRandomPlayersforPlay(2);

            Play_Result pResult = new Play_Result();
            Game_Player gp = Kickoff_Players[0];
            gp.p_and_r.p.First_Name = "Jim";
            gp.p_and_r.p.Last_Name = "Miller";
            pResult.Passer = gp;

            Game_Player gp2 = Kickoff_Players[1];
            gp2.p_and_r.p.First_Name = "Frank";
            gp2.p_and_r.p.Last_Name = "Johnson";
            pResult.Receiver = gp2;

            pResult.Running_Back = gp;
            pResult.bAwayXP1 = true;

            string s = ScoringSummary.CreateScoringSummaryEntry(Enum.Play_Enum.SCRIM_PLAY_2XP_PASS, pResult);
            string expected_result = "J Miller runs in the 2 point conversion play";

            Assert.IsTrue(s == expected_result);
        }
        [TestCategory("NarrationAndText")]
        [TestMethod]
        public void CreateScoringSummaryEntry_2XP_PASS()
        {
            List<Game_Player> Kickoff_Players = Help_Class.getRandomPlayersforPlay(2);
            Play_Result pResult = new Play_Result();
            Game_Player gp = Kickoff_Players[0];
            gp.p_and_r.p.First_Name = "Jim";
            gp.p_and_r.p.Last_Name = "Miller";
            pResult.Passer = gp;
            Game_Player gp2 = Kickoff_Players[1];
            gp2.p_and_r.p.First_Name = "Frank";
            gp2.p_and_r.p.Last_Name = "Johnson";
            pResult.Receiver = gp2;
            pResult.bAwayXP1 = true;

            string s = ScoringSummary.CreateScoringSummaryEntry(Enum.Play_Enum.SCRIM_PLAY_2XP_PASS, pResult);
            string expected_result = "J Miller pass to F Johnson for the 2 point conversion play";

            Assert.IsTrue(s == expected_result);
        }

        [TestCategory("NarrationAndText")]
        [TestMethod]
        public void CreateScoringSummaryEntry_3XP_PASS_QBRun()
        {
            List<Game_Player> Kickoff_Players = Help_Class.getRandomPlayersforPlay(2);

            Play_Result pResult = new Play_Result();

            Game_Player gp = Kickoff_Players[0];
            gp.p_and_r.p.First_Name = "Jim";
            gp.p_and_r.p.Last_Name = "Miller";
            pResult.Passer = gp;

            Game_Player gp2 = Kickoff_Players[1];
            gp2.p_and_r.p.First_Name = "Frank";
            gp2.p_and_r.p.Last_Name = "Johnson";
            pResult.Receiver = gp2;


            pResult.Running_Back = gp;
            pResult.bAwayXP1 = true;

            string s = ScoringSummary.CreateScoringSummaryEntry(Enum.Play_Enum.SCRIM_PLAY_3XP_PASS, pResult);
            string expected_result = "J Miller runs in the 3 point conversion play";

            Assert.IsTrue(s == expected_result);
        }
        [TestCategory("NarrationAndText")]
        [TestMethod]
        public void CreateScoringSummaryEntry_3XP_PASS()
        {
            List<Game_Player> Kickoff_Players = Help_Class.getRandomPlayersforPlay(2);
            Play_Result pResult = new Play_Result();
            Game_Player gp = Kickoff_Players[0];
            gp.p_and_r.p.First_Name = "Jim";
            gp.p_and_r.p.Last_Name = "Miller";
            pResult.Passer = gp;
            Game_Player gp2 = Kickoff_Players[1];
            gp2.p_and_r.p.First_Name = "Frank";
            gp2.p_and_r.p.Last_Name = "Johnson";
            pResult.Receiver = gp2;
            pResult.bAwayXP1 = true;

            string s = ScoringSummary.CreateScoringSummaryEntry(Enum.Play_Enum.SCRIM_PLAY_3XP_PASS, pResult);
            string expected_result = "J Miller pass to F Johnson for the 3 point conversion play";

            Assert.IsTrue(s == expected_result);
        }
        [TestCategory("NarrationAndText")]
        [TestMethod]
        public void CreateScoringSummaryEntry_Run_TD()
        {
            List<Game_Player> Kickoff_Players = Help_Class.getRandomPlayersforPlay(2);
            Play_Result pResult = new Play_Result();
            Game_Player gp = Kickoff_Players[0];
            gp.p_and_r.p.First_Name = "Jim";
            gp.p_and_r.p.Last_Name = "Miller";
            pResult.Running_Back = gp;
            pResult.Yards_Gained = 10;
            pResult.bAwayTD = true;

            string s = ScoringSummary.CreateScoringSummaryEntry(Enum.Play_Enum.RUN, pResult);
            string expected_result = "J Miller 10 yard run for a TD";

            Assert.IsTrue(s == expected_result);
        }

        [TestCategory("NarrationAndText")]
        [TestMethod]
        public void CreateScoringSummaryEntry_Run_Fumble_Recovered_TD()
        {
            List<Game_Player> Kickoff_Players = Help_Class.getRandomPlayersforPlay(2);
            Play_Result pResult = new Play_Result();
            Game_Player gp = Kickoff_Players[0];
            gp.p_and_r.p.First_Name = "Jim";
            gp.p_and_r.p.Last_Name = "Miller";
            pResult.Running_Back = gp;
            pResult.Yards_Gained = 10;
            pResult.bHomeTD = true;

            Game_Player gp2 = Kickoff_Players[0];
            gp2.p_and_r.p.First_Name = "Frank";
            gp2.p_and_r.p.Last_Name = "Davis";
            pResult.Fumble_Recoverer = gp2;

            string s = ScoringSummary.CreateScoringSummaryEntry(Enum.Play_Enum.RUN, pResult);
            string expected_result = "F Davis recovers a fumble for a defensive TD";

            Assert.IsTrue(s == expected_result);
        }

        [TestCategory("NarrationAndText")]
        [TestMethod]
        public void CreateScoringSummaryEntry_Run_Safety()
        {
            List<Game_Player> Kickoff_Players = Help_Class.getRandomPlayersforPlay(2);
            Play_Result pResult = new Play_Result();
            Game_Player gp = Kickoff_Players[0];
            gp.p_and_r.p.First_Name = "Jim";
            gp.p_and_r.p.Last_Name = "Miller";
            pResult.Running_Back = gp;
            pResult.bHomeSafetyFor = true;

            Game_Player gp2 = Kickoff_Players[0];
            gp2.p_and_r.p.First_Name = "Frank";
            gp2.p_and_r.p.Last_Name = "Davis";
            pResult.Tackler = gp2;

            string s = ScoringSummary.CreateScoringSummaryEntry(Enum.Play_Enum.RUN, pResult);
            string expected_result = "F Davis tackle for loss gets the safety";

            Assert.IsTrue(s == expected_result);
        }

        [TestCategory("NarrationAndText")]
        [TestMethod]
        public void CreateScoringSummaryEntry_Pass_QB_Run_TD()
        {
            List<Game_Player> Kickoff_Players = Help_Class.getRandomPlayersforPlay(2);
            Play_Result pResult = new Play_Result();
            Game_Player gp = Kickoff_Players[0];
            gp.p_and_r.p.First_Name = "Jim";
            gp.p_and_r.p.Last_Name = "Miller";
            pResult.Running_Back = gp;
            pResult.Passer = gp;
            pResult.Yards_Gained = 10;
            pResult.bAwayTD = true;

            Game_Player gp2 = Kickoff_Players[1];
            gp2.p_and_r.p.First_Name = "Frank";
            gp2.p_and_r.p.Last_Name = "Johnson";
            pResult.Receiver = gp2;

            string s = ScoringSummary.CreateScoringSummaryEntry(Enum.Play_Enum.PASS, pResult);
            string expected_result = "J Miller 10 yard run for a TD";

            Assert.IsTrue(s == expected_result);
        }

        [TestCategory("NarrationAndText")]
        [TestMethod]
        public void CreateScoringSummaryEntry_PASS()
        {
            List<Game_Player> Kickoff_Players = Help_Class.getRandomPlayersforPlay(2);
            Play_Result pResult = new Play_Result();
            Game_Player gp = Kickoff_Players[0];
            gp.p_and_r.p.First_Name = "Jim";
            gp.p_and_r.p.Last_Name = "Miller";
            pResult.Passer = gp;
            Game_Player gp2 = Kickoff_Players[1];
            gp2.p_and_r.p.First_Name = "Frank";
            gp2.p_and_r.p.Last_Name = "Johnson";
            pResult.Receiver = gp2;
            pResult.bAwayTD = true;
            pResult.Yards_Gained = 20.0;

            string s = ScoringSummary.CreateScoringSummaryEntry(Enum.Play_Enum.PASS, pResult);
            string expected_result = "J Miller 20 yard pass to F Johnson for the TD";

            Assert.IsTrue(s == expected_result);
        }

        [TestCategory("NarrationAndText")]
        [TestMethod]
        public void CreateScoringSummaryEntry_Pass_Fumble_Recovered_TD()
        {
            List<Game_Player> Kickoff_Players = Help_Class.getRandomPlayersforPlay(2);
            Play_Result pResult = new Play_Result();
            Game_Player gp = Kickoff_Players[0];
            gp.p_and_r.p.First_Name = "Jim";
            gp.p_and_r.p.Last_Name = "Miller";
            pResult.Passer = gp;
            pResult.bHomeTD = true;

            Game_Player gp2 = Kickoff_Players[0];
            gp2.p_and_r.p.First_Name = "Frank";
            gp2.p_and_r.p.Last_Name = "Davis";
            pResult.Fumble_Recoverer = gp2;

            string s = ScoringSummary.CreateScoringSummaryEntry(Enum.Play_Enum.PASS, pResult);
            string expected_result = "F Davis recovers a fumble for a defensive TD";

            Assert.IsTrue(s == expected_result);
        }

        [TestCategory("NarrationAndText")]
        [TestMethod]
        public void CreateScoringSummaryEntry_Pass_Safety()
        {
            List<Game_Player> Kickoff_Players = Help_Class.getRandomPlayersforPlay(2);
            Play_Result pResult = new Play_Result();
            Game_Player gp = Kickoff_Players[0];
            gp.p_and_r.p.First_Name = "Jim";
            gp.p_and_r.p.Last_Name = "Miller";
            pResult.Tackler = gp;
            pResult.bHomeSafetyFor = true;

            Game_Player gp2 = Kickoff_Players[0];
            gp2.p_and_r.p.First_Name = "Frank";
            gp2.p_and_r.p.Last_Name = "Davis";
            pResult.Tackler = gp2;

            string s = ScoringSummary.CreateScoringSummaryEntry(Enum.Play_Enum.PASS, pResult);
            string expected_result = "F Davis sack for a safety";

            Assert.IsTrue(s == expected_result);
        }
    }
}
