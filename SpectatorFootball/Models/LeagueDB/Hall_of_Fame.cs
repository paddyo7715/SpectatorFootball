namespace SpectatorFootball.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Hall_of_Fame
    {
        public long ID { get; set; }

        public long Season_ID { get; set; }

        public long Player_ID { get; set; }

        public virtual Season Season { get; set; }

        public virtual Player Player { get; set; }
    }
}
