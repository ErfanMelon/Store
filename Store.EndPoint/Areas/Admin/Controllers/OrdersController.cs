using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Store.Application.Interfaces.FacadePatterns;
using Store.Domain.Entities.Orders;
using Store.Common;
using Store.Application.Services.Orders.Commands.EditOrderDetail;
using Store.EndPoint.Areas.Admin.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Store.EndPoint.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Operator")]
    [Area("Admin")]
    public class OrdersController : Controller
    {
        private readonly IOrderFacade _orderFacade;
        static SelectList orderstatelist = GetOrderState();
        public OrdersController(IOrderFacade orderFacade)
        {
            _orderFacade = orderFacade;
        }

        public IActionResult Index(OrderState? orderState, string? Searchkey, int page = 1, int pagesize = 20)
        {
            var customerOrders = _orderFacade.getCustomerOrdersAdminService.Execute(orderState, Searchkey, page, pagesize);
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
            ViewBag.OrderStates = orderstatelist;
            return PartialView(orderId);
        }
        public IActionResult ChangeOrderDetailState(long orderDetailId)
        {
            ViewBag.OrderStates = orderstatelist;
            return PartialView(orderDetailId);
        }
        [HttpPost]
        public IActionResult ChangeOrderDetailState(long DetailId, OrderState orderState)
        {
            return Json(_orderFacade.changeOrderDetailStateService.Execute(DetailId, orderState));
        }

        static SelectList GetOrderState()
        {
            var orderstates = from OrderState o in Enum.GetValues(typeof(OrderState))
                              select new
                              {
                                  ID = (int)o,
                                  Name = EnumHelpers<OrderState>.GetDisplayValue(o)
                              };
            return new SelectList(orderstates, "ID", "Name");
        }
        [HttpPost]
        public IActionResult EditOrderDetail(RequestEditOrderDetailDto dto)
        {
            var result = _orderFacade.editOrderDetailService.Execute(dto);
            return Json(result);
        }
        [HttpPost]
        public IActionResult EditOrderDetailPage(EditOrderDetailViewModel model)
        {
            return PartialView("EditOrderDetail", model);
        }
        public IActionResult EditOrder()// Incomplete
        {
            return PartialView();
        }
        [HttpPost]
        public IActionResult EditOrder(long orderid, string description)
        {
            var result = _orderFacade.editOrderService.Execute(new Application.Services.Orders.Commands.EditOrder.RequestEditOrderDto
            {
                Description=description,
                OrderId=orderid
            });
            return Json(result);
        }
        public IActionResult DeleteOrder(long orderId)
        {
            var result =_orderFacade.deleteOrderService.Execute(orderId);
            return Json(result);
        }
    }
}
