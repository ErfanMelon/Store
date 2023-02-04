using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Store.Application.Interfaces.FacadePatterns;
using Store.Application.Services.Products.Queries.GetCategories;

namespace Store.EndPoint.ViewComponents
{
    public class Search : ViewComponent
    {
        private readonly IProductFacade _productFacade;
        private readonly IMediator _mediator;
        public Search(IProductFacade productFacade, IMediator mediator)
        {
            _productFacade = productFacade;
            _mediator = mediator;
        }
        public IViewComponentResult Invoke()
        {
            ViewBag.Categories = new SelectList(_mediator.Send(new GetCategoriesQuery()).Result.Data.Where(c => c.Parent!=null), "CategoryId", "CategoryTitle");
            return View("Search");
        }
    }
}
