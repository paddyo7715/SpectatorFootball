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

        public void KickBall(Player_States moving_ps, double prev_yl_1, double prev_v_1, double RunUp_YardLine_1, double RunUp_Vertical_Percent_Pos_1)
        {
            Action pas1 = new Action(Game_Object_Types.P, prev_yl_1, prev_v_1, RunUp_YardLine_1, RunUp_Vertical_Percent_Pos_1, false, false, moving_ps, null, null, Movement.LINE, null);
            Action pas2 = new Action(Game_Object_Types.P, RunUp_YardLine_1, RunUp_Vertical_Percent_Pos_1, Current_YardLine, Current_Vertical_Percent_Pos, false, true, Player_States.FG_KICK, null, Game_Sounds.KICK, Movement.LINE, null);
            Play_Stage pStage = new Play_Stage();
            pStage.Main_Object = true;
            pStage.Actions.Add(pas1);
            pStage.Actions.Add(pas2);
            Stages.Add(pStage);
        }

        public void Block()
        {
            Action pas = new Action(Game_Object_Types.P, Current_YardLine, Current_Vertical_Percent_Pos, Current_YardLine, Current_Vertical_Percent_Pos, false, false, Player_States.BLOCKING, null, null, Movement.NONE, null);
            Play_Stage pStage = new Play_Stage();
            pStage.Main_Object = false;
            pStage.Actions.Add(pas);
            Stages.Add(pStage);
        }
        public void Run_Then_Stand(Player_States moving_ps, double prev_yl, double prev_v)
        {
            Action pas = null;
            pas = new Action(Game_Object_Types.P, prev_yl, prev_v, Current_YardLine, Current_Vertical_Percent_Pos, false, false, moving_ps, null, null, Movement.LINE, null);
            Action pas2 = new Action(Game_Object_Types.P, Starting_YardLine, Starting_Vertical_Percent_Pos, 0.0, 0.0, false, false, Player_States.STANDING, null, null, Movement.NONE, null);
            Play_Stage pStage = new Play_Stage();
            pStage.Main_Object = false;
            pStage.Actions.Add(pas);
            pStage.Actions.Add(pas2);
            Stages.Add(pStage);
        }

        public void Run_Then_CatchKick(Player_States moving_ps, double prev_yl, double prev_v)
        {
            Action pas = new Action(Game_Object_Types.P, prev_yl, prev_v, Current_YardLine, Current_Vertical_Percent_Pos, false, false, moving_ps, null, null, Movement.LINE, null);
            Action pas2 = new Action(Game_Object_Types.P, prev_yl, prev_v, Current_YardLine, Current_Vertical_Percent_Pos, false, true, Player_States.ABOUT_TO_CATCH_KICK, null, null, null, null);
            Play_Stage pStage = new Play_Stage();
            pStage.Main_Object = false;
            pStage.Player_Catches_Ball = true;
            pStage.Actions.Add(pas);
            pStage.Actions.Add(pas2);
            Stages.Add(pStage);
        }

        public void Attempt_Tackle(Player_States moving_ps, double prev_yl, double prev_v)
        {
            Action pas = new Action(Game_Object_Types.P, prev_yl, prev_v, Current_YardLine, Current_Vertical_Percent_Pos, false, false, moving_ps, null, null, Movement.LINE, null);
            //                            Action pas2 = new Action(Game_Object_Types.P, prev_yl, prev_v, p.Current_YardLine + (app_Constants.TACKLER_FOO_MOVE * HorizontalAdj(bLefttoRight)), p.Current_Vertical_Percent_Pos, false, false, Player_States.TACKLING, null, null, Movement.LINE, null);
            Action pas3 = new Action(Game_Object_Types.P, prev_yl, prev_v, Current_YardLine, Current_Vertical_Percent_Pos, false, false, Player_States.ON_BACK, null, null, Movement.NONE, null);
            Play_Stage pStage = new Play_Stage();
            pStage.Main_Object = false;
            pStage.Actions.Add(pas);
            //                            pStage.Actions.Add(pas2);
            pStage.Actions.Add(pas3);
            Stages.Add(pStage);
        }

        public void Run(Player_States moving_ps, double prev_yl, double prev_v)
        {
            Action pas = new Action(Game_Object_Types.P, prev_yl, prev_v, Current_YardLine, Current_Vertical_Percent_Pos, true, false, moving_ps, null, null, Movement.LINE, null); 
            Play_Stage pStage = new Play_Stage();
            pStage.Main_Object = false;
            pStage.Actions.Add(pas);
            Stages.Add(pStage);
        }

        public void Run_and_Tackled(Player_States moving_ps, double prev_yl, double prev_v)
        {
            Action pas = new Action(Game_Object_Types.P, prev_yl, prev_v, Current_YardLine, Current_Vertical_Percent_Pos, true, false, moving_ps, null, null, Movement.LINE, null);
            Action pas2 = null;
            pas2 = new Action(Game_Object_Types.P, prev_yl, prev_v, Current_YardLine, Current_Vertical_Percent_Pos, true, false, Player_States.TACKLED, null, null, Movement.NONE, null);
            Play_Stage pStage = new Play_Stage();
            pStage.Main_Object = false;
            pStage.Actions.Add(pas);
            pStage.Actions.Add(pas2);
            Stages.Add(pStage);
        }

    }


}
