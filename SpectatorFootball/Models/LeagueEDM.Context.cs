﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SpectatorFootball.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class leagueContext : DbContext
    {
        public leagueContext()
            : base("name=leagueContext")
        {
        }

        public leagueContext(string connString)
            : base(connString)
        {
        }

    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Award> Awards { get; set; }
        public virtual DbSet<Conference> Conferences { get; set; }
        public virtual DbSet<DBVersion> DBVersions { get; set; }
        public virtual DbSet<Division> Divisions { get; set; }
        public virtual DbSet<Draft> Drafts { get; set; }
        public virtual DbSet<Franchise> Franchises { get; set; }
        public virtual DbSet<Free_Agency> Free_Agency { get; set; }
        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<Game_Player_Defense_Stats> Game_Player_Defense_Stats { get; set; }
        public virtual DbSet<Game_Player_FG_Defense_Stats> Game_Player_FG_Defense_Stats { get; set; }
        public virtual DbSet<Game_Player_Kick_Returner_Stats> Game_Player_Kick_Returner_Stats { get; set; }
        public virtual DbSet<Game_Player_Kicker_Stats> Game_Player_Kicker_Stats { get; set; }
        public virtual DbSet<Game_Player_Kickoff_Defenders> Game_Player_Kickoff_Defenders { get; set; }
        public virtual DbSet<Game_Player_Kickoff_Receiver_Stats> Game_Player_Kickoff_Receiver_Stats { get; set; }
        public virtual DbSet<Game_Player_Offensive_Linemen_Stats> Game_Player_Offensive_Linemen_Stats { get; set; }
        public virtual DbSet<Game_Player_Pass_Defense_Stats> Game_Player_Pass_Defense_Stats { get; set; }
        public virtual DbSet<Game_Player_Passing_Stats> Game_Player_Passing_Stats { get; set; }
        public virtual DbSet<Game_Player_Penalty_Stats> Game_Player_Penalty_Stats { get; set; }
        public virtual DbSet<Game_Player_Punt_Defenders> Game_Player_Punt_Defenders { get; set; }
        public virtual DbSet<Game_Player_Punt_Receiver_Stats> Game_Player_Punt_Receiver_Stats { get; set; }
        public virtual DbSet<Game_Player_Punt_Returner_Stats> Game_Player_Punt_Returner_Stats { get; set; }
        public virtual DbSet<Game_Player_Punter_Stats> Game_Player_Punter_Stats { get; set; }
        public virtual DbSet<Game_Player_Receiving_Stats> Game_Player_Receiving_Stats { get; set; }
        public virtual DbSet<Game_Player_Rushing_Stats> Game_Player_Rushing_Stats { get; set; }
        public virtual DbSet<Game_Scoring_Summary> Game_Scoring_Summary { get; set; }
        public virtual DbSet<Hall_of_Fame> Hall_of_Fame { get; set; }
        public virtual DbSet<Injury> Injuries { get; set; }
        public virtual DbSet<League_Structure_by_Season> League_Structure_by_Season { get; set; }
        public virtual DbSet<Penalty> Penalties { get; set; }
        public virtual DbSet<Player_Awards> Player_Awards { get; set; }
        public virtual DbSet<Player_Ratings> Player_Ratings { get; set; }
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<Playoff_Teams_by_Season> Playoff_Teams_by_Season { get; set; }
        public virtual DbSet<Season> Seasons { get; set; }
        public virtual DbSet<Teams_by_Season> Teams_by_Season { get; set; }
        public virtual DbSet<Training_Camp_by_Season> Training_Camp_by_Season { get; set; }
    }
}
