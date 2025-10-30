
using AppForSEII2526.API.Models;

namespace AppForSEII2526.API.DTOs.RentalDTOs
{
    public class RentalDetailDTO
    {


        public RentalDetailDTO(int id, string customerUserName, string customerNameSurname, string deliveryAddress, PaymentMethodTypes paymentMethod, DateTime startDate, DateTime endDate, DateTime rentingDate, decimal totalPrice, IList<RentalItemDTO> rentalItems)
        {
            CustomerUserName = customerUserName ?? throw new ArgumentNullException(nameof(customerUserName)); ;
            CustomerNameSurname = customerNameSurname ?? throw new ArgumentNullException(nameof(customerNameSurname)); ;
            DeliveryAddress = deliveryAddress ?? throw new ArgumentNullException(nameof(deliveryAddress)); ;
            PaymentMethod = paymentMethod;
            StartDate = startDate;
            EndDate = endDate;
            RentingDate = rentingDate;

            }
      


        [EmailAddress]
        [Required]
        public string CustomerUserName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Porfavor, escriba su nombre y apellido")]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "Nombre y apellidos")]
        public string CustomerNameSurname { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.MultilineText)]
        [Display(Name = "Delivery Address")]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "La direccion debe tener 10 caracteres")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Porfavor, escirba su direccion")]
        public string DeliveryAddress { get; set; }


        public PaymentMethodTypes PaymentMethod { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime RentingDate { get; set; }
        public IList<RentalItemDTO> RentalItems { get; set; }

        private int NumberOfDays
        {
            get
            {
                return (EndDate - StartDate).Days;
            }
        }

        [Display(Name = "Total Price")]
        [JsonPropertyName("TotalPrice")]


        public decimal TotalPrice
        {
            get
            {
                return RentalItems.Sum(item => item.RentingPrice * NumberOfDays);
            }
        }


        protected bool CompareDate(DateTime date1, DateTime date2)
        {
            return (date1.Subtract(date2) < new TimeSpan(0, 1, 0));
        }
       


        public override bool Equals(object? obj)
        {
            return obj is RentalDetailDTO dTO
                   && CustomerUserName == dTO.CustomerUserName
                   && CustomerNameSurname == dTO.CustomerNameSurname
                   && DeliveryAddress == dTO.DeliveryAddress
                   && PaymentMethod == dTO.PaymentMethod
                   && CompareDate(StartDate, dTO.StartDate)
                   && CompareDate(EndDate, dTO.EndDate)
                   && CompareDate(RentingDate, dTO.RentingDate)
                   && TotalPrice == dTO.TotalPrice
                   && EqualityComparer<IList<RentalItemDTO>>.Default.Equals(RentalItems, dTO.RentalItems);




        }

        public override int GetHashCode()
        {
            return HashCode.Combine(CustomerUserName, CustomerNameSurname, DeliveryAddress, PaymentMethod, StartDate, EndDate,RentalItems, TotalPrice);
        }
    }
   }


