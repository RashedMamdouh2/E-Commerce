using E_Commerce.Models;
using E_Commerce.Repository;
using E_Commerce.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ICustomerRepo customerManager;

        public AccountController(UserManager<ApplicationUser>userManager,SignInManager<ApplicationUser>signInManager, RoleManager<IdentityRole> roleManager,ICustomerRepo customerManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.customerManager = customerManager;
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
                var newUser = new ApplicationUser {
                
                    Email =model.Email,
                    UserName = model.Name,
                    PhoneNumber =model.Phone,
                    


                };
                IdentityResult result = await userManager.CreateAsync(newUser, model.Password);
                if (result.Succeeded)
                {
                   var roleResult = await userManager.AddToRoleAsync(newUser, "Admin");
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
