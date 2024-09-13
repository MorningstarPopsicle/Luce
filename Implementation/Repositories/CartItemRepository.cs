using Microsoft.EntityFrameworkCore;
using Luce.Interface.Repositories;

namespace Luce.Implementation.Repositories
{
    public class CartItemRepository : GenericRepository<CartItem> , ICartItemRepository 
    {
        public CartItemRepository(LuceDbContext Context) : base(Context)
        {
            _Context = Context;
        }

        public async Task<CartItem> GetCartItemAsync(int customerId, int productId)
        {
            var CartItem = await _Context.CartItems.Where(c => c.CustomerId == customerId && c.ProductId == productId).Include(x => x.Product).Include(x => x.Customer).SingleOrDefaultAsync();
            return CartItem;
        }

        public async Task<CartItem> GetAsync(int cartItemId)
        {
            var CartItem = await _Context.CartItems.Where(c => c.Id == cartItemId).Include(x => x.Product).Include(x => x.Customer).ThenInclude(x => x.User).SingleOrDefaultAsync();
            return CartItem;
        }

        public async Task<IList<CartItem>> GetCartItemsAsync(int customerId)
        {
            var cartItems = await _Context.CartItems.Where(a => a.CustomerId == customerId).Include(x => x.Product).Include(x => x.Customer).ThenInclude(x => x.User).ToListAsync();
            return cartItems;
        }
    
    }
} 