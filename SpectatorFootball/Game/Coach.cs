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

         //This is for penalties on the defense that includes kickoff defense, but for punts, the returning
        //team is considered the defense
        public bool AcceptDef_Penalty(Play_Enum pe, Play_Result pResult,double yards_to_go, bool bLefttoRight, bool bLastPlayGame, bool bLasPlayHalf, double touchback_yl)
        {
            bool r = false;
            double dist_from_GL = 0.0;
            Tuple<bool, double> t = null;
            bool bHalft_the_dist;
            double Penalty_Yards;

            Tuple<long, long> tScore = setOurThereScore();
            ourScore = tScore.Item1;
            theirScore = tScore.Item2;

            Penalty penalty = pResult.Penalty;

            if (!penalty.bDeclinable || penalty.bSpot_Foul)
                throw new Exception("Non decidable or spot penalty passed to AcceptDef_Penalty");

            if (pResult.bFumble_Lost || pResult.bInterception)
            {
                if (pe == Play_Enum.PUNT)
                    r = false;
                else
                    r = true;
            }
            else if (pResult.bTouchDown)
            {
                if (pe == Play_Enum.PUNT)
                    r = true;
                else
                    r = false;
            }
            else if (pResult.bSafety)
                r = true;
            else if (pResult.bXPMissed)
                r = true;
            else if (pResult.bXPMade)
                r = false;
            else if (pResult.bFGMissed)
                r = true;
            else if (pResult.bFGMade)
            {
                dist_from_GL = Game_Engine_Helper.calcDistanceFromOpponentGL(pResult.Play_Start_Yardline, bLefttoRight);
                t = Penalty_Helper.isHalfTheDistance(penalty.Yards, dist_from_GL);
                bool bhtdfd = t.Item1;
                double fgHalftheDist = t.Item2;
                if (!Penalty_Helper.isFirstDowwithPenalty(penalty, yards_to_go, bhtdfd, fgHalftheDist))
                    r = false;
                else
                {
                    r = true;
                    if (g.Quarter == 2 || (g.Quarter > 3 && (ourScore - 3) < theirScore))
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
            }
            else if (pResult.bOnePntAfterTDMissed || pResult.bTwoPntAfterTDMissed || pResult.bThreePntAfterTDMissed)
                r = true;
            else if (pResult.bOnePntAfterTDMade || pResult.bTwoPntAfterTDMade || pResult.bThreePntAfterTDMade)
                r = false;
            else if (this.ourScore <= this.theirScore && (bLastPlayGame || bLasPlayHalf))
                r = true;
            else  //all other play situations
            {

                switch (pe)
                {
                    case Play_Enum.PUNT:
                        dist_from_GL = Game_Engine_Helper.calcDistanceFromOpponentGL(pResult.Play_Start_Yardline, bLefttoRight);
                        t = Penalty_Helper.isHalfTheDistance(pResult.Penalty.Yards, dist_from_GL);
                        bHalft_the_dist = t.Item1;
                        if (t.Item1)
                            Penalty_Yards = t.Item2;
                        else
                            Penalty_Yards = pResult.Penalty.Yards;
                        bool bFirstDown = Penalty_Helper.isFirstDowwithPenalty(pResult.Penalty, yards_to_go, bHalft_the_dist, Penalty_Yards);

                        double end_yardLine = pResult.end_of_play_yardline;
                        if (pResult.bTouchback)
                            end_yardLine = Game_Engine_Helper.getScrimmageLine(touchback_yl, bLefttoRight);

                        double net_Punt_Yards = Game_Engine_Helper.getYardsGained(bLefttoRight, pResult.Play_Start_Yardline, end_yardLine);

                        if (pResult.Penalty.bSpot_Foul)
                            r = true;
                        else if (bFirstDown)
                            r = true;
                        else if (pResult.bCoffinCornerMade)
                            r = false;
                        else if (net_Punt_Yards <= app_Constants.NET_YARDS_TO_DECLINE_PENALTY)
                            r = true;
                        else
                            r = false;

                        break;
                    case Play_Enum.FREE_KICK:
                    case Play_Enum.KICKOFF_NORMAL:
                    case Play_Enum.KICKOFF_ONSIDES:
                        r = true;
                        break;
                    case Play_Enum.RUN:
                    case Play_Enum.PASS:
                        r = false;

                        double More_Yard_to_not_Accept_Penalty = 4.0;

                        if (pResult.Penalty.code == Penalty_Codes.PI)
                        {
                            double greaterYL = Game_Engine_Helper.GreaterYardline(pResult.Penalized_Player.Current_YardLine, pResult.end_of_play_yardline, bLefttoRight);
                            if (greaterYL == pResult.Penalized_Player.Current_YardLine)
                                r = true;
                            else if (pResult.Yards_Gained >= yards_to_go)
                                r = false;
                            else
                                r = true;
                        }
                        else
                        {
                            dist_from_GL = Game_Engine_Helper.calcDistanceFromOpponentGL(pResult.Play_Start_Yardline, bLefttoRight);
                            t = Penalty_Helper.isHalfTheDistance(penalty.Yards, dist_from_GL);
                            bHalft_the_dist = t.Item1;
                            Penalty_Yards = t.Item2;
                            bool bPenaltyFirstDown = Penalty_Helper.isFirstDowwithPenalty(penalty, yards_to_go, bHalft_the_dist, Penalty_Yards);

                            double pen_yards = penalty.Yards;
                            if (bHalft_the_dist)
                                pen_yards = Penalty_Yards;

                            if (!bPenaltyFirstDown && pResult.Yards_Gained >= yards_to_go)
                                r = false;
                            else if (pResult.Yards_Gained >= yards_to_go && pResult.Yards_Gained >= pen_yards)
                                r = false;
                            else if (pResult.Yards_Gained >= pen_yards + More_Yard_to_not_Accept_Penalty)
                                r = false;
                            else
                                r = true;
                        }
                        break;
                }
            }


            return r;
        }

        //This is for penalties on the offense that includes kickoff return team, but for punts, the punting
        //team is considered the defense
        public bool AcceptOff_Penalty(Play_Enum pe, Play_Result pResult, double yards_to_go, bool bLefttoRight, bool bLastPlayGame, bool bLasPlayHalf, double touchback_yl)
        {
            bool r = false;
            double dist_from_GL = 0.0;

            Tuple<long, long> t = setOurThereScore();
            ourScore = t.Item1;
            theirScore = t.Item2;

            Penalty penalty = pResult.Penalty;

            if (!penalty.bDeclinable || penalty.bSpot_Foul)
                throw new Exception("Non decidable or spot penalty passed to AcceptOff_Penalty");

            if (pResult.bFumble_Lost || pResult.bInterception)
            {
                if (pe == Play_Enum.PUNT)
                    r = true;
                else
                    r = false;
            }
            else if (pResult.bTouchDown)
            {
                if (pe == Play_Enum.PUNT)
                    r = false;
                else
                    r = true;
            }
            else if (pResult.bSafety)
                r = false;
            else if (pResult.bXPMissed)
                r = false;
            else if (pResult.bXPMade)
                r = true;
            else if (pResult.bFGMissed)
                r = false;
            else if (pResult.bFGMade)  //different then def penalty
                r = true;
            else if (pResult.bOnePntAfterTDMissed || pResult.bTwoPntAfterTDMissed || pResult.bThreePntAfterTDMissed)
                r = false;
            else if (pResult.bOnePntAfterTDMade || pResult.bTwoPntAfterTDMade || pResult.bThreePntAfterTDMade)
                r = true;
            else if (this.ourScore < this.theirScore && (bLastPlayGame || bLasPlayHalf))
                r = true;
            else  //all other play situations
            {
   //             int Horizonal_Adj = Game_Engine_Helper.HorizontalAdj(bLefttoRight);

                switch (pe)
                {
                    case Play_Enum.PUNT:
                        double end_yardLine = pResult.end_of_play_yardline;
                        if (pResult.bTouchback)
                            end_yardLine = Game_Engine_Helper.getScrimmageLine(touchback_yl, bLefttoRight);
         
                        double net_Punt_Yards = Game_Engine_Helper.getYardsGained(bLefttoRight, pResult.Play_Start_Yardline, end_yardLine);

                        if (pResult.bCoffinCornerMade || pResult.Penalty.bSpot_Foul)
                            r = true;
                        else
                        {
                            if (net_Punt_Yards <= app_Constants.NET_YARDS_TO_DECLINE_PENALTY)  
                                r = false;
                            else
                                r = true;
                        }
                        break;
                    case Play_Enum.FREE_KICK:
                    case Play_Enum.KICKOFF_NORMAL:
                    case Play_Enum.KICKOFF_ONSIDES:
                        r = true;
                        break;
                    case Play_Enum.RUN:  //different
                    case Play_Enum.PASS:
                        r = true;

                        dist_from_GL = Game_Engine_Helper.calcDistanceFromOpponentGL(pResult.Play_Start_Yardline, bLefttoRight);
                        Tuple<bool, double> t2 = Penalty_Helper.isHalfTheDistance(penalty.Yards, dist_from_GL);
                        bool bHalft = t2.Item1;
                        double Penalty_Yards = t2.Item2;

                        double pen_yards = penalty.Yards;
                        if (bHalft)
                            pen_yards = Penalty_Yards;

                        if (pResult.Yards_Gained <= pen_yards * 0.5)
                            r = false;
                        break;
                }
            }


            return r;
        }

    }
}
