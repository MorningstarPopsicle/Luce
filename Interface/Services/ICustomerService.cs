using Luce.DTOs;
namespace Luce.Interface.Services
{
    public interface ICustomerService
    {
        Task<CustomerDto> Register(CustomerDto customer);
        Task<CustomerDto> GetById(int id);
        Task<bool> UpdateCustomer(CustomerDto updatedCustomer, int id);

        

    
    }
}