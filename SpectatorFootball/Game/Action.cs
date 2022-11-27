using SpectatorFootball.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.GameNS
{
    public class Action
    {
        public Game_Object_Types type;
        public double end_yardline;
        public double end_vertical;
        public bool Move; //Y or N
        public bool bPossesses_Ball;
        public Player_States? p_state;
        public Ball_States? b_state;
        public Game_Sounds? Before_Sound;
        public Game_Sounds? After_Sound;

    }
}
