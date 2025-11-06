using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Net;

namespace AppForSEII2526.API.Models;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser {
    public ApplicationUser()
    {
    }

    public ApplicationUser(string id, string name, string surname, string userName, string address)
    {
        Id = id;
        Name = name;
        Surname = surname;
        UserName = userName;
        Email = userName;
        Address = address;
    }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Surname { get; set; }
    [Required]
    public string Address { get; set; }
    public IList<Purchase> Purchases { get;set;} = new List<Purchase>();
    public IList<Review> Reviews { get; set; } = new List<Review>();
    public IList<Rental> Rentals { get; set; } = new List<Rental>();

}