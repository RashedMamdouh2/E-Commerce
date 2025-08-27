using E_Commerce.Models;
using E_Commerce.ViewModels;

namespace E_Commerce.Repository
{
    public interface ICustomerRepo : IRepository<Customer, CategoryViewModel>
    {

        public Customer GetCustomerByUserId(string userId);
        
    }
}
