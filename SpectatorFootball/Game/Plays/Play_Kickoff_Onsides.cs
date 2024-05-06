using log4net;
using SpectatorFootball.Enum;
using SpectatorFootball.GameNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.AxHost;

namespace SpectatorFootball.GameNS
{
    internal class Play_Kickoff_Onsides : iPlay
    {
        public Play_Enum Play { get; set; } = Play_Enum.KICKOFF_ONSIDES;

        private static ILog logger = LogManager.GetLogger("RollingFile");

        private long Possessing_Team_Id;
        private long at;
        private long ht;
        private Game_Ball gBall;
        private List<Game_Player> Kickoff_Players;
        private List<Game_Player> Return_Players;
        private bool bLefttoRight;
        private bool FreeKic;
        private bool bSim;
        private bool bLast_Play;
        private Play_Result r = new Play_Result();

        public Play_Kickoff_Onsides(long Possessing_Team_Id, long at, long ht, Game_Ball gBall, List<Game_Player> Kickoff_Players, List<Game_Player> Return_Players, bool bLefttoRight, bool FreeKic, bool bSim, bool bLast_Play)
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

            r.BallPossessing_Team_Id = Possessing_Team_Id == at ? ht : at;
            r.NonbBallPossessing_Team_Id = Possessing_Team_Id == at ? at : ht;
            r.at = at;
            r.ht = ht;
            r = setPlayerActions(Kickoff_Players, Return_Players, r);
        }

        public Play_Result Execute(bool bPreSnapPenalty)
        {
            List<string> Play_Stages = new List<string>();
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
                        Player_States moving_ps = Game_Engine_Helper.setRunningState(bLefttoRight, true, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos);
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
            int rnd = CommonUtils.getRandomNum(4, 8) - 1;

            //possision where ball should be caught
            gBall.Current_YardLine = Return_Players[rnd].Starting_YardLine - (0.75 *  Game_Engine_Helper.HorizontalAdj(bLefttoRight)); 
            gBall.Current_Vertical_Percent_Pos = Return_Players[rnd].Starting_Vertical_Percent_Pos;

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
                        Player_States moving_ps = Game_Engine_Helper.setRunningState(bLefttoRight, true, prev_yl, prev_v, p.Current_YardLine, p.Current_Vertical_Percent_Pos);
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
            logger.Debug("=====================================================");
            //================  ==========================


            //===== the ball goes to a random member of the return team ================
            logger.Debug("=======================================================");
            logger.Debug("");

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
            r.Returner = Return_Players[app_Constants.RETURNER_INDEX];

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


    }
}



