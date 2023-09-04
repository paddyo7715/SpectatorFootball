using SpectatorFootball.Enum;
using SpectatorFootball.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpectatorFootball.PenaltiesNS;

namespace SpectatorFootball.GameNS
{
    public class Coach
    {
        private long Franchise_id { get; set; }
        private Game g;

        private long ourScore = 0;
        private long theirScore = 0;
        private long Max_TD_Points;

        private List<Player_and_Ratings> Our_Players = null;
        private List<Injury> lInj = null;

        public Coach(long Franchise_id, Game g, long Max_TD_Points,
            List<Player_and_Ratings> Home_Player,
            List<Player_and_Ratings> Away_Player,
            List<Injury> lInj)
        {
            this.Franchise_id = Franchise_id;
            this.g = g;
            this.Max_TD_Points = Max_TD_Points;
            this.lInj = lInj;

            if (Franchise_id == g.Away_Team_Franchise_ID)
                Our_Players = Away_Player;
            else
                Our_Players = Home_Player;

        }
 
        public Play_Package Call_Off_PlayFormation(bool bKickoff, bool bExtraPoint, bool bFreeKick, double PossessionAdjuster)
        {
            Formations_Enum f;
            Play_Enum p;
            string Formation_Name = "";
            List<Formation_Rec> fList = null;
            Formation formation = null;

            Tuple<long, long> t = setOurThereScore();
            ourScore = t.Item1;
            theirScore = t.Item2;

            long scoreDiff = ourScore - theirScore;

            long QTR = (long)g.Quarter;
            long time = (long)g.Time;

            if (bKickoff)
            {
                f = Formations_Enum.KICKOFF_REGULAR_KICK;
                p = Play_Enum.KICKOFF_NORMAL;
  
                if (QTR == 4)
                {
                    if (scoreDiff < -(Max_TD_Points * 2) && time <= -300)
                    {
                       f = Formations_Enum.KICKOFF_ONSIDE_KICK;
                        p = Play_Enum.KICKOFF_ONSIDES;
                    }
                    else if (scoreDiff < -(Max_TD_Points) && time <= -240)
                    {
                        f = Formations_Enum.KICKOFF_ONSIDE_KICK;
                        p = Play_Enum.KICKOFF_ONSIDES;
                    }
                    else if (scoreDiff < 0 && time <= -150)
                    {
                        f = Formations_Enum.KICKOFF_ONSIDE_KICK;
                        p = Play_Enum.KICKOFF_ONSIDES;
                    }
                }
            }
            else
            {
                f = Formations_Enum.KICKOFF_REGULAR_KICK;
                p = Play_Enum.KICKOFF_NORMAL;
            }

            formation = Game_Helper.getFormation(f, PossessionAdjuster);

            return new Play_Package() { Formation = formation, Play = p } ;
        }
        public Tuple<long, long> setOurThereScore()
        {
            if (Franchise_id == g.Away_Team_Franchise_ID)
            {
                ourScore = (long)g.Away_Score;
                theirScore = (long)g.Home_Score;
            }
            else
            {
                ourScore = (long)g.Home_Score;
                theirScore = (long)g.Away_Score;
            }

            return new Tuple<long, long>(ourScore, theirScore);
        }

