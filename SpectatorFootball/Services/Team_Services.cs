using System.Collections.Generic;
using SpectatorFootball.Models;
using SpectatorFootball.Enum;

namespace SpectatorFootball
{
    public class Team_Services
    {
        // This method is called when the user clicks Roll Players on the new team window
        public List<Player> Roll_Players(int team_ind, ref int p_id)
        {
            var r = new List<Player>();
            var p = new Player_Helper();
            int i = 0;

            // offense
            for (i = 1; i <= app_Constants.QB_PER_TEAM; i++)
                r.Add(p.CreatePlayer(Player_Pos.QB, r, team_ind, ref p_id, true));

            for (i = 1; i <= app_Constants.RB_PER_TEAM; i++)
                r.Add(p.CreatePlayer(Player_Pos.RB, r, team_ind, ref p_id, true));

            for (i = 1; i <= app_Constants.WR_PER_TEAM; i++)
                r.Add(p.CreatePlayer(Player_Pos.WR, r, team_ind, ref p_id, true));

            for (i = 1; i <= app_Constants.TE_PER_TEAM; i++)
                r.Add(p.CreatePlayer(Player_Pos.TE, r, team_ind, ref p_id, true));

            for (i = 1; i <= app_Constants.OL_PER_TEAM; i++)
                r.Add(p.CreatePlayer(Player_Pos.OL, r, team_ind, ref p_id, true));

            // Defense
            for (i = 1; i <= app_Constants.DL_PER_TEAM; i++)
                r.Add(p.CreatePlayer(Player_Pos.DL, r, team_ind, ref p_id, true));

            for (i = 1; i <= app_Constants.LB_PER_TEAM; i++)
                r.Add(p.CreatePlayer(Player_Pos.LB, r, team_ind, ref p_id, true));

            for (i = 1; i <= app_Constants.DB_PER_TEAM; i++)
                r.Add(p.CreatePlayer(Player_Pos.DB, r, team_ind, ref p_id, true));

            // Special Teams
            for (i = 1; i <= app_Constants.K_PER_TEAM; i++)
                r.Add(p.CreatePlayer(Player_Pos.K, r, team_ind, ref p_id, true));

            for (i = 1; i <= app_Constants.P_PER_TEAM; i++)
                r.Add(p.CreatePlayer(Player_Pos.P, r, team_ind, ref p_id, true));

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
