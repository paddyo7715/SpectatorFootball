using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpectatorFootball.Enum;
using SpectatorFootball.GameNS;
using SpectatorFootball.Models;
using SpectatorFootball.PenaltiesNS;

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
        private List<string> Punt_Great_Kick = null;
        private List<string> Punt_bad_Kick = null;

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

            Punt_Great_Kick = new List<string>()
                {"Great punt",
                "What a kick",
                "A very good kick",
                "What a booming kick",
                "He really got a hold of that one",
                "A booming kick"};

            Punt_bad_Kick = new List<string>()
                {"Not a good punt",
                "Not a long punt",
                "That is a short punt",
                "Bad punt"};


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
                case announce_event.PUNT_LONG_LENGTH:
                    r = CommonUtils.ShufleList(Punt_Great_Kick).First();
                    break;
                case announce_event.PUNT_SHORT_LENGTH:
                    r = CommonUtils.ShufleList(Punt_bad_Kick).First();
                    break;
            }


            r = r.Replace("[PLAYER_NAME]", player_last_name);
            r = r.Replace("[YARD_LINE]", yardline);

            return r;
        }
        public Tuple<List<string>, List<string>, List<string>> getPlayResult_Announcement(List<Game_Player> Home_Team, List<Game_Player> Away_Team, string home_team_name, string away_team_name, Play_Enum off_play, Play_Result pr, bool bpreSnapBenalty,
            Injury new_injury, string next_play_yardline)
        {
            List<string> Penlty_list = new List<string>();
            List<string> Play_list = new List<string>();
            List<string> Injury_list = new List<string>();
            string Next_Down_and_Yardage = null;

            if (pr.Penalty != null && !pr.bIgnorePenalty)
                Penlty_list = getPenalty_Announcement(Home_Team, Away_Team, home_team_name, away_team_name, pr, bpreSnapBenalty);

            Play_list = getPlayAnnouncement(Home_Team, Away_Team, home_team_name, away_team_name, pr, off_play, next_play_yardline);

            if (new_injury != null)
                Injury_list = InjuryAnnouncement(Home_Team, Away_Team, home_team_name, away_team_name, new_injury);

            return Tuple.Create(Penlty_list, Play_list, Injury_list);
        }

        private List<string> getPenalty_Announcement(List<Game_Player> Home_Team, List<Game_Player> Away_Team, string home_team, string away_team,
            Play_Result pr, bool bpreSnapBenalty)
        {
            List<string> r = new List<string>();
            string penalty_on_team = pr.bPenatly_on_Away_Team ? away_team : home_team;
            string penalty_not_on_team = !pr.bPenatly_on_Away_Team ? away_team : home_team;

            string penalty_rejected = null;

            string penalty_player = pr.Penalized_Player.p_and_r.p.Last_Name;

            r.Add("PENALTY ON THE PLAY!!!");
            r.Add(pr.Penalty.Description + " penalty on " + penalty_player + " of the " + penalty_on_team);
            r.Add(pr.Final_Added_Penalty_Yards + " yard penalty");

            if (!bpreSnapBenalty)
            { 
                penalty_rejected = pr.bPenalty_Rejected ? "rejected" : "accepted";
                r.Add(penalty_rejected + " by the " + penalty_not_on_team);
            }

            return r;
        }

        private List<string> getPlayAnnouncement(List<Game_Player> Home_Team, List<Game_Player> Away_Team, string home_team_name, string away_team_name, Play_Result pr, Play_Enum off_play, string next_play_yardline)
        {
            List<string> r = new List<string>();


            r.Add(getMajorAnnouncement(pr));

            switch (off_play)
            {
                case Play_Enum.KICKOFF_NORMAL:
                case Play_Enum.KICKOFF_DYNAMIC:
                case Play_Enum.KICKOFF_MODERN:
                case Play_Enum.KICKOFF_AFTER_SAFETY:
                    string returner_lastname = pr.Returner != null ? pr.Returner.p_and_r.p.Last_Name : null;
                    string kickoff_type = off_play == Play_Enum.KICKOFF_AFTER_SAFETY ? "free kick" : "kickoff";

                    if (pr.bTouchDown)
                        r.Add(returner_lastname + " returns the " + kickoff_type + " " + pr.Yards_Returned + " for the score");
                    else if (pr.bFumble)
                    {
                        FumbleAnnouncement(Home_Team, Away_Team, home_team_name, away_team_name, returner_lastname, pr);
                    }
                    else if (pr.bKick_KneelDown)
                        r.Add(returner_lastname + " kneels with the ball in the endzone, touchback");
                    if (pr.bKick_Out_of_Endzone)
                        r.Add("The kickoff sails throught endzone, touchback");
                    else if (pr.bTouchback)
                        r.Add("The " + kickoff_type + " results in a touchback");
                    else
                        r.Add("Kickoff returned " + pr.Yards_Returned);

                    break;
                case Play_Enum.KICKOFF_ONSIDES:
                    string ok_recoverer = pr.Onside_Kick_Recoverer.p_and_r.p.Last_Name;
                    string onside_result = pr.bOnsideMade ? "successful" : "unsuccessful";
                    r.Add("Onside kick " + onside_result + " ball recovered by " + ok_recoverer);
                    break;
                case Play_Enum.FIELD_GOAL:
                    string FGKicker_lastname = pr.Kicker.p_and_r.p.Last_Name;
                    if (pr.FGXP_Blocked)
                        r.Add("the kick was blocked.");
                    else
                    {
                        string fgGood = pr.bFGMade ? "makes" : "misses";
                        r.Add(FGKicker_lastname + " " + fgGood + " the " + pr.Field_Goal_Attempt_Length + " yard FG");
                    }
                    break;
                case Play_Enum.EXTRA_POINT:
                    string XPKicker_lastname = pr.Kicker.p_and_r.p.Last_Name;
                    if (pr.FGXP_Blocked)
                        r.Add("the kick was blocked.");
                    else
                    {
                        string fgGood = pr.bFGMade ? "makes" : "misses";
                        r.Add(XPKicker_lastname + " " + fgGood + " the extra point");
                    }
                    break;
                case Play_Enum.SCRIM_PLAY_1XP_PASS:
                case Play_Enum.SCRIM_PLAY_1XP_RUN:
                case Play_Enum.SCRIM_PLAY_2XP_PASS:
                case Play_Enum.SCRIM_PLAY_2XP_RUN:
                case Play_Enum.SCRIM_PLAY_3XP_PASS:
                case Play_Enum.SCRIM_PLAY_3XP_RUN:
                    string point = null;
                    string good = pr.bOnePntAfterTDMade || pr.bTwoPntAfterTDMade || pr.bThreePntAfterTDMade ? "made" : "not made";
                    if (off_play == Play_Enum.SCRIM_PLAY_1XP_PASS ||
                        off_play == Play_Enum.SCRIM_PLAY_1XP_RUN)
                        point = "1";
                    else if (off_play == Play_Enum.SCRIM_PLAY_2XP_PASS ||
                        off_play == Play_Enum.SCRIM_PLAY_2XP_RUN)
                        point = "2";
                    else if (off_play == Play_Enum.SCRIM_PLAY_3XP_PASS ||
                        off_play == Play_Enum.SCRIM_PLAY_3XP_RUN)
                        point = "3";
                    r.Add("The " + point + " conversion was " + good);

                    break;
                case Play_Enum.PUNT:
                    string punter_name = pr.Punter.p_and_r.p.Last_Name;
                    string returner = pr.Punt_Returner != null ?  pr.Punt_Returner.p_and_r.p.Last_Name : null;

                    string punt_team = null;
                    string return_team = null;
                    if (Home_Team.Any(x => x == pr.Punter))
                    {
                        punt_team = home_team_name;
                        return_team = away_team_name;
                    }
                    else
                    {
                        punt_team = away_team_name;
                        return_team = home_team_name;
                    }

                    if (pr.bPunt_blocked)
                    {
                        string block_recovered = pr.Blocked_Punt_Recoverer.p_and_r.p.Last_Name;
                        string punt_blocker = pr.Defender_Close_to_Kicker.p_and_r.p.Last_Name;

                        if (pr.bSafety)
                        {
                            r.Add("The punt was blocked and recovered in the endzone");
                            r.Add("by the " + punt_team + ", resulting in a touchdown");

                        }
                        else if (pr.bTouchDown)
                        {
                            r.Add("The punt was blocked and recovered in the endzone");
                            r.Add("by the " + return_team + ", resulting in a safety");
                        }
                        else
                            r.Add("The punt was blocked");

                        r.Add(block_recovered + " recovered the ball");
                        r.Add(punt_blocker + " is credited with the block");

                    }
                    else if (pr.bPunt_Out_of_Endzone)
                        r.Add("The punt sails throught endzone, touchback");
                    else if (pr.bPunt_KneelDown)
                        r.Add(returner + " kneels with the ball in the endzone, touchback");
                    else if (pr.bTouchback)
                        r.Add("The punt results in a touchback");
                    else if (pr.bCoffinCornerMade)
                        r.Add(punter_name + " pins them down inside the 20");
                    else if (pr.bFumble)
                    {
                        FumbleAnnouncement(Home_Team, Away_Team, home_team_name, away_team_name, returner, pr);
                    }
                    if (pr.bTouchDown)
                        r.Add(punter_name + " returns the punt " + pr.Yards_Returned + " for the score");
                    else
                    {
                        r.Add("A " + pr.Punt_Yards + " punt by " + punter_name);
                        r.Add("And a " + pr.Yards_Returned + " by " + returner);
                    }
                    break;
                case Play_Enum.RUN:
                    break;
                case Play_Enum.PASS:
                    break;
            }

            r.Add(next_play_yardline);

            return r;
        }

        private List<string> InjuryAnnouncement(List<Game_Player> Home_Team, List<Game_Player> Away_Team, string home_team_name, string away_team_name, Injury new_injury)
        {
            List<string> r = new List<string>();
            string injured_Player = new_injury.Player.Last_Name;
            string injured_team = null;

            if (Home_Team.Any(x => x.p_and_r.p == new_injury.Player))
                injured_team = home_team_name;
            else
                injured_team = away_team_name;

            r.Add("Injury on the Play!");
            r.Add(injured_Player + " of the " + injured_team);
            r.Add("has been injured on the play");

            if (new_injury.Num_of_Plays > 0)
                r.Add("He will be out " + new_injury.Num_of_Plays + " plays");
            else if (new_injury.Num_of_Weeks > 0)
                r.Add("He will be out " + new_injury.Num_of_Weeks + " weeks");
            else if (new_injury.Season_Ending > 0)
            {
                r.Add("This is a serious injury");
                r.Add("He will be out for the rest of the season");
            }
            else if (new_injury.Career_Ending > 0)
            {
                r.Add("This is a serious injury");
                r.Add("He will never play football again");
                r.Add("It is a career ending injust");
                r.Add("What a shame");
            }
            else
                throw new Exception("InjuryAnnouncement Unknown injury situation");

            return r;
        }

        private List<string> FumbleAnnouncement(List<Game_Player> Home_Team, List<Game_Player> Away_Team, string home_team_name, string away_team_name, string ball_carrier_name, Play_Result pr)
        {
            List<string> r = new List<string>();

            string fumble_recover_team = null;
            if (Home_Team.Any(x => x == pr.Fumble_Recoverer))
                fumble_recover_team = home_team_name;
            else
                fumble_recover_team = away_team_name;

            r.Add(ball_carrier_name + " fumbled the ball");
            r.Add("Recovered by " + pr.Fumble_Recoverer.p_and_r.p.Last_Name + " of the " + fumble_recover_team);

            string fumble_result = pr.bFumble_Lost ? "That's a turnover" : "They keep possession";
            r.Add(fumble_result);

            return r;
        }

        private string getMajorAnnouncement(Play_Result pr)
        {
            string r = null;

            if (pr.bTouchDown)
                r = "TOUCHDOW!";
            else if (pr.bFGMade || pr.bXPMade)
                r = "IT'S GOOD!";
            else if (pr.bFGMissed || pr.bXPMissed)
                r = "NO GOOD!";
            else if (pr.bOnePntAfterTDMade || pr.bTwoPntAfterTDMade || pr.bThreePntAfterTDMade)
                r = "CONVERSION GOOD!";
            else if (pr.bOnePntAfterTDMissed || pr.bTwoPntAfterTDMissed || pr.bThreePntAfterTDMissed)
                r = "CONVERSION NOT GOOD!";
            else if (pr.bFumble)
                r = "FUMBLE!";
            else if (pr.bSafety)
                r = "SAFETY!";
            else if (pr.bSack)
                r = "SACK!";
            else if (pr.Yards_Gained < 0)
                r = "TACKLED FOR A LOSS!";
            else
                r = "";

            return r;
        }
    }
}
