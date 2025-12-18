using System;
using System.IO;

namespace Sketcher3D.GeometryEngine
{
    internal class Point
    {
        private double mX;
        private double mY;
        private double mZ;

        public const double tolerance = 1e-6;

        public Point()
        {
            mX = 0;
            mY = 0;
            mZ = 0;
        }

        public Point(double x, double y, double z)
        {
            mX = x;
            mY = y;
            mZ = z;
        }

        public double GetX()
        {
            return mX;
        }

        public double GetY()
        {
            return mY;
        }

        public double GetZ()
        {
            return mZ;
        }

        public double X
        {
            get { return mX; }
        }

        public double Y
        {
            get { return mY; }
        }

        public double Z
        {
            get { return mZ; }
        }

        public void SetX(double x)
        {
            mX = x;
        }

        public void SetY(double y)
        {
            mY = y;
        }

        public void SetZ(double z)
        {
            mZ = z;
        }

        public double Distance(Point other)
        {
            return Math.Sqrt(
                (mX - other.mX) * (mX - other.mX) +
                (mY - other.mY) * (mY - other.mY) +
                (mZ - other.mZ) * (mZ - other.mZ)
            );
        }

        public void WriteXYZ(StreamWriter writer)
        {
            writer.Write(mX);
            writer.Write(" ");
            writer.Write(mY);
            writer.Write(" ");
            writer.Write(mZ);
        }

        public bool IsEqual(Point other)
        {
            return Math.Abs(mX - other.mX) < tolerance &&
                   Math.Abs(mY - other.mY) < tolerance &&
                   Math.Abs(mZ - other.mZ) < tolerance;
        }

        public override bool Equals(object obj)
        {
            Point other = obj as Point;
            if (other == null)
                return false;

            return IsEqual(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + mX.GetHashCode();
                hash = hash * 23 + mY.GetHashCode();
                hash = hash * 23 + mZ.GetHashCode();
                return hash;
            }
        }
    }
}
