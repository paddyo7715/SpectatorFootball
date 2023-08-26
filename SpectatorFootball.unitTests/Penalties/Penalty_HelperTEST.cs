using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpectatorFootball.PenaltiesNS;
using SpectatorFootball.Common;
using SpectatorFootball.Models;
using SpectatorFootball.PlayerNS;
using SpectatorFootball.Enum;
using SpectatorFootball.GameNS;

namespace SpectatorFootball.unitTests.Penalties
{
    [TestClass]
    public class Penalty_HelperTEST
    {
        private List<Penalty> penaltyList = Penalty_Helper.ReturnAllPenalties();

        [TestCategory("Penalties")]
        [TestMethod]
        public void All_Penalties_Returned_Count_Greater_Than_0()
        {
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Assert.IsTrue(p.Count > 0);
        }
        [TestCategory("Penalties")]
        [TestMethod]
        public void getPlayerAction_Passer()
        {
            Game_Player p = new Game_Player();
            Play_Result pResult = new Play_Result();
            pResult.Passer = p;
            Player_Action_State pa = Penalty_Helper.getPlayerAction(p, pResult);
            Assert.IsTrue(pa == Player_Action_State.PAS);
        }
        [TestCategory("Penalties")]
        [TestMethod]
        public void getPlayerAction_Kicker()
        {
            Game_Player p = new Game_Player();
            Play_Result pResult = new Play_Result();
            pResult.Kicker = p;
            Player_Action_State pa = Penalty_Helper.getPlayerAction(p, pResult);
            Assert.IsTrue(pa == Player_Action_State.K);
        }
        [TestCategory("Penalties")]
        [TestMethod]
        public void getPlayerAction_Punter()
        {
            Game_Player p = new Game_Player();
            Play_Result pResult = new Play_Result();
            pResult.Punter = p;
            Player_Action_State pa = Penalty_Helper.getPlayerAction(p, pResult);
            Assert.IsTrue(pa == Player_Action_State.P);
        }
        [TestCategory("Penalties")]
        [TestMethod]
        public void getPlayerAction_Returner()
        {
            Game_Player p = new Game_Player();
            Play_Result pResult = new Play_Result();
            pResult.Returner = p;
            Player_Action_State pa = Penalty_Helper.getPlayerAction(p, pResult);
            Assert.IsTrue(pa == Player_Action_State.KR);
        }
        [TestCategory("Penalties")]
        [TestMethod]
        public void getPlayerAction_PuntReturner()
        {
            Game_Player p = new Game_Player();
            Play_Result pResult = new Play_Result();
            pResult.Punt_Returner = p;
            Player_Action_State pa = Penalty_Helper.getPlayerAction(p, pResult);
            Assert.IsTrue(pa == Player_Action_State.PR);
        }
        [TestCategory("Penalties")]
        [TestMethod]
        public void getPlayerAction_PassCatchers()
        {
            Game_Player p = new Game_Player();
            Play_Result pResult = new Play_Result();
            pResult.Pass_Catchers.Add(p);
            Player_Action_State pa = Penalty_Helper.getPlayerAction(p, pResult);
            Assert.IsTrue(pa == Player_Action_State.PC);
        }
        [TestCategory("Penalties")]
        [TestMethod]
        public void getPlayerAction_BallRunners()
        {
            Game_Player p = new Game_Player();
            Play_Result pResult = new Play_Result();
            pResult.Ball_Runners.Add(p);
            Player_Action_State pa = Penalty_Helper.getPlayerAction(p, pResult);
            Assert.IsTrue(pa == Player_Action_State.BRN);
        }
        [TestCategory("Penalties")]
        [TestMethod]
        public void getPlayerAction_PassBlockers()
        {
            Game_Player p = new Game_Player();
            Play_Result pResult = new Play_Result();
            pResult.Pass_Blockers.Add(p);
            Player_Action_State pa = Penalty_Helper.getPlayerAction(p, pResult);
            Assert.IsTrue(pa == Player_Action_State.PB);
        }
        [TestCategory("Penalties")]
        [TestMethod]
        public void getPlayerAction_PassRushers()
        {
            Game_Player p = new Game_Player();
            Play_Result pResult = new Play_Result();
            pResult.Pass_Rushers.Add(p);
            Player_Action_State pa = Penalty_Helper.getPlayerAction(p, pResult);
            Assert.IsTrue(pa == Player_Action_State.PAR);
        }
        [TestCategory("Penalties")]
        [TestMethod]
        public void getPlayerAction_PassDefenders()
        {
            Game_Player p = new Game_Player();
            Play_Result pResult = new Play_Result();
            pResult.Pass_Defenders.Add(p);
            Player_Action_State pa = Penalty_Helper.getPlayerAction(p, pResult);
            Assert.IsTrue(pa == Player_Action_State.PD);
        }
        [TestCategory("Penalties")]
        [TestMethod]
        public void getPlayerAction_RunDefenders()
        {
            Game_Player p = new Game_Player();
            Play_Result pResult = new Play_Result();
            pResult.Run_Defenders.Add(p);
            Player_Action_State pa = Penalty_Helper.getPlayerAction(p, pResult);
            Assert.IsTrue(pa == Player_Action_State.RD);
        }
        [TestCategory("Penalties")]
        [TestMethod]
        public void getPlayerAction_KickReturners()
        {
            Game_Player p = new Game_Player();
            Play_Result pResult = new Play_Result();
            pResult.Kick_Returners.Add(p);
            Player_Action_State pa = Penalty_Helper.getPlayerAction(p, pResult);
            Assert.IsTrue(pa == Player_Action_State.KRT);
        }
        [TestCategory("Penalties")]
        [TestMethod]
        public void getPlayerAction_KickDefenders()
        {
            Game_Player p = new Game_Player();
            Play_Result pResult = new Play_Result();
            pResult.Kick_Defenders.Add(p);
            Player_Action_State pa = Penalty_Helper.getPlayerAction(p, pResult);
            Assert.IsTrue(pa == Player_Action_State.KDT);
        }
        [TestCategory("Penalties")]
        [TestMethod]
        public void getPlayerAction_PuntReturners()
        {
            Game_Player p = new Game_Player();
            Play_Result pResult = new Play_Result();
            pResult.Punt_Returners.Add(p);
            Player_Action_State pa = Penalty_Helper.getPlayerAction(p, pResult);
            Assert.IsTrue(pa == Player_Action_State.PRT);
        }
        [TestCategory("Penalties")]
        [TestMethod]
        public void getPlayerAction_Field_Goal_Kicking_Team()
        {
            Game_Player p = new Game_Player();
            Play_Result pResult = new Play_Result();
            pResult.FieldGaol_Kicking_Team.Add(p);
            Player_Action_State pa = Penalty_Helper.getPlayerAction(p, pResult);
            Assert.IsTrue(pa == Player_Action_State.FGT);
        }
        [TestCategory("Penalties")]
        [TestMethod]
        public void getPlayerAction_Field_Goal_Defenders()
        {
            Game_Player p = new Game_Player();
            Play_Result pResult = new Play_Result();
            pResult.Field_Goal_Defenders.Add(p);
            Player_Action_State pa = Penalty_Helper.getPlayerAction(p, pResult);
            Assert.IsTrue(pa == Player_Action_State.FGD);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void getPenalty_Kickoff_Kickoff_Return_Team()
        {
            int Num_Tries = 300;
            int KIB = 0;
            int UC = 0;
            int UR = 0;
            int FM = 0;

            for (int i = 0; i < Num_Tries; i++)
            {
                int n = CommonUtils.getRandomNum(1, 3);
                Play_Enum pe = Play_Enum.KICKOFF_NORMAL;
                switch (n)
                {
                    case 1:
                        pe = Play_Enum.KICKOFF_NORMAL;
                        break;
                    case 2:
                        pe = Play_Enum.FREE_KICK;
                        break;
                    case 3:
                        pe = Play_Enum.KICKOFF_ONSIDES;
                        break;
                }

                Penalty p = Penalty_Helper.getPenalty(this.penaltyList, pe, Player_Action_State.KRT);

                if (p.code == Penalty_Codes.KIB)
                    KIB++;
                else if (p.code == Penalty_Codes.UC)
                    UC++;
                else if (p.code == Penalty_Codes.UR)
                    UR++;
                else if (p.code == Penalty_Codes.FM)
                    FM++;
                else
                    throw new Exception("Incorrect Penalty Code");
            }

            Assert.IsTrue(KIB > 0 && UC > 0 && UR > 0 && FM > 0);
        }


        [TestCategory("Penalties")]
        [TestMethod]
        public void getPenalty_Kickoff_Kickoff_Returner()
        {
            int Num_Tries = 300;
            int UC = 0;
            int UR = 0;
            int FM = 0;

            for (int i = 0; i < Num_Tries; i++)
            {
                int n = CommonUtils.getRandomNum(1, 3);
                Play_Enum pe = Play_Enum.KICKOFF_NORMAL;
                switch (n)
                {
                    case 1:
                        pe = Play_Enum.KICKOFF_NORMAL;
                        break;
                    case 2:
                        pe = Play_Enum.FREE_KICK;
                        break;
                    case 3:
                        pe = Play_Enum.KICKOFF_ONSIDES;
                        break;
                }

                Penalty p = Penalty_Helper.getPenalty(this.penaltyList, pe, Player_Action_State.KR);

                if (p.code == Penalty_Codes.UC)
                    UC++;
                else if (p.code == Penalty_Codes.UR)
                    UR++;
                else if (p.code == Penalty_Codes.FM)
                    FM++;
                else
                    throw new Exception("Incorrect Penalty Code");
            }

            Assert.IsTrue(UC > 0 && UR > 0 && FM > 0);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void getPenalty_Kickoff_Kickoff_Kicker()
        {
            int Num_Tries = 300;
            int UC = 0;
            int UR = 0;
            int FM = 0;

            for (int i = 0; i < Num_Tries; i++)
            {
                int n = CommonUtils.getRandomNum(1, 3);
                Play_Enum pe = Play_Enum.KICKOFF_NORMAL;
                switch (n)
                {
                    case 1:
                        pe = Play_Enum.KICKOFF_NORMAL;
                        break;
                    case 2:
                        pe = Play_Enum.FREE_KICK;
                        break;
                    case 3:
                        pe = Play_Enum.KICKOFF_ONSIDES;
                        break;
                }

                Penalty p = Penalty_Helper.getPenalty(this.penaltyList, pe, Player_Action_State.K);
                if (p.code == Penalty_Codes.UC)
                    UC++;
                else if (p.code == Penalty_Codes.UR)
                    UR++;
                else if (p.code == Penalty_Codes.FM)
                    FM++;
                else
                    throw new Exception("Incorrect Penalty Code");
            }

            Assert.IsTrue(UC > 0 && UR > 0 && FM > 0);
        }


        [TestCategory("Penalties")]
        [TestMethod]
        public void getPenalty_Kickoff_Kickoff_Defense()
        {
            int Num_Tries = 300;
            int UC = 0;
            int UR = 0;
            int FM = 0;

            for (int i = 0; i < Num_Tries; i++)
            {
                int n = CommonUtils.getRandomNum(1, 3);
                Play_Enum pe = Play_Enum.KICKOFF_NORMAL;
                switch (n)
                {
                    case 1:
                        pe = Play_Enum.KICKOFF_NORMAL;
                        break;
                    case 2:
                        pe = Play_Enum.FREE_KICK;
                        break;
                    case 3:
                        pe = Play_Enum.KICKOFF_ONSIDES;
                        break;
                }

                Penalty p = Penalty_Helper.getPenalty(this.penaltyList, pe, Player_Action_State.KDT);
                if (p.code == Penalty_Codes.UC)
                    UC++;
                else if (p.code == Penalty_Codes.UR)
                    UR++;
                else if (p.code == Penalty_Codes.FM)
                    FM++;
                else
                    throw new Exception("Incorrect Penalty Code");
            }

            Assert.IsTrue(UC > 0 && UR > 0 && FM > 0);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void getPenalty_Punt_Return_Team()
        {
            int Num_Tries = 300;
            int PIB = 0;
            int UC = 0;
            int UR = 0;
            int FM = 0;

            for (int i = 0; i < Num_Tries; i++)
            {
                Play_Enum pe = Play_Enum.PUNT;

                Penalty p = Penalty_Helper.getPenalty(this.penaltyList, pe, Player_Action_State.PRT);
                if (p.code == Penalty_Codes.PIB)
                    PIB++;
                else if (p.code == Penalty_Codes.UC)
                    UC++;
                else if (p.code == Penalty_Codes.UR)
                    UR++;
                else if (p.code == Penalty_Codes.FM)
                    FM++;
                else
                    throw new Exception("Incorrect Penalty Code");
            }

            Assert.IsTrue(PIB > 0 && UC > 0 && UR > 0 && FM > 0);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void getPenalty_Punt_Retuner()
        {
            int Num_Tries = 300;
            int UC = 0;
            int UR = 0;
            int FM = 0;

            for (int i = 0; i < Num_Tries; i++)
            {
                Play_Enum pe = Play_Enum.PUNT;

                Penalty p = Penalty_Helper.getPenalty(this.penaltyList, pe, Player_Action_State.PR);
                if (p.code == Penalty_Codes.UC)
                    UC++;
                else if (p.code == Penalty_Codes.UR)
                    UR++;
                else if (p.code == Penalty_Codes.FM)
                    FM++;
                else
                    throw new Exception("Incorrect Penalty Code");
            }

            Assert.IsTrue(UC > 0 && UR > 0 && FM > 0);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void getPenalty_Punt_Tackling_Team()
        {
            int Num_Tries = 300;
            int UC = 0;
            int UR = 0;
            int FM = 0;

            for (int i = 0; i < Num_Tries; i++)
            {
                Play_Enum pe = Play_Enum.PUNT;

                Penalty p = Penalty_Helper.getPenalty(this.penaltyList, pe, Player_Action_State.PDT);
                if (p.code == Penalty_Codes.UC)
                    UC++;
                else if (p.code == Penalty_Codes.UR)
                    UR++;
                else if (p.code == Penalty_Codes.FM)
                    FM++;
                else
                    throw new Exception("Incorrect Penalty Code");
            }

            Assert.IsTrue(UC > 0 && UR > 0 && FM > 0);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void getPenalty_FG_FG_Team()
        {
            int Num_Tries = 300;
            int OO = 0;
            int UC = 0;
            int UR = 0;
            int FM = 0;

            for (int i = 0; i < Num_Tries; i++)
            {
                int n = CommonUtils.getRandomNum(1, 2);
                Play_Enum pe = Play_Enum.KICKOFF_NORMAL;
                switch (n)
                {
                    case 1:
                        pe = Play_Enum.FIELD_GOAL;
                        break;
                    case 2:
                        pe = Play_Enum.EXTRA_POINT;
                        break;
                }

                Penalty p = Penalty_Helper.getPenalty(this.penaltyList, pe, Player_Action_State.FGT);
                if (p.code == Penalty_Codes.OO)
                    OO++;
                else if (p.code == Penalty_Codes.UC)
                    UC++;
                else if (p.code == Penalty_Codes.UR)
                    UR++;
                else if (p.code == Penalty_Codes.FM)
                    FM++;
                else
                    throw new Exception("Incorrect Penalty Code");
            }

            Assert.IsTrue(OO > 0 && UC > 0 && UR > 0 && FM > 0);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void getPenalty_FG_Kicker()
        {
            int Num_Tries = 300;
            int UC = 0;
            int UR = 0;
            int FM = 0;

            for (int i = 0; i < Num_Tries; i++)
            {
                int n = CommonUtils.getRandomNum(1, 2);
                Play_Enum pe = Play_Enum.KICKOFF_NORMAL;
                switch (n)
                {
                    case 1:
                        pe = Play_Enum.FIELD_GOAL;
                        break;
                    case 2:
                        pe = Play_Enum.EXTRA_POINT;
                        break;
                }

                Penalty p = Penalty_Helper.getPenalty(this.penaltyList, pe, Player_Action_State.K);
                if (p.code == Penalty_Codes.UC)
                    UC++;
                else if (p.code == Penalty_Codes.UR)
                    UR++;
                else if (p.code == Penalty_Codes.FM)
                    FM++;
                else
                    throw new Exception("Incorrect Penalty Code");
            }

            Assert.IsTrue(UC > 0 && UR > 0 && FM > 0);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void getPenalty_FG_FG_Defense()
        {
            int Num_Tries = 300;
            int DO = 0;
            int UC = 0;
            int UR = 0;
            int FM = 0;

            for (int i = 0; i < Num_Tries; i++)
            {
                int n = CommonUtils.getRandomNum(1, 2);
                Play_Enum pe = Play_Enum.KICKOFF_NORMAL;
                switch (n)
                {
                    case 1:
                        pe = Play_Enum.FIELD_GOAL;
                        break;
                    case 2:
                        pe = Play_Enum.EXTRA_POINT;
                        break;
                }

                Penalty p = Penalty_Helper.getPenalty(this.penaltyList, pe, Player_Action_State.FGD);
                if (p.code == Penalty_Codes.DO)
                    DO++;
                else if (p.code == Penalty_Codes.UC)
                    UC++;
                else if (p.code == Penalty_Codes.UR)
                    UR++;
                else if (p.code == Penalty_Codes.FM)
                    FM++;
                else
                    throw new Exception("Incorrect Penalty Code");
            }

            Assert.IsTrue(DO > 0 && UC > 0 && UR > 0 && FM > 0);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void getPenalty_Run_Offense_Play_Passer()
        {
            bool bWrong = false;
            int Num_Tries = 300;

            for (int i = 0; i < Num_Tries; i++)
            {
                int n = CommonUtils.getRandomNum(1, 3);
                Play_Enum pe = Play_Enum.RUN;
                switch (n)
                {
                    case 1:
                        pe = Play_Enum.RUN;
                        break;
                    case 2:
                        pe = Play_Enum.SCRIM_PLAY_1XP_RUN;
                        break;
                    case 3:
                        pe = Play_Enum.SCRIM_PLAY_2XP_RUN;
                        break;
                    case 4:
                        pe = Play_Enum.SCRIM_PLAY_3XP_RUN;
                        break;
                }

                Penalty p = Penalty_Helper.getPenalty(this.penaltyList, pe, Player_Action_State.PAS);
                if (p.code != Penalty_Codes.FS && p.code != Penalty_Codes.UC && p.code != Penalty_Codes.UR && p.code != Penalty_Codes.FM)
                    bWrong = true;
            }

            Assert.IsTrue(!bWrong);
        }
        [TestCategory("Penalties")]
        [TestMethod]
        public void getPenalty_Run_Offense_Play_Ball_Runners()
        {
            bool bWrong = false;
            int Num_Tries = 300;

            for (int i = 0; i < Num_Tries; i++)
            {
                int n = CommonUtils.getRandomNum(1, 3);
                Play_Enum pe = Play_Enum.RUN;
                switch (n)
                {
                    case 1:
                        pe = Play_Enum.RUN;
                        break;
                    case 2:
                        pe = Play_Enum.SCRIM_PLAY_1XP_RUN;
                        break;
                    case 3:
                        pe = Play_Enum.SCRIM_PLAY_2XP_RUN;
                        break;
                    case 4:
                        pe = Play_Enum.SCRIM_PLAY_3XP_RUN;
                        break;
                }

                Penalty p = Penalty_Helper.getPenalty(this.penaltyList, pe, Player_Action_State.BRN);
                if (p.code != Penalty_Codes.IHO && p.code != Penalty_Codes.FS && p.code != Penalty_Codes.UC && p.code != Penalty_Codes.UR && p.code != Penalty_Codes.FM)
                    bWrong = true;
            }

            Assert.IsTrue(!bWrong);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void getPenalty_Run_Offense_Play_Pass_Catchers()
        {
            bool bWrong = false;
            int Num_Tries = 300;

            for (int i = 0; i < Num_Tries; i++)
            {
                int n = CommonUtils.getRandomNum(1, 3);
                Play_Enum pe = Play_Enum.RUN;
                switch (n)
                {
                    case 1:
                        pe = Play_Enum.RUN;
                        break;
                    case 2:
                        pe = Play_Enum.SCRIM_PLAY_1XP_RUN;
                        break;
                    case 3:
                        pe = Play_Enum.SCRIM_PLAY_2XP_RUN;
                        break;
                    case 4:
                        pe = Play_Enum.SCRIM_PLAY_3XP_RUN;
                        break;
                }

                Penalty p = Penalty_Helper.getPenalty(this.penaltyList, pe, Player_Action_State.PC);
                if (p.code != Penalty_Codes.IHO && p.code != Penalty_Codes.FS && p.code != Penalty_Codes.UC && p.code != Penalty_Codes.UR && p.code != Penalty_Codes.FM)
                    bWrong = true;
            }

            Assert.IsTrue(!bWrong);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void getPenalty_Run_Offense_Play_Rush_Blockers()
        {
            bool bWrong = false;
            int Num_Tries = 300;

            for (int i = 0; i < Num_Tries; i++)
            {
                int n = CommonUtils.getRandomNum(1, 3);
                Play_Enum pe = Play_Enum.RUN;
                switch (n)
                {
                    case 1:
                        pe = Play_Enum.RUN;
                        break;
                    case 2:
                        pe = Play_Enum.SCRIM_PLAY_1XP_RUN;
                        break;
                    case 3:
                        pe = Play_Enum.SCRIM_PLAY_2XP_RUN;
                        break;
                    case 4:
                        pe = Play_Enum.SCRIM_PLAY_3XP_RUN;
                        break;
                }

                Penalty p = Penalty_Helper.getPenalty(this.penaltyList, pe, Player_Action_State.RB);
                if (p.code != Penalty_Codes.IHO && p.code != Penalty_Codes.OO && p.code != Penalty_Codes.IF && p.code != Penalty_Codes.FS && p.code != Penalty_Codes.UC && p.code != Penalty_Codes.UR && p.code != Penalty_Codes.FM)
                    bWrong = true;
            }

            Assert.IsTrue(!bWrong);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void getPenalty_Run_Offense_Play_Rush_Defenders()
        {
            bool bWrong = false;
            int Num_Tries = 300;

            for (int i = 0; i < Num_Tries; i++)
            {
                int n = CommonUtils.getRandomNum(1, 3);
                Play_Enum pe = Play_Enum.RUN;
                switch (n)
                {
                    case 1:
                        pe = Play_Enum.RUN;
                        break;
                    case 2:
                        pe = Play_Enum.SCRIM_PLAY_1XP_RUN;
                        break;
                    case 3:
                        pe = Play_Enum.SCRIM_PLAY_2XP_RUN;
                        break;
                    case 4:
                        pe = Play_Enum.SCRIM_PLAY_3XP_RUN;
                        break;
                }

                Penalty p = Penalty_Helper.getPenalty(this.penaltyList, pe, Player_Action_State.RD);
                if (p.code != Penalty_Codes.DH && p.code != Penalty_Codes.DO && p.code != Penalty_Codes.IH && p.code != Penalty_Codes.IF && p.code != Penalty_Codes.FS && p.code != Penalty_Codes.UC && p.code != Penalty_Codes.UR && p.code != Penalty_Codes.FM)
                    bWrong = true;
            }

            Assert.IsTrue(!bWrong);
        }



    }
}
