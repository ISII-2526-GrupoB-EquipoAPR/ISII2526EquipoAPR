
using AppForSEII2526.API.Models;

namespace AppForSEII2526.API.DTOs.PurchaseDTOs
{
    public class PurchaseDetailDTO
    {
        public PurchaseDetailDTO()
        {
        }

        public PurchaseDetailDTO(string customerUserName, string customerNameSurname, PaymentMethodTypes paymentMethod, string deliveryAddress, IList<PurchaseItemDTO> purchaseItems)
        {
            CustomerUserName = customerUserName;
            CustomerNameSurname = customerNameSurname;
            PaymentMethod = paymentMethod;
            DeliveryAddress = deliveryAddress;
            PurchaseItems = purchaseItems ?? throw new ArgumentNullException(nameof(purchaseItems));
        }

        [EmailAddress]
        [Required]
        public string CustomerUserName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Por favor, introduzca su nombre y apellidos")]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "El nombre y apellido deben tener al menos 10 caracteres")]
        public string CustomerNameSurname { get; set; }

        [Required]
        public PaymentMethodTypes PaymentMethod { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.MultilineText)]
        [Display(Name = "Dirección de envío")]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "La dirección debe tener mínimo 10 caracteres")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Por favor, introduzca su dirección para el envío")]
        public string DeliveryAddress { get; set; }

        public IList<PurchaseItemDTO> PurchaseItems { get; set; }

        [Display(Name = "Precio total")]
        [JsonPropertyName("PrecioTotal")]
        public decimal TotalPrice
        {
            get
            {
                return PurchaseItems.Sum(item => item.PurchasingPrice * item.Quantity);
            }
        }

        public override bool Equals(object? obj)
        {
            return obj is PurchaseDetailDTO dTO &&
                   CustomerUserName == dTO.CustomerUserName &&
                   CustomerNameSurname == dTO.CustomerNameSurname &&
                   PaymentMethod == dTO.PaymentMethod &&
                   DeliveryAddress == dTO.DeliveryAddress &&
                   EqualityComparer<IList<PurchaseItemDTO>>.Default.Equals(PurchaseItems, dTO.PurchaseItems) &&
                   TotalPrice == dTO.TotalPrice;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(CustomerUserName, CustomerNameSurname, PaymentMethod, DeliveryAddress, PurchaseItems, TotalPrice);
        }
    }
}
