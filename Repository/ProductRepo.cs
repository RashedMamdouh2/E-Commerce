using E_Commerce.Models;
using E_Commerce.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Repository
{
    public class ProductRepo:IRepository<Product,ProductViewModel>
    {
        private readonly CommerceDbContext context;

        public ProductRepo(CommerceDbContext context)
        {
            this.context = context;
        }
        public bool Add(Product obj)
        {
            if (obj == null) { return false; }
            context.Add(obj);
            context.SaveChanges();
            return true;
        }
        public void AddFiltersToProduct(List<FilterWordProduct> filters)
        {
            context.filterWordProduct.AddRange(filters);
      

            context.SaveChanges();
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
            if(getProducts){
                return context.products.Where(p => p.Id == id).Include(p => p.Images).Include(p => p.Feedbacks).ThenInclude(feedback => feedback.Customer).Include(p => p.Filters).Include(p => p.Category).AsSplitQuery().FirstOrDefault();
            }
            return context.products.Find(id);
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

        public List<ProductViewModel> GetAllًWithNameAndIdOnly()
        {
            throw new NotImplementedException();
        }

        public List<Product> GetNewstItems(int numberOfItems)
        {
            return context.products.Include(p=>p.Images).OrderByDescending(p=>p.InsertionDate).Take(numberOfItems).ToList();
        }

        public bool Update(Product obj)
        {
            if(obj is not null)
            {
                context.products.Update(obj);
                context.SaveChanges();
                return true;
            }
            return false;
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
