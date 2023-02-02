using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Application.Services.Products.Commands.AddBrand;
using Store.Application.Services.Products.Commands.DeleteBrandService;
using Store.Application.Services.Products.Queries.GetBrands;

namespace Store.EndPoint.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin,Operator")]
public class BrandController : Controller
{
    private readonly IMediator _mediator;
    public BrandController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet(Name = "GetBrands")]
    public async Task<ActionResult> Index()
    {
        var brands = await _mediator.Send(new GetBrandsQuery());
        return View(brands);
    }
    public async Task<ActionResult> Create(AddBrandCommand command)
    {
        var result = _mediator.Send(command);
        return Json(await result);
    }
    [HttpPost("DeleteBrand")]
    public async Task<ActionResult> Delete(DeleteBrandCommand command)
    {
        var result = _mediator.Send(command);
        return Json(await result);
    }
}
