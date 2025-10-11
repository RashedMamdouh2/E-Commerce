using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Models
{
    public class Customer:IdentityUser,IEntity<string>
    {
    
        public string Address { get; set; }
        public string City { get; set; }
        public string ?Region { get; set; }
        public string ?PostalCode { get; set; }

        public List<Feedback> Feedbacks { get; set; } = new();
        public List<Coupon> Coupons { get; set; } = new();
        public List<Order> Orders { get; set; } = new();
        public List<Message> Messages { get; set; } = new();

        [ForeignKey(nameof(Cart))]
        public int ?CartId { get; set; }
        public Cart? Cart { get; set; } 


        
    }
}
