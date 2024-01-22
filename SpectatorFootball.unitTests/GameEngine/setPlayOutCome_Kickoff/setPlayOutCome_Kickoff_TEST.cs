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
    public class setPlayOutCome_Kickoff_TEST
    {
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
                pResult, bLefttoRgiht, TouchBack_Yardline);

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
                pResult, bLefttoRgiht, TouchBack_Yardline);

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
                pResult, bLefttoRgiht, TouchBack_Yardline);

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
                pResult, bLefttoRgiht, TouchBack_Yardline);

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
                pResult, bLefttoRgiht, TouchBack_Yardline);

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
                pResult, bLefttoRgiht, TouchBack_Yardline);

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
                pResult, bLefttoRgiht, TouchBack_Yardline);

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
                pResult, bLefttoRgiht, TouchBack_Yardline);

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
                pResult, bLefttoRgiht, TouchBack_Yardline);

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
                pResult, bLefttoRgiht, TouchBack_Yardline);

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
                pResult, bLefttoRgiht, TouchBack_Yardline);

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
                pResult, bLefttoRgiht, TouchBack_Yardline);

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
                pResult, bLefttoRgiht, TouchBack_Yardline);

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
                pResult, bLefttoRgiht, TouchBack_Yardline);

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
                pResult, bLefttoRgiht, TouchBack_Yardline);

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
                pResult, bLefttoRgiht, TouchBack_Yardline);

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
                pResult, bLefttoRgiht, TouchBack_Yardline);

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
                pResult, bLefttoRgiht, TouchBack_Yardline);

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
        public void setPlayOutCome_Kickoff_Left_Return05_Fumble_lost_KIB_Spot_Penalty_on_Returners_declined_at_4()
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
                pResult, bLefttoRgiht, TouchBack_Yardline);

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
        public void setPlayOutCome_Kickoff_Right_ReturntoOwn20_no_Penalty_to_80()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.KICKOFF_NORMAL;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player Returner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 80
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
                end_of_play_yardline = 80.0,
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

            bool bLefttoRgiht = false;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult, bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (!r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 80.0)
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
        public void setPlayOutCome_Kickoff_Right_Touchback_to_75()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.KICKOFF_NORMAL;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player Returner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 105.0
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
                end_of_play_yardline = 100.0,
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

            bool bLefttoRgiht = false;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult, bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (!r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 25.0)
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
        public void setPlayOutCome_Kickoff_Right_Touchdown_NextPlay_XP()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.KICKOFF_NORMAL;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player Returner = new Game_Player()
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
                Yards_Returned = 100.0,
                bTouchback = false,
                bTouchDown = true,
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
                ko_ret = 1,
                ko_ret_plays = 1,
                ko_ret_yards = 100,
                ko_ret_yards_long = 100,
                ko_ret_TDs = 1,
                ko_ret_fumbles = 0,
                ko_ret_fumbles_lost = 0
            });

            bool bLefttoRgiht = false;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult, bLefttoRgiht, TouchBack_Yardline);

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
        public void setPlayOutCome_Kickoff_Right_Touchdown_NextPlay_XP_Ignore_Penalty()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.KICKOFF_NORMAL;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player Returner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = -5
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.UR).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                Current_YardLine = 75.0
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
                end_of_play_yardline = -5.0,
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

            bool bLefttoRgiht = false;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult, bLefttoRgiht, TouchBack_Yardline);

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
        public void setPlayOutCome_Kickoff_Right_ReturntoOwn20_Spot_Penalty_on_Defenders_at_75_to_60()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.KICKOFF_NORMAL;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player Returner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 80
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.FM).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                Current_YardLine = 75.0
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
                end_of_play_yardline = 80.0,
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

            bool bLefttoRgiht = false;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult, bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (!r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 60.0)
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
        public void setPlayOutCome_Kickoff_Right_ReturntoOwn40_Spot_Penalty_on_Defenders_to_70()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.KICKOFF_NORMAL;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player Returner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 60
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.FM).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                Current_YardLine = 70.0
            };

            Play_Result pResult = new Play_Result()
            {
                at = 11,
                ht = 22,
                BallPossessing_Team_Id = 11,
                NonbBallPossessing_Team_Id = 22,
                Yards_Returned = 60.0,
                bTouchback = false,
                bTouchDown = false,
                end_of_play_yardline = 60.0,
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

            bool bLefttoRgiht = false;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult, bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (!r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 45.0)
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
        public void setPlayOutCome_Kickoff_Right_ReturntoOwn07_Spot_Penalty_on_Defenders_to_92()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.KICKOFF_NORMAL;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player Returner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 93
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
                Yards_Returned = 93.0,
                bTouchback = false,
                bTouchDown = false,
                end_of_play_yardline = 93.0,
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

            bool bLefttoRgiht = false;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult, bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (!r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 77.0)
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
        public void setPlayOutCome_Kickoff_Right_ReturntoOwn91_Spot_Penalty_on_Defenders_to_8()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.KICKOFF_NORMAL;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player Returner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 9
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
                Yards_Returned = 9.0,
                bTouchback = false,
                bTouchDown = false,
                end_of_play_yardline = 9.0,
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

            bool bLefttoRgiht = false;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult, bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (!r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 4.0)
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
        public void setPlayOutCome_Kickoff_Right_ReturntoOwn92_Spot_Penalty_on_Defenders_to_9()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.KICKOFF_NORMAL;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player Returner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 8
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
                Yards_Returned = 8.0,
                bTouchback = false,
                bTouchDown = false,
                end_of_play_yardline = 8.0,
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

            bool bLefttoRgiht = false;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult, bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (!r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 4.0)
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
        public void setPlayOutCome_Kickoff_Right_ReturntoOwn30_Spot_Penalty_on_Returners_at_75()
        {
            bool penOnBallCarryingTeam = true;
            Play_Enum PE = Play_Enum.KICKOFF_NORMAL;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player Returner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 70
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.FM).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                Current_YardLine = 75.0
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
                end_of_play_yardline = 70.0,
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
            bool bLefttoRgiht = false;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult, bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (!r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must stand");

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
        public void setPlayOutCome_Kickoff_Right_ReturntoOwn30_Spot_Penalty_on_Returners_at_65()
        {
            bool penOnBallCarryingTeam = true;
            Play_Enum PE = Play_Enum.KICKOFF_NORMAL;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player Returner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 70
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.FM).First();
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
                Yards_Returned = 30.0,
                bTouchback = false,
                bTouchDown = false,
                end_of_play_yardline = 70.0,
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
            bool bLefttoRgiht = false;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult, bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (!r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 85.0)
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
        public void setPlayOutCome_Kickoff_Right_ReturntoOwn92_Spot_Penalty_on_Returners_at_8()
        {
            bool penOnBallCarryingTeam = true;
            Play_Enum PE = Play_Enum.KICKOFF_NORMAL;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player Returner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 8
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
                Yards_Returned = 92.0,
                bTouchback = false,
                bTouchDown = false,
                end_of_play_yardline = 8.0,
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
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 23.0)
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
        public void setPlayOutCome_Kickoff_Right_ReturnTD_Spot_Penalty_on_Returners_at_50()
        {
            bool penOnBallCarryingTeam = true;
            Play_Enum PE = Play_Enum.KICKOFF_NORMAL;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player Returner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = -5
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
                end_of_play_yardline = -5.0,
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
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 65.0)
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
        public void setPlayOutCome_Kickoff_Right_Return04_Spot_Penalty_on_Returners_at_95()
        {
            bool penOnBallCarryingTeam = true;
            Play_Enum PE = Play_Enum.KICKOFF_NORMAL;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player Returner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 96.0
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.FM).First();
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
                Yards_Returned = 4.0,
                bTouchback = false,
                bTouchDown = false,
                end_of_play_yardline = 96.0,
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
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 98.0)
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
        public void setPlayOutCome_Kickoff_Right_Return05_Spot_Penalty_on_Returners_at_96()
        {
            bool penOnBallCarryingTeam = true;
            Play_Enum PE = Play_Enum.KICKOFF_NORMAL;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player Returner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 95.0
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.FM).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                Current_YardLine = 96.0
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
                end_of_play_yardline = 95.0,
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
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 98.0)
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
        public void setPlayOutCome_Kickoff_Right_Return_Fumbles_on_80_and_Recovers()
        {
            bool penOnBallCarryingTeam = true;
            Play_Enum PE = Play_Enum.KICKOFF_NORMAL;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player Returner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 80
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
                end_of_play_yardline = 80.0,
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
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 80.0)
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
        public void setPlayOutCome_Kickoff_Right_Return_lost_Fumbles_on_80_penalty_on_retuners()
        {
            bool penOnBallCarryingTeam = true;
            Play_Enum PE = Play_Enum.KICKOFF_NORMAL;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player Returner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 80
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.FM).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                Current_YardLine = 85.0
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
                end_of_play_yardline = 80.0,
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
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 92.5)
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
        public void setPlayOutCome_Kickoff_Right_Return_lost_Fumbles_on_80_penalty_on_defenders()
        {
            bool penOnBallCarryingTeam = false;
            Play_Enum PE = Play_Enum.KICKOFF_NORMAL;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player Returner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 80
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.FM).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                Current_YardLine = 85.0
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
                end_of_play_yardline = 80.0,
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
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 65)
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
        public void setPlayOutCome_Kickoff_Right_ReturntoOwn30_KIB_Spot_Penalty_on_Returners_at_75()
        {
            bool penOnBallCarryingTeam = true;
            Play_Enum PE = Play_Enum.KICKOFF_NORMAL;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player Returner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 70
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.KIB).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                Current_YardLine = 75.0
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
                end_of_play_yardline = 70.0,
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
            bool bLefttoRgiht = false;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult, bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (!r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must stand");

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
        public void setPlayOutCome_Kickoff_Right_ReturntoOwn30_KIB_Spot_Penalty_on_Returners_at_65()
        {
            bool penOnBallCarryingTeam = true;
            Play_Enum PE = Play_Enum.KICKOFF_NORMAL;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player Returner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 70
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.KIB).First();
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
                Yards_Returned = 30.0,
                bTouchback = false,
                bTouchDown = false,
                end_of_play_yardline = 70.0,
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
            bool bLefttoRgiht = false;
            double TouchBack_Yardline = 25.0;

            Play_Result r = GameEngine.setPlayOutCome(penOnBallCarryingTeam, PE, Down, Yards_to_Go,
                pResult, bLefttoRgiht, TouchBack_Yardline);

            //There should be a change in possession
            if (!r.bFinal_SwitchPossession)
                throw new Exception("Possession should have been swtich because of kickoff");

            if (!r.bPlay_Stands)
                throw new Exception("This Play must stand");

            //Check dow, yardage and yardline
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 85.0)
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
        public void setPlayOutCome_Kickoff_Right_ReturntoOwn92_KIB_Spot_Penalty_on_Returners_at_8()
        {
            bool penOnBallCarryingTeam = true;
            Play_Enum PE = Play_Enum.KICKOFF_NORMAL;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player Returner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 8
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.KIB).First();
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
                Yards_Returned = 92.0,
                bTouchback = false,
                bTouchDown = false,
                end_of_play_yardline = 8.0,
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
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 23.0)
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
        public void setPlayOutCome_Kickoff_Right_ReturnTD_KIB_Spot_Penalty_on_Returners_at_50()
        {
            bool penOnBallCarryingTeam = true;
            Play_Enum PE = Play_Enum.KICKOFF_NORMAL;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player Returner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = -5
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
                end_of_play_yardline = -5.0,
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
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 65.0)
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
        public void setPlayOutCome_Kickoff_Right_Return04_KIB_Spot_Penalty_on_Returners_at_95()
        {
            bool penOnBallCarryingTeam = true;
            Play_Enum PE = Play_Enum.KICKOFF_NORMAL;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player Returner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 96.0
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.KIB).First();
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
                Yards_Returned = 4.0,
                bTouchback = false,
                bTouchDown = false,
                end_of_play_yardline = 96.0,
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
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 98.0)
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
        public void setPlayOutCome_Kickoff_Right_Return05_KIB_Spot_Penalty_on_Returners_at_96()
        {
            bool penOnBallCarryingTeam = true;
            Play_Enum PE = Play_Enum.KICKOFF_NORMAL;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player Returner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 95.0
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.KIB).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                Current_YardLine = 96.0
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
                end_of_play_yardline = 95.0,
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
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 98.0)
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
        public void setPlayOutCome_Kickoff_Right_Return05_Fumble_lost_KIB_Spot_Penalty_on_Returners_declined_at_96()
        {
            bool penOnBallCarryingTeam = true;
            Play_Enum PE = Play_Enum.KICKOFF_NORMAL;
            int Down = 0;
            double Yards_to_Go = 0.0;

            Game_Player Returner = new Game_Player()
            {
                p_and_r = new Player_and_Ratings() { p = new Player() { ID = 1 } },
                Current_YardLine = 95.0
            };

            //get penalty settings
            List<Penalty> p = Penalty_Helper.ReturnAllPenalties();
            Penalty Penalty = p.Where(x => x.code == Penalty_Codes.KIB).First();
            Game_Player Penalized_Player = new Game_Player()
            {
                Current_YardLine = 96.0
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
                end_of_play_yardline = 95.0,
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
            if (r.Final_Down != 1 || r.Final_yard_to_go != 10 || r.Final_end_of_Play_Yardline != 95.0)
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
    }
}
