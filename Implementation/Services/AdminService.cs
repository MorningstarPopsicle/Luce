using Luce.DTOs;
using Luce.Interface.Repositories;
using Luce.Interface.Services;

namespace Luce.Implementation.Service
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IUserRepository _userRepository;

        public AdminService(IAdminRepository adminRepository, IUserRepository userRepository)
        {
            _adminRepository = adminRepository;
            _userRepository = userRepository;

        }

        public async Task<AdminDto> Register(AdminDto model)
        {
            var admin = await _adminRepository.GetAsync(Admin => Admin.User.Email == model.Admin.Email);
            if (admin != null)
            {
                return null;
            }

            var user = new User
            {
                Email = model.Admin.Email,
                Password = model.Admin.Password,
                FirstName = model.Admin.FirstName,
                LastName = model.Admin.LastName,
                Role = Role.Admin,
                PhoneNumber = model.Admin.PhoneNumber,
                

            };
            var adduser = await _userRepository.CreateAsync(user);
            var newAdmin = new Admin()
            {
                UserId = adduser.Id,
                AccountNumber = model.AccountNumber,
            };

            var addAdmin = await _adminRepository.CreateAsync(newAdmin);
            if (addAdmin == null)
            {
                return null;
            }
            else
            {

                return new AdminDto()
                {
                    Id = addAdmin.Id,
                    AccountNumber = addAdmin.AccountNumber,
                    Admin = new UserDto()
                    {
                        Id = addAdmin.User.Id,
                        Email = addAdmin.User.Email,
                        Password = addAdmin.User.Password,
                        Role = addAdmin.User.Role,
                        FirstName = addAdmin.User.FirstName,
                        LastName = addAdmin.User.LastName
                    }
                };
            }
        }

        public async Task<AdminDto> GetById(int id)
        {
            var admin = await _adminRepository.GetAdmin(id);
            if (admin == null)
            {
                return null;
            }

            return new AdminDto()
            {
                Id = admin.Id,
                AccountNumber = admin.AccountNumber,
                Admin = new UserDto()
                {
                    FirstName = admin.User.FirstName,
                    LastName = admin.User.LastName,
                    Email = admin.User.Email,
                    PhoneNumber = admin.User.PhoneNumber,
                    Role = admin.User.Role,
                },
            };
        }
        public async Task<bool> UpdateAdminAsync(AdminDto updatedAdmin, int id)
        {
            var admin = await _adminRepository.GetAdmin(id);
            if (admin == null)
            {
                return false;
            }
            admin.User.FirstName = updatedAdmin.Admin.FirstName ?? admin.User.FirstName;
            admin.User.LastName = updatedAdmin.Admin.LastName ?? admin.User.LastName;
            admin.User.PhoneNumber = updatedAdmin.Admin.PhoneNumber ?? admin.User.PhoneNumber;
            admin.AccountNumber = updatedAdmin.AccountNumber ?? admin.AccountNumber;


            await _adminRepository.UpdateAsync(admin);
            return true;
        }
    }
}