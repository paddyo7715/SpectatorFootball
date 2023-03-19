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
        public double Starting_Vertical_Percent_Pos;
        public double Current_YardLine;
        public double Current_Vertical_Percent_Pos;
        public Player_States State;
        public Player_States Initial_State;
        public Player_and_Ratings p_and_r;
        public bool bCarryingBall;
        public List<Play_Stage> Stages = new List<Play_Stage>();

        public void Stand()
        {
            Action pas = new Action(Game_Object_Types.P, Starting_YardLine, Starting_Vertical_Percent_Pos, 0.0, 0.0, false, false, Player_States.STANDING, null, null, Movement.NONE, null);
            Play_Stage pStage = new Play_Stage();
            pStage.Main_Object = false;
            pStage.Actions.Add(pas);
            Stages.Add(pStage);
        }

        public void Same_As_Last_Action()
        {
            //This method will take the last stage and create a new stage with the
            //each same actions.
            if (Stages.Count() == 0)
                throw new Exception("Same_As_Last_Action called on a player with 0 stages!");

            Play_Stage ps = Stages.Last();

            Play_Stage new_st = new Play_Stage()
            {
                Main_Object = ps.Main_Object,
                Player_Catches_Ball = ps.Player_Catches_Ball
            };

            Action a = ps.Actions.Last();

            Action new_action = new Action(a.type, a.start_yardline, a.start_vertical,
            a.end_yardline, a.end_vertical, a.bPossesses_Ball, a.bEndofStageAction,
            a.p_state, a.b_state, a.Sound, a.MoveType, a.Ball_Speed);
            new_st.Actions.Add(new_action);

            Stages.Add(new_st);
        }
    }


}
