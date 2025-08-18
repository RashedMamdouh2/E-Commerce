namespace E_Commerce.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }


        public List<FilterWord> Filters { get; set; } = new();
        public List<Product> Products { get; set; }= new();
        public List<Image>Images { get; set; }=new List<Image>();


    }
}
