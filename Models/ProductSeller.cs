using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Models
{
        [PrimaryKey(nameof(SellerId),new string []{nameof(ProductId) })]
    public class ProductSeller
    {
        public int SellerId { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public Seller Seller { get; set; }
    }
}
