using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs.PurchaseDTOs;
using AppForSEII2526.API.Models;
using Humanizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UT.PurchasesController_test
{
    public class CreatePurchase_test : AppForSEII25264SqliteUT
    {
        private const string _userName = "patricia@uclm.es";
        private const string _customerNameSurname = "Patricia Quintanar";
        private const string _deliveryAddress = "Avda. España s/n, Albacerte";

        private const string _car1Model = "Q3";
        
        private const string _car2Model = "Golf";
     

        public CreatePurchase_test()
        {
            var models = new List<Model>()
            {
                new Model(_car1Model),
                new Model(_car2Model),
            };

            var cars = new List<Car>()
            {
                new Car("Q3","Negro","Audi Q3","2.0","Audi",45000,2,"Diésel","18",models[0]),
                new Car("Sedan","Azul","Volkswagen Golf","1.4","Volkswagen",25000,3,"Gasolina","17",models[1]),
            };

            ApplicationUser user = new ApplicationUser("1", "Patricia", _customerNameSurname, _userName, _deliveryAddress);

            var purchase = new Purchase(_userName, _customerNameSurname, _deliveryAddress, "Juan",AppForSEII2526.API.Models.PaymentMethodTypes.Visa, DateTime.Now, new List<PurchaseItem>(),user);
            purchase.PurchaseItems.Add(new PurchaseItem(cars[0], purchase, 1));

            _context.ApplicationUsers.Add(user);
            _context.AddRange(models);
            _context.AddRange(cars);
            _context.Add(purchase);
            _context.SaveChanges();
        }

        public static IEnumerable<object[]> TestCaseFor_CreatePurchase()
        {
            var purchaseNoITem = new PurchaseForCreateDTO(_deliveryAddress, _userName, _customerNameSurname, new List<PurchaseItemDTO>(), PaymentMethodTypes.Visa);

            var purchaseItems = new List<PurchaseItemDTO>() { new PurchaseItemDTO(2, _car2Model, "Azul", 25000, 1, "Volkswagen Golf") };

            var purchaseApplicationUser = new PurchaseForCreateDTO(_deliveryAddress,"adela@uclm.es",_customerNameSurname, purchaseItems, PaymentMethodTypes.Visa);

            var purchaseCarNonExistent = new PurchaseForCreateDTO(_deliveryAddress, _userName, _customerNameSurname, new List<PurchaseItemDTO>() { new PurchaseItemDTO(3,"Cooper S","Blanco",18000,1,"") },PaymentMethodTypes.Visa);

            var purchaseCarNotAvailable = new PurchaseForCreateDTO(_deliveryAddress, _userName, _customerNameSurname, new List<PurchaseItemDTO>() { new PurchaseItemDTO (1, _car1Model,"Negro",45000,3)}, PaymentMethodTypes.Visa);

            var allTest = new List<object[]>
            {
                new object[] { purchaseNoITem, "Error! You must include at least one car to be purchased"},
                new object[] { purchaseApplicationUser, "Error! UserName is not registered" },
                new object[] { purchaseCarNonExistent, "Error! Car with id 3 does not exist" },
                new object[] { purchaseCarNotAvailable, "Error! Car 'Q3' does not have enough stock. Available: 1, Requested: 3" },
            };

            return allTest;
        }
        [Theory]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        [MemberData(nameof(TestCaseFor_CreatePurchase))]
        public async Task CreatePurchase_Error_test(PurchaseForCreateDTO purchaseDTO, string errorExpected)
        {
            var mock = new Mock<ILogger<PurchasesController>>();
            ILogger<PurchasesController> logger = mock.Object;

            var controller = new PurchasesController(_context, logger);

            // Act
            var result = await controller.CreatePurchase(purchaseDTO);
            // Assert
            //we check that the reponse type is BadRequest and obtain the error returned
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var problemDetails = Assert.IsType<ValidationProblemDetails>(badRequestResult.Value);

            var errorActual = problemDetails.Errors.First().Value[0];

            //we check that the expected error message and actual are the same
            Assert.Equal(errorExpected, errorActual);
        }
        [Fact]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        public async Task CreatePurchase_Success_test()
        {
            var mock = new Mock<ILogger<PurchasesController>>();
            ILogger<PurchasesController> logger = mock.Object;

            var controller = new PurchasesController(_context, logger);

            var purchaseDTO = new PurchaseForCreateDTO(_deliveryAddress, _userName, _customerNameSurname,
                new List<PurchaseItemDTO>() { new PurchaseItemDTO(2, _car2Model, "Azul", 25000, 1, "Volkswagen Golf") }, PaymentMethodTypes.Visa);

            var expectedDTO = new PurchaseDetailDTO(2, _userName, _customerNameSurname, PaymentMethodTypes.Visa,
                _deliveryAddress, DateTime.Now,
                new List<PurchaseItemDTO>() { new PurchaseItemDTO(2, _car2Model, "Azul", 25000, 1, "Volkswagen Golf") });

            var result = await controller.CreatePurchase(purchaseDTO);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var actualPurchaseDetailDTO = Assert.IsType<PurchaseDetailDTO>(createdResult.Value);

            Assert.Equal(expectedDTO,actualPurchaseDetailDTO);
        }
    }

}
