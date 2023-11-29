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
                Player_Action_State.PAS, Player_Action_State.K, Player_Action_State.P
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
                Play_Enum.SCRIM_PLAY_3XP_RUN
            };
            r.Last().Player_Action_States = new List<Player_Action_State>()
            {
                Player_Action_State.PB,Player_Action_State.RB, Player_Action_State.PC
            };
            //Illegal Motion
            r.Add(new Penalty()
            {
                code = Penalty_Codes.IM,
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
                Player_Action_State.PAR,Player_Action_State.PRT, Player_Action_State.FGD,
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
                Player_Action_State.PAR,Player_Action_State.PRT, Player_Action_State.FGD,
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

            //Illegal Use of Hands (Offense)
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
            //Pass Interference (Offense)
            r.Add(new Penalty()
            {
                code = Penalty_Codes.OI,
                Yards = 10,
                bDeclinable = true,
                bAuto_FirstDown = false,
                bSpot_Foul = false,
                Play_Timing = Play_Snap_Timing.DURING_PLAY,
                Frequency_Rating = 300,
                Description = "Offensive Pass Interference"
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
                Player_Action_State.PC
            };
            //Holding (Offense)
            r.Add(new Penalty()
            {
                code = Penalty_Codes.OH,
                Yards = 10,
                bDeclinable = true,
                bAuto_FirstDown = false,
                bSpot_Foul = false,
                Play_Timing = Play_Snap_Timing.DURING_PLAY,
                Frequency_Rating = 800,
                Description = "Holding"
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
                Player_Action_State.PB
            };
            //ineligible man downfield
            r.Add(new Penalty()
            {
                code = Penalty_Codes.IR,
                Yards = 5,
                bDeclinable = true,
                bAuto_FirstDown = false,
                bSpot_Foul = false,
                Play_Timing = Play_Snap_Timing.DURING_PLAY,
                Frequency_Rating = 600,
                Description = "ineligible man downfield"
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
                Player_Action_State.PB
            };
            //Illegal Block Above the Waist
            r.Add(new Penalty()
            {
                code = Penalty_Codes.IB,
                Yards = 10,
                bDeclinable = true,
                bAuto_FirstDown = false,
                bSpot_Foul = false,
                Play_Timing = Play_Snap_Timing.DURING_PLAY,
                Frequency_Rating = 150,
                Description = "Illegal Block Above the Waist"
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
                Player_Action_State.PB
            };
            //Illegal Contact (Defense)
            r.Add(new Penalty()
            {
                code = Penalty_Codes.IC,
                Yards = 5,
                bDeclinable = true,
                bAuto_FirstDown = true,
                bSpot_Foul = false,
                Play_Timing = Play_Snap_Timing.DURING_PLAY,
                Frequency_Rating = 100,
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
            //Roughing the Passer
            r.Add(new Penalty()
            {
                code = Penalty_Codes.RD,
                Yards = 15,
                bDeclinable = false,
                bAuto_FirstDown = true,
                bSpot_Foul = true,
                Play_Timing = Play_Snap_Timing.DURING_PLAY,
                Frequency_Rating = 300,
                Description = "Roughing the Passer"
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
                Player_Action_State.PAR
            };
            //Pass Interference (Defense)
            r.Add(new Penalty()
            {
                code = Penalty_Codes.PI,
                Yards = 15,
                bDeclinable = true,
                bAuto_FirstDown = true,
                bSpot_Foul = true,
                Play_Timing = Play_Snap_Timing.DURING_PLAY,
                Frequency_Rating = 700,
                Description = "Pass Interference"
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
            //Illegal Block Kickoff
            r.Add(new Penalty()
            {
                code = Penalty_Codes.KIB,
                Yards = 15,
                bDeclinable = true,
                bAuto_FirstDown = false,
                bSpot_Foul = true,
                Play_Timing = Play_Snap_Timing.DURING_PLAY,
                Frequency_Rating = 800,
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
                Play_Timing = Play_Snap_Timing.DURING_PLAY,
                Frequency_Rating = 800,
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

            //Roughing the Kicker
            r.Add(new Penalty()
            {
                code = Penalty_Codes.RK,
                Yards = 15,
                bDeclinable = true,
                bAuto_FirstDown = true,
                bSpot_Foul = false,
                Play_Timing = Play_Snap_Timing.DURING_PLAY,
                Frequency_Rating = 50,
                Description = "Roughing the Kicker"
            });
            r.Last().Penalty_Play_Types = new List<Play_Enum>()
            {
                Play_Enum.PUNT
            };
            r.Last().Player_Action_States = new List<Player_Action_State>()
            {
                Player_Action_State.PRT
            };
            //Running into the Kicker
            r.Add(new Penalty()
            {
                code = Penalty_Codes.RIK,
                Yards = 5,
                bDeclinable = true,
                bAuto_FirstDown = false,
                bSpot_Foul = true,
                Play_Timing = Play_Snap_Timing.DURING_PLAY,
                Frequency_Rating = 50,
                Description = "Running into the Kicker"
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
                Play_Timing = Play_Snap_Timing.DURING_PLAY,
                Frequency_Rating = 300,
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
                Play_Timing = Play_Snap_Timing.DURING_PLAY,
                Frequency_Rating = 300,
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
                Play_Timing = Play_Snap_Timing.DURING_PLAY,
                Frequency_Rating = 300,
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
        public static Tuple<Game_Player, Penalty> PostSnap_Penalty(Play_Enum pe, List<Penalty> Penalty_List,
            List<Game_Player> Offensive_Players, List<Game_Player> Defensive_Players, Play_Result pResult)
        {
            Game_Player Penalty_Player = null;
            List<Game_Player> Possible_Players = new List<Game_Player>();
            List<Penalty> Possible_Penalties = null;
            Penalty Penalty = null;
            int Upper_Limit_Roughing_Passer = 500;
            int Upper_Pass_Defender_Interference = 500;
            int Upper_Pass_Receiver_Interference = 1500;
            int Upper_Roughing_the_kicker = 1500;
            int Upper_Running_into_the_kciker = 1500;
            List<Game_Player> Player_list = new List<Game_Player>();

            //Get all possible positions for post snap and this play
            List<Player_Action_State> Poss_Play_Types_List = Penalty_List.Where(x => x.Play_Timing == Play_Snap_Timing.DURING_PLAY &&
             x.Penalty_Play_Types.Contains(pe)).SelectMany(d => d.Player_Action_States).Distinct().ToList();

            //Only try to find a penalty if there is a penalty for this situation
            if (Poss_Play_Types_List != null && Poss_Play_Types_List.Count() > 0)
            {
                Penalty Roughing_The_Passer = Penalty_List.Where(x => x.code == Penalty_Codes.RD).First();
                Penalty Pass_Interference_Def = Penalty_List.Where(x => x.code == Penalty_Codes.PI).First();
                Penalty Pass_Interference_Off = Penalty_List.Where(x => x.code == Penalty_Codes.OI).First();
                Penalty Roughing_The_Kicker = Penalty_List.Where(x => x.code == Penalty_Codes.RK).First();
                Penalty Running_Into_the_Kicker = Penalty_List.Where(x => x.code == Penalty_Codes.RIK).First();

                //Next let's check for possible Roughing the Passer
                if (pResult.Defender_Knocks_Down_QB != null && pe == Play_Enum.PASS)
                {
                    int t = (int) (101 - pResult.Defender_Knocks_Down_QB.p_and_r.pr.First().Sportsmanship_Ratings);
                    int rnd = CommonUtils.getRandomNum(1, Upper_Limit_Roughing_Passer);
                    if (rnd <= t)
                    {
                        Penalty_Player = pResult.Defender_Knocks_Down_QB;
                        Penalty = Roughing_The_Passer;
                    }
                }
                else if (pResult.Defender_Close_to_Receiver != null && pe == Play_Enum.PASS)
                {
                    int t = 0;
                    int rnd = 0;
                    t = (int)(101 - pResult.Defender_Close_to_Receiver.p_and_r.pr.First().Sportsmanship_Ratings);
                    rnd = CommonUtils.getRandomNum(1, Upper_Pass_Defender_Interference);
                    if (rnd <= t)
                    {
                        Penalty_Player = pResult.Defender_Close_to_Receiver;
                        Penalty = Pass_Interference_Def;
                    }
                    else
                    {
                        t = (int)(101 - pResult.Targeted_Receiver.p_and_r.pr.First().Sportsmanship_Ratings);
                        rnd = CommonUtils.getRandomNum(1, Upper_Pass_Receiver_Interference);
                        if (rnd <= t)
                        {
                            Penalty_Player = pResult.Targeted_Receiver;
                            Penalty = Pass_Interference_Off;
                        }
                    }
                }
                else if (pResult.Defender_Close_to_Kicker != null && pe == Play_Enum.PUNT)
                {
                    int t = 0;
                    int rnd = 0;
                    t = (int)(101 - pResult.Defender_Close_to_Kicker.p_and_r.pr.First().Sportsmanship_Ratings);
                    rnd = CommonUtils.getRandomNum(1, Upper_Roughing_the_kicker);
                    if (rnd <= t)
                    {
                        Penalty_Player = pResult.Defender_Close_to_Kicker;
                        Penalty = Roughing_The_Kicker;
                    }
                    else
                    {
                        t = (int)(101 - pResult.Defender_Close_to_Kicker.p_and_r.pr.First().Sportsmanship_Ratings);
                        rnd = CommonUtils.getRandomNum(1, Upper_Running_into_the_kciker);
                        if (rnd <= t)
                        {
                            Penalty_Player = pResult.Defender_Close_to_Kicker;
                            Penalty = Running_Into_the_Kicker;
                        }
                    }
                }

                //No special penalty, so look for another presnap penalty
                if (Penalty == null)
                {
                    Player_list.AddRange(Offensive_Players);
                    Player_list.AddRange(Defensive_Players);
                    Player_list = CommonUtils.ShufleList(Player_list);

                    foreach (Game_Player p in Player_list)
                    {
                        long sp_num;

                        Player_Action_State pa = getPlayerAction(p, pResult);

                        if (!Poss_Play_Types_List.Contains(pa))
                            continue;

                        if (p == pResult.Passer)
                            sp_num = (int)(app_Constants.SPORTSMANSHIP_ADJUSTER - p.p_and_r.pr.First().Sportsmanship_Ratings * app_Constants.PENALTY_UPPER_LIMIT_ADJ_QB);
                        else if (p == pResult.Kicker || p == pResult.Punter)
                            sp_num = (int)(app_Constants.SPORTSMANSHIP_ADJUSTER - p.p_and_r.pr.First().Sportsmanship_Ratings * app_Constants.PENALTY_UPPER_LIMIT_ADJ_K);
                        else
                            sp_num = app_Constants.SPORTSMANSHIP_ADJUSTER - p.p_and_r.pr.First().Sportsmanship_Ratings;

                        long rmd = CommonUtils.getRandomNum(1, (int)app_Constants.PENALTY_UPPER_LIMIT);
                        if (rmd <= sp_num)
                            Possible_Players.Add(p);
                    }

                    if (Possible_Players.Count > 0)
                    {
                        int ind = CommonUtils.getRandomIndex(Possible_Players.Count());
                        Penalty_Player = Possible_Players[ind];

                        Player_Action_State pa = getPlayerAction(Penalty_Player, pResult);

                        Possible_Penalties = Penalty_List.Where(x => x.Play_Timing == Play_Snap_Timing.DURING_PLAY && x.Penalty_Play_Types.Contains(pe) && x.Player_Action_States.Contains(pa)).ToList();

                        if (Possible_Penalties.Count == 0)
                            throw new Exception("Error in PostSnap_Penalty after play.  No possible penalties found for play " + pe.ToString() + " and player action type " + pa.ToString());

                        Penalty = Select_Penalty_by_Frequency(Possible_Penalties);
                    }

                }

            }

            return new Tuple<Game_Player, Penalty>(Penalty_Player, Penalty);
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

        public static Tuple<Game_Player, Penalty> Presnap_Penalty(Play_Enum pe, List<Penalty> Penalty_List,
            List<Game_Player> Offensive_Players, List<Game_Player> Defensive_Players, Play_Result pResult)
        {
            Game_Player Penalty_Player = null;
            List<Game_Player> Possible_Players = new List<Game_Player>();
            List<Penalty> Possible_Penalties = null;
            Penalty Penalty = null;
            int Upper_Limit_Regular_Play_DOG = 700;
            int Upper_Limit_Other_Play_DOG = 1400;
            List<Game_Player> Player_list = new List<Game_Player>();

            //Get all possible positions for presnap and this play
            List<Player_Action_State> Poss_Player_states_List = Penalty_List.Where(x => x.Play_Timing == Play_Snap_Timing.BEFORE_SNAP &&
             x.Penalty_Play_Types.Contains(pe)).SelectMany(d => d.Player_Action_States).Distinct().ToList();

            //Only try to find a penalty if there is a penalty for this situation
            if (Poss_Player_states_List != null && Poss_Player_states_List.Count() > 0)
            {
                Penalty Delay_of_Game = Penalty_List.Where(x => x.code == Penalty_Codes.DG).First();
                //Next let's check for possible delay of game penalty
                if (pResult.Passer != null)
                {
                    int t = (int) (101 - pResult.Passer.p_and_r.pr.First().Decision_Making_Rating);
                    int rnd = CommonUtils.getRandomNum(1, Upper_Limit_Regular_Play_DOG);
                    if (rnd <= t)
                    {
                        Penalty_Player = pResult.Passer;
                        Penalty = Delay_of_Game;
                    }
                }
                else if (pResult.Kicker != null || pResult.Punter != null)
                {
                    Game_Player gp = pResult.Kicker != null ? pResult.Kicker : pResult.Punter;
                    int t = (int)(101 - gp.p_and_r.pr.First().Sportsmanship_Ratings);
                    int rnd = CommonUtils.getRandomNum(1, Upper_Limit_Other_Play_DOG);
                    if (rnd <= t)
                    {
                        Penalty_Player = gp;
                        Penalty = Delay_of_Game;
                    }
                }

                //No delay of game so llook for another presnap penalty
                if (Penalty == null)
                {
                    Player_list.AddRange(Offensive_Players);
                    Player_list.AddRange(Defensive_Players);
                    Player_list = CommonUtils.ShufleList(Player_list);

                    foreach (Game_Player p in Player_list)
                    {
                        long sp_num;

                        Player_Action_State pa = getPlayerAction(p, pResult);

                        if (!Poss_Player_states_List.Contains(pa))
                            continue;

                        if (p == pResult.Passer)
                            sp_num = (int)(app_Constants.SPORTSMANSHIP_ADJUSTER - p.p_and_r.pr.First().Sportsmanship_Ratings * app_Constants.PENALTY_UPPER_LIMIT_ADJ_QB);
                        else if (p == pResult.Kicker || p == pResult.Punter)
                            sp_num = (int)(app_Constants.SPORTSMANSHIP_ADJUSTER - p.p_and_r.pr.First().Sportsmanship_Ratings * app_Constants.PENALTY_UPPER_LIMIT_ADJ_K);
                        else
                            sp_num = app_Constants.SPORTSMANSHIP_ADJUSTER - p.p_and_r.pr.First().Sportsmanship_Ratings;

                        long rmd = CommonUtils.getRandomNum(1, (int)app_Constants.PENALTY_UPPER_LIMIT);
                        if (rmd <= sp_num)
                            Possible_Players.Add(p);
                    }

                    if (Possible_Players.Count > 0)
                    {
                        int ind = CommonUtils.getRandomIndex(Possible_Players.Count());
                        Penalty_Player = Possible_Players[ind];

                        Player_Action_State pa = getPlayerAction(Penalty_Player, pResult);

                        Possible_Penalties = Penalty_List.Where(x => x.Play_Timing == Play_Snap_Timing.BEFORE_SNAP && x.Penalty_Play_Types.Contains(pe) && x.Player_Action_States.Contains(pa)).ToList();

                        if (Possible_Penalties.Count == 0)
                            throw new Exception("Error in Presnap_Penalty after play.  No possible penalties found for play " + pe.ToString() + " and player action type " + pa.ToString());

                        Penalty = Select_Penalty_by_Frequency(Possible_Penalties);
                    }

                }

            }

            return new Tuple<Game_Player, Penalty>(Penalty_Player, Penalty);
        }

        public static Penalty Select_Penalty_by_Frequency(List<Penalty> p_list)
        {
            int UPPER_LIMIT = 1000;
            Penalty r = null;
            int rndindex = 0;

            for (int i = 0; i < 1000; i++)
            {
                rndindex = CommonUtils.getRandomIndex(p_list.Count());
                int rnd = CommonUtils.getRandomNum(1, UPPER_LIMIT);

                if (p_list[rndindex].Frequency_Rating <= rnd)
                {
                    r = p_list[rndindex];
                    break;
                }
            }

            if (r == null)
                r = p_list[rndindex]; ;

            return r;
        }


    }
}
