using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpectatorFootball.Enum;

namespace SpectatorFootball.NarrationAndText
{
    public class Announcer
    {
        private List<string> Kickoff_Kick_List = null;
        private List<string> Kickoff_Caught_List = null;

        public Announcer()
        {
            Kickoff_Kick_List = new List<string>()
                {"Here we go",
                "Here's the kickoff",
                "[PLAYER_NAME] with the kickoff"};

            Kickoff_Caught_List = new List<string>()
                {"He fields it at the [YARD_LINE]",
                "[PLAYER_NAME] fields it at the [YARD_LINE]",
                "Fielded at the [YARD_LINE]",
                "Taken at the [YARD_LINE]"};


        }

        public string Announce_InPlay(announce_event ann_event, string player_last_name, string yardline)
        {
            string r = null;

            switch (ann_event)
            {
                case announce_event.KICKOFF_KICKED:
                    r = CommonUtils.ShufleList(Kickoff_Kick_List).First();
                    break;
                case announce_event.KICKON_CAUGHT:
                    r = CommonUtils.ShufleList(Kickoff_Caught_List).First();
                    break;
            }


            r = r.Replace("[PLAYER_NAME]", player_last_name);
            r = r.Replace("[YARD_LINE]", yardline);

            return r;
        }
    }
}
