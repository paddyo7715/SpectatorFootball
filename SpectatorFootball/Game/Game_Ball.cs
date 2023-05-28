using SpectatorFootball.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpectatorFootball.Common;

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
            Action bas = new Action(Game_Object_Types.B, Current_YardLine, Current_Vertical_Percent_Pos, 0.0, 0.0, false, false, null, Ball_States.TEED_UP, null, Movement.NONE, null, false);
            Play_Stage bStage = new Play_Stage();
            bStage.Main_Object = false;
            bStage.Actions.Add(bas);
            Stages.Add(bStage);

        }
        public void End_Over_End_Thru_Air()
        {
            State = Ball_States.END_OVER_END;
            Action bas = new Action(Game_Object_Types.B, Starting_YardLine, Starting_Vertical_Percent_Pos, Current_YardLine, Current_Vertical_Percent_Pos, false, true, null, Ball_States.END_OVER_END, null, Movement.LINE, Ball_Speed.SLOW, false);
            Play_Stage bStage = new Play_Stage();
            bStage.Main_Object = true;
            bStage.Actions.Add(bas);
            Stages.Add(bStage);
        }

        public void End_Over_End_Thru_Air_Not_Caught()
        {
            const double BOUNCE_LENGTH = 2;
            const double ROLL_LENGTH = 2;

            double prev_yardline = Starting_YardLine;
            double prev_vert = Starting_Vertical_Percent_Pos;

            PointXY new_end_point = null; 

            State = Ball_States.END_OVER_END;
            Action bas = new Action(Game_Object_Types.B, prev_yardline, prev_vert, Current_YardLine, Current_Vertical_Percent_Pos, false, false, null, Ball_States.END_OVER_END, null, Movement.LINE, Ball_Speed.SLOW, false);

            //Get the end point for bouncing ball
            new_end_point = PointPlotter.getExtendedEndpoint(prev_yardline, prev_vert, Current_YardLine, Current_Vertical_Percent_Pos, BOUNCE_LENGTH);

            prev_yardline = Current_YardLine;
            prev_vert = Current_Vertical_Percent_Pos;

            State = Ball_States.BOUNCING;
            Action bas2 = new Action(Game_Object_Types.B, prev_yardline, prev_vert, new_end_point.x, new_end_point.y, false, false, null, Ball_States.BOUNCING, null, Movement.LINE, Ball_Speed.SLOW, false);

            prev_yardline = new_end_point.x;
            prev_vert = new_end_point.y;

            //Get the end point for rolling ball
            new_end_point = PointPlotter.getExtendedEndpoint(prev_yardline, prev_vert, Current_YardLine, Current_Vertical_Percent_Pos, ROLL_LENGTH);


            State = Ball_States.ROLLING;
            Action bas3 = new Action(Game_Object_Types.B, prev_yardline, prev_vert, new_end_point.x, new_end_point.y, false, true, null, Ball_States.ROLLING, null, Movement.LINE, Ball_Speed.SLOW, false);

            Play_Stage bStage = new Play_Stage();
            bStage.Main_Object = true;
            bStage.Actions.Add(bas);
            bStage.Actions.Add(bas2);
            bStage.Actions.Add(bas3);
            Stages.Add(bStage);
        }
        public void Carried(double prev_yl, double prev_v)
        {
            Action bas = new Action(Game_Object_Types.B, prev_yl, prev_v, Current_YardLine, Current_Vertical_Percent_Pos, true, false, null, Ball_States.CARRIED, null, Movement.LINE, Ball_Speed.SLOW, false);
            Play_Stage bStage = new Play_Stage();
            bStage.Main_Object = true;
            bStage.Actions.Add(bas);
            Stages.Add(bStage);
        }

        public void Carried_Fake_Movement(double prev_yl, double prev_v)
        {
            Action bas = new Action(Game_Object_Types.B, prev_yl, prev_v, Current_YardLine, Current_Vertical_Percent_Pos, true, false, null, Ball_States.CARRIED, null, Movement.FAKE_MOVEMENT, Ball_Speed.SLOW, false);
            Play_Stage bStage = new Play_Stage();
            bStage.Main_Object = true;
            bStage.Actions.Add(bas);
            Stages.Add(bStage);
        }
    }
}