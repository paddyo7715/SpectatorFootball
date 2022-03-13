using log4net;
using SpectatorFootball.DAO;
using SpectatorFootball.League;
using SpectatorFootball.Models;
using SpectatorFootball.Playoffs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.Services
{
    public class Playoff_Services
    {
        private static ILog logger = LogManager.GetLogger("RollingFile");

        public List<Playoff_Bracket_rec> getPlayoffGames(Loaded_League_Structure lls)
        {
            List<Playoff_Bracket_rec> r = null;

            long season_id = lls.season.ID;

            string League_Shortname = lls.season.League_Structure_by_Season.First().Short_Name;
            string DIRPath_League = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + League_Shortname.ToUpper();
            string League_con_string = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + League_Shortname + Path.DirectorySeparatorChar + League_Shortname + "." + app_Constants.DB_FILE_EXT;

            PlayoffsDAO pdao = new PlayoffsDAO();

            //get this years playoff teams
            List<Playoff_Teams_by_Season> pTemList = pdao.getPlayoffsTeams(season_id, League_con_string);

            //get all playoff games for this season
            List<Game> gameList = pdao.getPlayoffGames(season_id, League_con_string);

            //cycle thru each playoff team and 
            foreach (Game g in gameList)
            {
                string away_rank_sring = pTemList.Where(x => x.ID == g.Away_Team_Franchise_ID).Select(x => x.Rank).First().ToString();
                string home_rank_sring = pTemList.Where(x => x.ID == g.Home_Team_Franchise_ID).Select(x => x.Rank).First().ToString();

                string away_city_abrv = lls.season.Teams_by_Season.Where(x => x.Franchise_ID == g.Away_Team_Franchise_ID).Select(x => x.City_Abr).First();
                string home_city_abrv = lls.season.Teams_by_Season.Where(x => x.Franchise_ID == g.Home_Team_Franchise_ID).Select(x => x.City_Abr).First();

                string away_helmet_file = lls.season.Teams_by_Season.Where(x => x.Franchise_ID == g.Away_Team_Franchise_ID).Select(x => x.Helmet_Image_File).First();
                string home_helmet_file = lls.season.Teams_by_Season.Where(x => x.Franchise_ID == g.Home_Team_Franchise_ID).Select(x => x.Helmet_Image_File).First();


                r.Add(new Playoff_Bracket_rec()
                    {
                        game_id = g.ID,
                        Away_city = away_city_abrv + " (" + away_rank_sring + ")",
                        Away_HelmetImage = lls.getHelmetImg(away_helmet_file),
                        Away_Score = g.Away_Score.ToString(),
                        Home_city = home_city_abrv + " (" + home_rank_sring + ")",
                        Home_HelmetImage = lls.getHelmetImg(home_helmet_file),
                        Home_Score = g.Home_Score.ToString()
                    }) ;
            }

            return r;
        }
    }
}
