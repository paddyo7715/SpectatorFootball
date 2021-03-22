using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpectatorFootball.Enum;
using SpectatorFootball.Models;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace SpectatorFootball.League
{
    public class Loaded_League_Structure
    {
        public League_State LState { get; set; }
        public long Current_Year{ get; set; }
        public List<Standings_Row> Standings = null;
        public Season season = null;
        public List<Season> AllSeasons = null;

        public List<League_Helmet> Team_Helmets = null;

        public BitmapImage getHelmetImg(string f)
        {
            return Team_Helmets.Where(x => x.Helmet_File.ToUpper() == f.ToUpper()).Select(x => x.Image).First();
        }

        public string getTeamStandings(string sCityNickname)
        {
            string r = null;

            Standings_Row sr = Standings.Where(x => x.Team_Name == sCityNickname).First();

            r = sr.wins.ToString() + "-" + sr.loses.ToString();

            if (sr.ties > 0)
                r += "-" + sr.ties.ToString();

            return r;
        }

    }

}
