using System.Collections.Generic;
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
                    if (sourceProperty.Name == destProperty.Name && sourceProperty.PropertyType == destProperty.PropertyType)
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

            Teams_by_Season r = new SpectatorFootball.Models.Team();

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

    }
}
