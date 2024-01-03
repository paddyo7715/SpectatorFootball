using SpectatorFootball.Models;
using SpectatorFootball.PenaltiesNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.GameNS
{
    public class Play_Result
    {
        //for kickoff, onside kics, free kick and punt this is the return team.  For all other plays,
        //it is the team that starts off with the ball.
        public long BallPossessing_Team_Id;

        public long NonbBallPossessing_Team_Id;

        public long at;
        public long ht;

        //Result
        public bool bKick_Out_of_Endzone = false;
        public bool bKick_Out_of_Bounds = false;
        public bool bTouchDown = false;
        public bool bTouchback = false;
        public bool bFumble = false;
        public bool bFumble_Lost = false;
        public bool bRunOutofBounds = false;
        public bool bSafety = false;
        public bool bSack = false;
        public bool bInterception = false;
        public bool bPassAttemted = false;
        public bool bPassComplete = false;
        public bool bFGMade = false;
        public bool bFGMissed = false;
        public bool bXPMade = false;
        public bool bXPMissed = false;
        public bool bOnePntAfterTDMade = false;
        public bool bOnePntAfterTDMissed = false;
        public bool bTwoPntAfterTDMade = false;
        public bool bTwoPntAfterTDMissed = false;
        public bool bThreePntAfterTDMade = false;
        public bool bThreePntAfterTDMissed = false;
        public bool bCoffinCornerAttemt = false;
        public bool bCoffinCornerMade = false;

        //Penalties
        public Game_Player Penalized_Player = null;
        public Penalty Penalty = null;
        public bool bPenalty_Rejected = false;
        public bool bPenatly_on_Away_Team = false;
        public bool bIgnorePenalty = false;

        //Used for Possible Special Penalties on the Defense
        public Game_Player Defender_Knocks_Down_QB = null;  //Possible Roughing the Passer
        public Game_Player Defender_Close_to_Kicker = null;  //Possible Roughing or Running into the kicker
        public Game_Player Defender_Close_to_Receiver = null;  //Possible Defensive Pass Interference
        public double Defender_Close_to_Receiver_Yardline; //Possible Defensive Pass Interference

        //Used for determining who might have commit a penalty and stats
        public Game_Player Passer = null;
        public Game_Player Kicker = null;
        public Game_Player Punter = null;
        public Game_Player Returner = null;
        public Game_Player Punt_Returner = null;
        public Game_Player Targeted_Receiver = null; //Targeted Pass Catcher
        public Game_Player Running_Back = null;  //Running back with ball or running QB
        public Game_Player Receiver = null;  //WR, TE or RB that is targeted with the pass

        public List<Game_Player> Pass_Catchers = new List<Game_Player>();
        public List<Game_Player> Ball_Runners = new List<Game_Player>();
        public List<Game_Player> Pass_Blockers = new List<Game_Player>();
        public List<Game_Player> Pass_Rushers = new List<Game_Player>();
        public List<Game_Player> Pass_Defenders = new List<Game_Player>();
        public List<Game_Player> Run_Blockers = new List<Game_Player>();
        public List<Game_Player> Run_Defenders = new List<Game_Player>();
        public List<Game_Player> Kick_Returners = new List<Game_Player>();
        public List<Game_Player> Kick_Defenders = new List<Game_Player>();
        public List<Game_Player> Punt_Returners = new List<Game_Player>();
        public List<Game_Player> Punt_Defenders = new List<Game_Player>();
        public List<Game_Player> FieldGaol_Kicking_Team = new List<Game_Player>();
        public List<Game_Player> Field_Goal_Defenders = new List<Game_Player>();
//===================================================

//Used for Initial stats and players before penalty
        public Game_Player Tackler = null;
        public Game_Player Fumble_Recoverer = null;
        public Game_Player Forced_Fumble_Tackler = null;

        public double Yards_Gained = 0;
        public double Yards_Returned = 0;
        public double Kick_Out_of_Bounds_Yardline = 0;
        public double Play_Start_Yardline = 0;
        public double end_of_play_yardline = 0;
//=============================================

//Final play results after accounting for possible penalties
        public bool bFinal_SwitchPossession = false;
        public bool bPlay_Stands = false;
        public int Final_Down = 0;
        public double Final_yard_to_go = 0;
        public double Final_end_of_Play_Yardline = 0;
        public bool bFinal_NextPlayXP = false;
        public bool bFinal_NextPlayKickoff = false;
        public bool bFinal_NextPlayFreeKick = false;
        public double Final_Added_Penalty_Yards = 0;

        public bool bAwayTD = false;
        public bool bAwayFG = false;
        public bool bAwayXP = false;
        public bool bAwaySafetyFor = false;
        public bool bAwayXP1 = false;
        public bool bAwayXP2 = false;
        public bool bAwayXP3 = false;

        public bool bHomeTD = false;
        public bool bHomeFG = false;
        public bool bHomeXP = false;
        public bool bHomeSafetyFor = false;
        public bool bHomeXP1 = false;
        public bool bHomeXP2 = false;
        public bool bHomeXP3 = false;

        public long AwayFirstDowns = 0;
        public long Away3rdDownAtt = 0;
        public long Away3rdDownMade = 0;
        public long Away4thDownAtt = 0;
        public long Away4thDownMade = 0;
        public long AwayXP1Attempt = 0;
        public long AwayXP1Made = 0;
        public long AwayXP2Attempt = 0;
        public long AwayXP2Made = 0;
        public long AwayXP3Attempt = 0;
        public long AwayXP3Made = 0;
        public double AwayPassingYards = 0.0;
        public double AwayRushingYards = 0.0;
        public long AwayTurnoers = 0;
        public long AwaySacks = 0;
        public double AwaySackYards = 0;
        public long AwayTOP = 0;

        public long HomeFirstDowns = 0;
        public long Home3rdDownAtt = 0;
        public long Home3rdDownMade = 0;
        public long Home4thDownAtt = 0;
        public long Home4thDownMade = 0;
        public long HomeXP1Attempt = 0;
        public long HomeXP1Made = 0;
        public long HomeXP2Attempt = 0;
        public long HomeXP2Made = 0;
        public long HomeXP3Attempt = 0;
        public long HomeXP3Made = 0;
        public double HomePassingYards = 0.0;
        public double HomeRushingYards = 0.0;
        public long HomeTurnoers = 0;
        public long HomeSacks = 0;
        public double HomeSackYards = 0;
        public long HomeTOP = 0;
        //==============================================

        public List<Game_Player_Stats> Play_Player_Stats = new List<Game_Player_Stats>();
        public List<Game_Player_Penalty_Stats> Play_Player_Penalty_Stats = new List<Game_Player_Penalty_Stats>();


    }
}
