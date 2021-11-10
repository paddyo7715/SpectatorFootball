using System.Collections.Generic;
using System;
using SpectatorFootball.Models;
using SpectatorFootball.Enum;
using log4net;
using SpectatorFootball.Common;

namespace SpectatorFootball.PlayerNS
{

    public class Player_Helper
    {
        private static ILog logger = LogManager.GetLogger("RollingFile");
        public static Player CreatePlayer(Player_Pos pos, Boolean bNewLeage,bool bCrappify,bool bDraftable)
        {
            Player r = new Player();

            string[] PlayerName = null;

            Player_NamesDAO pnDAO = new Player_NamesDAO();

            // Create a player name and only leave the while loop when the first name
            // does not equal the last name
            while (true)
            {
                PlayerName = pnDAO.CreatePlayerName();
                if (PlayerName[0].ToUpper() == PlayerName[1].ToUpper())
                    continue;

                break;
            }

            int age = generate_Player_Age(bNewLeage);

            Player_Ratings ratings = Create_Player_Ratings(pos);

            //if the player is younger than 28 then there is a chance that
            //their ratings should be lessened based on their age.
            if (age < app_Constants.PLAYER_ABILITY_PEAK_AGE)
            {
                int rnd = CommonUtils.getRandomNum(1, 100);

                if (rnd <= app_Constants.DRAFT_PERCENT_CRAPPIFY)
                {
                    int ageDelt = (int) Math.Round(((app_Constants.PLAYER_ABILITY_PEAK_AGE - age) * app_Constants.DRAFT_DELTA_MULTIPLYER));
                    CrappifyPlayerRatings(ratings, ageDelt);
                }
            }

            if (bCrappify)
                CrappifyPlayerRatings(ratings,20);

            int[] m = CreateHeightWeight(pos, ratings);
            r.Height = m[0];
            r.Weight = m[1];

            logger.Debug("Player Height and Weight Created");

            r.Handedness = getHandedness();

            r.First_Name = PlayerName[0];
            r.Last_Name = PlayerName[1];

            r.Age = age;
            r.Pos = (int)pos;
            
            Administration_Services adms = new Administration_Services();
            string HomeTown = adms.getRandomHomeTown();

            r.HomeTown = HomeTown;

            r.Player_Ratings.Add(ratings);

            //Create draft ratings that are used to create draft profile grade and scouting report.
            Player_Ratings draft_ratings = Draft_Profile.setDraftRatings(ratings);
            r.Draft_Grade = Player_Helper.Create_Overall_Rating(pos, draft_ratings);
            r.Draft_Profile = Draft_Profile.Create_Draft_Scoutingrpt(pos, draft_ratings);
            if (bDraftable)
                r.Eligible_for_Draft = 1;
            else
                r.Eligible_for_Draft = 0;

            logger.Debug("Finished");
            return r;
        }
        private static int[] CreateHeightWeight(Player_Pos pos, Player_Ratings pr)
        {
            int height=0;
            int weight=0;

            switch (pos)
            {
                case Player_Pos.QB:
                    {
                        height = CommonUtils.getRandomNum(app_Constants.QB_LOW_HEIGHT,app_Constants.QB_HIGH_HEIGHT);
                        weight = getPlayerWeight(app_Constants.QB_LOW_WEIGHT, app_Constants.QB_HIGH_WEIGHT,
                                    app_Constants.QB_LOW_HEIGHT, app_Constants.QB_HIGH_HEIGHT, height);
                        break;
                    }

                case Player_Pos.RB:
                    {
                        height = CommonUtils.getRandomNum(app_Constants.RB_LOW_HEIGHT, app_Constants.RB_HIGH_HEIGHT);
                        weight = getPlayerWeight(app_Constants.RB_LOW_WEIGHT, app_Constants.RB_HIGH_WEIGHT,
                                    app_Constants.RB_LOW_HEIGHT, app_Constants.RB_HIGH_HEIGHT, height);
                        weight = weight + weightAdjust(app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING,(int)pr.Speed_Rating, true);
                        weight = weight + weightAdjust(app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING, (int)pr.Running_Power_Rating, false);
                        break;
   
                    }

                case Player_Pos.WR:
                    {
                        height = CommonUtils.getRandomNum(app_Constants.WR_LOW_HEIGHT, app_Constants.WR_HIGH_HEIGHT);
                        weight = getPlayerWeight(app_Constants.WR_LOW_WEIGHT, app_Constants.WR_HIGH_WEIGHT,
                                    app_Constants.WR_LOW_HEIGHT, app_Constants.WR_HIGH_HEIGHT, height);
                        weight = weight + weightAdjust(app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING, (int)pr.Speed_Rating, true);
                        break;
                    }

                case Player_Pos.TE:
                    {
                        height = CommonUtils.getRandomNum(app_Constants.TE_LOW_HEIGHT, app_Constants.TE_HIGH_HEIGHT);
                        weight = getPlayerWeight(app_Constants.TE_LOW_WEIGHT, app_Constants.TE_HIGH_WEIGHT,
                                    app_Constants.TE_LOW_HEIGHT, app_Constants.TE_HIGH_HEIGHT, height);
                        weight = weight + weightAdjust(app_Constants.SECONDARY_1_ABILITY_LOW_RATING, app_Constants.SECONDARY_1_ABILITY_HIGH_RATING, (int)pr.Speed_Rating, true);
                        weight = weight + weightAdjust(app_Constants.SECONDARY_1_ABILITY_LOW_RATING, app_Constants.SECONDARY_1_ABILITY_HIGH_RATING, (int)pr.Pass_Block_Rating, false);
                        break;
                    }

                case Player_Pos.OL:
                    {
                        height = CommonUtils.getRandomNum(app_Constants.OL_LOW_HEIGHT, app_Constants.OL_HIGH_HEIGHT);
                        weight = getPlayerWeight(app_Constants.OL_LOW_WEIGHT, app_Constants.OL_HIGH_WEIGHT,
                                    app_Constants.OL_LOW_HEIGHT, app_Constants.OL_HIGH_HEIGHT, height);
                        weight = weight + weightAdjust(app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING, (int)pr.Pass_Block_Rating, false);
                        break;
                    }

                case Player_Pos.DL:
                    {
                        height = CommonUtils.getRandomNum(app_Constants.DL_LOW_HEIGHT, app_Constants.DL_HIGH_HEIGHT);
                        weight = getPlayerWeight(app_Constants.DL_LOW_WEIGHT, app_Constants.DL_HIGH_WEIGHT,
                                    app_Constants.DL_LOW_HEIGHT, app_Constants.DL_HIGH_HEIGHT, height);
                        weight = weight + weightAdjust(app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING, (int)pr.Pass_Attack_Rating, false);
                        break;
                    }

                case Player_Pos.LB:
                    {
                        height = CommonUtils.getRandomNum(app_Constants.LB_LOW_HEIGHT, app_Constants.LB_HIGH_HEIGHT);
                        weight = getPlayerWeight(app_Constants.LB_LOW_WEIGHT, app_Constants.LB_HIGH_WEIGHT,
                                    app_Constants.LB_LOW_HEIGHT, app_Constants.LB_HIGH_HEIGHT, height);
                        break;
                    }

                case Player_Pos.DB:
                    {
                        height = CommonUtils.getRandomNum(app_Constants.DB_LOW_HEIGHT, app_Constants.DB_HIGH_HEIGHT);
                        weight = getPlayerWeight(app_Constants.DB_LOW_WEIGHT, app_Constants.DB_HIGH_WEIGHT,
                                    app_Constants.DB_LOW_HEIGHT, app_Constants.DB_HIGH_HEIGHT, height);
                        weight = weight + weightAdjust(app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING, (int)pr.Speed_Rating, true);
                        break;
                    }

                case Player_Pos.K:
                    {
                        height = CommonUtils.getRandomNum(app_Constants.K_LOW_HEIGHT, app_Constants.K_HIGH_HEIGHT);
                        weight = getPlayerWeight(app_Constants.K_LOW_WEIGHT, app_Constants.K_HIGH_WEIGHT,
                                     app_Constants.K_LOW_HEIGHT, app_Constants.K_HIGH_HEIGHT, height);
                        break;
                    }

                case Player_Pos.P:
                    {
                        height = CommonUtils.getRandomNum(app_Constants.P_LOW_HEIGHT, app_Constants.P_HIGH_HEIGHT);
                        weight = getPlayerWeight(app_Constants.P_LOW_WEIGHT, app_Constants.P_HIGH_WEIGHT,
                        app_Constants.P_LOW_HEIGHT, app_Constants.P_HIGH_HEIGHT, height);

                        break;
                    }
            }

            return new int[] { height, weight};
        }
        private static int weightAdjust(int rating_low, int rating_hight, int value, Boolean flip)
        {
            int r;
            double range = rating_hight - rating_low;
            value = value - rating_low;
            logger.Debug("value = " + value);
            double percent = value / range;

            r = (int)((percent * 100) / 10);
            r = (r * app_Constants.WEIGHT_ADJUSTMENT_MULTIPLYER) - app_Constants.WEIGHT_ADJUSTMENT_HALF_RANGE;

            if (flip)
                r = r * -1;

            return r;
        }

