namespace Simulation
{
    class WallBounce
    {
        public static Vector2d GetWallReflectionVector(Vector2d vector, Orientation orient)
        {
            Vector2d finalVector = vector;
            switch (orient)
            {
                case Orientation.Vertical:
                    finalVector = new Vector2d(-vector.X, vector.Y);
                    break;
                case Orientation.Horizontal:
                    finalVector = new Vector2d(vector.X, -vector.Y);
                    break;

            }

            return finalVector;
        }

        public enum Orientation
        {
            Vertical, Horizontal, None
        }
    }
}
