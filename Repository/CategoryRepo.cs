using E_Commerce.Models;
using E_Commerce.ViewModels;
using Microsoft.EntityFrameworkCore;
using NuGet.ProjectModel;

namespace E_Commerce.Repository
{
    public class CategoryRepo : IRepository<Category,CategoryViewModel>
    {
        private readonly CommerceDbContext context;
        
        public CategoryRepo(CommerceDbContext context)
        {
            this.context = context;
        }

        public bool Add(Category obj)
        {
            if (obj == null) return false;
            context.Add(obj);
            context.SaveChanges();
            return true;
        }

        public List<Category> GetAll()
        {

            return context.categories.Include(c => c.Products).Include(c=>c.Images).ToList();
        }

        public List<CategoryViewModel> GetAllًWithNameAndIdOnly()
        {
            return context.categories.Select(c => new CategoryViewModel { Id=c.Id,Name=c.Name}).ToList();

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

        public List<Category> GetNewstItems(int numberOfItems)
        {
            throw new NotImplementedException();
        }

        public bool Update(Category obj)
        {
            throw new NotImplementedException();
        }
        public bool DeleteById(int id)
        {
            if (id > 0)
            {

                var obj = GetById(id, false);
                context.Remove(obj);
                context.SaveChanges();

                return true;
            }
            return false;

        }
    }

}
