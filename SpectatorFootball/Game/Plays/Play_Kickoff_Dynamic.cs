using log4net;
using SpectatorFootball.Common;
using SpectatorFootball.Enum;
using SpectatorFootball.Models;
using SpectatorFootball.NarrationAndText;
using SpectatorFootball.PlayNS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SpectatorFootball.GameNS
{


    public  class Play_Kickoff_Dynamic : iPlay
    {
        public Play_Enum Play { get; set; } = Play_Enum.KICKOFF_NORMAL;

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
        private string Play_Details = null;
        private Announcer _announcer = new Announcer();

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

                r = Finish_return_kickoff(Kickoff_Players, Return_Players, gBall, tackle_groups, r, bLefttoRight);
                r.Yards_Returned = Game_Engine_Helper.getKickoffReturnYards(!bLefttoRight, r.Kick_caught_yl, r.Returner.Current_YardLine);
            }

            //Set if touchdown
            r.bTouchDown = Game_Engine_Helper.isTouchdown(!bLefttoRight, r.Returner.Current_YardLine, r.bTouchback);

            if (r.Returner != null)
                r.end_of_play_yardline = r.Returner.Current_YardLine;

            //Create Player Stats Records for the play
            r.Play_Player_Stats = SetPlayerStats(Kickoff_Players, Return_Players, r.bTouchback, r.bKick_Out_of_Endzone, r.bTouchDown,
                r.bFumble, r.bFumble_Lost, r.Yards_Returned, r.Kicker, r.Returner, r.Tackler, r.Fumble_Recoverer,
                r.Missed_Tackles, r.Forced_Fumble_Tackler);

            return r;
        }
        private Play_Result Finish_return_kickoff(List<Game_Player> Kickoff_Players, List<Game_Player> Return_Players,
            Game_Ball gBall, List<List<int?>> tGroups, Play_Result r, bool bLefttoRight)
        {
            int slot_index, prev_slot_index = 0;
            List<int> Past_Blocker_Tackler_List = new List<int>();
            int id_Players = 0;
            int ind_close_Tklr = 0;

            slot_index = 2;
            double slot2_vert = gBall.Current_Vertical_Percent_Pos;
            bLefttoRight = Game_Engine_Helper.Switch_LefttoRight(bLefttoRight);

            Play_Details += "bLefttoRight: " + bLefttoRight + Environment.NewLine;

            List<int?> group = new List<int?>();
            for (int i = 1; i <= app_Constants.KICKOFF_DYNAMIC_TACKLING_GROUPS + 1; i++)
            {
                Play_Details += " T group num " + i + Environment.NewLine;
                Play_Details += "=======================================" + Environment.NewLine;

                bool bFindOpenSlot = false;
                double agility = r.Returner.p_and_r.pr.First().Agilty_Rating;
                bFindOpenSlot = ReturnerLookforHole(agility);

                int Tackler_Index = 0;
                double dbetweenVert = 0.0;

                bool bAnySlot = false;

                //For kicker tackler
                if (i > app_Constants.KICKOFF_DYNAMIC_TACKLING_GROUPS)
                {
                    group = new List<int?>() { null, null, null, null, null };
                    group[slot_index] = Kickoff_Formation.KickerIndex;
                }
                else
                    group = tGroups[i - 1];

                prev_slot_index = slot_index;
                slot_index = getKickoffReturnRunSlot(slot_index, bFindOpenSlot, group, bAnySlot);

//                Play_Details += "Slot: " + slot_index + " group " + string.Join(",", group) + Environment.NewLine;

                List<int> TB_List = new List<int>();  //tacklers/blocker around the returner

                double returner_swerve_vert = 0.0;
                double Breakthrough_len = 0.0;
                double Breakthrough_vert = 0.0;

                double returner_before_tackler_yardline = 0.0;
                double returner_before_tackler_vert = 0.0;
                double returner_hole_yl = 0.0;

                bool bSwereUp;

                if (group[slot_index] != null)
                {
                    Tackler_Index = (int)group[slot_index];
                    TB_List.Add(Tackler_Index);

                    dbetweenVert = app_Constants.KICKOFF_DYNAMIC_GROUP_VERT_DIST / 2.0;

                    bSwereUp = CommonUtils.getRandomTrueFalse();
                    if (bSwereUp)
                        dbetweenVert *= -1;

//                    Play_Details += "swerve " + dbetweenVert + Environment.NewLine;

                    int? adjacent_tackler = getPossibleUporDownTackler(bSwereUp, slot_index, group);
                    if (adjacent_tackler != null)
                        TB_List.Add((int)adjacent_tackler);
                }
                else
                {
                    //Since the returner is running to an open slot, let's see if the slot just above and below has potential tacklers
                    TB_List.AddRange(getPossibleAdjacentTacklers(slot_index, group));
                }

                if (TB_List.Count > 0)
                    ind_close_Tklr = getKickGroupPlayerInd(slot_index, group);

                //go thru the tacler/blocker list to determine if a tackle is made
                int b_list_ind = CommonUtils.getRandomIndex(TB_List.Count);
                for (int tb_xx = 0; tb_xx < TB_List.Count; tb_xx++)
                {
                    int tackler_ind = TB_List[b_list_ind];
                    block_result br = Game_Engine_Helper.Attempt_Block(true,
                        CommonUtils.getRandomNum(1, app_Constants.BLOCKING_MAX_RAND),
                        Return_Players[tackler_ind].p_and_r.pr.First().Pass_Block_Rating,
                        Return_Players[tackler_ind].p_and_r.pr.First().Run_Block_Rating,
                        Return_Players[tackler_ind].p_and_r.pr.First().Agilty_Rating,
                        Kickoff_Players[tackler_ind].p_and_r.pr.First().Pass_Attack_Rating,
                        Kickoff_Players[tackler_ind].p_and_r.pr.First().Run_Attack_Rating,
                        Kickoff_Players[tackler_ind].p_and_r.pr.First().Agilty_Rating,
                        Kickoff_Players[tackler_ind].p_and_r.pr.First().Speed_Rating);

                    long tackler_tackle_rating = Kickoff_Players[tackler_ind].p_and_r.pr.First().Tackle_Rating;
                    //adjust potential tackler's tackle rating based on the block
                    tackler_tackle_rating = Game_Engine_Helper.AdjustTackleRating_forBlock(br, tackler_tackle_rating);

                    //bpo test
                    //tackler_tackle_rating = 1;

                    bool bTack = Game_Engine_Helper.Make_Tackle(2.6,
                        r.Returner.p_and_r.pr.First().Speed_Rating,
                        r.Returner.p_and_r.pr.First().Agilty_Rating,
                        r.Returner.p_and_r.pr.First().Running_Power_Rating,
                        tackler_tackle_rating);

                    //bpo test
//                                      if (i <= 3)
//                                            bTack = false;
//                                        else
//                                            bTack = false; 
                    //********************

                    if (bTack)
                        r.Tackler = Kickoff_Players[tackler_ind];
                    else
                        r.Missed_Tackles.Add(Kickoff_Players[tackler_ind]);

                    if (r.Tackler != null)
                        break;

                    if (b_list_ind == TB_List.Count - 1)
                        b_list_ind = 0;
                    else
                        b_list_ind++;
                }

                if (TB_List.Count > 0)
                {
                    ind_close_Tklr = getKickGroupPlayerInd(slot_index, group);

                    returner_hole_yl = Kickoff_Players[ind_close_Tklr].Current_YardLine;
                    if (group[slot_index] != null)
                        returner_before_tackler_yardline = returner_hole_yl - (app_Constants.KICKOFF_DYNAMIC_YARDS_BEFORE_TACKLER * Game_Engine_Helper.HorizontalAdj(bLefttoRight));
                    else
                        returner_before_tackler_yardline = returner_hole_yl - (app_Constants.KICKOFF_DYNAMIC_YARDS_BEFORE_TACKLER2 * Game_Engine_Helper.HorizontalAdj(bLefttoRight));

                    returner_before_tackler_vert = slot2_vert + getKickoffGroupOffset(slot_index);

                    returner_swerve_vert = returner_before_tackler_vert + dbetweenVert;
                }

                if (i == 4)
                    Breakthrough_len = (Game_Engine_Helper.calcDistanceFromOpponentGL(returner_hole_yl, bLefttoRight) + 5.0) * Game_Engine_Helper.HorizontalAdj(bLefttoRight);
                else
                    Breakthrough_len = (app_Constants.KICKOFF_DYNAMIC_GROUP_1_MAX - app_Constants.KICKOFF_DYNAMIC_GROUP_1_MIN) + (app_Constants.KICKOFF_DYNAMIC_GROUP_2_MIN - app_Constants.KICKOFF_DYNAMIC_GROUP_1_MAX) * Game_Engine_Helper.HorizontalAdj(bLefttoRight);

                Breakthrough_vert = slot2_vert + getKickoffGroupOffset(slot_index);

//                Play_Details += "TB_List: " + string.Join(",", TB_List) + Environment.NewLine;
//                Play_Details += "Tackler " + r.Tackler + Environment.NewLine;
//                Play_Details += "Fumble " + r.bFumble + Environment.NewLine;

                id_Players = 0;
                foreach (Game_Player p in Return_Players)
                {
                    if (p == r.Returner)
                    {
                        string before_announcement = null;
                        if (i == 4)
                            before_announcement = _announcer.Announce_InPlay(announce_event.RETURN_KICKER_LEFT_TO_BEAT, r.Returner.p_and_r.p.Last_Name, Game_Engine_Helper.getYardlineDisplay(gBall.Current_YardLine));

                        if (TB_List.Count > 0)
                        {
                            double prev_yl = p.Current_YardLine;
                            double prev_v = p.Current_Vertical_Percent_Pos;

//                            Play_Details += "Returner before cut: " + " stage " + p.Stages.Count() + " " + prev_yl + " " + prev_v + Environment.NewLine;
//                            Play_Details += "ball     before cut: " + " stage " + gBall.Stages.Count() + " " + gBall.Current_YardLine + " " + gBall.Current_Vertical_Percent_Pos + Environment.NewLine;


                            p.Current_YardLine = returner_before_tackler_yardline;
                            p.Current_Vertical_Percent_Pos = returner_before_tackler_vert;

//                            Play_Details += "Returner cut one: " + " stage " + p.Stages.Count() + " " + p.Current_YardLine + " " + p.Current_Vertical_Percent_Pos + Environment.NewLine;

                            //must move the ball too, even thogh it will not be visible.
                            gBall.Current_YardLine = p.Current_YardLine;
                            gBall.Current_Vertical_Percent_Pos = p.Current_Vertical_Percent_Pos;

//                            Play_Details += "ball     cut one: " + " stage " + gBall.Stages.Count() + " " + gBall.Current_YardLine + " " + gBall.Current_Vertical_Percent_Pos + Environment.NewLine;

                            Player_States moving_ps = Game_Engine_Helper.setRunningState(bLefttoRight, true, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos, app_Constants.MOVEMENT_DIST_BEFORE_TURNING_BACK);
                            p.Run_With_Ball(moving_ps, prev_yl, prev_v, 0.0, before_announcement, null);

                            //for the ball
                            gBall.Carried(prev_yl, prev_v);

                            //he either gets tackled or not
                            prev_yl = p.Current_YardLine;
                            prev_v = p.Current_Vertical_Percent_Pos;

                            p.Current_YardLine = returner_hole_yl;
                            p.Current_Vertical_Percent_Pos = returner_swerve_vert;

//                            Play_Details += "Returner cut two: " + " stage " + p.Stages.Count() + " " + p.Current_YardLine + " " + p.Current_Vertical_Percent_Pos + Environment.NewLine;

                            //must move the ball too, even thogh it will not be visible.
                            gBall.Current_YardLine = p.Current_YardLine;
                            gBall.Current_Vertical_Percent_Pos = p.Current_Vertical_Percent_Pos;

//                            Play_Details += "gball   cut two: " + " stage " + gBall.Stages.Count() + " " + gBall.Current_YardLine + " " + p.Current_Vertical_Percent_Pos + Environment.NewLine;


                            Player_States moving_ps2 = Game_Engine_Helper.setRunningState(bLefttoRight, true, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos, app_Constants.MOVEMENT_DIST_BEFORE_TURNING_BACK);
                            if (r.Tackler != null)
                            {
                                double crowd_adj = 0.0;
                                if (i == 1)
                                    crowd_adj = 0.15;

                                p.Run_and_Tackled(moving_ps2, prev_yl, prev_v, crowd_adj);
                                gBall.Carried_Tackled(prev_yl, prev_v);
                            }
                            else
                            {
                                string after_announcment = null;
                                if (i == 4)  //if kicker tacker doesn't tackle then TD
                                {
                                    p.Current_YardLine += Breakthrough_len;
                                    gBall.Current_YardLine += Breakthrough_len;
                                    before_announcement = _announcer.Announce_InPlay(announce_event.RETURN_GOING_FOR_TD, r.Returner.p_and_r.p.Last_Name, Game_Engine_Helper.getYardlineDisplay(p.Current_YardLine));
                                }
                                else
                                    after_announcment = _announcer.Announce_InPlay(announce_event.BREAKTHRU_TACKLE, r.Returner.p_and_r.p.Last_Name, Game_Engine_Helper.getYardlineDisplay(p.Current_YardLine));

                                p.Run_With_Ball(moving_ps2, prev_yl, prev_v, -0.2, before_announcement, after_announcment);
                                //for the ball
                                gBall.Carried(prev_yl, prev_v);
                            }
                        }
                        else
                        {
                            double prev_yl = p.Current_YardLine;
                            double prev_v = p.Current_Vertical_Percent_Pos;

                            p.Current_YardLine += Breakthrough_len;
                            p.Current_Vertical_Percent_Pos = Breakthrough_vert;

                            gBall.Current_YardLine = p.Current_YardLine;
                            gBall.Current_Vertical_Percent_Pos = p.Current_Vertical_Percent_Pos;

                            Player_States moving_ps = Game_Engine_Helper.setRunningState(bLefttoRight, true, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos, app_Constants.MOVEMENT_DIST_BEFORE_TURNING_BACK);
                            p.Run_With_Ball(moving_ps, prev_yl, prev_v, -0.2, before_announcement, _announcer.Announce_InPlay(announce_event.RETURN_BREAKTHRU, r.Returner.p_and_r.p.Last_Name, Game_Engine_Helper.getYardlineDisplay(p.Current_YardLine)));

                            //for the ball
                            gBall.Carried(prev_yl, prev_v);
                        }
                    }
                    // Players from previous groups should still do what they last did not go back to blocking
                    else if (Past_Blocker_Tackler_List.Contains(id_Players))
                    {
                        p.Same_As_Last_Action();
                        if (TB_List.Count > 0)
                            p.Same_As_Last_Action();
                    }
                    else if (TB_List.Contains(id_Players))
                    {
                        p.Block(true);
                        p.Stand();
                    }
                    else
                    {
                        p.Block(true);

                        if (TB_List.Count > 0)
                            p.Block(true);
                    }
                    id_Players++;
                }

                Play_Details += "Ball stage index: " + (gBall.Stages.Count() - 1) + Environment.NewLine;

                bool bKicker = false;
                id_Players = 0;
                foreach (Game_Player p in Kickoff_Players)
                {
                    if (p == r.Kicker)
                        bKicker = true;
                    else
                        bKicker = false;

                    if (bKicker) Play_Details += "Starting loop for kicker" + Environment.NewLine;

                    if (p == r.Kicker && i != 4)  //In fourth group, kicker is tackler
                    {
                        if (bKicker) Play_Details += "kicker and i <> 4" + Environment.NewLine;

                        double prev_yl = p.Current_YardLine;
                        double prev_v = p.Current_Vertical_Percent_Pos;

                   //     Play_Details += "Kicker " + id_Players + " stage " + p.Stages.Count() + " before cut: " + prev_yl + " " + prev_v + Environment.NewLine;

                        if (TB_List.Count > 0)
                            p.Current_Vertical_Percent_Pos = returner_before_tackler_vert;
                        else
                            p.Current_Vertical_Percent_Pos = Breakthrough_vert;

                 //       Play_Details += "Kicker " + id_Players + " stage " + p.Stages.Count() + " cut one: " + p.Current_YardLine + " " + p.Current_Vertical_Percent_Pos + Environment.NewLine;

              //          Play_Details += "Going into kicker code" + Environment.NewLine;

                        if (prev_v == p.Current_Vertical_Percent_Pos)
                        {
                            p.Stand();
                            if (bKicker) Play_Details += "Kicker stand  " + Environment.NewLine;
                        }
                        else
                        {
                            if (bKicker) Play_Details += "not pre v == new current vert" + Environment.NewLine;
                            Player_States moving_ps = Game_Engine_Helper.setRunningState(bLefttoRight, false, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos, app_Constants.MOVEMENT_DIST_BEFORE_TURNING_BACK);
                            //                   p.Run(moving_ps, prev_yl, prev_v);

                            int ball_carrier_xyCount = Game_Helper.getTotXYPoints(gBall.Stages[p.Stages.Count()]);
                            p.Run_not_main(moving_ps, prev_yl, prev_v, ball_carrier_xyCount);
                            int player_xycount = Game_Helper.getTotXYPoints(p.Stages.Last());

                            if (bKicker) Play_Details += "ball xy count " + ball_carrier_xyCount + " kicker xy count: " + player_xycount + Environment.NewLine;

                            //========================

             //               Play_Details += "Stage Index: " + (p.Stages.Count() - 1) + Environment.NewLine;
             //               Play_Details += "Kicker run " + Game_Helper.getTotXYPoints(p.Stages.Last()) + Environment.NewLine;
             //               Play_Details += "ball xy " + Game_Helper.getTotXYPoints(gBall.Stages.Last()) + Environment.NewLine;

                        }



                        if (TB_List.Count > 0)
                        {
                            p.Stand();
                            if (bKicker) Play_Details += "Kicker stand tb list > 0  " + Environment.NewLine;

                        }
                    }
                    else if (TB_List.Contains(id_Players))
                    {
                        //keep blocking till the returner runs up to you
                        p.Block(false);

                        double prev_yl = p.Current_YardLine;
                        double prev_v = p.Current_Vertical_Percent_Pos;

//                        Play_Details += "Tackler " + id_Players + " stage " + p.Stages.Count() + " before cut: " + prev_yl + " " + prev_v + Environment.NewLine;

                        p.Current_YardLine = returner_hole_yl;
                        p.Current_Vertical_Percent_Pos = returner_swerve_vert;

//                        Play_Details += "Tackler " + id_Players + " stage " + p.Stages.Count() + " cut one: " + p.Current_YardLine + " " + p.Current_Vertical_Percent_Pos + Environment.NewLine;

                        Player_States moving_ps = Game_Engine_Helper.setRunningState(bLefttoRight, false, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos, app_Constants.MOVEMENT_DIST_BEFORE_TURNING_BACK);
                        int ball_carrier_xyCount = Game_Helper.getTotXYPoints(gBall.Stages[p.Stages.Count()]);
                        p.Attempt_the_Tackle(moving_ps, prev_yl, prev_v, ball_carrier_xyCount, app_Constants.TACKLER_FRAMES);
                    }
                    // Players from previous groups should still do what they last did not go back to blocking
                    else if (Past_Blocker_Tackler_List.Contains(id_Players))
                    {
                        p.Same_As_Last_Action();
                        if (TB_List.Count > 0)
                            p.Same_As_Last_Action();
                    }
                    else
                    {
                        p.Block(false);
                        //If there is a tackler then  continue to block while he attempts the tackle
                        if (TB_List.Count > 0)
                            p.Block(false);
                    }

                    //bpo test
                    if (bKicker)
                        Play_Details += "kicker stage index: " + (p.Stages.Count() - 1) + " xy count " + Game_Helper.getTotXYPoints(p.Stages.Last()) + Environment.NewLine;
                    //=================

                    id_Players++;
                }

                //if there is a tackle (not the kicker) then check if the ball is fumbled.
                if (r.Tackler != null && i != 4)
                {
                    long ball_safety_rating = r.Returner.p_and_r.pr.First().Ball_Safety_Rating;
                    long tackle_rating = r.Tackler.p_and_r.pr.First().Tackle_Rating;
                    long run_attack_rating = r.Tackler.p_and_r.pr.First().Run_Attack_Rating;

                    r.bFumble = Game_Engine_Helper.DoesBallCarrierFumble(
                               Ball_Carry_Actions.KICK_RETURN,
                               ball_safety_rating, tackle_rating, run_attack_rating);

                    //bpo test
                    //r.bFumble = true;

                    r.test_counter++;

                    //if there is a fumble then there can not be a tackle, but give the tackler
                    //creit for forcing the fumble
                    if (r.bFumble)
                    {
                        Play_Details += "Fumble! " + Environment.NewLine;

                        r.Forced_Fumble_Tackler = r.Tackler;
                        List<Game_Player> pFumble_Rec_Kickoff_Players = new List<Game_Player>();
                        List<Game_Player> pFumble_Rec_Return_Players = new List<Game_Player>();
                        List<int> closest_players = getKickoffGroupClosestPlayers(slot_index, group);
                        getBothGroupSlotPlayers(Kickoff_Players, Return_Players,
                            pFumble_Rec_Kickoff_Players, pFumble_Rec_Return_Players, closest_players);
                        pFumble_Rec_Return_Players.Add(r.Returner);
                        Tuple<Game_Player, bool> t = Playstub_Fumble.Execute(bLefttoRight, gBall,
                            Kickoff_Players, Return_Players,
                            pFumble_Rec_Kickoff_Players, pFumble_Rec_Return_Players,
                            r.Returner, r.Tackler, false, Fumble_OSKick_BlockPunt.FUMBLE);

                        r.Fumble_Recoverer = t.Item1;
                        r.bFumble_Lost = t.Item2;

                        //If there is a fumble then no tackle is awarded
                        r.Tackler = null;
                    }
                }

                //bpo test
                int ppp = 0;
                if ((r.Returner.Current_YardLine > 100.0 || r.Returner.Current_YardLine < 0) && r.bKick_Returned)
                    ppp = 1;

                //set td or not
                if (Game_Engine_Helper.isTouchdown(bLefttoRight, r.Returner.Current_YardLine, r.bTouchback))
                {
                    r.bTouchDown = true;
                    //handle case where the returner is tacked in the EZ
                    r.Tackler = null;
                    r.bFumble = false;
                    r.bFumble_Lost = false;
                    r.Fumble_Recoverer = null;
                    r.Forced_Fumble_Tackler = null;
                    break;
                }


                if (r.Tackler != null || r.bTouchback || r.bFumble)
                    break;

                Past_Blocker_Tackler_List.AddRange(group.Where(x => x != null).Select(x => (int)x).ToList());
            }  //on group 1,2,3 and 4

            return r;
        }

        public static int getKickGroupPlayerInd(int slot_index, List<int?> Group)
        {
            int r, g;
            List<int> Possible_Indexes = new List<int>();

            if (Group[slot_index] != null)
                Possible_Indexes.Add(slot_index);

            if (slot_index > 0 && Group[slot_index - 1] != null)
                Possible_Indexes.Add(slot_index - 1);

            if (slot_index < app_Constants.KICKOFF_DYNAMIC_PLAYERS_IN_GROUP - 1 && Group[slot_index + 1] != null)
                Possible_Indexes.Add(slot_index + 1);

            if (Possible_Indexes.Count == 0)
                throw new Exception("Could not find closest tacker in method getClosestKickGroupPlayerInd");

            int r_ind = CommonUtils.getRandomIndex(Possible_Indexes.Count());
            g = Possible_Indexes[r_ind];

            r = (int)Group[g];

            return r;
        }
        public static int? getPossibleUporDownTackler(bool bSwerveUp, int slot_index, List<int?> group)
        {
            int? r = null;

            if (bSwerveUp)
            {
                if (slot_index > 0) r = group[slot_index - 1];
            }
            else
            {
                if (slot_index < app_Constants.KICKOFF_DYNAMIC_PLAYERS_IN_GROUP - 1) r = group[slot_index + 1];
            }

            return r;
        }
        public static List<int> getPossibleAdjacentTacklers(int slot_index, List<int?> group)
        {
            List<int> r = new List<int>();

            //Check one spot above
            if (slot_index > 0)
            {
                int above_slot = slot_index - 1;
                if (group[above_slot] != null)
                    r.Add((int)group[above_slot]);
            }

            //Check one apot below
            if (slot_index < app_Constants.KICKOFF_DYNAMIC_PLAYERS_IN_GROUP - 1)
            {
                int below_slot = slot_index + 1;
                if (group[below_slot] != null)
                    r.Add((int)group[below_slot]);
            }

            return r;
        }
        public static double getKickoffGroupOffset(int ind)
        {
            double r = 0;

            ind -= 2;

            r = app_Constants.KICKOFF_DYNAMIC_GROUP_VERT_DIST * ind;

            return r;
        }
        public static void getBothGroupSlotPlayers(
            List<Game_Player> Kickoff_Players,
            List<Game_Player> Return_Players,
            List<Game_Player> pFumble_Rec_Kickoff_Players,
            List<Game_Player> pFumble_Rec_Return_Players,
            List<int> grpIndexes)
        {

            foreach (int i in grpIndexes)
            {
                pFumble_Rec_Kickoff_Players.Add(Kickoff_Players[i]);
                pFumble_Rec_Return_Players.Add(Return_Players[i]);
            }
        }

        public static List<Game_Player_Stats> SetPlayerStats(List<Game_Player> Kickoff_Players, List<Game_Player> Return_Players, bool bTouchback, bool bKicked_Out_of_Endzone, bool bTouchdown,
            bool bFumble, bool bFumble_Lost, double Yards,
            Game_Player Kicker, Game_Player Returner, Game_Player Tackler,
            Game_Player Forced_Fumble_Recoverer, List<Game_Player> Missed_Tackle,
            Game_Player Force_Fubmle)
        {
            long lTDs = bTouchdown ? 1 : 0;
            long lFubmle = bFumble ? 1 : 0;
            long lFubmle_Lost = bFumble_Lost ? 1 : 0;
            long lKickoff_out_of_Endzone = bKicked_Out_of_Endzone ? 1 : 0;

            List<Game_Player_Stats> r = new List<Game_Player_Stats>();

            //Set a play record for each player in the play
            foreach (Game_Player p in Kickoff_Players)
            {
                if (p == Kicker)
                    r.Add(new Game_Player_Stats() { Player_ID = Kicker.p_and_r.pr.First().Player_ID, Kickoffs = 1, kicker_plays = 1 });
                else
                    r.Add(new Game_Player_Stats() { Player_ID = p.p_and_r.pr.First().Player_ID, ko_def_plays = 1 });
            }

            foreach (Game_Player p in Return_Players)
            {
                if (p == Returner)
                    r.Add(new Game_Player_Stats() { Player_ID = Returner.p_and_r.pr.First().Player_ID, ko_ret_plays = 1 });
                else
                    r.Add(new Game_Player_Stats() { Player_ID = p.p_and_r.pr.First().Player_ID, ko_rec_plays = 1 });
            }

            //if touchback then set those stats
            if (bTouchback)
            {
                Game_Player_Stats ks = r.Where(x => x.Player_ID == Kicker.p_and_r.pr.First().Player_ID).First();
                ks.Kickoff_Touchbacks = 1;
            }

            if (bKicked_Out_of_Endzone)
            {
                Game_Player_Stats ks = r.Where(x => x.Player_ID == Kicker.p_and_r.pr.First().Player_ID).First();
                ks.Kickoff_Thru_Endzones = 1;
            }

            //set the returner stats
            Game_Player_Stats kr = r.Where(x => x.Player_ID == Returner.p_and_r.pr.First().Player_ID).First();
            kr.ko_ret = bTouchback ? 0 : 1;
            kr.ko_ret_TDs = lTDs;
            kr.ko_ret_fumbles = lFubmle;
            kr.ko_ret_fumbles_lost = lFubmle_Lost;
            kr.ko_ret_yards = (long)(Yards + .5);
            kr.ko_ret_yards_long = kr.ko_ret_yards;

            //Set Tackler stats
            if (Tackler != null)
            {
                Game_Player_Stats kt = r.Where(x => x.Player_ID == Tackler.p_and_r.pr.First().Player_ID).First();
                kt.ko_def_tackles = 1;

                //                if (lFubmle > 0)
                //                    kt.ko_def_Forced_Fumbles = 1;
            }

            //set missed tackles
            foreach (Game_Player m in Missed_Tackle)
            {
                Game_Player_Stats mt = r.Where(x => x.Player_ID == m.p_and_r.pr.First().Player_ID).First();
                mt.ko_def_tackles_missed = 1;
            }

            if (Force_Fubmle != null)
            {
                Game_Player_Stats fr = r.Where(x => x.Player_ID == Force_Fubmle.p_and_r.pr.First().Player_ID).First();
                fr.ko_def_Forced_Fumbles = 1;
            }

            //if there is a fumble and it is recovered give credit to the player that recovered it
            if (Forced_Fumble_Recoverer != null)
            {
                Game_Player_Stats fr = r.Where(x => x.Player_ID == Forced_Fumble_Recoverer.p_and_r.pr.First().Player_ID).First();
                fr.ko_fumbles_recovered = 1;
            }

            return r;
        }

        public static List<int> getKickoffGroupClosestPlayers(int slot_index, List<int?> group)
        {
            List<int> r = new List<int>();

            //Check one spot above
            if (slot_index > 0)
            {
                int above_slot = slot_index - 1;
                if (group[above_slot] != null)
                    r.Add((int)group[above_slot]);
            }

            //Check one apot below
            if (slot_index < app_Constants.KICKOFF_DYNAMIC_PLAYERS_IN_GROUP - 1)
            {
                int below_slot = slot_index + 1;
                if (group[below_slot] != null)
                    r.Add((int)group[below_slot]);
            }

            if (group[slot_index] != null)
                r.Add((int)group[slot_index]);

            return r;
        }

        public static int getKickoffReturnRunSlot(int slot_index, bool bLookforhole, List<int?> group, bool bAnyFive)
        {
            int r;
            List<int> empty_indexes = CommonUtils.GetIndexes(group, true);
            List<int> possible_indexes = new List<int>();
            if (bAnyFive)
            {
                if (bLookforhole && empty_indexes.Count() > 0)
                    possible_indexes = empty_indexes;
                else
                    possible_indexes = CommonUtils.GetIndexes(group, false);
            }
            else
            {
                if (bLookforhole) //you can go a max of 2 up or down
                {
                    foreach (int i in empty_indexes)
                    {
                        if (i >= slot_index - app_Constants.KICKOFF_AFTER_FIRST_GROUP_SLOT_VARIANCE && i <= slot_index + app_Constants.KICKOFF_AFTER_FIRST_GROUP_SLOT_VARIANCE)
                            possible_indexes.Add(i);
                    }
                    if (possible_indexes.Count() == 0)
                    {
                        for (int i = slot_index - app_Constants.KICKOFF_AFTER_FIRST_GROUP_SLOT_VARIANCE; i <= slot_index + app_Constants.KICKOFF_AFTER_FIRST_GROUP_SLOT_VARIANCE; i++)
                        {
                            if (i >= 0 && i < group.Count())
                                possible_indexes.Add(i);
                        }
                    }
                }
                else
                {
                    for (int i = slot_index - app_Constants.KICKOFF_AFTER_FIRST_GROUP_SLOT_VARIANCE; i <= slot_index + app_Constants.KICKOFF_AFTER_FIRST_GROUP_SLOT_VARIANCE; i++)
                    {
                        if (i >= 0 && i < group.Count())
                            possible_indexes.Add(i);
                    }
                }

            }

            int r_ind = CommonUtils.getRandomIndex(possible_indexes.Count());
            r = possible_indexes[r_ind];

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
        public static bool ReturnerLookforHole(double agility)
        {
            bool r = false;

            int agility_var = (int)agility - app_Constants.KICKOFF_AGILITY_CUTOFF;
            int r_agile = CommonUtils.getRandomNum(1, app_Constants.KICKOFF_AVOID_TRACKER_CALC_VARIABLE);
            if (r_agile <= agility_var)
                r = true;

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
                    p.KickBall(moving_ps, prev_yl, prev_v, Runup_end_yardline, Runup_end_vert_pos, _announcer.Announce_InPlay(announce_event.KICKOFF_KICKED, r.Kicker.p_and_r.p.Last_Name, Game_Engine_Helper.getYardlineDisplay(p.Current_YardLine)), null);

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

            Play_Details += ":kickoff_leng:" + Kickoff_Len;

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
                    {
                        double prev_yardline = gBall.Starting_YardLine;
                        double prev_vert = gBall.Starting_Vertical_Percent_Pos;
                        gBall.End_Over_End_Thru_Air(prev_yardline, prev_vert, bLefttoRight);
                    }
                    else
                        gBall.End_Over_End_Thru_Air_Caught();

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
                    string before_announcement = null;
                    before_announcement = _announcer.Announce_InPlay(announce_event.KICKOFF_CAUGHT, r.Returner.p_and_r.p.Last_Name, Game_Engine_Helper.getYardlineDisplay(gBall.Current_YardLine));

                    double initial_run_yards = 18.0;

                    p.Current_YardLine += initial_run_yards * Game_Engine_Helper.HorizontalAdj(bLefttoRight);

                    //must move the ball too, even thogh it will not be visible.
                    gBall.Current_YardLine = p.Current_YardLine;
                    gBall.Current_Vertical_Percent_Pos = p.Current_Vertical_Percent_Pos;

                    int ret_delay = 8;
                    Player_States moving_ps = Game_Engine_Helper.setRunningState(bLefttoRight, true, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos, 0.0);
                    p.Delay_Run_Slower_With_Ball(moving_ps, prev_yl, prev_v, ret_delay, before_announcement, null);
                    //for the ball
                    gBall.Delay_Carried_Slowly(prev_yl, prev_v, ret_delay);
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

            //bpo test
            string val_error = Play_Validator.Test_Last_Stage_for_NotMain_points(gBall.Stages.Count() - 1, gBall, Kickoff_Players, Return_Players);

            if (val_error == null && gBall.Stages.Count() > 1)
                val_error = Play_Validator.Test_Last_Stage_for_NotMain_points(gBall.Stages.Count() - 2, gBall, Kickoff_Players, Return_Players);


            //bpo to make sure that no stage has a mon main player with more xy points than the main
            //don't put this in production.
/*            if (val_error != null)
            {
                using (StreamWriter w = File.AppendText("c:\\data\\myFile.txt"))
                {
                    w.WriteLine(Play_Details);
                    w.WriteLine("xx stage: " + (gBall.Stages.Count() - 1) + " " + val_error);
                    w.WriteLine("");
                }
            }
*/
            //========================================================

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
