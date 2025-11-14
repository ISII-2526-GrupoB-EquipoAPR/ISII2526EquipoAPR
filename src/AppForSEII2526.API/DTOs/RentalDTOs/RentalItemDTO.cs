using AppForSEII2526.API.Models;

namespace AppForSEII2526.API.DTOs.RentalDTOs
{
    public class RentalItemDTO
    {
        public RentalItemDTO() { }
        public RentalItemDTO(int carId,string model, string manufacturer, decimal rentingPrice,int quantity)
        {
            CarId = carId;
            Model = model;
            Manufacturer = manufacturer;
            RentingPrice = rentingPrice;
            Quantity = quantity;

        }
        public int CarId { get; set; }
        public string Model { get; set; }


        public string Manufacturer { get; set; }


        public decimal RentingPrice { get; set; }

        [Required]
        public int Quantity { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is RentalItemDTO dTO
                   && CarId == dTO.CarId 
                   && Model == dTO.Model
                   && Manufacturer == dTO.Manufacturer
                   && RentingPrice == dTO.RentingPrice
                   && Quantity == dTO.Quantity;

        }

        public override int GetHashCode()
        {
            return HashCode.Combine(CarId,Model, Manufacturer, RentingPrice,Quantity);
        }
    }
}