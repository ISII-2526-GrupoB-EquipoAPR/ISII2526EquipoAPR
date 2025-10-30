namespace AppForSEII2526.API.DTOs.RentalDTOs
{
    public class RentalForCreateDTO
    {
        public RentalForCreateDTO(string customerUserName, string customerNameSurname, string deliveryAddress, PaymentMethodTypes paymentMethod, DateTime startDate, DateTime endDate, IList<RentalItemDTO> rentalItems)
        {

            CustomerUserName = customerUserName ?? throw new ArgumentNullException(nameof(customerUserName)); ;
            CustomerNameSurname = customerNameSurname ?? throw new ArgumentNullException(nameof(customerNameSurname)); ;
            DeliveryAddress = deliveryAddress ?? throw new ArgumentNullException(nameof(deliveryAddress)); ;
            PaymentMethod = paymentMethod;
            StartDate = startDate;
            EndDate = endDate;
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
        public IList<RentalItemDTO> RentalItems { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is RentalForCreateDTO dTO 
                   && CustomerUserName == dTO.CustomerUserName
                   && CustomerNameSurname == dTO.CustomerNameSurname
                   && DeliveryAddress == dTO.DeliveryAddress
                   && PaymentMethod == dTO.PaymentMethod;


        }
    }

}

