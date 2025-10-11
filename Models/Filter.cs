namespace E_Commerce.Models
{
    public class Filter : IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
       public List<Product> Products { get; set; } = new();
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }


    }
}
