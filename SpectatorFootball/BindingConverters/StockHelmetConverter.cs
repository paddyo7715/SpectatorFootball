using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SpectatorFootball.BindingConverters
{
    class StockHelmetConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            string r = "";

            if (value != null && !CommonUtils.isBlank(value.ToString()))
            {
                string init_folder = CommonUtils.getAppPath();
                init_folder += Path.DirectorySeparatorChar + "Images" + Path.DirectorySeparatorChar + "Helmets";
                r = init_folder + Path.DirectorySeparatorChar + Path.GetFileName(value.ToString());
            }

            return r;

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            string r = "";
            if (value != null && !CommonUtils.isBlank(value.ToString()))
            {
                r = Path.GetFileName(value.ToString());
            }

            return r;

        }


    }
}
