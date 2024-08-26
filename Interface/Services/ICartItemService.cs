using Luce.DTOs;
namespace Luce.Interface.Services
{
    public interface ICartItemService
    {
        Task<CartItemDto> CreateCartItemAsync(CartItemDto cartItem, int productId, int customerId);
        Task<List<CartItemDto>> GetCartItemsByCustomerIdAsync(int customerId);
        Task<CartItemDto> GetCartItemAsync(int customerId, int productId);
        Task<CartItemDto> GetCartItemByCartItemIdAsync(int cartItemId);
        Task<bool> DeleteCartItemByIdAsync(int cartItemId);
        Task<bool> UpdateCartItemStatusAsync( int id);
        


    }
}