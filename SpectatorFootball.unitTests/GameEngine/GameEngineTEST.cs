using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpectatorFootball.GameNS;
using SpectatorFootball.Models;
using SpectatorFootball.Enum;
using SpectatorFootball.PenaltiesNS;

namespace SpectatorFootball.unitTests.GameEngineNS
{
    [TestClass]
    public class GameEngineTEST
    {
        [TestCategory("GameEngine")]
        [TestMethod]
        public void coinToss_HTWins()
        {
            long r = GameEngine.CoinToss(49, 11, 22);

            Assert.IsTrue(r == 11);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void coinToss_ATWins()
        {
            long r = GameEngine.CoinToss(51, 11, 22);

            Assert.IsTrue(r == 22);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void isPlayWithReturner_Kickoff_true()
        {
            Assert.IsTrue(GameEngine.isPlayWithReturner(Play_Enum.KICKOFF_NORMAL) == true);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void isPlayWithReturner_FreeKick_true()
        {
            Assert.IsTrue(GameEngine.isPlayWithReturner(Play_Enum.FREE_KICK) == true);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void isPlayWithReturner_Punt_true()
        {
            Assert.IsTrue(GameEngine.isPlayWithReturner(Play_Enum.PUNT) == true);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void isPlayWithReturner_Pass_false()
        {
            Assert.IsTrue(GameEngine.isPlayWithReturner(Play_Enum.PASS) == false);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void Switch_Posession_at_possession()
        {
            Assert.IsTrue(GameEngine.Switch_Posession(11, 11, 22) == 22);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void Switch_Posession_ht_possession()
        {
            Assert.IsTrue(GameEngine.Switch_Posession(22, 11, 22) == 11);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void getPlayerAction_Passer()
        {
            Game_Player p = new Game_Player();
            Play_Result pResult = new Play_Result();
            pResult.Passer = p;
            pResult.Penalized_Player = p;
            bool b = GameEngine.isBallTeamPenalty(pResult);
            Assert.IsTrue(b);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void getPlayerAction_Kicker()
        {
            Game_Player p = new Game_Player();
            Play_Result pResult = new Play_Result();
            pResult.Kicker = p;
            pResult.Penalized_Player = p;
            bool b = GameEngine.isBallTeamPenalty(pResult);
            Assert.IsTrue(!b);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void getPlayerAction_Punter()
        {
            Game_Player p = new Game_Player();
            Play_Result pResult = new Play_Result();
            pResult.Punter = p;
            pResult.Penalized_Player = p;
            bool b = GameEngine.isBallTeamPenalty(pResult);
            Assert.IsTrue(!b);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void getPlayerAction_Returner()
        {
            Game_Player p = new Game_Player();
            Play_Result pResult = new Play_Result();
            pResult.Returner = p;
            pResult.Penalized_Player = p;
            bool b = GameEngine.isBallTeamPenalty(pResult);
            Assert.IsTrue(b);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void getPlayerAction_PuntReturner()
        {
            Game_Player p = new Game_Player();
            Play_Result pResult = new Play_Result();
            pResult.Punt_Returner = p;
            pResult.Penalized_Player = p;
            bool b = GameEngine.isBallTeamPenalty(pResult);
            Assert.IsTrue(b);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void getPlayerAction_PassCatchers()
        {
            Game_Player p = new Game_Player();
            Play_Result pResult = new Play_Result();
            pResult.Pass_Catchers.Add(p);
            pResult.Penalized_Player = p;
            bool b = GameEngine.isBallTeamPenalty(pResult);
            Assert.IsTrue(b);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void getPlayerAction_BallRunners()
        {
            Game_Player p = new Game_Player();
            Play_Result pResult = new Play_Result();
            pResult.Ball_Runners.Add(p);
            pResult.Penalized_Player = p;
            bool b = GameEngine.isBallTeamPenalty(pResult);
            Assert.IsTrue(b);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void getPlayerAction_PassBlockers()
        {
            Game_Player p = new Game_Player();
            Play_Result pResult = new Play_Result();
            pResult.Pass_Blockers.Add(p);
            pResult.Penalized_Player = p;
            bool b = GameEngine.isBallTeamPenalty(pResult);
            Assert.IsTrue(b);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void getPlayerAction_PassRushers()
        {
            Game_Player p = new Game_Player();
            Play_Result pResult = new Play_Result();
            pResult.Pass_Rushers.Add(p);
            pResult.Penalized_Player = p;
            bool b = GameEngine.isBallTeamPenalty(pResult);
            Assert.IsTrue(!b);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void getPlayerAction_PassDefenders()
        {
            Game_Player p = new Game_Player();
            Play_Result pResult = new Play_Result();
            pResult.Pass_Defenders.Add(p);
            pResult.Penalized_Player = p;
            bool b = GameEngine.isBallTeamPenalty(pResult);
            Assert.IsTrue(!b);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void getPlayerAction_RunDefenders()
        {
            Game_Player p = new Game_Player();
            Play_Result pResult = new Play_Result();
            pResult.Run_Defenders.Add(p);
            pResult.Penalized_Player = p;
            bool b = GameEngine.isBallTeamPenalty(pResult);
            Assert.IsTrue(!b);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void getPlayerAction_KickReturners()
        {
            Game_Player p = new Game_Player();
            Play_Result pResult = new Play_Result();
            pResult.Kick_Returners.Add(p);
            pResult.Penalized_Player = p;
            bool b = GameEngine.isBallTeamPenalty(pResult);
            Assert.IsTrue(b);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void getPlayerAction_KickDefenders()
        {
            Game_Player p = new Game_Player();
            Play_Result pResult = new Play_Result();
            pResult.Kick_Defenders.Add(p);
            pResult.Penalized_Player = p;
            bool b = GameEngine.isBallTeamPenalty(pResult);
            Assert.IsTrue(!b);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void getPlayerAction_PuntReturners()
        {
            Game_Player p = new Game_Player();
            Play_Result pResult = new Play_Result();
            pResult.Punt_Returners.Add(p);
            pResult.Penalized_Player = p;
            bool b = GameEngine.isBallTeamPenalty(pResult);
            Assert.IsTrue(b);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void getPlayerAction_Field_Goal_Kicking_Team()
        {
            Game_Player p = new Game_Player();
            Play_Result pResult = new Play_Result();
            pResult.FieldGaol_Kicking_Team.Add(p);
            pResult.Penalized_Player = p;
            bool b = GameEngine.isBallTeamPenalty(pResult);
            Assert.IsTrue(b);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void getPlayerAction_Field_Goal_Defenders()
        {
            Game_Player p = new Game_Player();
            Play_Result pResult = new Play_Result();
            pResult.Field_Goal_Defenders.Add(p);
            pResult.Penalized_Player = p;
            bool b = GameEngine.isBallTeamPenalty(pResult);
            Assert.IsTrue(!b);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void isGameEnd__Regualr_3rdQuarterTied_false()
        {
            Assert.IsTrue(GameEngine.isGameEnd(0, 3, 100, 10, 10) == false);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void isGameEnd_Regualr_EndofGameTied_False()
        {
            Assert.IsTrue(GameEngine.isGameEnd(0, 4, 0, 10, 10) == false);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void isGameEnd_Playeroffs_EndofGame_true()
        {
            Assert.IsTrue(GameEngine.isGameEnd(1, 4, 0, 10, 20) == true);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void isGameEnd_playoffs_OertimeTied_true()
        {
            Assert.IsTrue(GameEngine.isGameEnd(1, 6, 100, 20, 20) == false);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void isGameEnd_playoffs_FirstOertimeTied_true()
        {
            Assert.IsTrue(GameEngine.isGameEnd(1, 5, 0, 20, 20) == false);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void isGameEnd_Regualr_FirstOertimeTied_true()
        {
            Assert.IsTrue(GameEngine.isGameEnd(0, 5, 0, 20, 20) == true);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void getScrimmageLine_left_20()
        {
            Assert.IsTrue(GameEngine.getScrimmageLine(20.0, true) == 20.0);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void getScrimmageLine_right_20()
        {
            Assert.IsTrue(GameEngine.getScrimmageLine(20.0, false) == 80.0);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void setScoringBool_home_team_TD()
        {
            Play_Result pResult = new Play_Result()
            { BallPossessing_Team_Id = 11, ht = 11, at = 22, bTouchDown = true };

            pResult = GameEngine.setScoringBool(pResult);

            Assert.IsTrue(pResult.bHomeTD == true && pResult.bHomeFG == false && pResult.bHomeXP == false &&
                pResult.bHomeXP1 == false && pResult.bHomeXP2 == false && pResult.bHomeXP3 == false &&
                pResult.bHomeSafetyFor == false &&
                pResult.bAwayTD == false && pResult.bAwayFG == false && pResult.bAwayXP == false &&
                pResult.bAwayXP1 == false && pResult.bAwayXP2 == false && pResult.bAwayXP3 == false &&
                pResult.bAwaySafetyFor == false);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setScoringBool_away_team_TD()
        {
            Play_Result pResult = new Play_Result()
            { BallPossessing_Team_Id = 22, ht = 11, at = 22, bTouchDown = true };

            pResult = GameEngine.setScoringBool(pResult);

            Assert.IsTrue(pResult.bHomeTD == false && pResult.bHomeFG == false && pResult.bHomeXP == false &&
                pResult.bHomeXP1 == false && pResult.bHomeXP2 == false && pResult.bHomeXP3 == false &&
                pResult.bHomeSafetyFor == false &&
                pResult.bAwayTD == true && pResult.bAwayFG == false && pResult.bAwayXP == false &&
                pResult.bAwayXP1 == false && pResult.bAwayXP2 == false && pResult.bAwayXP3 == false &&
                pResult.bAwaySafetyFor == false);
        }


        [TestCategory("GameEngine")]
        [TestMethod]
        public void setScoringBool_Home_team_FG()
        {
            Play_Result pResult = new Play_Result()
            { BallPossessing_Team_Id = 11, ht = 11, at = 22, bFGMade = true };

            pResult = GameEngine.setScoringBool(pResult);

            Assert.IsTrue(pResult.bHomeTD == false && pResult.bHomeFG == true && pResult.bHomeXP == false &&
                pResult.bHomeXP1 == false && pResult.bHomeXP2 == false && pResult.bHomeXP3 == false &&
                pResult.bHomeSafetyFor == false &&
                pResult.bAwayTD == false && pResult.bAwayFG == false && pResult.bAwayXP == false &&
                pResult.bAwayXP1 == false && pResult.bAwayXP2 == false && pResult.bAwayXP3 == false &&
                pResult.bAwaySafetyFor == false);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setScoringBool_Away_team_FG()
        {
            Play_Result pResult = new Play_Result()
            { BallPossessing_Team_Id = 22, ht = 11, at = 22, bFGMade = true };

            pResult = GameEngine.setScoringBool(pResult);

            Assert.IsTrue(pResult.bHomeTD == false && pResult.bHomeFG == false && pResult.bHomeXP == false &&
                pResult.bHomeXP1 == false && pResult.bHomeXP2 == false && pResult.bHomeXP3 == false &&
                pResult.bHomeSafetyFor == false &&
                pResult.bAwayTD == false && pResult.bAwayFG == true && pResult.bAwayXP == false &&
                pResult.bAwayXP1 == false && pResult.bAwayXP2 == false && pResult.bAwayXP3 == false &&
                pResult.bAwaySafetyFor == false);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setScoringBool_Home_team_XP()
        {
            Play_Result pResult = new Play_Result()
            { BallPossessing_Team_Id = 11, ht = 11, at = 22, bXPMade = true };

            pResult = GameEngine.setScoringBool(pResult);

            Assert.IsTrue(pResult.bHomeTD == false && pResult.bHomeFG == false && pResult.bHomeXP == true &&
                pResult.bHomeXP1 == false && pResult.bHomeXP2 == false && pResult.bHomeXP3 == false &&
                pResult.bHomeSafetyFor == false &&
                pResult.bAwayTD == false && pResult.bAwayFG == false && pResult.bAwayXP == false &&
                pResult.bAwayXP1 == false && pResult.bAwayXP2 == false && pResult.bAwayXP3 == false &&
                pResult.bAwaySafetyFor == false);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setScoringBool_Away_team_XP()
        {
            Play_Result pResult = new Play_Result()
            { BallPossessing_Team_Id = 22, ht = 11, at = 22, bXPMade = true };

            pResult = GameEngine.setScoringBool(pResult);

            Assert.IsTrue(pResult.bHomeTD == false && pResult.bHomeFG == false && pResult.bHomeXP == false &&
                pResult.bHomeXP1 == false && pResult.bHomeXP2 == false && pResult.bHomeXP3 == false &&
                pResult.bHomeSafetyFor == false &&
                pResult.bAwayTD == false && pResult.bAwayFG == false && pResult.bAwayXP == true &&
                pResult.bAwayXP1 == false && pResult.bAwayXP2 == false && pResult.bAwayXP3 == false &&
                pResult.bAwaySafetyFor == false);
        }


        [TestCategory("GameEngine")]
        [TestMethod]
        public void setScoringBool_Home_team_XP1()
        {
            Play_Result pResult = new Play_Result()
            { BallPossessing_Team_Id = 11, ht = 11, at = 22, bOnePntAfterTDMade = true };

            pResult = GameEngine.setScoringBool(pResult);

            Assert.IsTrue(pResult.bHomeTD == false && pResult.bHomeFG == false && pResult.bHomeXP == false &&
                pResult.bHomeXP1 == true && pResult.bHomeXP2 == false && pResult.bHomeXP3 == false &&
                pResult.bHomeSafetyFor == false &&
                pResult.bAwayTD == false && pResult.bAwayFG == false && pResult.bAwayXP == false &&
                pResult.bAwayXP1 == false && pResult.bAwayXP2 == false && pResult.bAwayXP3 == false &&
                pResult.bAwaySafetyFor == false);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setScoringBool_Away_team_XP1()
        {
            Play_Result pResult = new Play_Result()
            { BallPossessing_Team_Id = 22, ht = 11, at = 22, bOnePntAfterTDMade = true };

            pResult = GameEngine.setScoringBool(pResult);

            Assert.IsTrue(pResult.bHomeTD == false && pResult.bHomeFG == false && pResult.bHomeXP == false &&
                pResult.bHomeXP1 == false && pResult.bHomeXP2 == false && pResult.bHomeXP3 == false &&
                pResult.bHomeSafetyFor == false &&
                pResult.bAwayTD == false && pResult.bAwayFG == false && pResult.bAwayXP == false &&
                pResult.bAwayXP1 == true && pResult.bAwayXP2 == false && pResult.bAwayXP3 == false &&
                pResult.bAwaySafetyFor == false);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setScoringBool_Home_team_XP2()
        {
            Play_Result pResult = new Play_Result()
            { BallPossessing_Team_Id = 11, ht = 11, at = 22, bTwoPntAfterTDMade = true };

            pResult = GameEngine.setScoringBool(pResult);

            Assert.IsTrue(pResult.bHomeTD == false && pResult.bHomeFG == false && pResult.bHomeXP == false &&
                pResult.bHomeXP1 == false && pResult.bHomeXP2 == true && pResult.bHomeXP3 == false &&
                pResult.bHomeSafetyFor == false &&
                pResult.bAwayTD == false && pResult.bAwayFG == false && pResult.bAwayXP == false &&
                pResult.bAwayXP1 == false && pResult.bAwayXP2 == false && pResult.bAwayXP3 == false &&
                pResult.bAwaySafetyFor == false);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setScoringBool_Away_team_XP2()
        {
            Play_Result pResult = new Play_Result()
            { BallPossessing_Team_Id = 22, ht = 11, at = 22, bTwoPntAfterTDMade = true };

            pResult = GameEngine.setScoringBool(pResult);

            Assert.IsTrue(pResult.bHomeTD == false && pResult.bHomeFG == false && pResult.bHomeXP == false &&
                pResult.bHomeXP1 == false && pResult.bHomeXP2 == false && pResult.bHomeXP3 == false &&
                pResult.bHomeSafetyFor == false &&
                pResult.bAwayTD == false && pResult.bAwayFG == false && pResult.bAwayXP == false &&
                pResult.bAwayXP1 == false && pResult.bAwayXP2 == true && pResult.bAwayXP3 == false &&
                pResult.bAwaySafetyFor == false);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setScoringBool_Home_team_XP3()
        {
            Play_Result pResult = new Play_Result()
            { BallPossessing_Team_Id = 11, ht = 11, at = 22, bThreePntAfterTDMade = true };

            pResult = GameEngine.setScoringBool(pResult);

            Assert.IsTrue(pResult.bHomeTD == false && pResult.bHomeFG == false && pResult.bHomeXP == false &&
                pResult.bHomeXP1 == false && pResult.bHomeXP2 == false && pResult.bHomeXP3 == true &&
                pResult.bHomeSafetyFor == false &&
                pResult.bAwayTD == false && pResult.bAwayFG == false && pResult.bAwayXP == false &&
                pResult.bAwayXP1 == false && pResult.bAwayXP2 == false && pResult.bAwayXP3 == false &&
                pResult.bAwaySafetyFor == false);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setScoringBool_Away_team_XP3()
        {
            Play_Result pResult = new Play_Result()
            { BallPossessing_Team_Id = 22, ht = 11, at = 22, bThreePntAfterTDMade = true };

            pResult = GameEngine.setScoringBool(pResult);

            Assert.IsTrue(pResult.bHomeTD == false && pResult.bHomeFG == false && pResult.bHomeXP == false &&
                pResult.bHomeXP1 == false && pResult.bHomeXP2 == false && pResult.bHomeXP3 == false &&
                pResult.bHomeSafetyFor == false &&
                pResult.bAwayTD == false && pResult.bAwayFG == false && pResult.bAwayXP == false &&
                pResult.bAwayXP1 == false && pResult.bAwayXP2 == false && pResult.bAwayXP3 == true &&
                pResult.bAwaySafetyFor == false);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setScoringBool_Home_team_Safety()
        {
            Play_Result pResult = new Play_Result()
            { BallPossessing_Team_Id = 22, ht = 11, at = 22, bSafety = true };

            pResult = GameEngine.setScoringBool(pResult);

            Assert.IsTrue(pResult.bHomeTD == false && pResult.bHomeFG == false && pResult.bHomeXP == false &&
                pResult.bHomeXP1 == false && pResult.bHomeXP2 == false && pResult.bHomeXP3 == false &&
                pResult.bHomeSafetyFor == true &&
                pResult.bAwayTD == false && pResult.bAwayFG == false && pResult.bAwayXP == false &&
                pResult.bAwayXP1 == false && pResult.bAwayXP2 == false && pResult.bAwayXP3 == false &&
                pResult.bAwaySafetyFor == false);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setScoringBool_Away_team_Safety()
        {
            Play_Result pResult = new Play_Result()
            { BallPossessing_Team_Id = 11, ht = 11, at = 22, bSafety = true };

            pResult = GameEngine.setScoringBool(pResult);

            Assert.IsTrue(pResult.bHomeTD == false && pResult.bHomeFG == false && pResult.bHomeXP == false &&
                pResult.bHomeXP1 == false && pResult.bHomeXP2 == false && pResult.bHomeXP3 == false &&
                pResult.bHomeSafetyFor == false &&
                pResult.bAwayTD == false && pResult.bAwayFG == false && pResult.bAwayXP == false &&
                pResult.bAwayXP1 == false && pResult.bAwayXP2 == false && pResult.bAwayXP3 == false &&
                pResult.bAwaySafetyFor == true);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void getForfeit_Message_HomeTeamForfeits()
        {
            string message = GameEngine.getForfeit_Message("Away_Team", "Home_Team_Name", 2,0);
            Assert.IsTrue(message == "The " + "Away_Team" + " have won the game through forfeit, becuase the " + "Home_Team_Name" + " could not field enough players." + Environment.NewLine + Environment.NewLine +
            "The final score is " + "2" + " to " + "0");
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void getForfeit_Message_AwayTeamForfeits()
        {
            string message = GameEngine.getForfeit_Message("Away_Team", "Home_Team_Name", 0, 2);
            Assert.IsTrue(message == "The " + "Home_Team_Name" + " have won the game through forfeit, becuase the " + "Away_Team" + " could not field enough players." + Environment.NewLine + Environment.NewLine +
            "The final score is " + "2" + " to " + "0");
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void UpdateScore_Away_TD_Scores_Q1()
        {
            Game g = new Game()
            {
                Away_Score = 0,
                Home_Score = 0,
                Home_Score_Q1 = 0,
                Away_Score_Q1 = 0,
                Away_Team_Franchise_ID = 11,
                Home_Team_Franchise_ID = 22,
                Time = 200,
                Quarter = 1
            };

            Play_Result pg = new Play_Result()
            {
                at = 22,
                ht = 11,
                bAwayTD = true,
            };

            GameEngine.UpdateScore(pg, g);

            Assert.IsTrue(g.Away_Score == 6 && g.Home_Score == 0 && g.Away_Score_Q1 == 6 &&
                g.Home_Score_Q1 == 0);

        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void UpdateScore_Away_FG_Scores_Q1()
        {
            Game g = new Game()
            {
                Away_Score = 0,
                Home_Score = 0,
                Home_Score_Q1 = 0,
                Away_Score_Q1 = 0,
                Away_Team_Franchise_ID = 11,
                Home_Team_Franchise_ID = 22,
                Time = 200,
                Quarter = 1
            };

            Play_Result pg = new Play_Result()
            {
                at = 22,
                ht = 11,
                bAwayFG = true,
            };

            GameEngine.UpdateScore(pg, g);

            Assert.IsTrue(g.Away_Score == 3 && g.Home_Score == 0 && g.Away_Score_Q1 == 3 &&
                g.Home_Score_Q1 == 0);

        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void UpdateScore_Away_XP_Scores_Q1()
        {
            Game g = new Game()
            {
                Away_Score = 0,
                Home_Score = 0,
                Home_Score_Q1 = 0,
                Away_Score_Q1 = 0,
                Away_Team_Franchise_ID = 11,
                Home_Team_Franchise_ID = 22,
                Time = 200,
                Quarter = 1
            };

            Play_Result pg = new Play_Result()
            {
                at = 22,
                ht = 11,
                bAwayXP = true
            };

            GameEngine.UpdateScore(pg, g);

            Assert.IsTrue(g.Away_Score == 1 && g.Home_Score == 0 && g.Away_Score_Q1 == 1 &&
                g.Home_Score_Q1 == 0);

        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void UpdateScore_Away_Safety_Scores_Q1()
        {
            Game g = new Game()
            {
                Away_Score = 0,
                Home_Score = 0,
                Home_Score_Q1 = 0,
                Away_Score_Q1 = 0,
                Away_Team_Franchise_ID = 11,
                Home_Team_Franchise_ID = 22,
                Time = 200,
                Quarter = 1
            };

            Play_Result pg = new Play_Result()
            {
                at = 22,
                ht = 11,
                bAwaySafetyFor = true
            };

            GameEngine.UpdateScore(pg, g);

            Assert.IsTrue(g.Away_Score == 2 && g.Home_Score == 0 && g.Away_Score_Q1 == 2 &&
                g.Home_Score_Q1 == 0);

        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void UpdateScore_Away_XP1_Q1()
        {
            Game g = new Game()
            {
                Away_Score = 0,
                Home_Score = 0,
                Home_Score_Q1 = 0,
                Away_Score_Q1 = 0,
                Away_Team_Franchise_ID = 11,
                Home_Team_Franchise_ID = 22,
                Time = 200,
                Quarter = 1
            };

            Play_Result pg = new Play_Result()
            {
                at = 22,
                ht = 11,
                bAwayXP1 = true
            };

            GameEngine.UpdateScore(pg, g);

            Assert.IsTrue(g.Away_Score == 1 && g.Home_Score == 0 && g.Away_Score_Q1 == 1 &&
                g.Home_Score_Q1 == 0);

        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void UpdateScore_Away_XP2_Q1()
        {
            Game g = new Game()
            {
                Away_Score = 0,
                Home_Score = 0,
                Home_Score_Q1 = 0,
                Away_Score_Q1 = 0,
                Away_Team_Franchise_ID = 11,
                Home_Team_Franchise_ID = 22,
                Time = 200,
                Quarter = 1
            };

            Play_Result pg = new Play_Result()
            {
                at = 22,
                ht = 11,
                bAwayXP2 = true
            };

            GameEngine.UpdateScore(pg, g);

            Assert.IsTrue(g.Away_Score == 2 && g.Home_Score == 0 && g.Away_Score_Q1 == 2 &&
                g.Home_Score_Q1 == 0);

        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void UpdateScore_Away_XP3_Q1()
        {
            Game g = new Game()
            {
                Away_Score = 0,
                Home_Score = 0,
                Home_Score_Q1 = 0,
                Away_Score_Q1 = 0,
                Away_Team_Franchise_ID = 11,
                Home_Team_Franchise_ID = 22,
                Time = 200,
                Quarter = 1
            };

            Play_Result pg = new Play_Result()
            {
                at = 22,
                ht = 11,
                bAwayXP3 = true
            };

            GameEngine.UpdateScore(pg, g);

            Assert.IsTrue(g.Away_Score == 3 && g.Home_Score == 0 && g.Away_Score_Q1 == 3 &&
                g.Home_Score_Q1 == 0);

        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void UpdateScore_Home_TD_Scores_Q1()
        {
            Game g = new Game()
            {
                Away_Score = 0,
                Home_Score = 0,
                Home_Score_Q1 = 0,
                Away_Score_Q1 = 0,
                Away_Team_Franchise_ID = 11,
                Home_Team_Franchise_ID = 22,
                Time = 200,
                Quarter = 1
            };

            Play_Result pg = new Play_Result()
            {
                at = 22,
                ht = 11,
                bHomeTD = true,
            };

            GameEngine.UpdateScore(pg, g);

            Assert.IsTrue(g.Away_Score == 0 && g.Home_Score == 6 && g.Away_Score_Q1 == 0 &&
                g.Home_Score_Q1 == 6);

        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void UpdateScore_Home_FG_Scores_Q1()
        {
            Game g = new Game()
            {
                Away_Score = 0,
                Home_Score = 0,
                Home_Score_Q1 = 0,
                Away_Score_Q1 = 0,
                Away_Team_Franchise_ID = 11,
                Home_Team_Franchise_ID = 22,
                Time = 200,
                Quarter = 1
            };

            Play_Result pg = new Play_Result()
            {
                at = 22,
                ht = 11,
                bHomeFG = true,
            };

            GameEngine.UpdateScore(pg, g);

            Assert.IsTrue(g.Away_Score == 0 && g.Home_Score == 3 && g.Away_Score_Q1 == 0 &&
                g.Home_Score_Q1 == 3);

        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void UpdateScore_Home_XP_Scores_Q1()
        {
            Game g = new Game()
            {
                Away_Score = 0,
                Home_Score = 0,
                Home_Score_Q1 = 0,
                Away_Score_Q1 = 0,
                Away_Team_Franchise_ID = 11,
                Home_Team_Franchise_ID = 22,
                Time = 200,
                Quarter = 1
            };

            Play_Result pg = new Play_Result()
            {
                at = 22,
                ht = 11,
                bHomeXP = true
            };

            GameEngine.UpdateScore(pg, g);

            Assert.IsTrue(g.Away_Score == 0 && g.Home_Score == 1 && g.Away_Score_Q1 == 0 &&
                g.Home_Score_Q1 == 1);

        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void UpdateScore_Home_Safety_Scores_Q1()
        {
            Game g = new Game()
            {
                Away_Score = 0,
                Home_Score = 0,
                Home_Score_Q1 = 0,
                Away_Score_Q1 = 0,
                Away_Team_Franchise_ID = 11,
                Home_Team_Franchise_ID = 22,
                Time = 200,
                Quarter = 1
            };

            Play_Result pg = new Play_Result()
            {
                at = 22,
                ht = 11,
                bHomeSafetyFor = true
            };

            GameEngine.UpdateScore(pg, g);

            Assert.IsTrue(g.Away_Score == 0 && g.Home_Score == 2 && g.Away_Score_Q1 == 0 &&
                g.Home_Score_Q1 == 2);

        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void UpdateScore_Home_XP1_Q1()
        {
            Game g = new Game()
            {
                Away_Score = 0,
                Home_Score = 0,
                Home_Score_Q1 = 0,
                Away_Score_Q1 = 0,
                Away_Team_Franchise_ID = 11,
                Home_Team_Franchise_ID = 22,
                Time = 200,
                Quarter = 1
            };

            Play_Result pg = new Play_Result()
            {
                at = 22,
                ht = 11,
                bHomeXP1 = true
            };

            GameEngine.UpdateScore(pg, g);

            Assert.IsTrue(g.Away_Score == 0 && g.Home_Score == 1 && g.Away_Score_Q1 == 0 &&
                g.Home_Score_Q1 == 1);

        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void UpdateScore_Home_XP2_Q1()
        {
            Game g = new Game()
            {
                Away_Score = 0,
                Home_Score = 0,
                Home_Score_Q1 = 0,
                Away_Score_Q1 = 0,
                Away_Team_Franchise_ID = 11,
                Home_Team_Franchise_ID = 22,
                Time = 200,
                Quarter = 1
            };

            Play_Result pg = new Play_Result()
            {
                at = 22,
                ht = 11,
                bHomeXP2 = true
            };

            GameEngine.UpdateScore(pg, g);

            Assert.IsTrue(g.Away_Score == 0 && g.Home_Score == 2 && g.Away_Score_Q1 == 0 &&
                g.Home_Score_Q1 == 2);

        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void UpdateScore_Home_XP3_Q1()
        {
            Game g = new Game()
            {
                Away_Score = 0,
                Home_Score = 0,
                Home_Score_Q1 = 0,
                Away_Score_Q1 = 0,
                Away_Team_Franchise_ID = 11,
                Home_Team_Franchise_ID = 22,
                Time = 200,
                Quarter = 1
            };

            Play_Result pg = new Play_Result()
            {
                at = 22,
                ht = 11,
                bHomeXP3 = true
            };

            GameEngine.UpdateScore(pg, g);

            Assert.IsTrue(g.Away_Score == 0 && g.Home_Score == 3 && g.Away_Score_Q1 == 0 &&
                g.Home_Score_Q1 == 3);

        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void UpdateScore_Away_TD_Scores_Q2()
        {
            Game g = new Game()
            {
                Away_Score = 0,
                Home_Score = 0,
                Home_Score_Q2 = 0,
                Away_Score_Q2 = 0,
                Away_Team_Franchise_ID = 11,
                Home_Team_Franchise_ID = 22,
                Time = 200,
                Quarter = 2
            };

            Play_Result pg = new Play_Result()
            {
                at = 22,
                ht = 11,
                bAwayTD = true,
            };

            GameEngine.UpdateScore(pg, g);

            Assert.IsTrue(g.Away_Score == 6 && g.Home_Score == 0 && g.Away_Score_Q2 == 6 &&
                g.Home_Score_Q2 == 0);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void UpdateScore_Home_TD_Scores_Q2()
        {
            Game g = new Game()
            {
                Away_Score = 0,
                Home_Score = 0,
                Home_Score_Q2 = 0,
                Away_Score_Q2 = 0,
                Away_Team_Franchise_ID = 11,
                Home_Team_Franchise_ID = 22,
                Time = 200,
                Quarter = 2
            };

            Play_Result pg = new Play_Result()
            {
                at = 22,
                ht = 11,
                bHomeTD = true,
            };

            GameEngine.UpdateScore(pg, g);

            Assert.IsTrue(g.Away_Score == 0 && g.Home_Score == 6 && g.Away_Score_Q2 == 0 &&
                g.Home_Score_Q2 == 6);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void UpdateScore_Away_TD_Scores_Q3()
        {
            Game g = new Game()
            {
                Away_Score = 0,
                Home_Score = 0,
                Home_Score_Q3 = 0,
                Away_Score_Q3 = 0,
                Away_Team_Franchise_ID = 11,
                Home_Team_Franchise_ID = 22,
                Time = 200,
                Quarter = 3
            };

            Play_Result pg = new Play_Result()
            {
                at = 22,
                ht = 11,
                bAwayTD = true,
            };

            GameEngine.UpdateScore(pg, g);

            Assert.IsTrue(g.Away_Score == 6 && g.Home_Score == 0 && g.Away_Score_Q3 == 6 &&
                g.Home_Score_Q3 == 0);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void UpdateScore_Home_TD_Scores_Q3()
        {
            Game g = new Game()
            {
                Away_Score = 0,
                Home_Score = 0,
                Home_Score_Q3 = 0,
                Away_Score_Q3 = 0,
                Away_Team_Franchise_ID = 11,
                Home_Team_Franchise_ID = 22,
                Time = 200,
                Quarter = 3
            };

            Play_Result pg = new Play_Result()
            {
                at = 22,
                ht = 11,
                bHomeTD = true,
            };

            GameEngine.UpdateScore(pg, g);

            Assert.IsTrue(g.Away_Score == 0 && g.Home_Score == 6 && g.Away_Score_Q3 == 0 &&
                g.Home_Score_Q3 == 6);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void UpdateScore_Away_TD_Scores_Q4()
        {
            Game g = new Game()
            {
                Away_Score = 0,
                Home_Score = 0,
                Home_Score_Q4 = 0,
                Away_Score_Q4 = 0,
                Away_Team_Franchise_ID = 11,
                Home_Team_Franchise_ID = 22,
                Time = 200,
                Quarter = 4
            };

            Play_Result pg = new Play_Result()
            {
                at = 22,
                ht = 11,
                bAwayTD = true,
            };

            GameEngine.UpdateScore(pg, g);

            Assert.IsTrue(g.Away_Score == 6 && g.Home_Score == 0 && g.Away_Score_Q4 == 6 &&
                g.Home_Score_Q4 == 0);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void UpdateScore_Home_TD_Scores_Q4()
        {
            Game g = new Game()
            {
                Away_Score = 0,
                Home_Score = 0,
                Home_Score_Q4 = 0,
                Away_Score_Q4 = 0,
                Away_Team_Franchise_ID = 11,
                Home_Team_Franchise_ID = 22,
                Time = 200,
                Quarter = 4
            };

            Play_Result pg = new Play_Result()
            {
                at = 22,
                ht = 11,
                bHomeTD = true,
            };

            GameEngine.UpdateScore(pg, g);

            Assert.IsTrue(g.Away_Score == 0 && g.Home_Score == 6 && g.Away_Score_Q4 == 0 &&
                g.Home_Score_Q4 == 6);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void UpdateScore_Away_TD_Scores_OT()
        {
            Game g = new Game()
            {
                Away_Score = 0,
                Home_Score = 0,
                Home_Score_OT = 0,
                Away_Score_OT = 0,
                Away_Team_Franchise_ID = 11,
                Home_Team_Franchise_ID = 22,
                Time = 200,
                Quarter = 5
            };

            Play_Result pg = new Play_Result()
            {
                at = 22,
                ht = 11,
                bAwayTD = true,
            };

            GameEngine.UpdateScore(pg, g);

            Assert.IsTrue(g.Away_Score == 6 && g.Home_Score == 0 && g.Away_Score_OT == 6 &&
                g.Home_Score_OT == 0);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void UpdateScore_Home_TD_Scores_OT()
        {
            Game g = new Game()
            {
                Away_Score = 0,
                Home_Score = 0,
                Home_Score_OT = 0,
                Away_Score_OT = 0,
                Away_Team_Franchise_ID = 11,
                Home_Team_Franchise_ID = 22,
                Time = 200,
                Quarter = 5
            };

            Play_Result pg = new Play_Result()
            {
                at = 22,
                ht = 11,
                bHomeTD = true,
            };

            GameEngine.UpdateScore(pg, g);

            Assert.IsTrue(g.Away_Score == 0 && g.Home_Score == 6 && g.Away_Score_OT == 0 &&
                g.Home_Score_OT == 6);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Kickoff_Left_ReturntoOwn20_no_Penalty_to_20()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.KICKOFF_NORMAL;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player Returner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 20
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = null;
            Game_Player Penalized_Player = null;

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                Yards_Returned = 20.0,
                bTouchback = false,
                bTouchDown = false,
                end_of_play_yardline = 20.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Returner = Returner,
                bPenalty_Rejected = false
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats() { 
                Player_ID = Returner.p_and_r.p.ID,
                ko_ret = 1,
                ko_ret_plays = 1,
                ko_ret_yards = 20,
                ko_ret_yards_long = 20,
                ko_ret_TDs = 0,
                ko_ret_fumbles = 0,
                ko_ret_fumbles_lost = 0} );

            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult,  bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (!r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 20.0)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 0)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Returner.p_and_r.p.ID).First();
            if (a_stat.ko_ret_yards != 20  || a_stat.ko_ret_yards_long != 20 && 
                a_stat.ko_ret_TDs != 0 || a_stat.ko_ret_fumbles != 0 | a_stat.ko_ret_fumbles_lost != 0)
                throw new Exception("Player stats not as expected");

            Assert.IsTrue(true);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Kickoff_Left_Touchback_to_25()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.KICKOFF_NORMAL;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player Returner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = -5.0
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = null;
            Game_Player Penalized_Player = null;

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                Yards_Returned = 0.0,
                bTouchback = true,
                bTouchDown = false,
                end_of_play_yardline = 0.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Returner = Returner,
                bPenalty_Rejected = false
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = Returner.p_and_r.p.ID,
                ko_ret = 0,
                ko_ret_plays = 1,
                ko_ret_yards = 0,
                ko_ret_yards_long = 0,
                ko_ret_TDs = 0,
                ko_ret_fumbles = 0,
                ko_ret_fumbles_lost = 0
            });

            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult,  bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (!r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 75.0)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 0)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Returner.p_and_r.p.ID).First();
            if (a_stat.ko_ret_yards != 0 || a_stat.ko_ret_yards_long != 0 &&
                a_stat.ko_ret_TDs != 0 || a_stat.ko_ret_fumbles != 0 | a_stat.ko_ret_fumbles_lost != 0)
                throw new Exception("Player stats not as expected");

            Assert.IsTrue(true);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Kickoff_Left_Touchdown_NextPlay_XP()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.KICKOFF_NORMAL;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player Returner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 105
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = null;
            Game_Player Penalized_Player = null;

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                Yards_Returned = 100.0,
                bTouchback = false,
                bTouchDown = true,
                end_of_play_yardline = 105.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Returner = Returner,
                bPenalty_Rejected = false
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = Returner.p_and_r.p.ID,
                ko_ret = 1,
                ko_ret_plays = 1,
                ko_ret_yards = 100,
                ko_ret_yards_long = 100,
                ko_ret_TDs = 1,
                ko_ret_fumbles = 0,
                ko_ret_fumbles_lost = 0
            });

            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult,  bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (r.bFinal_SwitchPossession)
                throw new Exception("Possession should not switch as the team must kick an XP next");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 0 || r.Final_yard_to_go != 0)
                throw new Exception("The next Play will be an XP");

            //Check for next play
            if (!r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("Next play must be an XP");

            if (r.Final_Added_Penalty_Yards != 0)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                !r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("Away Team Must Score a TD");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Returner.p_and_r.p.ID).First();
            if (a_stat.ko_ret_yards != 100 || a_stat.ko_ret_yards_long != 100 &&
                a_stat.ko_ret_TDs != 1 || a_stat.ko_ret_fumbles != 0 | a_stat.ko_ret_fumbles_lost != 0)
                throw new Exception("Player stats not as expected");

            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Kickoff_Left_Touchdown_NextPlay_XP_Ignore_Penalty()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.KICKOFF_NORMAL;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player Returner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 105
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.UR).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                Current_YardLine = 25.0
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                Yards_Returned = 100.0,
                bTouchback = false,
                bTouchDown = true,
                end_of_play_yardline = 105.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Returner = Returner,
                bPenalty_Rejected = false
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = Returner.p_and_r.p.ID,
                ko_ret = 1,
                ko_ret_plays = 1,
                ko_ret_yards = 100,
                ko_ret_yards_long = 100,
                ko_ret_TDs = 1,
                ko_ret_fumbles = 0,
                ko_ret_fumbles_lost = 0
            });

            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult,  bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (r.bFinal_SwitchPossession)
                throw new Exception("Possession should not switch as the team must kick an XP next");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 0 || r.Final_yard_to_go != 0)
                throw new Exception("The next Play will be an XP");

            //Check for next play
            if (!r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("Next play must be an XP");

            if (r.Final_Added_Penalty_Yards != 0 || !r.bIgnorePenalty)
                throw new Exception("Ignore the penalty for this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                !r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("Away Team Must Score a TD");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Returner.p_and_r.p.ID).First();
            if (a_stat.ko_ret_yards != 100 || a_stat.ko_ret_yards_long != 100 &&
                a_stat.ko_ret_TDs != 1 || a_stat.ko_ret_fumbles != 0 | a_stat.ko_ret_fumbles_lost != 0)
                throw new Exception("Player stats not as expected");

            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Kickoff_Left_ReturntoOwn20_Spot_Penalty_on_Defenders_at_25_to_40()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.KICKOFF_NORMAL;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player Returner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 20
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.FM).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                Current_YardLine = 25.0
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                Yards_Returned = 20.0,
                bTouchback = false,
                bTouchDown = false,
                end_of_play_yardline = 20.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Returner = Returner,
                bPenalty_Rejected = false
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = Returner.p_and_r.p.ID,
                ko_ret = 1,
                ko_ret_plays = 1,
                ko_ret_yards = 20,
                ko_ret_yards_long = 20,
                ko_ret_TDs = 0,
                ko_ret_fumbles = 0,
                ko_ret_fumbles_lost = 0
            });

            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult,  bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (!r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 40.0)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 15)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");
 
            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Returner.p_and_r.p.ID).First();
            if (a_stat.ko_ret_yards != 20 || a_stat.ko_ret_yards_long != 20 &&
                a_stat.ko_ret_TDs != 0 || a_stat.ko_ret_fumbles != 0 | a_stat.ko_ret_fumbles_lost != 0)
                throw new Exception("Player stats not as expected");

            Assert.IsTrue(true);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Kickoff_Left_ReturntoOwn40_Spot_Penalty_on_Defenders_to_30()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.KICKOFF_NORMAL;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player Returner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 40
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.FM).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                Current_YardLine = 30.0
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                Yards_Returned = 40.0,
                bTouchback = false,
                bTouchDown = false,
                end_of_play_yardline = 40.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Returner = Returner,
                bPenalty_Rejected = false
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = Returner.p_and_r.p.ID,
                ko_ret = 1,
                ko_ret_plays = 1,
                ko_ret_yards = 40,
                ko_ret_yards_long = 40,
                ko_ret_TDs = 0,
                ko_ret_fumbles = 0,
                ko_ret_fumbles_lost = 0
            });

            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult,  bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (!r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 55.0)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 15)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Returner.p_and_r.p.ID).First();
            if (a_stat.ko_ret_yards != 40 || a_stat.ko_ret_yards_long != 40 &&
                a_stat.ko_ret_TDs != 0 || a_stat.ko_ret_fumbles != 0 | a_stat.ko_ret_fumbles_lost != 0)
                throw new Exception("Player stats not as expected");

            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Kickoff_Left_ReturntoOwn07_Spot_Penalty_on_Defenders_to_08()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.KICKOFF_NORMAL;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player Returner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 7
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.FM).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                Current_YardLine = 8.0
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                Yards_Returned = 7.0,
                bTouchback = false,
                bTouchDown = false,
                end_of_play_yardline = 7.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Returner = Returner,
                bPenalty_Rejected = false
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = Returner.p_and_r.p.ID,
                ko_ret = 1,
                ko_ret_plays = 1,
                ko_ret_yards = 7,
                ko_ret_yards_long = 7,
                ko_ret_TDs = 0,
                ko_ret_fumbles = 0,
                ko_ret_fumbles_lost = 0
            });

            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult,  bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (!r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 23.0)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 15)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Returner.p_and_r.p.ID).First();
            if (a_stat.ko_ret_yards != 7 || a_stat.ko_ret_yards_long != 7 &&
                a_stat.ko_ret_TDs != 0 || a_stat.ko_ret_fumbles != 0 | a_stat.ko_ret_fumbles_lost != 0)
                throw new Exception("Player stats not as expected");

            Assert.IsTrue(true);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Kickoff_Left_ReturntoOwn91_Spot_Penalty_on_Defenders_to_92()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.KICKOFF_NORMAL;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player Returner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 91
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.FM).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                Current_YardLine = 92.0
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                Yards_Returned = 91.0,
                bTouchback = false,
                bTouchDown = false,
                end_of_play_yardline = 91.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Returner = Returner,
                bPenalty_Rejected = false
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = Returner.p_and_r.p.ID,
                ko_ret = 1,
                ko_ret_plays = 1,
                ko_ret_yards = 91,
                ko_ret_yards_long = 91,
                ko_ret_TDs = 0,
                ko_ret_fumbles = 0,
                ko_ret_fumbles_lost = 0
            });

            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult,  bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (!r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 96.0)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 4)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Returner.p_and_r.p.ID).First();
            if (a_stat.ko_ret_yards != 91 || a_stat.ko_ret_yards_long != 91 &&
                a_stat.ko_ret_TDs != 0 || a_stat.ko_ret_fumbles != 0 | a_stat.ko_ret_fumbles_lost != 0)
                throw new Exception("Player stats not as expected");

            Assert.IsTrue(true);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Kickoff_Left_ReturntoOwn92_Spot_Penalty_on_Defenders_to_91()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.KICKOFF_NORMAL;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player Returner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 92
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.FM).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                Current_YardLine = 92.0
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                Yards_Returned = 92.0,
                bTouchback = false,
                bTouchDown = false,
                end_of_play_yardline = 92.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Returner = Returner,
                bPenalty_Rejected = false
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = Returner.p_and_r.p.ID,
                ko_ret = 1,
                ko_ret_plays = 1,
                ko_ret_yards = 92,
                ko_ret_yards_long = 92,
                ko_ret_TDs = 0,
                ko_ret_fumbles = 0,
                ko_ret_fumbles_lost = 0
            });

            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult, bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (!r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 96.0)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 4)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Returner.p_and_r.p.ID).First();
            if (a_stat.ko_ret_yards != 92 || a_stat.ko_ret_yards_long != 92 &&
                a_stat.ko_ret_TDs != 0 || a_stat.ko_ret_fumbles != 0 | a_stat.ko_ret_fumbles_lost != 0)
                throw new Exception("Player stats not as expected");

            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Kickoff_Left_ReturntoOwn30_Spot_Penalty_on_Returners_at_25()
        {
            bool penOnBallCarryingTeam = true;
            Play_Enum PE = Play_Enum.KICKOFF_NORMAL;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player Returner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 30
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.FM).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                Current_YardLine = 25.0
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                Yards_Returned = 30.0,
                bTouchback = false,
                bTouchDown = false,
                end_of_play_yardline = 30.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Returner = Returner,
                bPenalty_Rejected = false
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = Returner.p_and_r.p.ID,
                ko_ret = 1,
                ko_ret_plays = 1,
                ko_ret_yards = 30,
                ko_ret_yards_long = 30,
                ko_ret_TDs = 0,
                ko_ret_fumbles = 0,
                ko_ret_fumbles_lost = 0
            });

            double original_Yardline = 0.0;
            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult,  bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (!r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 12.5)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 12.5)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Returner.p_and_r.p.ID).First();
            if (a_stat.ko_ret_yards != 25 || a_stat.ko_ret_yards_long != 25 &&
                a_stat.ko_ret_TDs != 0 || a_stat.ko_ret_fumbles != 0 | a_stat.ko_ret_fumbles_lost != 0)
                throw new Exception("Player stats not as expected");

            Assert.IsTrue(true);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Kickoff_Left_ReturntoOwn30_Spot_Penalty_on_Returners_at_35()
        {
            bool penOnBallCarryingTeam = true;
            Play_Enum PE = Play_Enum.KICKOFF_NORMAL;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player Returner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 30
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.FM).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                Current_YardLine = 35.0
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                Yards_Returned = 30.0,
                bTouchback = false,
                bTouchDown = false,
                end_of_play_yardline = 30.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Returner = Returner,
                bPenalty_Rejected = false
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = Returner.p_and_r.p.ID,
                ko_ret = 1,
                ko_ret_plays = 1,
                ko_ret_yards = 30,
                ko_ret_yards_long = 30,
                ko_ret_TDs = 0,
                ko_ret_fumbles = 0,
                ko_ret_fumbles_lost = 0
            });

            double original_Yardline = 0.0;
            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult, bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (!r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 15.0)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 15.0)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Returner.p_and_r.p.ID).First();
            if (a_stat.ko_ret_yards != 30 || a_stat.ko_ret_yards_long != 30 &&
                a_stat.ko_ret_TDs != 0 || a_stat.ko_ret_fumbles != 0 | a_stat.ko_ret_fumbles_lost != 0)
                throw new Exception("Player stats not as expected");

            Assert.IsTrue(true);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Kickoff_Left_ReturntoOwn92_Spot_Penalty_on_Returners_at_92()
        {
            bool penOnBallCarryingTeam = true;
            Play_Enum PE = Play_Enum.KICKOFF_NORMAL;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player Returner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 92
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.FM).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                Current_YardLine = 92.0
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                Yards_Returned = 92.0,
                bTouchback = false,
                bTouchDown = false,
                end_of_play_yardline = 92.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Returner = Returner,
                bPenalty_Rejected = false
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = Returner.p_and_r.p.ID,
                ko_ret = 1,
                ko_ret_plays = 1,
                ko_ret_yards = 92,
                ko_ret_yards_long = 92,
                ko_ret_TDs = 0,
                ko_ret_fumbles = 0,
                ko_ret_fumbles_lost = 0
            });

            double original_Yardline = 0.0;
            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult, bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (!r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must not stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 77.0)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 15.0)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Returner.p_and_r.p.ID).First();
            if (a_stat.ko_ret_yards != 92 || a_stat.ko_ret_yards_long != 92 &&
                a_stat.ko_ret_TDs != 0 || a_stat.ko_ret_fumbles != 0 | a_stat.ko_ret_fumbles_lost != 0)
                throw new Exception("Player stats not as expected");

            Assert.IsTrue(true);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Kickoff_Left_ReturnTD_Spot_Penalty_on_Returners_at_50()
        {
            bool penOnBallCarryingTeam = true;
            Play_Enum PE = Play_Enum.KICKOFF_NORMAL;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player Returner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 105
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.FM).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                Current_YardLine = 50.0
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                Yards_Returned = 100.0,
                bTouchback = false,
                bTouchDown = true,
                end_of_play_yardline = 105.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Returner = Returner,
                bPenalty_Rejected = false
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = Returner.p_and_r.p.ID,
                ko_ret = 1,
                ko_ret_plays = 1,
                ko_ret_yards = 100,
                ko_ret_yards_long = 100,
                ko_ret_TDs = 1,
                ko_ret_fumbles = 0,
                ko_ret_fumbles_lost = 0
            });

            double original_Yardline = 0.0;
            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult,  bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (!r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must not stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 35.0)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 15.0)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Returner.p_and_r.p.ID).First();
            if (a_stat.ko_ret_yards != 50 || a_stat.ko_ret_yards_long != 50 &&
                a_stat.ko_ret_TDs != 0 || a_stat.ko_ret_fumbles != 0 | a_stat.ko_ret_fumbles_lost != 0)
                throw new Exception("Player stats not as expected");

            Assert.IsTrue(true);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Kickoff_Left_Return04_Spot_Penalty_on_Returners_at_5()
        {
            bool penOnBallCarryingTeam = true;
            Play_Enum PE = Play_Enum.KICKOFF_NORMAL;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player Returner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 4.0
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.FM).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                Current_YardLine = 5.0
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                Yards_Returned = 4.0,
                bTouchback = false,
                bTouchDown = false,
                end_of_play_yardline = 4.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Returner = Returner,
                bPenalty_Rejected = false
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = Returner.p_and_r.p.ID,
                ko_ret = 1,
                ko_ret_plays = 1,
                ko_ret_yards = 4,
                ko_ret_yards_long = 4,
                ko_ret_TDs = 0,
                ko_ret_fumbles = 0,
                ko_ret_fumbles_lost = 0
            });

            double original_Yardline = 0.0;
            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult,  bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (!r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must not stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 2.0)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 2.0)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Returner.p_and_r.p.ID).First();
            if (a_stat.ko_ret_yards != 4 || a_stat.ko_ret_yards_long != 4 &&
                a_stat.ko_ret_TDs != 0 || a_stat.ko_ret_fumbles != 0 | a_stat.ko_ret_fumbles_lost != 0)
                throw new Exception("Player stats not as expected");

            Assert.IsTrue(true);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Kickoff_Left_Return05_Spot_Penalty_on_Returners_at_4()
        {
            bool penOnBallCarryingTeam = true;
            Play_Enum PE = Play_Enum.KICKOFF_NORMAL;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player Returner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 5.0
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.FM).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                Current_YardLine = 4.0
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                Yards_Returned = 5.0,
                bTouchback = false,
                bTouchDown = false,
                end_of_play_yardline = 5.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Returner = Returner,
                bPenalty_Rejected = false
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = Returner.p_and_r.p.ID,
                ko_ret = 1,
                ko_ret_plays = 1,
                ko_ret_yards = 5,
                ko_ret_yards_long = 5,
                ko_ret_TDs = 0,
                ko_ret_fumbles = 0,
                ko_ret_fumbles_lost = 0
            });

            double original_Yardline = 0.0;
            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult,  bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (!r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must not stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 2.0)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 2.0)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Returner.p_and_r.p.ID).First();
            if (a_stat.ko_ret_yards != 4 || a_stat.ko_ret_yards_long != 4 &&
                a_stat.ko_ret_TDs != 0 || a_stat.ko_ret_fumbles != 0 | a_stat.ko_ret_fumbles_lost != 0)
                throw new Exception("Player stats not as expected");

            Assert.IsTrue(true);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Kickoff_Left_Return_Fumbles_on_20_and_Recovers()
        {
            bool penOnBallCarryingTeam = true;
            Play_Enum PE = Play_Enum.KICKOFF_NORMAL;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player Returner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 20
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = null;
            Game_Player Penalized_Player = null;

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                Yards_Returned = 20.0,
                bTouchback = false,
                bTouchDown = false,
                end_of_play_yardline = 20.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                bPenalty_Rejected = false,
                Returner = Returner,
                bFumble = true
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = Returner.p_and_r.p.ID,
                ko_ret = 1,
                ko_ret_plays = 1,
                ko_ret_yards = 20,
                ko_ret_yards_long = 20,
                ko_ret_TDs = 0,
                ko_ret_fumbles = 1,
                ko_ret_fumbles_lost = 0
            });

            double original_Yardline = 0.0;
            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult,  bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (!r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must not stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 20.0)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 0.0)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Returner.p_and_r.p.ID).First();
            if (a_stat.ko_ret_yards != 20 || a_stat.ko_ret_yards_long != 20 &&
                a_stat.ko_ret_TDs != 0 || a_stat.ko_ret_fumbles != 1 | a_stat.ko_ret_fumbles_lost != 0)
                throw new Exception("Player stats not as expected");

            Assert.IsTrue(true);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Kickoff_Left_Return_lost_Fumbles_on_20_penalty_on_retuners()
        {
            bool penOnBallCarryingTeam = true;
            Play_Enum PE = Play_Enum.KICKOFF_NORMAL;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player Returner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 20
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.FM).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                Current_YardLine = 15.0
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                Yards_Returned = 20.0,
                bTouchback = false,
                bTouchDown = false,
                end_of_play_yardline = 20.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Returner = Returner,
                bPenalty_Rejected = false,
                bFumble = true,
                bFumble_Lost = true
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = Returner.p_and_r.p.ID,
                ko_ret = 1,
                ko_ret_plays = 1,
                ko_ret_yards = 20,
                ko_ret_yards_long = 20,
                ko_ret_TDs = 0,
                ko_ret_fumbles = 1,
                ko_ret_fumbles_lost = 1
            });

            double original_Yardline = 0.0;
            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult,  bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must not stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 7.5)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 7.5)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 1 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Returner.p_and_r.p.ID).First();
            if (a_stat.ko_ret_yards != 15 || a_stat.ko_ret_yards_long != 15 &&
                a_stat.ko_ret_TDs != 0 || a_stat.ko_ret_fumbles != 1 | a_stat.ko_ret_fumbles_lost != 1)
                throw new Exception("Player stats not as expected");

