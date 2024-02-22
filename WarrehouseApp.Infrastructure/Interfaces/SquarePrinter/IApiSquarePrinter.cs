using WarehouseApp.Domain;
using WarrehouseApp.Infrastructure.Data.DTOs;

namespace WarrehouseApp.Infrastructure.Data.Interfaces.SquarePrinter
{
    public interface IApiSquarePrinter : ISquarePrinter
    {
        WarehouseDto PrintAndReturnWarehouseDTO(string key,List<Square> squares);
    }
}
