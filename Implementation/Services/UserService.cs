using Luce.DTOs;
using Luce.Interface.Repositories;
using Luce.Interface.Services;

namespace Luce.Implementation.Service
{
    public class UserService : IUserService

    {
        private readonly IUserRepository _repository;
        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserDto> Login(string email, string passWord)
        {

            var user = await _repository.GetAsync(x => x.Email == email && x.Password == passWord);
            if (user != null)
            {
                return new UserDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    Password = user.Password,
                    Role = user.Role,
                            
                };
            }
            return null;
        }

        
    }
}