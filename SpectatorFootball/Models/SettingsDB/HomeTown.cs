namespace SpectatorFootball.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class HomeTown
    {
        public long ID { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string City { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string State { get; set; }
    }
}
