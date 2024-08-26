using Luce.DTOs;
namespace Luce.Interface.Services
{
    public interface IDispatchService
    {
        Task<bool> CompleteRegisteration(DispatchDto dispatch);
        Task<DispatchDto> GetById(int id);
        Task<DispatchDto> GetByEmail(string email);
        Task<bool> UpdateDispatch(DispatchDto updatedDispatch, int id);    
        Task<DispatchDto> RegisterDispatch(DispatchDto dispatch, int sellerId);  
        Task<List<DispatchDto>> GetDispatchesBySellerId(int sellerId);    
        Task<bool> DeleteDispatchAsync(int id);
    
  
    }
}