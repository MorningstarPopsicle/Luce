using Luce.DTOs;
using Luce.Interface.Repositories;
using Luce.Interface.Services;

namespace Luce.Implementation.Service
{
    public class CartItemService : ICartItemService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICartItemRepository _cartItemRepository;
        public CartItemService(ICustomerRepository customerRepository, ICartItemRepository cartItemRepository)
        {
            _customerRepository = customerRepository;
            _cartItemRepository = cartItemRepository;

        }

        public async Task<CartItemDto> CreateCartItemAsync(CartItemDto model, int productId, int customerId)
        {
            var cartItem = await _cartItemRepository.GetCartItemAsync(customerId, productId);
            if (cartItem != null)
            {
                cartItem.Quantity = model.Quantity;
                await _cartItemRepository.UpdateAsync(cartItem);
            }

            var newCartItem = new CartItem
            {
                OrderId = null,
                Quantity = model.Quantity,
                IsCheckedOut = false,
                ProductId = productId,
                CustomerId = customerId
            };

            var result = await _cartItemRepository.CreateAsync(newCartItem);
            return new CartItemDto()
            {
                Id = result.Id,
                OrderId = result.OrderId,
                Quantity = result.Quantity,
                IsCheckedOut = result.IsCheckedOut,
                ProductDto = new ProductDto()
                {
                    Id = result.Product.Id,
                    ProductName = result.Product.ProductName,
                    Price = result.Product.Price,
                    ImageUrl = result.Product.ImageUrl
                },
                CustomerDto = new CustomerDto()
                {
                    Id = result.Customer.Id,
                    UserDto = new UserDto()
                    {
                        Id = result.Customer.User.Id,
                        FirstName = result.Customer.User.FirstName,
                        LastName = result.Customer.User.LastName,
                        Email = result.Customer.User.Email,
                        PhoneNumber = result.Customer.User.PhoneNumber,
                    }
                }
            };

        }

        public async Task<List<CartItemDto>> GetCartItemsByCustomerIdAsync(int customerId)
        {
            var cartItem = await _cartItemRepository.GetCartItemsAsync(customerId);
            if (cartItem == null)
            {
                return null;
            }
            return cartItem.Select(x => new CartItemDto
            {
                OrderId = x.OrderId.GetValueOrDefault(),
                ProductDto = new ProductDto()
                {
                    Id = x.Product.Id,
                    ProductName = x.Product.ProductName,
                    Price = x.Product.Price,
                    SellerId = x.Product.SellerId,
                    ImageUrl = x.Product.ImageUrl
                },
                Quantity = x.Quantity,
                IsCheckedOut = x.IsCheckedOut,
                Id = x.Id,
                CustomerDto = new CustomerDto()
                {
                    Id = x.Customer.Id,
                    UserDto = new UserDto
                    {
                        FirstName = x.Customer.User.FirstName,
                        LastName = x.Customer.User.LastName,
                        Email = x.Customer.User.Email,
                        PhoneNumber = x.Customer.User.PhoneNumber,
                        Role = x.Customer.User.Role,
                    },
                },

            }).ToList();

        }


        public async Task<CartItemDto> GetCartItemAsync(int customerId, int productId)
        {
            var cartItem = await _cartItemRepository.GetCartItemAsync(customerId, productId);
            if(cartItem == null)
            {
                return null;
            }
            return new CartItemDto
            {
                OrderId = cartItem.OrderId.GetValueOrDefault(),
                Id = cartItem.Id,
                ProductDto = new ProductDto
                {
                    Id = productId,
                    ProductName = cartItem.Product.ProductName,
                    Price = cartItem.Product.Price,
                    SellerId = cartItem.Product.SellerId,
                    ImageUrl = cartItem.Product.ImageUrl
                },
                Quantity = cartItem.Quantity,
                IsCheckedOut = cartItem.IsCheckedOut,
                CustomerDto = new CustomerDto
                {
                    UserDto = new UserDto
                    {
                        FirstName = cartItem.Customer.User.FirstName,
                        LastName = cartItem.Customer.User.LastName,
                        Email = cartItem.Customer.User.Email,
                        PhoneNumber = cartItem.Customer.User.PhoneNumber,
                        Role = cartItem.Customer.User.Role,
                    },
                    Id = cartItem.Customer.Id
                }

            };

        }

        public async Task<CartItemDto> GetCartItemByCartItemIdAsync(int cartItemId)
        {
            var cartItem = await _cartItemRepository.GetAsync(cartItemId);
            if(cartItem == null)
            {
                return null;
            }
            return new CartItemDto
            {
                Id = cartItem.Id,
                OrderId = cartItem.OrderId.GetValueOrDefault(),
                ProductDto = new ProductDto
                {
                    Id = cartItem.Product.Id,
                    ProductName = cartItem.Product.ProductName,
                    Price = cartItem.Product.Price,
                    SellerId = cartItem.Product.SellerId,
                    ImageUrl = cartItem.Product.ImageUrl
                },
                Quantity = cartItem.Quantity,
                IsCheckedOut = cartItem.IsCheckedOut,
                CustomerDto = new CustomerDto
                {
                    UserDto = new UserDto
                    {
                        FirstName = cartItem.Customer.User.FirstName,
                        LastName = cartItem.Customer.User.LastName,
                        Email = cartItem.Customer.User.Email,
                        PhoneNumber = cartItem.Customer.User.PhoneNumber,
                        Role = cartItem.Customer.User.Role,
                    },
                    Id = cartItem.Customer.Id
                }
            };

        }
        // public async Task<BaseResponse> ChangeCartItemQuantityAsync(UpdateCartItemRequestModel updatedCartItem, int id)
        // {
        //     var cartItem = await _cartItemRepository.GetAsync(x => x.Id == id);
        //     if (cartItem == null)
        //     {
        //         return new BaseResponse()
        //         {
        //             Message = "Cart item not found",
        //             Success = false,
        //         };
        //     }
        //     cartItem.Quantity = updatedCartItem.Quantity;
        //     await _cartItemRepository.UpdateAsync(cartItem);
        //     return new BaseResponse()
        //     {
        //         Message = "Quantity updated  successfully",
        //         Success = true,
        //     };

        // }

        public async Task<bool> UpdateCartItemStatusAsync(int id)
        {
            var cartItem = await _cartItemRepository.GetAsync(x => x.Id == id);
            if (cartItem == null)
            {
                return false;
            }
            cartItem.IsCheckedOut = true;
            await _cartItemRepository.UpdateAsync(cartItem);
            return true;

        }
        public async Task<bool> DeleteCartItemByIdAsync(int cartItemId)
        {
            var cartItem = await _cartItemRepository.GetAsync(cartItemId);
            if (cartItem == null)
            {
                return false;
            }
            await _cartItemRepository.DeleteAsync(cartItem);
            return true;
        }

    }
}