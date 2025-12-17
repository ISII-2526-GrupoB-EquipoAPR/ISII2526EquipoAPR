using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.PurchaseCars
{
    public class UCPurchaseCars_UIT : UC_UIT
    {
        private SelectCarsForPurchase_PO selectcars;
        

        public UCPurchaseCars_UIT(ITestOutputHelper output) : base(output)
        {
            Initial_step_opening_the_web_page();
            selectcars = new SelectCarsForPurchase_PO(_driver, _output);

        }

        private const int carId1 = 9;
        private const string color1 = "Gris";
        private const string carModel1 = "CX-5";
        private const string purchasingPrice1 = "26000";
        private const string fuelType1 = "Gasolina";
        private const string manufacturer1 = "Mazda";


        private const int carId2 = 15;
        private const string color2 = "Rojo";
        private const string carModel2 = "Golf";
        private const string fuelType2 = "Gasolina";
        private const string purchasingPrice2 = "20000";
        private const string manufacturer2 = "Volkswagen";

        private const string quantity = "1";

        private void Precondition_perform_login()
        {
            Perform_login("elena@uclm.es", "Password1234%");
        }

        private void InitialStepsForPurchaseCars()
        {
            Precondition_perform_login();
            selectcars.WaitForBeingVisibleIgnoringExeptionTypes(By.Id("CreatePurchase"));
            selectcars.WaitForBeingClickable(By.Id("CreatePurchase"));
            _driver.FindElement(By.Id("CreatePurchase")).Click();
        }

        [Theory]
        [InlineData("Elena Navarro", "Calle de la Universidad 1, Albacete, 02006, España", "Visa")]
        [InlineData("Elena Navarro", "Calle de la Universidad 1, Albacete, 02006, España", "Paypal")]
        [InlineData("Elena Navarro", "Calle de la Universidad 1, Albacete, 02006, España", "GooglePay")]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC1_BF_1_2_3_FlujoBasico(string surname, string deliveryAddress, string paymentMethod)
        {
            //Arrange
            var createpurchase = new CreatePurchase_PO(_driver, _output);
            var detailPurchase = new DetailPurchase_PO(_driver, _output);

            //Act
            InitialStepsForPurchaseCars();

            selectcars.SelectCars(new List<string> { carModel1 });
            selectcars.PurchaseCars();

            createpurchase.FillInPurchaseInfo(surname, deliveryAddress, paymentMethod);
            createpurchase.FillInPurchaseQuantity(quantity, carModel1);
            createpurchase.PressPurchaseYourCars();
            createpurchase.PressOkModalDialog();

            //Assert
            //the expected error is shown in the view
            Assert.True(detailPurchase.CheckPurchaseDetail(surname,
                deliveryAddress, paymentMethod, DateTime.Now, purchasingPrice1 + " €"),
                "Error: detail purchase is not as expected");

            var expectedPurchaseItems = new List<string[]>
                    { new string[] { carModel1, purchasingPrice1 + " €" , color1, quantity}, };

            Assert.True(detailPurchase.CheckListOfPurchase(expectedPurchaseItems),
                "Error: purchase items are not as expected");
        }
        
        [Fact(Skip = "Primero cambie la QuantityForPurchasing a 0 con el script dbo.Cars.QuantityForPurchasing0")]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC1_AF0_4_NoCoches()
        {
            //Arrange
            InitialStepsForPurchaseCars();
            var expectedMessage = "There are no cars available for being purchased.";
            //Act
            selectcars.FilterCars("", "");
            //Assert
            Assert.True(selectcars.CheckMessageErrorNotAvailableCars(expectedMessage));

        }

        [Theory]
        [InlineData(manufacturer1, carModel1, color1, purchasingPrice1, fuelType1, "Gris", "")]
        [InlineData(manufacturer2, carModel2, color2, purchasingPrice2, fuelType2, "", "Golf")]
        [InlineData(manufacturer1, carModel1, color1, purchasingPrice1, fuelType1, "Gris", "CX-5")]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC1_FA1_5_6_7_Filtrado(string manufacturer, string model, string color, string price, string fuel, string filterColor, string filterModel)
        {
            //Arrange
            var expectedCars = new List<string[]>
            {
                new string[] { manufacturer, model, color, price, fuel }
            };
            //Act
            InitialStepsForPurchaseCars();
            selectcars.FilterCars(filterModel, filterColor);
            //Assert            
            Assert.True(selectcars.CheckListOfCars(expectedCars));
        }
        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC1_FA2_8_CompraNoDisponible()
        {   
            //Arrange
            InitialStepsForPurchaseCars();

            //Act 
            selectcars.FilterCars("", "");
            selectcars.SelectCars(new List<string> { carModel1 });
            selectcars.ModifyPurchasingCart(carModel1);

            //Assert 
            Assert.True(selectcars.PurchasingNotAvailable());
            
        }
        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC1_FA2_9_ModificacionCarrito() {
            //Arrange
            InitialStepsForPurchaseCars();
            //Act
            

            selectcars.FilterCars("", "");
            selectcars.SelectCars(new List<string> { carModel1, carModel2 });
            selectcars.ModifyPurchasingCart(carModel2);


            //Assert            
            Assert.True(selectcars.CheckShoppingCart(purchasingPrice1));
        }
        [Theory]
        [InlineData("", "Calle de la Universidad 1, Albacete, 02006, España", "The CustomerNameSurname field is required")]
        [InlineData("Elena", "Calle de la Universidad 1, Albacete, 02006, España", "The field CustomerNameSurname must be a string with a minimum length of 10 and a maximum length of 50")]
        [InlineData("Elena Navarro", "", "The DeliveryAddress field is required")]
        [InlineData("Elena Navarro", "Calle", "The field DeliveryAddress must be a string with a minimum length of 10 and a maximum length of 50")]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC2_12_13_14_15_AF5_AtributosObligatorios(string nameSurname, string deliveryAddress,
            string expectedMessageError)
        {
            //Arrange
            InitialStepsForPurchaseCars();
            CreatePurchase_PO createpurchase = new CreatePurchase_PO(_driver, _output);
            //Act


            selectcars.FilterCars("", "");
            selectcars.SelectCars(new List<string> { carModel1 });
            selectcars.PurchaseCars();
            createpurchase.FillInPurchaseInfo(nameSurname, deliveryAddress, "Visa");
            createpurchase.PressPurchaseYourCars();

            //Assert
            //the expected error is shown in the view
            Assert.True(createpurchase.CheckValidationError(expectedMessageError), $"Expected error: {expectedMessageError}");
        }

        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC2_16_AF6_ModificaPurchaseItems()
        {
            //Arrange

            var createpurchase = new CreatePurchase_PO(_driver, _output);


            //Act
            InitialStepsForPurchaseCars();

            selectcars.FilterCars("", "");
            selectcars.SelectCars(new List<string> { carModel1, carModel2 });
            selectcars.PurchaseCars();
            createpurchase.PressModifyCars();
            //we remove carModel2 from the purchasingcart
            selectcars.ModifyPurchasingCart(carModel2);
            selectcars.PurchaseCars();

            //Assert
            //the list of cars must change
            var expectedPurchaseItems = new List<string[]> { new string[] { carModel1, color1, purchasingPrice1 + " €" }, };
            Assert.True(createpurchase.CheckListOfPurchaseItems(expectedPurchaseItems));
        }


    }
}
