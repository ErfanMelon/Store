using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Store.Application.Interfaces.FacadePatterns;
using Store.Application.Services.Users.Commands.LoginUser;
using Store.Application.Services.Users.Commands.RegisterUser;
using Store.Common.Dto;
using Store.EndPoint.Models.AuthenticationViewModel;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace Store.EndPoint.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IUserFacade _userFacade;
        public AuthenticationController(IUserFacade userFacade)
        {
            _userFacade = userFacade;
        }

        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Signup(SignupViewModel request)
        {
            var signeupResult = _userFacade.registerUserService.Execute(new RegisterUserDto
            {
                Email = request.Email,
                FullName = request.FullName,
                Password = request.Password,
                RePassword = request.RePassword,
                RoleId = 3,
                Address=request.Address,
                PhoneNumber=request.Phone,
                ZipCode=request.ZipCode
            });

            if (signeupResult.IsSuccess == true)
            {
                var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,signeupResult.Data.UserId.ToString()),
                new Claim(ClaimTypes.Email, request.Email),
                new Claim(ClaimTypes.Name, request.FullName),
                new Claim(ClaimTypes.Role, "Customer"),
            };


                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                var properties = new AuthenticationProperties()
                {
                    IsPersistent = true
                };
                HttpContext.SignInAsync(principal, properties);

            }
            return Json(signeupResult);
        }


        public IActionResult Signin(string ReturnUrl = "/")
        {
            ViewBag.url = ReturnUrl;
            return View();
        }

        [HttpPost]
        public IActionResult Signin(string Email, string Password, string url = "/")
        {
            var signupResult = _userFacade.userLoginService.Execute(new RequestLoginDto { Username = Email, Password = Password });
            if (signupResult.IsSuccess == true)
            {
                var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,signupResult.Data.UserId.ToString()),
                new Claim(ClaimTypes.Email, Email),
                new Claim(ClaimTypes.Name, signupResult.Data.Name),
                new Claim(ClaimTypes.Role, signupResult.Data.Role ),
            };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                var properties = new AuthenticationProperties()
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.Now.AddDays(5),
                };
                HttpContext.SignInAsync(principal, properties);

            }
            return Json(signupResult);
        }


        public IActionResult SignOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }
    }
}
