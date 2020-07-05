using SpectatorFootball.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.Drafts
{
    class Draft_Helper
    {
        public List<Draft> Create_Draft(long year, string draft_Type,int Draft_Rounds ,List<Franchise> this_year_franchises, string league_filepath)
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
                            r.Add(new Draft() { Franchise_ID = f.ID, Pick_Number = p_num, Round = rd } );
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

    }
}
