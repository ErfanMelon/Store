using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Application.Interfaces.FacadePatterns;
using Store.EndPoint.Tools;

namespace Store.EndPoint.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderFacade _orderFacade;
        public OrderController(IOrderFacade orderFacade)
        {
            _orderFacade = orderFacade;
        }

        public IActionResult Index()
        {
            long userId = ClaimTool.GetUserId(User).Value;
            return View(_orderFacade.getCustomerOrdersService.Execute(userId).Data);
        }
        public IActionResult Factor(long orderId)
        {
            long userId = ClaimTool.GetUserId(User).Value;
            return View(_orderFacade.getCustomerOrderService.Execute(userId, orderId).Data);
        }
    }
}
