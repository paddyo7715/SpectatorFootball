namespace SpectatorFootball.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Draft")]
    public partial class Draft
    {
        public long ID { get; set; }

        public long Season_ID { get; set; }

        public long Round { get; set; }

        public long Pick_Number { get; set; }

        public long Franchise_ID { get; set; }

        public long? Player_ID { get; set; }

        public virtual Season Season { get; set; }

        public virtual Franchise Franchise { get; set; }

        public virtual Player Player { get; set; }
    }
}
