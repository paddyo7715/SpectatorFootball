using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.GameNS
{
    class Game_Helper
    {
        public static string getQTRTime(long? QTR, long? Time)
        {
            string r = "";

            switch (QTR)
            {
                case 1:
                    r = "1st  ";
                    break;
                case 2:
                    r = "2nd  ";
                    break;
                case 3:
                    r = "3rd  ";
                    break;
                case 4:
                    r = "4th  ";
                    break;
                default:
                    r = "OT   ";
                    break;
            }

            int min = (int)Time / 60;
            int sec = (int)Time % 60;
            string sec_string = sec < 10 ? "0" + sec.ToString() : sec.ToString();
            r += " " + min.ToString() + ":" + sec_string;


            return r;
        }
    }
}
