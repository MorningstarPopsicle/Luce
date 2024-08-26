using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Web;
using Luce.Interface.Services;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
namespace Luce.Controllers
{

    public class LoginController : Controller
    {
        private readonly IUserService _userService;

        public LoginController(IUserService userService)
        {
            _userService = userService;


        }



        public async Task<IActionResult> SignIn(string email, string password)
        {
            if (HttpContext.Request.Method == "POST")
            {
                var login = await _userService.Login(email, password);
                if (login == null)
                {
                    TempData["Message"] = "Email or Password is incorrect";
                    return View();
                }

                var claims = new List<Claim>
                {
                    new Claim (ClaimTypes.NameIdentifier, login.Id.ToString()),
                    new Claim (ClaimTypes.Email, login.Email),
                    new Claim (ClaimTypes.Name, login.Password),
                    new Claim (ClaimTypes.Role, login.Role.ToString()),

                };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authenticationProperties = new AuthenticationProperties();
                var principal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authenticationProperties);
                if (login.Role == Role.Customer)
                {
                    return RedirectToAction("Index", "Customer");
                }
                else if (login.Role == Role.Seller)
                {
                    return RedirectToAction("Index", "Seller");
                }
                else if (login.Role == Role.Admin)
                {
                    return RedirectToAction("Index", "Admin");
                }

            }
            return View();



        }

        public async Task LoginWithGoogle()
        {
            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme,
                new AuthenticationProperties
                {
                RedirectUri = Url.Action("GoogleResponse")
                });
        }

        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (result.Succeeded)
        {
            // User is authenticated, handle user data here
            var user = result.Principal;
            // Process user information here
        }
        

        return RedirectToAction("Index");
            // var claims = result.Principal.Identities.FirstOrDefault().Claims.Select(claim => new
            // {
            //     claim.Issuer,
            //     claim.OriginalIssuer,
            //     claim.Type,
            //     claim.Value,
                
            // });
            // return Json(claims);
        }


        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("SignIn");
        }




    }
}
