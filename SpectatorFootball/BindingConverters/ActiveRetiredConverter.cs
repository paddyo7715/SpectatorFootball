using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SpectatorFootball.BindingConverters
{
    class ActiveRetiredConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            string r = "";

            if (value != null)
            {
                string Retired = value.ToString();
                if (Retired == "1")
                    r = "Retired";
                else
                    r = "Active";
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


