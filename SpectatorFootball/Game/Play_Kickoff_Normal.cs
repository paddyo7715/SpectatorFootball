using log4net;
using SpectatorFootball.Enum;
using SpectatorFootball.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.GameNS
{
    class Play_Kickoff_Normal
    {
        private static ILog logger = LogManager.GetLogger("RollingFile");

        public static Play_Result Execute(Game_Ball gBall, List<Game_Player> Kickoff_Players, List<Game_Player> Return_Players, bool bLefttoRight, bool FreeKic, bool bSim, bool bLast_Play)
        {
            Play_Result r = new Play_Result();
            List<string> Play_Stages = new List<string>();
            r.Tackler = null;
            r.Kicker = Kickoff_Players[app_Constants.KICKER_INDEX];
            //Get the kicker - kicker and returner must be slot 5 in the formation
            r.Returner = Return_Players[app_Constants.RETURNER_INDEX];
            List<Game_Player> Missed_Tackles = new List<Game_Player>();
            double retuner_catches_ball_yl = 0.0;

            //for testing print out all the players and their relevant ratings
            logger.Debug("bLefttoRight: " + bLefttoRight.ToString());
            logger.Debug(" ");

            logger.Debug("Kickoff Players");
            int d_index = 0;
            foreach (Game_Player p in Kickoff_Players)
            {
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

            //================================  Stage One =======================================
            logger.Debug("Stage 1");
            logger.Debug("=====================================================");
            //================ Kicker Runs up to the ball and kicks it ==========================
            if (!bSim)
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

                    if (!bSim)
                    {
                        Player_States moving_ps = Game_Engine_Helper.setRunningState(bLefttoRight, true, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos);
                        p.KickBall(moving_ps, prev_yl, prev_v, Runup_end_yardline, Runup_end_vert_pos);
                    }
                }
                else
                {
                    //Other players just stand there waiting for the kick
                    if (!bSim)
                        p.Stand();
                }
                io_Players++;
            }

            //The team receiving the kick will just stand there before the kick
            foreach (Game_Player p in Return_Players)
            {
                //Receiving players just stand there waiting for the kick
                if (!bSim)
                    p.Stand();
            }
            //===== End of Stage One - Kicker Runs up to the ball and kicks it ================
            logger.Debug("=======================================================");
            logger.Debug("");
            //================================  Stage Two =======================================
            //================ The ball flies thru the air and the players run ==================
            logger.Debug("Stage 2");
            logger.Debug("=======================================================");

            //Now determine how far and straight the kick is
            long leg_strength = r.Kicker.p_and_r.pr.First().Kicker_Leg_Power_Rating;
            KickOff_Length kick_length_enum = Kicking_Helper.getKickOff_Len_enum(leg_strength);
            double Kickoff_Len = Kicking_Helper.getKickoff_len(kick_length_enum);

            long leg_accuracy = r.Kicker.p_and_r.pr.First().Kicker_Leg_Accuracy_Rating;
            KickOff_Verticl Kick_Vert_enum = Kicking_Helper.getKickoff_Vert_enum(leg_accuracy);

            double Kickoff_Vert = Kicking_Helper.getKickoff_Vert(Kick_Vert_enum);

            logger.Debug("Kicker Leg Strength:" + leg_strength + " Leg Accuracy:" + leg_accuracy);
            logger.Debug("Kickoff length enum:" + kick_length_enum.ToString());
            logger.Debug("Kickoff length:" + Kickoff_Len.ToString());
            logger.Debug("Kickoff Vert enum:" + Kick_Vert_enum.ToString());
            logger.Debug("Kickoff Vert:" + Kickoff_Vert.ToString());

            //Adjust the length of the kick based on the vertical
            Kickoff_Len = Kicking_Helper.AdjustKickLength(Kickoff_Len, Kickoff_Vert);

            logger.Debug("Adjusted Kickoff len:" + Kickoff_Len.ToString());

            //possision where ball should be caught
            gBall.Current_YardLine = gBall.Starting_YardLine + (Kickoff_Len * Game_Engine_Helper.HorizontalAdj(bLefttoRight));
            gBall.Current_Vertical_Percent_Pos = Kickoff_Vert;

            gBall.Current_YardLine = Kicking_Helper.SetMaxKickoffYardline(gBall.Current_YardLine);

            retuner_catches_ball_yl = retuner_catches_ball_yl;

            r.bKickoff_Out_of_Endzone = r.Returner.isKickOutofEndzone(gBall.Current_YardLine);
            if (r.bKickoff_Out_of_Endzone)
                r.bTouchback = true;
  
            if (!bSim)
            {
                if (r.bTouchback)
                    gBall.End_Over_End_Thru_Air_Not_Caught(bLefttoRight);
                else
                    gBall.End_Over_End_Thru_Air();
            }

            //decide which players are in group 1 (closest to returner( group 2 and group 3
            List<int?> group_1 = new List<int?>();
            List<int?> group_2 = new List<int?>();
            List<int?> group_3 = new List<int?>();
            int id_Players = 0;
            foreach (Game_Player p in Kickoff_Players)
            {
                long speed = p.p_and_r.pr.First().Speed_Rating;
                string debug_string = null;
                if (p != r.Kicker)
                {
                    int group_var = (int)speed - app_Constants.KICKOFF_SPEED_CUTOFF;
                    debug_string += "group_var:" + group_var + " ";
                    int temp1 = CommonUtils.getRandomNum(1, app_Constants.KICKOFF_GROUP_CALC_VARIABLE);
                    debug_string += "temp1:" + temp1 + " ";
                    if (temp1 <= group_var)
                    {
                        group_1.Add(id_Players);
                        debug_string += "added to group 1";
                    }
                    else
                    {
                        int temp2 = CommonUtils.getRandomNum(1, app_Constants.KICKOFF_GROUP_CALC_VARIABLE);
                        debug_string += "temp2:" + temp2 + " ";
                        if (temp2 <= group_var)
                        {
                            group_2.Add(id_Players);
                            debug_string += "added to group 2";
                        }
                        else
                        {
                            group_3.Add(id_Players);
                            debug_string += "added to group 3";
                        }
                    }
                    logger.Debug("Player slot:" + id_Players + " Speed:" + speed + " " + debug_string);
                }

                id_Players++;
            }

            logger.Debug("Groups before rearranging");
            logger.Debug("Group 1:" + string.Join(",", group_1.ToArray()));
            logger.Debug("Group 2:" + string.Join(",", group_2.ToArray()));
            logger.Debug("Group 3:" + string.Join(",", group_3.ToArray()));
            //Adjust the group lists if any one of them has > 5 members
            //emoveRandomIndexes(int Desired_size, List<int> lst)
            List<int?> g1_deletes = removeRandomIndexes(group_1);
            group_2.AddRange(g1_deletes);
            List<int?> g3_deletes = removeRandomIndexes(group_3);
            group_2.AddRange(g3_deletes);
            List<int?> g2_deletes = removeRandomIndexes(group_2);
            group_3.AddRange(g2_deletes);

            group_1 = group_1.OrderBy(x => x).ToList();
            group_2 = group_2.OrderBy(x => x).ToList();
            group_3 = group_3.OrderBy(x => x).ToList();

            logger.Debug("Groups after rearranging");
            logger.Debug("Group 1:" + string.Join(",", group_1.ToArray()));
            logger.Debug("Group 2:" + string.Join(",", group_2.ToArray()));
            logger.Debug("Group 3:" + string.Join(",", group_3.ToArray()));

            //Expand out the group lists
            group_1 = ExpandGroup(group_1);
            group_2 = ExpandGroup(group_2);
            group_3 = ExpandGroup(group_3);

            if (group_1.Count() > app_Constants.KICKOFF_PLAYERS_IN_GROUP ||
                group_2.Count() > app_Constants.KICKOFF_PLAYERS_IN_GROUP ||
                group_3.Count() > app_Constants.KICKOFF_PLAYERS_IN_GROUP)
                throw new Exception("Group list with more than " + app_Constants.KICKOFF_PLAYERS_IN_GROUP);

            double yardline_Offset;
            double vert_offset;

            id_Players = 0;
            logger.Debug("Kickoff Players:");
            foreach (Game_Player p in Kickoff_Players)
            {
                if (p == r.Kicker)
                {
                    yardline_Offset = Math.Abs(gBall.Current_YardLine - p.Current_YardLine) - app_Constants.KICKOFF_KICKER_FROM_RETURNER;
                    vert_offset = gBall.Current_Vertical_Percent_Pos;
                }
                else
                {
                    //which group is the play in
                    if (group_1.Contains(id_Players))
                    {
                        int yard_off_returner = CommonUtils.getRandomNum(app_Constants.KICKOFF_GROUP_1_MIN, app_Constants.KICKOFF_GROUP_1_MAX) + app_Constants.KICKOFF_GROUP_1_MIN;
                        yardline_Offset = Math.Abs(gBall.Current_YardLine - p.Current_YardLine) - yard_off_returner;
                        int group_index = group_1.IndexOf(id_Players);
                        vert_offset = gBall.Current_Vertical_Percent_Pos + getKickoffGroupOffset(group_index);

                        string sPos = p.Pos.ToString();
                        long spd = p.p_and_r.pr.First().Speed_Rating;
                        logger.Debug("Ind:" + id_Players +
                            " POS:" + sPos +
                            " target_YardLine:" + yardline_Offset * Game_Engine_Helper.HorizontalAdj(bLefttoRight) +
                            " target vertical:" + vert_offset
                            );
                    }
                    else if (group_2.Contains(id_Players))
                    {
                        int yard_off_returner = CommonUtils.getRandomNum(app_Constants.KICKOFF_GROUP_2_MIN, app_Constants.KICKOFF_GROUP_2_MAX) + app_Constants.KICKOFF_GROUP_1_MIN;
                        yardline_Offset = Math.Abs(gBall.Current_YardLine - p.Current_YardLine) - yard_off_returner;
                        int group_index = group_2.IndexOf(id_Players);
                        vert_offset = gBall.Current_Vertical_Percent_Pos + getKickoffGroupOffset(group_index);
                    }
                    else
                    {
                        int yard_off_returner = CommonUtils.getRandomNum(app_Constants.KICKOFF_GROUP_3_MIN, app_Constants.KICKOFF_GROUP_3_MAX) + app_Constants.KICKOFF_GROUP_1_MIN;
                        yardline_Offset = Math.Abs(gBall.Current_YardLine - p.Current_YardLine) - yard_off_returner;
                        int group_index = group_3.IndexOf(id_Players);
                        vert_offset = gBall.Current_Vertical_Percent_Pos + getKickoffGroupOffset(group_index);
                    }
                }

                double prev_yl = p.Current_YardLine;
                double prev_v = p.Current_Vertical_Percent_Pos;
                p.Current_YardLine += yardline_Offset * Game_Engine_Helper.HorizontalAdj(bLefttoRight);
                p.Current_Vertical_Percent_Pos = vert_offset;

                if (!bSim)
                {
                    Player_States moving_ps = Game_Engine_Helper.setRunningState(bLefttoRight, true, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos);
                    p.Run_Then_Stand(moving_ps, prev_yl, prev_v);
                }
                id_Players++;
            }

            id_Players = 0;
            foreach (Game_Player p in Return_Players)
            {
                //The player who will return the ball
                if (p == r.Returner)
                {
                    double prev_yl = p.Current_YardLine;
                    double prev_v = p.Current_Vertical_Percent_Pos;

                    if (r.bKickoff_Out_of_Endzone)
                    {
                        bool babove = CommonUtils.getRandomTrueFalse();
                        double vrt = 0.0;
                        double yt = 0.0;
                        if (babove)
                            vrt = app_Constants.KICKOFF_RETURNER_VERT_STANDBY * -1;
                        else
                            vrt = app_Constants.KICKOFF_RETURNER_VERT_STANDBY;
                        yt = gBall.Current_YardLine + (app_Constants.KICK_OUT_OF_ENDZONE_YARD * Game_Engine_Helper.HorizontalAdj(!bLefttoRight));

                        p.Current_YardLine = yt;
                        p.Current_Vertical_Percent_Pos = gBall.Current_Vertical_Percent_Pos;

                        if (!bSim)
                        {
                            Player_States moving_ps = Game_Engine_Helper.setRunningState(bLefttoRight, false, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos);
                            p.Run_Then_Stand(moving_ps, prev_yl, prev_v);
                        }
                    }
                    else
                    {
                        p.Current_YardLine = gBall.Current_YardLine;
                        p.Current_Vertical_Percent_Pos = gBall.Current_Vertical_Percent_Pos;

                        if (!bSim)
                        {
                            Player_States moving_ps = Game_Engine_Helper.setRunningState(bLefttoRight, false, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos);
                            p.Run_Then_CatchKick(moving_ps, prev_yl, prev_v);
                        }
                    }

                }
                else
                {
                    //Receiving players 
                    double prev_yl = p.Current_YardLine;
                    double prev_v = p.Current_Vertical_Percent_Pos;

                    p.Current_YardLine = Kickoff_Players[id_Players].Current_YardLine + (app_Constants.KICKOFF_DIST_BETWEEN_BLOCK_ATTACHERS * Game_Engine_Helper.HorizontalAdj(bLefttoRight));
                    p.Current_Vertical_Percent_Pos = Kickoff_Players[id_Players].Current_Vertical_Percent_Pos;
                    if (!bSim)
                    {
                        Player_States moving_ps = Game_Engine_Helper.setRunningState(bLefttoRight, false, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos);
                        p.Run_Then_Stand(moving_ps, prev_yl, prev_v);
                    }
                }
                id_Players++;
            }
            //===== End of Stage Two - The ball flies thru the air and the players run ============
            logger.Debug("=======================================================");
            logger.Debug("");

            if (!r.bKickoff_Out_of_Endzone)
            {
                //============================  Stage Three, four and five ==========================
                //==== Returner runs to group 1,2 and 3 other players block or attempt to tackle ====
                //Note that offensive team are now the tacklers or the returner could choose to not
                //return the ball.
                //            bool bTackled = false;
                double agility = r.Returner.p_and_r.pr.First().Agilty_Rating;
                double returner_catch_vert = r.Returner.Current_Vertical_Percent_Pos;
                int slot_index = 2;
                int prev_slot_index;
                List<int> Past_Blocker_Tackler_List = new List<int>();
                logger.Debug("Stage 3, 4 or 5");
                logger.Debug("=======================================================");
                //since the returner will catch the ball switch blefttoright

                bLefttoRight = Game_Engine_Helper.Switch_LefttoRight(bLefttoRight);

                //Should the kick be returned
                r.bTouchback = r.Returner.isTouchback(bLast_Play, group_1);

                List<int?> group = new List<int?>();
                for (int i = 1; i <= app_Constants.KICKOFF_TACKLING_GROUPS; i++)
                {
                    switch (i)
                    {
                        case 1:
                            group = group_1;
                            break;
                        case 2:
                            group = group_2;
                            break;
                        case 3:
                            group = group_3;
                            break;
                    }

                    logger.Debug("Group:" + i);
                    logger.Debug("Group List:" + string.Join(",", group));
                    logger.Debug("Group Count:" + group.Count());
                    logger.Debug("=======================================================");

                    bool bFindOpenSlot = false;
                    bFindOpenSlot = ReturnerLookforHole(agility);

                    logger.Debug("bFindOpenSlot:" + bFindOpenSlot.ToString());

                    int Tackler_Index = 0;
                    double dbetweenVert = 0.0;

                    bool bAnySlot = i == 1 ? true : false;
                    prev_slot_index = slot_index;
                    logger.Debug("bAnySlot: " + bAnySlot.ToString());
                    slot_index = getKickReturnRunSlot(slot_index, bFindOpenSlot, group, bAnySlot);

                    logger.Debug("slot_index: " + slot_index.ToString());

                    List<int> TB_List = new List<int>();  //tacklers/blocker around the returner

                    double returner_swerve_vert = 0.0;
                    double Breakthrough_len = 0.0;
                    double Breakthrough_vert = 0.0;

                    double returner_before_tackler_yardline = 0.0;
                    double returner_before_tackler_vert = 0.0;
                    double returner_hole_yl = 0.0;
                    double outofBound_yl = 0.0;
                    double outofBound_vert = 0.0;

                    bool bSwereUp;
                    if (!r.bTouchback) //if there is a touchback then no need to see if there will be tacklers
                    {
                        if (group[slot_index] != null)
                        {
                            logger.Debug("Tackler in this slot");

                            Tackler_Index = (int)group[slot_index];
                            TB_List.Add(Tackler_Index);

                            dbetweenVert = app_Constants.KICKOFF_GROUP_VERT_DIST / 2.0;

                            bSwereUp = CommonUtils.getRandomTrueFalse();
                            if (bSwereUp)
                                dbetweenVert *= -1;
                            int? adjacent_tackler = getPossibleUporDownTackler(bSwereUp, slot_index, group);
                            if (adjacent_tackler != null)
                                TB_List.Add((int)adjacent_tackler);
                        }
                        else
                        {
                            logger.Debug("The slot is empty");
                            //Since the returner is running to an open slot, let's see if the slot just above and below has potential tacklers
                            TB_List.AddRange(getPossibleAdjacentTacklers(slot_index, group));
                        }

                        if (TB_List.Count > 0)
                        {
                            int ind_close_Tklr = getClosestKickGroupPlayerInd(slot_index, group);
                            //Will the returner run out of bounds
                            r.bRunOutofBounds = r.Returner.Kickoff_GoOutofBounds(bLast_Play, CommonUtils.getRandomTrueFalse(), slot_index, r.Returner.Current_Vertical_Percent_Pos);
                            if (r.bRunOutofBounds)
                            {
                                r.bRunOutofBounds = true;
                                outofBound_yl = Kickoff_Players[ind_close_Tklr].Current_YardLine;
                                outofBound_vert = r.Returner.VertForNearestSideline(r.Returner.Current_Vertical_Percent_Pos);
                            }
                        }

                        if (!r.bRunOutofBounds)
                        {
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

                                bool bTack = Game_Engine_Helper.Make_Tackle(
                                    r.Returner.p_and_r.pr.First().Speed_Rating,
                                    r.Returner.p_and_r.pr.First().Agilty_Rating,
                                    r.Returner.p_and_r.pr.First().Running_Power_Rating,
                                    tackler_tackle_rating);

                                if (bTack)
                                    r.Tackler = Kickoff_Players[tackler_ind];
                                else
                                    Missed_Tackles.Add(Kickoff_Players[tackler_ind]);

                                if (r.Tackler != null)
                                    break;

                                if (b_list_ind == TB_List.Count - 1)
                                    b_list_ind = 0;
                                else
                                    b_list_ind++;
                            }

                            if (TB_List.Count > 0)
                            {
                                int ind_close_Tklr = getClosestKickGroupPlayerInd(slot_index, group);

                                //the runner will run one yard before the tackler and then swerve up or down

                                returner_hole_yl = Kickoff_Players[ind_close_Tklr].Current_YardLine;

                                if (group[slot_index] != null)
                                    returner_before_tackler_yardline = returner_hole_yl - (app_Constants.KICKOFF_YARDS_BEFORE_TACKLER * Game_Engine_Helper.HorizontalAdj(bLefttoRight));
                                else
                                    returner_before_tackler_yardline = returner_hole_yl - (app_Constants.KICKOFF_YARDS_BEFORE_TACKLER2 * Game_Engine_Helper.HorizontalAdj(bLefttoRight));

                                returner_before_tackler_vert = returner_catch_vert + getKickoffGroupOffset(slot_index);
                                returner_swerve_vert = returner_before_tackler_vert + dbetweenVert;


                                logger.Debug("ind_close_Tklr: " + ind_close_Tklr + "  Kickoff_Players[ind_close_Tklr].Current_YardLine: " + Kickoff_Players[ind_close_Tklr].Current_YardLine);
                                logger.Debug("returner_before_tackler_yardline: " + returner_before_tackler_yardline + "  returner_before_tackler_vert: " + returner_before_tackler_vert);
                                logger.Debug("dbetweenVert: " + dbetweenVert + "  returner_swerve_vert: " + returner_swerve_vert);
                                logger.Debug("returner_swerve_vert: " + returner_swerve_vert);
                            }
                            else
                            {
                                Breakthrough_vert = returner_catch_vert + getKickoffGroupOffset(slot_index);
                                Breakthrough_len = (app_Constants.KICKOFF_GROUP_1_MAX - app_Constants.KICKOFF_GROUP_1_MIN) + (app_Constants.KICKOFF_GROUP_2_MIN - app_Constants.KICKOFF_GROUP_1_MAX) * Game_Engine_Helper.HorizontalAdj(bLefttoRight);

                            }

                            logger.Debug("Past_Blocker_Tackler_List: " + string.Join(",", Past_Blocker_Tackler_List.ToArray()));
                        }
                    }

                    id_Players = 0;
                    foreach (Game_Player p in Kickoff_Players)
                    {
                        if (p == r.Kicker)  //Kicker mirrors the vert of the ball carrier
                        {
                            double prev_yl = p.Current_YardLine;
                            double prev_v = p.Current_Vertical_Percent_Pos;

                            if (TB_List.Count > 0)
                                p.Current_Vertical_Percent_Pos = returner_before_tackler_vert;
                            else
                                p.Current_Vertical_Percent_Pos = Breakthrough_vert;

                            if (!bSim)
                            {
                                Player_States moving_ps = Game_Engine_Helper.setRunningState(bLefttoRight, false, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos);
                                p.Run(moving_ps, prev_yl, prev_v);

                                if (TB_List.Count > 0 && !r.bRunOutofBounds)
                                    p.Stand();
                            }

                        }
                        else if (TB_List.Contains(id_Players))
                        {
                            //keep blocking till the returner runs up to you
                            if (!bSim)
                                p.Block();

                            double prev_yl = p.Current_YardLine;
                            double prev_v = p.Current_Vertical_Percent_Pos;

                            p.Current_YardLine = returner_hole_yl;
                            p.Current_Vertical_Percent_Pos = returner_swerve_vert;

                            //Move vertically to make the tackle
                            if (!bSim && !r.bRunOutofBounds)
                            {
                                Player_States moving_ps = Game_Engine_Helper.setRunningState(bLefttoRight, false, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos);
                                p.Attempt_Tackle(moving_ps, prev_yl, prev_v);
                            }
                        }
                        // Players from previous groups should still do what they last did not go back to blocking
                        else if (Past_Blocker_Tackler_List.Contains(id_Players))
                        {
                            if (!bSim)
                            {
                                p.Same_As_Last_Action();
                                if (TB_List.Count > 0 && !r.bRunOutofBounds)
                                    p.Same_As_Last_Action();
                            }
                        }
                        else
                        {
                            if (!bSim)
                            {
                                p.Block();
                                //If there is a tackler then  continue to block while he attempts the tackle
                                if (TB_List.Count > 0 && !r.bRunOutofBounds)
                                    p.Block();
                            }
                        }
                        id_Players++;
                    }

                    id_Players = 0;
                    foreach (Game_Player p in Return_Players)
                    {
                        if (p == r.Returner)  //Kick Returner
                        {
                            logger.Debug("Returner: ID " + id_Players);
                            if (r.bTouchback)
                            {
                                if (!bSim)
                                {
                                    p.Kneel_With_Ball(p.Current_YardLine, p.Current_Vertical_Percent_Pos);

                                    //for the ball
                                    gBall.Carried_Fake_Movement(1);
                                }
                            }
                            else if (r.bRunOutofBounds)
                            {
                                logger.Debug("Returner Runs out of Bounds:");

                                double prev_yl = p.Current_YardLine;
                                double prev_v = p.Current_Vertical_Percent_Pos;

                                p.Current_YardLine = outofBound_yl;
                                p.Current_Vertical_Percent_Pos = outofBound_vert;

                                gBall.Current_YardLine = p.Current_YardLine;
                                gBall.Current_Vertical_Percent_Pos = p.Current_Vertical_Percent_Pos;

                                if (!bSim)
                                {
                                    Player_States moving_ps = Game_Engine_Helper.setRunningState(bLefttoRight, true, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos);
                                    p.Run_and_GoOut_of_Bounds(moving_ps, prev_yl, prev_v, bLefttoRight);

                                    //for the ball
                                    gBall.Carried_Out_of_Bounds(prev_yl, prev_v, bLefttoRight);
                                }
                            }
                            else if (TB_List.Count > 0)
                            {
                                logger.Debug("Returner runs up to tackler:");

                                double prev_yl = p.Current_YardLine;
                                double prev_v = p.Current_Vertical_Percent_Pos;

                                p.Current_YardLine = returner_before_tackler_yardline;
                                p.Current_Vertical_Percent_Pos = returner_before_tackler_vert;

                                //must move the ball too, even thogh it will not be visible.
                                gBall.Current_YardLine = p.Current_YardLine;
                                gBall.Current_Vertical_Percent_Pos = p.Current_Vertical_Percent_Pos;

                                logger.Debug("prev_yl:" + prev_yl.ToString() + " prev_v:" + prev_v.ToString());
                                logger.Debug("Current_YardLine:" + p.Current_YardLine.ToString() + " Current_Vertical_Percent_Pos:" + p.Current_Vertical_Percent_Pos.ToString());
                                logger.Debug("returner_before_tackler_yardline:" + returner_before_tackler_yardline + " returner_before_tackler_vert:" + returner_before_tackler_vert);

                                if (!bSim)
                                {
                                    Player_States moving_ps = Game_Engine_Helper.setRunningState(bLefttoRight, true, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos);
                                    p.Run(moving_ps, prev_yl, prev_v);

                                    //for the ball
                                    gBall.Carried(prev_yl, prev_v);
                                }

                                //he either gets tackled or not
                                prev_yl = p.Current_YardLine;
                                prev_v = p.Current_Vertical_Percent_Pos;

                                p.Current_YardLine = returner_hole_yl;
                                p.Current_Vertical_Percent_Pos = returner_swerve_vert;

                                //must move the ball too, even thogh it will not be visible.
                                gBall.Current_YardLine = p.Current_YardLine;
                                gBall.Current_Vertical_Percent_Pos = p.Current_Vertical_Percent_Pos;

                                logger.Debug("prev_yl:" + prev_yl.ToString() + " prev_v:" + prev_v.ToString());
                                logger.Debug("Current_YardLine:" + p.Current_YardLine.ToString() + " Current_Vertical_Percent_Pos:" + p.Current_Vertical_Percent_Pos.ToString());
                                logger.Debug("returner_hole_yl:" + returner_hole_yl + " returner_swerve_vert:" + returner_swerve_vert);


                                if (!bSim)
                                {
                                    Player_States moving_ps = Game_Engine_Helper.setRunningState(bLefttoRight, true, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos);
                                    if (r.Tackler != null)
                                        p.Run_and_Tackled(moving_ps, prev_yl, prev_v);
                                    else
                                        p.Run(moving_ps, prev_yl, prev_v);

                                    //for the ball
                                    gBall.Carried(prev_yl, prev_v);
                                }
                            }
                            else
                            {
                                logger.Debug("Returner Breaks Thru:");

                                double prev_yl = p.Current_YardLine;
                                double prev_v = p.Current_Vertical_Percent_Pos;

                                p.Current_YardLine += Breakthrough_len;
                                p.Current_Vertical_Percent_Pos = Breakthrough_vert;

                                gBall.Current_YardLine = p.Current_YardLine;
                                gBall.Current_Vertical_Percent_Pos = p.Current_Vertical_Percent_Pos;

                                logger.Debug("prev_yl:" + prev_yl.ToString() + " prev_v:" + prev_v.ToString());
                                logger.Debug("Current_YardLine:" + p.Current_YardLine.ToString() + " Current_Vertical_Percent_Pos:" + p.Current_Vertical_Percent_Pos.ToString());

                                if (!bSim)
                                {
                                    Player_States moving_ps = Game_Engine_Helper.setRunningState(bLefttoRight, true, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos);
                                    p.Run(moving_ps, prev_yl, prev_v);

                                    //for the ball
                                    gBall.Carried(prev_yl, prev_v);
                                }
                            }
                        }
                        // Players from previous groups should still do what they last did not go back to blocking
                        else if (Past_Blocker_Tackler_List.Contains(id_Players))
                        {
                            if (!bSim)
                            {
                                p.Same_As_Last_Action();
                                if (TB_List.Count > 0 && !r.bRunOutofBounds)
                                    p.Same_As_Last_Action();
                            }
                        }
                        else if (TB_List.Contains(id_Players))
                        {
                            logger.Debug("Blocker");

                            if (!bSim)
                            {
                                p.Block();

                                if (!r.bRunOutofBounds)
                                p.Stand();
                            }
                        }
                        else
                        {
                            if (!bSim)
                            {
                                p.Block();

                                if (TB_List.Count > 0 && !r.bRunOutofBounds)
                                    p.Block();
                            }
                        }
                        id_Players++;
                    }

                    //bpo test code take it out
                    //bTackled = true;
                    //=========================

                    //if there is a tackle then check if the ball is fumbled.
                    if (r.Tackler != null)
                    {
                        long ball_safety_rating = r.Returner.p_and_r.pr.First().Ball_Safety_Rating;
                        long tackle_rating = r.Tackler.p_and_r.pr.First().Tackle_Rating;
                        long run_attack_rating = r.Tackler.p_and_r.pr.First().Run_Attack_Rating;

                        r.bFumble = Game_Engine_Helper.DoesBallCarrierFumble(
                                   Ball_Carry_Actions.KICK_RETURN,
                                   ball_safety_rating, tackle_rating, run_attack_rating);

                        //if there is a fumble then there can not be a tackle, but give the tackler
                        //creit for forcing the fumble
                        if (r.bFumble)
                        {
                            r.Forced_Fumble_Tackler = r.Tackler;
                            List<Game_Player> pFumble_Rec_Kickoff_Players = new List<Game_Player>();
                            List<Game_Player> pFumble_Rec_Return_Players = new List<Game_Player>();
                            List<int> closest_players = getkickoffGroupClosestPlayers(slot_index, group);
                            getBothGroupSlotPlayers(Kickoff_Players, Return_Players,
                                pFumble_Rec_Kickoff_Players, pFumble_Rec_Return_Players, closest_players);
                            pFumble_Rec_Return_Players.Add(r.Returner);
                            r.Fumble_Recoverer = Playstub_Fumble.Execute(bLefttoRight, gBall,
                                Kickoff_Players, Return_Players,
                                pFumble_Rec_Kickoff_Players, pFumble_Rec_Return_Players,
                                r.Returner, r.Tackler, bSim);
                            
                            //If there is a fumble then no tackle is awarded
                            r.Tackler = null;
                        }

                        //Next pass in the players how could recover the fumble and get back
                        //the player that recovers the ball.  I can then determine if the fumble
                        //was lost and assign credit to the players
                    }

                    if (r.Tackler != null || r.bTouchback || r.bRunOutofBounds || r.bFumble)
                        break;


                    Past_Blocker_Tackler_List.AddRange(group.Where(x => x != null).Select(x => (int)x).ToList());
                }  //on group 1,2 or 3

                //===== End of Stage Three,four or five - the returner runs thru groups 1,2 and 3 ============
                logger.Debug("=======================================================");
                logger.Debug("");


                //==== Stage Six Kicker tries to tackle the returner if the returner makes it this far =====
                logger.Debug("Stage 6");
                logger.Debug("=======================================================");

                if (r.Tackler == null && !r.bTouchback && !r.bRunOutofBounds && !r.bFumble)
                {
                    logger.Debug("Stage Six:");
                    bool bFindOpenSlot = false;
                    bFindOpenSlot = ReturnerLookforHole(agility);
                    logger.Debug("bFindOpenSlot:" + bFindOpenSlot.ToString());
                    double returner_swerve_vert = 0;
                    bool bTackler = false;

                    //The index of the kicker doesn't matter
                    List<int?> g_list = new List<int?>() { null, null, null, null, null };
                    g_list[slot_index] = app_Constants.KICKER_INDEX;
                    logger.Debug("slot_index:" + slot_index);

                    //bpo test
                    //slot_index = 2;
                    double BreakAwayYardline = 0.0;
                    double Breakthrough_vert = 0.0;
                    double returner_before_tackler_yardline = 0.0;
                    double returner_before_tackler_vert = 0.0;
                    double returner_hole_yl = 0.0;

                    if (bLefttoRight)
                        BreakAwayYardline = 105.0;
                    else
                        BreakAwayYardline = -5.0;
                    //                Breakthrough_vert = returner_catch_vert + getKickoffGroupOffset(slot_index);

                    double dbetweenVert = 0.0;
                    logger.Debug("bTackler true - slot index:" + slot_index);
                    bTackler = true;
                    long tackler_tackle_rating = r.Kicker.p_and_r.pr.First().Tackle_Rating;
                    bool bTackled = Game_Engine_Helper.Make_Tackle(
                            r.Returner.p_and_r.pr.First().Speed_Rating,
                            r.Returner.p_and_r.pr.First().Agilty_Rating,
                            r.Returner.p_and_r.pr.First().Running_Power_Rating,
                            tackler_tackle_rating);

                    if (bTackled)
                    {
                        r.Tackler = r.Kicker;
                        logger.Debug("Tackle Made!");
                    }
                    else
                        Missed_Tackles.Add(r.Kicker);

                    dbetweenVert = app_Constants.KICKOFF_GROUP_VERT_DIST / 2.0;

                    if (CommonUtils.getRandomTrueFalse())
                        dbetweenVert *= -1;

                    returner_hole_yl = r.Kicker.Current_YardLine;

                    if (g_list[slot_index] != null)
                        returner_before_tackler_yardline = returner_hole_yl - (app_Constants.KICKOFF_YARDS_BEFORE_TACKLER2 * Game_Engine_Helper.HorizontalAdj(bLefttoRight));
                    else
                        returner_before_tackler_yardline = returner_hole_yl - (app_Constants.KICKOFF_YARDS_BEFORE_TACKLER3 * Game_Engine_Helper.HorizontalAdj(bLefttoRight));

                    returner_before_tackler_vert = returner_catch_vert + getKickoffGroupOffset(slot_index);
                    returner_swerve_vert = returner_before_tackler_vert + dbetweenVert;

                    if (r.Tackler != null)
                        returner_hole_yl = r.Kicker.Current_YardLine;
                    else
                        returner_hole_yl = BreakAwayYardline;

                    logger.Debug("Slot: " + slot_index + "Curent vert: " + r.Kicker.Current_Vertical_Percent_Pos + " getKickoffGroupOffset:" + getKickoffGroupOffset(slot_index));


                    id_Players = 0;
                    foreach (Game_Player p in Kickoff_Players)
                    {
                        if (p == r.Kicker)
                        {
                            logger.Debug("Kicker Tacker:");

                            if (bTackler)
                            {
                                if (!bSim)
                                    p.Stand();

                                double prev_yl = p.Current_YardLine;
                                double prev_v = p.Current_Vertical_Percent_Pos;

                                p.Current_Vertical_Percent_Pos = returner_swerve_vert;

                                //Move vertically to make the tackle
                                if (!bSim)
                                {
                                    Player_States moving_ps = Game_Engine_Helper.setRunningState(bLefttoRight, false, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos);
                                    p.Attempt_Tackle(moving_ps, prev_yl, prev_v);
                                }
                            }
                        }
                        else if (Past_Blocker_Tackler_List.Contains(id_Players))
                        {
                            if (!bSim)
                            {
                                p.Same_As_Last_Action();
                                if (bTackler)
                                    p.Same_As_Last_Action();
                            }
                        }
                        else
                        {
                            if (!bSim)
                            {
                                p.Block();

                                //If there is a tackler then  continue to block while he attempts the tackle
                                if (bTackler)
                                    p.Block();
                            }
                        }
                        id_Players++;
                    }

                    id_Players = 0;
                    foreach (Game_Player p in Return_Players)
                    {
                        if (p == r.Returner)  //Kick Returner
                        {
                            logger.Debug("Returner: ID " + id_Players);
                            if (bTackler)
                            {
                                logger.Debug("Returner runs up to tackler:");

                                double prev_yl = p.Current_YardLine;
                                double prev_v = p.Current_Vertical_Percent_Pos;

                                p.Current_YardLine = returner_before_tackler_yardline;
                                p.Current_Vertical_Percent_Pos = returner_swerve_vert;

                                //must move the ball too, even thogh it will not be visible.
                                gBall.Current_YardLine = p.Current_YardLine;
                                gBall.Current_Vertical_Percent_Pos = p.Current_Vertical_Percent_Pos;

                                logger.Debug("prev_yl:" + prev_yl.ToString() + " prev_v:" + prev_v.ToString());
                                logger.Debug("Current_YardLine:" + p.Current_YardLine.ToString() + " Current_Vertical_Percent_Pos:" + p.Current_Vertical_Percent_Pos.ToString());
                                logger.Debug("returner_before_tackler_yardline:" + returner_before_tackler_yardline + " returner_before_tackler_vert:" + returner_before_tackler_vert);

                                if (!bSim)
                                {
                                    Player_States moving_ps = Game_Engine_Helper.setRunningState(bLefttoRight, true, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos);
                                    p.Run(moving_ps, prev_yl, prev_v);

                                    //for the ball
                                    gBall.Carried(prev_yl, prev_v);
                                }

                                //he either gets tackled or not
                                prev_yl = p.Current_YardLine;
                                prev_v = p.Current_Vertical_Percent_Pos;

                                p.Current_YardLine = returner_hole_yl;
                                //                          p.Current_Vertical_Percent_Pos = returner_swerve_vert;

                                //must move the ball too, even thogh it will not be visible.
                                gBall.Current_YardLine = p.Current_YardLine;
                                gBall.Current_Vertical_Percent_Pos = p.Current_Vertical_Percent_Pos;

                                logger.Debug("prev_yl:" + prev_yl.ToString() + " prev_v:" + prev_v.ToString());
                                logger.Debug("Current_YardLine:" + p.Current_YardLine.ToString() + " Current_Vertical_Percent_Pos:" + p.Current_Vertical_Percent_Pos.ToString());
                                logger.Debug("returner_hole_yl:" + returner_hole_yl + " returner_swerve_vert:" + returner_swerve_vert);

                                if (!bSim)
                                {
                                    Player_States moving_ps = Game_Engine_Helper.setRunningState(bLefttoRight, true, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos);
                                    if (r.Tackler != null)
                                        p.Run_and_Tackled(moving_ps, prev_yl, prev_v);
                                    else
                                        p.Run(moving_ps, prev_yl, prev_v);

                                    //for the ball
                                    gBall.Carried(prev_yl, prev_v);
                                }
                            }
                            else
                            {
                                logger.Debug("Returner Breaks Thru:");

                                double prev_yl = p.Current_YardLine;
                                double prev_v = p.Current_Vertical_Percent_Pos;

                                p.Current_YardLine = BreakAwayYardline;

                                gBall.Current_YardLine = p.Current_YardLine;
                                gBall.Current_Vertical_Percent_Pos = p.Current_Vertical_Percent_Pos;

                                logger.Debug("prev_yl:" + prev_yl.ToString() + " prev_v:" + prev_v.ToString());
                                logger.Debug("Current_YardLine:" + p.Current_YardLine.ToString() + " Current_Vertical_Percent_Pos:" + p.Current_Vertical_Percent_Pos.ToString());

                                if (!bSim)
                                {
                                    Player_States moving_ps = Game_Engine_Helper.setRunningState(bLefttoRight, true, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos);
                                    p.Run(moving_ps, prev_yl, prev_v);

                                    //for the ball
                                    gBall.Carried(prev_yl, prev_v);
                                }
                            }
                            id_Players++;
                        }
                        else if (Past_Blocker_Tackler_List.Contains(id_Players))
                        {
                            if (!bSim)
                            {
                                p.Same_As_Last_Action();
                                if (bTackler)
                                    p.Same_As_Last_Action();
                            }
                        }
                        else
                        {
                            if (!bSim)
                            {
                                p.Block();

                                if (bTackler)
                                    p.Block();
                            }
                        }
                        id_Players++;
                    }
                    logger.Debug("=======================================================");
                    logger.Debug("");
                } //not tackled
            }  //Not kicked out of the endzone
               //===== end of stage six

            //Set if touchdown
            r.bTouchDown = Game_Engine_Helper.isTouchdown(bLefttoRight, r.Returner.Current_YardLine);

            if (r.Returner != null)
                r.Line_of_Scrimmage = r.Returner.Current_YardLine;

            if (r.bFumble_Lost)
                r.bSwitchPossession = true;

            //Set the Play stats
            double yards_gamed = Game_Engine_Helper.getYardsGained(bLefttoRight, retuner_catches_ball_yl, r.Returner.Current_YardLine);
            //Create a play stats record for every player in this play and set the appropriate play count to 1
            r.Play_Player_Stats = CreateStatRecords(r.Kicker, r.Returner, Kickoff_Players, Return_Players);
            SetPlayerStats(r.bTouchback, r.bKickoff_Out_of_Endzone, r.bTouchDown,
                r.bFumble, r.bFumble_Lost, yards_gamed, r.Kicker, r.Returner, r.Tackler, r.Fumble_Recoverer,
                Missed_Tackles, r.Play_Player_Stats);

            return r;
        }

        //This method will pick the running slot
        public static int getKickReturnRunSlot(int slot_index, bool bLookforhole, List<int?> group, bool bAnyFive)
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

        public static int getClosestKickGroupPlayerInd(int slot_index, List<int?> Group)
        {
            int r, g;
            List<int> Possible_Indexes = new List<int>();

            if (Group[slot_index] != null)
                Possible_Indexes.Add(slot_index);

            if (slot_index > 0 && Group[slot_index - 1] != null)
                Possible_Indexes.Add(slot_index - 1);

            if (slot_index < app_Constants.KICKOFF_PLAYERS_IN_GROUP - 1 && Group[slot_index + 1] != null)
                Possible_Indexes.Add(slot_index + 1);

            if (Possible_Indexes.Count == 0)
                throw new Exception("Could not find closest tacker in method getClosestKickGroupPlayerInd");

            int r_ind = CommonUtils.getRandomIndex(Possible_Indexes.Count());
            g = Possible_Indexes[r_ind];

            r = (int)Group[g];

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
        public static int? getPossibleUporDownTackler(bool bSwerveUp, int slot_index, List<int?> group)
        {
            int? r = null;

            if (bSwerveUp)
            {
                if (slot_index > 0) r = group[slot_index - 1];
            }
            else
            {
                if (slot_index < app_Constants.KICKOFF_PLAYERS_IN_GROUP - 1) r = group[slot_index + 1];
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
            if (slot_index < app_Constants.KICKOFF_PLAYERS_IN_GROUP - 1)
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

            r = app_Constants.KICKOFF_GROUP_VERT_DIST * ind;

            return r;
        }
        public static List<int?> ExpandGroup(List<int?> Group)
        {
            List<int?> r = new List<int?>();
            int empty_spots = app_Constants.KICKOFF_PLAYERS_IN_GROUP - Group.Count();

            foreach (int? s in Group)
            {
                bool bStopEmpties = false;
                while (!bStopEmpties && empty_spots > 0)
                {
                    int rnd = CommonUtils.getRandomNum(1, 10);
                    if (rnd <= 6)
                    {
                        r.Add(null);
                        empty_spots--;
                    }
                    else
                        bStopEmpties = true;
                }

                r.Add(s);
            }

            for (int i = 0; i < empty_spots; i++)
                r.Add(null);

            return r;
        }
        public static List<int?> removeRandomIndexes(List<int?> lst)
        {
            List<int?> r = new List<int?>();

            while (lst.Count > app_Constants.KICKOFF_PLAYERS_IN_GROUP)
            {
                int ind = CommonUtils.getRandomNum(1, lst.Count()) - 1;
                r.Add(lst[ind]);
                lst.RemoveAt(ind);
            }

            return r;
        }
        public static List<int> getkickoffGroupClosestPlayers(int slot_index, List<int?> group)
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
            if (slot_index < app_Constants.KICKOFF_PLAYERS_IN_GROUP - 1)
            {
                int below_slot = slot_index + 1;
                if (group[below_slot] != null)
                    r.Add((int)group[below_slot]);
            }

            if (group[slot_index] != null)
                r.Add((int)group[slot_index]);

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
        public static List<Game_Player_Stats> CreateStatRecords(Game_Player Kicker, Game_Player Returner, List<Game_Player> Kickoff_Players, List<Game_Player> Return_Players)
        {
            List<Game_Player_Stats> r = new List<Game_Player_Stats>();

            foreach (Game_Player p in Kickoff_Players)
                if (p == Kicker)
                    r.Add(new Game_Player_Stats() {Player_ID = Kicker.p_and_r.pr.First().Player_ID,  Kickoffs = 1 });
                else
                    r.Add(new Game_Player_Stats() {Player_ID = p.p_and_r.pr.First().Player_ID, ko_def_plays = 1 });

            foreach (Game_Player p in Return_Players)
                if (p == Returner)
                    r.Add(new Game_Player_Stats() {Player_ID = Returner.p_and_r.pr.First().Player_ID, ko_ret_plays = 1 });
                else
                    r.Add(new Game_Player_Stats() { Player_ID = p.p_and_r.pr.First().Player_ID, ko_rec_plays = 1 });

            return r;
        }
        public static void SetPlayerStats(bool bTouchback, bool bKicked_Out_of_Endzone, bool bTouchdown, 
            bool bFumble, bool bFumble_Lost, double Yards,
            Game_Player Kicker, Game_Player Returner, Game_Player Tackler, 
            Game_Player Forced_Fumble_Recoverer, List<Game_Player> Missed_Tackle, List<Game_Player_Stats> Game_Player_Stats)
        {
            long lTDs = bTouchdown ? 1 : 0;
            long lFubmle = bFumble ? 1 : 0;
            long lFubmle_Lost = bFumble_Lost ? 1 : 0;
            long lKickoff_out_of_Endzone = bKicked_Out_of_Endzone ? 1 : 0;

            //if touchback then set those stats
            if (bTouchback)
            {
                Game_Player_Stats ks = Game_Player_Stats.Where(x => x.Player_ID == Kicker.p_and_r.pr.First().Player_ID).First();
                ks.Kickoff_Touchbacks = 1;
                ks.Kickoff_Thru_Endzones = lKickoff_out_of_Endzone;
            }

            if (bKicked_Out_of_Endzone)
            {
                Game_Player_Stats koe = Game_Player_Stats.Where(x => x.Player_ID == Returner.p_and_r.pr.First().Player_ID).First();
                koe.ko_ret_touchbacks = 1;
                koe.ko_ret_Thru_Endzones = lKickoff_out_of_Endzone;
            }

            //set the returner stats
            Game_Player_Stats kr = Game_Player_Stats.Where(x => x.Player_ID == Returner.p_and_r.pr.First().Player_ID).First();
            kr.ko_ret_TDs = lTDs;
            kr.ko_ret_fumbles = lFubmle;
            kr.ko_ret_fumbles_lost = lFubmle_Lost;
            kr.ko_ret_yards = (long) Math.Round(Yards);
            kr.ko_ret_yards_long = kr.ko_ret_yards;

            //Set Tackler stats
            if (Tackler != null)
            {
                Game_Player_Stats kt = Game_Player_Stats.Where(x => x.Player_ID == Tackler.p_and_r.pr.First().Player_ID).First();
                kt.ko_def_tackles = 1;

                if (lFubmle > 0)
                    kt.ko_def_Forced_Fumbles = 1;
            }

            //set missed tackles
            foreach(Game_Player m in Missed_Tackle)
            {
                Game_Player_Stats mt = Game_Player_Stats.Where(x => x.Player_ID == m.p_and_r.pr.First().Player_ID).First();
                mt.ko_def_tackles_missed = 1;
            }

            //if there is a fumble and it is recovered give credit to the player that recovered it
            if (Forced_Fumble_Recoverer != null)
            {
                Game_Player_Stats fr = Game_Player_Stats.Where(x => x.Player_ID == Forced_Fumble_Recoverer.p_and_r.pr.First().Player_ID).First();
                fr.ko_def_Forced_Fumbles = 1;
            }

        }
    }
}