﻿using System.IO;
using System;
using System.Drawing;


namespace SpectatorFootball
{
    public class app_Constants
    {
        // Max Settings
        public const int MAX_DIVISIONS = 20;
        public const int MAX_TEAMS = 200;
        public const int MAX_PLAYOFF_TEAMS = 64;

        // Folders / Files
        public const int MIN_FREE_DISK_SPACE = 50;
        public const string GAME_DOC_FOLDER = "Spect_Football_Data";
        public const string LOG_FOLDER = "Logs";
        public const string BACKUP_FOLDER = "Backup";
        public const string LEAGUE_HELMETS_SUBFOLDER = "Helmet_Images";
        public const string LEAGUE_STADIUM_SUBFOLDER = "Stadium_Images";
        public const string LEAGUE_PROFILE_FILE = "Profile.txt";

        // The following three were intended to be constants, but when I added the file seperator variable, they could no longer be Consts
        public static string APP_HELMET_FOLDER = Path.DirectorySeparatorChar + "Images" + Path.DirectorySeparatorChar + "Helmets" + Path.DirectorySeparatorChar;
        public static string APP_STADIUM_FOLDER = Path.DirectorySeparatorChar + "Images" + Path.DirectorySeparatorChar + "Stadiums" + Path.DirectorySeparatorChar;
        public static string BLANK_DB_FOLDER = Path.DirectorySeparatorChar + "Database";

        public const string DB_FILE_EXT = "db";
        public const string BLANK_DB = "BlankDB.db";
        public const string SETTINGS_DB = "BlankDB.db";

        // Draft
        public const float DRAFT_MULTIPLIER = 1.6f;  //Multiplied by number of rounds to determine num of players to create for draft.
        public const double DRAFT_DELTA_MULTIPLYER = 1.5;
        public const int NORMAL_DRAFT_ROUNDS = 7;
        public const int STARTER_MIN_OVERALL_GRADE = 82;
        public const double DRAFT_ROUND_PERCNT_BEFORE_KICKERS_CONSIDERED = 70.0;
        public const int DRAFT_STARTER_AGE_TOO_OLD = 35;
        public const int PLAYER_ABILITY_PEAK_AGE = 28;
        public const int DRAFT_QB_CHOICE_PICK_DEPTH = 10;
        public const int DRAFT_OTHER_CHOICE_PICK_DEPTH = 5;
        public const int DRAFT_PERCENT_CRAPPIFY = 75;

        // Players On a team - 53 total
        public const int QB_PER_TEAM = 3;
        public const int RB_PER_TEAM = 4;
        public const int WR_PER_TEAM = 6;
        public const int TE_PER_TEAM = 3;
        public const int OL_PER_TEAM = 10;

        public const int DL_PER_TEAM = 9;
        public const int LB_PER_TEAM = 8;
        public const int DB_PER_TEAM = 8;

        public const int K_PER_TEAM = 1;
        public const int P_PER_TEAM = 1;

        // Players On a Training Camp Team 86 total
        public const int TRIANINGCAMP_QB_PER_TEAM = 5;
        public const int TRIANINGCAMP_RB_PER_TEAM = 7;
        public const int TRIANINGCAMP_WR_PER_TEAM = 10;
        public const int TRIANINGCAMP_TE_PER_TEAM = 5;
        public const int TRIANINGCAMP_OL_PER_TEAM = 14;  

        public const int TRIANINGCAMP_DL_PER_TEAM = 15;
        public const int TRIANINGCAMP_LB_PER_TEAM = 13;
        public const int TRIANINGCAMP_DB_PER_TEAM = 13;  

        public const int TRIANINGCAMP_K_PER_TEAM = 2;
        public const int TRIANINGCAMP_P_PER_TEAM = 2;

        //Players Starters 22 + 2 kickers
        public const int STARTER_QB_PER_TEAM = 1;
        public const int STARTER_RB_PER_TEAM = 2;
        public const int STARTER_WR_PER_TEAM = 2;
        public const int STARTER_TE_PER_TEAM = 1;
        public const int STARTER_OL_PER_TEAM = 5;

        public const int STARTER_DL_PER_TEAM = 4;
        public const int STARTER_LB_PER_TEAM = 3;
        public const int STARTER_DB_PER_TEAM = 4;

        public const int STARTER_K_PER_TEAM = 1;
        public const int STARTER_P_PER_TEAM = 1;

