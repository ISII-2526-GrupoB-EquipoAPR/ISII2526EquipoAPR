namespace AppForSEII2526.API.Models
{
    [PrimaryKey(nameof(CarId), nameof(PurchaseId))]
    public class PurchaseItem
{
	public Car Car { get; set; }

    public int CarId { get; set; }

    public int PurchaseId { get; set; }

    public Purchase Purchase { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor que 1")]
    public int Quantity { get; set; }


    }
}
