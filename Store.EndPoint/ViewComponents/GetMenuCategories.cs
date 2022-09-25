using Microsoft.AspNetCore.Mvc;
using Store.Application.Services.Common.Queries.GetMenuCategories;

namespace Store.EndPoint.ViewComponents
{
    public class GetMenuCategories:ViewComponent
    {
        private readonly IGetMenuCategoriesService _getMenuCategoriesService;
        public GetMenuCategories(IGetMenuCategoriesService getMenuCategoriesService)
        {
            _getMenuCategoriesService = getMenuCategoriesService;
        }
        public IViewComponentResult Invoke()
        {
            return View("GetMenuCategories", _getMenuCategoriesService.Execute().Data);
        }
    }
}
