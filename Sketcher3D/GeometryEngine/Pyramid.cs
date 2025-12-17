using System;
using System.Collections.Generic;
using System.IO;

namespace Sketcher3D.GeometryEngine
{
    internal class Pyramid : Shape
    {
        private double mLength;
        private double mWidth;
        private double mHeight;

        public Pyramid(string name, double length, double width, double height)
            : base("Pyramid", name)
        {
            mLength = length;
            mWidth = width;
            mHeight = height;
            Build();
        }

        protected override void Build()
        {
            double L = mLength;
            double W = mWidth;
            double H = mHeight;

            int p0 = mTriangulation.GetPointIndex(new Point(0, 0, 0));
            int p1 = mTriangulation.GetPointIndex(new Point(L, 0, 0));
            int p2 = mTriangulation.GetPointIndex(new Point(L, W, 0));
            int p3 = mTriangulation.GetPointIndex(new Point(0, W, 0));
            int p4 = mTriangulation.GetPointIndex(new Point(L / 2, W / 2, H));

            mTriangulation.AddTriangle(p0, p2, p1);
            mTriangulation.AddTriangle(p0, p3, p2);

            mTriangulation.AddTriangle(p0, p1, p4);
            mTriangulation.AddTriangle(p1, p2, p4);
            mTriangulation.AddTriangle(p2, p3, p4);
            mTriangulation.AddTriangle(p3, p0, p4);
        }

        public override void Save(StreamWriter writer)
        {
            writer.WriteLine($"{GetShapeType()} {GetShapeName()} L {mLength} W {mWidth} H {mHeight}");
        }

        public override void SaveForGNU(StreamWriter writer)
        {
            double L = mLength;
            double W = mWidth;
            double H = mHeight;

            List<List<Point>> lines = new List<List<Point>>
            {
                new List<Point>
                {
                    new Point(0,0,0), new Point(L,0,0),
                    new Point(L,W,0), new Point(0,W,0),
                    new Point(0,0,0)
                },
                new List<Point> { new Point(0,0,0), new Point(L/2,W/2,H) },
                new List<Point> { new Point(L,0,0), new Point(L/2,W/2,H) },
                new List<Point> { new Point(L,W,0), new Point(L/2,W/2,H) },
                new List<Point> { new Point(0,W,0), new Point(L/2,W/2,H) }
            };

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
