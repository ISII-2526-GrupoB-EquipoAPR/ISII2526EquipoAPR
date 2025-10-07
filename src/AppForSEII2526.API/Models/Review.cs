namespace AppForSEII2526.API.Models
{
    public class Review
    {
        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha de creación")]
        public DateTime Created { get; set; }

        public IList<ReviewItem> ReviewItems { get; set; } =   new List<ReviewItem>();
    }
}
