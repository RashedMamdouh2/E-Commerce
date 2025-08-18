using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Models
{
    public class CommerceDbContext: DbContext
    {
        public DbSet<Product> products{ get; set; }
        public DbSet<Cart> carts{ get; set; }
        public DbSet<Customer> customers{ get; set; }
        public DbSet<Category> categories{ get; set; }
        public DbSet<Image> images{ get; set; }
        public DbSet<Feedback>feedbacks { get; set; }
        public DbSet<Seller> sellers{ get; set; }
        public DbSet<Coupon> coupons{ get; set; }
        public DbSet<FilterWord> filters{ get; set; }
        public DbSet<CustomerMessages> messages{ get; set; }
        public DbSet<FilterWordProduct> filterWordProduct{ get; set; }


        public CommerceDbContext(DbContextOptions options):base(options)
        {
            
        }
    }
}
