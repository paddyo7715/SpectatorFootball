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
        private List<string> return_breakthru_List = null;
        private List<string> Breakthru_Tackle_List = null;
        private List<string> Running_Cant_be_Caught_List = null;
        private List<string> Returner_one_man_to_beat = null;
        private List<string> Fubmle_List = null;
        private List<string> Onside_Kick_List = null;
        private List<string> Onside_Muffed_List = null;
        private List<string> Onside_Covered_List = null;
        private List<string> FG_Away_List = null;
        private List<string> FG_Blocked_List = null;
        private List<string> Punt_Away_List = null;
        private List<string> Punt_Blocked_List = null;
        private List<string> FG_Hits_The_Post_List = null;

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

            return_breakthru_List = new List<string>()
                {"There he goes",
                "He makes it into the open",
                "he breaks into the open",
                "He has a lane"};

            Breakthru_Tackle_List = new List<string>()
                {"They can't get him",
                "He makes it through",
                "He missed the tackle"};

            Running_Cant_be_Caught_List = new List<string>()
                {"He's gone",
                "No body will be able to catch him",
                "He's going in for the TD",
                "They won't be able to catch him"};

            Returner_one_man_to_beat = new List<string>()
                {"One man to beat!",
                "Only the kicker can stop him now!",
                "One man left to stop him!"};

            Fubmle_List = new List<string>()
                {"Fumble!",
                "He loses the ball!",
                "The ball pops out!"};

            Onside_Kick_List = new List<string>()
                {"Here we go!",
                "Here's the kick",
                "Here's the onside kick",
                "This is it"};

            Onside_Muffed_List = new List<string>()
                {"He can't hold onto it!",
                "The ball is free!",
                "That's a live ball!",
                "He muffed it!"};

            Onside_Covered_List = new List<string>()
                {"He falls on it at the [YARD_LINE]",
                "[PLAYER_NAME] covers the ball at the [YARD_LINE]",
                "Fielded at the [YARD_LINE]",
                "Taken at the [YARD_LINE]"};

            FG_Away_List = new List<string>()
                {"The kick is away",
                "There goes the ball"};

            FG_Blocked_List = new List<string>()
                {"Oh the kick is blocked!",
                "They blocked the kick!",
                "The kick is blocked!",
                "Blocked!"};

            Punt_Away_List = new List<string>()
                {"The punt is away",
                "There goes the ball"};

            Punt_Blocked_List = new List<string>()
                {"Oh the punt is blocked!",
                "They blocked the punt!",
                "The punt is blocked!",
                "Blocked!"};

            FG_Hits_The_Post_List = new List<string>()
                {"It's off the post",
                "It hit the post"};






        }

        public string Announce_InPlay(announce_event ann_event, string player_last_name, string yardline)
        {
            string r = null;

            switch (ann_event)
            {
                case announce_event.KICKOFF_KICKED:
                    r = CommonUtils.ShufleList(Kickoff_Kick_List).First();
                    break;
                case announce_event.KICKOFF_CAUGHT:
                    r = CommonUtils.ShufleList(Kickoff_Caught_List).First();
                    break;
                case announce_event.RETURN_BREAKTHRU:
                    r = CommonUtils.ShufleList(return_breakthru_List).First();
                    break;           
                case announce_event.BREAKTHRU_TACKLE:
                    r = CommonUtils.ShufleList(Breakthru_Tackle_List).First();
                    break;
                case announce_event.RETURN_GOING_FOR_TD:
                    r = CommonUtils.ShufleList(Running_Cant_be_Caught_List).First();
                    break;
                case announce_event.RETURN_KICKER_LEFT_TO_BEAT:
                    r = CommonUtils.ShufleList(Returner_one_man_to_beat).First();
                    break;
                case announce_event.FUMBLE:
                    r = CommonUtils.ShufleList(Fubmle_List).First();
                    break;
                case announce_event.ONSIDE_KICK:
                    r = CommonUtils.ShufleList(Onside_Kick_List).First();
                    break;
                case announce_event.ONSIDE_NUFFED:
                    r = CommonUtils.ShufleList(Onside_Muffed_List).First();
                    break;
                case announce_event.ONSIDE_COVER:
                    r = CommonUtils.ShufleList(Onside_Covered_List).First();
                    break;
                case announce_event.FG_AWAY:
                    r = CommonUtils.ShufleList(FG_Away_List).First();
                    break;
                case announce_event.FG_BLOCKED:
                    r = CommonUtils.ShufleList(FG_Blocked_List).First();
                    break;
                case announce_event.PUNT_AWAY:
                    r = CommonUtils.ShufleList(Punt_Away_List).First();
                    break;
                case announce_event.PUNT_BLOCKED:
                    r = CommonUtils.ShufleList(Punt_Blocked_List).First();
                    break;
                case announce_event.FG_HITS_GP:
                    r = CommonUtils.ShufleList(FG_Hits_The_Post_List).First();
                    break;
            }


            r = r.Replace("[PLAYER_NAME]", player_last_name);
            r = r.Replace("[YARD_LINE]", yardline);

            return r;
        }
    }
}
