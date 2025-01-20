namespace SpectatorFootball.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DBVersion")]
    public partial class DBVersion
    {
        public long ID { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string Date_Created { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string Version { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string Action { get; set; }
    }
}
