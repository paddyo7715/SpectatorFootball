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
        public double old_AdjustKickLength(bool bRgihttoLeft, double x, double y)
        {
            double r;
            double a;
            if (y >= 20.0 && y <= 90.0)
                a = Math.Abs(y - 50.0) * app_Constants.OFFCENTER_YARDS_LESS;
            else
                a = 20;

            if (bRgihttoLeft)
                a *= -1;

            r = y + a;

            return r;
        }

        public static double AdjustKickLength(double len, double vert)
        {
            double r;
            double a;
            if (vert >= 20.0 && vert <= 80.0)
                a = Math.Abs(vert - 50.0) * app_Constants.OFFCENTER_YARDS_LESS;
            else
                a = 20;
            r = len - a;

            return r;
        }
        public static double SetMaxKickoffYardline(double yt)
        {
            double r = yt;

            if (yt > app_Constants.KICKOFF_MAX_YARDLINE_1)
                r = app_Constants.KICKOFF_MAX_YARDLINE_1;
            else if (yt < app_Constants.KICKOFF_MAX_YARDLINE_2)
                r = app_Constants.KICKOFF_MAX_YARDLINE_2;

            return r;
        }
        public static KickOff_Length getKickOff_Len_enum(long leg_strength)
        {
            int long_variable = (int) leg_strength - app_Constants.PRIMARY_ABILITY_LOW_RATING;

            //If this is not a kicker then the kickoff should be very short
            if (long_variable <= 0)
                return KickOff_Length.SUPER_SHORT;

            int short_variable = app_Constants.PRIMARY_ABILITY_HIGH_RATING - (int)leg_strength + long_variable;
            int r_num = CommonUtils.getRandomNum(1, app_Constants.KICKOFF_LENGTH_CALC_VARIABLE);

            if (r_num <= long_variable)
                return KickOff_Length.LONG;
            else if (r_num <= short_variable)
                return KickOff_Length.SHORT;
            else
                return KickOff_Length.AVERAGE;

        }
        public static KickOff_Verticl getKickoff_Vert_enum(long leg_Accuracy)
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
                    return KickOff_Verticl.TOP;
                else
                    return KickOff_Verticl.BOTTOM;
            }
            else
                return KickOff_Verticl.MIDDLE;

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
            }

            return r;
        }
        public static double getKickoff_Vert(KickOff_Verticl vert_enum)
        {
            double r = 0.0;

            switch (vert_enum)
            {
                case KickOff_Verticl.TOP:
                    r = CommonUtils.getRandomNum(app_Constants.KICKOFF_TOP_MIN_VERTICAL, app_Constants.KICKOFF_TOP_AVG_VERTICAL);
                    break;
                case KickOff_Verticl.MIDDLE:
                    r = CommonUtils.getRandomNum(app_Constants.KICKOFF_TOP_AVG_VERTICAL, app_Constants.KICKOFF_BOTTOM_AVG_VERTICAL);
                    break;
                case KickOff_Verticl.BOTTOM:
                    r = CommonUtils.getRandomNum(app_Constants.KICKOFF_BOTTOM_AVG_VERTICAL, app_Constants.KICKOFF_BOTTOM_MAX_VERTICAL);
                    break;
            }

            return r;
        }
    }
}
