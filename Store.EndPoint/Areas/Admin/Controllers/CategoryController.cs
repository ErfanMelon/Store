using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Store.Application.Services.Products.Commands.AddCategory;
using Store.Application.Services.Products.Commands.DeleteCategory;
using Store.Application.Services.Products.Commands.EditCategory;
using Store.Application.Services.Products.Queries.GetCategories;
using Store.Application.Services.Products.Queries.GetCategory;
using Store.EndPoint.Areas.Admin.Models.ViewModels;

namespace Store.EndPoint.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin,Operator")]
public partial class CategoryController : Controller
{
    private readonly IMediator _mediator;

    public CategoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IActionResult> Index()
    {
        var result = await _mediator.Send(new GetCategoriesQuery());
        await LoadCategories();
        return View(result);
    }

    private async Task LoadCategories()
    {
        var categories = await _mediator.Send(new GetCategoriesQuery());
        ViewBag.Categorylist = new SelectList(categories.Data.Where(c => c.Parent == null), "CategoryId", "CategoryTitle");
    }

    [HttpPost]
    public async Task<IActionResult> Create(AddCategoryCommand command)
    {
        var result = await _mediator.Send(command);
        return Json(result);
    }
    public async Task<IActionResult> Edit(long id)
    {
        await LoadCategories();
        var result = await _mediator.Send(new GetCategoryQuery(id));
        return PartialView(result);
    }
    [HttpPost]
    public async Task<IActionResult> Edit(EditCategoryCommand command)
    {
        var result = await _mediator.Send(command);
        return Json(result);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(DeleteCategoryCommand command)
    {
        var result = await _mediator.Send(command);
        return Json(result);
    }
    [HttpPost]
    public async Task<IActionResult> Detail(GetCategoryQuery query)
    {
        var result = await _mediator.Send(query);
        return PartialView(result);
    }
}
