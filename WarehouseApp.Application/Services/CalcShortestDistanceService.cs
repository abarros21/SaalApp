using WarehouseApp.Application.DTOs;
using WarehouseApp.Application.Interfaces;
using WarehouseApp.Domain;
using WarehouseApp.Domain.Interfaces;

namespace WarehouseApp.Application.Services
{
    public class CalcShortestDistanceService(ISquareService squareService) : ICalcShortestDistanceService
    {
        private readonly ISquareService _squareService = squareService;

        public List<Square> Execute(WarehouseInputDto source)
        {
            Warehouse warehouse = CreateWarehouseFromModel(source);
            List<Square> squares = _squareService.CalcDistances(warehouse);

            return squares;
        }

        private static Warehouse CreateWarehouseFromModel(WarehouseInputDto model)
        {
            ArgumentNullException.ThrowIfNull(model);

            var coordinates = model.Squares.Select(square => new Coordinate(square.CoordX, square.CoordY))
                .Distinct()
                .ToList();
            var initPoint = new Coordinate(model.StartPointX, model.StartPointY);

            return new Warehouse(coordinates, initPoint);
        }
    }
}
