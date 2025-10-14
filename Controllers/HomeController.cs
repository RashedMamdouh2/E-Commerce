using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;
using E_Commerce.Models;
using E_Commerce.Repository;
using E_Commerce.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGeneralRepo<Category, int> categoryRepo;
        private readonly IGeneralRepo<Message, int> messagesRepo;
        private readonly IGeneralRepo<Product, int> productRepo;
        private readonly IGeneralRepo<Customer, string> customerRepo;
        private readonly IGeneralRepo<Order, int> orderRepo;

        public HomeController(ILogger<HomeController> logger, IGeneralRepo<Category, int> categoryRepo, IGeneralRepo<Message, int> messagesRepo, IGeneralRepo<Product, int> productRepo, IGeneralRepo<Customer, string> customerRepo, IGeneralRepo<Order, int> orderRepo)
        {
            _logger = logger;
            this.categoryRepo = categoryRepo;
            this.messagesRepo = messagesRepo;
            this.productRepo = productRepo;
            this.customerRepo = customerRepo;
            this.orderRepo = orderRepo;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var categories =  categoryRepo.FindAll(c=>true,new string[] { nameof(Category.Image)}).ToList();  
            var messages = await messagesRepo.GetAllAsync();
            var newProducts =   ( productRepo.FindAll(p=>true,new string[] { nameof(Product.Images)})).OrderByDescending(p=>p.InsertionDate).Take(3);
            var model = new HomeViewModel
            {
                Categories = categories,
                Messages = messages,
                NewItems = newProducts.ToList()

            };
            ViewBag.CustomersNumber = customerRepo.Count();
            ViewBag.OrdersNumber = orderRepo.Count();
            ViewBag.ProductssNumber = productRepo.Count();
            return View(model);
        }
        
        public async Task<IActionResult> ReciveMessage(MessageViewModel messages)
        {
            
                
            
            if (User.Identity.IsAuthenticated&&ModelState.IsValid)
            {
                Message obj = new Message
                {
                    Name=messages.Name,
                    Email=messages.Email,
                    Subject=messages.Subject,
                    Content=messages.Content,
                    CustomerId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value

                };
              await messagesRepo.AddAsync(obj);
                var res = await messagesRepo.SaveAsync();
            return Content(res?"OK":"Fail");
            }
            return Content("Fail");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
