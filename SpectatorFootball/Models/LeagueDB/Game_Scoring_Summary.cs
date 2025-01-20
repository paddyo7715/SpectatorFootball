namespace SpectatorFootball.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Game_Scoring_Summary
    {
        public long ID { get; set; }

        public long Game_ID { get; set; }

        public long Time { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string Scoring_Summary { get; set; }

        public long Quarter { get; set; }

        public virtual Game Game { get; set; }
    }
}
