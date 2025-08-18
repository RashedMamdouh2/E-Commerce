using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Models
{
    [PrimaryKey(nameof(FiltersId),nameof(ProductsId))]
    public class FilterWordProduct
    {
        public int ProductsId { get; set; }
        public int FiltersId { get; set; }
        [ForeignKey(nameof(FiltersId))]
        public FilterWord? Filter { get; set; }
        [ForeignKey(nameof(ProductsId))]
        public Product? Product { get; set; }

    }
}
