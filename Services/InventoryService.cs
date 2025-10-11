using E_Commerce.Models;
using E_Commerce.Repository;

namespace E_Commerce.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IGeneralRepo<Product, int> productRepo;

        public InventoryService(IGeneralRepo<Product, int> productRepo)
        {
            this.productRepo = productRepo;
        }
        public async Task<decimal> GetProductsFromInventoryAsync(List<OrderProduct> products)
        {

            decimal TotalInvoice = 0m;
            foreach (var orderedProduct in products)
            {
                var productDB = await productRepo.GetByIdAsync(orderedProduct.ProductId);
                if (productDB.Amount >= orderedProduct.Quantity)
                {
                    productDB.Amount -= orderedProduct.Quantity;
                    TotalInvoice += orderedProduct.Quantity * orderedProduct.Price;

                }

            }
            return TotalInvoice;
        }

    }
}
