using E_Commerce.Models;
using E_Commerce.Repository;
using E_Commerce.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;

namespace E_Commerce.Controllers
{
    [Authorize(Roles ="admin,customer")]
    public class OrderController : Controller
    {
        private readonly IGeneralRepo<Order, int> ordersRepo;
        private readonly UserManager<Customer> userManager;
        private readonly IInventoryService inventory;
        private readonly IGeneralRepo<Product, int> productRepo;
        private readonly IGeneralRepo<Cart, int> cartRepo;
        private readonly IGeneralRepo<Coupon, int> couponRepo;
        
        public OrderController(IGeneralRepo<Order,int>ordersRepo,UserManager<Customer>userManager,IInventoryService inventory,IGeneralRepo<Product,int>productRepo,IGeneralRepo<Cart,int> cartRepo, IGeneralRepo<Coupon, int> couponRepo)
        {
            this.ordersRepo = ordersRepo;
            this.userManager = userManager;
            this.inventory = inventory;
            this.productRepo = productRepo;
            this.cartRepo = cartRepo;
            this.couponRepo = couponRepo;
        }
        public async Task<IActionResult> CreateOrderAsync(string OrderProducts,string AppliedCoupons)
        {
            var CouponsId = AppliedCoupons.Split(',');
            var Coupons = couponRepo.FindAll(c => CouponsId.Contains( c.Description), new string[] { });
            var cutPercentage = Coupons.Select(c => c.Value).Sum();
            List<OrderProduct> products = JsonConvert.DeserializeObject<List<OrderProduct>>(OrderProducts)!;
            var customerId = userManager.GetUserId(User)!;
            decimal TotalPrice = await inventory.GetProductsFromInventoryAsync(products);
            TotalPrice -= TotalPrice * (cutPercentage/100.0m);
            
            var order = new Order { CustomerId = customerId, Date = DateTime.Now, Items = products, InvoiceValue = TotalPrice, Status = OrderStatus.Shipped };
            var IsAddeddSuccessfullyInDB = false;
            if (TotalPrice != 0)
            {
                IsAddeddSuccessfullyInDB = await ordersRepo.AddAsync(order) && await productRepo.SaveAsync();
            }
           
            if (IsAddeddSuccessfullyInDB)
            {
                //remove from the cookies
                Response.Cookies.Delete("Products");
                var cart=await cartRepo.FindAsync(c => c.CustomerId == customerId, new string[] { nameof(Cart.Products) });
                cart.Products.Clear();
                await cartRepo.SaveAsync();
                return View("SuccessfullOrder", new { Controller = "Order", Action = nameof(ShowCustomerOrders) });

            }
            return  View("FailedToCompleteOrder");
        }
        public IActionResult ShowCustomerOrders()
        {
            var Orders = ordersRepo.FindAll(order => order.CustomerId == userManager.GetUserId(User),new string[] { });
            return View(Orders);
        }
        public async Task<IActionResult> DetailsAsync(int id)
        {
            var order = (await ordersRepo.FindAsync(ord=>ord.Id==id,new string[] {nameof(Order.Items) }));
          
            return View(order);
        }

    }
}
