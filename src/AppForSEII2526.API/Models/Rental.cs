namespace AppForSEII2526.API.Models
{
    public enum PaymentMethodTypes2
    {
        Visa,
        GooglePay,
        PayPal
    }
    public class Rental
    {
        public string deliveryCarDealer { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha de finalización")]
        public DateTime endDate { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha de comienzo")]
        public DateTime startDate { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha de renta")]
        public DateTime rentingDate { get; set; }

        [EnumDataType(typeof(PaymentMethodTypes), ErrorMessage = "El tipo de pago no es válido.")]
        public PaymentMethodTypes2 paymentMethod { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        [Range(1, float.MaxValue, ErrorMessage = "El mínimo precio es 1.")]
        [Display(Name = "Precio total de renta")]
        [Precision(10, 2)]
        public decimal totalPrice { get; set; }

        public IList<RentalItem> rentalItems { get; set; } = new List<RentalItem>();

    }
}