        private static int getPlayerWeight(int low_weight, int high_weight,
                                    int low_height, int high_height,
                                    int height)
        {
            int r;
            int height_range = high_height - low_height;
            int height_range_value = height - low_height;
            logger.Debug("height_range = " + height_range);
            double height_range_percent = (float)height_range_value / height_range;

            int weight_range = high_weight - low_weight;
            int weight_middle_setting = low_weight + (int)((weight_range * height_range_percent) + .5);
            int half_WEIGHT_VARIANT = app_Constants.PLAYER_WEIGHT_VARIANT_PERCENT / 2;


            int actual_low = Math.Max(weight_middle_setting- half_WEIGHT_VARIANT, low_weight);
            int actual_high = Math.Min(weight_middle_setting + half_WEIGHT_VARIANT, high_weight);

            r = CommonUtils.getRandomNum(actual_low, actual_high);

            return r;
        }

        public static int getPlayerNumber(Player_Pos pos)
        {
            int r = default(int);

            switch (pos)
            {
                case Player_Pos.QB:
                    {
                        r = CommonUtils.getRandomNum(app_Constants.QBLOWNUM, app_Constants.QBHIGHNUM);
                        break;
                    }

                case Player_Pos.RB:
                    {
                        r = CommonUtils.getRandomNum(app_Constants.RBLOWNUM, app_Constants.RBHIGHNUM);
                        break;
                    }

                case Player_Pos.WR:
                    {
                        r = CommonUtils.getRandomNum(app_Constants.WRLOWNUM, app_Constants.WRHIGHNUM);
                        break;
                    }

                case Player_Pos.TE:
                    {
                        while (true)
                        {
                            r = CommonUtils.getRandomNum(app_Constants.TELOWNUM, app_Constants.TEHIGHNUM);
                            if (r >= 50 && r <= 79)
                                break;
                        }

                        break;
                    }

                case Player_Pos.OL:
                    {
                        r = CommonUtils.getRandomNum(app_Constants.OLLOWNUM, app_Constants.OLHIGHNUM);
                        break;
                    }

                case Player_Pos.DL:
                    {
                        while (true)
                        {
                            r = CommonUtils.getRandomNum(app_Constants.DLLOWNUM, app_Constants.DLHIGHNUM);
                            if (r < 80 || r > 89)
                                break;
                        }

                        break;
                    }

                case Player_Pos.LB:
                    {
                        r = CommonUtils.getRandomNum(app_Constants.LBLOWNUM, app_Constants.LBHIGHNUM);
                        break;
                    }

                case Player_Pos.DB:
                    {
                        r = CommonUtils.getRandomNum(app_Constants.DBLOWNUM, app_Constants.DBHIGHNUM);
                        break;
                    }

                case Player_Pos.K:
                case Player_Pos.P:
                    {
                        r = CommonUtils.getRandomNum(app_Constants.KLOWNUM, app_Constants.KHIGHNUM);
                        break;
                    }
            }

            return r;
        }
        private static string getHandedness()
        {
            string r = null;

            int t = CommonUtils.getRandomNum(1, 100);

            if (t > app_Constants.PERCENT_LEFTY)
                r = "R";
            else
                r = "L";

            return r;
        }
        public bool isPlayerNumber_Unique_Team_Memoory(string number, List<Players_By_Team> pbt)
        {
            bool r = true;

            if (pbt != null)
            {
                foreach (Players_By_Team p in pbt)
                {
                    if (p.Jersey_Number.ToString() == number.Trim())
                    {
                        r = false;
                        break;
                    }
                }
            }

            return r;
        }
        public static int generate_Player_Age(bool newleage)
        {
            int r = app_Constants.STARTING_ROOKIE_AGE;

            if (newleage)
                r = CommonUtils.getRandomNum(app_Constants.NEWLEAGE_LOW_AGE, app_Constants.NEWLEAGE_HIGH_AGE);

            return r;
        }

