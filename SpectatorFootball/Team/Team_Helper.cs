using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using SpectatorFootball.DAO;
using SpectatorFootball.Enum;
using SpectatorFootball.League;
using SpectatorFootball.Models;

namespace SpectatorFootball
{
    public class Team_Helper
    {
        public static Stock_Teams get_team_from_name(string team_city_name, List<Stock_Teams> t_list)
        {
            Stock_Teams r = null;

            foreach (var t in t_list)
            {
                string t_city_name = t.City + " " + t.Nickname;
                if (t_city_name.Trim() == team_city_name.Trim())
                {
                    r = t;
                    break;
                }
            }

            return r;
        }
        public static Stock_Teams ClonseStock_Team(Stock_Teams st)
        {
            Stock_Teams r = new Stock_Teams();

            var sourceProperties = st.GetType().GetProperties();
            var destProperties = r.GetType().GetProperties();

            foreach (var sourceProperty in sourceProperties)
            {
                foreach (var destProperty in destProperties)
                {
                    if (sourceProperty.Name == destProperty.Name && sourceProperty.PropertyType == destProperty.PropertyType)
                    {
                        destProperty.SetValue(r, sourceProperty.GetValue(st));
                        break;
                    }
                }
            }

            return r;
        }
        public static Teams_by_Season Clone_Team(Teams_by_Season t)
        {
            Teams_by_Season r = new Teams_by_Season();

            var sourceProperties = t.GetType().GetProperties();
            var destProperties = r.GetType().GetProperties();

            foreach (var sourceProperty in sourceProperties)
            {
                foreach (var destProperty in destProperties)
                {
                    if (sourceProperty.Name == destProperty.Name && sourceProperty.PropertyType == destProperty.PropertyType && sourceProperty.Name != "Franchise")
                    {
                        destProperty.SetValue(r, sourceProperty.GetValue(t));
                        break;
                    }
                }
            }

            return r;
        }
        public static Teams_by_Season Clonse_Team_from_Stock(Stock_Teams st)
        {

            Teams_by_Season r = new Teams_by_Season();

            var sourceProperties = st.GetType().GetProperties();
            var destProperties = r.GetType().GetProperties();

            foreach (var sourceProperty in sourceProperties)
            {
                foreach (var destProperty in destProperties)
                {
                    if (sourceProperty.Name == destProperty.Name && sourceProperty.PropertyType == destProperty.PropertyType)
                    {
                        destProperty.SetValue(r, sourceProperty.GetValue(st));
                        break;
                    }
                }
            }

            return r;

         }


        public static void CopyTeamValues(Teams_by_Season s, Teams_by_Season t)
        {

            var sourceProperties = s.GetType().GetProperties();
            var destProperties = t.GetType().GetProperties();

            foreach (var sourceProperty in sourceProperties)
            {
                foreach (var destProperty in destProperties)
                {
                    if (sourceProperty.Name == destProperty.Name && sourceProperty.PropertyType == destProperty.PropertyType)
                    {
                        destProperty.SetValue(t, sourceProperty.GetValue(s));
                        break;
                    }
                }
            }

        }

        public static bool isUniqueTeam(List<Teams_by_Season> lt, int orig_ind, Teams_by_Season new_team)
        {
            bool r = false;
            int i = 0;

            foreach (Teams_by_Season t in lt)
            {
                if (i == orig_ind)
                    continue;

                if (t.City == new_team.City && t.Nickname == new_team.Nickname)
                {
                    r = true;
                    break;
                }

                i++;
            }

            return r;
        }
        public static bool isTooManyPosPlayersCamp(Player_Pos p, int player_tot)
        {
            bool r = false;

            switch (p)
            {
                case Player_Pos.QB:
                    if (player_tot >= app_Constants.TRIANINGCAMP_QB_PER_TEAM)
                        r = true;
                    break;
                case Player_Pos.RB:
                    if (player_tot >= app_Constants.TRIANINGCAMP_RB_PER_TEAM)
                        r = true;
                    break;
                case Player_Pos.WR:
                    if (player_tot >= app_Constants.TRIANINGCAMP_WR_PER_TEAM)
                        r = true;
                    break;
                case Player_Pos.TE:
                    if (player_tot >= app_Constants.TRIANINGCAMP_TE_PER_TEAM)
                        r = true;
                    break;
                case Player_Pos.OL:
                    if (player_tot >= app_Constants.TRIANINGCAMP_OL_PER_TEAM)
                        r = true;
                    break;
                case Player_Pos.DL:
                    if (player_tot >= app_Constants.TRIANINGCAMP_DL_PER_TEAM)
                        r = true;
                    break;
                case Player_Pos.LB:
                    if (player_tot >= app_Constants.TRIANINGCAMP_LB_PER_TEAM)
                        r = true;
                    break;
                case Player_Pos.DB:
                    if (player_tot >= app_Constants.TRIANINGCAMP_DB_PER_TEAM)
                        r = true;
                    break;
                case Player_Pos.K:
                    if (player_tot >= app_Constants.TRIANINGCAMP_K_PER_TEAM)
                        r = true;
                    break;
                case Player_Pos.P:
                    if (player_tot >= app_Constants.TRIANINGCAMP_P_PER_TEAM)
                        r = true;
                    break;
            }

            return r;
        }

