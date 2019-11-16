using System.Collections.Generic;
using System;

namespace SpectatorFootball
{
    public class Player
    {
        public PlayerMdl CreatePlayer(PlayerMdl.Position pos, List<PlayerMdl> Players, int team_ind, string league_DB_Connecdtion)
        {
            PlayerMdl r = new PlayerMdl();

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

                if (league_DB_Connecdtion == null && league_DB_Connecdtion.Length > 0)
                {
                    if (!Player_DAO.isPlayerNumber_Unigue_DB(player_number.ToString(), league_DB_Connecdtion, team_ind))
                        continue;
                }
                break;
            }

            int age = generate_Player_Age(league_DB_Connecdtion==null);

            Player_Abilities ratings = Create_Player_Abilities(pos);

            r.First_Name = PlayerName[0];
            r.Last_Name = PlayerName[1];

            r.Age = age;
            r.Pos = pos;
            r.Jersey_Number = player_number;
            r.Ratings = ratings;
            r.Active = true;

            return r;
        }
        public int getPlayerNumber(PlayerMdl.Position pos)
        {
            int r = default(int);

            switch (pos)
            {
                case PlayerMdl.Position.QB:
                    {
                        r = CommonUtils.getRandomNum(App_Constants.QBLOWNUM, App_Constants.QBHIGHNUM);
                        break;
                    }

                case PlayerMdl.Position.RB:
                    {
                        r = CommonUtils.getRandomNum(App_Constants.RBLOWNUM, App_Constants.RBHIGHNUM);
                        break;
                    }

                case PlayerMdl.Position.WR:
                    {
                        r = CommonUtils.getRandomNum(App_Constants.WRLOWNUM, App_Constants.WRHIGHNUM);
                        break;
                    }

                case PlayerMdl.Position.TE:
                    {
                        while (true)
                        {
                            r = CommonUtils.getRandomNum(App_Constants.TELOWNUM, App_Constants.TEHIGHNUM);
                            if (r >= 50 && r <= 79)
                                break;
                        }

                        break;
                    }

                case PlayerMdl.Position.OL:
                    {
                        r = CommonUtils.getRandomNum(App_Constants.OLLOWNUM, App_Constants.OLHIGHNUM);
                        break;
                    }

                case PlayerMdl.Position.DL:
                    {
                        while (true)
                        {
                            r = CommonUtils.getRandomNum(App_Constants.DLLOWNUM, App_Constants.DLHIGHNUM);
                            if (r < 80 || r > 89)
                                break;
                        }

                        break;
                    }

                case PlayerMdl.Position.LB:
                    {
                        r = CommonUtils.getRandomNum(App_Constants.LBLOWNUM, App_Constants.LBHIGHNUM);
                        break;
                    }

                case PlayerMdl.Position.DB:
                    {
                        r = CommonUtils.getRandomNum(App_Constants.DBLOWNUM, App_Constants.DBHIGHNUM);
                        break;
                    }

                case PlayerMdl.Position.K:
                case PlayerMdl.Position.P:
                    {
                        r = CommonUtils.getRandomNum(App_Constants.KLOWNUM, App_Constants.KHIGHNUM);
                        break;
                    }
            }

            return r;
        }
        public bool isPlayerNumber_Unique_Team_Memoory(string number, List<PlayerMdl> Players)
        {
            bool r = true;

            if (Players != null)
            {
                foreach (PlayerMdl p in Players)
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
        public int generate_Player_Age(bool newleage)
        {
            int r = App_Constants.STARTING_ROOKIE_AGE;

            if (!newleage)
                r = CommonUtils.getRandomNum(App_Constants.NEWLEAGE_LOW_AGE, App_Constants.NEWLEAGE_HIGH_AGE);

            return r;
        }

        public Player_Abilities Create_Player_Abilities(PlayerMdl.Position pos)
        {
            Player_Abilities r = new Player_Abilities();

            switch (pos)
            {
                case PlayerMdl.Position.QB:
                    {
                        r.Accuracy_Rating = CommonUtils.getRandomNum(App_Constants.PRIMARY_ABILITY_LOW_RATING, App_Constants.PRIMARY_ABILITY_HIGH_RATING);
                        r.Decision_Making = CommonUtils.getRandomNum(App_Constants.PRIMARY_ABILITY_LOW_RATING, App_Constants.PRIMARY_ABILITY_HIGH_RATING);
                        r.Arm_Strength_Rating = CommonUtils.getRandomNum(App_Constants.PRIMARY_ABILITY_LOW_RATING, App_Constants.PRIMARY_ABILITY_HIGH_RATING);
                        r.Fumble_Rating = CommonUtils.getRandomNum(App_Constants.PRIMARY_ABILITY_LOW_RATING, App_Constants.PRIMARY_ABILITY_HIGH_RATING);

                        r.Agilty_Rating = CommonUtils.getRandomNum(App_Constants.SECONDARY_3_ABILITY_LOW_RATING, App_Constants.SECONDARY_3_ABILITY_HIGH_RATING);
                        r.Speed_Rating = CommonUtils.getRandomNum(App_Constants.SECONDARY_3_ABILITY_LOW_RATING, App_Constants.SECONDARY_3_ABILITY_HIGH_RATING);

                        r.Hands_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Kicking_Accuracy = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Leg_Strength = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Pass_Attack = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Pass_Block_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Running_Power_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Run_Attack = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Run_Block_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Tackle_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING);

                        r.OverAll = Convert.ToSingle(r.Fumble_Rating * App_Constants.QB_FUMBLE_PERCENT + r.Arm_Strength_Rating * App_Constants.QB_ARMSTRENGTH_PERCENT + r.Accuracy_Rating * App_Constants.QB_ACCURACY_RATING + r.Decision_Making * App_Constants.QB_DESISION_RATING);
                        break;
                    }

                case PlayerMdl.Position.RB:
                    {
                        r.Running_Power_Rating = CommonUtils.getRandomNum(App_Constants.PRIMARY_ABILITY_LOW_RATING, App_Constants.PRIMARY_ABILITY_HIGH_RATING);
                        r.Speed_Rating = CommonUtils.getRandomNum(App_Constants.PRIMARY_ABILITY_LOW_RATING, App_Constants.PRIMARY_ABILITY_HIGH_RATING);
                        r.Agilty_Rating = CommonUtils.getRandomNum(App_Constants.PRIMARY_ABILITY_LOW_RATING, App_Constants.PRIMARY_ABILITY_HIGH_RATING);
                        r.Fumble_Rating = CommonUtils.getRandomNum(App_Constants.PRIMARY_ABILITY_LOW_RATING, App_Constants.PRIMARY_ABILITY_HIGH_RATING);

                        r.Hands_Rating = CommonUtils.getRandomNum(App_Constants.SECONDARY_1_ABILITY_LOW_RATING, App_Constants.SECONDARY_1_ABILITY_HIGH_RATING);
                        r.Tackle_Rating = CommonUtils.getRandomNum(App_Constants.SECONDARY_3_ABILITY_LOW_RATING, App_Constants.SECONDARY_3_ABILITY_HIGH_RATING);

                        r.Accuracy_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Decision_Making = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Arm_Strength_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Kicking_Accuracy = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Leg_Strength = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Pass_Attack = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Pass_Block_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Run_Attack = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Run_Block_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING);

                        r.OverAll = Convert.ToSingle(r.Fumble_Rating * App_Constants.RB_FUMBLE_PERCENT + r.Running_Power_Rating * App_Constants.RB_RUNNING_PWER_PERCENT + r.Speed_Rating * App_Constants.RB_SPEED_PERCENT + (r.Hands_Rating + 100 - App_Constants.SECONDARY_1_ABILITY_HIGH_RATING) * App_Constants.RB_HANDS_PERCENT + r.Agilty_Rating * App_Constants.RB_AGILITY_PERCENT);
                        break;
                    }

                case PlayerMdl.Position.WR:
                    {
                        r.Speed_Rating = CommonUtils.getRandomNum(App_Constants.PRIMARY_ABILITY_LOW_RATING, App_Constants.PRIMARY_ABILITY_HIGH_RATING);
                        r.Agilty_Rating = CommonUtils.getRandomNum(App_Constants.PRIMARY_ABILITY_LOW_RATING, App_Constants.PRIMARY_ABILITY_HIGH_RATING);
                        r.Hands_Rating = CommonUtils.getRandomNum(App_Constants.PRIMARY_ABILITY_LOW_RATING, App_Constants.PRIMARY_ABILITY_HIGH_RATING);
                        r.Fumble_Rating = CommonUtils.getRandomNum(App_Constants.PRIMARY_ABILITY_LOW_RATING, App_Constants.PRIMARY_ABILITY_HIGH_RATING);

                        r.Tackle_Rating = CommonUtils.getRandomNum(App_Constants.SECONDARY_2_ABILITY_LOW_RATING, App_Constants.SECONDARY_2_ABILITY_HIGH_RATING);

                        r.Running_Power_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Accuracy_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Decision_Making = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Arm_Strength_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Kicking_Accuracy = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Leg_Strength = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Pass_Attack = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Pass_Block_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Run_Attack = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Run_Block_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING);

                        r.OverAll = Convert.ToSingle(r.Fumble_Rating * App_Constants.WR_FUMBLE_PERCENT + r.Speed_Rating * App_Constants.WR_SPEED_PERCENT + r.Hands_Rating * App_Constants.WR_HANDS_PERCENT + r.Agilty_Rating * App_Constants.WR_AGILITY_PERCENT);
                        break;
                    }

                case PlayerMdl.Position.TE:
                    {
                        r.Speed_Rating = CommonUtils.getRandomNum(App_Constants.SECONDARY_1_ABILITY_LOW_RATING, App_Constants.SECONDARY_1_ABILITY_HIGH_RATING);
                        r.Agilty_Rating = CommonUtils.getRandomNum(App_Constants.SECONDARY_1_ABILITY_LOW_RATING, App_Constants.SECONDARY_1_ABILITY_HIGH_RATING);
                        r.Hands_Rating = CommonUtils.getRandomNum(App_Constants.PRIMARY_ABILITY_LOW_RATING, App_Constants.PRIMARY_ABILITY_HIGH_RATING);
                        r.Fumble_Rating = CommonUtils.getRandomNum(App_Constants.PRIMARY_ABILITY_LOW_RATING, App_Constants.PRIMARY_ABILITY_HIGH_RATING);
                        r.Pass_Block_Rating = CommonUtils.getRandomNum(App_Constants.SECONDARY_1_ABILITY_LOW_RATING, App_Constants.SECONDARY_1_ABILITY_HIGH_RATING);
                        r.Run_Block_Rating = CommonUtils.getRandomNum(App_Constants.SECONDARY_1_ABILITY_LOW_RATING, App_Constants.SECONDARY_1_ABILITY_HIGH_RATING);

                        r.Tackle_Rating = CommonUtils.getRandomNum(App_Constants.SECONDARY_1_ABILITY_LOW_RATING, App_Constants.SECONDARY_1_ABILITY_HIGH_RATING);

                        r.Running_Power_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Accuracy_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Decision_Making = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Arm_Strength_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Kicking_Accuracy = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Leg_Strength = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Pass_Attack = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Run_Attack = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING);

                        r.OverAll = Convert.ToSingle(r.Fumble_Rating * App_Constants.TE_FUMBLE_PERCENT + (r.Speed_Rating + 100 - App_Constants.SECONDARY_1_ABILITY_HIGH_RATING) * App_Constants.TE_SPEED_PERCENT + r.Hands_Rating * App_Constants.TE_HANDS_PERCENT + (r.Agilty_Rating + 100 - App_Constants.SECONDARY_1_ABILITY_HIGH_RATING) * App_Constants.TE_AGILITY_PERCENT + (r.Pass_Block_Rating + 100 - App_Constants.SECONDARY_1_ABILITY_HIGH_RATING) * App_Constants.TE_PASS_BLOCK_PERCENT + (r.Run_Block_Rating + 100 - App_Constants.SECONDARY_1_ABILITY_HIGH_RATING) * App_Constants.TE_RUN_BLOCK_PERCENT);
                        break;
                    }

