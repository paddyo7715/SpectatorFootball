using System.Collections.Generic;

namespace SpectatorFootball
{
    public class Team
    {
        public static TeamMdl get_team_from_name(string team_city_name, List<TeamMdl> t_list)
        {
            TeamMdl r = null;

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
    }
}
