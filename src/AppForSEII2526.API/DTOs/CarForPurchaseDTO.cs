namespace AppForSEII2526.API.DTOs
{
    public class CarForPurchaseDTO
    {
        public CarForPurchaseDTO(){
        }

        public CarForPurchaseDTO(int id, string model, string color, string fuelType, string manufacturer, decimal purchasingPrice)
        {
            Id = id;
            Model = model;
            Color = color;
            FuelType = fuelType;
            Manufacturer = manufacturer;
            PurchasingPrice = purchasingPrice;
        }
        public int Id { get; set; }

        [StringLength(50, ErrorMessage = "El modelo tiene una longitud máxima de 50 caracteres", MinimumLength = 4)]
        public string Model { get; set; }

        [StringLength(10, MinimumLength = 1, ErrorMessage = "El color debe tener entre 1 y 10 caracteres.")]
        public string Color { get; set; }

        [StringLength(10, MinimumLength = 1, ErrorMessage = "El tipo de combustible debe tener entre 1 y 10 caracteres.")]
        public string FuelType { get; set; }

        [StringLength(20, MinimumLength = 1, ErrorMessage = "El fabricante debe tener entre 1 y 20 caracteres.")]
        public string Manufacturer { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        [Range(1, float.MaxValue, ErrorMessage = "El mínimo precio es 1.")]
        [Display(Name = "Precio de compra")]
        [Precision(10, 2)]
        public decimal PurchasingPrice { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is CarForPurchaseDTO dTO &&
                   Id == dTO.Id &&
                   Model == dTO.Model &&
                   Color == dTO.Color &&
                   FuelType == dTO.FuelType &&
                   Manufacturer == dTO.Manufacturer &&
                   PurchasingPrice == dTO.PurchasingPrice;
        }
    }
}
