using E_Commerce.Models;
using E_Commerce.ViewModels;

namespace E_Commerce.Repository
{
    public class CustomerRepo :ICustomerRepo
    {
        private readonly CommerceDbContext context;

        public CustomerRepo(CommerceDbContext context)
        {
            this.context = context;
        }
        public bool Add(Customer obj)
        {
            if (obj is null) return false;
            context.Add(obj);
            return true;
        }

        public bool DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Customer> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<CategoryViewModel> GetAllًWithNameAndIdOnly()
        {
            throw new NotImplementedException();
        }

        public Customer GetById(int id, bool getProducts)
        {
            throw new NotImplementedException();
        }

        public Customer GetCustomerByUserId(string userId)
        {
            return context.customers.FirstOrDefault(c => c.UserId == userId);
        }

        public List<Customer> GetNewstItems(int numberOfItems)
        {
            throw new NotImplementedException();
        }

        public bool Update(Customer obj)
        {
            throw new NotImplementedException();
        }
    }
}
