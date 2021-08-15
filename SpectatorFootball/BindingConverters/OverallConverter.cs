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
    class OverallConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            string r = "";

            if (value != null)
            {
                Player_Ratings pr = (Player_Ratings)value;
                double d = Player_Helper.Create_Overall_Rating((Player_Pos)pr.Player.Pos, pr);
                r = String.Format("{0:0.00}", d);
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
