using E_Commerce.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.ViewModels
{
    public class ProductViewModel
    {
        public int? Id { get; set; }
        [Range(0, int.MaxValue,ErrorMessage ="Invalid Amount should be positive value")]
        public int Amount { get; set; }
        [Range(0, (double)decimal.MaxValue, ErrorMessage = "Invalid Amount should be positive value")]
        public decimal Price { get; set; }
        public DateTime ?InsertionDate { get; set; }
        public string Name { get; set; }
        [DisplayName("Category")]
        public int CategoryId {  get; set; }
        public string Description { get; set; }
        public List<FilterWord>? Filters { get; set; } = new();
        public List<IFormFile>? Images { get; set; }= new List<IFormFile>();
        public List<Seller>? Sellers { get; set; } = new();
        public List<Category>? ShowCategoriesList { get; set; } = new();

    }
}
