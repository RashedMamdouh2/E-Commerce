namespace E_Commerce.Models
{
    public class Seller
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ProductSeller> Products { get; set; }
    }
}
