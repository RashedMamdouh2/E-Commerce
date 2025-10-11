namespace E_Commerce.Models
{
    public class Message : IEntity<int>
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Name { get; set; }

        public string CustomerId { get; set; }
        public Customer Customer { get; set; }


    }
}
