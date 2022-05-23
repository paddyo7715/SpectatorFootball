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

        private long ourScore;
        private long theirScore;

        private long Max_TD_Points;

        private List<Player_and_Ratings> Our_Players = null;

        public Coach(long Franchise_id, Game g, long Max_TD_Points,
            List<Player_and_Ratings> Home_Player,
            List<Player_and_Ratings> Away_Player)
        {
            this.Franchise_id = Franchise_id;
            this.g = g;
            this.Max_TD_Points = Max_TD_Points;

            if (Franchise_id == g.Away_Team_Franchise_ID)
                Our_Players = Away_Player;
            else
                Our_Players = Home_Player;

        }
 
        public Play_Package Call_Off_PlayFormation(bool bKickoff)
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

            formation = Game_Helper.getFormation(f);

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

        public Formation Call_Def_Formation(Play_Package pp)
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

            fList = Game_Helper.getFormation(r);

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

            foreach (Formation_Rec f in fList)
            {
                bool bSlotSubstitution = SlotSubstitute(f.Pos);

            }

            return fList;
        }
        private long? PlayerSamePOS(Player_Pos pp, bool bSubstitue)
        {
            long? r = null;

            List<long> Available_Players = 

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
    }
}
