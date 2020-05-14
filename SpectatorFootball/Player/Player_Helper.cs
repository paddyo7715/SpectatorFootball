using System.Collections.Generic;
using System;
using SpectatorFootball.Models;
using SpectatorFootball.Enum;

namespace SpectatorFootball
{

    public class Player_Helper
    {
        public static Player CreatePlayer(Player_Pos pos, Boolean bNewLeage)
        {
            Player r = new Player();

            string[] PlayerName = null;
            int player_number = 0;

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

            int[] m = CreateHeightWeight(pos, ratings);
            r.Height = m[0];
            r.Weight = m[1];

            r.First_Name = PlayerName[0];
            r.Last_Name = PlayerName[1];

            r.Age = age;
            r.Pos = (int)pos;
            r.Jersey_Number = null;

            Administration_Services adms = new Administration_Services();
            string HomeTown = adms.getRandomHomeTown();

            r.HomeTown = HomeTown;

            r.Player_Ratings.Add(ratings);

            r.Franchise_ID = null;

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
                        weight = weight + weightAdjust(app_Constants.SECONDARY_1_ABILITY_LOW_RATING, app_Constants.SECONDARY_1_ABILITY_LOW_RATING, (int)pr.Pass_Block_Rating, false);
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
                        weight = weight + weightAdjust(app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING, (int)pr.Pass_Attack, false);
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
            int range = rating_hight - rating_hight;
            value = value - rating_hight;
            double percent = rating_hight / value;

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
            double height_range_percent = (float)height_range_value / height_range;

            int weight_range = high_weight - low_weight;
            int weight_middle_setting = low_weight + (int)((weight_range * height_range_percent) + .5);
            int half_WEIGHT_VARIANT = app_Constants.PLAYER_WEIGHT_VARIANT_PERCENT / 2;


            int actual_low = Math.Max(weight_middle_setting- half_WEIGHT_VARIANT, low_weight);
            int actual_high = Math.Min(weight_middle_setting + half_WEIGHT_VARIANT, high_weight);

            r = CommonUtils.getRandomNum(actual_low, actual_high);

            return r;
        }

        public int getPlayerNumber(Player_Pos pos)
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
        public bool isPlayerNumber_Unique_Team_Memoory(string number, List<Player> Players)
        {
            bool r = true;

            if (Players != null)
            {
                foreach (Player p in Players)
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
                        r.Decision_Making = CommonUtils.getRandomNum(app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING);
                        r.Arm_Strength_Rating = CommonUtils.getRandomNum(app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING);
                        r.Ball_Safety_Rating = CommonUtils.getRandomNum(app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING);

                        r.Agilty_Rating = CommonUtils.getRandomNum(app_Constants.SECONDARY_3_ABILITY_LOW_RATING, app_Constants.SECONDARY_3_ABILITY_HIGH_RATING);
                        r.Speed_Rating = CommonUtils.getRandomNum(app_Constants.SECONDARY_3_ABILITY_LOW_RATING, app_Constants.SECONDARY_3_ABILITY_HIGH_RATING);

                        r.Hands_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Kicker_Leg_Accuracy = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Kicker_Leg_Power = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Pass_Attack = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Pass_Block_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Running_Power_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Run_Attack = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
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
                        r.Decision_Making = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Arm_Strength_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Kicker_Leg_Accuracy = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Kicker_Leg_Power = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Pass_Attack = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Pass_Block_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Run_Attack = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
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
                        r.Decision_Making = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Arm_Strength_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Kicker_Leg_Accuracy = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Kicker_Leg_Power = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Pass_Attack = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Pass_Block_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Run_Attack = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
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
                        r.Decision_Making = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Arm_Strength_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Kicker_Leg_Accuracy = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Kicker_Leg_Power = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Pass_Attack = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Run_Attack = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);

 //                       r.OverAll = Convert.ToSingle(r.Fumble_Rating * app_Constants.TE_FUMBLE_PERCENT + (r.Speed_Rating + 100 - app_Constants.SECONDARY_1_ABILITY_HIGH_RATING) * app_Constants.TE_SPEED_PERCENT + r.Hands_Rating * app_Constants.TE_HANDS_PERCENT + (r.Agilty_Rating + 100 - app_Constants.SECONDARY_1_ABILITY_HIGH_RATING) * app_Constants.TE_AGILITY_PERCENT + (r.Pass_Block_Rating + 100 - app_Constants.SECONDARY_1_ABILITY_HIGH_RATING) * app_Constants.TE_PASS_BLOCK_PERCENT + (r.Run_Block_Rating + 100 - app_Constants.SECONDARY_1_ABILITY_HIGH_RATING) * app_Constants.TE_RUN_BLOCK_PERCENT);
                        break;
                    }

