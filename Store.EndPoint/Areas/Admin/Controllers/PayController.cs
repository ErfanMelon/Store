using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Application.Services.Fainances.Commands.DeletePayRequest;
using Store.Application.Services.Fainances.Queries.GetRequestPays;

namespace Store.EndPoint.Areas.Admin.Controllers;

[Authorize(Roles = "Admin,Operator")]
[Area("Admin")]
public class PayController : Controller
{
    private readonly IMediator _mediator;

    public PayController(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task<IActionResult> Index(int page = 1, int pagesize = 20)
    {
        var result = await _mediator.Send(new GetRequestPaysQuery(page, pagesize));
        return View(result);
    }
    [HttpPost]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _mediator.Send(new DeletePayRequestCommand(id));
        return Json(result);
    }
}
