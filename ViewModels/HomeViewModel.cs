using E_Commerce.Models;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.ViewModels
{
    public class HomeViewModel
    {
        public List<Category> Categories { get; set; } = new List<Category>();
        public List<Message> Messages { get; set; }=new List<Message>();
        public List<Product> NewItems { get; set; }=new ();
    }
    public class MessageViewModel
    {
        [Required]
        public string Content { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Name { get; set; }

        
    }
}
