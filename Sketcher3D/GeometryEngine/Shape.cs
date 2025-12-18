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

        public string GetShapeName()
        {
            return mName;
        }

        public string GetShapeType()
        {
            return mType;
        }

        public Triangulation GetTriangulation()
        {
            return mTriangulation;
        }

        public Triangulation Triangulation
        {
            get { return mTriangulation; }
        }

        protected abstract void Build();

        public abstract void Save(StreamWriter writer);
        public abstract void SaveForGNU(StreamWriter writer);
    }
}
