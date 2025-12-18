using AppForSEII2526.UIT.Shared;
using AppForSEII2526.Web.Components.Pages.Rental;
using Humanizer;
using Microsoft.VisualBasic.FileIO;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.Rental
{
    public class UCRentalCars_UIT : UC_UIT
    {
        private SelectCarsForRental_PO selectcars;

        public UCRentalCars_UIT(ITestOutputHelper output) : base(output)
        {
            Initial_step_opening_the_web_page();
            selectcars = new SelectCarsForRental_PO(_driver, _output);

        }

        private const int carId1 = 9;
        private const string color1 = "Gris";
        private const string carModel1 = "CX-5";
        private const string rentingPrice1 = "3000";
        private const string manufacturer1 = "Mazda";
        private const string fuelType1 = "Gasolina";


        private const int carId2 = 16;
        private const string color2 = "Negro";
        private const string carModel2 = "Fiesta";
        private const string rentingPrice2 = "1500";
        private const string manufacturer2 = "Ford";
        private const string fuelType2 = "Gasolina";

        private const string quantity = "1";

        private void Precondition_perform_login()
        {
            Perform_login("elena@uclm.es", "Password1234%");
        }

        private void InitialStepsForRentalCars()
        {
            Precondition_perform_login();
            selectcars.WaitForBeingVisibleIgnoringExeptionTypes(By.Id("CreateRental"));
            selectcars.WaitForBeingClickable(By.Id("CreateRental"));
            _driver.FindElement(By.Id("CreateRental")).Click();
        }


        [Theory]
        [InlineData(carModel1, fuelType1, manufacturer1, rentingPrice1, color1, "CX-5", 3000f)]
        [InlineData(carModel2, fuelType2, manufacturer2, rentingPrice2, color2, "", 1500f)]
        [InlineData(carModel2, fuelType2, manufacturer2, rentingPrice2, color2, "Fiesta", 1500f)]
        [Trait("LevelTesting", "Funcional Testing")]
        public void CU2_FA1_filteringByModelAndPrice(string carmodel, string fueltype, string manufacturer, string rentingprice, string color, string filterCarModel, float? filterRentingPrice)
        {
            InitialStepsForRentalCars();
            var expectedCars = new List<string[]> { new string[] { manufacturer, carmodel, color, rentingprice, fueltype } };

            selectcars.FilterCars(filterCarModel, filterRentingPrice);

            Thread.Sleep(1000);

            Assert.True(selectcars.CheckListOfCars(expectedCars));

        }



        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC2_10_AF3_ModifySelectedCars()
        {

            InitialStepsForRentalCars();

            selectcars.FilterCars("", null);
            selectcars.SelectCars(new List<string> { carModel1, carModel2 });
            selectcars.ModifyRentingCart(carModel2);


            Assert.True(selectcars.CheckShoppingCart(rentingPrice1));
        }


        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC2_11_AF4_RentButtonNotAvailable()
        {
            InitialStepsForRentalCars();

            selectcars.FilterCars("", null);
            selectcars.SelectCars(new List<string> { carModel1 });
            selectcars.ModifyRentingCart(carModel1);


            Assert.True(selectcars.CheckRentCarsDisabled(), "Rent button should be disabled");
        }




        [Theory]
        [InlineData("", "Calle avn de España 123", "The CustomerNameSurname field is required.")]
        [InlineData("Elena Navarro maria", "", "The DeliveryAddress field is required.")]
        [InlineData("Elena ", "Calle avn de España 123", "The field CustomerNameSurname must be a string with a minimum length of 10 and a maximum length of 50.")]
        [InlineData("Elena Navarro maria ", "Calle ", "The field DeliveryAddress must be a string with a minimum length of 10 and a maximum length of 50.")]

        [Trait("LevelTesting", "Funcional Testing")]
        public void UC2_FA4_testingErrorsMandatoryData(string namesurname, string deliveryAddress, string expectedMessageError)
        {
            var createRental = new CreateRental_PO(_driver, _output);

            InitialStepsForRentalCars();


            selectcars.FilterCars("", null);
            selectcars.SelectCars(new List<string> { carModel1 });
            selectcars.RentCars();

            createRental.FillInRentalInfo(namesurname, deliveryAddress, "Visa");
            createRental.PressRentYourCars();
            Thread.Sleep(1000);

            Assert.True(
               createRental.CheckValidationError(expectedMessageError),
               $"Expected error: {expectedMessageError}"
            );
        }


        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC2_16_AF6_ModifyRentalItems()
        {


            var createrental = new CreateRental_PO(_driver, _output);



            InitialStepsForRentalCars();

            selectcars.FilterCars("", null);
            selectcars.SelectCars(new List<string> { carModel1, carModel2 });
            selectcars.RentCars();

            createrental.PressModifyCars();

            selectcars.WaitForBeingVisible(By.Id("TableOfCars"));
            selectcars.ModifyRentingCart(carModel2);
            selectcars.RentCars();


            var expectedRentalItems = new List<string[]> { new string[] { carModel1, manufacturer1, rentingPrice1 } };
            Assert.True(createrental.CheckListOfRentalItems(expectedRentalItems));
        }



        //[Fact(Skip = "Requiere ejecutar el script dbo.Cars.QuantityForRenting0.sql para poner la cantidad de coches disponibles a 0")]
        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC2_17_AF0_CarsNotAvailableForRentalPeriod()
        {
            var expectedMessage = "There are no cars available for being rented";

            InitialStepsForRentalCars();


            selectcars.FilterCars("", null);


            Assert.True(selectcars.CheckMessageErrorNotAvailableCars(expectedMessage));

        }


        [Theory]
        [InlineData("Elena Navarro", "Calle de la Universidad 1, Albacete, 02006, España", "Visa")]
        [InlineData("Elena Navarro", "Calle de la Universidad 1, Albacete, 02006, España", "Paypal")]
        [InlineData("Elena Navarro", "Calle de la Universidad 1, Albacete, 02006, España", "GooglePay")]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC2_BF_1_2_3(string nameSurname, string deliveryAddress, string paymentMethod)
        {
            //Arrange
            var createrental = new CreateRental_PO(_driver, _output);
            var detailrental = new DetailRental_PO(_driver, _output);

            //Act
            InitialStepsForRentalCars();

            selectcars.FilterCars("", null);
            selectcars.SelectCars(new List<string> { carModel1 });
            selectcars.RentCars();

            createrental.FillInRentalInfo(nameSurname, deliveryAddress, paymentMethod);
            createrental.FillInRentalQuantity(quantity, carModel1);
            createrental.PressRentYourCars();
            createrental.PressOkModalDialog();

            //Assert
            // Columnas de la tabla RentedCars: Modelo, Fabricante, Precio de alquiler, Cantidad
            var expectedRentalItems = new List<string[]>
                    { new string[] { carModel1, manufacturer1, rentingPrice1 + " €", quantity } };

            Assert.True(detailrental.CheckListOfCars(expectedRentalItems),
                "Error: rental items are not as expected");
        }

        //[Fact(Skip = "Requiere ejecutar el script dbo.Cars.QuantityForRenting0.sql para poner la cantidad de coches disponibles a 0")]
        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC2_Examen()
        {
           
            var createrental = new CreateRental_PO(_driver, _output);
            var detailrental = new DetailRental_PO(_driver, _output);

            
            InitialStepsForRentalCars();

            selectcars.FilterCars(carModel2, null);
            selectcars.SelectCars(new List<string> { carModel2 });
            selectcars.FilterCars("", float.Parse(rentingPrice1));
            selectcars.SelectCars(new List<string> { carModel1 });
            selectcars.RentCars();

            createrental.PressModifyCars();

            selectcars.ModifyRentingCart(carModel2);
            selectcars.RentCars();

            createrental.FillInRentalInfo("Adela Jerez Sánchez", "Calle José Martin,2008,234", "Visa");
            createrental.FillInRentalQuantity(quantity, carModel1);
            createrental.PressRentYourCars();
            createrental.PressOkModalDialog();

            //Assert
            // Columnas de la tabla RentedCars: Modelo, Fabricante, Precio de alquiler, Cantidad
            var expectedRentalItems = new List<string[]>
                    { new string[] { carModel1, manufacturer1, rentingPrice1 + " €", quantity } };

            Assert.True(detailrental.CheckListOfCars(expectedRentalItems),
                "Error: rental items are not as expected");
            
        }



    }
    }