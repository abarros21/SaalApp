namespace WarehouseApp.Application.DTOs
{
    public record WarehouseInputDto
    {
        public int StartPointX { get; set; }
        public int StartPointY { get; set; }
        public required List<CoordinateModel> Squares { get; set; }
    }

    public record CoordinateModel
    {
        public int CoordX { get; set; }
        public int CoordY { get; set; }
    }
}
