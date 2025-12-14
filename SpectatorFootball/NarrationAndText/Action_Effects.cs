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
        public double Before_noise_adj = app_Constants.OFF_NEURTRAL;
        public double After_noise_adj = app_Constants.OFF_NEURTRAL;

        public Game_Sounds Before_Sound = Game_Sounds.NONE;
        public Game_Sounds After_Sound = Game_Sounds.NONE;

        public string Start_Announcer_msg = null;
        public string End_Announcer_msg = null;
    }
}
