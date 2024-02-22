using WarehouseApp.Domain;
using WarrehouseApp.Infrastructure.Data.DTOs;
using WarrehouseApp.Infrastructure.Data.Interfaces.SquarePrinter;
using WarrehouseApp.Infrastructure.DTOs;

namespace WarrehouseApp.Infrastructure.Services.SquarePrinter
{
    public class ApiSquarePrinter : IApiSquarePrinter
    {
        public WarehouseDto PrintAndReturnWarehouseDTO(string key, List<Square> squares)
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

            return new WarehouseDto() { Squares = squareDtos, Key = key};
        }
    }
}
