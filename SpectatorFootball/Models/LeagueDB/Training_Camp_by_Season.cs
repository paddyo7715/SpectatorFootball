namespace SpectatorFootball.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Training_Camp_by_Season
    {
        public long ID { get; set; }

        public long Player_ID { get; set; }

        public long Season_ID { get; set; }

        public long Franchise_ID { get; set; }

        public long Grade { get; set; }

        public long Made_Team { get; set; }

        public virtual Franchise Franchise { get; set; }

        public virtual Player Player { get; set; }

        public virtual Season Season { get; set; }
    }
}
