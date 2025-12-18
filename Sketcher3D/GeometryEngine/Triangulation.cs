using System;
using System.Collections.Generic;

namespace Sketcher3D.GeometryEngine
{
    internal class Triangulation
    {
        private readonly List<Point> _points = new List<Point>();
        private readonly List<Triangle> _triangles = new List<Triangle>();

        private readonly Dictionary<Point, int> _pointIndex =
            new Dictionary<Point, int>();

        public IReadOnlyList<Point> GetPoints()
        {
            return _points;
        }

        public IReadOnlyList<Triangle> GetTriangles()
        {
            return _triangles;
        }

        public int GetPointIndex(Point p)
        {
            if (_pointIndex.TryGetValue(p, out int index))
                return index;

            index = _points.Count;
            _points.Add(p);
            _pointIndex[p] = index;
            return index;
        }

        public void AddTriangle(int a, int b, int c)
        {
            _triangles.Add(new Triangle(a, b, c));
        }

        public List<double> GetPointDoubleData()
        {
            List<double> data = new List<double>();

            foreach (var tri in _triangles)
            {
                Point p1 = _points[tri.A];
                Point p2 = _points[tri.B];
                Point p3 = _points[tri.C];

                data.Add(p1.GetX()); data.Add(p1.GetY()); data.Add(p1.GetZ());
                data.Add(p2.GetX()); data.Add(p2.GetY()); data.Add(p2.GetZ());
                data.Add(p3.GetX()); data.Add(p3.GetY()); data.Add(p3.GetZ());
            }

            return data;
        }
    }
}
