using Microsoft.AspNetCore.Mvc;
using Store.Application.Interfaces.FacadePatterns;

namespace Store.EndPoint.ViewComponents
{
    public class Comments:ViewComponent
    {
        private readonly IProductFacade _productFacade;
        public Comments(IProductFacade productFacade)
        {
            _productFacade = productFacade;
        }
        public IViewComponentResult Invoke(long productId)
        {
            var result = _productFacade.getProductCommentsService.Execute(productId, 1, 30);
            return View("Comments",result.Data);
        }
    }
}
