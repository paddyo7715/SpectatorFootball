using log4net;
using SpectatorFootball.Enum;
using SpectatorFootball.GameNS;
using SpectatorFootball.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.AxHost;

namespace SpectatorFootball.GameNS
{
    public class Play_Kickoff_Onsides : iPlay
    {
        public Play_Enum Play { get; set; } = Play_Enum.KICKOFF_ONSIDES;

        private static ILog logger = LogManager.GetLogger("RollingFile");

        private long Possessing_Team_Id;
        private long at;
        private long ht;
        private Game_Ball gBall;
        private List<Game_Player> Kickoff_Players;
        private List<Game_Player> Return_Players;
        private Game_Player Ball_Target_Recover;
        private Game_Player Onside_Recoverer;
        private bool bLefttoRight;
        private bool FreeKic;
        private bool bSim;
        private bool bLast_Play;
        private Formation Kickoff_Formation = null;
        private Formation Return_Formation = null;
        private Play_Result r = new Play_Result();

        public Play_Kickoff_Onsides(Formation Kickoff_Formation, Formation Return_Formation, long Possessing_Team_Id, long at, long ht, Game_Ball gBall, List<Game_Player> Kickoff_Players, List<Game_Player> Return_Players, bool bLefttoRight, bool FreeKic, bool bSim, bool bLast_Play)
        {
            this.Possessing_Team_Id = Possessing_Team_Id;
            this.at = at;
            this.ht = ht;
            this.gBall = gBall;
            this.Kickoff_Players = Kickoff_Players;
            this.Return_Players = Return_Players;
            this.bLefttoRight = bLefttoRight;
            this.FreeKic = FreeKic;
            this.bSim = bSim;
            this.bLast_Play = bLast_Play;
            this.Kickoff_Formation = Kickoff_Formation;
            this.Return_Formation = Return_Formation;

            r.BallPossessing_Team_Id = Possessing_Team_Id == at ? ht : at;
            r.NonbBallPossessing_Team_Id = Possessing_Team_Id == at ? at : ht;
            r.at = at;
            r.ht = ht;
            r = setPlayerActions(Kickoff_Players, Return_Players, r);
        }

