using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Application.Interfaces.FacadePatterns;

namespace Store.EndPoint.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Operator")]
    [Area("Admin")]
    public class PayController : Controller
    {
        private readonly IRequestPayFacade _requestPayFacade;
        public PayController(IRequestPayFacade requestPayFacade)
        {
            _requestPayFacade = requestPayFacade;
        }

        public IActionResult Index(int page = 1, int pagesize = 20)
        {
            var result = _requestPayFacade.getRequestPaysService.Execute(page, pagesize);
            return View(result.Data);
        }
        public IActionResult DeletePay(Guid payId)
        {
            var result = _requestPayFacade.deletePayRequestService.Execute(payId);
            return Json(result);
        }
    }
}
