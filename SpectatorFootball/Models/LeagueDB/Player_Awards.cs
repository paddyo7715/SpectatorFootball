namespace SpectatorFootball.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Player_Awards
    {
        public long ID { get; set; }

        public long Season_ID { get; set; }

        public long Player_ID { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string Award_Code { get; set; }

        public virtual Award Award { get; set; }

        public virtual Player Player { get; set; }

        public virtual Season Season { get; set; }
    }
}
