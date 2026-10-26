using AboutMeWeb.Models;
using Microsoft.AspNetCore.Identity;
namespace AboutMeWeb.Data;
// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    public ICollection<Service> Orders { get; set; } = new List<Service>();
}
