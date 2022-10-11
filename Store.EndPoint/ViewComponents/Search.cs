using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Store.Application.Interfaces.FacadePatterns;
using Store.Application.Services.Products.Queries.GetCategories;

namespace Store.EndPoint.ViewComponents
{
    public class Search : ViewComponent
    {
        private readonly IProductFacade _productFacade;
        public Search(IProductFacade productFacade)
        {
            _productFacade = productFacade;
        }
        public IViewComponentResult Invoke()
        {
            ViewBag.Categories = new SelectList(_productFacade.getCategoriesService.Execute().Data.Where(c => c.Parent!=null), "CategoryId", "CategoryTitle");
            return View("Search");
        }
    }
}
