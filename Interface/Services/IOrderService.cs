using Luce.DTOs;
namespace Luce.Interface.Services
{
    public interface IOrderService
    {
        Task<OrderDto> CreateOrder(OrderDto model, int cartItemId );
        Task<OrderDto> GetByRefNo(string RefNo);
        Task<List<OrderDto>> GetByCustomerId(int id);            
    }
}