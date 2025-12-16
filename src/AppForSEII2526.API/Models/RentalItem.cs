namespace AppForSEII2526.API.Models
{
    [PrimaryKey(nameof(CarId), nameof(RentalId))]
    public class RentalItem
    {
        public RentalItem()
        {
        }
        public RentalItem(Car car, Rental rental)
        {
            Car = car;
            CarId = car.Id;
            Rental = rental;
            RentalId = rental.Id;
        }

        public RentalItem(int carId, Rental rental, decimal priceForRenting)
        {
            CarId = carId;
            Rental = rental;
            PriceForRenting = priceForRenting;

        }
        public RentalItem(Car car, Rental rental,int quantity):this(car,rental)
        {
            PriceForRenting = Car.RentingPrice;
            Quantity = quantity;

        }
        public Car Car { get; set; }

        public int CarId { get; set; }

        public int RentalId { get; set; }

        public Rental Rental { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor que 1")]
        public int Quantity { get; set; }
        public decimal PriceForRenting { get; set; }
    }
}
