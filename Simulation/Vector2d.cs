using System;

namespace Simulation
{
    class Vector2d
    {
        private double x;
        private double y;

        public Vector2d(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public double X
        {
            get { return x; }
            set { this.x = value; }
        }

        public double Y
        {
            get { return y; }
            set { this.y = value; }
        }

        public static Vector2d operator +(Vector2d v1, Vector2d v2)
        {
            Vector2d result = new Vector2d(x: v1.X + v2.X, y: v1.Y + v2.Y);
            return result;
        }

        public double DistanceTo(Vector2d vector)
        {
            double xDiff = vector.X - this.x;
            double yDiff = vector.Y - this.y;

            double distance = Math.Sqrt(xDiff * xDiff + yDiff * yDiff);
            return distance;
        }

        public override string ToString()
        {
            return x.ToString() + ";" + y.ToString();
        }
    }
}
