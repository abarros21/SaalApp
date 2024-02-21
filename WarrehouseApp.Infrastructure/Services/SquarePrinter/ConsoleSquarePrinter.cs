using WarehouseApp.Domain;
using WarrehouseApp.Infrastructure.Data.Interfaces.SquarePrinter;

namespace WarrehouseApp.Infrastructure.Services.SquarePrinter
{
    public class ConsoleSquarePrinter : IConsoleSquarePrinter
    {
        public void PrintSquares(List<Square> squares)
        {
            foreach (var square in squares)
            {
                Console.WriteLine($"Coordinate: ({square.Coordinate.X}, {square.Coordinate.Y}), Distance: {square.DistanceToInitPoint}");
            }
        }
    }
}
