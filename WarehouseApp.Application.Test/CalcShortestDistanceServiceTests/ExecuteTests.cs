using Moq;
using WarehouseApp.Application.DTOs;
using WarehouseApp.Application.Services;
using WarehouseApp.Domain;
using WarehouseApp.Domain.Interfaces;

namespace WarehouseApp.Application.Test.CalcShortestDistanceServiceTests
{
    [TestFixture]
    public class ExecuteTests
    {
        private Mock<ISquareService> _mockSquareService;
        private CalcShortestDistanceService _calcShortestDistanceService;

        [SetUp]
        public void Setup()
        {
            _mockSquareService = new Mock<ISquareService>();
            _calcShortestDistanceService = new CalcShortestDistanceService(_mockSquareService.Object);
        }

        [Test]
        public void Execute_EmptyCoordinates_ThrowsException()
        {
            //Arrange
            WarehouseInputDto source = new()
            {
                Squares = [], StartPointX = 0, StartPointY = 0
            };

            //Act + Assert
            var exception = Assert.Throws<ArgumentNullException>(() => _calcShortestDistanceService.Execute(source));
            StringAssert.Contains("Coordinates list cannot be null or empty.", exception.Message);
        }

        [Test]
        public void Execute_CoordinatesDontContainInitPoint_ThrowsException() 
        {
            //Arrange
            WarehouseInputDto source = new()
            {
                Squares =
                [
                    new CoordinateModel{ CoordX = 1, CoordY = 1 },
                    new CoordinateModel { CoordX = 1, CoordY = 2}
                ],
                StartPointX = 0,
                StartPointY = 0
            };

            //Act + Assert
            var exception = Assert.Throws<ArgumentException>(() => _calcShortestDistanceService.Execute(source));
            StringAssert.Contains("Init point must be included in coordinates list.", exception.Message);
        }

        [Test]
        public void Execute_CoordinatesAreNotAllAdjacent_ThrowsException()
        {
            //Arrange
            WarehouseInputDto source = new()
            {
                Squares =
                [
                    new CoordinateModel { CoordX = 1, CoordY = 1 },
                    new CoordinateModel { CoordX = 2, CoordY = 1 },
                    new CoordinateModel { CoordX = 3, CoordY = 2 }
                ],
                StartPointX = 1,
                StartPointY = 1
            };

            //Act + Assert
            var exception = Assert.Throws<ArgumentException>(() => _calcShortestDistanceService.Execute(source));
            StringAssert.Contains("All coordinates must be adjacent to at least one other coordinate.", exception.Message);
        }

        [Test]
        public void Execute_NotThrownException()
        {
            //Arrange
            WarehouseInputDto source = new()
            {
                Squares =
                [
                    new CoordinateModel { CoordX = 1, CoordY = 1 },
                    new CoordinateModel { CoordX = 2, CoordY = 1 },
                    new CoordinateModel { CoordX = 3, CoordY = 1 }
                ],
                StartPointX = 1,
                StartPointY = 1
            };

            var expectedSquares = new List<Square>
            {
                // Agrega aquí los Square esperados
            };

            _mockSquareService.Setup(service => service.CalcDistances(It.IsAny<Warehouse>())).Returns(It.IsAny<List<Square>>);

            // Act + Assert
            Assert.DoesNotThrow(() => _calcShortestDistanceService.Execute(source));
        }
    }
}
