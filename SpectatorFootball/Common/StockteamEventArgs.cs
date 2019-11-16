using System;

namespace SpectatorFootball
{
    public class StockteamEventArgs : EventArgs
    {
        public int team_ind;

        public StockteamEventArgs(int team_ind)
        {
            this.team_ind = team_ind;
        }
    }
}
