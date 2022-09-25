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
    [Area("Admin")]
    public class ProductController : Controller
    {

        private readonly IProductFacade _productFacade;
        public ProductController(IProductFacade productFacade)
        {
            _productFacade = productFacade;
        }

        public IActionResult CategoryList()
        {
            return View(_productFacade.getCategoriesService.Execute());
        }


        public IActionResult AddCategory()
        {
            ViewBag.Categorylist = new SelectList(_productFacade.getCategoriesService.Execute().Data.Where(c=>c.HasChild), "CategoryId", "CategoryTitle");
            return PartialView();
        }
        [HttpPost]
        public IActionResult AddCategory(CategoryViewModel viewModel)
        {
            var resultAddCategory = _productFacade.addCategoryService.Execute(new AddCategoryDto
            {
                CategoryTitle = viewModel.CategoryTitle,
                ParentCategoryId = viewModel.ParentCategoryId,
            });
            return Json(resultAddCategory);
        }
        [HttpPost]
        public IActionResult EditCategoryPage(CategoryViewModel viewModel)
        {
            var categorylist = new SelectList(_productFacade.getCategoriesService.Execute().Data.Where(c => c.HasChild), "CategoryId", "CategoryTitle");
            ViewBag.Categorylist = categorylist;
            TempData["CurrentParentCategory"] = viewModel.ParentCategoryId != 0 ? categorylist.FirstOrDefault(p => p.Value == viewModel.ParentCategoryId.ToString()).Text : "-";
            return PartialView("EditCategory", viewModel);
        }
        [HttpPost]
        public IActionResult EditCategory(CategoryViewModel viewModel)
        {
            var resultEditCategory = _productFacade.editCategoryService.Execute(new EditCategoryDto
            {
                CategoryId = viewModel.CategoryId,
                CategoryTitle = viewModel.CategoryTitle,
                ParentCategoryId = viewModel.ParentCategoryId,
            });
            return Json(resultEditCategory);
        }
        [HttpPost]
        public IActionResult DeleteCategory(long categoryId)
        {
            var res = _productFacade.deleteCategoryService.Execute(categoryId);
            return Json(res);
        }
        [HttpPost]
        public IActionResult DetailCategory(long categoryId)
        {
            var res = _productFacade.getCategoryDetailService.Execute(categoryId);
            return PartialView(res.Data);
        }
        public IActionResult AddProduct()
        {
            ViewBag.Categories = new SelectList(_productFacade.getCategoriesService.Execute().Data.Where(c => !c.HasChild), "CategoryId", "CategoryTitle");
            return View();
        }
        [HttpPost]
        public IActionResult AddProduct(RequestProductDto request, List<RequestFeatureDto> Features)
        {
            List<IFormFile> images = new List<IFormFile>();
            for (int i = 0; i < Request.Form.Files.Count; i++)
            {
                var file = Request.Form.Files[i];
                images.Add(file);
            }
            request.Images = images;
            request.ProductFeatures = Features;

            var resultAddProduct = _productFacade.addProductService.Execute(request);
            return Json(resultAddProduct);
        }

        public IActionResult ProductList(int page = 1, int pagesize = 20)
        {
            return View(_productFacade.getProductsAdminService.Execute(page, pagesize).Data);
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
            TempData["productId"] = productid;
            return View(product.Data);
        }
        public IActionResult EditProduct(long productId)
        {
            var product = _productFacade.getProductEditService.Execute(productId);
            if (product.IsSuccess)
            {
                ViewBag.Categories = new SelectList(_productFacade.getCategoriesService.Execute().Data.Where(c => !c.HasChild), "CategoryId", "CategoryTitle");
                TempData["ProductId"] = productId;
                return View(product.Data);
            }
            return BadRequest();
        }
        [HttpPost]
        public IActionResult EditProduct(RequestEditProductDto request, List<RequestFeatureDto> Features)
        {
            List<IFormFile> images = new List<IFormFile>();
            for (int i = 0; i < Request.Form.Files.Count; i++)
            {
                var file = Request.Form.Files[i];
                images.Add(file);
            }
            request.Images = images;
            request.ProductFeatures = Features;
            var resultEditProduct = _productFacade.editProductService.Execute(request);
            return Json(resultEditProduct);
        }
    }
}
