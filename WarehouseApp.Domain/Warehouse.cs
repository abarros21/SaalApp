namespace WarehouseApp.Domain
{
    public class Warehouse
    {
        public List<Coordinate> Coordinates { get; }
        public Coordinate InitPoint { get; }

        public Warehouse(List<Coordinate> coordinates, Coordinate initPoint)
        {
            if (coordinates == null || coordinates.Count == 0)
            {
                throw new ArgumentNullException("Coordinates list cannot be null or empty.");
            }

            if (!coordinates.Contains(initPoint))
            {
                throw new ArgumentException("Init point must be included in coordinates list.");
            }

            if (!coordinates.All(coord => HasAdjacent(coord, coordinates)))
            {
                throw new ArgumentException("All coordinates must be adjacent to at least one other coordinate.");
            }

            Coordinates = coordinates;
            InitPoint = initPoint;
        }


        private static bool HasAdjacent(Coordinate point, List<Coordinate> coordinates)
        {
            return coordinates.Any(otherPoint => AreAdjacent(point, otherPoint));
        }

        private static bool AreAdjacent(Coordinate point1, Coordinate point2)
        {
            return Math.Abs(point1.X - point2.X) + Math.Abs(point1.Y - point2.Y) == 1;
        }
    }
}