        // Players Draft importance by position. 
        //This is the upper range for random number 1 to n
        public const int DRAFTIMPORTANCE_QB_PER_TEAM = 35;
        public const int DRAFTIMPORTANCE_RB_PER_TEAM = 12;
        public const int DRAFTIMPORTANCE_WR_PER_TEAM = 12;
        public const int DRAFTIMPORTANCE_TE_PER_TEAM = 12;
        public const int DRAFTIMPORTANCE_OL_PER_TEAM = 12;

        public const int DRAFTIMPORTANCE_DL_PER_TEAM = 12;
        public const int DRAFTIMPORTANCE_LB_PER_TEAM = 12;
        public const int DRAFTIMPORTANCE_DB_PER_TEAM = 12;

        public const int DRAFTIMPORTANCE_K_PER_TEAM = 10; //+12 near end of draf
        public const int DRAFTIMPORTANCE_P_PER_TEAM = 10; //+12 near end of draf

        //Player Height and Weight Ranges.
        //Height is in inches
        public const int PLAYER_WEIGHT_VARIANT_PERCENT = 20;
        public const int WEIGHT_ADJUSTMENT_MULTIPLYER = 2;
        public const int WEIGHT_ADJUSTMENT_HALF_RANGE = 10;

        public const int QB_LOW_HEIGHT = 71;
        public const int QB_HIGH_HEIGHT = 80;
        public const int QB_LOW_WEIGHT = 190;
        public const int QB_HIGH_WEIGHT = 230;

        public const int RB_LOW_HEIGHT = 66;
        public const int RB_HIGH_HEIGHT = 73;
        public const int RB_LOW_WEIGHT = 170;
        public const int RB_HIGH_WEIGHT = 240;

        public const int WR_LOW_HEIGHT = 68;
        public const int WR_HIGH_HEIGHT = 77;
        public const int WR_LOW_WEIGHT = 170;
        public const int WR_HIGH_WEIGHT = 210;

        public const int TE_LOW_HEIGHT = 73;
        public const int TE_HIGH_HEIGHT = 79;
        public const int TE_LOW_WEIGHT = 240;
        public const int TE_HIGH_WEIGHT = 270;

        public const int OL_LOW_HEIGHT = 74;
        public const int OL_HIGH_HEIGHT = 80;
        public const int OL_LOW_WEIGHT = 285;
        public const int OL_HIGH_WEIGHT = 340;

        public const int DL_LOW_HEIGHT = 73;
        public const int DL_HIGH_HEIGHT = 79;
        public const int DL_LOW_WEIGHT = 260;
        public const int DL_HIGH_WEIGHT = 300;

        public const int LB_LOW_HEIGHT = 71;
        public const int LB_HIGH_HEIGHT = 77;
        public const int LB_LOW_WEIGHT = 225;
        public const int LB_HIGH_WEIGHT = 245;

        public const int DB_LOW_HEIGHT = 68;
        public const int DB_HIGH_HEIGHT = 75;
        public const int DB_LOW_WEIGHT = 170;
        public const int DB_HIGH_WEIGHT = 210;

        public const int K_LOW_HEIGHT = 70;
        public const int K_HIGH_HEIGHT = 74;
        public const int K_LOW_WEIGHT = 180;
        public const int K_HIGH_WEIGHT = 220;

        public const int P_LOW_HEIGHT = 71;
        public const int P_HIGH_HEIGHT = 75;
        public const int P_LOW_WEIGHT = 185;
        public const int P_HIGH_WEIGHT = 230;

        public const int PERCENT_LEFTY = 10;

        // Numbering
        public const int QBLOWNUM = 7;
        public const int QBHIGHNUM = 18;

        public const int RBLOWNUM = 20;
        public const int RBHIGHNUM = 39;

        public const int OLLOWNUM = 60;
        public const int OLHIGHNUM = 79;

        public const int WRLOWNUM = 80;
        public const int WRHIGHNUM = 89;

        public const int TELOWNUM = 40;
        public const int TEHIGHNUM = 89;

        public const int DLLOWNUM = 70;
        public const int DLHIGHNUM = 99;

        public const int LBLOWNUM = 50;
        public const int LBHIGHNUM = 59;

        public const int DBLOWNUM = 20;
        public const int DBHIGHNUM = 49;

