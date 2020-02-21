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
            Stock_Teams r = new Stock_Teams();

            var sourceProperties = st.GetType().GetProperties();
            var destProperties = r.GetType().GetProperties();

            foreach (var sourceProperty in sourceProperties)
            {
                foreach (var destProperty in destProperties)
                {
                    if (sourceProperty.Name == destProperty.Name && sourceProperty.PropertyType == destProperty.PropertyType)
                    {
                        destProperty.SetValue(r, sourceProperty.GetValue(st));
                        break;
                    }
                }
            }

            return r;
        }
        public static SpectatorFootball.Models.Team Clone_Team(SpectatorFootball.Models.Team t)
        {
            SpectatorFootball.Models.Team r = new SpectatorFootball.Models.Team();

            var sourceProperties = t.GetType().GetProperties();
            var destProperties = r.GetType().GetProperties();

            foreach (var sourceProperty in sourceProperties)
            {
                foreach (var destProperty in destProperties)
                {
                    if (sourceProperty.Name == destProperty.Name && sourceProperty.PropertyType == destProperty.PropertyType)
                    {
                        destProperty.SetValue(r, sourceProperty.GetValue(t));
                        break;
                    }
                }
            }

            return r;
        }
        public static SpectatorFootball.Models.Team Clonse_Team_from_Stock(Stock_Teams st)
        {

            SpectatorFootball.Models.Team r = new SpectatorFootball.Models.Team();

            var sourceProperties = st.GetType().GetProperties();
            var destProperties = r.GetType().GetProperties();

            foreach (var sourceProperty in sourceProperties)
            {
                foreach (var destProperty in destProperties)
                {
                    if (sourceProperty.Name == destProperty.Name && sourceProperty.PropertyType == destProperty.PropertyType)
                    {
                        destProperty.SetValue(r, sourceProperty.GetValue(st));
                        break;
                    }
                }
            }

            return r;

         }


        public static void CopyTeamValues(SpectatorFootball.Models.Team s, SpectatorFootball.Models.Team t)
        {

            var sourceProperties = s.GetType().GetProperties();
            var destProperties = t.GetType().GetProperties();

            foreach (var sourceProperty in sourceProperties)
            {
                foreach (var destProperty in destProperties)
                {
                    if (sourceProperty.Name == destProperty.Name && sourceProperty.PropertyType == destProperty.PropertyType)
                    {
                        destProperty.SetValue(t, sourceProperty.GetValue(s));
                        break;
                    }
                }
            }

            /*            t.ID = s.ID;
                        t.Owner = s.Owner;
                        t.City_Abr = s.City_Abr;
                        t.City = s.City;
                        t.Nickname = s.Nickname;
                        t.Stadium_Name = s.Stadium_Name;
                        t.Stadium_Location = s.Stadium_Location;
                        t.Stadium_Capacity = s.Stadium_Capacity;
                        t.Stadium_Img_Path = s.Stadium_Img_Path;
                        t.Stadium_Field_Type = s.Stadium_Field_Type;
                        t.Stadium_Field_Color = s.Stadium_Field_Color;
                        t.Helmet_img_path = s.Helmet_img_path;
                        t.Helmet_Color = s.Helmet_Color;
                        t.Helmet_Logo_Color = s.Helmet_Logo_Color;
                        t.Helmet_Facemask_Color = s.Helmet_Facemask_Color;
                        t.Socks_Color = s.Socks_Color;
                        t.Cleats_Color = s.Cleats_Color;
                        t.Home_jersey_Color = s.Home_jersey_Color;
                        t.Home_Sleeve_Color = s.Home_Sleeve_Color;
                        t.Home_Jersey_Number_Color = s.Home_Jersey_Number_Color;
                        t.Home_Jersey_Number_Outline_Color = s.Home_Jersey_Number_Outline_Color;
                        t.Home_Jersey_Shoulder_Stripe = s.Home_Jersey_Shoulder_Stripe;
                        t.Home_Jersey_Sleeve_Stripe_Color_1 = s.Home_Jersey_Sleeve_Stripe_Color_1;
                        t.Home_Jersey_Sleeve_Stripe_Color_2 = s.Home_Jersey_Sleeve_Stripe_Color_2;
                        t.Home_Jersey_Sleeve_Stripe_Color_3 = s.Home_Jersey_Sleeve_Stripe_Color_3;
                        t.Home_Jersey_Sleeve_Stripe_Color_4 = s.Home_Jersey_Sleeve_Stripe_Color_4;
                        t.Home_Jersey_Sleeve_Stripe_Color_5 = s.Home_Jersey_Sleeve_Stripe_Color_5;
                        t.Home_Jersey_Sleeve_Stripe_Color_6 = s.Home_Jersey_Sleeve_Stripe_Color_6;
                        t.Home_Pants_Color = s.Home_Pants_Color;
                        t.Home_Pants_Stripe_Color_1 = s.Home_Pants_Stripe_Color_1;
                        t.Home_Pants_Stripe_Color_2 = s.Home_Pants_Stripe_Color_2;
                        t.Home_Pants_Stripe_Color_3 = s.Home_Pants_Stripe_Color_3;
                        t.Away_jersey_Color = s.Away_jersey_Color;
                        t.Away_Sleeve_Color = s.Away_Sleeve_Color;
                        t.Away_Jersey_Number_Color = s.Away_Jersey_Number_Color;
                        t.Away_Jersey_Number_Outline_Color = s.Away_Jersey_Number_Outline_Color;
                        t.Away_Jersey_Shoulder_Stripe = s.Away_Jersey_Shoulder_Stripe;
                        t.Away_Jersey_Sleeve_Stripe_Color_1 = s.Away_Jersey_Sleeve_Stripe_Color_1;
                        t.Away_Jersey_Sleeve_Stripe_Color_2 = s.Away_Jersey_Sleeve_Stripe_Color_2;
                        t.Away_Jersey_Sleeve_Stripe_Color_3 = s.Away_Jersey_Sleeve_Stripe_Color_3;
                        t.Away_Jersey_Sleeve_Stripe_Color_4 = s.Away_Jersey_Sleeve_Stripe_Color_4;
                        t.Away_Jersey_Sleeve_Stripe_Color_5 = s.Away_Jersey_Sleeve_Stripe_Color_5;
                        t.Away_Jersey_Sleeve_Stripe_Color_6 = s.Away_Jersey_Sleeve_Stripe_Color_6;
                        t.Away_Pants_Color = s.Away_Pants_Color;
                        t.Away_Pants_Stripe_Color_1 = s.Away_Pants_Stripe_Color_1;
                        t.Away_Pants_Stripe_Color_2 = s.Away_Pants_Stripe_Color_2;
                        t.Away_Pants_Stripe_Color_3 = s.Away_Pants_Stripe_Color_3;
            */
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
