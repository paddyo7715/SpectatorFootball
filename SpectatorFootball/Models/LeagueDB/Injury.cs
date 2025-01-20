namespace SpectatorFootball.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Injury
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Season_ID { get; set; }

        public long Franchise_ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Player_ID { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Week { get; set; }

        public long Num_of_Weeks { get; set; }

        public long Career_Ending { get; set; }

        public long Season_Ending { get; set; }

        public long Num_of_Plays { get; set; }

        public virtual Franchise Franchise { get; set; }

        public virtual Player Player { get; set; }

        public virtual Season Season { get; set; }
    }
}
