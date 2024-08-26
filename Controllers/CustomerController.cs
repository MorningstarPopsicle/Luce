using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Luce.Interface.Services;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Luce.DTOs;
using Luce.ViewModels.CustomerViewModel;
using Luce.ViewModels;
using Luce.ViewModels.ProductViewModel;

namespace Luce.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        private readonly IProductService _productService;
        private readonly ICartItemService _cartItemService;

        public CustomerController(ICustomerService customerService, IProductService productService, ICartItemService cartItemService)
        {
            _customerService = customerService;
            _productService = productService;
            _cartItemService = cartItemService;

        }

        public async Task<IActionResult> Index()
        {
            string claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int id = int.Parse(claim);
            var customer = await _customerService.GetById(id);
            if (customer == null)
            {
                return StatusCode(406, "Customer not logged in");
            }
            var products = await _productService.GetAllProducts();
            GetAllProductsViewModel model = new GetAllProductsViewModel()
            {
                Products = products
            };
            return View(model);

        }


        public async Task<IActionResult> Create(CreateCustomerViewModel model)
        {
            CustomerDto oldModel = new CustomerDto()
            {
                Id = model.Id,
                UserDto = new UserDto()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Password = model.Password,
                    PhoneNumber = model.PhoneNumber,
                    Role = model.Role
                }
            };
            if (HttpContext.Request.Method == "POST")
            {
                var customer = await _customerService.Register(oldModel);
                if (customer != null)
                {
                    return RedirectToAction("SignIn", "Login");
                }
                return StatusCode(406, "Customer not Registered");
            }
            return View();
        }



        public async Task<IActionResult> ViewProfile()
        {
            string claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int id = int.Parse(claim);
            var profile = await _customerService.GetById(id);
            ViewProfileViewModel model = new ViewProfileViewModel()
            {
                Id = profile.Id,
                FirstName = profile.UserDto.FirstName,
                LastName = profile.UserDto.LastName,
                Email = profile.UserDto.Email,
                PhoneNumber = profile.UserDto.PhoneNumber,
            };
            return View(model);

        }

        public async Task<IActionResult> Update(UpdateCustomerViewModel model)
        {
            CustomerDto oldModel = new CustomerDto()
            {
                Id = model.Id,
                UserDto = new UserDto()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                }
            };
            string claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int id = int.Parse(claim);
            var cus = await _customerService.GetById(id);
            model = new UpdateCustomerViewModel()
            {
                Id = cus.Id,
                FirstName = cus.UserDto.FirstName,
                LastName = cus.UserDto.LastName,
                PhoneNumber = cus.UserDto.PhoneNumber
            };

            if (HttpContext.Request.Method == "POST")
            {

                var customer = await _customerService.UpdateCustomer(oldModel, id);
                if (customer == true)
                {
                    return RedirectToAction("ViewProfile", "Customer");
                }
                return StatusCode(406, "Customer not Updated");
            }
            return View(model);
        }


        public async Task<IActionResult> ViewProduct(int productId)
        {
            var product = await _productService.GetProductById(productId);
            var model = new ViewProductViewModel()
            {
                Id = product.Id,
                ProductName = product.ProductName,
                Price = product.Price,
                Quantity = product.Quantity,
                ImageUrl = product.ImageUrl
            };
            return View(model);
        }



        public async Task<IActionResult> CreateCartItem(int productId, int quantity)
        {
            var product = await _productService.GetProductById(productId);
            var model = new ProductCartItemViewModel
            {
                Product = product,
                CartItem = new CreateCartItemViewModel()
                {
                    Quantity = quantity,
                },
            };

            if (HttpContext.Request.Method == "POST")
            {
                string claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
                int id = int.Parse(claim);
                var customer = await _customerService.GetById(id);
                var oldModel = new CartItemDto()
                {
                    Id = model.CartItem.Id,
                    OrderId = model.CartItem.OrderId,
                    Quantity = model.CartItem.Quantity,
                    IsCheckedOut = model.CartItem.IsCheckedOut,
                    // TotalPrice = model.CartItem.TotalPrice,
                    ProductDto = new ProductDto()
                    {
                        Id = model.Product.Id,
                        ImageUrl = model.Product.ImageUrl,
                        ProductName = model.Product.ProductName,
                        Price = model.Product.Price
                    }
                };
                var cartItem = await _cartItemService.CreateCartItemAsync(oldModel, oldModel.ProductDto.Id, customer.Id);

                if (cartItem != null)
                {
                    return StatusCode(406, "Product Added to Cart");
                }
                return StatusCode(406, "Product not found");
            }
            return View(model);
        }


        public async Task<IActionResult> ViewCart()
        {
                string claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
                 int id = int.Parse(claim);
                var customer = await _customerService.GetById(id);
                var cartItems = await _cartItemService.GetCartItemsByCustomerIdAsync(customer.Id);
                var model = new ViewCartViewModel()
                {
                    CartItems = cartItems
                };
                if (cartItems != null)
                {
                    return View(model);
                }
                return StatusCode(406, "Cart is Empty");  
        }


        public async Task<IActionResult> Delete(int cartItemId)
        {
            var result = await _cartItemService.DeleteCartItemByIdAsync(cartItemId);
                if (result == true)
                {

                    return RedirectToAction("ViewCart");
                }

            return RedirectToAction("ViewCart");
        }

        


    }
}