        public const int KLOWNUM = 1;
        public const int KHIGHNUM = 6;

        // Player Ratings Percentages for overall rating
        public const double QB_FUMBLE_PERCENT = 0.03;
        public const double QB_ARMSTRENGTH_PERCENT = 0.14;
        public const double QB_ACCURACY_RATING = 0.43;
        public const double QB_DESISION_RATING = 0.36;
        public const double QB_SPEED_RATING = 0.02;
        public const double QB_AGILITY_RATING = 0.02;

        public const double RB_RUNNING_PWER_PERCENT = 0.28;
        public const double RB_SPEED_PERCENT = 0.2;
        public const double RB_AGILITY_PERCENT = 0.28;
        public const double RB_FUMBLE_PERCENT = 0.06;
        public const double RB_HANDS_PERCENT = 0.18;

        public const double WR_SPEED_PERCENT = 0.35;
        public const double WR_AGILITY_PERCENT = 0.35;
        public const double WR_FUMBLE_PERCENT = 0.06;
        public const double WR_HANDS_PERCENT = 0.24;

        public const double TE_SPEED_PERCENT = 0.2;
        public const double TE_AGILITY_PERCENT = 0.3;
        public const double TE_FUMBLE_PERCENT = 0.03;
        public const double TE_HANDS_PERCENT = 0.3;
        public const double TE_PASS_BLOCK_PERCENT = 0.04;
        public const double TE_RUN_BLOCK_PERCENT = 0.13;

        public const double OL_PASS_BLOCK_PERCENT = 0.40;
        public const double OL_RUN_BLOCK_PERCENT = 0.44;
        public const double OL_AGILITY_PERCENT = 0.1;

        public const double DL_PASS_ATTACK_PERCENT = 0.2;
        public const double DL_RUN_ATTACK_PERCENT = 0.33;
        public const double DL_TACKLE_PERCENT = 0.2;
        public const double DL_AGILITY_PERCENT = 0.2;
        public const double DL_SPEED_PERCENT = 0.07;

        public const double DB_SPEED_PERCENT = 0.28;
        public const double DB_HANDS_PERCENT = 0.16;
        public const double DB_TACKLING_PERCENT = 0.28;
        public const double DB_AGILITY_PERCENT = 0.28;

        public const double LB_PASS_ATTACK_PERCENT = 0.3;
        public const double LB_RUN_ATTACK_PERCENT = 0.2;
        public const double LB_TACKLE_PERCENT = 0.2;
        public const double LB_HANDS_PERCENT = 0.06;
        public const double LB_SPEED_PERCENT = 0.12;
        public const double LB_AGILITY_PERCENT = 0.12;

        public const double K_KICK_ACC = 0.65;
        public const double K_LEG_STRENGTH = 0.35;


        // Stock Uniform Colors
        public static Color STOCK_GREY_COLOR = Color.FromArgb(40, 40, 40);

        public static Color STOCK_HELMET_COLOR = Color.FromArgb(150, 107, 117);
        public static Color STOCK_FACEMASK = Color.FromArgb(203, 19, 31);
        public static Color STOCK_HEL_LOGO_COLOR = Color.FromArgb(33, 56, 123);

        public static Color STOCK_JERSEY_COLOR = Color.FromArgb(128, 0, 0);
        public static Color STOCK_NUMBER_COLOR = Color.FromArgb(255, 255, 255);
        public static Color STOCK_NUM_OUTLINE_COLOR = Color.FromArgb(0, 16, 88);
        public static Color STOCK_SLEEVE_COLOR = Color.FromArgb(0, 255, 255);
        public static Color STOCK_SHOULDER_STRIPE_COLOR = Color.FromArgb(45, 84, 161);
        public static Color STOCK_SLEEVE_STRIPE_1_COLOR = Color.FromArgb(206, 40, 48);
        public static Color STOCK_SLEEVE_STRIPE_2_COLOR = Color.FromArgb(0, 128, 0);
        public static Color STOCK_SLEEVE_STRIPE_3_COLOR = Color.FromArgb(255, 255, 0);
        public static Color STOCK_SLEEVE_STRIPE_4_COLOR = Color.FromArgb(247, 52, 26);
        public static Color STOCK_SLEEVE_STRIPE_5_COLOR = Color.FromArgb(90, 4, 65);
        public static Color STOCK_SLEEVE_STRIPE_6_COLOR = Color.FromArgb(128, 54, 91);

