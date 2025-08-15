using E_Commerce.Models;
using E_Commerce.Repository;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IRepository<Category> repository;
        private readonly IRepository<Product> productRepo;

        public CategoryController(IRepository<Category>repository,IRepository<Product>productRepo)
        {
            this.repository = repository;
            this.productRepo = productRepo;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ShowCategory(int id,string input=null)
        {
            Category category;
            if (input is not null)
            {
                 category = repository.GetById(id,getProducts:false);
                var Repo = productRepo as ProductRepo;
                category.Products = Repo.SearchProduct(input);
            }
            else
            {
                category = repository.GetById(id, getProducts: true);

            }
            ViewBag.searchValue = input;

            return View(category);
        }
     
    }
}
