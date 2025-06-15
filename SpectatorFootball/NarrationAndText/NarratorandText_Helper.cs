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

        public static Tuple<double, Game_Sounds, string> getGameEffects(bool bBefore, Action_Effects effects)
        {
            double crowd_adj = 0.0;
            string flash_msg = null;
            Game_Sounds gsound = Game_Sounds.NONE;

            if (bBefore)
            {
                crowd_adj = effects.Before_noise_adj;
                flash_msg = effects.before_flash;
                gsound = effects.Before_Sound;
            }
            else
            {
                crowd_adj = effects.After_noise_adj;
                flash_msg = effects.After_flash;
                gsound = effects.After_Sound;
            }

            return Tuple.Create(crowd_adj, gsound, flash_msg);
        }
    }
}
