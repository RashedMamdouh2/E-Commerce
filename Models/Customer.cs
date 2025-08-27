using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string ?Region { get; set; }
        public string ?PostalCode { get; set; }
        public List<Feedback> Feedbacks { get; set; }
        [ForeignKey(nameof(Cart))]
        public int CartId { get; set; }
        public Cart Cart { get; set; }
        public List<Coupon> Coupons { get; set; }
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
