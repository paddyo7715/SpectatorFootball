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
    public class setPlayOutCome_FG_TEST
    {
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
                Play_Start_Yardline = 80.0,
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
                pResult, bLefttoRgiht, TouchBack_Yardline);

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
                pResult, bLefttoRgiht, TouchBack_Yardline);

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
                pResult, bLefttoRgiht, TouchBack_Yardline);

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
                pResult, bLefttoRgiht, TouchBack_Yardline);

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
                pResult, bLefttoRgiht, TouchBack_Yardline);

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
                pResult, bLefttoRgiht, TouchBack_Yardline);

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
                pResult, bLefttoRgiht, TouchBack_Yardline);

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
                Play_Start_Yardline = 80.0,
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
        public void setPlayOutCome_FG_Rightt_Good_No_Penalty()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.FIELD_GOAL;
            int Down = 4;
            double Yards_to_Go = 10.0;

            Game_Player kicker = new Game_Player()
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
                end_of_play_yardline = 20.0,
                Play_Start_Yardline = 20.0,
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
        public void setPlayOutCome_FG_Rightt_Not_Good_No_Penalty()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.FIELD_GOAL;
            int Down = 4;
            double Yards_to_Go = 10.0;

            Game_Player kicker = new Game_Player()
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
                end_of_play_yardline = 20.0,
                Play_Start_Yardline = 20.0,
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
            bool bLefttoRgiht = false;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult, bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (!r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must not stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 20)
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
        public void setPlayOutCome_FG_Rightt_Delay_of_Game_Penalty()
        {
            bool penOnBallCarryingTeam = true;
            Play_Enum PE = Play_Enum.FIELD_GOAL;
            int Down = 4;
            double Yards_to_Go = 10.0;

            Game_Player kicker = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 30
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.DG).First();
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
                end_of_play_yardline = 20.0,
                Play_Start_Yardline = 20.0,
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
            if (r.Final_Down != 4 || r.Final_yard_to_go != 15 || r.Final_end_of_Play_Yardline != 25)
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
        public void setPlayOutCome_FG_Rightt_false_start_Penalty()
        {
            bool penOnBallCarryingTeam = true;
            Play_Enum PE = Play_Enum.FIELD_GOAL;
            int Down = 4;
            double Yards_to_Go = 10.0;

            Game_Player kicker = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 30
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.FS).First();
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
                end_of_play_yardline = 20.0,
                Play_Start_Yardline = 20.0,
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
            if (r.Final_Down != 4 || r.Final_yard_to_go != 15 || r.Final_end_of_Play_Yardline != 25)
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
        public void setPlayOutCome_FG_Rightt_NZ_Penalty()
        {
            bool penOnBallCarryingTeam = true;
            Play_Enum PE = Play_Enum.FIELD_GOAL;
            int Down = 4;
            double Yards_to_Go = 10.0;

            Game_Player kicker = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 30
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.NZ).First();
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
                end_of_play_yardline = 20.0,
                Play_Start_Yardline = 20.0,
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
            if (r.Final_Down != 4 || r.Final_yard_to_go != 15 || r.Final_end_of_Play_Yardline != 25)
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
        public void setPlayOutCome_FG_Rightt_Missed_Def_Offsides_Accepted()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.FIELD_GOAL;
            int Down = 4;
            double Yards_to_Go = 10.0;

            Game_Player kicker = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 30
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.DO).First();
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
                end_of_play_yardline = 20.0,
                Play_Start_Yardline = 20.0,
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
            if (r.Final_Down != 4 || r.Final_yard_to_go != 5 || r.Final_end_of_Play_Yardline != 15)
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
        public void setPlayOutCome_FG_Rightt_Missed_Def_Offsides_Accepted_First_down()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.FIELD_GOAL;
            int Down = 4;
            double Yards_to_Go = 2.0;

            Game_Player kicker = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 30
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.DO).First();
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
                end_of_play_yardline = 20.0,
                Play_Start_Yardline = 20.0,
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
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 15)
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
        public void setPlayOutCome_FG_Rightt_Missed_Def_Offsides_Accepted_at_5_to_2point5()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.FIELD_GOAL;
            int Down = 4;
            double Yards_to_Go = 8.0;

            Game_Player kicker = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 20
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.DO).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                Current_YardLine = 3.0
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 5.0,
                Play_Start_Yardline = 5.0,
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
            if (r.Final_Down != 4 || r.Final_yard_to_go != 5.5 || r.Final_end_of_Play_Yardline != 2.5)
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
        public void setPlayOutCome_FG_Rightt_Good_Def_Offsides_Accepted_First_down()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.FIELD_GOAL;
            int Down = 4;
            double Yards_to_Go = 2.0;

            Game_Player kicker = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 30
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.DO).First();
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
                end_of_play_yardline = 20.0,
                Play_Start_Yardline = 20.0,
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
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 15)
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
        public void setPlayOutCome_FG_Rightt_Good_Def_Offsides_Declined()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.FIELD_GOAL;
            int Down = 4;
            double Yards_to_Go = 2.0;

            Game_Player kicker = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 30
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.DO).First();
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
                end_of_play_yardline = 20.0,
                Play_Start_Yardline = 20.0,
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
    }
}
