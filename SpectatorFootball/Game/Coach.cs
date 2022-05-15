using SpectatorFootball.Enum;
using SpectatorFootball.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.GameNS
{
    class Coach
    {
        private long Franchise_id { get; set; }
        private Game_Urgency gu;
        private Game g;

        public Coach(Game_Urgency gu, Game g)
        {
            this.gu = gu;
            this.g = g;
        }
        public Game_Urgency getUrgency()
        {
            Game_Urgency r = Game_Urgency.NORMAL;

            long Our_Score;
            long Their_Score;

            if (Franchise_id == g.Home_Team_Franchise_ID)
            {
                Our_Score = (long)g.Home_Score;
                Their_Score = (long)g.Away_Score;
            }
            else
            {
                Our_Score = (long)g.Away_Score;
                Their_Score = (long)g.Home_Score;
            }

            long Score_Diff = Our_Score = Their_Score;

            if (Score_Diff <= -28)
                r = Game_Urgency.VERRY_AGGRESSIVE;

            if (Score_Diff >= 28)
                r = Game_Urgency.VERRY_SAFE;

            if (g.Quarter >= 4 && Score_Diff < 0)
            {
                if (g.Time > app_Constants.GAME_QUARTER_SECONDS - 30)
                {
                    r = Game_Urgency.EXTREMELY_AGGRESSIVE;
                }
                else if (g.Time > app_Constants.GAME_QUARTER_SECONDS - 240)
                {
                    r = Game_Urgency.VERRY_AGGRESSIVE;
                }
            }

            if (g.Quarter >= 4 && Score_Diff > 0)
            {
                if (g.Time > app_Constants.GAME_QUARTER_SECONDS - 30)
                {
                    r = Game_Urgency.EXTREMELY_SAFE;
                }
                else if (g.Time > app_Constants.GAME_QUARTER_SECONDS - 240)
                {
                    r = Game_Urgency.VERRY_SAFE;
                }
            }

            if (g.Quarter == 2 && Score_Diff < 0)
            {
                if (g.Time > app_Constants.GAME_QUARTER_SECONDS - 30)
                {
                    r = Game_Urgency.EXTREMELY_AGGRESSIVE;
                }
                else if (g.Time > app_Constants.GAME_QUARTER_SECONDS - 240)
                {
                    r = Game_Urgency.VERRY_AGGRESSIVE;
                }
            }

            if (g.Quarter == 2 && Score_Diff > 0)
            {
                if (g.Time > app_Constants.GAME_QUARTER_SECONDS - 30)
                {
                    r = Game_Urgency.EXTREMELY_SAFE;
                }
                else if (g.Time > app_Constants.GAME_QUARTER_SECONDS - 240)
                {
                    r = Game_Urgency.VERRY_SAFE;
                }
            }

            return r;
        }
    }
}