            Assert.IsTrue(true);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Kickoff_Left_Return_lost_Fumbles_on_20_penalty_on_defenders()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.KICKOFF_NORMAL;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player Returner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 20
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.FM).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                Current_YardLine = 15.0
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                Yards_Returned = 20.0,
                bTouchback = false,
                bTouchDown = false,
                end_of_play_yardline = 20.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Returner = Returner,
                bPenalty_Rejected = false,
                bFumble = true,
                bFumble_Lost = true
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = Returner.p_and_r.p.ID,
                ko_ret = 1,
                ko_ret_plays = 1,
                ko_ret_yards = 20,
                ko_ret_yards_long = 20,
                ko_ret_TDs = 0,
                ko_ret_fumbles = 1,
                ko_ret_fumbles_lost = 1
            });

            double original_Yardline = 0.0;
            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult,  bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must not stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 35)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 15)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 1 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Returner.p_and_r.p.ID).First();
            if (a_stat.ko_ret_yards != 20 || a_stat.ko_ret_yards_long != 20 &&
                a_stat.ko_ret_TDs != 0 || a_stat.ko_ret_fumbles != 1 | a_stat.ko_ret_fumbles_lost != 1)
                throw new Exception("Player stats not as expected");

            Assert.IsTrue(true);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Kickoff_Left_ReturntoOwn30_KIB_Spot_Penalty_on_Returners_at_25()
        {
            bool penOnBallCarryingTeam = true;
            Play_Enum PE = Play_Enum.KICKOFF_NORMAL;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player Returner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 30
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.KIB).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                Current_YardLine = 25.0
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                Yards_Returned = 30.0,
                bTouchback = false,
                bTouchDown = false,
                end_of_play_yardline = 30.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Returner = Returner,
                bPenalty_Rejected = false
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = Returner.p_and_r.p.ID,
                ko_ret = 1,
                ko_ret_plays = 1,
                ko_ret_yards = 30,
                ko_ret_yards_long = 30,
                ko_ret_TDs = 0,
                ko_ret_fumbles = 0,
                ko_ret_fumbles_lost = 0
            });

            double original_Yardline = 0.0;
            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult,  bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (!r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 12.5)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 12.5)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Returner.p_and_r.p.ID).First();
            if (a_stat.ko_ret_yards != 25 || a_stat.ko_ret_yards_long != 25 &&
                a_stat.ko_ret_TDs != 0 || a_stat.ko_ret_fumbles != 0 | a_stat.ko_ret_fumbles_lost != 0)
                throw new Exception("Player stats not as expected");

            Assert.IsTrue(true);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Kickoff_Left_ReturntoOwn30_KIB_Spot_Penalty_on_Returners_at_35()
        {
            bool penOnBallCarryingTeam = true;
            Play_Enum PE = Play_Enum.KICKOFF_NORMAL;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player Returner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 30
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.KIB).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                Current_YardLine = 35.0
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                Yards_Returned = 30.0,
                bTouchback = false,
                bTouchDown = false,
                end_of_play_yardline = 30.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Returner = Returner,
                bPenalty_Rejected = false
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = Returner.p_and_r.p.ID,
                ko_ret = 1,
                ko_ret_plays = 1,
                ko_ret_yards = 30,
                ko_ret_yards_long = 30,
                ko_ret_TDs = 0,
                ko_ret_fumbles = 0,
                ko_ret_fumbles_lost = 0
            });

            double original_Yardline = 0.0;
            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult,  bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (!r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 15.0)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 15.0)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Returner.p_and_r.p.ID).First();
            if (a_stat.ko_ret_yards != 30 || a_stat.ko_ret_yards_long != 30 &&
                a_stat.ko_ret_TDs != 0 || a_stat.ko_ret_fumbles != 0 | a_stat.ko_ret_fumbles_lost != 0)
                throw new Exception("Player stats not as expected");

            Assert.IsTrue(true);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Kickoff_Left_ReturntoOwn92_KIB_Spot_Penalty_on_Returners_at_92()
        {
            bool penOnBallCarryingTeam = true;
            Play_Enum PE = Play_Enum.KICKOFF_NORMAL;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player Returner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 92
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.KIB).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                Current_YardLine = 92.0
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                Yards_Returned = 92.0,
                bTouchback = false,
                bTouchDown = false,
                end_of_play_yardline = 92.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Returner = Returner,
                bPenalty_Rejected = false
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = Returner.p_and_r.p.ID,
                ko_ret = 1,
                ko_ret_plays = 1,
                ko_ret_yards = 92,
                ko_ret_yards_long = 92,
                ko_ret_TDs = 0,
                ko_ret_fumbles = 0,
                ko_ret_fumbles_lost = 0
            });

            double original_Yardline = 0.0;
            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult,  bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (!r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must not stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 77.0)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 15.0)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Returner.p_and_r.p.ID).First();
            if (a_stat.ko_ret_yards != 92 || a_stat.ko_ret_yards_long != 92 &&
                a_stat.ko_ret_TDs != 0 || a_stat.ko_ret_fumbles != 0 | a_stat.ko_ret_fumbles_lost != 0)
                throw new Exception("Player stats not as expected");

            Assert.IsTrue(true);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Kickoff_Left_ReturnTD_KIB_Spot_Penalty_on_Returners_at_50()
        {
            bool penOnBallCarryingTeam = true;
            Play_Enum PE = Play_Enum.KICKOFF_NORMAL;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player Returner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 105
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.KIB).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                Current_YardLine = 50.0
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                Yards_Returned = 100.0,
                bTouchback = false,
                bTouchDown = true,
                end_of_play_yardline = 105.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Returner = Returner,
                bPenalty_Rejected = false
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = Returner.p_and_r.p.ID,
                ko_ret = 1,
                ko_ret_plays = 1,
                ko_ret_yards = 100,
                ko_ret_yards_long = 100,
                ko_ret_TDs = 1,
                ko_ret_fumbles = 0,
                ko_ret_fumbles_lost = 0
            });

            double original_Yardline = 0.0;
            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult,  bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (!r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must not stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 35.0)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 15.0)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Returner.p_and_r.p.ID).First();
            if (a_stat.ko_ret_yards != 50 || a_stat.ko_ret_yards_long != 50 &&
                a_stat.ko_ret_TDs != 0 || a_stat.ko_ret_fumbles != 0 | a_stat.ko_ret_fumbles_lost != 0)
                throw new Exception("Player stats not as expected");

            Assert.IsTrue(true);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Kickoff_Left_Return04_KIB_Spot_Penalty_on_Returners_at_5()
        {
            bool penOnBallCarryingTeam = true;
            Play_Enum PE = Play_Enum.KICKOFF_NORMAL;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player Returner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 4.0
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.KIB).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                Current_YardLine = 5.0
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                Yards_Returned = 4.0,
                bTouchback = false,
                bTouchDown = false,
                end_of_play_yardline = 4.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Returner = Returner,
                bPenalty_Rejected = false
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = Returner.p_and_r.p.ID,
                ko_ret = 1,
                ko_ret_plays = 1,
                ko_ret_yards = 4,
                ko_ret_yards_long = 4,
                ko_ret_TDs = 0,
                ko_ret_fumbles = 0,
                ko_ret_fumbles_lost = 0
            });

            double original_Yardline = 0.0;
            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult,  bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (!r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must not stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 2.0)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 2.0)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Returner.p_and_r.p.ID).First();
            if (a_stat.ko_ret_yards != 4 || a_stat.ko_ret_yards_long != 4 &&
                a_stat.ko_ret_TDs != 0 || a_stat.ko_ret_fumbles != 0 | a_stat.ko_ret_fumbles_lost != 0)
                throw new Exception("Player stats not as expected");

            Assert.IsTrue(true);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Kickoff_Left_Return05_KIB_Spot_Penalty_on_Returners_at_4()
        {
            bool penOnBallCarryingTeam = true;
            Play_Enum PE = Play_Enum.KICKOFF_NORMAL;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player Returner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 5.0
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.KIB).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                Current_YardLine = 4.0
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                Yards_Returned = 5.0,
                bTouchback = false,
                bTouchDown = false,
                end_of_play_yardline = 5.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Returner = Returner,
                bPenalty_Rejected = false
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = Returner.p_and_r.p.ID,
                ko_ret = 1,
                ko_ret_plays = 1,
                ko_ret_yards = 5,
                ko_ret_yards_long = 5,
                ko_ret_TDs = 0,
                ko_ret_fumbles = 0,
                ko_ret_fumbles_lost = 0
            });

            double original_Yardline = 0.0;
            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult,  bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (!r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must not stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 2.0)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 2.0)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Returner.p_and_r.p.ID).First();
            if (a_stat.ko_ret_yards != 4 || a_stat.ko_ret_yards_long != 4 &&
                a_stat.ko_ret_TDs != 0 || a_stat.ko_ret_fumbles != 0 | a_stat.ko_ret_fumbles_lost != 0)
                throw new Exception("Player stats not as expected");

            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Kickoff_Left_Return05_KIB_Spot_Penalty_on_Returners_declined_at_4()
        {
            bool penOnBallCarryingTeam = true;
            Play_Enum PE = Play_Enum.KICKOFF_NORMAL;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player Returner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 5.0
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.KIB).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                Current_YardLine = 4.0
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                Yards_Returned = 5.0,
                bTouchback = false,
                bTouchDown = false,
                end_of_play_yardline = 5.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Returner = Returner,
                bPenalty_Rejected = true,
                bFumble = true,
                bFumble_Lost = true     
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = Returner.p_and_r.p.ID,
                ko_ret = 1,
                ko_ret_plays = 1,
                ko_ret_yards = 5,
                ko_ret_yards_long = 5,
                ko_ret_TDs = 0,
                ko_ret_fumbles = 1,
                ko_ret_fumbles_lost = 1
            });

            double original_Yardline = 0.0;
            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult,  bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must not stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 5.0)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 0.0)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 1 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Returner.p_and_r.p.ID).First();
            if (a_stat.ko_ret_yards != 5 || a_stat.ko_ret_yards_long != 5 &&
                a_stat.ko_ret_TDs != 1 || a_stat.ko_ret_fumbles != 1 | a_stat.ko_ret_fumbles_lost != 1)
                throw new Exception("Player stats not as expected");

            Assert.IsTrue(true);
        }


        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_FREE_KICK_Left_ReturntoOwn20_no_Penalty_to_20()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.FREE_KICK;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player Returner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 20
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = null;
            Game_Player Penalized_Player = null;

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                Yards_Returned = 20.0,
                bTouchback = false,
                bTouchDown = false,
                end_of_play_yardline = 20.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Returner = Returner,
                bPenalty_Rejected = false
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = Returner.p_and_r.p.ID,
                ko_ret = 1,
                ko_ret_plays = 1,
                ko_ret_yards = 20,
                ko_ret_yards_long = 20,
                ko_ret_TDs = 0,
                ko_ret_fumbles = 0,
                ko_ret_fumbles_lost = 0
            });

            double original_Yardline = 0.0;
            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult,  bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (!r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 20.0)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 0)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Returner.p_and_r.p.ID).First();
            if (a_stat.ko_ret_yards != 20 || a_stat.ko_ret_yards_long != 20 &&
                a_stat.ko_ret_TDs != 0 || a_stat.ko_ret_fumbles != 0 | a_stat.ko_ret_fumbles_lost != 0)
                throw new Exception("Player stats not as expected");

            Assert.IsTrue(true);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_FREE_KICK_Left_Touchback_to_25()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.FREE_KICK;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player Returner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = -5.0
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = null;
            Game_Player Penalized_Player = null;

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                Yards_Returned = 0.0,
                bTouchback = true,
                bTouchDown = false,
                end_of_play_yardline = 0.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Returner = Returner,
                bPenalty_Rejected = false
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = Returner.p_and_r.p.ID,
                ko_ret = 0,
                ko_ret_plays = 1,
                ko_ret_yards = 0,
                ko_ret_yards_long = 0,
                ko_ret_TDs = 0,
                ko_ret_fumbles = 0,
                ko_ret_fumbles_lost = 0
            });

            double original_Yardline = 0.0;
            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult,  bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (!r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 75.0)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 0)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Returner.p_and_r.p.ID).First();
            if (a_stat.ko_ret_yards != 0 || a_stat.ko_ret_yards_long != 0 &&
                a_stat.ko_ret_TDs != 0 || a_stat.ko_ret_fumbles != 0 | a_stat.ko_ret_fumbles_lost != 0)
                throw new Exception("Player stats not as expected");

            Assert.IsTrue(true);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_FREE_KICK_Left_Touchdown_NextPlay_XP()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.FREE_KICK;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player Returner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 105
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = null;
            Game_Player Penalized_Player = null;

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                Yards_Returned = 100.0,
                bTouchback = false,
                bTouchDown = true,
                end_of_play_yardline = 105.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Returner = Returner,
                bPenalty_Rejected = false
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = Returner.p_and_r.p.ID,
                ko_ret = 1,
                ko_ret_plays = 1,
                ko_ret_yards = 100,
                ko_ret_yards_long = 100,
                ko_ret_TDs = 1,
                ko_ret_fumbles = 0,
                ko_ret_fumbles_lost = 0
            });

            double original_Yardline = 0.0;
            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult,  bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (r.bFinal_SwitchPossession)
                throw new Exception("Possession should not switch as the team must kick an XP next");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 0 || r.Final_yard_to_go != 0)
                throw new Exception("The next Play will be an XP");

            //Check for next play
            if (!r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("Next play must be an XP");

            if (r.Final_Added_Penalty_Yards != 0)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                !r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("Away Team Must Score a TD");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Returner.p_and_r.p.ID).First();
            if (a_stat.ko_ret_yards != 100 || a_stat.ko_ret_yards_long != 100 &&
                a_stat.ko_ret_TDs != 1 || a_stat.ko_ret_fumbles != 0 | a_stat.ko_ret_fumbles_lost != 0)
                throw new Exception("Player stats not as expected");

            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_FREE_KICK_Left_Touchdown_NextPlay_XP_Ignore_Penalty()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.FREE_KICK;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player Returner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 105
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.UR).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                Current_YardLine = 25.0
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                Yards_Returned = 100.0,
                bTouchback = false,
                bTouchDown = true,
                end_of_play_yardline = 105.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Returner = Returner,
                bPenalty_Rejected = false
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = Returner.p_and_r.p.ID,
                ko_ret = 1,
                ko_ret_plays = 1,
                ko_ret_yards = 100,
                ko_ret_yards_long = 100,
                ko_ret_TDs = 1,
                ko_ret_fumbles = 0,
                ko_ret_fumbles_lost = 0
            });

            double original_Yardline = 0.0;
            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult,  bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (r.bFinal_SwitchPossession)
                throw new Exception("Possession should not switch as the team must kick an XP next");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 0 || r.Final_yard_to_go != 0)
                throw new Exception("The next Play will be an XP");

            //Check for next play
            if (!r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("Next play must be an XP");

            if (r.Final_Added_Penalty_Yards != 0 || !r.bIgnorePenalty)
                throw new Exception("Ignore the penalty for this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                !r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("Away Team Must Score a TD");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Returner.p_and_r.p.ID).First();
            if (a_stat.ko_ret_yards != 100 || a_stat.ko_ret_yards_long != 100 &&
                a_stat.ko_ret_TDs != 1 || a_stat.ko_ret_fumbles != 0 | a_stat.ko_ret_fumbles_lost != 0)
                throw new Exception("Player stats not as expected");

            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_FREE_KICK_Left_ReturntoOwn20_Spot_Penalty_on_Defenders_at_25_to_40()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.FREE_KICK;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player Returner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 20
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.FM).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                Current_YardLine = 25.0
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                Yards_Returned = 20.0,
                bTouchback = false,
                bTouchDown = false,
                end_of_play_yardline = 20.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Returner = Returner,
                bPenalty_Rejected = false
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = Returner.p_and_r.p.ID,
                ko_ret = 1,
                ko_ret_plays = 1,
                ko_ret_yards = 20,
                ko_ret_yards_long = 20,
                ko_ret_TDs = 0,
                ko_ret_fumbles = 0,
                ko_ret_fumbles_lost = 0
            });

            double original_Yardline = 0.0;
            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult,  bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (!r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 40.0)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 15)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Returner.p_and_r.p.ID).First();
            if (a_stat.ko_ret_yards != 20 || a_stat.ko_ret_yards_long != 20 &&
                a_stat.ko_ret_TDs != 0 || a_stat.ko_ret_fumbles != 0 | a_stat.ko_ret_fumbles_lost != 0)
                throw new Exception("Player stats not as expected");

            Assert.IsTrue(true);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_FREE_KICK_Left_ReturntoOwn40_Spot_Penalty_on_Defenders_to_30()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.FREE_KICK;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player Returner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 40
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.FM).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                Current_YardLine = 30.0
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                Yards_Returned = 40.0,
                bTouchback = false,
                bTouchDown = false,
                end_of_play_yardline = 40.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Returner = Returner,
                bPenalty_Rejected = false
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = Returner.p_and_r.p.ID,
                ko_ret = 1,
                ko_ret_plays = 1,
                ko_ret_yards = 40,
                ko_ret_yards_long = 40,
                ko_ret_TDs = 0,
                ko_ret_fumbles = 0,
                ko_ret_fumbles_lost = 0
            });

            double original_Yardline = 0.0;
            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult,  bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (!r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 55.0)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 15)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Returner.p_and_r.p.ID).First();
            if (a_stat.ko_ret_yards != 40 || a_stat.ko_ret_yards_long != 40 &&
                a_stat.ko_ret_TDs != 0 || a_stat.ko_ret_fumbles != 0 | a_stat.ko_ret_fumbles_lost != 0)
                throw new Exception("Player stats not as expected");

            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_FREE_KICK_Left_ReturntoOwn07_Spot_Penalty_on_Defenders_to_08()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.FREE_KICK;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player Returner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 7
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.FM).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                Current_YardLine = 8.0
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                Yards_Returned = 7.0,
                bTouchback = false,
                bTouchDown = false,
                end_of_play_yardline = 7.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Returner = Returner,
                bPenalty_Rejected = false
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = Returner.p_and_r.p.ID,
                ko_ret = 1,
                ko_ret_plays = 1,
                ko_ret_yards = 7,
                ko_ret_yards_long = 7,
                ko_ret_TDs = 0,
                ko_ret_fumbles = 0,
                ko_ret_fumbles_lost = 0
            });

            double original_Yardline = 0.0;
            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult,  bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (!r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 23.0)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 15)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Returner.p_and_r.p.ID).First();
            if (a_stat.ko_ret_yards != 7 || a_stat.ko_ret_yards_long != 7 &&
                a_stat.ko_ret_TDs != 0 || a_stat.ko_ret_fumbles != 0 | a_stat.ko_ret_fumbles_lost != 0)
                throw new Exception("Player stats not as expected");

            Assert.IsTrue(true);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_FREE_KICK_Left_ReturntoOwn91_Spot_Penalty_on_Defenders_to_92()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.FREE_KICK;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player Returner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 91
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.FM).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                Current_YardLine = 92.0
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                Yards_Returned = 91.0,
                bTouchback = false,
                bTouchDown = false,
                end_of_play_yardline = 91.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Returner = Returner,
                bPenalty_Rejected = false
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = Returner.p_and_r.p.ID,
                ko_ret = 1,
                ko_ret_plays = 1,
                ko_ret_yards = 91,
                ko_ret_yards_long = 91,
                ko_ret_TDs = 0,
                ko_ret_fumbles = 0,
                ko_ret_fumbles_lost = 0
            });

            double original_Yardline = 0.0;
            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult,  bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (!r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 96.0)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 4)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Returner.p_and_r.p.ID).First();
            if (a_stat.ko_ret_yards != 91 || a_stat.ko_ret_yards_long != 91 &&
                a_stat.ko_ret_TDs != 0 || a_stat.ko_ret_fumbles != 0 | a_stat.ko_ret_fumbles_lost != 0)
                throw new Exception("Player stats not as expected");

            Assert.IsTrue(true);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_FREE_KICK_Left_ReturntoOwn92_Spot_Penalty_on_Defenders_to_91()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.FREE_KICK;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player Returner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 92
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.FM).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                Current_YardLine = 92.0
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                Yards_Returned = 92.0,
                bTouchback = false,
                bTouchDown = false,
                end_of_play_yardline = 92.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Returner = Returner,
                bPenalty_Rejected = false
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = Returner.p_and_r.p.ID,
                ko_ret = 1,
                ko_ret_plays = 1,
                ko_ret_yards = 92,
                ko_ret_yards_long = 92,
                ko_ret_TDs = 0,
                ko_ret_fumbles = 0,
                ko_ret_fumbles_lost = 0
            });

            double original_Yardline = 0.0;
            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult,  bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (!r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 96.0)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 4)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Returner.p_and_r.p.ID).First();
            if (a_stat.ko_ret_yards != 92 || a_stat.ko_ret_yards_long != 92 &&
                a_stat.ko_ret_TDs != 0 || a_stat.ko_ret_fumbles != 0 | a_stat.ko_ret_fumbles_lost != 0)
                throw new Exception("Player stats not as expected");

            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_FREE_KICK_Left_ReturntoOwn30_Spot_Penalty_on_Returners_at_25()
        {
            bool penOnBallCarryingTeam = true;
            Play_Enum PE = Play_Enum.FREE_KICK;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player Returner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 30
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.FM).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                Current_YardLine = 25.0
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                Yards_Returned = 30.0,
                bTouchback = false,
                bTouchDown = false,
                end_of_play_yardline = 30.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Returner = Returner,
                bPenalty_Rejected = false
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = Returner.p_and_r.p.ID,
                ko_ret = 1,
                ko_ret_plays = 1,
                ko_ret_yards = 30,
                ko_ret_yards_long = 30,
                ko_ret_TDs = 0,
                ko_ret_fumbles = 0,
                ko_ret_fumbles_lost = 0
            });

            double original_Yardline = 0.0;
            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult,  bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (!r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 12.5)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 12.5)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Returner.p_and_r.p.ID).First();
            if (a_stat.ko_ret_yards != 25 || a_stat.ko_ret_yards_long != 25 &&
                a_stat.ko_ret_TDs != 0 || a_stat.ko_ret_fumbles != 0 | a_stat.ko_ret_fumbles_lost != 0)
                throw new Exception("Player stats not as expected");

            Assert.IsTrue(true);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_FREE_KICK_Left_ReturntoOwn30_Spot_Penalty_on_Returners_at_35()
        {
            bool penOnBallCarryingTeam = true;
            Play_Enum PE = Play_Enum.FREE_KICK;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player Returner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 30
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.FM).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                Current_YardLine = 35.0
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                Yards_Returned = 30.0,
                bTouchback = false,
                bTouchDown = false,
                end_of_play_yardline = 30.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Returner = Returner,
                bPenalty_Rejected = false
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = Returner.p_and_r.p.ID,
                ko_ret = 1,
                ko_ret_plays = 1,
                ko_ret_yards = 30,
                ko_ret_yards_long = 30,
                ko_ret_TDs = 0,
                ko_ret_fumbles = 0,
                ko_ret_fumbles_lost = 0
            });

            double original_Yardline = 0.0;
            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult,  bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (!r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 15.0)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 15.0)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Returner.p_and_r.p.ID).First();
            if (a_stat.ko_ret_yards != 30 || a_stat.ko_ret_yards_long != 30 &&
                a_stat.ko_ret_TDs != 0 || a_stat.ko_ret_fumbles != 0 | a_stat.ko_ret_fumbles_lost != 0)
                throw new Exception("Player stats not as expected");

            Assert.IsTrue(true);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_FREE_KICK_Left_ReturntoOwn92_Spot_Penalty_on_Returners_at_92()
        {
            bool penOnBallCarryingTeam = true;
            Play_Enum PE = Play_Enum.FREE_KICK;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player Returner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 92
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.FM).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                Current_YardLine = 92.0
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                Yards_Returned = 92.0,
                bTouchback = false,
                bTouchDown = false,
                end_of_play_yardline = 92.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Returner = Returner,
                bPenalty_Rejected = false
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = Returner.p_and_r.p.ID,
                ko_ret = 1,
                ko_ret_plays = 1,
                ko_ret_yards = 92,
                ko_ret_yards_long = 92,
                ko_ret_TDs = 0,
                ko_ret_fumbles = 0,
                ko_ret_fumbles_lost = 0
            });

            double original_Yardline = 0.0;
            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult,  bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (!r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must not stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 77.0)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 15.0)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Returner.p_and_r.p.ID).First();
            if (a_stat.ko_ret_yards != 92 || a_stat.ko_ret_yards_long != 92 &&
                a_stat.ko_ret_TDs != 0 || a_stat.ko_ret_fumbles != 0 | a_stat.ko_ret_fumbles_lost != 0)
                throw new Exception("Player stats not as expected");

            Assert.IsTrue(true);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_FREE_KICK_Left_ReturnTD_Spot_Penalty_on_Returners_at_50()
        {
            bool penOnBallCarryingTeam = true;
            Play_Enum PE = Play_Enum.FREE_KICK;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player Returner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 105
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.FM).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                Current_YardLine = 50.0
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                Yards_Returned = 100.0,
                bTouchback = false,
                bTouchDown = true,
                end_of_play_yardline = 105.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Returner = Returner,
                bPenalty_Rejected = false
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = Returner.p_and_r.p.ID,
                ko_ret = 1,
                ko_ret_plays = 1,
                ko_ret_yards = 100,
                ko_ret_yards_long = 100,
                ko_ret_TDs = 1,
                ko_ret_fumbles = 0,
                ko_ret_fumbles_lost = 0
            });

            double original_Yardline = 0.0;
            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult,  bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (!r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must not stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 35.0)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 15.0)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Returner.p_and_r.p.ID).First();
            if (a_stat.ko_ret_yards != 50 || a_stat.ko_ret_yards_long != 50 &&
                a_stat.ko_ret_TDs != 0 || a_stat.ko_ret_fumbles != 0 | a_stat.ko_ret_fumbles_lost != 0)
                throw new Exception("Player stats not as expected");

            Assert.IsTrue(true);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_FREE_KICK_Left_Return04_Spot_Penalty_on_Returners_at_5()
        {
            bool penOnBallCarryingTeam = true;
            Play_Enum PE = Play_Enum.FREE_KICK;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player Returner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 4.0
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.FM).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                Current_YardLine = 5.0
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                Yards_Returned = 4.0,
                bTouchback = false,
                bTouchDown = false,
                end_of_play_yardline = 4.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Returner = Returner,
                bPenalty_Rejected = false
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = Returner.p_and_r.p.ID,
                ko_ret = 1,
                ko_ret_plays = 1,
                ko_ret_yards = 4,
                ko_ret_yards_long = 4,
                ko_ret_TDs = 0,
                ko_ret_fumbles = 0,
                ko_ret_fumbles_lost = 0
            });

            double original_Yardline = 0.0;
            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult,  bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (!r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must not stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 2.0)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 2.0)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Returner.p_and_r.p.ID).First();
            if (a_stat.ko_ret_yards != 4 || a_stat.ko_ret_yards_long != 4 &&
                a_stat.ko_ret_TDs != 0 || a_stat.ko_ret_fumbles != 0 | a_stat.ko_ret_fumbles_lost != 0)
                throw new Exception("Player stats not as expected");

            Assert.IsTrue(true);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_FREE_KICK_Left_Return05_Spot_Penalty_on_Returners_at_4()
        {
            bool penOnBallCarryingTeam = true;
            Play_Enum PE = Play_Enum.FREE_KICK;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player Returner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 5.0
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.FM).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                Current_YardLine = 4.0
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                Yards_Returned = 5.0,
                bTouchback = false,
                bTouchDown = false,
                end_of_play_yardline = 5.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Returner = Returner,
                bPenalty_Rejected = false
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = Returner.p_and_r.p.ID,
                ko_ret = 1,
                ko_ret_plays = 1,
                ko_ret_yards = 5,
                ko_ret_yards_long = 5,
                ko_ret_TDs = 0,
                ko_ret_fumbles = 0,
                ko_ret_fumbles_lost = 0
            });

            double original_Yardline = 0.0;
            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult,  bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (!r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must not stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 2.0)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 2.0)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Returner.p_and_r.p.ID).First();
            if (a_stat.ko_ret_yards != 4 || a_stat.ko_ret_yards_long != 4 &&
                a_stat.ko_ret_TDs != 0 || a_stat.ko_ret_fumbles != 0 | a_stat.ko_ret_fumbles_lost != 0)
                throw new Exception("Player stats not as expected");

            Assert.IsTrue(true);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_FREE_KICK_Left_Return_Fumbles_on_20_and_Recovers()
        {
            bool penOnBallCarryingTeam = true;
            Play_Enum PE = Play_Enum.FREE_KICK;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player Returner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 20
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = null;
            Game_Player Penalized_Player = null;

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                Yards_Returned = 20.0,
                bTouchback = false,
                bTouchDown = false,
                end_of_play_yardline = 20.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                bPenalty_Rejected = false,
                Returner = Returner,
                bFumble = true
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = Returner.p_and_r.p.ID,
                ko_ret = 1,
                ko_ret_plays = 1,
                ko_ret_yards = 20,
                ko_ret_yards_long = 20,
                ko_ret_TDs = 0,
                ko_ret_fumbles = 1,
                ko_ret_fumbles_lost = 0
            });

            double original_Yardline = 0.0;
            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult,  bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (!r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must not stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 20.0)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 0.0)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Returner.p_and_r.p.ID).First();
            if (a_stat.ko_ret_yards != 20 || a_stat.ko_ret_yards_long != 20 &&
                a_stat.ko_ret_TDs != 0 || a_stat.ko_ret_fumbles != 1 | a_stat.ko_ret_fumbles_lost != 0)
                throw new Exception("Player stats not as expected");

            Assert.IsTrue(true);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_FREE_KICK_Left_Return_lost_Fumbles_on_20_penalty_on_retuners()
        {
            bool penOnBallCarryingTeam = true;
            Play_Enum PE = Play_Enum.FREE_KICK;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player Returner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 20
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.FM).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                Current_YardLine = 15.0
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                Yards_Returned = 20.0,
                bTouchback = false,
                bTouchDown = false,
                end_of_play_yardline = 20.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Returner = Returner,
                bPenalty_Rejected = false,
                bFumble = true,
                bFumble_Lost = true
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = Returner.p_and_r.p.ID,
                ko_ret = 1,
                ko_ret_plays = 1,
                ko_ret_yards = 20,
                ko_ret_yards_long = 20,
                ko_ret_TDs = 0,
                ko_ret_fumbles = 1,
                ko_ret_fumbles_lost = 1
            });

            double original_Yardline = 0.0;
            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult,  bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must not stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 7.5)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 7.5)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 1 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Returner.p_and_r.p.ID).First();
            if (a_stat.ko_ret_yards != 15 || a_stat.ko_ret_yards_long != 15 &&
                a_stat.ko_ret_TDs != 0 || a_stat.ko_ret_fumbles != 1 | a_stat.ko_ret_fumbles_lost != 1)
                throw new Exception("Player stats not as expected");

            Assert.IsTrue(true);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_FREE_KICK_Left_Return_lost_Fumbles_on_20_penalty_on_defenders()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.FREE_KICK;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player Returner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 20
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.FM).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                Current_YardLine = 15.0
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                Yards_Returned = 20.0,
                bTouchback = false,
                bTouchDown = false,
                end_of_play_yardline = 20.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Returner = Returner,
                bPenalty_Rejected = false,
                bFumble = true,
                bFumble_Lost = true
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = Returner.p_and_r.p.ID,
                ko_ret = 1,
                ko_ret_plays = 1,
                ko_ret_yards = 20,
                ko_ret_yards_long = 20,
                ko_ret_TDs = 0,
                ko_ret_fumbles = 1,
                ko_ret_fumbles_lost = 1
            });

            double original_Yardline = 0.0;
            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult,  bLefttoRgiht, TouchBack_Yardline);

            if (r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must not stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 35)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 15)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 1 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Returner.p_and_r.p.ID).First();
            if (a_stat.ko_ret_yards != 20 || a_stat.ko_ret_yards_long != 20 &&
                a_stat.ko_ret_TDs != 0 || a_stat.ko_ret_fumbles != 1 | a_stat.ko_ret_fumbles_lost != 1)
                throw new Exception("Player stats not as expected");

            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_KICKOFF_ONSIDES_Left_returner_falls_on_ball_no_Penalty_to_50()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.FREE_KICK;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player Returner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 50
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = null;
            Game_Player Penalized_Player = null;

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                Yards_Returned = 0.0,
                bTouchback = false,
                bTouchDown = false,
                end_of_play_yardline = 50.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Returner = Returner,
                bPenalty_Rejected = false
            };

            double original_Yardline = 0.0;
            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult,  bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (!r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 50.0)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 0)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            Assert.IsTrue(true);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_KICKOFF_ONSIDES_Left_Fumble_Recovered_no_Penalty_to_50()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.FREE_KICK;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player Returner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 50
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = null;
            Game_Player Penalized_Player = null;

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                Yards_Returned = 0.0,
                bTouchback = false,
                bTouchDown = false,
                end_of_play_yardline = 50.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Returner = Returner,
                bPenalty_Rejected = false
            };

            double original_Yardline = 0.0;
            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult,  bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (!r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 50.0)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 0)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            Assert.IsTrue(true);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_KICKOFF_ONSIDES_Left_Fumble_Lost_no_Penalty_to_50()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.FREE_KICK;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player Returner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 50
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = null;
            Game_Player Penalized_Player = null;

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                Yards_Returned = 0.0,
                bTouchback = false,
                bTouchDown = false,
                end_of_play_yardline = 50.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Returner = Returner,
                bPenalty_Rejected = false,
                bFumble = true,
                bFumble_Lost = true
            };

            double original_Yardline = 0.0;
            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult,  bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (r.bFinal_SwitchPossession)
                throw new Exception("Switch possession is wrong");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 50.0)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 0)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 1 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_KICKOFF_ONSIDES_Left_Returner_Falls_on_Ball50_Spot_Penalty_on_Defenders_at_55_to_70()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.KICKOFF_ONSIDES;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player Returner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 50
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.FM).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                Current_YardLine = 55.0
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                Yards_Returned = 0.0,
                bTouchback = false,
                bTouchDown = false,
                end_of_play_yardline = 50.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Returner = Returner,
                bPenalty_Rejected = false
            };

            double original_Yardline = 0.0;
            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult,  bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (!r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 70.0)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 15)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_KICKOFF_ONSIDES_Left_Returner_Falls_on_Ball50_Spot_Penalty_on_Defenders_at_45_to_65()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.KICKOFF_ONSIDES;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player Returner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 50
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.FM).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                Current_YardLine = 45.0
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                Yards_Returned = 0.0,
                bTouchback = false,
                bTouchDown = false,
                end_of_play_yardline = 50.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Returner = Returner,
                bPenalty_Rejected = false
            };

            double original_Yardline = 0.0;
            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult,  bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (!r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 65.0)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 15)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            Assert.IsTrue(true);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_KICKOFF_ONSIDES_Left_Returner_fumbles_and_recovers_on_Ball50_Spot_Penalty_on_Defenders_at_55_to_70()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.KICKOFF_ONSIDES;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player Returner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 50
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.FM).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                Current_YardLine = 55.0
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                Yards_Returned = 0.0,
                bTouchback = false,
                bTouchDown = false,
                end_of_play_yardline = 50.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Returner = Returner,
                bPenalty_Rejected = false,
                bFumble = true
            };

            double original_Yardline = 0.0;
            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult, bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (!r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 70.0)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 15)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_KICKOFF_ONSIDES_Left_Returner_fumbles_and_recovers_on_Ball50_Spot_Penalty_on_Defenders_at_45_to_65()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.KICKOFF_ONSIDES;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player Returner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 50
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.FM).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                Current_YardLine = 45.0
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                Yards_Returned = 0.0,
                bTouchback = false,
                bTouchDown = false,
                end_of_play_yardline = 50.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Returner = Returner,
                bPenalty_Rejected = false,
                bFumble = true
            };

            double original_Yardline = 0.0;
            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult, bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (!r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 65.0)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 15)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            Assert.IsTrue(true);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_KICKOFF_ONSIDES_Left_Returner_fumbles_and_loses_it_on_Ball50_Spot_Penalty_on_Defenders_at_55_to_70()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.KICKOFF_ONSIDES;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player Returner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 50
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.FM).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                Current_YardLine = 55.0
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                Yards_Returned = 0.0,
                bTouchback = false,
                bTouchDown = false,
                end_of_play_yardline = 50.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Returner = Returner,
                bPenalty_Rejected = false,
                bFumble = true,
                bFumble_Lost = true
            };

            double original_Yardline = 0.0;
            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult, bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 70.0)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 15)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 1 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_KICKOFF_ONSIDES_Left_Returner_fumbles_and_loses_it_recovers_on_Ball50_Spot_Penalty_on_Defenders_at_45_to_65()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.KICKOFF_ONSIDES;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player Returner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 50
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.FM).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                Current_YardLine = 45.0
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                Yards_Returned = 0.0,
                bTouchback = false,
                bTouchDown = false,
                end_of_play_yardline = 50.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Returner = Returner,
                bPenalty_Rejected = false,
                bFumble = true,
                bFumble_Lost = true
            };

            double original_Yardline = 0.0;
            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult,  bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 65.0)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 15)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 1 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_KICKOFF_ONSIDES_Left_Returner_Falls_on_Ball50_Spot_Penalty_on_Returners_at_55_to_70()
        {
            bool penOnBallCarryingTeam = true;
            Play_Enum PE = Play_Enum.KICKOFF_ONSIDES;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player Returner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 50
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.FM).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                Current_YardLine = 55.0
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                Yards_Returned = 0.0,
                bTouchback = false,
                bTouchDown = false,
                end_of_play_yardline = 50.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Returner = Returner,
                bPenalty_Rejected = false
            };

            double original_Yardline = 0.0;
            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult,  bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (!r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 35.0)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 15)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_KICKOFF_ONSIDES_Left_Returner_Falls_on_Ball50_Spot_Penalty_on_Returners_at_45_to_65()
        {
            bool penOnBallCarryingTeam = true;
            Play_Enum PE = Play_Enum.KICKOFF_ONSIDES;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player Returner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 50
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.FM).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                Current_YardLine = 45.0
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                Yards_Returned = 0.0,
                bTouchback = false,
                bTouchDown = false,
                end_of_play_yardline = 50.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Returner = Returner,
                bPenalty_Rejected = false
            };

            double original_Yardline = 0.0;
            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult,  bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (!r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 30.0)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 15)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            Assert.IsTrue(true);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_KICKOFF_ONSIDES_Left_Returner_fumbles_and_recovers_on_Ball50_Spot_Penalty_on_Returners_at_55_to_70()
        {
            bool penOnBallCarryingTeam = true;
            Play_Enum PE = Play_Enum.KICKOFF_ONSIDES;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player Returner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 50
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.FM).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                Current_YardLine = 55.0
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                Yards_Returned = 0.0,
                bTouchback = false,
                bTouchDown = false,
                end_of_play_yardline = 50.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Returner = Returner,
                bPenalty_Rejected = false,
                bFumble = true
            };

            double original_Yardline = 0.0;
            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult,  bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (!r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 35.0)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 15)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_KICKOFF_ONSIDES_Left_Returner_fumbles_and_recovers_on_Ball50_Spot_Penalty_on_Returners_at_45_to_65()
        {
            bool penOnBallCarryingTeam = true;
            Play_Enum PE = Play_Enum.KICKOFF_ONSIDES;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player Returner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 50
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.FM).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                Current_YardLine = 45.0
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                Yards_Returned = 0.0,
                bTouchback = false,
                bTouchDown = false,
                end_of_play_yardline = 50.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Returner = Returner,
                bPenalty_Rejected = false,
                bFumble = true
            };

            double original_Yardline = 0.0;
            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult,  bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (!r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 30.0)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 15)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            Assert.IsTrue(true);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_KICKOFF_ONSIDES_Left_Returner_fumbles_and_loses_it_on_Ball50_Spot_Penalty_on_Returners_at_55_to_70()
        {
            bool penOnBallCarryingTeam = true;
            Play_Enum PE = Play_Enum.KICKOFF_ONSIDES;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player Returner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 50
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.FM).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                Current_YardLine = 55.0
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                Yards_Returned = 0.0,
                bTouchback = false,
                bTouchDown = false,
                end_of_play_yardline = 50.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Returner = Returner,
                bPenalty_Rejected = false,
                bFumble = true,
                bFumble_Lost = true
            };

            double original_Yardline = 0.0;
            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult,  bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 35.0)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 15)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 1 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_KICKOFF_ONSIDES_Left_Returner_fumbles_and_loses_it_recovers_on_Ball50_Spot_Penalty_on_Returners_at_45_to_65()
        {
            bool penOnBallCarryingTeam = true;
            Play_Enum PE = Play_Enum.KICKOFF_ONSIDES;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player Returner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 50
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.FM).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                Current_YardLine = 45.0
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                Yards_Returned = 0.0,
                bTouchback = false,
                bTouchDown = false,
                end_of_play_yardline = 50.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Returner = Returner,
                bPenalty_Rejected = false,
                bFumble = true,
                bFumble_Lost = true
            };

            double original_Yardline = 0.0;
            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult,  bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 30.0)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 15)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 1 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_FG_Left_Good_No_Penalty()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.FIELD_GOAL;
            int Down = 4;
            double Yards_to_Go = 10.0;

            Game_Player kicker = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 70
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = null;
            Game_Player Penalized_Player = null;

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 80.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Kicker = kicker,
                bPenalty_Rejected = false,
                bFGMade = true
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = kicker.p_and_r.p.ID,
                FG_Att = 1,
                FG_Made = 1
            });

            double original_Yardline = 0.0;
            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult, bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must not stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 0 || r.Final_yard_to_go != 0 || r.Final_end_of_Play_Yardline != 0)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || !r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 0)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || !r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == kicker.p_and_r.p.ID).First();
            if (a_stat.FG_Att != 1 || a_stat.FG_Made != 1)
                throw new Exception("Player stats not as expected");

            Assert.IsTrue(true);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_FG_Left_Not_Good_No_Penalty()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.FIELD_GOAL;
            int Down = 4;
            double Yards_to_Go = 10.0;

            Game_Player kicker = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 70
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = null;
            Game_Player Penalized_Player = null;

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 80.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Kicker = kicker,
                bPenalty_Rejected = false,
                bFGMade = false
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = kicker.p_and_r.p.ID,
                FG_Att = 1,
                FG_Made = 0
            });

            double original_Yardline = 0.0;
            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult,  bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (!r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must not stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 80)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 0)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == kicker.p_and_r.p.ID).First();
            if (a_stat.FG_Att != 1 || a_stat.FG_Made != 0)
                throw new Exception("Player stats not as expected");

            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_FG_Left_Delay_of_Game_Penalty()
        {
            bool penOnBallCarryingTeam = true;
            Play_Enum PE = Play_Enum.FIELD_GOAL;
            int Down = 4;
            double Yards_to_Go = 10.0;

            Game_Player kicker = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 70
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.DG).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                Current_YardLine = 65.0
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 80.0,
                Play_Start_Yardline = 80.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Kicker = kicker,
                bPenalty_Rejected = false,
                bFGMade = false
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = kicker.p_and_r.p.ID,
                FG_Att = 0,
                FG_Made = 0
            });

            double original_Yardline = 0.0;
            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult,  bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (r.bPlay_Stands)
                throw new Exception("This Play must not stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 4 || r.Final_yard_to_go != 15 || r.Final_end_of_Play_Yardline != 75)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 5)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == kicker.p_and_r.p.ID).First();
            if (a_stat.FG_Att != 0 || a_stat.FG_Made != 0)
                throw new Exception("Player stats not as expected");

            Assert.IsTrue(true);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_FG_Left_false_start_Penalty()
        {
            bool penOnBallCarryingTeam = true;
            Play_Enum PE = Play_Enum.FIELD_GOAL;
            int Down = 4;
            double Yards_to_Go = 10.0;

            Game_Player kicker = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 70
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.FS).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                Current_YardLine = 65.0
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 80.0,
                Play_Start_Yardline = 80.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Kicker = kicker,
                bPenalty_Rejected = false,
                bFGMade = false
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = kicker.p_and_r.p.ID,
                FG_Att = 0,
                FG_Made = 0
            });

            double original_Yardline = 0.0;
            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult,  bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (r.bPlay_Stands)
                throw new Exception("This Play must not stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 4 || r.Final_yard_to_go != 15 || r.Final_end_of_Play_Yardline != 75)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 5)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == kicker.p_and_r.p.ID).First();
            if (a_stat.FG_Att != 0 || a_stat.FG_Made != 0)
                throw new Exception("Player stats not as expected");

            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_FG_Left_NZ_Penalty()
        {
            bool penOnBallCarryingTeam = true;
            Play_Enum PE = Play_Enum.FIELD_GOAL;
            int Down = 4;
            double Yards_to_Go = 10.0;

            Game_Player kicker = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 70
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.NZ).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                Current_YardLine = 65.0
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 80.0,
                Play_Start_Yardline = 80.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Kicker = kicker,
                bPenalty_Rejected = false,
                bFGMade = false
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = kicker.p_and_r.p.ID,
                FG_Att = 0,
                FG_Made = 0
            });

            double original_Yardline = 0.0;
            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult,  bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (r.bPlay_Stands)
                throw new Exception("This Play must not stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 4 || r.Final_yard_to_go != 15 || r.Final_end_of_Play_Yardline != 75)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 5)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == kicker.p_and_r.p.ID).First();
            if (a_stat.FG_Att != 0 || a_stat.FG_Made != 0)
                throw new Exception("Player stats not as expected");

            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_FG_Left_Missed_Def_Offsides_Accepted()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.FIELD_GOAL;
            int Down = 4;
            double Yards_to_Go = 10.0;

            Game_Player kicker = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 70
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.DO).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                Current_YardLine = 65.0
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 80.0,
                Play_Start_Yardline = 80.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Kicker = kicker,
                bPenalty_Rejected = false,
                bFGMade = false
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = kicker.p_and_r.p.ID,
                FG_Att = 1,
                FG_Made = 0
            });

            double original_Yardline = 0.0;
            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult,  bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (r.bPlay_Stands)
                throw new Exception("This Play must not stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 4 || r.Final_yard_to_go != 5 || r.Final_end_of_Play_Yardline != 85)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 5)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //no need to check stats as this play won't stand
            //Check stats
/*            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == kicker.p_and_r.p.ID).First();
            if (a_stat.FG_Att != 0 || a_stat.FG_Made != 0)
                throw new Exception("Player stats not as expected");
*/

            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_FG_Left_Missed_Def_Offsides_Accepted_First_down()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.FIELD_GOAL;
            int Down = 4;
            double Yards_to_Go = 2.0;

            Game_Player kicker = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 70
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.DO).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                Current_YardLine = 65.0
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 80.0,
                Play_Start_Yardline = 80.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Kicker = kicker,
                bPenalty_Rejected = false,
                bFGMade = false
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = kicker.p_and_r.p.ID,
                FG_Att = 1,
                FG_Made = 0
            });

            double original_Yardline = 0.0;
            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult,  bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (r.bPlay_Stands)
                throw new Exception("This Play must not stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 85)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 5)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 1 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //no need to check stats as this play won't stand
            //Check stats
            /*            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == kicker.p_and_r.p.ID).First();
                        if (a_stat.FG_Att != 0 || a_stat.FG_Made != 0)
                            throw new Exception("Player stats not as expected");
            */

            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_FG_Left_Missed_Def_Offsides_Accepted_at_5_to_2point5()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.FIELD_GOAL;
            int Down = 4;
            double Yards_to_Go = 8.0;

            Game_Player kicker = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 80
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.DO).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                Current_YardLine = 97.0
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 95.0,
                Play_Start_Yardline = 95.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Kicker = kicker,
                bPenalty_Rejected = false,
                bFGMade = false
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = kicker.p_and_r.p.ID,
                FG_Att = 1,
                FG_Made = 0
            });

            double original_Yardline = 0.0;
            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult,  bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (r.bPlay_Stands)
                throw new Exception("This Play must not stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 4 || r.Final_yard_to_go != 5.5 || r.Final_end_of_Play_Yardline != 97.5)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 2.5)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //no need to check stats as this play won't stand
            //Check stats
            /*            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == kicker.p_and_r.p.ID).First();
                        if (a_stat.FG_Att != 0 || a_stat.FG_Made != 0)
                            throw new Exception("Player stats not as expected");
            */

            Assert.IsTrue(true);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_FG_Left_Good_Def_Offsides_Accepted_First_down()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.FIELD_GOAL;
            int Down = 4;
            double Yards_to_Go = 2.0;

            Game_Player kicker = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 70
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.DO).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                Current_YardLine = 65.0
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 80.0,
                Play_Start_Yardline = 80.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Kicker = kicker,
                bPenalty_Rejected = false,
                bFGMade = false
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = kicker.p_and_r.p.ID,
                FG_Att = 1,
                FG_Made = 1
            });

            double original_Yardline = 0.0;
            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult,  bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (r.bPlay_Stands)
                throw new Exception("This Play must not stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 85)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 5)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 1 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //no need to check stats as this play won't stand
            //Check stats
            /*            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == kicker.p_and_r.p.ID).First();
                        if (a_stat.FG_Att != 0 || a_stat.FG_Made != 0)
                            throw new Exception("Player stats not as expected");
            */

            Assert.IsTrue(true);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_FG_Left_Good_Def_Offsides_Declined()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.FIELD_GOAL;
            int Down = 4;
            double Yards_to_Go = 2.0;

            Game_Player kicker = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 70
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.DO).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                Current_YardLine = 65.0
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 80.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Kicker = kicker,
                bPenalty_Rejected = true,
                bFGMade = true
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = kicker.p_and_r.p.ID,
                FG_Att = 1,
                FG_Made = 1
            });

            double original_Yardline = 0.0;
            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult,  bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must not stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 0 || r.Final_yard_to_go != 0 || r.Final_end_of_Play_Yardline != 0)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || !r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards !=0)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || !r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
                Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == kicker.p_and_r.p.ID).First();
                if (a_stat.FG_Att != 1 || a_stat.FG_Made != 1)
                    throw new Exception("Player stats not as expected");


            Assert.IsTrue(true);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_XP_Left_Good_No_Penalty()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.EXTRA_POINT;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player kicker = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 70
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = null;
            Game_Player Penalized_Player = null;

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 80.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Kicker = kicker,
                bPenalty_Rejected = false,
                bFGMade = false,
                bXPMade = true
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = kicker.p_and_r.p.ID,
                XP_Att = 1,
                XP_Made = 1
            });

            double original_Yardline = 0.0;
            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult,  bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must not stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 0 || r.Final_yard_to_go != 0 || r.Final_end_of_Play_Yardline != 0)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || !r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 0)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || !r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == kicker.p_and_r.p.ID).First();
            if (a_stat.XP_Att != 1 || a_stat.XP_Made != 1)
                throw new Exception("Player stats not as expected");

            Assert.IsTrue(true);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_XP_Left_Not_Good_No_Penalty()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.EXTRA_POINT;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player kicker = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 70
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = null;
            Game_Player Penalized_Player = null;

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 80.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Kicker = kicker,
                bPenalty_Rejected = false,
                bXPMissed = true
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = kicker.p_and_r.p.ID,
                XP_Att = 1,
                XP_Made = 0
            });

            double original_Yardline = 0.0;
            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult,  bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must not stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 0 || r.Final_yard_to_go != 0 || r.Final_end_of_Play_Yardline != 0)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || !r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 0)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == kicker.p_and_r.p.ID).First();
            if (a_stat.XP_Att != 1 || a_stat.XP_Made != 0)
                throw new Exception("Player stats not as expected");

            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_XP_Left_Delay_of_Game_Penalty()
        {
            bool penOnBallCarryingTeam = true;
            Play_Enum PE = Play_Enum.EXTRA_POINT;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player kicker = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 70
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.DG).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                Current_YardLine = 65.0
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 80.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Kicker = kicker,
                bPenalty_Rejected = false,
                bFGMade = false
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = kicker.p_and_r.p.ID,
                XP_Att = 0,
                XP_Made = 0
            });

            double original_Yardline = 0.0;
            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult,  bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (r.bPlay_Stands)
                throw new Exception("This Play must not stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 0 || r.Final_yard_to_go != 0 || r.Final_end_of_Play_Yardline != 75)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (!r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 5)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == kicker.p_and_r.p.ID).First();
            if (a_stat.XP_Att != 0 || a_stat.XP_Made != 0)
                throw new Exception("Player stats not as expected");

            Assert.IsTrue(true);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_XP_Left_false_start_Penalty()
        {
            bool penOnBallCarryingTeam = true;
            Play_Enum PE = Play_Enum.EXTRA_POINT;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player kicker = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 70
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.FS).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                Current_YardLine = 65.0
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 80.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Kicker = kicker,
                bPenalty_Rejected = false,
                bFGMade = false
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = kicker.p_and_r.p.ID,
                XP_Att = 0,
                XP_Made = 0
            });

            double original_Yardline = 0.0;
            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult,  bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (r.bPlay_Stands)
                throw new Exception("This Play must not stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 0 || r.Final_yard_to_go != 0 || r.Final_end_of_Play_Yardline != 75)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (!r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 5)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == kicker.p_and_r.p.ID).First();
            if (a_stat.XP_Att != 0 || a_stat.XP_Made != 0)
                throw new Exception("Player stats not as expected");

            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_XP_Left_NZ_Penalty()
        {
            bool penOnBallCarryingTeam = true;
            Play_Enum PE = Play_Enum.EXTRA_POINT;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player kicker = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 70
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.NZ).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                Current_YardLine = 65.0
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 80.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Kicker = kicker,
                bPenalty_Rejected = false,
                bFGMade = false
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = kicker.p_and_r.p.ID,
                XP_Att = 0,
                XP_Made = 0
            });

            double original_Yardline = 0.0;
            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult,  bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (r.bPlay_Stands)
                throw new Exception("This Play must not stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 0 || r.Final_yard_to_go != 0 || r.Final_end_of_Play_Yardline != 75)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (!r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 5)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == kicker.p_and_r.p.ID).First();
            if (a_stat.XP_Att != 0 || a_stat.XP_Made != 0)
                throw new Exception("Player stats not as expected");

            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_XP_Left_Missed_Def_Offsides_Accepted()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.EXTRA_POINT;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player kicker = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 70
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.DO).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                Current_YardLine = 65.0
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 80.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Kicker = kicker,
                bPenalty_Rejected = false,
                bFGMade = false
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = kicker.p_and_r.p.ID,
                XP_Att = 1,
                XP_Made = 0
            });

            double original_Yardline = 0.0;
            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult, bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (r.bPlay_Stands)
                throw new Exception("This Play must not stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 0 || r.Final_yard_to_go != 0 || r.Final_end_of_Play_Yardline != 85)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (!r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 5)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //no need to check stats as this play won't stand
            //Check stats
            /*            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == kicker.p_and_r.p.ID).First();
                        if (a_stat.FG_Att != 0 || a_stat.FG_Made != 0)
                            throw new Exception("Player stats not as expected");
            */

            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_XP_Left_Missed_Def_Offsides_Accepted_at_5_to_2point5()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.EXTRA_POINT;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player kicker = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 80
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.DO).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                Current_YardLine = 97.0
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 95.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Kicker = kicker,
                bPenalty_Rejected = false,
                bFGMade = false
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = kicker.p_and_r.p.ID,
                XP_Att = 1,
                XP_Made = 0
            });

            double original_Yardline = 0.0;
            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult, bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (r.bPlay_Stands)
                throw new Exception("This Play must not stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 0 || r.Final_yard_to_go != 0 || r.Final_end_of_Play_Yardline != 97.5)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (!r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 2.5)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //no need to check stats as this play won't stand
            //Check stats
            /*            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == kicker.p_and_r.p.ID).First();
                        if (a_stat.FG_Att != 0 || a_stat.FG_Made != 0)
                            throw new Exception("Player stats not as expected");
            */

            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_XP_Left_Good_Def_Offsides_Declined()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.EXTRA_POINT;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player kicker = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 70
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.DO).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                Current_YardLine = 65.0
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 80.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Kicker = kicker,
                bPenalty_Rejected = true,
                bXPMade = true
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = kicker.p_and_r.p.ID,
                XP_Att = 1,
                XP_Made = 1
            });

            double original_Yardline = 0.0;
            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult, bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must not stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 0 || r.Final_yard_to_go != 0 || r.Final_end_of_Play_Yardline != 0)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || !r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 0)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || !r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == kicker.p_and_r.p.ID).First();
            if (a_stat.XP_Att != 1 || a_stat.XP_Made != 1)
                throw new Exception("Player stats not as expected");


            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Punt_Left_No_Penalty_10_Yard_Return()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.PUNT;
            int Down = 4;
            double Yards_to_Go = 7.0;

            Game_Player Punter = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 27
            };
            Game_Player Retuner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 70
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = null;
            Game_Player Penalized_Player = null;

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 70.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Punter = Punter,
                Punt_Returner = Retuner,
                bPenalty_Rejected = false
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = Retuner.p_and_r.p.ID,
                punt_ret = 1,
                punt_ret_yards = 40
            });

            double original_Yardline = 27.0;
            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult, bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (!r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must not stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 70)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 0)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Punter.p_and_r.p.ID).First();
            if (a_stat.punt_ret != 1 || a_stat.punt_ret_yards != 40)
                throw new Exception("Player stats not as expected");


            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Punt_Left_No_Penalty_10_Yard_Return_Fumble_Recovered()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.PUNT;
            int Down = 4;
            double Yards_to_Go = 7.0;

            Game_Player Punter = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 27
            };
            Game_Player Retuner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 70
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = null;
            Game_Player Penalized_Player = null;

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 70.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Punter = Punter,
                Punt_Returner = Retuner,
                bPenalty_Rejected = false,
                bFumble = true
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = Retuner.p_and_r.p.ID,
                punt_ret = 1,
                punt_ret_yards = 40,
                punt_ret_fumbles = 1
            });

            double original_Yardline = 27.0;
            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult, bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (!r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must not stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 70)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 0)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Punter.p_and_r.p.ID).First();
            if (a_stat.punt_ret != 1 || a_stat.punt_ret_yards != 40 || a_stat.punt_ret_fumbles != 1)
                throw new Exception("Player stats not as expected");


            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Punt_Left_No_Penalty_10_Yard_Return_Fumble_Lost()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.PUNT;
            int Down = 4;
            double Yards_to_Go = 7.0;

            Game_Player Punter = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 27
            };
            Game_Player Retuner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 70
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = null;
            Game_Player Penalized_Player = null;

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 70.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Punt_Returner = Punter,
                Returner = Retuner,
                bPenalty_Rejected = false,
                bFumble = true,
                bFumble_Lost = true
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = Retuner.p_and_r.p.ID,
                punt_ret = 1,
                punt_ret_yards = 40,
                punt_ret_fumbles = 1,
                punt_ret_fumbles_lost = 1
            });

            double original_Yardline = 27.0;
            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult, bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must not stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 70)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 0)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 1 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Punter.p_and_r.p.ID).First();
            if (a_stat.punt_ret != 1 || a_stat.punt_ret_yards != 40 || a_stat.punt_ret_fumbles != 1 || a_stat.punt_ret_fumbles_lost != 1)
                throw new Exception("Player stats not as expected");


            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Punt_Left_No_Penalty_Returned_for_TD()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.PUNT;
            int Down = 4;
            double Yards_to_Go = 7.0;

            Game_Player Punter = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 27
            };
            Game_Player Retuner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 0
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = null;
            Game_Player Penalized_Player = null;

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 70.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Punt_Returner = Punter,
                Returner = Retuner,
                bPenalty_Rejected = false,
                bTouchDown = true
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = Retuner.p_and_r.p.ID,
                punt_ret = 1,
                punt_ret_yards = 100,
                punt_ret_TDs = 1
            });

            double original_Yardline = 27.0;
            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult, bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (!r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must not stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 0 || r.Final_yard_to_go != 0 || r.Final_end_of_Play_Yardline != 0)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (!r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 0)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                !r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Punter.p_and_r.p.ID).First();
            if (a_stat.punt_ret != 1 || a_stat.punt_ret_yards != 100 || a_stat.punt_ret_TDs != 1)
                throw new Exception("Player stats not as expected");


            Assert.IsTrue(true);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Punt_Left_Sport_Penalty_Ignored_Returned_for_TD()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.PUNT;
            int Down = 4;
            double Yards_to_Go = 7.0;

            Game_Player Punter = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 27
            };
            Game_Player Retuner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 0
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.UC).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                Current_YardLine = 65.0
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 70.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Punt_Returner = Punter,
                Returner = Retuner,
                bPenalty_Rejected = false,
                bTouchDown = true
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = Retuner.p_and_r.p.ID,
                punt_ret = 1,
                punt_ret_yards = 100,
                punt_ret_TDs = 1
            });

            double original_Yardline = 27.0;
            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult, bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (!r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must not stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 0 || r.Final_yard_to_go != 0 || r.Final_end_of_Play_Yardline != 0)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (!r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 0 || !r.bIgnorePenalty)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                !r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Punter.p_and_r.p.ID).First();
            if (a_stat.punt_ret != 1 || a_stat.punt_ret_yards != 100 || a_stat.punt_ret_TDs != 1)
                throw new Exception("Player stats not as expected");


            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Punt_Left_No_Penalty_Touchbck()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.PUNT;
            int Down = 4;
            double Yards_to_Go = 7.0;

            Game_Player Punter = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 27
            };
            Game_Player Retuner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 70
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = null;
            Game_Player Penalized_Player = null;

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 70.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Punt_Returner = Punter,
                Returner = Retuner,
                bPenalty_Rejected = false,
                bTouchback = true
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = Retuner.p_and_r.p.ID,
                punt_ret = 1,
                punt_ret_yards = 40
            });

            double original_Yardline = 27.0;
            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult, bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (!r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must not stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 75)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 0)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Punter.p_and_r.p.ID).First();
            if (a_stat.punt_ret != 1 || a_stat.punt_ret_yards != 40)
                throw new Exception("Player stats not as expected");


            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Punt_Left_No_Penalty_Punt_Out_of_Bounds()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.PUNT;
            int Down = 4;
            double Yards_to_Go = 7.0;

            Game_Player Punter = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 27
            };
            Game_Player Retuner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 70
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = null;
            Game_Player Penalized_Player = null;

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 70.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Punt_Returner = Punter,
                Returner = Retuner,
                bPenalty_Rejected = false,
                bRunOutofBounds = true
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = Retuner.p_and_r.p.ID,
                punt_ret_plays = 1,
                punt_ret = 0,
                punt_ret_yards = 0
            });

            double original_Yardline = 27.0;
            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult, bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (!r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must not stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 70)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 0)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Punter.p_and_r.p.ID).First();
            if (a_stat.punt_ret_plays != 1 || a_stat.punt_ret != 0 || a_stat.punt_ret_yards != 0)
                throw new Exception("Player stats not as expected");


            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Punt_Left_Delay_of_Game_From_6()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.PUNT;
            int Down = 4;
            double Yards_to_Go = 7.0;

            Game_Player Punter = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 27
            };
            Game_Player Retuner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 70
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.DG).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                Current_YardLine = 2.0
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 6.0,
                Play_Start_Yardline = 6.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Punt_Returner = Punter,
                Returner = Retuner,
                bPenalty_Rejected = false,
                bRunOutofBounds = true
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = Retuner.p_and_r.p.ID,
                punt_ret_plays = 0,
                punt_ret = 0,
                punt_ret_yards = 0
            });

            double original_Yardline = 6.0;
            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult, bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (r.bPlay_Stands)
                throw new Exception("This Play must not stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 4 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 3)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 3)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
//            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Punter.p_and_r.p.ID).First();
//            if (a_stat.punt_ret_plays != 1 || a_stat.punt_ret != 0 || a_stat.punt_ret_yards != 0)
//                throw new Exception("Player stats not as expected");


            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Punt_Left_Delay_of_Game_From_20()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.PUNT;
            int Down = 4;
            double Yards_to_Go = 7.0;

            Game_Player Punter = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 27
            };
            Game_Player Retuner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 70
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.DG).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                Current_YardLine = 2.0
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 20.0,
                Play_Start_Yardline = 20.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Punt_Returner = Punter,
                Returner = Retuner,
                bPenalty_Rejected = false,
                bRunOutofBounds = true
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = Retuner.p_and_r.p.ID,
                punt_ret_plays = 0,
                punt_ret = 0,
                punt_ret_yards = 0
            });

            double original_Yardline = 20.0;
            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult, bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (r.bPlay_Stands)
                throw new Exception("This Play must not stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 4 || r.Final_yard_to_go != 12 || r.Final_end_of_Play_Yardline != 15)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 5)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            //            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Punter.p_and_r.p.ID).First();
            //            if (a_stat.punt_ret_plays != 1 || a_stat.punt_ret != 0 || a_stat.punt_ret_yards != 0)
            //                throw new Exception("Player stats not as expected");


            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Punt_Left_Defensive_Offsides_Decline_Returner_Fumble_Lost()
        {
            bool penOnBallCarryingTeam = true;
            Play_Enum PE = Play_Enum.PUNT;
            int Down = 4;
            double Yards_to_Go = 7.0;

            Game_Player Punter = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 27
            };
            Game_Player Retuner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 70
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.DO).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                Current_YardLine = 2.0
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 70.0,
                Play_Start_Yardline = 30,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Punter = Punter,
                Punt_Returner = Retuner,
                bPenalty_Rejected = true,
                bRunOutofBounds = false,
                bFumble = true,
                bFumble_Lost = true
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = Retuner.p_and_r.p.ID,
                punt_ret_plays = 1,
                punt_ret = 1,
                punt_ret_yards = 5,
                punt_ret_fumbles = 1,
                punt_ret_fumbles_lost = 1
            });

            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult, bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must not stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 70)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 0)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 1 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Punter.p_and_r.p.ID).First();
            if (a_stat.punt_ret_plays != 1 || a_stat.punt_ret !=1 || a_stat.punt_ret_yards != 5 || a_stat.punt_ret_fumbles != 1 || a_stat.punt_ret_fumbles_lost != 1)
                   throw new Exception("Player stats not as expected");

            Assert.IsTrue(true);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Punt_Left_Defensive_Offsides_Accepted_Not_First_Down_but_Big_Return()
        {
            bool penOnBallCarryingTeam = true;
            Play_Enum PE = Play_Enum.PUNT;
            int Down = 4;
            double Yards_to_Go = 10.0;

            Game_Player Punter = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 40
            };
            Game_Player Retuner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 40
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.DO).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                Current_YardLine = 30.0
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 30.0,
                Play_Start_Yardline = 20,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Punter = Punter,
                Punt_Returner = Retuner,
                bPenalty_Rejected = false,
                bRunOutofBounds = false,
                Yards_Returned = 40
             };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = Retuner.p_and_r.p.ID,
                punt_ret_plays = 1,
                punt_ret = 1,
                punt_ret_yards = 50,
                punt_ret_fumbles = 0,
                punt_ret_fumbles_lost = 0
            });

            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult, bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (r.bPlay_Stands)
                throw new Exception("This Play must not stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 4 || r.Final_yard_to_go != 5 || r.Final_end_of_Play_Yardline != 25)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 5)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
//            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Punter.p_and_r.p.ID).First();
//          if (a_stat.punt_ret_plays != 1 || a_stat.punt_ret != 1 || a_stat.punt_ret_yards != 5 || a_stat.punt_ret_fumbles != 1 || a_stat.punt_ret_fumbles_lost != 1)
//                throw new Exception("Player stats not as expected");

            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Punt_Left_Defensive_Offsides_Accepted_First_Down_but_Big_Return()
        {
            bool penOnBallCarryingTeam = true;
            Play_Enum PE = Play_Enum.PUNT;
            int Down = 4;
            double Yards_to_Go = 3.0;

            Game_Player Punter = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 40
            };
            Game_Player Retuner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 40
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.DO).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                Current_YardLine = 30.0
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 30.0,
                Play_Start_Yardline = 20,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Punter = Punter,
                Punt_Returner = Retuner,
                bPenalty_Rejected = false,
                bRunOutofBounds = false,
                Yards_Returned = 40
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = Retuner.p_and_r.p.ID,
                punt_ret_plays = 1,
                punt_ret = 1,
                punt_ret_yards = 50,
                punt_ret_fumbles = 0,
                punt_ret_fumbles_lost = 0
            });

            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult, bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (r.bPlay_Stands)
                throw new Exception("This Play must not stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 25)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 5)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 1 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            //            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Punter.p_and_r.p.ID).First();
            //          if (a_stat.punt_ret_plays != 1 || a_stat.punt_ret != 1 || a_stat.punt_ret_yards != 5 || a_stat.punt_ret_fumbles != 1 || a_stat.punt_ret_fumbles_lost != 1)
            //                throw new Exception("Player stats not as expected");

            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Punt_Left_Sport_Penalty_on_94_Returned_to_80()
        {
            bool penOnBallCarryingTeam = true;
            Play_Enum PE = Play_Enum.PUNT;
            int Down = 4;
            double Yards_to_Go = 7.0;

            Game_Player Punter = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 27
            };
            Game_Player Retuner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 80
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.UC).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                Current_YardLine = 94.0
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 80.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Punter = Punter,
                Punt_Returner = Retuner,
                bPenalty_Rejected = false,
                bTouchDown = false
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = Retuner.p_and_r.p.ID,
                punt_ret = 1,
                punt_ret_yards = 20,
                punt_ret_TDs = 0
            });

            double original_Yardline = 2.0;
            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult, bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (!r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must not stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 97)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 3)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Punter.p_and_r.p.ID).First();
            if (a_stat.punt_ret != 1 || a_stat.punt_ret_yards != 6 || a_stat.punt_ret_TDs != 0)
                throw new Exception("Player stats not as expected");


            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Punt_Left_Sport_Penalty_on_80_Returned_to_94()
        {
            bool penOnBallCarryingTeam = true;
            Play_Enum PE = Play_Enum.PUNT;
            int Down = 4;
            double Yards_to_Go = 7.0;

            Game_Player Punter = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 27
            };
            Game_Player Retuner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 94
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.UC).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                Current_YardLine = 80.0
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 94.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Punter = Punter,
                Punt_Returner = Retuner,
                bPenalty_Rejected = false,
                bTouchDown = false
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = Retuner.p_and_r.p.ID,
                punt_ret = 1,
                punt_ret_yards = 6,
                punt_ret_TDs = 0
            });

            double original_Yardline = 2.0;
            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult, bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (!r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must not stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 97)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 3)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Punter.p_and_r.p.ID).First();
            if (a_stat.punt_ret != 1 || a_stat.punt_ret_yards != 6 || a_stat.punt_ret_TDs != 0)
                throw new Exception("Player stats not as expected");


            Assert.IsTrue(true);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Punt_Left_Sport_Penalty_on_95_Returned_to_94_Fumble_Lost()
        {
            bool penOnBallCarryingTeam = true;
            Play_Enum PE = Play_Enum.PUNT;
            int Down = 4;
            double Yards_to_Go = 7.0;

            Game_Player Punter = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 27
            };
            Game_Player Retuner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 94
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.UC).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                Current_YardLine = 95.0
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 94.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Punter = Punter,
                Punt_Returner = Retuner,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = true,
                bFumble_Lost = true
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = Retuner.p_and_r.p.ID,
                punt_ret = 1,
                punt_ret_yards = 6,
                punt_ret_TDs = 0,
                punt_ret_fumbles = 1,
                punt_ret_fumbles_lost = 1
            });

            double original_Yardline = 2.0;
            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult, bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must not stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 97.5)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 2.5)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 1 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Punter.p_and_r.p.ID).First();
            if (a_stat.punt_ret != 1 || a_stat.punt_ret_yards != 5 || a_stat.punt_ret_TDs != 0)
                throw new Exception("Player stats not as expected");


            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Punt_Left_Sport_Penalty_on_10_Returned_to_8()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.PUNT;
            int Down = 4;
            double Yards_to_Go = 7.0;

            Game_Player Punter = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 27
            };
            Game_Player Retuner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 8
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.UC).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                Current_YardLine = 10.0
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 8.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Punter = Punter,
                Punt_Returner = Retuner,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = false,
                bFumble_Lost = false
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = Retuner.p_and_r.p.ID,
                punt_ret = 1,
                punt_ret_yards = 92,
                punt_ret_TDs = 0,
                punt_ret_fumbles = 0,
                punt_ret_fumbles_lost = 0
            });

            double original_Yardline = 2.0;
            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult, bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (!r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must not stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 4)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 4)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Punter.p_and_r.p.ID).First();
            if (a_stat.punt_ret != 1 || a_stat.punt_ret_yards != 92 || a_stat.punt_ret_TDs != 0)
                throw new Exception("Player stats not as expected");


            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Punt_Left_Sport_Penalty_on_45_Returned_to_50()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.PUNT;
            int Down = 4;
            double Yards_to_Go = 7.0;

            Game_Player Punter = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 27
            };
            Game_Player Retuner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 8
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.UC).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                Current_YardLine = 45.0
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 50.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Punter = Punter,
                Punt_Returner = Retuner,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = false,
                bFumble_Lost = false,
                Yards_Returned = 50
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = Retuner.p_and_r.p.ID,
                punt_ret = 1,
                punt_ret_yards = 50,
                punt_ret_TDs = 0,
                punt_ret_fumbles = 0,
                punt_ret_fumbles_lost = 0
            });

            double original_Yardline = 2.0;
            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult, bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (!r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must not stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 30)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 15)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Punter.p_and_r.p.ID).First();
            if (a_stat.punt_ret != 1 || a_stat.punt_ret_yards != 50 || a_stat.punt_ret_TDs != 0)
                throw new Exception("Player stats not as expected");

            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Punt_Left_Sport_Penalty_on_95_Defensders_Returned_to_94_Fumble_Lost()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.PUNT;
            int Down = 4;
            double Yards_to_Go = 7.0;

            Game_Player Punter = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 27
            };
            Game_Player Retuner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 94
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.UC).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                Current_YardLine = 95.0
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 94.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Punter = Punter,
                Punt_Returner = Retuner,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = true,
                bFumble_Lost = true
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = Retuner.p_and_r.p.ID,
                punt_ret = 1,
                punt_ret_yards = 6,
                punt_ret_TDs = 0,
                punt_ret_fumbles = 1,
                punt_ret_fumbles_lost = 1
            });

            double original_Yardline = 2.0;
            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult, bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must not stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 79)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 15)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 1 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Punter.p_and_r.p.ID).First();
            if (a_stat.punt_ret != 1 || a_stat.punt_ret_yards != 6 || a_stat.punt_ret_TDs != 0 || a_stat.punt_ret_fumbles != 1 || a_stat.punt_ret_fumbles_lost != 1)
                throw new Exception("Player stats not as expected");


            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Punt_Left_Sport_Penalty_on_15_Defensders_Returned_to_10_Fumble_Lost()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.PUNT;
            int Down = 4;
            double Yards_to_Go = 7.0;

            Game_Player Punter = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 27
            };
            Game_Player Retuner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 10
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.UC).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                Current_YardLine = 15.0
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 10.0,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Punter = Punter,
                Punt_Returner = Retuner,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = true,
                bFumble_Lost = true,
                Yards_Returned = 90
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = Retuner.p_and_r.p.ID,
                punt_ret = 1,
                punt_ret_yards = 90,
                punt_ret_TDs = 0,
                punt_ret_fumbles = 1,
                punt_ret_fumbles_lost = 1
            });

            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult, bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must not stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 5)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 5)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 1 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Punter.p_and_r.p.ID).First();
            if (a_stat.punt_ret != 1 || a_stat.punt_ret_yards != 90 || a_stat.punt_ret_TDs != 0 || a_stat.punt_ret_fumbles != 1 || a_stat.punt_ret_fumbles_lost != 1)
                throw new Exception("Player stats not as expected");


            Assert.IsTrue(true);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Run_Left_1st_and_10_2yardloss()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.RUN;
            int Down = 1;
            double Yards_to_Go = 10.0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 15
            };
            Game_Player RB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 17
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = null;
            Game_Player Penalized_Player = null;

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 18.0,
                Play_Start_Yardline = 20,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Running_Back = RB,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = false,
                bFumble_Lost = false,
                Yards_Gained = -2
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = RB.p_and_r.p.ID,
                off_rush_att = 1,
                off_rush_plays = 1,
                off_rush_Yards = -2,
                off_rush_long = -2,
                off_rush_TDs = 0,
                off_rush_fumbles = 0,
                off_rush_fumbles_lost = 0
            });

            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult, bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must not stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 2 || r.Final_yard_to_go != 12 || r.Final_end_of_Play_Yardline != 18)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 0)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != -2 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
 //           Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Punter.p_and_r.p.ID).First();
 //           if (a_stat.punt_ret != 1 || a_stat.punt_ret_yards != 90 || a_stat.punt_ret_TDs != 0 || a_stat.punt_ret_fumbles != 1 || a_stat.punt_ret_fumbles_lost != 1)
 //               throw new Exception("Player stats not as expected");


            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Pass_Left_1st_and_10_2yardloss()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.PASS;
            int Down = 1;
            double Yards_to_Go = 10.0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 15
            };
            Game_Player Receiver = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 18
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = null;
            Game_Player Penalized_Player = null;

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 18.0,
                Play_Start_Yardline = 20,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = false,
                bFumble_Lost = false,
                Yards_Gained = -2
            };

            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult, bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must not stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 2 || r.Final_yard_to_go != 12 || r.Final_end_of_Play_Yardline != 18)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 0)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != -2 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            //           Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Punter.p_and_r.p.ID).First();
            //           if (a_stat.punt_ret != 1 || a_stat.punt_ret_yards != 90 || a_stat.punt_ret_TDs != 0 || a_stat.punt_ret_fumbles != 1 || a_stat.punt_ret_fumbles_lost != 1)
            //               throw new Exception("Player stats not as expected");


            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Run_Left_1st_and_10_5Yard_Gain()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.RUN;
            int Down = 1;
            double Yards_to_Go = 10.0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 15
            };
            Game_Player RB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 25
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = null;
            Game_Player Penalized_Player = null;

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 25.0,
                Play_Start_Yardline = 20,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Running_Back = RB,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = false,
                bFumble_Lost = false,
                Yards_Gained = 5
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = RB.p_and_r.p.ID,
                off_rush_att = 1,
                off_rush_plays = 1,
                off_rush_Yards = 5,
                off_rush_long = 5,
                off_rush_TDs = 0,
                off_rush_fumbles = 0,
                off_rush_fumbles_lost = 0
            });

            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult, bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must not stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 2 || r.Final_yard_to_go != 5 || r.Final_end_of_Play_Yardline != 25)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 0)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 5 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            //           Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Punter.p_and_r.p.ID).First();
            //           if (a_stat.punt_ret != 1 || a_stat.punt_ret_yards != 90 || a_stat.punt_ret_TDs != 0 || a_stat.punt_ret_fumbles != 1 || a_stat.punt_ret_fumbles_lost != 1)
            //               throw new Exception("Player stats not as expected");


            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Pass_Left_1st_and_10_5_Yard_Gain()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.PASS;
            int Down = 1;
            double Yards_to_Go = 10.0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 15
            };
            Game_Player Receiver = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 25
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = null;
            Game_Player Penalized_Player = null;

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 25.0,
                Play_Start_Yardline = 20,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = false,
                bFumble_Lost = false,
                Yards_Gained = 5
            };

            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult, bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must not stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 2 || r.Final_yard_to_go != 5 || r.Final_end_of_Play_Yardline != 25)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 0)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 5 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            //           Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Punter.p_and_r.p.ID).First();
            //           if (a_stat.punt_ret != 1 || a_stat.punt_ret_yards != 90 || a_stat.punt_ret_TDs != 0 || a_stat.punt_ret_fumbles != 1 || a_stat.punt_ret_fumbles_lost != 1)
            //               throw new Exception("Player stats not as expected");


            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Run_Left_1st_and_10_10Yard_Gain_FirstDown()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.RUN;
            int Down = 1;
            double Yards_to_Go = 10.0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 15
            };
            Game_Player RB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 30
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = null;
            Game_Player Penalized_Player = null;

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 30.0,
                Play_Start_Yardline = 20,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Running_Back = RB,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = false,
                bFumble_Lost = false,
                Yards_Gained = 10
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = RB.p_and_r.p.ID,
                off_rush_att = 1,
                off_rush_plays = 1,
                off_rush_Yards = 10,
                off_rush_long = 10,
                off_rush_TDs = 0,
                off_rush_fumbles = 0,
                off_rush_fumbles_lost = 0
            });

            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult, bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must not stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 30)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 0)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 1 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 10 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            //           Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Punter.p_and_r.p.ID).First();
            //           if (a_stat.punt_ret != 1 || a_stat.punt_ret_yards != 90 || a_stat.punt_ret_TDs != 0 || a_stat.punt_ret_fumbles != 1 || a_stat.punt_ret_fumbles_lost != 1)
            //               throw new Exception("Player stats not as expected");


            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Pass_Left_1st_and_10_10_Yard_Gain_FirstDown()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.PASS;
            int Down = 1;
            double Yards_to_Go = 10.0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 15
            };
            Game_Player Receiver = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 30
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = null;
            Game_Player Penalized_Player = null;

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 30.0,
                Play_Start_Yardline = 20,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = false,
                bFumble_Lost = false,
                Yards_Gained = 10
            };

            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult, bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must not stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 30)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 0)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 1 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 10 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            //           Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Punter.p_and_r.p.ID).First();
            //           if (a_stat.punt_ret != 1 || a_stat.punt_ret_yards != 90 || a_stat.punt_ret_TDs != 0 || a_stat.punt_ret_fumbles != 1 || a_stat.punt_ret_fumbles_lost != 1)
            //               throw new Exception("Player stats not as expected");


            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Run_Left_3rd_and_2_2Yard_Gain_FirstDown_3rdDownConv()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.RUN;
            int Down = 3;
            double Yards_to_Go = 2.0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 25
            };
            Game_Player RB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 30
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = null;
            Game_Player Penalized_Player = null;

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 30.0,
                Play_Start_Yardline = 28,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Running_Back = RB,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = false,
                bFumble_Lost = false,
                Yards_Gained = 2
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = RB.p_and_r.p.ID,
                off_rush_att = 1,
                off_rush_plays = 1,
                off_rush_Yards = 2,
                off_rush_long = 2,
                off_rush_TDs = 0,
                off_rush_fumbles = 0,
                off_rush_fumbles_lost = 0
            });

            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult, bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must not stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 30)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 0)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 1 || r.Away3rdDownAtt != 1 || r.Away3rdDownMade != 1 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 2 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            //           Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Punter.p_and_r.p.ID).First();
            //           if (a_stat.punt_ret != 1 || a_stat.punt_ret_yards != 90 || a_stat.punt_ret_TDs != 0 || a_stat.punt_ret_fumbles != 1 || a_stat.punt_ret_fumbles_lost != 1)
            //               throw new Exception("Player stats not as expected");


            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Pass_Left_3rd_and_2_2_Yard_Gain_FirstDown_3rddownConv()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.PASS;
            int Down = 3;
            double Yards_to_Go = 2.0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 25
            };
            Game_Player Receiver = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 30
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = null;
            Game_Player Penalized_Player = null;

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 30.0,
                Play_Start_Yardline = 28,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = false,
                bFumble_Lost = false,
                Yards_Gained = 2
            };

            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult, bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must not stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 30)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 0)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 1 || r.Away3rdDownAtt != 1 || r.Away3rdDownMade != 1 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 2 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
                    r.AwayTOP != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 || r.HomeSacks != 0 ||
                    r.HomeTOP != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            //           Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Punter.p_and_r.p.ID).First();
            //           if (a_stat.punt_ret != 1 || a_stat.punt_ret_yards != 90 || a_stat.punt_ret_TDs != 0 || a_stat.punt_ret_fumbles != 1 || a_stat.punt_ret_fumbles_lost != 1)
            //               throw new Exception("Player stats not as expected");


            Assert.IsTrue(true);
        }
    }
}
