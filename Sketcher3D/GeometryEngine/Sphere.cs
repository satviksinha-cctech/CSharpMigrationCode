using System;

namespace Sketcher3D.GeometryEngine
{
    internal class Sphere : Shape
    {
        private double radius;
        private int slices = 24;

        public Sphere(string name, double radius)
            : base("Sphere", name)
        {
            this.radius = radius;
            Build();
        }

        protected override void Build()
        {
            mTriangulation = new Triangulation();

            int center = mTriangulation.GetPointIndex(new Point(0, 0, 0));

            for (int i = 0; i < slices; i++)
            {
                double a1 = 2 * Math.PI * i / slices;
                double a2 = 2 * Math.PI * (i + 1) / slices;

                Point p1 = new Point(radius * Math.Cos(a1), radius * Math.Sin(a1), 0);
                Point p2 = new Point(radius * Math.Cos(a2), radius * Math.Sin(a2), 0);

                int i1 = mTriangulation.GetPointIndex(p1);
                int i2 = mTriangulation.GetPointIndex(p2);

                mTriangulation.AddTriangle(center, i1, i2);
            }
        }

        public override void Save(System.IO.StreamWriter writer) { }
        public override void SaveForGNU(System.IO.StreamWriter writer) { }
    }
}
