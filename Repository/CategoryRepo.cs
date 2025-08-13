using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Repository
{
    public class CategoryRepo:IRepository<Category>
    {
        private readonly CommerceDbContext context;

        public CategoryRepo(CommerceDbContext context)
        {
            this.context = context;
        }
        public List<Category> GetAll() { 
        
            return context.categories.Include(c=>c.Products).ToList();
        }
        public Category GetById(int id)
        {
            return context.categories.Include(c=>c.Products).ThenInclude(p=>p.Images).AsSplitQuery().FirstOrDefault(x=>x.Id==id);
        }

    }
}
