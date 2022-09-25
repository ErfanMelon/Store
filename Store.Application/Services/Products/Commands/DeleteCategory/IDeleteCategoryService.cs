using Store.Common.Dto;

namespace Store.Application.Services.Products.Commands.DeleteCategory
{
    public interface IDeleteCategoryService
    {
        ResultDto Execute(long categoryId);
    }
}
