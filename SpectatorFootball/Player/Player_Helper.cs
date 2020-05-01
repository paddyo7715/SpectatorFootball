using System.Collections.Generic;
using System;
using SpectatorFootball.Models;
using SpectatorFootball.Enum;

namespace SpectatorFootball
{

    public class Player_Helper
    {
        public static Player CreatePlayer(Player_Pos pos, List<Player> Players, int team_ind, ref int p_id, Boolean bNewLeage)
        {
            Player r = new Player();

            string[] PlayerName = null;
            int player_number = 0;

            Player_NamesDAO pnDAO = new Player_NamesDAO();

            // Create a player name and only leave the while loop when it is unique
            while (true)
            {
                PlayerName = pnDAO.CreatePlayerName();
                // If first and last name are the same then pick a new name
                if (PlayerName[0].ToUpper() == PlayerName[1].ToUpper())
                    continue;

                break;
            }

            while (true)
            {
                player_number = getPlayerNumber(pos);

                if (!isPlayerNumber_Unique_Team_Memoory(player_number.ToString(), Players))
                    continue;

                break;
            }

            int age = generate_Player_Age(bNewLeage);

            Player_Abilities ratings = Create_Player_Abilities(pos);

            r.First_Name = PlayerName[0];
            r.Last_Name = PlayerName[1];

            r.Age = age;
            r.Pos = (int)pos;
            r.Jersey_Number = player_number;

            r.Fumble_Rating = ratings.Fumble_Rating;
            r.Accuracy_Rating = ratings.Accuracy_Rating;
            r.Decision_Making = ratings.Decision_Making;
            r.Arm_Strength_Rating = ratings.Arm_Strength_Rating;
            r.Pass_Block_Rating = ratings.Pass_Block_Rating;
            r.Run_Block_Rating = ratings.Run_Block_Rating;
            r.Running_Power_Rating = ratings.Running_Power_Rating;
            r.Speed_Rating = ratings.Speed_Rating;
            r.Agilty_Rating = ratings.Agilty_Rating;
            r.Hands_Rating = ratings.Hands_Rating;
            r.Pass_Attack = ratings.Pass_Attack;
            r.Run_Attack = ratings.Run_Attack;
            r.Tackle_Rating = ratings.Tackle_Rating;
            r.Kicker_Leg_Power = ratings.Leg_Strength;
            r.Kicker_Leg_Accuracy = ratings.Kicking_Accuracy;

            r.Team_ID = team_ind;
            p_id++;
            r.Player_ID = p_id;

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

        public static Player_Abilities Create_Player_Abilities(Player_Pos pos)
        {
            Player_Abilities r = new Player_Abilities();

            switch (pos)
            {
                case Player_Pos.QB:
                    {
                        r.Accuracy_Rating = CommonUtils.getRandomNum(app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING);
                        r.Decision_Making = CommonUtils.getRandomNum(app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING);
                        r.Arm_Strength_Rating = CommonUtils.getRandomNum(app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING);
                        r.Fumble_Rating = CommonUtils.getRandomNum(app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING);

                        r.Agilty_Rating = CommonUtils.getRandomNum(app_Constants.SECONDARY_3_ABILITY_LOW_RATING, app_Constants.SECONDARY_3_ABILITY_HIGH_RATING);
                        r.Speed_Rating = CommonUtils.getRandomNum(app_Constants.SECONDARY_3_ABILITY_LOW_RATING, app_Constants.SECONDARY_3_ABILITY_HIGH_RATING);

                        r.Hands_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Kicking_Accuracy = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Leg_Strength = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Pass_Attack = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Pass_Block_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Running_Power_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Run_Attack = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Run_Block_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Tackle_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);

                        r.OverAll = Convert.ToSingle(r.Fumble_Rating * app_Constants.QB_FUMBLE_PERCENT + r.Arm_Strength_Rating * app_Constants.QB_ARMSTRENGTH_PERCENT + r.Accuracy_Rating * app_Constants.QB_ACCURACY_RATING + r.Decision_Making * app_Constants.QB_DESISION_RATING);
                        break;
                    }

                case Player_Pos.RB:
                    {
                        r.Running_Power_Rating = CommonUtils.getRandomNum(app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING);
                        r.Speed_Rating = CommonUtils.getRandomNum(app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING);
                        r.Agilty_Rating = CommonUtils.getRandomNum(app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING);
                        r.Fumble_Rating = CommonUtils.getRandomNum(app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING);

                        r.Hands_Rating = CommonUtils.getRandomNum(app_Constants.SECONDARY_1_ABILITY_LOW_RATING, app_Constants.SECONDARY_1_ABILITY_HIGH_RATING);
                        r.Tackle_Rating = CommonUtils.getRandomNum(app_Constants.SECONDARY_3_ABILITY_LOW_RATING, app_Constants.SECONDARY_3_ABILITY_HIGH_RATING);

                        r.Accuracy_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Decision_Making = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Arm_Strength_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Kicking_Accuracy = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Leg_Strength = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Pass_Attack = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Pass_Block_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Run_Attack = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Run_Block_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);

                        r.OverAll = Convert.ToSingle(r.Fumble_Rating * app_Constants.RB_FUMBLE_PERCENT + r.Running_Power_Rating * app_Constants.RB_RUNNING_PWER_PERCENT + r.Speed_Rating * app_Constants.RB_SPEED_PERCENT + (r.Hands_Rating + 100 - app_Constants.SECONDARY_1_ABILITY_HIGH_RATING) * app_Constants.RB_HANDS_PERCENT + r.Agilty_Rating * app_Constants.RB_AGILITY_PERCENT);
                        break;
                    }

                case Player_Pos.WR:
                    {
                        r.Speed_Rating = CommonUtils.getRandomNum(app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING);
                        r.Agilty_Rating = CommonUtils.getRandomNum(app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING);
                        r.Hands_Rating = CommonUtils.getRandomNum(app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING);
                        r.Fumble_Rating = CommonUtils.getRandomNum(app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING);

                        r.Tackle_Rating = CommonUtils.getRandomNum(app_Constants.SECONDARY_2_ABILITY_LOW_RATING, app_Constants.SECONDARY_2_ABILITY_HIGH_RATING);

                        r.Running_Power_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Accuracy_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Decision_Making = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Arm_Strength_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Kicking_Accuracy = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Leg_Strength = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Pass_Attack = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Pass_Block_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Run_Attack = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Run_Block_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);

                        r.OverAll = Convert.ToSingle(r.Fumble_Rating * app_Constants.WR_FUMBLE_PERCENT + r.Speed_Rating * app_Constants.WR_SPEED_PERCENT + r.Hands_Rating * app_Constants.WR_HANDS_PERCENT + r.Agilty_Rating * app_Constants.WR_AGILITY_PERCENT);
                        break;
                    }

