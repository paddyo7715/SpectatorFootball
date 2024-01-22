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
    public class setPlayOutCome_OnsideKick_TEST
    {
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
                pResult, bLefttoRgiht, TouchBack_Yardline);

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
                pResult, bLefttoRgiht, TouchBack_Yardline);

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
                pResult, bLefttoRgiht, TouchBack_Yardline);

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
                pResult, bLefttoRgiht, TouchBack_Yardline);

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
                pResult, bLefttoRgiht, TouchBack_Yardline);

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
                pResult, bLefttoRgiht, TouchBack_Yardline);

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
                pResult, bLefttoRgiht, TouchBack_Yardline);

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
                pResult, bLefttoRgiht, TouchBack_Yardline);

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
                pResult, bLefttoRgiht, TouchBack_Yardline);

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
                pResult, bLefttoRgiht, TouchBack_Yardline);

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

    }
}
