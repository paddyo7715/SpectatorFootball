using SpectatorFootball.DAO;
using SpectatorFootball.DraftNS;
using SpectatorFootball.DraftsNS;
using SpectatorFootball.Enum;
using SpectatorFootball.League;
using SpectatorFootball.Models;
using SpectatorFootball.Team;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.Services
{
    public class Injuries_Services
    {
        public List<Injury> GetTeamInjuredPlayers(Loaded_League_Structure lls, long f_id)
        {
            List<Injury> r = null;
            string DIRPath_League = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + lls.season.League_Structure_by_Season[0].Short_Name.ToUpper();

            string League_con_string = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + lls.season.League_Structure_by_Season[0].Short_Name.ToUpper() + Path.DirectorySeparatorChar + lls.season.League_Structure_by_Season[0].Short_Name.ToUpper() + "." + app_Constants.DB_FILE_EXT;
            InjuriesDAO id = new InjuriesDAO();

            r = id.GetTeamInjuredPlayers(lls.season.ID, f_id, League_con_string);

            return r;
        }

        public List<League_Injuries> GetLeagueInjuredPlayers(Loaded_League_Structure lls)
        {
            List<League_Injuries> r = null;
            string DIRPath_League = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + lls.season.League_Structure_by_Season[0].Short_Name.ToUpper();

            string League_con_string = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + lls.season.League_Structure_by_Season[0].Short_Name.ToUpper() + Path.DirectorySeparatorChar + lls.season.League_Structure_by_Season[0].Short_Name.ToUpper() + "." + app_Constants.DB_FILE_EXT;
            InjuriesDAO id = new InjuriesDAO();

            r = id.GetLeagueInjuredPlayers(lls.season.ID, League_con_string);

            foreach (League_Injuries d in r)
            {
                string helmet_filename = d.helmet_filename;
                d.HelmetImage = lls.getHelmetImg(helmet_filename);
            }

            return r;
        }
    }
}
