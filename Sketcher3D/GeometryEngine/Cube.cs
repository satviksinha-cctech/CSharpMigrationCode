using System;
using System.Collections.Generic;
using System.IO;

namespace Sketcher3D.GeometryEngine
{
    internal class Cube : Shape
    {
        private double mSide;

        public Cube(string name, double side)
            : base("Cube", name)
        {
            mSide = side;
            Build();
        }

        public double GetSide() => mSide;

        protected override void Build()
        {
            double s = mSide;

            int p0 = mTriangulation.GetPointIndex(new Point(0, 0, 0));
            int p1 = mTriangulation.GetPointIndex(new Point(s, 0, 0));
            int p2 = mTriangulation.GetPointIndex(new Point(s, s, 0));
            int p3 = mTriangulation.GetPointIndex(new Point(0, s, 0));

            int p4 = mTriangulation.GetPointIndex(new Point(0, 0, s));
            int p5 = mTriangulation.GetPointIndex(new Point(s, 0, s));
            int p6 = mTriangulation.GetPointIndex(new Point(s, s, s));
            int p7 = mTriangulation.GetPointIndex(new Point(0, s, s));

            mTriangulation.AddTriangle(p0, p2, p1);
            mTriangulation.AddTriangle(p0, p3, p2);

            mTriangulation.AddTriangle(p4, p5, p6);
            mTriangulation.AddTriangle(p4, p6, p7);

            mTriangulation.AddTriangle(p7, p6, p2);
            mTriangulation.AddTriangle(p7, p2, p3);

            mTriangulation.AddTriangle(p0, p1, p5);
            mTriangulation.AddTriangle(p0, p5, p4);

            mTriangulation.AddTriangle(p5, p1, p2);
            mTriangulation.AddTriangle(p5, p2, p6);

            mTriangulation.AddTriangle(p0, p4, p7);
            mTriangulation.AddTriangle(p0, p7, p3);
        }

        public override void Save(StreamWriter writer)
        {
            writer.WriteLine($"{GetShapeType()} {GetShapeName()} S {mSide}");
        }

        public override void SaveForGNU(StreamWriter writer)
        {
            double s = mSide;
            List<List<Point>> lines = new List<List<Point>>();

            lines.Add(new List<Point>
            {
                new Point(0, 0, 0),
                new Point(s, 0, 0),
                new Point(s, s, 0),
                new Point(0, s, 0),
                new Point(0, 0, 0)
            });

            lines.Add(new List<Point>
            {
                new Point(0, 0, s),
                new Point(s, 0, s),
                new Point(s, s, s),
                new Point(0, s, s),
                new Point(0, 0, s)
            });

            lines.Add(new List<Point> { new Point(0, 0, 0), new Point(0, 0, s) });
            lines.Add(new List<Point> { new Point(s, 0, 0), new Point(s, 0, s) });
            lines.Add(new List<Point> { new Point(s, s, 0), new Point(s, s, s) });
            lines.Add(new List<Point> { new Point(0, s, 0), new Point(0, s, s) });

            foreach (var row in lines)
            {
                foreach (var p in row)
                    p.WriteXYZ(writer);

                writer.Write("\n\n");
            }

            writer.Write("\n\n");
        }
    }
}
