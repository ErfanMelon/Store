using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Application.Services.HomePages.Commands.AddBanner;
using Store.Application.Services.HomePages.Commands.ChangeBannerState;
using Store.Application.Services.HomePages.Commands.DeleteBanner;
using Store.Application.Services.HomePages.Queries.GetBannersAdmin;

namespace Store.EndPoint.Areas.Admin.Controllers;

[Authorize(Roles = "Admin,Operator")]
[Area("Admin")]
// Advertisement Controller
public class AdController : Controller
{
    private readonly IMediator _mediator;

    public AdController(IMediator mediator)
    {
        _mediator = mediator;
    }
    public IActionResult Create() => View();
    [HttpPost]
    public async Task<IActionResult> Create(AddBannerCommand command)
    {
        var result = await _mediator.Send(command);
        return Json(result);
    }
    public async Task<IActionResult> Index(int page = 1, int pagesize = 30)
    {
        var result = await _mediator.Send(new GetBannersAdminQuery(page, pagesize));
        return View(result);
    }
    [HttpPost]
    public async Task<IActionResult> Delete(DeleteBannerCommand command)
    {
        return Json(await _mediator.Send(command));
    }
    [HttpPost]
    public async Task<IActionResult> ChangeVisibility(ChangeBannerVisibilityCommand command)
    {
        return Json(await _mediator.Send(command));
    }
}
