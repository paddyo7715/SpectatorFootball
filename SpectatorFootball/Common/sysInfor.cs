using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.Common
{
    public class sysInfor
    {
        public static Tuple<int, int> getScreenResolution()
        {
            //Get the screen resolution of the primary monitor
            int Screen_Res_Width = (int)System.Windows.SystemParameters.PrimaryScreenWidth;
            int Screen_Res_Height = (int)System.Windows.SystemParameters.PrimaryScreenHeight;

            Tuple<int, int> Screen_Res_ = new Tuple<int, int>(Screen_Res_Width, Screen_Res_Height);
            return Screen_Res_;
        }
    }
}
