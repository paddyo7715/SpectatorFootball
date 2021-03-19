using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.Playoffs
{
    class Playoff_Helper
    {
        public static int NumPlayoffWeeks(long playoffTeams)
        {
            int r = 0;

            if (playoffTeams < 2 || playoffTeams > app_Constants.MAX_PLAYOFF_TEAMS)
                throw new Exception("Invalid Number of Playoff Teams");

            if (playoffTeams == 2)
                r = 1;
            else if (playoffTeams <= 4)
                r = 2;
            else if (playoffTeams <= 8)
                r = 3;
            else if (playoffTeams <= 16)
                r = 4;
            else if (playoffTeams <= 32)
                r = 5;
            else if (playoffTeams <= 64)
                r = 6;

            return r;
        }

        public static string GetPlayoffGameName(long Week, long conferences, int Num_Full_Playoff_Weeks, string champGame)
        {
            string r = null;

            if (Week < app_Constants.PLAYOFF_WIDLCARD_WEEK_2)
            {
                if (Num_Full_Playoff_Weeks < 5)
                    r = "Wildcard";
                else
                    r = "Wildcard 1";
            }
            else if (Week < app_Constants.PLAYOFF_WIDLCARD_WEEK_2)
                r = "Wildcard 2";
            else if (Week < app_Constants.PLAYOFF_WIDLCARD_WEEK_3)
                r = "Wildcard 3";
            else if (Week < app_Constants.PLAYOFF_DIVISIONAL_WEEK)
                r = "Divisional Playoff";
            else if (Week < app_Constants.PLAYOFF_CONFERENCE_WEEK)
            {
                if (conferences == 2)
                    r = "Conference Championship";
                else
                    r = "Quarter Finals";
            }
            else
                r = champGame;

            return r;
        }


    }
}
