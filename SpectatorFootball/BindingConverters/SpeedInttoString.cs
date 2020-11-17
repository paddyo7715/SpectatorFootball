using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SpectatorFootball.BindingConverters
{
    class SpeedInttoString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            string r = "";

            if (value != null)
            {
                int iSpeed = int.Parse(value.ToString());
                switch(iSpeed)
                {
                    case 0:
                        r = "Very Slow";
                        break;
                    case 1:
                        r = "Slow";
                        break;
                    case 2:
                        r = "Normal";
                            break;
                    case 3:
                        r = "Fast";
                        break;
                    case 4:
                        r = "Very Fast";
                        break;
                }
            }

            return r;

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            int r = 2;

            return r;

        }
    }
}

