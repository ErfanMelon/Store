using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Application.Services.Orders.Queries.GetCustomerOrder;
using Store.Application.Services.Orders.Queries.GetCustomerOrders;
using Store.EndPoint.Tools;

namespace Store.EndPoint.Controllers;

[Authorize]
public class OrderController : Controller
{
    private readonly IMediator _mediator;

    public OrderController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IActionResult> Index()
    {
        long userId = ClaimTool.GetUserId(User).Value;
        var result = await _mediator.Send(new GetCustomerOrdersQuery(userId));
        return View(result);
    }
    public async Task<IActionResult> Factor(long id)
    {
        long userId = ClaimTool.GetUserId(User).Value;
        var result = await _mediator.Send(new GetCustomerOrderQuery(userId, id));
        return View(result);
    }
}
