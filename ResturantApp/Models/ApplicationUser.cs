using Microsoft.AspNetCore.Identity;

namespace ResturantApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Order>? Orders { get; set; }
    }
}
