using Azure.Core;
using Azure.Core.Serialization;
using E_Commerce.Models;
using E_Commerce.Repository;
using E_Commerce.Services;
using E_Commerce.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Nodes;
using static System.Net.Mime.MediaTypeNames;

namespace E_Commerce.Controllers
{
    public class CartController : Controller
    {
        private readonly UserManager<Customer> userManager;
        private readonly SignInManager<Customer> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        
        private readonly IGeneralRepo<Product, int> productRepo;
        private readonly IGeneralRepo<Cart, int> cartRepo;
        private readonly IGeneralRepo<Customer, string> customerRepo;
        private readonly IGeneralRepo<Coupon, int> couponRepo;
        private readonly IGeneralRepo<Order, int> orderRepo;
        private readonly IInventoryService inventory;

        public CartController(
            UserManager<Customer> userManager, 
            SignInManager<Customer> signInManager, 
            RoleManager<IdentityRole> roleManager, 
            IGeneralRepo<Product, int> productRepo, 
            IGeneralRepo<Cart, int> cartRepo, 
            IGeneralRepo<Customer, string> customerRepo,
            IGeneralRepo<Coupon,int> couponRepo,
            IGeneralRepo<Order,int>orderRepo,
            IInventoryService inventory
            )
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
           
            this.productRepo = productRepo;
            this.cartRepo = cartRepo;
            this.customerRepo = customerRepo;
            this.couponRepo = couponRepo;
            this.orderRepo = orderRepo;
            this.inventory = inventory;
        }

        public async Task<IActionResult> ShowCartAsync()
        {
              var productsList = new List<int>();
            if (User.Identity!.IsAuthenticated)
            {
                string userId=User.Claims.FirstOrDefault(c=>c.Type==ClaimTypes.NameIdentifier)!.Value;
                var customer = await userManager.Users.Include(u => u.Cart).ThenInclude(c=>c!.Products).FirstOrDefaultAsync(u => u.Id == userId);
                
                productsList = customer?.Cart!.Products.Select(p=>p.Id).ToList();

            }
            else
            {
                if (Request.Cookies["Products"] is not null)
                {
                    productsList = JsonConvert.DeserializeObject<List<int>>(Request.Cookies["Products"]);




                }
               
                
            }
            var products =  productRepo.FindAll(p=>productsList.Contains(p.Id),new string[] { "Images"})
                     .Select(
                     p => new ProductInCartViewModel
                     {
                         Id = p.Id,
                         Name = p.Name,
                         Description = p.Description,
                         AvaliableInStock = p.Amount,
                         Image = p.Images.FirstOrDefault(defaultValue: new Models.Image { Url = "/ProductImages/Default.jpg" }).Url,
                         OrderedQuantity = productsList.Count(x => x == p.Id),
                         Price = p.Price,
                     }).ToList();
            var cart = new CartViewModel { Products = products, TotalPrice = products.Sum(p => Math.Round(p.Price, 2)) };
            return View(cart);
        }
        public async Task<IActionResult> AddToCartAsync(int id)//recives productId
        {
            //DB
            if (User.Identity.IsAuthenticated)
            {
                
                string ?userId = userManager.GetUserId(User);
                var customer = await userManager.Users.Include(u=>u.Cart).ThenInclude(cart=>cart.Products).FirstOrDefaultAsync(u=>u.Id==userId);
                var product = await productRepo.GetByIdAsync(id);
                if (customer?.Cart is null) {
                    await cartRepo.AddAsync(new Cart { Customer=customer,CustomerId=userId,Products=new List<Product> { product} });
                    await cartRepo.SaveAsync();
                }
                else
                {
                    var cart = customer?.Cart;
                cart?.Products?.Add(product);
                await cartRepo.SaveAsync();

                }
                
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
 
        public async Task<IActionResult> ApplyCouponAsync(string couponCode)
        {
           
            var c =await couponRepo.FindAsync(c => c.Description == couponCode,new string[] { });
            if (c != null) { 
                return Ok("Ok");
            }
            return NotFound("");
        }
       
    }
}