                case Player_Pos.OL:
                    {
                        r.Pass_Block_Rating = CommonUtils.getRandomNum(app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING);
                        r.Run_Block_Rating = CommonUtils.getRandomNum(Math.Max(app_Constants.PRIMARY_ABILITY_LOW_RATING, (int)r.Pass_Block_Rating - app_Constants.OL_RUN_PASS_BLOCK_DELTA), Math.Min(app_Constants.PRIMARY_ABILITY_HIGH_RATING, (int)r.Pass_Block_Rating + app_Constants.OL_RUN_PASS_BLOCK_DELTA));

                        r.Tackle_Rating = CommonUtils.getRandomNum(app_Constants.SECONDARY_1_ABILITY_LOW_RATING, app_Constants.SECONDARY_1_ABILITY_HIGH_RATING);
                        r.Speed_Rating = CommonUtils.getRandomNum(app_Constants.SECONDARY_3_ABILITY_LOW_RATING, app_Constants.SECONDARY_3_ABILITY_HIGH_RATING);
                        r.Agilty_Rating = CommonUtils.getRandomNum(app_Constants.SECONDARY_3_ABILITY_LOW_RATING, app_Constants.SECONDARY_3_ABILITY_HIGH_RATING);

                        r.Accuracy_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Decision_Making = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Arm_Strength_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Ball_Safety_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Hands_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Kicker_Leg_Accuracy = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Kicker_Leg_Power = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Pass_Attack = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Running_Power_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Run_Attack = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);

//                        r.OverAll = Convert.ToSingle(r.Pass_Block_Rating * app_Constants.OL_PASS_BLOCK_PERCENT + r.Run_Block_Rating * app_Constants.OL_RUN_BLOCK_PERCENT + (r.Agilty_Rating + 100 - app_Constants.SECONDARY_3_ABILITY_HIGH_RATING) * app_Constants.OL_AGILITY_PERCENT);
                        break;
                    }

