namespace AppForSEII2526.API.Models
{
    public class RentalItem
    {
        public Car car { get; set; }

        public int carId { get; set; }

        public int rentalId { get; set; }

        public Rental rental { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor que 1")]
        public int quantity { get; set; }
    }
}
