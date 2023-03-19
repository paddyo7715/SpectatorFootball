using SpectatorFootball.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.GameNS
{
    public class Game_Ball
    {
        public double Starting_YardLine;
        public double Starting_Vertical_Percent_Pos;
        public double Current_YardLine;
        public double Current_Vertical_Percent_Pos;
        public Ball_States Initial_State;
        public Ball_States State;
        public List<Play_Stage> Stages = new List<Play_Stage>();

        //The ball is see for tee up at the specified locaiton
        public void TeeUp()
        {
            State = Ball_States.TEED_UP;
            Action bas = new Action(Game_Object_Types.B, Current_YardLine, Current_Vertical_Percent_Pos, 0.0, 0.0, false, false, null, Ball_States.TEED_UP, null, Movement.NONE, null);
            Play_Stage bStage = new Play_Stage();
            bStage.Main_Object = false;
            bStage.Actions.Add(bas);
            Stages.Add(bStage);

        }
        public void End_Over_End_Thru_Air()
        {
            State = Ball_States.END_OVER_END;
            Action bas = new Action(Game_Object_Types.B, Starting_YardLine, Starting_Vertical_Percent_Pos, Current_YardLine, Current_Vertical_Percent_Pos, false, true, null, Ball_States.END_OVER_END, null, Movement.LINE, Ball_Speed.SLOW);
            Play_Stage bStage = new Play_Stage();
            bStage.Main_Object = true;
            bStage.Actions.Add(bas);
            Stages.Add(bStage);
        }
    }
}