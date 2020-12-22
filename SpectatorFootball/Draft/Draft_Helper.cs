using SpectatorFootball.DraftNS;
using SpectatorFootball.Enum;
using SpectatorFootball.Models;
using SpectatorFootball.Team;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.DraftsNS
{
    public class Draft_Helper
    {
        public List<Draft> Create_Draft(long year, string draft_Type, int Draft_Rounds, List<Franchise> this_year_franchises, string league_filepath)
        {
            //if league_filepath is null then this is a new league.

            List<Draft> r = new List<Draft>();
            int p_num;
            List<Franchise> Round_order1 = null;
            List<Franchise> Round_order2 = null;

            switch (draft_Type)
            {
                case "FD":
                    List<Franchise> Round_order = new List<Franchise>();
                    if (league_filepath != null)
                    {
                        //Get Draft Order from last yeear.
                        LeagueDAO Leg_DAO = new LeagueDAO();
                        List<int> ly_order_byID = Leg_DAO.GetTeamRankings_byID(year, league_filepath);
                        //Then find any expansion teams, since they will pick first in the order.
                        List<Franchise> Expansion_Franchises = get_ExpansionTeams(this_year_franchises, ly_order_byID);
                        List<int> Contraction_IDs = get_ContractedTeams(this_year_franchises, ly_order_byID);
                        //Randomize the list of expansion teams to determine the order
                        Expansion_Franchises = CommonUtils.ShufleList(Expansion_Franchises);

                        //Add the expansion teams to the beginning of the draft order
                        Round_order.AddRange(Expansion_Franchises);
                        foreach (int i in ly_order_byID)
                        {
                            if (Contraction_IDs.Contains(i))
                                continue;

                            Round_order.Add(this_year_franchises.Where(x => x.ID == ly_order_byID[i]).First());
                        }
                    }
                    else
                    {
                        Round_order = CommonUtils.ShufleList(this_year_franchises);
                    }

                    p_num = 1;
                    for (int rd = 1; rd <= Draft_Rounds; rd++)
                    {
                        foreach (Franchise f in Round_order)
                        {
                            r.Add(new Draft() { Franchise_ID = f.ID, Pick_Number = p_num, Round = rd });
                            p_num++;
                        }
                    }

                    break;
                case "SD":
                    Round_order1 = new List<Franchise>();
                    Round_order2 = new List<Franchise>();
                    if (league_filepath != null)
                    {
                        //Get Draft Order from last yeear.
                        LeagueDAO Leg_DAO = new LeagueDAO();
                        List<int> ly_order_byID = Leg_DAO.GetTeamRankings_byID(year, league_filepath);
                        //Then find any expansion teams, since they will pick first in the order.
                        List<Franchise> Expansion_Franchises = get_ExpansionTeams(this_year_franchises, ly_order_byID);
                        List<int> Contraction_IDs = get_ContractedTeams(this_year_franchises, ly_order_byID);
                        //Randomize the list of expansion teams to determine the order
                        Expansion_Franchises = CommonUtils.ShufleList(Expansion_Franchises);

                        //Add the expansion teams to the beginning of the draft order
                        Round_order1.AddRange(Expansion_Franchises);
                        foreach (int i in ly_order_byID)
                        {
                            if (Contraction_IDs.Contains(ly_order_byID[i]))
                                continue;

                            Round_order1.Add(this_year_franchises.Where(x => x.ID == ly_order_byID[i]).First());
                        }
                    }
                    else
                    {
                        Round_order1 = CommonUtils.ShufleList(this_year_franchises);
                    }

                    Round_order2.AddRange(Round_order1);
                    Round_order2.Reverse();

                    p_num = 1;
                    for (int rd = 1; rd <= Draft_Rounds; rd++)
                    {
                        List<Franchise> temp_Round = null;
                        if (rd % 2 == 1)
                            temp_Round = Round_order1;
                        else
                            temp_Round = Round_order2;

                        foreach (Franchise f in temp_Round)
                        {
                            r.Add(new Draft() { Franchise_ID = f.ID, Pick_Number = p_num, Round = rd });
                            p_num++;
                        }
                    }

                    break;

                case "DL":
                    Round_order1 = new List<Franchise>();
                    Round_order2 = new List<Franchise>();
                    if (league_filepath != null)
                    {
                        //Get Playoff team Draft Order from last yeear.
                        LeagueDAO Leg_DAO = new LeagueDAO();
                        List<int> ly_Playoff_order_byID = Leg_DAO.GetPlayoffTeamRankings_byID(year, league_filepath);
                        List<int> Contraction_IDs = get_ContractedTeams(this_year_franchises, ly_Playoff_order_byID);

                        //Create list of non playoff teams only
                        List<Franchise> np_Teams = new List<Franchise>();
                        foreach (Franchise f in this_year_franchises)
                        {
                            if (!ly_Playoff_order_byID.Contains((int)f.ID))
                                np_Teams.Add(f);
                        }
                        //Randomize non playoff teams
                        np_Teams = CommonUtils.ShufleList(np_Teams);

                        List<Franchise> PLayoff_Teams = new List<Franchise>();
                        foreach (int i in ly_Playoff_order_byID)
                        {
                            if (!Contraction_IDs.Contains(ly_Playoff_order_byID[i]))
                                PLayoff_Teams.Add(this_year_franchises.Where(x => x.ID == ly_Playoff_order_byID[i]).First());
                        }

                        Round_order1.AddRange(np_Teams);
                        Round_order1.AddRange(PLayoff_Teams);

                    }
                    else
                    {
                        Round_order1 = CommonUtils.ShufleList(this_year_franchises);
                    }

                    Round_order2.AddRange(Round_order1);
                    Round_order2.Reverse();

                    p_num = 1;
                    for (int rd = 1; rd <= Draft_Rounds; rd++)
                    {
                        List<Franchise> temp_Round = null;
                        if (rd % 2 == 1)
                            temp_Round = Round_order1;
                        else
                            temp_Round = Round_order2;

                        foreach (Franchise f in temp_Round)
                        {
                            r.Add(new Draft() { Franchise_ID = f.ID, Pick_Number = p_num, Round = rd });
                            p_num++;
                        }
                    }

                    break;

                case "FL":
                    Round_order1 = new List<Franchise>();
                    Round_order2 = new List<Franchise>();

                    Round_order1 = CommonUtils.ShufleList(this_year_franchises);

                    Round_order2.AddRange(Round_order1);
                    Round_order2.Reverse();

                    p_num = 1;
                    for (int rd = 1; rd <= Draft_Rounds; rd++)
                    {
                        List<Franchise> temp_Round = null;
                        if (rd % 2 == 1)
                            temp_Round = Round_order1;
                        else
                            temp_Round = Round_order2;

                        foreach (Franchise f in temp_Round)
                        {
                            r.Add(new Draft() { Franchise_ID = f.ID, Pick_Number = p_num, Round = rd });
                            p_num++;
                        }
                    }

                    break;
                case "FR":
                    p_num = 1;
                    for (int rd = 1; rd <= Draft_Rounds; rd++)
                    {

                        int rand_franchise_index = CommonUtils.getRandomNum(0, this_year_franchises.Count() - 1);

                        //Make sure the franchise doesn't already have the max number of drafts
                        if (r.Where(x => x.ID == this_year_franchises[rand_franchise_index].ID).Count() < Draft_Rounds)
                        {
                            r.Add(new Draft() { Franchise_ID = this_year_franchises[rand_franchise_index].ID, Pick_Number = p_num, Round = rd });
                            p_num++;
                        }

                    }

                    break;

                case "CR":
                    p_num = 1;
                    for (int rd = 1; rd <= Draft_Rounds; rd++)
                    {

                        int rand_franchise_index = CommonUtils.getRandomNum(0, this_year_franchises.Count() - 1);

                        r.Add(new Draft() { Franchise_ID = this_year_franchises[rand_franchise_index].ID, Pick_Number = p_num, Round = rd });
                        p_num++;

                    }

                    break;

            }


            return r;
        }

        public List<Franchise> get_ExpansionTeams(List<Franchise> this_year_franchises, List<int> ly_order)
        {
            List<Franchise> r = new List<Franchise>();

            foreach (Franchise f in this_year_franchises)
            {
                if (!ly_order.Contains((int)f.ID))
                    r.Add(f);
            }

            return r;
        }

        public List<int> get_ContractedTeams(List<Franchise> this_year_franchises, List<int> ly_order)
        {
            List<int> r = new List<int>();

            foreach (int i in ly_order)
                if (!this_year_franchises.Exists(x => x.ID == i))
                    r.Add(i);
            return r;
        }


        public Draft_Need getDraftNeeds(int Season_ID, List<Player> players, int draftRound, int DraftRounds)
        {
            Draft_Need r = new Draft_Need();

            //           List<Pos_Starter_Tot> r = new List<Pos_Starter_Tot>();
            double percent_complete = (draftRound / DraftRounds) * 100.0;

            //First decide which positions that the team does NOT want to draft
            foreach (Player_Pos pp in System.Enum.GetValues(typeof(Player_Pos)))
            {
                int iPos = (int)pp;
                int pos_count = players.Where(x => x.Pos == iPos && x.Retired == 0).Count();
                bool bTooManyPlayers = Team_Helper.isTooManyPosPlayersCamp(pp, pos_count);
                if (bTooManyPlayers || ((pp == Player_Pos.K || pp == Player_Pos.P) && (percent_complete < app_Constants.DRAFT_ROUND_PERCNT_BEFORE_KICKERS_CONSIDERED)))
                    r.Unwanted_Positions.Add(pp);
            }

            //Next, get all potential starts on the team, exluding players that are too old
            foreach (Player_Pos pp in System.Enum.GetValues(typeof(Player_Pos)))
            {
                int iPos = (int)pp;
                int pos_starter_count = 0;

                //if the position is in the unwanted positions list then don't bother investigating
                //if it should be a priority for this team's pick
                if (r.Unwanted_Positions.Contains(pp))
                    continue;

                List<Player> pList = players.Where(x => x.Pos == iPos).ToList();

                foreach (Player p in pList)
                {
                    double Overall_Grade;
                    Player_Pos pos = (Player_Pos)p.Pos;
                    if (p.Drafts.Any(x => x.Season_ID == Season_ID))
                        Overall_Grade = p.Draft_Grade;
                    else
                        Overall_Grade = Player_Helper.Create_Overall_Rating((Player_Pos)p.Pos, p.Player_Ratings.First());

                    if (Overall_Grade >= app_Constants.STARTER_MIN_OVERALL_GRADE || p.Age < app_Constants.DRAFT_STARTER_AGE_TOO_OLD)
                        pos_starter_count++;
                }

                //if the team already has the needed number of viable starters at the postiion then
                //do not make it a priority to draft that position
                if (pos_starter_count >= getNumStarters(pp))
                    continue;

                r.Wanted_Positions.Add(pp);
            }

            //Next sort the wanted positions by a random order favoring more popular positions
            r.Wanted_Positions = OrderWantedPositions(r.Wanted_Positions);

            return r;
        }
        private int getNumStarters(Player_Pos p)
        {
            int r = 0;

            switch (p)
            {
                case Player_Pos.QB:
                        r = app_Constants.STARTER_QB_PER_TEAM;
                    break;
                case Player_Pos.RB:
                    r = app_Constants.STARTER_RB_PER_TEAM;
                    break;
                case Player_Pos.WR:
                    r = app_Constants.STARTER_WR_PER_TEAM;
                    break;
                case Player_Pos.TE:
                    r = app_Constants.STARTER_TE_PER_TEAM;
                    break;
                case Player_Pos.OL:
                    r = app_Constants.STARTER_OL_PER_TEAM;
                    break;
                case Player_Pos.DL:
                    r = app_Constants.STARTER_DL_PER_TEAM;
                    break;
                case Player_Pos.LB:
                    r = app_Constants.STARTER_LB_PER_TEAM;
                    break;
                case Player_Pos.DB:
                    r = app_Constants.STARTER_DB_PER_TEAM;
                    break;
                case Player_Pos.K:
                    r = app_Constants.STARTER_K_PER_TEAM;
                    break;
                case Player_Pos.P:
                    r = app_Constants.STARTER_P_PER_TEAM;
                    break;
            }

            return r;
        }


        private List<Player_Pos> OrderWantedPositions(List<Player_Pos> pp)
        {
            List<Player_Pos> r = new List<Player_Pos>();
            List<Draft_Pos_Importance> dpi = new List<Draft_Pos_Importance>();
            foreach (Player_Pos p in pp)
            {
                int pos_importance = 0;
                switch (p)
                {
                    case Player_Pos.QB:
                        pos_importance = CommonUtils.getRandomNum(1, app_Constants.DRAFTIMPORTANCE_QB_PER_TEAM);
                        break;
                    case Player_Pos.RB:
                        pos_importance = CommonUtils.getRandomNum(1, app_Constants.DRAFTIMPORTANCE_RB_PER_TEAM);
                        break;
                    case Player_Pos.WR:
                        pos_importance = CommonUtils.getRandomNum(1, app_Constants.DRAFTIMPORTANCE_WR_PER_TEAM);
                        break;
                    case Player_Pos.TE:
                        pos_importance = CommonUtils.getRandomNum(1, app_Constants.DRAFTIMPORTANCE_TE_PER_TEAM);
                        break;
                    case Player_Pos.OL:
                        pos_importance = CommonUtils.getRandomNum(1, app_Constants.DRAFTIMPORTANCE_OL_PER_TEAM);
                        break;
                    case Player_Pos.DL:
                        pos_importance = CommonUtils.getRandomNum(1, app_Constants.DRAFTIMPORTANCE_DL_PER_TEAM);
                        break;
                    case Player_Pos.LB:
                        pos_importance = CommonUtils.getRandomNum(1, app_Constants.DRAFTIMPORTANCE_LB_PER_TEAM);
                        break;
                    case Player_Pos.DB:
                        pos_importance = CommonUtils.getRandomNum(1, app_Constants.DRAFTIMPORTANCE_DB_PER_TEAM);
                        break;
                    case Player_Pos.K:
                        pos_importance = CommonUtils.getRandomNum(1, app_Constants.DRAFTIMPORTANCE_K_PER_TEAM);
                        break;
                    case Player_Pos.P:
                        pos_importance = CommonUtils.getRandomNum(1, app_Constants.DRAFTIMPORTANCE_P_PER_TEAM);
                        break;
                }

                dpi.Add(new Draft_Pos_Importance { Pos = p, Importance = pos_importance });
            }

            r = dpi.OrderByDescending(x => x.Importance).Select(x => x.Pos).ToList();

            return r;
        }

        public Player MakePick(Draft_Need dn, List<Player> Draft_Class)
        {
            Player r = null;
            int i = 0;
            int search_limit;

            List<Player> Unsolected_players = Draft_Class.Where(x => x.Franchise_ID == null).OrderByDescending(x => x.Draft_Grade).ToList();

            foreach (Player_Pos pp in dn.Wanted_Positions)
            {
                i = 0;
                if (pp == Player_Pos.QB)
                    search_limit = app_Constants.DRAFT_QB_CHOICE_PICK_DEPTH;
                else
                    if (pp != dn.Wanted_Positions[dn.Wanted_Positions.Count() - 1])
                        search_limit = app_Constants.DRAFT_OTHER_CHOICE_PICK_DEPTH;
                    else //if this is the last wanted position then there is really no limit
                        search_limit = int.MaxValue;

                foreach (Player p in Unsolected_players)
                {
                    if (p.Pos == (int)pp)
                    {
                        r = p;
                        break;
                    }

                    if (i >= search_limit)
                        break;

                    Player_Pos p_pos = (Player_Pos)p.Pos;
                    if (pp == Player_Pos.K || pp == Player_Pos.P)
                    {
                        if (p_pos == Player_Pos.K || p_pos == Player_Pos.P)
                            i++;
                    }
                    else
                    {
                        if (p_pos != Player_Pos.K && p_pos != Player_Pos.P)
                            i++;
                    }
                }
                if (r != null)
                    break;
            }

            //Only if a draft choice was not found then take the next available pick that is not in the
            //unwanted list
            if (r == null)
            {
                foreach (Player p in Unsolected_players)
                {
                   if (!dn.Unwanted_Positions.Contains((Player_Pos)p.Pos))
                    {
                        r = p;
                        break;
                    }
                }
            }

            //Fianlly, if the draft pick has still not been made then just take the first player available in the draft
            if (r == null)
                r = Unsolected_players.First();

            return r;
        }

    }
}
