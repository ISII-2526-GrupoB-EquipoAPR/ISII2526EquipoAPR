namespace AppForSEII2526.API.Models
{ 
public class Car
{
        public Car() { }
        public Car(string carClass, string color, string description, string engDisplacement, string manufacturer, decimal purchasingPrice, int quantity, string fuelType, string rimSize, Model model)
        {
            CarClass = carClass;
            Color = color;
            Description = description;
            EngDisplacement = engDisplacement;
            Manufacturer = manufacturer;
            PurchasingPrice = purchasingPrice;
            QuantityForPurchasing = quantity;
            FuelType = fuelType;
            RimSize = rimSize;
            Model = model;
        }
        

        public Car(string carClass, string color, string? description, string engDisplacement, int id, string manufacturer, decimal purchasingPrice, decimal rentingPrice, int quantityForPurchasing, int quantityForRenting, string fuelType, string rimSize, Model model)
        {
            CarClass = carClass;
            Color = color;
            Description = description;
            EngDisplacement = engDisplacement;
            Id = id;
            Manufacturer = manufacturer;
            PurchasingPrice = purchasingPrice;
            RentingPrice = rentingPrice;
            QuantityForPurchasing = quantityForPurchasing;
            QuantityForRenting = quantityForRenting;
            FuelType = fuelType;
            RimSize = rimSize;
            Model = model;
        }
        public Car(decimal rentingPrice, string carClass, string color, string? description, string engDisplacement, string manufacturer, int quantityForRenting, string fuelType, string rimSize, Model model)
        {
            CarClass = carClass;
            Color = color;
            Description = description;
            EngDisplacement = engDisplacement;
            Manufacturer = manufacturer;          
            RentingPrice = rentingPrice;          
            QuantityForRenting = quantityForRenting;
            FuelType = fuelType;
            RimSize = rimSize;
            Model = model;
        }


        [StringLength(10, MinimumLength = 1, ErrorMessage = "La clase del coche debe tener entre 1 y 10 caracteres.")]
    public string CarClass { get; set; }
    [StringLength(10, MinimumLength = 1, ErrorMessage = "El color debe tener entre 1 y 10 caracteres.")]
    public string Color { get; set; }

    [StringLength(200, ErrorMessage = "La descripción no puede ser mayor de 200 caracteres.")]
    public string? Description { get; set; }
    public string EngDisplacement { get; set; }

    public int Id { get; set; }

    [StringLength(20, MinimumLength = 1, ErrorMessage = "El fabricante debe tener entre 1 y 20 caracteres.")]
    public string Manufacturer { get; set; }

    [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
    [Range(1, float.MaxValue, ErrorMessage = "El mínimo precio es 1.")]
    [Display(Name = "Precio de compra")]
    [Precision(10, 2)]
    public decimal PurchasingPrice { get; set; }

    [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
    [Range(1, float.MaxValue, ErrorMessage = "El mínimo precio es 1.")]
    [Display(Name = "Precio de renta")]
    [Precision(10, 2)]
    public decimal RentingPrice { get; set; }

    [Display(Name = "Cantidad de compra")]
    [Range(1, int.MaxValue, ErrorMessage = "La mínima cantidad es 1.")]
    public int QuantityForPurchasing { get; set; }

    [Display(Name = "Cantidad de renta")]
    [Range(1, int.MaxValue, ErrorMessage = "La mínima cantidad es 1.")]
    public int QuantityForRenting { get; set; }

    [StringLength(10, MinimumLength = 1, ErrorMessage = "El tipo de combustible debe tener entre 1 y 10 caracteres.")]
    public string FuelType { get; set; }

    [StringLength(20, MinimumLength = 1, ErrorMessage = "El tamaño de rueda debe tener entre 1 y 20 caracteres.")]
    public string RimSize { get; set; }

    public Model Model { get; set; }

    public IList<PurchaseItem> PurchaseItems { get; set; } = new List<PurchaseItem>();
    
    public IList<ReviewItem> ReviewItems { get; set; } = new List<ReviewItem>();

    public IList<RentalItem> RentalItems { get; set; } = new List<RentalItem>();
    }
}
