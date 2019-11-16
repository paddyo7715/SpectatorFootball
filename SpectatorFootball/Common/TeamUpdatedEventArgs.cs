using System;

namespace SpectatorFootball
{
    public class TeamUpdatedEventArgs : EventArgs
    {
        public bool team_upd;

        public TeamUpdatedEventArgs(bool team_upd)
        {
            this.team_upd = team_upd;
        }
    }
}