        public static Player_Ratings Create_Player_Ratings(Player_Pos pos)
        {
            Player_Ratings r = new Player_Ratings();

            switch (pos)
            {
                case Player_Pos.QB:
                    {
                        r.Accuracy_Rating = CommonUtils.getRandomNum(app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING);
                        r.Decision_Making_Rating = CommonUtils.getRandomNum(app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING);
                        r.Arm_Strength_Rating = CommonUtils.getRandomNum(app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING);
                        r.Ball_Safety_Rating = CommonUtils.getRandomNum(app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING);

                        int temprnd = CommonUtils.getRandomNum(1, 100);
                        if (temprnd <= app_Constants.QB_CHANCE_RUNNER)
                        {
                            r.Agilty_Rating = CommonUtils.getRandomNum(app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING);
                            r.Speed_Rating = CommonUtils.getRandomNum(app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING);
                        }
                        else
                        {
                            r.Agilty_Rating = CommonUtils.getRandomNum(app_Constants.SECONDARY_3_ABILITY_LOW_RATING, app_Constants.SECONDARY_3_ABILITY_HIGH_RATING);
                            r.Speed_Rating = CommonUtils.getRandomNum(app_Constants.SECONDARY_3_ABILITY_LOW_RATING, app_Constants.SECONDARY_3_ABILITY_HIGH_RATING);
                        }
                        r.Hands_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Kicker_Leg_Accuracy_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Kicker_Leg_Power_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Pass_Attack_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Pass_Block_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Running_Power_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Run_Attack_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Run_Block_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Tackle_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);

//                        r.OverAll = Convert.ToSingle(r.Fumble_Rating * app_Constants.QB_FUMBLE_PERCENT + r.Arm_Strength_Rating * app_Constants.QB_ARMSTRENGTH_PERCENT + r.Accuracy_Rating * app_Constants.QB_ACCURACY_RATING + r.Decision_Making * app_Constants.QB_DESISION_RATING);
                        break;
                    }

                case Player_Pos.RB:
                    {
                        r.Running_Power_Rating = CommonUtils.getRandomNum(app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING);
                        r.Speed_Rating = CommonUtils.getRandomNum(app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING);
                        r.Agilty_Rating = CommonUtils.getRandomNum(app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING);
                        r.Ball_Safety_Rating = CommonUtils.getRandomNum(app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING);

                        r.Hands_Rating = CommonUtils.getRandomNum(app_Constants.SECONDARY_1_ABILITY_LOW_RATING, app_Constants.SECONDARY_1_ABILITY_HIGH_RATING);
                        r.Tackle_Rating = CommonUtils.getRandomNum(app_Constants.SECONDARY_3_ABILITY_LOW_RATING, app_Constants.SECONDARY_3_ABILITY_HIGH_RATING);

                        r.Accuracy_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Decision_Making_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Arm_Strength_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Kicker_Leg_Accuracy_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Kicker_Leg_Power_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Pass_Attack_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Pass_Block_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Run_Attack_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Run_Block_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);

 //                       r.OverAll = Convert.ToSingle(r.Fumble_Rating * app_Constants.RB_FUMBLE_PERCENT + r.Running_Power_Rating * app_Constants.RB_RUNNING_PWER_PERCENT + r.Speed_Rating * app_Constants.RB_SPEED_PERCENT + (r.Hands_Rating + 100 - app_Constants.SECONDARY_1_ABILITY_HIGH_RATING) * app_Constants.RB_HANDS_PERCENT + r.Agilty_Rating * app_Constants.RB_AGILITY_PERCENT);
                        break;
                    }

                case Player_Pos.WR:
                    {
                        r.Speed_Rating = CommonUtils.getRandomNum(app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING);
                        r.Agilty_Rating = CommonUtils.getRandomNum(app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING);
                        r.Hands_Rating = CommonUtils.getRandomNum(app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING);
                        r.Ball_Safety_Rating = CommonUtils.getRandomNum(app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING);

                        r.Tackle_Rating = CommonUtils.getRandomNum(app_Constants.SECONDARY_2_ABILITY_LOW_RATING, app_Constants.SECONDARY_2_ABILITY_HIGH_RATING);

                        r.Running_Power_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Accuracy_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Decision_Making_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Arm_Strength_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Kicker_Leg_Accuracy_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Kicker_Leg_Power_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Pass_Attack_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Pass_Block_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Run_Attack_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Run_Block_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);

 //                       r.OverAll = Convert.ToSingle(r.Fumble_Rating * app_Constants.WR_FUMBLE_PERCENT + r.Speed_Rating * app_Constants.WR_SPEED_PERCENT + r.Hands_Rating * app_Constants.WR_HANDS_PERCENT + r.Agilty_Rating * app_Constants.WR_AGILITY_PERCENT);
                        break;
                    }

