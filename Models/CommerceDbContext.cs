using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Models
{
    public class CommerceDbContext: IdentityDbContext<Customer>
    {
        public DbSet<Product> products{ get; set; }
        public DbSet<Cart> carts{ get; set; }
        public DbSet<Customer> customers{ get; set; }
        public DbSet<Category> categories{ get; set; }
        public DbSet<Image> images{ get; set; }
        public DbSet<Feedback>feedbacks { get; set; }
        public DbSet<Coupon> coupons{ get; set; }
        public DbSet<Filter> filters{ get; set; }
        public DbSet<Message> messages{ get; set; }
        public DbSet<Filter> Filters{ get; set; }
        public DbSet<Customer> users{ get; set; }
        public DbSet<IdentityRole> roles{ get; set; }


        public CommerceDbContext(DbContextOptions options):base(options)
        {
            
        }
       
    }
}
