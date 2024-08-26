using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Luce.Interface.Services;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Luce.DTOs;
using Luce.ViewModels.SellerViewModel;
using Luce.ViewModels.DispatchViewModel;
using Luce.ViewModels.ProductViewModel;

namespace Luce.Controllers
{
    public class SellerController : Controller
    {
        private readonly ISellerService _sellerService;
        private readonly IDispatchService _dispatchService;
        private readonly IProductService _productService;

        public SellerController(ISellerService sellerService, IProductService productService, IDispatchService dispatchService)
        {
            _sellerService = sellerService;
            _dispatchService = dispatchService;
            _productService = productService;

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateSellerViewModel model)
        {
            SellerDto oldModel = new SellerDto()
            {
                Id = model.Id,
                AccountNumber = model.AccountNumber,
                Logo = model.Logo,
                IsVerified = model.IsVerified,
                StoreName = model.StoreName,
                UserDto = new UserDto()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Password = model.Password,
                    PhoneNumber = model.PhoneNumber,
                    Role = model.Role
                },
                Address = new AddressDto()
                {
                    HouseNumber = model.Address.HouseNumber,
                    StreetName = model.Address.StreetName,
                    LGA = model.Address.LGA,
                    Town = model.Address.Town,
                    State = model.Address.State,
                    Country = model.Address.Country
                }
            };

            var seller = await _sellerService.Register(oldModel);
            if (seller != null)
            {
                return RedirectToAction("SignIn", "Login");
            }
            return StatusCode(406, "Seller not Registered");
        }

        public async Task<IActionResult> RegisterDispatch(CreateDispatchViewModel model)
        {
            DispatchDto oldModel = new DispatchDto()
            {
                Id = model.Id,
                UserDto = new UserDto()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Role = model.Role
                }
            };
            if (HttpContext.Request.Method == "POST")
            {
                string claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
                int id = int.Parse(claim);
                var seller = await _sellerService.GetById(id);
                var dispatch = await _dispatchService.RegisterDispatch(oldModel, seller.Id);
                if (dispatch != null)
                {
                    return RedirectToAction("GetDispatches", "Seller");
                }
                return StatusCode(406, "Dispatch not Registered");
            }
            return View();
        }

        public async Task<IActionResult> ViewProfile()
        {
            string claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int id = int.Parse(claim);
            var profile = await _sellerService.GetById(id);
            ViewProfileVM model = new ViewProfileVM()
            {
                Id = profile.Id,
                FirstName = profile.UserDto.FirstName,
                LastName = profile.UserDto.LastName,
                Email = profile.UserDto.Email,
                PhoneNumber = profile.UserDto.PhoneNumber,
                Logo = profile.Logo,
                StoreName = profile.StoreName,
                AccountNumber = profile.AccountNumber,
                Address = new AddressDto()
                {
                    HouseNumber = profile.Address.HouseNumber,
                    StreetName = profile.Address.StreetName,
                    LGA = profile.Address.LGA,
                    Town = profile.Address.Town,
                    State = profile.Address.State,
                    Country = profile.Address.Country
                }
            };

            return View(model);
        }

        public async Task<IActionResult> Update()
        {
            string claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int id = int.Parse(claim);
            var sel = await _sellerService.GetById(id);
            UpdateSellerViewModel model = new UpdateSellerViewModel()
            {
                Id = sel.Id,
                FirstName = sel.UserDto.FirstName,
                LastName = sel.UserDto.LastName,
                PhoneNumber = sel.UserDto.PhoneNumber,
                Logo = sel.Logo,
                StoreName = sel.StoreName,
                AccountNumber = sel.AccountNumber,
                Address = new AddressDto()
                {
                    HouseNumber = sel.Address.HouseNumber,
                    StreetName = sel.Address.StreetName,
                    LGA = sel.Address.LGA,
                    Town = sel.Address.Town,
                    State = sel.Address.State,
                    Country = sel.Address.Country
                }
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateSellerViewModel model)
        {
            SellerDto oldModel = new SellerDto()
            {
                Id = model.Id,
                UserDto = new UserDto
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                },
                Logo = model.Logo,
                StoreName = model.StoreName,
                AccountNumber = model.AccountNumber,
                Address = new AddressDto
                {
                    HouseNumber = model.Address.HouseNumber,
                    StreetName = model.Address.StreetName,
                    LGA = model.Address.LGA,
                    Town = model.Address.Town,
                    State = model.Address.State,
                    Country = model.Address.Country
                }
            };
            string claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int id = int.Parse(claim);
            var seller = await _sellerService.UpdateSellerAsync(oldModel, id);
            if (seller == true)
            {
                return RedirectToAction("ViewProfile", "Seller");
            }
            return StatusCode(406, "Seller not Updated");
        }

        public async Task<IActionResult> GetDispatches()
        {
            string claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int id = int.Parse(claim);
            var seller = await _sellerService.GetById(id);
            var dispatches = await _dispatchService.GetDispatchesBySellerId(seller.Id);
            GetDispatchesViewModel model = new GetDispatchesViewModel()
            {
                Dispatches = dispatches
            };

            if (dispatches != null)
            {
                return View(model);
            }
            return StatusCode(406, "No Dispatch Found");
        }

        public async Task<IActionResult> GetAllProducts()
        {
            string claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int id = int.Parse(claim);
            var seller = await _sellerService.GetById(id);
            var products = await _productService.GetBySellerId(seller.Id);
            GetAllProductsViewModel model = new GetAllProductsViewModel()
            {
                Products = products
            };
            if (products != null)
            {
                return View(model);
            }
            return StatusCode(406, "No Product Found");
        }

         public async Task<IActionResult> Delete(int id)
        {
            var result = await _dispatchService.DeleteDispatchAsync(id);
                if (result == true)
                {
                    return RedirectToAction("GetDispatches");
                }

            return RedirectToAction("GetDispatches");
        }
        public async Task<IActionResult> CreateProduct(CreateProductViewModel model)
        {
            ProductDto oldModel = new ProductDto()
            {
                Id = model.Id,
                ProductName = model.ProductName,
                Price = model.Price,
                Quantity = model.Quantity,
                InitialQuantity = model.InitialQuantity,
                ImageUrl = model.ImageUrl,
            };

            if (HttpContext.Request.Method == "POST")
            {
                string claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
                int id = int.Parse(claim);
                var sel = await _sellerService.GetById(id);
                var product = await _productService.CreateProductAsync(oldModel, sel.Id);
                if (product != null)
                {
                    return RedirectToAction("GetAllProducts", "Seller");
                }
                return StatusCode(406, "Product not Added");
            }
            return View();
        }


        public async Task<IActionResult> UpdateProduct(int productId, UpdateProductViewModel model)
        {
            ProductDto oldModel = new ProductDto()
            {
                Id = model.Id,
                ProductName = model.ProductName,
                Price = model.Price,
                Quantity = model.Quantity,
                ImageUrl = model.ImageUrl
            };
            string claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int id = int.Parse(claim);
            var sel = await _sellerService.GetById(id);
            var prod = await _productService.GetById(productId, sel.Id);
            model = new UpdateProductViewModel()
            {
                Id = prod.Id,
                ProductName = prod.ProductName,
                Price = prod.Price,
                Quantity = prod.Quantity,
                ImageUrl = prod.ImageUrl
            };
            if (HttpContext.Request.Method == "POST")
            {
                var product = await _productService.UpdateProduct(oldModel, prod.Id, sel.Id);
                if (product == true)
                {
                    return RedirectToAction("GetAllProducts", "Seller");
                }
                return StatusCode(406, "Product not Updated");
            }
            return View(model);
        }





    }
}