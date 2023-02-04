using MediatR;
using Microsoft.AspNetCore.Mvc;
using Store.Application.Interfaces.FacadePatterns;
using Store.Application.Services.Products.Queries.GetCategories;
using Store.Application.Services.Products.Queries.GetCategory;

namespace Store.EndPoint.ViewComponents
{
    public class HomePageProducts:ViewComponent
    {
        private readonly IProductFacade _productFacade;
        private readonly IMediator _mediator;
        public HomePageProducts(IProductFacade productFacade, IMediator mediator)
        {
            _productFacade = productFacade;
            _mediator = mediator;
        }
        public IViewComponentResult Invoke(long categoryId)
        {
            ViewBag.Category = _mediator.Send(new GetCategoryQuery(categoryId)).Result.Data?.CategoryTitle;
            return View("HomePageProducts", _productFacade.getProductsSite.Execute(1, categoryId, "",7,Application.Services.Products.Queries.GetProductsSite.Order.NotOrdered).Data);
        }
    }
}
