namespace E_Commerce.Models
{
    public class FilterWord
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Category> Categories { get; set; }
        public List<Product>  Products { get; set; }
       
    }
}
