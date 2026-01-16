using log4net;
using SpectatorFootball.Enum;
using SpectatorFootball.GameNS;
using SpectatorFootball.Models;
using SpectatorFootball.PlayNS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.AxHost;
using SpectatorFootball.NarrationAndText;

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
        private Game_Player Onside_Recoverer;
        private bool bLefttoRight;
        private bool FreeKic;
        private bool bLast_Play;
        private Formation Kickoff_Formation = null;
        private Formation Return_Formation = null;
        private Play_Result r = new Play_Result();
        private Announcer _announcer = new Announcer();

        public Play_Kickoff_Onsides(Formation Kickoff_Formation, Formation Return_Formation, long Possessing_Team_Id, long at, long ht, Game_Ball gBall, List<Game_Player> Kickoff_Players, List<Game_Player> Return_Players, bool bLefttoRight, bool FreeKic, bool bLast_Play)
        {
            this.Possessing_Team_Id = Possessing_Team_Id;
            this.at = at;
            this.ht = ht;
            this.gBall = gBall;
            this.Kickoff_Players = Kickoff_Players;
            this.Return_Players = Return_Players;
            this.bLefttoRight = bLefttoRight;
            this.FreeKic = FreeKic;
            this.bLast_Play = bLast_Play;
            this.Kickoff_Formation = Kickoff_Formation;
            this.Return_Formation = Return_Formation;

            r.BallPossessing_Team_Id = Possessing_Team_Id == at ? ht : at;
            r.NonbBallPossessing_Team_Id = Possessing_Team_Id == at ? at : ht;
            r.at = at;
            r.ht = ht;
            r = setPlayerActions(Kickoff_Formation, Return_Formation,Kickoff_Players, Return_Players, r);
        }

        public Play_Result Execute(bool bPreSnapPenalty)
        {
            List<string> Play_Stages = new List<string>();
            bool bLost = false;
            List<Game_Player> Missed_Tackles = new List<Game_Player>();
            int rnd = CommonUtils.getRandomNum(4, 8) - 1;

            Kicker_Runs_Up_And_Kicks_Ball(gBall, Kickoff_Players, Return_Players);
            Game_Player Ball_Target_Recover = KickBall(gBall, Kickoff_Players, Return_Players, rnd);

            //Does the returner cover the ball or not.  If not treat it like a fumble
            long hands_rating = Ball_Target_Recover.p_and_r.pr.First().Hands_Rating;
            bool ballRecovered = Game_Engine_Helper.DoesPlayerCoverOnsideKick(hands_rating);

            if (ballRecovered == false)
                bLost = kickMuffed(r, gBall, Kickoff_Players, Return_Players, Ball_Target_Recover, rnd); 
            else
                FallOnBall(gBall, Kickoff_Players, Return_Players, Ball_Target_Recover, rnd);



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
        public Play_Result getPlayResult()
        {
            return r;
        }
        public static Play_Result setPlayerActions(Formation Kickoff_Formation, Formation Return_Formation, List<Game_Player> Kickoff_Players, List<Game_Player> Return_Players, Play_Result pResult)
        {
            Play_Result r = pResult;

            r.Kicker = Kickoff_Players[(int)Kickoff_Formation.KickerIndex];
            //Get the kicker - kicker and returner must be slot 5 in the formation

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
                    r.Add(new Game_Player_Stats() { Player_ID = Kicker.p_and_r.pr.First().Player_ID, ko_onside_kick_att = 1 , ko_onside_play = 1 });
                else
                    r.Add(new Game_Player_Stats() { Player_ID = p.p_and_r.pr.First().Player_ID, ko_onside_play = 1 });
            }

            foreach (Game_Player p in Return_Players)
            {
                r.Add(new Game_Player_Stats() { Player_ID = p.p_and_r.pr.First().Player_ID, ko_onside_def_plays = 1});
            }

            if (Onside_Recoverer != null)
            {
                Game_Player_Stats ks = r.Where(x => x.Player_ID == Onside_Recoverer.p_and_r.pr.First().Player_ID).FirstOrDefault();
                if (ks != null) ks.ko_onside_recovered = 1;
            }



             return r;
        }

        private void Kicker_Runs_Up_And_Kicks_Ball(Game_Ball gBall, List<Game_Player> Kickoff_Players, List<Game_Player> Return_Players)
        {
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

                    Player_States moving_ps = Game_Engine_Helper.setRunningState(bLefttoRight, true, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos, app_Constants.MOVEMENT_DIST_BEFORE_TURNING_BACK);
                    p.KickBall(moving_ps, prev_yl, prev_v, Runup_end_yardline, Runup_end_vert_pos, _announcer.Announce_InPlay(announce_event.ONSIDE_KICK, r.Kicker.p_and_r.p.Last_Name, Game_Engine_Helper.getYardlineDisplay(p.Current_YardLine)), null);
                }
                else
                {
                    //Other players just stand there waiting for the kick
                    p.Stand();
                }
                io_Players++;
            }

            //The team receiving the kick will just stand there before the kick
            foreach (Game_Player p in Return_Players)
            {
                //Receiving players just stand there waiting for the kick
                p.Stand();
            }

        }

        private Game_Player KickBall(Game_Ball gBall, List<Game_Player> Kickoff_Players, List<Game_Player> Return_Players, int rnd)
        {
            //possision where ball should be caught
            gBall.Current_YardLine = Return_Players[rnd].Starting_YardLine - (0.25 * Game_Engine_Helper.HorizontalAdj(bLefttoRight));
            gBall.Current_Vertical_Percent_Pos = Return_Players[rnd].Starting_Vertical_Percent_Pos;

             Game_Player Ball_Target_Recover = Return_Players[rnd];

            gBall.Bounce_Along_Ground_Slow();
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

                Player_States moving_ps = Game_Engine_Helper.setRunningState(bLefttoRight, true, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos, app_Constants.MOVEMENT_DIST_BEFORE_TURNING_BACK);
                p.Run_Then_Stand(moving_ps, prev_yl, prev_v);

                id_Players++;
            }

            id_Players = 0;
            foreach (Game_Player p in Return_Players)
            {
                //Receiving players just stand there waiting for the kick
                p.Stand();
            }

            return Ball_Target_Recover;
        }

        private bool kickMuffed(Play_Result r, Game_Ball gBall, List<Game_Player> Kickoff_Players, List<Game_Player> Return_Players, Game_Player Ball_Target_Recover, int rnd)
        {
            bool bLost = false;
            r.bOnside_Muffed = true;
            //Get all players adjacent to where the ball is
            List<Game_Player> pFumble_Rec_Kickoff_Players = new List<Game_Player>();
            List<Game_Player> pFumble_Rec_Return_Players = new List<Game_Player>();
            List<int> closest_players = getkickoffGroupClosestPlayers(rnd);
            getBothGroupSlotPlayers(Kickoff_Players, Return_Players,
             pFumble_Rec_Kickoff_Players, pFumble_Rec_Return_Players, closest_players);
            Tuple<Game_Player, bool> t = Playstub_Fumble.Execute(!bLefttoRight, gBall,
                Kickoff_Players, Return_Players,
                pFumble_Rec_Kickoff_Players, pFumble_Rec_Return_Players,
                Ball_Target_Recover, r.Tackler, false, Fumble_OSKick_BlockPunt.ONSIDE_KICK);
            r.Onside_Kick_Recoverer = t.Item1;
            bLost = t.Item2;

            return bLost;
        }

        private void FallOnBall(Game_Ball gBall, List<Game_Player> Kickoff_Players, List<Game_Player> Return_Players, Game_Player Ball_Target_Recover, int rnd)
        {
            int io_Players = 0;
            //cycle thru the offensive/kickoff team then he defense
            //if kicker then do their special thing; otherwise, the player just remains standing 
            foreach (Game_Player p in Kickoff_Players)
            {
                 p.Stand();
                io_Players++;
            }

            //for the ball
            gBall.Carried_Fake_Movement(1);


            //The team receiving the kick will just stand there before the kick
            foreach (Game_Player p in Return_Players)
            {
                    if (p == Ball_Target_Recover)
                    {
                        double prev_yl = p.Current_YardLine;
                        double prev_v = p.Current_Vertical_Percent_Pos;
                        p.Fall_On_Ball(prev_yl, prev_v, -0.2, _announcer.Announce_InPlay(announce_event.ONSIDE_COVER, r.Kicker.p_and_r.p.Last_Name, Game_Engine_Helper.getYardlineDisplay(p.Current_YardLine)), null);
                    }
                    else
                        p.Stand();
            }

        }



    }
}



