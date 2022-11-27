using SpectatorFootball.Enum;
using SpectatorFootball.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.GameNS
{
    public class Game_Player
    {
        public Player_Pos Pos;
        public double Starting_YardLine;
        public int Starting_Vertical_Percent_Pos;
        public double Current_YardLine;
        public int Current_Vertical_Percent_Pos;
        public Player_States State;
        public Player_and_Ratings p_and_r;
        public bool bCarryingBall;
        public List<Play_Stage> Stages = new List<Play_Stage>();
    }
}
