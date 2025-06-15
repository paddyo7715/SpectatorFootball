using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpectatorFootball.Enum;

namespace SpectatorFootball.NarrationAndText
{
    public class Action_Effects
    {
        public string before_flash = null;
        public string After_flash = null;

        public double Before_noise_adj = app_Constants.OFF_NEURTRAL;
        public double After_noise_adj = app_Constants.OFF_NEURTRAL;

        public Game_Sounds Before_Sound = Game_Sounds.NONE;
        public Game_Sounds After_Sound = Game_Sounds.NONE;
    }
}
