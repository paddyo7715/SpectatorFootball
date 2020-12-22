using SpectatorFootball.DAO;
using SpectatorFootball.League;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.Free_Agency
{
    class FreeAgency_Helper
    {
        public bool isFreeAgencyDone(Loaded_League_Structure lls)
        {
            bool r = false;
            string League_Shortname = lls.season.League_Structure_by_Season[0].Short_Name;
            FreeAgencyDAO fad = new FreeAgencyDAO();
            string League_con_string = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + League_Shortname + Path.DirectorySeparatorChar + League_Shortname + "." + app_Constants.DB_FILE_EXT;
            r = fad.isFreeAgencyDone(lls.season.League_Structure_by_Season[0].Season_ID, League_con_string);

            return r;
        }
    }
}
