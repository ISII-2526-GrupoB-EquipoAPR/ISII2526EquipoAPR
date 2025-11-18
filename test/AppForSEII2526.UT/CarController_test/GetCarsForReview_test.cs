using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs;
using AppForSEII2526.API.Models;

namespace AppForSEII2526.UT.CarsController_test
{
    public class GetCarsForReview_test : AppForSEII25264SqliteUT
    {
        public GetCarsForReview_test()
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
        public static IEnumerable<object[]> TestCasesFor_GetCarsForReview_OK()
        {

            var carDTOs = new List<CarForReviewDTO>() {
                new CarForReviewDTO(1,"Corsa","Opel","Gasolina","Rojo"),
                new CarForReviewDTO(2,"Golf","Volkswagen","Gasolina","Azul"),
                new CarForReviewDTO(3, "Q3" ,"Audi","Diésel","Negro"),
                new CarForReviewDTO(4, "Corolla","Toyota", "Híbrido","Blanco")
            };

            var carDTOsTC1 = new List<CarForReviewDTO>() { carDTOs[0], carDTOs[1], carDTOs[2], carDTOs[3] };

            var carDTOsTC2 = new List<CarForReviewDTO>() { carDTOs[0] };
            var carDTOsTC3 = new List<CarForReviewDTO>() { carDTOs[2] };

            var allTests = new List<object[]>
            {             //filters to apply - expected cars
                new object[] { null, null, carDTOsTC1 },
                new object[] { "Corsa", null, carDTOsTC2},
                new object[] { null, "Diésel", carDTOsTC3},
            };

            return allTests;
        }
        [Theory]
        [MemberData(nameof(TestCasesFor_GetCarsForReview_OK))]
        [Trait("Database", "WithoutFixture")]
        [Trait("LevelTesting", "Unit Testing")]
        public async Task GetCarsForReview_OK_test(string? Manufacturer, string? Fueltype,            IList<CarForReviewDTO> expectedCars)
        {
            // Arrange
            var controller = new CarsController(_context, null);

            // Act
            var result = await controller.GetCarForReview(Manufacturer, Fueltype);

            //Assert
            //we check that the response type is OK 
            var okResult = Assert.IsType<OkObjectResult>(result);
            //and obtain the list of movies
            var carDTOsActual = Assert.IsType<List<CarForReviewDTO>>(okResult.Value);
            Assert.Equal(expectedCars, carDTOsActual);

        }
    }
}