                case Player_Pos.TE:
                    {
                        r.Speed_Rating = CommonUtils.getRandomNum(app_Constants.SECONDARY_1_ABILITY_LOW_RATING, app_Constants.SECONDARY_1_ABILITY_HIGH_RATING);
                        r.Agilty_Rating = CommonUtils.getRandomNum(app_Constants.SECONDARY_1_ABILITY_LOW_RATING, app_Constants.SECONDARY_1_ABILITY_HIGH_RATING);
                        r.Hands_Rating = CommonUtils.getRandomNum(app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING);
                        r.Ball_Safety_Rating = CommonUtils.getRandomNum(app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING);
                        r.Pass_Block_Rating = CommonUtils.getRandomNum(app_Constants.SECONDARY_1_ABILITY_LOW_RATING, app_Constants.SECONDARY_1_ABILITY_HIGH_RATING);
                        r.Run_Block_Rating = CommonUtils.getRandomNum(app_Constants.SECONDARY_1_ABILITY_LOW_RATING, app_Constants.SECONDARY_1_ABILITY_HIGH_RATING);

                        r.Tackle_Rating = CommonUtils.getRandomNum(app_Constants.SECONDARY_1_ABILITY_LOW_RATING, app_Constants.SECONDARY_1_ABILITY_HIGH_RATING);

                        r.Running_Power_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Accuracy_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Decision_Making_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Arm_Strength_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Kicker_Leg_Accuracy_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Kicker_Leg_Power_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Pass_Attack_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Run_Attack_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);

 //                       r.OverAll = Convert.ToSingle(r.Fumble_Rating * app_Constants.TE_FUMBLE_PERCENT + (r.Speed_Rating + 100 - app_Constants.SECONDARY_1_ABILITY_HIGH_RATING) * app_Constants.TE_SPEED_PERCENT + r.Hands_Rating * app_Constants.TE_HANDS_PERCENT + (r.Agilty_Rating + 100 - app_Constants.SECONDARY_1_ABILITY_HIGH_RATING) * app_Constants.TE_AGILITY_PERCENT + (r.Pass_Block_Rating + 100 - app_Constants.SECONDARY_1_ABILITY_HIGH_RATING) * app_Constants.TE_PASS_BLOCK_PERCENT + (r.Run_Block_Rating + 100 - app_Constants.SECONDARY_1_ABILITY_HIGH_RATING) * app_Constants.TE_RUN_BLOCK_PERCENT);
                        break;
                    }

                case Player_Pos.OL:
                    {
                        r.Pass_Block_Rating = CommonUtils.getRandomNum(app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING);
                        r.Run_Block_Rating = CommonUtils.getRandomNum(app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING);

                        r.Tackle_Rating = CommonUtils.getRandomNum(app_Constants.SECONDARY_1_ABILITY_LOW_RATING, app_Constants.SECONDARY_1_ABILITY_HIGH_RATING);
                        r.Speed_Rating = CommonUtils.getRandomNum(app_Constants.SECONDARY_3_ABILITY_LOW_RATING, app_Constants.SECONDARY_3_ABILITY_HIGH_RATING);
                        r.Agilty_Rating = CommonUtils.getRandomNum(app_Constants.SECONDARY_3_ABILITY_LOW_RATING, app_Constants.SECONDARY_3_ABILITY_HIGH_RATING);

                        r.Accuracy_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Decision_Making_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Arm_Strength_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Ball_Safety_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Hands_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Kicker_Leg_Accuracy_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Kicker_Leg_Power_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Pass_Attack_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Running_Power_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Run_Attack_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);

//                        r.OverAll = Convert.ToSingle(r.Pass_Block_Rating * app_Constants.OL_PASS_BLOCK_PERCENT + r.Run_Block_Rating * app_Constants.OL_RUN_BLOCK_PERCENT + (r.Agilty_Rating + 100 - app_Constants.SECONDARY_3_ABILITY_HIGH_RATING) * app_Constants.OL_AGILITY_PERCENT);
                        break;
                    }

                case Player_Pos.DL:
                    {
                        r.Pass_Attack_Rating = CommonUtils.getRandomNum(app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING);
                        r.Run_Attack_Rating = CommonUtils.getRandomNum(Math.Max(app_Constants.PRIMARY_ABILITY_LOW_RATING, (int)r.Pass_Attack_Rating - app_Constants.OL_RUN_PASS_BLOCK_DELTA), Math.Min(app_Constants.PRIMARY_ABILITY_HIGH_RATING, (int)r.Pass_Attack_Rating + app_Constants.OL_RUN_PASS_BLOCK_DELTA));
                        r.Tackle_Rating = CommonUtils.getRandomNum(app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING);

                        r.Speed_Rating = CommonUtils.getRandomNum(app_Constants.SECONDARY_3_ABILITY_LOW_RATING, app_Constants.SECONDARY_3_ABILITY_HIGH_RATING);
                        r.Agilty_Rating = CommonUtils.getRandomNum(app_Constants.SECONDARY_3_ABILITY_LOW_RATING, app_Constants.SECONDARY_3_ABILITY_HIGH_RATING);

                        r.Pass_Block_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Run_Block_Rating = CommonUtils.getRandomNum(Math.Max(app_Constants.TERTIARY_ABILITY_LOW_RATING, (int)r.Pass_Block_Rating - app_Constants.OL_RUN_PASS_BLOCK_DELTA), Math.Min(app_Constants.TERTIARY_ABILITY_HIGH_RATING, (int)r.Pass_Block_Rating + app_Constants.OL_RUN_PASS_BLOCK_DELTA));
                        r.Accuracy_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Decision_Making_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Arm_Strength_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Ball_Safety_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Hands_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Kicker_Leg_Accuracy_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Kicker_Leg_Power_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Running_Power_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);

//                        r.OverAll = Convert.ToSingle(r.Pass_Attack * app_Constants.DL_PASS_ATTACK_PERCENT + r.Run_Attack * app_Constants.DL_RUN_ATTACK_PERCENT + r.Tackle_Rating * app_Constants.DL_TACKLE_PERCENT + (r.Agilty_Rating + 100 - app_Constants.SECONDARY_3_ABILITY_HIGH_RATING) * app_Constants.DL_AGILITY_PERCENT + (r.Speed_Rating + 100 - app_Constants.SECONDARY_3_ABILITY_HIGH_RATING) * app_Constants.DL_SPEED_PERCENT);
                        break;
                    }

                case Player_Pos.DB:
                    {
                        r.Speed_Rating = CommonUtils.getRandomNum(app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING);
                        r.Hands_Rating = CommonUtils.getRandomNum(app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING);
                        r.Tackle_Rating = CommonUtils.getRandomNum(app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING);
                        r.Agilty_Rating = CommonUtils.getRandomNum(app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING);

                        r.Pass_Block_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Run_Block_Rating = CommonUtils.getRandomNum(Math.Max(app_Constants.TERTIARY_ABILITY_LOW_RATING, (int)r.Pass_Block_Rating - app_Constants.OL_RUN_PASS_BLOCK_DELTA), Math.Min(app_Constants.TERTIARY_ABILITY_HIGH_RATING, (int)r.Pass_Block_Rating + app_Constants.OL_RUN_PASS_BLOCK_DELTA));
                        r.Accuracy_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Decision_Making_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Arm_Strength_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Ball_Safety_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Kicker_Leg_Accuracy_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Kicker_Leg_Power_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Pass_Attack_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Running_Power_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Run_Attack_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);

