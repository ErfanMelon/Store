using Microsoft.AspNetCore.Mvc;
using Store.Application.Interfaces.FacadePatterns;

namespace Store.EndPoint.ViewComponents
{
    public class HomePageProducts:ViewComponent
    {
        private readonly IProductFacade _productFacade;
        public HomePageProducts(IProductFacade productFacade)
        {
            _productFacade = productFacade;
        }
        public IViewComponentResult Invoke(long categoryId)
        {
            ViewBag.Category = _productFacade.getCategoryDetailService.Execute(categoryId).Data.CategoryTitle;
            return View("HomePageProducts", _productFacade.getProductsSite.Execute(1, categoryId, "",7,Application.Services.Products.Queries.GetProductsSite.Order.NotOrdered).Data);
        }
    }
}