                case PlayerMdl.Position.OL:
                    {
                        r.Pass_Block_Rating = CommonUtils.getRandomNum(App_Constants.PRIMARY_ABILITY_LOW_RATING, App_Constants.PRIMARY_ABILITY_HIGH_RATING);
                        r.Run_Block_Rating = CommonUtils.getRandomNum(Math.Max(App_Constants.PRIMARY_ABILITY_LOW_RATING, r.Pass_Block_Rating - App_Constants.OL_RUN_PASS_BLOCK_DELTA), Math.Min(App_Constants.PRIMARY_ABILITY_HIGH_RATING, r.Pass_Block_Rating + App_Constants.OL_RUN_PASS_BLOCK_DELTA));

                        r.Tackle_Rating = CommonUtils.getRandomNum(App_Constants.SECONDARY_1_ABILITY_LOW_RATING, App_Constants.SECONDARY_1_ABILITY_HIGH_RATING);
                        r.Speed_Rating = CommonUtils.getRandomNum(App_Constants.SECONDARY_3_ABILITY_LOW_RATING, App_Constants.SECONDARY_3_ABILITY_HIGH_RATING);
                        r.Agilty_Rating = CommonUtils.getRandomNum(App_Constants.SECONDARY_3_ABILITY_LOW_RATING, App_Constants.SECONDARY_3_ABILITY_HIGH_RATING);

                        r.Accuracy_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Decision_Making = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Arm_Strength_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Fumble_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Hands_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Kicking_Accuracy = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Leg_Strength = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Pass_Attack = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Running_Power_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Run_Attack = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING);

