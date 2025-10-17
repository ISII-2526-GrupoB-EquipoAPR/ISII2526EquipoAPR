using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AppForSEII2526.API.Models
{
    public enum DriverTypes
    {
          Novato,
          Experto
    }
    public class Review
    {
        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha de creación")]
        public DateTime Created { get; set; }

        public int Id { get; set; }

        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "El país solo puede contener letras.")]
        [StringLength(25,ErrorMessage = "El nombre del país no puede superar los 25 caracteres.")]
        public string Country { get; set; }

        [EnumDataType(typeof(DriverTypes), ErrorMessage = "El tipo de conductor no es válido.")]
        public DriverTypes DriverType { get; set; }
        public IList<ReviewItem> ReviewItems { get; set; } =   new List<ReviewItem>();
        public ApplicationUser ApplicationUser { get; set; }
        }
}
