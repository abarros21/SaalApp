using WarehouseApp.Domain;
using WarrehouseApp.Infrastructure.DTOs;

namespace WarrehouseApp.Infrastructure.Data.Interfaces.SquarePrinter
{
    public interface IApiSquarePrinter : ISquarePrinter
    {
        List<SquareDto> PrintAndReturnSquareDtos(List<Square> squares);
    }
}
