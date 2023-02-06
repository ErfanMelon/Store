using MediatR;
using Microsoft.AspNetCore.Mvc;
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
    public IViewComponentResult Invoke(long categoryId)
    {
        ViewBag.Category = _mediator.Send(new GetCategoryQuery(categoryId)).Result.Data?.CategoryTitle;
        return View("HomePageProducts", _mediator.Send(new GetProductsSiteQuery(1, 8, categoryId)).Result);
    }
}
