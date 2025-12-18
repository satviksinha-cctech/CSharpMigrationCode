namespace Sketcher3D.GeometryEngine
{
    internal class Triangle
    {
        public int m1;
        public int m2;
        public int m3;

        public Triangle(int a, int b, int c)
        {
            m1 = a;
            m2 = b;
            m3 = c;
        }

        public int A
        {
            get { return m1; }
        }

        public int B
        {
            get { return m2; }
        }

        public int C
        {
            get { return m3; }
        }
    }
}
