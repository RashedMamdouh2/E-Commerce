using E_Commerce.Models;
using E_Commerce.ViewModels;

namespace E_Commerce.Repository
{
    public class MessageRepo : IRepository<CustomerMessages, CategoryViewModel>
    {
        private readonly CommerceDbContext context;

        public MessageRepo(CommerceDbContext context)
        {
            this.context = context;
        }

        public bool Add(CustomerMessages obj)
        {
            if (obj == null) { return false; }
            context.Add(obj);
            context.SaveChanges();
            return true;
        }

        public List<CustomerMessages> GetAll()
        {
            return context.messages.ToList();
        }

        public List<CategoryViewModel> GetAllًWithNameAndIdOnly()
        {
            throw new NotImplementedException();
        }

        public CustomerMessages GetById(int id, bool getProducts)
        {
            throw new NotImplementedException();
        }

        public List<CustomerMessages> GetNewstItems(int numberOfItems)
        {
            throw new NotImplementedException();
        }

        public bool Update(CustomerMessages obj)
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