        public Play_Result Execute(bool bPreSnapPenalty)
        {
            List<string> Play_Stages = new List<string>();
            int rnd = 0;
            bool bLost = false;
            List<Game_Player> Missed_Tackles = new List<Game_Player>();
            //================================  Stage One =======================================
            logger.Debug("Stage 1");
            logger.Debug("=====================================================");
            //================ Kicker Runs up to the ball and kicks it ==========================
            if (!bSim)
                gBall.TeeUp();

            int io_Players = 0;
            //cycle thru the offensive/kickoff team then he defense
            //if kicker then do their special thing; otherwise, the player just remains standing 
            foreach (Game_Player p in Kickoff_Players)
            {
                if (p == r.Kicker)
                {
                    double prev_yl = p.Current_YardLine;
                    double prev_v = p.Current_Vertical_Percent_Pos;
                    double Runup_end_yardline = p.Current_YardLine += 5.4 * Game_Engine_Helper.HorizontalAdj(bLefttoRight);
                    double Runup_end_vert_pos = p.Current_Vertical_Percent_Pos += 0.0;

                    p.Current_YardLine = Runup_end_yardline + (0.4 * Game_Engine_Helper.HorizontalAdj(bLefttoRight));
                    p.Current_Vertical_Percent_Pos += 0.0;

                    if (!bSim)
                    {
                        Player_States moving_ps = Game_Engine_Helper.setRunningState(bLefttoRight, true, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos, app_Constants.MOVEMENT_DIST_BEFORE_TURNING_BACK);
                        p.KickBall(moving_ps, prev_yl, prev_v, Runup_end_yardline, Runup_end_vert_pos);
                    }
                }
                else
                {
                    //Other players just stand there waiting for the kick
                    if (!bSim)
                        p.Stand();
                }
                io_Players++;
            }

            //The team receiving the kick will just stand there before the kick
            foreach (Game_Player p in Return_Players)
            {
                //Receiving players just stand there waiting for the kick
                if (!bSim)
                    p.Stand();
            }
            //===== End of Stage One - Kicker Runs up to the ball and kicks it ================
            logger.Debug("=======================================================");
            logger.Debug("");

            //================================  Stage Two =======================================
            logger.Debug("Stage 2");
            logger.Debug("=====================================================");
            //================================================

            //Pick a random return player to kick the ball to
            rnd = CommonUtils.getRandomNum(4, 8) - 1;

            //possision where ball should be caught
            gBall.Current_YardLine = Return_Players[rnd].Starting_YardLine - (0.25 * Game_Engine_Helper.HorizontalAdj(bLefttoRight));
            gBall.Current_Vertical_Percent_Pos = Return_Players[rnd].Starting_Vertical_Percent_Pos;

            Ball_Target_Recover = Return_Players[rnd];

            gBall.Bounce_Along_Ground();
            int id_Players = 0;
            double yardline_Offset = 0.0;
            double vert = 0.0;
            foreach (Game_Player p in Kickoff_Players)
            {
                if (p == r.Kicker)
                {
                    yardline_Offset = 6.0;
                    vert = 50.0;
                }
                else
                {
                    yardline_Offset = 9.0;
                    vert = Return_Players[id_Players].Starting_Vertical_Percent_Pos;
                }
                double prev_yl = p.Current_YardLine;
                double prev_v = p.Current_Vertical_Percent_Pos;
                p.Current_YardLine += yardline_Offset * Game_Engine_Helper.HorizontalAdj(bLefttoRight);
                p.Current_Vertical_Percent_Pos = vert;

                if (!bSim)
                {
                    Player_States moving_ps = Game_Engine_Helper.setRunningState(bLefttoRight, true, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos, app_Constants.MOVEMENT_DIST_BEFORE_TURNING_BACK);
                    p.Run_Then_Stand(moving_ps, prev_yl, prev_v);
                }

                id_Players++;
            }

            id_Players = 0;
            foreach (Game_Player p in Return_Players)
            {
                //Receiving players just stand there waiting for the kick
                if (!bSim)
                    p.Stand();
            }


            //===== the ball goes to a random member of the return team ================
            logger.Debug("=======================================================");
            logger.Debug("");

            //================================  Stage Three =======================================
            logger.Debug("Stage 3");

            //Does the returner cover the ball or not.  If not treat it like a fumble
            long hands_rating = Ball_Target_Recover.p_and_r.pr.First().Hands_Rating;
            bool ballRecovered = Game_Engine_Helper.DoesPlayerCoverOnsideKick(hands_rating);

            if (ballRecovered == false)
            {
                r.bOnside_Muffed = true;
                //Get all players adjacent to where the ball is
                List<Game_Player> pFumble_Rec_Kickoff_Players = new List<Game_Player>();
                List<Game_Player> pFumble_Rec_Return_Players = new List<Game_Player>();
                List<int> closest_players = getkickoffGroupClosestPlayers(rnd);
                getBothGroupSlotPlayers(Kickoff_Players, Return_Players,
                    pFumble_Rec_Kickoff_Players, pFumble_Rec_Return_Players, closest_players);
//                pFumble_Rec_Return_Players.Add(r.Returner);
                Tuple<Game_Player, bool> t = Playstub_Fumble.Execute(!bLefttoRight, gBall,
                    Kickoff_Players, Return_Players,
                    pFumble_Rec_Kickoff_Players, pFumble_Rec_Return_Players,
                    Ball_Target_Recover, r.Tackler, bSim);
                r.Onside_Kick_Recoverer = t.Item1;
                bLost = t.Item2;

            }
            else
            {
                io_Players = 0;
                //cycle thru the offensive/kickoff team then he defense
                //if kicker then do their special thing; otherwise, the player just remains standing 
                foreach (Game_Player p in Kickoff_Players)
                {
                    {
                        if (!bSim)
                            p.Stand();
                    }
                    io_Players++;
                }

                if (!bSim)
                {
                    //for the ball
                    gBall.Carried_Fake_Movement(5);
                }

                //The team receiving the kick will just stand there before the kick
                foreach (Game_Player p in Return_Players)
                {
                    if (!bSim)
                    {
                        if (p == Ball_Target_Recover)
                        {
                            double prev_yl = p.Current_YardLine;
                            double prev_v = p.Current_Vertical_Percent_Pos;
                            p.Fall_On_Ball(prev_yl, prev_v);
                        }
                        else
                            p.Stand();
                    }
                }
            }

            logger.Debug("=====================================================");
            //================  ==========================


            //===== the ball goes to a random member of the return team ================
            logger.Debug("=======================================================");
            logger.Debug("");

            r.bOnsideAtt = true;
            if (bLost == true)
                r.bOnsideMade = true;
            else
                r.bFinal_SwitchPossession = true;

            //Create Player Stats Records for the play
            r.Play_Player_Stats = SetPlayerStats(Kickoff_Players, Return_Players, r.bOnsideMade, r.Kicker, r.Onside_Kick_Recoverer);

            return r;
        }
        public bool isPreSnapPenalty_Eligible()
        {
            return false;
        }
        public bool isAccumeStats()
        {
            return true;
        }
        public static Play_Result setPlayerActions(List<Game_Player> Kickoff_Players, List<Game_Player> Return_Players, Play_Result pResult)
        {
            Play_Result r = pResult;

            r.Kicker = Kickoff_Players[app_Constants.KICKER_INDEX];
            //Get the kicker - kicker and returner must be slot 5 in the formation
//            r.Returner = Return_Players[app_Constants.RETURNER_INDEX];

            //for testing print out all the players and their relevant ratings
            logger.Debug("Kickoff Players");
            int d_index = 0;
            foreach (Game_Player p in Kickoff_Players)
            {
                if (p != r.Kicker)
                    r.Kick_Defenders.Add(p);

                string sPos = p.Pos.ToString();
                long leg_stn = p.p_and_r.pr.First().Kicker_Leg_Power_Rating;
                long leg_acc = p.p_and_r.pr.First().Kicker_Leg_Accuracy_Rating;
                long rn_att = p.p_and_r.pr.First().Run_Attack_Rating;
                logger.Debug("Ind:" + d_index +
                    " POS:" + sPos +
                    " Leg Strength:" + leg_stn +
                    " Leg Accuracy:" + leg_acc +
                    " Run Attack:" + rn_att
                    );
                d_index++;
            }
            logger.Debug("Receiving Players");
            d_index = 0;
            foreach (Game_Player p in Return_Players)
            {
                if (p != r.Returner)
                    r.Kick_Returners.Add(p);

                string sPos = p.Pos.ToString();
                long spd = p.p_and_r.pr.First().Speed_Rating;
                long agile = p.p_and_r.pr.First().Agilty_Rating;
                long rn_block = p.p_and_r.pr.First().Run_Block_Rating;
                long tkl = p.p_and_r.pr.First().Tackle_Rating;
                logger.Debug("Ind:" + d_index +
                    " POS:" + sPos +
                    " Spped:" + spd +
                    " Agility:" + agile +
                    " Run Blocking:" + rn_block +
                    " Tackling:" + tkl
                    );
                d_index++;
            }
            logger.Debug(" ");
            //=============================================

            return r;
        }

