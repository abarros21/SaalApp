using WarrehouseApp.Infrastructure.DTOs;

namespace WarrehouseApp.Infrastructure.Data.DTOs
{
    public class WarehouseDto
    {
        public required List<SquareDto> Squares { get; set; }
        public required string Key { get; set; }
    }
}
