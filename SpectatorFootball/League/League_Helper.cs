using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpectatorFootball.Models;
using SpectatorFootball.PlayerNS;
using SpectatorFootball.Enum;
using log4net;
using System.Windows.Controls;
using System.IO;
using SpectatorFootball.Common;

namespace SpectatorFootball.League
{
    class League_Helper
    {
        private static ILog logger = LogManager.GetLogger("RollingFile");
        public static League_Structure_by_Season Clone_League(League_Structure_by_Season l)
        {
            League_Structure_by_Season r = new League_Structure_by_Season();

            var sourceProperties = l.GetType().GetProperties();
            var destProperties = r.GetType().GetProperties();

            foreach (var sourceProperty in sourceProperties)
            {
                foreach (var destProperty in destProperties)
                {
                    if (sourceProperty.Name == destProperty.Name && sourceProperty.PropertyType == destProperty.PropertyType)
                    {
                        destProperty.SetValue(r, sourceProperty.GetValue(l));
                        break;
                    }
                }
            }

            return r;
        }

        private static int getRandomPos()
        {
            int r = 0;
            int rand_num = CommonUtils.getRandomNum(1, app_Constants.REGULAR_SEASON_TEAM_PLAYER_COUNT);

            int numQB = app_Constants.QB_PER_TEAM;
            int numRB = app_Constants.RB_PER_TEAM;
            int numWR = app_Constants.WR_PER_TEAM;
            int numTE = app_Constants.TE_PER_TEAM;
            int numOL = app_Constants.OL_PER_TEAM;

            int numDL = app_Constants.DL_PER_TEAM;
            int numLB = app_Constants.LB_PER_TEAM;
            int numDB = app_Constants.DB_PER_TEAM;

            int numK = app_Constants.K_PER_TEAM;

            if (rand_num <= numQB)
                r = 0;
            else if (rand_num <= (numQB + numRB))
                r = 1;
            else if (rand_num <= (numQB + numRB + numWR))
                r = 2;
            else if (rand_num <= (numQB + numRB + numWR + numTE))
                r = 3;
            else if (rand_num <= (numQB + numRB + numWR + numTE + numOL))
                r = 4;
            else if (rand_num <= (numQB + numRB + numWR + numTE + numOL + numDL))
                r = 5;
            else if (rand_num <= (numQB + numRB + numWR + numTE + numOL + numDL + numLB))
                r = 6;
            else if (rand_num <= (numQB + numRB + numWR + numTE + numOL + numDL + numLB + numDB))
                r = 7;
            else if (rand_num <= (numQB + numRB + numWR + numTE + numOL + numDL + numLB + numDB + numK))
                r = 8;
            else
                r = 9;

            return r;
        }
        public static List<Player> Create_New_Players(int num_players)
        {

            List<Player> r = new List<Player>();
            var iTotal_Player_Positions = System.Enum.GetNames(typeof(Player_Pos)).Length;

            for (int i = 0; i < num_players; i++)
            {

//                int int_pos_num = CommonUtils.getRandomNum(0, iTotal_Player_Positions - 1);
                int int_pos_num = getRandomPos();

                Player_Pos Pos = (Player_Pos)int_pos_num;

                logger.Debug("i=" + i + " Pos=" + Pos.ToString());

                Player p = Player_Helper.CreatePlayer(Pos, true,false,true);
                r.Add(p);
            }

            return r;
        }
        public List<League_Helmet> getAllTeamHelmets(string shortname, List<Teams_by_Season> tbs)
        {
            List<League_Helmet> r = new List<League_Helmet>();
            string DIRPath_League = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + shortname.ToUpper();
            string helment_img_path = DIRPath_League + Path.DirectorySeparatorChar + app_Constants.LEAGUE_HELMETS_SUBFOLDER;

            foreach (Teams_by_Season t in tbs)
            {
                League_Helmet lh = new League_Helmet();
                lh.Image = CommonUtils.getImageorBlank(helment_img_path + Path.DirectorySeparatorChar + t.Helmet_Image_File);
                lh.Helmet_File = t.Helmet_Image_File;
                r.Add(lh);
            }

            return r;
        }
        public static List_and_Default getAllLeagueStructures()
        {
            List_and_Default r = new List_and_Default();

            r.source_list.Add("8 teams 2 divisions 0 conferences");
            r.source_list.Add("10 teams 2 divisions 0 conferences");
            r.source_list.Add("12 teams 2 divisions 0 conferences");
            r.source_list.Add("12 teams 3 divisions 0 conferences");
            r.source_list.Add("14 teams 2 divisions 0 conferences");
            r.source_list.Add("16 teams 4 divisions 2 conferences");
            r.source_list.Add("16 teams 2 divisions 2 conferences");
            r.source_list.Add("18 teams 3 divisions 0 conferences");
            r.source_list.Add("20 teams 4 divisions 2 conferences");
            r.source_list.Add("20 teams 2 divisions 2 conferences");
            r.source_list.Add("24 teams 4 divisions 2 conferences");
            r.source_list.Add("24 teams 6 divisions 2 conferences");
            r.source_list.Add("28 teams 4 divisions 2 conferences");
            r.source_list.Add("30 teams 6 divisions 2 conferences");
            r.source_list.Add("32 teams 8 divisions 2 conferences");
            r.source_list.Add("36 teams 6 divisions 2 conferences");
            r.source_list.Add("40 teams 8 divisions 2 conferences");
            r.source_list.Add("40 teams 4 divisions 2 conferences");

            r.selected = "32 teams 8 divisions 2 conferences";

            return r;
        }
        public static List_and_Default getWeeksforStructure(string lstructure)
        {
            List_and_Default r = new List_and_Default();

            switch (lstructure)
            {
                case "8 teams 2 divisions 0 conferences":
                    r.source_list.Add("8,9");
                    r.source_list.Add("10,11");
                    r.source_list.Add("12,13");

                    r.selected = "10,11";
                    break;
                case "10 teams 2 divisions 0 conferences":
                    r.source_list.Add("10,11");
                    r.source_list.Add("12,13");
                    r.source_list.Add("14,16");
                    r.source_list.Add("16,18");

                    r.selected = "12,13";
                    break;
                case "12 teams 2 divisions 0 conferences":
                    r.source_list.Add("14,16");
                    r.source_list.Add("16,18");
                    r.source_list.Add("18,20");

                    r.selected = "14,16";
                    break;
                case "12 teams 3 divisions 0 conferences":
                    r.source_list.Add("10,11");
                    r.source_list.Add("12,13");
                    r.source_list.Add("14,16");
                    r.source_list.Add("16,18");
                    r.source_list.Add("18,20");

                    r.selected = "18,20";
                    break;
                case "14 teams 2 divisions 0 conferences":
                    r.source_list.Add("14,16");
                    r.source_list.Add("16,18");
                    r.source_list.Add("18,20");

                    r.selected = "14,16";
                    break;
                case "16 teams 4 divisions 2 conferences":
                    r.source_list.Add("8,9");
                    r.source_list.Add("10,11");
                    r.source_list.Add("12,13");
                    r.source_list.Add("14,16");
                    r.source_list.Add("16,18");
                    r.source_list.Add("18,20");
                    r.source_list.Add("20,22");
                    r.source_list.Add("22,24");
                    r.source_list.Add("24,26");

                    r.selected = "14,16";
                    break;
                case "16 teams 2 divisions 2 conferences":
                    r.source_list.Add("16,18");
                    r.source_list.Add("18,20");
                    r.source_list.Add("20,22");
                    r.source_list.Add("22,24");
                    r.source_list.Add("24,26");

                    r.selected = "20,22";
                    break;
                case "18 teams 3 divisions 0 conferences":
                    r.source_list.Add("10,11");
                    r.source_list.Add("12,13");
                    r.source_list.Add("14,16");
                    r.source_list.Add("16,18");
                    r.source_list.Add("18,20");
                    r.source_list.Add("20,22");
                    r.source_list.Add("22,24");
                    r.source_list.Add("24,26");

                    r.selected = "16,18";
                    break;
                case "20 teams 4 divisions 2 conferences":
                    r.source_list.Add("10,11");
                    r.source_list.Add("12,13");
                    r.source_list.Add("14,16");
                    r.source_list.Add("16,18");
                    r.source_list.Add("18,20");
                    r.source_list.Add("20,22");
                    r.source_list.Add("22,24");
                    r.source_list.Add("24,26");

                    r.selected = "16,18";
                    break;
                case "20 teams 2 divisions 2 conferences":
                    r.source_list.Add("20,22");
                    r.source_list.Add("22,24");
                    r.source_list.Add("24,26");

                    r.selected = "20,22";
                    break;
                case "24 teams 4 divisions 2 conferences":
                    r.source_list.Add("12,13");
                    r.source_list.Add("14,16");
                    r.source_list.Add("16,18");
                    r.source_list.Add("18,20");
                    r.source_list.Add("20,22");
                    r.source_list.Add("22,24");
                    r.source_list.Add("24,26");

                    r.selected = "16,18";
                    break;
                case "24 teams 6 divisions 2 conferences":
                    r.source_list.Add("8,9");
                    r.source_list.Add("10,11");
                    r.source_list.Add("12,13");
                    r.source_list.Add("14,16");
                    r.source_list.Add("16,18");
                    r.source_list.Add("18,20");
                    r.source_list.Add("20,22");
                    r.source_list.Add("22,24");
                    r.source_list.Add("24,26");

                    r.selected = "16,18";
                    break;
                case "28 teams 4 divisions 2 conferences":
                    r.source_list.Add("14,16");
                    r.source_list.Add("16,18");
                    r.source_list.Add("18,20");
                    r.source_list.Add("20,22");
                    r.source_list.Add("22,24");
                    r.source_list.Add("24,26");

                    r.selected = "16,18";
                    break;
                case "30 teams 6 divisions 2 conferences":
                    r.source_list.Add("10,11");
                    r.source_list.Add("12,13");
                    r.source_list.Add("14,16");
                    r.source_list.Add("16,18");
                    r.source_list.Add("18,20");
                    r.source_list.Add("20,22");
                    r.source_list.Add("22,24");
                    r.source_list.Add("24,26");

                    r.selected = "16,18";
                    break;
                case "32 teams 8 divisions 2 conferences":
                    r.source_list.Add("8,9");
                    r.source_list.Add("10,11");
                    r.source_list.Add("12,13");
                    r.source_list.Add("14,16");
                    r.source_list.Add("16,18");
                    r.source_list.Add("18,20");
                    r.source_list.Add("20,22");
                    r.source_list.Add("22,24");
                    r.source_list.Add("24,26");

                    r.selected = "16,18";
                    break;
                case "36 teams 6 divisions 2 conferences":
                    r.source_list.Add("12,13");
                    r.source_list.Add("14,16");
                    r.source_list.Add("16,18");
                    r.source_list.Add("18,20");
                    r.source_list.Add("20,22");
                    r.source_list.Add("22,24");
                    r.source_list.Add("24,26");

                    r.selected = "18,20";
                    break;
                case "40 teams 8 divisions 2 conferences":
                    r.source_list.Add("10,11");
                    r.source_list.Add("12,13");
                    r.source_list.Add("14,16");
                    r.source_list.Add("16,18");
                    r.source_list.Add("18,20");
                    r.source_list.Add("20,22");
                    r.source_list.Add("22,24");
                    r.source_list.Add("24,26");

                    r.selected = "20,22";
                    break;
                case "40 teams 4 divisions 2 conferences":
                    r.source_list.Add("20,22");
                    r.source_list.Add("22,24");
                    r.source_list.Add("24,26");

                    r.selected = "24,26";
                    break;
            }

            return r;
        }
        public static List_and_Default getPlayoffGamesforStructure(string lstructure)
        {
            List_and_Default r = new List_and_Default();

            switch (lstructure)
            {
                case "8 teams 2 divisions 0 conferences":
                    r.source_list.Add("2");
                    r.source_list.Add("3"); 
                    r.source_list.Add("4"); 
                    r.source_list.Add("6"); 

                    r.selected = "4";
                    break;
                case "10 teams 2 divisions 0 conferences":
                    r.source_list.Add("2");
                    r.source_list.Add("3");
                    r.source_list.Add("4");
                    r.source_list.Add("6");
                    r.source_list.Add("8");

                    r.selected = "4";
                    break;
                case "12 teams 2 divisions 0 conferences":
                    r.source_list.Add("2");
                    r.source_list.Add("3");
                    r.source_list.Add("4");
                    r.source_list.Add("6");
                    r.source_list.Add("8");
                    r.source_list.Add("10");

                    r.selected = "4";
                    break;
                case "12 teams 3 divisions 0 conferences":
                    r.source_list.Add("3");
                    r.source_list.Add("4");
                    r.source_list.Add("5");
                    r.source_list.Add("6");
                    r.source_list.Add("8");
                    r.source_list.Add("10");

                    r.selected = "4";
                    break;
                case "14 teams 2 divisions 0 conferences":
                    r.source_list.Add("2");
                    r.source_list.Add("3");
                    r.source_list.Add("4");
                    r.source_list.Add("6");
                    r.source_list.Add("8");
                    r.source_list.Add("10");
                    r.source_list.Add("12");

                    r.selected = "4";
                    break;
                case "16 teams 4 divisions 2 conferences":
                    r.source_list.Add("4");
                    r.source_list.Add("6");
                    r.source_list.Add("8");
                    r.source_list.Add("10");
                    r.source_list.Add("12");
                    r.source_list.Add("14");

                    r.selected = "6";
                    break;
                case "16 teams 2 divisions 0 conferences":
                    r.source_list.Add("2");
                    r.source_list.Add("3");
                    r.source_list.Add("4");
                    r.source_list.Add("6");
                    r.source_list.Add("8");
                    r.source_list.Add("10");
                    r.source_list.Add("12");
                    r.source_list.Add("14");

                    r.selected = "6";
                    break;
                case "18 teams 3 divisions 0 conferences":
                    r.source_list.Add("3");
                    r.source_list.Add("4");
                    r.source_list.Add("5");
                    r.source_list.Add("6");
                    r.source_list.Add("8");
                    r.source_list.Add("10");
                    r.source_list.Add("12");
                    r.source_list.Add("14");
                    r.source_list.Add("16");

                    r.selected = "4";
                    break;
                case "20 teams 4 divisions 2 conferences":
                    r.source_list.Add("4");
                    r.source_list.Add("6");
                    r.source_list.Add("8");
                    r.source_list.Add("10");
                    r.source_list.Add("12");
                    r.source_list.Add("14");
                    r.source_list.Add("16");
                    r.source_list.Add("18");

                    r.selected = "4";
                    break;
                case "20 teams 2 divisions 2 conferences":
                    r.source_list.Add("2");
                    r.source_list.Add("4");
                    r.source_list.Add("6");
                    r.source_list.Add("8");
                    r.source_list.Add("10");
                    r.source_list.Add("12");
                    r.source_list.Add("14");
                    r.source_list.Add("16");
                    r.source_list.Add("18");

                    r.selected = "4";
                    break;
                case "24 teams 4 divisions 2 conferences":
                    r.source_list.Add("4");
                    r.source_list.Add("6");
                    r.source_list.Add("8");
                    r.source_list.Add("10");
                    r.source_list.Add("12");
                    r.source_list.Add("14");
                    r.source_list.Add("16");
                    r.source_list.Add("18");
                    r.source_list.Add("20");
                    r.source_list.Add("22");

                    r.selected = "6";
                    break;
                case "24 teams 6 divisions 2 conferences":
                    r.source_list.Add("6");
                    r.source_list.Add("8");
                    r.source_list.Add("10");
                    r.source_list.Add("12");
                    r.source_list.Add("14");
                    r.source_list.Add("16");
                    r.source_list.Add("18");
                    r.source_list.Add("20");
                    r.source_list.Add("22");

                    r.selected = "8";
                    break;
                case "28 teams 4 divisions 2 conferences":
                    r.source_list.Add("4");
                    r.source_list.Add("6");
                    r.source_list.Add("8");
                    r.source_list.Add("10");
                    r.source_list.Add("12");
                    r.source_list.Add("14");
                    r.source_list.Add("16");
                    r.source_list.Add("18");
                    r.source_list.Add("20");
                    r.source_list.Add("22");
                    r.source_list.Add("24");
                    r.source_list.Add("26");

                    r.selected = "6";
                    break;
                case "30 teams 6 divisions 2 conferences":
                    r.source_list.Add("6");
                    r.source_list.Add("8");
                    r.source_list.Add("10");
                    r.source_list.Add("12");
                    r.source_list.Add("14");
                    r.source_list.Add("16");
                    r.source_list.Add("18");
                    r.source_list.Add("20");
                    r.source_list.Add("22");
                    r.source_list.Add("24");
                    r.source_list.Add("26");
                    r.source_list.Add("28");

                    r.selected = "10";
                    break;
                case "32 teams 8 divisions 2 conferences":
                    r.source_list.Add("8");
                    r.source_list.Add("10");
                    r.source_list.Add("12");
                    r.source_list.Add("14");
                    r.source_list.Add("16");
                    r.source_list.Add("18");
                    r.source_list.Add("20");
                    r.source_list.Add("22");
                    r.source_list.Add("24");
                    r.source_list.Add("26");
                    r.source_list.Add("28");
                    r.source_list.Add("30");

                    r.selected = "14";
                    break;
                case "36 teams 6 divisions 2 conferences":
                    r.source_list.Add("6");
                    r.source_list.Add("8");
                    r.source_list.Add("10");
                    r.source_list.Add("12");
                    r.source_list.Add("14");
                    r.source_list.Add("16");
                    r.source_list.Add("18");
                    r.source_list.Add("20");
                    r.source_list.Add("22");
                    r.source_list.Add("24");
                    r.source_list.Add("26");
                    r.source_list.Add("28");
                    r.source_list.Add("30");
                    r.source_list.Add("32");
                    r.source_list.Add("34");

                    r.selected = "14";
                    break;
                case "40 teams 8 divisions 2 conferences":
                    r.source_list.Add("8");
                    r.source_list.Add("10");
                    r.source_list.Add("12");
                    r.source_list.Add("14");
                    r.source_list.Add("16");
                    r.source_list.Add("18");
                    r.source_list.Add("20");
                    r.source_list.Add("22");
                    r.source_list.Add("24");
                    r.source_list.Add("26");
                    r.source_list.Add("28");
                    r.source_list.Add("30");
                    r.source_list.Add("32");
                    r.source_list.Add("34");
                    r.source_list.Add("36");
                    r.source_list.Add("38");

                    r.selected = "14";
                    break;
                case "40 teams 4 divisions 2 conferences":
                    r.source_list.Add("4");
                    r.source_list.Add("6");
                    r.source_list.Add("8");
                    r.source_list.Add("10");
                    r.source_list.Add("12");
                    r.source_list.Add("14");
                    r.source_list.Add("16");
                    r.source_list.Add("18");
                    r.source_list.Add("20");
                    r.source_list.Add("22");
                    r.source_list.Add("24");
                    r.source_list.Add("26");
                    r.source_list.Add("28");
                    r.source_list.Add("30");
                    r.source_list.Add("32");
                    r.source_list.Add("34");
                    r.source_list.Add("36");
                    r.source_list.Add("38");

                    r.selected = "14";
                    break;
            }

            return r;
        }
        public static League_State DetermineLeagueState(bool Latest_year, int[] m)
        {
            League_State r;

            r = League_State.Season_Started;

            if (!Latest_year)
                r = League_State.Previous_Year;
            else
            {
                int draft_not_done = m[0];
                int draft_started = m[1];
                int free_agency_started = m[2];
                int teams_lt_tcamp_players = m[3];
                int teamsnotFull_Count = m[4];
                int training_camp_Started = m[5];
                int Regualr_Season_Started = m[6];
                int Regualar_Season_done = m[7];
                int Playoffs_Started = m[8];
                int Champ_game_played = m[9];
                int player_awards_done = m[10];

                //Is Draft done?
                if (draft_started == 1)
                {
                    if (draft_not_done == 1)
                        r = League_State.Draft_Started;
                    else
                        r = League_State.Draft_Completed;
                }

                //What about free agency
                if (free_agency_started == 1)
                {
                    if (teams_lt_tcamp_players == 0)
                        r = League_State.FreeAgency_Completed;
                    else
                        r = League_State.FreeAgency_Started;
                }

                //What about training camp
                if (training_camp_Started > 0)
                {
                    if (teamsnotFull_Count == 0)
                        r = League_State.Training_Camp_Ended;
                    else
                        r = League_State.Training_Camp_Started;
                }

                //What about the regular season
                if (Regualr_Season_Started > 0)
                {
                    if (Regualar_Season_done > 0)
                        r = League_State.Regular_Season_Ended;
                    else
                        r = League_State.Regular_Season_in_Progress;
                }

                //What about playoffs
                if (Playoffs_Started == 1)
                {
                    if (Champ_game_played == 0)
                        r = League_State.Playoffs_In_Progress;
                    else
                        r = League_State.Playoffs_Ended;
                }

                //Has the season been ended.  Not sure if this is needed since
                //if the season is ended, wouldn't it be a previous season?
                if (player_awards_done > 0)
                    r = League_State.Season_Ended;
            }
            return r;
        }

