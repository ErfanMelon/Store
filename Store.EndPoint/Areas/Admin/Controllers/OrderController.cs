using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Store.Application.Services.Orders.Commands.ChangeOrderDetailState;
using Store.Application.Services.Orders.Commands.ChangeOrderState;
using Store.Application.Services.Orders.Commands.DeleteOrder;
using Store.Application.Services.Orders.Commands.EditOrder;
using Store.Application.Services.Orders.Commands.EditOrderDetail;
using Store.Application.Services.Orders.Queries.GetCustomerOrderAdmin;
using Store.Application.Services.Orders.Queries.GetCustomerOrdersAdmin;
using Store.Common;
using Store.Domain.Entities.Orders;

namespace Store.EndPoint.Areas.Admin.Controllers;

[Authorize(Roles = "Admin,Operator")]
[Area("Admin")]
public class OrderController : Controller
{
    private readonly IMediator _mediator;

    public OrderController(IMediator mediator)
    {
        _mediator = mediator;
    }

    static SelectList orderstatelist = GetOrderState();

    public async Task<IActionResult> Index(OrderState? orderState, string? Searchkey, int page = 1, int pagesize = 20)
    {
        ViewBag.OrderStates = GetOrderState();
        var result = await _mediator.Send(new GetCustomerOrdersAdminQuery(orderState, Searchkey, page, pagesize));
        return View(result);
    }
    public async Task<IActionResult> Detail(long id)
    {
        ViewBag.OrderStates = GetOrderState();
        var result = await _mediator.Send(new GetCustomerOrderAdminQuery(id));
        return View(result);
    }
    [HttpPost]
    public async Task<IActionResult> ToggleOrder(ToggleOrderCommand command)
    {
        var result = await _mediator.Send(command);
        return Json(result);
    }
    [HttpPost]
    public async Task<IActionResult> ToggleOrderDetail(ToggleOrderDetailCommand command)
    {
        var result = await _mediator.Send(command);
        return Json(result);
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
    public async Task<IActionResult> EditOrderDetail(EditOrderDetailCommand command)
    {
        var result = await _mediator.Send(command);
        return Json(result);
    }
    [HttpPost]
    public async Task<IActionResult> EditOrder(EditOrderCommand command)
    {
        var result = await _mediator.Send(command);
        return Json(result);
    }
    [HttpPost]
    public async Task<IActionResult> Delete(DeleteOrderCommand command)
    {
        var result = await _mediator.Send(command);
        return Json(result);
    }
}
