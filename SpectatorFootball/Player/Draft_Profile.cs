using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SpectatorFootball.Enum;
using SpectatorFootball.Models;
using SpectatorFootball.NarrationAndText;

namespace SpectatorFootball
{
    class Draft_Profile
    {
        public static Player_Ratings setDraftRatings(Player_Ratings pr)
        {
            Player_Ratings r = new Player_Ratings();

            var sourceProperties = pr.GetType().GetProperties();
            var destProperties = r.GetType().GetProperties();

            foreach (var sourceProperty in sourceProperties)
            {
                //skip properties that are not ratings
                if (sourceProperty.Name == "Season" || sourceProperty.Name == "Player" || sourceProperty.Name == "ID" || sourceProperty.Name == "Player_ID" || sourceProperty.Name == "Season_ID")
                    continue;
                foreach (var destProperty in destProperties)
                {
                    if (sourceProperty.Name == destProperty.Name && sourceProperty.PropertyType == destProperty.PropertyType)
                    {
                        long new_value = (long) sourceProperty.GetValue(pr);
                        if (new_value >= app_Constants.PRIMARY_ABILITY_LOW_RATING)
                        {
                            new_value = alterValue(Convert.ToInt32(new_value));
                        }

                        destProperty.SetValue(r, new_value);
                        break;
                    }
                }
            }

            return r;
        }

        private static int alterValue(int v)
        {
            int loweri;
            int upperi;
            int r;

            int rmd = CommonUtils.getRandomNum(1, 100);
            if (rmd >= 85)
            {
                loweri = v - 25;
                upperi = v + 25;
            }
            else if (rmd >= 65)
            {
                loweri = v - 15;
                upperi = v + 15;
            }
            else if (rmd >= 40)
            {
                loweri = v - 5;
                upperi = v + 5;
            }
            else 
            {
                loweri = v;
                upperi = v;
            }

            //Make sure that the lower and upper are not beyond the range
            loweri = loweri < 1 ? 1 : loweri;
            upperi = upperi > 100 ? 100 : upperi;

            r = CommonUtils.getRandomNum(loweri,upperi);

            return r;
        }

