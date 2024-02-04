using System;
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
    public class setPlayOutCome_XPPlay123_TEST
    {
        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Pass_xp1_from_97_and_half_made()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.SCRIM_PLAY_1XP_PASS;
            int Down = 0;
            double Yards_to_Go = 0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 91
            };
            Game_Player Receiver = new Game_Player()
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
                end_of_play_yardline = 105.0,
                Play_Start_Yardline = 97.5,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = false,
                bFumble_Lost = false,
                bInterception = false,
                Yards_Gained = 2.5,
                bSack = false,
                bSafety = false,
                bTouchback = false,
                bOnePntAfterTDMade = true,
                bOnePntAfterTDMissed = false,
                bTwoPntAfterTDMade = false,
                bTwoPntAfterTDMissed = false,
                bThreePntAfterTDMade = false,
                bThreePntAfterTDMissed = false,
                bCoffinCornerAttemt = false,
                bCoffinCornerMade = false,
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
            if (r.Final_Down != 0 || r.Final_yard_to_go != 0 || r.Final_end_of_Play_Yardline != 0)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || !r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 0.0)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || !r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 ||
                    r.AwayTOP != 0 || r.AwaySacks != 0 || r.AwaySackYards != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 ||
                    r.HomeTOP != 0 || r.HomeSacks != 0 || r.HomeSackYards != 0)
                throw new Exception("None of these should be greater than 0");

            Assert.IsTrue(true);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Pass_xp2_from_97_and_half_made()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.SCRIM_PLAY_2XP_PASS;
            int Down = 0;
            double Yards_to_Go = 0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 91
            };
            Game_Player Receiver = new Game_Player()
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
                end_of_play_yardline = 105.0,
                Play_Start_Yardline = 97.5,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = false,
                bFumble_Lost = false,
                bInterception = false,
                Yards_Gained = 2.5,
                bSack = false,
                bSafety = false,
                bTouchback = false,
                bOnePntAfterTDMade = false,
                bOnePntAfterTDMissed = false,
                bTwoPntAfterTDMade = true,
                bTwoPntAfterTDMissed = false,
                bThreePntAfterTDMade = false,
                bThreePntAfterTDMissed = false,
                bCoffinCornerAttemt = false,
                bCoffinCornerMade = false,
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
            if (r.Final_Down != 0 || r.Final_yard_to_go != 0 || r.Final_end_of_Play_Yardline != 0)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || !r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 0.0)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || !r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 ||
                    r.AwayTOP != 0 || r.AwaySacks != 0 || r.AwaySackYards != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 ||
                    r.HomeTOP != 0 || r.HomeSacks != 0 || r.HomeSackYards != 0)
                throw new Exception("None of these should be greater than 0");

            Assert.IsTrue(true);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Pass_xp3_from_97_and_half_made()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.SCRIM_PLAY_3XP_PASS;
            int Down = 0;
            double Yards_to_Go = 0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 91
            };
            Game_Player Receiver = new Game_Player()
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
                end_of_play_yardline = 105.0,
                Play_Start_Yardline = 97.5,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = false,
                bFumble_Lost = false,
                bInterception = false,
                Yards_Gained = 2.5,
                bSack = false,
                bSafety = false,
                bTouchback = false,
                bOnePntAfterTDMade = false,
                bOnePntAfterTDMissed = false,
                bTwoPntAfterTDMade = false,
                bTwoPntAfterTDMissed = false,
                bThreePntAfterTDMade = true,
                bThreePntAfterTDMissed = false,
                bCoffinCornerAttemt = false,
                bCoffinCornerMade = false,
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
            if (r.Final_Down != 0 || r.Final_yard_to_go != 0 || r.Final_end_of_Play_Yardline != 0)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || !r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 0.0)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || !r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 ||
                    r.AwayTOP != 0 || r.AwaySacks != 0 || r.AwaySackYards != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 ||
                    r.HomeTOP != 0 || r.HomeSacks != 0 || r.HomeSackYards != 0)
                throw new Exception("None of these should be greater than 0");

            Assert.IsTrue(true);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Run_xp1_from_97_and_half_made()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.SCRIM_PLAY_1XP_RUN;
            int Down = 0;
            double Yards_to_Go = 0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 91
            };
            Game_Player Receiver = new Game_Player()
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
                end_of_play_yardline = 105.0,
                Play_Start_Yardline = 97.5,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = false,
                bFumble_Lost = false,
                bInterception = false,
                Yards_Gained = 2.5,
                bSack = false,
                bSafety = false,
                bTouchback = false,
                bOnePntAfterTDMade = true,
                bOnePntAfterTDMissed = false,
                bTwoPntAfterTDMade = false,
                bTwoPntAfterTDMissed = false,
                bThreePntAfterTDMade = false,
                bThreePntAfterTDMissed = false,
                bCoffinCornerAttemt = false,
                bCoffinCornerMade = false,
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
            if (r.Final_Down != 0 || r.Final_yard_to_go != 0 || r.Final_end_of_Play_Yardline != 0)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || !r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 0.0)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || !r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 ||
                    r.AwayTOP != 0 || r.AwaySacks != 0 || r.AwaySackYards != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 ||
                    r.HomeTOP != 0 || r.HomeSacks != 0 || r.HomeSackYards != 0)
                throw new Exception("None of these should be greater than 0");

            Assert.IsTrue(true);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Run_xp2_from_97_and_half_made()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.SCRIM_PLAY_2XP_RUN;
            int Down = 0;
            double Yards_to_Go = 0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 91
            };
            Game_Player Receiver = new Game_Player()
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
                end_of_play_yardline = 105.0,
                Play_Start_Yardline = 97.5,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = false,
                bFumble_Lost = false,
                bInterception = false,
                Yards_Gained = 2.5,
                bSack = false,
                bSafety = false,
                bTouchback = false,
                bOnePntAfterTDMade = false,
                bOnePntAfterTDMissed = false,
                bTwoPntAfterTDMade = true,
                bTwoPntAfterTDMissed = false,
                bThreePntAfterTDMade = false,
                bThreePntAfterTDMissed = false,
                bCoffinCornerAttemt = false,
                bCoffinCornerMade = false,
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
            if (r.Final_Down != 0 || r.Final_yard_to_go != 0 || r.Final_end_of_Play_Yardline != 0)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || !r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 0.0)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || !r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 ||
                    r.AwayTOP != 0 || r.AwaySacks != 0 || r.AwaySackYards != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 ||
                    r.HomeTOP != 0 || r.HomeSacks != 0 || r.HomeSackYards != 0)
                throw new Exception("None of these should be greater than 0");

            Assert.IsTrue(true);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Run_xp3_from_97_and_half_made()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.SCRIM_PLAY_3XP_RUN;
            int Down = 0;
            double Yards_to_Go = 0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 91
            };
            Game_Player Receiver = new Game_Player()
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
                end_of_play_yardline = 105.0,
                Play_Start_Yardline = 97.5,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = false,
                bFumble_Lost = false,
                bInterception = false,
                Yards_Gained = 2.5,
                bSack = false,
                bSafety = false,
                bTouchback = false,
                bOnePntAfterTDMade = false,
                bOnePntAfterTDMissed = false,
                bTwoPntAfterTDMade = false,
                bTwoPntAfterTDMissed = false,
                bThreePntAfterTDMade = true,
                bThreePntAfterTDMissed = false,
                bCoffinCornerAttemt = false,
                bCoffinCornerMade = false,
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
            if (r.Final_Down != 0 || r.Final_yard_to_go != 0 || r.Final_end_of_Play_Yardline != 0)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || !r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 0.0)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || !r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 ||
                    r.AwayTOP != 0 || r.AwaySacks != 0 || r.AwaySackYards != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 ||
                    r.HomeTOP != 0 || r.HomeSacks != 0 || r.HomeSackYards != 0)
                throw new Exception("None of these should be greater than 0");

            Assert.IsTrue(true);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Pass_xp1_from_97_and_half_made_penalty_on_Def_Declined()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.SCRIM_PLAY_1XP_PASS;
            int Down = 0;
            double Yards_to_Go = 0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 91
            };
            Game_Player Receiver = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 105
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.DH).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 98
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 105.0,
                Play_Start_Yardline = 97.5,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = true,
                bTouchDown = false,
                bFumble = false,
                bFumble_Lost = false,
                bInterception = false,
                Yards_Gained = 2.5,
                bSack = false,
                bSafety = false,
                bTouchback = false,
                bOnePntAfterTDMade = true,
                bOnePntAfterTDMissed = false,
                bTwoPntAfterTDMade = false,
                bTwoPntAfterTDMissed = false,
                bThreePntAfterTDMade = false,
                bThreePntAfterTDMissed = false,
                bCoffinCornerAttemt = false,
                bCoffinCornerMade = false,
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
            if (r.Final_Down != 0 || r.Final_yard_to_go != 0 || r.Final_end_of_Play_Yardline != 0)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || !r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 0.0)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || !r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 ||
                    r.AwayTOP != 0 || r.AwaySacks != 0 || r.AwaySackYards != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 ||
                    r.HomeTOP != 0 || r.HomeSacks != 0 || r.HomeSackYards != 0)
                throw new Exception("None of these should be greater than 0");

            Assert.IsTrue(true);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Run_xp2_from_97_and_half_made_FM_on_def_ignored()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.SCRIM_PLAY_2XP_RUN;
            int Down = 0;
            double Yards_to_Go = 0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 91
            };
            Game_Player Receiver = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 105
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.FM).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 98
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 105.0,
                Play_Start_Yardline = 97.5,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = false,
                bFumble_Lost = false,
                bInterception = false,
                Yards_Gained = 2.5,
                bSack = false,
                bSafety = false,
                bTouchback = false,
                bOnePntAfterTDMade = false,
                bOnePntAfterTDMissed = false,
                bTwoPntAfterTDMade = true,
                bTwoPntAfterTDMissed = false,
                bThreePntAfterTDMade = false,
                bThreePntAfterTDMissed = false,
                bCoffinCornerAttemt = false,
                bCoffinCornerMade = false,
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
            if (r.Final_Down != 0 || r.Final_yard_to_go != 0 || r.Final_end_of_Play_Yardline != 0)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || !r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 0.0 || !r.bIgnorePenalty)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || !r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 ||
                    r.AwayTOP != 0 || r.AwaySacks != 0 || r.AwaySackYards != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 ||
                    r.HomeTOP != 0 || r.HomeSacks != 0 || r.HomeSackYards != 0)
                throw new Exception("None of these should be greater than 0");

            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Run_xp2_from_97_and_half_made_FM_on_Off_ignored()
        {
            bool penOnBallCarryingTeam = true;
            Play_Enum PE = Play_Enum.SCRIM_PLAY_2XP_RUN;
            int Down = 0;
            double Yards_to_Go = 0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 91
            };
            Game_Player Receiver = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 105
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.FM).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 98
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 105.0,
                Play_Start_Yardline = 97.5,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = false,
                bFumble_Lost = false,
                bInterception = false,
                Yards_Gained = 2.5,
                bSack = false,
                bSafety = false,
                bTouchback = false,
                bOnePntAfterTDMade = false,
                bOnePntAfterTDMissed = false,
                bTwoPntAfterTDMade = true,
                bTwoPntAfterTDMissed = false,
                bThreePntAfterTDMade = false,
                bThreePntAfterTDMissed = false,
                bCoffinCornerAttemt = false,
                bCoffinCornerMade = false,
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
            if (r.Final_Down != 0 || r.Final_yard_to_go != 0 || r.Final_end_of_Play_Yardline != 0)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || !r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 0.0 || !r.bIgnorePenalty)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || !r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 ||
                    r.AwayTOP != 0 || r.AwaySacks != 0 || r.AwaySackYards != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 ||
                    r.HomeTOP != 0 || r.HomeSacks != 0 || r.HomeSackYards != 0)
                throw new Exception("None of these should be greater than 0");

            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Pass_xp1_from_97_and_half_not_made()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.SCRIM_PLAY_1XP_PASS;
            int Down = 0;
            double Yards_to_Go = 0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 91
            };
            Game_Player Receiver = new Game_Player()
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
                end_of_play_yardline = 97.5,
                Play_Start_Yardline = 97.5,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = false,
                bFumble_Lost = false,
                bInterception = false,
                Yards_Gained = 2.5,
                bSack = false,
                bSafety = false,
                bTouchback = false,
                bOnePntAfterTDMade = false,
                bOnePntAfterTDMissed = true,
                bTwoPntAfterTDMade = false,
                bTwoPntAfterTDMissed = false,
                bThreePntAfterTDMade = false,
                bThreePntAfterTDMissed = false,
                bCoffinCornerAttemt = false,
                bCoffinCornerMade = false,
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
            if (r.Final_Down != 0 || r.Final_yard_to_go != 0 || r.Final_end_of_Play_Yardline != 0)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || !r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
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
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 ||
                    r.AwayTOP != 0 || r.AwaySacks != 0 || r.AwaySackYards != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 ||
                    r.HomeTOP != 0 || r.HomeSacks != 0 || r.HomeSackYards != 0)
                throw new Exception("None of these should be greater than 0");

            Assert.IsTrue(true);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Pass_xp2_from_97_and_half_not_made()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.SCRIM_PLAY_2XP_PASS;
            int Down = 0;
            double Yards_to_Go = 0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 91
            };
            Game_Player Receiver = new Game_Player()
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
                end_of_play_yardline = 97.5,
                Play_Start_Yardline = 97.5,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = false,
                bFumble_Lost = false,
                bInterception = false,
                Yards_Gained = 2.5,
                bSack = false,
                bSafety = false,
                bTouchback = false,
                bOnePntAfterTDMade = false,
                bOnePntAfterTDMissed = false,
                bTwoPntAfterTDMade = false,
                bTwoPntAfterTDMissed = true,
                bThreePntAfterTDMade = false,
                bThreePntAfterTDMissed = false,
                bCoffinCornerAttemt = false,
                bCoffinCornerMade = false,
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
            if (r.Final_Down != 0 || r.Final_yard_to_go != 0 || r.Final_end_of_Play_Yardline != 0)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || !r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
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
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 ||
                    r.AwayTOP != 0 || r.AwaySacks != 0 || r.AwaySackYards != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 ||
                    r.HomeTOP != 0 || r.HomeSacks != 0 || r.HomeSackYards != 0)
                throw new Exception("None of these should be greater than 0");

            Assert.IsTrue(true);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Pass_xp3_from_97_and_half_not_made()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.SCRIM_PLAY_3XP_PASS;
            int Down = 0;
            double Yards_to_Go = 0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 91
            };
            Game_Player Receiver = new Game_Player()
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
                end_of_play_yardline = 97.5,
                Play_Start_Yardline = 97.5,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = false,
                bFumble_Lost = false,
                bInterception = false,
                Yards_Gained = 2.5,
                bSack = false,
                bSafety = false,
                bTouchback = false,
                bOnePntAfterTDMade = false,
                bOnePntAfterTDMissed = false,
                bTwoPntAfterTDMade = false,
                bTwoPntAfterTDMissed = false,
                bThreePntAfterTDMade = false,
                bThreePntAfterTDMissed = true,
                bCoffinCornerAttemt = false,
                bCoffinCornerMade = false,
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
            if (r.Final_Down != 0 || r.Final_yard_to_go != 0 || r.Final_end_of_Play_Yardline != 0)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || !r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
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
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 ||
                    r.AwayTOP != 0 || r.AwaySacks != 0 || r.AwaySackYards != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 ||
                    r.HomeTOP != 0 || r.HomeSacks != 0 || r.HomeSackYards != 0)
                throw new Exception("None of these should be greater than 0");

            Assert.IsTrue(true);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Run_xp1_from_97_and_half_not_made()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.SCRIM_PLAY_1XP_RUN;
            int Down = 0;
            double Yards_to_Go = 0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 91
            };
            Game_Player Receiver = new Game_Player()
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
                end_of_play_yardline = 97.5,
                Play_Start_Yardline = 97.5,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = false,
                bFumble_Lost = false,
                bInterception = false,
                Yards_Gained = 2.5,
                bSack = false,
                bSafety = false,
                bTouchback = false,
                bOnePntAfterTDMade = false,
                bOnePntAfterTDMissed = true,
                bTwoPntAfterTDMade = false,
                bTwoPntAfterTDMissed = false,
                bThreePntAfterTDMade = false,
                bThreePntAfterTDMissed = false,
                bCoffinCornerAttemt = false,
                bCoffinCornerMade = false,
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
            if (r.Final_Down != 0 || r.Final_yard_to_go != 0 || r.Final_end_of_Play_Yardline != 0)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || !r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
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
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 ||
                    r.AwayTOP != 0 || r.AwaySacks != 0 || r.AwaySackYards != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 ||
                    r.HomeTOP != 0 || r.HomeSacks != 0 || r.HomeSackYards != 0)
                throw new Exception("None of these should be greater than 0");

            Assert.IsTrue(true);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Run_xp2_from_97_and_half_not_made()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.SCRIM_PLAY_2XP_RUN;
            int Down = 0;
            double Yards_to_Go = 0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 91
            };
            Game_Player Receiver = new Game_Player()
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
                end_of_play_yardline = 97.5,
                Play_Start_Yardline = 97.5,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = false,
                bFumble_Lost = false,
                bInterception = false,
                Yards_Gained = 2.5,
                bSack = false,
                bSafety = false,
                bTouchback = false,
                bOnePntAfterTDMade = false,
                bOnePntAfterTDMissed = false,
                bTwoPntAfterTDMade = false,
                bTwoPntAfterTDMissed = true,
                bThreePntAfterTDMade = false,
                bThreePntAfterTDMissed = false,
                bCoffinCornerAttemt = false,
                bCoffinCornerMade = false,
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
            if (r.Final_Down != 0 || r.Final_yard_to_go != 0 || r.Final_end_of_Play_Yardline != 0)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || !r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
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
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 ||
                    r.AwayTOP != 0 || r.AwaySacks != 0 || r.AwaySackYards != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 ||
                    r.HomeTOP != 0 || r.HomeSacks != 0 || r.HomeSackYards != 0)
                throw new Exception("None of these should be greater than 0");

            Assert.IsTrue(true);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Run_xp3_from_97_and_half_not_made()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.SCRIM_PLAY_3XP_RUN;
            int Down = 0;
            double Yards_to_Go = 0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 91
            };
            Game_Player Receiver = new Game_Player()
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
                end_of_play_yardline = 97.5,
                Play_Start_Yardline = 97.5,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = false,
                bFumble_Lost = false,
                bInterception = false,
                Yards_Gained = 2.5,
                bSack = false,
                bSafety = false,
                bTouchback = false,
                bOnePntAfterTDMade = false,
                bOnePntAfterTDMissed = false,
                bTwoPntAfterTDMade = false,
                bTwoPntAfterTDMissed = false,
                bThreePntAfterTDMade = false,
                bThreePntAfterTDMissed = true,
                bCoffinCornerAttemt = false,
                bCoffinCornerMade = false,
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
            if (r.Final_Down != 0 || r.Final_yard_to_go != 0 || r.Final_end_of_Play_Yardline != 0)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || !r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
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
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 ||
                    r.AwayTOP != 0 || r.AwaySacks != 0 || r.AwaySackYards != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 ||
                    r.HomeTOP != 0 || r.HomeSacks != 0 || r.HomeSackYards != 0)
                throw new Exception("None of these should be greater than 0");

            Assert.IsTrue(true);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Pass_xp1_from_97_and_half_not_made_penalty_on_Def_Declined()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.SCRIM_PLAY_1XP_PASS;
            int Down = 0;
            double Yards_to_Go = 0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 91
            };
            Game_Player Receiver = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 105
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.DH).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 98
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 97.5,
                Play_Start_Yardline = 97.5,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = true,
                bTouchDown = false,
                bFumble = false,
                bFumble_Lost = false,
                bInterception = false,
                Yards_Gained = 2.5,
                bSack = false,
                bSafety = false,
                bTouchback = false,
                bOnePntAfterTDMade = false,
                bOnePntAfterTDMissed = true,
                bTwoPntAfterTDMade = false,
                bTwoPntAfterTDMissed = false,
                bThreePntAfterTDMade = false,
                bThreePntAfterTDMissed = false,
                bCoffinCornerAttemt = false,
                bCoffinCornerMade = false,
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
            if (r.Final_Down != 0 || r.Final_yard_to_go != 0 || r.Final_end_of_Play_Yardline != 0)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || !r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
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
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 ||
                    r.AwayTOP != 0 || r.AwaySacks != 0 || r.AwaySackYards != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 ||
                    r.HomeTOP != 0 || r.HomeSacks != 0 || r.HomeSackYards != 0)
                throw new Exception("None of these should be greater than 0");

            Assert.IsTrue(true);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Run_xp2_from_97_and_half_not_made_FM_on_def_ignored()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.SCRIM_PLAY_2XP_RUN;
            int Down = 0;
            double Yards_to_Go = 0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 91
            };
            Game_Player Receiver = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 105
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.FM).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 98
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 97.5,
                Play_Start_Yardline = 97.5,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = false,
                bFumble_Lost = false,
                bInterception = false,
                Yards_Gained = 2.5,
                bSack = false,
                bSafety = false,
                bTouchback = false,
                bOnePntAfterTDMade = false,
                bOnePntAfterTDMissed = false,
                bTwoPntAfterTDMade = false,
                bTwoPntAfterTDMissed = true,
                bThreePntAfterTDMade = false,
                bThreePntAfterTDMissed = false,
                bCoffinCornerAttemt = false,
                bCoffinCornerMade = false,
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
            if (r.Final_Down != 0 || r.Final_yard_to_go != 0 || r.Final_end_of_Play_Yardline != 0)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || !r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 0.0 || !r.bIgnorePenalty)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 ||
                    r.AwayTOP != 0 || r.AwaySacks != 0 || r.AwaySackYards != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 ||
                    r.HomeTOP != 0 || r.HomeSacks != 0 || r.HomeSackYards != 0)
                throw new Exception("None of these should be greater than 0");

            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Run_xp2_from_97_and_half_not_made_FM_on_Off_ignored()
        {
            bool penOnBallCarryingTeam = true;
            Play_Enum PE = Play_Enum.SCRIM_PLAY_2XP_RUN;
            int Down = 0;
            double Yards_to_Go = 0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 91
            };
            Game_Player Receiver = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 105
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.FM).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 98
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 97.5,
                Play_Start_Yardline = 97.5,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = false,
                bFumble_Lost = false,
                bInterception = false,
                Yards_Gained = 2.5,
                bSack = false,
                bSafety = false,
                bTouchback = false,
                bOnePntAfterTDMade = false,
                bOnePntAfterTDMissed = false,
                bTwoPntAfterTDMade = false,
                bTwoPntAfterTDMissed = true,
                bThreePntAfterTDMade = false,
                bThreePntAfterTDMissed = false,
                bCoffinCornerAttemt = false,
                bCoffinCornerMade = false,
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
            if (r.Final_Down != 0 || r.Final_yard_to_go != 0 || r.Final_end_of_Play_Yardline != 0)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || !r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 0.0 || !r.bIgnorePenalty)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 ||
                    r.AwayTOP != 0 || r.AwaySacks != 0 || r.AwaySackYards != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 ||
                    r.HomeTOP != 0 || r.HomeSacks != 0 || r.HomeSackYards != 0)
                throw new Exception("None of these should be greater than 0");

            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Run_xp2_from_97_and_half_Delay_of_Game_on_Off()
        {
            bool penOnBallCarryingTeam = true;
            Play_Enum PE = Play_Enum.SCRIM_PLAY_2XP_RUN;
            int Down = 0;
            double Yards_to_Go = 0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 91
            };
            Game_Player Receiver = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 105
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.DG).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 98
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 97.5,
                Play_Start_Yardline = 97.5,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = false,
                bFumble_Lost = false,
                bInterception = false,
                Yards_Gained = 2.5,
                bSack = false,
                bSafety = false,
                bTouchback = false,
                bOnePntAfterTDMade = false,
                bOnePntAfterTDMissed = false,
                bTwoPntAfterTDMade = false,
                bTwoPntAfterTDMissed = false,
                bThreePntAfterTDMade = false,
                bThreePntAfterTDMissed = false,
                bCoffinCornerAttemt = false,
                bCoffinCornerMade = false,
            };

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
            if (r.Final_Down != 0 || r.Final_yard_to_go != 0 || r.Final_end_of_Play_Yardline != 92.5)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (!r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 5.0 || r.bIgnorePenalty)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 ||
                    r.AwayTOP != 0 || r.AwaySacks != 0 || r.AwaySackYards != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 ||
                    r.HomeTOP != 0 || r.HomeSacks != 0 || r.HomeSackYards != 0)
                throw new Exception("None of these should be greater than 0");

            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Pass_xp2_from_97_and_half_Defensive_Holding_Accepted()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.SCRIM_PLAY_2XP_PASS;
            int Down = 0;
            double Yards_to_Go = 0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 91
            };
            Game_Player Receiver = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 105
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.DH).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 98
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 97.5,
                Play_Start_Yardline = 97.5,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = false,
                bFumble_Lost = false,
                bInterception = false,
                Yards_Gained = 2.5,
                bSack = false,
                bSafety = false,
                bTouchback = false,
                bOnePntAfterTDMade = false,
                bOnePntAfterTDMissed = false,
                bTwoPntAfterTDMade = false,
                bTwoPntAfterTDMissed = true,
                bThreePntAfterTDMade = false,
                bThreePntAfterTDMissed = false,
                bCoffinCornerAttemt = false,
                bCoffinCornerMade = false,
            };

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
            if (r.Final_Down != 0 || r.Final_yard_to_go != 0 || r.Final_end_of_Play_Yardline != 98.75)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (!r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 1.25 || r.bIgnorePenalty)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 ||
                    r.AwayTOP != 0 || r.AwaySacks != 0 || r.AwaySackYards != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 ||
                    r.HomeTOP != 0 || r.HomeSacks != 0 || r.HomeSackYards != 0)
                throw new Exception("None of these should be greater than 0");

            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Pass_xp2_from_97_and_half_Defensive_PI_Accepted()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.SCRIM_PLAY_2XP_PASS;
            int Down = 0;
            double Yards_to_Go = 0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 91
            };
            Game_Player Receiver = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 105
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.PI).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 99
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 97.5,
                Play_Start_Yardline = 97.5,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = false,
                bFumble_Lost = false,
                bInterception = false,
                Yards_Gained = 2.5,
                bSack = false,
                bSafety = false,
                bTouchback = false,
                bOnePntAfterTDMade = false,
                bOnePntAfterTDMissed = false,
                bTwoPntAfterTDMade = false,
                bTwoPntAfterTDMissed = true,
                bThreePntAfterTDMade = false,
                bThreePntAfterTDMissed = false,
                bCoffinCornerAttemt = false,
                bCoffinCornerMade = false,
            };

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
            if (r.Final_Down != 0 || r.Final_yard_to_go != 0 || r.Final_end_of_Play_Yardline != 99)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (!r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 1.5 || r.bIgnorePenalty)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 ||
                    r.AwayTOP != 0 || r.AwaySacks != 0 || r.AwaySackYards != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 ||
                    r.HomeTOP != 0 || r.HomeSacks != 0 || r.HomeSackYards != 0)
                throw new Exception("None of these should be greater than 0");

            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Pass_xp2_from_97_and_half_Defensive_PI_in_End_Zone_Accepted()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.SCRIM_PLAY_2XP_PASS;
            int Down = 0;
            double Yards_to_Go = 0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 91
            };
            Game_Player Receiver = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 105
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.PI).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 105
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 97.5,
                Play_Start_Yardline = 97.5,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = false,
                bFumble_Lost = false,
                bInterception = false,
                Yards_Gained = 2.5,
                bSack = false,
                bSafety = false,
                bTouchback = false,
                bOnePntAfterTDMade = false,
                bOnePntAfterTDMissed = false,
                bTwoPntAfterTDMade = false,
                bTwoPntAfterTDMissed = true,
                bThreePntAfterTDMade = false,
                bThreePntAfterTDMissed = false,
                bCoffinCornerAttemt = false,
                bCoffinCornerMade = false,
            };

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
            if (r.Final_Down != 0 || r.Final_yard_to_go != 0 || r.Final_end_of_Play_Yardline != 99)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (!r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 1.5 || r.bIgnorePenalty)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 ||
                    r.AwayTOP != 0 || r.AwaySacks != 0 || r.AwaySackYards != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 ||
                    r.HomeTOP != 0 || r.HomeSacks != 0 || r.HomeSackYards != 0)
                throw new Exception("None of these should be greater than 0");

            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Pass_xp2_from_97_and_half_Intercepted_in_Endzone()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.SCRIM_PLAY_2XP_PASS;
            int Down = 0;
            double Yards_to_Go = 0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 91
            };
            Game_Player Receiver = new Game_Player()
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
                end_of_play_yardline = 97.5,
                Play_Start_Yardline = 97.5,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                bPassAttemted = true,
                bPassComplete = false,
                Receiver = Receiver,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = false,
                bFumble_Lost = false,
                bInterception = true,
                Yards_Gained = 2.5,
                bSack = false,
                bSafety = false,
                bTouchback = false,
                bOnePntAfterTDMade = false,
                bOnePntAfterTDMissed = false,
                bTwoPntAfterTDMade = false,
                bTwoPntAfterTDMissed = true,
                bThreePntAfterTDMade = false,
                bThreePntAfterTDMissed = false,
                bCoffinCornerAttemt = false,
                bCoffinCornerMade = false,
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
            if (r.Final_Down != 0 || r.Final_yard_to_go != 0 || r.Final_end_of_Play_Yardline != 0)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || !r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 0.0 || r.bIgnorePenalty)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 ||
                    r.AwayTOP != 0 || r.AwaySacks != 0 || r.AwaySackYards != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 ||
                    r.HomeTOP != 0 || r.HomeSacks != 0 || r.HomeSackYards != 0)
                throw new Exception("None of these should be greater than 0");

            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Run_xp2_from_97_and_half_Fumble_Lost()
        {
            bool penOnBallCarryingTeam = true;
            Play_Enum PE = Play_Enum.SCRIM_PLAY_2XP_RUN;
            int Down = 0;
            double Yards_to_Go = 0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 91
            };
            Game_Player Receiver = new Game_Player()
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
                end_of_play_yardline = 97.5,
                Play_Start_Yardline = 98.5,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = true,
                bFumble_Lost = true,
                bInterception = false,
                Yards_Gained = 1.0,
                bSack = false,
                bSafety = false,
                bTouchback = false,
                bOnePntAfterTDMade = false,
                bOnePntAfterTDMissed = false,
                bTwoPntAfterTDMade = false,
                bTwoPntAfterTDMissed = true,
                bThreePntAfterTDMade = false,
                bThreePntAfterTDMissed = false,
                bCoffinCornerAttemt = false,
                bCoffinCornerMade = false,
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
            if (r.Final_Down != 0 || r.Final_yard_to_go != 0 || r.Final_end_of_Play_Yardline != 0.0)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || !r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 0.0 || r.bIgnorePenalty)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 ||
                    r.AwayTOP != 0 || r.AwaySacks != 0 || r.AwaySackYards != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 ||
                    r.HomeTOP != 0 || r.HomeSacks != 0 || r.HomeSackYards != 0)
                throw new Exception("None of these should be greater than 0");

            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Right_Pass_xp1_from_2_and_half_made()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.SCRIM_PLAY_1XP_PASS;
            int Down = 0;
            double Yards_to_Go = 0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 9
            };
            Game_Player Receiver = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = -5
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
                end_of_play_yardline = -5.0,
                Play_Start_Yardline = 2.5,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = false,
                bFumble_Lost = false,
                bInterception = false,
                Yards_Gained = 2.5,
                bSack = false,
                bSafety = false,
                bTouchback = false,
                bOnePntAfterTDMade = true,
                bOnePntAfterTDMissed = false,
                bTwoPntAfterTDMade = false,
                bTwoPntAfterTDMissed = false,
                bThreePntAfterTDMade = false,
                bThreePntAfterTDMissed = false,
                bCoffinCornerAttemt = false,
                bCoffinCornerMade = false,
            };

            bool bLefttoRgiht = false;
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

            if (r.Final_Added_Penalty_Yards != 0.0)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || !r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 ||
                    r.AwayTOP != 0 || r.AwaySacks != 0 || r.AwaySackYards != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 ||
                    r.HomeTOP != 0 || r.HomeSacks != 0 || r.HomeSackYards != 0)
                throw new Exception("None of these should be greater than 0");

            Assert.IsTrue(true);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Right_Pass_xp2_from_2_and_half_made()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.SCRIM_PLAY_2XP_PASS;
            int Down = 0;
            double Yards_to_Go = 0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 9
            };
            Game_Player Receiver = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = -5
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
                end_of_play_yardline = -5.0,
                Play_Start_Yardline = 2.5,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = false,
                bFumble_Lost = false,
                bInterception = false,
                Yards_Gained = 2.5,
                bSack = false,
                bSafety = false,
                bTouchback = false,
                bOnePntAfterTDMade = false,
                bOnePntAfterTDMissed = false,
                bTwoPntAfterTDMade = true,
                bTwoPntAfterTDMissed = false,
                bThreePntAfterTDMade = false,
                bThreePntAfterTDMissed = false,
                bCoffinCornerAttemt = false,
                bCoffinCornerMade = false,
            };

            bool bLefttoRgiht = false;
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

            if (r.Final_Added_Penalty_Yards != 0.0)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || !r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 ||
                    r.AwayTOP != 0 || r.AwaySacks != 0 || r.AwaySackYards != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 ||
                    r.HomeTOP != 0 || r.HomeSacks != 0 || r.HomeSackYards != 0)
                throw new Exception("None of these should be greater than 0");

            Assert.IsTrue(true);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Right_Pass_xp3_from_2_and_half_made()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.SCRIM_PLAY_3XP_PASS;
            int Down = 0;
            double Yards_to_Go = 0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 9
            };
            Game_Player Receiver = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = -5
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
                end_of_play_yardline = -5.0,
                Play_Start_Yardline = 2.5,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = false,
                bFumble_Lost = false,
                bInterception = false,
                Yards_Gained = 2.5,
                bSack = false,
                bSafety = false,
                bTouchback = false,
                bOnePntAfterTDMade = false,
                bOnePntAfterTDMissed = false,
                bTwoPntAfterTDMade = false,
                bTwoPntAfterTDMissed = false,
                bThreePntAfterTDMade = true,
                bThreePntAfterTDMissed = false,
                bCoffinCornerAttemt = false,
                bCoffinCornerMade = false,
            };

            bool bLefttoRgiht = false;
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

            if (r.Final_Added_Penalty_Yards != 0.0)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || !r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 ||
                    r.AwayTOP != 0 || r.AwaySacks != 0 || r.AwaySackYards != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 ||
                    r.HomeTOP != 0 || r.HomeSacks != 0 || r.HomeSackYards != 0)
                throw new Exception("None of these should be greater than 0");

            Assert.IsTrue(true);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Right_Run_xp1_from_2_and_half_made()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.SCRIM_PLAY_1XP_RUN;
            int Down = 0;
            double Yards_to_Go = 0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 9
            };
            Game_Player Receiver = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = -5
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
                end_of_play_yardline = -5.0,
                Play_Start_Yardline = 97.5,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = false,
                bFumble_Lost = false,
                bInterception = false,
                Yards_Gained = 2.5,
                bSack = false,
                bSafety = false,
                bTouchback = false,
                bOnePntAfterTDMade = true,
                bOnePntAfterTDMissed = false,
                bTwoPntAfterTDMade = false,
                bTwoPntAfterTDMissed = false,
                bThreePntAfterTDMade = false,
                bThreePntAfterTDMissed = false,
                bCoffinCornerAttemt = false,
                bCoffinCornerMade = false,
            };

            bool bLefttoRgiht = false;
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

            if (r.Final_Added_Penalty_Yards != 0.0)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || !r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 ||
                    r.AwayTOP != 0 || r.AwaySacks != 0 || r.AwaySackYards != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 ||
                    r.HomeTOP != 0 || r.HomeSacks != 0 || r.HomeSackYards != 0)
                throw new Exception("None of these should be greater than 0");

            Assert.IsTrue(true);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Right_Run_xp2_from_2_and_half_made()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.SCRIM_PLAY_2XP_RUN;
            int Down = 0;
            double Yards_to_Go = 0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 9
            };
            Game_Player Receiver = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = -5
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
                end_of_play_yardline = -5.0,
                Play_Start_Yardline = 2.5,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = false,
                bFumble_Lost = false,
                bInterception = false,
                Yards_Gained = 2.5,
                bSack = false,
                bSafety = false,
                bTouchback = false,
                bOnePntAfterTDMade = false,
                bOnePntAfterTDMissed = false,
                bTwoPntAfterTDMade = true,
                bTwoPntAfterTDMissed = false,
                bThreePntAfterTDMade = false,
                bThreePntAfterTDMissed = false,
                bCoffinCornerAttemt = false,
                bCoffinCornerMade = false,
            };

            bool bLefttoRgiht = false;
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

            if (r.Final_Added_Penalty_Yards != 0.0)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || !r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 ||
                    r.AwayTOP != 0 || r.AwaySacks != 0 || r.AwaySackYards != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 ||
                    r.HomeTOP != 0 || r.HomeSacks != 0 || r.HomeSackYards != 0)
                throw new Exception("None of these should be greater than 0");

            Assert.IsTrue(true);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Right_Run_xp3_from_97_and_half_made()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.SCRIM_PLAY_3XP_RUN;
            int Down = 0;
            double Yards_to_Go = 0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 9
            };
            Game_Player Receiver = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = -5
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
                end_of_play_yardline = -5.0,
                Play_Start_Yardline = 2.5,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = false,
                bFumble_Lost = false,
                bInterception = false,
                Yards_Gained = 2.5,
                bSack = false,
                bSafety = false,
                bTouchback = false,
                bOnePntAfterTDMade = false,
                bOnePntAfterTDMissed = false,
                bTwoPntAfterTDMade = false,
                bTwoPntAfterTDMissed = false,
                bThreePntAfterTDMade = true,
                bThreePntAfterTDMissed = false,
                bCoffinCornerAttemt = false,
                bCoffinCornerMade = false,
            };

            bool bLefttoRgiht = false;
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

            if (r.Final_Added_Penalty_Yards != 0.0)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || !r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 ||
                    r.AwayTOP != 0 || r.AwaySacks != 0 || r.AwaySackYards != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 ||
                    r.HomeTOP != 0 || r.HomeSacks != 0 || r.HomeSackYards != 0)
                throw new Exception("None of these should be greater than 0");

            Assert.IsTrue(true);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Right_Pass_xp1_from_2_and_half_made_penalty_on_Def_Declined()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.SCRIM_PLAY_1XP_PASS;
            int Down = 0;
            double Yards_to_Go = 0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 9
            };
            Game_Player Receiver = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = -5
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.DH).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 2
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = -5.0,
                Play_Start_Yardline = 2.5,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = true,
                bTouchDown = false,
                bFumble = false,
                bFumble_Lost = false,
                bInterception = false,
                Yards_Gained = 2.5,
                bSack = false,
                bSafety = false,
                bTouchback = false,
                bOnePntAfterTDMade = true,
                bOnePntAfterTDMissed = false,
                bTwoPntAfterTDMade = false,
                bTwoPntAfterTDMissed = false,
                bThreePntAfterTDMade = false,
                bThreePntAfterTDMissed = false,
                bCoffinCornerAttemt = false,
                bCoffinCornerMade = false,
            };

            bool bLefttoRgiht = false;
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

            if (r.Final_Added_Penalty_Yards != 0.0)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || !r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 ||
                    r.AwayTOP != 0 || r.AwaySacks != 0 || r.AwaySackYards != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 ||
                    r.HomeTOP != 0 || r.HomeSacks != 0 || r.HomeSackYards != 0)
                throw new Exception("None of these should be greater than 0");

            Assert.IsTrue(true);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Right_Run_xp2_from_2_and_half_made_FM_on_def_ignored()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.SCRIM_PLAY_2XP_RUN;
            int Down = 0;
            double Yards_to_Go = 0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 9
            };
            Game_Player Receiver = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = -5
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.FM).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 2
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = -5.0,
                Play_Start_Yardline = 2.5,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = false,
                bFumble_Lost = false,
                bInterception = false,
                Yards_Gained = 2.5,
                bSack = false,
                bSafety = false,
                bTouchback = false,
                bOnePntAfterTDMade = false,
                bOnePntAfterTDMissed = false,
                bTwoPntAfterTDMade = true,
                bTwoPntAfterTDMissed = false,
                bThreePntAfterTDMade = false,
                bThreePntAfterTDMissed = false,
                bCoffinCornerAttemt = false,
                bCoffinCornerMade = false,
            };

            bool bLefttoRgiht = false;
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

            if (r.Final_Added_Penalty_Yards != 0.0 || !r.bIgnorePenalty)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || !r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 ||
                    r.AwayTOP != 0 || r.AwaySacks != 0 || r.AwaySackYards != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 ||
                    r.HomeTOP != 0 || r.HomeSacks != 0 || r.HomeSackYards != 0)
                throw new Exception("None of these should be greater than 0");

            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Right_Run_xp2_from_2_and_half_made_FM_on_Off_ignored()
        {
            bool penOnBallCarryingTeam = true;
            Play_Enum PE = Play_Enum.SCRIM_PLAY_2XP_RUN;
            int Down = 0;
            double Yards_to_Go = 0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 9
            };
            Game_Player Receiver = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = -5
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.FM).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 2
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = -5.0,
                Play_Start_Yardline = 2.5,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = false,
                bFumble_Lost = false,
                bInterception = false,
                Yards_Gained = 2.5,
                bSack = false,
                bSafety = false,
                bTouchback = false,
                bOnePntAfterTDMade = false,
                bOnePntAfterTDMissed = false,
                bTwoPntAfterTDMade = true,
                bTwoPntAfterTDMissed = false,
                bThreePntAfterTDMade = false,
                bThreePntAfterTDMissed = false,
                bCoffinCornerAttemt = false,
                bCoffinCornerMade = false,
            };

            bool bLefttoRgiht = false;
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

            if (r.Final_Added_Penalty_Yards != 0.0 || !r.bIgnorePenalty)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || !r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 ||
                    r.AwayTOP != 0 || r.AwaySacks != 0 || r.AwaySackYards != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 ||
                    r.HomeTOP != 0 || r.HomeSacks != 0 || r.HomeSackYards != 0)
                throw new Exception("None of these should be greater than 0");

            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Right_Pass_xp1_from_2_and_half_not_made()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.SCRIM_PLAY_1XP_PASS;
            int Down = 0;
            double Yards_to_Go = 0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 9
            };
            Game_Player Receiver = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = -5
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
                end_of_play_yardline = 2.5,
                Play_Start_Yardline = 2.5,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = false,
                bFumble_Lost = false,
                bInterception = false,
                Yards_Gained = 2.5,
                bSack = false,
                bSafety = false,
                bTouchback = false,
                bOnePntAfterTDMade = false,
                bOnePntAfterTDMissed = true,
                bTwoPntAfterTDMade = false,
                bTwoPntAfterTDMissed = false,
                bThreePntAfterTDMade = false,
                bThreePntAfterTDMissed = false,
                bCoffinCornerAttemt = false,
                bCoffinCornerMade = false,
            };

            bool bLefttoRgiht = false;
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

            if (r.Final_Added_Penalty_Yards != 0.0)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 ||
                    r.AwayTOP != 0 || r.AwaySacks != 0 || r.AwaySackYards != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 ||
                    r.HomeTOP != 0 || r.HomeSacks != 0 || r.HomeSackYards != 0)
                throw new Exception("None of these should be greater than 0");

            Assert.IsTrue(true);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Right_Pass_xp2_from_2_and_half_not_made()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.SCRIM_PLAY_2XP_PASS;
            int Down = 0;
            double Yards_to_Go = 0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 9
            };
            Game_Player Receiver = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = -5
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
                end_of_play_yardline = 97.5,
                Play_Start_Yardline = 97.5,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = false,
                bFumble_Lost = false,
                bInterception = false,
                Yards_Gained = 2.5,
                bSack = false,
                bSafety = false,
                bTouchback = false,
                bOnePntAfterTDMade = false,
                bOnePntAfterTDMissed = false,
                bTwoPntAfterTDMade = false,
                bTwoPntAfterTDMissed = true,
                bThreePntAfterTDMade = false,
                bThreePntAfterTDMissed = false,
                bCoffinCornerAttemt = false,
                bCoffinCornerMade = false,
            };

            bool bLefttoRgiht = false;
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

            if (r.Final_Added_Penalty_Yards != 0.0)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 ||
                    r.AwayTOP != 0 || r.AwaySacks != 0 || r.AwaySackYards != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 ||
                    r.HomeTOP != 0 || r.HomeSacks != 0 || r.HomeSackYards != 0)
                throw new Exception("None of these should be greater than 0");

            Assert.IsTrue(true);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Right_Pass_xp3_from_2_and_half_not_made()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.SCRIM_PLAY_3XP_PASS;
            int Down = 0;
            double Yards_to_Go = 0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 9
            };
            Game_Player Receiver = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = -5
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
                end_of_play_yardline = 2.5,
                Play_Start_Yardline = 2.5,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = false,
                bFumble_Lost = false,
                bInterception = false,
                Yards_Gained = 2.5,
                bSack = false,
                bSafety = false,
                bTouchback = false,
                bOnePntAfterTDMade = false,
                bOnePntAfterTDMissed = false,
                bTwoPntAfterTDMade = false,
                bTwoPntAfterTDMissed = false,
                bThreePntAfterTDMade = false,
                bThreePntAfterTDMissed = true,
                bCoffinCornerAttemt = false,
                bCoffinCornerMade = false,
            };

            bool bLefttoRgiht = false;
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

            if (r.Final_Added_Penalty_Yards != 0.0)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 ||
                    r.AwayTOP != 0 || r.AwaySacks != 0 || r.AwaySackYards != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 ||
                    r.HomeTOP != 0 || r.HomeSacks != 0 || r.HomeSackYards != 0)
                throw new Exception("None of these should be greater than 0");

            Assert.IsTrue(true);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Right_Run_xp1_from_2_and_half_not_made()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.SCRIM_PLAY_1XP_RUN;
            int Down = 0;
            double Yards_to_Go = 0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 9
            };
            Game_Player Receiver = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = -5
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
                end_of_play_yardline = 2.5,
                Play_Start_Yardline = 2.5,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = false,
                bFumble_Lost = false,
                bInterception = false,
                Yards_Gained = 2.5,
                bSack = false,
                bSafety = false,
                bTouchback = false,
                bOnePntAfterTDMade = false,
                bOnePntAfterTDMissed = true,
                bTwoPntAfterTDMade = false,
                bTwoPntAfterTDMissed = false,
                bThreePntAfterTDMade = false,
                bThreePntAfterTDMissed = false,
                bCoffinCornerAttemt = false,
                bCoffinCornerMade = false,
            };

            bool bLefttoRgiht = false;
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

            if (r.Final_Added_Penalty_Yards != 0.0)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 ||
                    r.AwayTOP != 0 || r.AwaySacks != 0 || r.AwaySackYards != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 ||
                    r.HomeTOP != 0 || r.HomeSacks != 0 || r.HomeSackYards != 0)
                throw new Exception("None of these should be greater than 0");

            Assert.IsTrue(true);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Right_Run_xp2_from_2_and_half_not_made()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.SCRIM_PLAY_2XP_RUN;
            int Down = 0;
            double Yards_to_Go = 0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 9
            };
            Game_Player Receiver = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = -5
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
                end_of_play_yardline = 2.5,
                Play_Start_Yardline = 2.5,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = false,
                bFumble_Lost = false,
                bInterception = false,
                Yards_Gained = 2.5,
                bSack = false,
                bSafety = false,
                bTouchback = false,
                bOnePntAfterTDMade = false,
                bOnePntAfterTDMissed = false,
                bTwoPntAfterTDMade = false,
                bTwoPntAfterTDMissed = true,
                bThreePntAfterTDMade = false,
                bThreePntAfterTDMissed = false,
                bCoffinCornerAttemt = false,
                bCoffinCornerMade = false,
            };

            bool bLefttoRgiht = false;
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

            if (r.Final_Added_Penalty_Yards != 0.0)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 ||
                    r.AwayTOP != 0 || r.AwaySacks != 0 || r.AwaySackYards != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 ||
                    r.HomeTOP != 0 || r.HomeSacks != 0 || r.HomeSackYards != 0)
                throw new Exception("None of these should be greater than 0");

            Assert.IsTrue(true);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Right_Run_xp3_from_2_and_half_not_made()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.SCRIM_PLAY_3XP_RUN;
            int Down = 0;
            double Yards_to_Go = 0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 9
            };
            Game_Player Receiver = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = -5
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
                end_of_play_yardline = 2.5,
                Play_Start_Yardline = 2.5,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = false,
                bFumble_Lost = false,
                bInterception = false,
                Yards_Gained = 2.5,
                bSack = false,
                bSafety = false,
                bTouchback = false,
                bOnePntAfterTDMade = false,
                bOnePntAfterTDMissed = false,
                bTwoPntAfterTDMade = false,
                bTwoPntAfterTDMissed = false,
                bThreePntAfterTDMade = false,
                bThreePntAfterTDMissed = true,
                bCoffinCornerAttemt = false,
                bCoffinCornerMade = false,
            };

            bool bLefttoRgiht = false;
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

            if (r.Final_Added_Penalty_Yards != 0.0)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 ||
                    r.AwayTOP != 0 || r.AwaySacks != 0 || r.AwaySackYards != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 ||
                    r.HomeTOP != 0 || r.HomeSacks != 0 || r.HomeSackYards != 0)
                throw new Exception("None of these should be greater than 0");

            Assert.IsTrue(true);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Right_Pass_xp1_from_2_and_half_not_made_penalty_on_Def_Declined()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.SCRIM_PLAY_1XP_PASS;
            int Down = 0;
            double Yards_to_Go = 0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 9
            };
            Game_Player Receiver = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = -5
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.DH).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 2
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = -5.0,
                Play_Start_Yardline = 2.5,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = true,
                bTouchDown = false,
                bFumble = false,
                bFumble_Lost = false,
                bInterception = false,
                Yards_Gained = 2.5,
                bSack = false,
                bSafety = false,
                bTouchback = false,
                bOnePntAfterTDMade = false,
                bOnePntAfterTDMissed = true,
                bTwoPntAfterTDMade = false,
                bTwoPntAfterTDMissed = false,
                bThreePntAfterTDMade = false,
                bThreePntAfterTDMissed = false,
                bCoffinCornerAttemt = false,
                bCoffinCornerMade = false,
            };

            bool bLefttoRgiht = false;
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

            if (r.Final_Added_Penalty_Yards != 0.0)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 ||
                    r.AwayTOP != 0 || r.AwaySacks != 0 || r.AwaySackYards != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 ||
                    r.HomeTOP != 0 || r.HomeSacks != 0 || r.HomeSackYards != 0)
                throw new Exception("None of these should be greater than 0");

            Assert.IsTrue(true);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Right_Run_xp2_from_2_and_half_not_made_FM_on_def_ignored()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.SCRIM_PLAY_2XP_RUN;
            int Down = 0;
            double Yards_to_Go = 0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 9
            };
            Game_Player Receiver = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = -5
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.FM).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 2
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 2.5,
                Play_Start_Yardline = 2.5,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = false,
                bFumble_Lost = false,
                bInterception = false,
                Yards_Gained = 2.5,
                bSack = false,
                bSafety = false,
                bTouchback = false,
                bOnePntAfterTDMade = false,
                bOnePntAfterTDMissed = false,
                bTwoPntAfterTDMade = false,
                bTwoPntAfterTDMissed = true,
                bThreePntAfterTDMade = false,
                bThreePntAfterTDMissed = false,
                bCoffinCornerAttemt = false,
                bCoffinCornerMade = false,
            };

            bool bLefttoRgiht = false;
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

            if (r.Final_Added_Penalty_Yards != 0.0 || !r.bIgnorePenalty)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 ||
                    r.AwayTOP != 0 || r.AwaySacks != 0 || r.AwaySackYards != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 ||
                    r.HomeTOP != 0 || r.HomeSacks != 0 || r.HomeSackYards != 0)
                throw new Exception("None of these should be greater than 0");

            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Right_Run_xp2_from_2_and_half_not_made_FM_on_Off_ignored()
        {
            bool penOnBallCarryingTeam = true;
            Play_Enum PE = Play_Enum.SCRIM_PLAY_2XP_RUN;
            int Down = 0;
            double Yards_to_Go = 0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 9
            };
            Game_Player Receiver = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = -5
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.FM).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 2
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = -5.0,
                Play_Start_Yardline = 2.5,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = false,
                bFumble_Lost = false,
                bInterception = false,
                Yards_Gained = 2.5,
                bSack = false,
                bSafety = false,
                bTouchback = false,
                bOnePntAfterTDMade = false,
                bOnePntAfterTDMissed = false,
                bTwoPntAfterTDMade = false,
                bTwoPntAfterTDMissed = true,
                bThreePntAfterTDMade = false,
                bThreePntAfterTDMissed = false,
                bCoffinCornerAttemt = false,
                bCoffinCornerMade = false,
            };

            bool bLefttoRgiht = false;
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

            if (r.Final_Added_Penalty_Yards != 0.0 || !r.bIgnorePenalty)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 ||
                    r.AwayTOP != 0 || r.AwaySacks != 0 || r.AwaySackYards != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 ||
                    r.HomeTOP != 0 || r.HomeSacks != 0 || r.HomeSackYards != 0)
                throw new Exception("None of these should be greater than 0");

            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Right_Run_xp2_from_2_and_half_Delay_of_Game_on_Off()
        {
            bool penOnBallCarryingTeam = true;
            Play_Enum PE = Play_Enum.SCRIM_PLAY_2XP_RUN;
            int Down = 0;
            double Yards_to_Go = 0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 9
            };
            Game_Player Receiver = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 2.5
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.DG).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 98
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 2.5,
                Play_Start_Yardline = 2.5,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = false,
                bFumble_Lost = false,
                bInterception = false,
                Yards_Gained = 2.5,
                bSack = false,
                bSafety = false,
                bTouchback = false,
                bOnePntAfterTDMade = false,
                bOnePntAfterTDMissed = false,
                bTwoPntAfterTDMade = false,
                bTwoPntAfterTDMissed = false,
                bThreePntAfterTDMade = false,
                bThreePntAfterTDMissed = false,
                bCoffinCornerAttemt = false,
                bCoffinCornerMade = false,
            };

            bool bLefttoRgiht = false;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult, bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (r.bPlay_Stands)
                throw new Exception("This Play must not stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 0 || r.Final_yard_to_go != 0 || r.Final_end_of_Play_Yardline != 7.5)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (!r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 5.0 || r.bIgnorePenalty)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 ||
                    r.AwayTOP != 0 || r.AwaySacks != 0 || r.AwaySackYards != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 ||
                    r.HomeTOP != 0 || r.HomeSacks != 0 || r.HomeSackYards != 0)
                throw new Exception("None of these should be greater than 0");

            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Right_Pass_xp2_from_2_and_half_Defensive_Holding_Accepted()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.SCRIM_PLAY_2XP_PASS;
            int Down = 0;
            double Yards_to_Go = 0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 9
            };
            Game_Player Receiver = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = -5
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.DH).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 2
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 2.5,
                Play_Start_Yardline = 2.5,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = false,
                bFumble_Lost = false,
                bInterception = false,
                Yards_Gained = 2.5,
                bSack = false,
                bSafety = false,
                bTouchback = false,
                bOnePntAfterTDMade = false,
                bOnePntAfterTDMissed = false,
                bTwoPntAfterTDMade = false,
                bTwoPntAfterTDMissed = true,
                bThreePntAfterTDMade = false,
                bThreePntAfterTDMissed = false,
                bCoffinCornerAttemt = false,
                bCoffinCornerMade = false,
            };

            bool bLefttoRgiht = false;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult, bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (r.bPlay_Stands)
                throw new Exception("This Play must not stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 0 || r.Final_yard_to_go != 0 || r.Final_end_of_Play_Yardline != 1.25)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (!r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 1.25 || r.bIgnorePenalty)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 ||
                    r.AwayTOP != 0 || r.AwaySacks != 0 || r.AwaySackYards != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 ||
                    r.HomeTOP != 0 || r.HomeSacks != 0 || r.HomeSackYards != 0)
                throw new Exception("None of these should be greater than 0");

            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Right_Pass_xp2_from_2_and_half_Defensive_PI_Accepted()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.SCRIM_PLAY_2XP_PASS;
            int Down = 0;
            double Yards_to_Go = 0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 9
            };
            Game_Player Receiver = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = -5
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.PI).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 1
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 2.5,
                Play_Start_Yardline = 2.5,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = false,
                bFumble_Lost = false,
                bInterception = false,
                Yards_Gained = 2.5,
                bSack = false,
                bSafety = false,
                bTouchback = false,
                bOnePntAfterTDMade = false,
                bOnePntAfterTDMissed = false,
                bTwoPntAfterTDMade = false,
                bTwoPntAfterTDMissed = true,
                bThreePntAfterTDMade = false,
                bThreePntAfterTDMissed = false,
                bCoffinCornerAttemt = false,
                bCoffinCornerMade = false,
            };

            bool bLefttoRgiht = false;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult, bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (r.bPlay_Stands)
                throw new Exception("This Play must not stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 0 || r.Final_yard_to_go != 0 || r.Final_end_of_Play_Yardline != 1)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (!r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 1.5 || r.bIgnorePenalty)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 ||
                    r.AwayTOP != 0 || r.AwaySacks != 0 || r.AwaySackYards != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 ||
                    r.HomeTOP != 0 || r.HomeSacks != 0 || r.HomeSackYards != 0)
                throw new Exception("None of these should be greater than 0");

            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Right_Pass_xp2_from_2_and_half_Defensive_PI_in_End_Zone_Accepted()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.SCRIM_PLAY_2XP_PASS;
            int Down = 0;
            double Yards_to_Go = 0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 9
            };
            Game_Player Receiver = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = -5
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.PI).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = -5
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 2.5,
                Play_Start_Yardline = 2.5,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = false,
                bFumble_Lost = false,
                bInterception = false,
                Yards_Gained = 2.5,
                bSack = false,
                bSafety = false,
                bTouchback = false,
                bOnePntAfterTDMade = false,
                bOnePntAfterTDMissed = false,
                bTwoPntAfterTDMade = false,
                bTwoPntAfterTDMissed = true,
                bThreePntAfterTDMade = false,
                bThreePntAfterTDMissed = false,
                bCoffinCornerAttemt = false,
                bCoffinCornerMade = false,
            };

            bool bLefttoRgiht = false;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult, bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (r.bPlay_Stands)
                throw new Exception("This Play must not stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 0 || r.Final_yard_to_go != 0 || r.Final_end_of_Play_Yardline !=1)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (!r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 1.5 || r.bIgnorePenalty)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 ||
                    r.AwayTOP != 0 || r.AwaySacks != 0 || r.AwaySackYards != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 ||
                    r.HomeTOP != 0 || r.HomeSacks != 0 || r.HomeSackYards != 0)
                throw new Exception("None of these should be greater than 0");

            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Right_Pass_xp2_from_2_and_half_Intercepted_in_Endzone()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.SCRIM_PLAY_2XP_PASS;
            int Down = 0;
            double Yards_to_Go = 0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 9
            };
            Game_Player Receiver = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = -5
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
                end_of_play_yardline = 2.5,
                Play_Start_Yardline = 2.5,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                bPassAttemted = true,
                bPassComplete = false,
                Receiver = Receiver,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = false,
                bFumble_Lost = false,
                bInterception = true,
                Yards_Gained = 2.5,
                bSack = false,
                bSafety = false,
                bTouchback = false,
                bOnePntAfterTDMade = false,
                bOnePntAfterTDMissed = false,
                bTwoPntAfterTDMade = false,
                bTwoPntAfterTDMissed = true,
                bThreePntAfterTDMade = false,
                bThreePntAfterTDMissed = false,
                bCoffinCornerAttemt = false,
                bCoffinCornerMade = false,
            };

            bool bLefttoRgiht = false;
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

            if (r.Final_Added_Penalty_Yards != 0.0 || r.bIgnorePenalty)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 ||
                    r.AwayTOP != 0 || r.AwaySacks != 0 || r.AwaySackYards != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 ||
                    r.HomeTOP != 0 || r.HomeSacks != 0 || r.HomeSackYards != 0)
                throw new Exception("None of these should be greater than 0");

            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Right_Run_xp2_from_2_and_half_Fumble_Lost()
        {
            bool penOnBallCarryingTeam = true;
            Play_Enum PE = Play_Enum.SCRIM_PLAY_2XP_RUN;
            int Down = 0;
            double Yards_to_Go = 0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 9
            };
            Game_Player Receiver = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = -5
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
                end_of_play_yardline = 2.5,
                Play_Start_Yardline = 1.5,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = true,
                bFumble_Lost = true,
                bInterception = false,
                Yards_Gained = 1.0,
                bSack = false,
                bSafety = false,
                bTouchback = false,
                bOnePntAfterTDMade = false,
                bOnePntAfterTDMissed = false,
                bTwoPntAfterTDMade = false,
                bTwoPntAfterTDMissed = true,
                bThreePntAfterTDMade = false,
                bThreePntAfterTDMissed = false,
                bCoffinCornerAttemt = false,
                bCoffinCornerMade = false,
            };

            bool bLefttoRgiht = false;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult, bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must not stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 0 || r.Final_yard_to_go != 0 || r.Final_end_of_Play_Yardline != 0.0)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || !r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 0.0 || r.bIgnorePenalty)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 ||
                    r.AwayTOP != 0 || r.AwaySacks != 0 || r.AwaySackYards != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 ||
                    r.HomeTOP != 0 || r.HomeSacks != 0 || r.HomeSackYards != 0)
                throw new Exception("None of these should be greater than 0");

            Assert.IsTrue(true);
        }


    }
}
