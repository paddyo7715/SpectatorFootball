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
    
    public partial class Game_Player_Punt_Defenders
    {
        public long Game_ID { get; set; }
        public long Player_ID { get; set; }
        public long Franchise_ID { get; set; }
        public long Forced_Fumbles_Punt { get; set; }
        public long Fumbles_Punt_Recovered { get; set; }
        public long Fumbles_Punt_Recovered_TDs { get; set; }
        public long Fumbles_Punt_Recovered_Yards { get; set; }
        public long Punt_Tackles { get; set; }
    
        public virtual Franchise Franchise { get; set; }
        public virtual Game Game { get; set; }
        public virtual Player Player { get; set; }
    }
}
