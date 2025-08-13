namespace E_Commerce.Models
{
    public class Feedback
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
