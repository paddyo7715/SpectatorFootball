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

        public double Yards_Gained = 0.0;

        //Line_of_Scrimmage will be set in the play, except for the following cases:
        //kickoff touchback, punt touchback or free kick touchback (which should never happen)
        //In these special cases, the game engine must set the line of scrimmage for the next play
        //as the touchback yardline.
        public double Line_of_Scrimmage = 0.0;

        //the bSwitchPossession property is ONLY set on a play that has a fumble lost or interception.
        //For kickoffs, punts or free kicks, it is already known that after the play that the teams
        //switch possession, so don't set this to true on those plays except when there is a fumble lost
        public bool bSwitchPossession = false;

        public List<Game_Player_Stats> Play_Player_Stats = new List<Game_Player_Stats>();
        public List<Game_Player_Penalty_Stats> Play_Player_Penalty_Stats = new List<Game_Player_Penalty_Stats>();
    }
}
