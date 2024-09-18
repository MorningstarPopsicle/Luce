using Luce.DTOs;
namespace Luce.Interface.Services
{
    public interface IOrderService
    {
        Task<OrderDto> CreateOrder(OrderDto model);
        Task<OrderDto> GetByRefNo(string RefNo);
        Task<List<OrderDto>> GetByCustomerId(int id);   
        // Dictionary<int, double> GetTotalPricesBySeller(OrderDto order)   ;      
    }
}