        public static Color STOCK_PANTS_COLOR = Color.FromArgb(205, 82, 64);
        public static Color STOCK_PANTS_STRIPE_1_COLOR = Color.FromArgb(128, 128, 0);
        public static Color STOCK_PANTS_STRIPE_2_COLOR = Color.FromArgb(251, 91, 59);
        public static Color STOCK_PANTS_STRIPE_3_COLOR = Color.FromArgb(128, 128, 128);

        public static Color STOCK_SOCKS_COLOR = Color.FromArgb(236, 133, 102);
        public static Color STOCK_CLEATES_COLOR = Color.FromArgb(113, 89, 111);

        // age
        public const int STARTING_ROOKIE_AGE = 21;
        public const int NEWLEAGE_LOW_AGE = 21;
        public const int NEWLEAGE_HIGH_AGE = 35;

        // Abilities
        public const int OL_RUN_PASS_BLOCK_DELTA = 80;
        public const int QB_CHANCE_RUNNER = 10;

        public const int PRIMARY_ABILITY_LOW_RATING = 40;
        public const int PRIMARY_ABILITY_HIGH_RATING = 100;

        public const int SECONDARY_1_ABILITY_LOW_RATING = 30;
        public const int SECONDARY_1_ABILITY_HIGH_RATING = 90;

        public const int SECONDARY_2_ABILITY_LOW_RATING = 20;
        public const int SECONDARY_2_ABILITY_HIGH_RATING = 80;

        public const int SECONDARY_3_ABILITY_LOW_RATING = 10;
        public const int SECONDARY_3_ABILITY_HIGH_RATING = 70;

        public const int TERTIARY_ABILITY_LOW_RATING = 1;
        public const int TERTIARY_ABILITY_HIGH_RATING = 10;

        public const int NON_ATHLETIC_ABILITY_LOW = 1;
        public const int NON_ATHLETIC_ABILITY_HIGH = 100;

        // Teams
        public const string EMPTY_TEAM_SLOT = "Empty Team Slot";
        public const int TRAINING_CAMP_TEAM_PLAYER_COUNT = 86;
        public const int REGULAR_SEASON_TEAM_PLAYER_COUNT = 53;

        public const int MINCOLORDIFF = 80;

        //Training Camp
        public const int TRAINING_CAMP_NUM_PLAYS = 100;

        public const int TRAINING_CAMP_QB_PRESSURE_RATE = 33;
        public const int TRAINING_CAMP_QB_RECEIVER_OPEN = 50;
        public const int TRAINING_CAMP_QB_RECEIVER_CATCH_PERC = 85;
        public const int TRAINING_CAMP_QB_DEFENDER_INT_PERC = 5;
        public const int TRAINING_CAMP_QB_10YRD_PASS_PERC = 40;
        public const int TRAINING_CAMP_QB_20YRD_PASS_PERC = 65;
        public const int TRAINING_CAMP_QB_30YRD_PASS_PERC = 80;
        public const int TRAINING_CAMP_QB_40YRD_PASS_PERC = 95;
        public const double TRAINING_CAMP_QB_RECEIVE_AFTER_CATCH_MULTIPLYER = 1.5;
        public const int TRAINING_CAMP_QB_PRESSURE_REDUCER = 10;
        public const int TRAINING_CAMP_QB_LOOKS = 2;
        public const double TRAINING_CAMP_QB_COVERED_DIVIDER = 0.25;
        public const int TRAINING_CAMP_QB_FUDGE = 3;

        public const Double TRAINING_CAMP_QB_COMPLETION_AWARD = 1.2;
        public const Double TRAINING_CAMP_QB_TD_AWARD = 6.0;
        public const Double TRAINING_CAMP_QB_INT_AWARD = -6.0;

        public const int TRAINING_CAMP_RB_PERCENT_RECEIVER = 66;
        public const int TRAINING_CAMP_RB_ACCURACY_PERC = 80;
        public const int TRAINING_CAMP_RB_PASS_LEN = 10;
        public const int TRAINING_CAMP_RB_DELTA = 5;
        public const int TRAINING_CAMP_RB_POWER_BREAK_AWAY = 2;
        public const int TRAINING_CAMP_RB_AGILITY_BREAK_AWAY = 12;
        public const int TRAINING_CAMP_RB_STRIDE = 2;
        public const int TRAINING_CAMP_RB_POWER_BUMP = 100;
        public const int TRAINING_CAMP_RB_GAIN_FOR_POINT = 5;

