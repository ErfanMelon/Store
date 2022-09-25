using Store.Common.Dto;

namespace Store.Application.Services.Products.Queries.GetCategory
{
    public interface IGetCategoryDetailService
    {
        ResultDto<CategoryDetailDto> Execute(long categoryId);
    }


}
