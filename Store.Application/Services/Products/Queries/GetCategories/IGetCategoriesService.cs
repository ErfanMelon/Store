using Store.Common.Dto;

namespace Store.Application.Services.Products.Queries.GetCategories
{
    public interface IGetCategoriesService
    {
        ResultDto<List<CategoryDto>> Execute();
    }
}
