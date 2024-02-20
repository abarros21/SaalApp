using WarehouseApp.Domain;
using WarrehouseApp.Infrastructure.DTOs;
using WarrehouseApp.Infrastructure.Interfaces;

namespace WarrehouseApp.Infrastructure.Services.SquarePrinter
{
    public class ApiSquarePrinter : IApiSquarePrinter
    {
        public List<SquareDto> PrintAndReturnSquareDtos(List<Square> squares)
        {
            List<SquareDto> squareDtos = [];

            foreach (var square in squares)
            {
                squareDtos.Add(new SquareDto
                {
                    X = square.Coordinate.X,
                    Y = square.Coordinate.Y,
                    Distance = square.DistanceToInitPoint
                });
            }

            return squareDtos;
        }
    }
}
