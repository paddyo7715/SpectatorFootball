﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.Models
{
    public class Pass_Defense_Accum_Stats
    {
        public Player p { get; set; }
        public long Pass_Defenses { get; set; }
        public long Ints { get; set; }
        public long TDs_Surrendered { get; set; }
        public long Forced_Fumble { get; set; }
    }
}
