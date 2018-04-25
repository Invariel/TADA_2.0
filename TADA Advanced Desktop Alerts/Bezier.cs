using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TADA_Advanced_Desktop_Alerts
{
    static class Bezier
    {
        public static void CalculateLinearControlPoints(PointF p0, PointF p1, out PointF c0, out PointF c1)
        {
            float c0x, c0y, c1x, c1y;

            c0x = (p1.X - p0.X) / 3 + p0.X;
            c0y = (p1.Y - p0.Y) / 3 + p0.Y;
            c1x = (p1.X - p0.X) * 2 / 3 + p0.X;
            c1y = (p1.Y - p0.Y) * 2 / 3 + p0.Y;

            c0 = new PointF(c0x, c0y);
            c1 = new PointF(c1x, c1y);
        }

        public static PointF CalculateBezierPosition(PointF p0, PointF c0, PointF c1, PointF p1, double t)
        {
            return new PointF(CalculateBezierCoordinate((float)p0.X, (float)c0.X, (float)c1.X, (float)p1.X, (float)t),
                              CalculateBezierCoordinate((float)p0.Y, (float)c0.Y, (float)c1.Y, (float)p1.Y, (float)t));
        }

        public static float CalculateBezierCoordinate(double p0, double c0, double c1, double p1, float t)
        {
            float oneminust = 1 - t;

            return (float)(1 * Math.Pow(oneminust, 3) * Math.Pow(t, 0) * p0 +
                           3 * Math.Pow(oneminust, 2) * Math.Pow(t, 1) * c0 +
                           3 * Math.Pow(oneminust, 1) * Math.Pow(t, 2) * c1 +
                           1 * Math.Pow(oneminust, 0) * Math.Pow(t, 3) * p1);
        }
    }
}
