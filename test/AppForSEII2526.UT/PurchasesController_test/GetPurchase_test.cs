using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs.PurchaseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UT.PurchasesController_test
{
    public class GetPurchase_test : AppForSEII25264SqliteUT
    {
        public GetPurchase_test()
        {
            var models = new List<Model>
            {
                new Model ("Golf"),
                new Model ("Q3"),
            };

            var cars = new List<Car>
            {
                new Car("Sedan","Azul","Volkswagen Golf","1.4","Volkswagen",25000,3,"Gasolina","17",models[0]),
                new Car("SUV","Negro","Audi Q3","2.0","Audi",45000,2,"Diésel","18",models[1])
            };

            ApplicationUser user = new ApplicationUser("1", "Patricia", "Quintanar Martínez", "patricia@uclm.es", "Avda. España s/n, Albacete");

            var purchase = new Purchase("patricia@uclm.es", "Quintanar Martínez", "Avda. España s/n, Albacete", "Juan", AppForSEII2526.API.Models.PaymentMethodTypes.Visa, DateTime.Now, new List<PurchaseItem>(), user);
            purchase.PurchaseItems.Add(new PurchaseItem(cars[0], purchase, 1));

            _context.ApplicationUsers.Add(user);
            _context.AddRange(models);
            _context.AddRange(cars);
            _context.Add(purchase);
            _context.SaveChanges();
        }

        [Fact]
        [Trait("Database", "WithoutFixture")]
        [Trait("LevelTesting", "Unit Testing")]
        public async Task GetPurchase_NotFound_test()
        {
            //Arrange
            var mock = new Mock<ILogger<PurchasesController>>();
            ILogger<PurchasesController> logger = mock.Object;

            var controller = new PurchasesController(_context, logger);

            //Act
            var result = await controller.GetPurchase(0);

            //Assert
            //we check that the response is OK and obtain the list of cars
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        public async Task GetPurchase_Found_test()
        {
            var mock = new Mock<ILogger<PurchasesController>>();
            ILogger<PurchasesController> logger = mock.Object;

            var controller = new PurchasesController(_context, logger);

            var expectedPurchase = new PurchaseDetailDTO(1, "patricia@uclm.es", "Quintanar Martínez", PaymentMethodTypes.Visa, "Avda. España s/n, Albacete", DateTime.Now, new List<PurchaseItemDTO>());
            expectedPurchase.PurchaseItems.Add(new PurchaseItemDTO(1, "Golf", "Azul", 25000, 1, "Volkswagen Golf"));
            //Act
            var result = await controller.GetPurchase(1);

            //Assert
            //we check that the response is OK and obtain the purchase
            var okResult = Assert.IsType<OkObjectResult>(result);
            var purchaseDTOActual = Assert.IsType<PurchaseDetailDTO>(okResult.Value);
            var eq = expectedPurchase.Equals(purchaseDTOActual);

            Assert.Equal(expectedPurchase, purchaseDTOActual);
        }

    }
}
