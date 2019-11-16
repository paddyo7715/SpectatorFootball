using System;
using System.Collections.Generic;

namespace SpectatorFootball
{
    public class Uniform_Color_percents
    {
        public string color_string { get; set; }
        public float value { get; set; }

        public Uniform_Color_percents(string color_string, float axis_start)
        {
            this.color_string = color_string;
            value = axis_start;
        }
    }

    public class Uniform
    {
        public static List<Uniform_Color_percents> getColorList(UniformMdl u)
        {
            List<Uniform_Color_percents> r = new List<Uniform_Color_percents>();

            string letter_color = u.Home_Jersey.Number_Color;

            Uniform_Color_percents letter_uni_percent = new Uniform_Color_percents(letter_color, Convert.ToSingle(0.0));
            r.Add(letter_uni_percent);

            r.Add(new Uniform_Color_percents(u.Home_Jersey.Jersey_Color, Convert.ToSingle(40.0)));

            if (u.Helmet.Helmet_Color != letter_color)
                addUpdate_Uniform(r, new Uniform_Color_percents(u.Helmet.Helmet_Color, Convert.ToSingle(25.0)));

            if (u.Home_Pants.Pants_Color != letter_color)
                addUpdate_Uniform(r, new Uniform_Color_percents(u.Home_Pants.Pants_Color, Convert.ToSingle(50.0)));

            if (u.Home_Jersey.Sleeve_Color != letter_color)
                addUpdate_Uniform(r, new Uniform_Color_percents(u.Home_Jersey.Sleeve_Color, Convert.ToSingle(10.0)));

            if (u.Home_Jersey.Shoulder_Stripe_Color != letter_color)
                addUpdate_Uniform(r, new Uniform_Color_percents(u.Home_Jersey.Shoulder_Stripe_Color, Convert.ToSingle(2.0)));

            if (u.Home_Jersey.Sleeve_Stripe1 != letter_color)
                addUpdate_Uniform(r, new Uniform_Color_percents(u.Home_Jersey.Sleeve_Stripe1, Convert.ToSingle(1.0)));

            if (u.Home_Jersey.Sleeve_Stripe2 != letter_color)
                addUpdate_Uniform(r, new Uniform_Color_percents(u.Home_Jersey.Sleeve_Stripe2, Convert.ToSingle(1.0)));

            if (u.Home_Jersey.Sleeve_Stripe3 != letter_color)
                addUpdate_Uniform(r, new Uniform_Color_percents(u.Home_Jersey.Sleeve_Stripe3, Convert.ToSingle(1.0)));

            if (u.Home_Jersey.Sleeve_Stripe4 !=letter_color)
                addUpdate_Uniform(r, new Uniform_Color_percents(u.Home_Jersey.Sleeve_Stripe4, Convert.ToSingle(1.0)));

            if (u.Home_Jersey.Sleeve_Stripe5 != letter_color)
                addUpdate_Uniform(r, new Uniform_Color_percents(u.Home_Jersey.Sleeve_Stripe5, Convert.ToSingle(1.0)));

            if (u.Home_Jersey.Sleeve_Stripe6 != letter_color)
                addUpdate_Uniform(r, new Uniform_Color_percents(u.Home_Jersey.Sleeve_Stripe6, Convert.ToSingle(1.0)));

            if (u.Home_Pants.Stripe_Color_1 != letter_color)
                addUpdate_Uniform(r, new Uniform_Color_percents(u.Home_Pants.Stripe_Color_1, Convert.ToSingle(1.0)));

            if (u.Home_Pants.Stripe_Color_2 != letter_color)
                addUpdate_Uniform(r, new Uniform_Color_percents(u.Home_Pants.Stripe_Color_2, Convert.ToSingle(1.0)));

            if (u.Home_Pants.Stripe_Color_3 != letter_color)
                addUpdate_Uniform(r, new Uniform_Color_percents(u.Home_Pants.Stripe_Color_3, Convert.ToSingle(1.0)));

            float tot_values = default(float);
            foreach (Uniform_Color_percents w in r)
                tot_values += w.value;

            bool bFirst = true;
            float running_value = Convert.ToSingle(0.0);
            for (int i = 1; i <= r.Count - 1; i++)
            {
                if (bFirst)
                {
                    running_value = Convert.ToSingle(0.0);
                    bFirst = false;
                }

                running_value += r[i].value;
                r[i].value = running_value / tot_values;
            }

            return r;
        }

