using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Store.Application.Interfaces.FacadePatterns;
using Store.Application.Services.Users.Commands.EditUser;
using Store.Application.Services.Users.Commands.RegisterUser;
using Store.Application.Services.Users.Queries.GetUsers;

namespace Store.EndPoint.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IUserFacade _userFacade;
        public UserController(IUserFacade userFacade)
        {
            _userFacade = userFacade;
        }

        public IActionResult List(string searchkey, int page=1,int pagesize=20)
        {
            ViewBag.Roles = new SelectList(_userFacade.getRolesService.Execute().Data, "RoleId", "RoleName");
            return View(_userFacade.getUserService.Execute(new RequestGetUserDto { SearchKey = searchkey, Page = page,PageSize=pagesize }));
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Roles = new SelectList(_userFacade.getRolesService.Execute().Data, "RoleId", "RoleName");
            return View();
        }
        [HttpPost]
        public IActionResult Create(string FullName, string Email, string Password, string RePassword, int RoleId)
        {
            var resultRegisterUser = _userFacade.registerUserService.Execute(new RegisterUserDto
            {
                Email = Email,
                FullName = FullName,
                Password = Password,
                RePassword = RePassword,
                RoleIds = new List<int> { RoleId },
            });
            return Json(resultRegisterUser);
        }
        [HttpPost]
        public IActionResult Edit(long UserId, string FullName, string Email, int RoleId)
        {
            var resultEditUser = _userFacade.editUserService.Execute(new EditUserDto
            {
                Email=Email,
                FullName=FullName,
                UserId=UserId,
                RoleIds=new List<int> { RoleId}
            });
            return Json(resultEditUser);
        }
        [HttpPost]
        public IActionResult ChangeAccountState(long userId)
        {
            var resultChangeUserState = _userFacade.changeUserStateService.Execute(userId);
            return Json(resultChangeUserState);
        }
        [HttpPost]
        public IActionResult Delete(long userId)
        {
            var resultDeleteUser = _userFacade.deleteUserService.Execute(userId);
            return Json(resultDeleteUser);
        }
    }
}
