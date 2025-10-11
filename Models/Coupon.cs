namespace E_Commerce.Models
{
    public class Coupon:IEntity<int>
    {
        public int Id { get; set; }
        public decimal Value { get; set; }
        public string Description { get; set; }
        public Cart ? Cart { get; set; }
        public List<Customer> Customers { get; set; } = new();
    }
}
