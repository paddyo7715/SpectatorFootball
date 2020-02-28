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
    
    public partial class League
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public League()
        {
            this.Conferences = new HashSet<Conference>();
            this.Divisions = new HashSet<Division>();
            this.Games = new HashSet<Game>();
            this.Teams = new HashSet<Team>();
        }
    
        public long ID { get; set; }
        public string Short_Name { get; set; }
        public string Long_Name { get; set; }
        public string League_Logo_Filepath { get; set; }
        public long Starting_Year { get; set; }
        public long Number_of_weeks { get; set; }
        public long Number_of_Games { get; set; }
        public string Championship_Game_Name { get; set; }
        public long Num_Teams { get; set; }
        public long Num_Playoff_Teams { get; set; }
        public long Number_of_Conferences { get; set; }
        public long Number_of_Divisions { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Conference> Conferences { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Division> Divisions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Game> Games { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Team> Teams { get; set; }
    }
}
