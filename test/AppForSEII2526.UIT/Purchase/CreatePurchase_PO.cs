using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.PurchaseCars
{
    public class CreatePurchase_PO: PageObject
    {
        private By _nameSurnameBy = By.Id("NameSurname");
        private IWebElement _surname() => _driver.FindElement(_nameSurnameBy);
        private IWebElement _deliveryAddress() => _driver.FindElement(By.Id("DeliveryAddress"));
        private IWebElement _paymentMethod() => _driver.FindElement(By.Id("PaymentMethod"));

        public CreatePurchase_PO(IWebDriver driver, ITestOutputHelper output)
            : base(driver, output)
        {
        }
        public void FillInPurchaseInfo(string surname, string deliveryAddress, string paymentMethod)
        {
            WaitForBeingVisible(_nameSurnameBy);
            _surname().SendKeys(surname);
            _deliveryAddress().SendKeys(deliveryAddress);

            //create select element object 
            SelectElement selectElement = new SelectElement(_paymentMethod());

            //select Action from the dropdown menu
            selectElement.SelectByText(paymentMethod);
        }
        public void FillInPurchaseQuantity(string purchaseQuantity, string carModel)
        {
            _driver.FindElement(By.Id("quantity_" + carModel)).SendKeys(purchaseQuantity);
        }
        public void PressPurchaseYourCars()
        {
            _driver.FindElement(By.Id("Submit")).Click();
        }

        public void PressModifyCars()
        {
            _driver.FindElement(By.Id("ModifyCars")).Click();
        }

        public bool CheckListOfPurchaseItems(List<string[]> expectedPurchaseItems)
        {
            return CheckBodyTable(expectedPurchaseItems, By.Id("TableOfPurchaseItems"));
        }
        public bool CheckValidationError(string expectedError)
        {
            return _driver.PageSource.Contains(expectedError);
        }

    }
}
