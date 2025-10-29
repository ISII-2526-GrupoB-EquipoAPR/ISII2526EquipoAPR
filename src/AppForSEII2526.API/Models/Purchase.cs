namespace AppForSEII2526.API.Models
{
	public enum PaymentMethodTypes
	{
		Visa,
		GooglePay,
        Paypal
	}
    public class Purchase
	{
        public Purchase()
        {
            PurchaseItems= new List<PurchaseItem>();
        }

        public Purchase(int id, decimal totalPrice, string customerUserName, string customerNameSurname, string deliveryAddress, string deliveryCarDealer, PaymentMethodTypes paymentMethod, DateTime purchasingDate, decimal purchasingPrice, IList<PurchaseItem> purchaseItems, ApplicationUser applicationUser)
        {

            TotalPrice = decimal.Round(purchaseItems.Sum(pi => pi.Price * pi.Quantity),2);

            CustomerUserName = customerUserName;
            CustomerNameSurname = customerNameSurname;
            DeliveryAddress = deliveryAddress;
            DeliveryCarDealer = deliveryCarDealer;
            PaymentMethod = paymentMethod;
            PurchasingDate = purchasingDate;
            PurchasingPrice = purchasingPrice;
            PurchaseItems = purchaseItems;
            ApplicationUser = applicationUser;
        }

        public int Id { get; set; }

        [Precision(10, 2)]
        public decimal TotalPrice { get; set; }

        public string CustomerUserName { get; set; }

        public string CustomerNameSurname { get; set; } 

        [DataType(System.ComponentModel.DataAnnotations.DataType.MultilineText)]
        [Display(Name = "Dirección de envío")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Por favor, selecciona su dirección para el envío")]
        public string DeliveryAddress { get; set; }

        public string DeliveryCarDealer { get; set; }

        [EnumDataType(typeof(PaymentMethodTypes), ErrorMessage = "El tipo de coche no es válido.")]
		public PaymentMethodTypes PaymentMethod { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha de compra")]
        public DateTime PurchasingDate { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        [Range(1, float.MaxValue, ErrorMessage = "El mínimo precio es 1.")]
        [Display(Name = "Precio de compra")]
        [Precision(10, 2)]
        public decimal PurchasingPrice { get; set; }

        public IList<PurchaseItem> PurchaseItems { get; set; } = new List<PurchaseItem>();

        public ApplicationUser ApplicationUser { get; set; }






    }
}
