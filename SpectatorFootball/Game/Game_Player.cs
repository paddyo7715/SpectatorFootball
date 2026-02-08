using SpectatorFootball.Enum;
using SpectatorFootball.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using SpectatorFootball.Common;
using System.Windows.Markup;
using SpectatorFootball.NarrationAndText;

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
            Action pas = new Action(Game_Object_Types.P, Starting_YardLine, Starting_Vertical_Percent_Pos, 0.0, 0.0, false, Player_States.STANDING, null, Movement.NONE, null, false, 0);
            Play_Stage pStage = new Play_Stage();
            pStage.Main_Object = false;
            pStage.Actions.Add(pas);
            Stages.Add(pStage);
            State = Player_States.STANDING;
        }

        public void Ready_Hold_FG()
        {
            Action pas = new Action(Game_Object_Types.P, Starting_YardLine, Starting_Vertical_Percent_Pos, 0.0, 0.0, false, Player_States.FG_HOLDER_READY, null, Movement.NONE, null, false, 0);
            Play_Stage pStage = new Play_Stage();
            pStage.Main_Object = false;
            pStage.Actions.Add(pas);
            Stages.Add(pStage);
            State = Player_States.FG_HOLDER_READY;
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
            a.end_yardline, a.end_vertical, a.bPossesses_Ball, 
            a.p_state, a.b_state, a.MoveType, a.Ball_Speed, false, 0);
            new_st.Actions.Add(new_action);

            Stages.Add(new_st);
        }

        public void Same_As_Last_Action_not_main()
        {
            //This method will take the last stage and create a new stage with the
            //each same actions.
            if (Stages.Count() == 0)
                throw new Exception("Same_As_Last_Action called on a player with 0 stages!");

            Play_Stage ps = Stages.Last();

            Play_Stage new_st = new Play_Stage()
            {
                Main_Object = false,
                Player_Catches_Ball = ps.Player_Catches_Ball
            };

            Action a = ps.Actions.Last();

            Action new_action = new Action(a.type, a.start_yardline, a.start_vertical,
            a.end_yardline, a.end_vertical, a.bPossesses_Ball,
            a.p_state, a.b_state, a.MoveType, a.Ball_Speed, false, 0);
            new_st.Actions.Add(new_action);

            Stages.Add(new_st);
        }

        public void KickBall(Player_States moving_ps, double prev_yl_1, double prev_v_1, double RunUp_YardLine_1, double RunUp_Vertical_Percent_Pos_1,
            string beforemsg, string aftermsg)
        {
            Action pas1 = new Action(Game_Object_Types.P, prev_yl_1, prev_v_1, Current_YardLine, Current_Vertical_Percent_Pos, false, moving_ps, null, Movement.LINE, Ball_Speed.CARRIED_SLOW, false, 0);
            Action pas2 = new Action(Game_Object_Types.P, RunUp_YardLine_1, RunUp_Vertical_Percent_Pos_1, Current_YardLine, Current_Vertical_Percent_Pos, false, Player_States.FG_KICK, null,Movement.LINE, null, false, 0);

            if (beforemsg != null)
                pas1.Effects.Add(new AE_rec() { XYIndex = 0, Announcer_msg = beforemsg });

            if (aftermsg != null)
                pas2.Effects.Add(new AE_rec() { XYIndex = pas2.PointXY.Count() - 1, Announcer_msg = aftermsg });

            pas2.Effects.Add(new AE_rec() { XYIndex = 0, Sound = Game_Sounds.KICK });

            Play_Stage pStage = new Play_Stage();
            pStage.Main_Object = true;
            pStage.Actions.Add(pas1);
            pStage.Actions.Add(pas2);
            Stages.Add(pStage);
            State = Player_States.FG_KICK;
        }

        public void Block(bool bBlockSound)
        {
            Game_Sounds? sound = null;

            Action pas = new Action(Game_Object_Types.P, Current_YardLine, Current_Vertical_Percent_Pos, Current_YardLine, Current_Vertical_Percent_Pos, false, Player_States.BLOCKING, null, Movement.NONE, null, false, 0);
            Play_Stage pStage = new Play_Stage();
            pStage.Main_Object = false;
            pStage.Actions.Add(pas);
            Stages.Add(pStage);
            State = Player_States.BLOCKING;
        }
        public void Run_Then_Block(Player_States moving_ps, double prev_yl, double prev_v)
        {
            Action pas = null;
            pas = new Action(Game_Object_Types.P, prev_yl, prev_v, Current_YardLine, Current_Vertical_Percent_Pos, false, moving_ps, null, Movement.LINE, null, false, 0);
            Action pas2 = new Action(Game_Object_Types.P, Starting_YardLine, Starting_Vertical_Percent_Pos, 0.0, 0.0, false, Player_States.BLOCKING, null, Movement.NONE, null, false, 0);
            Play_Stage pStage = new Play_Stage();
            pStage.Main_Object = false;
            pStage.Actions.Add(pas);
            pStage.Actions.Add(pas2);
            Stages.Add(pStage);
            State = Player_States.BLOCKING;
        }
        public void DeBlock_then_Run(Player_States moving_ps, double prev_yl, double prev_v, int delay)
        {
            Action pas1 = new Action(Game_Object_Types.P, prev_yl, prev_v, prev_yl, prev_v, false, Player_States.BLOCK_KICK, null, Movement.FAKE_MOVEMENT, null, false, delay / 2);
            Action pas2 = new Action(Game_Object_Types.P, prev_yl, prev_v, Current_YardLine, Current_Vertical_Percent_Pos, false, moving_ps, null, Movement.LINE, null, false, 0);
            Play_Stage pStage = new Play_Stage();
            pStage.Main_Object = false;
            pStage.Actions.Add(pas1);
            pStage.Actions.Add(pas2);
            Stages.Add(pStage);
            State = moving_ps;
        }
        public void Delay_Then_Run_and_Stand(Player_States moving_ps, double prev_yl, double prev_v, int delay,
            string beforemsg, string aftermsg)
        {
             Action pas = new Action(Game_Object_Types.P, prev_yl, prev_v, prev_yl, prev_v, false, Player_States.STANDING, null, Movement.FAKE_MOVEMENT, null, false, delay);
            Action pas2 = new Action(Game_Object_Types.P, prev_yl, prev_v, Current_YardLine, Current_Vertical_Percent_Pos, false, moving_ps, null, Movement.LINE, null, false, 0);

            if (beforemsg != null)
                pas.Effects.Add(new AE_rec() { XYIndex = 0, Announcer_msg = beforemsg });

            if (aftermsg != null)
                pas2.Effects.Add(new AE_rec() { XYIndex = pas2.PointXY.Count() - 1, Announcer_msg = aftermsg });

            Action pas3 = new Action(Game_Object_Types.P, Starting_YardLine, Starting_Vertical_Percent_Pos, 0.0, 0.0, false, Player_States.STANDING, null, Movement.NONE, null, false, 0);
            Play_Stage pStage = new Play_Stage();
            pStage.Main_Object = false;
            pStage.Actions.Add(pas);
            pStage.Actions.Add(pas2);
            pStage.Actions.Add(pas3);
            Stages.Add(pStage);
            State = moving_ps;
        }

        public void Delay(int delay)
        {
            Action pas = new Action(Game_Object_Types.P, Current_YardLine, Current_Vertical_Percent_Pos, Current_YardLine, Current_YardLine, false, Player_States.STANDING, null, Movement.FAKE_MOVEMENT, null, false, delay);
            Play_Stage pStage = new Play_Stage();
            pStage.Main_Object = true;
            pStage.Actions.Add(pas);
            Stages.Add(pStage);
            State = Player_States.STANDING;
        }

        public void OnBack()
        {
            Action pas = new Action(Game_Object_Types.P, Current_YardLine, Current_Vertical_Percent_Pos, Current_YardLine, Current_Vertical_Percent_Pos, false, Player_States.ON_BACK, null, Movement.NONE, null, false, 0);
            Play_Stage pStage = new Play_Stage();
            pStage.Main_Object = false;
            pStage.Actions.Add(pas);
            Stages.Add(pStage);
            State = Player_States.ON_BACK;
        }
        public void Run_Then_Stand_notMain(Player_States moving_ps, double prev_yl, double prev_v, int xyLimit)
        {
            Action pas = null;
            pas = new Action(Game_Object_Types.P, prev_yl, prev_v, Current_YardLine, Current_Vertical_Percent_Pos, false, moving_ps, null, Movement.LINE, null, false, 0);

            //limit the number of xy points
            pas.PointXY = pas.PointXY.Take(xyLimit).ToList();
            Current_YardLine = pas.PointXY.Last().x;
            Current_Vertical_Percent_Pos = pas.PointXY.Last().y;

            Action pas2 = new Action(Game_Object_Types.P, Starting_YardLine, Starting_Vertical_Percent_Pos, 0.0, 0.0, false, Player_States.STANDING, null, Movement.NONE, null, false, 0);
            Play_Stage pStage = new Play_Stage();
            pStage.Main_Object = false;
            pStage.Actions.Add(pas);
            pStage.Actions.Add(pas2);
            Stages.Add(pStage);
            State = Player_States.STANDING;
        }

        public void Run_Then_Stand(Player_States moving_ps, double prev_yl, double prev_v)
        {
            Action pas = null;
            pas = new Action(Game_Object_Types.P, prev_yl, prev_v, Current_YardLine, Current_Vertical_Percent_Pos, false, moving_ps, null, Movement.LINE, null, false, 0);
            Action pas2 = new Action(Game_Object_Types.P, Starting_YardLine, Starting_Vertical_Percent_Pos, 0.0, 0.0, false, Player_States.STANDING, null, Movement.NONE, null, false, 0);
            Play_Stage pStage = new Play_Stage();
            pStage.Main_Object = false;
            pStage.Actions.Add(pas);
            pStage.Actions.Add(pas2);
            Stages.Add(pStage);
            State = Player_States.STANDING;
        }
        public void Run_Then_CatchKick(Player_States moving_ps, double prev_yl, double prev_v)
        {
            Action pas = new Action(Game_Object_Types.P, prev_yl, prev_v, Current_YardLine, Current_Vertical_Percent_Pos, false, moving_ps, null, Movement.LINE, null, false, 0);
            Action pas2 = new Action(Game_Object_Types.P, prev_yl, prev_v, Current_YardLine, Current_Vertical_Percent_Pos, false, Player_States.ABOUT_TO_CATCH_KICK, null, null, null, false, 0);

            Play_Stage pStage = new Play_Stage();
            pStage.Main_Object = false;
            pStage.Player_Catches_Ball = true;
            pStage.Actions.Add(pas);
            pStage.Actions.Add(pas2);
            Stages.Add(pStage);
            State = Player_States.ABOUT_TO_CATCH_KICK;
        }

        public void Attempt_Tackle(Player_States moving_ps, double prev_yl, double prev_v)
        {
            //in this method the player runs and then attempts atackle by falling to the ground.
            //what I am doing here is first plotting the full line to the ball carrier and
            //then I remove the last 3 points (for the tackle) and then use that to
            //create the tackle action.
            Action pas = new Action(Game_Object_Types.P, prev_yl, prev_v, Current_YardLine, Current_Vertical_Percent_Pos, false, moving_ps, null, Movement.LINE, null, false, 0);

            Tuple<List<PointXY>, List<PointXY>> t = pas.PullOffLastPoints(3);
            int run_points = pas.getXYPointsCount();
            if (run_points == 0)
                pas = null;
            else
                pas.PointXY = t.Item1;

            Action pas2 = new Action(Game_Object_Types.P, prev_yl, prev_v, Current_YardLine, Current_Vertical_Percent_Pos, false, Player_States.TACKLING, null, Movement.FAKE_MOVEMENT, null, false, 3);
            pas2.PointXY = t.Item2;

            Play_Stage pStage = new Play_Stage();
            pStage.Main_Object = false;
            if (pas != null) pStage.Actions.Add(pas);
            pStage.Actions.Add(pas2);
            Stages.Add(pStage);
            State = Player_States.TACKLING;
        }

        public void Attempt_the_Tackle(Player_States moving_ps, double prev_yl, double prev_v, int ball_carrier_points, int tackle_points)
        {
            List<PointXY> tackler_full_points = new List<PointXY>();
            //in this method the player runs and then attempts atackle by falling to the ground.
            //What I am doing is plotting the full line then if need be shortening to the length of the returner then 
            //shortening again for the tackle.
            Action pas = new Action(Game_Object_Types.P, prev_yl, prev_v, Current_YardLine, Current_Vertical_Percent_Pos, false, moving_ps, null, Movement.LINE, null, false, 0);

            //Shorten the xypoint list of the tacler if it is greater than the # of xypoints for the runner
            int full_len_line = pas.PointXY.Count() - ball_carrier_points;
            if (full_len_line > 0)
                pas.PointXY = pas.PointXY.Take(pas.PointXY.Count() - full_len_line).ToList();

            Tuple<List<PointXY>, List<PointXY>> t = pas.PullOffLastPoints(tackle_points);
            int run_points = pas.getXYPointsCount();
            if (run_points == 0)
                pas = null;
            else
                pas.PointXY = t.Item1;

            Action pas2 = new Action(Game_Object_Types.P, prev_yl, prev_v, Current_YardLine, Current_Vertical_Percent_Pos, false, Player_States.TACKLING, null, Movement.FAKE_MOVEMENT, null, false, tackle_points);
            pas2.PointXY = t.Item2;
            Current_YardLine = t.Item2.Last().x;
            Current_Vertical_Percent_Pos = t.Item2.Last().y;

            Play_Stage pStage = new Play_Stage();
            pStage.Main_Object = false;
            if (pas != null) pStage.Actions.Add(pas);
            pStage.Actions.Add(pas2);
            Stages.Add(pStage);
            State = Player_States.TACKLING;
        }

        public void Run_With_Ball(Player_States moving_ps, double prev_yl, double prev_v, double crowd_adj,
            string beforemsg, string aftermsg)
        {
            Action pas = new Action(Game_Object_Types.P, prev_yl, prev_v, Current_YardLine, Current_Vertical_Percent_Pos, true, moving_ps, null, Movement.LINE, null, false, 0);

            pas.Effects.Add(new AE_rec() { XYIndex = 0, Announcer_msg = beforemsg});
            pas.Effects.Add(new AE_rec() { XYIndex = pas.PointXY.Count() - 1, Announcer_msg = aftermsg, noise_adj = crowd_adj });

            Play_Stage pStage = new Play_Stage();
            pStage.Main_Object = false;
            pStage.Actions.Add(pas);
            Stages.Add(pStage);
            State = moving_ps;
        }

        public Player_States Convert_Movement_to_Slow(Player_States moving_ps)
        {
            Player_States r = Player_States.RUNNING_SLOW_FORWARD;
            switch(moving_ps)
            {
                case Player_States.RUNNING_FORWARD: 
                    r = Player_States.RUNNING_SLOW_FORWARD;
                    break;
                case Player_States.RUNNING_UP:
                    r = Player_States.RUNNING_SLOW_UP;
                    break;
                case Player_States.RUNNING_DOWN:
                    r = Player_States.RUNNING_SLOW_DOWN;
                    break;
                case Player_States.RUNNING_BACKWORDS:
                    r = Player_States.RUNNING_SLOW_BACKWORDS;
                    break;
            }
            return r;
        }
        public void Delay_Run_With_Ball(Player_States moving_ps, double prev_yl, double prev_v, int delay)
        {
            Action pas = new Action(Game_Object_Types.P, prev_yl, prev_v, prev_yl, prev_v, false, Player_States.STANDING, null, Movement.FAKE_MOVEMENT, null, false, delay);
            Action pas2 = new Action(Game_Object_Types.P, prev_yl, prev_v, Current_YardLine, Current_Vertical_Percent_Pos, true, moving_ps, null, Movement.LINE, null, false, 0);
            Play_Stage pStage = new Play_Stage();
            pStage.Main_Object = false;
            pStage.Actions.Add(pas);
            pStage.Actions.Add(pas2);
            Stages.Add(pStage);
            State = moving_ps;
        }
        public void Delay_Run_Slower_With_Ball(Player_States moving_ps, double prev_yl, double prev_v, int delay,
            string beforemsg = null, string aftermsg = null)
        {
            Action pas = new Action(Game_Object_Types.P, prev_yl, prev_v, prev_yl, prev_v, false, Player_States.STANDING, null, Movement.FAKE_MOVEMENT, null, false, delay);
            Action pas2 = new Action(Game_Object_Types.P, prev_yl, prev_v, Current_YardLine, Current_Vertical_Percent_Pos, true, moving_ps, null, Movement.LINE, Ball_Speed.CARRIED_SLOW, false, 0);

            if (beforemsg != null)
                pas.Effects.Add(new AE_rec() { XYIndex = 0, Announcer_msg = beforemsg });

            if (aftermsg != null)
                pas2.Effects.Add(new AE_rec() { XYIndex = pas2.PointXY.Count() - 1, Announcer_msg = aftermsg });

            Play_Stage pStage = new Play_Stage();
            pStage.Main_Object = false;
            pStage.Actions.Add(pas);
            pStage.Actions.Add(pas2);
            Stages.Add(pStage);
            State = moving_ps;
        }
        public void Run(Player_States moving_ps, double prev_yl, double prev_v)
        {
            Action pas = new Action(Game_Object_Types.P, prev_yl, prev_v, Current_YardLine, Current_Vertical_Percent_Pos, false, moving_ps, null, Movement.LINE, null, false, 0);
            Play_Stage pStage = new Play_Stage();
            pStage.Main_Object = false;
            pStage.Actions.Add(pas);
            Stages.Add(pStage);     
            State = moving_ps; 
        }

        public void Run_not_main(Player_States moving_ps, double prev_yl, double prev_v, int xyLimit)
        {
            Action pas = new Action(Game_Object_Types.P, prev_yl, prev_v, Current_YardLine, Current_Vertical_Percent_Pos, false, moving_ps, null, Movement.LINE, null, false, 0);

            //limit the number of xy points
            pas.PointXY = pas.PointXY.Take(xyLimit).ToList();
            Current_YardLine = pas.PointXY.Last().x;
            Current_Vertical_Percent_Pos = pas.PointXY.Last().y;

            Play_Stage pStage = new Play_Stage();
            pStage.Main_Object = false;
            pStage.Actions.Add(pas);
            Stages.Add(pStage);
            State = moving_ps;
        }

        public void Run_and_Tackled(Player_States moving_ps, double prev_yl, double prev_v, double crowd_adj)
        {
            Action pas = new Action(Game_Object_Types.P, prev_yl, prev_v, Current_YardLine, Current_Vertical_Percent_Pos, true, moving_ps, null, Movement.LINE, null, false, 0);
            Action pas2 = null;
            pas2 = new Action(Game_Object_Types.P, prev_yl, prev_v, Current_YardLine, Current_Vertical_Percent_Pos, true, Player_States.TACKLED, null, Movement.FAKE_MOVEMENT, null, false, 5);

            pas2.Effects.Add(new AE_rec() { XYIndex = 0, noise_adj = crowd_adj });

            Play_Stage pStage = new Play_Stage();
            pStage.Main_Object = true;
            pStage.Actions.Add(pas);
            pStage.Actions.Add(pas2);
            Stages.Add(pStage);
            State = Player_States.TACKLED;
        }

        public void Cover_Ball(Player_States moving_ps, double prev_yl, double prev_v, bool bOrigg_Ball_poss,
            string beforemsg = null, string aftermsg = null)
        {
            double crowd_adj = 0.30;
            crowd_adj = bOrigg_Ball_poss ? crowd_adj * -1 : crowd_adj;
            Action pas2 = null;
            pas2 = new Action(Game_Object_Types.P, prev_yl, prev_v, Current_YardLine, Current_Vertical_Percent_Pos, true, Player_States.TACKLED, null, Movement.FAKE_MOVEMENT, null, false, 24);

            pas2.Effects.Add(new AE_rec() { XYIndex = 0, Announcer_msg = beforemsg, noise_adj = crowd_adj });

            if (aftermsg != null)
                pas2.Effects.Add(new AE_rec() { XYIndex = pas2.PointXY.Count() - 1, Announcer_msg = aftermsg });

            Play_Stage pStage = new Play_Stage();
            pStage.Main_Object = true;
            pStage.Actions.Add(pas2);
            Stages.Add(pStage);
            State = Player_States.TACKLED;
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

            if (!bLast_Play && (Current_YardLine < -1.0 || Current_YardLine > 101.0))
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
            Action pas = new Action(Game_Object_Types.P, prev_yl, prev_v, Current_YardLine, Current_Vertical_Percent_Pos, true, Player_States.KNEELING, null, Movement.FAKE_MOVEMENT, null, true, 3);
            Play_Stage pStage = new Play_Stage();
            pStage.Main_Object = true;
            pStage.Actions.Add(pas);
            Stages.Add(pStage);
            State = Player_States.KNEELING;
        }

        public void Holder_Place_Ball(double prev_yl, double prev_v)
        {
            Action pas = new Action(Game_Object_Types.P, prev_yl, prev_v, Current_YardLine, Current_Vertical_Percent_Pos, true, Player_States.FG_HOLDER_PLACE_BALL, null, Movement.FAKE_MOVEMENT, null, true, 3);
            Play_Stage pStage = new Play_Stage();
            pStage.Main_Object = false;
            pStage.Actions.Add(pas);
            Stages.Add(pStage);
            State = Player_States.FG_HOLDER_PLACE_BALL;
        }

        public void Fall_On_Ball(double prev_yl, double prev_v, double crowd_adj,
            string beforemsg = null, string aftermsg = null)
        {
            int fakemovements = 10;

            Action pas = new Action(Game_Object_Types.P, prev_yl, prev_v, Current_YardLine, Current_Vertical_Percent_Pos, true, Player_States.FALL_ON_BALL, null, Movement.FAKE_MOVEMENT, null, true, fakemovements);
            pas.Effects.Add(new AE_rec() { XYIndex = 0, Announcer_msg = beforemsg, noise_adj = crowd_adj });

            if (aftermsg != null)
                pas.Effects.Add(new AE_rec() { XYIndex = pas.PointXY.Count() - 1, Announcer_msg = aftermsg });


            pas.Effects.Add(new AE_rec() { XYIndex = 0, Announcer_msg = beforemsg, noise_adj = crowd_adj });

            if (aftermsg != null)
                pas.Effects.Add(new AE_rec() { XYIndex = pas.PointXY.Count() - 1, Announcer_msg = aftermsg });

            Play_Stage pStage = new Play_Stage();
            pStage.Main_Object = true;
            pStage.Actions.Add(pas);
            Stages.Add(pStage);
            State = Player_States.FALL_ON_BALL;
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

            Action pas = new Action(Game_Object_Types.P, prev_yl, prev_v, Current_YardLine, Current_Vertical_Percent_Pos, true, moving_ps, null, Movement.LINE, null, false, 0);

            //Get the end point for the player running out of bounds
            new_end_point = PointPlotter.getExtendedEndpoint(prev_yl, prev_v, Current_YardLine, Current_Vertical_Percent_Pos, (app_Constants.OUT_OF_BOUNDS_LEN + app_Constants.TOP_OUTOFBOUNDS_ADJUSTMENT) * dlefttoRight);

            Action pas2 = null;
            pas2 = new Action(Game_Object_Types.P, Current_YardLine, Current_Vertical_Percent_Pos, new_end_point.x, new_end_point.y, true, moving_ps, null, Movement.LINE, null, false, 0);

            Play_Stage pStage = new Play_Stage();
            pStage.Main_Object = false;
            pStage.Actions.Add(pas);
            pStage.Actions.Add(pas2);
            Stages.Add(pStage);
            State = moving_ps;
        }

        public void Punter_Ready_for_Ball(double prev_yl, double prev_v)
        {
            Action pas = new Action(Game_Object_Types.P, prev_yl, prev_v, Current_YardLine, Current_Vertical_Percent_Pos, false, Player_States.PUNTER_READY, null, Movement.FAKE_MOVEMENT, null, true, 2);
            Play_Stage pStage = new Play_Stage();
            pStage.Main_Object = false;
            pStage.Actions.Add(pas);
            Stages.Add(pStage);
            State = Player_States.PUNTER_READY;
        }

        public void Crouch(double prev_yl, double prev_v, bool bmain)
        {
            Action pas = new Action(Game_Object_Types.P, prev_yl, prev_v, Current_YardLine, Current_Vertical_Percent_Pos, false, Player_States.CROUCH_BLOCK_DOWN, null, Movement.FAKE_MOVEMENT, null, true, 2);
            Play_Stage pStage = new Play_Stage();
            pStage.Main_Object = bmain;
            pStage.Actions.Add(pas);
            Stages.Add(pStage);
            State = Player_States.CROUCH_BLOCK_DOWN;
        }

        public void Crouch_Stand_Up(double prev_yl, double prev_v)
        {
            Action pas = new Action(Game_Object_Types.P, prev_yl, prev_v, Current_YardLine, Current_Vertical_Percent_Pos, false, Player_States.CROUCH_BLOCK_DOWN, null, Movement.FAKE_MOVEMENT, null, true, 2);
            Play_Stage pStage = new Play_Stage();
            pStage.Main_Object = false;
            pStage.Actions.Add(pas);
            Stages.Add(pStage);
            State = Player_States.CROUCH_BLOCK_UP;
        }

        public void Run_and_Punt(double prev_yl_1, double prev_v_1)
        {
            Action pasOne = new Action(Game_Object_Types.P, prev_yl_1, prev_v_1, prev_yl_1, prev_v_1, true, Player_States.PUNTER_READY, null, Movement.FAKE_MOVEMENT, null, true, 12);
            Action pas1 = new Action(Game_Object_Types.P, prev_yl_1, prev_v_1, Current_YardLine, Current_Vertical_Percent_Pos, true, Player_States.PUNTER_RUN, null, Movement.LINE, null, false, 0);
            Action pas = new Action(Game_Object_Types.P, Current_YardLine, Current_Vertical_Percent_Pos, Current_YardLine, Current_Vertical_Percent_Pos, true, Player_States.PUNTER_KICK, null, Movement.FAKE_MOVEMENT, null, true, 3); 
            Play_Stage pStage = new Play_Stage();
            pStage.Main_Object = true;
            pStage.Actions.Add(pasOne);
            pStage.Actions.Add(pas1);
            pStage.Actions.Add(pas);
            Stages.Add(pStage);
            State = Player_States.PUNTER_RUN;
        }
        public void Run_and_Punt_Free_Kick(double prev_yl_1, double prev_v_1,
            string beforemsg, string aftermsg)
        {
            Action pasOne = new Action(Game_Object_Types.P, prev_yl_1, prev_v_1, prev_yl_1, prev_v_1, true, Player_States.STANDING, null, Movement.FAKE_MOVEMENT, null, true, 5);
            Action pas1 = new Action(Game_Object_Types.P, prev_yl_1, prev_v_1, Current_YardLine, Current_Vertical_Percent_Pos, true, Player_States.PUNTER_RUN, Ball_States.CARRIED_SLOW, Movement.LINE, null, false, 0);
            Action pas2 = new Action(Game_Object_Types.P, Current_YardLine, Current_Vertical_Percent_Pos, Current_YardLine, Current_Vertical_Percent_Pos, true, Player_States.PUNTER_KICK, null, Movement.FAKE_MOVEMENT, null, true, 3);


            if (beforemsg != null)
                pas1.Effects.Add(new AE_rec() { XYIndex = 0, Announcer_msg = beforemsg });

            if (aftermsg != null)
                pas2.Effects.Add(new AE_rec() { XYIndex = pas2.PointXY.Count() - 1, Announcer_msg = aftermsg });

            pas2.Effects.Add(new AE_rec() { XYIndex = 0, Sound = Game_Sounds.KICK });

            Play_Stage pStage = new Play_Stage();
            pStage.Main_Object = true;
            pStage.Actions.Add(pasOne);
            pStage.Actions.Add(pas1);
            pStage.Actions.Add(pas2);
            Stages.Add(pStage);
            State = Player_States.PUNTER_RUN;
        }
        public void Run_and_TrytoBlockKick(Player_States moving_ps, double prev_yl_1, double prev_v_1)
        {
            Action pas = null;
            pas = new Action(Game_Object_Types.P, prev_yl_1, prev_v_1, Current_YardLine, Current_Vertical_Percent_Pos, false, moving_ps, null, Movement.LINE, null, false, 0);
            Action pas2 = new Action(Game_Object_Types.P, Starting_YardLine, Starting_Vertical_Percent_Pos, 0.0, 0.0, false, Player_States.BLOCK_KICK, null, Movement.NONE, null, false, 0);
            Play_Stage pStage = new Play_Stage();
            pStage.Main_Object = false;
            pStage.Actions.Add(pas);
            pStage.Actions.Add(pas2);
            Stages.Add(pStage);
            State = Player_States.BLOCK_KICK;
        }
        public void Punter_Put_Leg_Down(double crowd_adj,
            string beforemsg = null, string aftermsg = null)
        {
            Action pas = new Action(Game_Object_Types.P, Current_YardLine, Current_Vertical_Percent_Pos, Current_YardLine, Current_Vertical_Percent_Pos, true, Player_States.PUNTER_AFTER_KICK, null, Movement.FAKE_MOVEMENT, null, true, 3);
            pas.Effects.Add(new AE_rec() { XYIndex = 0, Announcer_msg = beforemsg, noise_adj = crowd_adj });

            if (aftermsg != null)
                pas.Effects.Add(new AE_rec() { XYIndex = pas.PointXY.Count() - 1, Announcer_msg = aftermsg });
            Play_Stage pStage = new Play_Stage();
            pStage.Main_Object = false;
            pStage.Actions.Add(pas);
            Stages.Add(pStage);
            State = Player_States.PUNTER_AFTER_KICK;
        }

        public void Kicker_Put_Leg_Down_and_Stand(string beforemsg = null, string aftermsg = null)
        {
            Action pas1 = new Action(Game_Object_Types.P, Current_YardLine, Current_Vertical_Percent_Pos, Current_YardLine, Current_Vertical_Percent_Pos, true, Player_States.PUNTER_AFTER_KICK, null, Movement.FAKE_MOVEMENT, null, true, 3);
            Action pas2 = new Action(Game_Object_Types.P, Starting_YardLine, Starting_Vertical_Percent_Pos, 0.0, 0.0, false, Player_States.STANDING, null, Movement.NONE, null, false, 0);
            pas1.Effects.Add(new AE_rec() { XYIndex = 0, Announcer_msg = beforemsg});

            if (aftermsg != null)
                pas2.Effects.Add(new AE_rec() { XYIndex = pas2.PointXY.Count() - 1, Announcer_msg = aftermsg });
            
            Play_Stage pStage = new Play_Stage();
            pStage.Main_Object = false;
            pStage.Actions.Add(pas1);
            pStage.Actions.Add(pas2);
            Stages.Add(pStage);
            State = Player_States.STANDING;
        }

        public void Punter_Put_Leg_Down_and_Run(Player_States moving_ps, double prev_yl, double prev_v,
            string beforemsg = null, string aftermsg = null)
        {
            Action pas1 = new Action(Game_Object_Types.P, prev_yl, prev_v, prev_yl, prev_v, true, Player_States.PUNTER_AFTER_KICK, null, Movement.FAKE_MOVEMENT, null, true, 3);
            Action pas2 = new Action(Game_Object_Types.P, prev_yl, prev_v, Current_YardLine, Current_Vertical_Percent_Pos, false, moving_ps, null, Movement.LINE, null, false, 0);

            pas1.Effects.Add(new AE_rec() { XYIndex = 0, Announcer_msg = beforemsg });

            if (aftermsg != null)
                pas2.Effects.Add(new AE_rec() { XYIndex = 0, Announcer_msg = aftermsg });

            Play_Stage pStage = new Play_Stage();
            pStage.Main_Object = false;
            pStage.Actions.Add(pas1);
            pStage.Actions.Add(pas2);
            Stages.Add(pStage);
            State = Player_States.PUNTER_AFTER_KICK;
        }

        public Tuple<bool, bool, bool, bool, bool> PuntReturnerActions(double ballx, double bally, bool bLast_Play,bool bLefttoRight)
        {
            var t = getReturnerAction(ballx, bally, bLast_Play, bLefttoRight);
            returnerWaitLocation(ballx, bally, t.Item1, t.Item2, t.Item3, t.Item4, t.Item5, bLefttoRight);

            return Tuple.Create(t.Item1, t.Item2, t.Item3, t.Item4, t.Item5 );
        }

        public Tuple<bool, bool, bool, bool, bool> getReturnerAction(double ballx, double bally, bool bLast_Play, bool bLefttoRight)
        {
            bool bOut_of_Bounds = false;
            bool bOut_of_Endzone = false;
            bool bKnell_with_Ball = false;
            bool bReturn = false;
            bool bDontField = false;

            double yardline = ballx;

            if (bLefttoRight && ballx >= app_Constants.FIELD_YARDS + app_Constants.ENDZONE_YARDS)
                bOut_of_Endzone = true;
            else if (!bLefttoRight && ballx <= -app_Constants.ENDZONE_YARDS)
                bOut_of_Endzone = true;
            else if (bally <= 0.0 || bally >= 100.0)
                bOut_of_Bounds = true;
            else if (bLast_Play)
                bReturn = true;
            else if (yardline > app_Constants.YARDS_FROM_GL_DECIDE_TO_RETURN && !bLefttoRight)
                bReturn = true;
            else if (yardline < (100 - app_Constants.YARDS_FROM_GL_DECIDE_TO_RETURN) && bLefttoRight)
                bReturn = true;
            else
            {
                if ((ballx >= 100 && bLefttoRight) || ballx <= 0 && !bLefttoRight)
                    bKnell_with_Ball = true;
                else
                    bDontField = true;
            }

            return Tuple.Create(bOut_of_Bounds, bOut_of_Endzone, bKnell_with_Ball, bReturn, bDontField);
        }

        public Tuple<bool, bool, bool> getKickoff_ReturnerAction(double ballx, bool bLast_Play,
            int rnd, bool bLefttoRight)
        {
            bool bOut_of_Endzone = false;
            bool bKnell_with_Ball = false;
            bool bReturn = false;

            double yardline = ballx;

            if (bLefttoRight && ballx >= app_Constants.FIELD_YARDS + app_Constants.ENDZONE_YARDS)
                bOut_of_Endzone = true;
            else if (!bLefttoRight && ballx <= -app_Constants.ENDZONE_YARDS)
                bOut_of_Endzone = true;
            else if (bLast_Play)
                bReturn = true;
            else
            {
                if ((ballx >= 101 && bLefttoRight) || ballx <= -1 && !bLefttoRight)
                    bKnell_with_Ball = true;
                else
                    bReturn = true;
            }

            return Tuple.Create(bOut_of_Endzone, bKnell_with_Ball, bReturn);
        }

        public void returnerWaitLocation(double ballx, double bally, bool bOut_of_Bounds, bool bOut_of_Endzone, bool bKnell_with_Ball, bool bReturn, bool bDontField, bool bLefttoRight)
        {
            if (bKnell_with_Ball || bReturn)
            {
                Current_YardLine = ballx;
                Current_Vertical_Percent_Pos = bally;
            }
            else if (bOut_of_Bounds)
            {
                Current_YardLine = ballx;
                double offsetY = Game_Engine_Helper.isBallvertTop(bally) ? app_Constants.PUNT_GROUP_VERT_DIST : 100 - app_Constants.PUNT_GROUP_VERT_DIST;
                Current_Vertical_Percent_Pos = offsetY;
            }
            else if (bDontField)
            {
                Current_YardLine = ballx;
                double offsetY = Game_Engine_Helper.isBallvertTop(bally) ? app_Constants.PUNT_GROUP_VERT_DIST : -app_Constants.PUNT_GROUP_VERT_DIST;
                Current_Vertical_Percent_Pos = bally + offsetY;
            }
            else if (bOut_of_Endzone)
            {
                Current_YardLine = bLefttoRight ? 105.0 : -5.0;
                Current_Vertical_Percent_Pos = bally;
            }
            else
            {
                Current_YardLine = ballx;
                double offsetY = Game_Engine_Helper.isBallvertTop(bally) ? 20.0 : -20.0;
                Current_Vertical_Percent_Pos = bally + offsetY;
            }

        }

        public double getMaxFGLen(long leg_stn)
        {
            double r = 0;
            const double MAX_FG_ATT_YARDS = 65.0;
            const int MIN_YARDS = 20;
            int i = 0;

            for (i = MIN_YARDS; i <= MAX_FG_ATT_YARDS; i++)
            {
                int upper_limit = 0;
                if (i < 30)
                    upper_limit = 20;
                else if (i < 35)
                    upper_limit = 25;
                else if (i < 40)
                    upper_limit = 42;
                else if (i < 45)
                    upper_limit = 47;
                else if (i < 50)
                    upper_limit = 50;
                else if (i < 55)
                    upper_limit = 70;
                else if (i < 60)
                    upper_limit = 80;
                else
                    upper_limit = 90;

                int rnd = CommonUtils.getRandomNum(1, upper_limit);
                if (leg_stn < rnd) break;

            }

            r = (double)i;

            return r;
        }

        public double getFGVert(double FG_Att_yrds)
        {
            double r = 0;
            double fg_cutoff = 40.0;
            const int UPPER_LIMIT = 325;
            const int MID_POINT = 50;
            const int FURTHEST_OUT = 5;
            int x_var = 0;

            long Leg_Accuracy = p_and_r.pr.First().Kicker_Leg_Accuracy_Rating;
            long leg_strength = p_and_r.pr.First().Kicker_Leg_Power_Rating;

            if (FG_Att_yrds > fg_cutoff)
                Leg_Accuracy -= (long) (FG_Att_yrds - fg_cutoff + .5);

            for (int i = MID_POINT; i >= FURTHEST_OUT; i--)
            {
                int rnd = CommonUtils.getRandomNum(1, UPPER_LIMIT);

                if (i == MID_POINT)
                    x_var = i;
                else
                {
                    bool bt = CommonUtils.getRandomTrueFalse();
                    if (bt)
                        x_var = i;
                    else
                        x_var = 100 - i;
                }

                if (rnd <= leg_strength)
                {
                    r = x_var;
                    break;
                }
            }    

            if (r == 0 )
            {
                bool b1 = CommonUtils.getRandomTrueFalse();

                if (b1)
                    r = FURTHEST_OUT;
                else
                    r = 100 - FURTHEST_OUT;
            }

            return r;
        }

    }


}
