namespace SpectatorFootball.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Game_Player_Penalty_Stats
    {
        public long ID { get; set; }

        public long Game_ID { get; set; }

        public long Player_ID { get; set; }

        public long Franchise_ID { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string Penalty_Code { get; set; }

        public long Penalty_Yards { get; set; }

        public virtual Franchise Franchise { get; set; }

        public virtual Game Game { get; set; }

        public virtual Player Player { get; set; }
    }
}
