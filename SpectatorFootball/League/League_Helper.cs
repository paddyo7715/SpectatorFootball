using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpectatorFootball.Models;
using SpectatorFootball.Enum;
using log4net;
using System.Windows.Controls;
using System.IO;

namespace SpectatorFootball.League
{
    class League_Helper
    {
        private static ILog logger = LogManager.GetLogger("RollingFile");
        public static League_Structure_by_Season Clone_League(League_Structure_by_Season l)
        {
            League_Structure_by_Season r = new League_Structure_by_Season();

            var sourceProperties = l.GetType().GetProperties();
            var destProperties = r.GetType().GetProperties();

            foreach (var sourceProperty in sourceProperties)
            {
                foreach (var destProperty in destProperties)
                {
                    if (sourceProperty.Name == destProperty.Name && sourceProperty.PropertyType == destProperty.PropertyType)
                    {
                        destProperty.SetValue(r, sourceProperty.GetValue(l));
                        break;
                    }
                }
            }

            return r;
        }

        public static List<Player> Create_New_Players(int num_players)
        {

            List<Player> r = new List<Player>();
            var iTotal_Player_Positions = System.Enum.GetNames(typeof(Player_Pos)).Length;

            for (int i = 0; i < num_players; i++)
            {
                int int_pos_num = CommonUtils.getRandomNum(0, iTotal_Player_Positions - 1);
                Player_Pos Pos = (Player_Pos)int_pos_num;

                logger.Debug("i=" + i + " Pos=" + Pos.ToString());

                Player p = Player_Helper.CreatePlayer(Pos, true);

                r.Add(p);
            }

            return r;
        }
        public List<League_Helmet> getAllTeamHelmets(string shortname, List<Teams_by_Season> tbs)
        {
            List<League_Helmet> r = new List<League_Helmet>();
            string DIRPath_League = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + shortname.ToUpper();
            string helment_img_path = DIRPath_League + Path.DirectorySeparatorChar + app_Constants.LEAGUE_HELMETS_SUBFOLDER;

            foreach (Teams_by_Season t in tbs)
            {
                League_Helmet lh = new League_Helmet();
                lh.Image = CommonUtils.getImageorBlank(helment_img_path + Path.DirectorySeparatorChar + t.Helmet_Image_File);
                lh.Helmet_File = t.Helmet_Image_File;
                r.Add(lh);
            }

            return r;
        }


    }
}

