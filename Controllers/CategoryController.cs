using E_Commerce.Models;
using E_Commerce.Repository;
using E_Commerce.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IRepository<Category,CategoryViewModel> repository;
        private readonly IRepository<Product,ProductViewModel> productRepo;

        public CategoryController(IRepository<Category, CategoryViewModel> repository,IRepository<Product, ProductViewModel> productRepo)
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
        public IActionResult GetFilters(int categoryId)
        {
           var cat= repository.GetById(categoryId, getProducts:false);
            return Json(cat.Filters.Select(f => new {Name=f.Name,Id =f.Id}));
        }
     
    }
}
