using WarehouseApp.Application.DTOs;
using WarehouseApp.Domain;

namespace WarehouseApp.Application.Interfaces
{
    public interface ICalcShortestDistanceService
    {
        List<Square> Execute(WarehouseInputDto source);
    }
}
