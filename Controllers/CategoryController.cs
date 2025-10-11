using E_Commerce.Models;
using E_Commerce.Repository;
using E_Commerce.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace E_Commerce.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IGeneralRepo<Category,int> categoryrepo;
        private readonly IGeneralRepo<Product,int> productRepo;

        public CategoryController(IGeneralRepo<Category, int> categoryrepo, IGeneralRepo<Product, int> productRepo)
        {
            this.categoryrepo = categoryrepo;
            this.productRepo = productRepo;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> ShowCategoryAsync(int id,string input=null)
        {
                Category category = await categoryrepo.FindAsync(c=>c.Id==id, new string[] { nameof(category.Filters)});

                var products = productRepo.FindAll(p =>  p.CategoryId==id , new string[] { nameof(Product.Images), nameof(Product.Filters) }).ToList();
            if (!input.IsNullOrEmpty()) products = products.Where(p => p.Name.Contains(input) || p.Description.Contains(input)).ToList();
            category.Products = products;
            ViewBag.searchValue = input;

            return View(category);
        }
        public async Task<IActionResult> GetFiltersAsync(int categoryId)
        {
           var cat= await categoryrepo.FindAsync(cat=>cat.Id==categoryId,new string[] { nameof(Category.Filters)});
            return Json(cat.Filters.Select(f => new {Name=f.Name,Id =f.Id}));
        }
     
    }
}
