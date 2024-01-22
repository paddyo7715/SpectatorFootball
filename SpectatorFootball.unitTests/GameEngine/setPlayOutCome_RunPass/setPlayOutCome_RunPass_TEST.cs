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
    public class setPlayOutCome_RunPass_TEST
    {
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

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Run_Left_4th_and_2_2Yard_Gain_FirstDown_4thDownConv()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.RUN;
            int Down = 4;
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

            if (r.AwayFirstDowns != 1 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 1 || r.Away4thDownMade != 1 || r.AwayXP1Attempt != 0 ||
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
        public void setPlayOutCome_Pass_Left_4th_and_2_2_Yard_Gain_FirstDown_4thdownConv()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.PASS;
            int Down = 4;
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

            if (r.AwayFirstDowns != 1 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 1 || r.Away4thDownMade != 1 || r.AwayXP1Attempt != 0 ||
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

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Run_Left_3rd_and_3_1Yard_Gain_no_3rdDownConv()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.RUN;
            int Down = 3;
            double Yards_to_Go = 3.0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 15
            };
            Game_Player RB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 21
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
                end_of_play_yardline = 21.0,
                Play_Start_Yardline = 20,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Running_Back = RB,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = false,
                bFumble_Lost = false,
                Yards_Gained = 1
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = RB.p_and_r.p.ID,
                off_rush_att = 1,
                off_rush_plays = 1,
                off_rush_Yards = 1,
                off_rush_long = 1,
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
            if (r.Final_Down != 4 || r.Final_yard_to_go != 2 || r.Final_end_of_Play_Yardline != 21)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 0)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 1 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 1 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
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
        public void setPlayOutCome_Pass_Left_4th_and_3_1_Yard_Gain_No_4thdownConv()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.PASS;
            int Down = 4;
            double Yards_to_Go = 3.0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 15
            };
            Game_Player Receiver = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 21
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
                end_of_play_yardline = 21.0,
                Play_Start_Yardline = 20,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = false,
                bFumble_Lost = false,
                Yards_Gained = 1
            };

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
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 21)
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
                    r.Away4thDownAtt != 1 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 1 ||
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
        public void setPlayOutCome_Run_Left_4th_and_3_1Yard_Gain_no_4thDownConv()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.RUN;
            int Down = 4;
            double Yards_to_Go = 3.0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 15
            };
            Game_Player RB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 21
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
                end_of_play_yardline = 21.0,
                Play_Start_Yardline = 20,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Running_Back = RB,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = false,
                bFumble_Lost = false,
                Yards_Gained = 1
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = RB.p_and_r.p.ID,
                off_rush_att = 1,
                off_rush_plays = 1,
                off_rush_Yards = 1,
                off_rush_long = 1,
                off_rush_TDs = 0,
                off_rush_fumbles = 0,
                off_rush_fumbles_lost = 0
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
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 21)
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
                    r.Away4thDownAtt != 1 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 1 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
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
        public void setPlayOutCome_Pass_Left_3rd_and_3_1_Yard_Gain_No_3rddownConv()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.PASS;
            int Down = 3;
            double Yards_to_Go = 3.0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 15
            };
            Game_Player Receiver = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 21
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
                end_of_play_yardline = 21.0,
                Play_Start_Yardline = 20,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = false,
                bFumble_Lost = false,
                Yards_Gained = 1
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
            if (r.Final_Down != 4 || r.Final_yard_to_go != 2 || r.Final_end_of_Play_Yardline != 21)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 0)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 1 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 1 ||
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
        public void setPlayOutCome_Run_Left_2nd_and_3_from_95_TD_and_FirstDown()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.RUN;
            int Down = 2;
            double Yards_to_Go = 3.0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 15
            };
            Game_Player RB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 21
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
                Play_Start_Yardline = 95,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Running_Back = RB,
                bPenalty_Rejected = false,
                bTouchDown = true,
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
                off_rush_TDs = 1,
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
            if (r.Final_Down != 0 || r.Final_yard_to_go != 0 || r.Final_end_of_Play_Yardline != 0)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (!r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 0)
                throw new Exception("There was no penalty on this play");

            if (!r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 1 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
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
        public void setPlayOutCome_Pass_Left_2nd_and_3_from_95_TD_and_FirstDown()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.PASS;
            int Down = 2;
            double Yards_to_Go = 3.0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 15
            };
            Game_Player Receiver = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 21
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
                Play_Start_Yardline = 95,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = false,
                bTouchDown = true,
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
            if (r.Final_Down != 0 || r.Final_yard_to_go != 0 || r.Final_end_of_Play_Yardline != 0)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (!r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 0)
                throw new Exception("There was no penalty on this play");

            if (!r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 1 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
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
        public void setPlayOutCome_Run_Left_3rd_and_8_from_95_TD_no_FirstDown_or_3rdDownConv()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.RUN;
            int Down = 3;
            double Yards_to_Go = 8.0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 15
            };
            Game_Player RB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 21
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
                Play_Start_Yardline = 95,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Running_Back = RB,
                bPenalty_Rejected = false,
                bTouchDown = true,
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
                off_rush_TDs = 1,
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
            if (r.Final_Down != 0 || r.Final_yard_to_go != 0 || r.Final_end_of_Play_Yardline != 0)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (!r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 0)
                throw new Exception("There was no penalty on this play");

            if (!r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
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
        public void setPlayOutCome_Pass_Left_3rd_and_8_from_95_TD_no_FirstDown_or_3rdDownConv()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.PASS;
            int Down = 3;
            double Yards_to_Go = 8.0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 15
            };
            Game_Player Receiver = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 21
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
                Play_Start_Yardline = 95,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = false,
                bTouchDown = true,
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
            if (r.Final_Down != 0 || r.Final_yard_to_go != 0 || r.Final_end_of_Play_Yardline != 0)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (!r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 0)
                throw new Exception("There was no penalty on this play");

            if (!r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
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
        public void setPlayOutCome_Run_Left_4th_and_8_from_95_TD_no_FirstDown_or_4thDownConv()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.RUN;
            int Down = 4;
            double Yards_to_Go = 8.0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 15
            };
            Game_Player RB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 21
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
                Play_Start_Yardline = 95,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Running_Back = RB,
                bPenalty_Rejected = false,
                bTouchDown = true,
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
                off_rush_TDs = 1,
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
            if (r.Final_Down != 0 || r.Final_yard_to_go != 0 || r.Final_end_of_Play_Yardline != 0)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (!r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 0)
                throw new Exception("There was no penalty on this play");

            if (!r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
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
        public void setPlayOutCome_Pass_Left_4th_and_8_from_95_TD_no_FirstDown_or_4thDownConv()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.PASS;
            int Down = 4;
            double Yards_to_Go = 8.0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 15
            };
            Game_Player Receiver = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 21
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
                Play_Start_Yardline = 95,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = false,
                bTouchDown = true,
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
            if (r.Final_Down != 0 || r.Final_yard_to_go != 0 || r.Final_end_of_Play_Yardline != 0)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (!r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 0)
                throw new Exception("There was no penalty on this play");

            if (!r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
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
        public void setPlayOutCome_Run_Left_3rd_and_8_from_92_TD_FirstDown_and_3rdDownConv()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.RUN;
            int Down = 3;
            double Yards_to_Go = 8.0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 15
            };
            Game_Player RB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 21
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
                Play_Start_Yardline = 92,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Running_Back = RB,
                bPenalty_Rejected = false,
                bTouchDown = true,
                bFumble = false,
                bFumble_Lost = false,
                Yards_Gained = 8
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = RB.p_and_r.p.ID,
                off_rush_att = 1,
                off_rush_plays = 1,
                off_rush_Yards = 8,
                off_rush_long = 8,
                off_rush_TDs = 1,
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
            if (r.Final_Down != 0 || r.Final_yard_to_go != 0 || r.Final_end_of_Play_Yardline != 0)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (!r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 0)
                throw new Exception("There was no penalty on this play");

            if (!r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 1 || r.Away3rdDownAtt != 1 || r.Away3rdDownMade != 1 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 8 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
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
        public void setPlayOutCome_Pass_Left_3rd_and_8_from_92_TD_FirstDown_and_3rdDownConv()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.PASS;
            int Down = 3;
            double Yards_to_Go = 8.0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 15
            };
            Game_Player Receiver = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 21
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
                Play_Start_Yardline = 92,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = false,
                bTouchDown = true,
                bFumble = false,
                bFumble_Lost = false,
                Yards_Gained = 8
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
            if (!r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 0)
                throw new Exception("There was no penalty on this play");

            if (!r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 1 || r.Away3rdDownAtt != 1 || r.Away3rdDownMade != 1 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 8 ||
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
        public void setPlayOutCome_Run_Left_4th_and_8_from_92_TD_FirstDown_and_4thDownConv()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.RUN;
            int Down = 4;
            double Yards_to_Go = 8.0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 15
            };
            Game_Player RB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 21
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
                Play_Start_Yardline = 92,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Running_Back = RB,
                bPenalty_Rejected = false,
                bTouchDown = true,
                bFumble = false,
                bFumble_Lost = false,
                Yards_Gained = 8
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = RB.p_and_r.p.ID,
                off_rush_att = 1,
                off_rush_plays = 1,
                off_rush_Yards = 8,
                off_rush_long = 8,
                off_rush_TDs = 1,
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
            if (r.Final_Down != 0 || r.Final_yard_to_go != 0 || r.Final_end_of_Play_Yardline != 0)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (!r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 0)
                throw new Exception("There was no penalty on this play");

            if (!r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 1 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 1 || r.Away4thDownMade != 1 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 8 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
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
        public void setPlayOutCome_Pass_Left_4th_and_8_from_92_TD_FirstDown_and_4thDownConv()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.PASS;
            int Down = 4;
            double Yards_to_Go = 8.0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 15
            };
            Game_Player Receiver = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 21
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
                Play_Start_Yardline = 92,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = false,
                bTouchDown = true,
                bFumble = false,
                bFumble_Lost = false,
                Yards_Gained = 8
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
            if (!r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 0)
                throw new Exception("There was no penalty on this play");

            if (!r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 1 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 1 || r.Away4thDownMade != 1 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 8 ||
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
        public void setPlayOutCome_Run_Left_3rd_and_10_from_30_2yardGane_Fumble_lost_no_3rdDownConv()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.RUN;
            int Down = 3;
            double Yards_to_Go = 10.0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 15
            };
            Game_Player RB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 21
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
                end_of_play_yardline = 32.0,
                Play_Start_Yardline = 30,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Running_Back = RB,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = true,
                bFumble_Lost = true,
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
                off_rush_fumbles = 1,
                off_rush_fumbles_lost = 1
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
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 32)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 0)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 1 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 2 || r.AwayTurnoers != 1 || r.AwaySacks != 0 ||
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
        public void setPlayOutCome_Pass_Left_3rd_and_10_from_30_Interception_no_3rdDownConv()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.PASS;
            int Down = 3;
            double Yards_to_Go = 10.0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 15
            };
            Game_Player Receiver = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 21
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
                end_of_play_yardline = 32.0,
                Play_Start_Yardline = 30,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = false,
                bFumble_Lost = false,
                bInterception = true,
                Yards_Gained = 0
            };

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
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 32)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 0)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 1 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 1 || r.AwaySacks != 0 ||
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
        public void setPlayOutCome_Pass_Left_3rd_and_10_from_30_2yardGame_Fubmle_no_3rdDownConv()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.PASS;
            int Down = 3;
            double Yards_to_Go = 10.0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 15
            };
            Game_Player Receiver = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 21
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
                end_of_play_yardline = 32.0,
                Play_Start_Yardline = 30,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = true,
                bFumble_Lost = true,
                bInterception = false,
                Yards_Gained = 2
            };

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
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 32)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 0)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 1 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 2 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 1 || r.AwaySacks != 0 ||
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
        public void setPlayOutCome_Run_Left_3rd_and_10_from_30_15yardGane_Fumble_lost_no_FirstDown_or_3rdDownConv()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.RUN;
            int Down = 3;
            double Yards_to_Go = 10.0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 15
            };
            Game_Player RB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 21
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
                end_of_play_yardline = 32.0,
                Play_Start_Yardline = 30,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Running_Back = RB,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = true,
                bFumble_Lost = true,
                Yards_Gained = 15
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = RB.p_and_r.p.ID,
                off_rush_att = 1,
                off_rush_plays = 1,
                off_rush_Yards = 2,
                off_rush_long = 15,
                off_rush_TDs = 0,
                off_rush_fumbles = 1,
                off_rush_fumbles_lost = 1
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
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 45)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 0)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 1 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 15 || r.AwayTurnoers != 1 || r.AwaySacks != 0 ||
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
        public void setPlayOutCome_Pass_Left_3rd_and_10_from_30_15yardGame_Fumble_lost_no_FirstDown_or_3rdDownConv()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.PASS;
            int Down = 3;
            double Yards_to_Go = 10.0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 15
            };
            Game_Player Receiver = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 21
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
                end_of_play_yardline = 32.0,
                Play_Start_Yardline = 30,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = true,
                bFumble_Lost = true,
                bInterception = false,
                Yards_Gained = 15
            };

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
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 45)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 0)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 1 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 15 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 1 || r.AwaySacks != 0 ||
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
        public void setPlayOutCome_Run_Left_4th_and_10_from_30_15yardGane_Fumble_lost_no_FirstDown_or_4thDownConv()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.RUN;
            int Down = 4;
            double Yards_to_Go = 10.0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 15
            };
            Game_Player RB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 21
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
                end_of_play_yardline = 32.0,
                Play_Start_Yardline = 30,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Running_Back = RB,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = true,
                bFumble_Lost = true,
                Yards_Gained = 15
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = RB.p_and_r.p.ID,
                off_rush_att = 1,
                off_rush_plays = 1,
                off_rush_Yards = 2,
                off_rush_long = 15,
                off_rush_TDs = 0,
                off_rush_fumbles = 1,
                off_rush_fumbles_lost = 1
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
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 45)
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
                    r.Away4thDownAtt != 1 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 15 || r.AwayTurnoers != 1 || r.AwaySacks != 0 ||
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
        public void setPlayOutCome_Pass_Left_4th_and_10_from_30_15yardGame_Fumble_lost_no_FirstDown_or_4thDownConv()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.PASS;
            int Down = 4;
            double Yards_to_Go = 10.0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 15
            };
            Game_Player Receiver = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 21
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
                end_of_play_yardline = 32.0,
                Play_Start_Yardline = 30,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = true,
                bFumble_Lost = true,
                bInterception = false,
                Yards_Gained = 15
            };

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
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 45)
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
                    r.Away4thDownAtt != 1 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 15 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 1 || r.AwaySacks != 0 ||
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
        public void setPlayOutCome_Run_Left_4th_and_10_from_2_3yardLoss_Fumble_lost_TD_no_FirstDown_or_4thDownConv()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.RUN;
            int Down = 4;
            double Yards_to_Go = 10.0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 15
            };
            Game_Player RB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 21
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
                end_of_play_yardline = -1.0,
                Play_Start_Yardline = 2,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Running_Back = RB,
                bPenalty_Rejected = false,
                bTouchDown = true,
                bFumble = true,
                bFumble_Lost = true,
                Yards_Gained = -3
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = RB.p_and_r.p.ID,
                off_rush_att = 1,
                off_rush_plays = 1,
                off_rush_Yards = -3,
                off_rush_long = -3,
                off_rush_TDs = 0,
                off_rush_fumbles = 1,
                off_rush_fumbles_lost = 1
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
                    r.Away4thDownAtt != 1 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != -3 || r.AwayTurnoers != 1 || r.AwaySacks != 0 ||
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
        public void setPlayOutCome_Pass_Left_4th_and_10_from_2_3yardLoss_Fumble_lost_TD_no_FirstDown_or_4thDownConv()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.PASS;
            int Down = 4;
            double Yards_to_Go = 10.0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 15
            };
            Game_Player Receiver = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 21
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
                end_of_play_yardline = -1.0,
                Play_Start_Yardline = 2,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = false,
                bTouchDown = true,
                bFumble = true,
                bFumble_Lost = true,
                bInterception = false,
                Yards_Gained = -3
            };

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
                    r.Away4thDownAtt != 1 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != -3 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 1 || r.AwaySacks != 0 ||
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
        public void setPlayOutCome_Run_Left_4th_and_10_from_2_3yardLoss_Safety_no_FirstDown_or_4thDownConv()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.RUN;
            int Down = 4;
            double Yards_to_Go = 10.0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 15
            };
            Game_Player RB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 21
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
                end_of_play_yardline = -1.0,
                Play_Start_Yardline = 2,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Running_Back = RB,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = false,
                bFumble_Lost = false,
                bSafety = true,
                Yards_Gained = -3
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = RB.p_and_r.p.ID,
                off_rush_att = 1,
                off_rush_plays = 1,
                off_rush_Yards = -3,
                off_rush_long = -3,
                off_rush_TDs = 0,
                off_rush_fumbles = 0,
                off_rush_fumbles_lost = 0
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
            if (r.Final_Down != 0 || r.Final_yard_to_go != 0 || r.Final_end_of_Play_Yardline != 0)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || !r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 0)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || !r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 1 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != -3 || r.AwayTurnoers != 0 || r.AwaySacks != 0 ||
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
        public void setPlayOutCome_Pass_Left_4th_and_10_from_2_3yardLoss_Safety_no_FirstDown_or_4thDownConv()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.PASS;
            int Down = 4;
            double Yards_to_Go = 10.0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 15
            };
            Game_Player Receiver = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 21
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
                end_of_play_yardline = -1.0,
                Play_Start_Yardline = 2,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = false,
                bFumble_Lost = false,
                bInterception = false,
                Yards_Gained = -3,
                bSafety = true
            };

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
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || !r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 0)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || !r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 1 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != -3 ||
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
        public void setPlayOutCome_Pass_Left_4th_and_10_from_2_Sack_Safety_no_FirstDown_or_4thDownConv()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.PASS;
            int Down = 4;
            double Yards_to_Go = 10.0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 15
            };
            Game_Player Receiver = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 21
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
                Play_Start_Yardline = 2,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = false,
                bFumble_Lost = false,
                bInterception = false,
                Yards_Gained = -7,
                bSack = true,
                bSafety = true
            };

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
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || !r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 0)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || !r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 1 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 ||
                    r.AwayTOP != 0 || r.AwaySacks != 0 || r.AwaySackYards != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 ||
                    r.HomeTOP != 0 || r.HomeSacks != 1 || r.HomeSackYards != -7)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            //           Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Punter.p_and_r.p.ID).First();
            //           if (a_stat.punt_ret != 1 || a_stat.punt_ret_yards != 90 || a_stat.punt_ret_TDs != 0 || a_stat.punt_ret_fumbles != 1 || a_stat.punt_ret_fumbles_lost != 1)
            //               throw new Exception("Player stats not as expected");


            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Pass_Left_3rd_and_10_from_2_QB_Fumdbles_TD_no_FirstDown_or_3rdDownConv()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.PASS;
            int Down = 3;
            double Yards_to_Go = 10.0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 15
            };
            Game_Player Receiver = null;

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
                Play_Start_Yardline = 2,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = false,
                bTouchDown = true,
                bFumble = true,
                bFumble_Lost = true,
                bInterception = false,
                Yards_Gained = -7,
                bSack = false,
                bSafety = false
            };

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

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 1 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 1 ||
                    r.AwayTOP != 0 || r.AwaySacks != 0 || r.AwaySackYards != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 ||
                    r.HomeTOP != 0 || r.HomeSacks != 0 || r.HomeSackYards != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            //           Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Punter.p_and_r.p.ID).First();
            //           if (a_stat.punt_ret != 1 || a_stat.punt_ret_yards != 90 || a_stat.punt_ret_TDs != 0 || a_stat.punt_ret_fumbles != 1 || a_stat.punt_ret_fumbles_lost != 1)
            //               throw new Exception("Player stats not as expected");


            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Pass_Left_1st_and_10_from_20_Sack_7_yard_loss()
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
            Game_Player Receiver = null;

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
                end_of_play_yardline = 13.0,
                Play_Start_Yardline = 20,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = false,
                bFumble_Lost = false,
                bInterception = false,
                Yards_Gained = -7,
                bSack = true,
                bSafety = false
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
            if (r.Final_Down != 2 || r.Final_yard_to_go != 17 || r.Final_end_of_Play_Yardline != 13)
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
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 ||
                    r.AwayTOP != 0 || r.AwaySacks != 0 || r.AwaySackYards != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 ||
                    r.HomeTOP != 0 || r.HomeSacks != 1 || r.HomeSackYards != -7)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            //           Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Punter.p_and_r.p.ID).First();
            //           if (a_stat.punt_ret != 1 || a_stat.punt_ret_yards != 90 || a_stat.punt_ret_TDs != 0 || a_stat.punt_ret_fumbles != 1 || a_stat.punt_ret_fumbles_lost != 1)
            //               throw new Exception("Player stats not as expected");


            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Pass_Left_1st_and_10_Pass_interference_Accepted()
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
                Current_YardLine = 21
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.PI).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 40
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 20.0,
                Play_Start_Yardline = 20,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = false,
                bFumble_Lost = false,
                bInterception = false,
                bPassAttemted = true,
                bPassComplete = false,
                Yards_Gained = 0,
                bSack = false,
                bSafety = false
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
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 40)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 20)
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

            //Check stats
            //           Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Punter.p_and_r.p.ID).First();
            //           if (a_stat.punt_ret != 1 || a_stat.punt_ret_yards != 90 || a_stat.punt_ret_TDs != 0 || a_stat.punt_ret_fumbles != 1 || a_stat.punt_ret_fumbles_lost != 1)
            //               throw new Exception("Player stats not as expected");


            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Pass_Left_1st_and_10_Pass_interference_Rejected()
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
                Current_YardLine = 21
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.PI).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 40
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 60.0,
                Play_Start_Yardline = 20,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = true,
                bTouchDown = false,
                bFumble = false,
                bFumble_Lost = false,
                bInterception = false,
                bPassAttemted = true,
                bPassComplete = true,
                Yards_Gained = 40,
                bSack = false,
                bSafety = false
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
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 60)
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
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 40 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 ||
                    r.AwayTOP != 0 || r.AwaySacks != 0 || r.AwaySackYards != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 ||
                    r.HomeTOP != 0 || r.HomeSacks != 0 || r.HomeSackYards != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            //           Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Punter.p_and_r.p.ID).First();
            //           if (a_stat.punt_ret != 1 || a_stat.punt_ret_yards != 90 || a_stat.punt_ret_TDs != 0 || a_stat.punt_ret_fumbles != 1 || a_stat.punt_ret_fumbles_lost != 1)
            //               throw new Exception("Player stats not as expected");
            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Pass_Left_3rd_and_10_from_88_Pass_interference_endzone_Accepted()
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
                Current_YardLine = 21
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
                end_of_play_yardline = 88.0,
                Play_Start_Yardline = 88,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = false,
                bFumble_Lost = false,
                bInterception = false,
                bPassAttemted = true,
                bPassComplete = false,
                Yards_Gained = 0,
                bSack = false,
                bSafety = false
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
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 99)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 11)
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

            //Check stats
            //           Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Punter.p_and_r.p.ID).First();
            //           if (a_stat.punt_ret != 1 || a_stat.punt_ret_yards != 90 || a_stat.punt_ret_TDs != 0 || a_stat.punt_ret_fumbles != 1 || a_stat.punt_ret_fumbles_lost != 1)
            //               throw new Exception("Player stats not as expected");


            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Pass_Left_3rd_and_10_from_5_Offensive_Pass_interference_Accepted()
        {
            bool penOnBallCarryingTeam = true;
            Play_Enum PE = Play_Enum.PASS;
            int Down = 3;
            double Yards_to_Go = 10.0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 15
            };
            Game_Player Receiver = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 21
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.OI).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 15
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 15.0,
                Play_Start_Yardline = 5,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = false,
                bFumble_Lost = false,
                bInterception = false,
                bPassAttemted = true,
                bPassComplete = true,
                Yards_Gained = 10,
                bSack = false,
                bSafety = false
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
            if (r.Final_Down != 3 || r.Final_yard_to_go != 12.5 || r.Final_end_of_Play_Yardline != 2.5)
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
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 ||
                    r.AwayTOP != 0 || r.AwaySacks != 0 || r.AwaySackYards != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 ||
                    r.HomeTOP != 0 || r.HomeSacks != 0 || r.HomeSackYards != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            //           Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Punter.p_and_r.p.ID).First();
            //           if (a_stat.punt_ret != 1 || a_stat.punt_ret_yards != 90 || a_stat.punt_ret_TDs != 0 || a_stat.punt_ret_fumbles != 1 || a_stat.punt_ret_fumbles_lost != 1)
            //               throw new Exception("Player stats not as expected");


            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Pass_Left_3rd_and_10_from_5_Offensive_Pass_interference_Rejected_Interception()
        {
            bool penOnBallCarryingTeam = true;
            Play_Enum PE = Play_Enum.PASS;
            int Down = 3;
            double Yards_to_Go = 10.0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 15
            };
            Game_Player Receiver = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 21
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.OI).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 15
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 20.0,
                Play_Start_Yardline = 5,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = true,
                bTouchDown = false,
                bFumble = false,
                bFumble_Lost = false,
                bInterception = true,
                bPassAttemted = true,
                bPassComplete = false,
                Yards_Gained = 0,
                bSack = false,
                bSafety = false
            };

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
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 20)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 0.0)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 1 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 1 ||
                    r.AwayTOP != 0 || r.AwaySacks != 0 || r.AwaySackYards != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 ||
                    r.HomeTOP != 0 || r.HomeSacks != 0 || r.HomeSackYards != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            //           Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Punter.p_and_r.p.ID).First();
            //           if (a_stat.punt_ret != 1 || a_stat.punt_ret_yards != 90 || a_stat.punt_ret_TDs != 0 || a_stat.punt_ret_fumbles != 1 || a_stat.punt_ret_fumbles_lost != 1)
            //               throw new Exception("Player stats not as expected");


            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Pass_Left_4th_and_10_from_80_Interception_in_Endzone()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.PASS;
            int Down = 4;
            double Yards_to_Go = 10.0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 15
            };
            Game_Player Receiver = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 21
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
                Play_Start_Yardline = 80,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = false,
                bFumble_Lost = false,
                bInterception = true,
                Yards_Gained = 0,
                bSack = false,
                bSafety = false,
                bTouchback = true
            };

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
                    r.Away4thDownAtt != 1 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 1 ||
                    r.AwayTOP != 0 || r.AwaySacks != 0 || r.AwaySackYards != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 ||
                    r.HomeTOP != 0 || r.HomeSacks != 0 || r.HomeSackYards != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            //           Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Punter.p_and_r.p.ID).First();
            //           if (a_stat.punt_ret != 1 || a_stat.punt_ret_yards != 90 || a_stat.punt_ret_TDs != 0 || a_stat.punt_ret_fumbles != 1 || a_stat.punt_ret_fumbles_lost != 1)
            //               throw new Exception("Player stats not as expected");


            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Pass_Left_3rd_and_5_from_80_Fubmle_Lost_10_yard_game()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.PASS;
            int Down = 3;
            double Yards_to_Go = 5.0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 15
            };
            Game_Player Receiver = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 21
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
                end_of_play_yardline = 90,
                Play_Start_Yardline = 80,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = true,
                bPassAttemted = true,
                bPassComplete = true,
                bFumble_Lost = true,
                bInterception = false,
                Yards_Gained = 10,
                bSack = false,
                bSafety = false,
                bTouchback = false
            };

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
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 90)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 0)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 1 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 10 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 1 ||
                    r.AwayTOP != 0 || r.AwaySacks != 0 || r.AwaySackYards != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 ||
                    r.HomeTOP != 0 || r.HomeSacks != 0 || r.HomeSackYards != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            //           Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Punter.p_and_r.p.ID).First();
            //           if (a_stat.punt_ret != 1 || a_stat.punt_ret_yards != 90 || a_stat.punt_ret_TDs != 0 || a_stat.punt_ret_fumbles != 1 || a_stat.punt_ret_fumbles_lost != 1)
            //               throw new Exception("Player stats not as expected");


            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Pass_Left_3rd_and10_delay_of_Game_Penalty()
        {
            bool penOnBallCarryingTeam = true;
            Play_Enum PE = Play_Enum.PASS;
            int Down = 3;
            double Yards_to_Go = 10.0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 15
            };
            Game_Player Receiver = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 21
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.DG).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 15
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 20,
                Play_Start_Yardline = 20,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = false,
                bPassAttemted = false,
                bPassComplete = false,
                bFumble_Lost = false,
                bInterception = false,
                Yards_Gained = 0,
                bSack = false,
                bSafety = false,
                bTouchback = false
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
            if (r.Final_Down != 3 || r.Final_yard_to_go != 15 || r.Final_end_of_Play_Yardline != 15)
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
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 ||
                    r.AwayTOP != 0 || r.AwaySacks != 0 || r.AwaySackYards != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 ||
                    r.HomeTOP != 0 || r.HomeSacks != 0 || r.HomeSackYards != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            //           Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Punter.p_and_r.p.ID).First();
            //           if (a_stat.punt_ret != 1 || a_stat.punt_ret_yards != 90 || a_stat.punt_ret_TDs != 0 || a_stat.punt_ret_fumbles != 1 || a_stat.punt_ret_fumbles_lost != 1)
            //               throw new Exception("Player stats not as expected");


            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Run_Left_3rd_and10_delay_of_Game_Penalty()
        {
            bool penOnBallCarryingTeam = true;
            Play_Enum PE = Play_Enum.RUN;
            int Down = 3;
            double Yards_to_Go = 10.0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 15
            };
            Game_Player Receiver = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 21
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.DG).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 15
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 20,
                Play_Start_Yardline = 20,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = false,
                bPassAttemted = false,
                bPassComplete = false,
                bFumble_Lost = false,
                bInterception = false,
                Yards_Gained = 0,
                bSack = false,
                bSafety = false,
                bTouchback = false
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
            if (r.Final_Down != 3 || r.Final_yard_to_go != 15 || r.Final_end_of_Play_Yardline != 15)
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
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 ||
                    r.AwayTOP != 0 || r.AwaySacks != 0 || r.AwaySackYards != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 ||
                    r.HomeTOP != 0 || r.HomeSacks != 0 || r.HomeSackYards != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            //           Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Punter.p_and_r.p.ID).First();
            //           if (a_stat.punt_ret != 1 || a_stat.punt_ret_yards != 90 || a_stat.punt_ret_TDs != 0 || a_stat.punt_ret_fumbles != 1 || a_stat.punt_ret_fumbles_lost != 1)
            //               throw new Exception("Player stats not as expected");


            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Pass_Left_3rd_and10_from_6_delay_of_Game_Penalty()
        {
            bool penOnBallCarryingTeam = true;
            Play_Enum PE = Play_Enum.PASS;
            int Down = 3;
            double Yards_to_Go = 10.0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 15
            };
            Game_Player Receiver = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 21
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.IHO).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 15
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 6,
                Play_Start_Yardline = 6,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = false,
                bPassAttemted = false,
                bPassComplete = false,
                bFumble_Lost = false,
                bInterception = false,
                Yards_Gained = 0,
                bSack = false,
                bSafety = false,
                bTouchback = false
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
            if (r.Final_Down != 3 || r.Final_yard_to_go != 13 || r.Final_end_of_Play_Yardline != 3)
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
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 ||
                    r.AwayTOP != 0 || r.AwaySacks != 0 || r.AwaySackYards != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 ||
                    r.HomeTOP != 0 || r.HomeSacks != 0 || r.HomeSackYards != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            //           Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Punter.p_and_r.p.ID).First();
            //           if (a_stat.punt_ret != 1 || a_stat.punt_ret_yards != 90 || a_stat.punt_ret_TDs != 0 || a_stat.punt_ret_fumbles != 1 || a_stat.punt_ret_fumbles_lost != 1)
            //               throw new Exception("Player stats not as expected");


            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Run_Left_3rd_and10_from_6_delay_of_Game_Penalty()
        {
            bool penOnBallCarryingTeam = true;
            Play_Enum PE = Play_Enum.RUN;
            int Down = 3;
            double Yards_to_Go = 10.0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 15
            };
            Game_Player Receiver = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 21
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.IHO).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 15
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 6,
                Play_Start_Yardline = 6,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = false,
                bPassAttemted = false,
                bPassComplete = false,
                bFumble_Lost = false,
                bInterception = false,
                Yards_Gained = 0,
                bSack = false,
                bSafety = false,
                bTouchback = false
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
            if (r.Final_Down != 3 || r.Final_yard_to_go != 13 || r.Final_end_of_Play_Yardline != 3)
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
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 ||
                    r.AwayTOP != 0 || r.AwaySacks != 0 || r.AwaySackYards != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 ||
                    r.HomeTOP != 0 || r.HomeSacks != 0 || r.HomeSackYards != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            //           Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Punter.p_and_r.p.ID).First();
            //           if (a_stat.punt_ret != 1 || a_stat.punt_ret_yards != 90 || a_stat.punt_ret_TDs != 0 || a_stat.punt_ret_fumbles != 1 || a_stat.punt_ret_fumbles_lost != 1)
            //               throw new Exception("Player stats not as expected");


            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Pass_Left_2nd_and10_from_6_Illegal_Contact_Aut_First_Down_on_Def_Penalty()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.PASS;
            int Down = 2;
            double Yards_to_Go = 10.0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 15
            };
            Game_Player Receiver = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 21
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.IC).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 15
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 6,
                Play_Start_Yardline = 6,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = false,
                bPassAttemted = false,
                bPassComplete = false,
                bFumble_Lost = false,
                bInterception = false,
                Yards_Gained = 0,
                bSack = false,
                bSafety = false,
                bTouchback = false
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
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 11)
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
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 ||
                    r.AwayTOP != 0 || r.AwaySacks != 0 || r.AwaySackYards != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 ||
                    r.HomeTOP != 0 || r.HomeSacks != 0 || r.HomeSackYards != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            //           Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Punter.p_and_r.p.ID).First();
            //           if (a_stat.punt_ret != 1 || a_stat.punt_ret_yards != 90 || a_stat.punt_ret_TDs != 0 || a_stat.punt_ret_fumbles != 1 || a_stat.punt_ret_fumbles_lost != 1)
            //               throw new Exception("Player stats not as expected");


            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Run_Left_2nd_and10_from_6_Illegal_Contact_Aut_First_Down_on_Def_Penalty()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.RUN;
            int Down = 2;
            double Yards_to_Go = 10.0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 15
            };
            Game_Player Receiver = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 21
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.IC).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 15
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 6,
                Play_Start_Yardline = 6,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = false,
                bPassAttemted = false,
                bPassComplete = false,
                bFumble_Lost = false,
                bInterception = false,
                Yards_Gained = 0,
                bSack = false,
                bSafety = false,
                bTouchback = false
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
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 11)
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
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 ||
                    r.AwayTOP != 0 || r.AwaySacks != 0 || r.AwaySackYards != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 ||
                    r.HomeTOP != 0 || r.HomeSacks != 0 || r.HomeSackYards != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            //           Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Punter.p_and_r.p.ID).First();
            //           if (a_stat.punt_ret != 1 || a_stat.punt_ret_yards != 90 || a_stat.punt_ret_TDs != 0 || a_stat.punt_ret_fumbles != 1 || a_stat.punt_ret_fumbles_lost != 1)
            //               throw new Exception("Player stats not as expected");


            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Run_Left_3rd_and3_from_30_Illegal_Contact_Aut_First_Down_on_Def_Penalty()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.RUN;
            int Down = 3;
            double Yards_to_Go = 3.0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 15
            };
            Game_Player Receiver = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 21
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.NZ).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 15
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 30,
                Play_Start_Yardline = 30,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = false,
                bPassAttemted = false,
                bPassComplete = false,
                bFumble_Lost = false,
                bInterception = false,
                Yards_Gained = 0,
                bSack = false,
                bSafety = false,
                bTouchback = false
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
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 35)
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
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 ||
                    r.AwayTOP != 0 || r.AwaySacks != 0 || r.AwaySackYards != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 ||
                    r.HomeTOP != 0 || r.HomeSacks != 0 || r.HomeSackYards != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            //           Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Punter.p_and_r.p.ID).First();
            //           if (a_stat.punt_ret != 1 || a_stat.punt_ret_yards != 90 || a_stat.punt_ret_TDs != 0 || a_stat.punt_ret_fumbles != 1 || a_stat.punt_ret_fumbles_lost != 1)
            //               throw new Exception("Player stats not as expected");


            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Pass_Left_3rd_and3_from_30_Illegal_Contact_Aut_First_Down_on_Def_Penalty()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.PASS;
            int Down = 3;
            double Yards_to_Go = 3.0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 15
            };
            Game_Player Receiver = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 21
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.NZ).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 15
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 30,
                Play_Start_Yardline = 30,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = false,
                bPassAttemted = false,
                bPassComplete = false,
                bFumble_Lost = false,
                bInterception = false,
                Yards_Gained = 0,
                bSack = false,
                bSafety = false,
                bTouchback = false
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
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 35)
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
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 ||
                    r.AwayTOP != 0 || r.AwaySacks != 0 || r.AwaySackYards != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 ||
                    r.HomeTOP != 0 || r.HomeSacks != 0 || r.HomeSackYards != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            //           Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Punter.p_and_r.p.ID).First();
            //           if (a_stat.punt_ret != 1 || a_stat.punt_ret_yards != 90 || a_stat.punt_ret_TDs != 0 || a_stat.punt_ret_fumbles != 1 || a_stat.punt_ret_fumbles_lost != 1)
            //               throw new Exception("Player stats not as expected");


            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Pass_Left_3rd_and6_from_95_NZ_Penalty_on_Def_4th_and_3p5()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.PASS;
            int Down = 3;
            double Yards_to_Go = 6.0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 15
            };
            Game_Player Receiver = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 21
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.NZ).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 15
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 95,
                Play_Start_Yardline = 95,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = false,
                bPassAttemted = false,
                bPassComplete = false,
                bFumble_Lost = false,
                bInterception = false,
                Yards_Gained = 0,
                bSack = false,
                bSafety = false,
                bTouchback = false
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
            if (r.Final_Down != 3 || r.Final_yard_to_go != 3.5 || r.Final_end_of_Play_Yardline != 97.5)
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
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 ||
                    r.AwayTOP != 0 || r.AwaySacks != 0 || r.AwaySackYards != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 ||
                    r.HomeTOP != 0 || r.HomeSacks != 0 || r.HomeSackYards != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            //           Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Punter.p_and_r.p.ID).First();
            //           if (a_stat.punt_ret != 1 || a_stat.punt_ret_yards != 90 || a_stat.punt_ret_TDs != 0 || a_stat.punt_ret_fumbles != 1 || a_stat.punt_ret_fumbles_lost != 1)
            //               throw new Exception("Player stats not as expected");


            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Run_Left_3rd_and6_from_95_NZ_Penalty_on_Def_4th_and_3p5()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.RUN;
            int Down = 3;
            double Yards_to_Go = 6.0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 15
            };
            Game_Player Receiver = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 21
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.NZ).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 15
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 95,
                Play_Start_Yardline = 95,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = false,
                bPassAttemted = false,
                bPassComplete = false,
                bFumble_Lost = false,
                bInterception = false,
                Yards_Gained = 0,
                bSack = false,
                bSafety = false,
                bTouchback = false
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
            if (r.Final_Down != 3 || r.Final_yard_to_go != 3.5 || r.Final_end_of_Play_Yardline != 97.5)
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
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 ||
                    r.AwayTOP != 0 || r.AwaySacks != 0 || r.AwaySackYards != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 ||
                    r.HomeTOP != 0 || r.HomeSacks != 0 || r.HomeSackYards != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            //           Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Punter.p_and_r.p.ID).First();
            //           if (a_stat.punt_ret != 1 || a_stat.punt_ret_yards != 90 || a_stat.punt_ret_TDs != 0 || a_stat.punt_ret_fumbles != 1 || a_stat.punt_ret_fumbles_lost != 1)
            //               throw new Exception("Player stats not as expected");


            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Run_Left_3rd_and3_from_27_Def_Off_Penalty_on_Def_Declined()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.RUN;
            int Down = 3;
            double Yards_to_Go = 3.0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 15
            };
            Game_Player RB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 21
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.DH).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 15
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 39,
                Play_Start_Yardline = 27,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = null,
                Running_Back = RB,
                bPenalty_Rejected = true,
                bTouchDown = false,
                bFumble = false,
                bPassAttemted = false,
                bPassComplete = false,
                bFumble_Lost = false,
                bInterception = false,
                Yards_Gained = 12,
                bSack = false,
                bSafety = false,
                bTouchback = false
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
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10.0 || r.Final_end_of_Play_Yardline != 39.0)
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
                    r.AwayRushingYards != 12 || r.AwayTurnoers != 0 ||
                    r.AwayTOP != 0 || r.AwaySacks != 0 || r.AwaySackYards != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 ||
                    r.HomeTOP != 0 || r.HomeSacks != 0 || r.HomeSackYards != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            //           Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Punter.p_and_r.p.ID).First();
            //           if (a_stat.punt_ret != 1 || a_stat.punt_ret_yards != 90 || a_stat.punt_ret_TDs != 0 || a_stat.punt_ret_fumbles != 1 || a_stat.punt_ret_fumbles_lost != 1)
            //               throw new Exception("Player stats not as expected");


            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Pass_Left_3rd_and3_from_27_Def_Off_Penalty_on_Def_Declined()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.PASS;
            int Down = 3;
            double Yards_to_Go = 3.0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 15
            };
            Game_Player Receiver = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 21
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.DH).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 15
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 39,
                Play_Start_Yardline = 27,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = true,
                bTouchDown = false,
                bFumble = false,
                bPassAttemted = false,
                bPassComplete = false,
                bFumble_Lost = false,
                bInterception = false,
                Yards_Gained = 12,
                bSack = false,
                bSafety = false,
                bTouchback = false
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
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10.0 || r.Final_end_of_Play_Yardline != 39.0)
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
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 12 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 ||
                    r.AwayTOP != 0 || r.AwaySacks != 0 || r.AwaySackYards != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 ||
                    r.HomeTOP != 0 || r.HomeSacks != 0 || r.HomeSackYards != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            //           Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Punter.p_and_r.p.ID).First();
            //           if (a_stat.punt_ret != 1 || a_stat.punt_ret_yards != 90 || a_stat.punt_ret_TDs != 0 || a_stat.punt_ret_fumbles != 1 || a_stat.punt_ret_fumbles_lost != 1)
            //               throw new Exception("Player stats not as expected");


            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Run_Left_1st_and_10_from_20_5_yard_game_UC_Penalty_on_Offense()
        {
            bool penOnBallCarryingTeam = true;
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
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.UC).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 23
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 25,
                Play_Start_Yardline = 20,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = null,
                Running_Back = RB,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = false,
                bPassAttemted = false,
                bPassComplete = false,
                bFumble_Lost = false,
                bInterception = false,
                Yards_Gained = 5,
                bSack = false,
                bSafety = false,
                bTouchback = false
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = RB.p_and_r.p.ID,
                off_rush_att = 1,
                off_rush_Yards = 5,
                off_rush_long = 3,
                off_rush_TDs = 0
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
            if (r.Final_Down != 2 || r.Final_yard_to_go != 18.5 || r.Final_end_of_Play_Yardline != 11.5)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 11.5)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 5 || r.AwayTurnoers != 0 ||
                    r.AwayTOP != 0 || r.AwaySacks != 0 || r.AwaySackYards != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 ||
                    r.HomeTOP != 0 || r.HomeSacks != 0 || r.HomeSackYards != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == RB.p_and_r.p.ID).First();
            if (a_stat.off_rush_att != 1 || a_stat.off_rush_Yards != 3 || a_stat.off_rush_long != 3 || a_stat.off_rush_TDs != 0)
                throw new Exception("Player stats not as expected");

            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Pass_Left_1st_and_10_from_20_5_yard_game_UC_Penalty_on_Offense()
        {
            bool penOnBallCarryingTeam = true;
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
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 2 } },
                Current_YardLine = 21
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.UC).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 23
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 25,
                Play_Start_Yardline = 20,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = true,
                bTouchDown = false,
                bFumble = false,
                bPassAttemted = true,
                bPassComplete = true,
                bFumble_Lost = false,
                bInterception = false,
                Yards_Gained = 5,
                bSack = false,
                bSafety = false,
                bTouchback = false
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = QB.p_and_r.p.ID,
                off_pass_Att = 1,
                off_pass_Comp = 1,
                off_pass_Yards = 5,
                off_pass_Long = 5,
                off_pass_TDs = 0
            });

            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = Receiver.p_and_r.p.ID,
                off_rec_catches = 1,
                off_rec_Yards = 5,
                off_rec_long = 5,
                off_rec_TDs = 0
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
            if (r.Final_Down != 2 || r.Final_yard_to_go != 18.5 || r.Final_end_of_Play_Yardline != 11.5)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 11.5)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 0 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 5 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 ||
                    r.AwayTOP != 0 || r.AwaySacks != 0 || r.AwaySackYards != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 ||
                    r.HomeTOP != 0 || r.HomeSacks != 0 || r.HomeSackYards != 0)
                throw new Exception("None of these should be greater than 0");


            //Check stats
            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == QB.p_and_r.p.ID).First();
            if (a_stat.off_pass_Att != 1 || a_stat.off_pass_Comp != 1 || a_stat.off_pass_Yards != 3 || a_stat.off_pass_Long != 3 || a_stat.off_pass_TDs != 0)
                throw new Exception("Player stats not as expected");

            //Check stats
            Game_Player_Stats a_stat2 = pResult.Play_Player_Stats.Where(x => x.Player_ID == Receiver.p_and_r.p.ID).First();
            if (a_stat2.off_rec_catches != 1 || a_stat2.off_rec_Yards != 3 || a_stat2.off_rec_long != 3 || a_stat2.off_rec_TDs != 0)
                throw new Exception("Player stats not as expected");

            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Run_Left_1st_and_10_from_40_0_yard_game_FM_Penalty_on_Offense()
        {
            bool penOnBallCarryingTeam = true;
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
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.FM).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 43
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 40,
                Play_Start_Yardline = 40,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = null,
                Running_Back = RB,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = false,
                bPassAttemted = false,
                bPassComplete = false,
                bFumble_Lost = false,
                bInterception = false,
                Yards_Gained = 0,
                bSack = false,
                bSafety = false,
                bTouchback = false
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = RB.p_and_r.p.ID,
                off_rush_att = 1,
                off_rush_Yards = 0,
                off_rush_long = 0,
                off_rush_TDs = 0
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
            if (r.Final_Down != 2 || r.Final_yard_to_go != 25 || r.Final_end_of_Play_Yardline != 25)
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
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 ||
                    r.AwayTOP != 0 || r.AwaySacks != 0 || r.AwaySackYards != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 ||
                    r.HomeTOP != 0 || r.HomeSacks != 0 || r.HomeSackYards != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == RB.p_and_r.p.ID).First();
            if (a_stat.off_rush_att != 1 || a_stat.off_rush_Yards != 0 || a_stat.off_rush_long != 0 || a_stat.off_rush_TDs != 0)
                throw new Exception("Player stats not as expected");

            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Pass_Left_1st_and_10_from_40_0_yard_game_FM_Penalty_on_Offense()
        {
            bool penOnBallCarryingTeam = true;
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
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 2 } },
                Current_YardLine = 21
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.FM).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 43
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 40,
                Play_Start_Yardline = 40,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = true,
                bTouchDown = false,
                bFumble = false,
                bPassAttemted = false,
                bPassComplete = false,
                bFumble_Lost = false,
                bInterception = false,
                Yards_Gained = 0,
                bSack = false,
                bSafety = false,
                bTouchback = false
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = QB.p_and_r.p.ID,
                off_pass_Att = 1,
                off_pass_Comp = 1,
                off_pass_Yards = 0,
                off_pass_Long = 0,
                off_pass_TDs = 0
            });

            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = Receiver.p_and_r.p.ID,
                off_rec_catches = 1,
                off_rec_Yards = 0,
                off_rec_long = 0,
                off_rec_TDs = 0
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
            if (r.Final_Down != 2 || r.Final_yard_to_go != 25 || r.Final_end_of_Play_Yardline != 25)
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
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 ||
                    r.AwayTOP != 0 || r.AwaySacks != 0 || r.AwaySackYards != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 ||
                    r.HomeTOP != 0 || r.HomeSacks != 0 || r.HomeSackYards != 0)
                throw new Exception("None of these should be greater than 0");


            //Check stats
            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == QB.p_and_r.p.ID).First();
            if (a_stat.off_pass_Att != 1 || a_stat.off_pass_Comp != 1 || a_stat.off_pass_Yards != 0 || a_stat.off_pass_Long != 0 || a_stat.off_pass_TDs != 0)
                throw new Exception("Player stats not as expected");

            //Check stats
            Game_Player_Stats a_stat2 = pResult.Play_Player_Stats.Where(x => x.Player_ID == Receiver.p_and_r.p.ID).First();
            if (a_stat2.off_rec_catches != 1 || a_stat2.off_rec_Yards != 0 || a_stat2.off_rec_long != 0 || a_stat2.off_rec_TDs != 0)
                throw new Exception("Player stats not as expected");

            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Run_Left_1st_and_10_from_40_0_yard_game_FM_Penalty_on_Offense_at_39()
        {
            bool penOnBallCarryingTeam = true;
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
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.FM).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 39
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 40,
                Play_Start_Yardline = 40,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = null,
                Running_Back = RB,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = false,
                bPassAttemted = false,
                bPassComplete = false,
                bFumble_Lost = false,
                bInterception = false,
                Yards_Gained = 0,
                bSack = false,
                bSafety = false,
                bTouchback = false
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = RB.p_and_r.p.ID,
                off_rush_att = 1,
                off_rush_Yards = 0,
                off_rush_long = 0,
                off_rush_TDs = 0
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
            if (r.Final_Down != 2 || r.Final_yard_to_go != 26 || r.Final_end_of_Play_Yardline != 24)
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
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 ||
                    r.AwayTOP != 0 || r.AwaySacks != 0 || r.AwaySackYards != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 ||
                    r.HomeTOP != 0 || r.HomeSacks != 0 || r.HomeSackYards != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == RB.p_and_r.p.ID).First();
            if (a_stat.off_rush_att != 1 || a_stat.off_rush_Yards != -1 || a_stat.off_rush_long != -1 || a_stat.off_rush_TDs != 0)
                throw new Exception("Player stats not as expected");

            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Pass_Left_1st_and_10_from_40_0_yard_game_FM_Penalty_on_Offense_at_39()
        {
            bool penOnBallCarryingTeam = true;
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
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 2 } },
                Current_YardLine = 21
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.FM).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 39
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 40,
                Play_Start_Yardline = 40,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = true,
                bTouchDown = false,
                bFumble = false,
                bPassAttemted = true,
                bPassComplete = true,
                bFumble_Lost = false,
                bInterception = false,
                Yards_Gained = 0,
                bSack = false,
                bSafety = false,
                bTouchback = false
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = QB.p_and_r.p.ID,
                off_pass_Att = 1,
                off_pass_Comp = 1,
                off_pass_Yards = 0,
                off_pass_Long = 0,
                off_pass_TDs = 0
            });

            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = Receiver.p_and_r.p.ID,
                off_rec_catches = 1,
                off_rec_Yards = 0,
                off_rec_long = 0,
                off_rec_TDs = 0
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
            if (r.Final_Down != 2 || r.Final_yard_to_go != 26 || r.Final_end_of_Play_Yardline != 24)
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
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 ||
                    r.AwayTOP != 0 || r.AwaySacks != 0 || r.AwaySackYards != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 ||
                    r.HomeTOP != 0 || r.HomeSacks != 0 || r.HomeSackYards != 0)
                throw new Exception("None of these should be greater than 0");


            //Check stats
            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == QB.p_and_r.p.ID).First();
            if (a_stat.off_pass_Att != 1 || a_stat.off_pass_Comp != 1 || a_stat.off_pass_Yards != -1 || a_stat.off_pass_Long != -1 || a_stat.off_pass_TDs != 0)
                throw new Exception("Player stats not as expected");

            //Check stats
            Game_Player_Stats a_stat2 = pResult.Play_Player_Stats.Where(x => x.Player_ID == Receiver.p_and_r.p.ID).First();
            if (a_stat2.off_rec_catches != 1 || a_stat2.off_rec_Yards != -1 || a_stat2.off_rec_long != -1 || a_stat2.off_rec_TDs != 0)
                throw new Exception("Player stats not as expected");

            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Run_Left_1st_and_10_from_40_0_yard_game_FM_Penalty_on_Offense_at_5()
        {
            bool penOnBallCarryingTeam = true;
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
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.FM).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 5
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 5,
                Play_Start_Yardline = 5,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = null,
                Running_Back = RB,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = false,
                bPassAttemted = false,
                bPassComplete = false,
                bFumble_Lost = false,
                bInterception = false,
                Yards_Gained = 0,
                bSack = false,
                bSafety = false,
                bTouchback = false
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = RB.p_and_r.p.ID,
                off_rush_att = 1,
                off_rush_Yards = 0,
                off_rush_long = 0,
                off_rush_TDs = 0
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
            if (r.Final_Down != 2 || r.Final_yard_to_go != 12.5 || r.Final_end_of_Play_Yardline != 2.5)
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
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 ||
                    r.AwayTOP != 0 || r.AwaySacks != 0 || r.AwaySackYards != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 ||
                    r.HomeTOP != 0 || r.HomeSacks != 0 || r.HomeSackYards != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == RB.p_and_r.p.ID).First();
            if (a_stat.off_rush_att != 1 || a_stat.off_rush_Yards != 0 || a_stat.off_rush_long != 0 || a_stat.off_rush_TDs != 0)
                throw new Exception("Player stats not as expected");

            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Pass_Left_1st_and_10_from_40_0_yard_game_FM_Penalty_on_Offense_at_5()
        {
            bool penOnBallCarryingTeam = true;
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
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 2 } },
                Current_YardLine = 21
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.FM).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 5
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 5,
                Play_Start_Yardline = 5,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = true,
                bTouchDown = false,
                bFumble = false,
                bPassAttemted = false,
                bPassComplete = false,
                bFumble_Lost = false,
                bInterception = false,
                Yards_Gained = 0,
                bSack = false,
                bSafety = false,
                bTouchback = false
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = QB.p_and_r.p.ID,
                off_pass_Att = 1,
                off_pass_Comp = 1,
                off_pass_Yards = 0,
                off_pass_Long = 0,
                off_pass_TDs = 0
            });

            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = Receiver.p_and_r.p.ID,
                off_rec_catches = 1,
                off_rec_Yards = 0,
                off_rec_long = 0,
                off_rec_TDs = 0
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
            if (r.Final_Down != 2 || r.Final_yard_to_go != 12.5 || r.Final_end_of_Play_Yardline != 2.5)
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
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 ||
                    r.AwayTOP != 0 || r.AwaySacks != 0 || r.AwaySackYards != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 ||
                    r.HomeTOP != 0 || r.HomeSacks != 0 || r.HomeSackYards != 0)
                throw new Exception("None of these should be greater than 0");


            //Check stats
            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == QB.p_and_r.p.ID).First();
            if (a_stat.off_pass_Att != 1 || a_stat.off_pass_Comp != 1 || a_stat.off_pass_Yards != 0 || a_stat.off_pass_Long != 0 || a_stat.off_pass_TDs != 0)
                throw new Exception("Player stats not as expected");

            //Check stats
            Game_Player_Stats a_stat2 = pResult.Play_Player_Stats.Where(x => x.Player_ID == Receiver.p_and_r.p.ID).First();
            if (a_stat2.off_rec_catches != 1 || a_stat2.off_rec_Yards != 0 || a_stat2.off_rec_long != 0 || a_stat2.off_rec_TDs != 0)
                throw new Exception("Player stats not as expected");

            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Run_Left_1st_and_10_from_40_0_yard_game_FM_Penalty_on_Defense()
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
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.FM).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 43
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 40,
                Play_Start_Yardline = 40,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = null,
                Running_Back = RB,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = false,
                bPassAttemted = false,
                bPassComplete = false,
                bFumble_Lost = false,
                bInterception = false,
                Yards_Gained = 0,
                bSack = false,
                bSafety = false,
                bTouchback = false
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = RB.p_and_r.p.ID,
                off_rush_att = 1,
                off_rush_Yards = 0,
                off_rush_long = 0,
                off_rush_TDs = 0
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
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 55)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 15)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 1 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
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

            //Check stats
            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == RB.p_and_r.p.ID).First();
            if (a_stat.off_rush_att != 1 || a_stat.off_rush_Yards != 0 || a_stat.off_rush_long != 0 || a_stat.off_rush_TDs != 0)
                throw new Exception("Player stats not as expected");

            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Pass_Left_1st_and_10_from_40_0_yard_game_FM_Penalty_on_Defense()
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
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 2 } },
                Current_YardLine = 21
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.FM).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 43
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 40,
                Play_Start_Yardline = 40,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = true,
                bTouchDown = false,
                bFumble = false,
                bPassAttemted = false,
                bPassComplete = false,
                bFumble_Lost = false,
                bInterception = false,
                Yards_Gained = 0,
                bSack = false,
                bSafety = false,
                bTouchback = false
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = QB.p_and_r.p.ID,
                off_pass_Att = 1,
                off_pass_Comp = 1,
                off_pass_Yards = 0,
                off_pass_Long = 0,
                off_pass_TDs = 0
            });

            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = Receiver.p_and_r.p.ID,
                off_rec_catches = 1,
                off_rec_Yards = 0,
                off_rec_long = 0,
                off_rec_TDs = 0
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
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 55)
                throw new Exception("The next Play must be first and ten on specified yard line");

            //Check for next play
            if (r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 15)
                throw new Exception("There was no penalty on this play");

            if (r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 1 || r.Away3rdDownAtt != 0 || r.Away3rdDownMade != 0 ||
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


            //Check stats
            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == QB.p_and_r.p.ID).First();
            if (a_stat.off_pass_Att != 1 || a_stat.off_pass_Comp != 1 || a_stat.off_pass_Yards != 0 || a_stat.off_pass_Long != 0 || a_stat.off_pass_TDs != 0)
                throw new Exception("Player stats not as expected");

            //Check stats
            Game_Player_Stats a_stat2 = pResult.Play_Player_Stats.Where(x => x.Player_ID == Receiver.p_and_r.p.ID).First();
            if (a_stat2.off_rec_catches != 1 || a_stat2.off_rec_Yards != 0 || a_stat2.off_rec_long != 0 || a_stat2.off_rec_TDs != 0)
                throw new Exception("Player stats not as expected");

            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Run_Left_4th_and_5_from_80_10_yard_game_Fumble_lost_FM_Penalty_on_Defense()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.RUN;
            int Down = 4;
            double Yards_to_Go = 5.0;

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
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.FM).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 80
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 90,
                Play_Start_Yardline = 80,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = null,
                Running_Back = RB,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = true,
                bPassAttemted = false,
                bPassComplete = false,
                bFumble_Lost = true,
                bInterception = false,
                Yards_Gained = 10,
                bSack = false,
                bSafety = false,
                bTouchback = false
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = RB.p_and_r.p.ID,
                off_rush_att = 1,
                off_rush_Yards = 10,
                off_rush_long = 10,
                off_rush_TDs = 0
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
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 95)
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
                    r.Away4thDownAtt != 1 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 10 || r.AwayTurnoers != 1 ||
                    r.AwayTOP != 0 || r.AwaySacks != 0 || r.AwaySackYards != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 ||
                    r.HomeTOP != 0 || r.HomeSacks != 0 || r.HomeSackYards != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == RB.p_and_r.p.ID).First();
            if (a_stat.off_rush_att != 1 || a_stat.off_rush_Yards != 10 || a_stat.off_rush_long != 10 || a_stat.off_rush_TDs != 0)
                throw new Exception("Player stats not as expected");

            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Pass_Left_3rd_and_10_from_80_TD_FM_Penalty_on_Defense_Ignored()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.PASS;
            int Down = 3;
            double Yards_to_Go = 10.0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 15
            };

            Game_Player Receiver = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 2 } },
                Current_YardLine = 21
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.FM).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 80
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 90,
                Play_Start_Yardline = 80,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = true,
                bIgnorePenalty = false,
                bTouchDown = true,
                bFumble = false,
                bPassAttemted = true,
                bPassComplete = true,
                bFumble_Lost = false,
                bInterception = false,
                Yards_Gained = 20,
                bSack = false,
                bSafety = false,
                bTouchback = false
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = QB.p_and_r.p.ID,
                off_pass_Att = 1,
                off_pass_Comp = 1,
                off_pass_Yards = 20,
                off_pass_Long = 20,
                off_pass_TDs = 1
            });

            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = Receiver.p_and_r.p.ID,
                off_rec_catches = 1,
                off_rec_Yards = 20,
                off_rec_long = 20,
                off_rec_TDs = 1
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
            if (!r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 0)
                throw new Exception("There was no penalty on this play");

            if (!r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 1 || r.Away3rdDownAtt != 1 || r.Away3rdDownMade != 1 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 20 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 ||
                    r.AwayTOP != 0 || r.AwaySacks != 0 || r.AwaySackYards != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 ||
                    r.HomeTOP != 0 || r.HomeSacks != 0 || r.HomeSackYards != 0)
                throw new Exception("None of these should be greater than 0");


            //Check stats
            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == QB.p_and_r.p.ID).First();
            if (a_stat.off_pass_Att != 1 || a_stat.off_pass_Comp != 1 || a_stat.off_pass_Yards != 20 || a_stat.off_pass_Long != 20 || a_stat.off_pass_TDs != 1)
                throw new Exception("Player stats not as expected");

            //Check stats
            Game_Player_Stats a_stat2 = pResult.Play_Player_Stats.Where(x => x.Player_ID == Receiver.p_and_r.p.ID).First();
            if (a_stat2.off_rec_catches != 1 || a_stat2.off_rec_Yards != 20 || a_stat2.off_rec_long != 20 || a_stat2.off_rec_TDs != 1)
                throw new Exception("Player stats not as expected");

            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Pass_Left_3rd_and_10_from_80_TD_FM_Penalty_on_Offense()
        {
            bool penOnBallCarryingTeam = true;
            Play_Enum PE = Play_Enum.PASS;
            int Down = 3;
            double Yards_to_Go = 10.0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 15
            };

            Game_Player Receiver = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 2 } },
                Current_YardLine = 21
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.FM).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 80
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = 90,
                Play_Start_Yardline = 80,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = true,
                bIgnorePenalty = false,
                bTouchDown = true,
                bFumble = false,
                bPassAttemted = true,
                bPassComplete = true,
                bFumble_Lost = false,
                bInterception = false,
                Yards_Gained = 20,
                bSack = false,
                bSafety = false,
                bTouchback = false
            };

            //set player stats
            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = QB.p_and_r.p.ID,
                off_pass_Att = 1,
                off_pass_Comp = 1,
                off_pass_Yards = 20,
                off_pass_Long = 20,
                off_pass_TDs = 1
            });

            pResult.Play_Player_Stats.Add(new Game_Player_Stats()
            {
                Player_ID = Receiver.p_and_r.p.ID,
                off_rec_catches = 1,
                off_rec_Yards = 20,
                off_rec_long = 20,
                off_rec_TDs = 1
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
            if (!r.bFinal_NextPlayXP || r.bFinal_NextPlayKickoff || r.bFinal_NextPlayFreeKick)
                throw new Exception("There should not be a special play on the next play");

            if (r.Final_Added_Penalty_Yards != 0)
                throw new Exception("There was no penalty on this play");

            if (!r.bAwayTD || r.bAwayFG || r.bAwayXP || r.bAwaySafetyFor || r.bAwayXP1 || r.bAwayXP2 || r.bAwayXP3 ||
                r.bHomeTD || r.bHomeFG || r.bHomeXP || r.bHomeSafetyFor || r.bHomeXP1 || r.bHomeXP2 || r.bHomeXP3)
                throw new Exception("There should have been no scoring on this play");

            if (r.AwayFirstDowns != 1 || r.Away3rdDownAtt != 1 || r.Away3rdDownMade != 1 ||
                    r.Away4thDownAtt != 0 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 20 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 0 ||
                    r.AwayTOP != 0 || r.AwaySacks != 0 || r.AwaySackYards != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 ||
                    r.HomeTOP != 0 || r.HomeSacks != 0 || r.HomeSackYards != 0)
                throw new Exception("None of these should be greater than 0");


            //Check stats
            Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == QB.p_and_r.p.ID).First();
            if (a_stat.off_pass_Att != 1 || a_stat.off_pass_Comp != 1 || a_stat.off_pass_Yards != 20 || a_stat.off_pass_Long != 20 || a_stat.off_pass_TDs != 1)
                throw new Exception("Player stats not as expected");

            //Check stats
            Game_Player_Stats a_stat2 = pResult.Play_Player_Stats.Where(x => x.Player_ID == Receiver.p_and_r.p.ID).First();
            if (a_stat2.off_rec_catches != 1 || a_stat2.off_rec_Yards != 20 || a_stat2.off_rec_long != 20 || a_stat2.off_rec_TDs != 1)
                throw new Exception("Player stats not as expected");

            Assert.IsTrue(true);
        }
        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Pass_Left_4th_and_10_from_80_Interception_in_Endzone_FM_on_Offense()
        {
            bool penOnBallCarryingTeam = true;
            Play_Enum PE = Play_Enum.PASS;
            int Down = 4;
            double Yards_to_Go = 10.0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 15
            };
            Game_Player Receiver = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 21
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.FM).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 80
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = -5.0,
                Play_Start_Yardline = 80,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = false,
                bFumble_Lost = false,
                bInterception = true,
                Yards_Gained = 0,
                bSack = false,
                bSafety = false,
                bTouchback = true
            };

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
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 60)
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
                    r.Away4thDownAtt != 1 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 1 ||
                    r.AwayTOP != 0 || r.AwaySacks != 0 || r.AwaySackYards != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 ||
                    r.HomeTOP != 0 || r.HomeSacks != 0 || r.HomeSackYards != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            //           Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Punter.p_and_r.p.ID).First();
            //           if (a_stat.punt_ret != 1 || a_stat.punt_ret_yards != 90 || a_stat.punt_ret_TDs != 0 || a_stat.punt_ret_fumbles != 1 || a_stat.punt_ret_fumbles_lost != 1)
            //               throw new Exception("Player stats not as expected");


            Assert.IsTrue(true);
        }

        [TestCategory("GameEngine")]
        [TestMethod]
        public void setPlayOutCome_Pass_Left_4th_and_10_from_80_Interception_in_Endzone_FM_on_Defense()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.PASS;
            int Down = 4;
            double Yards_to_Go = 10.0;

            Game_Player QB = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 15
            };
            Game_Player Receiver = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 21
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.FM).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 80
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                end_of_play_yardline = -5.0,
                Play_Start_Yardline = 80,
                Penalty = Penalty,
                Penalized_Player = Penalized_Player,
                Passer = QB,
                Receiver = Receiver,
                bPenalty_Rejected = false,
                bTouchDown = false,
                bFumble = false,
                bFumble_Lost = false,
                bInterception = true,
                Yards_Gained = 0,
                bSack = false,
                bSafety = false,
                bTouchback = true
            };

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
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 87.5)
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
                    r.Away4thDownAtt != 1 || r.Away4thDownMade != 0 || r.AwayXP1Attempt != 0 ||
                    r.AwayXP1Made != 0 || r.AwayXP2Attempt != 0 || r.AwayXP2Made != 0 ||
                    r.AwayXP3Attempt != 0 || r.AwayXP3Made != 0 || r.AwayPassingYards != 0 ||
                    r.AwayRushingYards != 0 || r.AwayTurnoers != 1 ||
                    r.AwayTOP != 0 || r.AwaySacks != 0 || r.AwaySackYards != 0 ||
                    r.HomeFirstDowns != 0 || r.Home3rdDownAtt != 0 || r.Home3rdDownMade != 0 ||
                    r.Home4thDownAtt != 0 || r.Home4thDownMade != 0 || r.HomeXP1Attempt != 0 ||
                    r.HomeXP1Made != 0 || r.HomeXP2Attempt != 0 || r.HomeXP2Made != 0 ||
                    r.HomeXP3Attempt != 0 || r.HomeXP3Made != 0 || r.HomePassingYards != 0 ||
                    r.HomeRushingYards != 0 || r.HomeTurnoers != 0 ||
                    r.HomeTOP != 0 || r.HomeSacks != 0 || r.HomeSackYards != 0)
                throw new Exception("None of these should be greater than 0");

            //Check stats
            //           Game_Player_Stats a_stat = pResult.Play_Player_Stats.Where(x => x.Player_ID == Punter.p_and_r.p.ID).First();
            //           if (a_stat.punt_ret != 1 || a_stat.punt_ret_yards != 90 || a_stat.punt_ret_TDs != 0 || a_stat.punt_ret_fumbles != 1 || a_stat.punt_ret_fumbles_lost != 1)
            //               throw new Exception("Player stats not as expected");


            Assert.IsTrue(true);
        }

    }
}
