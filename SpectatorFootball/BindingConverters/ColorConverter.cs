using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace SpectatorFootball.BindingConverters
{
    class ColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            object r = value;

            if (value == null )
            {
                r = app_Constants.STOCK_GREY_COLOR;
            }

            return r;

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            object r = value;

            if (r == null)
            {
                r = "";
            }
 
            return r;
        }

    }
}
