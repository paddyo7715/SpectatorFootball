using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SpectatorFootball.BindingConverters
{
    class StatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            string r = "";

            if (value != null)
            {
                int iStatus = (int)value;
                switch (iStatus)
                {
                    case 1:
                        r = "Not Started";
                        break;
                    case 2:
                        r = "In Progress";
                        break;
                    case 3:
                        r = "Completed";
                        break;
                    default:
                        r = "Unknown";
                        break;
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