        public Formation Call_Def_Formation(Play_Package pp, double PossessionAdjuster)
        {
            Formations_Enum r;
            Formation fList = null;

            Tuple<long, long> t = setOurThereScore();
            ourScore = t.Item1;
            theirScore = t.Item2;

            long scoreDiff = ourScore - theirScore;

            long QTR = (long)g.Quarter;
            long time = (long)g.Time;

            if (pp.Formation.f_enum == Formations_Enum.KICKOFF_ONSIDE_KICK)
                r = Formations_Enum.KICKOFF_ONSIDE_RECEIVE;
            else if (pp.Formation.f_enum == Formations_Enum.KICKOFF_REGULAR_KICK)
                r = Formations_Enum.KICKOFF_REGULAR_RECEIVE;
            else if (pp.Formation.f_enum == Formations_Enum.FIELD_GOAL)
                r = Formations_Enum.KICKOFF_REGULAR_RECEIVE;
            else if (pp.Formation.f_enum == Formations_Enum.PUNT)
                r = Formations_Enum.KICKOFF_REGULAR_RECEIVE;
            else if (pp.Formation.f_enum == Formations_Enum.EXTRA_POINT)
                r = Formations_Enum.KICKOFF_REGULAR_RECEIVE;
            else
            {
                //Just to get this to compile put this formation.
                //howver when I get to it, this will be a defensive
                //formation based on what the defense thinks the O will do.
                r = Formations_Enum.KICKOFF_REGULAR_RECEIVE;
            }

            fList = Game_Helper.getFormation(r, PossessionAdjuster);

            return fList;
        }
        public bool AllowSubstitutions(bool bSpecialTeams)
        {
            bool r = true;

            if (!bSpecialTeams)
            {
                if (g.Quarter == 1)
                {
                    if (g.Time >= 600)
                        r = false;
                }

                if (g.Quarter == 4)
                {
                    if (g.Time <= 120)
                        r = false;
                }
            }

            return false;
        }
        public List<Formation_Rec> Populate_Formation(List<Formation_Rec> fList, 
            bool bAllowSubstitutions,
            bool bSpecialTeams, bool bPlayWithReturner)
        {
            //if a player spot can not be filled (and this should almost never happen) then
            //the team will return null for the return code and they must forfeit the game.
            List<Formation_Rec> r = new List<Formation_Rec>();
            bool bCouldFine_Player = false;

            int p_id = 0;
            foreach (Formation_Rec f in fList)
            {
                bool bSlotSubstitution = false;
                Player_and_Ratings Player_rating = null;

                if (bAllowSubstitutions)
                    bSlotSubstitution = SlotSubstitute(f.Pos);

                if (bSpecialTeams)
                    bSlotSubstitution = true;

                bool bReturner = false;
                if (bPlayWithReturner && p_id == app_Constants.RETURNER_INDEX)
                    bReturner = true;
                //try to get a player at the need position either a starter or sub
                Player_rating = PlayerSamePOS(f.Pos, fList, bSlotSubstitution, bReturner, bSpecialTeams  );

                //if a player at the request position could not be found then we need to try to 
                //get a player at an alternate position
                if (Player_rating == null)
                    Player_rating = getPlayerSubstitutes(f.Pos, fList);

                if (Player_rating != null)
                    f.p_and_r = Player_rating;

                //if no other player could be found then the team will have to forfeit the game
                if (Player_rating == null)
                    bCouldFine_Player = true;

                p_id++;
            }

            if (bCouldFine_Player)
                return null;  //The team must forfeit
            else
                return fList;
        }
        private Player_and_Ratings PlayerSamePOS(Player_Pos pp, List<Formation_Rec> fList, bool bSubstitue, bool bReturner, bool bSpecialTeams)
        {
            Player_and_Ratings r = null;

            List<Player_and_Ratings> Players_List = Our_Players.Where(x => x.p.Pos == (int)pp &&
                !(fList.Any(f => f.p_and_r != null && f.p_and_r.p.ID == x.p.ID) && !(lInj.Any(j => j.Player_ID == x.p.ID))))
                .OrderByDescending(o => o.Overall_Grade).ToList();

            int num_starters = Team_Helper.getNumStartingPlayersByPosition(pp);
            int num_bench_warmers = Players_List.Count() - num_starters;

            if (!bSubstitue || Players_List.Count() <= num_starters)
                r = Players_List.FirstOrDefault();
            else if (bReturner)
            {
                Players_List = CommonUtils.RemoveFirstElements(Players_List, num_starters);
                r = Players_List.First();
            }
            else if (bSpecialTeams)
            {
                Players_List = CommonUtils.RemoveFirstElements(Players_List, num_starters);
                int id = CommonUtils.getRandomIndex(Players_List.Count());
                r = Players_List[id];
            }
            else if (bSubstitue)
            {
                Players_List = CommonUtils.RemoveFirstElements(Players_List, num_starters);
                int id = CommonUtils.getListIndexFavorEarly(Players_List.Count(), app_Constants.RETURNER_PERCENT_LIST_SELECTION);
                r = Players_List[id];
            }

            return r;
        }
        private bool SlotSubstitute(Player_Pos pp)
        {
            bool r = false;
            switch (pp)
            {
                case Player_Pos.QB:
                    {
                        long scoreDiff = ourScore - theirScore;
                        if (g.Quarter == 4)
                        {
                            if (g.Time <= 600 && (Math.Abs(scoreDiff) > (Max_TD_Points * 5)))
                                r = true;
                            else if (g.Time <= 480 && (Math.Abs(scoreDiff) > (Max_TD_Points * 4)))
                                r = true;
                            else if (g.Time <= 360 && (Math.Abs(scoreDiff) > (Max_TD_Points * 3)))
                                r = true;
                            else
                                r = false;
                        }
                        break;
                    }

                case Player_Pos.RB:
                    {
                        int rb_temp = CommonUtils.getRandomNum(1, 100);
                        if (rb_temp < app_Constants.STARTER_RB_SUB_PERCENT)
                            r = true;
                        break;
                    }

                case Player_Pos.WR:
                    {
                        int wr_temp = CommonUtils.getRandomNum(1, 100);
                        if (wr_temp < app_Constants.STARTER_WR_SUB_PERCENT)
                            r = true;
                        break;
                    }

                case Player_Pos.TE:
                    {
                        int te_temp = CommonUtils.getRandomNum(1, 100);
                        if (te_temp < app_Constants.STARTER_TE_SUB_PERCENT)
                            r = true;
                        break;
                    }

                case Player_Pos.OL:
                    {
                        int ol_temp = CommonUtils.getRandomNum(1, 100);
                        if (ol_temp < app_Constants.STARTER_OL_SUB_PERCENT)
                            r = true;
                        break;
                    }

                case Player_Pos.DL:
                    {
                        int dl_temp = CommonUtils.getRandomNum(1, 100);
                        if (dl_temp < app_Constants.STARTER_DL_SUB_PERCENT)
                            r = true;
                        break;
                    }

                case Player_Pos.LB:
                    {
                        int lb_temp = CommonUtils.getRandomNum(1, 100);
                        if (lb_temp < app_Constants.STARTER_LB_SUB_PERCENT)
                            r = true;
                        break;
                    }

                case Player_Pos.DB:
                    {
                        int db_temp = CommonUtils.getRandomNum(1, 100);
                        if (db_temp < app_Constants.STARTER_DB_SUB_PERCENT)
                            r = true;
                        break;
                    }

                case Player_Pos.P:
                case Player_Pos.K:
                    {
                        r = false;
                        break;
                    }
            }

            return r;
        }
        //This method is called during a game and a player for a specific position can not be found,
        //so a player at another position must be chosen to take his place.  Example:  All 3 QBs
        //go down to injury
        private Player_and_Ratings getPlayerSubstitutes(Player_Pos pos,
            List<Formation_Rec> fList)
        {
            Player_and_Ratings r = null;
            List<Player_Pos> pp = new List<Player_Pos>();

            switch (pos)
            {
                case Player_Pos.QB:
                    {
                        r = Our_Players.Where(x => !(fList.Any(f => f.p_and_r.p.ID == x.p.ID) && !(lInj.Any(j => j.Player_ID == x.p.ID))))
                            .OrderByDescending(x => x.pr.First().Accuracy_Rating).FirstOrDefault();
                        break;
                    }

                case Player_Pos.RB:
                    {
                        r = Our_Players.Where(x => !(fList.Any(f => f.p_and_r.p.ID == x.p.ID) && !(lInj.Any(j => j.Player_ID == x.p.ID))))
                            .OrderByDescending(x => x.pr.First().Running_Power_Rating).FirstOrDefault();
                        break;
                    }

                case Player_Pos.WR:
                    {
                        r = Our_Players.Where(x => !(fList.Any(f => f.p_and_r.p.ID == x.p.ID) && !(lInj.Any(j => j.Player_ID == x.p.ID))))
                            .OrderByDescending(x => x.pr.First().Hands_Rating).FirstOrDefault();
                        break;
                    }

                case Player_Pos.TE:
                    {
                        r = Our_Players.Where(x => !(fList.Any(f => f.p_and_r.p.ID == x.p.ID) && !(lInj.Any(j => j.Player_ID == x.p.ID))))
                            .OrderByDescending(x => x.pr.First().Hands_Rating).FirstOrDefault();
                        break;
                    }

                case Player_Pos.OL:
                    {
                        r = Our_Players.Where(x => !(fList.Any(f => f.p_and_r.p.ID == x.p.ID) && !(lInj.Any(j => j.Player_ID == x.p.ID))))
                            .OrderByDescending(x => x.pr.First().Pass_Block_Rating).FirstOrDefault();
                        break;
                    }

                case Player_Pos.DL:
                    {
                        r = Our_Players.Where(x => !(fList.Any(f => f.p_and_r.p.ID == x.p.ID) && !(lInj.Any(j => j.Player_ID == x.p.ID))))
                            .OrderByDescending(x => x.pr.First().Pass_Attack_Rating).FirstOrDefault();
                        break;
                    }

                case Player_Pos.LB:
                    {
                        r = Our_Players.Where(x => !(fList.Any(f => f.p_and_r.p.ID == x.p.ID) && !(lInj.Any(j => j.Player_ID == x.p.ID))))
                            .OrderByDescending(x => x.pr.First().Pass_Attack_Rating).FirstOrDefault();
                        break;
                    }

                case Player_Pos.DB:
                    {
                        r = Our_Players.Where(x => !(fList.Any(f => f.p_and_r.p.ID == x.p.ID) && !(lInj.Any(j => j.Player_ID == x.p.ID))))
                           .OrderByDescending(x => x.pr.First().Speed_Rating).FirstOrDefault();
                        break;
                    }

                case Player_Pos.K:
                    {
                        r = Our_Players.Where(x => !(fList.Any(f => f.p_and_r.p.ID == x.p.ID) && !(lInj.Any(j => j.Player_ID == x.p.ID))))
                            .OrderByDescending(x => x.pr.First().Kicker_Leg_Accuracy_Rating).FirstOrDefault();
                        break;
                    }

                case Player_Pos.P:
                    {
                        r = Our_Players.Where(x => !(fList.Any(f => f.p_and_r.p.ID == x.p.ID) && !(lInj.Any(j => j.Player_ID == x.p.ID))))
                            .OrderByDescending(x => x.pr.First().Kicker_Leg_Power_Rating).FirstOrDefault();
                        break;
                    }
            }

            return r;
        }

