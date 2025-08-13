namespace E_Commerce.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
        public List<ProductCart> Carts { get; set; }
        public List<Feedback> Feedbacks { get; set; }
        public List<Image> Images { get; set; }
        public List<ProductSeller> Sellers { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        
    }
}
