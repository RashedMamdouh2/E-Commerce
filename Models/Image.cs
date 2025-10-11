using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Models
{
    public class Image : IEntity<int>
    {
        public int Id { get; set; }
        public string Url { get; set; }
        
        public int? ProductId { get; set; }
        public int? CategoryId { get; set; }
        
        public Category? Category { get; set; }
        public Product? Product { get; set; }

    }
}
