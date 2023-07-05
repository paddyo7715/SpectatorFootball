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
        public Game_Player Fumble_Recoverer = null;
        public Game_Player Forced_Fumble_Tackler = null;

        public bool bSwitchPossession = false;

        public List<Game_Player_Stats> Game_Player_Stats = new List<Game_Player_Stats>();
        public List<Game_Player_Penalty_Stats> Game_Player_Penalty_Stats = new List<Game_Player_Penalty_Stats>();
    }
}
