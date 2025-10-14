using E_Commerce.Models;
using E_Commerce.Repository;
using E_Commerce.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<Customer> userManager;
        private readonly SignInManager<Customer> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IGeneralRepo<Cart, int> cartReop;

        public AccountController(UserManager<Customer>userManager,SignInManager<Customer>signInManager, RoleManager<IdentityRole> roleManager,IGeneralRepo<Cart,int>cartReop)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.cartReop = cartReop;
        }
        public IActionResult Login()
        {
            
            return View();
        }
        public async Task<IActionResult> CheckLogin(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if(user is  not null)
                {
                    bool IscorrectPassword = await userManager.CheckPasswordAsync(user, model.Password);
                    if (IscorrectPassword)
                    {

                        await signInManager.SignInAsync(user,model.RememberMe);
                        return RedirectToAction(controllerName:"Admin",actionName: "ShowAllProducts");
                    }
                }
            }
            ModelState.AddModelError("", "Invalid Email or Password");
            return View("Login",model);
        }
        public IActionResult Signup()
        {
            
            return View();
        }
        public async Task<IActionResult> SaveNewSignup(SignupViewModel model)
        {
            if (ModelState.IsValid)
            {
                var newUser = new Customer {
                
                    Email =model.Email,
                    UserName = model.Name,
                    PhoneNumber =model.Phone,
                    City=model.City,
                    Address=model.Address,
                    
                    

                };
                 IdentityResult result = await userManager.CreateAsync(newUser, model.Password);
                if (result.Succeeded)
                {
                    var cart = new Cart { Customer=newUser ,CustomerId=newUser.Id};
                    await cartReop.AddAsync(cart);
                    await cartReop.SaveAsync();
                   //var roleResult = await userManager.AddToRoleAsync(newUser, "admin");
                    var roleResult =await userManager.AddToRoleAsync(newUser, "customer");
                    if (roleResult.Succeeded)
                    {
                        await signInManager.SignInAsync(newUser,false);
                        return RedirectToAction(controllerName: "Home", actionName: "Index");

                    }
                    foreach (var err in roleResult.Errors)
                    {
                        ModelState.AddModelError("", err.Description);
                    }
                }
                foreach(var err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
                
            }
            return View("Signup",model);
        }
        public async Task<IActionResult> Signout()
        {
            await signInManager.SignOutAsync();
            
            return RedirectToAction(controllerName: "Home", actionName: "Index");
        }
    }
}
