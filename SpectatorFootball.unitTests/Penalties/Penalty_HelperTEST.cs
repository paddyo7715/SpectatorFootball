using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpectatorFootball.PenaltiesNS;
using SpectatorFootball.Common;
using SpectatorFootball.Models;
using SpectatorFootball.PlayerNS;
using SpectatorFootball.Enum;
using SpectatorFootball.GameNS;
using System.Linq;

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
                int n = CommonUtils.getRandomNum(1, 2);
                Play_Enum pe = Play_Enum.KICKOFF_NORMAL;
                switch (n)
                {
                    case 1:
                        pe = Play_Enum.KICKOFF_NORMAL;
                        break;
                    case 2:
                        pe = Play_Enum.FREE_KICK;
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
                int n = CommonUtils.getRandomNum(1, 2);
                Play_Enum pe = Play_Enum.KICKOFF_NORMAL;
                switch (n)
                {
                    case 1:
                        pe = Play_Enum.KICKOFF_NORMAL;
                        break;
                    case 2:
                        pe = Play_Enum.FREE_KICK;
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
                int n = CommonUtils.getRandomNum(1, 2);
                Play_Enum pe = Play_Enum.KICKOFF_NORMAL;
                switch (n)
                {
                    case 1:
                        pe = Play_Enum.KICKOFF_NORMAL;
                        break;
                    case 2:
                        pe = Play_Enum.FREE_KICK;
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
                int n = CommonUtils.getRandomNum(1, 2);
                Play_Enum pe = Play_Enum.KICKOFF_NORMAL;
                switch (n)
                {
                    case 1:
                        pe = Play_Enum.KICKOFF_NORMAL;
                        break;
                    case 2:
                        pe = Play_Enum.FREE_KICK;
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
        public void getPenalty_Onside_kickoff_Returner()
        {
            int Num_Tries = 300;
            int UC = 0;
            int UR = 0;
            int FM = 0;

            for (int i = 0; i < Num_Tries; i++)
            {
                Play_Enum pe = Play_Enum.KICKOFF_ONSIDES;
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
        public void getPenalty_Onside_Kickoff_Kicker()
        {
            int Num_Tries = 300;
            int UC = 0;
            int UR = 0;
            int FM = 0;

            for (int i = 0; i < Num_Tries; i++)
            {
                Play_Enum pe = Play_Enum.KICKOFF_NORMAL;
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
        public void getPenalty_Onside_Kickoff_Defense()
        {
            int Num_Tries = 300;
            int UC = 0;
            int UR = 0;
            int FM = 0;

            for (int i = 0; i < Num_Tries; i++)
            {
                Play_Enum pe = Play_Enum.KICKOFF_ONSIDES;
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
            int RK = 0;
            int RIK = 0;

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
                else if (p.code == Penalty_Codes.RK)
                    RK++;
                else if (p.code == Penalty_Codes.RIK)
                    RIK++;
                else
                    throw new Exception("Incorrect Penalty Code");
            }

            Assert.IsTrue(RIK > 0 && RK > 0 && PIB > 0 && UC > 0 && UR > 0 && FM > 0);
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
            int UC = 0;
            int UR = 0;
            int FM = 0;
 
            for (int i = 0; i < Num_Tries; i++)
            {
                int n = CommonUtils.getRandomNum(1, 2);
                Play_Enum pe = Play_Enum.FIELD_GOAL;
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
        public void getPenalty_Run_Offense_Play_Passer()
        {
            int Num_Tries = 300;
            int UC = 0;
            int UR = 0;
            int FM = 0;

            for (int i = 0; i < Num_Tries; i++)
            {
                int n = CommonUtils.getRandomNum(1, 4);
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
        public void getPenalty_Run_Offense_Play_Ball_Runners()
        {
            int Num_Tries = 300;
            int UC = 0;
            int UR = 0;
            int FM = 0;
            int IHO = 0;

            for (int i = 0; i < Num_Tries; i++)
            {
                int n = CommonUtils.getRandomNum(1, 4);
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
                if (p.code == Penalty_Codes.UC)
                    UC++;
                else if (p.code == Penalty_Codes.UR)
                    UR++;
                else if (p.code == Penalty_Codes.FM)
                    FM++;
                else if (p.code == Penalty_Codes.IHO)
                    IHO++;
                else
                    throw new Exception("Incorrect Penalty Code");
            }

            Assert.IsTrue(IHO > 0 && UC > 0 && UR > 0 && FM > 0);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void getPenalty_Run_Offense_Play_Pass_Catchers()
        {
            int Num_Tries = 300;
            int UC = 0;
            int UR = 0;
            int FM = 0;
            int IHO = 0;

            for (int i = 0; i < Num_Tries; i++)
            {
                int n = CommonUtils.getRandomNum(1, 4);
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
                if (p.code == Penalty_Codes.UC)
                    UC++;
                else if (p.code == Penalty_Codes.UR)
                    UR++;
                else if (p.code == Penalty_Codes.FM)
                    FM++;
                else if (p.code == Penalty_Codes.IHO)
                    IHO++;
                else
                    throw new Exception("Incorrect Penalty Code");
            }

            Assert.IsTrue(IHO > 0 && UC > 0 && UR > 0 && FM > 0);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void getPenalty_Run_Offense_Play_Rush_Blockers()
        {
            int Num_Tries = 300;
             int IHO = 0;
            int UC = 0;
            int UR = 0;
            int FM = 0;

            for (int i = 0; i < Num_Tries; i++)
            {
                int n = CommonUtils.getRandomNum(1, 4);
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
                if (p.code == Penalty_Codes.UC)
                    UC++;
                else if (p.code == Penalty_Codes.UR)
                    UR++;
                else if (p.code == Penalty_Codes.FM)
                    FM++;
                else if (p.code == Penalty_Codes.IHO)
                    IHO++;
                else
                    throw new Exception("Incorrect Penalty Code");
            }

            Assert.IsTrue(IHO > 0 && UC > 0 && UR > 0 && FM > 0);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void getPenalty_Run_Offense_Play_Rush_Defenders()
        {
            int Num_Tries = 300;
             int IH = 0;
            int UC = 0;
            int UR = 0;
            int FM = 0;

            for (int i = 0; i < Num_Tries; i++)
            {
                int n = CommonUtils.getRandomNum(1, 4);
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
                if (p.code == Penalty_Codes.UC)
                    UC++;
                else if (p.code == Penalty_Codes.UR)
                    UR++;
                else if (p.code == Penalty_Codes.FM)
                    FM++;
                else if (p.code == Penalty_Codes.IH)
                    IH++;
                else
                    throw new Exception("Incorrect Penalty Code");
            }

            Assert.IsTrue(IH > 0 && UC > 0 && UR > 0 && FM > 0);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void getPenalty_Run_Offense_Play_Pass_Defenders()
        {
            int Num_Tries = 300;
               int UC = 0;
            int UR = 0;
            int FM = 0;

            for (int i = 0; i < Num_Tries; i++)
            {
                int n = CommonUtils.getRandomNum(1, 4);
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

                Penalty p = Penalty_Helper.getPenalty(this.penaltyList, pe, Player_Action_State.PD);
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
        public void getPenalty_Pass_Offense_Play_Passer()
        {
            int Num_Tries = 300;
            int UC = 0;
            int UR = 0;
            int FM = 0;

            for (int i = 0; i < Num_Tries; i++)
            {
                int n = CommonUtils.getRandomNum(1, 4);
                Play_Enum pe = Play_Enum.PASS;
                switch (n)
                {
                    case 1:
                        pe = Play_Enum.PASS;
                        break;
                    case 2:
                        pe = Play_Enum.SCRIM_PLAY_1XP_PASS;
                        break;
                    case 3:
                        pe = Play_Enum.SCRIM_PLAY_2XP_PASS;
                        break;
                    case 4:
                        pe = Play_Enum.SCRIM_PLAY_3XP_PASS;
                        break;
                }

                Penalty p = Penalty_Helper.getPenalty(this.penaltyList, pe, Player_Action_State.PAS);
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
        public void getPenalty_Pass_Offense_Play_Ball_Runners()
        {
            int Num_Tries = 300;
            int UC = 0;
            int UR = 0;
            int FM = 0;
            int IHO = 0;

            for (int i = 0; i < Num_Tries; i++)
            {
                int n = CommonUtils.getRandomNum(1, 4);
                Play_Enum pe = Play_Enum.PASS;
                switch (n)
                {
                    case 1:
                        pe = Play_Enum.PASS;
                        break;
                    case 2:
                        pe = Play_Enum.SCRIM_PLAY_1XP_PASS;
                        break;
                    case 3:
                        pe = Play_Enum.SCRIM_PLAY_2XP_PASS;
                        break;
                    case 4:
                        pe = Play_Enum.SCRIM_PLAY_3XP_PASS;
                        break;
                }

                Penalty p = Penalty_Helper.getPenalty(this.penaltyList, pe, Player_Action_State.BRN);
                if (p.code == Penalty_Codes.UC)
                    UC++;
                else if (p.code == Penalty_Codes.UR)
                    UR++;
                else if (p.code == Penalty_Codes.FM)
                    FM++;
                else if (p.code == Penalty_Codes.IHO)
                    IHO++;
                else
                    throw new Exception("Incorrect Penalty Code");
            }

            Assert.IsTrue(IHO > 0 && UC > 0 && UR > 0 && FM > 0);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void getPenalty_Pass_Offense_Play_Pass_Catchers()
        {
            int Num_Tries = 300;
            int UC = 0;
            int UR = 0;
            int FM = 0;
            int IHO = 0;
            int OI = 0;

            for (int i = 0; i < Num_Tries; i++)
            {
                int n = CommonUtils.getRandomNum(1, 4);
                Play_Enum pe = Play_Enum.PASS;
                switch (n)
                {
                    case 1:
                        pe = Play_Enum.PASS;
                        break;
                    case 2:
                        pe = Play_Enum.SCRIM_PLAY_1XP_PASS;
                        break;
                    case 3:
                        pe = Play_Enum.SCRIM_PLAY_2XP_PASS;
                        break;
                    case 4:
                        pe = Play_Enum.SCRIM_PLAY_3XP_PASS;
                        break;
                }

                Penalty p = Penalty_Helper.getPenalty(this.penaltyList, pe, Player_Action_State.PC);
                if (p.code == Penalty_Codes.UC)
                    UC++;
                else if (p.code == Penalty_Codes.UR)
                    UR++;
                else if (p.code == Penalty_Codes.FM)
                    FM++;
                else if (p.code == Penalty_Codes.IHO)
                    IHO++;
                else if (p.code == Penalty_Codes.OI)
                    OI++;
                else
                    throw new Exception("Incorrect Penalty Code");
            }

            Assert.IsTrue(OI > 0 && IHO > 0 && UC > 0 && UR > 0 && FM > 0);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void getPenalty_Pass_Offense_Play_Pass_Blockers()
        {
            int Num_Tries = 300;
            int IHO = 0;
            int UC = 0;
            int UR = 0;
            int FM = 0;
            int OH = 0;
            int IR = 0;
            int IB = 0;

            for (int i = 0; i < Num_Tries; i++)
            {
                int n = CommonUtils.getRandomNum(1, 4);
                Play_Enum pe = Play_Enum.PASS;
                switch (n)
                {
                    case 1:
                        pe = Play_Enum.PASS;
                        break;
                    case 2:
                        pe = Play_Enum.SCRIM_PLAY_1XP_PASS;
                        break;
                    case 3:
                        pe = Play_Enum.SCRIM_PLAY_2XP_PASS;
                        break;
                    case 4:
                        pe = Play_Enum.SCRIM_PLAY_3XP_PASS;
                        break;
                }

                Penalty p = Penalty_Helper.getPenalty(this.penaltyList, pe, Player_Action_State.PB);
                if (p.code == Penalty_Codes.UC)
                    UC++;
                else if (p.code == Penalty_Codes.UR)
                    UR++;
                else if (p.code == Penalty_Codes.FM)
                    FM++;
                else if (p.code == Penalty_Codes.IHO)
                    IHO++;
                else if (p.code == Penalty_Codes.OH)
                    OH++;
                else if (p.code == Penalty_Codes.IR)
                    IR++;
                else if (p.code == Penalty_Codes.IB)
                    IB++;
                else
                    throw new Exception("Incorrect Penalty Code");
            }

            Assert.IsTrue(IB > 0 && IR > 0 && OH > 0 && IHO > 0 && UC > 0 && UR > 0 && FM > 0);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void getPenalty_Pass_Offense_Play_Pass_Rushers()
        {
            int Num_Tries = 300;
            int DH = 0;
            int IH = 0;
            int UC = 0;
            int UR = 0;
            int FM = 0;
            int RD = 0;

            for (int i = 0; i < Num_Tries; i++)
            {
                int n = CommonUtils.getRandomNum(1, 4);
                Play_Enum pe = Play_Enum.RUN;
                switch (n)
                {
                    case 1:
                        pe = Play_Enum.PASS;
                        break;
                    case 2:
                        pe = Play_Enum.SCRIM_PLAY_1XP_PASS;
                        break;
                    case 3:
                        pe = Play_Enum.SCRIM_PLAY_2XP_PASS;
                        break;
                    case 4:
                        pe = Play_Enum.SCRIM_PLAY_3XP_PASS;
                        break;
                }

                Penalty p = Penalty_Helper.getPenalty(this.penaltyList, pe, Player_Action_State.PAR);
                if (p.code == Penalty_Codes.UC)
                    UC++;
                else if (p.code == Penalty_Codes.UR)
                    UR++;
                else if (p.code == Penalty_Codes.FM)
                    FM++;
                else if (p.code == Penalty_Codes.IH)
                    IH++;
                else if (p.code == Penalty_Codes.DH)
                    DH++;
                else if (p.code == Penalty_Codes.RD)
                    RD++;
                else
                    throw new Exception("Incorrect Penalty Code");
            }

            Assert.IsTrue(RD > 0 && DH > 0 && IH > 0 && UC > 0 && UR > 0 && FM > 0);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void getPenalty_Pass_Offense_Play_Pass_Defenders()
        {
            int Num_Tries = 300;
            int DH = 0;
            int IC = 0;
            int UC = 0;
            int UR = 0;
            int FM = 0;
            int PI = 0;

            for (int i = 0; i < Num_Tries; i++)
            {
                int n = CommonUtils.getRandomNum(1, 4);
                Play_Enum pe = Play_Enum.PASS;
                switch (n)
                {
                    case 1:
                        pe = Play_Enum.PASS;
                        break;
                    case 2:
                        pe = Play_Enum.SCRIM_PLAY_1XP_PASS;
                        break;
                    case 3:
                        pe = Play_Enum.SCRIM_PLAY_2XP_PASS;
                        break;
                    case 4:
                        pe = Play_Enum.SCRIM_PLAY_3XP_PASS;
                        break;
                }

                Penalty p = Penalty_Helper.getPenalty(this.penaltyList, pe, Player_Action_State.PD);
                if (p.code == Penalty_Codes.UC)
                    UC++;
                else if (p.code == Penalty_Codes.UR)
                    UR++;
                else if (p.code == Penalty_Codes.FM)
                    FM++;
                else if (p.code == Penalty_Codes.DH)
                    DH++;
                else if (p.code == Penalty_Codes.IC)
                    IC++;
                else if (p.code == Penalty_Codes.PI)
                    PI++;
                else
                    throw new Exception("Incorrect Penalty Code");
            }

            Assert.IsTrue(PI > 0 && DH > 0 && IC > 0 && UC > 0 && UR > 0 && FM > 0);
        }
        [TestCategory("Penalties")]
        [TestMethod]
        public void getPenaltyPlayer_Prevelence()
        {
            //This test method will test that certain positions, such as QB, Kick, Punter will rarely
            //be charged with a penalty and other positions will depend of the player's
            //sportsmanship rating.

            //First get the offensive and defensive teams.  Note that they
            //positions would never made up a play but the method we are testing
            //doesn't care about that.

            List<Game_Player> Offensive_Players = new List<Game_Player>();
            List<Game_Player> Defensive_Players = new List<Game_Player>();
            Game_Player Passer = null;
            Game_Player Kicker = null;
            Game_Player Punter = null;
            int[] o_arr = new int[11];
            int[] d_arr = new int[11];
            int non_special_min = 100000;
            int non_special_max = 0;
            bool bPass = false;

            //Populate the offensive and defensive players players 0,1 and 2 do not get penalties 
            //often.  and players 9 and 10 get rare penalties and the most penalties
            for (int i = 0; i < 11; i++)
            {
                long sportsmanship = 65;

                if (i == 9)
                    sportsmanship = 99;
                else if (i == 10)
                    sportsmanship = 25;

                Game_Player gpo = new Game_Player() { Pos = Player_Pos.LB };
                Player_and_Ratings p_and_Ro = new Player_and_Ratings();
                p_and_Ro.p = new Player() { First_Name = "Fname", Last_Name = "LName", ID = 1 };
                p_and_Ro.pr = new List<Player_Ratings>() { new Player_Ratings() { Sportsmanship_Ratings = sportsmanship } };
                gpo.p_and_r = p_and_Ro;

                if (i == 0)
                    Passer = gpo;
                else if (i == 1)
                    Kicker = gpo;
                else if (i == 2)
                    Punter = gpo;

                Game_Player gpa = new Game_Player() { Pos = Player_Pos.LB };
                Player_and_Ratings p_and_Ra = new Player_and_Ratings();
                p_and_Ra.p = new Player() { First_Name = "Fname", Last_Name = "LName", ID = 1 };
                p_and_Ra.pr = new List<Player_Ratings>() { new Player_Ratings() { Sportsmanship_Ratings = sportsmanship } };
                gpa.p_and_r = p_and_Ra;

                Offensive_Players.Add(gpo);
                Defensive_Players.Add(gpa);
            }

            int numTries = 10000;

            for (int j=0; j<= numTries; j++)
            {
                Game_Player penalty_player = Penalty_Helper.getPenaltyPlayer(Offensive_Players, Defensive_Players, Passer, Kicker, Punter);
                bool bOff = false;

                int rt;
                if (penalty_player == null)
                    continue;

                if (Offensive_Players.Contains(penalty_player))
                    bOff = true;
                else
                    bOff = false;

                if (bOff)
                {
                    int oi = Offensive_Players.FindIndex(x => x == penalty_player);
                    o_arr[oi]++;
                }
                else
                {
                    int ai = Defensive_Players.FindIndex(x => x == penalty_player);
                    d_arr[ai]++;
                }
            }

            //git min and max
            for (int i = 0; i < 11; i++)
            {
                if (i == 9 || i == 10)
                    continue;
                else if (i == 0 || i == 1 || i == 2)
                {
                    if (d_arr[i] < non_special_min) non_special_min = d_arr[i];
                    if (d_arr[i] > non_special_max) non_special_max = d_arr[i];
                }
                else
                {
                    if (d_arr[i] < non_special_min) non_special_min = d_arr[i];
                    if (d_arr[i] > non_special_max) non_special_max = d_arr[i];
                    if (o_arr[i] < non_special_min) non_special_min = o_arr[i];
                    if (o_arr[i] > non_special_max) non_special_max = o_arr[i];
                }
            }

            if (o_arr[0] > non_special_min || o_arr[0] < o_arr[1] || o_arr[0] < o_arr[2] ||
                o_arr[1] > non_special_min || o_arr[2] > non_special_min)
                throw new Exception("Secial position penalty total wrong");

            if (o_arr[9] > non_special_min || o_arr[10] < non_special_max &&
                d_arr[9] > non_special_min || d_arr[10] < non_special_max)
                throw new Exception("Number of high low sportsmanship penalties not correct");

            int total_penalties = o_arr.Sum() + d_arr.Sum();

            if (total_penalties < 600 || total_penalties > 950)
                throw new Exception("Too few or too many penalties");

            Assert.IsTrue(true);
        }
        [TestCategory("Penalties")]
        [TestMethod]
        public void isNoPenaltyPlay_True()
        {
            Play_Result pResult = new Play_Result() { bTouchback = true };
            Play_Enum pe = Play_Enum.KICKOFF_NORMAL;
            bool bWrong = false;

            int numtries = 100;
            for (int i= 0; i < numtries; i++)
            {
                int n = CommonUtils.getRandomNum(1, 3);
                switch (n)
                {
                    case 1:
                        pe = Play_Enum.KICKOFF_NORMAL;
                        break;
                    case 2:
                        pe = Play_Enum.FREE_KICK;
                        break;
                    case 3:
                        pe = Play_Enum.PUNT;
                        break;
                }
                bool bNotPenaltyPlay = Penalty_Helper.isNoPenaltyPlay(pResult, pe);
                if (!bNotPenaltyPlay)
                {
                    bWrong = true;
                    break;
                }

            }

            Assert.IsTrue(!bWrong);
        }
        [TestCategory("Penalties")]
        [TestMethod]
        public void isNoPenaltyPlay_False()
        {
            Play_Result pResult = new Play_Result() { bTouchback = false };
            Play_Enum pe = Play_Enum.KICKOFF_NORMAL;
            bool bWrong = false;

            int numtries = 100;
            for (int i = 0; i < numtries; i++)
            {
                int n = CommonUtils.getRandomNum(1, 6);
                switch (n)
                {
                    case 1:
                        pe = Play_Enum.KICKOFF_NORMAL;
                        break;
                    case 2:
                        pe = Play_Enum.FREE_KICK;
                        break;
                    case 3:
                        pe = Play_Enum.PUNT;
                        break;
                    case 4:
                        pe = Play_Enum.RUN;
                        break;
                    case 5:
                        pe = Play_Enum.PASS;
                        break;
                    case 6:
                        pe = Play_Enum.FIELD_GOAL;
                        break;

                }
                bool bNotPenaltyPlay = Penalty_Helper.isNoPenaltyPlay(pResult, pe);
                if (bNotPenaltyPlay)
                {
                    bWrong = true;
                    break;
                }

            }

            Assert.IsTrue(!bWrong);
        }
        [TestCategory("Penalties")]
        [TestMethod]
        public void isHalfTheDistance_true()
        {
            Tuple<bool, double> t = Penalty_Helper.isHalfTheDistance(15, 14);
            bool bhalfDis = t.Item1;
            double yl = t.Item2;

            Assert.IsTrue(bhalfDis && yl == 7.0);
        }
        [TestCategory("Penalties")]
        [TestMethod]
        public void isHalfTheDistance_false()
        {
            Tuple<bool, double> t = Penalty_Helper.isHalfTheDistance(5, 14);
            bool bhalfDis = t.Item1;
            double yl = t.Item2;

            Assert.IsTrue(!bhalfDis);
        }
        [TestCategory("Penalties")]
        [TestMethod]
        public void isFirstDowwithPenalty_autoFirstDown()
        {
            Penalty p = new Penalty() { bAuto_FirstDown = true };
            double yards_to_go = 10.0;

            bool bFD = Penalty_Helper.isFirstDowwithPenalty(p, yards_to_go, false, 0.0);

            Assert.IsTrue(bFD);
        }
        [TestCategory("Penalties")]
        [TestMethod]
        public void isFirstDowwithPenalty_HalftheDist_NotFD()
        {
            Penalty p = new Penalty() { bAuto_FirstDown = false, Yards = 10 };
            double yards_to_go = 9.0;

            bool bFD = Penalty_Helper.isFirstDowwithPenalty(p, yards_to_go, true, 7.5);

            Assert.IsTrue(!bFD);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void isFirstDowwithPenalty_HalftheDist_FD()
        {
            Penalty p = new Penalty() { bAuto_FirstDown = false, Yards=10 };
            double yards_to_go = 3.0;
            double dist_from_gl = 18;

            bool bFD = Penalty_Helper.isFirstDowwithPenalty(p, yards_to_go, true, 9.0);

            Assert.IsTrue(bFD);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void isFirstDowwithPenalty_NOTHalftheDist_FD()
        {
            Penalty p = new Penalty() { bAuto_FirstDown = false, Yards = 10 };
            double yards_to_go = 3.0;
            double dist_from_gl = 40;

            bool bFD = Penalty_Helper.isFirstDowwithPenalty(p, yards_to_go, false, 0.0);

            Assert.IsTrue(bFD);
        }
        [TestCategory("Penalties")]
        [TestMethod]
        public void isFirstDowwithPenalty_NOTHalftheDist_NotFD()
        {
            Penalty p = new Penalty() { bAuto_FirstDown = false, Yards = 5 };
            double yards_to_go = 9.0;
            double dist_from_gl = 40;

            bool bFD = Penalty_Helper.isFirstDowwithPenalty(p, yards_to_go, false, 0.0);

            Assert.IsTrue(!bFD);
        }
    }
}
