using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SpectatorFootball.BindingConverters
{
    class DoubleConverter1d : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            string r = "";

            if (value != null)
            {
                r = String.Format("{0:0.0}", value);
            }

            return r;

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            string r = "";

            return r;

        }
    }
}

