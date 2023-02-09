using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Store.Application.Interfaces.FacadePatterns;
using Store.Application.Services.Carts.Commands.AttachUserToCart;
using Store.Application.Services.Users.Commands.LoginUser;
using Store.Application.Services.Users.Commands.RegisterUser;
using Store.EndPoint.Models.AuthenticationViewModel;
using Store.EndPoint.Tools;
using System.Security.Claims;

namespace Store.EndPoint.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IUserFacade _userFacade;
        private readonly IMediator _mediator;
        private readonly Tools.ICookieManager _cookieManager;
        public AuthenticationController(IUserFacade userFacade, IMediator mediator, Tools.ICookieManager cookieManager)
        {
            _userFacade = userFacade;
            _mediator = mediator;
            _cookieManager = cookieManager;
        }

        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Signup(SignupViewModel request)
        {
            var signeupResult = _userFacade.registerUserService.Execute(new RegisterUserDto
            {
                Email = request.Email,
                FullName = request.FullName,
                Password = request.Password,
                RePassword = request.RePassword,
                RoleId = 3,
                Address = request.Address,
                PhoneNumber = request.Phone,
                ZipCode = request.ZipCode
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

                await HttpContext.SignInAsync(principal, properties);

                Guid browserid = _cookieManager.GetBrowserId(HttpContext);
                await _mediator.Send(new AttachUserToCartCommand(signeupResult.Data.UserId, browserid));

            }
            return Json(signeupResult);
        }


        public IActionResult Signin(string ReturnUrl = "/")
        {
            ViewBag.url = ReturnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Signin(string Email, string Password, string url = "/")
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

                await HttpContext.SignInAsync(principal, properties);

                Guid browserid = _cookieManager.GetBrowserId(HttpContext);
                await _mediator.Send(new AttachUserToCartCommand(signupResult.Data.UserId, browserid));

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
