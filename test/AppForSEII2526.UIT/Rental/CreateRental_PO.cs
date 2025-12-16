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
        private By _nameSurnameBy = By.Id("NameSurname");
        private IWebElement _surname() => _driver.FindElement(_nameSurnameBy);
        private IWebElement _deliveryAddress() => _driver.FindElement(By.Id("DeliveryAddress"));
        private IWebElement _paymentMethod() => _driver.FindElement(By.Id("PaymentMethod"));

        public CreateRental_PO(IWebDriver driver, ITestOutputHelper output)
            : base(driver, output)
        {
        }

        public void FillInRentalInfo( string surname, string deliveryAddress, string paymentMethod)
        {
          
            WaitForBeingVisible(_nameSurnameBy);
            _surname().SendKeys(surname);
            _deliveryAddress().SendKeys(deliveryAddress);

            SelectElement selectElement = new SelectElement(_paymentMethod());

            selectElement.SelectByText(paymentMethod);
        }

        public void FillInRentalQuantity(string purchaseQuantity, string carModel)
        {
            _driver.FindElement(By.Id("quantity" + carModel)).SendKeys(purchaseQuantity);
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




