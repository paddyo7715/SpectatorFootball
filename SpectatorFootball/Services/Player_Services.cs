using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpectatorFootball;
using SpectatorFootball.Enum;
using SpectatorFootball.League;
using SpectatorFootball.Models;
using SpectatorFootball.PlayerNS;
using SpectatorFootball.Team;

namespace SpectatorFootball.Services

{
    class Player_Services
    {
        public Player_Card_Data getPlayerCardData(Teams_by_Season t, Player p, Loaded_League_Structure lls)
        {
            Player_Card_Data r = new Player_Card_Data()
            {
                team = t,
                Player = p
            };

            string DIRPath_League = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + lls.season.League_Structure_by_Season[0].Short_Name.ToUpper();
            string League_con_string = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + lls.season.League_Structure_by_Season[0].Short_Name.ToUpper() + Path.DirectorySeparatorChar + lls.season.League_Structure_by_Season[0].Short_Name.ToUpper() + "." + app_Constants.DB_FILE_EXT;
            
            Player_DAO pd = new Player_DAO();
            List<Team_Player_Accum_Stats_by_year> reg_playoff_stats = pd.getPlayerStatsByYear_Reg_Playoff(p.ID, t.Season_ID, League_con_string);

            r.Regular_Season_Stats = reg_playoff_stats[0];
            r.Playoff_Stats = reg_playoff_stats[1];

            List<Two_Coll_List> Award_List = pd.getPlayerAwards(p.ID, t.Season_ID, League_con_string);
            r.Player_Awards = Award_List;
            r.Player_Ratings = pd.getPlayerRatingsAllYears(p.ID, t.Season_ID, League_con_string);

            return r;
        }
    }
}