                case Player_Pos.TE:
                    {
                        r.Speed_Rating = CommonUtils.getRandomNum(app_Constants.SECONDARY_1_ABILITY_LOW_RATING, app_Constants.SECONDARY_1_ABILITY_HIGH_RATING);
                        r.Agilty_Rating = CommonUtils.getRandomNum(app_Constants.SECONDARY_1_ABILITY_LOW_RATING, app_Constants.SECONDARY_1_ABILITY_HIGH_RATING);
                        r.Hands_Rating = CommonUtils.getRandomNum(app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING);
                        r.Fumble_Rating = CommonUtils.getRandomNum(app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING);
                        r.Pass_Block_Rating = CommonUtils.getRandomNum(app_Constants.SECONDARY_1_ABILITY_LOW_RATING, app_Constants.SECONDARY_1_ABILITY_HIGH_RATING);
                        r.Run_Block_Rating = CommonUtils.getRandomNum(app_Constants.SECONDARY_1_ABILITY_LOW_RATING, app_Constants.SECONDARY_1_ABILITY_HIGH_RATING);

                        r.Tackle_Rating = CommonUtils.getRandomNum(app_Constants.SECONDARY_1_ABILITY_LOW_RATING, app_Constants.SECONDARY_1_ABILITY_HIGH_RATING);

                        r.Running_Power_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Accuracy_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Decision_Making = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Arm_Strength_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Kicking_Accuracy = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Leg_Strength = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Pass_Attack = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Run_Attack = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);

                        r.OverAll = Convert.ToSingle(r.Fumble_Rating * app_Constants.TE_FUMBLE_PERCENT + (r.Speed_Rating + 100 - app_Constants.SECONDARY_1_ABILITY_HIGH_RATING) * app_Constants.TE_SPEED_PERCENT + r.Hands_Rating * app_Constants.TE_HANDS_PERCENT + (r.Agilty_Rating + 100 - app_Constants.SECONDARY_1_ABILITY_HIGH_RATING) * app_Constants.TE_AGILITY_PERCENT + (r.Pass_Block_Rating + 100 - app_Constants.SECONDARY_1_ABILITY_HIGH_RATING) * app_Constants.TE_PASS_BLOCK_PERCENT + (r.Run_Block_Rating + 100 - app_Constants.SECONDARY_1_ABILITY_HIGH_RATING) * app_Constants.TE_RUN_BLOCK_PERCENT);
                        break;
                    }

