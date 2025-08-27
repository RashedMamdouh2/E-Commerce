using Azure.Core.Serialization;
using E_Commerce.Models;
using E_Commerce.Repository;
using E_Commerce.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Security.Claims;

namespace E_Commerce.Controllers
{
    public class CartController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ICustomerRepo customerManager;
        private readonly IProductRepo productRepo;
        private readonly ICartRepo cartRepo;

        public CartController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, ICustomerRepo customerRepo, IProductRepo productRepo,ICartRepo cartRepo)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.customerManager = customerRepo;
            this.productRepo = productRepo;
            this.cartRepo = cartRepo;
        }

        public IActionResult ShowCart()
        {
              var productsList = new List<int>();
            if (User.Identity.IsAuthenticated)
            {
                string userId=User.Claims.FirstOrDefault(c=>c.Type==ClaimTypes.NameIdentifier).Value;
                productsList = customerManager.GetCustomerByUserId(userId).Cart.Products.Select(p=>p.Id).ToList();

            }
            else
            {
                if (Request.Cookies["Products"] is not null)
                {
                    productsList = JsonConvert.DeserializeObject<List<int>>(Request.Cookies["Products"]);




                }
               
                
            }
            var products = productRepo.GetById(productsList, false)
                     .Select(
                     p => new ProductInCartViewModel
                     {
                         Id = p.Id,
                         Name = p.Name,
                         Description = p.Description,
                         AvaliableInStock = p.Amount,
                         Image = p.Images.FirstOrDefault(defaultValue: new Image { Url = "/ProductImages/Default.jpg" }).Url,
                         OrderedQuantity = productsList.Count(x => x == p.Id),
                         Price = p.Price,
                     }).ToList();
            var cart = new CartViewModel { Products = products, TotalPrice = products.Sum(p => Math.Round(p.Price, 2)) };
            return View(cart);
        }
        public IActionResult AddToCart(int id)//recives productId
        {
            //DB
            if (User.Identity.IsAuthenticated)
            {
                string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
                
                int cartId = customerManager.GetCustomerByUserId(userId).CartId;
                cartRepo.AddProduct(cartId,productId: id);
            }
            else
            {
                //cookie
                var option = new CookieOptions();
                option.Expires = DateTimeOffset.UtcNow.AddDays(7);
                option.HttpOnly = true;
                var productsList = new List<int>();
                if (Request.Cookies["Products"] is not null)
                {
                    productsList = JsonConvert.DeserializeObject<List<int>>(Request.Cookies["Products"]);




                }
                productsList.Add(id);


                Response.Cookies.Append("Products", JsonConvert.SerializeObject(productsList), option);
            }
                
            return RedirectToAction("ShowCart");

        }
        public IActionResult checkout(string cartItemsJson)
        {

            var products = System.Text.Json.JsonSerializer.Deserialize<List<Product>>(cartItemsJson);
            return Content(cartItemsJson);
        }
    }
}
