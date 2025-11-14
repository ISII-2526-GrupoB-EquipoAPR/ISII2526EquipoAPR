using AppForSEII2526.API.DTOs.ReviewDTOs;
using AppForSEII2526.API.Models;

namespace AppForSEII2526.API.DTOs.ReviewDTOs
{
    public class ReviewForCreateDTO
    {
        public ReviewForCreateDTO()
        {
        }

        public ReviewForCreateDTO(int id, string customerUserName, string country, string driverType, IList<ReviewItemsDTO> reviewItems)
        {
            Id = id;
            CustomerUserName = customerUserName ?? throw new ArgumentNullException(nameof(customerUserName));
            Country = country ?? throw new ArgumentNullException(nameof(country));
            DriverType = driverType ?? throw new ArgumentNullException(nameof(driverType));
        }
        
        public int Id { get; set; }

        [EmailAddress]

        [Required(AllowEmptyStrings = false, ErrorMessage = "Por favor, introduzca su nombre")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre debe tener al menos 3 caracteres")]
        public string CustomerUserName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Por favor, introduzca su país")]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "El nombre debe tener al menos 3 caracteres")]
        public string Country { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Por favor, introduzca su tipo de conductor")]
        public string DriverType { get; set; }
       IList<ReviewItemsDTO> ReviewItems{ get; set; }  




        public override bool Equals(object? obj)
        {
            return obj is ReviewForCreateDTO dTO &&
                     Id == dTO.Id &&
                     CustomerUserName == dTO.CustomerUserName &&
                     Country == dTO.Country &&
                     DriverType == dTO.DriverType &&
                     ReviewItems.SequenceEqual(dTO.ReviewItems);


        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, CustomerUserName, Country, DriverType,ReviewItems);
        }
    }
}
