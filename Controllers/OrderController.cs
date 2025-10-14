using E_Commerce.Models;
using E_Commerce.Repository;
using E_Commerce.Services;
using E_Commerce.ViewModels;
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
        private readonly IPaymobService paymob;
        
        public OrderController(IGeneralRepo<Order,int>ordersRepo,UserManager<Customer>userManager,IInventoryService inventory,IGeneralRepo<Product,int>productRepo,IGeneralRepo<Cart,int> cartRepo, IGeneralRepo<Coupon, int> couponRepo, IPaymobService paymob) { 
        

            this.ordersRepo = ordersRepo;
            this.userManager = userManager;
            this.inventory = inventory;
            this.productRepo = productRepo;
            this.cartRepo = cartRepo;
            this.couponRepo = couponRepo;
            this.paymob = paymob;
        }
        public async Task<IActionResult> CreateOrderAsync(string OrderProducts,string AppliedCoupons)
        {
            var CouponsId = AppliedCoupons.Split(',');
            var Coupons = couponRepo.FindAll(c => CouponsId.Contains( c.Description), new string[] { });
            var cutPercentage = Coupons.Select(c => c.Value).Sum();
            List<OrderProduct> products = JsonConvert.DeserializeObject<List<OrderProduct>>(OrderProducts)!;
            var customerId = userManager.GetUserId(User)!;
            decimal TotalPrice = await inventory.GetProductsFromInventoryAsync(products);
            var totalPriceAfterDiscount =TotalPrice- TotalPrice * (cutPercentage/100.0m);

            var order = new Order
            {
                CustomerId = customerId,
                Date = DateTime.Now,
                Items = products,
                TotalPriceBeforeDiscount = TotalPrice,
                TotalPriceAfterDiscount = totalPriceAfterDiscount,
                Status = OrderStatus.Delivered,
                AppliedCoupons = Coupons.ToList()
            };
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
        public async Task<IActionResult> DetailsAsync(int id,string view)
        {
            var order = (await ordersRepo.FindAsync(ord => ord.Id == id, new string[] { nameof(Order.Items), nameof(Order.Customer), nameof(Order.AppliedCoupons) }));
            var appliedCoupons = order.AppliedCoupons.Select(c => new CouponViewModel { Name = c.Description, Value = c.Value.ToString() });
            var itemsIds = order.Items.Select(p => p.ProductId);
            var products = productRepo.FindAll(p => itemsIds.Contains(p.Id), new string[] { nameof(Product.Category),nameof(Product.Images) });
            var items = products.Zip(order.Items).Select((product) => new ProductViewModel {Id=product.First.Id,Amount=product.Second.Quantity,Price=product.Second.Price,CategoryName=product.First.Category.Name,Name=product.First.Name,MainImagePath=product.First.Images.First().Url });
            var orderVM = new OrderViewModel { Id=order.Id,OrderDate=order.Date,Status=order.Status.ToString(),AppliedCoupons=appliedCoupons,Items=items,CustomerName=order.Customer.UserName!,CustomerEmail= order.Customer.Email!, CustomerPhoneNumber= order.Customer.PhoneNumber??"", InvoiceAfterCoupons = order.TotalPriceAfterDiscount,InvoiceBeforeCoupons=order.TotalPriceBeforeDiscount};
            if(view=="Admin")
                return View("OrderDetailsAdminView", orderVM);
            return View("OrderDetailsCustomerView", orderVM);
            
        }

    }
}
