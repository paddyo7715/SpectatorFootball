using System.Collections.Generic;
using SpectatorFootball.Models;

namespace SpectatorFootball
{
    public class Team_Services
    {
        // This method is called when the user clicks Roll Players on the new team window
        public List<Player> Roll_Players(int team_ind)
        {
            var r = new List<Player>();
            var p = new Player_Helper();
            int i = 0;

            // offense
            for (i = 1; i <= App_Constants.QB_PER_TEAM; i++)
                r.Add(p.CreatePlayer("QB", r, team_ind, true));

            for (i = 1; i <= App_Constants.RB_PER_TEAM; i++)
                r.Add(p.CreatePlayer("RB", r, team_ind, true));

            for (i = 1; i <= App_Constants.WR_PER_TEAM; i++)
                r.Add(p.CreatePlayer("WR", r, team_ind, true));

            for (i = 1; i <= App_Constants.TE_PER_TEAM; i++)
                r.Add(p.CreatePlayer("TE", r, team_ind, true));

            for (i = 1; i <= App_Constants.OL_PER_TEAM; i++)
                r.Add(p.CreatePlayer("OL", r, team_ind, true));

            // Defense
            for (i = 1; i <= App_Constants.DL_PER_TEAM; i++)
                r.Add(p.CreatePlayer("DL", r, team_ind, true));

            for (i = 1; i <= App_Constants.LB_PER_TEAM; i++)
                r.Add(p.CreatePlayer("LB", r, team_ind, true));

            for (i = 1; i <= App_Constants.DB_PER_TEAM; i++)
                r.Add(p.CreatePlayer("DB", r, team_ind, true));

            // Special Teams
            for (i = 1; i <= App_Constants.K_PER_TEAM; i++)
                r.Add(p.CreatePlayer("K", r, team_ind, true));

            for (i = 1; i <= App_Constants.P_PER_TEAM; i++)
                r.Add(p.CreatePlayer("P", r, team_ind, true));

            return r;
        }

        public string FirstDuplicateTeam(List<Team> Team_List)
        {
            string r = null;
            var hs = new HashSet<string>();

            foreach (var t in Team_List)
            {
                string s = t.City.ToUpper().Trim() + " " + t.Nickname.ToUpper().Trim();
                if (hs.Contains(s))
                {
                    r = s;
                    break;
                }
                else
                    hs.Add(s);
            }

            return r;
        }

    }
}
