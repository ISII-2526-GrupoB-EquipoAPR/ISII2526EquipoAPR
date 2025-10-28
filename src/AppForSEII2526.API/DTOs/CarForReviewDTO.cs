using Humanizer.Localisation;

namespace AppForSEII2526.API.DTOs
{
    public class CarForReviewDTO
    {
        public CarForReviewDTO()
        {

        }

        public CarForReviewDTO(int id, string Modelo, string CarClass, string Manufacter, string Fueltype, string Color)
   {
            Id = id;
            CarClass = CarClass;
            Modelo = Modelo;
            Manufacter = Manufacter;
            FuelType = FuelType;
            Color = Color;
        }


public int Id { get; set; }

[StringLength(50, ErrorMessage = "El modelo debe tener una longitud máxima de 50 caracteres")]
public string Modelo { get; set; }
[StringLength(50, ErrorMessage = "La clase debe tener una longitud máxima de 50 caracteres")]
public string CarClass { get; set; }
[StringLength(50, ErrorMessage = "El fabricante debe tener una longitud máxima de 50 caracteres y mínima de 4", MinimumLength = 4)]
public string Manufacter { get; set; }

[StringLength(50, ErrorMessage = "El tipo de combustible debe tener una longitud máxima de 50 caracteres")]
public string FuelType { get; set; }

[StringLength(50, ErrorMessage = "El color debe tener una longitud máxima de 50 caracteres")]
public string Color { get; set; }

        



        public override bool Equals(object? obj)
        {
            return obj is CarForReviewDTO dTO &&
                CarClass == dTO.CarClass &&
                
                Id == dTO.Id &&
                   Modelo == dTO.Modelo &&
                   Manufacter == dTO.Manufacter &&
                 FuelType == dTO.FuelType &&
                   Color == dTO.Color;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Modelo, CarClass, Manufacter, FuelType, Color);
        }
    }
}
