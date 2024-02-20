using WarehouseApp.Domain;
using WarrehouseApp.Infrastructure.DTOs;

namespace WarrehouseApp.Infrastructure.Interfaces
{
    public interface IApiSquarePrinter : ISquarePrinter
    {
        List<SquareDto> PrintAndReturnSquareDtos(List<Square> squares);
    }
}