        //This is for penalties on the offense that includes kickoff returns
        public bool AcceptOff_Penalty(Play_Enum pe, Play_Result pResult, double Line_of_Scrimmage, bool bLefttoRight, bool bLastPlayGame, bool bLasPlayHalf)
        {
            bool r = false;
            double dist_from_GL = Game_Engine_Helper.calcDistanceFromGL(Line_of_Scrimmage, bLefttoRight);

            Penalty penalty = pResult.Penalty;

            if (!penalty.bDeclinable)
                throw new Exception("Non decidable penalty passed to AcceptDef_Penalty");

            if (pResult.bFumble_Lost || pResult.bInterception)
                r = false;
            if (pResult.bTouchDown)
                r = true;
            else if (pResult.bSafety)
                r = false;
            else if (pResult.bXPMissed)
                r = false;
            else if (pResult.bXPMade)
                r = true;
            else if (pResult.bFGMade)
                r = true;
//            else if (pResult.bFGMade)
//                r = !AcceptPenaltyFGMade(dist_from_GL);
            else if (pResult.bOnePntAfterTDMissed)
                r = false;
            else if (pResult.bOnePntAfterTDMade)
                r = true;
            else if (pResult.bTwoPntAfterTDMissed)
                r = false;
            else if (pResult.bTwoPntAfterTDMade)
                r = true;
            else if (pResult.bThreePntAfterTDMissed)
                r = false;
            else if (pResult.bThreePntAfterTDMade)
                r = true;
            else if (this.ourScore <= this.theirScore && (bLastPlayGame || bLasPlayHalf))
                r = true;
            else  //all other play situations
            {
                int Horizonal_Adj = Game_Engine_Helper.HorizontalAdj(bLefttoRight);

                switch (pe)
                {
                    case Play_Enum.FREE_KICK:
                    case Play_Enum.KICKOFF_NORMAL:
                    case Play_Enum.KICKOFF_ONSIDES:
                    case Play_Enum.PUNT:
                        r = true;
                        break;
                    case Play_Enum.RUN:
                    case Play_Enum.PASS:  //stopped here
                        r = false;
                        if (pResult.Yards_Gained > 0)
                            r = true;
                        break;
                }
            }


            return r;
        }

