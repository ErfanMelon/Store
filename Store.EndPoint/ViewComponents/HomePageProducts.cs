using MediatR;
using Microsoft.AspNetCore.Mvc;
using Store.Application.Services.Products.Queries.GetCategories;
using Store.Application.Services.Products.Queries.GetCategory;
using Store.Application.Services.Products.Queries.GetProductsSite;

namespace Store.EndPoint.ViewComponents;

public class HomePageProducts : ViewComponent
{
    private readonly IMediator _mediator;
    public HomePageProducts(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task<IViewComponentResult> InvokeAsync(long categoryId)
    {
        var category = await _mediator.Send(new GetCategoryQuery(categoryId));

        if (!category.IsSuccess)
            return null;

        ViewBag.Category= category.Data.CategoryTitle;
        return View("HomePageProducts", _mediator.Send(new GetProductsSiteQuery(1, 8, categoryId)).Result);
    }
}
