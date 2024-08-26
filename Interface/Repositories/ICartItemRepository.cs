namespace Luce.Interface.Repositories
{
    public interface ICartItemRepository : IGenericRepository<CartItem>
    {
        Task<CartItem> GetCartItemAsync(int customerId, int productId);
        Task<CartItem> GetAsync(int cartItemId);
        Task<IList<CartItem>> GetCartItemsAsync(int customerId);
    }
}