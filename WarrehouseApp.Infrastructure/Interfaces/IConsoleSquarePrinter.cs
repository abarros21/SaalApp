using WarehouseApp.Domain;

namespace WarrehouseApp.Infrastructure.Interfaces
{
    public interface IConsoleSquarePrinter : ISquarePrinter
    {
        void PrintSquares(List<Square> squares);
    }
}