//                        r.OverAll = Convert.ToSingle(r.Speed_Rating * app_Constants.DB_SPEED_PERCENT + r.Hands_Rating * app_Constants.DB_HANDS_PERCENT + r.Tackle_Rating * app_Constants.DB_TACKLING_PERCENT + r.Agilty_Rating * app_Constants.DB_AGILITY_PERCENT);
                        break;
                    }

                case Player_Pos.LB:
                    {
                        r.Tackle_Rating = CommonUtils.getRandomNum(app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING);
                        r.Pass_Attack_Rating = CommonUtils.getRandomNum(app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING);
                        r.Run_Attack_Rating = CommonUtils.getRandomNum(Math.Max(app_Constants.PRIMARY_ABILITY_LOW_RATING, (int)r.Pass_Attack_Rating - app_Constants.OL_RUN_PASS_BLOCK_DELTA), Math.Min(app_Constants.PRIMARY_ABILITY_HIGH_RATING, (int)r.Pass_Attack_Rating + app_Constants.OL_RUN_PASS_BLOCK_DELTA));

                        r.Speed_Rating = CommonUtils.getRandomNum(app_Constants.SECONDARY_1_ABILITY_LOW_RATING, app_Constants.SECONDARY_1_ABILITY_HIGH_RATING);
                        r.Agilty_Rating = CommonUtils.getRandomNum(app_Constants.SECONDARY_1_ABILITY_LOW_RATING, app_Constants.SECONDARY_1_ABILITY_HIGH_RATING);
                        r.Hands_Rating = CommonUtils.getRandomNum(app_Constants.SECONDARY_2_ABILITY_LOW_RATING, app_Constants.SECONDARY_2_ABILITY_HIGH_RATING);

                        r.Pass_Block_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Run_Block_Rating = CommonUtils.getRandomNum(Math.Max(app_Constants.TERTIARY_ABILITY_LOW_RATING, (int)r.Pass_Block_Rating - app_Constants.OL_RUN_PASS_BLOCK_DELTA), Math.Min(app_Constants.TERTIARY_ABILITY_HIGH_RATING, (int)r.Pass_Block_Rating + app_Constants.OL_RUN_PASS_BLOCK_DELTA));
                        r.Accuracy_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Decision_Making_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Arm_Strength_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Ball_Safety_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Kicker_Leg_Accuracy_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Kicker_Leg_Power_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Running_Power_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);

//                        r.OverAll = Convert.ToSingle(r.Pass_Attack * app_Constants.LB_PASS_ATTACK_PERCENT + r.Run_Attack * app_Constants.LB_RUN_ATTACK_PERCENT + r.Tackle_Rating * app_Constants.LB_TACKLE_PERCENT + (r.Hands_Rating + 100 - app_Constants.SECONDARY_2_ABILITY_HIGH_RATING) * app_Constants.LB_HANDS_PERCENT + (r.Speed_Rating + 100 - app_Constants.SECONDARY_1_ABILITY_HIGH_RATING) * app_Constants.LB_SPEED_PERCENT + (r.Agilty_Rating + 100 - app_Constants.SECONDARY_1_ABILITY_HIGH_RATING) * app_Constants.LB_AGILITY_PERCENT);
                        break;
                    }

                case Player_Pos.K:
                case Player_Pos.P:
                    {
                        r.Kicker_Leg_Accuracy_Rating = CommonUtils.getRandomNum(app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING);
                        r.Kicker_Leg_Power_Rating = CommonUtils.getRandomNum(app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING);

                        r.Pass_Block_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Run_Block_Rating = CommonUtils.getRandomNum(Math.Max(app_Constants.TERTIARY_ABILITY_LOW_RATING, (int)r.Pass_Block_Rating - app_Constants.OL_RUN_PASS_BLOCK_DELTA), Math.Min(app_Constants.TERTIARY_ABILITY_HIGH_RATING, (int)r.Pass_Block_Rating + app_Constants.OL_RUN_PASS_BLOCK_DELTA));
                        r.Accuracy_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Decision_Making_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Arm_Strength_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Ball_Safety_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Agilty_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Hands_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Pass_Attack_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Running_Power_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Run_Attack_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Speed_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Run_Attack_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Tackle_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);

