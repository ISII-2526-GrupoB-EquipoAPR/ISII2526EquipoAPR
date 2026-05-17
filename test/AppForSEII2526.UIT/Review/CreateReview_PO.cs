using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AppForSEII2526.UIT.Review
{
    public class CreateReviewPO : PageObject
    {
        // IDs tal como aparecen en CreateReview.razor
        private By inputCustomerUserName = By.Id("CustomerUserName");
        private By inputCountry = By.Id("Country");
        private By selectDriverType = By.Id("DriverType");   // capital T igual que en el razor
        private By submitButton = By.Id("Submit");

        public CreateReviewPO(IWebDriver driver, ITestOutputHelper output)
            : base(driver, output) { }

       
        public void FillReviewerData(string name, string country, string driverType)
        {
            WaitForBeingVisible(inputCountry);

            _driver.FindElement(inputCountry).Clear();
            _driver.FindElement(inputCountry).SendKeys(country);

            var select = new SelectElement(_driver.FindElement(selectDriverType));
            if (!string.IsNullOrEmpty(driverType))
                select.SelectByText(driverType);
            else
                select.SelectByIndex(0); // opción vacía -> falla validación Required
        }

        public void OverrideCustomerUserName(string email)
        {
            WaitForBeingVisible(inputCustomerUserName);
            _driver.FindElement(inputCustomerUserName).Clear();
            _driver.FindElement(inputCustomerUserName).SendKeys(email);
        }
        public void FillReviewItemByCarId(int carId, string rating, string description)
        {
            var row = By.Id($"CreateReviewItem_{carId}");
            WaitForBeingVisible(row);

            var ratingInput = _driver.FindElement(By.Id($"ratingtxt_{carId}"));
            ratingInput.Clear();
            ratingInput.SendKeys(rating);

            var descInput = _driver.FindElement(By.Id($"descripciontxt_{carId}"));
            descInput.Clear();
            descInput.SendKeys(description);
        }

        public void SubmitReview()
        {
            WaitForBeingClickable(submitButton);
            _driver.FindElement(submitButton).Click();
        }

        public void ConfirmDialog()
        {
            PressOkModalDialog();
        }

   
        public string GetValidationErrorText()
        {
            var selector = By.CssSelector(".alert.alert-danger:not(.row)");
            WaitForBeingVisible(selector);
            return _driver.FindElement(selector).Text;
        }

        public string GetServerErrorText()
        {
            var selector = By.CssSelector(".row.alert.alert-danger");
            WaitForBeingVisible(selector);
            return _driver.FindElement(selector).Text;
        }

        public bool IsErrorMessageDisplayed()
        {
            var errorElement = By.ClassName("error-message");
            try
            {
                WaitForBeingVisible(errorElement);
                return _driver.FindElement(errorElement).Displayed;
            }
            catch
            {
                return false;
            }
        }

        public void GoBackToSelectCars()
        {
            var modifyCarsButton = By.CssSelector("button.btn.btn-outline-primary");
            WaitForBeingClickable(modifyCarsButton);
            _driver.FindElement(modifyCarsButton).Click();
        }
    }
}
