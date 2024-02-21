using WarehouseApp.Domain;

namespace WarrehouseApp.Infrastructure.Data.Interfaces.SquarePrinter
{
    public interface IConsoleSquarePrinter : ISquarePrinter
    {
        void PrintSquares(List<Square> squares);
    }
}
