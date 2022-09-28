using Microsoft.AspNetCore.Mvc;
using Store.Application.Interfaces.FacadePatterns;
using Store.Application.Services.Products.Queries.GetProductsSite;

namespace Store.EndPoint.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductFacade _productFacade;
        public ProductController(IProductFacade productFacade)
        {
            _productFacade = productFacade;
        }

        public IActionResult Index(string SearchKey,int page = 1,long? CategoryId=null,int pagesize=10,OrderProduct order=OrderProduct.NotOrdered)
        {
            var Result = _productFacade.getProductsSite.Execute(page,CategoryId,SearchKey,pagesize,order);
            if (Result.IsSuccess)
            {
                return View(Result.Data);
            }
            return BadRequest();
        }
        public IActionResult Detail(long Id)
        {
            var Result=_productFacade.getProductSiteService.Execute(Id);
            if (Result.IsSuccess)
            {
                return View(Result.Data);
            }
            return BadRequest();
        }
    }
}
