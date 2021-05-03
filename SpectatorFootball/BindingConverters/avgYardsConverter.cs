using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SpectatorFootball.BindingConverters
{
    class avgYardsConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string yard_avg = null;

            double d = ((double)values[1] / (double)values[0]) * 100.0;
            yard_avg = String.Format("{0:0.0}", d);

            return yard_avg;
        }


        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            string[] splitValues = ((string)value).Split(' ');
            return splitValues;
        }

    }
}
