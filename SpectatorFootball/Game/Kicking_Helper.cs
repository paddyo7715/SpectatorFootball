using log4net;
using log4net.Repository.Hierarchy;
using SpectatorFootball.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.GameNS
{
    public class Kicking_Helper
    {
        private static ILog logger = LogManager.GetLogger("RollingFile");
        public static double AdjustKickLength(double len, double vert)
        {
            double r;
            double a;
            if (vert >= 20.0 && vert <= 80.0)
                a = Math.Abs(vert - 50.0) * app_Constants.OFFCENTER_YARDS_LESS;
            else
                a = 5;
            r = len - a;

            return r;
        }
        public static double SetMaxKickoffYardline(double yt)
        {
            double r = yt;

            if (yt > app_Constants.KICKOFF_MAX_YARDLINE_1)
                r = app_Constants.KICKOFF_MAX_YARDLINE_1;
            else if (yt > 108.0)
                r = 108.0;
            else if (yt < app_Constants.KICKOFF_MAX_YARDLINE_2)
                r = app_Constants.KICKOFF_MAX_YARDLINE_2;
            else if (yt < -8.0)
                r = -8.9;

            return r;
        }

        public static KickOff_Length getKickOff_Dynamic_Len_enum(long leg_strength, long leg_acc)
        {
            KickOff_Length r = KickOff_Length.SUPER_LONG;
            long ls_temp = app_Constants.KICKOFF_LENGTH_CALC_VARIABLE - leg_strength;

            int i = 0;
            for (i = 1; i <= 5; i++)
            {

                long ls_var = ls_temp;
                if (i == 1 || i == 2)
                    ls_var = (long)(ls_var / 2.5);
                else if (i == 3)
                    ls_var = (long)(ls_var * 3);
                else if (i == 4)
                    ls_var = (long)(ls_var * 3);

                int r_num = CommonUtils.getRandomNum(1, app_Constants.KICKOFF_LENGTH_CALC_VARIABLE);
                if (r_num <= ls_var)
                    break;
            }

            switch (i)
            {
                case 1:
                    r = KickOff_Length.SUPER_SHORT;
                    break;
                case 2:
                    r = KickOff_Length.SHORT;
                    break;
                case 3:
                    r = KickOff_Length.AVERAGE;
                    break;
                case 4:
                    r = KickOff_Length.LONG;
                    break;
                case 5:
                    r = KickOff_Length.SUPER_LONG;
                    break;
            }

            return r;

        }
        public static KickOff_Length getKickOff_Len_enum(long leg_strength)
        {
            KickOff_Length r = KickOff_Length.SUPER_LONG;
            long ls_temp = app_Constants.KICKOFF_LENGTH_CALC_VARIABLE - leg_strength;

            int i = 0;
            for (i = 1; i <= 5; i++)
            {

                long ls_var = ls_temp;
                if (i == 1 || i == 2)
                    ls_var = (long) (ls_var / 2.5);
                else if (i == 4)
                    ls_var = (long) (ls_var * 3);

                int r_num = CommonUtils.getRandomNum(1, app_Constants.KICKOFF_LENGTH_CALC_VARIABLE);
                if (r_num <= ls_var)
                    break;
            }

            switch (i)
            {
                case 1:
                    r = KickOff_Length.SUPER_SHORT;
                    break;
                case 2:
                    r = KickOff_Length.SHORT;
                    break;
                case 3:
                    r = KickOff_Length.AVERAGE;
                    break;
                case 4:
                    r = KickOff_Length.LONG;
                    break;
                case 5:
                    r = KickOff_Length.SUPER_LONG;
                    break;
            }

            return r;

        }
        public static Kickoff_Verticl getKickoff_Vert_enum(long leg_Accuracy)
        {
            bool bNot_Straight;

            int not_straight = (int)leg_Accuracy - app_Constants.PRIMARY_ABILITY_LOW_RATING;
            if (not_straight <= 0)
                bNot_Straight = true;
            else
            {
                int acc_variable = CommonUtils.getRandomNum(1, app_Constants.KICKOFF_ACC_CALC_VARIABLE);
                if (acc_variable <= not_straight)
                    bNot_Straight = true;
                else
                    bNot_Straight = false;
            }

            if (bNot_Straight)
            {
                int r_num = CommonUtils.getRandomNum(1, 2);
                if (r_num == 1)
                    return Kickoff_Verticl.TOP;
                else
                    return Kickoff_Verticl.BOTTOM;
            }
            else
                return Kickoff_Verticl.MIDDLE;

        }
        public static double getKickoff_len(KickOff_Length kickoff_len_enum)
        {
            double r = 0.0;

            switch (kickoff_len_enum)
            {
                case KickOff_Length.SUPER_SHORT:
                    r = CommonUtils.getRandomNum(app_Constants.KICKOFF_MIN_SUPER_SHORT_DIST, app_Constants.KICKOFF_MAX_SUPER_SHORT_DIST);
                    break;
                case KickOff_Length.SHORT:
                    r = CommonUtils.getRandomNum(app_Constants.KICKOFF_MIN_SHORT_DISTANCE, app_Constants.KICKOFF_MAX_SHORT_DISTANCE);
                    break;
                case KickOff_Length.AVERAGE:
                    r = CommonUtils.getRandomNum(app_Constants.KICKOFF_MIN_AVG_DISTANCE, app_Constants.KICKOFF_MAX_AVG_DISTANCE);
                    break;
                case KickOff_Length.LONG:
                    r = CommonUtils.getRandomNum(app_Constants.KICKOFF_MIN_LONG_DISTANCE, app_Constants.KICKOFF_MAX_LONG_DISTANCE);
                    break;
                case KickOff_Length.SUPER_LONG:
                    r = CommonUtils.getRandomNum(app_Constants.KICKOFF_MIN_SUPER_LONG_DISTANCE, app_Constants.KICKOFF_MAX_SUPER_LONG_DISTANCE);
                    break;
            }

            return r;
        }

        public static double getKICKOFF_DYNAMIC_len(KickOff_Length KICKOFF_DYNAMIC_len_enum)
        {
            double r = 0.0;

            switch (KICKOFF_DYNAMIC_len_enum)
            {
                case KickOff_Length.SUPER_SHORT:
                    r = CommonUtils.getRandomNum(app_Constants.KICKOFF_DYNAMIC_MIN_SUPER_SHORT_DIST, app_Constants.KICKOFF_DYNAMIC_MAX_SUPER_SHORT_DIST);
                    break;
                case KickOff_Length.SHORT:
                    r = CommonUtils.getRandomNum(app_Constants.KICKOFF_DYNAMIC_MIN_SHORT_DISTANCE, app_Constants.KICKOFF_DYNAMIC_MAX_SHORT_DISTANCE);
                    break;
                case KickOff_Length.AVERAGE:
                    r = CommonUtils.getRandomNum(app_Constants.KICKOFF_DYNAMIC_MIN_AVG_DISTANCE, app_Constants.KICKOFF_DYNAMIC_MAX_AVG_DISTANCE);
                    break;
                case KickOff_Length.LONG:
                    r = CommonUtils.getRandomNum(app_Constants.KICKOFF_DYNAMIC_MIN_LONG_DISTANCE, app_Constants.KICKOFF_DYNAMIC_MAX_LONG_DISTANCE);
                    break;
                case KickOff_Length.SUPER_LONG:
                    r = CommonUtils.getRandomNum(app_Constants.KICKOFF_DYNAMIC_MIN_SUPER_LONG_DISTANCE, app_Constants.KICKOFF_DYNAMIC_MAX_SUPER_LONG_DISTANCE);
                    break;
            }

            return r;
        }
        public static double getKickoff_Vert(Kickoff_Verticl vert_enum)
        {
            double r = 0.0;

            switch (vert_enum)
            {
                case Kickoff_Verticl.TOP:
                    r = CommonUtils.getRandomNum(app_Constants.KICKOFF_TOP_MIN_VERTICAL, app_Constants.KICKOFF_TOP_AVG_VERTICAL);
                    break;
                case Kickoff_Verticl.MIDDLE:
                    r = CommonUtils.getRandomNum(app_Constants.KICKOFF_TOP_AVG_VERTICAL, app_Constants.KICKOFF_BOTTOM_AVG_VERTICAL);
                    break;
                case Kickoff_Verticl.BOTTOM:
                    r = CommonUtils.getRandomNum(app_Constants.KICKOFF_BOTTOM_AVG_VERTICAL, app_Constants.KICKOFF_BOTTOM_MAX_VERTICAL);
                    break;
            }

            return r;
        }

        public static Punt_Len getPunt_Len_enum(long leg_strength)
        {
            Punt_Len r = Punt_Len.LONG;
            long ls_temp = app_Constants.PUNT_LENGTH_CALC_VARIABLE - leg_strength;

            int i = 0;
            for (i = 1; i <= 3; i++)
            {

                long ls_var = ls_temp;
                int r_num = CommonUtils.getRandomNum(1, app_Constants.PUNT_LENGTH_CALC_VARIABLE);
                if (r_num <= ls_var)
                    break;
            }

            switch (i)
            {
                case 1:
                    r = Punt_Len.SHORT;
                    break;
                case 2:
                    r = Punt_Len.AVG;
                    break;
                case 3:
                    r = Punt_Len.LONG;
                    break;
            }

            return r;

        }

        public static double getPunt_len(Punt_Len Punt_len_enum)
        {
            double r = 0.0;

            switch (Punt_len_enum)
            {
                case Punt_Len.SHORT:
                    r = CommonUtils.getRandomNum(app_Constants.PUNT_MIN_SHORT_DISTANCE, app_Constants.PUNT_MAX_SHORT_DISTANCE);
                    break;
                case Punt_Len.AVG:
                    r = CommonUtils.getRandomNum(app_Constants.PUNT_MIN_AVG_DISTANCE, app_Constants.PUNT_MAX_AVG_DISTANCE);
                    break;
                case Punt_Len.LONG:
                    r = CommonUtils.getRandomNum(app_Constants.PUNT_MIN_LONG_DISTANCE, app_Constants.PUNT_MAX_LONG_DISTANCE);
                    break;
            }

            return r;
        }

        public static double getPunt_Vert(Punt_Vertical vert_enum)
        {
            double r = 0.0;

            switch (vert_enum)
            {
                case Punt_Vertical.TOP:
                    r = CommonUtils.getRandomNum(app_Constants.PUNT_TOP_MIN_VERTICAL, app_Constants.PUNT_TOP_AVG_VERTICAL);
                    break;
                case Punt_Vertical.MIDDLE:
                    r = CommonUtils.getRandomNum(app_Constants.PUNT_TOP_AVG_VERTICAL, app_Constants.PUNT_BOTTOM_AVG_VERTICAL);
                    break;
                case Punt_Vertical.BOTTOM:
                    r = CommonUtils.getRandomNum(app_Constants.PUNT_BOTTOM_AVG_VERTICAL, app_Constants.PUNT_BOTTOM_MAX_VERTICAL);
                    break;
            }

            return r;
        }

        public static Punt_Vertical getPunt_Vert_enum(long leg_Accuracy)
        {
            bool bNot_Straight;

            int not_straight = (int)leg_Accuracy - app_Constants.PRIMARY_ABILITY_LOW_RATING;
            if (not_straight <= 0)
                bNot_Straight = true;
            else
            {
                int acc_variable = CommonUtils.getRandomNum(1, app_Constants.PUNT_ACC_CALC_VARIABLE);
                if (acc_variable <= not_straight)
                    bNot_Straight = true;
                else
                    bNot_Straight = false;
            }

            if (bNot_Straight)
            {
                int r_num = CommonUtils.getRandomNum(1, 2);
                if (r_num == 1)
                    return Punt_Vertical.TOP;
                else
                    return Punt_Vertical.BOTTOM;
            }
            else
                return Punt_Vertical.MIDDLE;

        }
    }
}

