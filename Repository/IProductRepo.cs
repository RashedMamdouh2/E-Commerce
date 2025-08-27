using E_Commerce.Models;
using E_Commerce.ViewModels;

namespace E_Commerce.Repository
{
    public interface IProductRepo:IRepository<Product, ProductViewModel>
    {
        public List<Product> GetById(List<int> Ids, bool getProducts = true);
    }
}
