using Luce.DTOs;
namespace Luce.Interface.Services
{
    public interface IAdminService
    {
        Task<AdminDto> Register(AdminDto admin);
        Task<bool> UpdateAdminAsync(AdminDto updatedAdmin, int id);
           

        Task<AdminDto> GetById(int id);
       
        

    
    }
}