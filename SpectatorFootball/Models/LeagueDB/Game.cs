namespace SpectatorFootball.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Game")]
    public partial class Game
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Game()
        {
            Game_Player_Penalty_Stats = new HashSet<Game_Player_Penalty_Stats>();
            Game_Player_Stats = new HashSet<Game_Player_Stats>();
            Game_Scoring_Summary = new HashSet<Game_Scoring_Summary>();
        }

        public long ID { get; set; }

        public long Season_ID { get; set; }

        public long Week { get; set; }

        public long Home_Team_Franchise_ID { get; set; }

        public long Away_Team_Franchise_ID { get; set; }

        public long? Home_Score { get; set; }

        public long? Away_Score { get; set; }

        public long? Home_FirstDowns { get; set; }

        public long? Home_ThirdDown_Conversions { get; set; }

        public long? Home_ThirdDowns { get; set; }

        public long? Home_FourthDown_Conversions { get; set; }

        public long? Home_FourthDowns { get; set; }

        public long? Home_1Point_Conv_Att { get; set; }

        public long? Home_1Point_Conv_Made { get; set; }

        public long? Home_2Point_Conv_Att { get; set; }

        public long? Home_2Point_Conv_Made { get; set; }

        public long? Home_3Point_Conv_Att { get; set; }

        public long? Home_3Point_Conv_Made { get; set; }

        public long? Home_TOP { get; set; }

        public long? Home_Onside_Att { get; set; }
        public long? Home_Onside_Made { get; set; }

        public long? Away_FirstDowns { get; set; }

        public long? Away_ThirdDown_Conversions { get; set; }

        public long? Away_ThirdDowns { get; set; }

        public long? Away_FourthDown_Conversions { get; set; }

        public long? Away_FourthDowns { get; set; }

        public long? Away_1Point_Conv_Att { get; set; }

        public long? Away_1Point_Conv_Made { get; set; }

        public long? Away_2Point_Conv_Att { get; set; }

        public long? Away_2Point_Conv_Made { get; set; }

        public long? Away_3Point_Conv_Att { get; set; }

        public long? Away_3Point_Conv_Made { get; set; }

        public long? Away_TOP { get; set; }

        public long? Away_Score_Q1 { get; set; }

        public long? Home_Score_Q1 { get; set; }

        public long? Home_Score_Q2 { get; set; }

        public long? Away_Score_Q2 { get; set; }

        public long? Home_Score_Q3 { get; set; }

        public long? Away_Score_Q3 { get; set; }

        public long? Home_Score_Q4 { get; set; }

        public long? Away_Score_Q4 { get; set; }

        public long? Home_Score_OT { get; set; }

        public long? Away_Score_OT { get; set; }

        public long? Quarter { get; set; }

        public long? Time { get; set; }

        public long? Playoff_Game { get; set; }

        public long? Championship_Game { get; set; }

        public long? Game_Done { get; set; }

        public long? Home_Passing_Yards { get; set; }

        public long? Away_Passing_Yards { get; set; }

        public long? Home_Rushing_Yards { get; set; }

        public long? Away_Rushing_Yards { get; set; }

        public long? Home_Turnovers { get; set; }

        public long? Away_Turnovers { get; set; }

        public long? Home_Sacks { get; set; }

        public long? Away_Sacks { get; set; }

        public long? Away_Onside_Att { get; set; }
        public long? Away_Onside_Made { get; set; }

        public long? Forfeited_Game { get; set; }

        public virtual Season Season { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Game_Player_Penalty_Stats> Game_Player_Penalty_Stats { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Game_Player_Stats> Game_Player_Stats { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Game_Scoring_Summary> Game_Scoring_Summary { get; set; }
    }
}
