namespace E_Commerce.Models
{
    public class Cart:IEntity<int>
    {
        
        public int Id { get; set; }
        public int Quantity {  get; set; }
        public string CustomerId {  get; set; }
        public Customer Customer { get; set; }


        public List<Coupon> Coupons { get; set; } = new();
        public List<Product> Products { get; set; } = new();
        

    }
}
