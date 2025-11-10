using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs.PurchaseDTOs;
using AppForSEII2526.API.DTOs.RentalDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UT.RentalsController_test
{
    public class GetRental_test : AppForSEII25264SqliteUT
    {
        public GetRental_test()
        {
            var models = new List<Model>
            {
                new Model ("Civic"),
                new Model ("Yaris"),
            };

            var cars = new List<Car>
            {
             new Car("Sedan", "Rojo", "Honda Civic 1.8 Gasolina", "1.8", "Honda", 18000m, 5, "Gasolina", "16", models[0]),
             new Car("Hatchback", "Azul", "Toyota Yaris 1.5 Híbrido", "1.5", "Toyota", 17000m, 4, "Híbrido", "15", models[1]),
            };


            /*
             * 
            DeliveryAddress = deliveryAddress;
            CustomerUserName = customerUserName;
            CustomerNameSurname = customerNameSurname;
            Id = id;
            DeliveryCarDealer = deliveryCarDealer;
            EndDate = endDate;
            StartDate = startDate;
            RentingDate = rentingDate;
            PaymentMethod = paymentMethod;
            RentalItems = rentalItems;
            ApplicationUser = applicationUser;
             */

            ApplicationUser user = new ApplicationUser("18", "Adela", "Jerez Sanchez", "adela@uclm.es", "Avda. España s/n, Albacete");

            var rental = new Rental("Avda. España s/n, Albacete", "adela@uclm.es", "Jerez Sanchez", 17, "Paco", DateTime.Today.AddDays(5), DateTime.Today.AddDays(2), DateTime.Now, AppForSEII2526.API.Models.PaymentMethodTypes.Visa, new List<RentalItem>(), user);
            rental.RentalItems.Add(new RentalItem(cars[0], rental)); 

            _context.ApplicationUsers.Add(user);
            _context.AddRange(models);
            _context.AddRange(cars);
            _context.Add(rental);
            _context.SaveChanges();
        }

        [Fact]
        [Trait("Database", "WithoutFixture")]
        [Trait("LevelTesting", "Unit Testing")]
        public async Task GetPurchase_NotFound_test()
        {
            //Arrange
            var mock = new Mock<ILogger<RentalsController>>();
            ILogger<RentalsController> logger = mock.Object;

            var controller = new RentalsController(_context, logger);

            //Act
            var result = await controller.GetRental(0);

            //Assert
            //we check that the response is OK and obtain the list of cars
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        public async Task GetRental_Found_test()
        {
            var mock = new Mock<ILogger<RentalsController>>();
            ILogger<RentalsController> logger = mock.Object;

            var controller = new RentalsController(_context,logger);

            var expectedRental = new RentalDetailDTO(18,"adela@uclm.es","Jerez Sanchez", "Avda. España s/n, Albacete", AppForSEII2526.API.Models.PaymentMethodTypes.Visa, DateTime.Today.AddDays(5), DateTime.Today.AddDays(2),DateTime.Now,new List<RentalItemDTO>());
            expectedRental.RentalItems.Add(new RentalItemDTO(19,"Civic","Paco", 18000m,2));
            //Act
            var result = await controller.GetRental(1);

            //Assert
            //we check that the response is OK and obtain the purchase
            var okResult = Assert.IsType<OkObjectResult>(result);
            var rentalDTOActual = Assert.IsType<RentalDetailDTO>(okResult.Value);
            var eq = expectedRental.Equals(rentalDTOActual);

            Assert.Equal(expectedRental, rentalDTOActual);
        }

    }
}
