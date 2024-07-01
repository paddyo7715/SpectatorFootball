using log4net;
using SpectatorFootball.Enum;
using SpectatorFootball.GameNS;
using SpectatorFootball.Models;
using System;
using System.Collections.Generic;
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
        private List<Game_Player> Kickoff_Players;
        private List<Game_Player> Return_Players;
        private bool bLefttoRight;
        private bool FreeKic;
        private bool bSim;
        private bool bLast_Play;
        private Play_Result r = new Play_Result();

        Play_Enum Play { get; set; } = Play_Enum.PUNT;

        Play_Enum iPlay.Play { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Play_Result Execute(bool bPreSnapPenalty)
        {
            throw new NotImplementedException();
        }

        public bool isAccumeStats()
        {
            throw new NotImplementedException();
        }

        public bool isPreSnapPenalty_Eligible()
        {
            throw new NotImplementedException();
        }

        public static List<Game_Player_Stats> SetPlayerStats(List<Game_Player> Punt_Players, List<Game_Player> Return_Players, bool bTouchback, bool coffin_corner_att, bool coffin_corner_made, bool bTouchdown,
    bool bFumble, bool bFumble_Lost, double Yards,
    Game_Player Punter, Game_Player Punt_Returner, Game_Player Tackler,
    Game_Player Forced_Fumble_Recoverer, List<Game_Player> Missed_Tackle)
        {
            List<Game_Player_Stats> r = new List<Game_Player_Stats>();

            return r;
        }

        public static Play_Result setPlayerActions(List<Game_Player> Kickoff_Players, List<Game_Player> Return_Players, Play_Result pResult)
        {
            Play_Result r = pResult;

            r.Punter = Kickoff_Players[app_Constants.KICKER_INDEX];
            //Get the kicker - kicker and returner must be slot 5 in the formation
            r.Punt_Returner = Return_Players[app_Constants.RETURNER_INDEX];

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