//                        r.OverAll = Convert.ToSingle(r.Kicking_Accuracy * app_Constants.K_KICK_ACC + r.Leg_Strength * app_Constants.K_LEG_STRENGTH);
                        break;
                    }
            }

            r.Work_Ethic_Ratings = CommonUtils.getRandomNum(app_Constants.NON_ATHLETIC_ABILITY_LOW, app_Constants.NON_ATHLETIC_ABILITY_HIGH);
            r.Toughness_Ratings = CommonUtils.getRandomNum(app_Constants.NON_ATHLETIC_ABILITY_LOW, app_Constants.NON_ATHLETIC_ABILITY_HIGH);
            r.Sportsmanship_Ratings = CommonUtils.getRandomNum(app_Constants.NON_ATHLETIC_ABILITY_LOW, app_Constants.NON_ATHLETIC_ABILITY_HIGH);

            return r;
        }
        public static double Create_Overall_Rating(Player_Pos pos, Player_Ratings pr)
        {

            double OverAll = 0.0;
            switch (pos)
            {
                case Player_Pos.QB:
                    {

                        OverAll = Convert.ToSingle(pr.Ball_Safety_Rating * app_Constants.QB_FUMBLE_PERCENT + pr.Arm_Strength_Rating * app_Constants.QB_ARMSTRENGTH_PERCENT + pr.Accuracy_Rating * app_Constants.QB_ACCURACY_RATING + pr.Decision_Making_Rating * app_Constants.QB_DESISION_RATING +
                            pr.Agilty_Rating * app_Constants.QB_AGILITY_RATING + pr.Speed_Rating * app_Constants.QB_SPEED_RATING);
                        break;
                    }

                case Player_Pos.RB:
                    {

                        OverAll = Convert.ToSingle(pr.Ball_Safety_Rating * app_Constants.RB_FUMBLE_PERCENT + pr.Running_Power_Rating * app_Constants.RB_RUNNING_PWER_PERCENT + pr.Speed_Rating * app_Constants.RB_SPEED_PERCENT + (pr.Hands_Rating + 100 - app_Constants.SECONDARY_1_ABILITY_HIGH_RATING) * app_Constants.RB_HANDS_PERCENT + pr.Agilty_Rating * app_Constants.RB_AGILITY_PERCENT);
                        break;
                    }

                case Player_Pos.WR:
                    {

                        OverAll = Convert.ToSingle(pr.Ball_Safety_Rating * app_Constants.WR_FUMBLE_PERCENT + pr.Speed_Rating * app_Constants.WR_SPEED_PERCENT + pr.Hands_Rating * app_Constants.WR_HANDS_PERCENT + pr.Agilty_Rating * app_Constants.WR_AGILITY_PERCENT);
                        break;
                    }

                case Player_Pos.TE:
                    {

                        OverAll = Convert.ToSingle(pr.Ball_Safety_Rating * app_Constants.TE_FUMBLE_PERCENT + (pr.Speed_Rating + 100 - app_Constants.SECONDARY_1_ABILITY_HIGH_RATING) * app_Constants.TE_SPEED_PERCENT + pr.Hands_Rating * app_Constants.TE_HANDS_PERCENT + (pr.Agilty_Rating + 100 - app_Constants.SECONDARY_1_ABILITY_HIGH_RATING) * app_Constants.TE_AGILITY_PERCENT + (pr.Pass_Block_Rating + 100 - app_Constants.SECONDARY_1_ABILITY_HIGH_RATING) * app_Constants.TE_PASS_BLOCK_PERCENT + (pr.Run_Block_Rating + 100 - app_Constants.SECONDARY_1_ABILITY_HIGH_RATING) * app_Constants.TE_RUN_BLOCK_PERCENT);
                        break;
                    }

                case Player_Pos.OL:
                    {

                        OverAll = Convert.ToSingle(pr.Pass_Block_Rating * app_Constants.OL_PASS_BLOCK_PERCENT + pr.Run_Block_Rating * app_Constants.OL_RUN_BLOCK_PERCENT + (pr.Agilty_Rating + 100 - app_Constants.SECONDARY_3_ABILITY_HIGH_RATING) * app_Constants.OL_AGILITY_PERCENT);
                        break;
                    }

                case Player_Pos.DL:
                    {

                        OverAll = Convert.ToSingle(pr.Pass_Attack_Rating * app_Constants.DL_PASS_ATTACK_PERCENT + pr.Run_Attack_Rating * app_Constants.DL_RUN_ATTACK_PERCENT + pr.Tackle_Rating * app_Constants.DL_TACKLE_PERCENT + (pr.Agilty_Rating + 100 - app_Constants.SECONDARY_3_ABILITY_HIGH_RATING) * app_Constants.DL_AGILITY_PERCENT + (pr.Speed_Rating + 100 - app_Constants.SECONDARY_3_ABILITY_HIGH_RATING) * app_Constants.DL_SPEED_PERCENT);
                        break;
                    }

                case Player_Pos.DB:
                    {

                        OverAll = Convert.ToSingle(pr.Speed_Rating * app_Constants.DB_SPEED_PERCENT + pr.Hands_Rating * app_Constants.DB_HANDS_PERCENT + pr.Tackle_Rating * app_Constants.DB_TACKLING_PERCENT + pr.Agilty_Rating * app_Constants.DB_AGILITY_PERCENT);
                        break;
                    }

                case Player_Pos.LB:
                    {

                        OverAll = Convert.ToSingle(pr.Pass_Attack_Rating * app_Constants.LB_PASS_ATTACK_PERCENT + pr.Run_Attack_Rating * app_Constants.LB_RUN_ATTACK_PERCENT + pr.Tackle_Rating * app_Constants.LB_TACKLE_PERCENT + (pr.Hands_Rating + 100 - app_Constants.SECONDARY_2_ABILITY_HIGH_RATING) * app_Constants.LB_HANDS_PERCENT + (pr.Speed_Rating + 100 - app_Constants.SECONDARY_1_ABILITY_HIGH_RATING) * app_Constants.LB_SPEED_PERCENT + (pr.Agilty_Rating + 100 - app_Constants.SECONDARY_1_ABILITY_HIGH_RATING) * app_Constants.LB_AGILITY_PERCENT);
                        break;
                    }

                case Player_Pos.K:
                case Player_Pos.P:
                    {

                        OverAll = Convert.ToSingle(pr.Kicker_Leg_Accuracy_Rating * app_Constants.K_KICK_ACC + pr.Kicker_Leg_Power_Rating * app_Constants.K_LEG_STRENGTH);
                        break;
                    }
            }

            return OverAll;
        }
        public static void CrappifyPlayerRatings(Player_Ratings pr, int reducePercent)
        {
            double mfactor = (double)((100.0 - reducePercent) / 100.0);

            var classProperties = pr.GetType().GetProperties();
            foreach (var cp in classProperties)
            {
                if (cp.Name == "Work_Ethic_Ratings" ||
                    cp.Name == "Sportsmanship_Ratings" ||
                    cp.Name == "Toughness_Ratings")
                    continue;

                if (cp.Name.IndexOf("_Rating") != -1)
                {
                    //                    long x = (long)((long)cp.GetValue(pr) * .8);
                    long x = (long)((long)cp.GetValue(pr) * mfactor);
                    cp.SetValue(pr,x);
                }
            }

            return;
        }
        public static bool isOffense(long iPos)
        {

            bool r = false;
            Player_Pos pp = (Player_Pos)iPos;

            switch (pp)
            {
                case Player_Pos.QB:
                case Player_Pos.RB:
                case Player_Pos.WR:
                case Player_Pos.TE:
                case Player_Pos.OL:
                case Player_Pos.K:
                case Player_Pos.P:
                    {
                        r = true;
                        break;
                    }
            }

            return r;
        }
        public static string FormatCompPercent(long comp, long att)
        {
            string r = null;
            if (comp > 0)
            {
                double tmp = ((double)att / (double)comp) * 100.0;
                r = tmp.ToString("0.0");
            }
            else
                r = "0.0";

            return r;
        }
        public static string CalculateQBR(long comp, long att, long yards, long TDs, long ints)
        {
            string r = null;
            double dTemp = 0.0;

            if (att > 0)
            {
                double cat1 = ((double)comp / (double)att - 0.3) * 5.0;
                double cat2 = ((double)yards / (double)att - 3.0) * 0.25;
                double cat3 = ((double)TDs / (double)att) * 20.0;
                double cat4 = 2.375 -  ((double)ints / (double)att) * 25.0;

                cat1 = Math.Max(cat1, 0.0);
                cat1 = Math.Min(cat1, 2.375);

                cat2 = Math.Max(cat2, 0.0);
                cat2 = Math.Min(cat2, 2.375);

                cat3 = Math.Max(cat3, 0.0);
                cat3 = Math.Min(cat3, 2.375);

                cat4 = Math.Max(cat4, 0.0);
                cat4 = Math.Min(cat4, 2.375);

                dTemp = ((cat1 + cat2 + cat3 + cat4) / 6) * 100; 
            }

            if (dTemp > app_Constants.MAX_QBR)
                dTemp = app_Constants.MAX_QBR;

            r = dTemp.ToString("0.0");
            return r;
        }
        public static string CalcYardsPerCarry_or_Catch(long count, long Yards)
        {
            string r = null;
            if (count > 0)
            {
                double tmp = ((double)Yards / (double)count);
                r = tmp.ToString("0.0");
            }
            else
                r = "0.0";

            return r;
        }
        public static List<string> getRelaventRatingsforPos(Player_Pos pos)
        {
            List<string> r = new List<string>();

            switch (pos)
            {
                case Player_Pos.QB:
                    r.Add("Accuracy_Rating");
                    r.Add("Decision_Making_Rating");
                    r.Add("Arm_Strength_Rating");
                    r.Add("Speed_Rating");
                    r.Add("Agilty_Rating");
                    r.Add("Ball_Safety_Rating");
                    r.Add("Work_Ethic_Ratings");
                    break;
                case Player_Pos.RB:
                    r.Add("Running_Power_Rating");
                    r.Add("Speed_Rating");
                    r.Add("Agilty_Rating");
                    r.Add("Hands_Rating");
                    r.Add("Ball_Safety_Rating");
                    r.Add("Work_Ethic_Ratings");
                    break;
                case Player_Pos.WR:
                    r.Add("Speed_Rating");
                    r.Add("Agilty_Rating");
                    r.Add("Hands_Rating");
                    r.Add("Ball_Safety_Rating");
                    r.Add("Work_Ethic_Ratings");
                    break;
                case Player_Pos.TE:
                    r.Add("Speed_Rating");
                    r.Add("Agilty_Rating");
                    r.Add("Hands_Rating");
                    r.Add("Pass_Block_Rating");
                    r.Add("Run_Block_Rating");
                    r.Add("Ball_Safety_Rating");
                    r.Add("Work_Ethic_Ratings");
                    break;
                case Player_Pos.OL:
                    r.Add("Pass_Block_Rating");
                    r.Add("Run_Block_Rating");
                    r.Add("Agilty_Rating");
                    r.Add("Work_Ethic_Ratings");
                    break;
                case Player_Pos.DL:
                    r.Add("Pass_Attack_Rating");
                    r.Add("Run_Attack_Rating");
                    r.Add("Speed_Rating");
                    r.Add("Agilty_Rating");
                    r.Add("Tackle_Rating");
                    r.Add("Work_Ethic_Ratings");
                    break;
                case Player_Pos.LB:
                    r.Add("Pass_Attack_Rating");
                    r.Add("Run_Attack_Rating");
                    r.Add("Speed_Rating");
                    r.Add("Agilty_Rating");
                    r.Add("Tackle_Rating");
                    r.Add("Hands_Rating");
                    r.Add("Work_Ethic_Ratings");
                    break;               
                 case Player_Pos.DB:
                    r.Add("Speed_Rating");
                    r.Add("Agilty_Rating");
                    r.Add("Tackle_Rating");
                    r.Add("Hands_Rating");
                    r.Add("Work_Ethic_Ratings");
                    break;
                case Player_Pos.P:
                case Player_Pos.K:
                    r.Add("Kicker_Leg_Power_Rating");
                    r.Add("Kicker_Leg_Accuracy_Rating");
                    r.Add("Work_Ethic_Ratings");
                    break;
            }

            return r;
        }
        public static string ChangeRatingtoDisplayRating(string s)
        {
            string r = null;

            switch (s)
            {
                case "Accuracy_Rating":
                    s = "Passing_Accuracy_Rating";
                    break;
               case "Pass_Attack_Rating":
                    s = "Pass_Rush_Rating";
                    break;
                case "Run_Attack_Rating":
                    s = "Pass_Stop_Rating";
                    break;
                case "Kicker_Leg_Power_Rating":
                    s = "Leg_Power_Rating";
                    break;
                case "Kicker_Leg_Accuracy_Rating":
                    s = "Leg_Accuracy_Rating";
                    break;
            }

            r = s.Replace("_", " ");
            return r;
        }
        public static long getRatingValue(Player_Ratings pr, string s)
        {
            long r = 0;

            r = (long)pr.GetType().GetProperty(s)?.GetValue(pr, null);

            return r;
        }

        public static List<Long_and_Long> getRelaventRatingValesforPos(Player_Pos pos, ICollection<Player_Ratings> ratings)
        {
            List<Long_and_Long> r = new List<Long_and_Long>();

            foreach (Player_Ratings pr in ratings)
            {
                switch (pos)
                {
                    case Player_Pos.QB:
                        r.Add(new Long_and_Long() { l1 = pr.Season.Year,  d1 = pr.Accuracy_Rating });
                        r.Add(new Long_and_Long() { l1 = pr.Season.Year, d1 = pr.Decision_Making_Rating });
                        r.Add(new Long_and_Long() { l1 = pr.Season.Year, d1 = pr.Arm_Strength_Rating });
                        r.Add(new Long_and_Long() { l1 = pr.Season.Year, d1 = pr.Speed_Rating });
                        r.Add(new Long_and_Long() { l1 = pr.Season.Year, d1 = pr.Agilty_Rating });
                        r.Add(new Long_and_Long() { l1 = pr.Season.Year, d1 = pr.Ball_Safety_Rating });
                        r.Add(new Long_and_Long() { l1 = pr.Season.Year, d1 = pr.Work_Ethic_Ratings });
                        break;
                    case Player_Pos.RB:
                        r.Add(new Long_and_Long() { l1 = pr.Season.Year, d1 = pr.Running_Power_Rating });
                        r.Add(new Long_and_Long() { l1 = pr.Season.Year, d1 = pr.Speed_Rating });
                        r.Add(new Long_and_Long() { l1 = pr.Season.Year, d1 = pr.Agilty_Rating });
                        r.Add(new Long_and_Long() { l1 = pr.Season.Year, d1 = pr.Hands_Rating });
                        r.Add(new Long_and_Long() { l1 = pr.Season.Year, d1 = pr.Ball_Safety_Rating });
                        r.Add(new Long_and_Long() { l1 = pr.Season.Year, d1 = pr.Work_Ethic_Ratings });
                        break;
                    case Player_Pos.WR:
                        r.Add(new Long_and_Long() { l1 = pr.Season.Year, d1 = pr.Speed_Rating });
                        r.Add(new Long_and_Long() { l1 = pr.Season.Year, d1 = pr.Agilty_Rating });
                        r.Add(new Long_and_Long() { l1 = pr.Season.Year, d1 = pr.Hands_Rating });
                        r.Add(new Long_and_Long() { l1 = pr.Season.Year, d1 = pr.Ball_Safety_Rating });
                        r.Add(new Long_and_Long() { l1 = pr.Season.Year, d1 = pr.Work_Ethic_Ratings });
                        break;
                    case Player_Pos.TE:
                        r.Add(new Long_and_Long() { l1 = pr.Season.Year, d1 = pr.Speed_Rating });
                        r.Add(new Long_and_Long() { l1 = pr.Season.Year, d1 = pr.Agilty_Rating });
                        r.Add(new Long_and_Long() { l1 = pr.Season.Year, d1 = pr.Hands_Rating });
                        r.Add(new Long_and_Long() { l1 = pr.Season.Year, d1 = pr.Pass_Block_Rating });
                        r.Add(new Long_and_Long() { l1 = pr.Season.Year, d1 = pr.Run_Block_Rating });
                        r.Add(new Long_and_Long() { l1 = pr.Season.Year, d1 = pr.Ball_Safety_Rating });
                        r.Add(new Long_and_Long() { l1 = pr.Season.Year, d1 = pr.Work_Ethic_Ratings });
                        break;
                    case Player_Pos.OL:
                        r.Add(new Long_and_Long() { l1 = pr.Season.Year, d1 = pr.Pass_Block_Rating });
                        r.Add(new Long_and_Long() { l1 = pr.Season.Year, d1 = pr.Run_Block_Rating });
                        r.Add(new Long_and_Long() { l1 = pr.Season.Year, d1 = pr.Agilty_Rating });
                        r.Add(new Long_and_Long() { l1 = pr.Season.Year, d1 = pr.Work_Ethic_Ratings });
                        break;
                    case Player_Pos.DL:
                        r.Add(new Long_and_Long() { l1 = pr.Season.Year, d1 = pr.Pass_Attack_Rating });
                        r.Add(new Long_and_Long() { l1 = pr.Season.Year, d1 = pr.Run_Attack_Rating });
                        r.Add(new Long_and_Long() { l1 = pr.Season.Year, d1 = pr.Speed_Rating });
                        r.Add(new Long_and_Long() { l1 = pr.Season.Year, d1 = pr.Agilty_Rating });
                        r.Add(new Long_and_Long() { l1 = pr.Season.Year, d1 = pr.Tackle_Rating });
                        r.Add(new Long_and_Long() { l1 = pr.Season.Year, d1 = pr.Work_Ethic_Ratings });
                        break;
                    case Player_Pos.LB:
                        r.Add(new Long_and_Long() { l1 = pr.Season.Year, d1 = pr.Pass_Attack_Rating });
                        r.Add(new Long_and_Long() { l1 = pr.Season.Year, d1 = pr.Run_Attack_Rating });
                        r.Add(new Long_and_Long() { l1 = pr.Season.Year, d1 = pr.Speed_Rating });
                        r.Add(new Long_and_Long() { l1 = pr.Season.Year, d1 = pr.Agilty_Rating });
                        r.Add(new Long_and_Long() { l1 = pr.Season.Year, d1 = pr.Tackle_Rating });
                        r.Add(new Long_and_Long() { l1 = pr.Season.Year, d1 = pr.Hands_Rating });
                        r.Add(new Long_and_Long() { l1 = pr.Season.Year, d1 = pr.Work_Ethic_Ratings });
                        break;
                    case Player_Pos.DB:
                        r.Add(new Long_and_Long() { l1 = pr.Season.Year, d1 = pr.Speed_Rating });
                        r.Add(new Long_and_Long() { l1 = pr.Season.Year, d1 = pr.Agilty_Rating });
                        r.Add(new Long_and_Long() { l1 = pr.Season.Year, d1 = pr.Tackle_Rating });
                        r.Add(new Long_and_Long() { l1 = pr.Season.Year, d1 = pr.Hands_Rating });
                        r.Add(new Long_and_Long() { l1 = pr.Season.Year, d1 = pr.Work_Ethic_Ratings });
                        break;
                    case Player_Pos.P:
                    case Player_Pos.K:
                        r.Add(new Long_and_Long() { l1 = pr.Season.Year, d1 = pr.Kicker_Leg_Power_Rating });
                        r.Add(new Long_and_Long() { l1 = pr.Season.Year, d1 = pr.Kicker_Leg_Accuracy_Rating });
                        r.Add(new Long_and_Long() { l1 = pr.Season.Year, d1 = pr.Work_Ethic_Ratings });
                        break;
                }
            }

            return r;
        }
    }
}
