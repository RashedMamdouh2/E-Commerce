using E_Commerce.Models;
using E_Commerce.Repository;
using E_Commerce.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace E_Commerce.Controllers
{
    [Authorize(Roles ="admin")]
    public class AdminController : Controller
    {
        private readonly IGeneralRepo<Product, int> productRepo;
        private readonly IGeneralRepo<Category,int> categoryRepo;
        private readonly IGeneralRepo<Image, int> imageRepo;
        private readonly IGeneralRepo<Filter, int> filterRepo;
        private readonly IGeneralRepo<Order, int> orderRepo;

        public AdminController(
             IGeneralRepo<Product, int> productRepo,
            IGeneralRepo<Category, int> CategoryRepo,
            IGeneralRepo<Image, int> ImageRepo,
            IGeneralRepo<Filter, int> filterRepo,
            IGeneralRepo<Order,int>orderRepo
            )
        {
            this.productRepo = productRepo;
            categoryRepo = CategoryRepo;
            imageRepo = ImageRepo;
            this.filterRepo = filterRepo;
            this.orderRepo = orderRepo;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public  IActionResult AddProduct()
        {
            var model = new ProductViewModel { ShowCategoriesList = categoryRepo.FindAll(c => true, new string[] { }).ToList() };
            return View(model);
        }
        //[HttpPost]
        public async Task<IActionResult> SaveProduct(ProductViewModel model,string SelectedFilters="")
        {
            var imagesList=GetImages(model.ImagesFile); 
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
                var state= await productRepo.AddAsync(product);
                if (state)
                {
                    await productRepo.SaveAsync();
                   await AddfiltersToProductAsync(SelectedFilters,product.Id);
                    return Json(new { redirectUrl = Url.Action("ShowCategory", "Category", new{ id=model.CategoryId}) });
                }
                
            }
            return View("AddProduct",model);
        }
        [HttpGet]

        public async Task<IActionResult> ShowAllProducts()
        {
            List<Product>all=await productRepo.GetAllAsync();
            var categories = categoryRepo.FindAll(c => true, new string[] { }).Select(c => new CategoryViewModel { Id=c.Id,Name=c.Name}).ToList();
            ViewBag.categories = categories;
            return View(all);
        }
        public async Task<IActionResult> EditAsync(int id)
        {
            var product = await productRepo.FindAsync(x=>x.Id==id,new string[] { "Filters" });
            var viewModel = new ProductViewModel
            {
                Id=product.Id,
                Name=product.Name,
                Description=product.Description,
                Amount=product.Amount,
                Price=product.Price,
                Filters=product.Filters ,
                CategoryId=product.CategoryId,
                InsertionDate=product.InsertionDate,
            };
            viewModel.ShowCategoriesList = await categoryRepo.GetAllAsync();
            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> SaveEditAsync(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var product = await productRepo.GetByIdAsync((int)model.Id!);
                product.Name = model.Name;
                product.Description = model.Description;
                product.Price = model.Price;
                product.InsertionDate = model.InsertionDate;
                product.Amount = model.Amount;
                product.CategoryId = model.CategoryId;


               await productRepo.UpdateAsync(product);
                await productRepo.SaveAsync();
            return RedirectToAction(controllerName: "Admin", actionName: "ShowAllProducts");
            }
            return View("Edit",model);


        }
        [HttpGet]
        public async Task<IActionResult> DeleteAsync(int id)
        {
           
              await  productRepo.DeleteByIdAsync(id);

            
            return RedirectToAction(controllerName: "Admin", actionName: "ShowAllProducts");

        }
        public IActionResult ShowOrders()
        {
            List<Order> orders =  orderRepo.FindAll(order=>true,new string[] {nameof(Customer)}).ToList();
       
            return View(orders);
        }
        public IActionResult Dashboard()
        {
            return View();
        }
        private async Task AddfiltersToProductAsync(string selectedFilters,int productId)
        {
            var filterIds = selectedFilters.Split(',').Select(s => Convert.ToInt32(s)).ToList();
            List<Filter> filters = new();
            foreach (var filterId in filterIds)
            {
                filters.Add(await filterRepo.GetByIdAsync(filterId));

                
            }
            var product = await productRepo.FindAsync(p=>p.Id==productId,new string[] { nameof(Product.Filters) } );
            product.Filters.AddRange(filters);
            await productRepo.SaveAsync();

           
            
            
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
