using System.Collections.Generic;

namespace SpectatorFootball
{
    public class Team_Services
    {
        // This method is called when the user clicks Roll Players on the new team window
        public List<PlayerMdl> Roll_Players(string league_DB_Connecdtion)
        {
            var r = new List<PlayerMdl>();
            var p = new Player();
            int i = 0;

            // offense
            for (i = 1; i <= App_Constants.QB_PER_TEAM; i++)
                r.Add(p.CreatePlayer(PlayerMdl.Position.QB, r, 0, ""));

            for (i = 1; i <= App_Constants.RB_PER_TEAM; i++)
                r.Add(p.CreatePlayer(PlayerMdl.Position.RB, r, 0, ""));

            for (i = 1; i <= App_Constants.WR_PER_TEAM; i++)
                r.Add(p.CreatePlayer(PlayerMdl.Position.WR, r, 0, ""));

            for (i = 1; i <= App_Constants.TE_PER_TEAM; i++)
                r.Add(p.CreatePlayer(PlayerMdl.Position.TE, r, 0, ""));

            for (i = 1; i <= App_Constants.OL_PER_TEAM; i++)
                r.Add(p.CreatePlayer(PlayerMdl.Position.OL, r, 0, ""));

            // Defense
            for (i = 1; i <= App_Constants.DL_PER_TEAM; i++)
                r.Add(p.CreatePlayer(PlayerMdl.Position.DL, r, 0, ""));

            for (i = 1; i <= App_Constants.LB_PER_TEAM; i++)
                r.Add(p.CreatePlayer(PlayerMdl.Position.LB, r, 0, ""));

            for (i = 1; i <= App_Constants.DB_PER_TEAM; i++)
                r.Add(p.CreatePlayer(PlayerMdl.Position.DB, r, 0, ""));

            // Special Teams
            for (i = 1; i <= App_Constants.K_PER_TEAM; i++)
                r.Add(p.CreatePlayer(PlayerMdl.Position.K, r, 0, ""));

            for (i = 1; i <= App_Constants.P_PER_TEAM; i++)
                r.Add(p.CreatePlayer(PlayerMdl.Position.P, r, 0, ""));

            return r;
        }

        public string FirstDuplicateTeam(List<TeamMdl> Team_List)
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