        private static List<int> getkickoffGroupClosestPlayers(int slot_index)
        {
            List<int> r = new List<int>();
            int highest_index = 10;

            r.Add(slot_index);

            if (slot_index > 0)
                r.Add(slot_index - 1);

            if (slot_index < highest_index)
                r.Add(slot_index + 1);

            return r;
        }
        public static void getBothGroupSlotPlayers(
        List<Game_Player> Kickoff_Players,
        List<Game_Player> Return_Players,
        List<Game_Player> pFumble_Rec_Kickoff_Players,
        List<Game_Player> pFumble_Rec_Return_Players,
        List<int> grpIndexes)
        {

            foreach (int i in grpIndexes)
            {
                if (i != 5) pFumble_Rec_Kickoff_Players.Add(Kickoff_Players[i]);
                pFumble_Rec_Return_Players.Add(Return_Players[i]);
            }
        }

        public static List<Game_Player_Stats> SetPlayerStats(List<Game_Player> Kickoff_Players, List<Game_Player> Return_Players, 
            bool onside_successful, Game_Player Kicker, Game_Player Onside_Recoverer)
        {
            List<Game_Player_Stats> r = new List<Game_Player_Stats>();

            //Set a play record for each player in the play
            foreach (Game_Player p in Kickoff_Players)
            {
                if (p == Kicker)
                    r.Add(new Game_Player_Stats() { Player_ID = Kicker.p_and_r.pr.First().Player_ID, ko_onside_kick_att = 1 });
                else
                    r.Add(new Game_Player_Stats() { Player_ID = p.p_and_r.pr.First().Player_ID, ko_onside_play = 1 });
            }

            if (Onside_Recoverer != null)
            {
                Game_Player_Stats ks = r.Where(x => x.Player_ID == Onside_Recoverer.p_and_r.pr.First().Player_ID).First();
                if (ks != null) ks.ko_onside_recovered = 1;
            }

            //set the returner stats
            Game_Player_Stats kr = r.Where(x => x.Player_ID == Kicker.p_and_r.pr.First().Player_ID).First();
            kr.ko_onside_kick_att = 1;
            kr.ko_onside_kick_made = onside_successful ? 1 : 0;

             return r;
        }

    }
}



