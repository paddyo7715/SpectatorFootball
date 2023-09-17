﻿using SpectatorFootball.Models;
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

        //Result
        public bool bKick_Out_of_Endzone = false;
        public bool bKick_Out_of_Bounds = false;
        public bool bTouchDown = false;
        public bool bTouchback = false;
        public bool bFumble = false;
        public bool bFumble_Lost = false;
        public bool bRunOutofBounds = false;
        public bool bSafety = false;
        public bool bInterception = false;
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

        public Game_Player Passer = null;
        public Game_Player Kicker = null;
        public Game_Player Punter = null;
        public Game_Player Returner = null;
        public Game_Player Punt_Returner = null;
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

        public Game_Player Tackler = null;
        public Game_Player Fumble_Recoverer = null;
        public Game_Player Forced_Fumble_Tackler = null;

        public double Yards_Gained = 0;
        public double Yards_Returned = 0;
        public double Kick_Out_of_Bounds_Yardline = 0;
        public double end_of_play_yardline = 0;

        public List<Game_Player_Stats> Play_Player_Stats = new List<Game_Player_Stats>();
        public List<Game_Player_Penalty_Stats> Play_Player_Penalty_Stats = new List<Game_Player_Penalty_Stats>();
    }
}