                case Player_Pos.OL:
                    {
                        r.Pass_Block_Rating = CommonUtils.getRandomNum(app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING);
                        r.Run_Block_Rating = CommonUtils.getRandomNum(Math.Max(app_Constants.PRIMARY_ABILITY_LOW_RATING, r.Pass_Block_Rating - app_Constants.OL_RUN_PASS_BLOCK_DELTA), Math.Min(app_Constants.PRIMARY_ABILITY_HIGH_RATING, r.Pass_Block_Rating + app_Constants.OL_RUN_PASS_BLOCK_DELTA));

                        r.Tackle_Rating = CommonUtils.getRandomNum(app_Constants.SECONDARY_1_ABILITY_LOW_RATING, app_Constants.SECONDARY_1_ABILITY_HIGH_RATING);
                        r.Speed_Rating = CommonUtils.getRandomNum(app_Constants.SECONDARY_3_ABILITY_LOW_RATING, app_Constants.SECONDARY_3_ABILITY_HIGH_RATING);
                        r.Agilty_Rating = CommonUtils.getRandomNum(app_Constants.SECONDARY_3_ABILITY_LOW_RATING, app_Constants.SECONDARY_3_ABILITY_HIGH_RATING);

                        r.Accuracy_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Decision_Making = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Arm_Strength_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Fumble_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Hands_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Kicking_Accuracy = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Leg_Strength = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Pass_Attack = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Running_Power_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Run_Attack = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);

