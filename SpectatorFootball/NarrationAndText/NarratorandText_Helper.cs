using SpectatorFootball.GameNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpectatorFootball.Models;

namespace SpectatorFootball.NarrationAndText
{
    public class NarratorandText_Helper
    {
        public static string getShortPlayName_from_Game_Player(Game_Player gp)
        {
            string r = "";
            r = gp.p_and_r.p.First_Name.Substring(0, 1) + " ";
            r += gp.p_and_r.p.Last_Name;

            return r;
        }
    }
}
