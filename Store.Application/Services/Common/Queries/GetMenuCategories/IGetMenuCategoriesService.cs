using Store.Common.Dto;

namespace Store.Application.Services.Common.Queries.GetMenuCategories
{
    public interface IGetMenuCategoriesService
    {
        ResultDto<List<MenuCategoriesDto>> Execute();
    }
}
