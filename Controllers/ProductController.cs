using E_Commerce.Models;
using E_Commerce.Repository;
using E_Commerce.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using System.Security.Claims;

namespace E_Commerce.Controllers
{
    public class ProductController : Controller
    {
        private readonly IGeneralRepo<Feedback, int> feedbackRepo;
        private readonly IGeneralRepo<Product,int> productrepo;
        private readonly UserManager<Customer> userManager;

        

        public ProductController(IGeneralRepo<Feedback, int> feedbackRepo, IGeneralRepo<Product, int> productrepo,UserManager<Customer>userManager)
        {
            this.feedbackRepo = feedbackRepo;
            this.productrepo = productrepo;
            this.userManager = userManager;
            
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> ShowProductAsync(int id)
        {
            var product = await productrepo.FindAsync(p=>p.Id==id,new string[] { nameof(Product.Images),nameof(Product.Filters), nameof(Product.Category) });
            var feedbacks =  feedbackRepo.FindAll(f => f.ProductId == id, new string[] { nameof(Feedback.Customer) });
            var productVM=new ProductViewModel
            {
                Id = product.Id,
                Images = product.Images,
                Feedbacks = feedbacks.ToList(),
                Price = product.Price,
                Amount = product.Amount,
                Filters = product.Filters,
                Description = product.Description,
                Name = product.Name,
                CategoryName = product.Category.Name
            };
            return View(productVM);
        }
        [HttpPost]
        public async Task<IActionResult> AddProductReviewAsync(Feedback feedback,string UserId)
        {
            
            feedback.CustomerId = User.Claims.FirstOrDefault(c=>c.Type==ClaimTypes.NameIdentifier).Value;
            await feedbackRepo.AddAsync(feedback);
            await feedbackRepo.SaveAsync();
            return Content ("OK");
        }
   
    }
}
