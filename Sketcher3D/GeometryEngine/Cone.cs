using System;
using System.Collections.Generic;
using System.IO;

namespace Sketcher3D.GeometryEngine
{
    internal class Cone : Shape
    {
        private double mRadius;
        private double mHeight;

        public Cone(string name, double radius, double height)
            : base("Cone", name)
        {
            mRadius = radius;
            mHeight = height;
            Build();
        }

        protected override void Build()
        {
            int slices = 36;
            int apex = mTriangulation.GetPointIndex(new Point(0, 0, mHeight));

            for (int i = 0; i < slices; i++)
            {
                double a1 = 2 * Math.PI * i / slices;
                double a2 = 2 * Math.PI * (i + 1) / slices;

                int p1 = mTriangulation.GetPointIndex(new Point(mRadius * Math.Cos(a1), mRadius * Math.Sin(a1), 0));
                int p2 = mTriangulation.GetPointIndex(new Point(mRadius * Math.Cos(a2), mRadius * Math.Sin(a2), 0));

                mTriangulation.AddTriangle(p1, p2, apex);
            }
        }

        public override void Save(StreamWriter writer)
        {
            writer.WriteLine($"{GetShapeType()} {GetShapeName()} R {mRadius} H {mHeight}");
        }

        public override void SaveForGNU(StreamWriter writer)
        {
            int slices = 36;

            for (int i = 0; i <= slices; i++)
            {
                double a = 2 * Math.PI * i / slices;
                new Point(mRadius * Math.Cos(a), mRadius * Math.Sin(a), 0).WriteXYZ(writer);
                new Point(0, 0, mHeight).WriteXYZ(writer);
                writer.Write("\n\n");
            }

            writer.Write("\n\n");
        }
    }
}
