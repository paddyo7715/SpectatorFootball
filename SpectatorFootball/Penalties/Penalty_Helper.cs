using SpectatorFootball.Enum;
using SpectatorFootball.GameNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.PenaltiesNS
{
    public class Penalty_Helper
    {
        public static List<Penalty> ReturnAllPenalties()
        {
            List<Penalty> r = new List<Penalty>();

//Pre-Snap Penalties
//==================================================
            //Delay of Game
            r.Add(new Penalty()
            {
                code = Penalty_Codes.DG,
                Yards = 5,
                bDeclinable = false,
                bAuto_FirstDown = false,
                bSpot_Foul = false,
                Play_Timing = Play_Snap_Timing.BEFORE_SNAP,
                Frequency_Rating = 200,
                Description = "Delay of Game"
            }); 
            r.Last().Penalty_Play_Types = new List<Play_Enum>()
            {
                Play_Enum.RUN,
                Play_Enum.PASS,
                Play_Enum.SCRIM_PLAY_1XP_PASS,
                Play_Enum.SCRIM_PLAY_1XP_RUN,
                Play_Enum.SCRIM_PLAY_2XP_PASS,
                Play_Enum.SCRIM_PLAY_2XP_RUN,
                Play_Enum.SCRIM_PLAY_3XP_PASS,
                Play_Enum.SCRIM_PLAY_3XP_RUN,
                Play_Enum.PUNT,
                Play_Enum.FIELD_GOAL,
                Play_Enum.EXTRA_POINT
            };
            r.Last().Player_Action_States = new List<Player_Action_State>()
            {
                Player_Action_State.PAS
            };
            //False Start
            r.Add(new Penalty()
            {
                code = Penalty_Codes.FS,
                Yards = 5,
                bDeclinable = false,
                bAuto_FirstDown = false,
                bSpot_Foul = false,
                Play_Timing = Play_Snap_Timing.BEFORE_SNAP,
                Frequency_Rating = 800,
                Description = "False Start"
            });  
            r.Last().Penalty_Play_Types = new List<Play_Enum>()
            {
                Play_Enum.RUN,
                Play_Enum.PASS,
                Play_Enum.SCRIM_PLAY_1XP_PASS,
                Play_Enum.SCRIM_PLAY_1XP_RUN,
                Play_Enum.SCRIM_PLAY_2XP_PASS,
                Play_Enum.SCRIM_PLAY_2XP_RUN,
                Play_Enum.SCRIM_PLAY_3XP_PASS,
                Play_Enum.SCRIM_PLAY_3XP_RUN,
                Play_Enum.PUNT,
                Play_Enum.FIELD_GOAL,
                Play_Enum.EXTRA_POINT
            };
            r.Last().Player_Action_States = new List<Player_Action_State>()
            { 
                Player_Action_State.PAS,Player_Action_State.PC,Player_Action_State.BRN,
                Player_Action_State.PB, Player_Action_State.RB, 
                Player_Action_State.PDT, Player_Action_State.FGT
            };
            //Illegal Formation
            r.Add(new Penalty()
            {
                code = Penalty_Codes.IF,
                Yards = 5,
                bDeclinable = false,
                bAuto_FirstDown = false,
                bSpot_Foul = false,
                Play_Timing = Play_Snap_Timing.BEFORE_SNAP,
                Frequency_Rating = 100,
                Description = "Illegal Formation"
            }); ;
            r.Last().Penalty_Play_Types = new List<Play_Enum>()
            {
                Play_Enum.RUN,
                Play_Enum.PASS,
                Play_Enum.SCRIM_PLAY_1XP_PASS,
                Play_Enum.SCRIM_PLAY_1XP_RUN,
                Play_Enum.SCRIM_PLAY_2XP_PASS,
                Play_Enum.SCRIM_PLAY_2XP_RUN,
                Play_Enum.SCRIM_PLAY_3XP_PASS,
                Play_Enum.SCRIM_PLAY_3XP_RUN,
                Play_Enum.PUNT,
                Play_Enum.FIELD_GOAL,
                Play_Enum.EXTRA_POINT
            };
            r.Last().Player_Action_States = new List<Player_Action_State>()
            {
                Player_Action_State.PB,Player_Action_State.RB, Player_Action_State.PC
            };
            //Illegal Motion
            r.Add(new Penalty()
            {
                code = Penalty_Codes.IF,
                Yards = 5,
                bDeclinable = false,
                bAuto_FirstDown = false,
                bSpot_Foul = false,
                Play_Timing = Play_Snap_Timing.BEFORE_SNAP,
                Frequency_Rating = 20,
                Description = "Illegal Motion"
            }); ;
            r.Last().Penalty_Play_Types = new List<Play_Enum>()
            {
                Play_Enum.RUN,
                Play_Enum.PASS,
                Play_Enum.SCRIM_PLAY_1XP_PASS,
                Play_Enum.SCRIM_PLAY_1XP_RUN,
                Play_Enum.SCRIM_PLAY_2XP_PASS,
                Play_Enum.SCRIM_PLAY_2XP_RUN,
                Play_Enum.SCRIM_PLAY_3XP_PASS,
                Play_Enum.SCRIM_PLAY_3XP_RUN
            };
            r.Last().Player_Action_States = new List<Player_Action_State>()
            {
                Player_Action_State.PC
            };
            //Neutral zone infraction 
            r.Add(new Penalty()
            {
                code = Penalty_Codes.NZ,
                Yards = 5,
                bDeclinable = false,
                bAuto_FirstDown = false,
                bSpot_Foul = false,
                Play_Timing = Play_Snap_Timing.BEFORE_SNAP,
                Frequency_Rating = 300,
                Description = "Neutral zone infraction"
            });
            r.Last().Penalty_Play_Types = new List<Play_Enum>()
            {
                Play_Enum.RUN,
                Play_Enum.PASS,
                Play_Enum.SCRIM_PLAY_1XP_PASS,
                Play_Enum.SCRIM_PLAY_1XP_RUN,
                Play_Enum.SCRIM_PLAY_2XP_PASS,
                Play_Enum.SCRIM_PLAY_2XP_RUN,
                Play_Enum.SCRIM_PLAY_3XP_PASS,
                Play_Enum.SCRIM_PLAY_3XP_RUN,
                Play_Enum.PUNT,
                Play_Enum.FIELD_GOAL,
                Play_Enum.EXTRA_POINT
            };
            r.Last().Player_Action_States = new List<Player_Action_State>()
            {
                Player_Action_State.PAR,Player_Action_State.PDT, Player_Action_State.FGD,
                Player_Action_State.RD
            };
            //Encroachment 
            r.Add(new Penalty()
            {
                code = Penalty_Codes.EN,
                Yards = 5,
                bDeclinable = false,
                bAuto_FirstDown = false,
                bSpot_Foul = false,
                Play_Timing = Play_Snap_Timing.BEFORE_SNAP,
                Frequency_Rating = 150,
                Description = "Encroachment"
            });
            r.Last().Penalty_Play_Types = new List<Play_Enum>()
            {
                Play_Enum.RUN,
                Play_Enum.PASS,
                Play_Enum.SCRIM_PLAY_1XP_PASS,
                Play_Enum.SCRIM_PLAY_1XP_RUN,
                Play_Enum.SCRIM_PLAY_2XP_PASS,
                Play_Enum.SCRIM_PLAY_2XP_RUN,
                Play_Enum.SCRIM_PLAY_3XP_PASS,
                Play_Enum.SCRIM_PLAY_3XP_RUN,
                Play_Enum.PUNT,
                Play_Enum.FIELD_GOAL,
                Play_Enum.EXTRA_POINT
            };
            r.Last().Player_Action_States = new List<Player_Action_State>()
            {
                Player_Action_State.PAR,Player_Action_State.PDT, Player_Action_State.FGD,
                Player_Action_State.RD
            };
            //Offsides (Defense)
            r.Add(new Penalty()
            {
                code = Penalty_Codes.DO,
                Yards = 5,
                bDeclinable = true,
                bAuto_FirstDown = false,
                bSpot_Foul = false,
                Play_Timing = Play_Snap_Timing.BEFORE_SNAP,
                Frequency_Rating = 700,
                Description = "Offsides"
            });
            r.Last().Penalty_Play_Types = new List<Play_Enum>()
            {
                Play_Enum.RUN,
                Play_Enum.PASS,
                Play_Enum.SCRIM_PLAY_1XP_PASS,
                Play_Enum.SCRIM_PLAY_1XP_RUN,
                Play_Enum.SCRIM_PLAY_2XP_PASS,
                Play_Enum.SCRIM_PLAY_2XP_RUN,
                Play_Enum.SCRIM_PLAY_3XP_PASS,
                Play_Enum.SCRIM_PLAY_3XP_RUN,
                Play_Enum.PUNT,
                Play_Enum.FIELD_GOAL,
                Play_Enum.EXTRA_POINT
            };
            r.Last().Player_Action_States = new List<Player_Action_State>()
            {
                Player_Action_State.PAR, Player_Action_State.RD,  Player_Action_State.FGD, Player_Action_State.PRT
            };

            //During Play Penalties
            //==================================================

            //Holding (Defense)
            r.Add(new Penalty()
            {
                code = Penalty_Codes.DH,
                Yards = 5,
                bDeclinable = true,
                bAuto_FirstDown = true,
                bSpot_Foul = false,
                Play_Timing = Play_Snap_Timing.DURING_PLAY,
                Frequency_Rating = 400,
                Description = "Defensive Holding"
            });
            r.Last().Penalty_Play_Types = new List<Play_Enum>()
            {
                Play_Enum.PASS,
                Play_Enum.SCRIM_PLAY_1XP_PASS,
                Play_Enum.SCRIM_PLAY_2XP_PASS,
                Play_Enum.SCRIM_PLAY_3XP_PASS
            };
            r.Last().Player_Action_States = new List<Player_Action_State>()
            {
                Player_Action_State.PAR, Player_Action_State.PD
            };

            //Illegal Contact
            r.Add(new Penalty()
            {
                code = Penalty_Codes.IC,
                Yards = 5,
                bDeclinable = true,
                bAuto_FirstDown = true,
                bSpot_Foul = false,
                Play_Timing = Play_Snap_Timing.DURING_PLAY,
                Frequency_Rating = 80,
                Description = "Illegal Contact"
            });
            r.Last().Penalty_Play_Types = new List<Play_Enum>()
            {
                Play_Enum.PASS,
                Play_Enum.SCRIM_PLAY_1XP_PASS,
                Play_Enum.SCRIM_PLAY_2XP_PASS,
                Play_Enum.SCRIM_PLAY_3XP_PASS
            };
            r.Last().Player_Action_States = new List<Player_Action_State>()
            {
                Player_Action_State.PD
            };

            //Illegal Use of Hands (defense)
            r.Add(new Penalty()
            {
                code = Penalty_Codes.IH,
                Yards = 5,
                bDeclinable = true,
                bAuto_FirstDown = true,
                bSpot_Foul = false,
                Play_Timing = Play_Snap_Timing.DURING_PLAY,
                Frequency_Rating = 50,
                Description = "Illegal Use of Hands"
            });
            r.Last().Penalty_Play_Types = new List<Play_Enum>()
            {
                Play_Enum.RUN,
                Play_Enum.PASS,
                Play_Enum.SCRIM_PLAY_1XP_PASS,
                Play_Enum.SCRIM_PLAY_1XP_RUN,
                Play_Enum.SCRIM_PLAY_2XP_PASS,
                Play_Enum.SCRIM_PLAY_2XP_RUN,
                Play_Enum.SCRIM_PLAY_3XP_PASS,
                Play_Enum.SCRIM_PLAY_3XP_RUN
            };
            r.Last().Player_Action_States = new List<Player_Action_State>()
            {
                Player_Action_State.PAR, Player_Action_State.RD
            };

            //Illegal Use of Hands Offense
            r.Add(new Penalty()
            {
                code = Penalty_Codes.IHO,
                Yards = 10,
                bDeclinable = true,
                bAuto_FirstDown = false,
                bSpot_Foul = false,
                Play_Timing = Play_Snap_Timing.DURING_PLAY,
                Frequency_Rating = 20,
                Description = "Illegal Use of Hands"
            });
            r.Last().Penalty_Play_Types = new List<Play_Enum>()
            {
                Play_Enum.RUN,
                Play_Enum.PASS,
                Play_Enum.SCRIM_PLAY_1XP_PASS,
                Play_Enum.SCRIM_PLAY_1XP_RUN,
                Play_Enum.SCRIM_PLAY_2XP_PASS,
                Play_Enum.SCRIM_PLAY_2XP_RUN,
                Play_Enum.SCRIM_PLAY_3XP_PASS,
                Play_Enum.SCRIM_PLAY_3XP_RUN,
            };
            r.Last().Player_Action_States = new List<Player_Action_State>()
            {
                Player_Action_State.PB,Player_Action_State.RB, Player_Action_State.PC, Player_Action_State.BRN
            };

            //Illegal Block Kickoff
            r.Add(new Penalty()
            {
                code = Penalty_Codes.KIB,
                Yards = 15,
                bDeclinable = true,
                bAuto_FirstDown = false,
                bSpot_Foul = true,
                Play_Timing = Play_Snap_Timing.BEFORE_SNAP,
                Frequency_Rating = 750,
                Description = "Illegal Block"
            });
            r.Last().Penalty_Play_Types = new List<Play_Enum>()
            {
                Play_Enum.KICKOFF_NORMAL,
                Play_Enum.FREE_KICK
            };
            r.Last().Player_Action_States = new List<Player_Action_State>()
            {
                Player_Action_State.KRT
            };

            //Illegal Block Punt
            r.Add(new Penalty()
            {
                code = Penalty_Codes.PIB,
                Yards = 15,
                bDeclinable = true,
                bAuto_FirstDown = false,
                bSpot_Foul = true,
                Play_Timing = Play_Snap_Timing.BEFORE_SNAP,
                Frequency_Rating = 750,
                Description = "Illegal Block"
            });
            r.Last().Penalty_Play_Types = new List<Play_Enum>()
            {
                Play_Enum.PUNT
            };
            r.Last().Player_Action_States = new List<Player_Action_State>()
            {
                Player_Action_State.PRT
            };

            //Unsportsmen Like Conduct 
//defense and special teams tacklers tack on 15 yards to the end of the play
//Offense is 15 back from the line of scrmmage
//special team receving team is 15 yards back from spot of foul
            r.Add(new Penalty()
            {
                code = Penalty_Codes.UC,
                Yards = 15,
                bDeclinable = true,
                bAuto_FirstDown = true,
                bSpot_Foul = true,
                Play_Timing = Play_Snap_Timing.BEFORE_SNAP,
                Frequency_Rating = 750,
                Description = "Unsportsmen Like Conduct"
            });
            r.Last().Penalty_Play_Types = new List<Play_Enum>()
            {
                Play_Enum.KICKOFF_NORMAL,
                Play_Enum.FREE_KICK,
                Play_Enum.KICKOFF_ONSIDES,
                Play_Enum.RUN,
                Play_Enum.PASS,
                Play_Enum.SCRIM_PLAY_1XP_PASS,
                Play_Enum.SCRIM_PLAY_1XP_RUN,
                Play_Enum.SCRIM_PLAY_2XP_PASS,
                Play_Enum.SCRIM_PLAY_2XP_RUN,
                Play_Enum.SCRIM_PLAY_3XP_PASS,
                Play_Enum.SCRIM_PLAY_3XP_RUN,
                Play_Enum.PUNT,
                Play_Enum.FIELD_GOAL,
                Play_Enum.EXTRA_POINT
            };
            r.Last().Player_Action_States = new List<Player_Action_State>()
            {
                Player_Action_State.PAS,
                Player_Action_State.PC,
                Player_Action_State.BRN,
                Player_Action_State.PB,
                Player_Action_State.PAR,
                Player_Action_State.PD,
                Player_Action_State.RB,
                Player_Action_State.RD,
                Player_Action_State.K,
                Player_Action_State.KR,
                Player_Action_State.KRT,
                Player_Action_State.KDT,
                Player_Action_State.P,
                Player_Action_State.PR,
                Player_Action_State.PRT,
                Player_Action_State.PDT,
                Player_Action_State.FGT,
                Player_Action_State.FGD 
            };

            //Unnecessary Roughness
            //defense and special teams tacklers tack on 15 yards to the end of the play
            //Offense is 15 back from the line of scrmmage
            //special team receving team is 15 yards back from spot of foul
            r.Add(new Penalty()
            {
                code = Penalty_Codes.UR,
                Yards = 15,
                bDeclinable = true,
                bAuto_FirstDown = true,
                bSpot_Foul = true,
                Play_Timing = Play_Snap_Timing.BEFORE_SNAP,
                Frequency_Rating = 750,
                Description = "Unnecessary Roughness"
            });
            r.Last().Penalty_Play_Types = new List<Play_Enum>()
            {
                Play_Enum.KICKOFF_NORMAL,
                Play_Enum.FREE_KICK,
                Play_Enum.KICKOFF_ONSIDES,
                Play_Enum.RUN,
                Play_Enum.PASS,
                Play_Enum.SCRIM_PLAY_1XP_PASS,
                Play_Enum.SCRIM_PLAY_1XP_RUN,
                Play_Enum.SCRIM_PLAY_2XP_PASS,
                Play_Enum.SCRIM_PLAY_2XP_RUN,
                Play_Enum.SCRIM_PLAY_3XP_PASS,
                Play_Enum.SCRIM_PLAY_3XP_RUN,
                Play_Enum.PUNT,
                Play_Enum.FIELD_GOAL,
                Play_Enum.EXTRA_POINT
            };
            r.Last().Player_Action_States = new List<Player_Action_State>()
            {
                Player_Action_State.PAS,
                Player_Action_State.PC,
                Player_Action_State.BRN,
                Player_Action_State.PB,
                Player_Action_State.PAR,
                Player_Action_State.PD,
                Player_Action_State.RB,
                Player_Action_State.RD,
                Player_Action_State.K,
                Player_Action_State.KR,
                Player_Action_State.KRT,
                Player_Action_State.KDT,
                Player_Action_State.P,
                Player_Action_State.PR,
                Player_Action_State.PRT,
                Player_Action_State.PDT,
                Player_Action_State.FGT,
                Player_Action_State.FGD
            };

            //Facemask
            //defense and special teams tacklers tack on 15 yards to the end of the play
            //Offense is 15 back from the line of scrmmage
            //special team receving team is 15 yards back from spot of foul
            r.Add(new Penalty()
            {
                code = Penalty_Codes.FM,
                Yards = 15,
                bDeclinable = true,
                bAuto_FirstDown = true,
                bSpot_Foul = true,
                Play_Timing = Play_Snap_Timing.BEFORE_SNAP,
                Frequency_Rating = 750,
                Description = "Facemask"
            });
            r.Last().Penalty_Play_Types = new List<Play_Enum>()
            {
                Play_Enum.KICKOFF_NORMAL,
                Play_Enum.FREE_KICK,
                Play_Enum.KICKOFF_ONSIDES,
                Play_Enum.RUN,
                Play_Enum.PASS,
                Play_Enum.SCRIM_PLAY_1XP_PASS,
                Play_Enum.SCRIM_PLAY_1XP_RUN,
                Play_Enum.SCRIM_PLAY_2XP_PASS,
                Play_Enum.SCRIM_PLAY_2XP_RUN,
                Play_Enum.SCRIM_PLAY_3XP_PASS,
                Play_Enum.SCRIM_PLAY_3XP_RUN,
                Play_Enum.PUNT,
                Play_Enum.FIELD_GOAL,
                Play_Enum.EXTRA_POINT
            };
            r.Last().Player_Action_States = new List<Player_Action_State>()
            {
                Player_Action_State.PAS,
                Player_Action_State.PC,
                Player_Action_State.BRN,
                Player_Action_State.PB,
                Player_Action_State.PAR,
                Player_Action_State.PD,
                Player_Action_State.RB,
                Player_Action_State.RD,
                Player_Action_State.K,
                Player_Action_State.KR,
                Player_Action_State.KRT,
                Player_Action_State.KDT,
                Player_Action_State.P,
                Player_Action_State.PR,
                Player_Action_State.PRT,
                Player_Action_State.PDT,
                Player_Action_State.FGT,
                Player_Action_State.FGD
            };

            return r;
        }
        public static Player_Action_State getPlayerAction(Game_Player Penalty_Player, Play_Result pResult)

        {
            Player_Action_State r = Player_Action_State.PAS;

            if (Penalty_Player == pResult.Passer)
                r = Player_Action_State.PAS;
            else if ((Penalty_Player == pResult.Kicker))
                r = Player_Action_State.K;
            else if ((Penalty_Player == pResult.Punter))
                r = Player_Action_State.P;
            else if ((Penalty_Player == pResult.Returner))
                r = Player_Action_State.KR;
            else if ((Penalty_Player == pResult.Punt_Returner))
                r = Player_Action_State.PR;
            else if (pResult.Pass_Catchers.Contains(Penalty_Player))
                r = Player_Action_State.PC;
            else if (pResult.Ball_Runners.Contains(Penalty_Player))
                r = Player_Action_State.BRN;
            else if (pResult.Pass_Blockers.Contains(Penalty_Player))
                r = Player_Action_State.PB;
            else if (pResult.Pass_Rushers.Contains(Penalty_Player))
                r = Player_Action_State.PAR;
            else if (pResult.Pass_Defenders.Contains(Penalty_Player))
                r = Player_Action_State.PD;
            else if (pResult.Run_Blockers.Contains(Penalty_Player))
                r = Player_Action_State.RB;
            else if (pResult.Run_Defenders.Contains(Penalty_Player))
                r = Player_Action_State.RD;
            else if (pResult.Kick_Returners.Contains(Penalty_Player))
                r = Player_Action_State.KRT;
            else if (pResult.Kick_Defenders.Contains(Penalty_Player))
                r = Player_Action_State.KDT;
            else if (pResult.Punt_Returners.Contains(Penalty_Player))
                r = Player_Action_State.PRT;
            else if (pResult.Punt_Defenders.Contains(Penalty_Player))
                r = Player_Action_State.PDT;
            else if (pResult.FieldGaol_Kicking_Team.Contains(Penalty_Player))
                r = Player_Action_State.FGT;
            else if (pResult.Field_Goal_Defenders.Contains(Penalty_Player))
                r = Player_Action_State.FGD;
            return r;
        }
        public static Penalty getPenalty(List<Penalty> pList, Play_Enum pe, Player_Action_State pa)
        {
            List<Penalty> Penalties_for_Play_Player = null;

            Penalties_for_Play_Player = pList.Where(x => x.Penalty_Play_Types.Contains(pe) && x.Player_Action_States.Contains(pa)).ToList();

            if (Penalties_for_Play_Player.Count == 0)
                throw new Exception("Error in getPenalty after play.  No possible penalties found for play " + pe.ToString() + " and player action type " + pa.ToString());

            int ind = CommonUtils.getRandomIndex(Penalties_for_Play_Player.Count);

            return Penalties_for_Play_Player[ind];
        }
        public static Game_Player getPenaltyPlayer(List<Game_Player> Offensive_Players, List<Game_Player> Defensive_Players,
    Game_Player Passer, Game_Player Kicker, Game_Player Punter)
        {
            Game_Player r = null;
            List<Game_Player> Possible_Players = new List<Game_Player>();
            List<Game_Player> Players;


            for (int x = 0; x < 2; x++)
            {
                if (x == 0)
                    Players = Offensive_Players;
                else
                    Players = Defensive_Players;

                foreach (Game_Player p in Players)
                {
                    long sp_num;

                    if (p == Passer)
                        sp_num = (int)(app_Constants.SPORTSMANSHIP_ADJUSTER - p.p_and_r.pr.First().Sportsmanship_Ratings * app_Constants.PENALTY_UPPER_LIMIT_ADJ_QB);
                    else if (p == Kicker || p == Punter)
                        sp_num = (int) (app_Constants.SPORTSMANSHIP_ADJUSTER - p.p_and_r.pr.First().Sportsmanship_Ratings * app_Constants.PENALTY_UPPER_LIMIT_ADJ_K);
                    else
                        sp_num = app_Constants.SPORTSMANSHIP_ADJUSTER - p.p_and_r.pr.First().Sportsmanship_Ratings;

                    long rmd = CommonUtils.getRandomNum(1, (int)app_Constants.PENALTY_UPPER_LIMIT);
                    if (rmd <= sp_num)
                        Possible_Players.Add(p);
                }
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
        public static Tuple<bool, double> isHalfTheDistance(double penalty_yards, double dist_gl)
        {
            bool r = false;
            double half_the_dist = 0.0;
            double temp = dist_gl / 2;

            if (temp < penalty_yards)
            {
                r = true;
                half_the_dist = temp;
            }

            return new Tuple<bool,double>(r,half_the_dist);
        }
        public static bool isFirstDowwithPenalty(Penalty p, double yards_to_go,  bool bHlaft_the_dist, double half_dist_yards)
        {
            bool r = false;

            if (p.bAuto_FirstDown)
                r = true;
            else if (bHlaft_the_dist)
            {
                if (half_dist_yards >= yards_to_go)
                    r = true;
            }
            else
            {
                if (p.Yards >= yards_to_go)
                    r = true;
            }

            return r;
        }
    }
}
