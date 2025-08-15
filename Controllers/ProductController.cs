using E_Commerce.Models;
using E_Commerce.Repository;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    public class ProductController : Controller
    {
        private readonly IRepository<Product> repository;

        public ProductController(IRepository<Product>repository)
        {
            this.repository = repository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ShowProduct(int id)
        {
            var product = repository.GetById(id, false);
            return View(product);
        }
        public IActionResult AddProductReview(Feedback feedback)
        {
            var repo = repository as ProductRepo;
            repo.AddFeedback(feedback);
            return Content ("OK");
        }
   
    }
}
