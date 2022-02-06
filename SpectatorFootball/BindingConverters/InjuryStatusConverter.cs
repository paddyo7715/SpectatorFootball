using SpectatorFootball.Enum;
using SpectatorFootball.Models;
using SpectatorFootball.PlayerNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SpectatorFootball.BindingConverters
{
    public class InjuryStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            string r = "";

            if (value != null)
            {
                Injury iv = (Injury)value;
                if (iv.Season_Ending == 1)
                    r = "He will be out for the season.";
                else if (iv.Career_Ending == 1)
                    r = "Career ending injury, he will never play again.";
                else
                {
                    string injured_week = iv.Week < app_Constants.PLAYOFF_WIDLCARD_WEEK_1 ? "Week " + iv.Week.ToString() : "the Playoffs";
                    r = "Injured in " + injured_week + " for " + iv.Num_of_Weeks + " weeks";
                }
            }

            return r;

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            int r = 0;

            return r;

        }
    }
}

