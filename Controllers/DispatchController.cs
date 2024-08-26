using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Luce.Interface.Services;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Luce.ViewModels.DispatchViewModel;
using Luce.DTOs;
using Luce.ViewModels;

namespace Luce.Controllers
{
    public class DispatchController : Controller
    {
        private readonly ISellerService _sellerService;
        private readonly IDispatchService _dispatchService;

        public DispatchController(ISellerService sellerService, IDispatchService dispatchService)
        {
            _sellerService = sellerService;
            _dispatchService = dispatchService;
        }

        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> ContinueRegistration(ContinueRegViewModel model, string email)
        {
            DispatchDto oldModel = new DispatchDto()
            {
                UserDto = new UserDto()
                {
                    Password = model.Password,
                    PhoneNumber = model.PhoneNumber
                }
            };
            var dispatch = await _dispatchService.GetByEmail(email);
            if (HttpContext.Request.Method == "POST")
            {
                var newDispatch = await _dispatchService.CompleteRegisteration(oldModel);
                if (newDispatch != false)
                {
                    return RedirectToAction("Index", "Dispatch");
                }
                return StatusCode(406, "Registration not Completed");
            }
            return View();
        }



        public async Task<IActionResult> ViewProfile(string email)
        {
            string claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int id = int.Parse(claim);
            var profile = await _dispatchService.GetById(id);
            ViewProfileViewModel model = new ViewProfileViewModel()
            {
                Id = profile.Id,
                FirstName = profile.UserDto.FirstName,
                LastName = profile.UserDto.LastName,
                Email = profile.UserDto.Email,
                PhoneNumber = profile.UserDto.PhoneNumber,
            };
            return View(profile);

        }

        public async Task<IActionResult> Update(UpdateDispatchViewModel model)
        {
            DispatchDto oldModel = new DispatchDto()
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
            var disp = await _dispatchService.GetById(id);
            model = new UpdateDispatchViewModel()
            {
                Id = disp.Id,
                FirstName = disp.UserDto.FirstName,
                LastName = disp.UserDto.LastName,
                PhoneNumber = disp.UserDto.PhoneNumber
            };

            if (HttpContext.Request.Method == "POST")
            {
                var seller = await _dispatchService.UpdateDispatch(oldModel, id);
                if (seller == true)
                {
                    return RedirectToAction("ViewProfile", "Dispatch");
                }
                return StatusCode(406, "Dispatch not Updated");
            }
            return View(model);
        }
    }
}