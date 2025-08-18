using E_Commerce.Models;
using E_Commerce.Repository;
using E_Commerce.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    //[Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        private readonly IRepository<Product, ProductViewModel> productRepo;
        private readonly IRepository<Category, CategoryViewModel> categoryRepo;

        public AdminController(
            IRepository<Product,ProductViewModel>productRepo,
            IRepository<Category, CategoryViewModel> CategoryRepo,
            IRepository<Image, CategoryViewModel> ImageRepo
            )
        {
            this.productRepo = productRepo;
            categoryRepo = CategoryRepo;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddProduct()
        {
            var model = new ProductViewModel { ShowCategoriesList = categoryRepo.GetAll() };
            return View(model);
        }
        public IActionResult SaveProduct(ProductViewModel model,string SelectedFilters)
        {
            var imagesList=GetImages(model.Images);
            
           
            if (ModelState.IsValid) {
                var product = new Product
                {

                    Name = model.Name,
                    CategoryId = model.CategoryId,
                    InsertionDate = DateTime.Now,
                    Amount = model.Amount,
                    Price = model.Price,
                    Description = model.Description,
                    Images = imagesList



                };
                var state=productRepo.Add(product);
                if (state)
                {
                    AddfiltersToProduct(SelectedFilters,product.Id);
                    return Json(new { redirectUrl = Url.Action("ShowCategory", "Category", new{ id=model.CategoryId}) });
                }
                
            }
            return View("AddProduct",model);
        }

        public IActionResult ShowAllProducts()
        {
            List<Product>all=productRepo.GetAll();
            var categories = categoryRepo.GetAllًWithNameAndIdOnly();
            ViewBag.categories = categories;
            return View(all);
        }
        public IActionResult Edit(int id)
        {
            var product = productRepo.GetById(id,true);
            var viewModel = new ProductViewModel
            {
                Id=product.Id,
                Name=product.Name,
                Description=product.Description,
                Amount=product.Amount,
                Price=product.Price,
                Filters=product.Filters,
                CategoryId=product.CategoryId,
                InsertionDate=product.InsertionDate,
            };
            viewModel.ShowCategoriesList = categoryRepo.GetAll();
            return View(viewModel);
        }
        public IActionResult SaveEdit(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                int id = (int)model.Id;
                var product = productRepo.GetById(id, false);
                product.Name = model.Name;
                product.Description = model.Description;
                product.Price = model.Price;
                product.InsertionDate = model.InsertionDate;
                product.Amount = model.Amount;
                product.CategoryId = model.CategoryId;


                productRepo.Update(product);
            return RedirectToAction(controllerName: "Admin", actionName: "ShowAllProducts");
            }
            return View("Edit",model);


        }
        public IActionResult Delete(int id)
        {
            if (id > 0)
            {
                productRepo.DeleteById(id);

            }
            return RedirectToAction(controllerName: "Admin", actionName: "ShowAllProducts");

        }
        private void AddfiltersToProduct(string selectedFilters,int productId)
        {
            var filterIds = selectedFilters.Split(',').Select(s => Convert.ToInt32(s)).ToList();
            List<FilterWordProduct> filters = new();
            foreach (var filterId in filterIds)
            {
                filters.Add(new FilterWordProduct { FiltersId = filterId, ProductsId = productId });
            }

            var repo = productRepo as ProductRepo;
            repo.AddFiltersToProduct(filters);
        }

        private List<Image> GetImages(List<IFormFile>Images) {
            var imagesList = new List<Image>();

            foreach (var img in Images)
            {
                var image = new Image();
                image.Url = Path.Combine("/ProductsImages/", img.FileName);
                imagesList.Add(image);

                using (var stream = new FileStream(path: "wwwroot" + image.Url, FileMode.Create))
                {
                    img.CopyTo(stream);
                }
            }
            return imagesList;
        }
    }
}
