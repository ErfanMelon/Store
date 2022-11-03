using Microsoft.AspNetCore.Mvc;
using Store.Application.Interfaces.FacadePatterns;

namespace Store.EndPoint.Areas.Admin.Controllers
{
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
    }
}
