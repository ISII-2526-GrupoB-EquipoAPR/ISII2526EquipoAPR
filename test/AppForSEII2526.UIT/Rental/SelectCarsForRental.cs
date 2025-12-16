using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.Rental
{
    public class SelectCarsForRental : PageObject
    {

        private By _carModelBy = By.Id("modelCar");
        private By _rentingPriceBy = By.Id("rentingPrice");
        private By _fromBy = By.Id("fromDate");
        private By _toBy = By.Id("toDate");


        private By _ShowRentingCartBy = By.Id("showRentingCart");
        private By _searchCarsBy = By.Id("searchCars");
        private By _rentButtonBy = By.Id("Rent");

        private By _tableOfCarsBy = By.Id("TableOfCars");
        private By _modalBy = By.Id("DialogOKSaveDelete");

        private IWebElement _carModel() => _driver.FindElement(_carModelBy);
        //this code is a shortcut for:
        // private IWebElement _movieTitle() {return _driver.FindElement(By.Id("movieTitle"));}

        private IWebElement _rentingPrice() => _driver.FindElement(_rentingPriceBy);
        private IWebElement _showRentingCartButton() => _driver.FindElement(_ShowRentingCartBy);
        private IWebElement _searchCarsButton() => _driver.FindElement(_searchCarsBy);
        private IWebElement _rentButton() => _driver.FindElement(_rentButtonBy);


        public SelectCarsForRental(IWebDriver driver, ITestOutputHelper output)
            : base(driver, output)
        {

        }


        public void FilterCars(string model, float? rentingprice, string from, string to, string renting)
        {


            WaitForBeingVisible(_carModelBy);

            if (!string.IsNullOrEmpty(model))
            {
                _carModel().Clear();
                _carModel().SendKeys(model);
            }

            if (rentingprice.HasValue)
            {
                _rentingPrice().Clear();
                _rentingPrice().SendKeys(rentingprice.Value.ToString());
            }


            if (!string.IsNullOrEmpty(from))
            {
                InputDateInDatePicker(_fromBy, DateTime.Parse(from));
            }

            if (!string.IsNullOrEmpty(to))
            {
                InputDateInDatePicker(_toBy, DateTime.Parse(to));
            }


            _searchCarsButton().Click();

            Thread.Sleep(2000);
        }

        public void SelectCars(List<string> carModels)
        {

            foreach (var carModel in carModels)
            {
                WaitForBeingVisible(By.Id($"carToRent_{carModel}"));
                _driver.FindElement(By.Id($"carToRent_{carModel}")).Click();
            }
        }

        public void RentCars()
        {
            WaitForBeingClickable(_rentButtonBy);
            _rentButton().Click();
        }

        public void ModifyRentingCart(string carModel)
        {
            _showRentingCartButton().Click();
            WaitForBeingVisible(By.Id($"removeCar_{carModel}"));
            _driver.FindElement(By.Id($"removeCar_{carModel}")).Click();
        }

        public bool CheckListOfCars(List<string[]> expectedCars)
        {
            // Comprobamos que la tabla de coches contiene las filas esperadas
            return CheckBodyTable(expectedCars, _tableOfCarsBy);
        }

        public bool CheckRentCarsDisabled()
        {
            // Devuelve true si el botón de alquilar está deshabilitado
            return !(_rentButton().Enabled);
        }

        public bool CheckShoppingCart(string price)
        {
            // Devuelve true si el texto del carrito contiene el precio esperado
            return _showRentingCartButton().Text.Contains(price);
        }

        public bool CheckMessageErrorNotAvailableCars(string expectedError)
        {
            // Comprueba si la página contiene un mensaje de error genérico
            return _driver.PageSource.Contains(expectedError);
        }

        public bool CheckMessageError(string expectedError)
        {
            // Comprueba si el modal de error contiene el texto esperado
            return CheckModalBodyText(expectedError, _modalBy);
        }

    }
}

