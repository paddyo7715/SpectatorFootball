//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Game
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Game()
        {
            this.Game_Player_Defense_Stats = new HashSet<Game_Player_Defense_Stats>();
            this.Game_Player_FG_Defense_Stats = new HashSet<Game_Player_FG_Defense_Stats>();
            this.Game_Player_Kick_Returner_Stats = new HashSet<Game_Player_Kick_Returner_Stats>();
            this.Game_Player_Kicker_Stats = new HashSet<Game_Player_Kicker_Stats>();
            this.Game_Player_Kickoff_Defenders = new HashSet<Game_Player_Kickoff_Defenders>();
            this.Game_Player_Kickoff_Receiver_Stats = new HashSet<Game_Player_Kickoff_Receiver_Stats>();
            this.Game_Player_Offensive_Linemen_Stats = new HashSet<Game_Player_Offensive_Linemen_Stats>();
            this.Game_Player_Pass_Defense_Stats = new HashSet<Game_Player_Pass_Defense_Stats>();
            this.Game_Player_Passing_Stats = new HashSet<Game_Player_Passing_Stats>();
            this.Game_Player_Penalty_Stats = new HashSet<Game_Player_Penalty_Stats>();
            this.Game_Player_Punt_Defenders = new HashSet<Game_Player_Punt_Defenders>();
            this.Game_Player_Punt_Receiver_Stats = new HashSet<Game_Player_Punt_Receiver_Stats>();
            this.Game_Player_Punt_Returner_Stats = new HashSet<Game_Player_Punt_Returner_Stats>();
            this.Game_Player_Punter_Stats = new HashSet<Game_Player_Punter_Stats>();
            this.Game_Player_Receiving_Stats = new HashSet<Game_Player_Receiving_Stats>();
            this.Game_Player_Rushing_Stats = new HashSet<Game_Player_Rushing_Stats>();
            this.Game_Scoring_Summary = new HashSet<Game_Scoring_Summary>();
        }
    
        public long ID { get; set; }
        public long Season_ID { get; set; }
        public long Week { get; set; }
        public long Home_Team_Franchise_ID { get; set; }
        public long Away_Team_Franchise_ID { get; set; }
        public Nullable<long> Home_Score { get; set; }
        public Nullable<long> Away_Score { get; set; }
        public Nullable<long> Home_FirstDowns { get; set; }
        public Nullable<long> Home_ThirdDown_Conversions { get; set; }
        public Nullable<long> Home_ThirdDowns { get; set; }
        public Nullable<long> Home_FourthDown_Conversions { get; set; }
        public Nullable<long> Home_FourthDowns { get; set; }
        public Nullable<long> Home_1Point_Conv_Att { get; set; }
        public Nullable<long> Home_1Point_Conv_Made { get; set; }
        public Nullable<long> Home_2Point_Conv_Att { get; set; }
        public Nullable<long> Home_2Point_Conv_Made { get; set; }
        public Nullable<long> Home_3Point_Conv_Att { get; set; }
        public Nullable<long> Home_3Point_Conv_Made { get; set; }
        public Nullable<long> Home_TOP { get; set; }
        public Nullable<long> Away_FirstDowns { get; set; }
        public Nullable<long> Away_ThirdDown_Conversions { get; set; }
        public Nullable<long> Away_ThirdDowns { get; set; }
        public Nullable<long> Away_FourthDown_Conversions { get; set; }
        public Nullable<long> Away_FourthDowns { get; set; }
        public Nullable<long> Away_1Point_Conv_Att { get; set; }
        public Nullable<long> Away_1Point_Conv_Made { get; set; }
        public Nullable<long> Away_2Point_Conv_Att { get; set; }
        public Nullable<long> Away_2Point_Conv_Made { get; set; }
        public Nullable<long> Away_3Point_Conv_Att { get; set; }
        public Nullable<long> Away_3Point_Conv_Made { get; set; }
        public Nullable<long> Away_TOP { get; set; }
        public Nullable<long> Away_Score_Q1 { get; set; }
        public Nullable<long> Home_Score_Q1 { get; set; }
        public Nullable<long> Home_Score_Q2 { get; set; }
        public Nullable<long> Away_Score_Q2 { get; set; }
        public Nullable<long> Home_Score_Q3 { get; set; }
        public Nullable<long> Away_Score_Q3 { get; set; }
        public Nullable<long> Home_Score_Q4 { get; set; }
        public Nullable<long> Away_Score_Q4 { get; set; }
        public Nullable<long> Home_Score_OT { get; set; }
        public Nullable<long> Away_Score_OT { get; set; }
        public Nullable<long> Quarter { get; set; }
        public Nullable<long> Time { get; set; }
        public Nullable<long> Playoff_Game { get; set; }
        public Nullable<long> Championship_Game { get; set; }
        public Nullable<long> Game_Done { get; set; }
        public Nullable<long> Home_Passing_Yards { get; set; }
        public Nullable<long> Away_Passing_Yards { get; set; }
        public Nullable<long> Home_Rushing_Yards { get; set; }
        public Nullable<long> Away_Rushing_Yards { get; set; }
        public Nullable<long> Home_Turnovers { get; set; }
        public Nullable<long> Away_Turnovers { get; set; }
    
        public virtual Season Season { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Game_Player_Defense_Stats> Game_Player_Defense_Stats { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Game_Player_FG_Defense_Stats> Game_Player_FG_Defense_Stats { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Game_Player_Kick_Returner_Stats> Game_Player_Kick_Returner_Stats { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Game_Player_Kicker_Stats> Game_Player_Kicker_Stats { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Game_Player_Kickoff_Defenders> Game_Player_Kickoff_Defenders { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Game_Player_Kickoff_Receiver_Stats> Game_Player_Kickoff_Receiver_Stats { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Game_Player_Offensive_Linemen_Stats> Game_Player_Offensive_Linemen_Stats { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Game_Player_Pass_Defense_Stats> Game_Player_Pass_Defense_Stats { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Game_Player_Passing_Stats> Game_Player_Passing_Stats { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Game_Player_Penalty_Stats> Game_Player_Penalty_Stats { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Game_Player_Punt_Defenders> Game_Player_Punt_Defenders { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Game_Player_Punt_Receiver_Stats> Game_Player_Punt_Receiver_Stats { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Game_Player_Punt_Returner_Stats> Game_Player_Punt_Returner_Stats { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Game_Player_Punter_Stats> Game_Player_Punter_Stats { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Game_Player_Receiving_Stats> Game_Player_Receiving_Stats { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Game_Player_Rushing_Stats> Game_Player_Rushing_Stats { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Game_Scoring_Summary> Game_Scoring_Summary { get; set; }
    }
}
