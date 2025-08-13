namespace E_Commerce.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int Quantity {  get; set; }
        public int CustomerId {  get; set; }
        public Customer Customer { get; set; }


        public List<CartCoupon> Coupons { get; set; }
        public List<ProductCart> Products { get; set; }
        

    }
}
