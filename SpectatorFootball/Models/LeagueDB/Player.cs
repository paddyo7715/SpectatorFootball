namespace SpectatorFootball.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Player
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Player()
        {
            Drafts = new HashSet<Draft>();
            Free_Agency = new HashSet<Free_Agency>();
            Game_Player_Penalty_Stats = new HashSet<Game_Player_Penalty_Stats>();
            Game_Player_Stats = new HashSet<Game_Player_Stats>();
            Hall_of_Fame = new HashSet<Hall_of_Fame>();
            Injuries = new HashSet<Injury>();
            Injury_Log = new HashSet<Injury_Log>();
            Player_Awards = new HashSet<Player_Awards>();
            Player_Ratings = new HashSet<Player_Ratings>();
            Player_Retiring_Log = new HashSet<Player_Retiring_Log>();
            Players_By_Team = new HashSet<Players_By_Team>();
            Training_Camp_by_Season = new HashSet<Training_Camp_by_Season>();
        }

        public long ID { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string First_Name { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string Last_Name { get; set; }

        public long Age { get; set; }

        public long Height { get; set; }

        public long Weight { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string Handedness { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string HomeTown { get; set; }

        public long Pos { get; set; }

        public long Retired { get; set; }

        public long Eligible_for_Draft { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string Draft_Profile { get; set; }

        [Column(TypeName = "real")]
        public double Draft_Grade { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Draft> Drafts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Free_Agency> Free_Agency { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Game_Player_Penalty_Stats> Game_Player_Penalty_Stats { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Game_Player_Stats> Game_Player_Stats { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Hall_of_Fame> Hall_of_Fame { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Injury> Injuries { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Injury_Log> Injury_Log { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Player_Awards> Player_Awards { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Player_Ratings> Player_Ratings { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Player_Retiring_Log> Player_Retiring_Log { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Players_By_Team> Players_By_Team { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Training_Camp_by_Season> Training_Camp_by_Season { get; set; }
    }
}