                        r.OverAll = Convert.ToSingle(r.Pass_Block_Rating * App_Constants.OL_PASS_BLOCK_PERCENT + r.Run_Block_Rating * App_Constants.OL_RUN_BLOCK_PERCENT + (r.Agilty_Rating + 100 - App_Constants.SECONDARY_3_ABILITY_HIGH_RATING) * App_Constants.OL_AGILITY_PERCENT);
                        break;
                    }

                case PlayerMdl.Position.DL:
                    {
                        r.Pass_Attack = CommonUtils.getRandomNum(App_Constants.PRIMARY_ABILITY_LOW_RATING, App_Constants.PRIMARY_ABILITY_HIGH_RATING);
                        r.Run_Attack = CommonUtils.getRandomNum(Math.Max(App_Constants.PRIMARY_ABILITY_LOW_RATING, r.Pass_Attack - App_Constants.OL_RUN_PASS_BLOCK_DELTA), Math.Min(App_Constants.PRIMARY_ABILITY_HIGH_RATING, r.Pass_Attack + App_Constants.OL_RUN_PASS_BLOCK_DELTA));
                        r.Tackle_Rating = CommonUtils.getRandomNum(App_Constants.PRIMARY_ABILITY_LOW_RATING, App_Constants.PRIMARY_ABILITY_HIGH_RATING);

                        r.Speed_Rating = CommonUtils.getRandomNum(App_Constants.SECONDARY_3_ABILITY_LOW_RATING, App_Constants.SECONDARY_3_ABILITY_HIGH_RATING);
                        r.Agilty_Rating = CommonUtils.getRandomNum(App_Constants.SECONDARY_3_ABILITY_LOW_RATING, App_Constants.SECONDARY_3_ABILITY_HIGH_RATING);

                        r.Pass_Block_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Run_Block_Rating = CommonUtils.getRandomNum(Math.Max(App_Constants.TERTIARY_ABILITY_LOW_RATING, r.Pass_Block_Rating - App_Constants.OL_RUN_PASS_BLOCK_DELTA), Math.Min(App_Constants.TERTIARY_ABILITY_HIGH_RATING, r.Pass_Block_Rating + App_Constants.OL_RUN_PASS_BLOCK_DELTA));
                        r.Accuracy_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Decision_Making = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Arm_Strength_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Fumble_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Hands_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Kicking_Accuracy = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Leg_Strength = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Running_Power_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING);

                        r.OverAll = Convert.ToSingle(r.Pass_Attack * App_Constants.DL_PASS_ATTACK_PERCENT + r.Run_Attack * App_Constants.DL_RUN_ATTACK_PERCENT + r.Tackle_Rating * App_Constants.DL_TACKLE_PERCENT + (r.Agilty_Rating + 100 - App_Constants.SECONDARY_3_ABILITY_HIGH_RATING) * App_Constants.DL_AGILITY_PERCENT + (r.Speed_Rating + 100 - App_Constants.SECONDARY_3_ABILITY_HIGH_RATING) * App_Constants.DL_SPEED_PERCENT);
                        break;
                    }

                case PlayerMdl.Position.DB:
                    {
                        r.Speed_Rating = CommonUtils.getRandomNum(App_Constants.PRIMARY_ABILITY_LOW_RATING, App_Constants.PRIMARY_ABILITY_HIGH_RATING);
                        r.Hands_Rating = CommonUtils.getRandomNum(App_Constants.PRIMARY_ABILITY_LOW_RATING, App_Constants.PRIMARY_ABILITY_HIGH_RATING);
                        r.Tackle_Rating = CommonUtils.getRandomNum(App_Constants.PRIMARY_ABILITY_LOW_RATING, App_Constants.PRIMARY_ABILITY_HIGH_RATING);
                        r.Agilty_Rating = CommonUtils.getRandomNum(App_Constants.PRIMARY_ABILITY_LOW_RATING, App_Constants.PRIMARY_ABILITY_HIGH_RATING);

                        r.Pass_Block_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Run_Block_Rating = CommonUtils.getRandomNum(Math.Max(App_Constants.TERTIARY_ABILITY_LOW_RATING, r.Pass_Block_Rating - App_Constants.OL_RUN_PASS_BLOCK_DELTA), Math.Min(App_Constants.TERTIARY_ABILITY_HIGH_RATING, r.Pass_Block_Rating + App_Constants.OL_RUN_PASS_BLOCK_DELTA));
                        r.Accuracy_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Decision_Making = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Arm_Strength_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Fumble_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Kicking_Accuracy = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Leg_Strength = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Pass_Attack = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Running_Power_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Run_Attack = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING);

                        r.OverAll = Convert.ToSingle(r.Speed_Rating * App_Constants.DB_SPEED_PERCENT + r.Hands_Rating * App_Constants.DB_HANDS_PERCENT + r.Tackle_Rating * App_Constants.DB_TACKLING_PERCENT + r.Agilty_Rating * App_Constants.DB_AGILITY_PERCENT);
                        break;
                    }

                case PlayerMdl.Position.LB:
                    {
                        r.Tackle_Rating = CommonUtils.getRandomNum(App_Constants.PRIMARY_ABILITY_LOW_RATING, App_Constants.PRIMARY_ABILITY_HIGH_RATING);
                        r.Pass_Attack = CommonUtils.getRandomNum(App_Constants.PRIMARY_ABILITY_LOW_RATING, App_Constants.PRIMARY_ABILITY_HIGH_RATING);
                        r.Run_Attack = CommonUtils.getRandomNum(Math.Max(App_Constants.PRIMARY_ABILITY_LOW_RATING, r.Pass_Attack - App_Constants.OL_RUN_PASS_BLOCK_DELTA), Math.Min(App_Constants.PRIMARY_ABILITY_HIGH_RATING, r.Pass_Attack + App_Constants.OL_RUN_PASS_BLOCK_DELTA));

                        r.Speed_Rating = CommonUtils.getRandomNum(App_Constants.SECONDARY_1_ABILITY_LOW_RATING, App_Constants.SECONDARY_1_ABILITY_HIGH_RATING);
                        r.Agilty_Rating = CommonUtils.getRandomNum(App_Constants.SECONDARY_1_ABILITY_LOW_RATING, App_Constants.SECONDARY_1_ABILITY_HIGH_RATING);
                        r.Hands_Rating = CommonUtils.getRandomNum(App_Constants.SECONDARY_2_ABILITY_LOW_RATING, App_Constants.SECONDARY_2_ABILITY_HIGH_RATING);

                        r.Pass_Block_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Run_Block_Rating = CommonUtils.getRandomNum(Math.Max(App_Constants.TERTIARY_ABILITY_LOW_RATING, r.Pass_Block_Rating - App_Constants.OL_RUN_PASS_BLOCK_DELTA), Math.Min(App_Constants.TERTIARY_ABILITY_HIGH_RATING, r.Pass_Block_Rating + App_Constants.OL_RUN_PASS_BLOCK_DELTA));
                        r.Accuracy_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Decision_Making = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Arm_Strength_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Fumble_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Kicking_Accuracy = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Leg_Strength = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Running_Power_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING);

                        r.OverAll = Convert.ToSingle(r.Pass_Attack * App_Constants.LB_PASS_ATTACK_PERCENT + r.Run_Attack * App_Constants.LB_RUN_ATTACK_PERCENT + r.Tackle_Rating * App_Constants.LB_TACKLE_PERCENT + (r.Hands_Rating + 100 - App_Constants.SECONDARY_2_ABILITY_HIGH_RATING) * App_Constants.LB_HANDS_PERCENT + (r.Speed_Rating + 100 - App_Constants.SECONDARY_1_ABILITY_HIGH_RATING) * App_Constants.LB_SPEED_PERCENT + (r.Agilty_Rating + 100 - App_Constants.SECONDARY_1_ABILITY_HIGH_RATING) * App_Constants.LB_AGILITY_PERCENT);
                        break;
                    }

                case PlayerMdl.Position.K:
                case PlayerMdl.Position.P:
                    {
                        r.Kicking_Accuracy = CommonUtils.getRandomNum(App_Constants.PRIMARY_ABILITY_LOW_RATING, App_Constants.PRIMARY_ABILITY_HIGH_RATING);
                        r.Leg_Strength = CommonUtils.getRandomNum(App_Constants.PRIMARY_ABILITY_LOW_RATING, App_Constants.PRIMARY_ABILITY_HIGH_RATING);

                        r.Pass_Block_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Run_Block_Rating = CommonUtils.getRandomNum(Math.Max(App_Constants.TERTIARY_ABILITY_LOW_RATING, r.Pass_Block_Rating - App_Constants.OL_RUN_PASS_BLOCK_DELTA), Math.Min(App_Constants.TERTIARY_ABILITY_HIGH_RATING, r.Pass_Block_Rating + App_Constants.OL_RUN_PASS_BLOCK_DELTA));
                        r.Accuracy_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Decision_Making = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Arm_Strength_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Fumble_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Agilty_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Hands_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Pass_Attack = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Running_Power_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Run_Attack = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Speed_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Run_Attack = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING);
                        r.Tackle_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING);

                        r.OverAll = Convert.ToSingle(r.Kicking_Accuracy * App_Constants.K_KICK_ACC + r.Leg_Strength * App_Constants.K_LEG_STRENGTH);
                        break;
                    }
            }

            return r;
        }
    }
}
