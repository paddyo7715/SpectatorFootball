using SpectatorFootball.DAO;
using SpectatorFootball.DraftNS;
using SpectatorFootball.DraftsNS;
using SpectatorFootball.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.Services
{
    public class Draft_Services
    {
        //This method will assess a team's roster make a draft pick, save the draft pick to the 
        //database and then return that draft pick
        public Player Select_Draft_Pick(string League_Shortname, List<Player> Draft_Class, Draft d_selection, int DraftRounds)
        {
            Player r = null;

            int Season_ID = (int)d_selection.Season_ID;
            int Franchise_ID = (int)d_selection.Franchise_ID;
            string DIRPath_League = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + League_Shortname.ToUpper();
            string helment_img_path = DIRPath_League + Path.DirectorySeparatorChar + app_Constants.LEAGUE_HELMETS_SUBFOLDER;
            string League_con_string = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + League_Shortname + Path.DirectorySeparatorChar + League_Shortname + "." + app_Constants.DB_FILE_EXT;
            TeamDAO td = new TeamDAO();

            //First get the current roster for this time for this seasondraftRound, DraftRounds
            List<Player> p = td.GetTeamPlayers(Season_ID, Franchise_ID, League_con_string);

            //Next get a structure of wanted positions, in order, and unwanted positions for the team
            int draftRound = (int)d_selection.Round;
            Draft_Helper dh = new Draft_Helper();
            Draft_Need dn = dh.getDraftNeeds(Season_ID, p, draftRound, DraftRounds);

            //Next, make the pick
            r = dh.MakePick(dn,Draft_Class);
            r.Franchise_ID = Franchise_ID;
            d_selection.Player_ID = r.ID;

            //Next, save the pick to the database



            return r;



        }
    }
}