        public static List<Team_Wins_Loses_rec> getFinalRegSeasonRecords(List<Standings_Row> Standings, Game g, long num_conf, long num_teams)
        {
            List<Team_Wins_Loses_rec> r = new List<Team_Wins_Loses_rec>();

            int i = 1;
            long team_midpoint = num_teams / num_conf;
            foreach (Standings_Row sr in Standings)
            {
                Team_Wins_Loses_rec w = new Team_Wins_Loses_rec()
                {
                    Conf_Num = i <= team_midpoint ? 1 : 2,
                    Div_Num = sr.Div_Num,
                    Franchise_ID = sr.Franchise_ID,
                    wins = sr.wins,
                    loses = sr.loses,
                    ties = sr.ties,
                    pointsfor = sr.pointsfor,
                    pointagainst = sr.pointagainst,
                    Team_Name = sr.Team_Name,
                    winlossRating = (sr.wins * 2) + (sr.ties) - (sr.loses * 2),
                    Random_Number = CommonUtils.getRandomNum(1, 10000)
                };

                i++;
            }

            r = r.OrderBy(x => num_conf).OrderBy(x => x.Div_Num)
            .OrderByDescending(x => x.winlossRating)
            .OrderByDescending(x => x.pointsfor - x.pointagainst)
            .ThenByDescending(x => x.Random_Number).ToList();

            return r;
        }
    }
}

