﻿using SpectatorFootball.Common;
using SpectatorFootball.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.GameNS
{
    public class PointPlotter
    {
        private static int BALL_NORMAL_SKIP = 12;
        private static int BALL_SLOW_SKIP = 8;
//        private static int BALL_SLOW_SKIP = 16; 
        private static int PLAYER_SKIP = 8;
        private static int STARTING_KICK_SKIP = 24;
        private static int ENDING_KICK_SKIP = 6;

        public static List<PointXY> PlotLine (bool bBall,double sx, double sy, double ex, double ey, bool addEndpoint, Ball_Speed? Ball_Speed, Ball_States? b_state)
        {
            List<PointXY> r = new List<PointXY>();

            int quantity;
            int p1X;
            int p1Y;

            int p2X;
            int p2Y;

            int skip_count;
            if (bBall)
            {
                if ((b_state == Ball_States.END_OVER_END || b_state == Ball_States.SPIRAL))
                    skip_count = STARTING_KICK_SKIP;
                else if (Ball_Speed == Enum.Ball_Speed.SLOW)
                    skip_count = BALL_SLOW_SKIP;
                else
                    skip_count = BALL_NORMAL_SKIP;
            }
            else
                skip_count = PLAYER_SKIP;

            p1X = (int)(sx * 10.0);
            p1Y = (int)(sy * 10.0);

            p2X = (int)(ex * 10.0);
            p2Y = (int)(ey * 10.0);

            int ydiff = p2Y - p1Y;
            int xdiff = p2X - p1X;

            if (Math.Abs(xdiff) > Math.Abs(ydiff))
                quantity = Math.Abs(xdiff);
            else
                quantity = Math.Abs(ydiff);

            double slope = (double)(p2Y - p1Y) / (p2X - p1X);
            double x, y;

            int mid_point = quantity / 2;

            for (double i = 0; i < quantity; i++)
            {
                y = slope == 0 ? 0 : ydiff * (i / quantity);
                x = slope == 0 ? xdiff * (i / quantity) : y / slope;
                int new_x = (int)Math.Round(x) + p1X;
                int new_y = (int)Math.Round(y) + p1Y;

                if ((i + 1) % skip_count > 0)
                    continue;

                if (bBall && (b_state == Ball_States.END_OVER_END || b_state == Ball_States.SPIRAL))
                {
                    if (i < mid_point)
                        skip_count--;
                    else
                        skip_count++;

                    if (skip_count > STARTING_KICK_SKIP)
                        skip_count = STARTING_KICK_SKIP;

                    if (skip_count < ENDING_KICK_SKIP)
                        skip_count = ENDING_KICK_SKIP;
                }

                r.Add(new PointXY() { x = new_x / 10.0, y = new_y / 10.0 });
            }

            if (addEndpoint)
                r.Add(new PointXY() { x = ex, y = ey });

            return r;
        }
        public static double calcLineLength(double x1, double y1, double x2, double y2)
        {
            double r;

            double xDiff = Math.Abs(x1 - x2);
            double yDiff = Math.Abs(y1 - y2);

            double xDiff_Squared = Math.Pow(xDiff, 2);
            double yDiff_Squared = Math.Pow(yDiff, 2);

            r = Math.Sqrt(xDiff_Squared + yDiff_Squared);

            return r;

        }


    }
}
