using Humanizer.Localisation;

namespace AppForSEII2526.API.DTOs
{
    public class CarForReviewDTO
    {
        public CarForReviewDTO()
        {

        }

        public CarForReviewDTO(int id, string Modelo, string Manufacter, string Fueltype, string Color)
        {
            Id = id;
            Modelo = Modelo;
            Manufacter = Manufacter;
            FuelType = FuelType;
            Color = Color;
        }


        public int Id { get; set; }

        [StringLength(50, ErrorMessage = "Title must have a maximun length of 50 characters")]
        public string Modelo { get; set; }

        [StringLength(50, ErrorMessage = "Genre must have a maximun length of 50 characters", MinimumLength = 4)]
        public string Manufacter { get; set; }

        [StringLength(50, ErrorMessage = "Title must have a maximun length of 50 characters")]
        public string FuelType { get; set; }

        [StringLength(50, ErrorMessage = "Title must have a maximun length of 50 characters")]
        public string Color { get; set; }



        public override bool Equals(object? obj)
        {
            return obj is CarForReviewDTO dTO &&
                   Id == dTO.Id &&
                   Modelo == dTO.Modelo &&
                   Manufacter == dTO.Manufacter &&
                 FuelType == dTO.FuelType &&
                   Color == dTO.Color;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Modelo, Manufacter, FuelType, Color);
        }
    }
}
