using SpectatorFootball.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.GameNS
{
    public class Play_Result
    {
        public long yards_gained = 0;

        //Result
        public bool bKickoff_Out_of_Endzone = false;
        public bool bTouchDown = false;
        public bool bTouchback = false;
        public bool bFumble = false;
        public bool bFumble_Lost = false;
        public bool bRunOutofBounds = false;

        public Game_Player Kicker = null;
        public Game_Player Returner = null;
        public Game_Player Tackler = null;
        public Game_Player Fumber_Recoverer = null;

        public bool bSwitchPossession = false;

        public List<Game_Player_Defense_Stats> Game_Player_Defense_Stats = new List<Game_Player_Defense_Stats>();
        public List<Game_Player_FG_Defense_Stats> Game_Player_FG_Defense_Stats = new List<Game_Player_FG_Defense_Stats>();
        public List<Game_Player_Kick_Returner_Stats> Game_Player_Kick_Returner_Stats = new List<Game_Player_Kick_Returner_Stats>();
        public List<Game_Player_Kicker_Stats> Game_Player_Kicker_Stats = new List<Game_Player_Kicker_Stats>();
        public List<Game_Player_Kickoff_Defenders> Game_Player_Kickoff_Defenders = new List<Game_Player_Kickoff_Defenders>();
        public List<Game_Player_Kickoff_Receiver_Stats> Game_Player_Kickoff_Receiver_Stats = new List<Game_Player_Kickoff_Receiver_Stats>();
        public List<Game_Player_Offensive_Linemen_Stats> Game_Player_Offensive_Linemen_Stats = new List<Game_Player_Offensive_Linemen_Stats>();
        public List<Game_Player_Pass_Defense_Stats> Game_Player_Pass_Defense_Stats = new List<Game_Player_Pass_Defense_Stats>();
        public List<Game_Player_Passing_Stats> Game_Player_Passing_Stats = new List<Game_Player_Passing_Stats>();
        public List<Game_Player_Penalty_Stats> Game_Player_Penalty_Stats = new List<Game_Player_Penalty_Stats>();
        public List<Game_Player_Punt_Defenders> Game_Player_Punt_Defenders = new List<Game_Player_Punt_Defenders>();
        public List<Game_Player_Punt_Receiver_Stats> Game_Player_Punt_Receiver_Stats = new List<Game_Player_Punt_Receiver_Stats>();
        public List<Game_Player_Punt_Returner_Stats> Game_Player_Punt_Returner_Stats = new List<Game_Player_Punt_Returner_Stats>();
        public List<Game_Player_Punter_Stats> Game_Player_Punter_Stats = new List<Game_Player_Punter_Stats>();
        public List<Game_Player_Receiving_Stats> Game_Player_Receiving_Stats = new List<Game_Player_Receiving_Stats>();
        public List<Game_Player_Rushing_Stats> Game_Player_Rushing_Stats = new List<Game_Player_Rushing_Stats>();

    }
}
