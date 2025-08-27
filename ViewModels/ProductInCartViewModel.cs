namespace E_Commerce.ViewModels
{
    public class ProductInCartViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image {  get; set; }
        public decimal Price {  get; set; }
        public int AvaliableInStock { get; set; }
        public int OrderedQuantity { get; set; }

    }
}
