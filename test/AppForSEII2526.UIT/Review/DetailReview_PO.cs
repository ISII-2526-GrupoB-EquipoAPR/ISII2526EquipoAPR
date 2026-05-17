using OpenQA.Selenium;
using Xunit.Abstractions;
using System.Collections.Generic;

namespace AppForSEII2526.UIT.Review
{
    public class DetailReviewPO : PageObject
    {
        public DetailReviewPO(IWebDriver driver, ITestOutputHelper output)
            : base(driver, output) { }

        public bool CheckReviewHeader(string name, string country, string driverType)
        {
            // El razor usa id="ReviewerUserName" (muestra el email/username del usuario logueado)
            WaitForBeingVisible(By.Id("ReviewerUserName"));

            bool result = true;

            result &= _driver.FindElement(By.Id("ReviewerUserName"))
                .Text.Contains(name);

            result &= _driver.FindElement(By.Id("ReviewerCountry"))
                .Text.Contains(country);

            result &= _driver.FindElement(By.Id("ReviewerDriverType"))
                .Text.Contains(driverType);

            return result;
        }

        public bool CheckReviewedCars(List<string[]> expectedCars)
        {
            return CheckBodyTable(expectedCars, By.Id("ReviewedCars"));
        }

        public bool CheckReviewItemDescriptionIsEmpty(int carId)
        {
            try
            {
                var carRow = _driver.FindElement(By.Id($"ReviewItem_{carId}"));
                var descriptionCell = carRow.FindElement(By.XPath(".//td[5]")); // Descripci�n en la quinta columna
                return string.IsNullOrEmpty(descriptionCell.Text);
            }
            catch (NoSuchElementException)
            {
                return false; 
            }
        }

        public bool CheckCarModelInDetail(int carId)
        {
           
            WaitForBeingVisible(By.Id("ReviewedCars"));

            
            string expectedModel = GetCarModelById(carId);

            
            var modelCell = _driver.FindElement(By.XPath($"//table[@id='ReviewedCars']//tr/td[1]"));

            
            string actualModel = modelCell.Text.Trim();

            return actualModel.Equals(expectedModel, StringComparison.OrdinalIgnoreCase);
        }

        
        private string GetCarModelById(int carId)
        {
            switch (carId)
            {
                case 6: return "Civic";
                case 7: return "Escape";

                default: return "Desconocido";
            }
        }



    }
}