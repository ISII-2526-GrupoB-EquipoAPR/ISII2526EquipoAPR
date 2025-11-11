using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UT.CarController_test
{
    public class GetCarsForRental_test: AppForSEII25264SqliteUT
    {
        public GetCarsForRental_test()
        {
            var models = new List<Model>
            {
                new Model("Civic"),
                new Model("Yaris"),
                new Model("Model 3"),
                new Model("CX-5")
            };

            var cars = new List<Car>
            {

                new Car("Sedan", "Rojo", "Honda Civic 1.8 Gasolina", "1.8", "Honda", 50000, 5, "Gasolina", "16", models[0]),
                new Car("Hatchback", "Azul", "Toyota Yaris 1.5 Híbrido", "1.5", "Toyota", 45000, 4, "Híbrido", "15", models[1]), 
                new Car("Sedan", "Blanco", "Tesla Model 3 Eléctrico", "0", "Tesla", 80000, 3, "Eléctrico", "18", models[2]),
                new Car("SUV", "Gris", "Mazda CX-5 2.0 Gasolina", "2.0", "Mazda", 60000, 6, "Gasolina", "17", models[3])
            };

            _context.AddRange(models);
            _context.AddRange(cars);
            _context.SaveChanges();

        }
        public static IEnumerable<object[]> TestCasesFor_GetCarsForRental_OK()
        {

            var carDTOs = new List<CarForRentalDTO>()
            {
                new CarForRentalDTO(1, "Civic",   "Gasolina", "Rojo",   50000, "Honda"),
                new CarForRentalDTO(2, "Yaris",   "Híbrido",  "Azul",   45000, "Toyota"),
                new CarForRentalDTO(3, "Model 3", "Eléctrico","Blanco", 80000, "Tesla"),
                new CarForRentalDTO(4, "CX-5",    "Gasolina", "Gris",   60000, "Mazda")
            };

            var carDTOsTC1 = new List<CarForRentalDTO>() { carDTOs[0], carDTOs[1], carDTOs[2], carDTOs[3] };

            var carDTOsTC2 = new List<CarForRentalDTO>() { carDTOs[0] };
            var carDTOsTC3 = new List<CarForRentalDTO>() { carDTOs[2] };

            var allTests = new List<object[]>
            {             //filters to apply - expected cars
                new object[] { null, null, carDTOsTC1 },
                new object[] { "Civic", null, carDTOsTC2},
                new object[] { null, 8000, carDTOsTC3},
            };

            return allTests;
        }
        [Theory]
        [MemberData(nameof(TestCasesFor_GetCarsForRental_OK))]
        [Trait("Database", "WithoutFixture")]
        [Trait("LevelTesting", "Unit Testing")]
        public async Task GetCarsForRental_OK_test(string? carModel, decimal? rentingPrice,
            IList<CarForRentalDTO> expectedCars)
        {
            // Arrange
            var controller = new CarsController(_context, null);

            // Act
            var result = await controller.GetCarForRental(carModel,rentingPrice);

            //Assert
            //we check that the response type is OK 
            var okResult = Assert.IsType<OkObjectResult>(result);
            //and obtain the list of cars
            var carDTOsActual = Assert.IsType<List<CarForRentalDTO>>(okResult.Value);
            Assert.Equal(expectedCars, carDTOsActual);

        }

    }
}
