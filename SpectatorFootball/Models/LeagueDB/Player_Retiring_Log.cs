namespace SpectatorFootball.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Player_Retiring_Log
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long ID { get; set; }

        public long Season_ID { get; set; }

        public long Player_ID { get; set; }

        public long Week { get; set; }

        public virtual Season Season { get; set; }

        public virtual Player Player { get; set; }
    }
}
