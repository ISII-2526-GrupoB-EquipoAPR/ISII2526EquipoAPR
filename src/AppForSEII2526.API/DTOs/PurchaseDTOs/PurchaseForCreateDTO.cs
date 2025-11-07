using AppForSEII2526.API.DTOs.RentalDTOs;
using AppForSEII2526.API.Models;

namespace AppForSEII2526.API.DTOs.PurchaseDTOs
{
    public class PurchaseForCreateDTO
    {

        public PurchaseForCreateDTO(string deliveryAddress, string customerUserName, string customerNameSurname, IList<PurchaseItemDTO> purchaseItems, PaymentMethodTypes paymentMethod)
        {
            DeliveryAddress = deliveryAddress ?? throw new ArgumentNullException(nameof(deliveryAddress));
            CustomerUserName = customerUserName ?? throw new ArgumentNullException(nameof(customerUserName));
            CustomerNameSurname = customerNameSurname ?? throw new ArgumentNullException(nameof(customerNameSurname));
            PurchaseItems = purchaseItems ?? throw new ArgumentNullException(nameof(purchaseItems));
            PaymentMethod = paymentMethod;
        }
        public PurchaseForCreateDTO()
        {
            PurchaseItems = new List<PurchaseItemDTO>();
        }


        [DataType(System.ComponentModel.DataAnnotations.DataType.MultilineText)]
        [Display(Name = "Dirección de envío")]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "La dirección de envío tiene que tener, al menos 10 caracteres")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Por favor, introduzca su dirección para el envío")]
        public string DeliveryAddress { get; set; }

        [EmailAddress]
        [Required]
        public string CustomerUserName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please, set your Name and Surname")]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "Name and Surname must have at least 10 characters")]
        public string CustomerNameSurname { get; set; }

        public IList<PurchaseItemDTO> PurchaseItems { get; set; }

        [Required]
        public PaymentMethodTypes PaymentMethod { get; set; }

        [Display(Name = "Total Price")]
        [JsonPropertyName("TotalPrice")]
        public decimal TotalPrice
        {
            get
            {
                return PurchaseItems.Sum(item => item.PurchasingPrice * item.Quantity);
            }
        }

        public override bool Equals(object? obj)
        {
            return obj is PurchaseForCreateDTO dTO &&
                   DeliveryAddress == dTO.DeliveryAddress &&
                   CustomerUserName == dTO.CustomerUserName &&
                   CustomerNameSurname == dTO.CustomerNameSurname &&
                   PurchaseItems.SequenceEqual(dTO.PurchaseItems) &&
                   PaymentMethod == dTO.PaymentMethod &&
                   TotalPrice == dTO.TotalPrice;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(DeliveryAddress, CustomerUserName, CustomerNameSurname, PurchaseItems, PaymentMethod, TotalPrice);
        }
    }
}
