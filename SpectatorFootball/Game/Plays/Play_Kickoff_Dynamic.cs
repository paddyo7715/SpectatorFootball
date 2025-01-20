using log4net;
using SpectatorFootball.Enum;
using SpectatorFootball.GameNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.GameNS
{


    public  class Play_Kickoff_Dynamic : iPlay, IKickoff
    {
        public Play_Enum Play { get; set; } = Play_Enum.KICKOFF_NORMAL;
        public double kickoff_yl { get; set; } = 35;
        public double touchback_yl { get; set; } = 30;

        private static ILog logger = LogManager.GetLogger("RollingFile");

        private long Possessing_Team_Id;
        private long at;
        private long ht;
        private Game_Ball gBall;
        private List<Game_Player> Kickoff_Players;
        private List<Game_Player> Return_Players;
        private bool bLefttoRight;
        private bool FreeKic;
        private bool bLast_Play;
        private Formation Kickoff_Formation = null;
        private Formation Return_Formation = null;
        private Play_Result r = new Play_Result();

        public Play_Kickoff_Dynamic(Formation Kickoff_Formation, Formation Return_Formation, long Possessing_Team_Id, long at, long ht, Game_Ball gBall, List<Game_Player> Kickoff_Players, List<Game_Player> Return_Players, bool bLefttoRight, bool bLast_Play)
        {
            this.Possessing_Team_Id = Possessing_Team_Id;
            this.at = at;
            this.ht = ht;
            this.gBall = gBall;
            this.Kickoff_Players = Kickoff_Players;
            this.Return_Players = Return_Players;
            this.bLefttoRight = bLefttoRight;
            this.bLast_Play = bLast_Play;
            this.Kickoff_Formation = Kickoff_Formation;
            this.Return_Formation = Return_Formation;

            r.BallPossessing_Team_Id = Possessing_Team_Id == at ? ht : at;
            r.NonbBallPossessing_Team_Id = Possessing_Team_Id == at ? at : ht;
            r.at = at;
            r.ht = ht;
            r = setPlayerActions(Kickoff_Formation, Return_Formation, Kickoff_Players, Return_Players, r);
        }
        public Play_Result Execute(bool bPreSnapPenalty)
        {
            List<string> Play_Stages = new List<string>();
            double starting_yl = gBall.Current_YardLine;
            double starting_yardline = gBall.Current_YardLine;
            r.Play_Start_Yardline = starting_yardline;
            double retuner_catches_ball_yl = 0.0;

            Kicker_Runs_Up_And_Kicks_Ball(gBall, Kickoff_Players, Return_Players);

            gBall = getKickoff_Len_and_Vert(r.Kicker.p_and_r.pr.First().Kicker_Leg_Power_Rating,
                r.Kicker.p_and_r.pr.First().Kicker_Leg_Accuracy_Rating, gBall);

            var tackle_groups = Game_Engine_Helper.setTackleGroups(Kickoff_Players, r.Kicker);
            BallKicked(gBall, Kickoff_Players, Return_Players, r.Kicker, tackle_groups, r, bLast_Play, bLefttoRight);
            if (r.bKick_Returned)
            {
                r.Kick_caught_yl = gBall.Current_YardLine;
                r = return_initial(gBall, Kickoff_Players, Return_Players, tackle_groups, r, bLefttoRight);
                r.Yards_Returned = Game_Engine_Helper.getKickoffReturnYards(!bLefttoRight, r.Kick_caught_yl, r.Returner.Current_YardLine);
                //bpo stopped here

            }


            return r;
        }
        public bool isPreSnapPenalty_Eligible()
        {
            return false;
        }
        public bool isAccumeStats()
        {
            return true;
        }

        public Play_Result getPlayResult()
        {
            return r;
        }

        public static Play_Result setPlayerActions(Formation Kickoff_Formation, Formation Return_Formation,
     List<Game_Player> Kickoff_Players, List<Game_Player> Return_Players, Play_Result pResult)
        {
            Play_Result r = pResult;

            r.Kicker = Kickoff_Players[(int)Kickoff_Formation.KickerIndex];
            //Get the kicker - kicker and returner must be slot 5 in the formation
            r.Returner = Return_Players[(int)Return_Formation.ReturnerIndex];

            //for testing print out all the players and their relevant ratings
            logger.Debug("Kickoff Players");
            int d_index = 0;
            foreach (Game_Player p in Kickoff_Players)
            {
                if (p != r.Kicker)
                    r.Kick_Defenders.Add(p);

                string sPos = p.Pos.ToString();
                long leg_stn = p.p_and_r.pr.First().Kicker_Leg_Power_Rating;
                long leg_acc = p.p_and_r.pr.First().Kicker_Leg_Accuracy_Rating;
                long rn_att = p.p_and_r.pr.First().Run_Attack_Rating;
                logger.Debug("Ind:" + d_index +
                    " POS:" + sPos +
                    " Leg Strength:" + leg_stn +
                    " Leg Accuracy:" + leg_acc +
                    " Run Attack:" + rn_att
                    );
                d_index++;
            }
            logger.Debug("Receiving Players");
            d_index = 0;
            foreach (Game_Player p in Return_Players)
            {
                if (p != r.Returner)
                    r.Kick_Returners.Add(p);

                string sPos = p.Pos.ToString();
                long spd = p.p_and_r.pr.First().Speed_Rating;
                long agile = p.p_and_r.pr.First().Agilty_Rating;
                long rn_block = p.p_and_r.pr.First().Run_Block_Rating;
                long tkl = p.p_and_r.pr.First().Tackle_Rating;
                logger.Debug("Ind:" + d_index +
                    " POS:" + sPos +
                    " Spped:" + spd +
                    " Agility:" + agile +
                    " Run Blocking:" + rn_block +
                    " Tackling:" + tkl
                    );
                d_index++;
            }
            logger.Debug(" ");
            //=============================================

            return r;
        }

        private void Kicker_Runs_Up_And_Kicks_Ball(Game_Ball gBall, List<Game_Player> Kickoff_Players, List<Game_Player> Return_Players)
        {

            gBall.TeeUp();

            int io_Players = 0;
            //cycle thru the offensive/kickoff team then he defense
            //if kicker then do their special thing; otherwise, the player just remains standing 
            foreach (Game_Player p in Kickoff_Players)
            {
                if (p == r.Kicker)
                {
                    double prev_yl = p.Current_YardLine;
                    double prev_v = p.Current_Vertical_Percent_Pos;
                    double Runup_end_yardline = p.Current_YardLine += 5.4 * Game_Engine_Helper.HorizontalAdj(bLefttoRight);
                    double Runup_end_vert_pos = p.Current_Vertical_Percent_Pos += 0.0;

                    p.Current_YardLine = Runup_end_yardline + (0.4 * Game_Engine_Helper.HorizontalAdj(bLefttoRight));
                    p.Current_Vertical_Percent_Pos += 0.0;

                    Player_States moving_ps = Game_Engine_Helper.setRunningState(bLefttoRight, true, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos, app_Constants.MOVEMENT_DIST_BEFORE_TURNING_BACK);
                    p.KickBall(moving_ps, prev_yl, prev_v, Runup_end_yardline, Runup_end_vert_pos);

                }
                else
                {
                    //Other players just stand there waiting for the kick
                    p.Stand();
                }
                io_Players++;
            }

            //The team receiving the kick will just stand there before the kick
            foreach (Game_Player p in Return_Players)
            {
                //Receiving players just stand there waiting for the kick
                p.Stand();
            }

        }

        private Game_Ball getKickoff_Len_and_Vert(long leg_strength, long Leg_Accuracy, Game_Ball gBall)
        {

            KickOff_Length kick_length_enum = Kicking_Helper.getKickOff_Dynamic_Len_enum(leg_strength, Leg_Accuracy);

            double Kickoff_Len = Kicking_Helper.getKICKOFF_DYNAMIC_len(kick_length_enum);

            Kickoff_Verticl Kick_Vert_enum = Kicking_Helper.getKickoff_Vert_enum(Leg_Accuracy);
            double Kickoff_Vert = Kicking_Helper.getKickoff_Dynamic_Vert(Kick_Vert_enum);

            //Adjust the length of the kick based on the vertical
            Kickoff_Len = Kicking_Helper.AdjustKickLength(Kickoff_Len, Kickoff_Vert);

            //possision where ball should be caught
            gBall.Current_YardLine = gBall.Starting_YardLine + (Kickoff_Len * Game_Engine_Helper.HorizontalAdj(bLefttoRight));
            gBall.Current_Vertical_Percent_Pos = Kickoff_Vert;

            gBall.Current_YardLine = Kicking_Helper.SetMaxKickoffYardline(gBall.Current_YardLine);

            return gBall;
        }
        private void BallKicked(Game_Ball gBall, List<Game_Player> Kickoff_Players, List<Game_Player> Return_Players,
            Game_Player Kicker,  List<List<int?>> tGroups, Play_Result pr, bool bLast_Play, bool bLefttoRight)
        {

            double prevBallX = gBall.Current_YardLine;
            double prevBallY = gBall.Current_Vertical_Percent_Pos;
            pr.Kick_landing_YL = gBall.Current_YardLine;

            int delay_factor = 3;

            int tgroup_ind = 0;

            int id_Players = 0;
            foreach (Game_Player p in Kickoff_Players)
            {
                double yardline_Offset = 0.0;
                double vert_offset = 0.0;

                double prev_yl = p.Current_YardLine;
                double prev_v = p.Current_Vertical_Percent_Pos;

                if (p == r.Kicker)
                {
                    yardline_Offset = Math.Abs(gBall.Current_YardLine - p.Current_YardLine) - app_Constants.KICKOFF_DYNAMIC_KICKER_FROM_RETURNER;
                    vert_offset = gBall.Current_Vertical_Percent_Pos;

                    p.Current_YardLine += yardline_Offset * Game_Engine_Helper.HorizontalAdj(bLefttoRight);
                    p.Current_Vertical_Percent_Pos = vert_offset;

                    Player_States moving_ps = Game_Engine_Helper.setRunningState(bLefttoRight, true, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos, app_Constants.MOVEMENT_DIST_BEFORE_TURNING_BACK);
                    p.Run_Then_Stand(moving_ps, prev_yl, prev_v);
                }
                else
                {
                    //Other players just stand there waiting for the kick
                    p.Stand();
                }
                id_Players++;
            }

            id_Players = 0;
            foreach (Game_Player p in Return_Players)
            {
                double prev_yl = p.Current_YardLine;
                double prev_v = p.Current_Vertical_Percent_Pos;

                if (p == r.Returner)
                {
                    int rnd = CommonUtils.getRandomNum(1, 100);
                    var tRetAct = r.Returner.getKickoff_ReturnerAction(gBall.Current_YardLine, bLast_Play, rnd, bLefttoRight);

                    pr.bKick_Out_of_Endzone = tRetAct.Item1;
                    pr.bKick_KneelDown = tRetAct.Item2;
                    pr.bKick_Returned = tRetAct.Item3;

                    r.Returner.returnerWaitLocation(gBall.Current_YardLine, gBall.Current_Vertical_Percent_Pos,
                        false, pr.bKick_Out_of_Endzone, pr.bKick_KneelDown, pr.bKick_Returned, false, bLefttoRight);

                    if (pr.bKick_Out_of_Endzone || pr.bKick_KneelDown)
                        pr.bTouchback = true;

                    if (r.bKick_Out_of_Endzone)
                        gBall.End_Over_End_Thru_Air_Not_Caught(bLefttoRight);
                    else
                        gBall.End_Over_End_Thru_Air();

                    Player_States moving_ps = Game_Engine_Helper.setRunningState(bLefttoRight, false, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos, app_Constants.MOVEMENT_DIST_BEFORE_TURNING_BACK);
                    if (pr.bKick_Out_of_Endzone)
                        p.Run_Then_Stand(moving_ps, prev_yl, prev_v);
                    else if (pr.bKick_KneelDown)
                        p.Run_Then_CatchKick(moving_ps, prev_yl, prev_v);
                    else if (pr.bKick_Returned)
                        p.Run_Then_CatchKick(moving_ps, prev_yl, prev_v);
                }
                else
                {
                    //Other players just stand there waiting for the kick
                    p.Stand();
                }
                id_Players++;
            }

            //If the returner kneels then we need to do antoher stage
            if (pr.bKick_KneelDown)
            {
                foreach (Game_Player p in Kickoff_Players)
                    p.Same_As_Last_Action();

                foreach (Game_Player p in Return_Players)
                {
                    if (p == r.Returner)
                    {
                        p.Kneel_With_Ball(p.Current_YardLine, p.Current_Vertical_Percent_Pos);

                        //for the ball
                        gBall.Carried_Fake_Movement(1);
                    }
                    else
                        p.Same_As_Last_Action();
                }
            }
        }
        private Play_Result return_initial(Game_Ball gBall, List<Game_Player> Kickoff_Players, List<Game_Player> Return_Players,
           List<List<int?>> tGroups, Play_Result pr, bool bLefttoRight)
        {
            double prevBallX = gBall.Current_YardLine;
            double prevBallY = gBall.Current_Vertical_Percent_Pos;
            pr.Kick_landing_YL = gBall.Current_YardLine;

            int delay_factor = 1;

            int tgroup_ind = 0;

            //First the blockers attackers get into position while the return starts to return the  ball
            int id_Players = 0;
            foreach (Game_Player p in Kickoff_Players)
            {
                double yardline_Offset = 0.0;
                double vert_offset = 0.0;

                double prev_yl = p.Current_YardLine;
                double prev_v = p.Current_Vertical_Percent_Pos;

                if (p == r.Kicker)
                {
                    yardline_Offset = Math.Abs(gBall.Current_YardLine - p.Current_YardLine) - app_Constants.KICKOFF_DYNAMIC_KICKER_FROM_RETURNER;
                    vert_offset = gBall.Current_Vertical_Percent_Pos;

                    p.Current_YardLine += yardline_Offset * Game_Engine_Helper.HorizontalAdj(bLefttoRight);
                    p.Current_Vertical_Percent_Pos = vert_offset;

                    Player_States moving_ps = Game_Engine_Helper.setRunningState(bLefttoRight, true, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos, app_Constants.MOVEMENT_DIST_BEFORE_TURNING_BACK);
                    p.Run_Then_Stand(moving_ps, prev_yl, prev_v);
                }
                else
                {
                    tgroup_ind = Game_Engine_Helper.getTackleGroup(id_Players, tGroups);
                    int slot = tGroups[tgroup_ind].IndexOf(id_Players);

                    p.Current_YardLine += getAttBlkyl(gBall.Current_YardLine, p.Current_YardLine, tgroup_ind) * Game_Engine_Helper.HorizontalAdj(bLefttoRight);
                    p.Current_Vertical_Percent_Pos = getAttBlkVert(gBall.Current_Vertical_Percent_Pos, slot);
                    int delay = tgroup_ind * delay_factor + 1;
                    Player_States moving_ps = Game_Engine_Helper.setRunningState(bLefttoRight, true, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos, app_Constants.MOVEMENT_DIST_BEFORE_TURNING_BACK);
                    p.Run_Then_Stand(moving_ps, prev_yl, prev_v);
                }
                id_Players++;
            }

            bLefttoRight = Game_Engine_Helper.Switch_LefttoRight(bLefttoRight);

            id_Players = 0;
            foreach (Game_Player p in Return_Players)
            {
                double prev_yl = p.Current_YardLine;
                double prev_v = p.Current_Vertical_Percent_Pos;

                if (p == r.Returner)
                {
                    double initial_run_yards = 24.0;

                    p.Current_YardLine += initial_run_yards * Game_Engine_Helper.HorizontalAdj(bLefttoRight);

                    //must move the ball too, even thogh it will not be visible.
                    gBall.Current_YardLine = p.Current_YardLine;
                    gBall.Current_Vertical_Percent_Pos = p.Current_Vertical_Percent_Pos;

                    int ret_delay = 10;
                    Player_States moving_ps = Game_Engine_Helper.setRunningState(bLefttoRight, true, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos, 0.0);
                    p.Delay_Run_Slower_With_Ball(moving_ps, prev_yl, prev_v, ret_delay);
                    //for the ball
                    gBall.Delay_Carried(prev_yl, prev_v, ret_delay);
                }
                else
                {
                    p.Current_YardLine = Kickoff_Players[id_Players].Current_YardLine + (app_Constants.KICKOFF_DYNAMIC_DIST_BETWEEN_BLOCK_ATTACHERS * Game_Engine_Helper.HorizontalAdj(!bLefttoRight));
                    p.Current_Vertical_Percent_Pos = Kickoff_Players[id_Players].Current_Vertical_Percent_Pos;
                    tgroup_ind = Game_Engine_Helper.getTackleGroup(id_Players, tGroups);
                    int delay = tgroup_ind * delay_factor + 1;
                    Player_States moving_ps = Game_Engine_Helper.setRunningState(bLefttoRight, false, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos, app_Constants.MOVEMENT_DIST_BEFORE_TURNING_BACK);
                    //                    p.Delay_Then_Run_and_Stand(moving_ps, prev_yl, prev_v, delay);
                    p.Run_Then_Stand(moving_ps, prev_yl, prev_v);
                }
                id_Players++;
            }
            return pr;
        }

        public double getAttBlkyl(double ballx, double playerx, int group)
        {
            double r = 0;
            int min_yl, max_yl = 0;

//            ballx += 30.0;
            
            switch (group)
            {
                case 0:
                    min_yl = app_Constants.KICKOFF_DYNAMIC_GROUP_1_MIN;
                    max_yl = app_Constants.KICKOFF_DYNAMIC_GROUP_1_MAX;
                    break;
                case 1:
                    min_yl = app_Constants.KICKOFF_DYNAMIC_GROUP_2_MIN;
                    max_yl = app_Constants.KICKOFF_DYNAMIC_GROUP_2_MAX;
                    break;
                case 2:
                    min_yl = app_Constants.KICKOFF_DYNAMIC_GROUP_3_MIN;
                    max_yl = app_Constants.KICKOFF_DYNAMIC_GROUP_3_MAX;
                    break;
                default:
                    throw new Exception("Error in method getAttBlkyl, unknow group index on normal kickoff " + group);
            }

            int yard_off_returner = CommonUtils.getRandomNum(min_yl, max_yl) + app_Constants.KICKOFF_DYNAMIC_GROUP_1_MIN;
            r = Math.Abs(ballx - playerx) - yard_off_returner;

            return r;
        }
        public double getAttBlkVert(double bally, int slot)
        {
            double r = 0;
            double too_low = 90.0;
            double too_high = 10.0;

            slot -= 2;

            if (bally < too_high)
                bally = 21.0;
            else if (bally > too_low)
                bally = 79.0;

            r = bally + app_Constants.KICKOFF_DYNAMIC_GROUP_VERT_DIST * slot;

            return r;
        }
    }
}
