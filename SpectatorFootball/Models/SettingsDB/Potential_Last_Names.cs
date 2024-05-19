namespace SpectatorFootball.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Potential_Last_Names
    {
        [Key]
        [StringLength(2147483647)]
        public string LastName { get; set; }
    }
}
