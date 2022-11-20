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
        public string Message = "";
        public long yards_gained = 0;
        public bool bRighttoLeft;

        //Next manditory play
        public bool bKickoff = false;
        public bool bExtraPoint = false;
        public bool bFreeKick = false;

        public Play_Package Offensive_Package = null;
        public Formation DEF_Formation = null;

        public bool bSwitchPossession = false;

        public long away_points = 0;
        public long home_points = 0;

        public long away_first_downs = 0;
        public long away_third_down_att = 0;
        public long away_third_down_conv = 0;

        public long away_fourth_down_att = 0;
        public long away_fourth_down_conv = 0;

        public long away_1point_att = 0;
        public long away_1point_conv = 0;

        public long away_2point_att = 0;
        public long away_2point_conv = 0;

        public long away_3point_att = 0;
        public long away_3point_conv = 0;

        public long away_time_of_possession = 0;

        public long away_turnovers = 0;
        public long away_sacks = 0;  //by the defense

        public long home_first_downs = 0;
        public long home_third_down_att = 0;
        public long home_third_down_conv = 0;

        public long home_fourth_down_att = 0;
        public long home_fourth_down_conv = 0;

        public long home_1point_att = 0;
        public long home_1point_conv = 0;

        public long home_2point_att = 0;
        public long home_2point_conv = 0;

        public long home_3point_att = 0;
        public long home_3point_conv = 0;

        public long home_time_of_possession = 0;

        public long home_turnovers = 0;
        public long home_sacks = 0;

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
        public List<Game_Scoring_Summary> Game_Scoring_Summary = new List<Game_Scoring_Summary>();

        public string Before_Snap = null;
        public List<string> Play_Stages = null;
        public Play_Result(bool bRighttoLeft)
        {
            this.bRighttoLeft = bRighttoLeft;
        }
    }
}
