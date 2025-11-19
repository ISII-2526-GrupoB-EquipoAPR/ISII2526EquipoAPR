namespace AppForSEII2526.API.DTOs.ReviewDTOs
{
    public class ReviewItemsDTO
    {
        private string name;
        private int? rating;
        private string? description;

        public ReviewItemsDTO(int carId, string name, string color, string fuelType, string manufacturer, int rating, string? description)
        {
           CarID = carId;
            Model=name;
            Color = color;
            Fueltype = fuelType;
            Manufacturer = manufacturer;
            Rating = rating;
           ReviewDescription = description;
        }

        public ReviewItemsDTO(int carId, string name, string color, string fuelType, string manufacturer, int? rating, string? description)
        {
            CarID = carId;
            this.name = name;
            Color = color;
            Fueltype = fuelType;
            Manufacturer = manufacturer;
            this.rating = rating;
            this.description = description;
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
                return obj is ReviewItemsDTO dTO &&
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
