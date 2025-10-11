namespace E_Commerce.Models
{
    public class Feedback : IEntity<int>
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