        //This is for penalties on the defense that includes kickoff defense
        public bool AcceptDef_Penalty(Play_Enum pe, Play_Result pResult,double yards_to_go, double Line_of_Scrimmage, bool bLefttoRight, bool bLastPlayGame, bool bLasPlayHalf)
        {
            bool r = false;
            double dist_from_GL = Game_Engine_Helper.calcDistanceFromGL(Line_of_Scrimmage, bLefttoRight);

            Penalty penalty = pResult.Penalty;

            if (!penalty.bDeclinable)
                throw new Exception("Non decidable penalty passed to AcceptDef_Penalty");

            if (pResult.bFumble_Lost || pResult.bInterception)
                r = true;
            else if (pResult.bTouchDown)
                r = false;
            else if (pResult.bSafety)
                r = true;
            else if (pResult.bXPMissed)
                r = true;
            else if (pResult.bXPMade)
                r = false;
            else if (pResult.bFGMissed)
                r = true;
            else if (pResult.bFGMade)
                if (!Penalty_Helper.isFirstDowwithPenalty(penalty, yards_to_go, dist_from_GL))
                    r = false;
                else
                {
                    r = true;
                    if (g.Quarter == 2 || g.Quarter > 3)
                    {
                        if (g.Time <= 10)
                            r = false;
                        else if (g.Time <= 20 && dist_from_GL >= 15)
                            r = false;
                        else if (g.Time <= 30 && dist_from_GL > 25)
                            r = false;
                        else if (g.Time <= 40 && dist_from_GL > 30)
                            r = false;
                        else
                            r = true;
                    }
                }
            else if (pResult.bOnePntAfterTDMissed)
                r = true;
            else if (pResult.bOnePntAfterTDMade)
                r = false;
            else if (pResult.bTwoPntAfterTDMissed)
                r = true;
            else if (pResult.bTwoPntAfterTDMade)
                r = false;
            else if (pResult.bThreePntAfterTDMissed)
                r = true;
            else if (pResult.bThreePntAfterTDMade)
                r = false;
            else if (this.ourScore <= this.theirScore && (bLastPlayGame || bLasPlayHalf))
                r = true;
            else  //all other play situations
            {
                int Horizonal_Adj = Game_Engine_Helper.HorizontalAdj(bLefttoRight);

                switch (pe)
                {
                    case Play_Enum.PUNT:
                        if (!pResult.bCoffinCornerMade)
                            r = true;
                        break;
                    case Play_Enum.FREE_KICK:
                    case Play_Enum.KICKOFF_NORMAL:
                    case Play_Enum.KICKOFF_ONSIDES:
                        r = true;
                        break;
                    case Play_Enum.RUN:
                    case Play_Enum.PASS:
                        r = false;
                        bool bFirstDown = Penalty_Helper.isFirstDowwithPenalty(penalty, yards_to_go, dist_from_GL);

                        if (bFirstDown && pResult.Yards_Gained < yards_to_go)
                            r = true;
                        else if (pResult.Yards_Gained > yards_to_go)
                            r = true;
                        else
                            r = false;
                        break;
                }
            }


            return r;
        }

/*        public Next_Play_Situation NextDownandSpot(Play_Enum pe, Play_Result pResult, int Down, int yards_to_go, int Line_of_Scrimmage, bool bLefttoRight)
        {
            int new_Down = 0;
            int new_Yards_to_Go = 0;
            int new_Line_of_Scrimmage = 0;
            bool bTurnover_on_Downs = false;

                switch (pe)
                {
                case Play_Enum.RUN:
                case Play_Enum.PASS:
                    if (pResult.Yards_Gained >= yards_to_go)
                    {
                        new_Down = 1;
                        new_Yards_to_Go = 10;
                    }
                    else if (Down == 4)
                    {
                        new_Down = 1;
                        new_Yards_to_Go = 10;
                        bTurnover_on_Downs = true;
                    }
                    else
                    {
                        new_Down = Down + 1;
                        new_Yards_to_Go -= pResult.Yards_Gained;
                    }
                    new_Line_of_Scrimmage += pResult.Yards_Gained * Game_Engine_Helper.HorizontalAdj(bLefttoRight);
                    break;
                case Play_Enum.EXTRA_POINT:
                case Play_Enum.SCRIM_PLAY_1XP:
                case Play_Enum.SCRIM_PLAY_2XP:
                case Play_Enum.SCRIM_PLAY_3XP:
                    break;
            }

            return new Next_Play_Situation() { Down = new_Down, Yardline = new_Line_of_Scrimmage, bTurnover_On_Downs = bTurnover_on_Downs, Yards_Gained = pResult.Yards_Gained };
        }

        public Next_Play_Situation NextDownandSpot_KickingTeam_Penalty(Play_Result pResult, Penalty penalty, Play_Enum pe, int yards_to_go, int Returner_goalline, bool bLefttoRight)
        {
            int new_Down = 0;
            double new_yards_to_go = 0;
            double new_Line_of_Scrimmage = 0;
            double Returner_Yardline = pResult.Returner.Current_YardLine;
            double Spot_of_Found_Yardline = pResult.Penalized_Player.Current_YardLine;
            int Horizonal_Adj = Game_Engine_Helper.HorizontalAdj(bLefttoRight);
            double Penalty_Yards = 0.0;
            bool bHalf_the_D = false;

            //All penalties on the kick or punt teams are spot fouls.
            //so there will never be a penalty to rekick
            if (!penalty.bSpot_Foul)
                throw new Exception("Penalty " + penalty.code + " on kick/punt return not allowed because it is not a spot foul.");

            double point_of_foul_Yardline;

            //first decide the point of fould. .  
            if (bLefttoRight)
            {
                if (Returner_Yardline > Spot_of_Found_Yardline)
                    point_of_foul_Yardline = Returner_Yardline;
                else
                    point_of_foul_Yardline = Spot_of_Found_Yardline;
            }
            else
            {
                if (Returner_Yardline < Spot_of_Found_Yardline)
                    point_of_foul_Yardline = Returner_Yardline;
                else
                    point_of_foul_Yardline = Spot_of_Found_Yardline;
            }

            //then add or subtract penatly and check for half the distance.
            double dist_from_GL = Game_Engine_Helper.calcDistanceFromGL(point_of_foul_Yardline, bLefttoRight);
            bHalf_the_D = Penalty_Helper.isHalfTheDistance(penalty.Yards, dist_from_GL);

            if (bHalf_the_D)
                new_Line_of_Scrimmage = point_of_foul_Yardline + ((dist_from_GL / 2) * Horizonal_Adj);
            else
                new_Line_of_Scrimmage = point_of_foul_Yardline + (penalty.Yards * Horizonal_Adj);


            new_Down = 1;
            new_yards_to_go = 10;

            return new Next_Play_Situation() { Down = new_Down, Yardline = new_Line_of_Scrimmage, Yards_Gained = pResult.Yards_Gained, Penalty_Yards = Penalty_Yards, bHalf_the_distance = bHalf_the_D };
        }
*/








