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

        private const int carId1 = 14;
        private const string color1 = "Blanco";
        private const string carModel1 = "Q3";
        private const string purchasingPrice1 = "42000";
        private const string manufacturer1 = "Audi";
        
        
        private const int carId2 = 2;
        private const string color2 = "Rojo";
        private const string carModel2 = "Golf";
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
        public void UC1_BF_1_2_3 (string surname, string deliveryAddress, string paymentMethod)
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

            var expectedRentalItems = new List<string[]>
                    { new string[] { carModel1, purchasingPrice1 + " €" , color1, quantity}, };

            Assert.True(detailPurchase.CheckListOfPurchase(expectedRentalItems),
                "Error: purchase items are not as expected");
        }
    }
}
