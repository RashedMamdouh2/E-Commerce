namespace E_Commerce.Models
{
    public class Coupon
    {
        public int Id { get; set; }
        public decimal Value { get; set; }
        public string Description { get; set; }
        List<CartCoupon> Carts { get; set; }
    }
}
