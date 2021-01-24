using SpectatorFootball.DAO;
using SpectatorFootball.League;
using SpectatorFootball.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SpectatorFootball.Services
{
    class TrainingCamp_Services
    {
        public List<TrainingCampStatus> getTrainingCampStatuses(Loaded_League_Structure lls)
        {
            List<TrainingCampStatus> r = null;

            string League_Shortname = lls.season.League_Structure_by_Season[0].Short_Name;
            long season_id = lls.season.ID;

            string DIRPath_League = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + League_Shortname.ToUpper();
            string League_con_string = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + League_Shortname + Path.DirectorySeparatorChar + League_Shortname + "." + app_Constants.DB_FILE_EXT;
            TrainingCampDAO tcd = new TrainingCampDAO();

            r = tcd.getTrainingCampStatuses(season_id, League_con_string);

            foreach (TrainingCampStatus d in r)
            {
                string helmet_filename = d.helmet_filename;
                d.HelmetImage = lls.getHelmetImg(helmet_filename);
            }

            return r;
        }

        public void Execute_Team_TrainingCamp(long franchise_id, long season_id, string league_filepath)
        {
            TrainingCampDAO tDAO = new TrainingCampDAO();
            List<Player_and_Ratings_and_Draft> prd_list = tDAO.getTrainingCampPlayers(franchise_id, season_id, league_filepath);
            List<Player> PlayersMadeTeam = new List<Player>();
            List<Player> PlayersCut = new List<Player>();
            List<Free_Agency> CutTransactions = new List<Free_Agency>();

            /*
                        create loop positions for each position call a different method in trainingcamp_helper
                        and pass in a list of just that position on the team of Player_and_Ratings_and_Draft
                         in the method I will set the madeteam boolean
                            when returning from the set the jersey number.  I can check all players in prd_list
                            to see if the proposed number is used or not.
            */
        }
    }
}

