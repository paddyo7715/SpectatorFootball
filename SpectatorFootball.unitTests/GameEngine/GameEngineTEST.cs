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

            pResult = GameEngine.setScoringBool(pResult, false);

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

            pResult = GameEngine.setScoringBool(pResult, false);

            Assert.IsTrue(pResult.bHomeTD == false && pResult.bHomeFG == false && pResult.bHomeXP == false &&
                pResult.bHomeXP1 == false && pResult.bHomeXP2 == false && pResult.bHomeXP3 == false &&
                pResult.bHomeSafetyFor == false &&
                pResult.bAwayTD == true && pResult.bAwayFG == false && pResult.bAwayXP == false &&
                pResult.bAwayXP1 == false && pResult.bAwayXP2 == false && pResult.bAwayXP3 == false &&
                pResult.bAwaySafetyFor == false);
        }


        [TestCategory("GameEngine")]
        [TestMethod]
        public void setScoringBool_home_team_TD_Turnover()
        {
            Play_Result pResult = new Play_Result()
            { BallPossessing_Team_Id = 22, ht = 11, at = 22, bTouchDown = true };

            pResult = GameEngine.setScoringBool(pResult, true);

            Assert.IsTrue(pResult.bHomeTD == true && pResult.bHomeFG == false && pResult.bHomeXP == false &&
                pResult.bHomeXP1 == false && pResult.bHomeXP2 == false && pResult.bHomeXP3 == false &&
                pResult.bHomeSafetyFor == false &&
                pResult.bAwayTD == false && pResult.bAwayFG == false && pResult.bAwayXP == false &&
                pResult.bAwayXP1 == false && pResult.bAwayXP2 == false && pResult.bAwayXP3 == false &&
                pResult.bAwaySafetyFor == false);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setScoringBool_away_team_TD_Turnover()
        {
            Play_Result pResult = new Play_Result()
            { BallPossessing_Team_Id = 11, ht = 11, at = 22, bTouchDown = true };

            pResult = GameEngine.setScoringBool(pResult, true);

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

            pResult = GameEngine.setScoringBool(pResult, false);

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

            pResult = GameEngine.setScoringBool(pResult, false);

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

            pResult = GameEngine.setScoringBool(pResult, false);

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

            pResult = GameEngine.setScoringBool(pResult, false);

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

            pResult = GameEngine.setScoringBool(pResult, false);

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

            pResult = GameEngine.setScoringBool(pResult, false);

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

            pResult = GameEngine.setScoringBool(pResult, false);

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

            pResult = GameEngine.setScoringBool(pResult, false);

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

            pResult = GameEngine.setScoringBool(pResult, false);

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

            pResult = GameEngine.setScoringBool(pResult, false);

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

            pResult = GameEngine.setScoringBool(pResult, false);

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

            pResult = GameEngine.setScoringBool(pResult, false);

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
        public void setPlayOutCome_NomralKickoff_Left_ReturntoOwn20_no_Penalty_to_20()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.KICKOFF_NORMAL;
            int Down = 0;
            double Yards_to_Go = 0.0;

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
                bPenalty_Rejected = false
            };
            double original_Yardline = 0.0;
            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult, original_Yardline, bLefttoRgiht, TouchBack_Yardline);

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

            if (r.Final_Added_Penalty_Yards > 0)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns > 0 || r.Away3rdDownAtt > 0 || r.Away3rdDownMade > 0 ||
                    r.Away4thDownAtt > 0 || r.Away4thDownMade > 0 || r.AwayXP1Attempt > 0 ||
                    r.AwayXP1Made > 0 || r.AwayXP2Attempt > 0 || r.AwayXP2Made > 0 ||
                    r.AwayXP3Attempt > 0 || r.AwayXP3Made > 0 || r.AwayPassingYards > 0 ||
                    r.AwayRushingYards > 0 || r.AwayTurnoers > 0 || r.AwaySacks > 0 ||
                    r.AwayTOP > 0 ||
                    r.HomeFirstDowns > 0 || r.Home3rdDownAtt > 0 || r.Home3rdDownMade > 0 ||
                    r.Home4thDownAtt > 0 || r.Home4thDownMade > 0 || r.HomeXP1Attempt > 0 ||
                    r.HomeXP1Made > 0 || r.HomeXP2Attempt > 0 || r.HomeXP2Made > 0 ||
                    r.HomeXP3Attempt > 0 || r.HomeXP3Made > 0 || r.HomePassingYards > 0 ||
                    r.HomeRushingYards > 0 || r.HomeTurnoers > 0 || r.HomeSacks > 0 ||
                    r.HomeTOP > 0)
                throw new Exception("None of these should be greater than 0");

            Assert.IsTrue(true);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_NomralKickoff_Left_ReturntoOwn20_Spot_Penalty_on_Returners_to_40()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.KICKOFF_NORMAL;
            int Down = 0;
            double Yards_to_Go = 0.0;

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
                bPenalty_Rejected = false
            };
            double original_Yardline = 0.0;
            bool bLefttoRgiht = true;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult, original_Yardline, bLefttoRgiht, TouchBack_Yardline);

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

            if (r.Final_Added_Penalty_Yards > 15)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns > 0 || r.Away3rdDownAtt > 0 || r.Away3rdDownMade > 0 ||
                    r.Away4thDownAtt > 0 || r.Away4thDownMade > 0 || r.AwayXP1Attempt > 0 ||
                    r.AwayXP1Made > 0 || r.AwayXP2Attempt > 0 || r.AwayXP2Made > 0 ||
                    r.AwayXP3Attempt > 0 || r.AwayXP3Made > 0 || r.AwayPassingYards > 0 ||
                    r.AwayRushingYards > 0 || r.AwayTurnoers > 0 || r.AwaySacks > 0 ||
                    r.AwayTOP > 0 ||
                    r.HomeFirstDowns > 0 || r.Home3rdDownAtt > 0 || r.Home3rdDownMade > 0 ||
                    r.Home4thDownAtt > 0 || r.Home4thDownMade > 0 || r.HomeXP1Attempt > 0 ||
                    r.HomeXP1Made > 0 || r.HomeXP2Attempt > 0 || r.HomeXP2Made > 0 ||
                    r.HomeXP3Attempt > 0 || r.HomeXP3Made > 0 || r.HomePassingYards > 0 ||
                    r.HomeRushingYards > 0 || r.HomeTurnoers > 0 || r.HomeSacks > 0 ||
                    r.HomeTOP > 0)
                throw new Exception("None of these should be greater than 0");



            Assert.IsTrue(true);
        }

    }
}
