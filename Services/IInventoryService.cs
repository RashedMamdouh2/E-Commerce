using E_Commerce.Models;

namespace E_Commerce.Services
{
    public interface IInventoryService
    {
        public  Task<decimal> GetProductsFromInventoryAsync(List<OrderProduct> orderedproduct);
    }
}
