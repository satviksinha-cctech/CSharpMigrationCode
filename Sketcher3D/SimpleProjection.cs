using System;
using Sketcher3D.GeometryEngine;

namespace Sketcher3D
{
    internal static class SimpleProjection
    {
        public static Point RotateY(Point p, double angle)
        {
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);

            double x = p.GetX() * cos + p.GetZ() * sin;
            double z = -p.GetX() * sin + p.GetZ() * cos;

            return new Point(x, p.GetY(), z);
        }

        public static Point Project(Point p, double scale, double cx, double cy)
        {
            double sx = p.GetX() * scale + cx;
            double sy = -p.GetY() * scale + cy;
            return new Point(sx, sy, 0);
        }
    }
}
