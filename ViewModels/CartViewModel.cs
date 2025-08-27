using E_Commerce.Models;

namespace E_Commerce.ViewModels
{
    public class CartViewModel
    {
        public List<ProductInCartViewModel> Products { get; set; } = new();
        public List<Product> RelatedProducts { get; set; } = new();
        public decimal TotalPrice { get; set; }

    }
}
