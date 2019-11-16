// This eventArgs is used to pass the team number/id when someone clicks on a team label/name
using System;

namespace SpectatorFootball
{
    public class teamEventArgs : EventArgs
    {
        public int team_num;

        public teamEventArgs(int team_num)
        {
            this.team_num = team_num;
        }
    }
}
