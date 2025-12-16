using AppForMovies.UIT.Shared;
using AppForSEII2526.UIT.Rental;
using AppForSEII2526.UIT.Shared;
using AppForSEII2526.Web.Components.Pages.Rental;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.Rental
{
    public class UCRentalCars_UIT : UC_UIT
    {


        public UCRentalCars_UIT(ITestOutputHelper output) : base(output)
        {

            Initial_step_opening_the_web_page();
            listcars = new SelectCarsForRental(_driver, _output);
        }

        private const string Name = "Elena";
        private const string SurName = "Navarro Martínez";
        private const string DeliveryCarDealer = "Calle Albacete";
        private const string PaymentMethod = "Visa";
        private const string Quantity = "1";

        private const int carId1 = 1;
        private const string CarModel1 = "Audi A4";
        private const string FuelType1 = "Gasolina";
        private const string Manufacturer1 = "Audi";
        private const string RentingPrice1 = "85";
        private const string Color1 = "Gris";


        private const string CarModel2 = "Toyota Corolla";
        private const string FuelType2 = "Diesel";
        private const string Manufacturer2 = "Toyota";
        private const string RentingPrice2 = "60";
        private const string Color2 = "Rojo";

        private SelectCarsForRental listcars;

        private void Precondition_perform_login()
        {
            Perform_login("elena@uclm.es", "Password1234%");
        }

        private void InitialStepsForRentalCars_UIT()
        {
            Precondition_perform_login();
            listcars.WaitForBeingVisibleIgnoringExeptionTypes(By.Id("CreateRenting"));
            _driver.FindElement(By.Id("CreateRenting")).Click();
        }


        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC2_FA0_CU2_2_CarsNotAvailable()
        {
            InitialStepsForRentalCars_UIT();
            var expectedMessage = "There are no cars available for being rented.";
            listcars.FilterCars("", null, "", "", "");
            Assert.True(listcars.CheckMessageErrorNotAvailableCars(expectedMessage));
        }


        [Theory]
        [InlineData(CarModel1, FuelType1, Manufacturer1, RentingPrice1, Color1, "Audi A4", null)]
        [InlineData(CarModel2, FuelType2, Manufacturer2, RentingPrice2, Color2, "", 60f)]
        [Trait("LevelTesting", "Funcional Testing")]
        public void CU2_FA1_filteringByModelAndPrice(string carmodel, string fueltype, string manufacturer, string rentingprice, string color, string filterCarModel, float? filterRentingPrice)
        {
            InitialStepsForRentalCars_UIT();
            var expectedCars = new List<string[]> { new string[] { carmodel, fueltype, manufacturer, rentingprice, color } };

            listcars.FilterCars(filterCarModel, filterRentingPrice, "", "", "");
            Thread.Sleep(1000);

            Assert.True(listcars.CheckListOfCars(expectedCars));
        }

        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC2_10_AF3_ModifySelectedCars()
        {

            InitialStepsForRentalCars_UIT();

            listcars.FilterCars("", null, "", "", "");
            listcars.SelectCars(new List<string> { CarModel1, CarModel2 });
            listcars.ModifyRentingCart(CarModel2);


            Assert.True(listcars.CheckShoppingCart(RentingPrice1));
        }

        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC2_11_AF4_RentButtonNotAvailable()
        {
            InitialStepsForRentalCars_UIT();

            listcars.FilterCars("", null, "", "", "");
            listcars.SelectCars(new List<string> { CarModel1 });
            listcars.ModifyRentingCart(CarModel1);


            Assert.True(listcars.CheckRentCarsDisabled(), "Rent button should be disabled");
        }

        [Theory]
        [InlineData("", SurName, DeliveryCarDealer, PaymentMethod, "The field Name must be a string with a minimum length of 2 and a maximum length of 20.")]
        [InlineData(Name, "", DeliveryCarDealer, PaymentMethod, "The field Surname must be a string with a minimum length of 4 and a maximum length of 100.")]
        [InlineData(Name, SurName, "", PaymentMethod, "The field DeliveryCarDealer must be a string with a minimum length of 1 and a maximum length of 20.")]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC2_FA4_testingErrorsMandatoryData(string name, string surname, string deliveryCarDealer, string paymentMethod, string expectedMessageError)
        {
            var createRental = new CreateRental_PO(_driver, _output);

            var from = DateTime.Today.AddDays(1);
            var to = DateTime.Today.AddDays(2);
            var renting = DateTime.Today.AddDays(1);

            InitialStepsForRentalCars_UIT();


            listcars.FilterCars("", null,
             from.ToString("dd-MM-yyyy"),
             to.ToString("dd-MM-yyyy"),
             renting.ToString("dd-MM-yyyy"));

            listcars.SelectCars(new List<string> { CarModel1 });
            listcars.RentCars();

            createRental.FillInRentalInfo(name, surname, deliveryCarDealer, paymentMethod);
            createRental.PressRentYourCars();


            Assert.True(
               createRental.CheckValidationError(expectedMessageError),
               $"Expected error: {expectedMessageError}"
            );
        }


        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC2_16_AF6_ModifyRentalItems()
        {
                var createRental = new CreateRental_PO(_driver, _output);
            
                var from = DateTime.Today.AddDays(1);
                var to = DateTime.Today.AddDays(2);
                var renting = DateTime.Today.AddDays(1);

                InitialStepsForRentalCars_UIT();

              
                listcars.FilterCars("", null,
                    from.ToString("dd-MM-yyyy"),
                    to.ToString("dd-MM-yyyy"),
                    renting.ToString("dd-MM-yyyy"));

                listcars.SelectCars(new List<string> { CarModel1, CarModel2 });
                listcars.RentCars();

               
                createRental.PressModifyCars();

            
                listcars.ModifyRentingCart(CarModel2);
                listcars.RentCars();

           
                var expectedRentalItems = new List<string[]>
                {
                     new string[] { CarModel1, FuelType1, RentingPrice1 }
                 };

                Assert.True(
                    createRental.CheckListOfRentalItems(expectedRentalItems),
                    "The list of rental cars was not updated correctly"
                );
        }

        [Fact(Skip = "First change the quantity of renting of the cars to 0 using script dbo.Cars.QuantityForRenting0")]
        //[Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC2_17_AF0_CarsNotAvailableForRentalPeriod()
        {
            // Arrange
            var from = DateTime.Today.AddDays(1);
            var to = DateTime.Today.AddDays(2);
            var renting = DateTime.Today.AddDays(1);

            var expectedMessage = "There are no cars available for being rented";

            // Act
            InitialStepsForRentalCars_UIT();

            listcars.FilterCars(
                "",
                null,
                from.ToString("dd-MM-yyyy"),
                to.ToString("dd-MM-yyyy"),
                renting.ToString("dd-MM-yyyy")
            );

            // Assert
            Assert.True(
                listcars.CheckMessageErrorNotAvailableCars(expectedMessage),
                $"Expected message not shown: {expectedMessage}"
            );
        }




        [Theory]
        [InlineData("Elena Navarro", "Calle de la Universidad 1, Albacete, 02006, España", "CreditCard")]
        [InlineData("Elena Navarro", "Calle de la Universidad 1, Albacete, 02006, España", "PayPal")]
        [InlineData("Elena Navarro", "Calle de la Universidad 1, Albacete, 02006, España", "Cash")]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC2_1_2_3_BasicFlow_Cars(string name,string nameSurname,string deliveryAddress,string paymentMethod)
        {
         
            // Arrange
            var createrental = new CreateRental_PO(_driver, _output);
            var detailRental = new DetailRental_PO(_driver, _output);

            var from = DateTime.Today.AddDays(1);
            var to = DateTime.Today.AddDays(2);
            var renting = DateTime.Today.AddDays(1);

            // Act
            InitialStepsForRentalCars_UIT();

            listcars.FilterCars("",null,from.ToString("dd-MM-yyyy"),to.ToString("dd-MM-yyyy"),renting.ToString("dd-MM-yyyy"));
            listcars.SelectCars(new List<string> { CarModel1 });
            listcars.RentCars();

            createrental.FillInRentalInfo(name,nameSurname,deliveryAddress,paymentMethod);
            createrental.PressRentYourCars();
            createrental.PressOkModalDialog();

            Assert.True(detailRental.CheckRentalDetail(name,nameSurname,deliveryAddress,paymentMethod,DateTime.Now,from,to,RentingPrice1 + " €"),"Error: rental detail is not as expected");

            var expectedRentalItems = new List<string[]>
            {
                new string[]{CarModel1,FuelType1, RentingPrice1 + " €"}
            };

            Assert.True(
                detailRental.CheckListOfCars(expectedRentalItems),
                "Error: rental items are not as expected"
            );
        }






    }
}

