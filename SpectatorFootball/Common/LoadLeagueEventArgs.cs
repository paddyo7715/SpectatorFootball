using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.Common
{
    public class LoadLeagueEventArgs : EventArgs
    {
        public string League_Short_Name;

        public LoadLeagueEventArgs(string League_Short_Name)
        {
            this.League_Short_Name = League_Short_Name;
        }

    }
}