        public static int getNumPlayersByPosition(Player_Pos p)
        {
            int r = 0;

            switch (p)
            {
                case Player_Pos.QB:
                    r = app_Constants.QB_PER_TEAM;
                    break;
                case Player_Pos.RB:
                    r = app_Constants.RB_PER_TEAM;
                    break;
                case Player_Pos.WR:
                    r = app_Constants.WR_PER_TEAM;
                    break;
                case Player_Pos.TE:
                    r = app_Constants.TE_PER_TEAM;
                    break;
                case Player_Pos.OL:
                    r = app_Constants.OL_PER_TEAM;
                    break;
                case Player_Pos.DL:
                    r = app_Constants.DL_PER_TEAM;
                    break;
                case Player_Pos.LB:
                    r = app_Constants.LB_PER_TEAM;
                    break;
                case Player_Pos.DB:
                    r = app_Constants.DB_PER_TEAM;
                    break;
                case Player_Pos.K:
                    r = app_Constants.K_PER_TEAM;
                    break;
                case Player_Pos.P:
                    r = app_Constants.P_PER_TEAM;
                   break;
            }

            return r;
        }


        public static List<long> getAllFranchiseIDThisSeason(Loaded_League_Structure lls)
        {
            List<long> r = null;

            string League_Shortname = lls.season.League_Structure_by_Season[0].Short_Name;
            TeamDAO tda = new TeamDAO();
            string League_con_string = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + League_Shortname + Path.DirectorySeparatorChar + League_Shortname + "." + app_Constants.DB_FILE_EXT;
            r = tda.getAllFranchiseIDThisSeason(lls.season.League_Structure_by_Season[0].Season_ID, League_con_string);

            return r;
        }

        public static void FixStock_TeamColors(Stock_Teams st)
        {
            // Create a pattern for a word that starts with letter "M"  
            string pattern = @"^#([A-Fa-f0-9]{8}$";
            // Create a Regex  
            Regex rg = new Regex(pattern);

            var TeamProperties = st.GetType().GetProperties();

            foreach (var tProperty in TeamProperties)
            {
                if (tProperty.PropertyType != typeof(string)) continue;

                if (rg.IsMatch(tProperty.GetValue(st,null).ToString()))
                {
                    string s = tProperty.GetValue(st).ToString();
                    s = "#" + s.Substring(3);
                    tProperty.SetValue(st, s);
                }
            }

        }
        public static void FixTeamColors(Teams_by_Season t)
        {
            // Create a pattern for a word that starts with letter "M"  
            //            string pattern = @"^#([A-Fa-f0-9]{8}$";
            string pattern = "^#([A-Fa-f0-9]{8})$";
            // Create a Regex  
            Regex rg = new Regex(pattern);

            var TeamProperties = t.GetType().GetProperties();

            foreach (var tProperty in TeamProperties)
            {
                if (tProperty.PropertyType != typeof(string)) continue;

                string prop_val = tProperty.GetValue(t, null).ToString();
                if (rg.IsMatch(prop_val))
                {
                    string s = tProperty.GetValue(t).ToString();
                    s = "#" + s.Substring(3);
                    tProperty.SetValue(t, s);
                }
            }

        }
    }
}
