using Store.Common.Dto;

namespace Store.Application.Services.Products.Commands.AddCategory
{
    public interface IAddCategoryService
    {
        ResultDto Execute(AddCategoryDto request);
    }
}
