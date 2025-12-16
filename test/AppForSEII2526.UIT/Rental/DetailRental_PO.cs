
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.Rental
{
    public class DetailRental_PO : PageObject
    {
        public DetailRental_PO(IWebDriver driver, ITestOutputHelper output)
            : base(driver, output)
        {
        }
        public bool CheckRentalDetail( string surname, string delivery, string paymentMethod,
            DateTime rentalDate, DateTime from, DateTime to, string totalPrice)
        {

            WaitForBeingVisible(By.Id("RentalTotalPrice"));
            bool result = true;

            result = result && _driver.FindElement(By.Id("Surname")).Text.Contains(surname);

            result = result && _driver.FindElement(By.Id("DeliveryAddress")).Text.Contains(delivery);

            result = result && _driver.FindElement(By.Id("PaymentMethod")).Text.Contains(paymentMethod);

            result = result && _driver.FindElement(By.Id("TotalPrice")).Text.Contains(totalPrice);

            var actualRentalDate = DateTime.Parse(_driver.FindElement(By.Id("RentalDate")).Text);

            result = result && ((actualRentalDate - rentalDate) < new TimeSpan(0, 1, 0));

            result = result && _driver.FindElement(By.Id("RentalPeriod"))
                .Text.Contains($"{from.ToShortDateString()} - {to.ToShortDateString()}");

            return result;
        }

        public bool CheckListOfCars(List<string[]> expectedRentalItems)
        {
            return CheckBodyTable(expectedRentalItems, By.Id("RentedCars"));
        }
    }
}

