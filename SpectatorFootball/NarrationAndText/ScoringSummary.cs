using SpectatorFootball.Enum;
using SpectatorFootball.GameNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.NarrationAndText
{
    public class ScoringSummary
    {
        public static string CreateScoringSummaryEntry(Play_Enum play, Play_Result pResult)
        {
            string r= "";

            switch (play)
            {
                case Play_Enum.KICKOFF_NORMAL:
                    if (pResult.bAwayTD || pResult.bHomeTD)
                    {
                        string name = NarratorandText_Helper.getShortPlayName_from_Game_Player(pResult.Returner);
                        r = name + " kickoff return of " + (int)pResult.Yards_Returned + " yards for a TD";
                    }
                    break;
                case Play_Enum.FREE_KICK:
                    if (pResult.bAwayTD || pResult.bHomeTD)
                    {
                        string name = NarratorandText_Helper.getShortPlayName_from_Game_Player(pResult.Returner);
                        r = name + " free kick return of " + (int)pResult.Yards_Returned + " yards for a TD";
                    }
                    break;
                case Play_Enum.PUNT:
                    if (pResult.bAwayTD || pResult.bHomeTD)
                    {
                        string name = NarratorandText_Helper.getShortPlayName_from_Game_Player(pResult.Punt_Returner);
                        r = name + " punt return of " + (int)pResult.Yards_Returned + " yards for a TD";
                    }
                    break;
                case Play_Enum.FIELD_GOAL:
                    if (pResult.bAwayFG || pResult.bHomeFG)
                    {
                        string name = NarratorandText_Helper.getShortPlayName_from_Game_Player(pResult.Kicker);
                        r = name + " makes a " + (int)pResult.Field_Goal_Attempt_Length + " yard field goal";
                    }
                    break;
                case Play_Enum.EXTRA_POINT:
                    if (pResult.bAwayXP || pResult.bHomeXP)
                    {
                        string name = NarratorandText_Helper.getShortPlayName_from_Game_Player(pResult.Kicker);
                        r = name + " makes the extra point";
                    }
                    break;
                case Play_Enum.SCRIM_PLAY_1XP_RUN:
                case Play_Enum.SCRIM_PLAY_2XP_RUN:
                case Play_Enum.SCRIM_PLAY_3XP_RUN:
                    if (pResult.bAwayXP1 || pResult.bHomeXP1 ||
                        pResult.bAwayXP2 || pResult.bHomeXP2 ||
                        pResult.bAwayXP3 || pResult.bHomeXP3)
                    {
                        string ep_play = null;

                        if (play == Play_Enum.SCRIM_PLAY_1XP_RUN)
                            ep_play = "1 point conversion play";
                        else if (play == Play_Enum.SCRIM_PLAY_2XP_RUN)
                            ep_play = "2 point conversion play";
                        else
                            ep_play = "3 point conversion play";

                        string name = NarratorandText_Helper.getShortPlayName_from_Game_Player(pResult.Running_Back);
                        r = name + " runs in the " + ep_play;
                    }
                    break;
                case Play_Enum.SCRIM_PLAY_1XP_PASS:
                case Play_Enum.SCRIM_PLAY_2XP_PASS:
                case Play_Enum.SCRIM_PLAY_3XP_PASS:
                    if (pResult.bAwayXP1 || pResult.bHomeXP1 ||
                        pResult.bAwayXP2 || pResult.bHomeXP2 ||
                        pResult.bAwayXP3 || pResult.bHomeXP3)
                    {
                        string ep_play = null;

                        if (play == Play_Enum.SCRIM_PLAY_1XP_PASS)
                            ep_play = "1 point conversion play";
                        else if (play == Play_Enum.SCRIM_PLAY_2XP_PASS)
                            ep_play = "2 point conversion play";
                        else
                            ep_play = "3 point conversion play";

                        string passer_name = null;
                        string receiver_name = null;
                        passer_name = NarratorandText_Helper.getShortPlayName_from_Game_Player(pResult.Passer);
                        receiver_name = NarratorandText_Helper.getShortPlayName_from_Game_Player(pResult.Receiver);

                        if (pResult.Passer == pResult.Running_Back)
                            r = passer_name + " runs in the " + ep_play;
                        else
                            r = passer_name + " pass to " + receiver_name + " for the " + ep_play;
                    }
                    break;
                case Play_Enum.RUN:
                    if ((pResult.bAwayTD || pResult.bHomeTD) && pResult.Fumble_Recoverer == null)
                    {
                        string name = NarratorandText_Helper.getShortPlayName_from_Game_Player(pResult.Running_Back);
                        r = name + " " + (int) pResult.Yards_Gained + " yard run for a TD";
                    }
                    else if ((pResult.bAwayTD || pResult.bHomeTD) && pResult.Fumble_Recoverer != null)
                    {
                        string name = NarratorandText_Helper.getShortPlayName_from_Game_Player(pResult.Fumble_Recoverer);
                        r = name + " recovers a fumble for a defensive TD";
                    }
                    else if (pResult.bAwaySafetyFor || pResult.bHomeSafetyFor)
                    {
                        string name = NarratorandText_Helper.getShortPlayName_from_Game_Player(pResult.Tackler);
                        r = name + " tackle for loss gets the safety";
                    }
                    break;
                case Play_Enum.PASS:
                    if ((pResult.bAwayTD || pResult.bHomeTD) && pResult.Fumble_Recoverer == null)
                    {
                        string passer_name = null;
                        string receiver_name = null;
                        passer_name = NarratorandText_Helper.getShortPlayName_from_Game_Player(pResult.Passer);
                        receiver_name = NarratorandText_Helper.getShortPlayName_from_Game_Player(pResult.Receiver);

                        if (pResult.Passer == pResult.Running_Back)
                            r = passer_name + " " + (int)pResult.Yards_Gained + " yard run for a TD";
                        else
                            r = passer_name + " " + pResult.Yards_Gained + " yard pass to " + receiver_name + " for the TD";
                    }
                    else if ((pResult.bAwayTD || pResult.bHomeTD) && pResult.Fumble_Recoverer != null)
                    {
                        string name = NarratorandText_Helper.getShortPlayName_from_Game_Player(pResult.Fumble_Recoverer);
                        r = name + " recovers a fumble for a defensive TD";
                    }
                    else if (pResult.bAwaySafetyFor || pResult.bHomeSafetyFor)
                    {
                        string name = NarratorandText_Helper.getShortPlayName_from_Game_Player(pResult.Tackler);
                        r = name + " sack for a safety";
                    }
                    break;
            }
            if (r == null)
                throw new Exception("Error in CreateScoringSummaryEntry no game summary string set");

            return r;
        }
    }
}
