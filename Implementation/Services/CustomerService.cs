

using Luce.DTOs;
using Luce.Interface.Repositories;
using Luce.Interface.Services;

namespace Luce.Implementation.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUserRepository _userRepository;

        public CustomerService(ICustomerRepository customerRepository, IUserRepository userRepository)
        {
            _customerRepository = customerRepository;
            _userRepository = userRepository;

        }

        public async Task<CustomerDto> Register(CustomerDto model)
        {
            var customer = await _customerRepository.GetAsync(customer => customer.User.Email == model.UserDto.Email);
            if (customer != null)
            {
                return null;
            }

            var user = new User
            {
                Email = model.UserDto.Email,
                Password = model.UserDto.Password,
                FirstName = model.UserDto.FirstName,
                LastName = model.UserDto.LastName,
                Role = Role.Customer,
                PhoneNumber = model.UserDto.PhoneNumber,

            };
            var adduser = await _userRepository.CreateAsync(user);
            var newCustomer = new Customer()
            {  
                UserId = adduser.Id    
            };

            var addCustomer = await _customerRepository.CreateAsync(newCustomer);
            if (addCustomer == null)
            {
                return null;
            }
            else
            {

                return new CustomerDto()
                {
                    Id = addCustomer.Id,
                    UserDto = new UserDto()
                    {
                        Id = addCustomer.User.Id,
                        Email = addCustomer.User.Email,
                        Password = addCustomer.User.Password,
                        Role = addCustomer.User.Role,
                        FirstName = addCustomer.User.FirstName,
                        LastName = addCustomer.User.LastName
                    }
                };
            }
        }

        public async Task<CustomerDto> GetById(int id)
        {
            var customer = await _customerRepository.GetCustomer(id);
            if (customer == null)
            {
                return null;
            }

            return new CustomerDto
            {
                UserDto = new UserDto
                {
                    FirstName = customer.User.FirstName,
                    LastName = customer.User.LastName,
                    Email = customer.User.Email,
                    PhoneNumber = customer.User.PhoneNumber,
                    Role = customer.User.Role,
                },
                Id = customer.Id
            };
        }
        public async Task<bool> UpdateCustomer(CustomerDto updatedCustomer, int id)
        {
            var customer = await _customerRepository.GetCustomer(id);
            if (customer == null)
            {
                return false;
            }
            customer.User.FirstName = updatedCustomer.UserDto.FirstName ?? customer.User.FirstName;
            customer.User.LastName = updatedCustomer.UserDto.LastName ?? customer.User.LastName;
            customer.User.PhoneNumber = updatedCustomer.UserDto.PhoneNumber ?? customer.User.PhoneNumber;


            await _customerRepository.UpdateAsync(customer);
            return true;
        }
    }
}