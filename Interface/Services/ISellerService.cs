using Luce.DTOs;
namespace Luce.Interface.Services
{
    public interface ISellerService
    {
        Task<SellerDto> Register(SellerDto seller);
        Task<SellerDto> GetById(int id);
        Task<bool> UpdateSellerAsync(SellerDto updatedSeller, int id);
        Task<bool> DeleteSellerAsync(int sellerId);
        Task<bool> VerifySellerAsync(int sellerId);
        Task<List<SellerDto>> GetSellers(); 



    }
}