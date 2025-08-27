using Microsoft.AspNetCore.Identity;

namespace E_Commerce.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int? CustomerId { get; set; }
        public Customer ? Customer { get; set; }

    }
}
