namespace SpectatorFootball.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Conference")]
    public partial class Conference
    {
        public long ID { get; set; }

        public long Ordinal { get; set; }

        public long Season_ID { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string Conf_Name { get; set; }

        public virtual Season Season { get; set; }
    }
}
