using AppForSEII2526.Web.API;

namespace AppForSEII2526.Web

{
    public class RentalStateContainer
    {
        public RentalForCreateDTO Rental { get; private set; } = new RentalForCreateDTO()
        {
            RentalItems = new List<RentalItemDTO>()
        };

        
        public decimal TotalPrice
        {
            get
            {
                int numberOfDays = (Rental.EndDate - Rental.StartDate).Days;
                return Convert.ToDecimal(Rental.RentalItems.Sum(ri => ri.RentingPrice * numberOfDays));
            }
        }

        public event Action? OnChange;

        private void NotifyStateChanged() => OnChange?.Invoke();



        public void AddCarToRental(CarForRentalDTO car)
        {
            if (!Rental.RentalItems.Any(ri => ri.CarId == car.Id))
                Rental.RentalItems.Add(new RentalItemDTO()
                {
                    CarId = car.Id,
                    Model = car.Model,
                    Manufacturer = car.Manufacturer,
                    RentingPrice = car.PriceForRenting
                }
            );

        }

        public void RemoveRentalItemToRent(RentalItemDTO item)
        {
            Rental.RentalItems.Remove(item);

        }

        public void ClearRentingCart()
        {
            Rental.RentalItems.Clear();

        }

        public void RentalProcessed()
        {
            Rental = new RentalForCreateDTO()
            {
                RentalItems = new List<RentalItemDTO>()
            };
        }
    }
}
    
