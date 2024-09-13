using Microsoft.EntityFrameworkCore;
using Luce.Interface.Repositories;

namespace Luce.Implementation.Repositories
{
    public class OrderRepository : GenericRepository<Order> , IOrderRepository 
    {
        public OrderRepository(LuceDbContext Context) : base(Context)
        {
            _Context = Context;
        }

         public async Task<Order> GetOrder(string id)
        {
            var order = await _Context.Orders.Include(x => x.Customer).Include(x => x.Payment).Include(x => x.CartItem).ThenInclude(x => x.Product).ThenInclude(x => x.Seller).Include(x => x.DeliveryAddress).Include(x => x.CartItem).SingleOrDefaultAsync(c => c.RefNo == id);
            return order;
        }

        public async Task<IList<Order>> GetOrders(int customerId)
        {
            var orders = await _Context.Orders.Where(c => c.CustomerId == customerId).Include(x => x.Customer).Include(x => x.Payment).Include(x => x.CartItem).ThenInclude(x => x.Product).ThenInclude(x => x.Seller).Include(x => x.DeliveryAddress).Include(x => x.CartItem).ToListAsync();
            return orders;
        }
    }
} 