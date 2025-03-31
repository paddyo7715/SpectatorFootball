namespace SpectatorFootball.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Game_Player_Stats
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Game_ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Player_ID { get; set; }

        public long Franchise_ID { get; set; }

        public long Started { get; set; }

        public long off_pass_plays { get; set; }

        public long off_pass_fumbles { get; set; }

        public long off_pass_Fumbles_Lost { get; set; }

        public long off_pass_Sacked { get; set; }

        public long off_pass_Pressures { get; set; }

        public long off_pass_Comp { get; set; }

        public long off_pass_Att { get; set; }

        public long off_pass_Yards { get; set; }

        public long off_pass_TDs { get; set; }

        public long off_pass_Ints { get; set; }

        public long off_pass_Long { get; set; }

        public long off_pass_Passes_Blocked { get; set; }

        public long off_rush_plays { get; set; }

        public long off_rush_fumbles { get; set; }

        public long off_rush_fumbles_lost { get; set; }

        public long off_rush_att { get; set; }

        public long off_rush_Yards { get; set; }

        public long off_rush_TDs { get; set; }

        public long off_rush_long { get; set; }

        public long off_rec_plays { get; set; }

        public long off_rec_fumbles { get; set; }

        public long off_rec_fumbles_lost { get; set; }

        public long off_rec_catches { get; set; }

        public long off_rec_drops { get; set; }

        public long off_rec_Yards { get; set; }

        public long off_rec_TDs { get; set; }

        public long off_rec_long { get; set; }

        public long off_line_plays { get; set; }

        public long off_line_sacks_allowed { get; set; }

        public long off_line_Rushing_Loss_Allowed { get; set; }

        public long off_line_missed_block { get; set; }

        public long off_line_pancakes { get; set; }

        public long off_line_was_pancaked { get; set; }

        public long off_line_QB_Pressures_Allowed { get; set; }

        public long def_rush_plays { get; set; }

        public long def_rush_sacks { get; set; }

        public long def_rush_Rushing_Loss { get; set; }

        public long def_rush_tackles { get; set; }

        public long def_rush_Missed_Tackles { get; set; }

        public long def_rush_Safety { get; set; }

        public long def_rush_TDs { get; set; }

        public long def_rush_Forced_Fumbles { get; set; }

        public long def_rush_Recovered_Fumbles { get; set; }

        public long def_rush_QB_Pressures { get; set; }

        public long def_rush_Pass_Blocks { get; set; }

        public long def_rush_pancakes { get; set; }

        public long def_rush_was_pancaked { get; set; }

        public long def_pass_plays { get; set; }

        public long def_pass_Ints { get; set; }

        public long def_pass_Int_Yards { get; set; }

        public long def_pass_Int_TDs { get; set; }

        public long def_pass_Pass_KnockedAway { get; set; }

        public long def_pass_Tackles { get; set; }

        public long def_pass_Missed_Tackles { get; set; }

        public long def_pass_Forced_Fumbles { get; set; }

        public long def_pass_Recovered_Fumbles { get; set; }

        public long def_pass_Recovered_Fumble_Yards { get; set; }

        public long def_pass_Recovered_Fumble_TDs { get; set; }

        public long kicker_plays { get; set; }

        public long XP_Plays { get; set; }

        public long XP_Def_Plays { get; set; }

        public long XP_Att { get; set; }

        public long XP_Made { get; set; }
        public long XP_Block { get; set; }

        public long FG_Plays { get; set; }

        public long FG_Att { get; set; }

        public long FG_Made { get; set; }

        public long FG_Long { get; set; }
        public long fg_def_plays { get; set; }

        public long fg_def_block { get; set; }

        public long fg_def_block_recovered { get; set; }

        public long fg_def_block_recovered_yards { get; set; }

        public long fg_def_block_recovered_TDs { get; set; }

        public long Kickoffs { get; set; }

        public long Kickoffs_Out_of_Bounds { get; set; }

        public long Kickoff_Touchbacks { get; set; }

        public long Kickoff_Thru_Endzones { get; set; }

        public long ko_ret_plays { get; set; }

        public long ko_ret { get; set; }

        public long ko_ret_yards { get; set; }

        public long ko_ret_TDs { get; set; }

        public long ko_ret_yards_long { get; set; }

        public long ko_ret_fumbles { get; set; }

        public long ko_ret_fumbles_lost { get; set; }

        public long ko_def_plays { get; set; }

        public long ko_def_Forced_Fumbles { get; set; }

        public long ko_fumbles_recovered { get; set; }

        public long ko_def_fumbles_recovered_TDs { get; set; }

        public long ko_def_fumbles_recovered_Yards { get; set; }

        public long ko_def_tackles { get; set; }

        public long ko_def_tackles_missed { get; set; }

        public long ko_rec_plays { get; set; }

        public long punter_plays { get; set; }

        public long punter_Fumbles { get; set; }

        public long punter_Fumbles_lost { get; set; }

        public long punter_Fumbles_recovered { get; set; }

        public long punter_punts { get; set; }

        public long punter_punt_yards { get; set; }

        public long punter_kill_att { get; set; }

        public long punter_kill_Succ { get; set; }

        public long punter_blocks { get; set; }

        public long punt_ret_plays { get; set; }

        public long punt_ret { get; set; }

        public long punt_ret_yards { get; set; }

        public long punt_ret_TDs { get; set; }

        public long punt_ret_yards_long { get; set; }

        public long punt_ret_fumbles { get; set; }

        public long punt_ret_fumbles_lost { get; set; }

        public long punt_rec_plays { get; set; }

        public long punt_rec_blocks { get; set; }

        public long punt_rec_block_recovery { get; set; }

        public long punt_rec_block_recovery_yards { get; set; }

        public long punt_rec_block_recovery_TDs { get; set; }

        public long punt_def_plays { get; set; }

        public long punt_def_forced_fumbles { get; set; }

        public long punt_forced_fumbles_recovered { get; set; }

        public long punt_def_forced_fumbles_recovered_TDs { get; set; }

        public long punt_def_forced_fumbles_recovered_Yards { get; set; }

        public long punt_def_tackles { get; set; }

        public long punt_def_tackles_missed { get; set; }

        public long def_pass_TDs_Surrendered { get; set; }

        public long punt_rec_block_safety { get; set; }
 
        public long punt_rec_muffed { get; set; }
 
        public long punt_rec_muffed_lost { get; set; }
 
        public long punt_muffed_recovered { get; set; }
        public long scrimmage_onside_play { get; set; }
        public long ko_onside_play { get; set; }

        public long ko_onside_kick_att { get; set; }
        public long ko_onside_kick_made { get; set; }
        public long ko_onside_recovered { get; set; }
        public long ko_onside_def_plays { get; set; }

        



        public virtual Franchise Franchise { get; set; }

        public virtual Game Game { get; set; }

        public virtual Player Player { get; set; }
    }
}
