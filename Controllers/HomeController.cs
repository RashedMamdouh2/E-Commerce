using System.Diagnostics;
using E_Commerce.Models;
using E_Commerce.Repository;
using E_Commerce.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICategoryRepo categoryRepo;

        //Must be modified the ViewModel for messages 
        private readonly IMessageRepo messagesRepo;
        private readonly IProductRepo productRepo;

        public HomeController(ILogger<HomeController> logger,ICategoryRepo categoryRepo, IMessageRepo messagesRepo, IProductRepo productRepo)
        {
            _logger = logger;
            this.categoryRepo = categoryRepo;
            this.messagesRepo = messagesRepo;
            this.productRepo = productRepo;
        }

        public IActionResult Index()
        {
            var categories = categoryRepo.GetAll().ToList();  
            var messages = messagesRepo.GetAll().ToList();
            var newProducts = productRepo.GetNewstItems(3).ToList();
            var model = new HomeViewModel
            {
                Categories = categories,
                Messages = messages,
                NewItems=newProducts
                
            };

            return View(model);
        }

        public IActionResult ReciveMessage(CustomerMessages messages)
        {
            var res = messagesRepo.Add(messages);
            return Content(res?"OK":"Fail");
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