                        r.OverAll = Convert.ToSingle(r.Pass_Block_Rating * app_Constants.OL_PASS_BLOCK_PERCENT + r.Run_Block_Rating * app_Constants.OL_RUN_BLOCK_PERCENT + (r.Agilty_Rating + 100 - app_Constants.SECONDARY_3_ABILITY_HIGH_RATING) * app_Constants.OL_AGILITY_PERCENT);
                        break;
                    }

                case Player_Pos.DL:
                    {
                        r.Pass_Attack = CommonUtils.getRandomNum(app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING);
                        r.Run_Attack = CommonUtils.getRandomNum(Math.Max(app_Constants.PRIMARY_ABILITY_LOW_RATING, r.Pass_Attack - app_Constants.OL_RUN_PASS_BLOCK_DELTA), Math.Min(app_Constants.PRIMARY_ABILITY_HIGH_RATING, r.Pass_Attack + app_Constants.OL_RUN_PASS_BLOCK_DELTA));
                        r.Tackle_Rating = CommonUtils.getRandomNum(app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING);

                        r.Speed_Rating = CommonUtils.getRandomNum(app_Constants.SECONDARY_3_ABILITY_LOW_RATING, app_Constants.SECONDARY_3_ABILITY_HIGH_RATING);
                        r.Agilty_Rating = CommonUtils.getRandomNum(app_Constants.SECONDARY_3_ABILITY_LOW_RATING, app_Constants.SECONDARY_3_ABILITY_HIGH_RATING);

                        r.Pass_Block_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Run_Block_Rating = CommonUtils.getRandomNum(Math.Max(app_Constants.TERTIARY_ABILITY_LOW_RATING, r.Pass_Block_Rating - app_Constants.OL_RUN_PASS_BLOCK_DELTA), Math.Min(app_Constants.TERTIARY_ABILITY_HIGH_RATING, r.Pass_Block_Rating + app_Constants.OL_RUN_PASS_BLOCK_DELTA));
                        r.Accuracy_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Decision_Making = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Arm_Strength_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Fumble_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Hands_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Kicking_Accuracy = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Leg_Strength = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Running_Power_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);

                        r.OverAll = Convert.ToSingle(r.Pass_Attack * app_Constants.DL_PASS_ATTACK_PERCENT + r.Run_Attack * app_Constants.DL_RUN_ATTACK_PERCENT + r.Tackle_Rating * app_Constants.DL_TACKLE_PERCENT + (r.Agilty_Rating + 100 - app_Constants.SECONDARY_3_ABILITY_HIGH_RATING) * app_Constants.DL_AGILITY_PERCENT + (r.Speed_Rating + 100 - app_Constants.SECONDARY_3_ABILITY_HIGH_RATING) * app_Constants.DL_SPEED_PERCENT);
                        break;
                    }

                case Player_Pos.DB:
                    {
                        r.Speed_Rating = CommonUtils.getRandomNum(app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING);
                        r.Hands_Rating = CommonUtils.getRandomNum(app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING);
                        r.Tackle_Rating = CommonUtils.getRandomNum(app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING);
                        r.Agilty_Rating = CommonUtils.getRandomNum(app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING);

                        r.Pass_Block_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Run_Block_Rating = CommonUtils.getRandomNum(Math.Max(app_Constants.TERTIARY_ABILITY_LOW_RATING, r.Pass_Block_Rating - app_Constants.OL_RUN_PASS_BLOCK_DELTA), Math.Min(app_Constants.TERTIARY_ABILITY_HIGH_RATING, r.Pass_Block_Rating + app_Constants.OL_RUN_PASS_BLOCK_DELTA));
                        r.Accuracy_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Decision_Making = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Arm_Strength_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Fumble_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Kicking_Accuracy = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Leg_Strength = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Pass_Attack = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Running_Power_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Run_Attack = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);

                        r.OverAll = Convert.ToSingle(r.Speed_Rating * app_Constants.DB_SPEED_PERCENT + r.Hands_Rating * app_Constants.DB_HANDS_PERCENT + r.Tackle_Rating * app_Constants.DB_TACKLING_PERCENT + r.Agilty_Rating * app_Constants.DB_AGILITY_PERCENT);
                        break;
                    }

                case Player_Pos.LB:
                    {
                        r.Tackle_Rating = CommonUtils.getRandomNum(app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING);
                        r.Pass_Attack = CommonUtils.getRandomNum(app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING);
                        r.Run_Attack = CommonUtils.getRandomNum(Math.Max(app_Constants.PRIMARY_ABILITY_LOW_RATING, r.Pass_Attack - app_Constants.OL_RUN_PASS_BLOCK_DELTA), Math.Min(app_Constants.PRIMARY_ABILITY_HIGH_RATING, r.Pass_Attack + app_Constants.OL_RUN_PASS_BLOCK_DELTA));

                        r.Speed_Rating = CommonUtils.getRandomNum(app_Constants.SECONDARY_1_ABILITY_LOW_RATING, app_Constants.SECONDARY_1_ABILITY_HIGH_RATING);
                        r.Agilty_Rating = CommonUtils.getRandomNum(app_Constants.SECONDARY_1_ABILITY_LOW_RATING, app_Constants.SECONDARY_1_ABILITY_HIGH_RATING);
                        r.Hands_Rating = CommonUtils.getRandomNum(app_Constants.SECONDARY_2_ABILITY_LOW_RATING, app_Constants.SECONDARY_2_ABILITY_HIGH_RATING);

                        r.Pass_Block_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Run_Block_Rating = CommonUtils.getRandomNum(Math.Max(app_Constants.TERTIARY_ABILITY_LOW_RATING, r.Pass_Block_Rating - app_Constants.OL_RUN_PASS_BLOCK_DELTA), Math.Min(app_Constants.TERTIARY_ABILITY_HIGH_RATING, r.Pass_Block_Rating + app_Constants.OL_RUN_PASS_BLOCK_DELTA));
                        r.Accuracy_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Decision_Making = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Arm_Strength_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Fumble_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Kicking_Accuracy = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Leg_Strength = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Running_Power_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);

                        r.OverAll = Convert.ToSingle(r.Pass_Attack * app_Constants.LB_PASS_ATTACK_PERCENT + r.Run_Attack * app_Constants.LB_RUN_ATTACK_PERCENT + r.Tackle_Rating * app_Constants.LB_TACKLE_PERCENT + (r.Hands_Rating + 100 - app_Constants.SECONDARY_2_ABILITY_HIGH_RATING) * app_Constants.LB_HANDS_PERCENT + (r.Speed_Rating + 100 - app_Constants.SECONDARY_1_ABILITY_HIGH_RATING) * app_Constants.LB_SPEED_PERCENT + (r.Agilty_Rating + 100 - app_Constants.SECONDARY_1_ABILITY_HIGH_RATING) * app_Constants.LB_AGILITY_PERCENT);
                        break;
                    }

                case Player_Pos.K:
                case Player_Pos.P:
                    {
                        r.Kicking_Accuracy = CommonUtils.getRandomNum(app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING);
                        r.Leg_Strength = CommonUtils.getRandomNum(app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING);

                        r.Pass_Block_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Run_Block_Rating = CommonUtils.getRandomNum(Math.Max(app_Constants.TERTIARY_ABILITY_LOW_RATING, r.Pass_Block_Rating - app_Constants.OL_RUN_PASS_BLOCK_DELTA), Math.Min(app_Constants.TERTIARY_ABILITY_HIGH_RATING, r.Pass_Block_Rating + app_Constants.OL_RUN_PASS_BLOCK_DELTA));
                        r.Accuracy_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Decision_Making = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Arm_Strength_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Fumble_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Agilty_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Hands_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Pass_Attack = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Running_Power_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Run_Attack = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Speed_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Run_Attack = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Tackle_Rating = CommonUtils.getRandomNum(app_Constants.TERTIARY_ABILITY_LOW_RATING, app_Constants.TERTIARY_ABILITY_HIGH_RATING);

                        r.OverAll = Convert.ToSingle(r.Kicking_Accuracy * app_Constants.K_KICK_ACC + r.Leg_Strength * app_Constants.K_LEG_STRENGTH);
                        break;
                    }
            }

            return r;
        }
 
    }
}
