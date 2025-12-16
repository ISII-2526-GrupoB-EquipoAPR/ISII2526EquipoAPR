using Humanizer;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.Rental
{
    public class SelectCarsForRental_PO : PageObject
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

        /*private IWebElement _carModel() => _driver.FindElement(_carModelBy);
        private IWebElement _rentingPrice() => _driver.FindElement(_rentingPriceBy);
        private IWebElement _showRentingCartButton() => _driver.FindElement(_ShowRentingCartBy);
        private IWebElement _searchCarsButton() => _driver.FindElement(_searchCarsBy);
        private IWebElement _rentButton() => _driver.FindElement(_rentButtonBy);
        */


        public SelectCarsForRental_PO(IWebDriver driver, ITestOutputHelper output)
            : base(driver, output)
        {

        }


        public void FilterCars(string model, float rentingprice)
        {
            WaitForBeingClickable(_carModelBy);
            _driver.FindElement(_carModelBy).SendKeys(model);
            _driver.FindElement(_rentingPriceBy).SendKeys(rentingprice.ToString());
            _driver.FindElement(_searchCarsBy).Click();

            Thread.Sleep(1500);

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
            _driver.FindElement(_rentButtonBy).Click();
        }

        public void ModifyRentingCart(string carModel)
        {
            _driver.FindElement(_ShowRentingCartBy).Click();
            WaitForBeingVisible(By.Id($"removeCar_{carModel}"));
            _driver.FindElement(By.Id($"removeCar_{carModel}")).Click();
        }

        public bool CheckListOfCars(List<string[]> expectedCars)
        {
            return CheckBodyTable(expectedCars, _tableOfCarsBy);
        }

        public bool CheckRentCarsDisabled()
        {
            return !(_driver.FindElement(_rentButtonBy).Enabled);
        }

        public bool CheckShoppingCart(string price)
        {
            
            return _driver.FindElement(_ShowRentingCartBy).Text.Contains(price);
        }

        public bool CheckMessageErrorNotAvailableCars(string expectedError)
        {
            return _driver.PageSource.Contains(expectedError);
        }

        public bool CheckMessageError(string expectedError)
        {
            return CheckModalBodyText(expectedError, _modalBy);
        }






    }
}

