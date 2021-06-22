using System.Collections.Generic;
using SpectatorFootball.Models;
using SpectatorFootball.Enum;
using SpectatorFootball.DAO;
using System;
using SpectatorFootball.DraftNS;
using SpectatorFootball.League;
using System.Linq;
using System.IO;

namespace SpectatorFootball
{
    public class Team_Services
    {

        public string FirstDuplicateTeam(List<Teams_by_Season> Team_List)
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

        public void UpdateTeam(Loaded_League_Structure lls, Teams_by_Season t)
        {
            string League_Shortname = lls.season.League_Structure_by_Season.First().Short_Name;
            string DIRPath_League = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + League_Shortname.ToUpper();
            string League_con_string = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + League_Shortname + Path.DirectorySeparatorChar + League_Shortname + "." + app_Constants.DB_FILE_EXT;

            TeamDAO tDAO = new TeamDAO();
            tDAO.UpdateTeam(t, League_con_string);
        }


    }
}
