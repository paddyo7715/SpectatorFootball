using SpectatorFootball.Enum;
using SpectatorFootball.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.GameNS
{
    class Coach
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

            setOurThereScore();

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
        private void setOurThereScore()
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
        }

        public Formation Call_Def_Formation(Play_Package pp, double PossessionAdjuster)
        {
            Formations_Enum r;
            Formation fList = null;

            setOurThereScore();

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
        public bool AllowSubstitutions()
        {
            bool r = true;
            setOurThereScore();
            long scoreDiff = ourScore - theirScore;

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

            return false;
        }
        public List<Formation_Rec> Populate_Formation(List<Formation_Rec> fList, 
            bool bAllowSubstitutions,
            bool bSpecialTeams)
        {
            //if a player spot can not be filled (and this should almost never happen) then
            //the team will return null for the return code and they must forfeit the game.
            List<Formation_Rec> r = new List<Formation_Rec>();
            bool bCouldFine_Player = false;

            foreach (Formation_Rec f in fList)
            {
                bool bSlotSubstitution = false;
                Player_and_Ratings Player_rating = null;

                if (bAllowSubstitutions)
                    bSlotSubstitution = SlotSubstitute(f.Pos);

                //try to get a player at the need position either a starter or sub
                Player_rating = PlayerSamePOS(f.Pos, bSpecialTeams, fList);

                //if a player at the request position could not be found then we need to try to 
                //get a player at an alternate position
                if (Player_rating == null)
                    Player_rating = getPlayerSubstitutes(f.Pos, fList);

                if (Player_rating != null)
                    f.p_and_r = Player_rating;

                //if no other player could be found then the team will have to forfeit the game
                if (Player_rating == null)
                    bCouldFine_Player = true;

            }

            if (bCouldFine_Player)
                return null;  //The team must forfeit
            else
                return fList;
        }
        private Player_and_Ratings PlayerSamePOS(Player_Pos pp, bool bSubstitue, List<Formation_Rec> fList)
        {
            Player_and_Ratings r = null;

            List<Player_and_Ratings> Available_Players = Our_Players.Where(x => x.p.Pos == (int)pp &&
                !(fList.Any(f => f.p_and_r != null && f.p_and_r.p.ID == x.p.ID) && !(lInj.Any(j => j.Player_ID == x.p.ID))))
                .OrderByDescending(o => o.Overall_Grade).ToList();

            if (!bSubstitue)
                r = Available_Players.FirstOrDefault();
            else
            {
                int num_starters = Team_Helper.getNumStartingPlayersByPosition(pp);
                if (Available_Players.Count() <= num_starters)
                    r = Available_Players.FirstOrDefault();
                else
                {
                    int indexes = Available_Players.Count() - num_starters;
                    int ind = CommonUtils.getRandomIndex(indexes);
                    ind += (num_starters - 1);
                    r = r = Available_Players[ind];
                }
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
    }
}
