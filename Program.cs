using E_Commerce.Models;
using E_Commerce.Repository;
using E_Commerce.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddIdentity<IdentityUser,IdentityRole>().AddEntityFrameworkStores<CommerceDbContext>();
            builder.Services.AddDbContext<CommerceDbContext>(optionsBuilder =>
            {
                optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("Commerce"));
            });
            builder.Services.AddScoped<IRepository<Category,CategoryViewModel>, CategoryRepo>();
            builder.Services.AddScoped<IRepository<Image,CategoryViewModel>, ImageRepo>();
            builder.Services.AddScoped<IRepository<CustomerMessages,CategoryViewModel>, MessageRepo>();
            builder.Services.AddScoped<IRepository<Product, ProductViewModel>, ProductRepo>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
