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
    
    public partial class Game_Scoring_Summary
    {
        public long ID { get; set; }
        public long Game_ID { get; set; }
        public long Time { get; set; }
        public string Scoring_Summary { get; set; }
        public long Quarter { get; set; }
    
        public virtual Game Game { get; set; }
    }
}
