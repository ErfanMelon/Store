using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Application.Interfaces.FacadePatterns;
using Store.Application.Services.Users.Commands.EditUser;
using Store.Application.Services.Users.Commands.EditUserSite;
using Store.EndPoint.Tools;

namespace Store.EndPoint.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserFacade _userFacade;
        public UserController(IUserFacade userFacade)
        {
            _userFacade = userFacade;
        }
        public IActionResult Index()
        {
            var userid = ClaimTool.GetUserId(User);
            var result = _userFacade.getUserDetailSiteService.Execute(userid.Value);
            return View(result.Data);
        }
        public IActionResult Edit()
        {
            var userid = ClaimTool.GetUserId(User);
            var result = _userFacade.getUserDetailSiteService.Execute(userid.Value);
            return View(result.Data);
        }
        [HttpPost]
        public IActionResult Edit(EditUserSiteDto dto)
        {
            var userid = ClaimTool.GetUserId(User);
            dto.UserId = userid.Value;
            var result = _userFacade.editUserSiteService.Execute(dto);
            return Json(result);
        }
    }
}
