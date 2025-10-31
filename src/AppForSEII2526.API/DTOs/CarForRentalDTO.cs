namespace AppForSEII2526.API.DTOs
{

    public class CarForRentalDTO
    {
        public CarForRentalDTO()
        {
        }

        public CarForRentalDTO(int id, string model, string fuelType, string color, decimal priceForRenting, string manufacturer)
        {
            Id = id;
            Model = model;
            FuelType = fuelType;
            Color = color;
            Manufacturer = manufacturer;
            PriceForRenting = priceForRenting;
        }
       


        public int Id { get; set; }

        [StringLength(50, ErrorMessage = "Modelo puede tener como maximo 50 caracteres")]
        public string Model { get; set; }

        [StringLength(10, ErrorMessage = "Tipo de gasoil puede tener como maximo 10 caracteres")]
        public string FuelType { get; set; }

        [StringLength(50, ErrorMessage = "Color puede tener como maximo 50 caracteres")]
        public string Color { get; set; }
        [StringLength(20, MinimumLength = 1, ErrorMessage = "El fabricante debe tener entre 1 y 20 caracteres.")]
        public string Manufacturer { get; set; }


        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        [Range(1, float.MaxValue, ErrorMessage = "Precio minimo es 1")]
        [Display(Name = "Precio para alquilar")]
        public decimal PriceForRenting { get; set; }



        public override bool Equals(object? obj)
        {
            return obj is CarForRentalDTO dTO &&
                   Id == dTO.Id &&
                   Model == dTO.Model &&
                   FuelType == dTO.FuelType &&
                   Color == dTO.Color &&
                   Manufacturer == dTO.Manufacturer &&
                   PriceForRenting == dTO.PriceForRenting;
                   
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Model, FuelType, Color, Manufacturer, PriceForRenting);
        }
    }
}



