using System.Collections.Generic;
using SpectatorFootball.Models;

namespace SpectatorFootball
{
    public class Team_Helper
    {
        public static Stock_Teams get_team_from_name(string team_city_name, List<Stock_Teams> t_list)
        {
            Stock_Teams r = null;

            foreach (var t in t_list)
            {
                string t_city_name = t.City + " " + t.Nickname;
                if (t_city_name.Trim() == team_city_name.Trim())
                {
                    r = t;
                    break;
                }
            }

            return r;
        }
        public static Stock_Teams ClonseStock_Team(Stock_Teams st)
        {
            return new Stock_Teams()
            {
                ID = st.ID,
                City_Abr = st.City_Abr,
                City = st.City,
                Nickname = st.Nickname,
                Stadium_Name = st.Stadium_Name,
                Stadium_Location = st.Stadium_Location,
                Stadium_Capacity = st.Stadium_Capacity,
                Stadium_Img_Path = st.Stadium_Img_Path,
                Stadium_Field_Type = st.Stadium_Field_Type,
                Stadium_Field_Color = st.Stadium_Field_Color,
                Helmet_img_path = st.Helmet_img_path,
                Helmet_Color = st.Helmet_Color,
                Helmet_Logo_Color = st.Helmet_Logo_Color,
                Helmet_Facemask_Color = st.Helmet_Facemask_Color,
                Socks_Color = st.Socks_Color,
                Cleats_Color = st.Cleats_Color,
                Home_jersey_Color = st.Home_jersey_Color,
                Home_Sleeve_Color = st.Home_Sleeve_Color,
                Home_Jersey_Number_Color = st.Home_Jersey_Number_Color,
                Home_Jersey_Number_Outline_Color = st.Home_Jersey_Number_Outline_Color,
                Home_Jersey_Shoulder_Stripe = st.Home_Jersey_Shoulder_Stripe,
                Home_Jersey_Sleeve_Stripe_Color_1 = st.Home_Jersey_Sleeve_Stripe_Color_1,
                Home_Jersey_Sleeve_Stripe_Color_2 = st.Home_Jersey_Sleeve_Stripe_Color_2,
                Home_Jersey_Sleeve_Stripe_Color_3 = st.Home_Jersey_Sleeve_Stripe_Color_3,
                Home_Jersey_Sleeve_Stripe_Color_4 = st.Home_Jersey_Sleeve_Stripe_Color_4,
                Home_Jersey_Sleeve_Stripe_Color_5 = st.Home_Jersey_Sleeve_Stripe_Color_5,
                Home_Jersey_Sleeve_Stripe_Color_6 = st.Home_Jersey_Sleeve_Stripe_Color_6,
                Home_Pants_Color = st.Home_Pants_Color,
                Home_Pants_Stripe_Color_1 = st.Home_Pants_Stripe_Color_1,
                Home_Pants_Stripe_Color_2 = st.Home_Pants_Stripe_Color_2,
                Home_Pants_Stripe_Color_3 = st.Home_Pants_Stripe_Color_3,
                Away_jersey_Color = st.Away_jersey_Color,
                Away_Sleeve_Color = st.Away_Sleeve_Color,
                Away_Jersey_Number_Color = st.Away_Jersey_Number_Color,
                Away_Jersey_Number_Outline_Color = st.Away_Jersey_Number_Outline_Color,
                Away_Jersey_Shoulder_Stripe = st.Away_Jersey_Shoulder_Stripe,
                Away_Jersey_Sleeve_Stripe_Color_1 = st.Away_Jersey_Sleeve_Stripe_Color_1,
                Away_Jersey_Sleeve_Stripe_Color_2 = st.Away_Jersey_Sleeve_Stripe_Color_2,
                Away_Jersey_Sleeve_Stripe_Color_3 = st.Away_Jersey_Sleeve_Stripe_Color_3,
                Away_Jersey_Sleeve_Stripe_Color_4 = st.Away_Jersey_Sleeve_Stripe_Color_4,
                Away_Jersey_Sleeve_Stripe_Color_5 = st.Away_Jersey_Sleeve_Stripe_Color_5,
                Away_Jersey_Sleeve_Stripe_Color_6 = st.Away_Jersey_Sleeve_Stripe_Color_6,
                Away_Pants_Color = st.Away_Pants_Color,
                Away_Pants_Stripe_Color_1 = st.Away_Pants_Stripe_Color_1,
                Away_Pants_Stripe_Color_2 = st.Away_Pants_Stripe_Color_2,
                Away_Pants_Stripe_Color_3 = st.Away_Pants_Stripe_Color_3
            };

            
        }
        public static SpectatorFootball.Models.Team Clone_Team(SpectatorFootball.Models.Team t)
        {
            return new SpectatorFootball.Models.Team()
            {
                ID = t.ID,
                Owner = t.Owner,
                City_Abr = t.City_Abr,
                City = t.City,
                Nickname = t.Nickname,
                Stadium_Name = t.Stadium_Name,
                Stadium_Location = t.Stadium_Location,
                Stadium_Capacity = t.Stadium_Capacity,
                Stadium_Img_Path = t.Stadium_Img_Path,
                Stadium_Field_Type = t.Stadium_Field_Type,
                Stadium_Field_Color = t.Stadium_Field_Color,
                Helmet_img_path = t.Helmet_img_path,
                Helmet_Color = t.Helmet_Color,
                Helmet_Logo_Color = t.Helmet_Logo_Color,
                Helmet_Facemask_Color = t.Helmet_Facemask_Color,
                Socks_Color = t.Socks_Color,
                Cleats_Color = t.Cleats_Color,
                Home_jersey_Color = t.Home_jersey_Color,
                Home_Sleeve_Color = t.Home_Sleeve_Color,
                Home_Jersey_Number_Color = t.Home_Jersey_Number_Color,
                Home_Jersey_Number_Outline_Color = t.Home_Jersey_Number_Outline_Color,
                Home_Jersey_Shoulder_Stripe = t.Home_Jersey_Shoulder_Stripe,
                Home_Jersey_Sleeve_Stripe_Color_1 = t.Home_Jersey_Sleeve_Stripe_Color_1,
                Home_Jersey_Sleeve_Stripe_Color_2 = t.Home_Jersey_Sleeve_Stripe_Color_2,
                Home_Jersey_Sleeve_Stripe_Color_3 = t.Home_Jersey_Sleeve_Stripe_Color_3,
                Home_Jersey_Sleeve_Stripe_Color_4 = t.Home_Jersey_Sleeve_Stripe_Color_4,
                Home_Jersey_Sleeve_Stripe_Color_5 = t.Home_Jersey_Sleeve_Stripe_Color_5,
                Home_Jersey_Sleeve_Stripe_Color_6 = t.Home_Jersey_Sleeve_Stripe_Color_6,
                Home_Pants_Color = t.Home_Pants_Color,
                Home_Pants_Stripe_Color_1 = t.Home_Pants_Stripe_Color_1,
                Home_Pants_Stripe_Color_2 = t.Home_Pants_Stripe_Color_2,
                Home_Pants_Stripe_Color_3 = t.Home_Pants_Stripe_Color_3,
                Away_jersey_Color = t.Away_jersey_Color,
                Away_Sleeve_Color = t.Away_Sleeve_Color,
                Away_Jersey_Number_Color = t.Away_Jersey_Number_Color,
                Away_Jersey_Number_Outline_Color = t.Away_Jersey_Number_Outline_Color,
                Away_Jersey_Shoulder_Stripe = t.Away_Jersey_Shoulder_Stripe,
                Away_Jersey_Sleeve_Stripe_Color_1 = t.Away_Jersey_Sleeve_Stripe_Color_1,
                Away_Jersey_Sleeve_Stripe_Color_2 = t.Away_Jersey_Sleeve_Stripe_Color_2,
                Away_Jersey_Sleeve_Stripe_Color_3 = t.Away_Jersey_Sleeve_Stripe_Color_3,
                Away_Jersey_Sleeve_Stripe_Color_4 = t.Away_Jersey_Sleeve_Stripe_Color_4,
                Away_Jersey_Sleeve_Stripe_Color_5 = t.Away_Jersey_Sleeve_Stripe_Color_5,
                Away_Jersey_Sleeve_Stripe_Color_6 = t.Away_Jersey_Sleeve_Stripe_Color_6,
                Away_Pants_Color = t.Away_Pants_Color,
                Away_Pants_Stripe_Color_1 = t.Away_Pants_Stripe_Color_1,
                Away_Pants_Stripe_Color_2 = t.Away_Pants_Stripe_Color_2,
                Away_Pants_Stripe_Color_3 = t.Away_Pants_Stripe_Color_3
            };


        }
        public static SpectatorFootball.Models.Team Clonse_Team_from_Stock(Stock_Teams st)
        {
            return new SpectatorFootball.Models.Team()
            {
                ID = st.ID,
                City_Abr = st.City_Abr,
                City = st.City,
                Nickname = st.Nickname,
                Stadium_Name = st.Stadium_Name,
                Stadium_Location = st.Stadium_Location,
                Stadium_Capacity = st.Stadium_Capacity,
                Stadium_Img_Path = st.Stadium_Img_Path,
                Stadium_Field_Type = st.Stadium_Field_Type,
                Stadium_Field_Color = st.Stadium_Field_Color,
                Helmet_img_path = st.Helmet_img_path,
                Helmet_Color = st.Helmet_Color,
                Helmet_Logo_Color = st.Helmet_Logo_Color,
                Helmet_Facemask_Color = st.Helmet_Facemask_Color,
                Socks_Color = st.Socks_Color,
                Cleats_Color = st.Cleats_Color,
                Home_jersey_Color = st.Home_jersey_Color,
                Home_Sleeve_Color = st.Home_Sleeve_Color,
                Home_Jersey_Number_Color = st.Home_Jersey_Number_Color,
                Home_Jersey_Number_Outline_Color = st.Home_Jersey_Number_Outline_Color,
                Home_Jersey_Shoulder_Stripe = st.Home_Jersey_Shoulder_Stripe,
                Home_Jersey_Sleeve_Stripe_Color_1 = st.Home_Jersey_Sleeve_Stripe_Color_1,
                Home_Jersey_Sleeve_Stripe_Color_2 = st.Home_Jersey_Sleeve_Stripe_Color_2,
                Home_Jersey_Sleeve_Stripe_Color_3 = st.Home_Jersey_Sleeve_Stripe_Color_3,
                Home_Jersey_Sleeve_Stripe_Color_4 = st.Home_Jersey_Sleeve_Stripe_Color_4,
                Home_Jersey_Sleeve_Stripe_Color_5 = st.Home_Jersey_Sleeve_Stripe_Color_5,
                Home_Jersey_Sleeve_Stripe_Color_6 = st.Home_Jersey_Sleeve_Stripe_Color_6,
                Home_Pants_Color = st.Home_Pants_Color,
                Home_Pants_Stripe_Color_1 = st.Home_Pants_Stripe_Color_1,
                Home_Pants_Stripe_Color_2 = st.Home_Pants_Stripe_Color_2,
                Home_Pants_Stripe_Color_3 = st.Home_Pants_Stripe_Color_3,
                Away_jersey_Color = st.Away_jersey_Color,
                Away_Sleeve_Color = st.Away_Sleeve_Color,
                Away_Jersey_Number_Color = st.Away_Jersey_Number_Color,
                Away_Jersey_Number_Outline_Color = st.Away_Jersey_Number_Outline_Color,
                Away_Jersey_Shoulder_Stripe = st.Away_Jersey_Shoulder_Stripe,
                Away_Jersey_Sleeve_Stripe_Color_1 = st.Away_Jersey_Sleeve_Stripe_Color_1,
                Away_Jersey_Sleeve_Stripe_Color_2 = st.Away_Jersey_Sleeve_Stripe_Color_2,
                Away_Jersey_Sleeve_Stripe_Color_3 = st.Away_Jersey_Sleeve_Stripe_Color_3,
                Away_Jersey_Sleeve_Stripe_Color_4 = st.Away_Jersey_Sleeve_Stripe_Color_4,
                Away_Jersey_Sleeve_Stripe_Color_5 = st.Away_Jersey_Sleeve_Stripe_Color_5,
                Away_Jersey_Sleeve_Stripe_Color_6 = st.Away_Jersey_Sleeve_Stripe_Color_6,
                Away_Pants_Color = st.Away_Pants_Color,
                Away_Pants_Stripe_Color_1 = st.Away_Pants_Stripe_Color_1,
                Away_Pants_Stripe_Color_2 = st.Away_Pants_Stripe_Color_2,
                Away_Pants_Stripe_Color_3 = st.Away_Pants_Stripe_Color_3
            };


        }
        public static bool isUniqueTeam(List<Team> lt, int orig_ind, Team new_team)
        {
            bool r = false;
            int i = 0;

            foreach (Team t in lt)
            {
                if (i == orig_ind)
                    continue;

                if (t.City == new_team.City && t.Nickname == new_team.Nickname)
                {
                    r = true;
                    break;
                }

                i++;
            }

            return r;
        }

    }
}
