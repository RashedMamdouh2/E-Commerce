using E_Commerce.Models;
using E_Commerce.ViewModels;

namespace E_Commerce.Repository
{
    public class CartRepository : ICartRepo
    {
        private readonly CommerceDbContext context;

        public CartRepository(CommerceDbContext context)
        {
            this.context = context;
        }
        public bool Add(Cart obj)
        {
            throw new NotImplementedException();
        }

        public bool DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Cart> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<CartViewModel> GetAllًWithNameAndIdOnly()
        {
            throw new NotImplementedException();
        }

        public Cart GetById(int id, bool getProducts)
        {
            throw new NotImplementedException();
        }

        public List<Cart> GetNewstItems(int numberOfItems)
        {
            throw new NotImplementedException();
        }

        public bool Update(Cart obj)
        {
            throw new NotImplementedException();
        }

        public bool AddProduct(int cartId, int productId)
        {
            var product = new ProductCart
            {
                CartId = cartId,
                ProductId = productId,
                
            };
            context.cartProducts.Add(product);
            context.SaveChanges();


            return true;
        }
    }
}
