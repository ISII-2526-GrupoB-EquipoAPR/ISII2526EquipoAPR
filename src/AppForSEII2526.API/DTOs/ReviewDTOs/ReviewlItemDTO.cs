namespace AppForSEII2526.API.DTOs.ReviewDTOs
{
    public class ReviewlItemDTO
    {
        public class PurchaseItemDTO
        {
            public PurchaseItemDTO(int carID, string model, string color,string FuelType, string Manufacturer,int rating, string reviewDescription)
            {
                CarID = carID;
                Model = model;
                Color = color;
                FuelType = FuelType;
                Manufacturer= Manufacturer;
                Rating= rating;
                ReviewDescription = reviewDescription;
            }

            public int CarID { get; set; }

            [StringLength(50, ErrorMessage = "El modelo debe tener una longitud máxima de 50 caracteres")]
            public string Model { get; set; }

            [StringLength(50, ErrorMessage = "El color debe tener una longitud máxima de 50 caracteres")]
            public string Color { get; set; }

            [StringLength(50, ErrorMessage = "El combustible debe tener una longitud máxima de 50 caracteres")]
            public string Fueltype { get; set; }

            [StringLength(50, ErrorMessage = "El fabricante debe tener una longitud máxima de 50 caracteres y mínima de 4", MinimumLength = 4)]
            public string Manufacturer { get; set; }

            [Required]
            [Range(1, 5, ErrorMessage = "El rating debe estar entre 1 y 5.")]
            public int Rating { get; set; }

            [StringLength(500, ErrorMessage = "La descripción de la reseña debe tener una longitud máxima de 500 caracteres y minima de 5", MinimumLength = 5)]
            public string ReviewDescription { get; set; }

            public override bool Equals(object? obj)
            {
                return obj is PurchaseItemDTO dTO &&
                       CarID == dTO.CarID &&
                       Model == dTO.Model &&
                       Color == dTO.Color &&
                       Fueltype== dTO.Fueltype &&
                       Manufacturer== dTO.Manufacturer &&
                        Rating == dTO.Rating &&
                        ReviewDescription == dTO.ReviewDescription; 
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(CarID, Model, Color, Fueltype, Manufacturer, Rating, ReviewDescription);
            }
        }
    }
}
