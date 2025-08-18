namespace E_Commerce.Models
{
    public class CustomerMessages
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Name { get; set; }

        public int? CustomerId { get; set; }
        public Customer? Customer { get; set; }


    }
}
