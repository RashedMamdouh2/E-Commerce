using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Repository
{
    public class ProductRepo:IRepository<Product>
    {
        private readonly CommerceDbContext context;

        public ProductRepo(CommerceDbContext context)
        {
            this.context = context;
        }
        public List<Product> GetAll()
        {

            return context.products.Include(p => p.Images).Include(p => p.Filters).AsSplitQuery().ToList();
        }
        public List<Product> SearchProduct(string input)
        {

            return context.products.Where(p=>p.Description.Contains(input)).Include(p => p.Images).Include(p => p.Filters).AsSplitQuery().ToList();
        }
        public Product GetById(int id, bool getProducts = true)
        {
            return context.products.Where(p=>p.Id==id).Include(p => p.Images).Include(p=>p.Feedbacks).ThenInclude(feedback=>feedback.Customer).Include(p => p.Filters).Include(p=>p.Category).AsSplitQuery().FirstOrDefault();
        }
        public void AddFeedback(Feedback feedback)
        {
            Product product= context.products.Where(p=>p.Id==feedback.ProductId).Include(p=>p.Feedbacks).FirstOrDefault();
            if(product is not null)
            {
            product.Feedbacks.Add(feedback);
               context.SaveChanges();   
            }
        }
    }
}
