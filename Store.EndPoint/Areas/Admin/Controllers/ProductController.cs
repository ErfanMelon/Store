using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Store.Application.Services.Products.Commands.AddProduct;
using Store.Application.Services.Products.Commands.DeleteProduct;
using Store.Application.Services.Products.Commands.EditProduct;
using Store.Application.Services.Products.Queries.GetBrands;
using Store.Application.Services.Products.Queries.GetCategories;
using Store.Application.Services.Products.Queries.GetProductAdmin;
using Store.Application.Services.Products.Queries.GetProductForEdit;
using Store.Application.Services.Products.Queries.GetProductsAdmin;

namespace Store.EndPoint.Areas.Admin.Controllers;

[Authorize(Roles = "Admin,Operator")]
[Area("Admin")]
public class ProductController : Controller
{
    private readonly IMediator _mediator;
    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public IActionResult Create()
    {
        ViewBagsData();
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(AddProductCommand command)
    {
        FormFileCollection images = new FormFileCollection();

        foreach (var file in Request.Form.Files)
        {
            images.Add(file);
        }
        command.Images = images;
        var result = await _mediator.Send(command);
        return Json(result);
    }

    public async Task<IActionResult> Index(int page = 1, int pagesize = 10, string searchKey = null)
    {
        var result = await _mediator.Send(new GetProductsAdminQuery(page, pagesize, searchKey));
        return View(result);
    }
    [HttpPost]
    public async Task<IActionResult> Delete(DeleteProductCommand command)
    {
        var result = await _mediator.Send(command);
        return Json(result);
    }
    public async Task<IActionResult> Detail(long id)
    {
        var product = await _mediator.Send(new GetProductAdminQuery(id));
        if (product.IsSuccess == false)
        {
            return BadRequest();
        }
        return View(product.Data);
    }
    public async Task<IActionResult> Edit(long id)
    {
        var product = await _mediator.Send(new GetProductForEditQuery(id));
        if (product.IsSuccess)
        {
            ViewBagsData();
            return View(product.Data);
        }
        return BadRequest();
    }

    private void ViewBagsData()
    {
        ViewBag.Categories = new SelectList(_mediator.Send(new GetCategoriesQuery()).Result.Data.Where(c => c.Parent != null), "CategoryId", "CategoryTitle");
        ViewBag.Brands = new SelectList(_mediator.Send(new GetBrandsQuery()).Result.Data, "BrandId", "BrandName");
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditProductCommand command)
    {
        FormFileCollection images = new FormFileCollection();

        foreach (var file in Request.Form.Files)
        {
            images.Add(file);
        }
        command.Images = images;
        var result = await _mediator.Send(command);
        return Json(result);
    }
}
