using SpectatorFootball.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SpectatorFootball.BindingConverters
{
    class DraftInfoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string r = null;

            if (value != null)
            {
                Draft d = (Draft)value;
                r = d.Season.Year + " Round " + d.Round.ToString() + " Pick # " + d.Pick_Number.ToString();
            }
            else
                r = "UNDRAFTED";

            return r;

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            int r = 0;

            return r;

        }
    }
}

