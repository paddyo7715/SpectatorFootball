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
using SpectatorFootball.unitTests.Helper_ClassesNS;

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
        public void getPreSnapPenalty_Kickoff_Freekick()
        {
            Play_Result pResult = new Play_Result() { at = 11, ht = 22 };
            int Num_Tries = 1000;
            Penalty penalty = null;
            Game_Player Penalty_Player = null;

            List<Game_Player> Offensive_Players = Help_Class.getRandomPlayersforPlay(11);
            List<Game_Player> Defensive_Players = Help_Class.getRandomPlayersforPlay(22);

            for (int i = 0; i < 11; i++)
            {
                if (i == 0)
                {
                    pResult.Kicker = Defensive_Players[i];
                    pResult.Returner = Offensive_Players[i];
                }
                else
                {
                    pResult.Kick_Returners.Add(Defensive_Players[i]);
                    pResult.Kick_Defenders.Add(Offensive_Players[i]);
                }
            }

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

                Tuple<Game_Player, Penalty> t = Penalty_Helper.Presnap_Penalty(pe, this.penaltyList,
                    Offensive_Players, Defensive_Players, pResult);
                Penalty_Player = t.Item1;
                penalty = t.Item2;

                if (penalty != null)
                    throw new Exception("No presnap penalties on kickoffs");
            }

            Assert.IsTrue(true);
        }
        [TestCategory("Penalties")]
        [TestMethod]
        public void getPreSnapPenalty_Onside_Kickoff()
        {
            Play_Result pResult = new Play_Result() { at = 11, ht = 22 };
            int Num_Tries = 1000;
            Penalty penalty = null;
            Game_Player Penalty_Player = null;

            List<Game_Player> Offensive_Players = Help_Class.getRandomPlayersforPlay(11);
            List<Game_Player> Defensive_Players = Help_Class.getRandomPlayersforPlay(22);

            for (int i = 0; i < 11; i++)
            {
                if (i == 0)
                {
                    pResult.Kicker = Defensive_Players[i];
                    pResult.Returner = Offensive_Players[i];
                }
                else
                {
                    pResult.Kick_Returners.Add(Defensive_Players[i]);
                    pResult.Kick_Defenders.Add(Offensive_Players[i]);
                }
            }

            for (int i = 0; i < Num_Tries; i++)
            {
                Play_Enum pe = Play_Enum.KICKOFF_ONSIDES;


                Tuple<Game_Player, Penalty> t = Penalty_Helper.Presnap_Penalty(pe, this.penaltyList,
                    Offensive_Players, Defensive_Players, pResult);
                Penalty_Player = t.Item1;
                penalty = t.Item2;

                if (penalty != null)
                    throw new Exception("No presnap penalties on kickoffs");
            }

            Assert.IsTrue(true);
        }
        [TestCategory("Penalties")]
        [TestMethod]
        public void getPreSnapPenalty_Punt()
        {
            int DG = 0;
            int FS = 0;
            int NZ = 0;
            int EN = 0;
            int DO = 0;

            Play_Result pResult = new Play_Result() { at = 11, ht = 22 };
            int Num_Tries = 1000;
            Penalty penalty = null;
            Game_Player Penalty_Player = null;

            List<Game_Player> Offensive_Players = Help_Class.getRandomPlayersforPlay(11);
            List<Game_Player> Defensive_Players = Help_Class.getRandomPlayersforPlay(22);

            for (int i = 0; i < 11; i++)
            {
                if (i == 0)
                    pResult.Punter = Defensive_Players[i];
                else
                    pResult.Punt_Defenders.Add(Defensive_Players[i]);
            }

            for (int i = 0; i < 11; i++)
            {
                if (i == 0)
                    pResult.Punt_Returner = Offensive_Players[i];
                else
                    pResult.Punt_Returners.Add(Offensive_Players[i]);
            }

            for (int i = 0; i < Num_Tries; i++)
            {
                Play_Enum pe = Play_Enum.PUNT;
  
                Tuple<Game_Player, Penalty> t = Penalty_Helper.Presnap_Penalty(pe, this.penaltyList,
                    Offensive_Players, Defensive_Players, pResult);
                Penalty_Player = t.Item1;
                penalty = t.Item2;

                if (penalty == null) continue;

                if (penalty.code == Penalty_Codes.DG)
                    DG++;
                else if (penalty.code == Penalty_Codes.FS)
                    FS++;
                else if (penalty.code == Penalty_Codes.NZ)
                    NZ++;
                else if (penalty.code == Penalty_Codes.EN)
                    EN++;
                else if (penalty.code == Penalty_Codes.DO)
                    DO++;
                else
                    throw new Exception("Not all penalties encountered");

                if (!penalty.Penalty_Play_Types.Contains(pe))
                    throw new Exception("Not all penalties encountered");

                if (penalty.Play_Timing != Play_Snap_Timing.BEFORE_SNAP)
                    throw new Exception("Penalty is not presnap");

                Player_Action_State pa = Penalty_Helper.getPlayerAction(Penalty_Player, pResult);
                if (!penalty.Player_Action_States.Contains(pa))
                    throw new Exception("Penalty player should not have gotten this penalty");
            }

            if (DG == 0 || FS == 0 || NZ == 0 || EN == 0 || DO == 0)
                throw new Exception("Incorrect Penalty Code");

            Assert.IsTrue(true);
        }
        [TestCategory("Penalties")]
        [TestMethod]
        public void getPreSnapPenalty_FG_XP()
        {
            int DG = 0;
            int FS = 0;
            int NZ = 0;
            int EN = 0;
            int DO = 0;

            Play_Result pResult = new Play_Result() { at = 11, ht = 22 };
            int Num_Tries = 1000;
            Penalty penalty = null;
            Game_Player Penalty_Player = null;

            List<Game_Player> Offensive_Players = Help_Class.getRandomPlayersforPlay(11);
            List<Game_Player> Defensive_Players = Help_Class.getRandomPlayersforPlay(22);

            for (int i = 0; i < 11; i++)
            {
                if (i == 0)
                    pResult.Kicker = Offensive_Players[i];
                else
                    pResult.FieldGaol_Kicking_Team.Add(Offensive_Players[i]);
            }

            for (int i = 0; i < 11; i++)
            {
                    pResult.Field_Goal_Defenders.Add(Defensive_Players[i]);
            }

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

                Tuple<Game_Player, Penalty> t = Penalty_Helper.Presnap_Penalty(pe, this.penaltyList,
                    Offensive_Players, Defensive_Players, pResult);
                Penalty_Player = t.Item1;
                penalty = t.Item2;

                if (penalty == null) continue;

                if (penalty.code == Penalty_Codes.DG)
                    DG++;
                else if (penalty.code == Penalty_Codes.FS)
                    FS++;
                else if (penalty.code == Penalty_Codes.NZ)
                    NZ++;
                else if (penalty.code == Penalty_Codes.EN)
                    EN++;
                else if (penalty.code == Penalty_Codes.DO)
                    DO++;
                else
                    throw new Exception("Not all penalties encountered");

                if (!penalty.Penalty_Play_Types.Contains(pe))
                    throw new Exception("Not all penalties encountered");

                if (penalty.Play_Timing != Play_Snap_Timing.BEFORE_SNAP)
                    throw new Exception("Penalty is not presnap");

                Player_Action_State pa = Penalty_Helper.getPlayerAction(Penalty_Player, pResult);
                if (!penalty.Player_Action_States.Contains(pa))
                    throw new Exception("Penalty player should not have gotten this penalty");
            }

            if (DG == 0 || FS == 0 || NZ == 0 || EN == 0 || DO == 0)
                throw new Exception("Incorrect Penalty Code");

            Assert.IsTrue(true);
        }
        [TestCategory("Penalties")]
        [TestMethod]
        public void getPreSnapPenalty_RUN()
        {
            int DG = 0;
            int FS = 0;
            int NZ = 0;
            int EN = 0;
            int DO = 0;
            int IF = 0;
            int IM = 0;

            Play_Result pResult = new Play_Result() { at = 11, ht = 22 };
            int Num_Tries = 1000;
            Penalty penalty = null;
            Game_Player Penalty_Player = null;

            List<Game_Player> Offensive_Players = Help_Class.getRandomPlayersforPlay(11);
            List<Game_Player> Defensive_Players = Help_Class.getRandomPlayersforPlay(22);

            for (int i = 0; i < 11; i++)
            {
                if (i == 0)
                    pResult.Passer = Offensive_Players[i];
                else if (i == 2 || i == 3)
                    pResult.Ball_Runners.Add(Offensive_Players[i]);
                else if (i == 4 || i == 5)
                    pResult.Pass_Catchers.Add(Offensive_Players[i]);
                else
                    pResult.Run_Blockers.Add(Offensive_Players[i]);
            }

            for (int i = 0; i < 11; i++)
            {
                if (i < 6)
                    pResult.Pass_Defenders.Add(Defensive_Players[i]);
                else
                    pResult.Run_Defenders.Add(Defensive_Players[i]);
            }

            for (int i = 0; i < Num_Tries; i++)
            {
                int n = CommonUtils.getRandomNum(1, 4);
                Play_Enum pe = Play_Enum.KICKOFF_NORMAL;
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

                Tuple<Game_Player, Penalty> t = Penalty_Helper.Presnap_Penalty(pe, this.penaltyList,
                    Offensive_Players, Defensive_Players, pResult);
                Penalty_Player = t.Item1;
                penalty = t.Item2;

                if (penalty == null) continue;

                if (penalty.code == Penalty_Codes.DG)
                    DG++;
                else if (penalty.code == Penalty_Codes.FS)
                    FS++;
                else if (penalty.code == Penalty_Codes.NZ)
                    NZ++;
                else if (penalty.code == Penalty_Codes.EN)
                    EN++;
                else if (penalty.code == Penalty_Codes.DO)
                    DO++;
                else if (penalty.code == Penalty_Codes.IF)
                    IF++;
                else if (penalty.code == Penalty_Codes.IM)
                    IM++;
                else
                    throw new Exception("Not all penalties encountered");

                if (!penalty.Penalty_Play_Types.Contains(pe))
                    throw new Exception("Not all penalties encountered");

                if (penalty.Play_Timing != Play_Snap_Timing.BEFORE_SNAP)
                    throw new Exception("Penalty is not presnap");

                Player_Action_State pa = Penalty_Helper.getPlayerAction(Penalty_Player, pResult);
                if (!penalty.Player_Action_States.Contains(pa))
                    throw new Exception("Penalty player should not have gotten this penalty");
            }

            if (IF == 0 || IM == 0 || DG == 0 || FS == 0 || NZ == 0 || EN == 0 || DO == 0)
                throw new Exception("Incorrect Penalty Code");

            Assert.IsTrue(true);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void getPreSnapPenalty_PASS()
        {
            int DG = 0;
            int FS = 0;
            int NZ = 0;
            int EN = 0;
            int DO = 0;
            int IF = 0;
            int IM = 0;

            Play_Result pResult = new Play_Result() { at = 11, ht = 22 };
            int Num_Tries = 1000;
            Penalty penalty = null;
            Game_Player Penalty_Player = null;

            List<Game_Player> Offensive_Players = Help_Class.getRandomPlayersforPlay(11);
            List<Game_Player> Defensive_Players = Help_Class.getRandomPlayersforPlay(22);

            for (int i = 0; i < 11; i++)
            {
                if (i == 0)
                    pResult.Passer = Offensive_Players[i];
                else if (i == 2 || i == 3)
                    pResult.Ball_Runners.Add(Offensive_Players[i]);
                else if (i == 4 || i == 5)
                    pResult.Pass_Catchers.Add(Offensive_Players[i]);
                else
                    pResult.Pass_Blockers.Add(Offensive_Players[i]);
            }

            for (int i = 0; i < 11; i++)
            {
                if (i < 6)
                    pResult.Pass_Defenders.Add(Defensive_Players[i]);
                else
                    pResult.Pass_Rushers.Add(Defensive_Players[i]);
            }

            for (int i = 0; i < Num_Tries; i++)
            {
                int n = CommonUtils.getRandomNum(1, 4);
                Play_Enum pe = Play_Enum.KICKOFF_NORMAL;
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

                Tuple<Game_Player, Penalty> t = Penalty_Helper.Presnap_Penalty(pe, this.penaltyList,
                    Offensive_Players, Defensive_Players, pResult);
                Penalty_Player = t.Item1;
                penalty = t.Item2;

                if (penalty == null) continue;

                if (penalty.code == Penalty_Codes.DG)
                    DG++;
                else if (penalty.code == Penalty_Codes.FS)
                    FS++;
                else if (penalty.code == Penalty_Codes.NZ)
                    NZ++;
                else if (penalty.code == Penalty_Codes.EN)
                    EN++;
                else if (penalty.code == Penalty_Codes.DO)
                    DO++;
                else if (penalty.code == Penalty_Codes.IF)
                    IF++;
                else if (penalty.code == Penalty_Codes.IM)
                    IM++;
                else
                    throw new Exception("Not all penalties encountered");

                if (!penalty.Penalty_Play_Types.Contains(pe))
                    throw new Exception("Not all penalties encountered");

                if (penalty.Play_Timing != Play_Snap_Timing.BEFORE_SNAP)
                    throw new Exception("Penalty is not presnap");

                Player_Action_State pa = Penalty_Helper.getPlayerAction(Penalty_Player, pResult);
                if (!penalty.Player_Action_States.Contains(pa))
                    throw new Exception("Penalty player should not have gotten this penalty");
            }

            if (IF == 0 || IM == 0 || DG == 0 || FS == 0 || NZ == 0 || EN == 0 || DO == 0)
                throw new Exception("Incorrect Penalty Code");

            Assert.IsTrue(true);
        }




        [TestCategory("Penalties")]
        [TestMethod]
        public void getPostSnapPenalty_Kickoff_Freekick()
        {
            int KIB = 0;
            int UC = 0;
            int UR = 0;
            int FM = 0;

            Play_Result pResult = new Play_Result(){ at = 11, ht = 22 };
            int Num_Tries = 1000;
            Penalty penalty = null;
            Game_Player Penalty_Player = null;

            List<Game_Player> Offensive_Players = Help_Class.getRandomPlayersforPlay(11);
            List<Game_Player> Defensive_Players = Help_Class.getRandomPlayersforPlay(22);

            for (int i = 0; i < 11; i++)
            {
                if (i == 0)
                {
                    pResult.Kicker = Defensive_Players[i];
                    pResult.Returner = Offensive_Players[i];
                }
                else
                {
                    pResult.Kick_Returners.Add(Defensive_Players[i]);
                    pResult.Kick_Defenders.Add(Offensive_Players[i]);
                }
            }

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

                Tuple<Game_Player, Penalty> t = Penalty_Helper.PostSnap_Penalty(pe, this.penaltyList,
                    Offensive_Players, Defensive_Players, pResult);
                Penalty_Player = t.Item1;
                penalty = t.Item2;

                if (penalty == null) continue;

                if (penalty.code == Penalty_Codes.KIB)
                    KIB++;
                else if (penalty.code == Penalty_Codes.UC)
                    UC++;
                else if (penalty.code == Penalty_Codes.UR)
                    UR++;
                else if (penalty.code == Penalty_Codes.FM)
                    FM++;
                else
                    throw new Exception("Not all penalties encountered");

                if (!penalty.Penalty_Play_Types.Contains(pe))
                    throw new Exception("Not all penalties encountered");

                if (penalty.Play_Timing != Play_Snap_Timing.DURING_PLAY)
                    throw new Exception("Penalty is not presnap");

                Player_Action_State pa = Penalty_Helper.getPlayerAction(Penalty_Player, pResult);
                if (!penalty.Player_Action_States.Contains(pa))
                    throw new Exception("Penalty player should not have gotten this penalty");
            }

            if (KIB == 0 || UC == 0 || UR == 0 || FM == 0)
                throw new Exception("Incorrect Penalty Code");

            Assert.IsTrue(true);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void getPostSnapPenalty_OnsideKickoff()
        {
            int UC = 0;
            int UR = 0;
            int FM = 0;

            Play_Result pResult = new Play_Result() { at = 11, ht = 22 };
            int Num_Tries = 1000;
            Penalty penalty = null;
            Game_Player Penalty_Player = null;

            List<Game_Player> Offensive_Players = Help_Class.getRandomPlayersforPlay(11);
            List<Game_Player> Defensive_Players = Help_Class.getRandomPlayersforPlay(22);

            for (int i = 0; i < 11; i++)
            {
                if (i == 0)
                {
                    pResult.Kicker = Defensive_Players[i];
                    pResult.Returner = Offensive_Players[i];
                }
                else
                {
                    pResult.Kick_Returners.Add(Defensive_Players[i]);
                    pResult.Kick_Defenders.Add(Offensive_Players[i]);
                }
            }

            for (int i = 0; i < Num_Tries; i++)
            {
                Play_Enum pe = Play_Enum.KICKOFF_ONSIDES;

                Tuple<Game_Player, Penalty> t = Penalty_Helper.PostSnap_Penalty(pe, this.penaltyList,
                    Offensive_Players, Defensive_Players, pResult);
                Penalty_Player = t.Item1;
                penalty = t.Item2;

                if (penalty == null) continue;

                if (penalty.code == Penalty_Codes.UC)
                    UC++;
                else if (penalty.code == Penalty_Codes.UR)
                    UR++;
                else if (penalty.code == Penalty_Codes.FM)
                    FM++;
                else
                    throw new Exception("Not all penalties encountered");

                if (!penalty.Penalty_Play_Types.Contains(pe))
                    throw new Exception("Not all penalties encountered");

                if (penalty.Play_Timing != Play_Snap_Timing.DURING_PLAY)
                    throw new Exception("Penalty is not presnap");

                Player_Action_State pa = Penalty_Helper.getPlayerAction(Penalty_Player, pResult);
                if (!penalty.Player_Action_States.Contains(pa))
                    throw new Exception("Penalty player should not have gotten this penalty");
            }

            if (UC == 0 || UR == 0 || FM == 0)
                throw new Exception("Incorrect Penalty Code");

            Assert.IsTrue(true);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void getPostSnapPenalty_Punt()
        {
            int PIB = 0;
            int RK = 0;
            int RIK = 0;
            int UC = 0;
            int UR = 0;
            int FM = 0;

            Play_Result pResult = new Play_Result() { at = 11, ht = 22 };
            int Num_Tries = 10000;
            Penalty penalty = null;
            Game_Player Penalty_Player = null;

            List<Game_Player> Offensive_Players = Help_Class.getRandomPlayersforPlay(11);
            List<Game_Player> Defensive_Players = Help_Class.getRandomPlayersforPlay(22);

            for (int i = 0; i < 11; i++)
            {
                if (i == 0)
                    pResult.Punter = Defensive_Players[i];
                else
                    pResult.Punt_Defenders.Add(Defensive_Players[i]);
            }

            for (int i = 0; i < 11; i++)
            {
                if (i == 0)
                    pResult.Returner = Offensive_Players[i];
                else
                    pResult.Punt_Returners.Add(Offensive_Players[i]);
            }

            for (int i = 0; i < Num_Tries; i++)
            {
                Play_Enum pe = Play_Enum.PUNT;

                if (i % 10 == 0)
                    pResult.Defender_Close_to_Kicker = Offensive_Players[8];

                Tuple<Game_Player, Penalty> t = Penalty_Helper.PostSnap_Penalty(pe, this.penaltyList,
                    Offensive_Players, Defensive_Players, pResult);
                Penalty_Player = t.Item1;
                penalty = t.Item2;

                if (penalty == null) continue;

                if (penalty.code == Penalty_Codes.PIB)
                    PIB++;
                else if (penalty.code == Penalty_Codes.RK)
                    RK++;
                else if (penalty.code == Penalty_Codes.RIK)
                    RIK++;
                else if (penalty.code == Penalty_Codes.UC)
                    UC++;
                else if (penalty.code == Penalty_Codes.UR)
                    UR++;
                else if (penalty.code == Penalty_Codes.FM)
                    FM++;
                else
                    throw new Exception("Not all penalties encountered");

                if (!penalty.Penalty_Play_Types.Contains(pe))
                    throw new Exception("Not all penalties encountered");

                if (penalty.Play_Timing != Play_Snap_Timing.DURING_PLAY)
                    throw new Exception("Penalty is not presnap");

                Player_Action_State pa = Penalty_Helper.getPlayerAction(Penalty_Player, pResult);
                if (!penalty.Player_Action_States.Contains(pa))
                    throw new Exception("Penalty player should not have gotten this penalty");
            }

            if (PIB == 0 || RK == 0 || RIK == 0 || UC == 0 || UR == 0 || FM == 0)
                throw new Exception("Incorrect Penalty Code");

            Assert.IsTrue(true);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void getPostSnapPenalty_FGXP()
        {
            int UC = 0;
            int UR = 0;
            int FM = 0;

            Play_Result pResult = new Play_Result() { at = 11, ht = 22 };
            int Num_Tries = 10000;
            Penalty penalty = null;
            Game_Player Penalty_Player = null;

            List<Game_Player> Offensive_Players = Help_Class.getRandomPlayersforPlay(11);
            List<Game_Player> Defensive_Players = Help_Class.getRandomPlayersforPlay(22);

            for (int i = 0; i < 11; i++)
            {
                if (i == 0)
                    pResult.Kicker = Defensive_Players[i];
                else
                    pResult.FieldGaol_Kicking_Team.Add(Defensive_Players[i]);
            }

            for (int i = 0; i < 11; i++)
            {
                   pResult.Field_Goal_Defenders.Add(Offensive_Players[i]);
            }

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

                Tuple<Game_Player, Penalty> t = Penalty_Helper.PostSnap_Penalty(pe, this.penaltyList,
                    Offensive_Players, Defensive_Players, pResult);
                Penalty_Player = t.Item1;
                penalty = t.Item2;

                if (penalty == null) continue;

                if (penalty.code == Penalty_Codes.UC)
                    UC++;
                else if (penalty.code == Penalty_Codes.UR)
                    UR++;
                else if (penalty.code == Penalty_Codes.FM)
                    FM++;
                else
                    throw new Exception("Not all penalties encountered");

                if (!penalty.Penalty_Play_Types.Contains(pe))
                    throw new Exception("Not all penalties encountered");

                if (penalty.Play_Timing != Play_Snap_Timing.DURING_PLAY)
                    throw new Exception("Penalty is not presnap");

                Player_Action_State pa = Penalty_Helper.getPlayerAction(Penalty_Player, pResult);
                if (!penalty.Player_Action_States.Contains(pa))
                    throw new Exception("Penalty player should not have gotten this penalty");
            }

            if (UC == 0 || UR == 0 || FM == 0)
                throw new Exception("Incorrect Penalty Code");

            Assert.IsTrue(true);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void getPostSnapPenalty_PASS()
        {
            int UC = 0;
            int UR = 0;
            int FM = 0;
            int IHO = 0;
            int OI = 0;
            int OH = 0;
            int IR = 0;
            int IB = 0;
            int IC = 0;
            int IH = 0;
            int DH = 0;
            int RD = 0;
            int PI = 0;

            Play_Result pResult = new Play_Result() { at = 11, ht = 22 };
            int Num_Tries = 10000;
            Penalty penalty = null;
            Game_Player Penalty_Player = null;

            List<Game_Player> Offensive_Players = Help_Class.getRandomPlayersforPlay(11);
            List<Game_Player> Defensive_Players = Help_Class.getRandomPlayersforPlay(22);

            for (int i = 0; i < 11; i++)
            {
                if (i == 0)
                    pResult.Passer = Offensive_Players[i];
                else if (i == 2 || i == 3)
                    pResult.Pass_Catchers.Add(Offensive_Players[i]);
                else
                    pResult.Pass_Blockers.Add(Offensive_Players[i]);
            }

            for (int i = 0; i < 11; i++)
            {
                if (i < 6)
                    pResult.Pass_Rushers.Add(Defensive_Players[i]); 
                else
                    pResult.Pass_Defenders.Add(Defensive_Players[i]);
            }

            for (int i = 0; i < Num_Tries; i++)
            {
                int n = CommonUtils.getRandomNum(1, 4);
                Play_Enum pe = Play_Enum.KICKOFF_NORMAL;
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

                if (i % 10 == 0)
                    pResult.Defender_Knocks_Down_QB = Defensive_Players[2];
                else if (i % 8 == 0)
                    pResult.Defender_Close_to_Receiver = Defensive_Players[8];

                Tuple<Game_Player, Penalty> t = Penalty_Helper.PostSnap_Penalty(pe, this.penaltyList,
                    Offensive_Players, Defensive_Players, pResult);
                Penalty_Player = t.Item1;
                penalty = t.Item2;

                if (penalty == null) continue;

                if (penalty.code == Penalty_Codes.UC)
                    UC++;
                else if (penalty.code == Penalty_Codes.UR)
                    UR++;
                else if (penalty.code == Penalty_Codes.FM)
                    FM++;
                else if (penalty.code == Penalty_Codes.IHO)
                    IHO++;
                else if (penalty.code == Penalty_Codes.OI)
                    OI++;
                else if (penalty.code == Penalty_Codes.OH)
                    OH++;
                else if (penalty.code == Penalty_Codes.IR)
                    IR++;
                else if (penalty.code == Penalty_Codes.IB)
                    IB++;
                else if (penalty.code == Penalty_Codes.IC)
                    IC++;
                else if (penalty.code == Penalty_Codes.IH)
                    IH++;
                else if (penalty.code == Penalty_Codes.DH)
                    DH++;
                else if (penalty.code == Penalty_Codes.RD)
                    RD++;
                else if (penalty.code == Penalty_Codes.PI)
                    PI++;
                else
                    throw new Exception("Not all penalties encountered");

                if (!penalty.Penalty_Play_Types.Contains(pe))
                    throw new Exception("Not all penalties encountered");

                if (penalty.Play_Timing != Play_Snap_Timing.DURING_PLAY)
                    throw new Exception("Penalty is not presnap");

                Player_Action_State pa = Penalty_Helper.getPlayerAction(Penalty_Player, pResult);
                if (!penalty.Player_Action_States.Contains(pa))
                    throw new Exception("Penalty player should not have gotten this penalty");
            }

            if (UC == 0 || UR == 0 || FM == 0 || IHO == 0 || OI == 0 || OH == 0 || IR == 0 || IB == 0
                 || IC == 0 || IH == 0 || DH == 0 || PI == 0 || RD == 0)
                throw new Exception("Incorrect Penalty Code");

            Assert.IsTrue(true);
        }

        [TestCategory("Penalties")]
        [TestMethod]
        public void getPostSnapPenalty_RUN()
        {
            int UC = 0;
            int UR = 0;
            int FM = 0;
            int IHO = 0;
            int IH = 0;

            Play_Result pResult = new Play_Result() { at = 11, ht = 22 };
            int Num_Tries = 10000;
            Penalty penalty = null;
            Game_Player Penalty_Player = null;

            List<Game_Player> Offensive_Players = Help_Class.getRandomPlayersforPlay(11);
            List<Game_Player> Defensive_Players = Help_Class.getRandomPlayersforPlay(22);

            for (int i = 0; i < 11; i++)
            {
                if (i == 0)
                    pResult.Passer = Offensive_Players[i];
                else if (i == 2 || i == 3)
                    pResult.Ball_Runners.Add(Offensive_Players[i]);
                else
                    pResult.Run_Blockers.Add(Offensive_Players[i]);
            }

            for (int i = 0; i < 11; i++)
            {
                    pResult.Run_Defenders.Add(Defensive_Players[i]);
            }

            for (int i = 0; i < Num_Tries; i++)
            {
                int n = CommonUtils.getRandomNum(1, 4);
                Play_Enum pe = Play_Enum.KICKOFF_NORMAL;
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

                Tuple<Game_Player, Penalty> t = Penalty_Helper.PostSnap_Penalty(pe, this.penaltyList,
                    Offensive_Players, Defensive_Players, pResult);
                Penalty_Player = t.Item1;
                penalty = t.Item2;

                if (penalty == null) continue;

                if (penalty.code == Penalty_Codes.UC)
                    UC++;
                else if (penalty.code == Penalty_Codes.UR)
                    UR++;
                else if (penalty.code == Penalty_Codes.FM)
                    FM++;
                else if (penalty.code == Penalty_Codes.IHO)
                    IHO++;
                else if (penalty.code == Penalty_Codes.IH)
                    IH++;
                else
                    throw new Exception("Not all penalties encountered");

                if (!penalty.Penalty_Play_Types.Contains(pe))
                    throw new Exception("Not all penalties encountered");

                if (penalty.Play_Timing != Play_Snap_Timing.DURING_PLAY)
                    throw new Exception("Penalty is not presnap");

                Player_Action_State pa = Penalty_Helper.getPlayerAction(Penalty_Player, pResult);
                if (!penalty.Player_Action_States.Contains(pa))
                    throw new Exception("Penalty player should not have gotten this penalty");
            }

            if (UC == 0 || UR == 0 || FM == 0 || IHO == 0 || IH == 0)
                throw new Exception("Incorrect Penalty Code");

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
