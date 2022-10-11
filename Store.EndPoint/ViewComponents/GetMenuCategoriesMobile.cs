using Microsoft.AspNetCore.Mvc;
using Store.Application.Services.Common.Queries.GetMenuCategories;

namespace Store.EndPoint.ViewComponents
{
    public class GetMenuCategoriesMobile : ViewComponent
    {
        private readonly IGetMenuCategoriesService _getMenuCategoriesService;
        public GetMenuCategoriesMobile(IGetMenuCategoriesService getMenuCategoriesService)
        {
            _getMenuCategoriesService = getMenuCategoriesService;
        }
        public IViewComponentResult Invoke()
        {
            return View("GetMenuCategoriesMobile", _getMenuCategoriesService.Execute().Data);
        }
    }
}
