using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Luce.Interface.Services;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Luce.ViewModels.AdminViewModel;
using Luce.DTOs;
// using Luce.ViewModels;

namespace Luce.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly ISellerService _sellerService;

        public AdminController(IAdminService adminService, ISellerService sellerService)
        {
            _adminService = adminService;
            _sellerService = sellerService;
        }

        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> Create(CreateAdminViewModel model)
        {
            AdminDto oldModel = new AdminDto()
            {
                Id = model.Id,
                AccountNumber = model.AccountNumber,
                Admin = new UserDto()
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
                var admin = await _adminService.Register(oldModel);
                if (admin != null)
                {
                    return RedirectToAction("SignIn", "Login");
                }
                return StatusCode(406, "Admin not Registered");
            }
            return View();
        }



        public async Task<IActionResult> ViewProfile()
        {
            string claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
             int id = int.Parse(claim);
            var profile = await _adminService.GetById(id);
            ViewProfileVM model = new ViewProfileVM()
            {
                Id = profile.Id,
                FirstName = profile.Admin.FirstName,
                LastName = profile.Admin.LastName,
                Email = profile.Admin.Email,
                PhoneNumber = profile.Admin.PhoneNumber,
                AccountNumber = profile.AccountNumber
            };
            return View(model);

        }

        public async Task<IActionResult> Update(UpdateAdminViewModel model)
        {
             AdminDto oldModel = new AdminDto()
            {
                Id = model.Id,
                AccountNumber = model.AccountNumber,
                Admin = new UserDto()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                }
            };
            string claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
             int id = int.Parse(claim);
            var admin = await _adminService.GetById(id);
            model = new UpdateAdminViewModel()
            {
                Id = admin.Id,
                FirstName = admin.Admin.FirstName,
                LastName = admin.Admin.LastName,
                PhoneNumber = admin.Admin.PhoneNumber,
                AccountNumber = admin.AccountNumber
            };
            if (HttpContext.Request.Method == "POST")
            {
                var adm = await _adminService.UpdateAdminAsync(oldModel, id);
                if (adm == true)
                {
                    return RedirectToAction("ViewProfile", "Admin");
                }
                return StatusCode(406, "Admin not Updated");
            }
            return View(model);
        }

        
        public async Task<IActionResult> Delete( int sellerId)
        {
            var seller = await _sellerService.DeleteSellerAsync(sellerId);
             if (seller == true)
                {

                    return RedirectToAction("GetAllSellers");
                }

            return RedirectToAction("GetAllSellers");
        
        }
      
      

        public async Task<IActionResult> Verify( int id)
        {
            var seller = await _sellerService.VerifySellerAsync(id);
            if (seller == true)
                {
                    return RedirectToAction("GetAllSellers");
                }

            return RedirectToAction("GetAllSellers");
        }

        

        public async Task<IActionResult> GetAllSellers()
        {
                string claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
                int id = int.Parse(claim);
                var admin = await _adminService.GetById(id);
                var sellers = await _sellerService.GetSellers();
                ViewSellersViewModel model = new ViewSellersViewModel()
                {
                    Sellers = sellers
                };
                if (sellers != null)
                {
                    return View(model);
                }
                 return StatusCode(406, "No Seller Found");  
        }

    }
}