using System;
using System.Collections.Generic;

namespace Sketcher3D.GeometryEngine
{
    internal static class Transformations
    {
        public static List<Point> RotateX(List<Point> points, double angle)
        {
            var result = new List<Point>();
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);

            foreach (var p in points)
            {
                double y = p.GetY() * cos - p.GetZ() * sin;
                double z = p.GetY() * sin + p.GetZ() * cos;
                result.Add(new Point(p.GetX(), y, z));
            }
            return result;
        }

        public static List<Point> RotateY(List<Point> points, double angle)
        {
            var result = new List<Point>();
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);

            foreach (var p in points)
            {
                double x = p.GetX() * cos + p.GetZ() * sin;
                double z = -p.GetX() * sin + p.GetZ() * cos;
                result.Add(new Point(x, p.GetY(), z));
            }
            return result;
        }
    }
}
