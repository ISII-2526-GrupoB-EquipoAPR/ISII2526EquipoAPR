namespace AppForSEII2526.API.DTOs
{
   
        public class CarForRentalDTO
        {
            public CarForRentalDTO()
            {
            }

            public CarForRentalDTO(int id, string model, string fuelType, string color, DateTime rentingDate, decimal priceForRenting)
            {
                Id = id;
                Model = model;
                FuelType = fuelType;
                Color = color;
                RentingDate = rentingDate;
                PriceForRenting = priceForRenting;
            }
            public CarForRentalDTO(int id, string model, string color, DateTime rentingDate, double priceForRenting, DateTime? lastRental) : this(id, model, color, rentingDate, priceForRenting)
            {
                LastRental = lastRental;
            }

            public int Id { get; set; }

            [StringLength(50, ErrorMessage = "Modelo puede tener como maximo 50 caracteres")]
            public string Model { get; set; }

            [StringLength(50, ErrorMessage = "Tipo de gasoil puede tener como maximo 50 caracteres")]
            public string FuelType { get; set; }

            [StringLength(50, ErrorMessage = "Color puede tener como maximo 50 caracteres")]
            public string Color { get; set; }

            [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
            [Display(Name = "Fecha para alquilar")]
            public DateTime RentingDate { get; set; }

            [Required]
            [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
            [Range(1, float.MaxValue, ErrorMessage = "Precio minimo es 1")]
            [Display(Name = "Precio para alquilar")]
            public decimal PriceForRenting { get; set; }


            public DateTime? LastRental { get; set; }

            public override bool Equals(object? obj)
            {
                return obj is CarForRentalDTO dTO
                    && Id == dTO.Id
                    && Model == dTO.Model
                    && FuelType == dTO.FuelType
                    && Color == dTO.Color
                    && RentingDate == dTO.RentingDate
                    && PriceForRenting == dTO.PriceForRenting;


            }
            public override int GetHashCode()
            {
                return HashCode.Combine(Id, Model, FuelType, Color, RentingDate, PriceForRenting);
            }






        }
    }


