using System.Collections.Generic;

namespace SpectatorFootball
{
    public class TeamMdl
    {
        public int id { get; set; }
        public string Owner { get; set; }
        public string City_Abr { get; set; } = "";
        public string City { get; set; } = "";
        public string Nickname { get; set; } = "";
        public StadiumMdl Stadium { get; set; }
        public UniformMdl Uniform { get; set; }
        public string Helmet_img_path { get; set; } = "";

        public List<PlayerMdl> Players { get; set; }
        public TeamMdl(int id, string City)
        {
            this.id = id;
            this.City = City;
        }
        // Clone the team model
        public TeamMdl(TeamMdl tc)
        {
            id = tc.id;
            Owner = tc.Owner;
            City = tc.City;
            Nickname = tc.Nickname;
            City_Abr = tc.City_Abr;

            StadiumMdl std = new StadiumMdl(tc.Stadium.Stadium_Name, tc.Stadium.Stadium_Location, tc.Stadium.Field_Type, tc.Stadium.Field_Color, tc.Stadium.Capacity, tc.Stadium.Stadium_Img_Path);
            Stadium = std;

            HelmetMdl helmt = new HelmetMdl(tc.Uniform.Helmet.Helmet_Color, tc.Uniform.Helmet.Helmet_Logo_Color, tc.Uniform.Helmet.Helmet_Facemask_Color);
            JerseyMdl h_jers = new JerseyMdl(tc.Uniform.Home_Jersey.Jersey_Color, tc.Uniform.Home_Jersey.Sleeve_Color, tc.Uniform.Home_Jersey.Shoulder_Stripe_Color, tc.Uniform.Home_Jersey.Number_Color, tc.Uniform.Home_Jersey.Number_Outline_Color, tc.Uniform.Home_Jersey.Sleeve_Stripe1, tc.Uniform.Home_Jersey.Sleeve_Stripe2, tc.Uniform.Home_Jersey.Sleeve_Stripe3, tc.Uniform.Home_Jersey.Sleeve_Stripe4, tc.Uniform.Home_Jersey.Sleeve_Stripe5, tc.Uniform.Home_Jersey.Sleeve_Stripe6);
            PantsMdl h_pants = new PantsMdl(tc.Uniform.Home_Pants.Pants_Color, tc.Uniform.Home_Pants.Stripe_Color_1, tc.Uniform.Home_Pants.Stripe_Color_2, tc.Uniform.Home_Pants.Stripe_Color_3);

            JerseyMdl a_jers = new JerseyMdl(tc.Uniform.Away_Jersey.Jersey_Color, tc.Uniform.Away_Jersey.Sleeve_Color, tc.Uniform.Away_Jersey.Shoulder_Stripe_Color, tc.Uniform.Away_Jersey.Number_Color, tc.Uniform.Away_Jersey.Number_Outline_Color, tc.Uniform.Away_Jersey.Sleeve_Stripe1, tc.Uniform.Away_Jersey.Sleeve_Stripe2, tc.Uniform.Away_Jersey.Sleeve_Stripe3, tc.Uniform.Away_Jersey.Sleeve_Stripe4, tc.Uniform.Away_Jersey.Sleeve_Stripe5, tc.Uniform.Away_Jersey.Sleeve_Stripe6);
            PantsMdl a_pants = new PantsMdl(tc.Uniform.Away_Pants.Pants_Color, tc.Uniform.Away_Pants.Stripe_Color_1, tc.Uniform.Away_Pants.Stripe_Color_2, tc.Uniform.Away_Pants.Stripe_Color_3);
            FootwearMdl footw = new FootwearMdl(tc.Uniform.Footwear.Socks_Color, tc.Uniform.Footwear.Cleats_Color);

            UniformMdl uni = new UniformMdl(helmt, h_jers, a_jers, h_pants, a_pants, footw);

            Uniform = uni;
            Helmet_img_path = tc.Helmet_img_path;
        }
        public void setStockImagePaths(string helmet_folder, string stadium_folder)
        {
            Helmet_img_path = helmet_folder;
            Stadium.Stadium_Img_Path = stadium_folder;
        }
        public void setPlayers(List<PlayerMdl> Players)
        {
        }



        public void setFields(string Owner, string City_Abr, string City, string Nickname, StadiumMdl Stadium, UniformMdl uniform, string Helmet_img_path)
        {
            this.Owner = Owner;
            this.City_Abr = City_Abr;
            this.City = City;
            this.Nickname = Nickname;
            this.Stadium = Stadium;
            Uniform = uniform;
            this.Helmet_img_path = Helmet_img_path;
        }
        public void setID(int id)
        {
            this.id = id;
        }
    }
}
