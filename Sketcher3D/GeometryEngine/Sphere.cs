using System;
using System.Collections.Generic;
using System.IO;

namespace Sketcher3D.GeometryEngine
{
    internal class Sphere : Shape
    {
        private double mRadius;

        public Sphere(string name, double radius)
            : base("Sphere", name)
        {
            this.mRadius = radius;
            Build();
        }

        public double GetRadius() => mRadius;

        protected override void Build()
        {
            int stacks = 36;
            int slices = 72;

            for (int i = 0; i < stacks; i++)
            {
                double lat1 = Math.PI * (-0.5 + (double)i / stacks);
                double lat2 = Math.PI * (-0.5 + (double)(i + 1) / stacks);

                double z1 = mRadius * Math.Sin(lat1);
                double r1 = mRadius * Math.Cos(lat1);

                double z2 = mRadius * Math.Sin(lat2);
                double r2 = mRadius * Math.Cos(lat2);

                for (int j = 0; j < slices; j++)
                {
                    double lon1 = 2 * Math.PI * j / slices;
                    double lon2 = 2 * Math.PI * (j + 1) / slices;

                    // First ring
                    int i1 = mTriangulation.GetPointIndex(new Point(r1 * Math.Cos(lon1), r1 * Math.Sin(lon1), z1));
                    int i2 = mTriangulation.GetPointIndex(new Point(r1 * Math.Cos(lon2), r1 * Math.Sin(lon2), z1));

                    // Second ring
                    int i3 = mTriangulation.GetPointIndex(new Point(r2 * Math.Cos(lon1), r2 * Math.Sin(lon1), z2));
                    int i4 = mTriangulation.GetPointIndex(new Point(r2 * Math.Cos(lon2), r2 * Math.Sin(lon2), z2));

                    // Two triangles per quad
                    mTriangulation.AddTriangle(i1, i2, i3);
                    mTriangulation.AddTriangle(i2, i4, i3);
                }
            }
        }

        public override void Save(StreamWriter writer)
        {
            writer.WriteLine($"{GetShapeType()} {GetShapeName()} R {mRadius}");
        }

        public override void SaveForGNU(StreamWriter writer)
        {
            int steps = 72;

            List<List<Point>> rings = new List<List<Point>>();

            double dTheta = Math.PI / steps;       
            double dPhi = 2 * Math.PI / steps;     

            for (int i = 0; i <= steps; i++)
            {
                double theta = i * dTheta;
                List<Point> row = new List<Point>();

                for (int j = 0; j <= steps; j++)
                {
                    double phi = j * dPhi;

                    double x = mRadius * Math.Sin(theta) * Math.Cos(phi);
                    double y = mRadius * Math.Sin(theta) * Math.Sin(phi);
                    double z = mRadius * Math.Cos(theta);

                    row.Add(new Point(x, y, z));
                }

                
                row.Add(new Point(
                    mRadius * Math.Sin(theta),
                    0,
                    mRadius * Math.Cos(theta)
                ));

                rings.Add(row);
            }

            
            foreach (var row in rings)
            {
                foreach (var pt in row)
                    pt.WriteXYZ(writer);

                writer.Write("\n\n");
            }

            writer.Write("\n\n");
        }
    }
}
