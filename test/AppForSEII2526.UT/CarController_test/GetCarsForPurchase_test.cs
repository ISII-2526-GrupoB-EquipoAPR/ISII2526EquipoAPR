using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs;
using AppForSEII2526.API.Models;

namespace AppForSEII2526.UT.CarsController_test
{
    public class GetCarsForPurchase_test : AppForSEII25264SqliteUT
    {
        public GetCarsForPurchase_test()
        {
            var models = new List<Model>
            {
                new Model("Corsa"),
                new Model("Golf"),
                new Model("Q3"),
                new Model("Corolla")
            };

            var cars = new List<Car> {
                new Car("Harchback","Rojo","Opel Corsa","1.2","Opel",17500,5,"Gasolina","16",models[0]),
                new Car("Sedan","Azul","Volkswagen Golf","1.4","Volkswagen",25000,3,"Gasolina","17",models[1]),
                new Car("SUV","Negro","Audi Q3","2.0","Audi",45000,2,"Diésel","18",models[2]),
                new Car("Sedan","Blanco","","1.8","Toyota",23000,4,"Híbrido","17",models[3])
            };
            _context.AddRange(models);
            _context.AddRange(cars);
            _context.SaveChanges();
        }
        public static IEnumerable<object[]> TestCasesFor_GetCarsForPurchase_OK()
        {

            var carDTOs = new List<CarForPurchaseDTO>() {
                new CarForPurchaseDTO(1,"Corsa","Rojo","Gasolina","Opel",17500),
                new CarForPurchaseDTO(2,"Golf","Azul","Gasolina","Volkswagen",25000),
                new CarForPurchaseDTO(3, "Q3", "Negro", "Diésel","Audi",45000),
                new CarForPurchaseDTO(4, "Corolla", "Blanco", "Híbrido","Toyota",23000)
            };

            var carDTOsTC1 = new List<CarForPurchaseDTO>() { carDTOs[0], carDTOs[1], carDTOs[2], carDTOs[3] };

            var carDTOsTC2 = new List<CarForPurchaseDTO>() { carDTOs[0] };
            var carDTOsTC3 = new List<CarForPurchaseDTO>() { carDTOs[2] };

            var allTests = new List<object[]>
            {             //filters to apply - expected cars
                new object[] { null, null, carDTOsTC1 },
                new object[] { "Rojo", null, carDTOsTC2},
                new object[] { null, "Q3", carDTOsTC3},
            };

            return allTests;
        }
        [Theory]
        [MemberData(nameof(TestCasesFor_GetCarsForPurchase_OK))]
        [Trait("Database", "WithoutFixture")]
        [Trait("LevelTesting", "Unit Testing")]
        public async Task GetCarsForPurchase_OK_test(string? carColor, string? carModel,
            IList<CarForPurchaseDTO> expectedCars)
        {
            // Arrange
            var controller = new CarsController(_context, null);

            // Act
            var result = await controller.GetCarForPurchase(carColor, carModel);

            //Assert
            //we check that the response type is OK 
            var okResult = Assert.IsType<OkObjectResult>(result);
            //and obtain the list of movies
            var carDTOsActual = Assert.IsType<List<CarForPurchaseDTO>>(okResult.Value);
            Assert.Equal(expectedCars, carDTOsActual);

        }
    }
}
