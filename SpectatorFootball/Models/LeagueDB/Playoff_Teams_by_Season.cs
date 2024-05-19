namespace SpectatorFootball.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Playoff_Teams_by_Season
    {
        public long ID { get; set; }

        public long Franchise_ID { get; set; }

        public long Season_ID { get; set; }

        public long Rank { get; set; }

        public long Eliminated { get; set; }

        public long conf_side_id { get; set; }

        public virtual Franchise Franchise { get; set; }

        public virtual Season Season { get; set; }
    }
}
