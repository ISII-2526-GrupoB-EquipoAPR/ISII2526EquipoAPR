
using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs.RentalDTOs;
using AppForSEII2526.API.Models;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppForSEII2526.UT.RentalsController_test
{
    public class CreateRental_test : AppForSEII25264SqliteUT
    {
        private const string _userName = "adela@uclm.es";
        private const string _customerNameSurname = "Adela Jerez Sanchez";
        private const string _deliveryAddress = "Avda. España s/n, Albacete";

        private const string _car1Model = "Civic";
        private const string _car2Model = "Yaris";

        public CreateRental_test()
        {
            var models = new List<Model>()
            {
                new Model(_car1Model),
                new Model(_car2Model),
            };

            var cars = new List<Car>()
            {
                new Car(50000,"Sedan", "Rojo", "Honda Civic 1.8 Gasolina", "1.8", "Honda",  5, "Gasolina", "16", models[0]),
                new Car(45000,"Hatchback", "Azul", "Toyota Yaris 1.5 Híbrido", "1.5", "Toyota",  4, "Híbrido", "15", models[1]),
            };

            ApplicationUser user = new ApplicationUser("1", "Adela", _customerNameSurname, _userName, _deliveryAddress);

            var rental = new Rental(_deliveryAddress, _userName, _customerNameSurname, 1, "Honda",
                DateTime.Today.AddDays(5), DateTime.Today.AddDays(2), DateTime.Now,
                AppForSEII2526.API.Models.PaymentMethodTypes.Visa, new List<RentalItem>(), user);

            rental.RentalItems.Add(new RentalItem(cars[0], rental, 2));

            _context.ApplicationUsers.Add(user);
            _context.AddRange(models);
            _context.AddRange(cars);
            _context.Add(rental);
            _context.SaveChanges();
        }

        public static IEnumerable<object[]> TestCaseFor_CreateRental()
        {
                var rentalNoItems = new RentalForCreateDTO(_userName, _customerNameSurname, _deliveryAddress,
                    PaymentMethodTypes.Visa, DateTime.Today.AddDays(2), DateTime.Today.AddDays(5),
                    new List<RentalItemDTO>());

                var rentalItems = new List<RentalItemDTO>() {
                new RentalItemDTO(2, _car2Model, "Toyota", 45000, 1)
                };

                var rentalApplicationUser = new RentalForCreateDTO("patricia@uclm.es", _customerNameSurname, _deliveryAddress,
                    PaymentMethodTypes.Visa, DateTime.Today.AddDays(2), DateTime.Today.AddDays(5), rentalItems);

                var rentalCarNonExistent = new RentalForCreateDTO(_userName, _customerNameSurname, _deliveryAddress,
                    PaymentMethodTypes.Visa, DateTime.Today.AddDays(2), DateTime.Today.AddDays(5),
                    new List<RentalItemDTO>() { new RentalItemDTO(3, "Model 3", "Tesla", 80000, 1) });

                var rentalCarNotAvailable = new RentalForCreateDTO(_userName, _customerNameSurname, _deliveryAddress,
                    PaymentMethodTypes.Visa, DateTime.Today.AddDays(2), DateTime.Today.AddDays(5),
                    new List<RentalItemDTO>() { new RentalItemDTO(1, _car1Model, "Honda", 50000, 10) });

                var allTest = new List<object[]>
                {
                 new object[] { rentalNoItems, "Error! Debes seleccionar al menos un coche para alquilar" },
                 new object[] { rentalApplicationUser, "Error! El nombre de usuario no está registrado" },
                 new object[] { rentalCarNonExistent, "Error! El coche con id 3 no existe" }, 
                 new object[] { rentalCarNotAvailable, "Error! El coche 'Civic' no tiene suficiente stock" }, 
   
                };

                return allTest;
        }

        [Theory]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        [MemberData(nameof(TestCaseFor_CreateRental))]
        public async Task CreateRental_Error_test(RentalForCreateDTO rentalDTO, string errorExpected)
        {
            var mock = new Mock<ILogger<RentalsController>>();
            ILogger<RentalsController> logger = mock.Object;

            var controller = new RentalsController(_context, logger);

            // Act
            var result = await controller.CreateRental(rentalDTO);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var problemDetails = Assert.IsType<ValidationProblemDetails>(badRequestResult.Value);

            var errorActual = problemDetails.Errors.First().Value[0];

            // We check that the expected error message and actual are the same
            Assert.Equal(errorExpected, errorActual); 
        }

        [Fact]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        public async Task CreateRental_Success_test()
        {
            var mock = new Mock<ILogger<RentalsController>>();
            ILogger<RentalsController> logger = mock.Object;

            var controller = new RentalsController(_context, logger);

            var rentalDTO = new RentalForCreateDTO(_userName, _customerNameSurname, _deliveryAddress,
                PaymentMethodTypes.Visa, DateTime.Today.AddDays(2), DateTime.Today.AddDays(5),
                new List<RentalItemDTO>() { new RentalItemDTO(2, _car2Model, "Toyota", 45000, 1) });

            var expectedDTO = new RentalDetailDTO(2, _userName, _customerNameSurname, _deliveryAddress,
                PaymentMethodTypes.Visa, DateTime.Today.AddDays(2), DateTime.Today.AddDays(5), DateTime.Now,
                new List<RentalItemDTO>() { new RentalItemDTO(2, _car2Model, "Toyota", 45000, 1) });

            // Act
            var result = await controller.CreateRental(rentalDTO);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var actualRentalDetailDTO = Assert.IsType<RentalDetailDTO>(createdResult.Value);

            Assert.Equal(expectedDTO, actualRentalDetailDTO);
        }
    }
}