        public const double TRAINING_CAMP_RB_RUNNING_LOSS = -1.0;
        public const double TRAINING_CAMP_RB_RUNNING_PLUS5 = 1.0;
        public const double TRAINING_CAMP_RB_TD = 6.0;

        public const int TRAINING_CAMP_RECEIVER_FUDGE = 2;
        public const int TRAINING_CAMP_RECEIVER_PASS_CAUGHT = 1;
        public const int TRAINING_CAMP_RECEIVER_AFTER_CATCH_FACTOR = 300;
        public const int TRAINING_CAMP_RECEIVER_OPEN_FUDGE = 240;
        public const int TRAINING_CAMP_RECEIVER_YAC = 5;

        public const int TRAINING_CAMP_TE_BLOCK_PERCENT = 50;
        public const int TRAINING_CAMP_TE_FUDGE = 5;

        public const Double TRAINING_CAMP_BLOCKER_GOOD_RUN_BLOCK = 1.2;
        public const Double TRAINING_CAMP_BLOCKER_GOOD_PASS_BLOCK = 1.2;

        public const Double TRAINING_CAMP_RUSHER_GOOD_RUN_BLOCK = 1.2;
        public const Double TRAINING_CAMP_RUSHER_GOOD_PASS_BLOCK = 1.2;

        public const int TRAINING_BLOCKER_RUN_PERCENT = 45;
        public const int TRAINING_BLOCK_FUDGE = 3;

        public const int TRAINING_CAMP_LB_BLOCK_PERCENT = 50;
        public const int TRAINING_CAMP_LB_FUDGE = 3;

        public const Double TRAINING_CAMP_COVER_RECEIVER = 1.2;
        public const Double TRAINING_CAMP_COVER_int = 1.0;
        public const double TRAINING_CAMP_COVER_SURRENDER_TD = -6.0;

        public const int TRAINING_CAMP_FG_FURTHEST_YRD = 50;
        public const int TRAINING_CAMP_FG_EXTRA_YEARDS = 15;
        public const int TRAINING_CAMP_FG_FUDGE = 3;

        public const Double TRAINING_CAMP_FG_AWARD = 1.2;

        public const int TRAINING_CAMP_P_COFFIN_YRD = 60;
        public const int TRAINING_CAMP_P_MIN_PUNT = 30;
        public const int TRAINING_CAMP_P_FUDGE = 5;
        public const int TRAINING_CAMP_P_YARD_INC = 5;

        public const double TRAINING_CAMP_P_COFFIN_CORNER_AWARD = 5.0;

        //Week values
        public const int PLAYER_RETIRING_WEEK = -100;
        public const int DRAFT_WEEK = -70;
        public const int FREE_AGENCY_WEEK = -30;
        public const int TRAINING_CAMP_WEEK = -15;

        public const int PLAYOFF_WIDLCARD_WEEK_1 = 1000;
        public const int PLAYOFF_WIDLCARD_WEEK_2 = 1100;
        public const int PLAYOFF_WIDLCARD_WEEK_3 = 1200;
        public const int PLAYOFF_WIDLCARD_WEEK_4 = 1300;
        public const int PLAYOFF_WIDLCARD_WEEK_5 = 1400;
        public const int PLAYOFF_WIDLCARD_WEEK_6 = 1500;
        public const int PLAYOFF_WIDLCARD_WEEK_7 = 1600;
        public const int PLAYOFF_WIDLCARD_WEEK_8 = 1700;
        public const int PLAYOFF_WIDLCARD_WEEK_9 = 1800;
        public const int PLAYOFF_WIDLCARD_WEEK_10 = 1900;
        public const int PLAYOFF_DIVISIONAL_WEEK = 2000;
        public const int PLAYOFF_CONFERENCE_WEEK = 3000;
        public const int PLAYOFF_CHAMPIONSHIP_WEEK = 9999;

        //Game
        public const int GAME_QUARTER_SECONDS = 900;
        public const int QTRS_IN_REGULATION = 4;

        //Stats
        public const double MAX_QBR = 158.3;

        //Roster
        public const double PLAYER_TO_WATCH_MIN = 87.0;


    }
}