        //bLefttoRight should be from the point of the returning team so use ! when calling
/*        public Next_Play_Situation NextDownandSpot_KickReturn_Penalty(Play_Result pResult, Penalty penalty, Play_Enum pe, int yards_to_go, int Returner_goalline, bool bLefttoRight)
        {
            int new_Down = 0;
            double new_yards_to_go = 0;
            double new_Line_of_Scrimmage = 0;
            double Returner_Yardline = pResult.Returner.Current_YardLine;
            double Spot_of_Found_Yardline = pResult.Penalized_Player.Current_YardLine;
            int Horizonal_Adj = Game_Engine_Helper.HorizontalAdj(bLefttoRight);
            double Penalty_Yards = 0.0;
            bool bHalf_the_D = false;

            //All penalties on the kick or punt return teams are spot fouls.
            //so there will never be a penalty to rekick
            if (!penalty.bSpot_Foul)
                throw new Exception("Penalty " + penalty.code + " on kick/punt return not allowed because it is not a spot foul.");

            double point_of_foul_Yardline;

            //first decide the point of fould. .  
            if (bLefttoRight)
            {
                if (Returner_Yardline < Spot_of_Found_Yardline)
                    point_of_foul_Yardline = Returner_Yardline;
                else
                    point_of_foul_Yardline = Spot_of_Found_Yardline;
            }
            else
            {
                if (Returner_Yardline > Spot_of_Found_Yardline)
                    point_of_foul_Yardline = Returner_Yardline;
                else
                    point_of_foul_Yardline = Spot_of_Found_Yardline;
            }

            //then add or subtract penatly and check for half the distance.
            double dist_from_GL = Game_Engine_Helper.calcDistanceFromGL(point_of_foul_Yardline, !bLefttoRight);
            bHalf_the_D = Penalty_Helper.isHalfTheDistance(penalty.Yards, dist_from_GL);

            if (bHalf_the_D)
                new_Line_of_Scrimmage = point_of_foul_Yardline - ((dist_from_GL / 2) * Horizonal_Adj);
            else
                new_Line_of_Scrimmage = point_of_foul_Yardline - (penalty.Yards * Horizonal_Adj);


            new_Down = 1;
            new_yards_to_go = 10;

            return new Next_Play_Situation() { Down = new_Down, Yardline = new_Line_of_Scrimmage, Yards_Gained = pResult.Yards_Gained, Penalty_Yards = Penalty_Yards, bHalf_the_distance = bHalf_the_D };
        }
*/



    }
}
