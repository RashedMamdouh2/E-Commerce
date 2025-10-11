namespace E_Commerce.Models
{
    public class Category:IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }


       
        public List<Product> Products { get; set; } = new();
        public Image Image { get; set; }
        public List<Filter> Filters { get; set; } = new();


    }
}
