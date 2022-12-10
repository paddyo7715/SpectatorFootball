using SpectatorFootball.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.GameNS
{
    public class PointPlotter
    {
        private static int BALL_SKIP = 6;
        private static int PLAYER_SKIP = 2;

        public static List<PointXY> PlotLine (bool bBall,double sx, double sy, double ex, double ey, bool addEndpoint)
        {
            List<PointXY> r = new List<PointXY>();

            int quantity;
            int p1X;
            int p1Y;

            int p2X;
            int p2Y;

            int skip_count = bBall ? BALL_SKIP : PLAYER_SKIP;

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

            for (double i = 0; i < quantity; i++)
            {

                y = slope == 0 ? 0 : ydiff * (i / quantity);
                x = slope == 0 ? xdiff * (i / quantity) : y / slope;
                int new_x = (int)Math.Round(x) + p1X;
                int new_y = (int)Math.Round(y) + p1Y;

                if ((i + 1) % skip_count == 0)
                    continue;

                r.Add(new PointXY() { x = new_x / 10.0, y = new_y / 10.0 });
            }

            if (addEndpoint)
                r.Add(new PointXY() { x = ex, y = ey });

            return r;
        }
    }
}
