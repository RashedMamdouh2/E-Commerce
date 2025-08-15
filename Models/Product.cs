namespace E_Commerce.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
        public List<ProductCart> Carts { get; set; } = new List<ProductCart>();
        public List<Feedback> Feedbacks { get; set; } = new List<Feedback>();
        public List<Image> Images { get; set; } = new List<Image>();
        public List<FilterWord> Filters { get; set; } = new List<FilterWord>();
        public List<ProductSeller> Sellers { get; set; }=new List<ProductSeller>();
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        
    }
}
