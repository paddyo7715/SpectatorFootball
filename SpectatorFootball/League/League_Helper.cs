using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpectatorFootball.Models;
using SpectatorFootball.Enum;

namespace SpectatorFootball.League
{
    class League_Helper
    {

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
                int int_pos_num = CommonUtils.getRandomNum(0,iTotal_Player_Positions-1);
                Player_Pos Pos = (Player_Pos)int_pos_num;

                Player p = Player_Helper.CreatePlayer(Pos, true);
                r.Add(p);
            }

            return r;
        }

    }
}