        public static List<string> getAllColorList(UniformMdl u)
        {
            List<string> r = new List<string>();

            string helmet_color = u.Helmet.Helmet_Color;
            r.Add(helmet_color);

            string helmet_logo_color = u.Helmet.Helmet_Logo_Color;
            if (!r.Contains(helmet_logo_color))
                r.Add(helmet_logo_color);

            string helmet_facemask_color = u.Helmet.Helmet_Facemask_Color;
            if (!r.Contains(helmet_facemask_color))
                r.Add(helmet_facemask_color);

            string home_jersey_color = u.Home_Jersey.Jersey_Color;
            if (!r.Contains(home_jersey_color))
                r.Add(home_jersey_color);

            string home_Sleeve_Color = u.Home_Jersey.Sleeve_Color;
            if (!r.Contains(home_Sleeve_Color))
                r.Add(home_Sleeve_Color);

            string home_Shoulder_Stripe_Color = u.Home_Jersey.Shoulder_Stripe_Color;
            if (!r.Contains(home_Shoulder_Stripe_Color))
                r.Add(home_Shoulder_Stripe_Color);

            string home_Number_Color = u.Home_Jersey.Number_Color;
            if (!r.Contains(home_Number_Color))
                r.Add(home_Number_Color);

            string home_Number_Outline_Color = u.Home_Jersey.Number_Outline_Color;
            if (!r.Contains(home_Number_Outline_Color))
                r.Add(home_Number_Outline_Color);

            string home_Sleeve_Stripe1 = u.Home_Jersey.Sleeve_Stripe1;
            if (!r.Contains(home_Sleeve_Stripe1))
                r.Add(home_Sleeve_Stripe1);

            string home_Sleeve_Stripe2 = u.Home_Jersey.Sleeve_Stripe2;
            if (!r.Contains(home_Sleeve_Stripe2))
                r.Add(home_Sleeve_Stripe2);

            string home_Sleeve_Stripe3 = u.Home_Jersey.Sleeve_Stripe3;
            if (!r.Contains(home_Sleeve_Stripe3))
                r.Add(home_Sleeve_Stripe3);

            string home_Sleeve_Stripe4 = u.Home_Jersey.Sleeve_Stripe4;
            if (!r.Contains(home_Sleeve_Stripe4))
                r.Add(home_Sleeve_Stripe4);

            string home_Sleeve_Stripe5 = u.Home_Jersey.Sleeve_Stripe5;
            if (!r.Contains(home_Sleeve_Stripe5))
                r.Add(home_Sleeve_Stripe5);

            string home_Sleeve_Stripe6 = u.Home_Jersey.Sleeve_Stripe6;
            if (!r.Contains(home_Sleeve_Stripe6))
                r.Add(home_Sleeve_Stripe6);

            string home_Pantshome_Color = u.Home_Pants.Pants_Color;
            if (!r.Contains(home_Pantshome_Color))
                r.Add(home_Pantshome_Color);

            string home_Stripe_Color_1 = u.Home_Pants.Stripe_Color_1;
            if (!r.Contains(home_Stripe_Color_1))
                r.Add(home_Stripe_Color_1);

            string home_Stripe_Color_2 = u.Home_Pants.Stripe_Color_2;
            if (!r.Contains(home_Stripe_Color_2))
                r.Add(home_Stripe_Color_2);

            string home_Stripe_Color_3 = u.Home_Pants.Stripe_Color_3;
            if (!r.Contains(home_Stripe_Color_3))
                r.Add(home_Stripe_Color_3);

            string away_jersey_color = u.Away_Jersey.Jersey_Color;
            if (!r.Contains(away_jersey_color))
                r.Add(away_jersey_color);

            string away_Sleeve_Color = u.Away_Jersey.Sleeve_Color;
            if (!r.Contains(away_Sleeve_Color))
                r.Add(away_Sleeve_Color);

            string away_Shoulder_Stripe_Color = u.Away_Jersey.Shoulder_Stripe_Color;
            if (!r.Contains(away_Shoulder_Stripe_Color))
                r.Add(away_Shoulder_Stripe_Color);

            string away_Number_Color = u.Away_Jersey.Number_Color;
            if (!r.Contains(away_Number_Color))
                r.Add(away_Number_Color);

            string away_Number_Outline_Color = u.Away_Jersey.Number_Outline_Color;
            if (!r.Contains(away_Number_Outline_Color))
                r.Add(away_Number_Outline_Color);

            string away_Sleeve_Stripe1 = u.Away_Jersey.Sleeve_Stripe1;
            if (!r.Contains(away_Sleeve_Stripe1))
                r.Add(away_Sleeve_Stripe1);

            string away_Sleeve_Stripe2 = u.Away_Jersey.Sleeve_Stripe2;
            if (!r.Contains(away_Sleeve_Stripe2))
                r.Add(away_Sleeve_Stripe2);

            string away_Sleeve_Stripe3 = u.Away_Jersey.Sleeve_Stripe3;
            if (!r.Contains(away_Sleeve_Stripe3))
                r.Add(away_Sleeve_Stripe3);

            string away_Sleeve_Stripe4 = u.Away_Jersey.Sleeve_Stripe4;
            if (!r.Contains(away_Sleeve_Stripe4))
                r.Add(away_Sleeve_Stripe4);

            string away_Sleeve_Stripe5 = u.Away_Jersey.Sleeve_Stripe5;
            if (!r.Contains(away_Sleeve_Stripe5))
                r.Add(away_Sleeve_Stripe5);

            string away_Sleeve_Stripe6 = u.Away_Jersey.Sleeve_Stripe6;
            if (!r.Contains(away_Sleeve_Stripe6))
                r.Add(away_Sleeve_Stripe6);

            string away_Pants_Color = u.Away_Pants.Pants_Color;
            if (!r.Contains(away_Pants_Color))
                r.Add(away_Pants_Color);

            string away_Stripe_Color_1 = u.Away_Pants.Stripe_Color_1;
            if (!r.Contains(away_Stripe_Color_1))
                r.Add(away_Stripe_Color_1);

            string away_Stripe_Color_2 = u.Away_Pants.Stripe_Color_2;
            if (!r.Contains(away_Stripe_Color_2))
                r.Add(away_Stripe_Color_2);

            string away_Stripe_Color_3 = u.Away_Pants.Stripe_Color_3;
            if (!r.Contains(away_Stripe_Color_3))
                r.Add(away_Stripe_Color_3);

            string sock_color = u.Footwear.Socks_Color;
            if (!r.Contains(sock_color))
                r.Add(sock_color);

            string cleat_color = u.Footwear.Cleats_Color;
            if (!r.Contains(cleat_color))
                r.Add(cleat_color);

            return r;
        }



        public static void addUpdate_Uniform(List<Uniform_Color_percents> lup_l, Uniform_Color_percents lup)
        {
            bool bfound = false;

            foreach (Uniform_Color_percents p in lup_l)
            {
                if (lup.color_string == p.color_string)
                {
                    p.value += lup.value;
                    bfound = true;
                    break;
                }
            }

            if (!bfound)
                lup_l.Add(lup);
        }
    }
}
