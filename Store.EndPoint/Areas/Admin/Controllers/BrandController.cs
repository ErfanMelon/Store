using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Store.EndPoint.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Operator")]
    public partial class ProductController : Controller
    {
        public IActionResult BrandList()
        {
            var result = _productFacade.getBrandsService.Execute();
            if (result.IsSuccess)
            {
                return View(result.Data);
            }
            return BadRequest();
        }
        [HttpPost]
        public IActionResult AddBrand(string brandName)
        {
            return Json(_productFacade.addBrandService.Execute(brandName));
        }
        [HttpPost]
        public IActionResult DeleteBrand(int brandId)
        {
            return Json(_productFacade.deleteBrandService.Execute(brandId));
        }
    }
}
