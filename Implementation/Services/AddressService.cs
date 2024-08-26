
using Luce.DTOs;
using Luce.Interface.Repositories;
using Luce.Interface.Services;

namespace Luce.Implementation.Service
{
    public class AddressService : IAddressService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAddressRepository _addressRepository;
        public AddressService(IUserRepository userRepository, IAddressRepository addressRepository)
        {
            _userRepository = userRepository;
            _addressRepository = addressRepository;
           
        }

         public async Task<AddressDto> CreateAddressAsync(AddressDto model)
        {
            var address = new Address
            {
                HouseNumber = model.HouseNumber, 
                StreetName = model.StreetName,
                LGA = model.LGA,
                Town = model.Town,
                State = model.State,
                Country  = model.Country, 
            };

            var result = await _addressRepository.CreateAsync(address);
            return new AddressDto()
            {
                Id = result.Id,
                HouseNumber = result.HouseNumber, 
                StreetName = result.StreetName,
                LGA = result.LGA,
                Town = result.Town,
                State = result.State,
                Country  = result.Country, 
            };
            
        }    
    }
}