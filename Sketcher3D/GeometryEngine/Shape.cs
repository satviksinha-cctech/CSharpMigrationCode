using System.IO;

namespace Sketcher3D.GeometryEngine
{
    internal abstract class Shape
    {
        private string mType;
        private string mName;

        protected Triangulation mTriangulation = new Triangulation();

        protected Shape(string type, string name)
        {
            mType = type;
            mName = name;
        }

        protected abstract void Build();

        public Triangulation GetTriangulation()
        {
            return mTriangulation;
        }

        public string GetShapeName() => mName;
        public string GetShapeType() => mType;

        public abstract void Save(StreamWriter writer);
        public abstract void SaveForGNU(StreamWriter writer);
    }
}
