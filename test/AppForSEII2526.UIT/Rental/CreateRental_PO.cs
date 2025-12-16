using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AppForSEII2526.UIT.Rental
{
    public class CreateRental_PO : PageObject
    {
        private By _nameBy = By.Id("Name");
        private By _surnameBy = By.Id("Surname");
        private By _deliveryAddressBy = By.Id("DeliveryAddress");
        private By _paymentMethodBy = By.Id("PaymentMethod");
        private By _totalPriceBy = By.Id("TotalCost");
        private By _submitButtonBy = By.Id("Submit");
        private By _modifyCarsButtonBy = By.Id("ModifyCars");

        private IWebElement _name() => _driver.FindElement(_nameBy);
        private IWebElement _surname() => _driver.FindElement(_surnameBy);
        private IWebElement _deliveryAddress() => _driver.FindElement(_deliveryAddressBy);
        private IWebElement _paymentMethod() => _driver.FindElement(_paymentMethodBy);
        private IWebElement _submitButton() => _driver.FindElement(_submitButtonBy);
        private IWebElement _modifyCarsButton() => _driver.FindElement(_modifyCarsButtonBy);

        public CreateRental_PO(IWebDriver driver, ITestOutputHelper output)
            : base(driver, output)
        {
        }

        public void FillInRentalInfo(string name, string surname, string deliveryAddress, string paymentMethod)
        {
            WaitForBeingVisible(_nameBy);
            WaitForBeingVisible(_surnameBy);
            WaitForBeingVisible(_deliveryAddressBy);
            _name().SendKeys(name);
            _surname().SendKeys(surname);
            _deliveryAddress().SendKeys(deliveryAddress);

            //create select element object 
            SelectElement selectElement = new SelectElement(_paymentMethod());

            //select Action from the dropdown menu
            selectElement.SelectByText(paymentMethod);
        }

        public void PressRentYourCars()
        {
            _driver.FindElement(By.Id("Submit")).Click();
        }

        public void PressModifyCars()
        {
            _driver.FindElement(By.Id("ModifyCars")).Click();
        }
        public bool CheckListOfRentalItems(List<string[]> expectedRentalItems)
        {
            return CheckBodyTable(expectedRentalItems, By.Id("TableOfRentalItems"));
        }
        public bool CheckValidationError(string expectedError)
        {
            return _driver.PageSource.Contains(expectedError);
        }


    }
}