        public static string Create_Draft_Scoutingrpt(Player_Pos pos, Player_Ratings pr)
        {

            List<string> Player_Attributes = new List<string>();

            StringBuilder sb = new StringBuilder();

            switch (pos)
            {
                case Player_Pos.QB:
                    {
                        Player_Attributes.Add("Accuracy_Rating");
                        Player_Attributes.Add("Decision_Making");
                        Player_Attributes.Add("Arm_Strength_Rating");

                        Player_Attributes = CommonUtils.ShufleList(Player_Attributes);

                        int i = 0;
                        bool bFirst = false;
                        bool bLast = false;
                        bool bNexttoLast = false;
                        foreach (string pa in Player_Attributes)
                        {
                            if (i == 0)
                                bFirst = true;
                            else
                                bFirst = false;

                            if (i == Player_Attributes.Count() - 1)
                                bLast = true;
                            else
                                bLast = false;

                            switch (pa)
                            {
                                case "Accuracy_Rating":
                                    sb.Append(ScoutingReport.AccuracyReport((int) pr.Accuracy_Rating, app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING, bFirst, bLast));
                                    break;
                                case "Decision_Making":
                                    sb.Append(ScoutingReport.DecisionMakingReport((int)pr.Decision_Making, app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING, bFirst, bLast));
                                    break;
                                case "Arm_Strength_Rating":
                                    sb.Append(ScoutingReport.ArmStrengthReport((int)pr.Arm_Strength_Rating, app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING, bFirst, bLast));
                                    break;
                            }
                            i++;
                        }


                        break;
                    }

                case Player_Pos.RB:
                    {
                        Player_Attributes.Add("Running_Power");
                        Player_Attributes.Add("Spped");
                        Player_Attributes.Add("Agility");
                        Player_Attributes.Add("Hands");

                        Player_Attributes = CommonUtils.ShufleList(Player_Attributes);

                        int i = 0;
                        bool bFirst = false;
                        bool bLast = false;
                        bool bNexttoLast = false;
                        foreach (string pa in Player_Attributes)
                        {
                            if (i == 0)
                                bFirst = true;
                            else
                                bFirst = false;

                            if (i == Player_Attributes.Count() - 1)
                                bLast = true;
                            else
                                bLast = false;

                            switch (pa)
                            {
                                case "Running_Power":
                                    sb.Append(ScoutingReport.PowerRunningReport((int)pr.Running_Power_Rating, app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING, bFirst, bLast));
                                    break;
                                case "Spped":
                                    sb.Append(ScoutingReport.SpeedReport((int)pr.Speed_Rating, app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING, bFirst, bLast));
                                    break;
                                case "Agility":
                                    sb.Append(ScoutingReport.AgilityReport((int)pr.Agilty_Rating, app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING, bFirst, bLast));
                                    break;
                                case "Hands":
                                    sb.Append(ScoutingReport.HandsReport((int)pr.Hands_Rating, app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING, bFirst, bLast));
                                    break;
                            }
                            i++;
                        }


                        break;
                    }


                case Player_Pos.WR:
                    {
                        Player_Attributes.Add("Spped");
                        Player_Attributes.Add("Agility");
                        Player_Attributes.Add("Hands");

                        Player_Attributes = CommonUtils.ShufleList(Player_Attributes);

                        int i = 0;
                        bool bFirst = false;
                        bool bLast = false;
                        bool bNexttoLast = false;
                        foreach (string pa in Player_Attributes)
                        {
                            if (i == 0)
                                bFirst = true;
                            else
                                bFirst = false;

                            if (i == Player_Attributes.Count() - 1)
                                bLast = true;
                            else
                                bLast = false;

                            switch (pa)
                            {
                                case "Spped":
                                    sb.Append(ScoutingReport.SpeedReport((int)pr.Speed_Rating, app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING, bFirst, bLast));
                                    break;
                                case "Agility":
                                    sb.Append(ScoutingReport.AgilityReport((int)pr.Agilty_Rating, app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING, bFirst, bLast));
                                    break;
                                case "Hands":
                                    sb.Append(ScoutingReport.HandsReport((int)pr.Hands_Rating, app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING, bFirst, bLast));
                                    break;
                            }
                            i++;
                        }


                        break;
                    }


                case Player_Pos.TE:
                    {
                        Player_Attributes.Add("Spped");
                        Player_Attributes.Add("Agility");
                        Player_Attributes.Add("Hands");
                        Player_Attributes.Add("PassBlocking");
                        Player_Attributes.Add("RunBlocking");

                        Player_Attributes = CommonUtils.ShufleList(Player_Attributes);

                        int i = 0;
                        bool bFirst = false;
                        bool bLast = false;
                        bool bNexttoLast = false;
                        foreach (string pa in Player_Attributes)
                        {
                            if (i == 0)
                                bFirst = true;
                            else
                                bFirst = false;

                            if (i == Player_Attributes.Count() - 1)
                                bLast = true;
                            else
                                bLast = false;

                            switch (pa)
                            {
                                case "Spped":
                                    sb.Append(ScoutingReport.SpeedReport((int)pr.Speed_Rating, app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING, bFirst, bLast));
                                    break;
                                case "Agility":
                                    sb.Append(ScoutingReport.AgilityReport((int)pr.Agilty_Rating, app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING, bFirst, bLast));
                                    break;
                                case "Hands":
                                    sb.Append(ScoutingReport.HandsReport((int)pr.Hands_Rating, app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING, bFirst, bLast));
                                    break;
                                case "PassBlocking":
                                    sb.Append(ScoutingReport.PassBlockingReport((int)pr.Pass_Block_Rating, app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING, bFirst, bLast));
                                    break;
                                case "RunBlocking":
                                    sb.Append(ScoutingReport.RunBlockingReport((int)pr.Run_Block_Rating, app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING, bFirst, bLast));
                                    break;
                            }
                            i++;
                        }


                        break;
                    }

                case Player_Pos.OL:
                    {
                        Player_Attributes.Add("PassBlocking");
                        Player_Attributes.Add("RunBlocking");

                        Player_Attributes = CommonUtils.ShufleList(Player_Attributes);

                        int i = 0;
                        bool bFirst = false;
                        bool bLast = false;
                        bool bNexttoLast = false;
                        foreach (string pa in Player_Attributes)
                        {
                            if (i == 0)
                                bFirst = true;
                            else
                                bFirst = false;

                            if (i == Player_Attributes.Count() - 1)
                                bLast = true;
                            else
                                bLast = false;

                            switch (pa)
                            {
                                case "PassBlocking":
                                    sb.Append(ScoutingReport.PassBlockingReport((int)pr.Pass_Block_Rating, app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING, bFirst, bLast));
                                    break;
                                case "RunBlocking":
                                    sb.Append(ScoutingReport.RunBlockingReport((int)pr.Run_Block_Rating, app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING, bFirst, bLast));
                                    break;
                            }
                            i++;
                        }


                        break;
                    }

                case Player_Pos.DL:
                    {
                        Player_Attributes.Add("PassRushing");
                        Player_Attributes.Add("RunStopping");

                        Player_Attributes = CommonUtils.ShufleList(Player_Attributes);

                        int i = 0;
                        bool bFirst = false;
                        bool bLast = false;
                        bool bNexttoLast = false;
                        foreach (string pa in Player_Attributes)
                        {
                            if (i == 0)
                                bFirst = true;
                            else
                                bFirst = false;

                           if (i == Player_Attributes.Count() - 1)
                                bLast = true;
                            else
                                bLast = false;

                            switch (pa)
                            {
                                case "PassRushing":
                                    sb.Append(ScoutingReport.PassRushingReport((int)pr.Pass_Attack, app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING, bFirst, bLast));
                                    break;
                                case "RunStopping":
                                    sb.Append(ScoutingReport.RunStoppingReport((int)pr.Run_Attack, app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING, bFirst, bLast));
                                    break;
                            }
                            i++;
                        }

                        break;
                    }

                case Player_Pos.DB:
                    {
                        Player_Attributes.Add("Spped");
                        Player_Attributes.Add("Agility");
                        Player_Attributes.Add("Hands");
                        Player_Attributes.Add("tackling");

                        Player_Attributes = CommonUtils.ShufleList(Player_Attributes);

                        int i = 0;
                        bool bFirst = false;
                        bool bLast = false;
                        bool bNexttoLast = false;
                        foreach (string pa in Player_Attributes)
                        {
                            if (i == 0)
                                bFirst = true;
                            else
                                bFirst = false;

                            if (i == Player_Attributes.Count() - 1)
                                bLast = true;
                            else
                                bLast = false;

                            switch (pa)
                            {
                                case "Spped":
                                    sb.Append(ScoutingReport.SpeedReport((int)pr.Speed_Rating, app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING, bFirst, bLast));
                                    break;
                                case "Agility":
                                    sb.Append(ScoutingReport.AgilityReport((int)pr.Agilty_Rating, app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING, bFirst, bLast));
                                    break;
                                case "Hands":
                                    sb.Append(ScoutingReport.HandsReport((int)pr.Hands_Rating, app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING, bFirst, bLast));
                                    break;
                                case "tackling":
                                    sb.Append(ScoutingReport.TackleReport((int)pr.Tackle_Rating, app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING, bFirst, bLast));
                                    break;
                            }
                            i++;
                        }


                        break;
                    }


                case Player_Pos.LB:
                    {
                        Player_Attributes.Add("Spped");
                        Player_Attributes.Add("Agility");
                        Player_Attributes.Add("tackling");
                        Player_Attributes.Add("PassRushing");
                        Player_Attributes.Add("RunStopping");


                        Player_Attributes = CommonUtils.ShufleList(Player_Attributes);

                        int i = 0;
                        bool bFirst = false;
                        bool bLast = false;
                        bool bNexttoLast = false;
                        foreach (string pa in Player_Attributes)
                        {
                            if (i == 0)
                                bFirst = true;
                            else
                                bFirst = false;

                            if (i == Player_Attributes.Count() - 1)
                                bLast = true;
                            else
                                bLast = false;

                            switch (pa)
                            {
                                case "Spped":
                                    sb.Append(ScoutingReport.SpeedReport((int)pr.Speed_Rating, app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING, bFirst, bLast));
                                    break;
                                case "Agility":
                                    sb.Append(ScoutingReport.AgilityReport((int)pr.Agilty_Rating, app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING, bFirst, bLast));
                                    break;
                                case "tackling":
                                    sb.Append(ScoutingReport.TackleReport((int)pr.Tackle_Rating, app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING, bFirst, bLast));
                                    break;
                                case "PassRushing":
                                    sb.Append(ScoutingReport.PassRushingReport((int)pr.Pass_Attack, app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING, bFirst, bLast));
                                    break;
                                case "RunStopping":
                                    sb.Append(ScoutingReport.RunStoppingReport((int)pr.Run_Attack, app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING, bFirst, bLast));
                                    break;
                            }
                            i++;
                        }


                        break;
                    }


                case Player_Pos.K:
                    {
                        Player_Attributes.Add("legstrength");
                        Player_Attributes.Add("kickeraccuracy");

                        Player_Attributes = CommonUtils.ShufleList(Player_Attributes);

                        int i = 0;
                        bool bFirst = false;
                        bool bLast = false;
                        bool bNexttoLast = false;
                        foreach (string pa in Player_Attributes)
                        {
                            if (i == 0)
                                bFirst = true;
                            else
                                bFirst = false;

                            if (i == Player_Attributes.Count() - 1)
                                bLast = true;
                            else
                                bLast = false;

                            switch (pa)
                            {
                                case "legstrength":
                                    sb.Append(ScoutingReport.LegPowerReport((int)pr.Kicker_Leg_Power, app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING, bFirst, bLast));
                                    break;
                                case "kickeraccuracy":
                                    sb.Append(ScoutingReport.LegAccuracyKReport((int)pr.Kicker_Leg_Accuracy, app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING, bFirst, bLast));
                                    break;

                            }
                            i++;
                        }


                        break;
                    }

                case Player_Pos.P:
                    {
                        Player_Attributes.Add("legstrength");
                        Player_Attributes.Add("kickeraccuracy");

                        Player_Attributes = CommonUtils.ShufleList(Player_Attributes);

                        int i = 0;
                        bool bFirst = false;
                        bool bLast = false;
                        bool bNexttoLast = false;
                        foreach (string pa in Player_Attributes)
                        {
                            if (i == 0)
                                bFirst = true;
                            else
                                bFirst = false;

                            if (i == Player_Attributes.Count() - 1)
                                bLast = true;
                            else
                                bLast = false;

                            switch (pa)
                            {
                                case "legstrength":
                                    sb.Append(ScoutingReport.LegPowerReport((int)pr.Kicker_Leg_Power, app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING, bFirst, bLast));
                                    break;
                                case "kickeraccuracy":
                                    sb.Append(ScoutingReport.LegAccuracyPReport((int)pr.Kicker_Leg_Accuracy, app_Constants.PRIMARY_ABILITY_LOW_RATING, app_Constants.PRIMARY_ABILITY_HIGH_RATING, bFirst, bLast));
                                    break;

                            }
                            i++;
                        }


                        break;
                    }
            }

// When returning the value for the players draft profile, remove the isse where it ends in , and
            return sb.ToString().Replace(",  and ", " and ");
        }

    }
}
