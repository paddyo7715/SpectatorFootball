using SpectatorFootball.Enum;
using SpectatorFootball.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using SpectatorFootball.Common;

namespace SpectatorFootball.GameNS
{
    public class Game_Player
    {
        private static ILog logger = LogManager.GetLogger("RollingFile");

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
            Action pas = new Action(Game_Object_Types.P, Starting_YardLine, Starting_Vertical_Percent_Pos, 0.0, 0.0, false, false, Player_States.STANDING, null, null, Movement.NONE, null, false);
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
            a.p_state, a.b_state, a.Sound, a.MoveType, a.Ball_Speed, false);
            new_st.Actions.Add(new_action);

            Stages.Add(new_st);
        }

        public void KickBall(Player_States moving_ps, double prev_yl_1, double prev_v_1, double RunUp_YardLine_1, double RunUp_Vertical_Percent_Pos_1)
        {
            Action pas1 = new Action(Game_Object_Types.P, prev_yl_1, prev_v_1, RunUp_YardLine_1, RunUp_Vertical_Percent_Pos_1, false, false, moving_ps, null, null, Movement.LINE, null, false);
            Action pas2 = new Action(Game_Object_Types.P, RunUp_YardLine_1, RunUp_Vertical_Percent_Pos_1, Current_YardLine, Current_Vertical_Percent_Pos, false, true, Player_States.FG_KICK, null, Game_Sounds.KICK, Movement.LINE, null, false);
            Play_Stage pStage = new Play_Stage();
            pStage.Main_Object = true;
            pStage.Actions.Add(pas1);
            pStage.Actions.Add(pas2);
            Stages.Add(pStage);
        }

        public void Block()
        {
            Action pas = new Action(Game_Object_Types.P, Current_YardLine, Current_Vertical_Percent_Pos, Current_YardLine, Current_Vertical_Percent_Pos, false, false, Player_States.BLOCKING, null, null, Movement.NONE, null, false);
            Play_Stage pStage = new Play_Stage();
            pStage.Main_Object = false;
            pStage.Actions.Add(pas);
            Stages.Add(pStage);
        }
        public void OnBack()
        {
            Action pas = new Action(Game_Object_Types.P, Current_YardLine, Current_Vertical_Percent_Pos, Current_YardLine, Current_Vertical_Percent_Pos, false, false, Player_States.ON_BACK, null, null, Movement.NONE, null, false);
            Play_Stage pStage = new Play_Stage();
            pStage.Main_Object = false;
            pStage.Actions.Add(pas);
            Stages.Add(pStage);
        }
        public void Run_Then_Stand(Player_States moving_ps, double prev_yl, double prev_v)
        {
            Action pas = null;
            pas = new Action(Game_Object_Types.P, prev_yl, prev_v, Current_YardLine, Current_Vertical_Percent_Pos, false, false, moving_ps, null, null, Movement.LINE, null, false);
            Action pas2 = new Action(Game_Object_Types.P, Starting_YardLine, Starting_Vertical_Percent_Pos, 0.0, 0.0, false, false, Player_States.STANDING, null, null, Movement.NONE, null, false);
            Play_Stage pStage = new Play_Stage();
            pStage.Main_Object = false;
            pStage.Actions.Add(pas);
            pStage.Actions.Add(pas2);
            Stages.Add(pStage);
        }

        public void Run_Then_CatchKick(Player_States moving_ps, double prev_yl, double prev_v)
        {
            Action pas = new Action(Game_Object_Types.P, prev_yl, prev_v, Current_YardLine, Current_Vertical_Percent_Pos, false, false, moving_ps, null, null, Movement.LINE, null, false);
            Action pas2 = new Action(Game_Object_Types.P, prev_yl, prev_v, Current_YardLine, Current_Vertical_Percent_Pos, false, true, Player_States.ABOUT_TO_CATCH_KICK, null, null, null, null, false);
            Play_Stage pStage = new Play_Stage();
            pStage.Main_Object = false;
            pStage.Player_Catches_Ball = true;
            pStage.Actions.Add(pas);
            pStage.Actions.Add(pas2);
            Stages.Add(pStage);
        }

        public void Attempt_Tackle(Player_States moving_ps, double prev_yl, double prev_v)
        {
            Action pas = new Action(Game_Object_Types.P, prev_yl, prev_v, Current_YardLine, Current_Vertical_Percent_Pos, false, false, moving_ps, null, null, Movement.LINE, null, false);
            //                            Action pas2 = new Action(Game_Object_Types.P, prev_yl, prev_v, p.Current_YardLine + (app_Constants.TACKLER_FOO_MOVE * HorizontalAdj(bLefttoRight)), p.Current_Vertical_Percent_Pos, false, false, Player_States.TACKLING, null, null, Movement.LINE, null);
            Action pas3 = new Action(Game_Object_Types.P, prev_yl, prev_v, Current_YardLine, Current_Vertical_Percent_Pos, false, false, Player_States.ON_BACK, null, null, Movement.NONE, null, false);

            Play_Stage pStage = new Play_Stage();
            pStage.Main_Object = false;
            pStage.Actions.Add(pas);
            //                            pStage.Actions.Add(pas2);
            pStage.Actions.Add(pas3);
            Stages.Add(pStage);
        }

        public void Run(Player_States moving_ps, double prev_yl, double prev_v)
        {
            Action pas = new Action(Game_Object_Types.P, prev_yl, prev_v, Current_YardLine, Current_Vertical_Percent_Pos, true, false, moving_ps, null, null, Movement.LINE, null, false); 
            Play_Stage pStage = new Play_Stage();
            pStage.Main_Object = false;
            pStage.Actions.Add(pas);
            Stages.Add(pStage);
        }

        public void Run_and_Tackled(Player_States moving_ps, double prev_yl, double prev_v)
        {
            Action pas = new Action(Game_Object_Types.P, prev_yl, prev_v, Current_YardLine, Current_Vertical_Percent_Pos, true, false, moving_ps, null, null, Movement.LINE, null, false);
            Action pas2 = null;
            pas2 = new Action(Game_Object_Types.P, prev_yl, prev_v, Current_YardLine, Current_Vertical_Percent_Pos, true, false, Player_States.TACKLED, null, null, Movement.NONE, null, false);
            Play_Stage pStage = new Play_Stage();
            pStage.Main_Object = false;
            pStage.Actions.Add(pas);
            pStage.Actions.Add(pas2);
            Stages.Add(pStage);
        }

        public bool isKickOutofEndzone(double yardline)
        {
            bool r = false;

            if (yardline < 0.0 || yardline > 100.0)
            {
                double d = 0;

                if (yardline < 0)
                    d = Math.Abs(yardline);
                else if (yardline > 100)
                    d = yardline - 100;

                if (d >= app_Constants.KICK_OUT_OF_ENDZONE_YARD)
                    r = true; ;
            }

            return r;
        }

        public bool isTouchback(bool bLast_Play, List<int?> group_1)
        {
            logger.Debug("isReturnKickoff");
            logger.Debug("Current Yardline: " + Current_YardLine);

            bool r = false;

            if (!bLast_Play && (Current_YardLine < 0.0 || Current_YardLine > 100.0))
            {
                double d = 0;

                if (Current_YardLine < 0)
                    d = Math.Abs(Current_YardLine);
                else if (Current_YardLine > 100)
                    d = Current_YardLine - 100;

                int empty_slots = group_1.Where(x => x == null).Count();
                d -= app_Constants.KICKOFF_RUN_OUT_YARD_FACTOR * empty_slots;
                if (d > 0)
                    r = true;
            }

            return r;
        }

        public void Kneel_With_Ball(double prev_yl, double prev_v)
        {
            Action pas = new Action(Game_Object_Types.P, prev_yl, prev_v, Current_YardLine, Current_Vertical_Percent_Pos, true, true, Player_States.KNEELING, null, null, Movement.FAKE_MOVEMENT, null, true);
            Play_Stage pStage = new Play_Stage();
            pStage.Main_Object = false;
            pStage.Actions.Add(pas);
            Stages.Add(pStage);
        }

        public bool Kickoff_GoOutofBounds(bool bLast_Play, bool bGoOut, int slot, double ret_vert)
        {
            bool r = false;

            if (!bLast_Play && bGoOut && (slot == 1 || slot == app_Constants.KICKOFF_PLAYERS_IN_GROUP) &&
                (ret_vert <= app_Constants.KICKOFF_RUN_OOB_TOP_LIMIT || ret_vert > app_Constants.KICKOFF_RUN_OOB_BOTTOM_LIMIT))
                r = true;

            return r;
        }
        //If the layer is looking to go out of bounds then get him the vert for the closest sideline
        public double VertForNearestSideline(double v)
        {
            double r = 0.0;

            if (v > 50.0)
                r = 100.0;

            return r;
        }

        public void Run_and_GoOut_of_Bounds(Player_States moving_ps, double prev_yl, double prev_v, bool blefttoRight)
        {
            double top_adjustment = 0.0;

            double dlefttoRight = blefttoRight ? 1 : -1;

            if (Current_Vertical_Percent_Pos <= 0)
                top_adjustment = app_Constants.TOP_OUTOFBOUNDS_ADJUSTMENT;

            PointXY new_end_point = null;

            Action pas = new Action(Game_Object_Types.P, prev_yl, prev_v, Current_YardLine, Current_Vertical_Percent_Pos, true, false, moving_ps, null, null, Movement.LINE, null, false);

            //Get the end point for the layer running out of bounds
            new_end_point = PointPlotter.getExtendedEndpoint(prev_yl, prev_v, Current_YardLine, Current_Vertical_Percent_Pos, (app_Constants.OUT_OF_BOUNDS_LEN + app_Constants.TOP_OUTOFBOUNDS_ADJUSTMENT) * dlefttoRight);

            Action pas2 = null;
            pas2 = new Action(Game_Object_Types.P, Current_YardLine, Current_Vertical_Percent_Pos, new_end_point.x, new_end_point.y, true, false, moving_ps, null, null, Movement.LINE, null, false);

            Play_Stage pStage = new Play_Stage();
            pStage.Main_Object = false;
            pStage.Actions.Add(pas);
            pStage.Actions.Add(pas2);
            Stages.Add(pStage);
        }



    }


}
