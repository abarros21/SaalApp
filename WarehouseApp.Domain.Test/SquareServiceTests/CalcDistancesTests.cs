using WarehouseApp.Domain.Services;

namespace WarehouseApp.Domain.Test.SquareServiceTests
{
    [TestFixture]
    public class CalcDistancesTests
    {
        private SquareService _squareService;

        [SetUp]
        public void Setup()
        {
            // Configurar el estado necesario antes de cada prueba
            _squareService = new SquareService();
        }
        private static IEnumerable<TestCaseData> TestCasesSimple
        {
            get
            {
                yield return new TestCaseData(1, 1, 0);
                yield return new TestCaseData(2, 1, 1);
                yield return new TestCaseData(2, 2, 2);
                yield return new TestCaseData(3, 2, 3);
                yield return new TestCaseData(2, 3, 3);
                yield return new TestCaseData(3, 3, 4);
            }
        }

        private static IEnumerable<TestCaseData> TestCasesComplex
        {
            get
            {
                yield return new TestCaseData(1, 1, new List<int> { 8, 11, 7, 10, 14, 12 });
                yield return new TestCaseData(4, 5, new List<int> { 1, 4, 0, 3, 7, 7 });
                yield return new TestCaseData(8, 8, new List<int> { 8, 5, 7, 4, 0, 8 });
            }
        }

        [Test]
        [TestCaseSource(nameof(TestCasesSimple))]
        public void CalcDistances_SimpleCase(int startPointX, int startPointY, int expectedDistance)
        {
            // Arrange
            var warehouse = new Warehouse(
                [
                    new(1, 1),
                    new(2, 1),
                    new(2, 2),
                    new(3, 2),
                    new(2, 3),
                    new(3, 3)
                ],
                new Coordinate(startPointX, startPointY)
            );

            //Act
            var result = _squareService.CalcDistances(warehouse);

            // Assert
            Assert.That(result, Has.Count.EqualTo(warehouse.Coordinates.Count));
            // Distance from coordinate (1,1) yo InitPoint
            Assert.That(result.FirstOrDefault(sq => sq.Coordinate.X == 1 && sq.Coordinate.Y == 1)?.DistanceToInitPoint, Is.EqualTo(expectedDistance));

            // Check distance from initPoint is = 0
            foreach (var square in result.Where(sq => sq.Coordinate.X == startPointX && sq.Coordinate.Y == startPointY))
            {
                Assert.That(square.DistanceToInitPoint, Is.EqualTo(0));
            }
        }

        [Test]
        [TestCaseSource(nameof(TestCasesComplex))]
        public void CalcDistances_ComplexCase(int startPointX, int startPointY, List<int> expectedDistance)
        {
            // Arrange
            var warehouse = new Warehouse(
                [
                    new(1, 1),
                    new(2, 1),
                    new(4, 1),
                    new(5, 1),
                    new(6, 1),
                    new(8, 1),
                    new(2, 2),
                    new(4, 2),
                    new(6, 2),
                    new(7, 2),
                    new(8, 2),
                    new(1, 3),
                    new(2, 3),
                    new(3, 3),
                    new(4, 3),
                    new(8, 3),
                    new(1, 4),
                    new(4, 4),
                    new(7, 4),
                    new(8, 4),
                    new(1, 5),
                    new(2, 5),
                    new(3, 5),
                    new(4, 5),
                    new(5, 5),
                    new(6, 5),
                    new(7, 5),
                    new(3, 6),
                    new(7, 6),
                    new(8, 6),
                    new(1, 7),
                    new(3, 7),
                    new(5, 7),
                    new(8, 7),
                    new(1, 8),
                    new(2, 8),
                    new(3, 8),
                    new(4, 8),
                    new(5, 8),
                    new(6, 8),
                    new(7, 8),
                    new(8, 8)
                ],
                new Coordinate(startPointX, startPointY)
            );

            //Act
            var result = _squareService.CalcDistances(warehouse);

            // Assert
            Assert.That(result, Has.Count.EqualTo(warehouse.Coordinates.Count));
            Assert.Multiple(() =>
            {
                // Distance from init to conflict squares
                Assert.That(result.FirstOrDefault(sq => sq.Coordinate.X == 3 && sq.Coordinate.Y == 5)?.DistanceToInitPoint, Is.EqualTo(expectedDistance[0]));
                Assert.That(result.FirstOrDefault(sq => sq.Coordinate.X == 3 && sq.Coordinate.Y == 8)?.DistanceToInitPoint, Is.EqualTo(expectedDistance[1]));
                Assert.That(result.FirstOrDefault(sq => sq.Coordinate.X == 4 && sq.Coordinate.Y == 5)?.DistanceToInitPoint, Is.EqualTo(expectedDistance[2]));
                Assert.That(result.FirstOrDefault(sq => sq.Coordinate.X == 7 && sq.Coordinate.Y == 5)?.DistanceToInitPoint, Is.EqualTo(expectedDistance[3]));
                Assert.That(result.FirstOrDefault(sq => sq.Coordinate.X == 8 && sq.Coordinate.Y == 8)?.DistanceToInitPoint, Is.EqualTo(expectedDistance[4]));
                Assert.That(result.FirstOrDefault(sq => sq.Coordinate.X == 8 && sq.Coordinate.Y == 2)?.DistanceToInitPoint, Is.EqualTo(expectedDistance[5]));
            });
        }
    }
}
