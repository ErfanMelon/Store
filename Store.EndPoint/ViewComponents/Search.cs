using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Store.Application.Services.Products.Queries.GetCategories;
using Store.EndPoint.Models;

namespace Store.EndPoint.ViewComponents;

public class Search : ViewComponent
{
    private readonly IMediator _mediator;
    public Search(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var categories = await _mediator.Send(new GetCategoriesQuery());
        List<SelectListItem> selectlist = new List<SelectListItem>();
        selectlist = categories.Data
            .Select(e =>
            new SelectListItem(e.CategoryTitle, e.CategoryId.ToString()))
            .Prepend(new SelectListItem("همه دسته ها",null,true))
            .ToList();
        SearchViewModel model = new SearchViewModel
        {
            Categories = new SelectList(selectlist,"Value","Text")
        };
        return View("Search", model);
    }
}
