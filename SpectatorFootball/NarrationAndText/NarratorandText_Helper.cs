using SpectatorFootball.GameNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpectatorFootball.Models;
using SpectatorFootball.Enum;

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

        public static Tuple<double, Game_Sounds, string> getGameEffects(AE_rec effects)
        {
            double crowd_adj = 0.0;
            Game_Sounds gsound = Game_Sounds.NONE;
            string announcement = null;

            crowd_adj = effects.noise_adj;
            gsound = effects.Sound;
            announcement = effects.Announcer_msg == null ? "" : effects.Announcer_msg;


            return Tuple.Create(crowd_adj, gsound, announcement);
        }
    }
}
