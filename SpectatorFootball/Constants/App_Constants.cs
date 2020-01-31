using System.IO;
using System;
using System.Drawing;


namespace SpectatorFootball
{
    public class App_Constants
    {
        // Max Settings
        public const int MAX_DIVISIONS = 20;
        public const int MAX_TEAMS = 200;

        // Folders / Files
        public const int MIN_FREE_DISK_SPACE = 50;
        public const string GAME_DOC_FOLDER = "Spect_Football_Data";
        public const string LOG_FOLDER = "Logs";
        public const string BACKUP_FOLDER = "Backup";
        public const string LEAGUE_HELMETS_SUBFOLDER = "Helmet_Images";
        public const string LEAGUE_STADIUM_SUBFOLDER = "Stadium_Images";

        // The following three were intended to be constants, but when I added the file seperator variable, they could no longer be Consts
        public static string APP_HELMET_FOLDER = Path.DirectorySeparatorChar + "Images" + Path.DirectorySeparatorChar + "Helmets" + Path.DirectorySeparatorChar;
        public static string APP_STADIUM_FOLDER = Path.DirectorySeparatorChar + "Images" + Path.DirectorySeparatorChar + "Stadiums" + Path.DirectorySeparatorChar;
        public static string BLANK_DB_FOLDER = Path.DirectorySeparatorChar + "Database";

        public const string DB_FILE_EXT = "db";
        public const string BLANK_DB = "BlankDB.db";

        // Players
        public const int QB_PER_TEAM = 3;
        public const int RB_PER_TEAM = 4;
        public const int WR_PER_TEAM = 4;
        public const int TE_PER_TEAM = 2;
        public const int OL_PER_TEAM = 10;

        public const int DL_PER_TEAM = 8;
        public const int LB_PER_TEAM = 7;
        public const int DB_PER_TEAM = 8;

        public const int K_PER_TEAM = 1;
        public const int P_PER_TEAM = 1;

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

        // Player Ratings Percentages
        public const double QB_FUMBLE_PERCENT = 0.03;
        public const double QB_ARMSTRENGTH_PERCENT = 0.15;
        public const double QB_ACCURACY_RATING = 0.45;
        public const double QB_DESISION_RATING = 0.37;

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

        public const double OL_PASS_BLOCK_PERCENT = 0.28;
        public const double OL_RUN_BLOCK_PERCENT = 0.44;
        public const double OL_AGILITY_PERCENT = 0.28;

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
        public const int STARTING_ROOKIE_AGE = 20;
        public const int NEWLEAGE_LOW_AGE = 20;
        public const int NEWLEAGE_HIGH_AGE = 31;

        // Abilities
        public const int OL_RUN_PASS_BLOCK_DELTA = 5;

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

        // Teams
        public const string EMPTY_TEAM_SLOT = "Empty Team Slot";
    }
}
