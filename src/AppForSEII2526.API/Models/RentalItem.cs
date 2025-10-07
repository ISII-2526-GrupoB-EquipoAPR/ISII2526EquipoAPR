namespace AppForSEII2526.API.Models
{
    [PrimaryKey(nameof(CarId), nameof(RentalId))]
    public class RentalItem
    {
        public Car Car { get; set; }

        public int CarId { get; set; }

        public int RentalId { get; set; }

        public Rental Rental { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor que 1")]
        public int Quantity { get; set; }
    }
}
