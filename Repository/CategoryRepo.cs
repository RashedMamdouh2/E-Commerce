using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Repository
{
    public class CategoryRepo : IRepository<Category>
    {
        private readonly CommerceDbContext context;

        public CategoryRepo(CommerceDbContext context)
        {
            this.context = context;
        }
        public List<Category> GetAll()
        {

            return context.categories.Include(c => c.Products).ToList();
        }
        public Category GetById(int id, bool getProducts = true)
        {
            if (getProducts)
            {
                return context.categories.Where(x => x.Id == id)


                    .Include(c => c.Products).
                    ThenInclude(p => p.Images).Include(c => c.Products).ThenInclude(p => p.Filters).Include(c => c.Filters).AsSplitQuery().FirstOrDefault();
            }
            else
            {
                return context.categories.Where(x => x.Id == id).Include(c => c.Filters).AsSplitQuery().FirstOrDefault();

            }
        }

    }

}
