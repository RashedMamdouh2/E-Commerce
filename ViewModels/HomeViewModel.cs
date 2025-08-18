using E_Commerce.Models;

namespace E_Commerce.ViewModels
{
    public class HomeViewModel
    {
        public List<Category> Categories { get; set; } = new List<Category>();
        public List<CustomerMessages> Messages { get; set; }=new List<CustomerMessages>();
        public List<Product> NewItems { get; set; }=new ();
    }
}
