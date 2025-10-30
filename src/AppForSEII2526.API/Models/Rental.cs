namespace AppForSEII2526.API.Models
{

    public class Rental
    {
        public Rental()
        {
        }

        public Rental(string deliveryAddress, string customerUserName, string customerNameSurname, int id, string deliveryCarDealer, DateTime endDate, DateTime startDate, DateTime rentingDate, PaymentMethodTypes paymentMethod, decimal totalPrice, IList<RentalItem> rentalItems, ApplicationUser applicationUser)
        {
            TotalPrice = rentalItems.Sum(ri => ri.PriceForRenting * (endDate - startDate).Days);

            DeliveryAddress = deliveryAddress;
            CustomerUserName = customerUserName;
            CustomerNameSurname = customerNameSurname;
            Id = id;
            DeliveryCarDealer = deliveryCarDealer;
            EndDate = endDate;
            StartDate = startDate;
            RentingDate = rentingDate;
            PaymentMethod = paymentMethod;
            RentalItems = rentalItems;
            ApplicationUser = applicationUser;
        }

        [DataType(System.ComponentModel.DataAnnotations.DataType.MultilineText)]
        [Display(Name = "Dirección de envío")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Por favor, indica tu dirección para el envío")]
        public string DeliveryAddress { get; set; }
        public string CustomerUserName { get; set; }

        public string CustomerNameSurname { get; set; }
        public int Id { get; set; }
        public string DeliveryCarDealer { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha de finalización")]
        public DateTime EndDate { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha de comienzo")]
        public DateTime StartDate { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha de renta")]
        public DateTime RentingDate { get; set; }

        [EnumDataType(typeof(PaymentMethodTypes), ErrorMessage = "El tipo de pago no es válido.")]
        public PaymentMethodTypes PaymentMethod { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        [Range(1, float.MaxValue, ErrorMessage = "El mínimo precio es 1.")]
        [Display(Name = "Precio total de renta")]
        [Precision(10, 2)]
        public double TotalPrice { get; set; }

        public IList<RentalItem> RentalItems { get; set; } = new List<RentalItem>();
        public ApplicationUser ApplicationUser { get; set; }

    }
}