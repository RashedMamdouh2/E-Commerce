using E_Commerce.Models;
using E_Commerce.ViewModels;

namespace E_Commerce.Repository
{
    public interface ICartRepo:IRepository<Cart,CartViewModel>
    {
        bool AddProduct(int cartId, int productId);
    }
}
