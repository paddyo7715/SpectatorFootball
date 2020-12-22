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
    class Draft_Services
    {
        //This method will assess a team's roster make a draft pick, save the draft pick to the 
        //database and then return that draft pick
        public Player Select_Draft_Pick(string League_Shortname, List<Player> Draft_Class, DraftPick d_selection, int DraftRounds)
        {
            Player r = null;

            int Season_ID = (int)d_selection.Season_ID;
            string DIRPath_League = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + League_Shortname.ToUpper();
            string League_con_string = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + League_Shortname + Path.DirectorySeparatorChar + League_Shortname + "." + app_Constants.DB_FILE_EXT;
            TeamDAO td = new TeamDAO();

            //First get the current roster for this time for this seasondraftRound, DraftRounds
            List<Player> p = td.GetTeamPlayers(Season_ID, d_selection.Franchise_ID, League_con_string);

            //Next get a structure of wanted positions, in order, and unwanted positions for the team
            int draftRound = (int)d_selection.Round;
            Draft_Helper dh = new Draft_Helper();
            Draft_Need dn = dh.getDraftNeeds(Season_ID, p, draftRound, DraftRounds);

            //Next, make the pick
            r = dh.MakePick(dn, Draft_Class);
            r.Franchise_ID = d_selection.Franchise_ID;

            //Next, save the pick to the database
            DraftDAO dd = new DraftDAO();
            dd.SelectPlayer(r, d_selection, League_con_string);

            return r;

        }
        public List<DraftPick> GetDraftList(Loaded_League_Structure lls)
        {
            List<DraftPick> r = null;
            string DIRPath_League = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + lls.season.League_Structure_by_Season[0].Short_Name.ToUpper();

            string League_con_string = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + lls.season.League_Structure_by_Season[0].Short_Name.ToUpper() + Path.DirectorySeparatorChar + lls.season.League_Structure_by_Season[0].Short_Name.ToUpper() + "." + app_Constants.DB_FILE_EXT;
            DraftDAO dd = new DraftDAO();

            r = dd.GetDraftList(lls.season.ID, League_con_string);

            foreach (DraftPick d in r)
            {
                string helmet_filename = d.helmet_filename;
                d.HelmetImage = lls.getHelmetImg(helmet_filename);

                if (d.Pick_Pos_Name != null && d.Pick_Pos_Name.Trim().Length > 0)
                {
                    string[] m = d.Pick_Pos_Name.Split(' ');
                    int ipos = int.Parse(m[0]);
                    Player_Pos ppos = (Player_Pos)ipos;
                    string pick_name = d.Pick_Pos_Name.Substring(1);
                    d.Pick_Pos_Name = ppos.ToString() + " " + pick_name;
                }
            }

            return r;
        }
        public List<Player> getDraftablePlayers(Loaded_League_Structure lls)
        {
            List<Player> r = null;
            string DIRPath_League = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + lls.season.League_Structure_by_Season[0].Short_Name.ToUpper();

            string League_con_string = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + lls.season.League_Structure_by_Season[0].Short_Name.ToUpper() + Path.DirectorySeparatorChar + lls.season.League_Structure_by_Season[0].Short_Name.ToUpper() + "." + app_Constants.DB_FILE_EXT;
            DraftDAO dd = new DraftDAO();

            r = dd.getDraftablePlayers(lls.season.ID, League_con_string);

            return r;
        }
    }
}
