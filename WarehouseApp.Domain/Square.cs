namespace WarehouseApp.Domain
{
    public class Square(Coordinate coordinate, int distancetoInitPoint)
    {
        public Coordinate Coordinate { get; } = coordinate;
        public int DistanceToInitPoint { get; } = distancetoInitPoint;
    }
}
