namespace SpectatorFootball.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class League_Structure_by_Season
    {
        public long ID { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string Short_Name { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string Long_Name { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string League_Logo_Filepath { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string League_Logo_File { get; set; }

        public long Season_ID { get; set; }

        public long Number_of_weeks { get; set; }

        public long Number_of_Games { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string Championship_Game_Name { get; set; }

        public long Num_Teams { get; set; }

        public long Num_Playoff_Teams { get; set; }

        public long Number_of_Conferences { get; set; }

        public long Number_of_Divisions { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string Draft_Type_Code { get; set; }

        public long Injuries { get; set; }

        public long Penalties { get; set; }

        public string Kickoff_Type { get; set; }

        public long Extra_Point { get; set; }

        public long Two_Point_Conversion { get; set; }

        public long Three_Point_Conversion { get; set; }

        public long Onside_Kick { get; set; }

        public long Home_Advantage { get; set; }

        public virtual Season Season { get; set; }
    }
}
