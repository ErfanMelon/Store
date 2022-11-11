using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Store.Application.Services.Products.Commands.AddCategory;
using Store.Application.Services.Products.Commands.EditCategory;
using Store.EndPoint.Areas.Admin.Models.ViewModels;

namespace Store.EndPoint.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Operator")]
    public partial class ProductController : Controller
    {
        public IActionResult CategoryList()
        {
            return View(_productFacade.getCategoriesService.Execute());
        }

        public IActionResult AddCategory()
        {
            ViewBag.Categorylist = new SelectList(_productFacade.getCategoriesService.Execute().Data.Where(c => c.Parent==null), "CategoryId", "CategoryTitle");
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
            var categorylist = new SelectList(_productFacade.getCategoriesService.Execute().Data.Where(c => c.Parent == null), "CategoryId", "CategoryTitle");
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
    }
}
