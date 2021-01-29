using SpectatorFootball.DAO;
using SpectatorFootball.League;
using SpectatorFootball.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SpectatorFootball.Enum;
using SpectatorFootball.Training_CampNS;

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

            //Set the starting of their draft grade which is either the overall grad or
            //draft profile grade if this is the player's first year.
            foreach (Player_and_Ratings_and_Draft p in prd_list)
            {
                Player player = p.p;
                Player_Pos ppos = (Player_Pos)player.Pos;
                if (player.Drafts.Count() == 0)
                    p.Grade = Player_Helper.Create_Overall_Rating(ppos, p.pr[0]);
                else
                    p.Grade = player.Draft_Grade;
            }

                
            foreach (Player_Pos pp in System.Enum.GetValues(typeof(Player_Pos)))
            {
                List<Player_and_Ratings_and_Draft> posResultList = null;
//        int iPos = (int)pp;
                switch (pp)
                {
                    case Player_Pos.QB:
                        posResultList = TrainingCamp_Helper.TrainingCampQB(prd_list);
                        break;
                    case Player_Pos.RB:
                        posResultList = TrainingCamp_Helper.TrainingCampRB(prd_list);
                        break;
                    case Player_Pos.WR:
                        posResultList = TrainingCamp_Helper.TrainingCampWR(prd_list);
                        break;
                    case Player_Pos.TE:
                        posResultList = TrainingCamp_Helper.TrainingCampTE(prd_list);
                        break;
                }

                //next add to the free agency transaction and player lists from the 
                //posResultList based on that bool of wether they made the team



            }
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

