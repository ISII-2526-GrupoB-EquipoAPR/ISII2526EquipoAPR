namespace AppForSEII2526.API.Models
{
	public enum PaymentMethodTypes
	{
		Visa,
		GooglePay
	}
    public class Purchase
	{
        public int Id { get; set; }
        
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







    }
}
