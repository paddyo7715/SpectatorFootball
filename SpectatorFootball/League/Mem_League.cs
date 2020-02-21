using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpectatorFootball.Models;

namespace SpectatorFootball.League_Info
{ 
    public enum League_State
    {
        Regular_Season,
        Playoffs,
        Season_Complete,
        Prev_Season
    }

    public class Mem_League
    {
        public Mem_League()
        {
            Years = new List<int>();
            Awards = new List<Award>();
            Conferences = new List<Conference>();
            DBVersion = new List<DBVersion>();
            Divisions = new List<Division>();
            Games = new List<Game>();
            Game_Player_Kick_Returner_Stats = new List<Game_Player_Kick_Returner_Stats>();
            Game_Player_Kicker_Stats = new List<Game_Player_Kicker_Stats>();
            Game_Player_Offensive_Linemen_Stats = new List<Game_Player_Offensive_Linemen_Stats>();
            Game_Player_Pass_Defense_Stats = new List<Game_Player_Pass_Defense_Stats>();
            Game_Player_Pass_Rushers_Stats = new List<Game_Player_Pass_Rushers_Stats>();
            Game_Player_Passing_Stats = new List<Game_Player_Passing_Stats>();
            Game_Player_Punter_Stats = new List<Game_Player_Punter_Stats>();
            Game_Player_Receiving_Stats = new List<Game_Player_Receiving_Stats>();
            Game_Player_Rushing_Stats = new List<Game_Player_Rushing_Stats>();
            Game_Player_Special_Team_Receive_Stats = new List<Game_Player_Special_Team_Receive_Stats>();
            Game_Player_Special_Teams_Has_Ball_Stats = new List<Game_Player_Special_Teams_Has_Ball_Stats>();
            Game_Scoring_Summary = new List<Game_Scoring_Summary>();
            Hall_of_Fame = new List<Hall_of_Fame>();
            Leagues = new League();
            Player_Awards = new List<Player_Awards>();
            Players = new List<Player>();
            Playoff_Teams = new List<Playoff_Teams>();
            Teams = new List<Team>();


        }

        public int currentYear { get; set; }
        public List<int> Years { get; set; }
        public League_State State { get; set; } = default(League_State);
        public List<Award> Awards { get; set; }
        public List<Conference> Conferences { get; set; }
        public List<DBVersion> DBVersion { get; set; }
        public List<Division> Divisions { get; set; }
        public List<Game> Games { get; set; }
        public List<Game_Player_Kick_Returner_Stats> Game_Player_Kick_Returner_Stats { get; set; }
        public List<Game_Player_Kicker_Stats> Game_Player_Kicker_Stats { get; set; }
        public List<Game_Player_Offensive_Linemen_Stats> Game_Player_Offensive_Linemen_Stats { get; set; }
        public List<Game_Player_Pass_Defense_Stats> Game_Player_Pass_Defense_Stats { get; set; }
        public List<Game_Player_Pass_Rushers_Stats> Game_Player_Pass_Rushers_Stats { get; set; }
        public List<Game_Player_Passing_Stats> Game_Player_Passing_Stats { get; set; }
        public List<Game_Player_Punter_Stats> Game_Player_Punter_Stats { get; set; }
        public List<Game_Player_Receiving_Stats> Game_Player_Receiving_Stats { get; set; }
        public List<Game_Player_Rushing_Stats> Game_Player_Rushing_Stats { get; set; }
        public List<Game_Player_Special_Team_Receive_Stats> Game_Player_Special_Team_Receive_Stats { get; set; }
        public List<Game_Player_Special_Teams_Has_Ball_Stats> Game_Player_Special_Teams_Has_Ball_Stats { get; set; }
        public List<Game_Scoring_Summary> Game_Scoring_Summary { get; set; }
        public List<Hall_of_Fame> Hall_of_Fame { get; set; }
        public League Leagues { get; set; }
        public List<Player_Awards> Player_Awards { get; set; }
        public List<Player> Players { get; set; }
        public List<Playoff_Teams> Playoff_Teams { get; set; }
        public List<Team> Teams { get; set; }
    }
}
