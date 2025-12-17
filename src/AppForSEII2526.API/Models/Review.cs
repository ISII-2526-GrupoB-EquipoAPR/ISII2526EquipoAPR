using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AppForSEII2526.API.Models
{
 
   
    public class Review
    {
        public Review()
        {
            ReviewItems = new List<ReviewItem>();
        }

        public Review(string customerUserName, string country, string driverType)
        {
            Country = country;
        }
     

        public  Review(DateTime created ,string customerusername, string country, int driverType, IList<ReviewItem> reviewItems, ApplicationUser applicationUser)
        {
            Created = created;
            CustomerUserName = customerusername ?? throw new ArgumentNullException(nameof(customerusername));
            Country = country ?? throw new ArgumentNullException(nameof(country));
            DriverType = driverType;
            ReviewItems = reviewItems ?? throw new ArgumentNullException(nameof(reviewItems));
            ApplicationUser = applicationUser ?? throw new ArgumentNullException(nameof(applicationUser));

        }
        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha de creación")]
        public DateTime Created { get; set; }
        public string CustomerUserName { get; set; }

        public int Id { get; set; }

        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "El país solo puede contener letras.")]
        [StringLength(25, ErrorMessage = "El nombre del país no puede superar los 25 caracteres.")]
        public string Country { get; set; }


        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "El país solo puede contener letras.")]
        [StringLength(25, ErrorMessage = "El nombre del país no puede superar los 25 caracteres.")]
        public int DriverType { get; set; }
        public IList<ReviewItem> ReviewItems { get; set; } =   new List<ReviewItem>();
        public ApplicationUser ApplicationUser { get; set; }
        }
}
