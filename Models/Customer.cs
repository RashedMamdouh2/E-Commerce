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
        public List<Cart> Carts { get; set; }
        public List<Coupon> Coupons { get; set; }

    }
}
