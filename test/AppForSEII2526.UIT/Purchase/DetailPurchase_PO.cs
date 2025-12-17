using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.PurchaseCars
{
    public class DetailPurchase_PO : PageObject 
    {
        public DetailPurchase_PO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {

        }

        public bool CheckPurchaseDetail (string surname,string delivery, string paymentmethod, DateTime purchasingDate, string totalprice)
        {
            WaitForBeingVisible(By.Id("TotalPrice"));
            bool result = true;
            result = result && _driver.FindElement(By.Id("Surname")).Text.Contains(surname);
            result = result && _driver.FindElement(By.Id("PaymentMethod")).Text.Contains(paymentmethod);
            result = result && _driver.FindElement(By.Id("DeliveryAddress")).Text.Contains(delivery);
            result = result && _driver.FindElement(By.Id("TotalPrice")).Text.Contains(totalprice);

            var actualPurchaseDate = DateTime.Parse(_driver.FindElement(By.Id("PurchaseDate")).Text);
            result = result && ((actualPurchaseDate - purchasingDate) < new TimeSpan(0, 1, 0));

            return result;
        }

        public bool CheckListOfPurchase(List<string[]> expectedPurchaseItems)
        {
            return CheckBodyTable(expectedPurchaseItems, By.Id("PurchasedCars"));
        }
    }       
}
