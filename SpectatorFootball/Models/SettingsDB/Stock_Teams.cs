namespace SpectatorFootball.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Stock_Teams
    {
        public long ID { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string City_Abr { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string City { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string Nickname { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string Stadium_Name { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string Stadium_Location { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string Stadium_Capacity { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string Stadium_Img_Path { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string Helmet_img_path { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string Helmet_Color { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string Helmet_Logo_Color { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string Helmet_Facemask_Color { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string Socks_Color { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string Cleats_Color { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string Home_jersey_Color { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string Home_Sleeve_Color { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string Home_Jersey_Number_Color { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string Home_Jersey_Number_Outline_Color { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string Home_Jersey_Shoulder_Stripe { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string Home_Jersey_Sleeve_Stripe_Color_1 { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string Home_Jersey_Sleeve_Stripe_Color_2 { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string Home_Jersey_Sleeve_Stripe_Color_3 { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string Home_Jersey_Sleeve_Stripe_Color_4 { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string Home_Jersey_Sleeve_Stripe_Color_5 { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string Home_Jersey_Sleeve_Stripe_Color_6 { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string Home_Pants_Color { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string Home_Pants_Stripe_Color_1 { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string Home_Pants_Stripe_Color_2 { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string Home_Pants_Stripe_Color_3 { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string Away_jersey_Color { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string Away_Sleeve_Color { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string Away_Jersey_Number_Color { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string Away_Jersey_Number_Outline_Color { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string Away_Jersey_Shoulder_Stripe { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string Away_Jersey_Sleeve_Stripe_Color_1 { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string Away_Jersey_Sleeve_Stripe_Color_2 { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string Away_Jersey_Sleeve_Stripe_Color_3 { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string Away_Jersey_Sleeve_Stripe_Color_4 { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string Away_Jersey_Sleeve_Stripe_Color_5 { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string Away_Jersey_Sleeve_Stripe_Color_6 { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string Away_Pants_Color { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string Away_Pants_Stripe_Color_1 { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string Away_Pants_Stripe_Color_2 { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string Away_Pants_Stripe_Color_3 { get; set; }

        public long Stadium_Field_Type { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string Stadium_Field_Color { get; set; }
    }
}
