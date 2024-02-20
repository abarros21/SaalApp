namespace WarehouseApp.Domain
{
    public class Coordinate(int x, int y)
    {
        public int X { get; } = x;
        public int Y { get; } = y;

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Coordinate other = (Coordinate)obj;
            return X == other.X && Y == other.Y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }
    }
}
