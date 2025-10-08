using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace AppForSEII2526.API.Models;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser {

    [Display(Name = "Name")]
    public string Name
    {
        get;
        set;
    }

    [Display(Name = "Surname")]
    public string Surname
    {
        get;
        set;
    }

    public IList<Purchase> Purchases { get;set;} = new List<Purchase>();

}