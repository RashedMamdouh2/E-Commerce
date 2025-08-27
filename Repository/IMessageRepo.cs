using E_Commerce.Models;
using E_Commerce.ViewModels;

namespace E_Commerce.Repository
{
    public interface IMessageRepo: IRepository<CustomerMessages, CategoryViewModel> 
    {
    }
}
