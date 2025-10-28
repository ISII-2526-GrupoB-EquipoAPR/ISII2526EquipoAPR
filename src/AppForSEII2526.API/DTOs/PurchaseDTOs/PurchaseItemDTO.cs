namespace AppForSEII2526.API.DTOs.PurchaseDTOs
{
    public class PurchaseItemDTO
    {
        public PurchaseItemDTO(int carID, string model, string color, decimal purchasingPrice, int quantity, string description)
        {
            CarID = carID;
            Model = model;
            Color = color;
            PurchasingPrice = purchasingPrice;
            Description = description;
            Quantity = quantity;
        }

        public int CarID { get; set; }

        public string Model { get; set; }

        public string Color { get; set; }

        public decimal PurchasingPrice { get; set; }

        public string Description { get; set; }

        [Required]
        public int Quantity { get; set; }


    }
}
