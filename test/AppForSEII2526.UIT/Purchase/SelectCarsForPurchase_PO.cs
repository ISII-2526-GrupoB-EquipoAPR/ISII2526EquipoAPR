using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.PurchaseCars
{
    public class SelectCarsForPurchase_PO : PageObject
    {
        private By _carColorBy = By.Id("carColor");
        private By _carModelBy = By.Id("carModel");

        private By _ShowPurchasingCartBy = By.Id("showPurchasingCart");
        private By _searchCarsBy = By.Id("searchCars");
        private By _purchaseButtonBy = By.Id("Purchase");
        
        private By _tableOfCarsBy = By.Id("TableOfCars");
        private By _modalBy = By.Id("DialogOkSaveDelete");

        private IWebElement _carModel() => _driver.FindElement(_carModelBy);

        private IWebElement _carColor() => _driver.FindElement(_carColorBy);
        private IWebElement _showPurchasingCartButton() => _driver.FindElement(_ShowPurchasingCartBy);
        private IWebElement _searchCarButton() => _driver.FindElement(_searchCarsBy);
        private IWebElement _purchaseButton() => _driver.FindElement(_purchaseButtonBy);



        public SelectCarsForPurchase_PO(IWebDriver driver, ITestOutputHelper output)
            : base(driver, output)
        {

        }

        public void FilterCars (string carModel, string carColor)
        {
            WaitForBeingVisible(_carColorBy);

            _carColor().SendKeys(carColor);

            if (carModel == "") carModel = "All";

            SelectElement selectElement = new SelectElement(_carModel());
            selectElement.SelectByText(carModel);

            _searchCarButton().Click();

            System.Threading.Thread.Sleep(2000);

        }

        public void SelectCars(List<string> carModels)
        {
            //we wait for till the movies are available to be selected 
            foreach (var carModel in carModels)
            {
                WaitForBeingVisible(By.Id($"carToPurchase_{carModel}"));
                _driver.FindElement(By.Id($"carToPurchase_{carModel}")).Click();
            }
        }

        public void PurchaseCars()
        {
            WaitForBeingClickable(_purchaseButtonBy);
            _purchaseButton().Click();
        }

        public void ModifyRentingCart(string model)
        {
            _showPurchasingCartButton().Click();
            WaitForBeingVisible(By.Id($"removeCar_{model}"));
            _driver.FindElement(By.Id($"removeCar_{model}")).Click();
        }

        public bool CheckListOfCars(List<string[]> expectedCars)
        {

            return CheckBodyTable(expectedCars, _tableOfCarsBy);
        }

        public bool CheckPurchaseCarsDisabled()
        {
            //we return true if the button is disabled
            return !(_purchaseButton().Enabled);
        }

        public bool CheckShoppingCart(string price)
        {
            return _showPurchasingCartButton().Text.Contains(price);
        }

        public bool CheckMessageErrorNotAvaibleMovies(string expectedError)
        {
            return _driver.PageSource.Contains(expectedError);

        }

        public bool CheckMessageError(string expectedError)
        {
            return CheckModalBodyText(expectedError, _modalBy);
        }

    }
}
