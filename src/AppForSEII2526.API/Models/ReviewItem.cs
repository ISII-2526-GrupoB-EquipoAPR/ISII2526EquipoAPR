namespace AppForSEII2526.API.Models
{
    [PrimaryKey(nameof(CarId), nameof(ReviewId))]
    public class ReviewItem
    {
        public Car Car { get; set; }

        [StringLength(200, ErrorMessage = "La descripción no puede ser mayor de 200 caracteres.")]
        public string? Description { get; set; }

        public int CarId { get; set; }

        public int ReviewId { get; set; }

        public Review Review { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor que 1")]
        public int Quantity { get; set; }
        [Range(1, 5, ErrorMessage = "La valoración debe estar entre 1 y 5")]
        public int Rating { get; set; }
    }
}
