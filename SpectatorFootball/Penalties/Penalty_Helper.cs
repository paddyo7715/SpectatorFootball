using SpectatorFootball.Enum;
using SpectatorFootball.GameNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.PenaltiesNS
{
    class Penalty_Helper
    {
        public static List<Penalty> ReturnAllPenalties()
        {
            List<Penalty> r = new List<Penalty>();

            //False Start
            r.Add(new Penalty()
            {
                code = Penalty_Codes.FS,
                Penalty_Play_Type = Penalty_Play_Types.AO,
                Yards = 5,
                bDeclinable = true,
                bAuto_FirstDown = false,
                bSpot_Foul = false,
                Description = "False Start"
            });
            r.Last().Player_Action_States = new List<Player_Action_Stats>()
            { 
                Player_Action_Stats.PAS,Player_Action_Stats.PC,Player_Action_Stats.BRN,
                Player_Action_Stats.PB
            };

            //Offensive Holidng
            r.Add(new Penalty()
            {
                code = Penalty_Codes.OH,
                Penalty_Play_Type = Penalty_Play_Types.PO,
                Yards = 10,
                bDeclinable = true,
                bAuto_FirstDown = false,
                bSpot_Foul = false,
                Description = "Holding"
            });
            r.Last().Player_Action_States = new List<Player_Action_Stats>()
            {
                Player_Action_Stats.PB
            };

            //Offensive Pass Interference
            r.Add(new Penalty()
            {
                code = Penalty_Codes.OI,
                Penalty_Play_Type = Penalty_Play_Types.PO,
                Yards = 10,
                bDeclinable = true,
                bAuto_FirstDown = false,
                bSpot_Foul = false,
                Description = "Offensive Pass Interference"
            });
            r.Last().Player_Action_States = new List<Player_Action_Stats>()
            {
                Player_Action_Stats.PC
            };

            //Offensive offsides
            r.Add(new Penalty()
            {
                code = Penalty_Codes.OO,
                Penalty_Play_Type = Penalty_Play_Types.AO,
                Yards = 5,
                bDeclinable = true,
                bAuto_FirstDown = true,
                bSpot_Foul = false,
                Description = "Offsides"
            });
            r.Last().Player_Action_States = new List<Player_Action_Stats>()
            {
                Player_Action_Stats.PB,Player_Action_Stats.FGT
            };

            //Illegal Formation
            r.Add(new Penalty()
            {
                code = Penalty_Codes.IF,
                Penalty_Play_Type = Penalty_Play_Types.A,
                Yards = 5,
                bDeclinable = true,
                bAuto_FirstDown = false,
                bSpot_Foul = false,
                Description = "Illegal Formation"
            });
            r.Last().Player_Action_States = new List<Player_Action_Stats>()
            {
                Player_Action_Stats.PB
            };

            //Defense offsides
            r.Add(new Penalty()
            {
                code = Penalty_Codes.DO,
                Penalty_Play_Type = Penalty_Play_Types.A,
                Yards = 5,
                bDeclinable = true,
                bAuto_FirstDown = false,
                bSpot_Foul = false,
                Description = "Offsides"
            });
            r.Last().Player_Action_States = new List<Player_Action_Stats>()
            {
                Player_Action_Stats.PAR, Player_Action_Stats.RD, Player_Action_Stats.FGD
            };

            //Defense Holding
            r.Add(new Penalty()
            {
                code = Penalty_Codes.DH,
                Penalty_Play_Type = Penalty_Play_Types.AD,
                Yards = 5,
                bDeclinable = true,
                bAuto_FirstDown = true,
                bSpot_Foul = false,
                Description = "Holding"
            });
            r.Last().Player_Action_States = new List<Player_Action_Stats>()
            {
                Player_Action_Stats.PAR, Player_Action_Stats.RD, Player_Action_Stats.PD
            };

            //Illegal Contact
            r.Add(new Penalty()
            {
                code = Penalty_Codes.IC,
                Penalty_Play_Type = Penalty_Play_Types.PD,
                Yards = 5,
                bDeclinable = true,
                bAuto_FirstDown = true,
                bSpot_Foul = false,
                Description = "Illegal Contact"
            });
            r.Last().Player_Action_States = new List<Player_Action_Stats>()
            {
                Player_Action_Stats.PD
            };

            //Defensive Pass Interference
            r.Add(new Penalty()
            {
                code = Penalty_Codes.PI,
                Penalty_Play_Type = Penalty_Play_Types.PD,
                Yards = 0,
                bDeclinable = true,
                bAuto_FirstDown = true,
                bSpot_Foul = true,
                Description = "Pass Interference"
            });
            r.Last().Player_Action_States = new List<Player_Action_Stats>()
            {
                Player_Action_Stats.PD
            };

            //Illegal Use of Hands (defense)
            r.Add(new Penalty()
            {
                code = Penalty_Codes.IH,
                Penalty_Play_Type = Penalty_Play_Types.AD,
                Yards = 5,
                bDeclinable = true,
                bAuto_FirstDown = true,
                bSpot_Foul = false,
                Description = "Illegal Use of Hands"
            });
            r.Last().Player_Action_States = new List<Player_Action_Stats>()
            {
                Player_Action_Stats.PAR, Player_Action_Stats.RD
            };

            //Illegal Use of Hands Offense
            r.Add(new Penalty()
            {
                code = Penalty_Codes.IHO,
                Penalty_Play_Type = Penalty_Play_Types.AD,
                Yards = 10,
                bDeclinable = true,
                bAuto_FirstDown = false,
                bSpot_Foul = false,
                Description = "Illegal Use of Hands"
            });
            r.Last().Player_Action_States = new List<Player_Action_Stats>()
            {
                Player_Action_Stats.PB, Player_Action_Stats.PC, Player_Action_Stats.BRN
            };

            //Illegal Block Kickoff
            r.Add(new Penalty()
            {
                code = Penalty_Codes.KIB,
                Penalty_Play_Type = Penalty_Play_Types.KR,
                Yards = 15,
                bDeclinable = false,
                bAuto_FirstDown = false,
                bSpot_Foul = true,
                Description = "Illegal Block"
            });
            r.Last().Player_Action_States = new List<Player_Action_Stats>()
            {
                Player_Action_Stats.KRT
            };

            //Illegal Block Punt
            r.Add(new Penalty()
            {
                code = Penalty_Codes.PIB,
                Penalty_Play_Type = Penalty_Play_Types.PTR,
                Yards = 15,
                bDeclinable = false,
                bAuto_FirstDown = false,
                bSpot_Foul = true,
                Description = "Illegal Block"
            });
            r.Last().Player_Action_States = new List<Player_Action_Stats>()
            {
                Player_Action_Stats.PRT
            };

            //Unsportsmen Like Conduct 
//defense and special teams tacklers tack on 15 yards to the end of the play
//Offense is 15 back from the line of scrmmage
//special team receving team is 15 yards back from spot of foul
            r.Add(new Penalty()
            {
                code = Penalty_Codes.UC,
                Penalty_Play_Type = Penalty_Play_Types.A,
                Yards = 15,
                bDeclinable = false,
                bAuto_FirstDown = true,
                bSpot_Foul = true,               
                Description = "Unsportsmen Like Conduct"
            });
            r.Last().Player_Action_States = new List<Player_Action_Stats>()
            {
                Player_Action_Stats.PAS,
                Player_Action_Stats.PC,
                Player_Action_Stats.BRN,
                Player_Action_Stats.PB,
                Player_Action_Stats.PAR,
                Player_Action_Stats.PD,
                Player_Action_Stats.RB,
                Player_Action_Stats.RD,
                Player_Action_Stats.K,
                Player_Action_Stats.KR,
                Player_Action_Stats.KRT,
                Player_Action_Stats.KDT,
                Player_Action_Stats.P,
                Player_Action_Stats.PR,
                Player_Action_Stats.PRT,
                Player_Action_Stats.PDT,
                Player_Action_Stats.FGT,
                Player_Action_Stats.FGD 
            };

            //Unnecessary Roughness
            //defense and special teams tacklers tack on 15 yards to the end of the play
            //Offense is 15 back from the line of scrmmage
            //special team receving team is 15 yards back from spot of foul
            r.Add(new Penalty()
            {
                code = Penalty_Codes.UR,
                Penalty_Play_Type = Penalty_Play_Types.A,
                Yards = 15,
                bDeclinable = false,
                bAuto_FirstDown = true,
                bSpot_Foul = true,
                Description = "Unnecessary Roughness"
            });
            r.Last().Player_Action_States = new List<Player_Action_Stats>()
            {
                Player_Action_Stats.PAS,
                Player_Action_Stats.PC,
                Player_Action_Stats.BRN,
                Player_Action_Stats.PB,
                Player_Action_Stats.PAR,
                Player_Action_Stats.PD,
                Player_Action_Stats.RB,
                Player_Action_Stats.RD,
                Player_Action_Stats.K,
                Player_Action_Stats.KR,
                Player_Action_Stats.KRT,
                Player_Action_Stats.KDT,
                Player_Action_Stats.P,
                Player_Action_Stats.PR,
                Player_Action_Stats.PRT,
                Player_Action_Stats.PDT,
                Player_Action_Stats.FGT,
                Player_Action_Stats.FGD
            };

            //Facemask
            //defense and special teams tacklers tack on 15 yards to the end of the play
            //Offense is 15 back from the line of scrmmage
            //special team receving team is 15 yards back from spot of foul
            r.Add(new Penalty()
            {
                code = Penalty_Codes.FM,
                Penalty_Play_Type = Penalty_Play_Types.A,
                Yards = 15,
                bDeclinable = false,
                bAuto_FirstDown = true,
                bSpot_Foul = true, 
                Description = "Facemask"
            });
            r.Last().Player_Action_States = new List<Player_Action_Stats>()
            {
                Player_Action_Stats.PAS,
                Player_Action_Stats.PC,
                Player_Action_Stats.BRN,
                Player_Action_Stats.PB,
                Player_Action_Stats.PAR,
                Player_Action_Stats.PD,
                Player_Action_Stats.RB,
                Player_Action_Stats.RD,
                Player_Action_Stats.K,
                Player_Action_Stats.KR,
                Player_Action_Stats.KRT,
                Player_Action_Stats.KDT,
                Player_Action_Stats.P,
                Player_Action_Stats.PR,
                Player_Action_Stats.PRT,
                Player_Action_Stats.PDT,
                Player_Action_Stats.FGT,
                Player_Action_Stats.FGD
            };

            return r;
        }
        public static Player_Action_Stats getPlayerAction(Game_Player Penalty_Player, Play_Result pResult)

        {
            Player_Action_Stats r = Player_Action_Stats.PAS;

            if (Penalty_Player == pResult.Passer)
                r = Player_Action_Stats.PAS;
            else if ((Penalty_Player == pResult.Kicker))
                r = Player_Action_Stats.K;
            else if ((Penalty_Player == pResult.Punter))
                r = Player_Action_Stats.P;
            else if ((Penalty_Player == pResult.Returner))
                r = Player_Action_Stats.KR;
            else if ((Penalty_Player == pResult.Punt_Returner))
                r = Player_Action_Stats.PR;
            else if (pResult.Pass_Catchers.Contains(Penalty_Player))
                r = Player_Action_Stats.PC;
            else if (pResult.Ball_Runners.Contains(Penalty_Player))
                r = Player_Action_Stats.BRN;
            else if (pResult.Pass_Blockers.Contains(Penalty_Player))
                r = Player_Action_Stats.PB;
            else if (pResult.Pass_Rushers.Contains(Penalty_Player))
                r = Player_Action_Stats.PAR;
            else if (pResult.Pass_Defenders.Contains(Penalty_Player))
                r = Player_Action_Stats.PD;
            else if (pResult.Run_Blockers.Contains(Penalty_Player))
                r = Player_Action_Stats.RB;
            else if (pResult.Run_Defenders.Contains(Penalty_Player))
                r = Player_Action_Stats.RD;
            else if (pResult.Kick_Returners.Contains(Penalty_Player))
                r = Player_Action_Stats.KRT;
            else if (pResult.Kick_Defenders.Contains(Penalty_Player))
                r = Player_Action_Stats.KDT;
            else if (pResult.Punt_Returners.Contains(Penalty_Player))
                r = Player_Action_Stats.PRT;
            else if (pResult.Punt_Defenders.Contains(Penalty_Player))
                r = Player_Action_Stats.PDT;
            else if (pResult.FieldGaol_Kicking_Team.Contains(Penalty_Player))
                r = Player_Action_Stats.FGT;
            else if (pResult.Field_Goal_Defenders.Contains(Penalty_Player))
                r = Player_Action_Stats.FGD;
            return r;
        }
        public static Penalty getPenalty(List<Penalty> pList, Play_Enum pe, Player_Action_Stats pa)
        {
            Penalty r = null;

            List<Penalty> Penalties_for_Play = null;
            List<Penalty> Penalties_for_Play_Player = null;

            //first create a list of penalties based on the play enum
            switch (pe)
            {
                case Play_Enum.KICKOFF_NORMAL:
                case Play_Enum.FREE_KICK:
                    Penalties_for_Play = pList.Where(x => x.Penalty_Play_Type == Penalty_Play_Types.A ||
                    x.Penalty_Play_Type == Penalty_Play_Types.KR ||
                    x.Penalty_Play_Type == Penalty_Play_Types.KD).ToList();
                    break;
                case Play_Enum.KICKOFF_ONSIDES:
                    Penalties_for_Play = pList.Where(x => x.Penalty_Play_Type == Penalty_Play_Types.A).ToList();
                    break;
                case Play_Enum.FIELD_GOAL:
                case Play_Enum.EXTRA_POINT:
                    Penalties_for_Play = pList.Where(x => x.Penalty_Play_Type == Penalty_Play_Types.A ||
                    x.Penalty_Play_Type == Penalty_Play_Types.FGO ||
                    x.Penalty_Play_Type == Penalty_Play_Types.FGD).ToList();
                    break;
                case Play_Enum.PUNT:
                    Penalties_for_Play = pList.Where(x => x.Penalty_Play_Type == Penalty_Play_Types.A ||
                    x.Penalty_Play_Type == Penalty_Play_Types.PTR ||
                    x.Penalty_Play_Type == Penalty_Play_Types.PTD).ToList();
                    break;
                case Play_Enum.RUN:
                    Penalties_for_Play = pList.Where(x => x.Penalty_Play_Type == Penalty_Play_Types.A ||
                    x.Penalty_Play_Type == Penalty_Play_Types.AO ||
                    x.Penalty_Play_Type == Penalty_Play_Types.RO ||
                    x.Penalty_Play_Type == Penalty_Play_Types.AD ||
                    x.Penalty_Play_Type == Penalty_Play_Types.PR).ToList();
                    break;
                case Play_Enum.PASS:
                    Penalties_for_Play = pList.Where(x => x.Penalty_Play_Type == Penalty_Play_Types.A ||
                    x.Penalty_Play_Type == Penalty_Play_Types.AO ||
                    x.Penalty_Play_Type == Penalty_Play_Types.PO ||
                    x.Penalty_Play_Type == Penalty_Play_Types.AD ||
                    x.Penalty_Play_Type == Penalty_Play_Types.PD).ToList();
                    break;
            }

            if (Penalties_for_Play.Count == 0)
                throw new Exception("Error in getPenalty after play.  No possible penalties found for play " + pe.ToString());

            //Now widdle down the penalties for the specific player
            Penalties_for_Play_Player = Penalties_for_Play.Where(x => x.Player_Action_States.Contains(pa)).ToList();

            if (Penalties_for_Play_Player.Count == 0)
                throw new Exception("Error in getPenalty after play.  No possible penalties found for play " + pe.ToString() + " and player action type " + pa.ToString());

            return r;
        }
        public static Game_Player getPenaltyPlayer(Play_Enum pe, List<Game_Player> Offensive_Players, List<Game_Player> Defensive_Players,
    Game_Player Passer, Game_Player Kicker, Game_Player Punter)
        {
            Game_Player r = null;
            List<Game_Player> Possible_Players = new List<Game_Player>();

            foreach (Game_Player p in Offensive_Players)
            {
                long sp_num;

                if (p == Passer || p == Kicker || p == Punter)
                    sp_num = app_Constants.SPORTSMANSHIP_ADJUSTER - p.p_and_r.pr.First().Sportsmanship_Ratings / app_Constants.PENALTY_UPPER_LIMIT_ADJ_QB_K;
                else
                    sp_num = app_Constants.SPORTSMANSHIP_ADJUSTER - p.p_and_r.pr.First().Sportsmanship_Ratings;

                long rmd = CommonUtils.getRandomNum(1, (int)app_Constants.PENALTY_UPPER_LIMIT);
                if (rmd <= sp_num)
                    Possible_Players.Add(p);
            }

            if (Possible_Players.Count > 0)
            {
                int ind = CommonUtils.getRandomIndex(Possible_Players.Count());
                r = Possible_Players[ind];
            }

            return r;
        }
        public static bool isNoPenaltyPlay(Play_Result pResult, Play_Enum pe)
        {
            bool r = false;

            if ((pe == Play_Enum.KICKOFF_NORMAL || pe == Play_Enum.FREE_KICK || pe == Play_Enum.PUNT) &&
                pResult.bTouchback)
                r = true;
            else if (pe == Play_Enum.PUNT && (pResult.bKick_Out_of_Bounds || pResult.bKick_Out_of_Endzone))
                r = true;

            return r;
        }
        public static bool isHalfTheDistance(double penalty_yards, double dist_gl)
        {
            bool r = false;

            if (dist_gl / 2 < penalty_yards)
                r = true;

            return r;
        }
    }
}
