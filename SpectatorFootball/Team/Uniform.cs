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
        public static List<Uniform_Color_percents> getColorList(string Home_Jersey_Number_Color,string Home_Jersey_Jersey_Color,
            string Helmet_Color,string Home_Pants_Color,string Home_Jersey_Sleeve_Color,
            string Home_Jersey_Shoulder_Stripe_Color, string Home_Jersey_Sleeve_Stripe1,
            string Home_Jersey_Sleeve_Stripe2, string Home_Jersey_Sleeve_Stripe3,
            string Home_Jersey_Sleeve_Stripe4, string Home_Jersey_Sleeve_Stripe5,
            string Home_Jersey_Sleeve_Stripe6, string Home_Pants_Stripe_Color_1,
            string Home_Pants_Stripe_Color_2, string Home_Pants_Stripe_Color_3)
        {
            List<Uniform_Color_percents> r = new List<Uniform_Color_percents>();

            string letter_color = Home_Jersey_Number_Color;

            Uniform_Color_percents letter_uni_percent = new Uniform_Color_percents(letter_color, Convert.ToSingle(0.0));
            r.Add(letter_uni_percent);

            r.Add(new Uniform_Color_percents(Home_Jersey_Jersey_Color, Convert.ToSingle(40.0)));

            if (Helmet_Color != letter_color)
                addUpdate_Uniform(r, new Uniform_Color_percents(Helmet_Color, Convert.ToSingle(25.0)));

            if (Home_Pants_Color != letter_color)
                addUpdate_Uniform(r, new Uniform_Color_percents(Home_Pants_Color, Convert.ToSingle(50.0)));

            if (Home_Jersey_Sleeve_Color != letter_color)
                addUpdate_Uniform(r, new Uniform_Color_percents(Home_Jersey_Sleeve_Color, Convert.ToSingle(10.0)));

            if (Home_Jersey_Shoulder_Stripe_Color != letter_color)
                addUpdate_Uniform(r, new Uniform_Color_percents(Home_Jersey_Shoulder_Stripe_Color, Convert.ToSingle(2.0)));

            if (Home_Jersey_Sleeve_Stripe1 != letter_color)
                addUpdate_Uniform(r, new Uniform_Color_percents(Home_Jersey_Sleeve_Stripe1, Convert.ToSingle(1.0)));

            if (Home_Jersey_Sleeve_Stripe2 != letter_color)
                addUpdate_Uniform(r, new Uniform_Color_percents(Home_Jersey_Sleeve_Stripe2, Convert.ToSingle(1.0)));

            if (Home_Jersey_Sleeve_Stripe3 != letter_color)
                addUpdate_Uniform(r, new Uniform_Color_percents(Home_Jersey_Sleeve_Stripe3, Convert.ToSingle(1.0)));

            if (Home_Jersey_Sleeve_Stripe4 != letter_color)
                addUpdate_Uniform(r, new Uniform_Color_percents(Home_Jersey_Sleeve_Stripe4, Convert.ToSingle(1.0)));

            if (Home_Jersey_Sleeve_Stripe5 != letter_color)
                addUpdate_Uniform(r, new Uniform_Color_percents(Home_Jersey_Sleeve_Stripe5, Convert.ToSingle(1.0)));

            if (Home_Jersey_Sleeve_Stripe6 != letter_color)
                addUpdate_Uniform(r, new Uniform_Color_percents(Home_Jersey_Sleeve_Stripe6, Convert.ToSingle(1.0)));

            if (Home_Pants_Stripe_Color_1 != letter_color)
                addUpdate_Uniform(r, new Uniform_Color_percents(Home_Pants_Stripe_Color_1, Convert.ToSingle(1.0)));

            if (Home_Pants_Stripe_Color_2 != letter_color)
                addUpdate_Uniform(r, new Uniform_Color_percents(Home_Pants_Stripe_Color_2, Convert.ToSingle(1.0)));

            if (Home_Pants_Stripe_Color_3 != letter_color)
                addUpdate_Uniform(r, new Uniform_Color_percents(Home_Pants_Stripe_Color_3, Convert.ToSingle(1.0)));

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

       public static List<string> getAllColorList(
                    string Helmet_Color,
        string Helmet_Logo_Color,
        string Helmet_Facemask_Color,
        string Socks_Color,
        string Cleats_Color,
        string Home_jersey_Color,
        string Home_Sleeve_Color,
        string Home_Jersey_Number_Color,
        string Home_Jersey_Number_Outline_Color,
        string Home_Jersey_Shoulder_Stripe,
        string Home_Jersey_Sleeve_Stripe_Color_1,
        string Home_Jersey_Sleeve_Stripe_Color_2,
        string Home_Jersey_Sleeve_Stripe_Color_3,
        string Home_Jersey_Sleeve_Stripe_Color_4,
        string Home_Jersey_Sleeve_Stripe_Color_5,
        string Home_Jersey_Sleeve_Stripe_Color_6,
        string Home_Pants_Color,
        string Home_Pants_Stripe_Color_1,
        string Home_Pants_Stripe_Color_2,
        string Home_Pants_Stripe_Color_3,
        string Away_jersey_Color,
        string Away_Sleeve_Color,
        string Away_Jersey_Number_Color,
        string Away_Jersey_Number_Outline_Color,
        string Away_Jersey_Shoulder_Stripe,
        string Away_Jersey_Sleeve_Stripe_Color_1,
        string Away_Jersey_Sleeve_Stripe_Color_2,
        string Away_Jersey_Sleeve_Stripe_Color_3,
        string Away_Jersey_Sleeve_Stripe_Color_4,
        string Away_Jersey_Sleeve_Stripe_Color_5,
        string Away_Jersey_Sleeve_Stripe_Color_6,
        string Away_Pants_Color,
        string Away_Pants_Stripe_Color_1,
        string Away_Pants_Stripe_Color_2,
        string Away_Pants_Stripe_Color_3)
        {
            List<string> r = new List<string>();

            string helmet_color = Helmet_Color;
            r.Add(helmet_color);

            string helmet_logo_color = Helmet_Logo_Color;
            if (!r.Contains(helmet_logo_color))
                r.Add(helmet_logo_color);

            string helmet_facemask_color = Helmet_Facemask_Color;
            if (!r.Contains(helmet_facemask_color))
                r.Add(helmet_facemask_color);

            string home_jersey_color = Home_jersey_Color;
            if (!r.Contains(home_jersey_color))
                r.Add(home_jersey_color);

            string home_Sleeve_Color = Home_Sleeve_Color;
            if (!r.Contains(home_Sleeve_Color))
                r.Add(home_Sleeve_Color);

            string home_Shoulder_Stripe_Color = Home_Jersey_Shoulder_Stripe;
            if (!r.Contains(home_Shoulder_Stripe_Color))
                r.Add(home_Shoulder_Stripe_Color);

            string home_Number_Color = Home_Jersey_Number_Color;
            if (!r.Contains(home_Number_Color))
                r.Add(home_Number_Color);

            string home_Number_Outline_Color = Home_Jersey_Number_Outline_Color;
            if (!r.Contains(home_Number_Outline_Color))
                r.Add(home_Number_Outline_Color);

            string home_Sleeve_Stripe1 = Home_Jersey_Sleeve_Stripe_Color_1;
            if (!r.Contains(home_Sleeve_Stripe1))
                r.Add(home_Sleeve_Stripe1);

            string home_Sleeve_Stripe2 = Home_Jersey_Sleeve_Stripe_Color_2;
            if (!r.Contains(home_Sleeve_Stripe2))
                r.Add(home_Sleeve_Stripe2);

            string home_Sleeve_Stripe3 = Home_Jersey_Sleeve_Stripe_Color_3;
            if (!r.Contains(home_Sleeve_Stripe3))
                r.Add(home_Sleeve_Stripe3);

            string home_Sleeve_Stripe4 = Home_Jersey_Sleeve_Stripe_Color_4;
            if (!r.Contains(home_Sleeve_Stripe4))
                r.Add(home_Sleeve_Stripe4);

            string home_Sleeve_Stripe5 = Home_Jersey_Sleeve_Stripe_Color_5;
            if (!r.Contains(home_Sleeve_Stripe5))
                r.Add(home_Sleeve_Stripe5);

            string home_Sleeve_Stripe6 = Home_Jersey_Sleeve_Stripe_Color_6;
            if (!r.Contains(home_Sleeve_Stripe6))
                r.Add(home_Sleeve_Stripe6);

            string home_Pantshome_Color = Home_Pants_Color;
            if (!r.Contains(home_Pantshome_Color))
                r.Add(home_Pantshome_Color);

            string home_Stripe_Color_1 = Home_Pants_Stripe_Color_1;
            if (!r.Contains(home_Stripe_Color_1))
                r.Add(home_Stripe_Color_1);

            string home_Stripe_Color_2 = Home_Pants_Stripe_Color_2;
            if (!r.Contains(home_Stripe_Color_2))
                r.Add(home_Stripe_Color_2);

            string home_Stripe_Color_3 = Home_Pants_Stripe_Color_3;
            if (!r.Contains(home_Stripe_Color_3))
                r.Add(home_Stripe_Color_3);

            string away_jersey_color = Away_jersey_Color;
            if (!r.Contains(away_jersey_color))
                r.Add(away_jersey_color);

            string away_Sleeve_Color = Away_Sleeve_Color;
            if (!r.Contains(away_Sleeve_Color))
                r.Add(away_Sleeve_Color);

            string away_Shoulder_Stripe_Color = Away_Jersey_Shoulder_Stripe;
            if (!r.Contains(away_Shoulder_Stripe_Color))
                r.Add(away_Shoulder_Stripe_Color);

            string away_Number_Color = Away_Jersey_Number_Color;
            if (!r.Contains(away_Number_Color))
                r.Add(away_Number_Color);

            string away_Number_Outline_Color = Away_Jersey_Number_Outline_Color;
            if (!r.Contains(away_Number_Outline_Color))
                r.Add(away_Number_Outline_Color);

            string away_Sleeve_Stripe1 = Away_Jersey_Sleeve_Stripe_Color_1;
            if (!r.Contains(away_Sleeve_Stripe1))
                r.Add(away_Sleeve_Stripe1);

            string away_Sleeve_Stripe2 = Away_Jersey_Sleeve_Stripe_Color_2;
            if (!r.Contains(away_Sleeve_Stripe2))
                r.Add(away_Sleeve_Stripe2);

            string away_Sleeve_Stripe3 = Away_Jersey_Sleeve_Stripe_Color_3;
            if (!r.Contains(away_Sleeve_Stripe3))
                r.Add(away_Sleeve_Stripe3);

            string away_Sleeve_Stripe4 = Away_Jersey_Sleeve_Stripe_Color_4;
            if (!r.Contains(away_Sleeve_Stripe4))
                r.Add(away_Sleeve_Stripe4);

            string away_Sleeve_Stripe5 = Away_Jersey_Sleeve_Stripe_Color_5;
            if (!r.Contains(away_Sleeve_Stripe5))
                r.Add(away_Sleeve_Stripe5);

            string away_Sleeve_Stripe6 = Away_Jersey_Sleeve_Stripe_Color_6;
            if (!r.Contains(away_Sleeve_Stripe6))
                r.Add(away_Sleeve_Stripe6);

            string away_Pants_Color = Away_Pants_Color;
            if (!r.Contains(away_Pants_Color))
                r.Add(away_Pants_Color);

            string away_Stripe_Color_1 = Away_Pants_Stripe_Color_1;
            if (!r.Contains(away_Stripe_Color_1))
                r.Add(away_Stripe_Color_1);

            string away_Stripe_Color_2 = Away_Pants_Stripe_Color_2;
            if (!r.Contains(away_Stripe_Color_2))
                r.Add(away_Stripe_Color_2);

            string away_Stripe_Color_3 = Away_Pants_Stripe_Color_3;
            if (!r.Contains(away_Stripe_Color_3))
                r.Add(away_Stripe_Color_3);

            string sock_color = Socks_Color;
            if (!r.Contains(sock_color))
                r.Add(sock_color);

            string cleat_color = Cleats_Color;
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
        //This method returns the colors for a team in the following ordedr
        //  0 - background
        //  1 - foreground
        //  2 - tertiary color
        public static string[] getTeamDispColors(string homejersey, string homenumber, string homeoutline,
            string helmet, string helmet_logo)
        {
            string white = "#FFFFFF";
            string black = "#000000";
            string[] r = null;
            string foreground = null;
            string background = homejersey;

            r[0] = background;

            if (CommonUtils.isTwoColorDifferent(background, homenumber))
                foreground = homenumber;
            else if (CommonUtils.isTwoColorDifferent(background, homeoutline))
                foreground = homeoutline;
            else if (CommonUtils.isTwoColorDifferent(background, white))
                foreground = white;
            else
                foreground = black;

            r[1] = foreground;

            //sometimes we only need the first two colors, as in the box score popup, so don't even
            //bother trying to get the third color in that case.
            if (helmet != null)
            {
                r[2] = background;
                if (CommonUtils.isTwoColorDifferent(background, helmet_logo) ||
                    CommonUtils.isTwoColorDifferent(foreground, helmet_logo))
                    r[2] = helmet_logo;
                if (CommonUtils.isTwoColorDifferent(background, helmet) ||
                    CommonUtils.isTwoColorDifferent(foreground, helmet))
                    r[2] = helmet;
            }

            return r;
        }
    }
}
