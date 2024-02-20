using WarehouseApp.Domain.Interfaces;

namespace WarehouseApp.Domain.Services
{
    public class SquareService : ISquareService
    {
        public List<Square> CalcDistances(Warehouse warehouse)
        {      
            List<Square> result = [];

            Dictionary<Coordinate, int> listCoords = [];
            Queue<Coordinate> queue = new();

            // Init point -> Distance = 0
            listCoords[warehouse.InitPoint] = 0;

            queue.Enqueue(warehouse.InitPoint);

            while (queue.Count > 0)
            {
                Coordinate current = queue.Dequeue();
                int currentDistance = listCoords[current];
         
                List<Coordinate> adjacentCoords = GetAdjacentCoordinates(current, warehouse);

                foreach (Coordinate adjacent in adjacentCoords)
                {
                    // If new coord or new distance < current one
                    if (!listCoords.ContainsKey(adjacent) || currentDistance + 1 < listCoords[adjacent])
                    {
                        listCoords[adjacent] = currentDistance + 1;
                        queue.Enqueue(adjacent);
                    }
                }
            }

            foreach (var kvp in listCoords)
            {
                result.Add(new Square(kvp.Key, kvp.Value));
            }

            return result;
        }

        private List<Coordinate> GetAdjacentCoordinates(Coordinate coordinate, Warehouse warehouse)
        {
            List<Coordinate> result = [];

            //Possible directions
            int[] deltaX = [0, 0, -1, 1];
            int[] deltaY = [-1, 1, 0, 0];

            foreach (var (dx, dy) in Enumerable.Zip(deltaX, deltaY))
            {
                int newX = coordinate.X + dx;
                int newY = coordinate.Y + dy;

                Coordinate newCoord = new(newX, newY);

                // Check if coordinate exists in warehouse
                if (IsValidCoordinate(newCoord, warehouse))
                {
                    result.Add(newCoord);
                }
            }

            return result;
        }

        private static bool IsValidCoordinate(Coordinate coordinate, Warehouse warehouse)
        {
            return warehouse.Coordinates.Contains(coordinate);
        }
    }
}
