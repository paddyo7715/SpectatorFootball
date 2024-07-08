using log4net;
using SpectatorFootball.Enum;
using SpectatorFootball.GameNS;
using SpectatorFootball.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.GameNS
{
    public class Play_Punt : iPlay
    {
        private static ILog logger = LogManager.GetLogger("RollingFile");

        private long Possessing_Team_Id;
        private long at;
        private long ht;
        private Game_Ball gBall;
        private List<Game_Player> Punt_Players;
        private List<Game_Player> Return_Players;
        private bool bLefttoRight;
        private bool FreeKic;
        private bool bSim;
        private bool bLast_Play;
        private Formation Punt_Formation = null;
        private Formation Return_Formation = null;

        public Play_Result r = new Play_Result();

        Play_Enum Play { get; set; } = Play_Enum.PUNT;

        Play_Enum iPlay.Play { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }        private Formation Kickoff_Formation = null;
        public Play_Punt(Formation Punt_Formation, Formation Return_Formation, long Possessing_Team_Id, long at, long ht, Game_Ball gBall, List<Game_Player> Punt_Players, List<Game_Player> Return_Players, bool bLefttoRight, bool bSim, bool bLast_Play)
        {
            this.Possessing_Team_Id = Possessing_Team_Id;
            this.at = at;
            this.ht = ht;
            this.gBall = gBall;
            this.Punt_Players = Punt_Players;
            this.Return_Players = Return_Players;
            this.bLefttoRight = bLefttoRight;
            this.bSim = bSim;
            this.bLast_Play = bLast_Play;
            this.Punt_Formation = Punt_Formation;
            this.Return_Formation = Return_Formation;

            r.BallPossessing_Team_Id = Possessing_Team_Id == at ? ht : at;
            r.NonbBallPossessing_Team_Id = Possessing_Team_Id == at ? at : ht;
            r.at = at;
            r.ht = ht;
            r = setPlayerActions(Punt_Formation, Return_Formation, Punt_Players, Return_Players, r);
        }

        public bool isPreSnapPenalty_Eligible()
        {
            return true;
        }

        public bool isAccumeStats()
        {
            return true;
        }
        public Play_Result getPlayResult()
        {
            return r;
        }

        public Play_Result Execute(bool bPreSnapPenalty)
        {
            List<string> Play_Stages = new List<string>();
            List<Game_Player> Missed_Tackles = new List<Game_Player>();
            double retuner_catches_ball_yl = 0.0;
            //================================  Stage One =======================================
            //Players get ready for play 
            logger.Debug("Stage 1");
            logger.Debug("=====================================================");
            logger.Debug(this.bLefttoRight);

            if (!bSim)
                gBall.TeeUp();

            int io_Players = 0;
            //cycle thru the offensive/kickoff team then he defense
            //if kicker then do their special thing; otherwise, the player just remains standing 
            foreach (Game_Player p in Punt_Players)
            {
                double prev_yl = p.Current_YardLine;
                double prev_v = p.Current_Vertical_Percent_Pos;

                if (!bSim)
                {
                    if (p == r.Punter)
                        p.Punter_Ready_for_Ball(prev_yl, prev_v);
                    else if (Punt_Formation.Line_Players.Contains(io_Players))
                        p.Crouch(prev_yl, prev_v);
                    else
                        p.Stand();
                }


                io_Players++;
            }

            io_Players = 0;
            //The team receiving the kick will just stand there before the kick
            foreach (Game_Player p in Return_Players)
            {
                double prev_yl = p.Current_YardLine;
                double prev_v = p.Current_Vertical_Percent_Pos;

                if (!bSim)
                {
                    if (Return_Formation.Line_Players.Contains(io_Players))
                        p.Crouch(prev_yl, prev_v);
                    else
                        p.Stand();
                }

                io_Players++;
            }
            //===== End of Stage One - Kicker Runs up to the ball and kicks it ================
            logger.Debug("=======================================================");
            logger.Debug("");

            //================================  Stage Two =======================================
            //if presnap penatly then plaers stand up; otherwise snap ball back to punter
            logger.Debug("Stage 2");
            logger.Debug("=====================================================");
            if (bPreSnapPenalty)
            {
                Playstub_Uncrouch.Execute(bLefttoRight, gBall, Punt_Players, Return_Players, bSim);
            }
            else
            {


            }

            return r;
        }





        public static List<Game_Player_Stats> SetPlayerStats(List<Game_Player> Punt_Players, List<Game_Player> Return_Players, bool bTouchback, bool coffin_corner_att, bool coffin_corner_made, bool bTouchdown,
    bool bFumble, bool bFumble_Lost, double Yards,
    Game_Player Punter, Game_Player Punt_Returner, Game_Player Tackler,
    Game_Player Forced_Fumble_Recoverer, List<Game_Player> Missed_Tackle)
        {
            List<Game_Player_Stats> r = new List<Game_Player_Stats>();

            return r;
        }

        public static Play_Result setPlayerActions(Formation Kickoff_Formation, Formation Return_Formation, 
            List<Game_Player> Kickoff_Players, List<Game_Player> Return_Players, Play_Result pResult)
        {
            Play_Result r = pResult;

            r.Punter = Kickoff_Players[(int)Kickoff_Formation.KickerIndex];
            //Get the kicker - kicker and returner must be slot 5 in the formation
            r.Punt_Returner = Return_Players[(int)Return_Formation.ReturnerIndex];

            //for testing print out all the players and their relevant ratings
            logger.Debug("Kickoff Players");
            int d_index = 0;
            foreach (Game_Player p in Kickoff_Players)
            {
                if (p != r.Punter)
                    r.Punt_Defenders.Add(p);

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
                if (p != r.Punt_Returner)
                    r.Punt_Returners.Add(p);

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
    }
}
