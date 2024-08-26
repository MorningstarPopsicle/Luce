namespace Luce.Interface.Repositories
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<Order> GetOrder(string refNo);
        Task<IList<Order>> GetOrders(int CustomerId);
    }
}