                case Player_Pos.DL:
                    {
                        r.Pass_Attack = CommonUtils.getRandomNum(app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING);
                        r.Run_Attack = CommonUtils.getRandomNum(Math.Max(app_Constants.PRIMARY_ABILITY_LOW_RATING, (int)r.Pass_Attack - app_Constants.OL_RUN_PASS_BLOCK_DELTA), Math.Min(app_Constants.PRIMARY_ABILITY_HIGH_RATING, (int)r.Pass_Attack + app_Constants.OL_RUN_PASS_BLOCK_DELTA));
                        r.Tackle_Rating = CommonUtils.getRandomNum(app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING);

                        r.Speed_Rating = CommonUtils.getRandomNum(app_Constants.SECONDARY_3_ABILITY_LOW_RATING, app_Constants.SECONDARY_3_ABILITY_HIGH_RATING);
                        r.Agilty_Rating = CommonUtils.getRandomNum(app_Constants.SECONDARY_3_ABILITY_LOW_RATING, app_Constants.SECONDARY_3_ABILITY_HIGH_RATING);

                        r.Pass_Block_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Run_Block_Rating = CommonUtils.getRandomNum(Math.Max(app_Constants.TERTIARY_ABILITY_LOW_RATING, (int)r.Pass_Block_Rating - app_Constants.OL_RUN_PASS_BLOCK_DELTA), Math.Min(app_Constants.TERTIARY_ABILITY_HIGH_RATING, (int)r.Pass_Block_Rating + app_Constants.OL_RUN_PASS_BLOCK_DELTA));
                        r.Accuracy_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Decision_Making = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Arm_Strength_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Ball_Safety_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Hands_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Kicker_Leg_Accuracy = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Kicker_Leg_Power = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
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
                        r.Decision_Making = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Arm_Strength_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Ball_Safety_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Kicker_Leg_Accuracy = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Kicker_Leg_Power = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Pass_Attack = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Running_Power_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Run_Attack = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);

//                        r.OverAll = Convert.ToSingle(r.Speed_Rating * app_Constants.DB_SPEED_PERCENT + r.Hands_Rating * app_Constants.DB_HANDS_PERCENT + r.Tackle_Rating * app_Constants.DB_TACKLING_PERCENT + r.Agilty_Rating * app_Constants.DB_AGILITY_PERCENT);
                        break;
                    }

                case Player_Pos.LB:
                    {
                        r.Tackle_Rating = CommonUtils.getRandomNum(app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING);
                        r.Pass_Attack = CommonUtils.getRandomNum(app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING);
                        r.Run_Attack = CommonUtils.getRandomNum(Math.Max(app_Constants.PRIMARY_ABILITY_LOW_RATING, (int)r.Pass_Attack - app_Constants.OL_RUN_PASS_BLOCK_DELTA), Math.Min(app_Constants.PRIMARY_ABILITY_HIGH_RATING, (int)r.Pass_Attack + app_Constants.OL_RUN_PASS_BLOCK_DELTA));

                        r.Speed_Rating = CommonUtils.getRandomNum(app_Constants.SECONDARY_1_ABILITY_LOW_RATING, app_Constants.SECONDARY_1_ABILITY_HIGH_RATING);
                        r.Agilty_Rating = CommonUtils.getRandomNum(app_Constants.SECONDARY_1_ABILITY_LOW_RATING, app_Constants.SECONDARY_1_ABILITY_HIGH_RATING);
                        r.Hands_Rating = CommonUtils.getRandomNum(app_Constants.SECONDARY_2_ABILITY_LOW_RATING, app_Constants.SECONDARY_2_ABILITY_HIGH_RATING);

                        r.Pass_Block_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Run_Block_Rating = CommonUtils.getRandomNum(Math.Max(app_Constants.TERTIARY_ABILITY_LOW_RATING, (int)r.Pass_Block_Rating - app_Constants.OL_RUN_PASS_BLOCK_DELTA), Math.Min(app_Constants.TERTIARY_ABILITY_HIGH_RATING, (int)r.Pass_Block_Rating + app_Constants.OL_RUN_PASS_BLOCK_DELTA));
                        r.Accuracy_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Decision_Making = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Arm_Strength_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Ball_Safety_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Kicker_Leg_Accuracy = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Kicker_Leg_Power = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Running_Power_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);

//                        r.OverAll = Convert.ToSingle(r.Pass_Attack * app_Constants.LB_PASS_ATTACK_PERCENT + r.Run_Attack * app_Constants.LB_RUN_ATTACK_PERCENT + r.Tackle_Rating * app_Constants.LB_TACKLE_PERCENT + (r.Hands_Rating + 100 - app_Constants.SECONDARY_2_ABILITY_HIGH_RATING) * app_Constants.LB_HANDS_PERCENT + (r.Speed_Rating + 100 - app_Constants.SECONDARY_1_ABILITY_HIGH_RATING) * app_Constants.LB_SPEED_PERCENT + (r.Agilty_Rating + 100 - app_Constants.SECONDARY_1_ABILITY_HIGH_RATING) * app_Constants.LB_AGILITY_PERCENT);
                        break;
                    }

                case Player_Pos.K:
                case Player_Pos.P:
                    {
                        r.Kicker_Leg_Accuracy = CommonUtils.getRandomNum(app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING);
                        r.Kicker_Leg_Power = CommonUtils.getRandomNum(app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING);

                        r.Pass_Block_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Run_Block_Rating = CommonUtils.getRandomNum(Math.Max(app_Constants.TERTIARY_ABILITY_LOW_RATING, (int)r.Pass_Block_Rating - app_Constants.OL_RUN_PASS_BLOCK_DELTA), Math.Min(app_Constants.TERTIARY_ABILITY_HIGH_RATING, (int)r.Pass_Block_Rating + app_Constants.OL_RUN_PASS_BLOCK_DELTA));
                        r.Accuracy_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Decision_Making = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Arm_Strength_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Ball_Safety_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Agilty_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Hands_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Pass_Attack = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Running_Power_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Run_Attack = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Speed_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Run_Attack = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
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
 
    }
}
