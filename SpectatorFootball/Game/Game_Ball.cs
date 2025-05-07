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
            Action bas = new Action(Game_Object_Types.B, Current_YardLine, Current_Vertical_Percent_Pos, 0.0, 0.0, false, null, Ball_States.TEED_UP, Movement.NONE, null, false,0);
            Play_Stage bStage = new Play_Stage();
            bStage.Main_Object = false;
            bStage.Actions.Add(bas);
            Stages.Add(bStage);
        }
        public void Fake_Movement(int n)
        {
            Play_Stage bStage = new Play_Stage();
            bStage.Main_Object = true;
            Action bas = new Action(Game_Object_Types.B, Current_YardLine, Current_Vertical_Percent_Pos, Current_YardLine, Current_Vertical_Percent_Pos, true, null, State, Movement.FAKE_MOVEMENT, Ball_Speed.CARRIED, false, n);
            bStage.Actions.Add(bas);
            Stages.Add(bStage);
        }
        public void End_Over_End_Thru_Air_Caught()
        {
            State = Ball_States.END_OVER_END;
            Action bas = new Action(Game_Object_Types.B, Starting_YardLine, Starting_Vertical_Percent_Pos, Current_YardLine, Current_Vertical_Percent_Pos, false, null, Ball_States.END_OVER_END, Movement.LINE, Ball_Speed.SLOW, false,0);
            Play_Stage bStage = new Play_Stage();
            bStage.Main_Object = true;
            bStage.Actions.Add(bas);
            Stages.Add(bStage);
        }
        public void FG_Into_Stands(double prev_yl, double prev_v, bool blefttoRight)
        {
            State = Ball_States.END_OVER_END;
            Action bas = new Action(Game_Object_Types.B, prev_yl, prev_v, Current_YardLine, Current_Vertical_Percent_Pos, false, null, Ball_States.END_OVER_END, Movement.LINE, Ball_Speed.SLOW, false, 0);
            Action bas2 = new Action(Game_Object_Types.B, Current_YardLine, Current_Vertical_Percent_Pos, Current_YardLine, Current_Vertical_Percent_Pos, true, null, Ball_States.CARRIED, Movement.FAKE_MOVEMENT, Ball_Speed.CARRIED, false, 4);

            Play_Stage bStage = new Play_Stage();
            bStage.Main_Object = true;
            bStage.bBall_Over_Goalposts = true;
            bStage.Actions.Add(bas);
            bStage.Actions.Add(bas2);
            Stages.Add(bStage);
        }
        public void FG_Hits_GP_Into_Stands(double prev_yl, double prev_v, double end_yl, double end_v, bool blefttoRight)
        {
            State = Ball_States.END_OVER_END;
            Action bas = new Action(Game_Object_Types.B, prev_yl, prev_v, Current_YardLine, Current_Vertical_Percent_Pos, false, null, Ball_States.END_OVER_END, Movement.LINE, Ball_Speed.SLOW, false, 0);
            Action bas2 = new Action(Game_Object_Types.B, Current_YardLine, Current_Vertical_Percent_Pos, end_yl, end_v, false, null, Ball_States.END_OVER_END, Movement.LINE, Ball_Speed.SLOW, false, 0);
            Action bas3 = new Action(Game_Object_Types.B, end_yl, end_v, end_yl, end_v, true, null, Ball_States.CARRIED, Movement.FAKE_MOVEMENT, Ball_Speed.CARRIED, false, 4);

            bas.PointXY.AddRange(bas2.PointXY);

            Play_Stage bStage = new Play_Stage();
            bStage.Main_Object = true;
            bStage.bBall_Over_Goalposts = true;
            bStage.Actions.Add(bas);
            bStage.Actions.Add(bas3);
            Stages.Add(bStage);
        }
        public void FG_Hits_GP(double prev_yl, double prev_v, double end_yl, double end_v, bool blefttoRight)
        {
            State = Ball_States.END_OVER_END;
            Action bas = new Action(Game_Object_Types.B, prev_yl, prev_v, Current_YardLine, Current_Vertical_Percent_Pos, false, null, Ball_States.END_OVER_END, Movement.LINE, Ball_Speed.SLOW, false, 0);
            Action bas2 = new Action(Game_Object_Types.B, Current_YardLine, Current_Vertical_Percent_Pos, end_yl, end_v, false, null, Ball_States.END_OVER_END, Movement.LINE, Ball_Speed.SLOW, false, 0);

            Play_Stage bStage = new Play_Stage();
            List<Action> BR_list = getBounceRollActions(Current_YardLine, Current_Vertical_Percent_Pos, end_yl, end_v, blefttoRight);
            bStage.Actions.AddRange(BR_list);
            bStage.Main_Object = true;
            bStage.bBall_Over_Goalposts = true;
            bStage.Actions.Add(bas);
            bStage.Actions.Add(bas2);
            Stages.Add(bStage);
        }

        public List<Action> getBounceRollActions(double starting_yl, double ending_y, double landing_yl, double landing_v, bool blefttoRight)
        {
            List<Action> r = new List<Action>();
            double prev_yl = 0.0;
            double prev_v = 0.0;

            double new_yl = 0.0;
            double new_v = 0.0;

            double BOUNCE_LENGTH = 4.0;
            double ROLL_LENGTH = 4.0;

            double wall_yl = Game_Engine_Helper.getWall_yl(blefttoRight);
            PointXY new_end_point = null;

            //Get the end point for bouncing ball
            new_end_point = PointPlotter.getExtendedEndpoint(starting_yl, ending_y, landing_yl, landing_v, BOUNCE_LENGTH * Game_Engine_Helper.HorizontalAdj(blefttoRight));

            double x = Game_Engine_Helper.getBeyondWall(wall_yl, new_end_point.x, blefttoRight);

            if (blefttoRight && x > 0)
                new_end_point.x = wall_yl;
            else if (!blefttoRight && x > 0)
                new_end_point.x = wall_yl;

            prev_yl = landing_yl;
            prev_v = landing_v;

            new_yl = new_end_point.x;
            new_v = new_end_point.y;

            State = Ball_States.BOUNCING;

            r.Add(new Action(Game_Object_Types.B, prev_yl, prev_v, new_yl, new_v, false, null, Ball_States.BOUNCING, Movement.LINE, Ball_Speed.SLOW, false, 0));

            if (x  > 0)
            {
                blefttoRight = Game_Engine_Helper.Switch_LefttoRight(blefttoRight);
                wall_yl = Game_Engine_Helper.getWall_yl(blefttoRight);
                prev_yl = new_yl;
                prev_v = new_v;

                new_yl = prev_yl + (x * Game_Engine_Helper.HorizontalAdj(blefttoRight));
                new_v = new_end_point.y;

                if (new_v > 48.5 && new_v < 51.5)
                    ROLL_LENGTH = 1.0;

                r.Add(new Action(Game_Object_Types.B, prev_yl, prev_v, new_yl, new_v, false, null, Ball_States.BOUNCING, Movement.LINE, Ball_Speed.SLOW, false, 0));
            }

            //Get the end point for rolling ball
//                new_v = new_end_point.y;
            PointXY rolling_end_point = PointPlotter.getExtendedEndpoint(starting_yl, ending_y, new_yl, new_v, ROLL_LENGTH * Game_Engine_Helper.HorizontalAdj(blefttoRight));

            x = Game_Engine_Helper.getBeyondWall(wall_yl, rolling_end_point.x, blefttoRight);

            if (blefttoRight && x > 0)
                rolling_end_point.x = wall_yl;
            else if (!blefttoRight && x > 0)
                rolling_end_point.x = wall_yl;

            prev_yl = new_yl;
            prev_v = new_v;

            new_yl = rolling_end_point.x;
            new_v = rolling_end_point.y;

            State = Ball_States.ROLLING;
            r.Add(new Action(Game_Object_Types.B, prev_yl, prev_v, new_yl, new_v, false, null, Ball_States.ROLLING, Movement.LINE, Ball_Speed.SLOW, false, 0));

            if (x > 0)
            {
                blefttoRight = Game_Engine_Helper.Switch_LefttoRight(blefttoRight);
                wall_yl = Game_Engine_Helper.getWall_yl(blefttoRight);
                prev_yl = new_yl;
                prev_v = new_v;

                new_yl = prev_yl + (x * Game_Engine_Helper.HorizontalAdj(blefttoRight));
                new_v = new_end_point.y;

                if (new_v > 48.5 && new_v < 51.5)
                    BOUNCE_LENGTH = 1.0;

                r.Add(new Action(Game_Object_Types.B, prev_yl, prev_v, new_yl, new_v, false, null, Ball_States.ROLLING, Movement.LINE, Ball_Speed.SLOW, false, 0));
            }

            return r;
        }

        public void FG_Short(double prev_yardline, double prev_vert, bool blefttoRight)
        {
            State = Ball_States.END_OVER_END;
            Action bas = new Action(Game_Object_Types.B, prev_yardline, prev_vert, Current_YardLine, Current_Vertical_Percent_Pos, false, null, Ball_States.END_OVER_END, Movement.LINE, Ball_Speed.SLOW, false, 0);
            List<Action> BR_list = getBounceRollActions(prev_yardline, prev_vert, Current_YardLine, Current_Vertical_Percent_Pos, blefttoRight);

            Play_Stage bStage = new Play_Stage();
            bStage.Main_Object = true;
            bStage.Actions.Add(bas);

            bStage.Actions.AddRange(BR_list);
            Stages.Add(bStage);
        }


        public void FG_Blocked(double prev_yl, double prev_v, double end_yl, double end_v, bool blefttoRight)
        {
            const double BOUNCE_LENGTH = 4.7;
            const double ROLL_LENGTH = 4.0;

            double prev_yardline = prev_yl;
            double prev_vert = prev_v;

            PointXY new_end_point = null;

            double dlefttoRight = blefttoRight ? 1 : -1;

            State = Ball_States.ROLLING; 
            Action bas = new Action(Game_Object_Types.B, prev_yl, prev_v, Current_YardLine, Current_Vertical_Percent_Pos, false, null, Ball_States.END_OVER_END, Movement.LINE, Ball_Speed.NORMAL, false, 0);

            State = Ball_States.END_OVER_END;
            Action bas2 = new Action(Game_Object_Types.B, Current_YardLine, Current_Vertical_Percent_Pos, end_yl, end_v, false, null, Ball_States.END_OVER_END, Movement.LINE, Ball_Speed.NORMAL, false, 0);

            Current_YardLine = end_yl;
            Current_Vertical_Percent_Pos = end_v;

            Action bas3 = null;
            Action bas4 = null;

            bool bTop = Game_Engine_Helper.isBallvertTop(Current_Vertical_Percent_Pos);
            double bounce_roll_vert = bTop ? 0.5 : -0.5;

            //Get the end point for bouncing ball
            new_end_point = PointPlotter.getExtendedEndpoint(prev_yl, prev_v, end_yl, end_v, BOUNCE_LENGTH * dlefttoRight);

            State = Ball_States.BOUNCING;
            bas3 = new Action(Game_Object_Types.B, Current_YardLine, Current_Vertical_Percent_Pos, new_end_point.x, new_end_point.y + bounce_roll_vert, false, null, Ball_States.BOUNCING, Movement.LINE, Ball_Speed.SLOW, false, 0);

            //Get the end point for rolling ball
            PointXY rolling_end_point = PointPlotter.getExtendedEndpoint(prev_yl, prev_v, new_end_point.x, new_end_point.y + (bounce_roll_vert * 1.5), ROLL_LENGTH * dlefttoRight);

            State = Ball_States.ROLLING_VERTICAL;
            bas4 = new Action(Game_Object_Types.B, new_end_point.x, new_end_point.y, rolling_end_point.x, rolling_end_point.y, false, null, Ball_States.ROLLING_VERTICAL, Movement.LINE, Ball_Speed.SLOW, false, 0);


            Play_Stage bStage = new Play_Stage();
            bStage.Main_Object = true;
            bStage.Actions.Add(bas);
            bStage.Actions.Add(bas2);
            bStage.Actions.Add(bas3);
            bStage.Actions.Add(bas4);
            Stages.Add(bStage);
        }
        public void Punt_End_Over_End_Thru_Air(double prev_yl, double prev_v, bool bLefttoRight)
        {
            double vert_adjust_off_Foot = -1.5;
            double yl_adjust_off_foot = 0.0;
            if (bLefttoRight)
                yl_adjust_off_foot = -1.0;
            else
                yl_adjust_off_foot = 1.5;

            State = Ball_States.PUNT_THRU_THE_AIR;
            Action bas = new Action(Game_Object_Types.B, prev_yl + yl_adjust_off_foot, prev_v + vert_adjust_off_Foot, Current_YardLine, Current_Vertical_Percent_Pos, false, null, State, Movement.LINE, Ball_Speed.SLOW, false, 0);
            Play_Stage bStage = new Play_Stage();
            bStage.Main_Object = true;
            bStage.Actions.Add(bas);
            Stages.Add(bStage);
        }

        public void Spiral(double prev_yl, double prev_v)
        {
            State = Ball_States.SPIRAL;
            Action bas = new Action(Game_Object_Types.B, prev_yl, prev_v, Current_YardLine, Current_Vertical_Percent_Pos, false, null, Ball_States.SPIRAL, Movement.LINE, Ball_Speed.SLOW, false, 0);
            Play_Stage bStage = new Play_Stage();
            bStage.Main_Object = true;
            bStage.Actions.Add(bas);
            Stages.Add(bStage);
        }

        public void Punt_Out_of_Bounds(double prev_yl, double prev_v, bool blefttoRight)
        {
            State = Ball_States.PUNT_THRU_THE_AIR;
            Action bas = new Action(Game_Object_Types.B, prev_yl, prev_v, Current_YardLine, Current_Vertical_Percent_Pos, false, null, State, Movement.LINE, Ball_Speed.SLOW, false, 0);

            Play_Stage bStage = new Play_Stage();
            bStage.Main_Object = true;
            bStage.Actions.Add(bas);
            List<Action> BR_list = getBounceRollActions(prev_yl, prev_v, Current_YardLine, Current_Vertical_Percent_Pos, blefttoRight);
            bStage.Actions.AddRange(BR_list);
            Stages.Add(bStage);
        }

        public void Punt_Out_of_Endzone(bool blefttoRight)
        {
            double prev_yardline = Starting_YardLine;
            double prev_vert = Starting_Vertical_Percent_Pos;

            State = Ball_States.PUNT_THRU_THE_AIR;
            Action bas = new Action(Game_Object_Types.B, prev_yardline, prev_vert, Current_YardLine, Current_Vertical_Percent_Pos, false, null, State, Movement.LINE, Ball_Speed.SLOW, false, 0);
            Play_Stage bStage = new Play_Stage();
            List<Action> BR_list = getBounceRollActions(prev_yardline, prev_vert, Current_YardLine, Current_Vertical_Percent_Pos, blefttoRight);

            bStage.Main_Object = true;
            bStage.Actions.Add(bas);
            bStage.Actions.AddRange(BR_list);
            Stages.Add(bStage);
        }
        public void FG_Long_Enough(double prev_yardline, double prev_vert, bool blefttoRight)
        {
            State = Ball_States.END_OVER_END;
            Action bas = new Action(Game_Object_Types.B, prev_yardline, prev_vert, Current_YardLine, Current_Vertical_Percent_Pos, false, null, Ball_States.END_OVER_END, Movement.LINE, Ball_Speed.SLOW, false, 0);
            List<Action> BR_list = getBounceRollActions(prev_yardline, prev_vert, Current_YardLine, Current_Vertical_Percent_Pos, blefttoRight);

            Play_Stage bStage = new Play_Stage();
            bStage.Main_Object = true;
            bStage.bBall_Over_Goalposts = true;
            bStage.Actions.Add(bas);
            bStage.Actions.AddRange(BR_list);
            Stages.Add(bStage);
        }
        public void End_Over_End_Thru_Air(double prev_yardline, double prev_vert, bool blefttoRight)
        {
            State = Ball_States.END_OVER_END;
            Action bas = new Action(Game_Object_Types.B, prev_yardline, prev_vert, Current_YardLine, Current_Vertical_Percent_Pos, false, null, Ball_States.END_OVER_END, Movement.LINE, Ball_Speed.SLOW, false, 0);
            List<Action> BR_list = getBounceRollActions(prev_yardline, prev_vert, Current_YardLine, Current_Vertical_Percent_Pos, blefttoRight);

            Play_Stage bStage = new Play_Stage();
            bStage.Main_Object = true;
            bStage.Actions.Add(bas);
            bStage.Actions.AddRange(BR_list);
            Stages.Add(bStage);
        }
        public void Punt_End_Over_End_Thru_Air_Out_of_Endzone(double prev_yl, double prev_v, bool blefttoRight)
        {
            Action bas = new Action(Game_Object_Types.B, prev_yl, prev_v, Current_YardLine, Current_Vertical_Percent_Pos, false, null, Ball_States.PUNT_THRU_THE_AIR, Movement.LINE, Ball_Speed.SLOW, false, 0);
            Play_Stage bStage = new Play_Stage();
            bStage.Main_Object = true;
            bStage.Actions.Add(bas);
            List<Action> BR_list = getBounceRollActions(prev_yl, prev_v, Current_YardLine, Current_Vertical_Percent_Pos, blefttoRight);
            bStage.Actions.AddRange(BR_list);
            Stages.Add(bStage);
        }
        public void Punt_End_Over_End_Thru_Air_Not_Caught(double prev_yl, double prev_v, bool blefttoRight)
        {
            Action bas = new Action(Game_Object_Types.B, prev_yl, prev_v, Current_YardLine, Current_Vertical_Percent_Pos, false, null, Ball_States.PUNT_THRU_THE_AIR, Movement.LINE, Ball_Speed.SLOW, false, 0);

            Play_Stage bStage = new Play_Stage();
            bStage.Main_Object = true;
            bStage.Actions.Add(bas);
            List<Action> BR_list = getBounceRollActions(prev_yl, prev_v, Current_YardLine, Current_Vertical_Percent_Pos, blefttoRight);
            bStage.Actions.AddRange(BR_list);
            Stages.Add(bStage);


        }
        public void Carried(double prev_yl, double prev_v)
        {
            Action bas = new Action(Game_Object_Types.B, prev_yl, prev_v, Current_YardLine, Current_Vertical_Percent_Pos, true, null, Ball_States.CARRIED, Movement.LINE, Ball_Speed.CARRIED, false, 0);
            Play_Stage bStage = new Play_Stage();
            bStage.Main_Object = true;
            bStage.Actions.Add(bas);
            Stages.Add(bStage);
            State = Ball_States.CARRIED;
        }

        public void Delay_Carried(double prev_yl, double prev_v, int delay)
        {
            Action bas = new  Action(Game_Object_Types.B, prev_yl, prev_v, prev_yl, prev_v, true, null, Ball_States.CARRIED, Movement.FAKE_MOVEMENT, null, false, delay);
            Action bas2 = new Action(Game_Object_Types.B, prev_yl, prev_v, Current_YardLine, Current_Vertical_Percent_Pos, true, null, Ball_States.CARRIED, Movement.LINE, Ball_Speed.CARRIED, false, 0);
            Play_Stage bStage = new Play_Stage();
            bStage.Main_Object = true;
            bStage.Actions.Add(bas);
            bStage.Actions.Add(bas2);
            Stages.Add(bStage);
            State = Ball_States.CARRIED;
        }

        public void Delay_Carried_Slowly(double prev_yl, double prev_v, int delay)
        {
            Action bas = new Action(Game_Object_Types.B, prev_yl, prev_v, prev_yl, prev_v, true, null, Ball_States.CARRIED, Movement.FAKE_MOVEMENT, null, false, delay);
            Action bas2 = new Action(Game_Object_Types.B, prev_yl, prev_v, Current_YardLine, Current_Vertical_Percent_Pos, true, null, Ball_States.CARRIED, Movement.LINE, Ball_Speed.CARRIED_SLOW, false, 0);
            Play_Stage bStage = new Play_Stage();
            bStage.Main_Object = true;
            bStage.Actions.Add(bas);
            bStage.Actions.Add(bas2);
            Stages.Add(bStage);
            State = Ball_States.CARRIED;
        }

        public void Carried_notMain(double prev_yl, double prev_v)
        {
            Action bas = new Action(Game_Object_Types.B, prev_yl, prev_v, Current_YardLine, Current_Vertical_Percent_Pos, true, null, Ball_States.CARRIED, Movement.LINE, Ball_Speed.CARRIED, false, 0);
            Play_Stage bStage = new Play_Stage();
            bStage.Main_Object = false;
            bStage.Actions.Add(bas);
            Stages.Add(bStage);
            State = Ball_States.CARRIED;
        }

        public void Carried_Tackled(double prev_yl, double prev_v)
        {
            Action bas = new Action(Game_Object_Types.B, prev_yl, prev_v, Current_YardLine, Current_Vertical_Percent_Pos, true, null, Ball_States.CARRIED, Movement.LINE, Ball_Speed.CARRIED, false, 0);
            Play_Stage bStage = new Play_Stage();
            bStage.Main_Object = false;
            bStage.Actions.Add(bas);
            Stages.Add(bStage);
            State = Ball_States.CARRIED;
        }

        public void Carried_Fake_Movement(int n)
        {
            Play_Stage bStage = new Play_Stage();
            bStage.Main_Object = false;
            Action bas = new Action(Game_Object_Types.B, Current_YardLine, Current_Vertical_Percent_Pos, Current_YardLine, Current_Vertical_Percent_Pos, true, null, Ball_States.CARRIED, Movement.FAKE_MOVEMENT, Ball_Speed.CARRIED, false, n);
            bStage.Actions.Add(bas);
            Stages.Add(bStage);
            State = Ball_States.CARRIED;
        }

        public void Popup(int n, double vertMinus)
        {
            Play_Stage bStage = new Play_Stage();
            bStage.Main_Object = true;
            Action bas = new Action(Game_Object_Types.B, Current_YardLine, Current_Vertical_Percent_Pos - vertMinus, Current_YardLine, Current_Vertical_Percent_Pos - vertMinus, false, null, Ball_States.POPUP, Movement.FAKE_MOVEMENT, Ball_Speed.NORMAL, false, n);
            bStage.Actions.Add(bas);
            Stages.Add(bStage);
            State = Ball_States.CARRIED;
        }

        public void Carried_Out_of_Bounds(double prev_yl, double prev_v, bool blefttoRight)
        {
            double top_adjustment = 0.0;

            double dlefttoRight = blefttoRight ? 1 : -1;

            if (Current_Vertical_Percent_Pos <= 0)
                top_adjustment = app_Constants.TOP_OUTOFBOUNDS_ADJUSTMENT;

            PointXY new_end_point = null;

            Action bas = new Action(Game_Object_Types.B, prev_yl, prev_v, Current_YardLine, Current_Vertical_Percent_Pos, true, null, Ball_States.CARRIED, Movement.LINE, Ball_Speed.SLOW, false, 0);

            //Get the end point for the layer running out of bounds
            new_end_point = PointPlotter.getExtendedEndpoint(prev_v, prev_v, Current_YardLine, Current_Vertical_Percent_Pos, (app_Constants.OUT_OF_BOUNDS_LEN + top_adjustment) * dlefttoRight);

            Action bas2 = new Action(Game_Object_Types.B, Current_YardLine, Current_Vertical_Percent_Pos, new_end_point.x, new_end_point.y, true, null, Ball_States.CARRIED, Movement.LINE, Ball_Speed.SLOW, false, 0);
            Play_Stage bStage = new Play_Stage();
            bStage.Main_Object = true;
            bStage.Actions.Add(bas);
            bStage.Actions.Add(bas2);
            Stages.Add(bStage);
            State = Ball_States.CARRIED;
        }
        public void Bounce_Along_Ground()
        {
            Play_Stage bStage = new Play_Stage();
            bStage.Main_Object = true;
            Action bas2 = new Action(Game_Object_Types.B, Starting_YardLine, Starting_Vertical_Percent_Pos, Current_YardLine, Current_Vertical_Percent_Pos, false, null, Ball_States.BOUNCING, Movement.LINE, Ball_Speed.NORMAL, false, 0);
            bStage.Actions.Add(bas2);
            Stages.Add(bStage);
            State = Ball_States.BOUNCING;
        }

    }
}