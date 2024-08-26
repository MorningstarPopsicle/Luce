using Luce.DTOs;
using Luce.Interface.Repositories;
using Luce.Interface.Services;

namespace Luce.Implementation.Service
{
    public class SellerService : ISellerService
    {
        private readonly ISellerRepository _sellerRepository;
        private readonly IUserRepository _userRepository;

        public SellerService(ISellerRepository sellerRepository, IUserRepository userRepository)
        {
            _sellerRepository = sellerRepository;
            _userRepository = userRepository;

        }

        public async Task<SellerDto> Register(SellerDto model)
        {
            var seller = await _sellerRepository.GetAsync(seller => seller.User.Email == model.UserDto.Email);
            if (seller != null)
            {
                return null;
            }
            var user = new User
            {
                Email = model.UserDto.Email,
                Password = model.UserDto.Password,
                FirstName = model.UserDto.FirstName,
                LastName = model.UserDto.LastName,
                Role = Role.Seller,
                PhoneNumber = model.UserDto.PhoneNumber,

            };
            var adduser = await _userRepository.CreateAsync(user);
            var newSeller = new Seller()
            {
                Logo = model.Logo,
                AccountNumber = model.AccountNumber,
                StoreName = model.StoreName,
                IsVerified = false,
                Address = new Address
                {
                    HouseNumber = model.Address.HouseNumber,
                    StreetName = model.Address.StreetName,
                    LGA = model.Address.LGA,
                    Town = model.Address.Town,
                    State = model.Address.State,
                    Country = model.Address.Country
                },
                UserId = adduser.Id,
            };

            var addSeller = await _sellerRepository.CreateAsync(newSeller);
            if (addSeller == null)
            {
                return null;
            }
            else
            {

                return new SellerDto()
                {
                    UserDto = new UserDto
                    {
                        Id = addSeller.User.Id,
                        FirstName = addSeller.User.FirstName,
                        LastName = addSeller.User.LastName,
                        Email = addSeller.User.Email,
                        PhoneNumber = addSeller.User.PhoneNumber,
                        Role = addSeller.User.Role,
                    },
                    Logo = addSeller.Logo,
                    StoreName = addSeller.StoreName,
                    AccountNumber = addSeller.AccountNumber,
                    Id = addSeller.Id,
                    IsVerified = addSeller.IsVerified,
                    Address = new AddressDto
                    {
                        HouseNumber = addSeller.Address.HouseNumber,
                        StreetName = addSeller.Address.StreetName,
                        LGA = addSeller.Address.LGA,
                        Town = addSeller.Address.Town,
                        State = addSeller.Address.State,
                        Country = addSeller.Address.Country
                    }
                };
            }
        }

        public async Task<SellerDto> GetById(int id)
        {
            var seller = await _sellerRepository.GetSeller(id);
            if (seller == null)
            {
                return null;
            }

            return new SellerDto()
            {
                UserDto = new UserDto
                {
                    Id = seller.User.Id,
                    FirstName = seller.User.FirstName,
                    LastName = seller.User.LastName,
                    Email = seller.User.Email,
                    PhoneNumber = seller.User.PhoneNumber,
                    Role = seller.User.Role,
                },
                Logo = seller.Logo,
                StoreName = seller.StoreName,
                AccountNumber = seller.AccountNumber,
                Id = seller.Id,
                IsVerified = seller.IsVerified,
                Address = new AddressDto
                {
                    HouseNumber = seller.Address.HouseNumber,
                    StreetName = seller.Address.StreetName,
                    LGA = seller.Address.LGA,
                    Town = seller.Address.Town,
                    State = seller.Address.State,
                    Country = seller.Address.Country
                }
            };
        }
        public async Task<bool> UpdateSellerAsync(SellerDto updatedSeller, int id)
        {
            var seller = await _sellerRepository.GetSeller(id);
            if (seller == null)
            {
                return false;
            }
            seller.User.FirstName = updatedSeller.UserDto.FirstName ?? seller.User.FirstName;
            seller.User.LastName = updatedSeller.UserDto.LastName ?? seller.User.LastName;
            seller.User.PhoneNumber = updatedSeller.UserDto.PhoneNumber ?? seller.User.PhoneNumber;
            seller.AccountNumber = updatedSeller.AccountNumber ?? seller.AccountNumber;
            seller.Logo = updatedSeller.Logo ?? seller.Logo;
            seller.StoreName = updatedSeller.StoreName ?? seller.StoreName;
            seller.Address.HouseNumber = updatedSeller.Address.HouseNumber ?? seller.Address.HouseNumber;
            seller.Address.StreetName = updatedSeller.Address.StreetName ?? seller.Address.StreetName;
            seller.Address.LGA = updatedSeller.Address.LGA ?? seller.Address.LGA;
            seller.Address.Town = updatedSeller.Address.Town ?? seller.Address.Town;
            seller.Address.State = updatedSeller.Address.State ?? seller.Address.State;
            seller.Address.Country = updatedSeller.Address.Country ?? seller.Address.Country;

            await _sellerRepository.UpdateAsync(seller);
            return true;
        }

        public async Task<bool> DeleteSellerAsync(int sellerId)
        {
            var seller = await _sellerRepository.GetAsync(x => x.Id == sellerId);
            if (seller == null)
            {
                return false;
            }
            await _sellerRepository.DeleteAsync(seller);
            return true;
        }

        public async Task<bool> VerifySellerAsync(int sellerId)
        {
            var seller = await _sellerRepository.GetAsync(x => x.Id == sellerId);
            if (seller == null)
            {
                return false;
            }
            seller.IsVerified = true;
            await _sellerRepository.UpdateAsync(seller);
            return true;
        }

        public async Task<List<SellerDto>> GetSellers()
        {
            var sellers = await _sellerRepository.GetSellers();
            if (sellers == null)
            {
                return null;
            }
            return sellers.Select(x => new SellerDto
            {
                Id = x.Id,
                Logo = x.Logo,
                AccountNumber = x.AccountNumber,
                StoreName = x.StoreName,
                IsVerified = x.IsVerified,
                Address = new AddressDto
                {
                    HouseNumber = x.Address.HouseNumber,
                    StreetName = x.Address.StreetName,
                    LGA = x.Address.LGA,
                    Town = x.Address.Town,
                    State = x.Address.State,
                    Country = x.Address.Country
                },

                UserDto = new UserDto
                {
                    Id = x.User.Id,
                    Email = x.User.Email,
                    FirstName = x.User.FirstName,
                    LastName = x.User.LastName,
                    PhoneNumber = x.User.PhoneNumber,
                    Role = x.User.Role,
                },

            }).ToList();

    }
}
}