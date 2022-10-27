using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Store.Application.Interfaces.FacadePatterns;
using Store.Domain.Entities.Orders;
using Store.Common;

namespace Store.EndPoint.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrdersController : Controller
    {
        private readonly IOrderFacade _orderFacade;
        public OrdersController(IOrderFacade orderFacade)
        {
            _orderFacade = orderFacade;
        }

        public IActionResult Index(OrderState? orderState,string? Searchkey,int page=1,int pagesize=20)
        {
            var customerOrders = _orderFacade.getCustomerOrdersAdminService.Execute(orderState,Searchkey,page,pagesize);
            return View(customerOrders.Data);
        }
        public IActionResult Detail(long orderId)
        {
            var customerOrder = _orderFacade.getCustomerOrderAdminService.Execute(orderId);
            return View(customerOrder.Data);
        }
        [HttpPost]
        public IActionResult ChangeOrderState(long orderId, OrderState orderState)
        {
            return Json(_orderFacade.changeOrderStateService.Execute(orderId, orderState));
        }
        public IActionResult ChangeOrderState(long orderId)
        {
            ViewBag.OrderStates = GetOrderState();
            return PartialView(orderId);
        }
        public IActionResult ChangeOrderDetailState(long orderDetailId)
        {
            ViewBag.OrderStates = GetOrderState();
            return PartialView(orderDetailId);
        }
        [HttpPost]
        public IActionResult ChangeOrderDetailState(long DetailId, OrderState orderState)
        {
            return Json(_orderFacade.changeOrderDetailStateService.Execute(DetailId, orderState));
        }
        
        SelectList GetOrderState()
        {
            var orderstates = from OrderState o in Enum.GetValues(typeof(OrderState))
                              select new
                              {
                                  ID = (int)o,
                                  Name = EnumHelpers<OrderState>.GetDisplayValue(o)
                              };
            return new SelectList(orderstates, "ID", "Name");
        }
    }
}
