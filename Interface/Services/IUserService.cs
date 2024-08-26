using Luce.DTOs;

namespace Luce.Interface.Services
{
    public interface IUserService
    {
        Task<UserDto> Login(string email, string passWord);    
    }
}