using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.League
{
    public class Standings_Row
    {
        int Team_ID = 0;
        string Team_Name = null;
        string clinch_char = null;
        int wins = 0;
        int loses = 0;
        int ties = 0;
        int winpct;
        int pointsfor = 0;
        int pointagainst = 0;
        string streak_char = null;
    }

}
