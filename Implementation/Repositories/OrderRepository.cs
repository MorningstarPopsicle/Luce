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
            var order = await _Context.Orders.Include(x => x.Customer).Include(x => x.Payment).Include(x => x.DeliveryAddress).SingleOrDefaultAsync(c => c.RefNo == id);
            return order;
        }

        public async Task<IList<Order>> GetOrders(int customerId)
        {
            var orders = await _Context.Orders.Where(c => c.CustomerId == customerId).Include(x => x.Customer).Include(x => x.Payment).Include(x => x.DeliveryAddress).ToListAsync();
            return orders;
        }
    }
} 