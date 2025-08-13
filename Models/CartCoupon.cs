namespace E_Commerce.Models
{
    public class CartCoupon
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public int CouponId { get; set; }
        public Cart Cart { get; set; }
        public Coupon Coupon { get; set; }

    }
}
