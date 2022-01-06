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
    
    public partial class Season
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Season()
        {
            this.Conferences = new HashSet<Conference>();
            this.Divisions = new HashSet<Division>();
            this.Drafts = new HashSet<Draft>();
            this.Free_Agency = new HashSet<Free_Agency>();
            this.Games = new HashSet<Game>();
            this.Hall_of_Fame = new HashSet<Hall_of_Fame>();
            this.Injuries = new HashSet<Injury>();
            this.Injury_Log = new HashSet<Injury_Log>();
            this.League_Structure_by_Season = new List<League_Structure_by_Season>();
            this.Player_Awards = new HashSet<Player_Awards>();
            this.Player_Ratings = new HashSet<Player_Ratings>();
            this.Player_Retiring_Log = new HashSet<Player_Retiring_Log>();
            this.Players_By_Team = new HashSet<Players_By_Team>();
            this.Playoff_Teams_by_Season = new HashSet<Playoff_Teams_by_Season>();
            this.Teams_by_Season = new List<Teams_by_Season>();
            this.Training_Camp_by_Season = new HashSet<Training_Camp_by_Season>();
        }
    
        public long ID { get; set; }
        public long Year { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Conference> Conferences { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Division> Divisions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Draft> Drafts { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Free_Agency> Free_Agency { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Game> Games { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Hall_of_Fame> Hall_of_Fame { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Injury> Injuries { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Injury_Log> Injury_Log { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual IList<League_Structure_by_Season> League_Structure_by_Season { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Player_Awards> Player_Awards { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Player_Ratings> Player_Ratings { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Player_Retiring_Log> Player_Retiring_Log { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Players_By_Team> Players_By_Team { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Playoff_Teams_by_Season> Playoff_Teams_by_Season { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual List<Teams_by_Season> Teams_by_Season { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Training_Camp_by_Season> Training_Camp_by_Season { get; set; }
    }
}
