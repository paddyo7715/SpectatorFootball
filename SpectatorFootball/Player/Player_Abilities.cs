

namespace SpectatorFootball
{
    public class Player_Abilities
    {
        public float OverAll { get; set; }
        public int Accuracy_Rating { get; set; }
        public int Decision_Making { get; set; }
        public int Arm_Strength_Rating { get; set; }
        public int Pass_Block_Rating { get; set; }
        public int Run_Block_Rating { get; set; }
        public int Running_Power_Rating { get; set; }
        public int Speed_Rating { get; set; }
        public int Agilty_Rating { get; set; }
        public int Hands_Rating { get; set; }
        public int Pass_Attack { get; set; }
        public int Run_Attack { get; set; }
        public int Tackle_Rating { get; set; }
        public int Leg_Strength { get; set; }
        public int Kicking_Accuracy { get; set; }
        public int Fumble_Rating { get; set; }

        public Player_Abilities()
        {
            Accuracy_Rating = 0;
            Decision_Making = 0;
            Arm_Strength_Rating = 0;
            Pass_Block_Rating = 0;
            Run_Block_Rating = 0;
            Running_Power_Rating = 0;
            Speed_Rating = 0;
            Agilty_Rating = 0;
            Hands_Rating = 0;
            Pass_Attack = 0;
            Run_Attack = 0;
            Tackle_Rating = 0;
            Leg_Strength = 0;
            Kicking_Accuracy = 0;
            Fumble_Rating = 0;
        }
    }
}
