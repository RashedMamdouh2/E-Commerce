using E_Commerce.Models;
using E_Commerce.ViewModels;

namespace E_Commerce.Repository
{
    public class ImageRepo : IRepository<Image, CategoryViewModel>
    {
        private readonly CommerceDbContext context;

        public ImageRepo(CommerceDbContext context)
        {
            this.context = context;
        }
        public bool Add(Image obj)
        {
            context.images.Add(obj);
            context.SaveChanges();
            return true;
        }

        public List<Image> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<CategoryViewModel> GetAllًWithNameAndIdOnly()
        {
            throw new NotImplementedException();
        }

        public Image GetById(int id, bool getProducts)
        {
            throw new NotImplementedException();
        }

        public List<Image> GetNewstItems(int numberOfItems)
        {
            throw new NotImplementedException();
        }

        public bool Update(Image obj)
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
