using System.Collections.Generic;

namespace SpectatorFootball
{
    public class PlayerMdl
    {
        public enum Position
        {
            QB,
            RB,
            WR,
            TE,
            OL,
            DL,
            LB,
            DB,
            K,
            P
        }


        public string First_Name { get; set; } = "";
        public string Last_Name { get; set; } = "";
        public int Age { get; set; } = 0;
        public int Jersey_Number { get; set; }
        public bool Active { get; set; }
        public Position Pos { get; set; }

        public Player_Abilities Ratings { get; set; }

        public StatsMdl Stats { get; set; } = null;

        public List<string> Awards { get; set; }
    }
}
