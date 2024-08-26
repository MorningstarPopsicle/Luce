using Luce.DTOs;

namespace Luce.Interface.Services
{
    public interface IAddressService
    {
        Task<AddressDto> CreateAddressAsync(AddressDto address);
        
    }
}