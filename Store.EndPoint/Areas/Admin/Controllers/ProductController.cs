using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Store.Application.Interfaces.FacadePatterns;
using Store.Application.Services.Products.Commands.AddCategory;
using Store.Application.Services.Products.Commands.AddProduct;
using Store.Application.Services.Products.Commands.EditCategory;
using Store.Application.Services.Products.Commands.EditProduct;
using Store.EndPoint.Areas.Admin.Models.ViewModels;

namespace Store.EndPoint.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Operator")]
    [Area("Admin")]
    public partial class ProductController : Controller
    {

        private readonly IProductFacade _productFacade;
        public ProductController(IProductFacade productFacade)
        {
            _productFacade = productFacade;
        }

        public IActionResult AddProduct()
        {
            ViewBag.Categories = new SelectList(_productFacade.getCategoriesService.Execute().Data.Where(c => c.Parent!=null), "CategoryId", "CategoryTitle");
            ViewBag.Brands = new SelectList(_productFacade.getBrandsService.Execute().Data, "BrandId", "BrandName");
            return View();
        }
        [HttpPost]
        public IActionResult AddProduct(RequestProductDto request, List<RequestFeatureDto> Features)
        {
            List<IFormFile> images = new List<IFormFile>();
            
            foreach (var file in Request.Form.Files)
            {
                images.Add(file);
            }
            request.Images = images;
            request.ProductFeatures = Features;

            var resultAddProduct = _productFacade.addProductService.Execute(request);
            return Json(resultAddProduct);
        }

        public IActionResult ProductList(int page = 1, int pagesize = 10,string searchKey=null)
        {
            return View(_productFacade.getProductsAdminService.Execute(page, pagesize,searchKey).Data);
        }
        public IActionResult DeleteProduct(long productid)
        {
            return Json(_productFacade.deleteProductService.Execute(productid));
        }
        public IActionResult DetailProduct(long productid)
        {
            var product = _productFacade.getProductAdminService.Execute(productid);
            if (product.IsSuccess == false)
            {
                return BadRequest();
            }
            return View(product.Data);
        }
        public IActionResult EditProduct(long productId)
        {
            var product = _productFacade.getProductEditService.Execute(productId);
            if (product.IsSuccess)
            {
                ViewBag.Categories = new SelectList(_productFacade.getCategoriesService.Execute().Data.Where(c => c.Parent != null), "CategoryId", "CategoryTitle");
                ViewBag.Brands = new SelectList(_productFacade.getBrandsService.Execute().Data, "BrandId", "BrandName");
                TempData["ProductId"] = productId;
                return View(product.Data);
            }
            return BadRequest();
        }
        [HttpPost]
        public IActionResult EditProduct(RequestEditProductDto request, List<RequestFeatureDto> Features)
        {
            List<IFormFile> images = new List<IFormFile>();
            foreach (var file in Request.Form.Files)
            {
                images.Add(file);
            }
            request.Images = images;
            request.ProductFeatures = Features;
            var resultEditProduct = _productFacade.editProductService.Execute(request);
            return Json(resultEditProduct);
        }
    }
}
